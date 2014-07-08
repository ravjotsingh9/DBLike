using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Client.Threads
{
    class SignUp
    {
        Thread thread;
        Configuration.config serverdetails = new Configuration.config();
        static ConnectionManager.Connection conn = new ConnectionManager.Connection();
        static Socket snder = null; 
        public void start(String username, String Password)
        {
            thread = new Thread(()=>threadStartFun(serverdetails.serverAddr ,serverdetails.port, username, Password));
            //TBD
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string serverIP, int port, String username,string password)
        {
            //TBD
            MessageClasses.MsgSignUp msgobj = new MessageClasses.MsgSignUp(username,password);

            // Fill out the content in msgobj
            Message.CreateMsg sign = new Message.CreateMsg();

            //call CreateMsg.createSignUpMsg(msgobj) get it in bytes form
            String msg = sign.signUpMsg(msgobj);

            //create a socket connection. you may need to create in Conection Manager
            snder=conn.connect(serverIP, port);

            //call  SocketCommunication.ReaderWriter.write(byte[] msg) to write msg on socket
            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            rw.writeBytes(snder, msg);
            //call  SocketCommunication.ReaderWriter.read() to read response from server
            String response = rw.readBytes(snder, msg);

            //call parser and process it.....
            Message.MessageParser mp = new Message.MessageParser();
            msgobj=mp.signupParseMsg(response);
        }
    }
}
