﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Threads
{
    class ServiceDownloadReq
    {

        public Thread thread;
        public void start(Socket soc, string req)
        {
            thread = new Thread(() => threadStartFun(soc,req));
            thread.Start();
        }
        public void stop()
        {
            thread.Abort();
        }
        private void threadStartFun(Socket soc, string req)
        {

        }
    }
}
