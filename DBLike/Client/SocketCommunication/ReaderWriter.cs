using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data;
using System.Runtime.CompilerServices;

namespace Client.SocketCommunication
{
    class ReaderWriter
    {

        //TDB socket reader function
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string readfromSocket(Socket soc)
        {
            try
            {
                soc.ReceiveTimeout = 10000;
                byte[] tmp = new byte[1024];
                soc.Receive(tmp);
                
                string str = System.Text.Encoding.ASCII.GetString(tmp);
                return str;
            }
            catch(SocketException e)
            {
                if (e.SocketErrorCode == SocketError.TimedOut)
                {
                    Program.ClientForm.addtoConsole("SocketException[Reading from Socket]: Timeout");
                }
                else
                {
                    Program.ClientForm.addtoConsole("SocketException[Reading from Socket]: " + e.Message);
                }
                return null;
            }
        }
        //TBD socket writer function
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool writetoSocket(Socket soc, String message)
        {
            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(message);
                soc.SendTimeout = 10000;
                int bytesSent = soc.Send(msg);
                return true;
            }
            catch(SocketException e)
            {
                if (e.SocketErrorCode == SocketError.TimedOut)
                {
                    Program.ClientForm.addtoConsole("SocketException[Writing to Socket]: Timeout");
                }
                else
                {
                    Program.ClientForm.addtoConsole("SocketException[Writing to Socket]: " + e.Message);
                }
                return false;
            }
        }
    }
}
