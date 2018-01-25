using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using MofDoc.Class;
using MofDoc.Forms.Page.Info;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Collections.ObjectModel;

namespace MofDoc.Forms.Page.Income
{
    public partial class DirectRegister : XtraForm
    {

        #region Properties

        private string dbName = null;
        private DataTable locationDt = null;
        private DataTable organizationDt = null;
        private decimal defaultDocNoteType = 34;
        bool isActionProgress = false;
        private string scannedFileName = null;

        private Document document = null;
        private bool isEditMode = false;
        private string replaceScannedFile = "http://cmc/docs/upload/";
        private List<string> childPkIds = null;
        private bool isOnly = false;

        #endregion

        #region Constructor

        public DirectRegister(string dbName)
        {
            InitializeComponent();
            this.dbName = dbName;
            InitControl();
            InitData();
        }

        public DirectRegister(string dbName, Document document)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.document = document;
            isEditMode = true;
            InitControl();
            InitData();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Шууд бүртгэл(Хуучин)";
            this.MaximumSize = this.MinimumSize = this.Size;

            txtRegisterNum.Properties.ReadOnly = txtRegisteredBy.Properties.ReadOnly = true;
            txtRegisterNum.Properties.Appearance.Font = txtRegisteredBy.Properties.Appearance.Font = new Font("Arial", 10, FontStyle.Bold);
            txtPageNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPageNum.Properties.Mask.EditMask = "[0-9]+";

            dateRegistered.DateTime = dateDoc.DateTime = dateMovement.DateTime = DateTime.Now;

            lkUpLocation.Properties.DisplayMember = "NAME";
            lkUpLocation.Properties.ValueMember = "PKID";
            lkUpLocation.Properties.PopulateColumns();
            lkUpLocation.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байршлын нэр"));

            lkUpOrganization.Properties.DisplayMember = "NAME";
            lkUpOrganization.Properties.ValueMember = "PKID";
            lkUpOrganization.Properties.PopulateColumns();
            lkUpOrganization.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байгууллагын нэр"));
            lkUpOrganization.Properties.TextEditStyle = TextEditStyles.Standard;

            lkUpDocNoteType.Properties.DisplayMember = "NAME";
            lkUpDocNoteType.Properties.ValueMember = "PKID";
            lkUpDocNoteType.Properties.PopulateColumns();
            lkUpDocNoteType.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Бичгийн төрөл"));

