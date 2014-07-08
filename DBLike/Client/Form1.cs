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
        delegate void changeStatetoSignedUp();
        public void enableServiceController()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            changeStatetoSignedUp change = new changeStatetoSignedUp(enableController);
            this.Invoke(change);
        }
        void enableController()
        {
            groupBox1.Enabled = true;
        }


        public Form1()
        {
            InitializeComponent();
            groupBox1.Enabled = false;
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
            
            signupthread.start();
        }


        internal void CreateHandle()
        {
            this.CreateHandle();
            //throw new NotImplementedException();
        }

        private void btnSignintb1_Click(object sender, EventArgs e)
        {

        }
    }
}
