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
        /// Get Container's Sas Uri
        /// </summary>
        /// <param name="blobclient"></param>
        /// <param name="container"></param>
        /// <param name="policyName"></param>
        /// <returns>Uri for container</returns>
        public string GetContainerSasUri(CloudBlobClient blobclient, CloudBlobContainer container, string policyName)
        {
            
            if (policyName == "RWL")
            {
                CreateSASRWL(blobclient, container, policyName);

            }
            
            //Generate the shared access signature on the container. In this case, all of the constraints for the 
            //shared access signature are specified on the stored access policy.
            string sasContainerToken = container.GetSharedAccessSignature(null, policyName);

            //Return the URI string for the container, including the SAS token.
            return container.Uri + sasContainerToken;
        }

        

    }
}
