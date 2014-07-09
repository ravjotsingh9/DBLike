using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using Server;
using Server.BlobAccess;
using Server.MessageClasses;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ClientUnitTest
{
    /**
    [TestClass]
    public class UploadIntegrationTest
    {
        [TestMethod]
        public void uploadIntegrationTest()
        {
         
            //1 Client.cs should detect there is a file in the sync folder and get the local path
            //2 Client.cs send upload msg to server
            Client.Message.CreateMsg uploadM = new Client.Message.CreateMsg();
            string fileLocalPath = @"C:\Users\yi-man\Desktop\ttt\blob.txt";
            Client.LocalFileSysAccess.getFileAttributes att = new Client.LocalFileSysAccess.getFileAttributes(fileLocalPath);
            string syncFolderPath = "blob.txt";

            DateTime time = att.lastModified;
            string md5r = att.md5Value;
            string msg = uploadM.uploadMsg("blob","123456",syncFolderPath,time,md5r);
            Console.WriteLine(msg);
            //3 send msg by socket
            //4 server get msg and parse it
            Server.Message.MessageParser parse = new Server.Message.MessageParser();
            parse.uploadParseMsg(msg);
            Server.MessageClasses.MsgRespUpload upload = parse.uploadParseMsg(msg);
            Console.WriteLine("userName: {0} password: {1}, sync folder patt: {2}, md5: {3}, timestamp: {4} fileName {5}",
                               upload.userName, upload.password, upload.filePathInSynFolder, upload.fileHashValue, upload.fileTimeStamps, upload.fileName);
            //5 Server check the file info in the blob storage, if file has not existed, let the client to create the file
            //  If file existed, check timestamp, hashvalue to see if client can upload
            //  If allow upload, change hashvalue and timestamp in the blob storage
            CloudBlobClient blobClient = new Server.ConnectionManager.BlobConn(1).BlobConnect();
            Blob blob = new Blob(blobClient, upload.userName, upload.filePathInSynFolder,upload.fileHashValue,upload.fileTimeStamps);

             Server.DatabaseAccess.Query query = new Server.DatabaseAccess.Query();
            if (!query.fileAlreadyExist(parse.userName, parse.filePathInSynFolder))
            {
                query.insertNewFileData(parse.userName, parse.fileName, parse.filePathInSynFolder, parse.fileHashValue, parse.fileTimeStamps);
            }
            else
            {
                query.updateFilesData(parse.userName, parse.filePathInSynFolder, parse.fileHashValue, parse.fileTimeStamps);
            }

            //6 Server Generate sas container/blob for the client. For basic upload, this case just generate container sas
            //Server.ConnectionManager.BlobConn conn = new Server.ConnectionManager.BlobConn(1);

            CloudBlobContainer container = blob.container;
            string containerSAS = new Server.ConnectionManager.GenerateSAS().GetContainerSasUri(container, "RWLD");
            Console.WriteLine("container sas uri: {0}", containerSAS);
            //7 send upload msg back to client
            Server.Message.CreateMsg resp = new Server.Message.CreateMsg();
            string respMsg = resp.uploadRespMsg(upload.filePathInSynFolder, containerSAS, null);
            Console.WriteLine("server repsonse: {0}", respMsg);
            //8 Client parse msg
            Client.Message.MessageParser par2 = new Client.Message.MessageParser();
            Client.MessageClasses.MsgUpload reup = par2.uploadParseMsg(respMsg);
            Console.WriteLine("file path: {0}, container uri: {1}",
                               reup.filePathInSynFolder, reup.fileContainerUri);
            //9 Client upload
            new Client.UploadFunctions.UploadFile().UploadFileWithContainerUri(reup.fileContainerUri, fileLocalPath, reup.filePathInSynFolder, md5r,time);
   **/
        }

    }
}
