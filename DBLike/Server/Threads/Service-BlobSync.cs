using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Server.BlobAccess;

namespace Server.Threads
{
    class Service_BlobSync
    {
        private static bool responForSync = false;
        static Thread thread;
        public void start()
        {
            //TBD
            thread = new Thread(() => threadStartFun());
            thread.Start();
        }
        public void stop()
        {
            //TBD
            thread.Abort();
        }
        private void threadStartFun()
        {
            try
            {

                while (true)
                {
                    sync();
                    TimeSpan interval = new TimeSpan(0, 1, 0);
                    Thread.Sleep(interval);
                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                //System.IO.File.WriteAllText("errors.txt", e.ToString());
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }

       private void sync()
        {
            try
            {
                String strHostName = Dns.GetHostName();
                if (strHostName == "group3525")
                {
                    responForSync = true;
                }
                else if (strHostName == "cics525Acc2")
                {
                    Console.WriteLine("i am cics525Acc2 ");
                    if (pingFunction("group3525.cloudapp.net"))
                    {
                        responForSync = false;
                    }
                    else
                    {
                        responForSync = true;
                    }
                }
                else if (strHostName == "dblike")
                {
                    Console.WriteLine("dblike ");
                    if (pingFunction("group3525.cloudapp.net"))
                    {
                        responForSync = false;
                    }
                    else if (pingFunction("cics525acc2.cloudapp.net"))
                    {
                        responForSync = false;
                    }
                    else
                    {
                        responForSync = true;
                    }

                }

                if (responForSync)
                {
                    Console.WriteLine("i am responsible ");
                    // blob sync class;
                    blobCopy blobcopy = new blobCopy();
                    blobcopy.startCopyBlob();
                }
                else
                {
                    Console.WriteLine("i am not responsible ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private bool pingFunction(string domainName)
        {
            Socket sender = null;
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(domainName);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.
                sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                //sender.Connect(remoteEP);
                IAsyncResult result = sender.BeginConnect(remoteEP, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(10000, true);
                if (success)
                {
                    Console.WriteLine("success");
                }
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                sender.Close();
            }

        }
    }
}
