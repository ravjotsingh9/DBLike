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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowsetb2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            LoadForm(new Form4());

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

    }
}
