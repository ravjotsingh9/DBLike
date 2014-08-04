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
            Program.poll.stop();
            //thread.Abort();
        }
        static private void threadStartFun(string serverIP, int port, String username, String password,Form frm)
        {
            //TBD
            Program.ClientForm.addtoConsole("Starting signin thread");
            //TBD
            if(username == "" || password == "")
            {
                Program.ClientForm.addtoConsole("Error : <<Username: \"" +username + "\" and Password: \"" +password + "\" >>" );
                //Program.ClientForm.addtoConsole("Password:" + password);
                //MessageBox.Show("Username and Password field cannot be empty", "DBLike Client Sign In");
                if (!Program.ClientForm.IsHandleCreated)
                {
                    Program.ClientForm.CreateHandle();
                }
                //enable service controller
                Program.ClientForm.signinfail();
                Program.ClientForm.addtoConsole("Exiting");
                Thread.CurrentThread.Abort();
            }

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
                //System.Windows.Forms.MessageBox.Show("Could not connect to server.Please check if Server is Running.", "DBLike Client");
                if (!Program.ClientForm.IsHandleCreated)
                {
                    Program.ClientForm.CreateHandle();
                }
                //enable service controller
                Program.ClientForm.addtoConsole("Error : <<Unable to connect to server>> ");
                Program.ClientForm.signinfail();
                Program.ClientForm.addtoConsole("Exiting");
                Thread.CurrentThread.Abort();
            }
            //call  SocketCommunication.ReaderWriter.write(byte[] msg) to write msg on socket
            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            Program.ClientForm.addtoConsole("Writing to socket");
            rw.writetoSocket(sender, message);
            //call  SocketCommunication.ReaderWriter.read() to read response from server
            String response = rw.readfromSocket(sender);
            Program.ClientForm.addtoConsole("Reading from socket");
            //call parser and process it.....
            Message.MessageParser mp = new Message.MessageParser();
            msgobj = mp.signInParseMessage(response);
            if(!msgobj.getAck().Equals(""))
            {
                if(msgobj.getAck()=="ERRORS")
                {
                    Program.ClientForm.addtoConsole("Error : <<" + msgobj.getAddiMsg() + ">>");
                    System.Windows.Forms.MessageBox.Show("AUTHENTICATION FAILED", "User name or Password incorrect");
                    if (!Program.ClientForm.IsHandleCreated)
                    {
                        Program.ClientForm.CreateHandle();
                    }
                    //enable service controller
                    Program.ClientForm.signinfail();
                    Program.ClientForm.addtoConsole("Exiting");
                    Thread.CurrentThread.Abort();
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show(msgobj.getAck(),msgobj.getAddiMsg());\
                    //Program.ClientForm.addtoConsole("Username:" + username);
                    LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
                    file = file.readfromfile();
                    if (file != null)
                    {
                        if (username != file.getUsername() || password != file.getPassword())
                        {

                            if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("This System is already configured for a dblike user." +
                                "Do you really want to reconfigure it for another user?", "DBLike Client", MessageBoxButtons.YesNo))
                            {

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

                                Program.ClientForm.addtoConsole("Wrote to Config file");
                            }
                            else
                            {
                                Program.ClientForm.signinfail();
                                Program.ClientForm.addtoConsole("Exiting");
                                Thread.CurrentThread.Abort();
                            }
                        }
                    }
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show("Localdb doesnot exist.");
                        
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

                        Program.ClientForm.addtoConsole("Wrote to Confgi file");


                    }
                    //poll = new PollFiles();
                    if (!Program.ClientForm.IsHandleCreated)
                    {
                        Program.ClientForm.CreateHandle();
                    }
                    //enable service controller
                    Program.ClientForm.signinpass();
                   
                    Program.ClientForm.addtoConsole("Successefully Signed in");
                    //poll.start();
                    //MessageBox.Show("Signing in done","Client");
                    Client.Program.poll.pull = true;
                    Client.Program.poll.start();
                    Program.ClientForm.addtoConsole("Polling started");
                    //Thread.Sleep(10000);



                    // initialize the file list for sign in scenario
                    Client.LocalFileSysAccess.FileListMaintain fileMaintain = new Client.LocalFileSysAccess.FileListMaintain();
                    fileMaintain.scanAllFilesAttributes();

                    //FileSysWatchDog.Run();
                    Program.folderWatcher.start();
                    Program.ClientForm.addtoConsole("File-watcher Installed");
                    
                    Thread.CurrentThread.Abort();
                }
            }
        }
    }
}
