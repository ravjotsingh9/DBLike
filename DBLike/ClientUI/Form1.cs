using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            setAllPanelsToInvisible();
            // display the first panel
            panel1.Visible = true;
        }

        // set all panels to invisible when fist launch and also before wake up the next panel
        public void setAllPanelsToInvisible()
        {
            List<Panel> panelList = new List<Panel>();
            panelList.Add(panel1);
            panelList.Add(panel2);
            panelList.Add(panel3);
            panelList.Add(panel4);

            foreach (Panel panel in panelList)
            {
                panel.Visible = false;
                menuStrip1.Visible = false;
            }
        }

        // display the choose folder panel
        private void btnSignintb1_Click(object sender, EventArgs e)
        {
            setAllPanelsToInvisible();
            panel3.Visible = true;
        }

        // display the sign up panel
        private void button1_Click(object sender, EventArgs e)
        {
            setAllPanelsToInvisible();
            panel2.Visible = true;
        }

        // display the choose folder panel
        private void button3_Click(object sender, EventArgs e)
        {
            setAllPanelsToInvisible();
            panel3.Visible = true;
        }

        // display the sign in panel
        private void button2_Click(object sender, EventArgs e)
        {
            setAllPanelsToInvisible();
            panel1.Visible = true;
        }


        // open the file chooser
        private void btnBrowsetb2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        // display the file panel
        private void button4_Click(object sender, EventArgs e)
        {
            setAllPanelsToInvisible();
            panel4.Visible = true;
            menuStrip1.Visible = true;
        }







    }
}
