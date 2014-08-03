namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbsignin = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSignintb1 = new System.Windows.Forms.Button();
            this.txtPasstb1 = new System.Windows.Forms.TextBox();
            this.txtUserNametb1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbsignup = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCreateAcctb2 = new System.Windows.Forms.Button();
            this.btnBrowsetb2 = new System.Windows.Forms.Button();
            this.txtfoldertb2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpasstb2 = new System.Windows.Forms.TextBox();
            this.txtusernametb2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tbsignin.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tbsignup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tbsignin);
            this.tabControl1.Controls.Add(this.tbsignup);
            this.tabControl1.Location = new System.Drawing.Point(6, 268);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 261);
            this.tabControl1.TabIndex = 0;
            // 
            // tbsignin
            // 
            this.tbsignin.Controls.Add(this.groupBox2);
            this.tbsignin.Controls.Add(this.groupBox1);
            this.tbsignin.Location = new System.Drawing.Point(4, 25);
            this.tbsignin.Name = "tbsignin";
            this.tbsignin.Padding = new System.Windows.Forms.Padding(3);
            this.tbsignin.Size = new System.Drawing.Size(352, 232);
            this.tbsignin.TabIndex = 0;
            this.tbsignin.Text = "Sign in";
            this.tbsignin.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSignintb1);
            this.groupBox2.Controls.Add(this.txtPasstb1);
            this.groupBox2.Controls.Add(this.txtUserNametb1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(6, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 93);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // btnSignintb1
            // 
            this.btnSignintb1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSignintb1.Location = new System.Drawing.Point(215, 27);
            this.btnSignintb1.Name = "btnSignintb1";
            this.btnSignintb1.Size = new System.Drawing.Size(100, 43);
            this.btnSignintb1.TabIndex = 13;
            this.btnSignintb1.Text = "Sign in";
            this.btnSignintb1.UseVisualStyleBackColor = true;
            this.btnSignintb1.Click += new System.EventHandler(this.btnSignintb1_Click);
            // 
            // txtPasstb1
            // 
            this.txtPasstb1.Location = new System.Drawing.Point(96, 51);
            this.txtPasstb1.Name = "txtPasstb1";
            this.txtPasstb1.Size = new System.Drawing.Size(100, 22);
            this.txtPasstb1.TabIndex = 12;
            // 
            // txtUserNametb1
            // 
            this.txtUserNametb1.Location = new System.Drawing.Point(96, 27);
            this.txtUserNametb1.Name = "txtUserNametb1";
            this.txtUserNametb1.Size = new System.Drawing.Size(100, 22);
            this.txtUserNametb1.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pass";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "User Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(6, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 111);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Controller";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(16, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 31);
            this.button3.TabIndex = 13;
            this.button3.Text = " Versions Control";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(180, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 31);
            this.button2.TabIndex = 12;
            this.button2.Text = "Stop DBLike Service";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(16, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 31);
            this.button1.TabIndex = 11;
            this.button1.Text = "Start DBLike Service";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbsignup
            // 
            this.tbsignup.Controls.Add(this.groupBox3);
            this.tbsignup.Location = new System.Drawing.Point(4, 25);
            this.tbsignup.Name = "tbsignup";
            this.tbsignup.Padding = new System.Windows.Forms.Padding(3);
            this.tbsignup.Size = new System.Drawing.Size(352, 232);
            this.tbsignup.TabIndex = 1;
            this.tbsignup.Text = "Sign up";
            this.tbsignup.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.btnCreateAcctb2);
            this.groupBox3.Controls.Add(this.btnBrowsetb2);
            this.groupBox3.Controls.Add(this.txtfoldertb2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtpasstb2);
            this.groupBox3.Controls.Add(this.txtusernametb2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(6, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(338, 213);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(42, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "label7";
            // 
            // btnCreateAcctb2
            // 
            this.btnCreateAcctb2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateAcctb2.Location = new System.Drawing.Point(20, 116);
            this.btnCreateAcctb2.Name = "btnCreateAcctb2";
            this.btnCreateAcctb2.Size = new System.Drawing.Size(288, 35);
            this.btnCreateAcctb2.TabIndex = 15;
            this.btnCreateAcctb2.Text = "Create Account";
            this.btnCreateAcctb2.UseVisualStyleBackColor = true;
            this.btnCreateAcctb2.Click += new System.EventHandler(this.btnCreateAcctb2_Click);
            // 
            // btnBrowsetb2
            // 
            this.btnBrowsetb2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowsetb2.Location = new System.Drawing.Point(233, 77);
            this.btnBrowsetb2.Name = "btnBrowsetb2";
            this.btnBrowsetb2.Size = new System.Drawing.Size(75, 21);
            this.btnBrowsetb2.TabIndex = 14;
            this.btnBrowsetb2.Text = "Browse";
            this.btnBrowsetb2.UseVisualStyleBackColor = true;
            this.btnBrowsetb2.Click += new System.EventHandler(this.btnBrowsetb2_Click_1);
            // 
            // txtfoldertb2
            // 
            this.txtfoldertb2.Location = new System.Drawing.Point(117, 80);
            this.txtfoldertb2.Name = "txtfoldertb2";
            this.txtfoldertb2.Size = new System.Drawing.Size(100, 22);
            this.txtfoldertb2.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "Choose Folder";
            // 
            // txtpasstb2
            // 
            this.txtpasstb2.Location = new System.Drawing.Point(117, 53);
            this.txtpasstb2.Name = "txtpasstb2";
            this.txtpasstb2.PasswordChar = '*';
            this.txtpasstb2.Size = new System.Drawing.Size(100, 22);
            this.txtpasstb2.TabIndex = 11;
            // 
            // txtusernametb2
            // 
            this.txtusernametb2.Location = new System.Drawing.Point(117, 29);
            this.txtusernametb2.Name = "txtusernametb2";
            this.txtusernametb2.Size = new System.Drawing.Size(100, 22);
            this.txtusernametb2.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Choose Pass";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "User Name";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(351, 252);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSignintb1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 532);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DBLike Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbsignin.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tbsignup.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbsignin;
        private System.Windows.Forms.TabPage tbsignup;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSignintb1;
        private System.Windows.Forms.TextBox txtPasstb1;
        private System.Windows.Forms.TextBox txtUserNametb1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreateAcctb2;
        private System.Windows.Forms.Button btnBrowsetb2;
        private System.Windows.Forms.TextBox txtfoldertb2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpasstb2;
        private System.Windows.Forms.TextBox txtusernametb2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
    }
}

