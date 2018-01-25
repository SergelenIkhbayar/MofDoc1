namespace MofDoc.Forms.Page
{
    partial class MainPage
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemIncome = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOutcome = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemReport = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuIncomeReport = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuOutcomeReport = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOrganization = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabCtl = new DevExpress.XtraTab.XtraTabControl();
            this.MenuItemDomestic = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainTabCtl)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemIncome,
            this.MenuItemOutcome,
            this.MenuItemDomestic,
            this.MenuItemReport,
            this.MenuItemInfo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1118, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuItemQuit});
            this.MenuItemFile.Name = "MenuItemFile";
            this.MenuItemFile.Size = new System.Drawing.Size(48, 20);
            this.MenuItemFile.Text = "Файл";
            // 
            // SubMenuItemQuit
            // 
            this.SubMenuItemQuit.Name = "SubMenuItemQuit";
            this.SubMenuItemQuit.Size = new System.Drawing.Size(104, 22);
            this.SubMenuItemQuit.Text = "Гарах";
            // 
            // MenuItemIncome
            // 
            this.MenuItemIncome.Name = "MenuItemIncome";
            this.MenuItemIncome.Size = new System.Drawing.Size(90, 20);
            this.MenuItemIncome.Text = "Ирсэн бичиг";
            // 
            // MenuItemOutcome
            // 
            this.MenuItemOutcome.Name = "MenuItemOutcome";
            this.MenuItemOutcome.Size = new System.Drawing.Size(87, 20);
            this.MenuItemOutcome.Text = "Явсан бичиг";
            // 
            // MenuItemReport
            // 
            this.MenuItemReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuIncomeReport,
            this.SubMenuOutcomeReport});
            this.MenuItemReport.Name = "MenuItemReport";
            this.MenuItemReport.Size = new System.Drawing.Size(59, 20);
            this.MenuItemReport.Text = "Тайлан";
            // 
            // SubMenuIncomeReport
            // 
            this.SubMenuIncomeReport.Name = "SubMenuIncomeReport";
            this.SubMenuIncomeReport.Size = new System.Drawing.Size(145, 22);
            this.SubMenuIncomeReport.Text = "Ирсэн бичиг";
            // 
            // SubMenuOutcomeReport
            // 
            this.SubMenuOutcomeReport.Name = "SubMenuOutcomeReport";
            this.SubMenuOutcomeReport.Size = new System.Drawing.Size(145, 22);
            this.SubMenuOutcomeReport.Text = "Явсан бичиг";
            // 
            // MenuItemInfo
            // 
            this.MenuItemInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemOrganization});
            this.MenuItemInfo.Name = "MenuItemInfo";
            this.MenuItemInfo.Size = new System.Drawing.Size(57, 20);
            this.MenuItemInfo.Text = "Лавлах";
            // 
            // MenuItemOrganization
            // 
            this.MenuItemOrganization.Name = "MenuItemOrganization";
            this.MenuItemOrganization.Size = new System.Drawing.Size(142, 22);
            this.MenuItemOrganization.Text = "Байгууллага";
            // 
            // mainTabCtl
            // 
            this.mainTabCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabCtl.Location = new System.Drawing.Point(0, 24);
            this.mainTabCtl.Name = "mainTabCtl";
            this.mainTabCtl.Size = new System.Drawing.Size(1118, 693);
            this.mainTabCtl.TabIndex = 5;
            // 
            // MenuItemDomestic
            // 
            this.MenuItemDomestic.Name = "MenuItemDomestic";
            this.MenuItemDomestic.Size = new System.Drawing.Size(95, 20);
            this.MenuItemDomestic.Text = "Дотоод бичиг";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainTabCtl);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainPage";
            this.Size = new System.Drawing.Size(1118, 717);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainTabCtl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem SubMenuItemQuit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemIncome;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOutcome;
        private System.Windows.Forms.ToolStripMenuItem MenuItemReport;
        private System.Windows.Forms.ToolStripMenuItem SubMenuIncomeReport;
        private System.Windows.Forms.ToolStripMenuItem SubMenuOutcomeReport;
        internal DevExpress.XtraTab.XtraTabControl mainTabCtl;
        private System.Windows.Forms.ToolStripMenuItem MenuItemInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOrganization;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDomestic;
    }
}
