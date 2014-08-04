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
        Thread waiting;
        Thread version;
        public volatile bool acountcreated = false;
        delegate void changeStatetoSignedUp();
        delegate void signupFailed();
        delegate void signinFailed();
        delegate void signinpassed();
        delegate void stopservice();
        delegate void addToConsole(string str);

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
            if (!btnSignintb1.Enabled)
            {
                return false;
            }
            return true;
        }

        public void ServiceStopped()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            stopservice sStop = new stopservice(stopService);
            this.Invoke(sStop);
        }

        public void stopService()
        {
            groupBox2.Enabled = true;
            btnCreateAcctb2.Enabled = true;
            btnSignintb1.Enabled = true;
            txtUserNametb1.Enabled = true;
            txtPasstb1.Enabled = true;
            vcbtn.Enabled = false;
        }

        public void addtoConsole(string str)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            addToConsole app = new addToConsole(Appendconsole);
            this.Invoke(app,(object)str);
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
            if(value.Equals("."))
            {
                console.AppendText(value + " ");
            }
            else
            {
                console.AppendText(value + "\n" + Environment.NewLine + "> ");
            }
            
            
        }
        public void signinpass()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            signinpassed spass = new signinpassed(signInpassed);
            this.Invoke(spass);
        }

        public void signInpassed()
        {
            btnSignintb1.Enabled = false;
            btnSignintb1.Text = "Sign in";
            button2.Enabled = true;
            groupBox2.Enabled = false;
            vcbtn.Enabled = true;
        }

        public void signinfail()
        {
            if(!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            signinFailed sfail = new signinFailed(signInfailed);
            this.Invoke(sfail);
        }
        public void signInfailed()
        {
            //pictureBox1.Visible = false;
            btnSignintb1.Enabled = true;
            btnSignintb1.Text = "Sign in";
            button2.Enabled = false;
            vcbtn.Enabled = false;
        }


        public void signupfail()
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            signinFailed sfail = new signinFailed(signUpfailed);
            this.Invoke(sfail);
        }
        public void signUpfailed()
        {
            //pictureBox1.Visible = false;
            btnCreateAcctb2.Enabled = true;
            btnCreateAcctb2.Text = "Create Account";
            button2.Enabled = false;
        }

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
            //groupBox1.Enabled = true;

            //disable start button inside it
            //button1.Enabled = false;
            btnSignintb1.Enabled = false;
            txtUserNametb1.Enabled = false;
            txtPasstb1.Enabled = false;
            button2.Enabled = true;
            ////disable create account 
            groupBox2.Enabled = false;

            //disable create Account
            btnCreateAcctb2.Enabled = false;
            btnCreateAcctb2.Text = "Create Account";
            //shift tosign in tab
            
            //tabControl1.SelectedIndex = 1;

        }


        public Form1()
        {
            
            InitializeComponent();
            Appendconsole(" Initialized");
            //groupBox2.BackColor = Color.FromArgb(50, Color.White);
            //groupBox1.Enabled = true;
            // watchdog = new Threads.FileSysWatchDog();
            button2.Enabled = false;
            //pictureBox1.Visible = false;
            // for testing convinence, set the local path
            /*
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path += @"\DBLike_test";
            txtfoldertb2.Text = path;
            */
            //Appendconsole("hi");
            Application.ApplicationExit += new EventHandler(OnAppExit);
        }


        public void OnAppExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            //Threads.FileSysWatchDog.Run();
            if(txtUserNametb1.Text == "" || txtPasstb1.Text == "")
            {
                MessageBox.Show("Username and Password field cannot be empty","DBLike Client Sign In");
                return;
            }
            Client.Program.folderWatcher.start();
            Client.Program.poll.pull = true;
            Client.Program.poll.start();
            //watchdog.start();
            //button1.Enabled = false;
            btnSignintb1.Enabled = false;
            button2.Enabled = true;
            enableController();
        }
        */
        private void button2_Click(object sender, EventArgs e)
        {
            Appendconsole("Stopping DBLike Services...");
            Client.Program.folderWatcher.stop();
            Appendconsole("File Watcher Closed...");
            Client.Program.poll.pull = false;
            Appendconsole("Shutting down Polling...");
            button2.Enabled = false;
            vcbtn.Enabled = false;
            waiting = new Thread(wait);
            waiting.Start();
            
        }
        public void wait()
        {
            while(initiateWait() != true)
            {
                addtoConsole(".");
                Thread.Sleep(1000);
            }
            Thread.CurrentThread.Abort();
        }

        private void btnCreateAcctb2_Click(object sender, EventArgs e)
        {
            Appendconsole("Initializing Account creation");
            btnCreateAcctb2.Text = "Creating...";
            if (acountcreated == true)
            {
                MessageBox.Show("Already Account Created.", "DBLike Client");
                Appendconsole("Already Account Created.");
                Appendconsole("Exiting");
                return;
            }
            if (txtfoldertb2.Text.Equals("") || txtusernametb2.Text.Equals("") || txtpasstb2.Text.Equals(""))
            {
                MessageBox.Show("All fields are Required.", "Information Missing");
                Appendconsole("All fields are Required.");
                Appendconsole("Exiting");
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
                    //addtoConsole();
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
                    label7.Text = "Username cannot contain any special characters except '-'";
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
            //vcbtn.Enabled = true;
            btnSignintb1.Enabled = false;
            btnSignintb1.Text = "Signing in...";
            //pictureBox1.Visible = true;
            Threads.SignIn signinthread = new Threads.SignIn();
            String username = txtUserNametb1.Text;
            String password = txtPasstb1.Text;
            signinthread.start(username, password, this);
            
            //button1.Enabled = false;
        }

        private void btnBrowsetb2_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                txtfoldertb2.Text = fbd.SelectedPath;
            }
        }

        private void vcBtn_Click(object sender, EventArgs e)
        {
            Appendconsole("Opening version control windows...");
            
            version = new Thread(versionControl);
            version.Start();
        }
        private void versionControl()
        {
            Program.ClientForm.addtoConsole("Version Control thread started");
            Form2 VCForm = new Form2();
            VCForm.ShowDialog();
        }
       
    }
}
