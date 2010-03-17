using System;
using FireFly.CourseEditor.GUI.HtmlEditor;
namespace FireFly.CourseEditor.GUI
{
    partial class CourseDesigner
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CourseDesigner));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvItems = new FireFly.CourseEditor.GUI.FFTreeView();
            this.cmsManifestNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddChapter = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddTheory = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddExamination = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddSummaryPage = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddControlChapter = new System.Windows.Forms.ToolStripMenuItem();
            this.miGrouping = new System.Windows.Forms.ToolStripMenuItem();
            this.miInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.miInsertGroupingChapter = new System.Windows.Forms.ToolStripMenuItem();
            this.miInsertGroupingControlChapter = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveMerge = new System.Windows.Forms.ToolStripMenuItem();
            this.miSequencing = new System.Windows.Forms.ToolStripMenuItem();
            this.miApplyPatterns = new System.Windows.Forms.ToolStripMenuItem();
            this.miForcedSequentialOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.miForcedForwardOnlyOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.miRename = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditInMSWord = new System.Windows.Forms.ToolStripMenuItem();
            this.miUp = new System.Windows.Forms.ToolStripMenuItem();
            this.miDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tcEditor = new System.Windows.Forms.TabControl();
            this.tpPlainText = new System.Windows.Forms.TabPage();
            this.tbText = new FireFly.CourseEditor.GUI.FFTextBox();
            this.tpBrowser = new System.Windows.Forms.TabPage();
            this.webBrowser = new FireFly.CourseEditor.GUI.FFWebBrowser();
            this.tpPageDesigner = new System.Windows.Forms.TabPage();
            this.errorsSummary = new FireFly.CourseEditor.GUI.HtmlEditor.PageErrorsSummary();
            this._pageEditor = new FireFly.CourseEditor.GUI.HtmlEditor.PageEditor();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmsManifestNode.SuspendLayout();
            this.tcEditor.SuspendLayout();
            this.tpPlainText.SuspendLayout();
            this.tpBrowser.SuspendLayout();
            this.tpPageDesigner.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvItems);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcEditor);
            this.splitContainer1.Size = new System.Drawing.Size(745, 439);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvItems
            // 
            this.tvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItems.LabelEdit = true;
            this.tvItems.Location = new System.Drawing.Point(0, 0);
            this.tvItems.Name = "tvItems";
            this.tvItems.NodeContextMenuStrip = this.cmsManifestNode;
            this.tvItems.Size = new System.Drawing.Size(246, 439);
            this.tvItems.TabIndex = 0;
            this.tvItems.ManifestNodeAdding += new System.Action<FireFly.CourseEditor.GUI.FFTreeView.NodeAddingArgs>(this.tvItems_ManifestNodeAdding);
            this.tvItems.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvItems_AfterLabelEdit);
            this.tvItems.Enter += new System.EventHandler(this.tvItems_Enter);
            this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvItems_AfterSelect);
            this.tvItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvItems_KeyUp);
            this.tvItems.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvItems_BeforeSelect);
            // 
            // cmsManifestNode
            // 
            this.cmsManifestNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miGrouping,
            this.miSequencing,
            this.miRename,
            this.miDelete,
            this.miProperties,
            this.miEditInMSWord,
            this.miUp,
            this.miDown});
            this.cmsManifestNode.Name = "cmsManifestNode";
            this.cmsManifestNode.Size = new System.Drawing.Size(179, 224);
            // 
            // miNew
            // 
            this.miNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddChapter,
            this.miAddTheory,
            this.miAddExamination,
            this.miAddSummaryPage,
            this.miAddControlChapter});
            this.miNew.Image = global::FireFly.CourseEditor.Properties.Resources.add_to_list16;
            this.miNew.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miNew.Name = "miNew";
            this.miNew.Size = new System.Drawing.Size(178, 22);
            this.miNew.Text = "&New";
            // 
            // miAddChapter
            // 
            this.miAddChapter.Image = ((System.Drawing.Image)(resources.GetObject("miAddChapter.Image")));
            this.miAddChapter.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miAddChapter.Name = "miAddChapter";
            this.miAddChapter.Size = new System.Drawing.Size(159, 22);
            this.miAddChapter.Text = "&Chapter";
            this.miAddChapter.ToolTipText = "Add Chapter";
            this.miAddChapter.Click += new System.EventHandler(this.AddChapterMenuItem_Click);
            // 
            // miAddTheory
            // 
            this.miAddTheory.Image = global::FireFly.CourseEditor.Properties.Resources.Theory;
            this.miAddTheory.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miAddTheory.Name = "miAddTheory";
            this.miAddTheory.Size = new System.Drawing.Size(159, 22);
            this.miAddTheory.Text = "&Theory...";
            this.miAddTheory.ToolTipText = "Add page with theory";
            this.miAddTheory.Click += new System.EventHandler(this.miAddTheory_Click);
            // 
            // miAddExamination
            // 
            this.miAddExamination.Image = global::FireFly.CourseEditor.Properties.Resources.Examination;
            this.miAddExamination.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miAddExamination.Name = "miAddExamination";
            this.miAddExamination.Size = new System.Drawing.Size(159, 22);
            this.miAddExamination.Text = "&Examination";
            this.miAddExamination.ToolTipText = "Add Examination";
            this.miAddExamination.Click += new System.EventHandler(this.AddExamination_Click);
            // 
            // miAddSummaryPage
            // 
            this.miAddSummaryPage.Image = global::FireFly.CourseEditor.Properties.Resources.Summary;
            this.miAddSummaryPage.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miAddSummaryPage.Name = "miAddSummaryPage";
            this.miAddSummaryPage.Size = new System.Drawing.Size(159, 22);
            this.miAddSummaryPage.Text = "&Summary page";
            this.miAddSummaryPage.ToolTipText = "Add Summary Page";
            this.miAddSummaryPage.Click += new System.EventHandler(this.AddSummaryPage_Click);
            // 
            // miAddControlChapter
            // 
            this.miAddControlChapter.Image = global::FireFly.CourseEditor.Properties.Resources.ControlChapter;
            this.miAddControlChapter.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miAddControlChapter.Name = "miAddControlChapter";
            this.miAddControlChapter.Size = new System.Drawing.Size(159, 22);
            this.miAddControlChapter.Text = "C&ontrol Chapter";
            this.miAddControlChapter.ToolTipText = "Add Control Chapter";
            this.miAddControlChapter.Click += new System.EventHandler(this.AddControlChapterMenuItemp_Click);
            // 
            // miGrouping
            // 
            this.miGrouping.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInsert,
            this.miRemoveMerge});
            this.miGrouping.Image = global::FireFly.CourseEditor.Properties.Resources.group;
            this.miGrouping.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miGrouping.Name = "miGrouping";
            this.miGrouping.Size = new System.Drawing.Size(178, 22);
            this.miGrouping.Text = "&Grouping";
            // 
            // miInsert
            // 
            this.miInsert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInsertGroupingChapter,
            this.miInsertGroupingControlChapter});
            this.miInsert.Image = global::FireFly.CourseEditor.Properties.Resources.wrap;
            this.miInsert.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miInsert.Name = "miInsert";
            this.miInsert.Size = new System.Drawing.Size(167, 22);
            this.miInsert.Text = "&Insert";
            // 
            // miInsertGroupingChapter
            // 
            this.miInsertGroupingChapter.Image = ((System.Drawing.Image)(resources.GetObject("miInsertGroupingChapter.Image")));
            this.miInsertGroupingChapter.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miInsertGroupingChapter.Name = "miInsertGroupingChapter";
            this.miInsertGroupingChapter.Size = new System.Drawing.Size(212, 22);
            this.miInsertGroupingChapter.Text = "Grouping &Chapter";
            this.miInsertGroupingChapter.ToolTipText = "Insert Grouping Chapter";
            this.miInsertGroupingChapter.Click += new System.EventHandler(this.miInsertGroupingChapter_Click);
            // 
            // miInsertGroupingControlChapter
            // 
            this.miInsertGroupingControlChapter.Image = global::FireFly.CourseEditor.Properties.Resources.ControlChapterGrouping;
            this.miInsertGroupingControlChapter.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miInsertGroupingControlChapter.Name = "miInsertGroupingControlChapter";
            this.miInsertGroupingControlChapter.Size = new System.Drawing.Size(212, 22);
            this.miInsertGroupingControlChapter.Text = "Grouping Contro&l Chapter";
            this.miInsertGroupingControlChapter.ToolTipText = "Insert Grouping Control Chapter";
            this.miInsertGroupingControlChapter.Click += new System.EventHandler(this.miInsertGroupingControlChapter_Click);
            // 
            // miRemoveMerge
            // 
            this.miRemoveMerge.Image = global::FireFly.CourseEditor.Properties.Resources.merge;
            this.miRemoveMerge.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miRemoveMerge.Name = "miRemoveMerge";
            this.miRemoveMerge.Size = new System.Drawing.Size(167, 22);
            this.miRemoveMerge.Text = "&Remove && Merge";
            this.miRemoveMerge.ToolTipText = "Remove item and merge it\'s children";
            this.miRemoveMerge.Click += new System.EventHandler(this.miRemoveMerge_Click);
            // 
            // miSequencing
            // 
            this.miSequencing.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miApplyPatterns});
            this.miSequencing.Image = global::FireFly.CourseEditor.Properties.Resources.SequenceIcon;
            this.miSequencing.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miSequencing.Name = "miSequencing";
            this.miSequencing.Size = new System.Drawing.Size(178, 22);
            this.miSequencing.Text = "&Sequencing";
            // 
            // miApplyPatterns
            // 
            this.miApplyPatterns.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miForcedSequentialOrder,
            this.miForcedForwardOnlyOrder});
            this.miApplyPatterns.Name = "miApplyPatterns";
            this.miApplyPatterns.Size = new System.Drawing.Size(152, 22);
            this.miApplyPatterns.Text = "&Apply Patterns";
            // 
            // miForcedSequentialOrder
            // 
            this.miForcedSequentialOrder.Name = "miForcedSequentialOrder";
            this.miForcedSequentialOrder.Size = new System.Drawing.Size(201, 22);
            this.miForcedSequentialOrder.Text = "Forced Sequential Order";
            this.miForcedSequentialOrder.Click += new System.EventHandler(this.miForcedSequentialOrder_Click);
            // 
            // miForcedForwardOnlyOrder
            // 
            this.miForcedForwardOnlyOrder.Name = "miForcedForwardOnlyOrder";
            this.miForcedForwardOnlyOrder.Size = new System.Drawing.Size(201, 22);
            this.miForcedForwardOnlyOrder.Text = "Forward-Only Order";
            this.miForcedForwardOnlyOrder.Click += new System.EventHandler(this.miForwardOnlyOrder_Click);
            // 
            // miRename
            // 
            this.miRename.Name = "miRename";
            this.miRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.miRename.Size = new System.Drawing.Size(178, 22);
            this.miRename.Text = "&Rename";
            // 
            // miDelete
            // 
            this.miDelete.Image = global::FireFly.CourseEditor.Properties.Resources.delete_16;
            this.miDelete.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miDelete.Name = "miDelete";
            this.miDelete.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.miDelete.Size = new System.Drawing.Size(178, 22);
            this.miDelete.Text = "&Delete";
            this.miDelete.ToolTipText = "Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miProperties
            // 
            this.miProperties.Image = global::FireFly.CourseEditor.Properties.Resources.properties_16;
            this.miProperties.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.miProperties.Name = "miProperties";
            this.miProperties.Size = new System.Drawing.Size(178, 22);
            this.miProperties.Text = "Properties";
            // 
            // miEditInMSWord
            // 
            this.miEditInMSWord.Image = global::FireFly.CourseEditor.Properties.Resources.word;
            this.miEditInMSWord.Name = "miEditInMSWord";
            this.miEditInMSWord.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.miEditInMSWord.Size = new System.Drawing.Size(178, 22);
            this.miEditInMSWord.Text = "&Edit in MS Word";
            this.miEditInMSWord.ToolTipText = "Edit in MS Word";
            this.miEditInMSWord.Click += new System.EventHandler(this.editInMSWordToolStripMenuItem_Click);
            // 
            // miUp
            // 
            this.miUp.Image = global::FireFly.CourseEditor.Properties.Resources.arrow_up;
            this.miUp.ImageTransparentColor = System.Drawing.Color.White;
            this.miUp.Name = "miUp";
            this.miUp.Size = new System.Drawing.Size(178, 22);
            this.miUp.Text = "Move &Up";
            this.miUp.ToolTipText = "Move current item up";
            this.miUp.Visible = false;
            this.miUp.Click += new System.EventHandler(this.miUp_Click);
            // 
            // miDown
            // 
            this.miDown.Image = global::FireFly.CourseEditor.Properties.Resources.arrow_down;
            this.miDown.ImageTransparentColor = System.Drawing.Color.White;
            this.miDown.Name = "miDown";
            this.miDown.Size = new System.Drawing.Size(178, 22);
            this.miDown.Text = "Move &Down";
            this.miDown.ToolTipText = "Move current item down";
            this.miDown.Visible = false;
            this.miDown.Click += new System.EventHandler(this.miDown_Click);
            // 
            // tcEditor
            // 
            this.tcEditor.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcEditor.Controls.Add(this.tpPlainText);
            this.tcEditor.Controls.Add(this.tpBrowser);
            this.tcEditor.Controls.Add(this.tpPageDesigner);
            this.tcEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEditor.Location = new System.Drawing.Point(0, 0);
            this.tcEditor.Multiline = true;
            this.tcEditor.Name = "tcEditor";
            this.tcEditor.SelectedIndex = 0;
            this.tcEditor.Size = new System.Drawing.Size(495, 439);
            this.tcEditor.TabIndex = 2;
            // 
            // tpPlainText
            // 
            this.tpPlainText.BackColor = System.Drawing.SystemColors.Control;
            this.tpPlainText.Controls.Add(this.tbText);
            this.tpPlainText.Location = new System.Drawing.Point(4, 4);
            this.tpPlainText.Name = "tpPlainText";
            this.tpPlainText.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlainText.Size = new System.Drawing.Size(487, 413);
            this.tpPlainText.TabIndex = 0;
            this.tpPlainText.Text = "Plain text";
            // 
            // tbText
            // 
            this.tbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbText.Location = new System.Drawing.Point(3, 3);
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbText.Size = new System.Drawing.Size(481, 407);
            this.tbText.TabIndex = 1;
            this.tbText.WordWrap = false;
            // 
            // tpBrowser
            // 
            this.tpBrowser.Controls.Add(this.webBrowser);
            this.tpBrowser.Location = new System.Drawing.Point(4, 4);
            this.tpBrowser.Name = "tpBrowser";
            this.tpBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tpBrowser.Size = new System.Drawing.Size(487, 413);
            this.tpBrowser.TabIndex = 1;
            this.tpBrowser.Text = "Browser";
            this.tpBrowser.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(3, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(481, 407);
            this.webBrowser.TabIndex = 0;
            // 
            // tpPageDesigner
            // 
            this.tpPageDesigner.BackColor = System.Drawing.SystemColors.Control;
            this.tpPageDesigner.Controls.Add(this.errorsSummary);
            this.tpPageDesigner.Controls.Add(this._pageEditor);
            this.tpPageDesigner.Location = new System.Drawing.Point(4, 4);
            this.tpPageDesigner.Name = "tpPageDesigner";
            this.tpPageDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tpPageDesigner.Size = new System.Drawing.Size(487, 413);
            this.tpPageDesigner.TabIndex = 2;
            this.tpPageDesigner.Text = "Page Designer";
            // 
            // errorsSummary
            // 
            this.errorsSummary.AutoSize = true;
            this.errorsSummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.errorsSummary.BackColor = System.Drawing.Color.White;
            this.errorsSummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.errorsSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.errorsSummary.Location = new System.Drawing.Point(3, 380);
            this.errorsSummary.Name = "errorsSummary";
            this.errorsSummary.Size = new System.Drawing.Size(481, 30);
            this.errorsSummary.TabIndex = 1;
            // 
            // _pageEditor
            // 
            this._pageEditor.BackColor = System.Drawing.Color.White;
            this._pageEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pageEditor.Location = new System.Drawing.Point(3, 3);
            this._pageEditor.Name = "_pageEditor";
            this._pageEditor.Size = new System.Drawing.Size(481, 407);
            this._pageEditor.TabIndex = 0;
            // 
            // CourseDesigner
            // 
            this.ClientSize = new System.Drawing.Size(745, 439);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CourseDesigner";
            this.TabText = "Course Designer";
            this.Text = "Course Designer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.cmsManifestNode.ResumeLayout(false);
            this.tcEditor.ResumeLayout(false);
            this.tpPlainText.ResumeLayout(false);
            this.tpPlainText.PerformLayout();
            this.tpBrowser.ResumeLayout(false);
            this.tpPageDesigner.ResumeLayout(false);
            this.tpPageDesigner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private FFWebBrowser webBrowser;
        public System.Windows.Forms.ContextMenuStrip cmsManifestNode;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private FFTreeView tvItems;
        private System.Windows.Forms.ToolStripMenuItem miNew;
        private System.Windows.Forms.ToolStripMenuItem miAddTheory;
        private FFTextBox tbText;
        private System.Windows.Forms.TabControl tcEditor;
        private System.Windows.Forms.TabPage tpPlainText;
        private System.Windows.Forms.TabPage tpBrowser;
        private System.Windows.Forms.ToolStripMenuItem miAddExamination;
        private System.Windows.Forms.TabPage tpPageDesigner;
        private PageEditor _pageEditor;
        private System.Windows.Forms.ToolStripMenuItem miProperties;
        private System.Windows.Forms.ToolStripMenuItem miAddSummaryPage;
        private System.Windows.Forms.ToolStripMenuItem miEditInMSWord;
        private System.Windows.Forms.ToolStripMenuItem miAddChapter;
        private System.Windows.Forms.ToolStripMenuItem miRename;
        private System.Windows.Forms.ToolStripMenuItem miUp;
        private System.Windows.Forms.ToolStripMenuItem miDown;
        private PageErrorsSummary errorsSummary;
        private System.Windows.Forms.ToolStripMenuItem miAddControlChapter;
        private System.Windows.Forms.ToolStripMenuItem miGrouping;
        private System.Windows.Forms.ToolStripMenuItem miInsert;
        private System.Windows.Forms.ToolStripMenuItem miRemoveMerge;
        private System.Windows.Forms.ToolStripMenuItem miInsertGroupingChapter;
        private System.Windows.Forms.ToolStripMenuItem miInsertGroupingControlChapter;
        private System.Windows.Forms.ToolStripMenuItem miSequencing;
        private System.Windows.Forms.ToolStripMenuItem miApplyPatterns;
        private System.Windows.Forms.ToolStripMenuItem miForcedSequentialOrder;
        private System.Windows.Forms.ToolStripMenuItem miForcedForwardOnlyOrder;
    }
}
