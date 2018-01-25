namespace MofDoc.Forms.Page.Info
{
    partial class AddOrganizationInfo
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
            this.lcAddOrganization = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnRegister = new DevExpress.XtraEditors.SimpleButton();
            this.txtOrganizationName = new DevExpress.XtraEditors.TextEdit();
            this.lkUpLocation = new DevExpress.XtraEditors.LookUpEdit();
            this.lcgAddOrganization = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciLocation = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOrganizationName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciRegister = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCancel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcAddOrganization)).BeginInit();
            this.lcAddOrganization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrganizationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAddOrganization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOrganizationName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRegister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // lcAddOrganization
            // 
            this.lcAddOrganization.Controls.Add(this.btnCancel);
            this.lcAddOrganization.Controls.Add(this.btnRegister);
            this.lcAddOrganization.Controls.Add(this.txtOrganizationName);
            this.lcAddOrganization.Controls.Add(this.lkUpLocation);
            this.lcAddOrganization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcAddOrganization.Location = new System.Drawing.Point(0, 0);
            this.lcAddOrganization.Name = "lcAddOrganization";
            this.lcAddOrganization.Root = this.lcgAddOrganization;
            this.lcAddOrganization.Size = new System.Drawing.Size(372, 157);
            this.lcAddOrganization.TabIndex = 0;
            this.lcAddOrganization.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(149, 22);
            this.btnCancel.StyleController = this.lcAddOrganization;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Буцах";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(165, 80);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(195, 22);
            this.btnRegister.StyleController = this.lcAddOrganization;
            this.btnRegister.TabIndex = 6;
            this.btnRegister.Text = "Бүртгэх";
            // 
            // txtOrganizationName
            // 
            this.txtOrganizationName.Location = new System.Drawing.Point(86, 46);
            this.txtOrganizationName.Name = "txtOrganizationName";
            this.txtOrganizationName.Size = new System.Drawing.Size(274, 20);
            this.txtOrganizationName.StyleController = this.lcAddOrganization;
            this.txtOrganizationName.TabIndex = 5;
            // 
            // lkUpLocation
            // 
            this.lkUpLocation.Location = new System.Drawing.Point(86, 12);
            this.lkUpLocation.Name = "lkUpLocation";
            this.lkUpLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkUpLocation.Properties.NullText = "";
            this.lkUpLocation.Size = new System.Drawing.Size(274, 20);
            this.lkUpLocation.StyleController = this.lcAddOrganization;
            this.lkUpLocation.TabIndex = 4;
            // 
            // lcgAddOrganization
            // 
            this.lcgAddOrganization.CustomizationFormText = "lcgAddOrganization";
            this.lcgAddOrganization.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgAddOrganization.GroupBordersVisible = false;
            this.lcgAddOrganization.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciLocation,
            this.lciOrganizationName,
            this.lciRegister,
            this.lciCancel});
            this.lcgAddOrganization.Location = new System.Drawing.Point(0, 0);
            this.lcgAddOrganization.Name = "lcgAddOrganization";
            this.lcgAddOrganization.Size = new System.Drawing.Size(372, 157);
            this.lcgAddOrganization.Text = "lcgAddOrganization";
            this.lcgAddOrganization.TextVisible = false;
            // 
            // lciLocation
            // 
            this.lciLocation.Control = this.lkUpLocation;
            this.lciLocation.CustomizationFormText = "Байршил :";
            this.lciLocation.Location = new System.Drawing.Point(0, 0);
            this.lciLocation.Name = "lciLocation";
            this.lciLocation.Size = new System.Drawing.Size(352, 24);
            this.lciLocation.Text = "Байршил :";
            this.lciLocation.TextSize = new System.Drawing.Size(71, 13);
            // 
            // lciOrganizationName
            // 
            this.lciOrganizationName.Control = this.txtOrganizationName;
            this.lciOrganizationName.CustomizationFormText = "Байгууллага :";
            this.lciOrganizationName.Location = new System.Drawing.Point(0, 24);
            this.lciOrganizationName.Name = "lciOrganizationName";
            this.lciOrganizationName.Size = new System.Drawing.Size(352, 44);
            this.lciOrganizationName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 10);
            this.lciOrganizationName.Text = "Байгууллага :";
            this.lciOrganizationName.TextSize = new System.Drawing.Size(71, 13);
            // 
            // lciRegister
            // 
            this.lciRegister.Control = this.btnRegister;
            this.lciRegister.CustomizationFormText = "Бүртгэх";
            this.lciRegister.Location = new System.Drawing.Point(153, 68);
            this.lciRegister.Name = "lciRegister";
            this.lciRegister.Size = new System.Drawing.Size(199, 69);
            this.lciRegister.Text = "Бүртгэх";
            this.lciRegister.TextSize = new System.Drawing.Size(0, 0);
            this.lciRegister.TextToControlDistance = 0;
            this.lciRegister.TextVisible = false;
            // 
            // lciCancel
            // 
            this.lciCancel.Control = this.btnCancel;
            this.lciCancel.CustomizationFormText = "lciCancel";
            this.lciCancel.Location = new System.Drawing.Point(0, 68);
            this.lciCancel.Name = "lciCancel";
            this.lciCancel.Size = new System.Drawing.Size(153, 69);
            this.lciCancel.Text = "lciCancel";
            this.lciCancel.TextSize = new System.Drawing.Size(0, 0);
            this.lciCancel.TextToControlDistance = 0;
            this.lciCancel.TextVisible = false;
            // 
            // AddOrganizationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 157);
            this.Controls.Add(this.lcAddOrganization);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddOrganizationInfo";
            ((System.ComponentModel.ISupportInitialize)(this.lcAddOrganization)).EndInit();
            this.lcAddOrganization.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOrganizationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkUpLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAddOrganization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOrganizationName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRegister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcAddOrganization;
        private DevExpress.XtraLayout.LayoutControlGroup lcgAddOrganization;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnRegister;
        private DevExpress.XtraEditors.TextEdit txtOrganizationName;
        private DevExpress.XtraEditors.LookUpEdit lkUpLocation;
        private DevExpress.XtraLayout.LayoutControlItem lciLocation;
        private DevExpress.XtraLayout.LayoutControlItem lciOrganizationName;
        private DevExpress.XtraLayout.LayoutControlItem lciRegister;
        private DevExpress.XtraLayout.LayoutControlItem lciCancel;
    }
}
