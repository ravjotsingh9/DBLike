using System;
using Client.UploadFunctions;
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
        
        [TestMethod]
        public void uploadTest()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
                 new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo("en-US");
            UploadFile uu = new UploadFile();
            string uri = "https://portalvhdscgcgqr43r8dpq.blob.core.windows.net/samples?sv=2013-08-15&sr=c&sig=FGJ%2BlJHj2zZjP8xsp2yVr8pVrzYCoEbZq2H0JIzncpQ%3D&se=2014-07-07T05%3A23%3A56Z&sp=rwdl";
            string localpath = "C:\\Users\\yi-man\\Desktop\\testFolder\\123.txt";
            string pathinSynFolder = "testFolder\\123.txt";
            try
            {
               // uu.UploadFileWithContainerUri(uri,localpath,pathinSynFolder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
