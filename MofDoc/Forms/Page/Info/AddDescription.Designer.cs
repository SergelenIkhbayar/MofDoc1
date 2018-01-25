namespace MofDoc.Forms.Page.Info
{
    partial class AddDescription
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
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.bnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddDesc = new DevExpress.XtraEditors.SimpleButton();
            this.memoDesc = new DevExpress.XtraEditors.MemoEdit();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciDesc = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Controls.Add(this.bnCancel);
            this.lcMain.Controls.Add(this.btnAddDesc);
            this.lcMain.Controls.Add(this.memoDesc);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
            this.lcMain.Root = this.lcgMain;
            this.lcMain.Size = new System.Drawing.Size(361, 246);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // bnCancel
            // 
            this.bnCancel.Location = new System.Drawing.Point(12, 212);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(166, 22);
            this.bnCancel.StyleController = this.lcMain;
            this.bnCancel.TabIndex = 6;
            this.bnCancel.Text = "Болих";
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // btnAddDesc
            // 
            this.btnAddDesc.Location = new System.Drawing.Point(182, 212);
            this.btnAddDesc.Name = "btnAddDesc";
            this.btnAddDesc.Size = new System.Drawing.Size(167, 22);
            this.btnAddDesc.StyleController = this.lcMain;
            this.btnAddDesc.TabIndex = 5;
            this.btnAddDesc.Text = "Оруулах";
            this.btnAddDesc.Click += new System.EventHandler(this.btnAddDesc_Click);
            // 
            // memoDesc
            // 
            this.memoDesc.Location = new System.Drawing.Point(12, 28);
            this.memoDesc.Name = "memoDesc";
            this.memoDesc.Size = new System.Drawing.Size(337, 180);
            this.memoDesc.StyleController = this.lcMain;
            this.memoDesc.TabIndex = 4;
            // 
            // lcgMain
            // 
            this.lcgMain.CustomizationFormText = "lcgMain";
            this.lcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgMain.GroupBordersVisible = false;
            this.lcgMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciDesc,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.lcgMain.Location = new System.Drawing.Point(0, 0);
            this.lcgMain.Name = "lcgMain";
            this.lcgMain.Size = new System.Drawing.Size(361, 246);
            this.lcgMain.Text = "lcgMain";
            this.lcgMain.TextVisible = false;
            // 
            // lciDesc
            // 
            this.lciDesc.Control = this.memoDesc;
            this.lciDesc.CustomizationFormText = "Тайлбар :";
            this.lciDesc.Location = new System.Drawing.Point(0, 0);
            this.lciDesc.Name = "lciDesc";
            this.lciDesc.Size = new System.Drawing.Size(341, 200);
            this.lciDesc.Text = "Тайлбар :";
            this.lciDesc.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciDesc.TextSize = new System.Drawing.Size(49, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnAddDesc;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(170, 200);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(171, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.bnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 200);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(170, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // AddDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 246);
            this.Controls.Add(this.lcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDescription";
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMain;
        private DevExpress.XtraEditors.MemoEdit memoDesc;
        private DevExpress.XtraLayout.LayoutControlItem lciDesc;
        private DevExpress.XtraEditors.SimpleButton bnCancel;
        private DevExpress.XtraEditors.SimpleButton btnAddDesc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}