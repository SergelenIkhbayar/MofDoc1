namespace MofDoc.Forms
{
    partial class Home
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
            this.tabCtl = new DevExpress.XtraTab.XtraTabControl();
            this.tabHome = new DevExpress.XtraTab.XtraTabPage();
            this.tileCtl = new DevExpress.XtraEditors.TileControl();
            this.galleryControl = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.picTop = new DevExpress.XtraEditors.PictureEdit();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabelDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDbUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuItemConfigDb = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tabCtl)).BeginInit();
            this.tabCtl.SuspendLayout();
            this.tabHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl)).BeginInit();
            this.galleryControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTop.Properties)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtl
            // 
            this.tabCtl.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tabCtl.Appearance.Options.UseBackColor = true;
            this.tabCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtl.Location = new System.Drawing.Point(0, 0);
            this.tabCtl.Name = "tabCtl";
            this.tabCtl.SelectedTabPage = this.tabHome;
            this.tabCtl.Size = new System.Drawing.Size(1056, 703);
            this.tabCtl.TabIndex = 0;
            this.tabCtl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabHome});
            // 
            // tabHome
            // 
            this.tabHome.Controls.Add(this.tileCtl);
            this.tabHome.Controls.Add(this.galleryControl);
            this.tabHome.Controls.Add(this.picTop);
            this.tabHome.Controls.Add(this.statusStrip1);
            this.tabHome.Controls.Add(this.menuStrip1);
            this.tabHome.Name = "tabHome";
            this.tabHome.Size = new System.Drawing.Size(1050, 684);
            // 
            // tileCtl
            // 
            this.tileCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileCtl.Location = new System.Drawing.Point(0, 323);
            this.tileCtl.MaxId = 8;
            this.tileCtl.Name = "tileCtl";
            this.tileCtl.Size = new System.Drawing.Size(1050, 339);
            this.tileCtl.TabIndex = 1;
            this.tileCtl.Text = "tileControl";
            // 
            // galleryControl
            // 
            this.galleryControl.Controls.Add(this.galleryControlClient1);
            this.galleryControl.DesignGalleryGroupIndex = 0;
            this.galleryControl.DesignGalleryItemIndex = 0;
            this.galleryControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.galleryControl.Location = new System.Drawing.Point(0, 122);
            this.galleryControl.Name = "galleryControl";
            this.galleryControl.Size = new System.Drawing.Size(1050, 201);
            this.galleryControl.TabIndex = 4;
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(1029, 197);
            // 
            // picTop
            // 
            this.picTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.picTop.Location = new System.Drawing.Point(0, 24);
            this.picTop.Name = "picTop";
            this.picTop.Size = new System.Drawing.Size(1050, 98);
            this.picTop.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabelDb,
            this.toolStripStatusLabelDb,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelDbUser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 662);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1050, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabelDb
            // 
            this.StatusLabelDb.Name = "StatusLabelDb";
            this.StatusLabelDb.Size = new System.Drawing.Size(149, 17);
            this.StatusLabelDb.Text = "Өгөгдлийн сангийн хаяг : ";
            // 
            // toolStripStatusLabelDb
            // 
            this.toolStripStatusLabelDb.Name = "toolStripStatusLabelDb";
            this.toolStripStatusLabelDb.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelDb.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(139, 17);
            this.toolStripStatusLabel2.Text = "Холбогдсон хэрэглэгч : ";
            // 
            // toolStripStatusLabelDbUser
            // 
            this.toolStripStatusLabelDbUser.Name = "toolStripStatusLabelDbUser";
            this.toolStripStatusLabelDbUser.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelDbUser.Text = "toolStripStatusLabel3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemDatabase});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1050, 24);
            this.menuStrip1.TabIndex = 2;
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
            // MenuItemDatabase
            // 
            this.MenuItemDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuItemConfigDb});
            this.MenuItemDatabase.Name = "MenuItemDatabase";
            this.MenuItemDatabase.Size = new System.Drawing.Size(101, 20);
            this.MenuItemDatabase.Text = "Өгөгдлийн сан";
            // 
            // SubMenuItemConfigDb
            // 
            this.SubMenuItemConfigDb.Name = "SubMenuItemConfigDb";
            this.SubMenuItemConfigDb.Size = new System.Drawing.Size(130, 22);
            this.SubMenuItemConfigDb.Text = "Шинэчлэх";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 703);
            this.Controls.Add(this.tabCtl);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Home";
            this.Text = "Home";
            ((System.ComponentModel.ISupportInitialize)(this.tabCtl)).EndInit();
            this.tabCtl.ResumeLayout(false);
            this.tabHome.ResumeLayout(false);
            this.tabHome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl)).EndInit();
            this.galleryControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTop.Properties)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabPage tabHome;
        private DevExpress.XtraEditors.PictureEdit picTop;
        private DevExpress.XtraEditors.TileControl tileCtl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDatabase;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem SubMenuItemQuit;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelDb;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDb;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDbUser;
        private System.Windows.Forms.ToolStripMenuItem SubMenuItemConfigDb;
        internal DevExpress.XtraTab.XtraTabControl tabCtl;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
    }
}