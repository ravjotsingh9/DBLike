using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using Server;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ClientUnitTest
{
    [TestClass]
    public class UploadIntegrationTest
    {
        [TestMethod]
        public void uploadIntegrationTest()
        {
         
            //1 Client.cs should detect there is a file in the sync folder and get the local path
            //2 Client.cs send upload msg to server
            Client.MessageClasses.MsgUpload uploadM = new Client.MessageClasses.MsgUpload();
            Client.LocalFileSysAccess.getFileAttributes att = new Client.LocalFileSysAccess.getFileAttributes();
            string syncFolderPath = "C:\\Users\\yi-man\\Desktop\\testFolder";
            DateTime time = att.getTimeStamp("C:\\Users\\yi-man\\Desktop\\testFolder\\456.txt");
            string md5r = att.getFileMD5Value("C:\\Users\\yi-man\\Desktop\\testFolder\\456.txt");
            string msg = uploadM.uploadMsg("blob","123456",syncFolderPath,time,md5r);
            Console.WriteLine(msg);
            //3 send msg by socket
            //4 server get msg and parse it
            Server.Message.MessageParser parse = new Server.Message.MessageParser(msg);
            Console.WriteLine("userName: {0} password: {1}, sync folder patt: {2}, md5: {3}, timestamp: {4}",
                               parse.userName, parse.password, parse.filePathInSynFolder, parse.fileHashValue,parse.fileTimeStamps);
            //5 Server check the file info in the database, if file has not existed, let the client to create the file
            //  If file existed, check timestamp, hashvalue to see if client can upload
            //6 Server Generate sas container/blob for the client. For basic upload, this case just generate container sas
            Server.ConnectionManager.BlobConn conn = new Server.ConnectionManager.BlobConn(1);
            CloudBlobClient blobclient = conn.BlobConnect();
            CloudBlobContainer container = new Server.BlobAccess.Blob().getClientContainer(blobclient,parse.userName);
            string containerSAS = new Server.ConnectionManager.GenerateSAS().GetContainerSasUri(container, "RWLD");
            Console.WriteLine("container sas uri: {0}", containerSAS);
            //7 send upload msg back to client
            
   
        }

    }
}
