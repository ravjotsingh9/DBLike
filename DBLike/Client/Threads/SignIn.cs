using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Client.Threads
{
    class SignIn
    {
        public Thread thread;
        Configuration.config serverdetails = new Configuration.config();
        static ConnectionManager.Connection conn = new ConnectionManager.Connection();
        static Socket sender;

        public void start( String username, String password)
        {
            //TBD
            thread = new Thread(() => threadStartFun(serverdetails.serverAddr, serverdetails.port, username, password));
            thread.Start();
         
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string serverIP, int port, String username, String password)
        {
            //TBD
            //TBD
            MessageClasses.MsgSignIn msgobj = new MessageClasses.MsgSignIn();

            msgobj.setUsername(username);
            msgobj.setPassword(password);

            // Fill out the content in msgobj

            //call CreateMsg.createSignUpMsg(msgobj) get it in bytes form
            Message.CreateMsg msg = new Message.CreateMsg();
            String message = msg.createSignInMsg(msgobj);

            //create a socket connection. you may need to create in Conection Manager
            sender = conn.connect(serverIP, port);
            if (sender == null)
            {
                System.Windows.Forms.MessageBox.Show("Could not connect to server.Please check if Server is Running.", "DBLike Client");
                Thread.CurrentThread.Abort();
            }
            //call  SocketCommunication.ReaderWriter.write(byte[] msg) to write msg on socket
            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            rw.writetoSocket(sender, message);
            //call  SocketCommunication.ReaderWriter.read() to read response from server
            String response = rw.readfromSocket(sender);

            //call parser and process it.....
            Message.MessageParser mp = new Message.MessageParser();
            msgobj = mp.signInParseMessage(response);
            if(!msgobj.getAck().Equals(""))
            {
                if(msgobj.getAck()=="ERRORS")
                {
                    System.Windows.Forms.MessageBox.Show("AUTHENTICATION FAILED", "User name or Password incorrect");
                    Thread.CurrentThread.Abort();
                }
                else
                {
                    LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
                    if(!file.isExists())
                    {
                        /*FolderBrowserDialog folder = new FolderBrowserDialog();
                        if (folder.ShowDialog() == DialogResult.OK)
                        {
                            string path = folder.SelectedPath;

                        }*/
                        //have to create the dblike text file by asking the user to select the path
                    }
                    else
                    {
                        //compares the user name and password from the existing dblike text file
                        file.readfromfile();
                        string user = file.getUsername();
                        string pass = file.getPassword();
                        string path = null;
                        FolderBrowserDialog folder = new FolderBrowserDialog();
                        if(user!=username && pass!=password)
                        {
                            if (folder.ShowDialog() == DialogResult.OK)
                            {
                                path = folder.SelectedPath;
                            }
                            file.writetofile(user, pass, path);
                        }
                    }
                }
            }
        }
    }
}
