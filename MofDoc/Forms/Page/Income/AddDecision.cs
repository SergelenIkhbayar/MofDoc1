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
using DevExpress.XtraEditors.Controls;

namespace MofDoc.Forms.Page.Income
{
    public partial class AddDecision : DevExpress.XtraEditors.XtraForm
    {

        #region Properties

        private string dbName = null;
        private decimal pkId;
        private bool isEditMode = false;
        private IncomeDoc incomeDoc = null;
        private string replaceScannedFile = "http://cmc/docs/upload/";
        private bool isActionProgress = false;

        #endregion

        #region Constructor

        public AddDecision(string dbName, decimal pkId)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.pkId = pkId;

            InitControl();
            InitData();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Шийдвэр оруулах";
            this.MaximumSize = this.MinimumSize = this.Size;

            txtDocNum.Properties.Appearance.Font = txtRegNum.Properties.Appearance.Font = new Font("Arial", 10, FontStyle.Bold);
            txtDocNum.Properties.ReadOnly = txtRegNum.Properties.ReadOnly = true;
            txtDocNum.TabStop = txtRegNum.TabStop = false;

            dateClosed.EditValue = DateTime.Now;

            btnCancel.Tag = 0;
            btnCancel.Click += new EventHandler(btn_Click);

            btnRegister.Tag = 1;
            btnRegister.Click += new EventHandler(btn_Click);

