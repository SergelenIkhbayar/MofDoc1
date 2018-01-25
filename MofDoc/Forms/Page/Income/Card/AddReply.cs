using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MofDoc.Class;
using MofDoc.Forms;
using DevExpress.XtraEditors.Controls;

namespace MofDoc.Forms.Page.Income.Card
{
    public partial class AddReply : XtraForm
    {

        #region Properties

        private string dbName = null;
        private bool isEditMode = false;
        private IncomeDoc incomeDoc = null;
        private Document document = null;
        private string replaceScannedFile = "http://cmc/docs/upload/";
        private bool isActionProgress = false;
        private bool isChanged = false;
        private decimal defaultDocNoteType = 34;
        private string emIndexNumber = null;
        private decimal emBranchId;
        private string regNum = "";

        #endregion

        #region Constructor

        public AddReply(string dbName, Document document)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.document = document;

            InitControl();
            InitData();
            isChanged = false;
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Хариу бүртгэх";
            this.MaximumSize = this.MinimumSize = this.Size;

            txtReplyNum.Properties.Appearance.Font = txtRegNum.Properties.Appearance.Font =  txtDocNum.Properties.Appearance.Font = txtControlNum.Properties.Appearance.Font = new Font("Arial", 10, FontStyle.Bold);
            txtReplyNum.Properties.ReadOnly = dateReturn.Properties.ReadOnly = txtRegNum.Properties.ReadOnly = txtDocNum.Properties.ReadOnly = txtControlNum.Properties.ReadOnly = true;
            dateReturn.TabStop = txtRegNum.TabStop = txtDocNum.TabStop = txtControlNum.TabStop = false;

            txtPageNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPageNum.Properties.Mask.EditMask = "[0-9]+";

            dateClosed.EditValue = DateTime.Now;

            btnCancel.Tag = 0;
            btnCancel.Click += new EventHandler(btn_Click);

            btnAddReply.Tag = 1;
            btnAddReply.Click += new EventHandler(btn_Click);

            lkUpReplyType.Properties.DisplayMember = "NAME";
            lkUpReplyType.Properties.ValueMember = "PKID";
            lkUpReplyType.Properties.PopulateColumns();
            lkUpReplyType.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Бичгийн төрөл"));

