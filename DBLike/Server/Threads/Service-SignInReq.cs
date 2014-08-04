using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlClient;

namespace Server.Threads
{
    class ServiceSignInReq
    {

        public Thread thread;
        public void start(Socket soc, string req)
        {
            thread = new Thread(() => threadStartFun(soc, req));
            thread.Start();
        }
        public void stop()
        {
            thread.Abort();
        }
        private void threadStartFun(Socket soc, string req)
        {
            Program.ServerForm.addtoConsole("Sign In thread Started");
            Message.MessageParser parser = new Message.MessageParser();
            MessageClasses.MsgSignIn.req reqobj = new MessageClasses.MsgSignIn.req();
            reqobj = parser.signInParseReq(req);
            ConnectionManager.DataBaseConn con = new ConnectionManager.DataBaseConn(1);
            SqlConnection conn = con.DBConnect();
            //check if user already exits
            UserAuth.SignInFunctions userauth = new UserAuth.SignInFunctions();
            if(userauth.userAuthentication(reqobj.userName,reqobj.psw)==true)
            {
                Program.ServerForm.addtoConsole("User Exists");
                MessageClasses.MsgSignIn.resp resp = new MessageClasses.MsgSignIn.resp();
                resp.ack = "OK";
                resp.addiMsg = "EXISTING";
                Message.CreateMsg msg = new Message.CreateMsg();
                string res = msg.signInResp(resp);
                SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                rw.writetoSocket(soc, res);
                Program.ServerForm.addtoConsole("Wrote Respose to scoket");
            }
            else
            {
                Program.ServerForm.addtoConsole("User Does Not Exist");
                MessageClasses.MsgSignIn.resp resp = new MessageClasses.MsgSignIn.resp();
                resp.ack = "ERRORS";
                resp.addiMsg = "NON-EXISTING";
                Message.CreateMsg msg = new Message.CreateMsg();
                string res = msg.signInResp(resp);
                SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                rw.writetoSocket(soc, res);
                Program.ServerForm.addtoConsole("Wrote Response to socket. Exiting");
                
            }
            Thread.CurrentThread.Abort();
        }
    }
}
