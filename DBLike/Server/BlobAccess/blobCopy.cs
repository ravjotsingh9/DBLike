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
        private string srcAccountName = "portalvhdscgcgqr43r8dpq";
        private string srcAccountKey = "q7u0mob+u/kgqTlIe4lfKa6xmo4Cm/rT6VlmBd2EwWZTy3tq2h3LeHFwQandnM7AIunrH0n65et3dKAX9LghBA==";
        private string destAccountName = "portalvhdstj44khd5kg1p9";
        private string destAccountKey = "fS6etqKHnyQuYMtxURvhFEtpAUasAT+iKS98+Y9fFctgAusnN+gsetygq0ZerTrCPL1Cc7Y/NS8JAIw7MzNlZA==";


        public void startCopyBlob()
        {

            BlobConn conn = new BlobConn();
            var srcBlobClient = conn.BlobConnect();
            if (conn.currentBlob == 1)
            {
                var dstBlobClient = conn.getSpecifyClient(2);
                if (dstBlobClient != null)
                {
                    Copy(srcBlobClient, dstBlobClient);
                }
                dstBlobClient = conn.getSpecifyClient(3);
                if (dstBlobClient != null)
                {
                    Copy(srcBlobClient, dstBlobClient);
                }
            }
            else if (conn.currentBlob == 2)
            {
                var dstBlobClient = conn.getSpecifyClient(3);
                if (dstBlobClient != null)
                {
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
                    var dstCloudBlobContainer = dstBlobClient
                 .GetContainerReference(srcCloudBlobContainer.Name);

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
            
    }
}
