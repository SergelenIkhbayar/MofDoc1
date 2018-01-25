namespace MofDoc.Forms.Page.Income
{
    partial class DirectRegisterMode
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
            this.btnOld = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnOld
            // 
            this.btnOld.Location = new System.Drawing.Point(55, 37);
            this.btnOld.Name = "btnOld";
            this.btnOld.Size = new System.Drawing.Size(221, 44);
            this.btnOld.TabIndex = 0;
            this.btnOld.Text = "simpleButton1";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(55, 109);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(221, 44);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "simpleButton2";
            // 
            // DirectRegisterMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 203);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnOld);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DirectRegisterMode";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOld;
        private DevExpress.XtraEditors.SimpleButton btnNew;
    }
}
