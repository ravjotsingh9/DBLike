﻿using System;
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

        public void start( String username, String password, Form frm)
        {
            //TBD
            thread = new Thread(() => threadStartFun(serverdetails.serverAddr, serverdetails.port, username, password,frm));
            thread.Start();
         
        }
        public void stop()
        {
            //TBD
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
                    string path = null;
                    System.Windows.Forms.MessageBox.Show("Please select a path to download your folder from the server");
                    var t = new Thread((ThreadStart)(() =>
                    {
                        FolderBrowserDialog folder = new FolderBrowserDialog();
                        if(folder.ShowDialog() == DialogResult.OK)
                        {
                            path = folder.SelectedPath;
                        }
                    }));
                    t.IsBackground = true;
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                    t.Join();
                    file.writetofile(username, password, path);
                    PollFiles poll = new PollFiles();
                    poll.start();
                    FileSysWatchDog.Run();
                }
            }
        }
    }
}
