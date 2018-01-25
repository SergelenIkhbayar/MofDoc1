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
    public partial class InfoMessage : Form
    {

        #region Constructor

        public InfoMessage()
        {
            InitializeComponent();
            InitControl();
        }

        public InfoMessage(string desc)
        {
            InitializeComponent();
            InitControl(desc);
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            this.Text = "Бичиг хэргийн програм. Хувилбар 2.0";
            this.Icon = Properties.Resources.info;

            txtDesc.Properties.ReadOnly = true;
            btnClose.Focus();
            btnClose.Click += new EventHandler(btnClose_Click);
        }

        private void InitControl(string desc)
        {
            this.MaximumSize = this.MinimumSize = this.Size;
            this.Text = "Бичиг хэргийн програм. Хувилбар 2.0";
            this.Icon = Properties.Resources.info;

            txtDesc.Properties.ReadOnly = true;
            txtDesc.Text = desc;
            btnClose.Focus();
            btnClose.Click += new EventHandler(btnClose_Click);
        }

        #endregion

        #region Event

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

    }
}