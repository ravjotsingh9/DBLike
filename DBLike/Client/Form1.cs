using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        Threads.FileSysWatchDog watchdog;
        public Form1()
        {
            InitializeComponent();
            watchdog = new Threads.FileSysWatchDog();
            button2.Enabled = false;
        }

        private void btnBrowsetb2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            watchdog.start();
            button1.Enabled = false;
            button2.Enabled = true;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            watchdog.stop();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void btnCreateAcctb2_Click(object sender, EventArgs e)
        {
            /*
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = "Myapplication.exe";

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process process = new Process();

            process.StartInfo = startInfo;

            process.Start();
             */ 
            
            Threads.SignUp signupthread = new Threads.SignUp();
            signupthread.start(); 
        }

        private void btnSignintb1_Click(object sender, EventArgs e)
        {
            ConnectionManager.Connection conn = new ConnectionManager.Connection();
            Configuration.config conf = new Configuration.config();
            conn.connect(conf.serverAddr, conf.port);
            Socket soc = conn.sender;
            string str = "abc";
            byte[] b = new byte[1024];
            b = System.Text.Encoding.ASCII.GetBytes(str);
            soc.Send(b);
        }

    }
}
