using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.VersionControl;
using Client.Threads;
using System.Threading;

namespace Client
{
    public partial class Form2 : Form
    {
        public blobCollection blobscollect;
        public blobCollection blobscollect2;
        public VCmanager VC;
        
        public Form2()
        {
            Program.ClientForm.addtoConsole("Initializing Form 2");
            InitializeComponent();
            Configuration.userInfo.containerURI = "null";
            VCThread vcThread = new VCThread();
            Thread thread = new Thread(() => vcThread.start());
            thread.Start();
            Program.ClientForm.addtoConsole("vcThread Started");
            thread.Join();
            if (Configuration.userInfo.containerURI != "null")
            {
                VC = new VCmanager(Configuration.userInfo.containerURI);
                blobscollect = VC.list();
                listBox1.DataSource = blobscollect.blobNames;
            }
            else
            {
                listBox1.DataSource = "Not able connect to Server";
            }
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            VC.revertFromSnapshot(blobscollect.blobList[listBox1.SelectedIndex], blobscollect.snapShotList[listBox2.SelectedIndex]);
           // Thread.CurrentThread.Abort();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;

            blobscollect2 = new blobCollection();
            blobscollect2 = VC.listSnapshot(blobscollect.blobList[i]);
            listBox2.DataSource = blobscollect2.snapShotNames;
            blobscollect.snapShotList = blobscollect2.snapShotList;
            blobscollect.snapShotNames = blobscollect2.snapShotNames;
        }
    }
}
