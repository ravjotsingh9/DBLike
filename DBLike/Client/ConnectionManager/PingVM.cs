using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Client.ConnectionManager
{
    public class PingVM
    {

    
        
        public IPAddress startPin(IPHostEntry ipHostInfo)
        {
            Program.ClientForm.addtoConsole("Started Pinging");
            IPAddress ipAddress = null;
            for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
            {
                if (pingFunction(ipHostInfo.AddressList[i]))
                 {
                     return ipHostInfo.AddressList[i];
                 }
            }

            return ipAddress; 
        }



        private bool pingFunction(IPAddress ipAddress)
        {
            Socket sender = null;
            try
            {
                
                //ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.
                sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                //sender.Connect(remoteEP);
                Program.ClientForm.addtoConsole("Request Sent...");
                IAsyncResult result = sender.BeginConnect(remoteEP, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(5000, true);
                
                if (success)
                {
                    Program.ClientForm.addtoConsole("Response received!");
                    Console.WriteLine("success");
                }
                Program.ClientForm.addtoConsole("Timed Out!");
                return success;
            }
            catch (SocketException e)
            {
                
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                sender.Close();
            }

        }
    }
}
