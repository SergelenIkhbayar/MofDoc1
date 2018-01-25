using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using MofDoc.Class;
using MofDoc.Forms.Page.Info;

namespace MofDoc.Forms.Page.Income.Card
{
    public partial class CardRegisterWizard : XtraForm
    {

        #region Properties

        private string dbName = null;
        private string scannedFileName = null;
        private DataTable locationDt = null;
        private DataTable organizationDt = null;
        private decimal defaultDocNoteType = 34;

        #endregion

        #region Constructor

        public CardRegisterWizard(string dbName)
        {
            InitializeComponent();
            this.dbName = dbName;
            InitControl();
            InitData();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Карттай бүртгэл(Шинэ)";
            this.MaximumSize = this.MinimumSize = this.Size;

            dateReturn.Properties.ReadOnly = txtControlNum.Properties.ReadOnly = txtRegisterNum.Properties.ReadOnly = txtRegisteredBy.Properties.ReadOnly = true;
            txtControlNum.Properties.Appearance.Font = txtRegisterNum.Properties.Appearance.Font = txtRegisteredBy.Properties.Appearance.Font = new Font("Arial", 10, FontStyle.Bold);

            txtPageNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPageNum.Properties.Mask.EditMask = "[0-9]+";

            dateMovement.DateTime = dateDoc.DateTime = dateRegistered.DateTime = DateTime.Now;

            lkUpLocation.Properties.DisplayMember = "NAME";
            lkUpLocation.Properties.ValueMember = "PKID";
            lkUpLocation.Properties.PopulateColumns();
            lkUpLocation.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байршлын нэр"));

            lkUpOrganization.Properties.DisplayMember = "NAME";
            lkUpOrganization.Properties.ValueMember = "PKID";
            lkUpOrganization.Properties.PopulateColumns();
            lkUpOrganization.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байгууллагын нэр"));
            lkUpOrganization.Properties.TextEditStyle = TextEditStyles.Standard;
            lkUpOrganization.ProcessNewValue += new ProcessNewValueEventHandler(lkUpOrganization_ProcessNewValue);

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

            lkUpDateType.Properties.DisplayMember = "Name";
            lkUpDateType.Properties.ValueMember = "Id";
            lkUpDateType.Properties.PopulateColumns();
            lkUpDateType.Properties.Columns.Add(new LookUpColumnInfo("Name", "Хугацааны төрөл"));
            lkUpDateType.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpDateType.Tag = 2;

            lciAddOrganization.Control.Enabled = lciOrganization.Control.Enabled = false;

            wizardControl.FinishClick += new CancelEventHandler(wizardControl_FinishClick);
            wizardControl.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(wizardControl_NextClick);
            wizardControl.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(wizardControl_SelectedPageChanged);
            lkUpLocation.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpLocation.Tag = 0;

            btnAddOrganization.Click += new EventHandler(btn_Click);
            btnAddOrganization.Tag = 0;

            btnAttachment.Click += new EventHandler(btn_Click);
            btnAttachment.Tag = 1;

            txtFromWho.Enabled = false;
            lcgMovementGroup.Enabled = false;
            ckIsMovement.Checked = false;
            ckIsMovement.CheckedChanged += new EventHandler(ck_CheckedChanged);

            txtDocNum.Tag = "Бичгийн дугаар бөглөнө үү.";
            txtPageNum.Tag = "Бичгийн хуудасны тоог оруулна уу.";
            txtFromWho.Tag = "Бичиг ирсэн хүний нэрийг оруулна уу.";

            wizardPageRegisteredBy.Tag = wizardPageDesc.Tag = wizardPageFrom.Tag = wizardPageDoc.Tag = wizardPageToStaff.Tag = 0;
            wizardPageMovement.Tag = 1;
            lkUpToStaff.Tag = "Хэнд ирсэн хүнийг тодорхойлно уу.";
            memoDesc.Tag = "Товч утга оруулна уу.";

            lkUpMoveStaff.Tag = "Ажилтанг тодорхойлно уу.";
            dateMovement.Tag = "Шилжүүлсэн огноог тодорхойлно уу.";
            dateDoc.Tag = dateRegistered.Tag = dateReturn.Tag = "Огноог тодорхойлно уу.";
        }

