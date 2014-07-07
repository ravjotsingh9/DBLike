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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbsignin = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSignintb1 = new System.Windows.Forms.Button();
            this.txtPasstb1 = new System.Windows.Forms.TextBox();
            this.txtUserNametb1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbsignup = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCreateAcctb2 = new System.Windows.Forms.Button();
            this.btnBrowsetb2 = new System.Windows.Forms.Button();
            this.txtfoldertb2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpasstb2 = new System.Windows.Forms.TextBox();
            this.txtusernametb2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tbsignin.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tbsignup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tbsignin);
            this.tabControl1.Controls.Add(this.tbsignup);
            this.tabControl1.Location = new System.Drawing.Point(6, 5);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(349, 214);
            this.tabControl1.TabIndex = 0;
            // 
            // tbsignin
            // 
            this.tbsignin.Controls.Add(this.groupBox2);
            this.tbsignin.Controls.Add(this.groupBox1);
            this.tbsignin.Location = new System.Drawing.Point(4, 25);
            this.tbsignin.Name = "tbsignin";
            this.tbsignin.Padding = new System.Windows.Forms.Padding(3);
            this.tbsignin.Size = new System.Drawing.Size(341, 185);
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
            this.groupBox2.Location = new System.Drawing.Point(6, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 74);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // btnSignintb1
            // 
            this.btnSignintb1.Location = new System.Drawing.Point(215, 17);
            this.btnSignintb1.Name = "btnSignintb1";
            this.btnSignintb1.Size = new System.Drawing.Size(100, 46);
            this.btnSignintb1.TabIndex = 13;
            this.btnSignintb1.Text = "Sign in";
            this.btnSignintb1.UseVisualStyleBackColor = true;
            // 
            // txtPasstb1
            // 
            this.txtPasstb1.Location = new System.Drawing.Point(96, 43);
            this.txtPasstb1.Name = "txtPasstb1";
            this.txtPasstb1.Size = new System.Drawing.Size(100, 20);
            this.txtPasstb1.TabIndex = 12;
            // 
            // txtUserNametb1
            // 
            this.txtUserNametb1.Location = new System.Drawing.Point(96, 17);
            this.txtUserNametb1.Name = "txtUserNametb1";
            this.txtUserNametb1.Size = new System.Drawing.Size(100, 20);
            this.txtUserNametb1.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pass";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "User Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(6, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 90);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service Controller";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(180, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 46);
            this.button2.TabIndex = 12;
            this.button2.Text = "Stop DBLike Service";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 46);
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
            this.tbsignup.Size = new System.Drawing.Size(341, 185);
            this.tbsignup.TabIndex = 1;
            this.tbsignup.Text = "Sign up";
            this.tbsignup.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCreateAcctb2);
            this.groupBox3.Controls.Add(this.btnBrowsetb2);
            this.groupBox3.Controls.Add(this.txtfoldertb2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtpasstb2);
            this.groupBox3.Controls.Add(this.txtusernametb2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(329, 170);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // btnCreateAcctb2
            // 
            this.btnCreateAcctb2.Location = new System.Drawing.Point(18, 113);
            this.btnCreateAcctb2.Name = "btnCreateAcctb2";
            this.btnCreateAcctb2.Size = new System.Drawing.Size(288, 46);
            this.btnCreateAcctb2.TabIndex = 15;
            this.btnCreateAcctb2.Text = "Create Account";
            this.btnCreateAcctb2.UseVisualStyleBackColor = true;
            // 
            // btnBrowsetb2
            // 
            this.btnBrowsetb2.Location = new System.Drawing.Point(231, 71);
            this.btnBrowsetb2.Name = "btnBrowsetb2";
            this.btnBrowsetb2.Size = new System.Drawing.Size(75, 23);
            this.btnBrowsetb2.TabIndex = 14;
            this.btnBrowsetb2.Text = "Browse";
            this.btnBrowsetb2.UseVisualStyleBackColor = true;
            // 
            // txtfoldertb2
            // 
            this.txtfoldertb2.Location = new System.Drawing.Point(115, 73);
            this.txtfoldertb2.Name = "txtfoldertb2";
            this.txtfoldertb2.Size = new System.Drawing.Size(100, 20);
            this.txtfoldertb2.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Choose Folder";
            // 
            // txtpasstb2
            // 
            this.txtpasstb2.Location = new System.Drawing.Point(115, 45);
            this.txtpasstb2.Name = "txtpasstb2";
            this.txtpasstb2.Size = new System.Drawing.Size(100, 20);
            this.txtpasstb2.TabIndex = 11;
            // 
            // txtusernametb2
            // 
            this.txtusernametb2.Location = new System.Drawing.Point(115, 19);
            this.txtusernametb2.Name = "txtusernametb2";
            this.txtusernametb2.Size = new System.Drawing.Size(100, 20);
            this.txtusernametb2.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Choose Pass";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "User Name";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSignintb1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 226);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DBLike Client";
            this.tabControl1.ResumeLayout(false);
            this.tbsignin.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tbsignup.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
    }
}

