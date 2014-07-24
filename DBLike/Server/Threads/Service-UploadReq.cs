using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using Microsoft.WindowsAzure.Storage.Blob;
using Server.BlobAccess;

namespace Server.Threads
{
    class ServiceUploadReq
    {

        public Thread thread;
        public void start(Socket soc, string req)
        {
            thread = new Thread(() => threadStartFun(soc, req));
            thread.Start();
        }
        public void stop()
        {
            thread.Abort();
        }
        private void threadStartFun(Socket soc, string req)
        {
            try
            {


                //System.Windows.Forms.MessageBox.Show("uploader started:" + req, "Server");
                //get the msg parse it
                Server.Message.MessageParser parse = new Server.Message.MessageParser();
                //MessageClasses.MsgUpload msgobj = new MessageClasses.MsgUpload();
                //msgobj = parse.uploadParseMsg(req);
                MessageClasses.MsgUpload upload = parse.uploadParseMsg(req);
                /*
                Console.WriteLine("userName: {0} password: {1}, sync folder patt: {2}, md5: {3}, timestamp: {4} fileName {5}",
                                   upload.userName, upload.password, upload.filePathInSynFolder, upload.fileHashValue, upload.fileTimeStamps, upload.fileName);
                **/
                //5 Server check the file info in the blob storage, if file has not existed, let the client to create the file
                //  If file existed, check timestamp, hashvalue to see if client can upload
                //  If allow upload, change hashvalue and timestamp in the blob storage
                CloudBlobClient blobClient = new Server.ConnectionManager.BlobConn(1).BlobConnect();


                if (upload.addInfo == "create" || upload.addInfo == "change" || upload.addInfo == "signUpStart")
                {

                    Blob blob = new Blob(blobClient, upload.userName, upload.filePathInSynFolder, upload.fileHashValue, upload.fileTimeStamps);


                    /**
                     Server.DatabaseAccess.Query query = new Server.DatabaseAccess.Query();
                    if (!query.fileAlreadyExist(parse.userName, parse.filePathInSynFolder))
                    {
                        query.insertNewFileData(parse.userName, parse.fileName, parse.filePathInSynFolder, parse.fileHashValue, parse.fileTimeStamps);
                    }
                    else
                    {
                        query.updateFilesData(parse.userName, parse.filePathInSynFolder, parse.fileHashValue, parse.fileTimeStamps);
                    }
        **/
                    //6 Server Generate sas container/blob for the client. For basic upload, this case just generate container sas
                    //Server.ConnectionManager.BlobConn conn = new Server.ConnectionManager.BlobConn(1);

                    CloudBlobContainer container = blob.container;
                    string containerSAS = new Server.ConnectionManager.GenerateSAS().GetContainerSasUri(container, "RWLD");
                    Console.WriteLine("container sas uri: {0}", containerSAS);
                    //7 send upload msg back to client
                    Server.Message.CreateMsg resp = new Server.Message.CreateMsg();
                    string respMsg = resp.uploadRespMsg(upload.filePathInSynFolder, containerSAS, null);
                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    rw.writetoSocket(soc, respMsg);
                    //System.Windows.Forms.MessageBox.Show("uploader write:" + respMsg, "Server");

                }


                if (upload.addInfo == "delete")
                {

                }


            }
            catch (Exception e)
            {
                //System.IO.File.WriteAllText("bug.txt", e.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
    }
}
