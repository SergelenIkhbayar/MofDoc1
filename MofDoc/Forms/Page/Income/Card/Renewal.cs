using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MofDoc.Class;

namespace MofDoc.Forms.Page.Income.Card
{
    public partial class Renewal : DevExpress.XtraEditors.XtraForm
    {

        #region Properties

        private string dbName = null;
        private decimal pkId;
        private DateTime returnDate;
        private bool isActionProgress = false;

        #endregion

        #region Constructor

        public Renewal(string dbName, decimal pkId, DateTime returnDate)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.pkId = pkId;
            this.returnDate = returnDate;
            InitControl();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Сунгалт";
            this.MinimumSize = this.MaximumSize = this.Size;

            txtRenewal.Properties.Appearance.Font = new Font("Arial", 10, FontStyle.Bold);
            txtRenewal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtRenewal.Properties.Mask.EditMask = "[0-9]+";
            txtRenewal.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(txt_EditValueChanging);

            dateReturn.EditValue = returnDate;
            dateReturn.Properties.ReadOnly = true;

            btnRenewal.Click += new EventHandler(btnRenewal_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void CheckControl()
        {
            isActionProgress = false;
            if (string.IsNullOrEmpty(memoDesc.Text))
            {
                memoDesc.ErrorText = "Бичгийг сунгах болсон шалтгаанаа оруулна уу.";
                if (isActionProgress) return;
                memoDesc.Focus();
                isActionProgress = !isActionProgress;
            }
            if (string.IsNullOrEmpty(txtRenewal.Text))
            {
                txtRenewal.ErrorText = "Сунгах огноогоо оруулна уу.";
                if (isActionProgress) return;
                txtRenewal.Focus();
                isActionProgress = !isActionProgress;
            }
        }

        #endregion

        #region Event

        private void txt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewValue.ToString()))
                dateReturn.EditValue = returnDate;
            else
                dateReturn.EditValue = returnDate.AddDays(int.Parse(e.NewValue.ToString()));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnRenewal_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> parameters = null;

            Dictionary<string, object> filterStored = null;
            List<string> tableNames = null;
            DataSet ds = null;
            try
            {
                CheckControl();
                if (isActionProgress) return;

                Tool.ShowWaiting();
                parameters = new Dictionary<string, string>();
                parameters.Add("SHORTDESC", string.Format("SHORTDESC + N'; Сунгалтын шалтгаан: {0}/ Огноо: {1}({2})'", memoDesc.Text, Tool.ConvertNonTimeDateTime(dateReturn.EditValue), txtRenewal.Text));
                parameters.Add("RETURNDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateReturn.EditValue)));
                SqlConnector.UpdateByPkId(dbName, "Document", parameters, pkId);

                filterStored = new Dictionary<string, object>();
                filterStored.Add("@PKID", pkId);
                filterStored.Add("@IsFirst", "Y");
                tableNames = new List<string>() { "ChildrenDocument" };
                ds = SqlConnector.GetStoredProcedure(dbName, "GetChildrenIncome", filterStored, tableNames);
                if (ds.Tables["ChildrenDocument"] == null) { goto Finish; }
                if (ds.Tables["ChildrenDocument"].Rows == null || ds.Tables["ChildrenDocument"].Rows.Count.Equals(0)) { goto Finish; }

                foreach (DataRow dr in ds.Tables["ChildrenDocument"].Rows)
                    SqlConnector.UpdateByPkId(dbName, "Document", parameters, decimal.Parse(dr["PKID"].ToString()));

            Finish:
                Tool.ShowSuccess("Амжилттай сунгалт орууллаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Dispose();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Сунгалт хийхэд алдаа гарлаа: " + ex.InnerException.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Сунгалт хийхэд алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Сунгалт хийхэд алдаа гарлаа!", ex.Message);
            }
            finally { parameters = null; filterStored = null; tableNames = null; ds = null; Tool.CloseWaiting(); }
        }

        #endregion

    }
}