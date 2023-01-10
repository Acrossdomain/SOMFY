namespace DueAMT
{
    partial class Config
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
			this.panel1 = new System.Windows.Forms.Panel();
			this.Exit = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtsapass = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtsqluserid = new System.Windows.Forms.TextBox();
			this.txtSageDB = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.txtServername = new System.Windows.Forms.TextBox();
			this.btncreateConnection = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.panel1.Controls.Add(this.Exit);
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.btncreateConnection);
			this.panel1.Location = new System.Drawing.Point(8, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(436, 274);
			this.panel1.TabIndex = 0;
			// 
			// Exit
			// 
			this.Exit.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Exit.Location = new System.Drawing.Point(247, 232);
			this.Exit.Name = "Exit";
			this.Exit.Size = new System.Drawing.Size(113, 26);
			this.Exit.TabIndex = 4;
			this.Exit.Text = "Exit";
			this.Exit.UseVisualStyleBackColor = true;
			this.Exit.Click += new System.EventHandler(this.Exit_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtsapass);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.txtsqluserid);
			this.groupBox3.Controls.Add(this.txtSageDB);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.label15);
			this.groupBox3.Controls.Add(this.txtServername);
			this.groupBox3.Location = new System.Drawing.Point(11, 56);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(416, 170);
			this.groupBox3.TabIndex = 54;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Sage Credential";
			// 
			// txtsapass
			// 
			this.txtsapass.Location = new System.Drawing.Point(117, 115);
			this.txtsapass.Name = "txtsapass";
			this.txtsapass.PasswordChar = '*';
			this.txtsapass.Size = new System.Drawing.Size(232, 20);
			this.txtsapass.TabIndex = 44;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 119);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 46;
			this.label1.Text = "Password:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 91);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 45;
			this.label2.Text = "User Id:";
			// 
			// txtsqluserid
			// 
			this.txtsqluserid.Location = new System.Drawing.Point(117, 89);
			this.txtsqluserid.Name = "txtsqluserid";
			this.txtsqluserid.Size = new System.Drawing.Size(232, 20);
			this.txtsqluserid.TabIndex = 43;
			// 
			// txtSageDB
			// 
			this.txtSageDB.Location = new System.Drawing.Point(117, 63);
			this.txtSageDB.Name = "txtSageDB";
			this.txtSageDB.PasswordChar = '*';
			this.txtSageDB.Size = new System.Drawing.Size(232, 20);
			this.txtSageDB.TabIndex = 2;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(20, 67);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(84, 13);
			this.label14.TabIndex = 42;
			this.label14.Text = "Sage Database:";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(20, 39);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(72, 13);
			this.label15.TabIndex = 40;
			this.label15.Text = "Server Name:";
			// 
			// txtServername
			// 
			this.txtServername.Location = new System.Drawing.Point(117, 37);
			this.txtServername.Name = "txtServername";
			this.txtServername.Size = new System.Drawing.Size(232, 20);
			this.txtServername.TabIndex = 1;
			// 
			// btncreateConnection
			// 
			this.btncreateConnection.Location = new System.Drawing.Point(128, 232);
			this.btncreateConnection.Name = "btncreateConnection";
			this.btncreateConnection.Size = new System.Drawing.Size(107, 26);
			this.btncreateConnection.TabIndex = 3;
			this.btncreateConnection.Text = "Submit";
			this.btncreateConnection.UseVisualStyleBackColor = true;
			this.btncreateConnection.Click += new System.EventHandler(this.btncreateConnection_Click);
			// 
			// Config
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(451, 287);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Config";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login with  Accpac";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Config_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Config_FormClosed);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button btncreateConnection;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtsapass;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtsqluserid;
		private System.Windows.Forms.TextBox txtSageDB;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox txtServername;
	}
}