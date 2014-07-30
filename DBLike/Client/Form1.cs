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
        //public static Threads.FileSysWatchDog watchdog;
         public volatile bool acountcreated= false;
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
            acountcreated = true;
            groupBox1.Enabled = true;

            //disable start button inside it
            button1.Enabled = false;
            button2.Enabled = true;

            ////disable login 
            groupBox2.Enabled = false;

            //disable create Account
            btnCreateAcctb2.Enabled = false;
            //shift tosign in tab
            
            tabControl1.SelectedIndex = 1;

        }


        public Form1()
        {
            InitializeComponent();
            groupBox1.Enabled = true;
           // watchdog = new Threads.FileSysWatchDog();
            button2.Enabled = true;            

            // for testing convinence, set the local path
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path += @"\DBLike_test";
            txtfoldertb2.Text = path;
            
        }


        private void btnBrowsetb2_Click(object sender, EventArgs e)
        {
            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Threads.FileSysWatchDog.Run();
            //watchdog.start();
            button1.Enabled = false;
            button2.Enabled = true;
            enableController();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Threads.FileSysWatchDog.stop();
            //watchdog.stop();
            button1.Enabled = true;
            button2.Enabled = false;
            btnCreateAcctb2.Enabled = true;
            btnSignintb1.Enabled = true;
        }

        private void btnCreateAcctb2_Click(object sender, EventArgs e)
        {
            if(acountcreated == true)
            {
                MessageBox.Show("Already Account Created.", "DBLike Client");
                return;
            }
            if(txtfoldertb2.Text.Equals("") || txtusernametb2.Text.Equals("") || txtpasstb2.Text.Equals(""))
            {
                MessageBox.Show("All fields are Required.","Information Missing");
                return;
            }
            else
            {
                MessageBox.Show("Now, DBLike service will start and will automatically upload the content of your selected folder. Creating and Modifying folder content will Upload the file to Server.", "DBLike Client Information");
            }
            Threads.SignUp signupthread = new Threads.SignUp();
			LocalDbAccess.LocalDB file = new LocalDbAccess.LocalDB();
            String username = txtusernametb2.Text;
            String password = txtpasstb2.Text;


            //bool result=file.writetofile(username,password,txtfoldertb2.Text);
            /*
            if (result == false)
            {
                MessageBox.Show("Unable to write on the file", "Unable to write on file");
                return;
            }
             */ 
            //file.writetofile(username,password,txtfoldertb2.Text);
            signupthread.start(username, password, txtfoldertb2.Text);
        }


        internal void CreateHandle()
        {
            this.CreateHandle();
            //throw new NotImplementedException();
        }

        private void btnSignintb1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("We are working on this part.","Functionality Not yet Completed");
            //return;
            
            Threads.SignIn signinthread = new Threads.SignIn();
            String username = txtUserNametb1.Text;
            String password = txtPasstb1.Text;
            signinthread.start(username,password,this);
            btnSignintb1.Enabled = false;
            button1.Enabled = false;
        }

        private void btnBrowsetb2_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                txtfoldertb2.Text = fbd.SelectedPath;
            }
        }
    }
}
