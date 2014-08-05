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
                //byte[] tmp = new byte[1024];
                //soc.Receive(tmp);


                // Read the  message sent by the server.
                // The end of the message is signaled using the
                // "<EOF>" marker.
                byte[] buffer = new byte[2048];
                StringBuilder messageData = new StringBuilder();
                int bytes = -1;

                do
                {
                    bytes = soc.Receive(buffer);

                    //bytes = sslStream.Read(buffer, 0, buffer.Length);

                    // Use Decoder class to convert from bytes to UTF8
                    // in case a character spans two buffers.
                    Decoder decoder = Encoding.UTF8.GetDecoder();
                    char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                    decoder.GetChars(buffer, 0, bytes, chars, 0);


                    messageData.Append(chars);
                    // Check for EOF.
                    if (messageData.ToString().IndexOf("<EOF>") != -1)
                    {
                        break;
                    }
                } while (bytes != 0);


                // get the msg
                return messageData.ToString();

                //string str = System.Text.Encoding.ASCII.GetString(tmp);
                //return str;
            }
            catch (SocketException e)
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
                byte[] msg = Encoding.UTF8.GetBytes(message);
                soc.SendTimeout = 10000;
                int bytesSent = soc.Send(msg);
                return true;
            }
            catch (SocketException e)
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
