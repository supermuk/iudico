namespace DBUpdater
{
    partial class frmDBConsole
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.llRun = new System.Windows.Forms.LinkLabel();
            this.rtbSqlCommands = new System.Windows.Forms.RichTextBox();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.llRun);
            this.splitContainer1.Panel1.Controls.Add(this.rtbSqlCommands);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbResult);
            this.splitContainer1.Size = new System.Drawing.Size(599, 273);
            this.splitContainer1.SplitterDistance = 133;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // llRun
            // 
            this.llRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llRun.AutoSize = true;
            this.llRun.BackColor = System.Drawing.Color.Transparent;
            this.llRun.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.llRun.Location = new System.Drawing.Point(568, 116);
            this.llRun.Name = "llRun";
            this.llRun.Size = new System.Drawing.Size(27, 13);
            this.llRun.TabIndex = 1;
            this.llRun.TabStop = true;
            this.llRun.Text = "&Run";
            this.llRun.Click += new System.EventHandler(this.llRun_Click);
            // 
            // rtbSqlCommands
            // 
            this.rtbSqlCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSqlCommands.Location = new System.Drawing.Point(0, 0);
            this.rtbSqlCommands.Name = "rtbSqlCommands";
            this.rtbSqlCommands.Size = new System.Drawing.Size(599, 133);
            this.rtbSqlCommands.TabIndex = 0;
            this.rtbSqlCommands.Text = "";
            // 
            // rtbResult
            // 
            this.rtbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbResult.Location = new System.Drawing.Point(0, 0);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(599, 134);
            this.rtbResult.TabIndex = 0;
            this.rtbResult.Text = "";
            // 
            // frmDBConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 273);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmDBConsole";
            this.Text = "DB Console";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.LinkLabel llRun;
        private System.Windows.Forms.RichTextBox rtbSqlCommands;
        private System.Windows.Forms.RichTextBox rtbResult;
    }
}