﻿using System;
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
        public static Thread thread;
        volatile public bool pull = true;
        private AutoResetEvent autoEvent = new AutoResetEvent(false);
        public static AutoResetEvent stopPollingEvent { get; set; }

        public void start()
        {
            stopPollingEvent = new AutoResetEvent(false);
            thread = new Thread(() => threadStartFun());
            //TBD
            
            thread.Start();
        }
        public void stop()
        {
            if (thread.IsAlive)
            {
                Client.Program.poll.pull = false;
                Threads.PollFiles.stopPollingEvent.Set();
                //Threads.PollFiles.thread.Join();
                
            }
            //thread.Abort();
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
                    //Thread.Sleep(60000);
                    stopPollingEvent.WaitOne(60000);
                }
                Program.ClientForm.addtoConsole("DBLike Service stopped!");
                Program.ClientForm.ServiceStopped();
                
            }
            catch (Exception e)
            {
                Program.ClientForm.addtoConsole("Exception:" + e.Message);
                Program.ClientForm.ServiceStopped();
                //System.Windows.Forms.MessageBox.Show(e.ToString());
                //System.IO.File.WriteAllText("errors.txt", e.ToString());
            }
            finally
            {
                Program.ClientForm.addtoConsole("Exiting Poll thread.");
                Thread.CurrentThread.Abort();
            }
        }

        public void poll()
        {
            Configuration.flag.polling = true; 
            Program.ClientForm.addtoConsole("Poll initiated");
           
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
                Program.ClientForm.addtoConsole("Writing Socket");
                //receive the msg
                string resp = rw.readfromSocket(soc);
                Program.ClientForm.addtoConsole("Reading Socket");
                //parse msg and poll
                Client.Message.MessageParser parseResp = new Client.Message.MessageParser();
                msgpoll = parseResp.pollParseMsg(resp);
                if (msgpoll.indicator == "OK")
                {
                    new Client.PollFunction.Poll(msgpoll.fileContainerUri);
                    Configuration.userInfo.containerURI = msgpoll.fileContainerUri;
                }
                Configuration.flag.polling = false; 
            }
            catch(Exception ex)
            {
                Program.ClientForm.addtoConsole("Poll thread Exception:" + ex.Message);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());                
            }
        }
    }
}
