namespace Client_newUI
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnSignintb1 = new System.Windows.Forms.Button();
            this.txtPasstb1 = new System.Windows.Forms.TextBox();
            this.txtUserNametb1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 446);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 21;
            this.button1.Text = "Sign Up";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSignintb1
            // 
            this.btnSignintb1.Location = new System.Drawing.Point(82, 334);
            this.btnSignintb1.Margin = new System.Windows.Forms.Padding(4);
            this.btnSignintb1.Name = "btnSignintb1";
            this.btnSignintb1.Size = new System.Drawing.Size(226, 30);
            this.btnSignintb1.TabIndex = 20;
            this.btnSignintb1.Text = "Sign in";
            this.btnSignintb1.UseVisualStyleBackColor = true;
            this.btnSignintb1.Click += new System.EventHandler(this.btnSignintb1_Click);
            // 
            // txtPasstb1
            // 
            this.txtPasstb1.Location = new System.Drawing.Point(176, 293);
            this.txtPasstb1.Margin = new System.Windows.Forms.Padding(4);
            this.txtPasstb1.Name = "txtPasstb1";
            this.txtPasstb1.Size = new System.Drawing.Size(132, 22);
            this.txtPasstb1.TabIndex = 19;
            // 
            // txtUserNametb1
            // 
            this.txtUserNametb1.Location = new System.Drawing.Point(176, 263);
            this.txtUserNametb1.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserNametb1.Name = "txtUserNametb1";
            this.txtUserNametb1.Size = new System.Drawing.Size(132, 22);
            this.txtUserNametb1.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 296);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 266);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "User Name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 493);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSignintb1);
            this.Controls.Add(this.txtPasstb1);
            this.Controls.Add(this.txtUserNametb1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSignintb1;
        private System.Windows.Forms.TextBox txtPasstb1;
        private System.Windows.Forms.TextBox txtUserNametb1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

