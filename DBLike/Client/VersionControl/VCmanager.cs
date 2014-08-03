using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Client.VersionControl
{
    public partial class VCmanager
    {
        private Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient client;
        Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer container;
        public VCmanager()
        {
            //CloudBlobContainer container = new CloudBlobContainer(new Uri(sasUri));
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            Microsoft.WindowsAzure.Storage.CloudStorageAccount account = new Microsoft.WindowsAzure.Storage.CloudStorageAccount(creds, useHttps: true);

            this.client = account.CreateCloudBlobClient();

            this.container = this.client.GetContainerReference("qqremoteyes");
            this.container.CreateIfNotExists();
        }
        
        
        public CloudBlockBlob getLatestSnapshot(CloudBlockBlob file)
        {
            CloudBlockBlob blobSnapshot = null;

            string blobPrefix = null;
            bool useFlatBlobListing = true;

            var snapshots = container.ListBlobs(blobPrefix, useFlatBlobListing,
            BlobListingDetails.Snapshots).Where(item => ((CloudBlockBlob)item).SnapshotTime.HasValue && item.Uri.Equals(file.Uri)).ToList<IListBlobItem>();
            int versons = snapshots.Count;
            if (versons < 1)
            {
                Console.WriteLine("snapshot was not created");
            }
            else
            {
                blobSnapshot = (CloudBlockBlob)snapshots[versons - 1];
            }

            return blobSnapshot;
        }

        public void deleteSnapshot(CloudBlockBlob blobRef)
        {
            string blobPrefix = null;
            bool useFlatBlobListing = true;

            var snapshots = container.ListBlobs(blobPrefix, useFlatBlobListing,
            BlobListingDetails.Snapshots).Where(item => ((CloudBlockBlob)item).SnapshotTime.HasValue && item.Uri.Equals(blobRef.Uri)).ToList<IListBlobItem>();

            if (snapshots.Count < 1)
            {
                Console.WriteLine("snapshot was not created");
            }
            else
            {
                foreach (IListBlobItem item in snapshots)
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    blob.DeleteIfExists();
                    Console.WriteLine("Delete Name: {0}, Timestamp: {1}", blob.Name, blob.SnapshotTime);
                    Console.WriteLine("success");
                }
                //snapshots.ForEach(item => Console.WriteLine(String.Format("{0} {1} ", ((CloudBlockBlob)item).Name, ((CloudBlockBlob)item).Metadata["timestamp"])));
            }
        }
    }
}