            lkUpStaff.Properties.DisplayMember = "FNAME";
            lkUpStaff.Properties.ValueMember = "ST_ID";
            lkUpStaff.Properties.PopulateColumns();
            lkUpStaff.Properties.Columns.Add(new LookUpColumnInfo("LNAME", "Овог"));
            lkUpStaff.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));
            lkUpStaff.Properties.Columns.Add(new LookUpColumnInfo("SHORTNAME", "Газар, алба"));
        }

        private void InitData()
        {
            Dictionary<string, string> filters = null;
            DataTable dataTable = null;
            string condition = null;
            try
            {
                Tool.ShowWaiting();
                condition = "ORDER BY PKID DESC";
                filters = new Dictionary<string, string>();
                filters.Add("PARENTPKID", string.Format("={0}", pkId.ToString()));
                filters.Add("CLOSEDDATE", "IS NOT NULL");
                filters.Add("ISDECISION", "='Y'");
                dataTable = SqlConnector.GetTable(dbName, "Document", filters, condition);

                if (dataTable.Rows.Count.Equals(0))
                {
                    isEditMode = false;
                    filters.Clear();
                    filters.Add("PKID", string.Format("={0}", pkId));
                    dataTable = SqlConnector.GetTable(dbName, "Document", filters);
                    if (dataTable.Rows.Count.Equals(0)) return;
                }
                else
                {
                    isEditMode = true;
                    dateClosed.EditValue = DateTime.Parse(dataTable.Rows[0]["CLOSEDDATE"].ToString());
                    memoDecision.EditValue = dataTable.Rows[0]["SHORTDESC"].ToString();
                }

                incomeDoc = new IncomeDoc(dataTable.Rows[0]["PKID"].ToString(), dataTable.Rows[0]["REGNUM"].ToString(), dataTable.Rows[0]["REGDATE"].ToString(), 
                    dataTable.Rows[0]["DOCNUM"].ToString(), dataTable.Rows[0]["DOCDATE"].ToString(), dataTable.Rows[0]["CONTROLDIRECTION"].ToString(), dataTable.Rows[0]["STAFFID"].ToString(), 
                    dataTable.Rows[0]["TOSTAFFID"].ToString(), dataTable.Rows[0]["TOBRID"].ToString(), dataTable.Rows[0]["DOCNOTEPKID"].ToString(), dataTable.Rows[0]["ORGANIZATIONTYPEPKID"].ToString(), 
                    dataTable.Rows[0]["INFROMWHO"].ToString(), dataTable.Rows[0]["ISREPLYDOC"].ToString(), dataTable.Rows[0]["SHORTDESC"].ToString(), dataTable.Rows[0]["PAGENUM"].ToString(), 
                    dataTable.Rows[0]["CREATEDDATE"].ToString());

                txtRegNum.Text = incomeDoc.RegNum;
                txtDocNum.Text = incomeDoc.DocNum;
                lkUpStaff.Properties.DataSource = MainPage.allUser;
                lkUpStaff.EditValue = isEditMode ? incomeDoc.StaffId : Tool.userStaffId;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шийдвэрийн мэдээллийг авчрахад алдаа гарлаа: " + ex.InnerException.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шийдвэрийн мэдээллийг авчрахад гарлаа: " + ex.Message);
                Tool.ShowError("Шийдвэрийн мэдээллийг авчрахад гарлаа!", ex.Message);
            }
            finally { filters = null; dataTable = null; Tool.CloseWaiting(); }
        }

        private void CheckControl()
        {
            try
            {
                isActionProgress = false;
                if (dateClosed.EditValue == null)
                {
                    dateClosed.ErrorText = "Хаасан огноог оруулна уу.";
                    if (!isActionProgress)
                    {
                        dateClosed.Focus();
                        isActionProgress = !isActionProgress;
                    }
                }
                if(string.IsNullOrEmpty(memoDecision.Text.Trim()))
                {
                    memoDecision.ErrorText = "Хаасан тайлбараа оруулна уу.";
                    if (!isActionProgress)
                    {
                        memoDecision.Focus();
                        isActionProgress = !isActionProgress;
                    }
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шийдвэрийн мэдээллийг авчрахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шийдвэрийн мэдээллийг авчрахад гарлаа: " + ex.Message);
                throw new MofException("Шийдвэрийн мэдээллийг авчрахад гарлаа!", ex);
            }
        }

        private void DecisionRegister()
        {
            Dictionary<string,string> parameters = null;
            try
            {
                CheckControl();
                if (isActionProgress) return;

                Tool.ShowWaiting();
                parameters = new Dictionary<string, string>();
                parameters.Add("ISDECISION", "'Y'");
                if (isEditMode)
                {
                    parameters.Add("STAFFID", lkUpStaff.EditValue.ToString());
                    parameters.Add("CLOSEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateClosed.EditValue)));
                    parameters.Add("SHORTDESC", string.Format("N'{0}'", memoDecision.EditValue));
                    SqlConnector.UpdateByPkId(dbName, "Document", parameters, incomeDoc.PkId);
                }
                else
                {
                    parameters.Add("REGNUM", incomeDoc.RegNum);
                    parameters.Add("REGDATE", string.Format("N'{0}'", Tool.ConvertDateTime(incomeDoc.RegDate)));
                    parameters.Add("DOCNUM", string.Format("N'{0}'", incomeDoc.DocNum));
                    parameters.Add("DOCDATE", string.Format("N'{0}'", Tool.ConvertDateTime(incomeDoc.DocDate)));

                    if (!string.IsNullOrEmpty(incomeDoc.ControlDirection))
                        parameters.Add("CONTROLDIRECTION", string.Format("N'{0}'", incomeDoc.ControlDirection));

                    parameters.Add("STAFFID", lkUpStaff.EditValue.ToString());
                    parameters.Add("TOSTAFFID", incomeDoc.ToStaffId.ToString());
                    parameters.Add("TOBRID", incomeDoc.ToBrId.ToString());
                    parameters.Add("DOCNOTEPKID", incomeDoc.DocNotePkId.ToString());
                    parameters.Add("ORGANIZATIONTYPEPKID", incomeDoc.OrganizationTypePkId.ToString());
                    parameters.Add("INFROMWHO", string.Format("N'{0}'", incomeDoc.InFromWho));
                    parameters.Add("ISREPLYDOC", "'N'");
                    parameters.Add("SHORTDESC", string.Format("N'{0}'", memoDecision.EditValue));
                    parameters.Add("PAGENUM", incomeDoc.PageNum.ToString());
                    parameters.Add("CREATEDDATE", string.Format("'{0}'", Tool.DateTimeNow()));
                    parameters.Add("CLOSEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateClosed.EditValue)));
                    parameters.Add("STATUS", "'N'");
                    parameters.Add("PARENTPKID", pkId.ToString());
                    if (!string.IsNullOrEmpty(incomeDoc.ScannedFile))
                        parameters.Add("SCANNEDFILE", string.Format("N'{0}'", incomeDoc.ScannedFile.Replace(replaceScannedFile, "")));

                    parameters.Add("PKID", Tool.NewPkId().ToString());
                    SqlConnector.Insert(dbName, "Document", parameters);
                }
                RecursiveUpdate();
                Tool.ShowSuccess("Амжилттай шийдвэрийг хадгаллаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Dispose();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шийдвэрийн мэдээллийг хадгалахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шийдвэрийн мэдээллийг хадгалахад гарлаа: " + ex.Message);
                throw new MofException("Шийдвэрийн мэдээллийг хадгалахад гарлаа!", ex);
            }
            finally { parameters = null; Tool.CloseWaiting(); }
        }

        private void RecursiveUpdate()
        {
            Dictionary<string, string> parameter = null;
            Dictionary<string, object> filterStored = null;
            List<string> tableNames = null;
            DataSet ds = null;
            try
            {
                parameter = new Dictionary<string, string>();
                parameter.Add("STATUS", "'N'");
                parameter.Add("CLOSEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateClosed.EditValue)));
                SqlConnector.UpdateByPkId(dbName, "Document", parameter, this.pkId);

                filterStored = new Dictionary<string, object>();
                filterStored.Add("@PKID", this.pkId);
                filterStored.Add("@IsFirst", "Y");
                tableNames = new List<string>() { "ChildrenDocument" };
                ds = SqlConnector.GetStoredProcedure(dbName, "GetChildrenIncome", filterStored, tableNames);
                if (ds.Tables["ChildrenDocument"] == null) return;
                if (ds.Tables["ChildrenDocument"].Rows == null || ds.Tables["ChildrenDocument"].Rows.Count.Equals(0)) return;
                foreach (DataRow dr in ds.Tables["ChildrenDocument"].Rows)
                    SqlConnector.UpdateByPkId(dbName, "Document", parameter, decimal.Parse((dr["PKID"].ToString())));
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Бичгийн төлөвт өөрчлөлт оруулахад алдаа гарлаа: " + ex.InnerException.Message);
                throw new MofException("Бичгийн төлөвт өөрчлөлт оруулахад алдаа гарлаа!", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Бичгийн төлөвт өөрчлөлт оруулахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Бичгийн төлөвт өөрчлөлт оруулахад алдаа гарлаа!", ex);
            }
            finally { parameter = null; filterStored = null; tableNames = null; ds = null; }
        }

        #endregion

        #region Event

        private void btn_Click(object sender, EventArgs e)
        {
            if ((sender as SimpleButton).Tag == null) return;
            int tagId = int.Parse((sender as SimpleButton).Tag.ToString());
            try
            {
                if (tagId.Equals(0)) this.Dispose();
                else DecisionRegister();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Товч дарахад алдаа гарлаа: " + ex.InnerException.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Товч дарахад алдаа гарлаа!", ex.Message);
            }
        }

        #endregion

    }
}