        private void InitData()
        {
            Dictionary<string, string> filter = null;
            DataTable info = null;
            try
            {
                Tool.ShowWaiting();
                lkUpDateType.Properties.DataSource = Tool.GetDateType();

                txtRegisterNum.Text = SqlConnector.GetFunction(dbName, "GetRegNum").ToString();
                txtControlNum.Text = SqlConnector.GetFunction(dbName, "GetControlNum").ToString();
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
            finally { filter = null; info = null; Tool.CloseWaiting(); }
        }

        private void Print()
        {
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

                parameter = new List<object>() { txtRegisteredBy.Text, Tool.ConvertNonTimeDateTime(dateRegistered.EditValue), txtRegisterNum.Text, txtControlNum.Text,
                    Tool.ConvertNonTimeDateTime(dateDoc.EditValue), txtDocNum.Text, txtPageNum.Text, lkUpLocation.Text, lkUpOrganization.Text, txtFromWho.Text, toStaff, 
                    lkUpDocNoteType.Text, memoDesc.Text, memoControlDirection.Text, Tool.ConvertNonTimeDateTime(dateMovement.EditValue), lkUpMoveBranch.Text, lkUpMoveStaff.Text, Tool.ConvertNonTimeDateTime(dateReturn.EditValue) };
                Tool.PrintReport(1, "Карттай бүртгэлийн тайлан", parameter);
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

        private void SendMail()
        {
            MailParameter mailParameter = null;
            string headId, domainId, userName, branchId = null;
            try
            {
                if (!string.IsNullOrEmpty(scannedFileName))
                    scannedFileName = "http://cmc/docs/upload/" + scannedFileName;

                headId = MainPage.allUser.Select(string.Format("ST_ID = {0}", lkUpToStaff.EditValue))[0].ItemArray[4].ToString();
                domainId = ckIsMovement.Checked ? MainPage.allUser.Select(string.Format("ST_ID = {0}", lkUpMoveStaff.EditValue))[0].ItemArray[4].ToString() : null;
                userName = ckIsMovement.Checked ? lkUpMoveStaff.Text : null;

                if (string.IsNullOrEmpty(headId)) return;
                mailParameter = new MailParameter(headId, txtRegisterNum.Text, txtControlNum.Text, Tool.ConvertNonTimeDateTime(dateDoc.EditValue),
                    txtDocNum.Text, string.Format("{0} - {1}", lkUpLocation.Text, lkUpOrganization.Text), txtFromWho.Text,
                    scannedFileName, Tool.ConvertNonTimeDateTime(dateReturn.EditValue), memoDesc.Text, lkUpToStaff.Text, userName);

                Console.WriteLine(string.Format("1b.Mail is about sending to {0}", headId));
                Tool.SendMail(mailParameter, domainId);
                Console.WriteLine("Successfully sent!");
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Майл илгээхэд гаргахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Майл илгээхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Майл илгээхэд алдаа гарлаа!", ex);
            }
            finally
            {
                mailParameter = null;
                domainId = branchId = headId = userName = null;
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
                    if (MainPage.allUser.Select(string.Format("BR_ID = {0}", control.EditValue)).Length.Equals(0))
                    {
                        lkUpMoveStaff.Properties.DataSource = null;
                        lkUpMoveStaff.EditValue = null;
                    }
                    else lkUpMoveStaff.Properties.DataSource = MainPage.allUser.Select(string.Format("BR_ID = {0}", control.EditValue)).CopyToDataTable();
                }
                else if (control.Tag.Equals(2))
                {
                    if (control.EditValue == null) return;
                    dateReturn.DateTime = DateTime.Now;
                    dateReturn.DateTime = dateReturn.DateTime.AddDays(Tool.GetDateTypeDay(control.EditValue));
                    dateReturn.Properties.ReadOnly = !control.EditValue.ToString().Equals("5");
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
                else if (btn.Tag.Equals(1))
                    scannedFileName = Tool.SetPdf();
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

        private void wizardControl_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (e.Page.Tag == null)
                return;
            LayoutControl currentlc = null;
            LayoutControlItem currentItem = null;
            TextEdit txtCtrl = null;
            LookUpEdit lkUpCtrl = null;
            MemoEdit memoCtrl = null;
            DateEdit dateCtrl = null;
            bool isActionProgress = false;
            int number = 0;
            try
            {
                if (e.Page.Tag.Equals(1))
                {
                    if (!ckIsMovement.Checked)
                        return;

                    if (lkUpMoveBranch.EditValue == null)
                    {
                        lkUpMoveBranch.ErrorText = "Газар, албыг тодорхойлно уу.";
                        if (!isActionProgress)
                        {
                            lkUpMoveBranch.Focus();
                            isActionProgress = !isActionProgress;
                        }
                    }
                    if (lkUpMoveStaff.EditValue == null)
                    {
                        lkUpMoveStaff.ErrorText = lkUpMoveStaff.Tag.ToString();
                        if (!isActionProgress)
                        {
                            lkUpMoveStaff.Focus();
                            isActionProgress = !isActionProgress;
                        }
                    }
                    if (dateMovement.EditValue == null)
                    {
                        dateMovement.ErrorText = dateMovement.Tag.ToString();
                        if (!isActionProgress)
                        {
                            dateMovement.Focus();
                            isActionProgress = !isActionProgress;
                        }
                    }
                    if (isActionProgress)
                        e.Handled = true;
                }
                else
                {
                    if (e.Page.Controls.Count.Equals(0)) return;
                    if (!(e.Page.Controls[0] is LayoutControl)) return;
                    currentlc = e.Page.Controls[0] as LayoutControl;
                    foreach (var entry in ((System.Collections.CollectionBase)(currentlc.Items)))
                    {
                        if (entry is LayoutControlItem)
                        {
                            txtCtrl = null;
                            lkUpCtrl = null;
                            memoCtrl = null;
                            dateCtrl = null;
                            currentItem = entry as LayoutControlItem;
                            if (currentItem.Control == null) continue;
                            if (currentItem.Control.Tag == null) continue;
                            if (int.TryParse(currentItem.Control.Tag.ToString(), out number)) continue;
                            if (currentItem.Control.ToString().Equals("DevExpress.XtraEditors.TextEdit"))
                                txtCtrl = currentItem.Control as TextEdit;
                            else if (currentItem.Control.ToString().Equals("DevExpress.XtraEditors.LookUpEdit"))
                                lkUpCtrl = currentItem.Control as LookUpEdit;
                            else if (currentItem.Control.ToString().Equals("DevExpress.XtraEditors.MemoEdit"))
                                memoCtrl = currentItem.Control as MemoEdit;
                            else if (currentItem.Control.ToString().Equals("DevExpress.XtraEditors.DateEdit"))
                                dateCtrl = currentItem.Control as DateEdit;
                            else continue;
                            if (txtCtrl != null)
                            {
                                if (string.IsNullOrEmpty(txtCtrl.Text))
                                {
                                    if (!txtCtrl.Enabled && !isActionProgress)
                                    {
                                        lkUpLocation.Focus();
                                        isActionProgress = !isActionProgress;
                                    }

                                    if (!isActionProgress)
                                    {
                                        txtCtrl.Focus();
                                        isActionProgress = !isActionProgress;
                                    }
                                    txtCtrl.ErrorText = txtCtrl.Tag.ToString();
                                    e.Handled = true;
                                }
                            }
                            else if (lkUpCtrl != null)
                            {
                                if (lkUpCtrl.EditValue == null)
                                {
                                    lkUpCtrl.ErrorText = lkUpCtrl.Tag.ToString();
                                    if (!isActionProgress)
                                    {
                                        lkUpCtrl.Focus();
                                        isActionProgress = !isActionProgress;
                                    }
                                    e.Handled = true;
                                }
                            }
                            else if (memoCtrl != null)
                            {
                                if (string.IsNullOrEmpty(memoCtrl.Text))
                                {
                                    memoCtrl.ErrorText = memoCtrl.Tag.ToString();
                                    if (!isActionProgress)
                                    {
                                        memoCtrl.Focus();
                                        isActionProgress = !isActionProgress;
                                    }
                                    e.Handled = true;
                                }
                            }
                            else if (dateCtrl != null)
                            {
                                if (dateCtrl.EditValue == null)
                                {
                                    dateCtrl.ErrorText = dateCtrl.Tag.ToString();
                                    if (!isActionProgress)
                                    {
                                        dateCtrl.Focus();
                                        isActionProgress = !isActionProgress;
                                    }
                                    e.Handled = true;
                                }
                            }
                        }
                    }
                }
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
            finally { currentlc = null; currentItem = null; txtCtrl = null; lkUpCtrl = null; memoCtrl = null; }
        }

        private void wizardControl_FinishClick(object sender, CancelEventArgs e)
        {
            decimal newPkId;
            Dictionary<string, string> parameters = null;
            Dictionary<string, string> parameterWithMovement = null;
            Dictionary<string, string> filters = null;
            try
            {
                parameters = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(txtReplyNum.Text.Trim()))
                {
                    parameters.Add("STATUS", "'N'");
                    parameters.Add("CLOSEDDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateDoc.EditValue)));
                    parameters.Add("DOCNUM", string.Format("N'{0}'", txtDocNum.Text));

                    filters = new Dictionary<string, string>();
                    filters.Add("REGNUM", string.Format("='{0}'", txtReplyNum.Text));
                    SqlConnector.Update(dbName, "Document", parameters, filters);
                }

                newPkId = Tool.NewPkId();
                parameters.Clear();
                parameters.Add("PKID", newPkId.ToString());
                parameters.Add("REGNUM", txtRegisterNum.Text);
                parameters.Add("REGDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateRegistered.DateTime)));
                parameters.Add("DOCNUM", string.Format("N'{0}'", txtDocNum.Text));
                parameters.Add("DOCDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateDoc.DateTime)));

                if (!string.IsNullOrEmpty(memoControlDirection.Text))
                    parameters.Add("CONTROLDIRECTION", string.Format("N'{0}'", memoControlDirection.Text));

                parameters.Add("STAFFID", Tool.userStaffId.ToString());
                parameters.Add("TOSTAFFID", lkUpToStaff.EditValue.ToString());
                parameters.Add("TOBRID", MainPage.allHead.Select(string.Format("ST_ID = {0}", lkUpToStaff.EditValue))[0]["BR_ID"].ToString());
                parameters.Add("DOCNOTEPKID", lkUpDocNoteType.EditValue.ToString());
                parameters.Add("ORGANIZATIONTYPEPKID", lkUpOrganization.EditValue.ToString());
                parameters.Add("INFROMWHO", string.Format("N'{0}'", txtFromWho.Text));
                parameters.Add("ISREPLYDOC", "'Y'");
                parameters.Add("SHORTDESC", string.Format("N'{0}'", memoDesc.Text));
                parameters.Add("PAGENUM", txtPageNum.Text);
                parameters.Add("CREATEDDATE", string.Format("'{0}'", Tool.DateTimeNow()));

                parameters.Add("RETURNDATE", string.Format("N'{0}'", Tool.ConvertDateTime(dateReturn.DateTime)));
                parameters.Add("CONTROLNUM", txtControlNum.Text);

                if (!string.IsNullOrEmpty(scannedFileName))
                    parameters.Add("SCANNEDFILE", string.Format("N'{0}'", scannedFileName));

                SqlConnector.Insert(dbName, "Document", parameters);

                if (ckIsMovement.Checked)
                {
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
                    parameterWithMovement.Add("CREATEDDATE", string.Format("'{0}'", Tool.ConvertDateTime(dateMovement.DateTime)));
                    SqlConnector.Insert(dbName, "Document", parameterWithMovement);
                }
                SendMail();
                if (ckIsPrint.Checked)
                    Print();
                //Tool.ShowSuccess("Мэдээллийг амжилттай хадгаллаа!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (MofException ex)
            {
                if (ex.errorCode.Equals(2627))
                {
                    txtRegisterNum.Text = SqlConnector.GetFunction(dbName, "GetRegNum").ToString();
                    wizardControl_FinishClick(this, null);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Бүртгэлийг хадгалахад алдаа гарлаа: " + ex.InnerException.Message);
                    Tool.ShowError(ex.Message, ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Бүртгэлийг хадгалахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Бүртгэлийг хадгалахад алдаа гарлаа!", ex.Message);
            }
            finally { parameters = null; parameterWithMovement = null; filters = null; }
        }

        private void wizardControl_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            LayoutControl currentlc = null;
            LayoutControlItem currentItem = null;
            try
            {
                if (e.Page.Controls.Count.Equals(0)) return;
                if (!(e.Page.Controls[0] is LayoutControl)) return;
                currentlc = e.Page.Controls[0] as LayoutControl;
                foreach (var entry in ((System.Collections.CollectionBase)(currentlc.Items)))
                {
                    if (entry is LayoutControlItem)
                    {
                        currentItem = entry as LayoutControlItem;
                        if (currentItem.Control == null) continue;
                        currentItem.Control.Focus();
                        return;
                    }
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Бүртгэлийг хадгалахад алдаа гарлаа: " + ex.InnerException.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Бүртгэлийг хадгалахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Бүртгэлийг хадгалахад алдаа гарлаа!", ex.Message);
            }
            finally { currentlc = null; currentItem = null; }
        }

        private void ck_CheckedChanged(object sender, EventArgs e)
        {
            lcgMovementGroup.Enabled = ckIsMovement.Checked;
        }

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