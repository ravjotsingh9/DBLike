using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Client.Threads
{
    class SignUp
    {
        public Thread thread;
        Configuration.config serverdetails = new Configuration.config();
        static ConnectionManager.Connection conn = new ConnectionManager.Connection();
        static Socket sender;
        public void start(String username, String password)
        {
            thread = new Thread(()=>threadStartFun(serverdetails.serverAddr ,serverdetails.port, username,password));
            //TBD
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun(string serverIP, int port,String username, String password )
        {
            
            //TBD
            MessageClasses.MsgSignUp msgobj = new MessageClasses.MsgSignUp(username,password);

            // Fill out the content in msgobj

            //call CreateMsg.createSignUpMsg(msgobj) get it in bytes form
            Message.CreateMsg msg=new Message.CreateMsg();
            String message = msg.signUpMsg(msgobj);

            //create a socket connection. you may need to create in Conection Manager
            sender = conn.connect(serverIP, port);
            
            //call  SocketCommunication.ReaderWriter.write(byte[] msg) to write msg on socket
            SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
            rw.writetoSocket(sender, message);
            //call  SocketCommunication.ReaderWriter.read() to read response from server
            String response=rw.readfromSocket(sender);

            //call parser and process it.....
            Message.MessageParser mp = new Message.MessageParser();
            msgobj = mp.signupParseMsg(response);
        }
    }
}
