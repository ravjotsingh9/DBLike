using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Threads
{
    class Uploader
    {
        Thread thread = new Thread(new ThreadStart(threadStartFun));
        public void start()
        {
            //TBD
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        static private void threadStartFun()
        {
            //TBD
        }
    }
}
