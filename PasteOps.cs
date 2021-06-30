namespace Medusa
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    class PasteOps
    {
        private readonly CloudStorageAccount cloudStorageAccount;
        private readonly CloudBlobClient cloudBlobClient;
        private readonly CloudQueueClient cloudQueueClient;
        private readonly CloudBlobContainer cloudContainer;
        private readonly static string containerName = Program.fileContainerName;
        private readonly static string dataBaseName = Program.dataBaseName;
        public PasteOps()
        {
            string connString = StorageConfigHandler.StorageConnectionString();
            string destContainer = StorageConfigHandler.DestContainer();
            string queueName = StorageConfigHandler.Queue();
            try
            {
                this.cloudStorageAccount = CloudStorageAccount.Parse(connString);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(string.Format("connection string format exception: {0}", ex));
                return;
            }


            try
            {
                this.cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Unhandled exception encountered while creating blob client {0}", ex));
                return;
            }


            try
            {
                this.cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();

            }
            catch (Exception ex)
            {

                Console.WriteLine(string.Format("Unhandled exception encountered while creating cloud queue client {0}", ex));
                return;
            }

            try
            {
                CloudQueue queue = cloudQueueClient.GetQueueReference(queueName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Unhandled exception encountered while creating cloud queue client {0}", ex));
                return;
            }

            try
            {
                this.cloudBlobClient.GetContainerReference(destContainer);

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Unhandled exception encountered while container reference {0}", ex));
                throw;
            }

            try
            {
                this.cloudContainer = cloudBlobClient.GetContainerReference(destContainer);

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Unhandled exception encountered while container reference {0}", ex));
                throw;
            }
        }

        public async Task PasteWorkItem()
        {
            await ReceivePaste();
        }

        private async Task ReceivePaste()
        {
            string tempFolder = Path.GetTempPath();
            DocumentClient client;
            var option = new FeedOptions { EnableCrossPartitionQuery = true };
            client = new DocumentClient(new Uri(Shared.EndPoint()), Shared.PrimaryKey());
            Document doc = client.CreateDocumentQuery<Document>(UriFactory.CreateDocumentCollectionUri(dataBaseName, containerName), option)
                        .Where(r => r.Id == "commonFiles")
                        .AsEnumerable()
                        .SingleOrDefault();
            var filesNames = doc.GetPropertyValue<JArray>("files");
            //string[] filesNamesString = filesNames.ToObject<string[]>();
            StringCollection paths = new StringCollection();
            var semaphore = new SemaphoreSlim(100);
            List<Task> downloadFiles = new List<Task>();
            foreach(var fileName in filesNames)
            {
                var fileNameString = fileName.ToObject<string>();
                await semaphore.WaitAsync();
                downloadFiles.Add(Task.Run(async () =>
                {
                    var blockBlob = cloudContainer.GetBlockBlobReference(fileNameString);
                    var downloadsPathOnServer = Path.Combine(tempFolder, fileNameString);
                    paths.Add(tempFolder + fileNameString);
                    using (var fileStream = File.OpenWrite(downloadsPathOnServer))
                    {
                        await blockBlob.DownloadToStreamAsync(fileStream);
                    }
                    semaphore.Release();
                }));
            }
            await Task.WhenAll(downloadFiles);            
            Clipboard.SetFileDropList(paths);          
        }
    }
}
