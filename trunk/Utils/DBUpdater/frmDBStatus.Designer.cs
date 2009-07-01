namespace DBUpdater
{
    partial class frmDBStatus
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tbOperationLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbOperation = new System.Windows.Forms.ToolStripProgressBar();
            this.btnRecreateDataBase = new LEX.CONTROLS.ReleatedButton();
            this.btnOk = new LEX.CONTROLS.ReleatedButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lvRunned = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnRun = new LEX.CONTROLS.ReleatedButton();
            this.lvScripts = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnConsole = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbOperationLabel,
            this.pbOperation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 226);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(666, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tbOperationLabel
            // 
            this.tbOperationLabel.Name = "tbOperationLabel";
            this.tbOperationLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // pbOperation
            // 
            this.pbOperation.Name = "pbOperation";
            this.pbOperation.Size = new System.Drawing.Size(100, 16);
            this.pbOperation.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // btnRecreateDataBase
            // 
            this.btnRecreateDataBase.Location = new System.Drawing.Point(4, 7);
            this.btnRecreateDataBase.Name = "btnRecreateDataBase";
            this.btnRecreateDataBase.Size = new System.Drawing.Size(225, 23);
            this.btnRecreateDataBase.TabIndex = 2;
            this.btnRecreateDataBase.Text = "RECREATE DATABASE";
            this.btnRecreateDataBase.UseVisualStyleBackColor = true;
            this.btnRecreateDataBase.Click += new System.EventHandler(this.btnRecreateDataBase_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(534, 7);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(128, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lvRunned);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(658, 161);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Runned scripts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lvRunned
            // 
            this.lvRunned.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvRunned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRunned.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvRunned.Location = new System.Drawing.Point(3, 3);
            this.lvRunned.Name = "lvRunned";
            this.lvRunned.Size = new System.Drawing.Size(652, 155);
            this.lvRunned.TabIndex = 0;
            this.lvRunned.UseCompatibleStateImageBehavior = false;
            this.lvRunned.View = System.Windows.Forms.View.Details;
            this.lvRunned.Resize += new System.EventHandler(this.ListViewScripts_Resize);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Script name";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnRun);
            this.tabPage1.Controls.Add(this.lvScripts);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(658, 161);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scripts to run";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(596, 6);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(56, 149);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "&RUN";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lvScripts
            // 
            this.lvScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvScripts.CheckBoxes = true;
            this.lvScripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvScripts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvScripts.Location = new System.Drawing.Point(0, 0);
            this.lvScripts.Name = "lvScripts";
            this.lvScripts.Size = new System.Drawing.Size(588, 161);
            this.lvScripts.TabIndex = 0;
            this.lvScripts.UseCompatibleStateImageBehavior = false;
            this.lvScripts.View = System.Windows.Forms.View.Details;
            this.lvScripts.Resize += new System.EventHandler(this.ListViewScripts_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Script name";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(666, 187);
            this.tabControl1.TabIndex = 1;
            // 
            // btnConsole
            // 
            this.btnConsole.Location = new System.Drawing.Point(236, 7);
            this.btnConsole.Name = "btnConsole";
            this.btnConsole.Size = new System.Drawing.Size(75, 23);
            this.btnConsole.TabIndex = 4;
            this.btnConsole.Text = "&Console";
            this.btnConsole.UseVisualStyleBackColor = true;
            this.btnConsole.Click += new System.EventHandler(this.btnConsole_Click);
            // 
            // frmDBStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 248);
            this.Controls.Add(this.btnConsole);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnRecreateDataBase);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmDBStatus";
            this.Text = "frmDBStatus";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tbOperationLabel;
        private System.Windows.Forms.ToolStripProgressBar pbOperation;
        private LEX.CONTROLS.ReleatedButton btnRecreateDataBase;
        private LEX.CONTROLS.ReleatedButton btnOk;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView lvRunned;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabPage1;
        private LEX.CONTROLS.ReleatedButton btnRun;
        private System.Windows.Forms.ListView lvScripts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnConsole;
    }
}