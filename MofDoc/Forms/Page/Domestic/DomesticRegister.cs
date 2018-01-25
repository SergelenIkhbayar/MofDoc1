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

namespace MofDoc.Forms.Page.Domestic
{
    public partial class DomesticRegister : XtraForm
    {

        #region Properties

        private string dbName = null;
        private bool isEditMode = false;
        private Document document = null;
        private decimal docStaffId;
        private string scannedFileName = null;
        private bool isActionProgress = false;

        #endregion

        #region Constructor

        public DomesticRegister(string dbName)
        {
            InitializeComponent();
            this.dbName = dbName;
            isEditMode = false;

            InitControl();
            InitData();
        }

        public DomesticRegister(string dbName, Document document)
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
            this.Text = "Дотоод бичгийн бүртгэл";
            this.MaximumSize = this.MinimumSize = this.Size;

            txtRegNum.Properties.ReadOnly = true;
            txtRegNum.Properties.Appearance.Font = new Font("Arial", 10, FontStyle.Bold);
            txtRegNum.Tag = "Бүртгэлийн дугаараа оруулна уу.";

            txtPageNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPageNum.Properties.Mask.EditMask = "[0-9]+";
            txtPageNum.Tag = "Хуудасны тоог оруулна уу.";

            dateReg.EditValue = DateTime.Now;
            dateReturnDate.EditValue = DateTime.Now.AddDays(7);
            dateReturnDate.Enabled = false;

            ckIsManual.CheckedChanged += new EventHandler(ck_CheckedChanged);
            ckIsManual.Tag = 0;

            ckIsReturn.CheckedChanged += new EventHandler(ck_CheckedChanged);
            ckIsReturn.Tag = 1;

            lkUpBranch.Properties.DisplayMember = "NAME";
            lkUpBranch.Properties.ValueMember = "BR_ID";
            lkUpBranch.Properties.PopulateColumns();
            lkUpBranch.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Газар, алба"));
            lkUpBranch.EditValueChanged += new EventHandler(lkUp_EditValueChanged);

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
        }

        private void InitData()
        {
            try
            {
                Tool.ShowWaiting();
                lkUpBranch.Properties.DataSource = MainPage.branchInfo.Select("BR_ID NOT IN ('9911', '9910', '9903')").CopyToDataTable();
                if (isEditMode)
                {
                    lkUpBranch.EditValue = document.ToBrId;
                    dateReg.EditValue = document.RegDate;
                    txtRegNum.Text = document.RegNum;
                    if (document.ReturnDate != null)
                    {
                        ckIsReturn.Checked = true;
                        dateReturnDate.EditValue = document.ReturnDate;
                    }
                    txtPageNum.Text = document.PageNum.ToString();
                    txtTitle.Text = document.InFromWho;
                    memoDesc.Text = document.ShortDesc;
                    docStaffId = (decimal)document.ToStaffId;
                }
                else
                    txtRegNum.Text = SqlConnector.GetFunction(dbName, "GetDomesticRegNum").ToString();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Дотоод бичгийн мэдээллийг уншихад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Дотоод бичгийн мэдээллийг уншихад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Дотоод бичгийн мэдээллийг уншихад алдаа гарлаа!", ex.Message);
            }
            finally { Tool.CloseWaiting(); }
        }

