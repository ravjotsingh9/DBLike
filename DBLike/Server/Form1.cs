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
        static Threads.ServerConnListener Server = new Threads.ServerConnListener();
        static Threads.Service_BlobSync blobSync = new Threads.Service_BlobSync();
        static Thread syncingThread = new Thread(() => blobSync.start());
        
        public Form1()
        {
            InitializeComponent();
            btnStart.Enabled = true;
            //btnStop.Enabled = false;
        }

        public void btnStart_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("For this Prototype, only Sign Up and Upload functionality is available.","Information");
            Server.start();
            lblServerStatus.Text = "Running";
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            
            //start blob sync
            Threads.Service_BlobSync.blobsSync = true;
            syncingThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //stop blob sync, wait sync thread to join to close the program
            Threads.Service_BlobSync.blobsSync = false;
            //Wait server to complete blob storage synchronization, may take some time to close the program...
            syncingThread.Join();

            Server.stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
