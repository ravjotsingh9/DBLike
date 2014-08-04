using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public volatile bool shuttingDownComplete = false;
        static Threads.ServerConnListener Server = new Threads.ServerConnListener();
        static Threads.Service_BlobSync blobSync = new Threads.Service_BlobSync();
        static Thread syncingThread = new Thread(() => blobSync.start());
        Thread waiting;
        delegate void addToConsole(string str);
        /*
        delegate bool Wait();

        public bool initiateWait()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            Wait w = new Wait(Waiting);
            return (bool)this.Invoke(w);
        }
        public bool Waiting()
        {
            if (shuttingDownComplete)
            {
                return false;
            }
            return true;
        }
        */
        public void addtoConsole(string str)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            addToConsole app = new addToConsole(Appendconsole);
            this.Invoke(app, (object)str);
        }

        public void Appendconsole(string value)
        {
            /*
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(Appendconsole), new object[] { value });
                return;
            }
             */
            //console.ScrollToCaret();
            //console.AppendText(value + "\n" + Environment.NewLine + "> ");
            if (value.Equals("."))
            {
                console.AppendText(value + " ");
            }
            else
            {
                console.AppendText(value + "\n" + Environment.NewLine + "> ");
            }

        }

        public Form1()
        {
            InitializeComponent();
            btnStart.Enabled = true;
            //btnStop.Enabled = false;
            addtoConsole(" Initialized");
        }

        public void btnStart_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("For this Prototype, only Sign Up and Upload functionality is available.","Information");
            Server.start();
            lblServerStatus.Text = "Running";
            btnStart.Enabled = false;
            btnStart.ForeColor = Color.WhiteSmoke;
            btnStop.Enabled = true;
            
            //start blob sync
            Threads.Service_BlobSync.blobsSync = true;
            syncingThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            //stop blob sync, wait sync thread to join to close the program
            Threads.Service_BlobSync.blobsSync = false;
            Threads.Service_BlobSync.stopSyncEvent.Set();
            //Wait server to complete blob storage synchronization, may take some time to close the program...


            //Program.ServerForm.addtoConsole("Shutting Down...");
            Server.stop();

            if (syncingThread.IsAlive)
            {
                //syncingThread.Join();
                
                //stop blob sync, wait sync thread to join to close the program
                Threads.Service_BlobSync.blobsSync = false;
                //Wait server to complete blob storage synchronization, may take some time to close the program...
                btnStop.Enabled = false;
                btnStop.ForeColor = Color.WhiteSmoke;
                waiting = new Thread(wait);
                waiting.Start();
            }

            else
            {
                btnStop.Enabled = false;
                btnStop.ForeColor = Color.WhiteSmoke;
                //btnStart.Enabled = true;
            }
            
            //btnStart.Enabled = true;
        }

        public void wait()
        {
            while (!shuttingDownComplete)
            {
                Program.ServerForm.addtoConsole(".");
                Thread.Sleep(1000);
            }
            Application.Exit();
            //Thread.CurrentThread.Abort();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
