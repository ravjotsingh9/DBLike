using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Threads
{
    class FileSysWatchDog
    {
        Thread btnclicked;

        public FileSysWatchDog()
        {
            btnclicked = new Thread(new ThreadStart(servicestart));
        }

        public void start()
        {
            btnclicked.Start();
        }
        public void stop()
        {
            btnclicked.Abort();
        }

        private void servicestart()
        {
            DateTime lastedited = File.GetLastWriteTime(@"textDoc.txt");
            while (true)
            {
                if (lastedited < File.GetLastWriteTime(@"textDoc.txt"))
                {
                    lastedited = File.GetLastWriteTime(@"textDoc.txt");
                    MessageBox.Show("filechanged!");
                    break;
                }
            }
            btnclicked.Abort();
        }
    }
}
