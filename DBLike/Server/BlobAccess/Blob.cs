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
        public DateTime timestamp { get; set; }
        public string filePath { get; set; }
        public CloudBlobContainer container { get; set; }
        public CloudBlockBlob blob { get; set; }
        public bool ifBlobExist { get; set; }
        public bool isHashSame { get; set; }
        public bool isTimestampLater { get; set; }
        public bool isDirectory { get; set; }

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

        public Blob(CloudBlobClient blobClient, string clientContainerName, string filePathInSynFolder, string fileHashValue, DateTime fileTimestamp)
        {
            
            container = blobClient.GetContainerReference(clientContainerName);
            container.CreateIfNotExists();
            blob = container.GetBlockBlobReference(filePathInSynFolder);
            if (fileHashValue == "isDirectory")
            {
                isDirectory = true;
            }
            if (blob.Exists() && isDirectory==false)
            {
                ifBlobExist = true;
                
                blob.FetchAttributes();
                hashValue = blob.Metadata["hashValue"];
                timestamp = DateTime.ParseExact(blob.Metadata["timestamp"], "MM/dd/yyyy HH:mm:ss",
                                                null);;
                filePath = blob.Metadata["filePath"];
                if (hashValue == fileHashValue)
                {
                    isHashSame = true;
                }

                if ((DateTime.Compare(timestamp,fileTimestamp)<0))                
                {
                    isTimestampLater = true;
                }
            }
            else if (blob.Exists() && isDirectory)
            {
                // do nothing or set allupload to be false, future implementaion
            }
            else 
            {
                ifBlobExist = false;
            }

        }

        //need to be modified for directroy
        public bool allUpload()
        {
            if(isTimestampLater){
              if (isHashSame)
                {
                    return false;
                }
                return true;
            }

            return false;
            
        }

    }
}
