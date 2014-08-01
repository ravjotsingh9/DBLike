﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.ConnectionManager
{
    public class Connection
    {

        public Socket sender{get; set;}
        
        public Socket connect(string serverIP, int port)
        {
            // Connect to a remote device.
            try
            {

                #region Localhost connection 
                
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
           
                #endregion

                #region Remote connection
                
                //PingVM getIpAddress = new PingVM();
                //IPHostEntry ipHostInfo = Dns.GetHostEntry("server.lovecics525.net");
                //IPAddress ipAddress = getIpAddress.startPin(ipHostInfo);
                //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
       
                #endregion

                // Create a TCP/IP  socket.
                sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
                try 
                {
                    sender.Connect(remoteEP);
                    return sender;
                } 
                catch (ArgumentNullException ane) 
                {
                    Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
                    return null ;
                } 
                catch (SocketException se) 
                {
                    Console.WriteLine("SocketException : {0}",se.ToString());
                    return null;
                } 
                catch (Exception e) 
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    return null;
                }

            } 
            catch (Exception e) 
            {
                Console.WriteLine( e.ToString());
                return null;
            }
        }


        public bool disconnect(Socket soc)
        {
            // Release the socket.
            soc.Shutdown(SocketShutdown.Both);
            soc.Close();
            return true;
        }
    }
}
