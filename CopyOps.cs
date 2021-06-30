using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Medusa
{
    class CopyOps
    {
        private readonly CloudStorageAccount cloudStorageAccount;
        private readonly CloudBlobClient cloudBlobClient;
        private readonly CloudQueueClient cloudQueueClient;
        private readonly CloudBlobContainer cloudContainer;
        private readonly static string containerName = Program.fileContainerName;
        private readonly static string dataBaseName = Program.dataBaseName;
        public CopyOps()
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

        public async Task copyWorkItem(WorkItemMessage message)
        {
            
            await makeHeader(message);
        }

        public async Task PasteWorkItem(string message)
        {
            await breakHeader(message);
        }

        private async Task makeHeader(WorkItemMessage message)
        {
            await cloudContainer.CreateIfNotExistsAsync();
            List<string> fileNames = new List<string>();           
            //Upload the files
            foreach(string filePath in message.content)
            {
                string key = Path.GetFileName(filePath);
                fileNames.Add(key);
                CopyOps.uploadBlob(cloudContainer, key, filePath, true);
            }        
            //Update the files array
            DocumentClient client;
            var option = new FeedOptions { EnableCrossPartitionQuery = true };
            client = new DocumentClient(new Uri(Shared.EndPoint()), Shared.PrimaryKey());           
            Document doc = client.CreateDocumentQuery<Document>(UriFactory.CreateDocumentCollectionUri(dataBaseName, containerName), option)
                        .Where(r => r.Id == "commonFiles")
                        .AsEnumerable()
                        .SingleOrDefault();
            doc.SetPropertyValue("files", fileNames);
            await client.ReplaceDocumentAsync(doc.SelfLink, doc);


            //var container = Shared.Client.GetContainer(dataBaseName, containerName);

            //SharedAccessBlobPermissions permissions = SharedAccessBlobPermissions.Read;
            //TimeSpan clockSkew = TimeSpan.FromMinutes(15d);
            //TimeSpan accessDuration = TimeSpan.FromMinutes(15d);

            //string serializedMessage = JsonConvert.SerializeObject(message);
            //Console.WriteLine(serializedMessage);


            //var blobSAS = new SharedAccessBlobPolicy
            //{
            //    SharedAccessStartTime = DateTime.UtcNow.Subtract(clockSkew),
            //    SharedAccessExpiryTime = DateTime.UtcNow.Add(accessDuration) + clockSkew,
            //    Permissions = permissions
            //};

            //foreach (string filePath in message.content)
            //{
            //    string key = Path.GetFileName(filePath);

            //    var blobRef = cloudContainer.GetBlockBlobReference(key);
            //    var sasBlobToken = blobRef.GetSharedAccessSignature(blobSAS);

            //    var blobUri = blobRef.Uri.AbsoluteUri;
            //    var queueUrl = blobUri + sasBlobToken;                
            //}

            //var blobRef2 = cloudContainer.GetBlockBlobReference("AkshayKumar_CV_2021.pdf");
            //var blockBlob = cloudContainer.GetBlockBlobReference("AkshayKumar_CV_2021.pdf");

            //var downloadsPathOnServer = Path.Combine("C:\\SEM 3", "AkshayKumar_CV_2021.pdf");

            //using (var fileStream = File.OpenWrite(downloadsPathOnServer))
            //{
            //    await blockBlob.DownloadToStreamAsync(fileStream);
            //}

            //var test = cloudContainer.ListBlobs("AkshayKumar_CV_2021.pdf");

            //foreach(var item in test)
            //{
            //    Console.WriteLine(item);
            //}
        }


        private static async void uploadBlob(CloudBlobContainer container, string key, string fileName, bool deleteAfter)
        {
            CloudBlockBlob b = container.GetBlockBlobReference(key);

            using(var fs=System.IO.File.Open(fileName,FileMode.Open,FileAccess.Read,FileShare.None))
            {
                await b.UploadFromStreamAsync(fs);
            }
        }

        private static async Task breakHeader(string header)
        {
            WorkItemMessage message = JsonConvert.DeserializeObject<WorkItemMessage>(header);
            foreach(var itr in message.content)
            {
                Clipboard.SetText(itr);
            }
            //Clipboard.SetText(message.content);
        }
    }
}
