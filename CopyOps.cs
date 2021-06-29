using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Medusa
{
    class CopyOps
    {
        private readonly CloudStorageAccount cloudStorageAccount;
        private readonly CloudBlobClient cloudBlobClient;
        private readonly CloudQueueClient cloudQueueClient;
        private CloudBlobContainer cloudContainer;

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

            SharedAccessBlobPermissions permissions = SharedAccessBlobPermissions.Read;

            TimeSpan clockSkew = TimeSpan.FromMinutes(15d);
            TimeSpan accessDuration = TimeSpan.FromMinutes(15d);

            string serializedMessage = JsonConvert.SerializeObject(message);
            Console.WriteLine(serializedMessage);
            
            foreach(string filePath in message.content)
            {
                string key = Path.GetFileName(filePath);
                CopyOps.uploadBlob(cloudContainer, key, filePath, true);
            }

            var blobSAS = new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = DateTime.UtcNow.Subtract(clockSkew),
                SharedAccessExpiryTime = DateTime.UtcNow.Add(accessDuration) + clockSkew,
                Permissions = permissions
            };

            foreach (string filePath in message.content)
            {
                string key = Path.GetFileName(filePath);

                var blobRef = cloudContainer.GetBlockBlobReference(key);
                var sasBlobToken = blobRef.GetSharedAccessSignature(blobSAS);

                var blobUri = blobRef.Uri.AbsoluteUri;
                var queueUrl = blobUri + sasBlobToken;                
            }

            var blobRef2 = cloudContainer.GetBlockBlobReference("Shanthi Insurance Policy.pdf");
            var blockBlob = cloudContainer.GetBlockBlobReference("Shanthi Insurance Policy.pdf");

            var downloadsPathOnServer = Path.Combine("C:\\SEM 2", "Shanthi Insurance Policy.pdf");

            using (var fileStream = File.OpenWrite(downloadsPathOnServer))
            {
                await blockBlob.DownloadToStreamAsync(fileStream);
            }

            var test = cloudContainer.ListBlobs("Shanthi Insurance Policy.pdf");

            foreach(var item in test)
            {
                Console.WriteLine(item);
            }
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
