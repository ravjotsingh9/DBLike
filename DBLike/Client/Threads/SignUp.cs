using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;


namespace Client.Threads
{
    class SignUp
    {
        public Thread thread;
        Configuration.config serverdetails = new Configuration.config();
        static ConnectionManager.Connection conn = new ConnectionManager.Connection();
        static Socket sender;
        public void start(String username, String password, string syncpath)
        {
            thread = new Thread(()=>threadStartFun(serverdetails.serverAddr ,serverdetails.port, username,password, syncpath));
            //TBD
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string serverIP, int port,String username, String password,string sysncpath )
        {
            Program.ClientForm.addtoConsole("Sign Up initiated");
            //TBD
            //System.Windows.Forms.MessageBox.Show("start", "SignUp Thread started");
            MessageClasses.MsgSignUp msgobj = new MessageClasses.MsgSignUp();
            msgobj.userName = username;
            msgobj.psw = password;
            // Fill out the content in msgobj

            //call CreateMsg.createSignUpMsg(msgobj) get it in bytes form
            Message.CreateMsg msg=new Message.CreateMsg();
            String message = msg.createSignUpMsg(msgobj);

            //create a socket connection. you may need to create in Conection Manager
            sender = conn.connect(serverIP, port);
            if(sender == null)
            {
                Program.ClientForm.addtoConsole("Error : <<Could not connect to server. May be Server is not Running>>");
                //System.Windows.Forms.MessageBox.Show("Could not connect to server.Please check if Server is Running.", "DBLike Client");
                Program.ClientForm.addtoConsole("Exiting");
                Program.ClientForm.signUpfailed();
                Thread.CurrentThread.Abort();
            }
            //call  SocketCommunication.ReaderWriter.write(byte[] msg) to write msg on socket
            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            //System.Windows.Forms.MessageBox.Show("going to write socket:"+message, "SignUp Thread started");
            Program.ClientForm.addtoConsole("Writing to socket");
            rw.writetoSocket(sender, message);


            //call  SocketCommunication.ReaderWriter.read() to read response from server
            //System.Windows.Forms.MessageBox.Show("going to read socket: " , "SignUp Thread started");
            String response=rw.readfromSocket(sender);
            Program.ClientForm.addtoConsole("Read from socket");
            //System.Windows.Forms.MessageBox.Show("read:"+ response, "SignUp Thread started");
            //call parser and process it.....
            Message.MessageParser mp = new Message.MessageParser();

  
            //msgobj = mp.signInParseMessage(response);
            msgobj = mp.signUpParseMessage(response);

			// This functionality should be added here
			
			//call parser and process it.....
            if (!msgobj.ack.Equals(""))
            {
                Message.MessageParser msgparser = new Message.MessageParser();
                if (msgobj.ack.Equals("ERRORS"))
                {
                    Program.ClientForm.addtoConsole("Error :" +"<<"+ msgobj.addiMsg +">>");
                    //System.Windows.Forms.MessageBox.Show("Some error occured!Please try again.", "Error Occured");
                    Program.ClientForm.addtoConsole("Exiting");
                    Program.ClientForm.signUpfailed();
                    Thread.CurrentThread.Abort();
                }
                else
                {
                    /*
                    if (!Program.ClientForm.IsHandleCreated)
                    {
                        Program.ClientForm.CreateHandle();
                    }
                    Program.ClientForm.Appendconsole("Successefully Signed in");
                     */ 
                    LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
                    if (false == file.writetofile(username, password, sysncpath))
                    {
                        Program.ClientForm.addtoConsole("Error : <<Unable to access config file. Please try Sign In>>");
                        //System.Windows.Forms.MessageBox.Show("Unable to access dblike file. Please try Sign In.", "Error Occured");
                        Program.ClientForm.addtoConsole("Exiting");
                        Program.ClientForm.signUpfailed();
                        Thread.CurrentThread.Abort();
                    }
                    else
                    {
                        
                        //Upload all the content
                        bool result = file.writetofile(username, password, sysncpath);
                        if (result == false)
                        {
                            Program.ClientForm.addtoConsole("Error : <<Unable to access config file. Please try Sign In>>");
                            //System.Windows.Forms.MessageBox.Show("Unable to write on the file. Please try Sign In.", "Unable to write on file");
                            Program.ClientForm.addtoConsole("Exiting");
                            Program.ClientForm.signUpfailed();
                            Thread.CurrentThread.Abort();
                            //return;
                        }
                        Program.ClientForm.addtoConsole("Initiating content upload");
                        uploadeverything(sysncpath);
                        //System.Windows.Forms.MessageBox.Show("Uploaded!!!", "Client");
                        //System.Windows.Forms.MessageBox.Show("Started!!!", "Client");


                        // initialize the file list for sign up scenario
                        Client.LocalFileSysAccess.FileListMaintain fileMaintain = new Client.LocalFileSysAccess.FileListMaintain();
                        fileMaintain.scanAllFilesAttributes();

                        Program.folderWatcher.start();
                        Program.ClientForm.addtoConsole("File watcher Installed");

                        Client.Program.poll.start();
                        Program.ClientForm.addtoConsole("Poll thread started");
                       

                        if (!Program.ClientForm.IsHandleCreated)
                        {
                            Program.ClientForm.CreateHandle();
                        }
                        //enable service controller
                        Program.ClientForm.signUppassed();

                        /*
                        Threads.FileSysWatchDog watchdog = new FileSysWatchDog();
                        if (watchdog.start() == false)
                        {
                            //disable stop service button 
                            //enable start service button
                        }
                         */ 
                    }
                    Program.ClientForm.addtoConsole("Exiting Sign Up thread");
                    Thread.CurrentThread.Abort();
                }
            }
        }
        static void uploadeverything(string path)
        {
            string filepath = path;
            string[] files;
            string[] directories;

            files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                // Process each file
                string eventType = "signUpStart";
                LocalFileSysAccess.getFileAttributes timestamp = new LocalFileSysAccess.getFileAttributes(file);
                fileBeingUsed.eventDetails eventdet = new fileBeingUsed.eventDetails();
                eventdet.datetime = timestamp.lastModified;
                eventdet.filepath = file;
                eventdet.eventType = eventType;
                if (Client.Program.filesInUse.alreadyPresent(eventdet))
                {
                    //return;
                }
                else
                {
                    Client.Program.filesInUse.addToList(eventdet);
                    Uploader upload = new Uploader();
                    upload.start(file, "signUpStart", null, timestamp.lastModified);
                }
                
            }

            directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                // Process each directory recursively
                uploadeverything(directory);
            }
        }
    }
}
