namespace MofDoc.Forms.Page.Income
{
    partial class AddDecision
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
            this.btnRegister = new DevExpress.XtraEditors.SimpleButton();
            this.memoDecision = new DevExpress.XtraEditors.MemoEdit();
            this.dateClosed = new DevExpress.XtraEditors.DateEdit();
            this.txtDocNum = new DevExpress.XtraEditors.TextEdit();
            this.txtRegNum = new DevExpress.XtraEditors.TextEdit();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgDoc = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciRegNum = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDocNum = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgDecision = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciClosedDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDecision = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciRegister = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lkUpStaff = new DevExpress.XtraEditors.LookUpEdit();
            this.lciStaff = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoDecision.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateClosed.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateClosed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRegNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDocNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciClosedDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRegister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStaff)).BeginInit();
            this.SuspendLayout();
            // 
            // lcMain
            // 
            this.lcMain.Controls.Add(this.lkUpStaff);
            this.lcMain.Controls.Add(this.btnCancel);
            this.lcMain.Controls.Add(this.btnRegister);
            this.lcMain.Controls.Add(this.memoDecision);
            this.lcMain.Controls.Add(this.dateClosed);
            this.lcMain.Controls.Add(this.txtDocNum);
            this.lcMain.Controls.Add(this.txtRegNum);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcMain.Location = new System.Drawing.Point(0, 0);
            this.lcMain.Name = "lcMain";
            this.lcMain.Root = this.lcgMain;
            this.lcMain.Size = new System.Drawing.Size(589, 277);
            this.lcMain.TabIndex = 0;
            this.lcMain.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(243, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 22);
            this.btnCancel.StyleController = this.lcMain;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Буцах";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(385, 243);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(192, 22);
            this.btnRegister.StyleController = this.lcMain;
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "Бүртгэх";
            // 
            // memoDecision
            // 
            this.memoDecision.Location = new System.Drawing.Point(24, 150);
            this.memoDecision.Name = "memoDecision";
            this.memoDecision.Size = new System.Drawing.Size(541, 53);
            this.memoDecision.StyleController = this.lcMain;
            this.memoDecision.TabIndex = 7;
            // 
            // dateClosed
            // 
            this.dateClosed.EditValue = null;
            this.dateClosed.Location = new System.Drawing.Point(123, 110);
            this.dateClosed.Name = "dateClosed";
            this.dateClosed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateClosed.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateClosed.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dateClosed.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dateClosed.Size = new System.Drawing.Size(169, 20);
            this.dateClosed.StyleController = this.lcMain;
            this.dateClosed.TabIndex = 6;
            // 
            // txtDocNum
            // 
            this.txtDocNum.Location = new System.Drawing.Point(395, 43);
            this.txtDocNum.Name = "txtDocNum";
            this.txtDocNum.Size = new System.Drawing.Size(170, 20);
            this.txtDocNum.StyleController = this.lcMain;
            this.txtDocNum.TabIndex = 5;
            // 
            // txtRegNum
            // 
            this.txtRegNum.Location = new System.Drawing.Point(123, 43);
            this.txtRegNum.Name = "txtRegNum";
            this.txtRegNum.Size = new System.Drawing.Size(169, 20);
            this.txtRegNum.StyleController = this.lcMain;
            this.txtRegNum.TabIndex = 4;
            // 
            // lcgMain
            // 
            this.lcgMain.CustomizationFormText = "lcgMain";
            this.lcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgMain.GroupBordersVisible = false;
            this.lcgMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgDoc,
            this.lcgDecision,
            this.lciRegister,
            this.lciCancel,
            this.emptySpaceItem3});
            this.lcgMain.Location = new System.Drawing.Point(0, 0);
            this.lcgMain.Name = "lcgMain";
            this.lcgMain.Size = new System.Drawing.Size(589, 277);
            this.lcgMain.Text = "lcgMain";
            this.lcgMain.TextVisible = false;
            // 
            // lcgDoc
            // 
            this.lcgDoc.CustomizationFormText = "Бичгийн мэдээлэл";
            this.lcgDoc.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciRegNum,
            this.lciDocNum});
            this.lcgDoc.Location = new System.Drawing.Point(0, 0);
            this.lcgDoc.Name = "lcgDoc";
            this.lcgDoc.Size = new System.Drawing.Size(569, 67);
            this.lcgDoc.Text = "Бичгийн мэдээлэл";
            // 
            // lciRegNum
            // 
            this.lciRegNum.Control = this.txtRegNum;
            this.lciRegNum.CustomizationFormText = "Бүртгэлийн дугаар";
            this.lciRegNum.Location = new System.Drawing.Point(0, 0);
            this.lciRegNum.Name = "lciRegNum";
            this.lciRegNum.Size = new System.Drawing.Size(272, 24);
            this.lciRegNum.Text = "Бүртгэлийн дугаар";
            this.lciRegNum.TextSize = new System.Drawing.Size(96, 13);
            // 
            // lciDocNum
            // 
            this.lciDocNum.Control = this.txtDocNum;
            this.lciDocNum.CustomizationFormText = "Бичгийн дугаар :";
            this.lciDocNum.Location = new System.Drawing.Point(272, 0);
            this.lciDocNum.Name = "lciDocNum";
            this.lciDocNum.Size = new System.Drawing.Size(273, 24);
            this.lciDocNum.Text = "Бичгийн дугаар :";
            this.lciDocNum.TextSize = new System.Drawing.Size(96, 13);
            // 
            // lcgDecision
            // 
            this.lcgDecision.CustomizationFormText = "Шийдвэр";
            this.lcgDecision.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciClosedDate,
            this.lciDecision,
            this.emptySpaceItem2,
            this.lciStaff});
            this.lcgDecision.Location = new System.Drawing.Point(0, 67);
            this.lcgDecision.Name = "lcgDecision";
            this.lcgDecision.Size = new System.Drawing.Size(569, 164);
            this.lcgDecision.Text = "Шийдвэр";
            // 
            // lciClosedDate
            // 
            this.lciClosedDate.Control = this.dateClosed;
            this.lciClosedDate.CustomizationFormText = "Хаасан огноо :";
            this.lciClosedDate.Location = new System.Drawing.Point(0, 0);
            this.lciClosedDate.Name = "lciClosedDate";
            this.lciClosedDate.Size = new System.Drawing.Size(272, 24);
            this.lciClosedDate.Text = "Хаасан огноо :";
            this.lciClosedDate.TextSize = new System.Drawing.Size(96, 13);
            // 
            // lciDecision
            // 
            this.lciDecision.Control = this.memoDecision;
            this.lciDecision.CustomizationFormText = "Шийдвэр :";
            this.lciDecision.Location = new System.Drawing.Point(0, 24);
            this.lciDecision.Name = "lciDecision";
            this.lciDecision.Size = new System.Drawing.Size(545, 73);
            this.lciDecision.Text = "Шийдвэр :";
            this.lciDecision.TextLocation = DevExpress.Utils.Locations.Top;
            this.lciDecision.TextSize = new System.Drawing.Size(96, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(272, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(273, 24);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciRegister
            // 
            this.lciRegister.Control = this.btnRegister;
            this.lciRegister.CustomizationFormText = "lciRegister";
            this.lciRegister.Location = new System.Drawing.Point(373, 231);
            this.lciRegister.Name = "lciRegister";
            this.lciRegister.Size = new System.Drawing.Size(196, 26);
            this.lciRegister.Text = "lciRegister";
            this.lciRegister.TextSize = new System.Drawing.Size(0, 0);
            this.lciRegister.TextToControlDistance = 0;
            this.lciRegister.TextVisible = false;
            // 
            // lciCancel
            // 
            this.lciCancel.Control = this.btnCancel;
            this.lciCancel.CustomizationFormText = "lciCancel";
            this.lciCancel.Location = new System.Drawing.Point(231, 231);
            this.lciCancel.Name = "lciCancel";
            this.lciCancel.Size = new System.Drawing.Size(142, 26);
            this.lciCancel.Text = "lciCancel";
            this.lciCancel.TextSize = new System.Drawing.Size(0, 0);
            this.lciCancel.TextToControlDistance = 0;
            this.lciCancel.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 231);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(231, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lkUpStaff
            // 
            this.lkUpStaff.Location = new System.Drawing.Point(123, 207);
            this.lkUpStaff.Name = "lkUpStaff";
            this.lkUpStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkUpStaff.Properties.NullText = "";
            this.lkUpStaff.Size = new System.Drawing.Size(442, 20);
            this.lkUpStaff.StyleController = this.lcMain;
            this.lkUpStaff.TabIndex = 11;
            // 
            // lciStaff
            // 
            this.lciStaff.Control = this.lkUpStaff;
            this.lciStaff.CustomizationFormText = "Хаасан ажилтан :";
            this.lciStaff.Location = new System.Drawing.Point(0, 97);
            this.lciStaff.Name = "lciStaff";
            this.lciStaff.Size = new System.Drawing.Size(545, 24);
            this.lciStaff.Text = "Хаасан ажилтан :";
            this.lciStaff.TextSize = new System.Drawing.Size(96, 13);
            // 
            // AddDecision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 277);
            this.Controls.Add(this.lcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddDecision";
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoDecision.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateClosed.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateClosed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRegNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDocNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciClosedDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRegister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStaff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMain;
        private DevExpress.XtraEditors.TextEdit txtDocNum;
        private DevExpress.XtraEditors.TextEdit txtRegNum;
        private DevExpress.XtraLayout.LayoutControlItem lciRegNum;
        private DevExpress.XtraLayout.LayoutControlItem lciDocNum;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDoc;
        private DevExpress.XtraEditors.MemoEdit memoDecision;
        private DevExpress.XtraEditors.DateEdit dateClosed;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDecision;
        private DevExpress.XtraLayout.LayoutControlItem lciClosedDate;
        private DevExpress.XtraLayout.LayoutControlItem lciDecision;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnRegister;
        private DevExpress.XtraLayout.LayoutControlItem lciRegister;
        private DevExpress.XtraLayout.LayoutControlItem lciCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.LookUpEdit lkUpStaff;
        private DevExpress.XtraLayout.LayoutControlItem lciStaff;
    }
}