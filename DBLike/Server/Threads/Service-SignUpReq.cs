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
            Program.ServerForm.addtoConsole("Service SignUpReq Thread Started");
            //parse msg received
            //System.Windows.Forms.MessageBox.Show("Signup service started:"+ req, "Server");
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
                con.DBClose();
                //System.Windows.Forms.MessageBox.Show("Signup service if user exists", "Server");
                MessageClasses.MsgSignUp.resp resp = new MessageClasses.MsgSignUp.resp();
                resp.ack = "ERRORS";
                resp.addiMsg = "AlreadyExist";
                Message.CreateMsg msg = new Message.CreateMsg();
                string res=  msg.signUpResp(resp);
                SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                rw.writetoSocket(soc, res);
                Program.ServerForm.addtoConsole("Writing response to Socket");
                Program.ServerForm.addtoConsole("Exiting");
                Thread.CurrentThread.Abort();
            }
            else
            {
                ConnectionManager.DataBaseConn con1 = new ConnectionManager.DataBaseConn(1);
                SqlConnection conn1 = con.DBConnect();
                if (true == q.insertNewUser(reqobj.userName, reqobj.psw, conn1))
                {
                    
                    Program.ServerForm.addtoConsole("New User Added");
                    conn1.Close();
                    //System.Windows.Forms.MessageBox.Show("Signup service insert user", "Server");
                    MessageClasses.MsgSignUp.resp resp = new MessageClasses.MsgSignUp.resp();
                    resp.ack = "OK";
                    resp.addiMsg = "Added";
                    Message.CreateMsg msg = new Message.CreateMsg();
                    string res = msg.signUpResp(resp);
                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    rw.writetoSocket(soc, res);
                    Program.ServerForm.addtoConsole("Wrote response to Socket");
                    
                    //conn.Close();
                }
                else
                {
                    conn1.Close();
                    Program.ServerForm.addtoConsole("Sign Up Error while inserting to Database");
                    //System.Windows.Forms.MessageBox.Show("Signup service could insert", "Server");
                    MessageClasses.MsgSignUp.resp resp = new MessageClasses.MsgSignUp.resp();
                    resp.ack = "ERRORS";
                    resp.addiMsg = "ERRORWHILEINSERTING";
                    Message.CreateMsg msg = new Message.CreateMsg();
                    string res = msg.signUpResp(resp);
                    SocketCommunication.ReaderWriter rw = new SocketCommunication.ReaderWriter();
                    rw.writetoSocket(soc, res);
                    Program.ServerForm.addtoConsole("Wrote Respose to Socket");
                    //conn.Close();
                }
                //conn.Close();
                Program.ServerForm.addtoConsole("Exiting");
                Thread.CurrentThread.Abort();
            }
            //TBD
        }
    }
}
