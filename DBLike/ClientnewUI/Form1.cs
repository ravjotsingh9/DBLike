using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_newUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            //form2.MdiParent = this;
            LoadForm(form2);

        }



        private void LoadForm(Form frm)
        {
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            this.Hide();
            // Here you can set a bunch of properties, apply skins, save logs...
            // before you show any form
            frm.Show();
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btnSignintb1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            //form2.MdiParent = this;
            LoadForm(form3);
        }


    }
}