        private void CheckControl()
        {
            try
            {
                isActionProgress = false;
                CheckControl(lkUpBranch);
                CheckControl(dateReg);
                CheckControl(txtRegNum);
                CheckControl(txtPageNum);
                CheckControl(memoDesc);
                if(ckIsReturn.Checked)
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
                    if (dateEdit.EditValue != null) return;

                    dateEdit.ErrorText = "Бүртгэсэн огноо оруулна уу.";
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
                    if (lookUpEdit.EditValue != null) return;
                    lookUpEdit.ErrorText = "Дотоод бичиг илгээх газар/хэлтсээ сонгоно уу.";

                    if (isActionProgress) return;
                    lookUpEdit.Focus();
                    isActionProgress = !isActionProgress;
                }
                else if (control.ToString().Equals("DevExpress.XtraEditors.MemoEdit"))
                {
                    memoEdit = control as MemoEdit;
                    if (!string.IsNullOrEmpty(memoEdit.Text)) return;

                    memoEdit.ErrorText = "Дотоод бичгийн агуулгыг оруулна уу.";
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

        private void ClearControl()
        {
            if (isEditMode) return;
            lkUpBranch.EditValue = null;
            dateReg.EditValue = DateTime.Now;
            memoDesc.Text = txtTitle.Text = txtPageNum.Text = string.Empty;
        }

        private void RegisterDomestic()
        {
            Dictionary<string, string> parameters = null;
            try
            {
                CheckControl();
                if (isActionProgress) return;

                Tool.ShowWaiting();
                parameters = new Dictionary<string, string>();
                parameters.Add("DOCUMENTTYPE", "N'D'");
                parameters.Add("SHORTDESC", string.Format("N'{0}'", memoDesc.EditValue));
                parameters.Add("PAGENUM", txtPageNum.Text);

                parameters.Add("REGNUM", string.Format("N'{0}'", txtRegNum.Text));
                parameters.Add("REGDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateReg.EditValue)));
                parameters.Add("STAFFID", Tool.userStaffId.ToString());
                parameters.Add("TOSTAFFID", docStaffId.ToString());
                parameters.Add("TOBRID", lkUpBranch.EditValue.ToString());
                parameters.Add("DOCNOTEPKID", "50");
                parameters.Add("ORGANIZATIONTYPEPKID", "27");
                parameters.Add("INFROMWHO", string.Format("N'{0}'", txtTitle.Text));
                parameters.Add("CREATEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(DateTime.Now)));
                parameters.Add("SCANNEDFILE", string.IsNullOrEmpty(scannedFileName) ? null : string.Format("N'{0}'", scannedFileName));
                parameters.Add("RETURNDATE", !ckIsReturn.Checked ? null : string.Format("N'{0}'", Tool.ConvertDateTime(dateReturnDate.EditValue)));
                parameters.Add("ISREPLYDOC", !ckIsReturn.Checked ? "N'N'" : "N'Y'");

                if (isEditMode)
                    SqlConnector.UpdateByPkId(dbName, "Document", parameters, document.PkId);
                else
                {
                    parameters.Add("PKID", Tool.NewPkId().ToString());
                    SqlConnector.Insert(dbName, "Document", parameters);
                }
                Tool.ShowSuccess("Дотоод бичгийн мэдээллийг амжилттай бүртгэлээ!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Dispose();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Дотоод бичиг бүртгэхэд алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Дотоод бичиг бүртгэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Дотоод бичиг бүртгэхэд алдаа гарлаа!", ex);
            }
            finally { parameters = null; Tool.CloseWaiting(); }
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

                Tool.ShowWaiting();
                parameter = new Dictionary<string, object>();
                parameter.Add("@PKID", document.PkId);
                SqlConnector.DeleteByPkId(dbName, "Document", document.PkId);

                Tool.ShowSuccess("Мэдээллийг амжилттай устгалаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Дотоод бичиг устгахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Дотоод бичиг устгахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Дотоод бичиг устгахад алдаа гарлаа!", ex);
            }
            finally { Tool.CloseWaiting(); }
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

        #endregion

        #region Event

        private void lkUp_EditValueChanged(object sender, EventArgs e)
        {
            string brMainId = null;
            string brChildId = null;
            try
            {
                brMainId = MainPage.branchInfo.Select(string.Format("BR_ID = {0}", lkUpBranch.EditValue))[0].ItemArray[2].ToString();
                if (brMainId.Equals(lkUpBranch.EditValue))  // ГАЗАР РУУ БИЧИГ ИЛГЭЭХ
                {
                    if (MainPage.branchInfo.Select(string.Format("BR_MAIN_ID1 = {0}", brMainId)).Length.Equals(1))  // ГАЗАР НЬ ХЭЛТЭСГҮЙ
                        docStaffId = decimal.Parse(MainPage.allUser.Select(string.Format("BR_ID = {0} AND DOCPERMISSIONID = 3", brMainId))[0].ItemArray[0].ToString());
                    else   // ГАЗАР НЬ ХЭЛТЭСТЭЙ
                    {
                        foreach (DataRow row in MainPage.branchInfo.Select(string.Format("BR_MAIN_ID1 = {0}", brMainId)))
                        {
                            brChildId = row.ItemArray[0].ToString();
                            if (!MainPage.allUser.Select(string.Format("BR_ID = {0} AND DOCPERMISSIONID = 3", brChildId)).Length.Equals(0))
                            {
                                docStaffId = decimal.Parse(MainPage.allUser.Select(string.Format("BR_ID = {0} AND DOCPERMISSIONID = 3", brChildId))[0].ItemArray[0].ToString());
                                return;
                            }
                        }
                    }
                }
                else   // ХЭЛТЭС РҮҮ БИЧИГ ИЛГЭЭХ
                {
                    if (MainPage.allUser.Select(string.Format("BR_ID = {0} AND DOCPERMISSIONID = 2", lkUpBranch.EditValue)).Length.Equals(0))
                    {
                        foreach (DataRow row in MainPage.branchInfo.Select(string.Format("BR_MAIN_ID1 = {0}", brMainId)))
                        {
                            brChildId = row.ItemArray[0].ToString();
                            if (!MainPage.allUser.Select(string.Format("BR_ID = {0} AND DOCPERMISSIONID = 3", brChildId)).Length.Equals(0))
                            {
                                docStaffId = decimal.Parse(MainPage.allUser.Select(string.Format("BR_ID = {0} AND DOCPERMISSIONID = 3", brChildId))[0].ItemArray[0].ToString());
                                return;
                            }
                        }
                    }
                    else
                        docStaffId = decimal.Parse(MainPage.allUser.Select(string.Format("BR_ID = {0} AND DOCPERMISSIONID = 2", lkUpBranch.EditValue))[0].ItemArray[0].ToString());
                }
                if (docStaffId.Equals(decimal.Zero))
                    throw new MofException("Алдаа гарлаа!! Илгээсэн газар/хэлтсийн бичиг хэргийн ажилтан тодорхойгүй байна!");
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Газар/Хэлтэс сонгоход алдаа гарлаа: " + ex.InnerException.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Газар/Хэлтэс сонгоход алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Газар/Хэлтэс сонгоход алдаа гарлаа", ex.Message);
            }
            finally { brMainId = null; brChildId = null; }
        }

        private void ck_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckEdit).Tag == null) return;
            int tagId = int.Parse((sender as CheckEdit).Tag.ToString());
            if(tagId.Equals(0))
                txtRegNum.Properties.ReadOnly = !ckIsManual.Checked;
            else
                dateReturnDate.Enabled = ckIsReturn.Checked;
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
                        RegisterDomestic();
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

        #endregion

    }
}