using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
            Threads.SignUp signupthread = new Threads.SignUp();
            string username = txtusernametb2.Text;
            string password = txtpasstb2.Text;
            signupthread.start(username,password);
        }

    }
}
