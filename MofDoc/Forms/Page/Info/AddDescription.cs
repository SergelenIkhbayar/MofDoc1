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

namespace MofDoc.Forms.Page.Info
{
    public partial class AddDescription : XtraForm
    {

        #region Properties

        private string dbName = null;
        private decimal pkId;
        private string desc = string.Empty;
        private string renewalDesc = string.Empty;

        #endregion

        #region Constructor

        public AddDescription(string dbName, decimal pkId, string desc)
        {
            InitializeComponent();

            this.dbName = dbName;
            this.pkId = pkId;
            this.desc = Tool.GetStringWithoutRenew(desc);
            this.renewalDesc = Tool.GetRenewalString(desc);
            InitControl();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Тайлбар оруулах";
            this.MaximumSize = this.MinimumSize = this.Size;
            memoDesc.Text = desc;
        }

        #endregion

        #region Event

        private void bnCancel_Click(object sender, EventArgs e) { this.Dispose(); }

        private void btnAddDesc_Click(object sender, EventArgs e)
        {
            string desc = null;
            Dictionary<string, string> parameter = null;

            Dictionary<string, object> filterStored = null;
            List<string> tableNames = null;
            DataSet ds = null;
            try
            {
                desc = memoDesc.Text;
                if (string.IsNullOrEmpty(desc) || string.IsNullOrEmpty(desc.Trim()))
                {
                    memoDesc.ErrorText = "Тайлбар хоосон байгаа тул оруулна уу.";
                    memoDesc.Focus();
                    return;
                }

                parameter = new Dictionary<string, string>();
                parameter.Add("SHORTDESC", string.Format("N'{0}'", desc+renewalDesc));
                SqlConnector.UpdateByPkId(dbName, "Document", parameter, pkId);

                filterStored = new Dictionary<string, object>();
                filterStored.Add("@PKID", pkId);
                filterStored.Add("@IsFirst", "Y");
                tableNames = new List<string>() { "ChildrenDocument" };
                ds = SqlConnector.GetStoredProcedure(dbName, "GetChildrenIncome", filterStored, tableNames);
                if (ds.Tables["ChildrenDocument"] == null) { goto Finish; }
                if (ds.Tables["ChildrenDocument"].Rows == null || ds.Tables["ChildrenDocument"].Rows.Count.Equals(0)) { goto Finish; }

                foreach (DataRow dr in ds.Tables["ChildrenDocument"].Rows)
                    SqlConnector.UpdateByPkId(dbName, "Document", parameter, decimal.Parse(dr["PKID"].ToString()));

            Finish:
                Tool.ShowSuccess("Амжилттай тайлбар орууллаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Dispose();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлбар оруулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлбар оруулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Тайлбар оруулахад алдаа гарлаа!", ex.InnerException.Message);
            }
            finally
            {
                desc = null; parameter = null; filterStored = null;
                tableNames = null; ds = null;
            }
        }

        #endregion

    }
}