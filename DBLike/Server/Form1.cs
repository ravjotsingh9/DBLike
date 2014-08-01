﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        static Threads.ServerConnListener Server = new Threads.ServerConnListener();
        static Threads.Service_BlobSync blobSync = new Threads.Service_BlobSync();
        
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
            //blobSync.start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Server.stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
