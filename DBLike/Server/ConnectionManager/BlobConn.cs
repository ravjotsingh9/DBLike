using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;


namespace Server.ConnectionManager
{
    public class BlobConn
    {
        private string accountName1;
        private string accountKey1;
        private string accountName2;
        private string accountKey2;
        private string accountName3;
        private string accountKey3;
        private CloudBlobClient client = null;
        public int currentBlob = 0;

        /// <summary>
        /// Constructor --- setup information for Blob
        /// </summary>
        /// <param name="BlobNumber">specift which Blob you want to connect
        ///                        e.g. 1 for Blob --- portalvhdscgcgqr43r8dpq</param>
        public BlobConn()
        {
            accountName1 = "portalvhdscgcgqr43r8dpq";
            accountKey1 = "q7u0mob+u/kgqTlIe4lfKa6xmo4Cm/rT6VlmBd2EwWZTy3tq2h3LeHFwQandnM7AIunrH0n65et3dKAX9LghBA==";

            accountName2 = "portalvhdstj44khd5kg1p9";
            accountKey2 = "yDFs8SXY2j0kyY87wARkn4LxQzmJIzEhWpGJoggUpUrUBEUXZMlBKmNcVVAyH9mvsT3/wKhhz2/8gI27B+mytg==";

            accountName3 = "portalvhdsblb4n5fvbfnvq";
            accountKey3 = "RoRrFmQHZ9KWj4e4fnU36jYMwj2COm9C5jxDiwoBXNaoLY0AZwwry5Ke1TUbdKus+r69p48KQrvLxTpcRrT5Fw==";

        }

        /// <summary>
        /// Start Blob connection
        /// </summary>
        /// <returns>
        /// CloudBlobClient
        /// </returns>
        public CloudBlobClient BlobConnect()
        {
            if (pinConnection(accountName1, accountKey1))
            {
                currentBlob = 1;
                return this.client;
            }
            else if (pinConnection(accountName2, accountKey2))
            {
                currentBlob = 2;
                return this.client;
            }
            else if (pinConnection(accountName3, accountKey3))
            {
                currentBlob = 3;
                return this.client;
            }
            else
            {
                return null;
            }

        }

        private bool pinConnection(string accountName, string accountKey)
        {
            try
            {
               

                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                client = account.CreateCloudBlobClient();
                client.ServerTimeout = TimeSpan.FromSeconds(11);
                CloudBlobContainer container = client.GetContainerReference("zforconnection");
                container.CreateIfNotExists();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                client = null;
                return false;
            }

        }

        public CloudBlobClient getSpecifyClient(int i)
        {
            CloudBlobClient blobClient = null;
            try
            {
                string accountName = accountName2;
                string accountKey = accountKey2;
                if (i == 1)
                {
                    accountName = accountName1;
                    accountKey = accountKey1;
                }
                else if (i == 3)
                {
                    accountName = accountName3;
                    accountKey = accountKey3;
                }
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                blobClient = account.CreateCloudBlobClient();
                blobClient.ServerTimeout = TimeSpan.FromSeconds(11);
                CloudBlobContainer container = blobClient.GetContainerReference("zforconnection");
                container.CreateIfNotExists();
            }
            catch (Exception e)
            {
                return blobClient;
            }

            return blobClient;
        }
            
    }
}
