namespace DBUpdater
{
    partial class SqlCommandExceptionDetails
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
            this.tbSql = new System.Windows.Forms.RichTextBox();
            this.tbErrors = new System.Windows.Forms.RichTextBox();
            this.releatedButton1 = new LEX.CONTROLS.ReleatedButton();
            this.SuspendLayout();
            // 
            // tbSql
            // 
            this.tbSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSql.Location = new System.Drawing.Point(2, 3);
            this.tbSql.Name = "tbSql";
            this.tbSql.ReadOnly = true;
            this.tbSql.Size = new System.Drawing.Size(559, 160);
            this.tbSql.TabIndex = 0;
            this.tbSql.Text = "";
            // 
            // tbErrors
            // 
            this.tbErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbErrors.Location = new System.Drawing.Point(2, 170);
            this.tbErrors.Name = "tbErrors";
            this.tbErrors.ReadOnly = true;
            this.tbErrors.Size = new System.Drawing.Size(559, 69);
            this.tbErrors.TabIndex = 1;
            this.tbErrors.Text = "";
            // 
            // releatedButton1
            // 
            this.releatedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.releatedButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.releatedButton1.Location = new System.Drawing.Point(486, 246);
            this.releatedButton1.Name = "releatedButton1";
            this.releatedButton1.Size = new System.Drawing.Size(75, 23);
            this.releatedButton1.TabIndex = 2;
            this.releatedButton1.Text = "&Close";
            this.releatedButton1.UseVisualStyleBackColor = true;
            // 
            // SqlCommandExceptionDetails
            // 
            this.AcceptButton = this.releatedButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.releatedButton1;
            this.ClientSize = new System.Drawing.Size(562, 273);
            this.Controls.Add(this.releatedButton1);
            this.Controls.Add(this.tbErrors);
            this.Controls.Add(this.tbSql);
            this.Name = "SqlCommandExceptionDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SqlCommandExceptionDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tbSql;
        private System.Windows.Forms.RichTextBox tbErrors;
        private LEX.CONTROLS.ReleatedButton releatedButton1;
    }
}