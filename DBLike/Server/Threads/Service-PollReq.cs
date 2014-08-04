using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using Microsoft.WindowsAzure.Storage.Blob;
using Server.BlobAccess;
using Server.ConnectionManager;

namespace Server.Threads
{
    class ServicePollReq
    {

        public Thread thread;
        public void start(Socket soc, string req)
        {
            thread = new Thread(() => threadStartFun(soc,req));
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
                Program.ServerForm.addtoConsole("Service Poll Req Thread Started");
                Server.Message.MessageParser serverPollPar = new Server.Message.MessageParser();
                Server.MessageClasses.MsgPoll msgpollServer = serverPollPar.pollParseMsg(req);

                CloudBlobClient blobClient = new Server.ConnectionManager.BlobConn().BlobConnect();
                Blob blob = new Blob(blobClient, msgpollServer.userName);
                GenerateSAS sas = new GenerateSAS();
                string link = "";
                if (msgpollServer.password == "requestVC")
                {
                    link = sas.GetContainerSasUri(blob.container, "RWLD");
                }
                else
                {
                    link = sas.GetContainerSasUri(blob.container, "RWLD");
                }
                

                // Server send response to Client
                Server.Message.CreateMsg pollResp = new Server.Message.CreateMsg();
                msgpollServer.fileContainerUri = link;
                msgpollServer.fileBlobUri = "none";
                string respMsg = pollResp.pollRespMsg("OK", msgpollServer);

                //socket
                SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                // write to socket
                rw.writetoSocket(soc, respMsg);
                Program.ServerForm.addtoConsole("Wrote Response to socket");

            }
            catch (Exception e)
            {
                Program.ServerForm.addtoConsole("Exception: [Poll Req]"+ e.Message);
                //System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            finally
            {
                Program.ServerForm.addtoConsole("Exiting");
                Thread.CurrentThread.Abort();
            }

        }
    }
}
