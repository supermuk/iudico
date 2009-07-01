namespace DBUpdater
{
    partial class frmSelectDB
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
            this.lexLabel1 = new LEX.CONTROLS.LexLabel();
            this.tbServerInstance = new LEX.CONTROLS.RememberTextBox();
            this.btnOk = new LEX.CONTROLS.ReleatedButton();
            this.btnCancel = new LEX.CONTROLS.ReleatedButton();
            this.lexLabel2 = new LEX.CONTROLS.LexLabel();
            this.tbDBName = new LEX.CONTROLS.RememberTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lexLabel4 = new LEX.CONTROLS.LexLabel();
            this.tbUserName = new LEX.CONTROLS.RememberTextBox();
            this.lexLabel3 = new LEX.CONTROLS.LexLabel();
            this.cbWindowsAutentication = new LEX.CONTROLS.CheckBoxEx();
            this.btnConsole = new System.Windows.Forms.Button();
            this.btnTypeConnectionString = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lexLabel1
            // 
            this.lexLabel1.AutoSize = true;
            this.lexLabel1.For = this.tbServerInstance;
            this.lexLabel1.Location = new System.Drawing.Point(12, 9);
            this.lexLabel1.Name = "lexLabel1";
            this.lexLabel1.Size = new System.Drawing.Size(134, 13);
            this.lexLabel1.TabIndex = 0;
            this.lexLabel1.Text = "Sql Server Instance Name:";
            // 
            // tbServerInstance
            // 
            this.tbServerInstance.ErrorMessage = null;
            this.tbServerInstance.FormattingEnabled = true;
            this.tbServerInstance.Location = new System.Drawing.Point(149, 6);
            this.tbServerInstance.Name = "tbServerInstance";
            this.tbServerInstance.Size = new System.Drawing.Size(331, 21);
            this.tbServerInstance.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(337, 118);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(143, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(15, 118);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lexLabel2
            // 
            this.lexLabel2.AutoSize = true;
            this.lexLabel2.For = this.tbDBName;
            this.lexLabel2.Location = new System.Drawing.Point(12, 34);
            this.lexLabel2.Name = "lexLabel2";
            this.lexLabel2.Size = new System.Drawing.Size(56, 13);
            this.lexLabel2.TabIndex = 4;
            this.lexLabel2.Text = "&DB Name:";
            // 
            // tbDBName
            // 
            this.tbDBName.ErrorMessage = null;
            this.tbDBName.FormattingEnabled = true;
            this.tbDBName.Location = new System.Drawing.Point(149, 31);
            this.tbDBName.Name = "tbDBName";
            this.tbDBName.Size = new System.Drawing.Size(331, 21);
            this.tbDBName.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.lexLabel4);
            this.groupBox1.Controls.Add(this.tbUserName);
            this.groupBox1.Controls.Add(this.lexLabel3);
            this.groupBox1.Controls.Add(this.cbWindowsAutentication);
            this.groupBox1.Location = new System.Drawing.Point(15, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 48);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(309, 16);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(150, 20);
            this.tbPassword.TabIndex = 4;
            // 
            // lexLabel4
            // 
            this.lexLabel4.AutoSize = true;
            this.lexLabel4.Location = new System.Drawing.Point(247, 19);
            this.lexLabel4.Name = "lexLabel4";
            this.lexLabel4.Size = new System.Drawing.Size(56, 13);
            this.lexLabel4.TabIndex = 0;
            this.lexLabel4.Text = "&Password:";
            // 
            // tbUserName
            // 
            this.tbUserName.ErrorMessage = null;
            this.tbUserName.FormattingEnabled = true;
            this.tbUserName.Location = new System.Drawing.Point(75, 17);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(165, 21);
            this.tbUserName.TabIndex = 3;
            // 
            // lexLabel3
            // 
            this.lexLabel3.AutoSize = true;
            this.lexLabel3.Location = new System.Drawing.Point(6, 20);
            this.lexLabel3.Name = "lexLabel3";
            this.lexLabel3.Size = new System.Drawing.Size(63, 13);
            this.lexLabel3.TabIndex = 1;
            this.lexLabel3.Text = "&User Name:";
            // 
            // cbWindowsAutentication
            // 
            this.cbWindowsAutentication.AutoSize = true;
            this.cbWindowsAutentication.Checked = true;
            this.cbWindowsAutentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWindowsAutentication.Enabled = false;
            this.cbWindowsAutentication.Location = new System.Drawing.Point(0, 0);
            this.cbWindowsAutentication.Name = "cbWindowsAutentication";
            this.cbWindowsAutentication.Size = new System.Drawing.Size(135, 17);
            this.cbWindowsAutentication.TabIndex = 2;
            this.cbWindowsAutentication.Text = "&Windows Autentication";
            this.cbWindowsAutentication.UseVisualStyleBackColor = true;
            this.cbWindowsAutentication.Value = true;
            // 
            // btnConsole
            // 
            this.btnConsole.Location = new System.Drawing.Point(171, 118);
            this.btnConsole.Name = "btnConsole";
            this.btnConsole.Size = new System.Drawing.Size(115, 23);
            this.btnConsole.TabIndex = 7;
            this.btnConsole.Text = "Console";
            this.btnConsole.UseVisualStyleBackColor = true;
            this.btnConsole.Click += new System.EventHandler(this.btnConsole_Click);
            // 
            // btnTypeConnectionString
            // 
            this.btnTypeConnectionString.Location = new System.Drawing.Point(286, 118);
            this.btnTypeConnectionString.Name = "btnTypeConnectionString";
            this.btnTypeConnectionString.Size = new System.Drawing.Size(30, 23);
            this.btnTypeConnectionString.TabIndex = 8;
            this.btnTypeConnectionString.Text = "...";
            this.btnTypeConnectionString.UseVisualStyleBackColor = true;
            this.btnTypeConnectionString.Click += new System.EventHandler(this.btnTypeConnectionString_Click);
            // 
            // frmSelectDB
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(486, 150);
            this.ControlBox = false;
            this.Controls.Add(this.btnTypeConnectionString);
            this.Controls.Add(this.btnConsole);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbDBName);
            this.Controls.Add(this.lexLabel2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbServerInstance);
            this.Controls.Add(this.lexLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectDB";
            this.ShowIcon = false;
            this.Text = "Select DataBase to update";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LEX.CONTROLS.LexLabel lexLabel1;
        private LEX.CONTROLS.RememberTextBox tbServerInstance;
        private LEX.CONTROLS.ReleatedButton btnOk;
        private LEX.CONTROLS.ReleatedButton btnCancel;
        private LEX.CONTROLS.LexLabel lexLabel2;
        private LEX.CONTROLS.RememberTextBox tbDBName;
        private System.Windows.Forms.GroupBox groupBox1;
        private LEX.CONTROLS.CheckBoxEx cbWindowsAutentication;
        private System.Windows.Forms.TextBox tbPassword;
        private LEX.CONTROLS.LexLabel lexLabel4;
        private LEX.CONTROLS.RememberTextBox tbUserName;
        private LEX.CONTROLS.LexLabel lexLabel3;
        private System.Windows.Forms.Button btnConsole;
        private System.Windows.Forms.Button btnTypeConnectionString;

    }
}

