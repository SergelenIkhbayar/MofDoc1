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
using System.Data.SqlClient;

namespace MofDoc.Forms.Page.Outcome
{
    public partial class OutcomeRegister : XtraForm
    {

        #region Properties

        private string dbName = null;
        private bool isEditMode = false;
        private Document document = null;

        private string scannedFileName = null;
        private DataTable locationDt = null;
        private DataTable organizationDt = null;
        private decimal defaultDocNoteType = 34;
        private bool isActionProgress = false;
        private bool isChanged = false;
        private string emIndexNumber = null;
        private decimal emBranchId;

        #endregion

        #region Constructor

        public OutcomeRegister(string dbName)
        {
            InitializeComponent();
            this.dbName = dbName;
            isEditMode = false;

            InitControl();
            InitData();
        }

        public OutcomeRegister(string dbName, Document document)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.document = document;
            isChanged = isEditMode = true;

            InitControl();
            InitData();
            isChanged = false;
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Явсан бичгийн бүртгэл";
            this.MaximumSize = this.MinimumSize = this.Size;

            txtRegNum.Properties.ReadOnly = true;
            txtRegNum.Properties.Appearance.Font = new Font("Arial", 10, FontStyle.Bold);

            txtPageNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPageNum.Properties.Mask.EditMask = "[0-9]+";

            dateCreated.EditValue = dateReturnDate.EditValue = DateTime.Now;

            lkUpBranch.Properties.DisplayMember = "NAME";
            lkUpBranch.Properties.ValueMember = "BR_ID";
            lkUpBranch.Properties.PopulateColumns();
            lkUpBranch.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Газар, алба"));
            lkUpBranch.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpBranch.Tag = 0;

