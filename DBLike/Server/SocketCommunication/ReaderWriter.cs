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

                byte[] tmp = new byte[2048];
                tmp = System.Text.Encoding.UTF8.GetBytes(str);
                soc.Send(tmp);
                return true;
            }
            catch (SocketException e)
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



        [MethodImpl(MethodImplOptions.Synchronized)]
        public string readfromSocket(Socket soc)
        {
            try
            {
                soc.ReceiveTimeout = 10000;

                // Read the  message sent by the client.
                // The client signals the end of the message using the
                // "<EOF>" marker.
                byte[] buffer = new byte[1024];
                StringBuilder messageData = new StringBuilder();
                int bytes = -1;

                do
                {

                    // Read the client's test message.
                    //bytes = sslStream.Read(buffer, 0, buffer.Length);
                    bytes = soc.Receive(buffer);

                    // Use Decoder class to convert from bytes to UTF8
                    // in case a character spans two buffers.
                    Decoder decoder = Encoding.UTF8.GetDecoder();
                    char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                    decoder.GetChars(buffer, 0, bytes, chars, 0);
                    messageData.Append(chars);


                    // Check for EOF or an empty message.
                    if (messageData.ToString().IndexOf("<EOF>") != -1)
                    {
                        break;
                    }
                } while (bytes != 0);

                return messageData.ToString();



            }
            catch (SocketException e)
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
