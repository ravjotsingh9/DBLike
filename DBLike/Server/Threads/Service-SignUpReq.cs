using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Data.SqlClient;

namespace Server.Threads
{
    class ServiceSignUpReq
    {

        public Thread thread;
        public void start(Socket soc, string req)
        {
            thread = new Thread(()=>threadStartFun(soc, req));
            thread.Start();
        }

        public void stop()
        {
            thread.Abort();
        }

        private void threadStartFun(Socket soc, string req)
        {
            //parse msg received
            Message.MessageParser parser = new Message.MessageParser();
            MessageClasses.MsgSignUp.req reqobj = new MessageClasses.MsgSignUp.req();
            reqobj = parser.signUpParseReq(req);

            //create connection with sql
            ConnectionManager.DataBaseConn con = new ConnectionManager.DataBaseConn(1);
            SqlConnection conn = con.DBConnect();

            //check if user already exits
            DatabaseAccess.Query q = new DatabaseAccess.Query();
            if (true == q.checkIfUserExists(reqobj.userName, conn))
            {
                MessageClasses.MsgSignUp.resp resp = new MessageClasses.MsgSignUp.resp();
                resp.ack = "ERRORS";
                resp.addiMsg = "AlreadyExist";
                Message.CreateMsg msg = new Message.CreateMsg();
                string res=  msg.signUpResp(resp);
                SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                rw.writetoSocket(soc, res);
                conn.Close();
                Thread.CurrentThread.Abort();
            }
            else
            {
                if (true == q.insertNewUser(reqobj.userName, reqobj.psw, conn))
                {
                    MessageClasses.MsgSignUp.resp resp = new MessageClasses.MsgSignUp.resp();
                    resp.ack = "OK";
                    resp.addiMsg = "Added";
                    Message.CreateMsg msg = new Message.CreateMsg();
                    string res = msg.signUpResp(resp);
                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    rw.writetoSocket(soc, res);
                    conn.Close();
                }
                else
                {
                    MessageClasses.MsgSignUp.resp resp = new MessageClasses.MsgSignUp.resp();
                    resp.ack = "ERRORS";
                    resp.addiMsg = "ERRORWHILEINSERTING";
                    Message.CreateMsg msg = new Message.CreateMsg();
                    string res = msg.signUpResp(resp);
                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    rw.writetoSocket(soc, res);
                    conn.Close();
                }
                Thread.CurrentThread.Abort();
            }
            //TBD
        }
    }
}
