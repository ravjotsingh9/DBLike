using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Client.LocalDbAccess;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Diagnostics;

namespace Client.Threads
{
    public class PollFiles
    {
        static Configuration.config conf = new Configuration.config();
        static Thread thread;
        volatile public bool pull = true;
        public void start()
        {
            //TBD
            thread = new Thread(() => threadStartFun());
            thread.Start();
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
                //poll();
                
                //Thread.Sleep(60000);
                
                while (pull)
                {
                    poll();
                    Thread.Sleep(60000);
                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                //System.IO.File.WriteAllText("errors.txt", e.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }

        public void poll()
        {
            //MessageBox.Show("Polling started", "Client");
            try
            {
                // Client create poll msg 
                LocalDB readLocalDB = new LocalDB();
                readLocalDB = readLocalDB.readfromfile();

                Client.MessageClasses.MsgPoll msgpoll = new Client.MessageClasses.MsgPoll();
                Client.Message.CreateMsg pollM = new Client.Message.CreateMsg();
                msgpoll.userName = readLocalDB.getUsername();
                msgpoll.password = readLocalDB.getPassword();
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
                    new Client.PollFunction.Poll(msgpoll.fileContainerUri);
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());                
            }
        }
    }
}
