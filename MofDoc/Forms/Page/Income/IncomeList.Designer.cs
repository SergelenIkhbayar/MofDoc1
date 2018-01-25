namespace MofDoc.Forms.Page.Income
{
    partial class IncomeList
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
            this.navBarGroup = new DevExpress.XtraNavBar.NavBarGroup();
            this.nvItemRefresh = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemSearch = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem2 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.nvItemDirectRegister = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemAddCard = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemDelete = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemHistory = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemAddDesc = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemAddReply = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemMove = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemPrint = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemAddFile = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemRenewal = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemPrintList = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemPrintBack = new DevExpress.XtraNavBar.NavBarItem();
            this.nvItemLastList = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarMenu = new DevExpress.XtraNavBar.NavBarControl();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripLblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblNoAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripNoAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblDecision = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDecision = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblExpired = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripExpired = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblApp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripApp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblAppDecision = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripAppDecision = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblAppAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripAppAnswer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblAppExpired = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripAppExpired = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarMenu)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeList
            // 
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.Location = new System.Drawing.Point(186, 0);
            this.treeList.Name = "treeList";
            this.treeList.Size = new System.Drawing.Size(1007, 653);
            this.treeList.TabIndex = 1;
            // 
            // navBarGroup
            // 
            this.navBarGroup.Caption = "Цэс";
            this.navBarGroup.Expanded = true;
            this.navBarGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemRefresh),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemSearch),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarSeparatorItem2),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemDirectRegister),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemAddCard),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemDelete),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemHistory),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemAddDesc),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemAddReply),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemMove),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemPrint),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemAddFile),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemRenewal),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemPrintList),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemPrintBack),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nvItemLastList)});
            this.navBarGroup.Name = "navBarGroup";
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
            // nvItemDirectRegister
            // 
            this.nvItemDirectRegister.Caption = "Шууд бүртгэх (F2)";
            this.nvItemDirectRegister.Name = "nvItemDirectRegister";
            this.nvItemDirectRegister.SmallImage = global::MofDoc.Properties.Resources.box_approve;
            // 
            // nvItemAddCard
            // 
            this.nvItemAddCard.Caption = "Карт нээх (F3)";
            this.nvItemAddCard.Name = "nvItemAddCard";
            this.nvItemAddCard.SmallImage = global::MofDoc.Properties.Resources.box_add;
            // 
            // nvItemDelete
            // 
            this.nvItemDelete.Caption = "Бичиг устгах (Del)";
            this.nvItemDelete.Name = "nvItemDelete";
            this.nvItemDelete.SmallImage = global::MofDoc.Properties.Resources.box_delete;
            // 
            // nvItemHistory
            // 
            this.nvItemHistory.Caption = "Бичгийн түүх";
            this.nvItemHistory.Name = "nvItemHistory";
            this.nvItemHistory.SmallImage = global::MofDoc.Properties.Resources.wired;
            // 
            // nvItemAddDesc
            // 
            this.nvItemAddDesc.Caption = "Тайлбар оруулах";
            this.nvItemAddDesc.Name = "nvItemAddDesc";
            this.nvItemAddDesc.SmallImage = global::MofDoc.Properties.Resources.edit;
            // 
            // nvItemAddReply
            // 
            this.nvItemAddReply.Caption = "Хариу бүртгэх";
            this.nvItemAddReply.Name = "nvItemAddReply";
            this.nvItemAddReply.SmallImage = global::MofDoc.Properties.Resources.replay;
            // 
            // nvItemMove
            // 
            this.nvItemMove.Caption = "Шилжүүлэх";
            this.nvItemMove.Name = "nvItemMove";
            this.nvItemMove.SmallImage = global::MofDoc.Properties.Resources.move;
            // 
            // nvItemPrint
            // 
            this.nvItemPrint.Caption = "Хэвлэх";
            this.nvItemPrint.Name = "nvItemPrint";
            this.nvItemPrint.SmallImage = global::MofDoc.Properties.Resources.print;
            // 
            // nvItemAddFile
            // 
            this.nvItemAddFile.Caption = "Файл хавсаргах";
            this.nvItemAddFile.Name = "nvItemAddFile";
            this.nvItemAddFile.SmallImage = global::MofDoc.Properties.Resources.baggage_trolley;
            // 
            // nvItemRenewal
            // 
            this.nvItemRenewal.Caption = "Сунгалт";
            this.nvItemRenewal.Name = "nvItemRenewal";
            this.nvItemRenewal.SmallImage = global::MofDoc.Properties.Resources.calendar;
            // 
            // nvItemPrintList
            // 
            this.nvItemPrintList.Caption = "Жагсаалтыг хэвлэх";
            this.nvItemPrintList.Name = "nvItemPrintList";
            this.nvItemPrintList.SmallImage = global::MofDoc.Properties.Resources.book;
            // 
            // nvItemPrintBack
            // 
            this.nvItemPrintBack.Caption = "Арын хуудас хэвлэх";
            this.nvItemPrintBack.Name = "nvItemPrintBack";
            this.nvItemPrintBack.SmallImage = global::MofDoc.Properties.Resources.print;
            // 
            // nvItemLastList
            // 
            this.nvItemLastList.Caption = "Сүүлийн хүнээр харах";
            this.nvItemLastList.LargeImage = global::MofDoc.Properties.Resources.boat;
            this.nvItemLastList.Name = "nvItemLastList";
            this.nvItemLastList.SmallImage = global::MofDoc.Properties.Resources.boat;
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
            this.nvItemDirectRegister,
            this.nvItemAddCard,
            this.nvItemDelete,
            this.nvItemHistory,
            this.nvItemAddDesc,
            this.nvItemAddReply,
            this.nvItemMove,
            this.nvItemPrint,
            this.nvItemAddFile,
            this.nvItemRenewal,
            this.nvItemPrintList,
            this.nvItemPrintBack,
            this.nvItemLastList});
            this.navBarMenu.Location = new System.Drawing.Point(0, 0);
            this.navBarMenu.Name = "navBarMenu";
            this.navBarMenu.OptionsNavPane.ExpandedWidth = 186;
            this.navBarMenu.Size = new System.Drawing.Size(186, 653);
            this.navBarMenu.TabIndex = 0;
            this.navBarMenu.Text = "navBarControl1";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLblTotal,
            this.toolStripTotal,
            this.toolStripLblNoAnswer,
            this.toolStripNoAnswer,
            this.toolStripLblDecision,
            this.toolStripDecision,
            this.toolStripLblAnswer,
            this.toolStripAnswer,
            this.toolStripLblExpired,
            this.toolStripExpired,
            this.toolStripLblApp,
            this.toolStripApp,
            this.toolStripLblAppDecision,
            this.toolStripAppDecision,
            this.toolStripLblAppAnswer,
            this.toolStripAppAnswer,
            this.toolStripLblAppExpired,
            this.toolStripAppExpired});
            this.statusStrip.Location = new System.Drawing.Point(0, 653);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1193, 22);
            this.statusStrip.TabIndex = 2;
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
            // toolStripLblNoAnswer
            // 
            this.toolStripLblNoAnswer.Name = "toolStripLblNoAnswer";
            this.toolStripLblNoAnswer.Size = new System.Drawing.Size(73, 17);
            this.toolStripLblNoAnswer.Text = "ХАРИУГҮЙ :";
            // 
            // toolStripNoAnswer
            // 
            this.toolStripNoAnswer.Name = "toolStripNoAnswer";
            this.toolStripNoAnswer.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripLblDecision
            // 
            this.toolStripLblDecision.Name = "toolStripLblDecision";
            this.toolStripLblDecision.Size = new System.Drawing.Size(110, 17);
            this.toolStripLblDecision.Text = "ШИЙДВЭРЛЭСЭН :";
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
            // toolStripLblApp
            // 
            this.toolStripLblApp.Name = "toolStripLblApp";
            this.toolStripLblApp.Size = new System.Drawing.Size(106, 17);
            this.toolStripLblApp.Text = "НИЙТ ӨРГӨДӨЛ :";
            // 
            // toolStripApp
            // 
            this.toolStripApp.Name = "toolStripApp";
            this.toolStripApp.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripLblAppDecision
            // 
            this.toolStripLblAppDecision.Name = "toolStripLblAppDecision";
            this.toolStripLblAppDecision.Size = new System.Drawing.Size(169, 17);
            this.toolStripLblAppDecision.Text = "ӨРГӨДӨЛ ШИЙДВЭРЛЭСЭН :";
            // 
            // toolStripAppDecision
            // 
            this.toolStripAppDecision.Name = "toolStripAppDecision";
            this.toolStripAppDecision.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripLblAppAnswer
            // 
            this.toolStripLblAppAnswer.Name = "toolStripLblAppAnswer";
            this.toolStripLblAppAnswer.Size = new System.Drawing.Size(134, 17);
            this.toolStripLblAppAnswer.Text = "ӨРГӨДӨЛ ХАРИУТАЙ :";
            // 
            // toolStripAppAnswer
            // 
            this.toolStripAppAnswer.Name = "toolStripAppAnswer";
            this.toolStripAppAnswer.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripLblAppExpired
            // 
            this.toolStripLblAppExpired.Name = "toolStripLblAppExpired";
            this.toolStripLblAppExpired.Size = new System.Drawing.Size(187, 17);
            this.toolStripLblAppExpired.Text = "ӨРГӨДӨЛ ХУГАЦАА ХЭТЭРСЭН :";
            // 
            // toolStripAppExpired
            // 
            this.toolStripAppExpired.Name = "toolStripAppExpired";
            this.toolStripAppExpired.Size = new System.Drawing.Size(0, 17);
            // 
            // IncomeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeList);
            this.Controls.Add(this.navBarMenu);
            this.Controls.Add(this.statusStrip);
            this.Name = "IncomeList";
            this.Size = new System.Drawing.Size(1193, 675);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarMenu)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup;
        private DevExpress.XtraNavBar.NavBarControl navBarMenu;
        private DevExpress.XtraNavBar.NavBarItem nvItemRefresh;
        private DevExpress.XtraNavBar.NavBarItem nvItemSearch;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem2;
        private DevExpress.XtraNavBar.NavBarItem nvItemDirectRegister;
        private DevExpress.XtraNavBar.NavBarItem nvItemAddCard;
        private DevExpress.XtraNavBar.NavBarItem nvItemDelete;
        private DevExpress.XtraNavBar.NavBarItem nvItemHistory;
        private DevExpress.XtraNavBar.NavBarItem nvItemAddDesc;
        private DevExpress.XtraNavBar.NavBarItem nvItemAddReply;
        private DevExpress.XtraNavBar.NavBarItem nvItemMove;
        private DevExpress.XtraNavBar.NavBarItem nvItemPrint;
        private DevExpress.XtraNavBar.NavBarItem nvItemAddFile;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblNoAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripNoAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblDecision;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblExpired;
        private System.Windows.Forms.ToolStripStatusLabel toolStripExpired;
        private DevExpress.XtraNavBar.NavBarItem nvItemRenewal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripDecision;
        private DevExpress.XtraNavBar.NavBarItem nvItemPrintList;
        private DevExpress.XtraNavBar.NavBarItem nvItemPrintBack;
        private DevExpress.XtraNavBar.NavBarItem nvItemLastList;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblApp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripApp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblAppDecision;
        private System.Windows.Forms.ToolStripStatusLabel toolStripAppDecision;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblAppAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripAppAnswer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblAppExpired;
        private System.Windows.Forms.ToolStripStatusLabel toolStripAppExpired;
    }
}
