using System;
using System.Collections.Generic;
using System.Linq;
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
        public string readBytes(Socket snder, string mes)
        {
            byte[] bytes = new byte[1024];
            int bytesRec = snder.Receive(bytes);
            string response = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            return response;
        }


         //TBD socket writer function
        public void writeBytes(Socket snder,String mes)
        {
            byte[] bytes = new byte[1024];
            byte[] msg = Encoding.ASCII.GetBytes(mes);
            int bytesSent = snder.Send(msg);
        }
    }
}
