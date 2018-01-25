namespace MofDoc.Forms.Dialog
{
    partial class ErrorMessage
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
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtLongDesc = new DevExpress.XtraEditors.MemoEdit();
            this.txtShortDesc = new DevExpress.XtraEditors.MemoEdit();
            this.lblLongDesc = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.linkContact = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtLongDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnClose.Location = new System.Drawing.Point(244, 308);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(339, 32);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Хаах";
            // 
            // txtLongDesc
            // 
            this.txtLongDesc.Location = new System.Drawing.Point(244, 205);
            this.txtLongDesc.Name = "txtLongDesc";
            this.txtLongDesc.Size = new System.Drawing.Size(339, 97);
            this.txtLongDesc.TabIndex = 2;
            // 
            // txtShortDesc
            // 
            this.txtShortDesc.Location = new System.Drawing.Point(338, 66);
            this.txtShortDesc.Name = "txtShortDesc";
            this.txtShortDesc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.txtShortDesc.Properties.Appearance.Options.UseFont = true;
            this.txtShortDesc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.txtShortDesc.Size = new System.Drawing.Size(245, 94);
            this.txtShortDesc.TabIndex = 3;
            // 
            // lblLongDesc
            // 
            this.lblLongDesc.Location = new System.Drawing.Point(244, 186);
            this.lblLongDesc.Name = "lblLongDesc";
            this.lblLongDesc.Size = new System.Drawing.Size(162, 13);
            this.lblLongDesc.TabIndex = 5;
            this.lblLongDesc.Text = "Алдааны дэлгэрэнгүй тайлбар :";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::MofDoc.Properties.Resources.Government_logo_Mon;
            this.pictureEdit1.Location = new System.Drawing.Point(23, 68);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(154, 150);
            this.pictureEdit1.TabIndex = 6;
            // 
            // linkContact
            // 
            this.linkContact.AutoSize = true;
            this.linkContact.BackColor = System.Drawing.Color.Transparent;
            this.linkContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkContact.Location = new System.Drawing.Point(37, 285);
            this.linkContact.Name = "linkContact";
            this.linkContact.Size = new System.Drawing.Size(124, 17);
            this.linkContact.TabIndex = 7;
            this.linkContact.TabStop = true;
            this.linkContact.Text = "Алдаа  мэдээллэх";
            // 
            // ErrorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::MofDoc.Properties.Resources.ErrorTemplate;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(595, 352);
            this.Controls.Add(this.linkContact);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.lblLongDesc);
            this.Controls.Add(this.txtShortDesc);
            this.Controls.Add(this.txtLongDesc);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ErrorMessage";
            this.Text = "ErrorMessage";
            ((System.ComponentModel.ISupportInitialize)(this.txtLongDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.MemoEdit txtLongDesc;
        private DevExpress.XtraEditors.MemoEdit txtShortDesc;
        private DevExpress.XtraEditors.LabelControl lblLongDesc;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.LinkLabel linkContact;
    }
}