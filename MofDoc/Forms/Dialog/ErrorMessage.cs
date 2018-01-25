using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MofDoc.Forms.Dialog
{
    public partial class ErrorMessage : Form
    {

        #region Constructor Function

        public ErrorMessage(string shortDesc, string longDesc)
        {
            InitializeComponent();
            InitControl(shortDesc, longDesc);
        }

        #endregion

        #region Function

        private void InitControl(string shortDesc, string longDesc)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            this.Text = "Бичиг хэргийн програм. Хувилбар 2.0";
            this.Icon = Properties.Resources.info;
            txtLongDesc.Properties.ReadOnly = txtShortDesc.Properties.ReadOnly = true;

            btnClose.Tag = 0;
            btnClose.Click += new EventHandler(btn_Click);

            txtShortDesc.Text = shortDesc;
            txtLongDesc.Text = longDesc;
        }

        #endregion

        #region Event

        private void btn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

    }
}