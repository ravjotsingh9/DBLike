using System;
using Server.ConnectionManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ServerUnitTest
{
    [TestClass]
    public class blobUnitTest
    {
        [TestMethod]
        public void blobUriUnitTest()
        {
            BlobConn blobconn = new BlobConn(1);
            CloudBlobClient blobClient = blobconn.BlobConnect();
            CloudBlobContainer container = blobClient.GetContainerReference("samples");
            GenerateSAS sas = new GenerateSAS();
            string uri = sas.GetContainerSasUri(container, "WRLD");
            Console.WriteLine(uri);
        }
    }
}
