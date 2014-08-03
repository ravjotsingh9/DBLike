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

namespace Client
{
    public partial class Form2 : Form
    {
        public blobCollection blobscollect;
        public blobCollection blobscollect2;
        public VCmanager VC;
        
        public Form2()
        {
            InitializeComponent();
            VC = new VCmanager();
            blobscollect = VC.list();
            listBox1.DataSource = blobscollect.blobNames;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            VC.revertFromSnapshot(blobscollect.blobList[listBox1.SelectedIndex], blobscollect.snapShotList[listBox2.SelectedIndex]);
               
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
