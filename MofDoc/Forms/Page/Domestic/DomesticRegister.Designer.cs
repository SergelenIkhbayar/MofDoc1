namespace MofDoc.Forms.Page.Domestic
{
    partial class DomesticRegister
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
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barBtnClear = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnRegister = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnPdf = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.lcMain = new DevExpress.XtraLayout.LayoutControl();
            this.ckIsReturn = new DevExpress.XtraEditors.CheckEdit();
            this.dateReturnDate = new DevExpress.XtraEditors.DateEdit();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.ckIsManual = new DevExpress.XtraEditors.CheckEdit();
            this.memoDesc = new DevExpress.XtraEditors.MemoEdit();
            this.txtPageNum = new DevExpress.XtraEditors.TextEdit();
            this.dateReg = new DevExpress.XtraEditors.DateEdit();
            this.txtRegNum = new DevExpress.XtraEditors.TextEdit();
            this.lkUpBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.lcgMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgDoc = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDateCreated = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciRegNum = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIsManual = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDateReturn = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciPageNum = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciIsReturn = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgContent = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDesc = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).BeginInit();
            this.lcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsReturn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReturnDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReturnDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsManual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReg.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDateCreated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRegNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIsManual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDateReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPageNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIsReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDesc)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.barBtnClear,
            this.barBtnRegister,
            this.barBtnDelete,
            this.barBtnCancel,
            this.barBtnPdf});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 8;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
            this.ribbonControl.Size = new System.Drawing.Size(758, 142);
            // 
            // barBtnClear
            // 
            this.barBtnClear.Caption = "Арилгах";
            this.barBtnClear.Id = 1;
            this.barBtnClear.LargeGlyph = global::MofDoc.Properties.Resources._48px_Edit_clear;
            this.barBtnClear.LargeWidth = 70;
            this.barBtnClear.Name = "barBtnClear";
            // 
            // barBtnRegister
            // 
            this.barBtnRegister.Caption = "Хадгалах";
            this.barBtnRegister.Id = 2;
            this.barBtnRegister.LargeGlyph = global::MofDoc.Properties.Resources.icontexto_webdev_file_032x032;
            this.barBtnRegister.LargeWidth = 70;
            this.barBtnRegister.Name = "barBtnRegister";
            // 
            // barBtnDelete
            // 
            this.barBtnDelete.Caption = "Устгах";
            this.barBtnDelete.Id = 5;
            this.barBtnDelete.LargeGlyph = global::MofDoc.Properties.Resources.icontexto_webdev_cancel_032x032;
            this.barBtnDelete.LargeWidth = 70;
            this.barBtnDelete.Name = "barBtnDelete";
            // 
            // barBtnCancel
            // 
            this.barBtnCancel.Caption = "Гарах";
            this.barBtnCancel.Id = 6;
            this.barBtnCancel.LargeGlyph = global::MofDoc.Properties.Resources.quit_48x48;
            this.barBtnCancel.LargeWidth = 70;
            this.barBtnCancel.Name = "barBtnCancel";
            // 
            // barBtnPdf
            // 
            this.barBtnPdf.Caption = "Файл хавсаргах";
            this.barBtnPdf.Id = 7;
            this.barBtnPdf.LargeGlyph = global::MofDoc.Properties.Resources.pdf32x32;
            this.barBtnPdf.LargeWidth = 70;
            this.barBtnPdf.Name = "barBtnPdf";
            // 
            // ribbonPage
            // 
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "Дотоод бичиг";
            // 
            // ribbonPageGroup
            // 
            this.ribbonPageGroup.ItemLinks.Add(this.barBtnClear);
            this.ribbonPageGroup.ItemLinks.Add(this.barBtnRegister);
            this.ribbonPageGroup.ItemLinks.Add(this.barBtnDelete);
            this.ribbonPageGroup.ItemLinks.Add(this.barBtnPdf);
            this.ribbonPageGroup.ItemLinks.Add(this.barBtnCancel);
            this.ribbonPageGroup.Name = "ribbonPageGroup";
            this.ribbonPageGroup.Text = "Дотоод бичгийн цэс";
            // 
            // lcMain
            // 
            this.lcMain.Controls.Add(this.ckIsReturn);
            this.lcMain.Controls.Add(this.dateReturnDate);
            this.lcMain.Controls.Add(this.txtTitle);
            this.lcMain.Controls.Add(this.ckIsManual);
            this.lcMain.Controls.Add(this.memoDesc);
            this.lcMain.Controls.Add(this.txtPageNum);
            this.lcMain.Controls.Add(this.dateReg);
            this.lcMain.Controls.Add(this.txtRegNum);
            this.lcMain.Controls.Add(this.lkUpBranch);
            this.lcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcMain.Location = new System.Drawing.Point(0, 142);
            this.lcMain.Name = "lcMain";
            this.lcMain.Root = this.lcgMain;
            this.lcMain.Size = new System.Drawing.Size(758, 333);
            this.lcMain.TabIndex = 1;
            this.lcMain.Text = "layoutControl1";
            // 
            // ckIsReturn
            // 
            this.ckIsReturn.Location = new System.Drawing.Point(631, 67);
            this.ckIsReturn.MenuManager = this.ribbonControl;
            this.ckIsReturn.Name = "ckIsReturn";
            this.ckIsReturn.Properties.Caption = "Хариутай бичиг";
            this.ckIsReturn.Size = new System.Drawing.Size(103, 19);
            this.ckIsReturn.StyleController = this.lcMain;
            this.ckIsReturn.TabIndex = 20;
            // 
            // dateReturnDate
            // 
            this.dateReturnDate.EditValue = null;
            this.dateReturnDate.Location = new System.Drawing.Point(492, 67);
            this.dateReturnDate.MenuManager = this.ribbonControl;
            this.dateReturnDate.Name = "dateReturnDate";
            this.dateReturnDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateReturnDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateReturnDate.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dateReturnDate.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dateReturnDate.Size = new System.Drawing.Size(135, 20);
            this.dateReturnDate.StyleController = this.lcMain;
            this.dateReturnDate.TabIndex = 19;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(130, 158);
            this.txtTitle.MenuManager = this.ribbonControl;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(604, 20);
            this.txtTitle.StyleController = this.lcMain;
            this.txtTitle.TabIndex = 18;
            // 
            // ckIsManual
            // 
            this.ckIsManual.Location = new System.Drawing.Point(303, 67);
            this.ckIsManual.MenuManager = this.ribbonControl;
            this.ckIsManual.Name = "ckIsManual";
            this.ckIsManual.Properties.Caption = "Гараар";
            this.ckIsManual.Size = new System.Drawing.Size(79, 19);
            this.ckIsManual.StyleController = this.lcMain;
            this.ckIsManual.TabIndex = 17;
            // 
            // memoDesc
            // 
            this.memoDesc.Location = new System.Drawing.Point(130, 182);
            this.memoDesc.Name = "memoDesc";
            this.memoDesc.Size = new System.Drawing.Size(604, 127);
            this.memoDesc.StyleController = this.lcMain;
            this.memoDesc.TabIndex = 14;
            // 
            // txtPageNum
            // 
            this.txtPageNum.Location = new System.Drawing.Point(130, 91);
            this.txtPageNum.Name = "txtPageNum";
            this.txtPageNum.Size = new System.Drawing.Size(252, 20);
            this.txtPageNum.StyleController = this.lcMain;
            this.txtPageNum.TabIndex = 8;
            // 
            // dateReg
            // 
            this.dateReg.EditValue = null;
            this.dateReg.Location = new System.Drawing.Point(492, 43);
            this.dateReg.Name = "dateReg";
            this.dateReg.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateReg.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateReg.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dateReg.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dateReg.Size = new System.Drawing.Size(242, 20);
            this.dateReg.StyleController = this.lcMain;
            this.dateReg.TabIndex = 7;
            // 
            // txtRegNum
            // 
            this.txtRegNum.Location = new System.Drawing.Point(130, 67);
            this.txtRegNum.Name = "txtRegNum";
            this.txtRegNum.Size = new System.Drawing.Size(169, 20);
            this.txtRegNum.StyleController = this.lcMain;
            this.txtRegNum.TabIndex = 6;
            // 
            // lkUpBranch
            // 
            this.lkUpBranch.Location = new System.Drawing.Point(130, 43);
            this.lkUpBranch.Name = "lkUpBranch";
            this.lkUpBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkUpBranch.Properties.NullText = "";
            this.lkUpBranch.Size = new System.Drawing.Size(252, 20);
            this.lkUpBranch.StyleController = this.lcMain;
            this.lkUpBranch.TabIndex = 4;
            // 
            // lcgMain
            // 
            this.lcgMain.CustomizationFormText = "layoutControlGroup1";
            this.lcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgMain.GroupBordersVisible = false;
            this.lcgMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgDoc,
            this.lcgContent});
            this.lcgMain.Location = new System.Drawing.Point(0, 0);
            this.lcgMain.Name = "lcgMain";
            this.lcgMain.Size = new System.Drawing.Size(758, 333);
            this.lcgMain.Text = "lcgMain";
            this.lcgMain.TextVisible = false;
            // 
            // lcgDoc
            // 
            this.lcgDoc.CustomizationFormText = "Явуулсан бичиг";
            this.lcgDoc.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciBranch,
            this.lciDateCreated,
            this.lciRegNum,
            this.lciIsManual,
            this.lciDateReturn,
            this.lciPageNum,
            this.lciIsReturn,
            this.emptySpaceItem1});
            this.lcgDoc.Location = new System.Drawing.Point(0, 0);
            this.lcgDoc.Name = "lcgDoc";
            this.lcgDoc.Size = new System.Drawing.Size(738, 115);
            this.lcgDoc.Text = "Дотоод бичиг";
            // 
            // lciBranch
            // 
            this.lciBranch.Control = this.lkUpBranch;
            this.lciBranch.CustomizationFormText = "Газар, алба :";
            this.lciBranch.Location = new System.Drawing.Point(0, 0);
            this.lciBranch.Name = "lciBranch";
            this.lciBranch.Size = new System.Drawing.Size(362, 24);
            this.lciBranch.Text = "Газар, алба :";
            this.lciBranch.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lciDateCreated
            // 
            this.lciDateCreated.Control = this.dateReg;
            this.lciDateCreated.CustomizationFormText = "Явуулсан огноо :";
            this.lciDateCreated.Location = new System.Drawing.Point(362, 0);
            this.lciDateCreated.Name = "lciDateCreated";
            this.lciDateCreated.Size = new System.Drawing.Size(352, 24);
            this.lciDateCreated.Text = "Явуулсан огноо :";
            this.lciDateCreated.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lciRegNum
            // 
            this.lciRegNum.Control = this.txtRegNum;
            this.lciRegNum.CustomizationFormText = "Бүртгэлийн дугаар :";
            this.lciRegNum.Location = new System.Drawing.Point(0, 24);
            this.lciRegNum.Name = "lciRegNum";
            this.lciRegNum.Size = new System.Drawing.Size(279, 24);
            this.lciRegNum.Text = "Бүртгэлийн дугаар :";
            this.lciRegNum.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lciIsManual
            // 
            this.lciIsManual.Control = this.ckIsManual;
            this.lciIsManual.CustomizationFormText = "lciIsManual";
            this.lciIsManual.Location = new System.Drawing.Point(279, 24);
            this.lciIsManual.Name = "lciIsManual";
            this.lciIsManual.Size = new System.Drawing.Size(83, 24);
            this.lciIsManual.Text = "lciIsManual";
            this.lciIsManual.TextSize = new System.Drawing.Size(0, 0);
            this.lciIsManual.TextToControlDistance = 0;
            this.lciIsManual.TextVisible = false;
            // 
            // lciDateReturn
            // 
            this.lciDateReturn.Control = this.dateReturnDate;
            this.lciDateReturn.CustomizationFormText = "Хариу өгөх огноо :";
            this.lciDateReturn.Location = new System.Drawing.Point(362, 24);
            this.lciDateReturn.Name = "lciDateReturn";
            this.lciDateReturn.Size = new System.Drawing.Size(245, 24);
            this.lciDateReturn.Text = "Хариу өгөх огноо :";
            this.lciDateReturn.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lciPageNum
            // 
            this.lciPageNum.Control = this.txtPageNum;
            this.lciPageNum.CustomizationFormText = "Хуудасны тоо :";
            this.lciPageNum.Location = new System.Drawing.Point(0, 48);
            this.lciPageNum.Name = "lciPageNum";
            this.lciPageNum.Size = new System.Drawing.Size(362, 24);
            this.lciPageNum.Text = "Хуудасны тоо :";
            this.lciPageNum.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lciIsReturn
            // 
            this.lciIsReturn.Control = this.ckIsReturn;
            this.lciIsReturn.CustomizationFormText = "lciIsReturn";
            this.lciIsReturn.Location = new System.Drawing.Point(607, 24);
            this.lciIsReturn.Name = "lciIsReturn";
            this.lciIsReturn.Size = new System.Drawing.Size(107, 24);
            this.lciIsReturn.Text = "lciIsReturn";
            this.lciIsReturn.TextSize = new System.Drawing.Size(0, 0);
            this.lciIsReturn.TextToControlDistance = 0;
            this.lciIsReturn.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(362, 48);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(352, 24);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgContent
            // 
            this.lcgContent.CustomizationFormText = "Агуулга";
            this.lcgContent.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciTitle,
            this.lciDesc});
            this.lcgContent.Location = new System.Drawing.Point(0, 115);
            this.lcgContent.Name = "lcgContent";
            this.lcgContent.Size = new System.Drawing.Size(738, 198);
            this.lcgContent.Text = "Агуулга";
            // 
            // lciTitle
            // 
            this.lciTitle.Control = this.txtTitle;
            this.lciTitle.CustomizationFormText = "Гарчиг";
            this.lciTitle.Location = new System.Drawing.Point(0, 0);
            this.lciTitle.Name = "lciTitle";
            this.lciTitle.Size = new System.Drawing.Size(714, 24);
            this.lciTitle.Text = "Гарчиг";
            this.lciTitle.TextSize = new System.Drawing.Size(103, 13);
            // 
            // lciDesc
            // 
            this.lciDesc.Control = this.memoDesc;
            this.lciDesc.CustomizationFormText = "Тайлбар :";
            this.lciDesc.Location = new System.Drawing.Point(0, 24);
            this.lciDesc.Name = "lciDesc";
            this.lciDesc.Size = new System.Drawing.Size(714, 131);
            this.lciDesc.Text = "Тайлбар :";
            this.lciDesc.TextSize = new System.Drawing.Size(103, 13);
            // 
            // DomesticRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 475);
            this.Controls.Add(this.lcMain);
            this.Controls.Add(this.ribbonControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DomesticRegister";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcMain)).EndInit();
            this.lcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckIsReturn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReturnDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReturnDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsManual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReg.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateReg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDateCreated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRegNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIsManual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDateReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPageNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciIsReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDesc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.BarButtonItem barBtnClear;
        private DevExpress.XtraBars.BarButtonItem barBtnRegister;
        private DevExpress.XtraBars.BarButtonItem barBtnDelete;
        private DevExpress.XtraBars.BarButtonItem barBtnCancel;
        private DevExpress.XtraBars.BarButtonItem barBtnPdf;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup;
        private DevExpress.XtraLayout.LayoutControl lcMain;
        private DevExpress.XtraEditors.CheckEdit ckIsManual;
        private DevExpress.XtraEditors.MemoEdit memoDesc;
        private DevExpress.XtraEditors.TextEdit txtPageNum;
        private DevExpress.XtraEditors.DateEdit dateReg;
        private DevExpress.XtraEditors.TextEdit txtRegNum;
        private DevExpress.XtraEditors.LookUpEdit lkUpBranch;
        private DevExpress.XtraLayout.LayoutControlGroup lcgMain;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDoc;
        private DevExpress.XtraLayout.LayoutControlItem lciBranch;
        private DevExpress.XtraLayout.LayoutControlItem lciDateCreated;
        private DevExpress.XtraLayout.LayoutControlItem lciRegNum;
        private DevExpress.XtraLayout.LayoutControlItem lciPageNum;
        private DevExpress.XtraLayout.LayoutControlItem lciIsManual;
        private DevExpress.XtraLayout.LayoutControlItem lciDesc;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraLayout.LayoutControlItem lciTitle;
        private DevExpress.XtraLayout.LayoutControlGroup lcgContent;
        private DevExpress.XtraEditors.DateEdit dateReturnDate;
        private DevExpress.XtraLayout.LayoutControlItem lciDateReturn;
        private DevExpress.XtraEditors.CheckEdit ckIsReturn;
        private DevExpress.XtraLayout.LayoutControlItem lciIsReturn;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}