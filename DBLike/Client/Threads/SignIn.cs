using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Threads
{
    class SignIn
    {
        public Thread thread;
        Configuration.config serverdetails = new Configuration.config();
        static ConnectionManager.Connection conn = new ConnectionManager.Connection();
        static Socket sender;
        //static PollFiles poll;
        public void start( String username, String password, Form frm)
        {
            //TBD
            thread = new Thread(() => threadStartFun(serverdetails.serverAddr, serverdetails.port, username, password,frm));
            thread.Start();
         
        }
        public void stop()
        {
            //TBD
            Client.Program.poll.pull = false;
            thread.Abort();
        }
        static private void threadStartFun(string serverIP, int port, String username, String password,Form frm)
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
                    System.Windows.Forms.MessageBox.Show(msgobj.getAck(),msgobj.getAddiMsg());
                    LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
                    file = file.readfromfile();
                    if (file != null)
                    {
                        if (username != file.getUsername() || password != file.getPassword())
                        {
                            System.Windows.Forms.MessageBox.Show("System is already configured for a dblike user."+ 
                                "Now onwards,only the folder selected for this user will be synchronized.");
                            string path = null;
                            System.Windows.Forms.MessageBox.Show("Please select a path to download your folder from the server");
                            var t = new Thread((ThreadStart)(() =>
                            {
                                FolderBrowserDialog folder = new FolderBrowserDialog();
                                if (folder.ShowDialog() == DialogResult.OK)
                                {
                                    path = folder.SelectedPath;
                                }
                            }));
                            t.IsBackground = true;
                            t.SetApartmentState(ApartmentState.STA);
                            t.Start();
                            t.Join();
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Localdb doesnot exist.");
                        
                        string path = null;
                        System.Windows.Forms.MessageBox.Show("Please select a path to download your folder from the server");
                        var t = new Thread((ThreadStart)(() =>
                        {
                            FolderBrowserDialog folder = new FolderBrowserDialog();
                            if (folder.ShowDialog() == DialogResult.OK)
                            {
                                path = folder.SelectedPath;
                            }
                        }));
                        
                        t.IsBackground = true;
                        t.SetApartmentState(ApartmentState.STA);
                        t.Start();
                        t.Join();
                        //write to file
                        file = new LocalDbAccess.LocalDB();
                        file.writetofile(username, password, path);
                    }
                    //poll = new PollFiles();
                    
                    //poll.start();
                    //MessageBox.Show("Signing in done","Client");
                    Client.Program.poll.start();
                    //Thread.Sleep(10000);
                    FileSysWatchDog.Run();
                    
                    
                    Thread.CurrentThread.Abort();
                }
            }
        }
    }
}
