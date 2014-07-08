using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Threads
{
    class ServerConnListener
    {
        static Socket handler;
        static Thread ServerMainthread = new Thread(new ThreadStart(listen));
        public void start()
        {
            ServerMainthread.Start();
        }

        public void stop()
        {
        //    stopListening();
            ServerMainthread.Abort();
        }

        static public void listen()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                handler = listener.Accept();

                //TBD
                /*
                byte[] str = new byte[1024];
                handler.Receive(str);
                Console.WriteLine(str.ToString());
                System.Windows.Forms.MessageBox.Show(System.Text.Encoding.ASCII.GetString(str));
                 */ 
                SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                string req = rw.readfromSocket(handler);
                string reqtype = findReqType(req);
                switch(reqtype)
                {
                    case "SIGNUP":
                        {
                            Threads.ServiceSignUpReq obj = new ServiceSignUpReq();
                            obj.start(handler, req);
                            break;
                        }

                    case "SIGNIN":
                        {
                            ServiceSignInReq obj = new ServiceSignInReq();
                            obj.start(handler, req);
                            break;
                        }

                    case "POLL":
                        {
                            ServicePollReq obj = new ServicePollReq();
                            obj.start(handler, req);
                            break;
                        }

                    case "UPLOAD":
                        {
                            ServiceUploadReq obj = new ServiceUploadReq();
                            obj.start(handler, req);
                            break;
                        }

                    case "DOWNLOAD":
                        {
                            ServiceDownloadReq obj = new ServiceDownloadReq();
                            obj.start(handler, req);
                            break;
                        }

                    case "NOTIFICATION":
                        {
                            Service_Notification obj = new Service_Notification();
                            obj.start(handler, req);
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
            string[] separators = { "<", ">:<", ">" };
            string[] words = msg.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return words[0];
        }
    }
}