            lkUpClosedStaff.Properties.DisplayMember = "FNAME";
            lkUpClosedStaff.Properties.ValueMember = "ST_ID";
            lkUpClosedStaff.Properties.PopulateColumns();
            lkUpClosedStaff.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));

            lkUpBranch.Properties.DisplayMember = "NAME";
            lkUpBranch.Properties.ValueMember = "BR_ID";
            lkUpBranch.Properties.PopulateColumns();
            lkUpBranch.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Газар, алба"));
            lkUpBranch.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpBranch.Tag = 0;

            ckIsReturn.CheckedChanged += new EventHandler(ck_CheckedChanged);
            ckIsReturn.Tag = 0;
            ckIsReturn.Checked = true;

            ckIsManual.CheckedChanged += new EventHandler(ck_CheckedChanged);
            ckIsManual.Tag = 1;
            ckIsManual.Checked = false;
        }

        private void InitData()
        {
            Dictionary<string, string> filters = null;
            DataTable dataTable = null;

            Dictionary<string, object> filterStored = null;
            List<string> tableNames = null;
            DataSet ds = null;
            decimal toBrId;
            try
            {
                Tool.ShowWaiting();
                filters = new Dictionary<string, string>();
                filters.Add("PARENTPKID", string.Format("={0}", document.PkId.ToString()));
                filters.Add("CLOSEDDATE", "IS NOT NULL");
                filters.Add("ISDECISION", "='Y'");
                dataTable = SqlConnector.GetTable(dbName, "Document", filters);

                if (dataTable.Rows.Count.Equals(0))
                {
                    isEditMode = false;
                    filters.Clear();
                    filters.Add("PKID", string.Format("={0}", document.PkId));
                    dataTable = SqlConnector.GetTable(dbName, "Document", filters);
                    if (dataTable.Rows.Count.Equals(0)) return;
                }
                else
                {
                    isChanged = isEditMode = true;
                    dateClosed.EditValue = DateTime.Parse(dataTable.Rows[0]["CLOSEDDATE"].ToString());
                    memoDecision.EditValue = dataTable.Rows[0]["SHORTDESC"].ToString();
                    emIndexNumber = txtReplyNum.Text = dataTable.Rows[0]["REGNUM"].ToString();
                    txtPageNum.Text = dataTable.Rows[0]["PAGENUM"].ToString();
                }

                incomeDoc = new IncomeDoc(dataTable.Rows[0]["PKID"].ToString(), dataTable.Rows[0]["REGNUM"].ToString(), dataTable.Rows[0]["REGDATE"].ToString(),
                    dataTable.Rows[0]["DOCNUM"].ToString(), dataTable.Rows[0]["DOCDATE"].ToString(), dataTable.Rows[0]["CONTROLNUM"].ToString(), dataTable.Rows[0]["CONTROLDIRECTION"].ToString(),
                    dataTable.Rows[0]["STAFFID"].ToString(), dataTable.Rows[0]["TOSTAFFID"].ToString(), dataTable.Rows[0]["TOBRID"].ToString(), dataTable.Rows[0]["DOCNOTEPKID"].ToString(),
                    dataTable.Rows[0]["ORGANIZATIONTYPEPKID"].ToString(), dataTable.Rows[0]["INFROMWHO"].ToString(), dataTable.Rows[0]["ISREPLYDOC"].ToString(), dataTable.Rows[0]["SHORTDESC"].ToString(),
                    dataTable.Rows[0]["PAGENUM"].ToString(), dataTable.Rows[0]["CREATEDDATE"].ToString(), dataTable.Rows[0]["RETURNDATE"].ToString(), dataTable.Rows[0]["SCANNEDFILE"].ToString());

                txtRegNum.Text = document.RegNum;
                txtDocNum.Text = incomeDoc.DocNum;
                txtControlNum.Text = incomeDoc.ControlNum;
                dateReturn.EditValue = incomeDoc.ReturnDate;

                filters.Clear();
                filters.Add("STATUS", "='Y'");
                lkUpReplyType.Properties.DataSource = SqlConnector.GetTable(dbName, "DocNoteType", new List<String>() { "PKID", "NAME" }, filters);
                lkUpReplyType.EditValue = isEditMode ? incomeDoc.DocNotePkId : defaultDocNoteType;
                //lkUpBranch.Properties.DataSource = MainPage.branchInfo.Select("BR_ID NOT IN ('9908', '9905', '9907','9915', '9913')").CopyToDataTable();
                lkUpBranch.Properties.DataSource = MainPage.branchInfo;

                if(isEditMode)
                    ckIsReturn.Checked = !txtReplyNum.Text.Equals(document.RegNum);

                if (isEditMode)
                {
                    lkUpBranch.EditValue = incomeDoc.ToBrId;
                    emBranchId = (decimal)incomeDoc.ToBrId;
                    lkUpClosedStaff.EditValue = incomeDoc.ToStaffId;
                }
                else
                {
                    filterStored = new Dictionary<string, object>();
                    filterStored.Add("@PKID", document.PkId);
                    filterStored.Add("@IsFirst", "Y");
                    tableNames = new List<string>() { "ChildrenDocument" };
                    ds = SqlConnector.GetStoredProcedure(dbName, "GetChildrenIncome", filterStored, tableNames);
                    if (ds.Tables["ChildrenDocument"] == null)
                    {
                        if (incomeDoc.ToBrId.Equals(9908) || incomeDoc.ToBrId.Equals(9905) || incomeDoc.ToBrId.Equals(9907) || incomeDoc.ToBrId.Equals(9915) || incomeDoc.ToBrId.Equals(9913))
                        {
                            Tool.ShowInfo("Бичгээ хэлтсийн мэргэжилтэн рүү шилжүүлнэ үү.");
                            this.Dispose();
                            return;
                        }
                        lkUpBranch.EditValue = incomeDoc.ToBrId;
                        lkUpClosedStaff.EditValue = incomeDoc.ToStaffId;
                    }
                    else
                    {
                        toBrId = decimal.Parse(ds.Tables["ChildrenDocument"].Rows[ds.Tables["ChildrenDocument"].Rows.Count - 1]["TOBRID"].ToString());
                        if (toBrId.Equals(9908) || toBrId.Equals(9905) || toBrId.Equals(9907) || toBrId.Equals(9915) || toBrId.Equals(9913))
                        {
                            Tool.ShowInfo("Бичгээ хэлтсийн мэргэжилтэн рүү шилжүүлнэ үү.");
                            this.Dispose();
                            return;
                        }
                        lkUpBranch.EditValue = toBrId;
                        lkUpClosedStaff.EditValue = decimal.Parse(ds.Tables["ChildrenDocument"].Rows[ds.Tables["ChildrenDocument"].Rows.Count - 1]["TOSTAFFID"].ToString());
                    }
                }
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
            finally
            {
                filters = null; dataTable = null; filterStored = null;
                tableNames = null; ds = null; Tool.CloseWaiting();
            }
        }

        private void CheckControl()
        {
            try
            {
                isActionProgress = false;
                if(lkUpBranch.EditValue == null)
                {
                    lkUpBranch.ErrorText = "Хариу бичиг хариуцах газар, албыг тодорхойлно уу.";
                    if (!isActionProgress)
                    {
                        lkUpBranch.Focus();
                        isActionProgress = !isActionProgress;
                    }
                }
                if(lkUpReplyType.EditValue == null)
                {
                    lkUpReplyType.ErrorText = "Хариу бичгийн бичгийн төрлийн тодорхойлно уу.";
                    if (!isActionProgress)
                    {
                        lkUpReplyType.Focus();
                        isActionProgress = !isActionProgress;
                    }
                }
                if (string.IsNullOrEmpty(txtReplyNum.Text.Trim()) || !txtReplyNum.Text.Contains("/"))
                {
                    txtReplyNum.ErrorText = "Хариу бичгийн дугаарыг оруулахын тулд газар, хэлтсийг сонгоно уу.";
                    if (!isActionProgress)
                    {
                        txtReplyNum.Focus();
                        isActionProgress = !isActionProgress;
                    }
                }
                if (dateClosed.EditValue == null)
                {
                    dateClosed.ErrorText = "Хаасан огноог оруулна уу.";
                    if (!isActionProgress)
                    {
                        dateClosed.Focus();
                        isActionProgress = !isActionProgress;
                    }
                }
                if (lkUpClosedStaff.EditValue == null)
                {
                    lkUpClosedStaff.ErrorText = "Хаасан ажилтанг сонгоно уу.";
                    if (!isActionProgress)
                    {
                        lkUpClosedStaff.Focus();
                        isActionProgress = !isActionProgress;
                    }
                }
                if (string.IsNullOrEmpty(txtPageNum.Text.Trim()))
                {
                    txtPageNum.ErrorText = "Хариу бичгийн хуудасны тоог оруулна уу.";
                    if (!isActionProgress)
                    {
                        txtPageNum.Focus();
                        isActionProgress = !isActionProgress;
                    }
                }
                if (string.IsNullOrEmpty(memoDecision.Text.Trim()))
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
                System.Diagnostics.Debug.WriteLine("Шийдвэрийн мэдээллийг баталгаажуулахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шийдвэрийн мэдээллийг баталгаажуулахад гарлаа: " + ex.Message);
                throw new MofException("Шийдвэрийн мэдээллийг баталгаажуулахад гарлаа!", ex);
            }
        }

        private void RegisterReply()
        {
            Dictionary<string, string> parameters = null;
            Dictionary<string, string> parameterStored = null;
            try
            {
                CheckControl();
                if (isActionProgress) return;

                Tool.ShowWaiting();
                if (ckIsReturn.Checked)
                {
                    parameters = new Dictionary<string, string>();
                    parameters.Add("ISDECISION", "'Y'");
                    parameters.Add("STAFFID", Tool.userStaffId.ToString());
                    parameters.Add("TOSTAFFID", lkUpClosedStaff.EditValue.ToString());
                    parameters.Add("TOBRID", lkUpBranch.EditValue.ToString());
                    parameters.Add("DOCNOTEPKID", lkUpReplyType.EditValue.ToString());
                    parameters.Add("CLOSEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateClosed.EditValue)));
                    parameters.Add("RETURNDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateReturn.EditValue)));
                    parameters.Add("CREATEDDATE", string.Format("'{0}'", Tool.DateTimeNow()));
                    parameters.Add("SHORTDESC", string.Format("N'{0}'", memoDecision.EditValue));
                    parameters.Add("PAGENUM", txtPageNum.Text);

                    parameterStored = new Dictionary<string, string>();
                    parameterStored.Add("LASTNUMBER", txtReplyNum.Text.Substring(txtReplyNum.Text.IndexOf("/")).Replace("/", ""));

                    if (isEditMode)
                    {
                        SqlConnector.UpdateByPkId(dbName, "Document", parameters, incomeDoc.PkId);
                        if (!emIndexNumber.Equals(txtReplyNum.Text))
                            SqlConnector.Update(dbName, "OutcomeIndex", parameterStored, null);
                    }
                    else
                    {
                        parameters.Add("REGNUM", string.Format("N'{0}'", txtReplyNum.Text));
                        parameters.Add("REGDATE", string.Format("N'{0}'", Tool.ConvertDateTime(incomeDoc.RegDate)));
                        parameters.Add("DOCNUM", string.Format("N'{0}'", txtDocNum.Text));
                        parameters.Add("DOCDATE", string.Format("N'{0}'", Tool.ConvertDateTime(document.DocDate)));

                        parameters.Add("CONTROLNUM", string.Format("N'{0}'", incomeDoc.ControlNum));
                        if (!string.IsNullOrEmpty(incomeDoc.ControlDirection))
                            parameters.Add("CONTROLDIRECTION", string.Format("N'{0}'", incomeDoc.ControlDirection));

                        parameters.Add("ORGANIZATIONTYPEPKID", incomeDoc.OrganizationTypePkId.ToString());
                        parameters.Add("INFROMWHO", string.Format("N'{0}'", incomeDoc.InFromWho));
                        parameters.Add("DOCUMENTTYPE", "'O'");
                        parameters.Add("ISREPLYDOC", "'Y'");
                        parameters.Add("STATUS", "'Y'");
                        parameters.Add("PARENTPKID", document.PkId.ToString());
                        if (!string.IsNullOrEmpty(incomeDoc.ScannedFile))
                            parameters.Add("SCANNEDFILE", string.Format("N'{0}'", incomeDoc.ScannedFile.Replace(replaceScannedFile, "")));

                        parameters.Add("PKID", Tool.NewPkId().ToString());
                        SqlConnector.Insert(dbName, "Document", parameters);
                        SqlConnector.Update(dbName, "OutcomeIndex", parameterStored, null);
                    }
                }
                else
                {
                    parameters = new Dictionary<string, string>();
                    parameters.Add("ISDECISION", "'Y'");

                    if (isEditMode)
                    {
                        parameters.Add("STAFFID", Tool.userStaffId.ToString());
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

                        parameters.Add("STAFFID", Tool.userStaffId.ToString());
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
                        parameters.Add("RETURNDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateReturn.EditValue)));
                        parameters.Add("STATUS", "'N'");
                        parameters.Add("PARENTPKID", document.PkId.ToString());
                        if (!string.IsNullOrEmpty(incomeDoc.ScannedFile))
                            parameters.Add("SCANNEDFILE", string.Format("N'{0}'", incomeDoc.ScannedFile.Replace(replaceScannedFile, "")));

                        parameters.Add("PKID", Tool.NewPkId().ToString());
                        SqlConnector.Insert(dbName, "Document", parameters);
                    }
                }
                RecursiveUpdate();
                Tool.ShowSuccess("Амжилттай шийдвэрийг хадгаллаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Dispose();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хариутай бичгийн хариуг бүртгэхэд алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хариутай бичгийн хариуг бүртгэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Хариутай бичгийн хариуг бүртгэхэд алдаа гарлаа", ex);
            }
            finally { parameters = null; parameterStored = null; Tool.CloseWaiting(); }
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
                SqlConnector.UpdateByPkId(dbName, "Document", parameter, document.PkId);

                filterStored = new Dictionary<string, object>();
                filterStored.Add("@PKID", document.PkId);
                filterStored.Add("@IsFirst", "Y");
                tableNames = new List<string>() { "ChildrenDocument" };
                ds = SqlConnector.GetStoredProcedure(dbName, "GetChildrenIncome", filterStored, tableNames);
                if (ds.Tables["ChildrenDocument"] == null) return;
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
            try
            {
                if ((sender as SimpleButton).Tag.Equals(0)) this.Dispose();
                else RegisterReply();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хариутай бичгийн товч дарахад алдаа гарлаа: " + ex.InnerException.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хариутай бичгийн товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Хариутай бичгийн товч дарахад алдаа гарлаа", ex.Message);
            }
        }

        private void lkUp_EditValueChanged(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = null;
            string indexNumber = null;
            try
            {
                if ((sender as LookUpEdit).Tag == null || lkUpBranch.EditValue == null) return;
                parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@BRID", lkUpBranch.EditValue));
                indexNumber = SqlConnector.GetFunction(dbName, "GetOutcomeNumberByIndex", parameters).ToString();

                if (MainPage.allUser.Select(string.Format("BR_ID = {0}", lkUpBranch.EditValue)).Length.Equals(0))
                {
                    lkUpClosedStaff.Properties.DataSource = null;
                    lkUpClosedStaff.EditValue = null;
                }
                else lkUpClosedStaff.Properties.DataSource = MainPage.allUser.Select(string.Format("BR_ID = {0}", lkUpBranch.EditValue)).CopyToDataTable();

                if (string.IsNullOrEmpty(indexNumber))
                    throw new MofException(string.Format("{0} газар/хэлтэсийн явсан бичгийн дугаарыг гаргахад(Generate хийхэд) алдаа гарлаа!"));
                if (isChanged) return;
                if (lkUpBranch.EditValue.ToString().Equals(emBranchId.ToString())) txtReplyNum.Text = emIndexNumber;
                else txtReplyNum.Text = indexNumber;
                regNum = txtReplyNum.Text;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("LookupEdit контролыг сонгоход алдаа гарлаа: " + ex.InnerException.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LookupEdit контролыг сонгоход алдаа гарлаа: " + ex.Message);
                Tool.ShowError("LookupEdit контролыг сонгоход алдаа гарлаа", ex.Message);
            }
            finally { indexNumber = null; parameters = null; }
        }

        private void ck_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckEdit).Tag.Equals(0))
            {
                if (ckIsReturn.Checked)
                {
                    ckIsManual.Enabled = lcgDoc.Enabled = true;
                    txtReplyNum.Text = regNum;
                }
                else
                {
                    ckIsManual.Checked = ckIsManual.Enabled = lcgDoc.Enabled = false;
                    regNum = txtReplyNum.Text;
                    txtReplyNum.Text = incomeDoc.RegNum;
                }
            }
            else txtReplyNum.Properties.ReadOnly = !ckIsManual.Checked;
        }

        #endregion

    }
}