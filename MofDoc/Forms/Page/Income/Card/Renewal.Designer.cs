namespace MofDoc.Forms.Page.Income.Card
{
    partial class Renewal
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnRenewal = new DevExpress.XtraEditors.SimpleButton();
            this.txtRenewal = new DevExpress.XtraEditors.TextEdit();
            this.dateReturn = new DevExpress.XtraEditors.DateEdit();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciReturnDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciRenewal = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.memoDesc = new DevExpress.XtraEditors.MemoEdit();
            this.lciDesc = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRenewal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReturn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReturn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciReturnDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRenewal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDesc)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Controls.Add(this.memoDesc);
            this.lcMain.Controls.Add(this.btnCancel);
            this.lcMain.Controls.Add(this.btnRenewal);
            this.lcMain.Controls.Add(this.txtRenewal);
            this.lcMain.Controls.Add(this.dateReturn);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
            this.lcMain.Root = this.lcgMain;
            this.lcMain.Size = new System.Drawing.Size(391, 186);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(182, 22);
            this.btnCancel.StyleController = this.lcMain;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Буцах";
            // 
            // btnRenewal
            // 
            this.btnRenewal.Location = new System.Drawing.Point(198, 152);
            this.btnRenewal.Name = "btnRenewal";
            this.btnRenewal.Size = new System.Drawing.Size(181, 22);
            this.btnRenewal.StyleController = this.lcMain;
            this.btnRenewal.TabIndex = 6;
            this.btnRenewal.Text = "Сунгах";
            // 
            // txtRenewal
            // 
            this.txtRenewal.Location = new System.Drawing.Point(202, 104);
            this.txtRenewal.Name = "txtRenewal";
            this.txtRenewal.Size = new System.Drawing.Size(177, 20);
            this.txtRenewal.StyleController = this.lcMain;
            this.txtRenewal.TabIndex = 5;
            // 
            // dateReturn
            // 
            this.dateReturn.EditValue = null;
            this.dateReturn.Location = new System.Drawing.Point(202, 128);
            this.dateReturn.Name = "dateReturn";
            this.dateReturn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateReturn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateReturn.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dateReturn.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dateReturn.Size = new System.Drawing.Size(177, 20);
            this.dateReturn.StyleController = this.lcMain;
            this.dateReturn.TabIndex = 4;
            // 
            // lcgMain
            // 
            this.lcgMain.CustomizationFormText = "lcgMain";
            this.lcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgMain.GroupBordersVisible = false;
            this.lcgMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciReturnDate,
            this.lciRenewal,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.lciDesc});
            this.lcgMain.Location = new System.Drawing.Point(0, 0);
            this.lcgMain.Name = "lcgMain";
            this.lcgMain.Size = new System.Drawing.Size(391, 186);
            this.lcgMain.Text = "lcgMain";
            this.lcgMain.TextVisible = false;
            // 
            // lciReturnDate
            // 
            this.lciReturnDate.Control = this.dateReturn;
            this.lciReturnDate.CustomizationFormText = "Хаах огноо :";
            this.lciReturnDate.Location = new System.Drawing.Point(0, 116);
            this.lciReturnDate.Name = "lciReturnDate";
            this.lciReturnDate.Size = new System.Drawing.Size(371, 24);
            this.lciReturnDate.Text = "Хариу өгөх огноо :";
            this.lciReturnDate.TextSize = new System.Drawing.Size(187, 13);
            // 
            // lciRenewal
            // 
            this.lciRenewal.Control = this.txtRenewal;
            this.lciRenewal.CustomizationFormText = "layoutControlItem2";
            this.lciRenewal.Location = new System.Drawing.Point(0, 92);
            this.lciRenewal.Name = "lciRenewal";
            this.lciRenewal.Size = new System.Drawing.Size(371, 24);
            this.lciRenewal.Text = "Сунгалт (өдрөөр оруулна уу) :";
            this.lciRenewal.TextSize = new System.Drawing.Size(187, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnRenewal;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(186, 140);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(185, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 140);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(186, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // memoDesc
            // 
            this.memoDesc.Location = new System.Drawing.Point(12, 28);
            this.memoDesc.Name = "memoDesc";
            this.memoDesc.Size = new System.Drawing.Size(367, 72);
            this.memoDesc.StyleController = this.lcMain;
            this.memoDesc.TabIndex = 8;
            // 
            // lciDesc
            // 
            this.lciDesc.Control = this.memoDesc;
            this.lciDesc.CustomizationFormText = "Сунгаж буй шалтгаанаа оруулна уу.";
            this.lciDesc.Location = new System.Drawing.Point(0, 0);
            this.lciDesc.Name = "lciDesc";
            this.lciDesc.Size = new System.Drawing.Size(371, 92);
            this.lciDesc.Text = "Сунгаж буй шалтгаанаа оруулна уу.";
            this.lciDesc.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciDesc.TextSize = new System.Drawing.Size(187, 13);
            // 
            // Renewal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 186);
            this.Controls.Add(this.lcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Renewal";
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRenewal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReturn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReturn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciReturnDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRenewal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDesc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMain;
        private DevExpress.XtraEditors.TextEdit txtRenewal;
        private DevExpress.XtraEditors.DateEdit dateReturn;
        private DevExpress.XtraLayout.LayoutControlItem lciReturnDate;
        private DevExpress.XtraLayout.LayoutControlItem lciRenewal;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnRenewal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.MemoEdit memoDesc;
        private DevExpress.XtraLayout.LayoutControlItem lciDesc;
    }
}