using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Threads
{
    class SignUp
    {
        public Thread thread;
        Configuration.config serverdetails = new Configuration.config();
        static ConnectionManager.Connection conn = new ConnectionManager.Connection();
        
        public void start(string username, string pass)
        {
            thread = new Thread(()=>threadStartFun(serverdetails.serverAddr ,serverdetails.port, username, pass));
            //TBD
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string serverIP, int port, string username, string pass)
        {
            //Form1 f1 = new Form1();

            if (!Program.ClientForm.IsHandleCreated)
            {
                Program.ClientForm.CreateHandle();
            }
            Program.ClientForm.enableServiceController();
            Thread.CurrentThread.Abort();

            //TBD
            MessageClasses.MsgSignUp.req msgobj = new MessageClasses.MsgSignUp.req();

            // Fill out the content in msgobj
            msgobj.userName = username;
            msgobj.psw = pass;

            //call CreateMsg.createSignUpMsg(msgobj) get it in bytes form
            Message.CreateMsg msgcreator = new Message.CreateMsg();
            string msg = msgcreator.createSignUpMsg(msgobj);
            
            //create a socket connection. you may need to create in Conection Manager
            //conn.connect(serverIP, port)
            ConnectionManager.Connection conn = new ConnectionManager.Connection();
            Socket soc = conn.connect(serverIP, port);
 
            //call  SocketCommunication.ReaderWriter.write(byte[] msg) to write msg on socket
            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            rw.writetoSocket(soc, msg);
            //call  SocketCommunication.ReaderWriter.read() to read response from server
            string resp = rw.readfromSocket(soc);
            
            //call parser and process it.....
            Message.MessageParser msgparser = new Message.MessageParser();
            if ((msgparser.signUpRespParser(resp)).Equals("ERROR"))
            {
                System.Windows.Forms.MessageBox.Show("Some error occured!Please try again.","Error Occured");
                Thread.CurrentThread.Abort();
            }
            else
            {
                Form1 f = new Form1();
                f.enableServiceController();
                Threads.FileSysWatchDog watchdog = new FileSysWatchDog();
                watchdog.start();
                Thread.CurrentThread.Abort();
            }

        }
    }
}
