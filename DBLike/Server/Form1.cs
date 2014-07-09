using System;
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
        
        public Form1()
        {
            InitializeComponent();
            btnStart.Enabled = true;
            //btnStop.Enabled = false;
        }

        public void btnStart_Click(object sender, EventArgs e)
        {
            Server.start();
            lblServerStatus.Text = "Running";
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Server.stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
