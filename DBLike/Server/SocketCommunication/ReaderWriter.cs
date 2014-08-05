using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Server.SocketCommunication
{
    class ReaderWriter
    {
        //TDB socket reader function
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool writetoSocket(Socket soc, string str)
        {
            try
            {
                soc.SendTimeout = 10000;
                byte[] tmp = new byte[1024];
                tmp = System.Text.Encoding.ASCII.GetBytes(str);
                soc.Send(tmp);
                return true;
            }
            catch(SocketException e)
            {
                if (e.SocketErrorCode == SocketError.TimedOut)
                {
                    Program.ServerForm.addtoConsole("SocketException[Writing to Socket]: Timeout");
                }
                else
                {
                    Program.ServerForm.addtoConsole("SocketException[Writing to Socket]: " + e.Message);
                }
                return false;
            }
        }


        //TBD socket writer function
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
                    Program.ServerForm.addtoConsole("SocketException[Reading from Socket]: Timeout");
                }
                else
                {
                    Program.ServerForm.addtoConsole("SocketException[Reading from Socket]: " + e.Message);
                }
                return null;
            }
        }
    }
}
