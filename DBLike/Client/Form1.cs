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
using System.Text.RegularExpressions;

namespace Client
{
    public partial class Form1 : Form
    {
        //public static Threads.FileSysWatchDog watchdog;
        public volatile bool acountcreated = false;
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
            Client.Program.poll.pull = true;
            Client.Program.poll.start();
            //watchdog.start();
            button1.Enabled = false;
            button2.Enabled = true;
            enableController();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Threads.FileSysWatchDog.stop();
            Client.Program.poll.pull = false;
            //watchdog.stop();
            button1.Enabled = true;
            button2.Enabled = false;
            btnCreateAcctb2.Enabled = true;
            btnSignintb1.Enabled = true;
        }

        private void btnCreateAcctb2_Click(object sender, EventArgs e)
        {
            if (acountcreated == true)
            {
                MessageBox.Show("Already Account Created.", "DBLike Client");
                return;
            }
            if (txtfoldertb2.Text.Equals("") || txtusernametb2.Text.Equals("") || txtpasstb2.Text.Equals(""))
            {
                MessageBox.Show("All fields are Required.", "Information Missing");
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
            //System.Windows.Forms.MessageBox.Show(username.Length.ToString());

            bool startWithLetterNum = false;
            bool definedLength = false;
            bool noUpperCase = false;
            bool notZforCoonection = false;
            bool noSpecialChar = false;
            bool hasSpecificFormat = false;
            if (username != "zforconnection")
                notZforCoonection = true;
            if (username.Length > 3 && username.Length < 63)
                definedLength = true;
            if (!username.Any(Char.IsUpper))
                noUpperCase = true;
            if (Char.IsLetter(username[0]) || Char.IsNumber(username[0]))
                startWithLetterNum = true;
            Regex r = new Regex("^[a-zA-Z0-9-]*$");
            if (r.IsMatch(username))
                noSpecialChar = true;

            if (username.Contains("-"))
            {
                for (int i = 0; i < username.Length; i++)
                {
                    if (username[i].ToString() == "-")
                    {
                        if (i != 0 && i != username.Length - 1)
                        {
                            if ((Char.IsLetter(username[i - 1]) || Char.IsNumber(username[i - 1]))
                                && (Char.IsLetter(username[i + 1]) || Char.IsNumber(username[i + 1])))
                            {
                                hasSpecificFormat = true;
                            }
                            else
                            {
                                hasSpecificFormat = false;
                                goto Validation;
                            }
                        }
                        else if (i == username.Length - 1)
                        {
                            if (Char.IsLetter(username[i - 1]) || Char.IsNumber(username[i - 1]))
                            {
                                hasSpecificFormat = true;
                            }
                            else
                            {
                                hasSpecificFormat = false;
                                goto Validation;
                            }
                        }
                    }
                }
            }
            else
                hasSpecificFormat = true;

        Validation:
            {
                if (startWithLetterNum != true)
                {
                    label7.Text = "Username must start with a Character or a Number only";
                    label7.Enabled = true;
                }
                else if (definedLength != true)
                {
                    label7.Text = "Username must be atleast of length 3 and atmost oflength 63";
                    label7.Enabled = true;
                }
                else if (noUpperCase != true)
                {
                    label7.Text = "Username cannot have any uppercase characters";
                    label7.Enabled = true;
                }
                else if (notZforCoonection != true)
                {
                    label7.Text = "Username provided is invalid, please choose another username";
                    label7.Enabled = true;
                }
                else if (noSpecialChar != true)
                {
                    label7.Text = "Username cannot contain anyspecial characters";
                    label7.Enabled = true;
                }
                else if (hasSpecificFormat != true)
                {
                    label7.Text = "Cannot have consecutive '-' characters";
                    label7.Enabled = true;
                }
                else
                {
                    label7.Text = "Username Validated";
                    label7.ForeColor = System.Drawing.Color.Green;
                    label7.Enabled = true;
                    signupthread.start(username, password, txtfoldertb2.Text);
                }
            }
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
            signinthread.start(username, password, this);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Form2 VCForm = new Form2();
            VCForm.ShowDialog();
        }
    }
}
