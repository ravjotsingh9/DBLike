using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Server.BlobAccess
{
    public class Blob
    {
        /// <summary>
        /// Get client's container in the Blob
        /// </summary>
        /// <param name="blobClient">use BlobConn to get blobClient</param>
        /// <param name="clientContainerName">This should be the ID of the User</param>
        /// <returns></returns>
        public CloudBlobContainer getClientContainer(CloudBlobClient blobClient,string clientContainerName)
        {
           //Get a reference to a container
            CloudBlobContainer container = blobClient.GetContainerReference(clientContainerName);

            return container;
        }
    }
}