            lkUpToStaff.Properties.DisplayMember = "FNAME";
            lkUpToStaff.Properties.ValueMember = "ST_ID";
            lkUpToStaff.Properties.PopulateColumns();
            lkUpToStaff.Properties.Columns.Add(new LookUpColumnInfo("LNAME", "Овог"));
            lkUpToStaff.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));
            lkUpToStaff.Properties.Columns.Add(new LookUpColumnInfo("SHORTNAME", "Газар, алба"));

            lkUpMoveBranch.Properties.DisplayMember = "NAME";
            lkUpMoveBranch.Properties.ValueMember = "BR_ID";
            lkUpMoveBranch.Properties.PopulateColumns();
            lkUpMoveBranch.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Газар, алба"));
            lkUpMoveBranch.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpMoveBranch.Tag = 1;

            lkUpMoveStaff.Properties.DisplayMember = "FNAME";
            lkUpMoveStaff.Properties.ValueMember = "ST_ID";
            lkUpMoveStaff.Properties.PopulateColumns();
            lkUpMoveStaff.Properties.Columns.Add(new LookUpColumnInfo("LNAME", "Овог"));
            lkUpMoveStaff.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));
            lkUpMoveStaff.Click += new EventHandler(lkUpMoveStaff_Click);

            lciAddOrganization.Control.Enabled = lciOrganization.Control.Enabled = false;

            lkUpLocation.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpLocation.Tag = 0;

            btnAddOrganization.Click += new EventHandler(btn_Click);
            btnAddOrganization.Tag = 0;

            txtFromWho.Enabled = false;
            lcgMovementGroup.Enabled = false;
            ckIsMovement.Checked = false;
            ckIsMovement.CheckedChanged += new EventHandler(ck_CheckedChanged);
            ckIsMovement.Tag = 0;

            txtDocNum.Tag = "Бичгийн дугаар бөглөнө үү.";
            txtPageNum.Tag = "Бичгийн хуудасны тоог оруулна уу.";
            txtFromWho.Tag = "Бичиг ирсэн хүний нэрийг оруулна уу.";

            lkUpToStaff.Tag = "Хэнд ирсэн хүнийг тодорхойлно уу.";
            memoDesc.Tag = "Товч утга оруулна уу.";

            lkUpMoveStaff.Tag = "Ажилтанг тодорхойлно уу.";
            dateMovement.Tag = "Шилжүүлсэн огноог тодорхойлно уу.";

            barBtnClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnClear.Tag = 0;

            barBtnRegister.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnRegister.Tag = 1;

            barBtnPrintSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnPrintSave.Tag = 3;

            barBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnDelete.Tag = 4;

            barBtnPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnPdf.Tag = 5;

            barBtnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnCancel.Tag = 6;

            dateRegistered.Validating += new CancelEventHandler(date_Validating);
            dateRegistered.Tag = 0;

            dateDoc.Validating += new CancelEventHandler(date_Validating);
            dateDoc.Tag = 1;

            dateMovement.Validating += new CancelEventHandler(date_Validating);
            dateMovement.Tag = 2;

            txtRegisterNum.Validating += new CancelEventHandler(txt_Validating);
            txtRegisterNum.Tag = "Бүртгэлийн дугаар оруулна уу.";

            txtPageNum.Validating += new CancelEventHandler(txt_Validating);
            txtPageNum.Tag = "Хуудасны дугаар оруулна уу.";

            txtDocNum.Validating += new CancelEventHandler(txt_Validating);
            txtDocNum.Tag = "Бичгийн дугаар оруулна уу.";

            txtFromWho.Validating += new CancelEventHandler(txt_Validating);
            txtFromWho.Tag = "Хэнээс бичиг ирсэн хүний нэрийг оруулна уу.";

            memoDesc.Validating += new CancelEventHandler(memo_Validating);
            memoDesc.Tag = "Товч утгыг оруулна уу.";

            lkUpLocation.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpMoveBranch.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpOrganization.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpOrganization.ProcessNewValue += new ProcessNewValueEventHandler(lkUpOrganization_ProcessNewValue);
            lkUpOrganization.Tag = "Байгууллагын мэдээллийг оруулна уу.";

            lkUpDocNoteType.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpDocNoteType.Tag = "Бичгийн төрлийг оруулна уу.";

            lkUpMoveStaff.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpMoveStaff.Tag = 2;

            lkUpToStaff.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpToStaff.Tag = "Бичиг ирсэн даргыг оруулна уу.";

            barBtnDelete.Enabled = isEditMode;
            lciOutcome.Control.Enabled = false;
            ckIsOutcome.CheckedChanged += new EventHandler(ck_CheckedChanged);
            ckIsOutcome.Tag = 1;
            ckIsOutcome.Checked = false;

            if (isEditMode)
            {
                barBtnRegister.Caption = "Хадгалах";
                barBtnPdf.Enabled = document.ParentPkId == null;

                if (Tool.permissionId.Equals(decimal.One))
                {
                    lcgToStaff.Enabled = lcgRegisterNum.Enabled = lcgRegisteredBy.Enabled = lcgFrom.Enabled =
                        lcgDocNoteType.Enabled = lcgDoc.Enabled = lcgDesc.Enabled = document.ParentPkId == null;
                }
                else
                {
                    barBtnDelete.Enabled = lcgToStaff.Enabled = lcgRegisterNum.Enabled = lcgRegisteredBy.Enabled = lcgFrom.Enabled =
                        lcgDocNoteType.Enabled = lcgDoc.Enabled = lcgDesc.Enabled = false;
                }
            }
        }

        private void InitData()
        {
            Dictionary<string, string> filter = null;
            DataTable info = null;
            decimal locationPkId;

            Dictionary<string, object> filterStored = null;
            List<string> tableNames = null;
            DataSet ds = null;
            DataTable datatable = null;
            try
            {
                Tool.ShowWaiting();
                if (isEditMode)
                {
                    if (document == null) return;
                    dateRegistered.EditValue = document.RegDate;
                    txtRegisterNum.Text = document.RegNum;
                    txtRegisteredBy.Text = document.Name;

                    dateDoc.EditValue = document.DocDate;
                    txtPageNum.Text = document.PageNum.ToString();
                    txtDocNum.Text = document.DocNum;

                    filter = new Dictionary<string, string>();
                    filter.Add("STATUS", "='Y'");
                    SqlConnector.ResetConnection(dbName);
                    info = SqlConnector.GetTable(dbName, "OrganizationType", new List<String>() { "PKID", "PARENTPKID", "NAME" }, filter);
                    locationDt = info.Select("PARENTPKID IS NULL").CopyToDataTable();
                    organizationDt = info.Select("PARENTPKID IS NOT NULL").CopyToDataTable();
                    lkUpLocation.Properties.DataSource = locationDt;
                    lkUpLocation.EditValue = locationPkId =
                        string.IsNullOrEmpty(((System.Data.DataRow)(info.Select(string.Format("PKID = {0}", document.OrganizationTypePkId)).GetValue(0))).ItemArray[1].ToString()) ?
                            decimal.Parse(((System.Data.DataRow)(info.Select(string.Format("PKID = {0}", document.OrganizationTypePkId)).GetValue(0))).ItemArray[0].ToString()) :
                            decimal.Parse(((System.Data.DataRow)(info.Select(string.Format("PKID = {0}", document.OrganizationTypePkId)).GetValue(0))).ItemArray[1].ToString());
                    lkUpOrganization.EditValue = document.OrganizationTypePkId;
                    txtFromWho.Text = document.InFromWho;

                    lkUpDocNoteType.Properties.DataSource = SqlConnector.GetTable(dbName, "DocNoteType", new List<String>() { "PKID", "NAME" }, filter);
                    lkUpDocNoteType.EditValue = document.DocNotePkId;
                    memoControlDirection.Text = document.ControlDirection;
                    memoDesc.Text = document.ShortDesc;
                    lkUpMoveBranch.Properties.DataSource = MainPage.branchInfo;
                    scannedFileName = document.ScannedFile;

                    filter.Clear();
                    filter.Add("DOCUMENTTYPE", "='O'");
                    filter.Add("DOCNUM", string.Format("='{0}'", document.DocNum));
                    datatable = SqlConnector.GetTable(dbName, "Document", filter);
                    if (!datatable.Rows.Count.Equals(0))
                    {
                        ckIsOutcome.Checked = true;
                        txtReplyNum.Text = datatable.Rows[0]["REGNUM"].ToString();
                    }

                    filterStored = new Dictionary<string, object>();
                    filterStored.Add("@PKID", document.PkId);
                    filterStored.Add("@IsFirst", "Y");
                    tableNames = new List<string>() { "ChildrenDocument" };
                    lkUpToStaff.Properties.DataSource = document.ParentPkId == null ? MainPage.allHead : MainPage.allUser;
                    lkUpToStaff.EditValue = document.ToStaffId;

                    ds = SqlConnector.GetStoredProcedure(dbName, "GetChildrenIncome", filterStored, tableNames);
                    if (ds.Tables["ChildrenDocument"] == null) return;
                    if (ds.Tables["ChildrenDocument"].Rows == null || ds.Tables["ChildrenDocument"].Rows.Count.Equals(0)) return;
                    ckIsMovement.Checked = true;
                    lkUpMoveBranch.EditValue = ds.Tables["ChildrenDocument"].Rows[0].ItemArray[10];
                    lkUpMoveStaff.EditValue = ds.Tables["ChildrenDocument"].Rows[0].ItemArray[9];
                    if(MainPage.allUser.Select(string.Format("ST_ID = {0} AND BR_ID = {1}", lkUpMoveStaff.EditValue, lkUpMoveBranch.EditValue)).Length.Equals(0))
                    {
                        lkUpMoveStaff.Properties.DataSource = MainPage.allUser.Select(string.Format("ST_ID = {0}", ds.Tables["ChildrenDocument"].Rows[0].ItemArray[9])).CopyToDataTable();
                        isOnly = true;
                    }

                    dateMovement.EditValue = Tool.ConvertNonTimeDateTime(ds.Tables["ChildrenDocument"].Rows[0].ItemArray[21]);

                    childPkIds = new List<string>();
                    foreach (DataRow dr in ds.Tables["ChildrenDocument"].Rows)
                        childPkIds.Add(dr["PKID"].ToString());
                }
                else
                {
                    txtRegisterNum.Text = SqlConnector.GetFunction(dbName, "GetRegNum").ToString();
                    txtRegisteredBy.Text = Tool.userFName;

                    filter = new Dictionary<string, string>();
                    filter.Add("STATUS", "='Y'");
                    info = SqlConnector.GetTable(dbName, "OrganizationType", new List<String>() { "PKID", "PARENTPKID", "NAME" }, filter);
                    locationDt = info.Select("PARENTPKID IS NULL").CopyToDataTable();
                    organizationDt = info.Select("PARENTPKID IS NOT NULL").CopyToDataTable();
                    lkUpLocation.Properties.DataSource = locationDt;

                    lkUpDocNoteType.Properties.DataSource = SqlConnector.GetTable(dbName, "DocNoteType", new List<String>() { "PKID", "NAME" }, filter);
                    lkUpDocNoteType.EditValue = defaultDocNoteType;

                    lkUpToStaff.Properties.DataSource = MainPage.allHead;
                    lkUpMoveBranch.Properties.DataSource = MainPage.branchInfo;
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шидэт шууд бүртгэлийг нээхэд алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шидэт шууд бүртгэлийг нээхэд алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Шидэт шууд бүртгэлийг нээхэд алдаа гарлаа!", ex.Message);
            }
            finally
            {
                filter = null; info = null; filterStored = null; tableNames = null; 
                ds = null; datatable = null; Tool.CloseWaiting();
            }
        }

        private void CheckControl()
        {
            try
            {
                isActionProgress = false;
                CheckControl(dateRegistered);
                CheckControl(txtRegisteredBy);
                CheckControl(txtRegisterNum);
                CheckControl(dateDoc);
                CheckControl(txtPageNum);
                CheckControl(txtDocNum);
                CheckControl(lkUpLocation);
                CheckControl(txtFromWho);
                CheckControl(lkUpDocNoteType);
                CheckControl(lkUpToStaff);
                CheckControl(memoDesc);
                CheckControl(lkUpMoveBranch);
                CheckControl(lkUpMoveStaff);
                CheckControl(dateMovement);
            }
            catch (MofException ex) { throw ex; }
            catch (Exception ex) { throw new MofException("Контролыг шалгахад алдаа гарлаа!", ex); }
        }

        private void ClearControl()
        {
            try
            {
                if (isEditMode) return;
                dateRegistered.Focus();
                dateRegistered.DateTime = dateDoc.DateTime = dateMovement.DateTime = DateTime.Now;
                txtPageNum.Text = string.Empty;
                txtDocNum.Text = string.Empty;

                lciAddOrganization.Control.Enabled = lciOrganization.Control.Enabled = false;
                txtFromWho.Enabled = false;
                ckIsMovement.Checked = false;

                lkUpLocation.EditValue = null;
                lkUpOrganization.EditValue = null;
                txtFromWho.Text = string.Empty;

                lkUpToStaff.EditValue = null;
                lkUpMoveBranch.EditValue = null;
                lkUpMoveStaff.EditValue = null;

                memoControlDirection.Text = string.Empty;
                memoDesc.Text = string.Empty;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Контролыг цэвэрлэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Контролыг цэвэрлэхэд алдаа гарлаа", ex);
            }
        }

        private void RegisterIncome()
        {
            CheckControl();
            if (isActionProgress) return;

            decimal newPkId;
            Dictionary<string, string> parameters = null;
            Dictionary<string, string> parameterWithMovement = null;
            Dictionary<string, string> filters = null;
            try
            {
                Tool.ShowWaiting();
                parameters = new Dictionary<string, string>();
                if (ckIsOutcome.Checked && !string.IsNullOrEmpty(txtReplyNum.Text.Trim()))
                {
                    parameters.Add("STATUS", "'N'");
                    parameters.Add("CLOSEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateDoc.EditValue)));
                    parameters.Add("DOCNUM", string.Format("N'{0}'", txtDocNum.Text));

                    filters = new Dictionary<string, string>();
                    filters.Add("REGNUM", string.Format("='{0}'", txtReplyNum.Text));
                    SqlConnector.Update(dbName, "Document", parameters, filters);
                }

                parameters.Clear();
                parameters.Add("REGNUM", txtRegisterNum.Text);
                parameters.Add("REGDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateRegistered.DateTime)));
                parameters.Add("DOCNUM", string.Format("N'{0}'", txtDocNum.Text));
                parameters.Add("DOCDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateDoc.DateTime)));

                if (!string.IsNullOrEmpty(memoControlDirection.Text))
                    parameters.Add("CONTROLDIRECTION", string.Format("N'{0}'", memoControlDirection.Text));

                parameters.Add("STAFFID", Tool.userStaffId.ToString());
                parameters.Add("TOSTAFFID", lkUpToStaff.EditValue.ToString());
                parameters.Add("TOBRID", MainPage.allUser.Select(string.Format("ST_ID = {0}", lkUpToStaff.EditValue))[0]["BR_ID"].ToString());
                parameters.Add("DOCNOTEPKID", lkUpDocNoteType.EditValue.ToString());
                parameters.Add("ORGANIZATIONTYPEPKID", lkUpOrganization.EditValue.ToString());
                parameters.Add("INFROMWHO", string.Format("N'{0}'", txtFromWho.Text));
                parameters.Add("ISREPLYDOC", "'N'");
                parameters.Add("SHORTDESC", string.Format("N'{0}'", memoDesc.Text));
                parameters.Add("PAGENUM", txtPageNum.Text);
                parameters.Add("CREATEDDATE", string.Format("'{0}'", Tool.DateTimeNow()));
                if (!string.IsNullOrEmpty(scannedFileName))
                    parameters.Add("SCANNEDFILE", string.Format("N'{0}'", scannedFileName.Replace(replaceScannedFile, "")));

                if (isEditMode)
                {
                    SqlConnector.UpdateByPkId(dbName, "Document", parameters, document.PkId);
                    if (!ckIsMovement.Checked) goto Finish;
                    parameterWithMovement = parameters;
                    parameterWithMovement.Remove("PKID");
                    parameterWithMovement.Remove("PARENTPKID");
                    parameterWithMovement.Remove("TOSTAFFID");
                    parameterWithMovement.Remove("TOBRID");
                    parameterWithMovement.Remove("CREATEDDATE");

                    parameterWithMovement.Add("PARENTPKID", document.PkId.ToString());
                    parameterWithMovement.Add("TOSTAFFID", lkUpMoveStaff.EditValue.ToString());
                    parameterWithMovement.Add("TOBRID", lkUpMoveBranch.EditValue.ToString());
                    parameterWithMovement.Add("CREATEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateMovement.DateTime)));
                    if (childPkIds == null)
                    {
                        parameterWithMovement.Add("PKID", Tool.NewPkId().ToString());
                        SqlConnector.Insert(dbName, "Document", parameterWithMovement);
                    }
                    else RecursiveUpdate(parameterWithMovement);
                }
                else
                {
                    newPkId = Tool.NewPkId();
                    parameters.Add("PKID", newPkId.ToString());
                    SqlConnector.Insert(dbName, "Document", parameters);
                    if (!ckIsMovement.Checked) goto Finish;
                    parameterWithMovement = parameters;
                    parameterWithMovement.Remove("PKID");
                    parameterWithMovement.Remove("PARENTPKID");
                    parameterWithMovement.Remove("TOSTAFFID");
                    parameterWithMovement.Remove("TOBRID");
                    parameterWithMovement.Remove("CREATEDDATE");

                    parameterWithMovement.Add("PKID", Tool.NewPkId().ToString());
                    parameterWithMovement.Add("PARENTPKID", newPkId.ToString());
                    parameterWithMovement.Add("TOSTAFFID", lkUpMoveStaff.EditValue.ToString());
                    parameterWithMovement.Add("TOBRID", lkUpMoveBranch.EditValue.ToString());
                    parameterWithMovement.Add("CREATEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateMovement.DateTime)));
                    SqlConnector.Insert(dbName, "Document", parameterWithMovement);
                }
            Finish:
                //Tool.ShowSuccess("Мэдээллийг амжилттай хадгаллаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (MofException ex)
            {
                if (ex.errorCode.Equals(2627))
                {
                    txtRegisterNum.Text = SqlConnector.GetFunction(dbName, "GetRegNum").ToString();
                    RegisterIncome();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Шууд бүртгэлийг хадгалахад алдаа гарлаа: " + ex.Message);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шууд бүртгэлийг хадгалахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Шууд бүртгэлийг хадгалахад алдаа гарлаа!", ex);
            }
            finally { parameters = null; parameterWithMovement = null; filters = null; Tool.CloseWaiting(); }
        }

        private void Print() 
        {
            CheckControl();
            if (isActionProgress) return;

            List<object> parameter = null;
            DataTable dt = null;
            Dictionary<string, string> filter = null;
            string toStaff = null;
            try
            {
                filter = new Dictionary<string, string>();
                filter.Add("PARENTPKID IS ", "NULL");
                filter.Add("REGNUM =", string.Format("'{0}'", txtRegisterNum.Text));
                dt = SqlConnector.GetTable(dbName, "Document", filter);
                toStaff = dt.Rows.Count.Equals(0) ? lkUpToStaff.Text
                    : MainPage.allUser.Select(string.Format("ST_ID = {0}", dt.Rows[0]["TOSTAFFID"]))[0].ItemArray[3].ToString();

                parameter = new List<object>() { txtRegisteredBy.Text, Tool.ConvertNonTimeDateTime(dateRegistered.EditValue), txtRegisterNum.Text, 
                    Tool.ConvertNonTimeDateTime(dateDoc.EditValue), txtDocNum.Text, txtPageNum.Text, lkUpLocation.Text, lkUpOrganization.Text, txtFromWho.Text, toStaff,
                    lkUpDocNoteType.Text, memoDesc.Text, memoControlDirection.Text, Tool.ConvertNonTimeDateTime(dateMovement.EditValue), lkUpMoveBranch.Text, lkUpMoveStaff.Text };
                Tool.PrintReport(0, "Шууд бүртгэлийн тайлан", parameter);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайланг гаргахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайланг гаргахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайланг гаргахад алдаа гарлаа!", ex);
            }
            finally
            {
                parameter = null;
                dt = null;
                filter = null;
                toStaff = null;
            }
        }

        private void PrintSave()
        {
            try
            {
                CheckControl();
                if (isActionProgress) return;

                RegisterIncome();
                Print();
            }
            catch (MofException ex) { Tool.ShowError(ex.Message, ex.InnerException.Message); }
            catch (Exception ex) { Tool.ShowError(ex.Message, ex.InnerException.Message); }
        }

        private void DeleteIncome()
        {
            if (!isEditMode) return;
            Dictionary<string, object> parameter = null;
            DialogResult dialogResult = System.Windows.Forms.DialogResult.Cancel;
            try
            {
                dialogResult = MessageBox.Show(this.Parent, "Мэдээллийг устгахдаа итгэлтэй байна уу?", "Шууд бүртгэл устгах", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult.Equals(DialogResult.Cancel))
                    return;

                Tool.ShowWaiting();
                parameter = new Dictionary<string, object>();
                parameter.Add("@PKID", document.PkId);
                SqlConnector.GetStoredProcedure(dbName, "DeleteIncome", parameter);

                Tool.ShowSuccess("Мэдээллийг амжилттай устгалаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шууд бүртгэл устгахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шууд бүртгэл устгахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Шууд бүртгэл устгахад алдаа гарлаа!", ex);
            }
            finally { parameter = null; Tool.CloseWaiting(); }
        }

        private void GetPdf()
        {
            try { scannedFileName = Tool.SetPdf(); }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Файл хавсаргахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Файл хавсаргахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Файл хавсаргахад алдаа гарлаа!", ex);
            }
        }

        private void Quit() 
        {
            this.Dispose();
        }

        private void CheckControl(object control)
        {
            DateEdit dateEdit = null;
            TextEdit textEdit = null;
            LookUpEdit lookUpEdit = null;
            MemoEdit memoEdit = null;
            try
            {
                if (control.ToString().Equals("DevExpress.XtraEditors.DateEdit"))
                {
                    dateEdit = control as DateEdit;
                    if (dateEdit.Tag == null) return;
                    if (dateEdit.Tag.Equals(2) && !ckIsMovement.Checked) return;
                    if (dateEdit.EditValue != null) return;

                    dateEdit.ErrorText = "Огноо оруулна уу.";
                    if (isActionProgress) return;
                    dateEdit.Focus();
                    isActionProgress = !isActionProgress;
                }
                else if (control.ToString().Equals("DevExpress.XtraEditors.TextEdit"))
                {
                    textEdit = control as TextEdit;
                    if (!string.IsNullOrEmpty(textEdit.Text)) return;

                    textEdit.ErrorText = textEdit.Tag.ToString();
                    if (isActionProgress) return;
                    textEdit.Focus();
                    isActionProgress = !isActionProgress;
                }
                else if (control.ToString().Equals("DevExpress.XtraEditors.LookUpEdit"))
                {
                    lookUpEdit = control as LookUpEdit;
                    if (lookUpEdit.Tag == null) return;
                    if (lookUpEdit.EditValue != null) return;
                    if (!ckIsMovement.Checked && (lookUpEdit.Tag.Equals(1) || lookUpEdit.Tag.Equals(2))) return;

                    if (lookUpEdit.Tag.Equals(0)) { lookUpEdit.ErrorText = "Байршлийг тодорхойлно уу."; }
                    else if (lookUpEdit.Tag.Equals(1)) { lookUpEdit.ErrorText = "Шилжүүлэх газар, албыг тодорхойлно уу."; }
                    else if (lookUpEdit.Tag.Equals(2)) { lookUpEdit.ErrorText = "Шилжүүлэх ажилтанг тодорхойлно уу."; }
                    else { lookUpEdit.ErrorText = lookUpEdit.Tag.ToString(); }

                    if (isActionProgress) return;
                    lookUpEdit.Focus();
                    isActionProgress = !isActionProgress;
                }
                else if (control.ToString().Equals("DevExpress.XtraEditors.MemoEdit"))
                {
                    memoEdit = control as MemoEdit;
                    if (!string.IsNullOrEmpty(memoEdit.Text)) return;

                    memoEdit.ErrorText = memoEdit.Tag.ToString();
                    if (isActionProgress) return;
                    memoEdit.Focus();
                    isActionProgress = !isActionProgress;
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Контрол шалгахад алдаа гарлаа: " + ex.InnerException.Message);
                throw new MofException("Контрол шалгахад алдаа гарлаа!", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Контрол шалгахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Контрол шалгахад алдаа гарлаа!", ex);
            }
            finally { dateEdit = null; textEdit = null; lookUpEdit = null; memoEdit = null; }
        }

        private void RecursiveUpdate(Dictionary<string,string> parentParameter)
        {
            if (childPkIds == null) return;
            int count = 0;
            try
            {
                foreach (string pkId in childPkIds)
                {
                    if (!count.Equals(0))
                    {
                        parentParameter.Remove("PARENTPKID");
                        parentParameter.Remove("TOSTAFFID");
                        parentParameter.Remove("TOBRID");
                        parentParameter.Remove("CREATEDDATE");
                    }
                    SqlConnector.UpdateByPkId(dbName, "Document", parentParameter, decimal.Parse(pkId));
                    count++;
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шилжүүлсэн бичигт өөрчлөлт оруулахад алдаа гарлаа: " + ex.InnerException.Message);
                throw new MofException("Шилжүүлсэн бичигт өөрчлөлт оруулахад алдаа гарлаа!", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шилжүүлсэн бичигт өөрчлөлт оруулахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Шилжүүлсэн бичигт өөрчлөлт оруулахад алдаа гарлаа!", ex);
            }
        }

        #endregion

        #region Event

        private void lkUp_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit control = null;
            DataRow[] dr = null;
            try
            {
                control = sender as LookUpEdit;
                if (control.EditValue == null || control.Tag == null) return;
                if (control.Tag.Equals(0))
                {
                    dr = organizationDt.Select(string.Format("PARENTPKID = {0}", control.EditValue));
                    lciAddOrganization.Control.Enabled = true;
                    if (dr == null || dr.Length.Equals(0))
                    {
                        Tool.ShowInfo(string.Format(MofDoc.Properties.Resources.AddOrganizationInfo, control.Text));
                        lkUpOrganization.Properties.DataSource = null;
                        txtFromWho.Enabled = lciOrganization.Control.Enabled = false;
                        txtFromWho.Text = string.Empty;
                        return;
                    }
                    lkUpOrganization.Properties.DataSource = dr.CopyToDataTable();
                    txtFromWho.Enabled = lciOrganization.Control.Enabled = true;
                    lkUpOrganization.EditValue = dr.CopyToDataTable().DefaultView[0].Row[0];
                }
                else if (control.Tag.Equals(1))
                {
                    lkUpMoveStaff.EditValue = null;
                    if (MainPage.allUser.Select(string.Format("BR_ID = {0}", control.EditValue)).Length.Equals(0))
                    {
                        lkUpMoveStaff.Properties.DataSource = null;
                        lkUpMoveStaff.EditValue = null;
                    }
                    else
                    {
                        lkUpMoveStaff.Properties.DataSource = MainPage.allUser.Select(string.Format("BR_ID = {0}", control.EditValue)).CopyToDataTable();
                        if (isOnly) isOnly = false;
                    }
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("LookupEdit контролыг сонгоход алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LookupEdit контролыг сонгоход алдаа гарлаа: " + ex.Message);
                Tool.ShowError("LookupEdit контролыг сонгоход алдаа гарлаа!", ex.Message);
            }
            finally { control = null; dr = null; }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = null;
            OrganizationInfoList organizationInfoList = null;
            Dictionary<string, string> filter = null;
            DataTable info = null;
            DialogResult dialogResult = DialogResult.Cancel;
            try
            {
                btn = sender as SimpleButton;
                if (btn.Tag.Equals(0))
                {
                    organizationInfoList = new OrganizationInfoList(dbName, decimal.Parse(lkUpLocation.EditValue.ToString()));
                    organizationInfoList.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = organizationInfoList.ShowDialog();

                    filter = new Dictionary<string, string>();
                    filter.Add("STATUS", "='Y'");
                    SqlConnector.ResetConnection(dbName);
                    info = SqlConnector.GetTable(dbName, "OrganizationType", new List<String>() { "PKID", "PARENTPKID", "NAME" }, filter);
                    locationDt = info.Select("PARENTPKID IS NULL").CopyToDataTable();
                    organizationDt = info.Select("PARENTPKID IS NOT NULL").CopyToDataTable();

                    lkUpLocation.Properties.DataSource = locationDt;
                    lkUpLocation.EditValue = organizationInfoList.locationPkId;
                    lkUpOrganization.Properties.DataSource = organizationDt.Select(string.Format("PARENTPKID = {0}", organizationInfoList.locationPkId)).CopyToDataTable();
                    lkUpOrganization.EditValue = organizationInfoList.organizationPkId;

                    lciOrganization.Control.Enabled = true;
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Товч дарахад алдаа гарлаа!", ex.Message);
            }
            finally { btn = null; organizationInfoList = null; filter = null; info = null; }
        }

        private void ck_CheckedChanged(object sender, EventArgs e)
        {
            int tagNum = int.Parse((sender as CheckEdit).Tag.ToString());
            if (tagNum.Equals(0))
                lcgMovementGroup.Enabled = ckIsMovement.Checked;
            else 
                lciOutcome.Control.Enabled = ckIsOutcome.Checked;
        }

        private void barBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (e.Item.Tag == null) return;
                int caseSwitch = int.Parse(e.Item.Tag.ToString());
                switch (caseSwitch)
                {
                    case 0:
                        ClearControl();
                        break;
                    case 1:
                        RegisterIncome();
                        break;
                    case 2:
                        Print();
                        break;
                    case 3:
                        PrintSave();
                        break;
                    case 4:
                        DeleteIncome();
                        break;
                    case 5:
                        GetPdf();
                        break;
                    case 6:
                        Quit();
                        break;
                    default:
                        throw new MofException("Товчны код тодорхой бус байна!");
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Цэсний товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Цэсний товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Цэсний товч дарахад алдаа гарлаа!", ex.Message);
            }
        }

        #region Validating Events

        private void date_Validating(object sender, CancelEventArgs e)
        {
            DateEdit dateEdit = null;
            try
            {
                dateEdit = sender as DateEdit;
                if (dateEdit.Tag == null) return;
                if (dateEdit.Tag.Equals(2) && !ckIsMovement.Checked) return;
                if (dateEdit.EditValue != null) return;
                dateEdit.ErrorText = "Огноо оруулна уу.";
                e.Cancel = true;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("DateEdit контролыг баталгаажуулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("DateEdit контролыг баталгаажуулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("DateEdit контролыг баталгаажуулахад алдаа гарлаа!", ex.Message);
            }
            finally { dateEdit = null;}
        }

        private void txt_Validating(object sender, CancelEventArgs e)
        {
            TextEdit txtEdit = null;
            try
            {
                txtEdit = sender as TextEdit;
                if (txtEdit.Tag == null) return;
                if (!string.IsNullOrEmpty(txtEdit.Text)) return;
                txtEdit.ErrorText = txtEdit.Tag.ToString();
                e.Cancel = true;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("TextEdit контролыг баталгаажуулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TextEdit контролыг баталгаажуулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("TextEdit контролыг баталгаажуулахад алдаа гарлаа!", ex.Message);
            }
            finally { txtEdit = null; }
        }

        private void lkUp_Validating(object sender, CancelEventArgs e)
        {
            LookUpEdit lkUpEdit = null;
            try
            {
                lkUpEdit = sender as LookUpEdit;
                if (lkUpEdit.Tag == null) return;
                if (lkUpEdit.Tag.Equals(1) && lkUpMoveStaff.Properties.DataSource == null)
                {
                    lkUpEdit.ErrorText = "Ажилтан, албан хаагчтай газар, алба сонгоно уу.";
                    e.Cancel = true;
                }
                if (lkUpEdit.EditValue != null) return;
                if (lkUpEdit.Tag.Equals(0)) { lkUpEdit.ErrorText = "Байршлийг тодорхойлно уу."; }
                else if (lkUpEdit.Tag.Equals(1)) { lkUpEdit.ErrorText = "Шилжүүлэх газар, албыг тодорхойлно уу."; }
                else if (lkUpEdit.Tag.Equals(2)) { lkUpEdit.ErrorText = "Шилжүүлэх ажилтанг тодорхойлно уу."; }
                else { lkUpEdit.ErrorText = lkUpEdit.Tag.ToString(); }
                e.Cancel = true;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("LookUpEdit контролыг баталгаажуулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LookUpEdit контролыг баталгаажуулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("LookupEdit контролыг баталгаажуулахад алдаа гарлаа!", ex.Message);
            }
            finally { lkUpEdit = null; }
        }

        private void memo_Validating(object sender, CancelEventArgs e)
        {
            MemoEdit memoEdit = null;
            try
            {
                memoEdit = sender as MemoEdit;
                if (memoEdit.Tag == null) return;
                if (memoEdit.EditValue != null || !string.IsNullOrEmpty(memoEdit.Text)) return;
                memoEdit.ErrorText = memoEdit.Tag.ToString();
                e.Cancel = true;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("MemoEdit контролыг баталгаажуулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MemoEdit контролыг баталгаажуулахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("MemoEdit контролыг баталгаажуулахад алдаа гарлаа!", ex.Message);
            }
            finally { memoEdit = null; }
        }

        #endregion

        private void lkUpOrganization_ProcessNewValue(object sender, ProcessNewValueEventArgs e)
        {
            Dictionary<string, string> parameters = null;
            decimal pkId;
            DataRow dr = null;
            DataTable source = null;
            try
            {
                if (e.DisplayValue == null || string.IsNullOrEmpty(e.DisplayValue.ToString().Trim()) || string.Empty.Equals(e.DisplayValue))
                    return;

                pkId = Tool.NewPkId();
                parameters = new Dictionary<string, string>();
                parameters.Add("PKID", pkId.ToString());
                parameters.Add("PARENTPKID", lkUpLocation.EditValue.ToString());
                parameters.Add("NAME", string.Format("N'{0}'", e.DisplayValue.ToString()));
                parameters.Add("CREATEDBY", Tool.userStaffId.ToString());
                parameters.Add("CREATEDDATE", string.Format("'{0}'", Tool.ConvertDateTime(DateTime.Now)));
                parameters.Add("STATUS", "'Y'");

                SqlConnector.Insert(dbName, "OrganizationType", parameters);

                dr = organizationDt.NewRow();
                dr["PKID"] = pkId;
                dr["PARENTPKID"] = lkUpLocation.EditValue;
                dr["NAME"] = e.DisplayValue;
                organizationDt.Rows.Add(dr);

                source = lkUpOrganization.Properties.DataSource as DataTable;
                if (source != null)
                {
                    dr = source.NewRow();
                    dr["PKID"] = pkId;
                    dr["PARENTPKID"] = lkUpLocation.EditValue;
                    dr["NAME"] = e.DisplayValue;
                    source.Rows.Add(dr);
                    lkUpOrganization.Refresh();
                }
                e.Handled = true;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын бүртгэлийг нэмэхэд алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын бүртгэлийг нэмэхэд алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Байгууллагын бүртгэлийг нэмэхэд алдаа гарлаа!", ex.Message);
            }
            finally { parameters = null; dr = null; source = null; }
        }

        private void lkUpMoveStaff_Click(object sender, EventArgs e)
        {
            if (!isOnly) return;
            Tool.ShowInfo("Энэхүү ажилтан нь тус хэлтсээс шилжсэн байна! Ажилтныг өөрчлөх бол эхлээд хэлтэс/газраа дахин сонгоно уу.");
        }

        #endregion

    }
}