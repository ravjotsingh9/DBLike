﻿using System;
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
            
            //call  SocketCommunication.ReaderWriter.write(byte[] msg) to write msg on socket
            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            //System.Windows.Forms.MessageBox.Show("going to write socket:"+message, "SignUp Thread started");
            rw.writetoSocket(sender, message);
            //call  SocketCommunication.ReaderWriter.read() to read response from server
            //System.Windows.Forms.MessageBox.Show("going to read socket: " , "SignUp Thread started");
            String response=rw.readfromSocket(sender);
            //System.Windows.Forms.MessageBox.Show("read:"+ response, "SignUp Thread started");
            //call parser and process it.....
            Message.MessageParser mp = new Message.MessageParser();
            msgobj = mp.signUpParseMessage(response);

			// This functionality should be added here
			
			//call parser and process it.....
            if (!msgobj.ack.Equals(""))
            {
                Message.MessageParser msgparser = new Message.MessageParser();
                if (msgobj.ack.Equals("ERRORS"))
                {
                    System.Windows.Forms.MessageBox.Show("Some error occured!Please try again.", "Error Occured");
                    Thread.CurrentThread.Abort();
                }
                else
                {
                    LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
                    if (false == file.writetofile(username, password, sysncpath))
                    {
                        System.Windows.Forms.MessageBox.Show("Unable to access dblike file.", "Error Occured");

                    }
                    else
                    {
                        //Upload all the content
                        bool result = file.writetofile(username, password, sysncpath);
                        if (result == false)
                        {
                            System.Windows.Forms.MessageBox.Show("Unable to write on the file", "Unable to write on file");
                            return;
                        }
                        uploadeverything(sysncpath);
                        System.Windows.Forms.MessageBox.Show("Uploaded!!!", "Client");
                        
                        if (!Program.ClientForm.IsHandleCreated)
                        {
                            Program.ClientForm.CreateHandle();
                        }
                        //enable service controller
                        Program.ClientForm.enableServiceController();
                        
                        FileSysWatchDog.Run();
                        /*
                        Threads.FileSysWatchDog watchdog = new FileSysWatchDog();
                        if (watchdog.start() == false)
                        {
                            //disable stop service button 
                            //enable start service button
                        }
                         */ 
                    }
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
                Uploader upload = new Uploader();
                upload.start(file);
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
