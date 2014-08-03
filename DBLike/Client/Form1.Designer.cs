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
                /******************tray****************/
                trayIcon.Dispose();
                /************************************/
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPasstb1 = new System.Windows.Forms.TextBox();
            this.txtUserNametb1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSignintb1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCreateAcctb2 = new System.Windows.Forms.Button();
            this.btnBrowsetb2 = new System.Windows.Forms.Button();
            this.txtfoldertb2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpasstb2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtusernametb2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.console = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPasstb1);
            this.groupBox1.Controls.Add(this.txtUserNametb1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnSignintb1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(1, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 289);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(9, 24);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(3);
            this.label8.Size = new System.Drawing.Size(79, 30);
            this.label8.TabIndex = 87;
            this.label8.Text = "Sign In";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Lucida Handwriting", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(183, 179);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3);
            this.label6.Size = new System.Drawing.Size(121, 37);
            this.label6.TabIndex = 86;
            this.label6.Text = "DBLike";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(32, 102);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 85;
            this.label4.Text = "Password  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(32, 79);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3);
            this.label5.Size = new System.Drawing.Size(70, 20);
            this.label5.TabIndex = 84;
            this.label5.Text = "User Name";
            // 
            // txtPasstb1
            // 
            this.txtPasstb1.Location = new System.Drawing.Point(103, 102);
            this.txtPasstb1.Name = "txtPasstb1";
            this.txtPasstb1.PasswordChar = '*';
            this.txtPasstb1.Size = new System.Drawing.Size(100, 20);
            this.txtPasstb1.TabIndex = 2;
            // 
            // txtUserNametb1
            // 
            this.txtUserNametb1.Location = new System.Drawing.Point(103, 79);
            this.txtUserNametb1.Name = "txtUserNametb1";
            this.txtUserNametb1.Size = new System.Drawing.Size(100, 20);
            this.txtUserNametb1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Location = new System.Drawing.Point(121, 125);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(82, 26);
            this.button2.TabIndex = 4;
            this.button2.Text = "Sign off";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSignintb1
            // 
            this.btnSignintb1.BackColor = System.Drawing.Color.White;
            this.btnSignintb1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSignintb1.BackgroundImage")));
            this.btnSignintb1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSignintb1.FlatAppearance.BorderSize = 0;
            this.btnSignintb1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSignintb1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignintb1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSignintb1.Location = new System.Drawing.Point(35, 125);
            this.btnSignintb1.Name = "btnSignintb1";
            this.btnSignintb1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnSignintb1.Size = new System.Drawing.Size(83, 26);
            this.btnSignintb1.TabIndex = 3;
            this.btnSignintb1.Text = "Sign in";
            this.btnSignintb1.UseVisualStyleBackColor = false;
            this.btnSignintb1.Click += new System.EventHandler(this.btnSignintb1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnCreateAcctb2);
            this.groupBox2.Controls.Add(this.btnBrowsetb2);
            this.groupBox2.Controls.Add(this.txtfoldertb2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtpasstb2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtusernametb2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(388, -5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 289);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(6, 24);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(3);
            this.label9.Size = new System.Drawing.Size(87, 30);
            this.label9.TabIndex = 88;
            this.label9.Text = "Sign Up";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(36, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 79;
            this.label7.Text = "<<>>";
            // 
            // btnCreateAcctb2
            // 
            this.btnCreateAcctb2.BackColor = System.Drawing.Color.White;
            this.btnCreateAcctb2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreateAcctb2.BackgroundImage")));
            this.btnCreateAcctb2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCreateAcctb2.FlatAppearance.BorderSize = 0;
            this.btnCreateAcctb2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCreateAcctb2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateAcctb2.ForeColor = System.Drawing.Color.Black;
            this.btnCreateAcctb2.Location = new System.Drawing.Point(39, 165);
            this.btnCreateAcctb2.Name = "btnCreateAcctb2";
            this.btnCreateAcctb2.Padding = new System.Windows.Forms.Padding(41, 0, 0, 0);
            this.btnCreateAcctb2.Size = new System.Drawing.Size(170, 26);
            this.btnCreateAcctb2.TabIndex = 9;
            this.btnCreateAcctb2.Text = "Create Account";
            this.btnCreateAcctb2.UseVisualStyleBackColor = false;
            this.btnCreateAcctb2.Click += new System.EventHandler(this.btnCreateAcctb2_Click);
            // 
            // btnBrowsetb2
            // 
            this.btnBrowsetb2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBrowsetb2.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowsetb2.Location = new System.Drawing.Point(165, 138);
            this.btnBrowsetb2.Name = "btnBrowsetb2";
            this.btnBrowsetb2.Size = new System.Drawing.Size(42, 22);
            this.btnBrowsetb2.TabIndex = 8;
            this.btnBrowsetb2.Text = "Browse";
            this.btnBrowsetb2.UseVisualStyleBackColor = true;
            this.btnBrowsetb2.Click += new System.EventHandler(this.btnBrowsetb2_Click_1);
            // 
            // txtfoldertb2
            // 
            this.txtfoldertb2.BackColor = System.Drawing.Color.White;
            this.txtfoldertb2.Location = new System.Drawing.Point(106, 139);
            this.txtfoldertb2.Name = "txtfoldertb2";
            this.txtfoldertb2.Size = new System.Drawing.Size(100, 20);
            this.txtfoldertb2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(36, 139);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3, 4, 9, 3);
            this.label3.Size = new System.Drawing.Size(70, 21);
            this.label3.TabIndex = 75;
            this.label3.Text = "Sync with";
            // 
            // txtpasstb2
            // 
            this.txtpasstb2.BackColor = System.Drawing.Color.White;
            this.txtpasstb2.Location = new System.Drawing.Point(106, 116);
            this.txtpasstb2.Name = "txtpasstb2";
            this.txtpasstb2.PasswordChar = '*';
            this.txtpasstb2.Size = new System.Drawing.Size(100, 20);
            this.txtpasstb2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(35, 116);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3, 3, 4, 4);
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.TabIndex = 72;
            this.label2.Text = "Password  ";
            // 
            // txtusernametb2
            // 
            this.txtusernametb2.BackColor = System.Drawing.Color.White;
            this.txtusernametb2.Location = new System.Drawing.Point(106, 93);
            this.txtusernametb2.Name = "txtusernametb2";
            this.txtusernametb2.Size = new System.Drawing.Size(100, 20);
            this.txtusernametb2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(35, 93);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 4);
            this.label1.Size = new System.Drawing.Size(70, 21);
            this.label1.TabIndex = 71;
            this.label1.Text = "User Name";
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.Color.Black;
            this.console.ForeColor = System.Drawing.Color.White;
            this.console.Location = new System.Drawing.Point(0, 284);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console.Size = new System.Drawing.Size(643, 68);
            this.console.TabIndex = 16;
            this.console.Text = ">";
            this.console.UseSystemPasswordChar = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(643, 352);
            this.Controls.Add(this.console);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DBLike Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCreateAcctb2;
        private System.Windows.Forms.Button btnBrowsetb2;
        private System.Windows.Forms.TextBox txtfoldertb2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpasstb2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtusernametb2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPasstb1;
        private System.Windows.Forms.TextBox txtUserNametb1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSignintb1;
        private System.Windows.Forms.TextBox console;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
    }
}

