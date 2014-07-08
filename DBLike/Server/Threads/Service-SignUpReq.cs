using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

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
            Message.MessageParser parser = new Message.MessageParser();
            MessageClasses.MsgSignUp.req reqobj = new MessageClasses.MsgSignUp.req();
            reqobj = parser.signUpParseReq(req);

            //TBD
        }
    }
}
