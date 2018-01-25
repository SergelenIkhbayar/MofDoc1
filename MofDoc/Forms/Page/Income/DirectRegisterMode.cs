using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MofDoc.Forms.Page.Income
{
    public partial class DirectRegisterMode : XtraForm
    {

        #region Properties

        internal bool? isWizard = null;

        #endregion

        #region Constructor

        public DirectRegisterMode()
        {
            InitializeComponent();
            InitControl();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Бичиг бүртгэх загвар сонгох";
            this.MaximumSize = this.MinimumSize = this.Size;
            this.Tag = "DirectRegisterMode";

            btnOld.Tag = 0;
            btnOld.Text = "ХУУЧИН ХЭВ ЗАГВАРААР БҮРТГЭХ";
            btnOld.Click += new EventHandler(btn_Click);

            btnNew.Tag = 1;
            btnNew.Text = "ШИНЭ ХЭВ ЗАГВАРААР БҮРТГЭХ";
            btnNew.Click += new EventHandler(btn_Click);
        }

        #endregion

        #region Event

        private void btn_Click(object sender, EventArgs e)
        {
            var btn = sender as SimpleButton;
            isWizard = btn.Tag.Equals(0) ? false : true;
            this.Close();
        }

        #endregion

    }
}