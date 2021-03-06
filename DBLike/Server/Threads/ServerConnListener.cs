﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.Threads
{
    class ServerConnListener
    {
        static volatile bool varstop;
        static Socket handler;
        static Thread ServerMainthread = new Thread(new ThreadStart(listen));
        public void start()
        {
            ServerMainthread.Start();
        }

        public void stop()
        {

        //    stopListening();
            //ServerMainthread.Abort();
            if (ServerMainthread.IsAlive == true)
            {
                // Establish the remote endpoint for the socket.
                Program.ServerForm.addtoConsole("Shutting down the server");
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);


                // Create a TCP/IP  socket.
                Socket snder = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    varstop = true;
                    snder.Connect(remoteEP);
                }
                catch (Exception ex)
                {
                    Program.ServerForm.addtoConsole("Exception:" + ex.Message);
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                snder.Shutdown(SocketShutdown.Both);
                snder.Close();
                // Abort watchdog thread also
                /*
                if (watchdog.IsAlive == true)
                {
                    watchdog.Abort();
                }
                 */ 
            }
            else
            {
                 Application.Exit();
            }
        }

        static public void listen()
        {
            //System.Windows.Forms.MessageBox.Show("Servermainthread started", "Server");


            Program.ServerForm.addtoConsole("Starting Server...");
            //**************setting to connect to local server **********************
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            //***************************************************************************/




            /*************** Setting to run on VM*******************************
            // Iterate through IP list and find IPV4
            
            bool found = false;
            int i = 0;
            IPAddress ipAddress = null;

            String strHostName = Dns.GetHostName();
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);
            while (!found)
            {
                if (!iphostentry.AddressList[i].IsIPv6LinkLocal)
                {
                    ipAddress = iphostentry.AddressList[i];
                    found = true;
                }
                i++;
                if (i == 10)
                {
                    found = true;
                }
            }
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            *********************************************************************/

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Program.ServerForm.addtoConsole("Server started");
                while(true)
                {
                    Program.ServerForm.addtoConsole("Listening...");
                    handler = listener.Accept();
                    Program.ServerForm.addtoConsole("Received Conn from "+ ((IPEndPoint)handler.RemoteEndPoint).Address.ToString());
                    if (varstop)
                    {
                        Program.ServerForm.addtoConsole("Shutting down...");
                        //if (DialogResult.Yes == MessageBox.Show("Do you really want to shut down server? ", "Allow", MessageBoxButtons.YesNo))
                        //{
                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();
                            //Application.Exit();
                            break;
                        //}
                        //varstop = false;
                    }
                    //TBD
                    /*
                    byte[] str = new byte[1024];
                    handler.Receive(str);
                    Console.WriteLine(str.ToString());
                    System.Windows.Forms.MessageBox.Show(System.Text.Encoding.ASCII.GetString(str));
                     */ 
                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    
                    string req = rw.readfromSocket(handler);
                    Program.ServerForm.addtoConsole(req);
                    string reqtype = findReqType(req);
                    switch(reqtype)
                    {
                        case "SIGNUP":
                            {
                                //System.Windows.Forms.MessageBox.Show("Switch signUp", "Server");
                                Program.ServerForm.addtoConsole("Request Type:" + reqtype);
                                Threads.ServiceSignUpReq obj = new ServiceSignUpReq();
                                obj.start(handler, req);
                                break;
                            }

                        case "SIGNIN":
                            {
                                Program.ServerForm.addtoConsole("Request Type:" + reqtype);
                                ServiceSignInReq obj = new ServiceSignInReq();
                                obj.start(handler, req);
                                break;
                            }

                        case "POLL":
                            {
                                Program.ServerForm.addtoConsole("Request Type:" + reqtype);
                                ServicePollReq obj = new ServicePollReq();
                                obj.start(handler, req);
                                break;
                            }

                        case "UPLOAD":
                            {
                                Program.ServerForm.addtoConsole("Request Type:" + reqtype);
                                //System.Windows.Forms.MessageBox.Show("Switch Upload", "Server");
                                ServiceUploadReq obj = new ServiceUploadReq();
                                obj.start(handler, req);
                                //obj.stop();
                                break;
                            }

                        case "DOWNLOAD":
                            {
                                Program.ServerForm.addtoConsole("Request Type:" + reqtype);
                                ServiceDownloadReq obj = new ServiceDownloadReq();
                                obj.start(handler, req);
                                break;
                            }

                        case "NOTIFICATION":
                            {
                                Program.ServerForm.addtoConsole("Request Type:" + reqtype);
                                Service_Notification obj = new Service_Notification();
                                obj.start(handler, req);
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.ServerForm.addtoConsole(ex.ToString());
            }
        }
        bool stopListening()
        {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            return true;
        }

        static string findReqType(string msg)
        {
            Program.ServerForm.addtoConsole(msg);
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return words[0];
        }
    }
}
