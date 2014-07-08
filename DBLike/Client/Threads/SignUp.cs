using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Threads
{
    class SignUp
    {
        Thread thread;
        Configuration.config serverdetails = new Configuration.config();
        static ConnectionManager.Connection conn = new ConnectionManager.Connection();

        public void start()
        {
            thread = new Thread(()=>threadStartFun(serverdetails.serverAddr ,serverdetails.port));
            //TBD
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string serverIP, int port)
        {
            //TBD
            MessageClasses.MsgSignUp msgobj = new MessageClasses.MsgSignUp();

            // Fill out the content in msgobj


            //call CreateMsg.createSignUpMsg(msgobj) get it in bytes form

            //create a socket connection. you may need to create in Conection Manager
            //conn.connect(serverIP, port)
 
            //call  SocketCommunication.ReaderWriter.write(byte[] msg) to write msg on socket
             
            //call  SocketCommunication.ReaderWriter.read() to read response from server


            //call parser and process it.....

        }
    }
}
