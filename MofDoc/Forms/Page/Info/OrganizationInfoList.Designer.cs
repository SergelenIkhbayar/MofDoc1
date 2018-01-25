namespace MofDoc.Forms.Page.Info
{
    partial class OrganizationInfoList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrganizationInfoList));
            this.navBarControl = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiChoose = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiAdd = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiEdit = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiDelete = new DevExpress.XtraNavBar.NavBarItem();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl
            // 
            this.navBarControl.ActiveGroup = this.navBarGroup;
            this.navBarControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup});
            this.navBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiChoose,
            this.nbiAdd,
            this.nbiEdit,
            this.nbiDelete});
            this.navBarControl.Location = new System.Drawing.Point(0, 0);
            this.navBarControl.Name = "navBarControl";
            this.navBarControl.OptionsNavPane.ExpandedWidth = 172;
            this.navBarControl.Size = new System.Drawing.Size(172, 641);
            this.navBarControl.TabIndex = 0;
            this.navBarControl.Text = "navBarControl1";
            // 
            // navBarGroup
            // 
            this.navBarGroup.Caption = "Цэс";
            this.navBarGroup.Expanded = true;
            this.navBarGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiChoose),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiAdd),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiEdit),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDelete)});
            this.navBarGroup.Name = "navBarGroup";
            // 
            // nbiChoose
            // 
            this.nbiChoose.Caption = "СОНГОХ";
            this.nbiChoose.Name = "nbiChoose";
            this.nbiChoose.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiChoose.SmallImage")));
            // 
            // nbiAdd
            // 
            this.nbiAdd.Caption = "НЭМЭХ";
            this.nbiAdd.Name = "nbiAdd";
            this.nbiAdd.SmallImage = global::MofDoc.Properties.Resources.folder_upload;
            // 
            // nbiEdit
            // 
            this.nbiEdit.Caption = "ЗАСАХ";
            this.nbiEdit.Name = "nbiEdit";
            this.nbiEdit.SmallImage = global::MofDoc.Properties.Resources.edit_page;
            // 
            // nbiDelete
            // 
            this.nbiDelete.Caption = "УСТГАХ";
            this.nbiDelete.Name = "nbiDelete";
            this.nbiDelete.SmallImage = global::MofDoc.Properties.Resources.icontexto_webdev_cancel_032x032;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(172, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.NullText = "ХАЙХ ЗҮЙЛЭЭ ЭНД БИЧНЭ ҮҮ.";
            this.txtSearch.Properties.NullValuePrompt = "ХАЙХ ЗҮЙЛЭЭ ЭНД БИЧНЭ ҮҮ.";
            this.txtSearch.Size = new System.Drawing.Size(426, 20);
            this.txtSearch.TabIndex = 2;
            // 
            // treeList
            // 
            this.treeList.Location = new System.Drawing.Point(172, 29);
            this.treeList.Name = "treeList";
            this.treeList.Size = new System.Drawing.Size(597, 609);
            this.treeList.TabIndex = 3;
            // 
            // OrganizationInfoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 641);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.navBarControl);
            this.Controls.Add(this.treeList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrganizationInfoList";
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup;
        private DevExpress.XtraNavBar.NavBarItem nbiChoose;
        private DevExpress.XtraNavBar.NavBarItem nbiAdd;
        private DevExpress.XtraNavBar.NavBarItem nbiEdit;
        private DevExpress.XtraNavBar.NavBarItem nbiDelete;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraTreeList.TreeList treeList;
    }
}
