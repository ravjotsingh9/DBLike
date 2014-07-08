using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Data;

namespace Client.SocketCommunication
{
    class ReaderWriter
    {

        //TDB socket reader function
        public string readfromSocket(Socket soc)
        {
            byte[] tmp = new byte[1024];
            soc.Receive(tmp);
            string str = System.Text.Encoding.ASCII.GetString(tmp);
            return str;
        }
        //TBD socket writer function

        public void writetoSocket(Socket soc, String message)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);
            int bytesSent = soc.Send(msg);
        }
    }
}
