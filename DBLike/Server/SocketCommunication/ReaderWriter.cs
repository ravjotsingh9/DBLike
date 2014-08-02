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
            byte[] tmp = new byte[1024];
            tmp = System.Text.Encoding.ASCII.GetBytes(str);
            soc.Send(tmp);
            return true;
        }


        //TBD socket writer function
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string readfromSocket(Socket soc)
        {
            byte[] tmp = new byte[1024];
            soc.Receive(tmp);
            string str = System.Text.Encoding.ASCII.GetString(tmp);
            return str;

        }
    }
}
