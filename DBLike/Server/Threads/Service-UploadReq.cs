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
                MessageClasses.MsgUpload upload = parse.uploadParseMsg(req);
                
                //5 Server check the file info in the blob storage, if file has not existed, let the client to create the file
                //  If file existed, check timestamp, hashvalue to see if client can upload
                //  If allow upload, change hashvalue and timestamp in the blob storage
                CloudBlobClient blobClient = new Server.ConnectionManager.BlobConn().BlobConnect();


                if (upload.addInfo == "create" || upload.addInfo == "change" || upload.addInfo == "signUpStart")
                {

                    Blob blob = new Blob(blobClient, upload.userName, upload.filePathInSynFolder, upload.fileHashValue, upload.fileTimeStamps);

                    //6 Server Generate sas container/blob for the client. For basic upload, this case just generate container sas
                    //Server.ConnectionManager.BlobConn conn = new Server.ConnectionManager.BlobConn(1);

                    CloudBlobContainer container = blob.container;
                    string containerSAS = new Server.ConnectionManager.GenerateSAS().GetContainerSasUri(container, "RWLD");
                    Console.WriteLine("container sas uri: {0}", containerSAS);

                    //7 send upload msg back to client
                    Server.Message.CreateMsg resp = new Server.Message.CreateMsg();


                    //string respMsg = resp.uploadRespMsg(upload.filePathInSynFolder, containerSAS, null, upload.addInfo);
                    string respMsg = resp.uploadRespMsg(blob.indicator,upload.filePathInSynFolder, containerSAS, " ", upload.addInfo);


                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    // write to socket
                    rw.writetoSocket(soc, respMsg);
                    //System.Windows.Forms.MessageBox.Show("uploader write:" + respMsg, "Server");

                }




                if (upload.addInfo == "delete")
                {
                    // get container
                    CloudBlobContainer container = blobClient.GetContainerReference(upload.userName);

                    UploadFunctions.DeleteFile del = new UploadFunctions.DeleteFile();
                    del.deleteAll(container, upload.filePathInSynFolder);

                }


                if (upload.addInfo.IndexOf("rename") == 0)
                {
                    // get container
                    CloudBlobContainer container = blobClient.GetContainerReference(upload.userName);

                    // get new path in the server
                    string[] separators = { "|||" };
                    string[] str = upload.addInfo.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string newPath = str[1];

                    //grab the blob
                    CloudBlockBlob existBlob = container.GetBlockBlobReference(upload.filePathInSynFolder);
                    // create a new blob
                    CloudBlockBlob newBlob = container.GetBlockBlobReference(newPath);
                    //copy from the old blob
                    newBlob.StartCopyFromBlob(existBlob);

                    newBlob.Metadata["hashValue"] = upload.fileHashValue;
                    newBlob.Metadata["timestamp"] = upload.fileTimeStamps.ToString("MM/dd/yyyy HH:mm:ss");
                    newBlob.Metadata["filePath"] = newPath;
                    newBlob.SetMetadata();

                    //delete the old blob
                    existBlob.Delete();
                }


            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                //System.IO.File.WriteAllText("bug.txt", e.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
    }
}