            lkUpStaff.Properties.DisplayMember = "FNAME";
            lkUpStaff.Properties.ValueMember = "ST_ID";
            lkUpStaff.Properties.PopulateColumns();
            lkUpStaff.Properties.Columns.Add(new LookUpColumnInfo("LNAME", "Овог"));
            lkUpStaff.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));
            lkUpStaff.Tag = "Бичиг илгээсэн ажилтанг тодорхойлно уу";

            lkUpDocNoteType.Properties.DisplayMember = "NAME";
            lkUpDocNoteType.Properties.ValueMember = "PKID";
            lkUpDocNoteType.Properties.PopulateColumns();
            lkUpDocNoteType.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Бичгийн төрөл"));

            lkUpLocation.Properties.DisplayMember = "NAME";
            lkUpLocation.Properties.ValueMember = "PKID";
            lkUpLocation.Properties.PopulateColumns();
            lkUpLocation.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байршлын нэр"));
            lkUpLocation.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpLocation.Tag = 1;

            lkUpOrganization.Properties.DisplayMember = "NAME";
            lkUpOrganization.Properties.ValueMember = "PKID";
            lkUpOrganization.Properties.PopulateColumns();
            lkUpOrganization.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байгууллагын нэр"));
            lkUpOrganization.Properties.TextEditStyle = TextEditStyles.Standard;

            btnAddOrganization.Click += new EventHandler(btn_Click);
            btnAddOrganization.Tag = 0;

            btnAddOrganization.Enabled = false;
            lciOrganization.Control.Enabled = false;
            txtFromWho.Enabled = false;
            dateReturnDate.Enabled = false;
            dateReturnDate.Tag = 0;

            ckIsReturn.Checked = false;
            ckIsReturn.Tag = 0;
            ckIsReturn.CheckedChanged += new EventHandler(ck_CheckedChanged);

            ckIsManual.Checked = false;
            ckIsManual.Tag = 1;
            ckIsManual.CheckedChanged += new EventHandler(ck_CheckedChanged);

            barBtnClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnClear.Tag = 0;

            barBtnRegister.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnRegister.Tag = 1;

            barBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnDelete.Tag = 2;

            barBtnPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnPdf.Tag = 3;

            barBtnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barBtn_ItemClick);
            barBtnCancel.Tag = 4;

            barBtnDelete.Enabled = isEditMode;
            barBtnClear.Enabled = !isEditMode;

            lkUpBranch.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpStaff.Validating += new CancelEventHandler(lkUp_Validating);

            txtRegNum.Validating += new CancelEventHandler(txt_Validating);
            txtRegNum.Tag = "Явсан бичгийн бүртгэлийн дугаар оруулна уу";

            dateCreated.Validating += new CancelEventHandler(date_Validating);
            dateCreated.Tag = "Бичиг явсан огноог оруулна уу";

            txtPageNum.Validating += new CancelEventHandler(txt_Validating);
            txtPageNum.Tag = "Бичгийн тоог оруулна уу";

            lkUpDocNoteType.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpDocNoteType.Tag = "Бичгийн төрлийг оруулна уу";

            lkUpLocation.Validating += new CancelEventHandler(lkUp_Validating);

            lkUpOrganization.Validating += new CancelEventHandler(lkUp_Validating);
            lkUpOrganization.ProcessNewValue += new ProcessNewValueEventHandler(lkUpOrganization_ProcessNewValue);
            lkUpOrganization.Tag = "Илгээсэн байгууллагыг сонгон уу";

            txtFromWho.Validating += new CancelEventHandler(txt_Validating);
            txtFromWho.Tag = "Бичиг илгээсэн хүнийг оруулна уу";

            memoDesc.Validating += new CancelEventHandler(memo_Validating);
            memoDesc.Tag = "Бичгийн тайлбар оруулна уу";

            if (isEditMode)
            {
                if (Tool.permissionId.Equals(decimal.One))
                {
                    barBtnRegister.Caption = "Засах";
                    barBtnPdf.Enabled = document.ParentPkId == null;
                }
                else
                {
                    barBtnRegister.Caption = "Засах";
                    barBtnPdf.Enabled = false;
                    lcgMain.Enabled = false;
                }
            }
        }

        private void InitData()
        {
            Dictionary<string, string> filter = null;
            DataTable info = null;
            try
            {
                Tool.ShowWaiting();
                filter = new Dictionary<string, string>();
                filter.Add("STATUS", "='Y'");
                info = SqlConnector.GetTable(dbName, "OrganizationType", new List<String>() { "PKID", "PARENTPKID", "NAME" }, filter);
                locationDt = info.Select("PARENTPKID IS NULL").CopyToDataTable();
                organizationDt = info.Select("PARENTPKID IS NOT NULL").CopyToDataTable();
                lkUpLocation.Properties.DataSource = locationDt;
                lkUpDocNoteType.Properties.DataSource = SqlConnector.GetTable(dbName, "DocNoteType", new List<String>() { "PKID", "NAME" }, filter);
                lkUpBranch.Properties.DataSource = MainPage.branchInfo;

                if (isEditMode)
                {
                    if (document == null) return;
                    lkUpBranch.EditValue = emBranchId = document.ToBrId;
                    lkUpStaff.EditValue = document.ToStaffId;
                    txtRegNum.Text = emIndexNumber = document.RegNum;
                    dateCreated.EditValue = document.RegDate;
                    txtPageNum.Text = document.PageNum.ToString();
                    lkUpDocNoteType.EditValue = document.DocNotePkId;

                    lkUpLocation.EditValue = decimal.Parse(((System.Data.DataRow)(info.Select(string.Format("PKID = {0}", document.OrganizationTypePkId)).GetValue(0))).ItemArray[1].ToString());
                    lkUpOrganization.EditValue = document.OrganizationTypePkId;

                    txtFromWho.Text = document.InFromWho;
                    memoDesc.EditValue = document.ShortDesc;

                    if (document.ReturnDate == null) return;
                    ckIsReturn.Checked = true;
                    dateReturnDate.EditValue = document.ReturnDate;
                }
                else 
                    lkUpDocNoteType.EditValue = defaultDocNoteType;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичгийн мэдээллийг уншихад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичгийн мэдээллийг уншихад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Явсан бичгийн мэдээллийг уншихад алдаа гарлаа!", ex.Message);
            }
            finally { filter = null; info = null; Tool.CloseWaiting(); }
        }

        private void ClearControl()
        {
            try
            {
                if(isEditMode) return;
                lkUpBranch.Focus();
                lkUpBranch.EditValue = null;
                lkUpStaff.EditValue = null;
                txtRegNum.Text = string.Empty;
                dateCreated.EditValue = null;
                txtPageNum.Text = string.Empty;
                lkUpDocNoteType.EditValue = null;
                lkUpLocation.EditValue = null;
                lkUpOrganization.EditValue = null;
                txtFromWho.Text = string.Empty;
                dateReturnDate.EditValue = null;

                InitControl();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Контролыг цэвэрлэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Контролыг цэвэрлэхэд алдаа гарлаа", ex);
            }
        }

        private void RegisterOutcome()
        {
            Dictionary<string, string> parameters = null;
            Dictionary<string, string> parameterStored = null;
            try
            {
                CheckControl();
                if (isActionProgress) return;

                if (string.IsNullOrEmpty(txtRegNum.Text.Trim()) || !txtRegNum.Text.Contains("/"))
                {
                    txtRegNum.ErrorText = "Хариу бичгийн дугаарыг оруулахын тулд газар, хэлтсийг сонгоно уу.";
                    if (!isActionProgress)
                    {
                        txtRegNum.Focus();
                        isActionProgress = !isActionProgress;
                    }
                    return;
                }
                isActionProgress = false;

                Tool.ShowWaiting();
                parameters = new Dictionary<string, string>();
                parameters.Add("DOCUMENTTYPE", "N'O'");
                parameters.Add("SHORTDESC", string.Format("N'{0}'", memoDesc.EditValue));
                parameters.Add("PAGENUM", txtPageNum.Text);

                parameters.Add("REGNUM", string.Format("N'{0}'", txtRegNum.Text));
                parameters.Add("REGDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateCreated.EditValue)));
                parameters.Add("STAFFID", Tool.userStaffId.ToString());
                parameters.Add("TOSTAFFID", lkUpStaff.EditValue.ToString());
                parameters.Add("TOBRID", lkUpBranch.EditValue.ToString());
                parameters.Add("DOCNOTEPKID", lkUpDocNoteType.EditValue.ToString());
                parameters.Add("ORGANIZATIONTYPEPKID", lkUpOrganization.EditValue.ToString());
                parameters.Add("INFROMWHO", string.Format("N'{0}'", txtFromWho.Text));
                parameters.Add("CREATEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(DateTime.Now)));
                parameters.Add("SCANNEDFILE", string.IsNullOrEmpty(scannedFileName) ? null : string.Format("N'{0}'", scannedFileName));
                parameters.Add("RETURNDATE", !ckIsReturn.Checked ? null : string.Format("N'{0}'", Tool.ConvertDateTime(dateReturnDate.EditValue)));
                parameters.Add("ISREPLYDOC", !ckIsReturn.Checked ? "N'N'" : "N'Y'");

                parameterStored = new Dictionary<string, string>();
                parameterStored.Add("LASTNUMBER", txtRegNum.Text.Substring(txtRegNum.Text.IndexOf("/")).Replace("/", ""));

                if (isEditMode)
                {
                    SqlConnector.UpdateByPkId(dbName, "Document", parameters, document.PkId);
                    if (!emIndexNumber.Equals(txtRegNum.Text))
                        SqlConnector.Update(dbName, "OutcomeIndex", parameterStored, null);
                }
                else
                {
                    parameters.Add("PKID", Tool.NewPkId().ToString());
                    SqlConnector.Insert(dbName, "Document", parameters);
                    SqlConnector.Update(dbName, "OutcomeIndex", parameterStored, null);
                }
                Tool.ShowSuccess("Явсан бичгийн мэдээллийг амжилттай бүртгэлээ!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Dispose();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичиг бүртгэхэд алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичиг бүртгэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Явсан бичиг бүртгэхэд алдаа гарлаа!", ex);
            }
            finally { parameters = null; parameterStored = null; Tool.CloseWaiting(); }
        }

        private void Delete()
        {
            try
            {
                if (!isEditMode) return;
                Dictionary<string, object> parameter = null;
                DialogResult dialogResult = System.Windows.Forms.DialogResult.Cancel;
                dialogResult = MessageBox.Show(this.Parent, "Мэдээллийг устгахдаа итгэлтэй байна уу?", "Явсан бичгийн бүртгэл устгах", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult.Equals(DialogResult.Cancel))
                    return;

                parameter = new Dictionary<string, object>();
                parameter.Add("@PKID", document.PkId);
                SqlConnector.DeleteByPkId(dbName, "Document", document.PkId);

                Tool.ShowSuccess("Мэдээллийг амжилттай устгалаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Явуулсан бичиг устгахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Явуулсан бичиг устгахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Явуулсан бичиг устгахад алдаа гарлаа!", ex);
            }
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

        private void CheckControl()
        {
            try
            {
                isActionProgress = false;
                CheckControl(lkUpBranch);
                CheckControl(lkUpStaff);
                CheckControl(txtRegNum);
                CheckControl(dateCreated);
                CheckControl(txtPageNum);
                CheckControl(lkUpDocNoteType);
                CheckControl(lkUpLocation);
                CheckControl(lkUpOrganization);
                CheckControl(txtFromWho);
                CheckControl(memoDesc);
                CheckControl(dateReturnDate);
            }
            catch (MofException ex) { throw ex; }
            catch (Exception ex) { throw new MofException("Контролыг шалгахад алдаа гарлаа!", ex); }
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
                    if (dateEdit.Tag.Equals(0) && !ckIsReturn.Checked) return;
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

                    if (lookUpEdit.Tag.Equals(0)) { lookUpEdit.ErrorText = "Алба/газар оруулна уу."; }
                    else if (lookUpEdit.Tag.Equals(1)) { lookUpEdit.ErrorText = "Байршил тодорхойлно уу."; }
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

        #endregion

        #region Event

        private void lkUp_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit control = null;
            DataRow[] dr = null;

            string indexNumber = null;
            List<SqlParameter> parameters = null;
            try
            {
                control = sender as LookUpEdit;
                if (control.EditValue == null || control.Tag == null) return;
                if (control.Tag.Equals(0))
                {
                    lkUpStaff.EditValue = null;
                    if (MainPage.allUser.Select(string.Format("BR_ID = {0}", control.EditValue)).Length.Equals(0))
                    {
                        lkUpStaff.Properties.DataSource = null;
                        lkUpStaff.EditValue = null;
                    }
                    else lkUpStaff.Properties.DataSource = MainPage.allUser.Select(string.Format("BR_ID = {0}", control.EditValue)).CopyToDataTable();

                    parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@BRID", lkUpBranch.EditValue));
                    indexNumber = SqlConnector.GetFunction(dbName, "GetOutcomeNumberByIndex", parameters).ToString();

                    if (string.IsNullOrEmpty(indexNumber))
                        throw new MofException(string.Format("{0} газар/хэлтэсийн явсан бичгийн дугаарыг гаргахад(Generate хийхэд) алдаа гарлаа!"));
                    if (isChanged) return;
                    if (lkUpBranch.EditValue.ToString().Equals(emBranchId.ToString())) txtRegNum.Text = emIndexNumber;
                    txtRegNum.Text = indexNumber;
                }
                else if (control.Tag.Equals(1))
                {
                    dr = organizationDt.Select(string.Format("PARENTPKID = {0}", control.EditValue));
                    lciAddOrganization.Control.Enabled = true;
                    if (dr == null || dr.Length.Equals(0))
                    {
                        Tool.ShowInfo(string.Format(MofDoc.Properties.Resources.AddOrganizationInfo, control.Text));
                        lkUpOrganization.Properties.DataSource = null;
                        txtFromWho.Enabled = lciOrganization.Control.Enabled = btnAddOrganization.Enabled = false;
                        txtFromWho.Text = string.Empty;
                        return;
                    }
                    lkUpOrganization.Properties.DataSource = dr.CopyToDataTable();
                    txtFromWho.Enabled = lciOrganization.Control.Enabled = btnAddOrganization.Enabled = true;
                    if (isChanged) return;
                    lkUpOrganization.EditValue = dr.CopyToDataTable().DefaultView[0].Row[0];
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
            finally { control = null; dr = null; indexNumber = null; parameters = null; }
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
                    if (dialogResult.Equals(DialogResult.Cancel)) return;
                    else
                    {
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
            if ((sender as CheckEdit).Equals(0))
                dateReturnDate.Enabled = !dateReturnDate.Enabled;
            else
                txtRegNum.Properties.ReadOnly = !ckIsManual.Checked;
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
                        RegisterOutcome();
                        break;
                    case 2:
                        Delete();
                        break;
                    case 3:
                        GetPdf();
                        break;
                    case 4:
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
                if (dateEdit.Tag.Equals(0) && !ckIsReturn.Checked) return;
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
            finally { dateEdit = null; }
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
                if (lkUpEdit.Tag.Equals(1) && lkUpStaff.Properties.DataSource == null)
                {
                    lkUpEdit.ErrorText = "Ажилтан, албан хаагчтай газар, алба сонгоно уу.";
                    e.Cancel = true;
                }
                if (lkUpEdit.EditValue != null) return;
                if (lkUpEdit.Tag.Equals(0)) { lkUpEdit.ErrorText = "Алба/газар оруулна уу."; }
                else if (lkUpEdit.Tag.Equals(1)) { lkUpEdit.ErrorText = "Байршил тодорхойлно уу."; }
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

        #endregion

    }
}