using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Server.ConnectionManager
{
    public partial class GenerateSAS
    {
        //private CloudBlobClient blobclient;

        /// <summary>
        /// Get Container's SaS Uri
        /// </summary>
        /// <param name="blobclient"></param>
        /// <param name="container"></param>
        /// <param name="policyName"></param>
        /// <returns>Uri for container</returns>
        public string GetContainerSasUri(CloudBlobContainer container, string permissionType)
        {
            SharedAccessBlobPolicy sasConstraints = CreateSASPermission(permissionType);

                    
            //Generate the shared access signature on the container. In this case, all of the constraints for the 
            //shared access signature are specified on the stored access policy.
            string sasContainerToken = container.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            return container.Uri + sasContainerToken;
        }

        /// <summary>
        /// Get blob's SaS Uri
        /// </summary>
        /// <param name="blob"></param>
        /// <param name="permissionType"></param>
        /// <returns></returns>
        public string GetContainerBlobUri(CloudBlockBlob blob, string permissionType)
        {
            SharedAccessBlobPolicy sasConstraints = CreateSASPermission(permissionType);

            //Generate the shared access signature on the blob, setting the constraints directly on the signature.
            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            return blob.Uri + sasBlobToken;
        }
        

    }
}
