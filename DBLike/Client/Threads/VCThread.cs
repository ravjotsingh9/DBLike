using Client.LocalDbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Threads
{
    class VCThread
    {
        static Configuration.config conf = new Configuration.config();
        static Thread thread;
        public void start()
        {
            threadStartFun();
            //TBD
            //thread = new Thread(() => threadStartFun());
            //thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        private void threadStartFun()
        {
            try
            {
                Console.WriteLine("in Vc thread");
                // Client create poll msg 
                LocalDB readLocalDB = new LocalDB();
                readLocalDB = readLocalDB.readfromfile();
                Client.MessageClasses.MsgPoll msgpoll = new Client.MessageClasses.MsgPoll();
                Client.Message.CreateMsg pollM = new Client.Message.CreateMsg();
                msgpoll.userName = readLocalDB.getUsername();
                msgpoll.password = "requestVC";
                string msg = pollM.pollMsg(msgpoll);

                //send the msg using socket
                ConnectionManager.Connection conn = new ConnectionManager.Connection();
                Socket soc = conn.connect(conf.serverAddr, conf.port);
              
                SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                rw.writetoSocket(soc, msg);

                //receive the msg
                string resp = rw.readfromSocket(soc);
                
                //parse msg and poll
                Client.Message.MessageParser parseResp = new Client.Message.MessageParser();
                msgpoll = parseResp.pollParseMsg(resp);
                if (msgpoll.indicator == "OK")
                {
                    Configuration.userInfo.containerURI = msgpoll.fileContainerUri;
                    // write container somewhere (msgpoll.fileContainerUri);
                }
              

            }
            catch (Exception e)
            {
                Program.ClientForm.addtoConsole(e.ToString());
                System.Windows.Forms.MessageBox.Show(e.ToString());
                //System.IO.File.WriteAllText("errors.txt", e.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }


    }
}
