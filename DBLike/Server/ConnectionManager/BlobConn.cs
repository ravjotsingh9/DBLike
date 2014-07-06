using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;


namespace Server.ConnectionManager
{
    class BlobConn
    {
        private string accountName;
        private string accountKey;

        /// <summary>
        /// Constructor --- setup information for Blob
        /// </summary>
        /// <param name="BlobNumber">specift which Blob you want to connect
        ///                        e.g. 1 for Blob --- portalvhdscgcgqr43r8dpq</param>
        public BlobConn(int BlobNumber)
        {
            BlobNo(BlobNumber);
        }

        /// <summary>
        /// initialize database info according to the BlobNumber
        /// </summary>
        /// <param name="number"></param>
        private void BlobNo(int number)
        {
            if (number == 1)
            {
                accountName = "portalvhdscgcgqr43r8dpq";
                accountKey = "q7u0mob+u/kgqTlIe4lfKa6xmo4Cm/rT6VlmBd2EwWZTy3tq2h3LeHFwQandnM7AIunrH0n65et3dKAX9LghBA==";
            }
        }

        /// <summary>
        /// Start Blob connection
        /// </summary>
        /// <returns>
        /// CloudBlobClient
        /// </returns>
        public CloudBlobClient BlobConnect(){
            StorageCredentials creds = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudBlobClient client = account.CreateCloudBlobClient();

            return client;
        }
            
            
    }
}
