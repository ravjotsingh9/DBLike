using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Server.ConnectionManager;

namespace Server.BlobAccess
{
    class blobCopy
    {

        public void startCopyBlob()
        {

            BlobConn conn = new BlobConn();
            var srcBlobClient = conn.BlobConnect();
            if (conn.currentBlob == 1)
            {
                var dstBlobClient = conn.getSpecifyClient(2);
                if (dstBlobClient != null)
                {
                    delete(srcBlobClient, dstBlobClient);
                    Copy(srcBlobClient, dstBlobClient);
                }
                dstBlobClient = conn.getSpecifyClient(3);
                if (dstBlobClient != null)
                {
                    delete(srcBlobClient, dstBlobClient);
                    Copy(srcBlobClient, dstBlobClient);
                }
            }
            else if (conn.currentBlob == 2)
            {
                var dstBlobClient = conn.getSpecifyClient(3);
                if (dstBlobClient != null)
                {
                    delete(srcBlobClient, dstBlobClient);
                    Copy(srcBlobClient, dstBlobClient);
                }

            }

            Console.WriteLine("done sync blob");

        }

        private void Copy(CloudBlobClient srcBlobClient, CloudBlobClient dstBlobClient)
        {
            foreach (var srcCloudBlobContainer in srcBlobClient.ListContainers())
            {
                if (srcCloudBlobContainer.Name != "vhds")
                {
                    var dstCloudBlobContainer = dstBlobClient.GetContainerReference(srcCloudBlobContainer.Name);

                    dstCloudBlobContainer.CreateIfNotExists();

                    //Assuming the source blob container ACL is "Private", let's create a Shared Access Signature with
                    //Start Time = Current Time (UTC) - 15 minutes to account for Clock Skew
                    //Expiry Time = Current Time (UTC) + 7 Days - 7 days is the maximum time allowed for copy operation to finish.
                    //Permission = Read so that copy service can read the blob from source
                    var sas = srcCloudBlobContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                        SharedAccessExpiryTime = DateTime.UtcNow.AddDays(7),
                        Permissions = SharedAccessBlobPermissions.Read,
                    });
                    foreach (var srcBlob in srcCloudBlobContainer.ListBlobs())
                    {
                        if (srcBlob.GetType() == typeof(CloudBlockBlob))
                        {
                            var srcBlockBlock = (CloudBlockBlob)srcBlob;
                            var dstBlockBlock = dstCloudBlobContainer
                                .GetBlockBlobReference(srcBlockBlock.Name);
                            //Create a SAS URI for the blob
                            var srcBlockBlobSasUri = string.Format("{0}{1}", srcBlockBlock.Uri, sas);
                            // throws exception StorageException:
                            // The remote server returned an error: (404) Not Found.
                            dstBlockBlock.StartCopyFromBlob(new Uri(srcBlockBlobSasUri));
                        }
                    }

                }

            }
        }

        private void delete(CloudBlobClient srcBlobClient, CloudBlobClient dstBlobClient)
        {
            foreach (var dstCloudBlobContainer in dstBlobClient.ListContainers())
            {
                if (dstCloudBlobContainer.Name != "vhds")
                {
                    var srcCloudBlobContainer = srcBlobClient.GetContainerReference(dstCloudBlobContainer.Name);
                    if (!srcCloudBlobContainer.Exists())
                    {
                        dstCloudBlobContainer.Delete();
                    }
                    else
                    {
                        foreach (var dstBlob in dstCloudBlobContainer.ListBlobs())
                        {
                            if (dstBlob.GetType() == typeof(CloudBlockBlob))
                            {
                                var dstBlockBlob = (CloudBlockBlob)dstBlob;
                                var srcBlockBlob = srcCloudBlobContainer.GetBlockBlobReference(dstBlockBlob.Name);
                                if (!srcBlockBlob.Exists())
                                {
                                    dstBlockBlob.Delete();
                                }
                            }
                        }

                    }
                    

                    

                }

            }
        }
            
    }
}
