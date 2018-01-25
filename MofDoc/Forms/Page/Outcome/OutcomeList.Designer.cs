namespace MofDoc.Forms.Page.Outcome
{
    partial class OutcomeList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.nvItemRefresh = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemSearch = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem2 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.navBarGroup = new DevExpress.XtraNavBar.NavBarGroup();
            this.nvItemAddOutcome = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemOutcomeDelete = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemOutcomeEdit = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarMenu = new DevExpress.XtraNavBar.NavBarControl();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripLblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblNonAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripNonAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblDecision = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDecision = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblExpired = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripExpired = new System.Windows.Forms.ToolStripStatusLabel();
            this.nvItemPrintList = new DevExpress.XtraNavBar.NavBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarMenu)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeList
            // 
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.Location = new System.Drawing.Point(239, 0);
            this.treeList.Name = "treeList";
            this.treeList.Size = new System.Drawing.Size(708, 575);
            this.treeList.TabIndex = 3;
            // 
            // nvItemRefresh
            // 
            this.nvItemRefresh.Caption = "Сэргээх (F5)";
            this.nvItemRefresh.Name = "nvItemRefresh";
            this.nvItemRefresh.SmallImage = global::MofDoc.Properties.Resources.box_remove;
            // 
            // nvItemSearch
            // 
            this.nvItemSearch.Caption = "Хайх (F1)";
            this.nvItemSearch.Name = "nvItemSearch";
            this.nvItemSearch.SmallImage = global::MofDoc.Properties.Resources.search;
            // 
            // navBarSeparatorItem2
            // 
            this.navBarSeparatorItem2.CanDrag = false;
            this.navBarSeparatorItem2.Enabled = false;
            this.navBarSeparatorItem2.Hint = null;
            this.navBarSeparatorItem2.LargeImageIndex = 0;
            this.navBarSeparatorItem2.LargeImageSize = new System.Drawing.Size(0, 0);
            this.navBarSeparatorItem2.Name = "navBarSeparatorItem2";
            this.navBarSeparatorItem2.SmallImageIndex = 0;
            this.navBarSeparatorItem2.SmallImageSize = new System.Drawing.Size(0, 0);
            // 
            // navBarGroup
            // 
            this.navBarGroup.Caption = "Цэс";
            this.navBarGroup.Expanded = true;
            this.navBarGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemRefresh),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemSearch),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem2),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemAddOutcome),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemOutcomeDelete),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemOutcomeEdit),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemPrintList)});
            this.navBarGroup.Name = "navBarGroup";
            // 
            // nvItemAddOutcome
            // 
            this.nvItemAddOutcome.Caption = "Явсан бичиг бүртгэх (F2)";
            this.nvItemAddOutcome.Name = "nvItemAddOutcome";
            this.nvItemAddOutcome.SmallImage = global::MofDoc.Properties.Resources.boat;
            // 
            // nvItemOutcomeDelete
            // 
            this.nvItemOutcomeDelete.Caption = "Бичиг устгах (Del)";
            this.nvItemOutcomeDelete.Name = "nvItemOutcomeDelete";
            this.nvItemOutcomeDelete.SmallImage = global::MofDoc.Properties.Resources.box_delete;
            // 
            // nvItemOutcomeEdit
            // 
            this.nvItemOutcomeEdit.Caption = "Явсан бичиг засах";
            this.nvItemOutcomeEdit.Name = "nvItemOutcomeEdit";
            this.nvItemOutcomeEdit.SmallImage = global::MofDoc.Properties.Resources.edit;
            // 
            // navBarMenu
            // 
            this.navBarMenu.ActiveGroup = this.navBarGroup;
            this.navBarMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarMenu.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup});
            this.navBarMenu.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nvItemRefresh,
            this.nvItemSearch,
            this.navBarSeparatorItem2,
            this.nvItemAddOutcome,
            this.nvItemOutcomeDelete,
            this.nvItemOutcomeEdit,
            this.nvItemPrintList});
            this.navBarMenu.Location = new System.Drawing.Point(0, 0);
            this.navBarMenu.Name = "navBarMenu";
            this.navBarMenu.OptionsNavPane.ExpandedWidth = 239;
            this.navBarMenu.Size = new System.Drawing.Size(239, 575);
            this.navBarMenu.TabIndex = 2;
            this.navBarMenu.Text = "navBarControl1";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLblTotal,
            this.toolStripTotal,
            this.toolStripLblNonAnswer,
            this.toolStripNonAnswer,
            this.toolStripLblDecision,
            this.toolStripDecision,
            this.toolStripLblAnswer,
            this.toolStripAnswer,
            this.toolStripLblExpired,
            this.toolStripExpired});
            this.statusStrip.Location = new System.Drawing.Point(0, 575);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(947, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripLblTotal
            // 
            this.toolStripLblTotal.Name = "toolStripLblTotal";
            this.toolStripLblTotal.Size = new System.Drawing.Size(47, 17);
            this.toolStripLblTotal.Text = "НИЙТ :";
            // 
            // toolStripTotal
            // 
            this.toolStripTotal.Name = "toolStripTotal";
            this.toolStripTotal.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripLblNonAnswer
            // 
            this.toolStripLblNonAnswer.Name = "toolStripLblNonAnswer";
            this.toolStripLblNonAnswer.Size = new System.Drawing.Size(73, 17);
            this.toolStripLblNonAnswer.Text = "ХАРИУГҮЙ :";
            // 
            // toolStripNonAnswer
            // 
            this.toolStripNonAnswer.Name = "toolStripNonAnswer";
            this.toolStripNonAnswer.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripLblDecision
            // 
            this.toolStripLblDecision.Name = "toolStripLblDecision";
            this.toolStripLblDecision.Size = new System.Drawing.Size(94, 17);
            this.toolStripLblDecision.Text = "ХАРИУ ИРСЭН :";
            // 
            // toolStripDecision
            // 
            this.toolStripDecision.Name = "toolStripDecision";
            this.toolStripDecision.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripLblAnswer
            // 
            this.toolStripLblAnswer.Name = "toolStripLblAnswer";
            this.toolStripLblAnswer.Size = new System.Drawing.Size(75, 17);
            this.toolStripLblAnswer.Text = "ХАРИУТАЙ :";
            // 
            // toolStripAnswer
            // 
            this.toolStripAnswer.Name = "toolStripAnswer";
            this.toolStripAnswer.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripLblExpired
            // 
            this.toolStripLblExpired.Name = "toolStripLblExpired";
            this.toolStripLblExpired.Size = new System.Drawing.Size(128, 17);
            this.toolStripLblExpired.Text = "ХУГАЦАА ХЭТЭРСЭН :";
            // 
            // toolStripExpired
            // 
            this.toolStripExpired.Name = "toolStripExpired";
            this.toolStripExpired.Size = new System.Drawing.Size(0, 17);
            // 
            // nvItemPrintList
            // 
            this.nvItemPrintList.Caption = "Жагсаалтыг хэвлэх";
            this.nvItemPrintList.Name = "nvItemPrintList";
            this.nvItemPrintList.SmallImage = global::MofDoc.Properties.Resources.book;
            // 
            // OutcomeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeList);
            this.Controls.Add(this.navBarMenu);
            this.Controls.Add(this.statusStrip);
            this.Name = "OutcomeList";
            this.Size = new System.Drawing.Size(947, 597);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarMenu)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraNavBar.NavBarItem nvItemRefresh;
        private DevExpress.XtraNavBar.NavBarItem nvItemSearch;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup;
        private DevExpress.XtraNavBar.NavBarControl navBarMenu;
        private DevExpress.XtraNavBar.NavBarItem nvItemAddOutcome;
        private DevExpress.XtraNavBar.NavBarItem nvItemOutcomeDelete;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblNonAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripNonAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblDecision;
        private System.Windows.Forms.ToolStripStatusLabel toolStripDecision;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripAnswer;
        private DevExpress.XtraNavBar.NavBarItem nvItemOutcomeEdit;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblExpired;
        private System.Windows.Forms.ToolStripStatusLabel toolStripExpired;
        private DevExpress.XtraNavBar.NavBarItem nvItemPrintList;
    }
}
