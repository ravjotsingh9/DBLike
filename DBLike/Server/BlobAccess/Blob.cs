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
    public partial class Blob
    {
        public string hashValue { get; set; }
        public string timestamp { get; set; }
        public string filePath { get; set; }
        public CloudBlobContainer container { get; set; }
        public CloudBlockBlob blob { get; set; }
        public bool ifBlobExist { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="blobClient">use BlobConn to get blobClient</param>
        /// <param name="clientContainerName">This should be the ID of the User</param>
        /// <returns></returns>
        public Blob(CloudBlobClient blobClient, string clientContainerName)
        {
            //Get a reference to a container and create container for first time use user
            container = blobClient.GetContainerReference(clientContainerName);
            container.CreateIfNotExists();
        }

        public Blob(CloudBlobClient blobClient, string clientContainerName, string filePathInSynFolder)
        {
            
            container = blobClient.GetContainerReference(clientContainerName);
            container.CreateIfNotExists();
            blob = container.GetBlockBlobReference(filePathInSynFolder);
            if (blob.Exists())
            {
                ifBlobExist = true;
                blob.FetchAttributes();
                hashValue = blob.Metadata["hashValue"];
                timestamp = blob.Metadata["timestamp"];
                filePath = blob.Metadata["filePath"];
            }
            else
            {
                ifBlobExist = false;
            }

        }



    }
}
