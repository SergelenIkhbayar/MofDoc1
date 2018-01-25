using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MofDoc.Class;
using MofDoc.Forms.Page.Income.Card;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace MofDoc.Forms.Page.Income
{
    public partial class IncomeList : UserControl
    {

        #region Properties

        internal string FormCaption = null;
        private int year;
        private string dbName = null;
        private string xmlStr = null;
        internal List<Document> listDocument = null;
        internal List<Document> nonBranchDocument = null;
        private List<Document> currentDocument = null;
        private Search search = null;
        private Dictionary<string, object> searchData = null;

        #endregion

        #region Constructor

        public IncomeList(string xmlStr, string dbName, int year)
        {
            InitializeComponent();
            this.xmlStr = xmlStr;
            this.year = year;
            this.dbName = dbName;

            InitControl();
            InitTreeList();
            InitData();
            InitPermission();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            FormCaption = string.Format("{0}({1} - {2})", MofDoc.Properties.Resources.IncomeListCaption, Tool.ConvertNonTimeDateTime(DateTime.Now.AddDays(-3), "yyyy/MM/dd"), Tool.ConvertNonTimeDateTime(DateTime.Now, "yyyy/MM/dd"));

            nvItemRefresh.Tag = 0;
            nvItemRefresh.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemSearch.Tag = 1;
            nvItemSearch.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemDirectRegister.Tag = 2;
            nvItemDirectRegister.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemAddCard.Tag = 3;
            nvItemAddCard.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemDelete.Tag = 4;
            nvItemDelete.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemHistory.Tag = 5;
            nvItemHistory.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemAddDesc.Tag = 6;
            nvItemAddDesc.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemAddReply.Tag = 7;
            nvItemAddReply.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemMove.Tag = 8;
            nvItemMove.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemPrint.Tag = 9;
            nvItemPrint.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemAddFile.Tag = 10;
            nvItemAddFile.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemRenewal.Tag = 11;
            nvItemRenewal.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemPrintList.Tag = 12;
            nvItemPrintList.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemPrintBack.Tag = 13;
            nvItemPrintBack.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemLastList.Tag = 14;
            nvItemLastList.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            toolStripTotal.Click += new EventHandler(toolStrip_Click);
            toolStripTotal.Tag = 0;

            toolStripNoAnswer.Click += new EventHandler(toolStrip_Click);
            toolStripNoAnswer.Tag = 1;

            toolStripDecision.Click += new EventHandler(toolStrip_Click);
            toolStripDecision.Tag = 2;

            toolStripAnswer.Click += new EventHandler(toolStrip_Click);
            toolStripAnswer.Tag = 3;

            toolStripExpired.Click += new EventHandler(toolStrip_Click);
            toolStripExpired.Tag = 4;

            toolStripApp.Click += new EventHandler(toolStrip_Click);
            toolStripLblApp.Visible = toolStripApp.Visible = false;
            toolStripApp.Tag = 5;

            toolStripAppDecision.Click += new EventHandler(toolStrip_Click);
            toolStripLblAppDecision.Visible = toolStripAppDecision.Visible = false;
            toolStripAppDecision.Tag = 6;

            toolStripAppAnswer.Click += new EventHandler(toolStrip_Click);
            toolStripLblAppAnswer.Visible = toolStripAppAnswer.Visible = false;
            toolStripAppAnswer.Tag = 7;

            toolStripAppExpired.Click += new EventHandler(toolStrip_Click);
            toolStripLblAppExpired.Visible = toolStripAppExpired.Visible = false;
            toolStripAppExpired.Tag = 8;

            toolStripLblDecision.ForeColor = toolStripLblAppDecision.ForeColor = Color.Green;
            toolStripLblAnswer.ForeColor = toolStripLblAppAnswer.ForeColor = Color.DarkOrange;
            toolStripLblExpired.ForeColor = toolStripLblAppExpired.ForeColor = Color.Red;
            this.Disposed += new EventHandler(IncomeList_Disposed);
        }

        private void InitTreeList()
        {
            TreeListColumn newColumn = null;
            try
            {
                treeList.ParentFieldName = "ParentPkId";
                treeList.KeyFieldName = "PkId";
                treeList.OptionsSelection.MultiSelect = true;
                treeList.Appearance.FocusedRow.Font = new Font("Arial", 10, FontStyle.Bold);
                treeList.DoubleClick += new EventHandler(treeList_DoubleClick);
                treeList.KeyDown += new KeyEventHandler(treeList_KeyDown);
                treeList.NodeCellStyle += new GetCustomNodeCellStyleEventHandler(treeList_NodeCellStyle);
                treeList.Columns.Clear();

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Бүртгэлийн №";
                newColumn.Name = newColumn.FieldName = "RegNum";
                newColumn.Width = 30;
                newColumn.SortOrder = System.Windows.Forms.SortOrder.Descending;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хяналтын №";
                newColumn.Name = newColumn.FieldName = "ControlNum";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Бичгийн №";
                newColumn.Name = newColumn.FieldName = "DocNum";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Бүртгэсэн огноо";
                newColumn.Name = newColumn.FieldName = "RegDate";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Бичгийн огноо";
                newColumn.Name = newColumn.FieldName = "DocDate";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хуудас";
                newColumn.Name = newColumn.FieldName = "PageNum";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хүлээн авсан ажилтан";
                newColumn.Name = newColumn.FieldName = "Name";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хаанаас";
                newColumn.Name = newColumn.FieldName = "Location";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Аль байгууллагаас";
                newColumn.Name = newColumn.FieldName = "OrganizationName";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хэнээс ирсэн";
                newColumn.Name = newColumn.FieldName = "InFromWho";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хэнд";
                newColumn.Name = newColumn.FieldName = "ToName";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хэлтэс";
                newColumn.Name = newColumn.FieldName = "Branch";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Ялгаа";
                newColumn.Name = newColumn.FieldName = "DocNoteType";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Товч утга";
                newColumn.Name = newColumn.FieldName = "ShortDesc";
                newColumn.Width = 50;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Төлөв";
                newColumn.Name = newColumn.FieldName = "Status";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хариу өгөх огноо";
                newColumn.Name = newColumn.FieldName = "ReturnDate";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Хаасан огноо";
                newColumn.Name = newColumn.FieldName = "ClosedDate";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "PKID";
                newColumn.Name = newColumn.FieldName = "PkId";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = false;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "ParentPkId";
                newColumn.Name = newColumn.FieldName = "ParentPkId";
                newColumn.Width = 30;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = false;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Файл";
                newColumn.Name = newColumn.FieldName = "ScannedFile";
                newColumn.Width = 30;
                newColumn.OptionsColumn.ReadOnly = true;
                newColumn.Visible = true;
                newColumn.ColumnEdit = new RepositoryItemHyperLinkEdit();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("TreeList контролыг бэлдэхэд алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TreeList контролыг бэлдэхэд алдаа гарлаа: " + ex.Message);
                Tool.ShowError("TreeList контролыг бэлдэхэд алдаа гарлаа!", ex.InnerException.Message);
            }
            finally { newColumn = null; }
        }

        private void InitData()
        {
            Dictionary<string, object> parameters = null;
            DataSet ds = null;
            Document document = null;
            try
            {
                Tool.ShowWaiting();

                if (listDocument == null)
                    listDocument = new List<Document>();
                else
                    listDocument.Clear();

                parameters = new Dictionary<string, object>();
                parameters.Add("@XML", xmlStr);
                parameters.Add("@DocType", "I");
                ds = SqlConnector.GetStoredProcedure(dbName, "GetUserData", parameters, null);

                if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows == null || ds.Tables[0].Rows.Count.Equals(0))
                    return;
                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    document = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(),
                        reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(),
                        reader[19].ToString(), reader[20].ToString(), reader[21].ToString(), reader[22].ToString(), reader[23].ToString(), reader[24].ToString(), reader[25].ToString(), reader[26].ToString(), reader[27].ToString(), reader[28].ToString());
                    listDocument.Add(document);
                }

                currentDocument = listDocument.Where(t => DateTime.Now.AddDays(-3) <= t.RegDate && t.RegDate <= DateTime.Now).ToList();
                treeList.DataSource = currentDocument;

                toolStripNoAnswer.Text = listDocument.Count(t => t.Status && !t.IsReplyDoc && t.ParentPkId.Equals(null)).ToString();
                toolStripDecision.Text = listDocument.Count(t => !t.Status && t.ParentPkId.Equals(null)).ToString();

                toolStripApp.Text = listDocument.Count(t => t.ParentPkId.Equals(null) && t.DocNotePkId.Equals(decimal.Parse("47"))).ToString();
                toolStripAppDecision.Text = listDocument.Count(t => !t.Status && t.ParentPkId.Equals(null) && t.DocNotePkId.Equals(decimal.Parse("47"))).ToString();
                toolStripAppAnswer.Text = listDocument.Count(t => t.Status && t.IsReplyDoc && t.ParentPkId.Equals(null) && t.DocNotePkId.Equals(decimal.Parse("47"))).ToString();
                toolStripAppExpired.Text = listDocument.Count(t => t.Status && t.IsReplyDoc && t.ReturnDate <= DateTime.Now && t.ParentPkId.Equals(null) && t.DocNotePkId.Equals(decimal.Parse("47"))).ToString();

                if (nonBranchDocument == null)
                    nonBranchDocument = new List<Document>();
                else
                    nonBranchDocument.Clear();

                ds = SqlConnector.GetStoredProcedure(dbName, "GetUserReportNonBranch", parameters, null);
                if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows == null || ds.Tables[0].Rows.Count.Equals(0))
                    return;
                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    document = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(),
                        reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(),
                        reader[19].ToString(), reader[20].ToString(), reader[21].ToString(), reader[22].ToString(), reader[23].ToString(), reader[24].ToString(), reader[25].ToString(), reader[26].ToString(), reader[27].ToString(), reader[28].ToString());
                    nonBranchDocument.Add(document);
                }

                toolStripAnswer.Text = nonBranchDocument.Count(t => t.Status && t.IsReplyDoc).ToString();
                toolStripExpired.Text = nonBranchDocument.Count(t => t.Status && t.IsReplyDoc && t.ReturnDate <= DateTime.Now).ToString();
                toolStripTotal.Text = (listDocument.Count(t => t.Status && !t.IsReplyDoc && t.ParentPkId.Equals(null))
                    + listDocument.Count(t => !t.Status && t.ParentPkId.Equals(null))
                    + nonBranchDocument.Count(t => t.Status && t.IsReplyDoc)
                    + nonBranchDocument.Count(t => t.Status && t.IsReplyDoc && t.ReturnDate <= DateTime.Now)).ToString();

                if (search != null)
                    if (!search.IsDisposed && searchData != null)
                        SearchIncome(searchData);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хэрэглэгчийн харах боломжтой мэдээллийг өгөгдлийн сангаас авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хэрэглэгчийн харах боломжтой мэдээллийг өгөгдлийн сангаас авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Хэрэглэгчийн харах боломжтой мэдээллийг өгөгдлийн сангаас авчрахад алдаа гарлаа!", ex.Message);
            }
            finally { parameters = null; ds = null; document = null; Tool.CloseWaiting(); }
        }

        private void InitPermission()
        {
            if (Tool.permissionId.Equals(decimal.One)) return;
            nvItemDirectRegister.Enabled = false;
            nvItemAddCard.Enabled = false;
            nvItemDelete.Enabled = false;
            nvItemAddDesc.Enabled = false;
            nvItemAddFile.Enabled = false;
            nvItemRenewal.Enabled = false;

            if (Tool.permissionId.Equals(6))
            {
                toolStripLblApp.Visible = toolStripApp.Visible =
                    toolStripLblAppDecision.Visible = toolStripAppDecision.Visible =
                    toolStripLblAppAnswer.Visible = toolStripAppAnswer.Visible =
                    toolStripLblAppExpired.Visible = toolStripAppExpired.Visible = true;
                nvItemAddDesc.Enabled = nvItemRenewal.Enabled = true;
            }
        }

        internal void RefreshControl()
        {
            try
            {
                searchData = null;
                treeList.DataSource = currentDocument;
                treeList.RefreshDataSource();
                if (treeList.Nodes.FirstNode == null) return;
                treeList.Nodes.FirstNode.Expanded = true;
                treeList.SetFocusedNode(treeList.Nodes.FirstNode);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг дахин сэргээхэд алдаа гарлаа: " + ex.Message);
                throw new MofException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг дахин сэргээхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Мэдээллийг дахин сэргээхэд алдаа гарлаа!", ex);
            }
        }

        private void RefreshInnerControl()
        {
            try
            {
                InitData();
                treeList.RefreshDataSource();
                if (treeList.Nodes.FirstNode == null) return;
                treeList.Nodes.FirstNode.Expanded = true;
                treeList.SetFocusedNode(treeList.Nodes.FirstNode);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг дахин сэргээхэд алдаа гарлаа: " + ex.Message);
                throw new MofException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг дахин сэргээхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Мэдээллийг дахин сэргээхэд алдаа гарлаа!", ex);
            }
        }

        internal void SearchDocument()
        {
            try
            {
                if (treeList.DataSource == null)
                    return;
                if (search == null) { search = new Search(this.ParentForm, Enum.DocumentType.Income, dbName); }
                if (search.IsDisposed) { search = new Search(this.ParentForm, Enum.DocumentType.Income, dbName); }
                search.StartPosition = FormStartPosition.CenterParent;
                search.Focus();
                search.Show();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг хайхад алдаа гарлаа: " + ex.Message);
                throw new MofException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг хайхад алдаа гарлаа: " + ex.Message);
                throw new MofException("Мэдээллийг хайхад алдаа гарлаа!", ex);
            }
        }

        private bool? ChooseRegisterMode()
        {
            DirectRegisterMode direcRegisterMode = null;
            try
            {
                direcRegisterMode = new DirectRegisterMode();
                direcRegisterMode.StartPosition = FormStartPosition.CenterParent;
                direcRegisterMode.ShowDialog();
                return direcRegisterMode.isWizard;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Бүртгэх төрөл сонгоход алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Бүртгэх төрөл сонгоход алдаа гарлаа: " + ex.Message);
                throw new MofException("Бүртгэх төрөл сонгоход алдаа гарлаа!", ex);
            }
            finally { direcRegisterMode = null; }
        }

        internal void AddDirectRegister()
        {
            try { DirectRegisterFunc(ChooseRegisterMode()); }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шууд бүртгэлийг бүртгэхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шууд бүртгэлийг бүртгэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Шууд бүртгэлийг бүртгэхэд алдаа гарлаа!", ex);
            }
        }

        private void DirectRegisterFunc(bool? isWizard)
        {
            DirectRegisterWizard wizard = null;
            DirectRegister form = null;
            DialogResult dialogResult = DialogResult.Cancel;
            try
            {
                if (isWizard == null || isWizard.Equals(null))
                    return;
                if (isWizard.Equals(true))
                {
                    wizard = new DirectRegisterWizard(dbName);
                    wizard.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = wizard.ShowDialog();
                    if (dialogResult.Equals(DialogResult.Cancel))
                        return;
                    RefreshInnerControl();

                    dialogResult = MessageBox.Show(this.Parent, "Дахин мэдээлэл нэмэх үү?", "Шууд бүртгэл(Шинэ)", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult.Equals(DialogResult.Cancel))
                        return;
                    DirectRegisterFunc(true);
                }
                else
                {
                    form = new DirectRegister(dbName);
                    form.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = form.ShowDialog();
                    if (dialogResult.Equals(DialogResult.Cancel))
                        return;
                    RefreshInnerControl();

                    dialogResult = MessageBox.Show(this.Parent, "Дахин мэдээлэл нэмэх үү?", "Шууд бүртгэл(Хуучин)", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult.Equals(DialogResult.Cancel))
                        return;
                    DirectRegisterFunc(false);
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шууд бүртгэлд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шууд бүртгэлд алдаа гарлаа: " + ex.Message);
                throw new MofException("Шууд бүртгэлд алдаа гарлаа!", ex);
            }
            finally { wizard = null; form = null; isWizard = null; }
        }

        private void EditIncome()
        {
            bool IsReplyDoc = false;
            DialogResult dialogResult = DialogResult.Cancel;
            DirectRegister directRegister = null;
            CardRegister cardRegister = null;
            DataTable dataTab = null;
            try
            {
                if (Tool.permissionId.Equals(decimal.One) || Tool.permissionId.Equals(decimal.Parse("6"))) { }
                else if (treeList.FocusedNode.ParentNode == null)
                {
                    Tool.ShowInfo("Бичгийн эх хувийг засах боломжгүй. Засах бол бичгийн хэрэгт хандана уу.");
                    return;
                }
                else if (Tool.permissionId.Equals(2))
                {
                    if (!Tool.userBrId.Equals(decimal.Parse(treeList.FocusedNode["ToBrId"].ToString())))
                    {
                        Tool.ShowInfo("Зөвхөн өөрийн хэлтэст ирсэн мэдээллийг зассан уу.");
                        return;
                    }
                }
                else if (Tool.permissionId.Equals(3))
                {
                    dataTab = MainPage.branchInfo.Select(string.Format("BR_MAIN_ID1 = {0}", Tool.userMainBrId)).CopyToDataTable();
                    if (dataTab.Select(string.Format("BR_ID = {0}", treeList.FocusedNode["ToBrId"])).Length.Equals(0))
                    {
                        Tool.ShowInfo("Зөвхөн өөрийн газарт ирсэн мэдээллийг зассан уу.");
                        return;
                    }
                }
                else if (Tool.permissionId.Equals(4))
                {
                    if (MainPage.availableUsers.Select(string.Format("ST_ID = {0}", treeList.FocusedNode["ToStaffId"])).Length.Equals(0))
                    {
                        Tool.ShowInfo("Зөвхөн удирдах албанд ирсэн мэдээллийг зассан уу.");
                        return;
                    }
                }
                else if (Tool.permissionId.Equals(5))
                {
                    if (!Tool.userStaffId.Equals(decimal.Parse(treeList.FocusedNode["ToStaffId"].ToString())))
                    {
                        Tool.ShowInfo("Зөвхөн өөрт ирсэн мэдээллийг зассан уу.");
                        return;
                    }
                }
                else throw new MofException("Эрхийн код олдсонгүй!");

                if (treeList.FocusedNode == null) return;
                if (treeList.FocusedNode["PkId"] == null) return;
                if (string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString())) return;
                if (!bool.Parse(treeList.FocusedNode["Status"].ToString())) return;

                bool.TryParse(treeList.FocusedNode["IsReplyDoc"].ToString(), out IsReplyDoc);
                if (IsReplyDoc)
                {
                    cardRegister = new CardRegister(dbName, listDocument.SingleOrDefault(t => t.PkId.Equals(decimal.Parse(treeList.FocusedNode["PkId"].ToString()))));
                    cardRegister.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = cardRegister.ShowDialog();
                }
                else
                {
                    directRegister = new DirectRegister(dbName, listDocument.SingleOrDefault(t => t.PkId.Equals(decimal.Parse(treeList.FocusedNode["PkId"].ToString()))));
                    directRegister.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = directRegister.ShowDialog();
                }
                if (dialogResult.Equals(DialogResult.Cancel))
                    return;
                RefreshInnerControl();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Ирсэн бичиг засахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ирсэн бичиг засахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Ирсэн бичиг засахад алдаа гарлаа!", ex);
            }
            finally { directRegister = null; cardRegister = null; dataTab = null; }
        }

        internal void AddCard()
        {
            try { CardFunc(ChooseRegisterMode()); }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Карт бүртгэлийг бүртгэхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Карт бүртгэлийг бүртгэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Карт бүртгэлийг бүртгэхэд алдаа гарлаа!", ex);
            }
        }

        private void CardFunc(bool? isWizard)
        {
            CardRegister form = null;
            CardRegisterWizard wizard = null;
            DialogResult dialogResult = DialogResult.Cancel;
            try
            {
                if (isWizard == null || isWizard.Equals(null))
                    return;
                if (isWizard.Equals(true))
                {
                    wizard = new CardRegisterWizard(dbName);
                    wizard.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = wizard.ShowDialog();
                    if (dialogResult.Equals(DialogResult.Cancel))
                        return;
                    RefreshInnerControl();

                    dialogResult = MessageBox.Show(this.Parent, "Дахин мэдээлэл нэмэх үү?", "Карттай бүртгэл(Шинэ)", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult.Equals(DialogResult.Cancel))
                        return;
                    CardFunc(true);
                }
                else
                {
                    form = new CardRegister(dbName);
                    form.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = form.ShowDialog();
                    if (dialogResult.Equals(DialogResult.Cancel))
                        return;
                    RefreshInnerControl();

                    dialogResult = MessageBox.Show(this.Parent, "Дахин мэдээлэл нэмэх үү?", "Карттай бүртгэл(Хуучин)", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult.Equals(DialogResult.Cancel))
                        return;
                    CardFunc(false);
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Карттай бүртгэлд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Карттай бүртгэлд алдаа гарлаа: " + ex.Message);
                throw new MofException("Карттай бүртгэлд алдаа гарлаа!", ex);
            }
            finally { wizard = null; form = null; isWizard = null; }
        }

        private void DeleteIncome()
        {
            Dictionary<string, object> parameter = null;
            DialogResult dialogResult = System.Windows.Forms.DialogResult.Cancel;
            try
            {
                if (!Tool.permissionId.Equals(1)) return;
                if (treeList.FocusedNode == null) return;
                if (treeList.FocusedNode["PkId"] == null) return;
                if (string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString())) return;

                dialogResult = MessageBox.Show(this.Parent, "Мэдээллийг устгахдаа итгэлтэй байна уу?", "Ирсэн бичгийн бүртгэл устгах", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult.Equals(DialogResult.Cancel))
                    return;

                Tool.ShowWaiting();
                parameter = new Dictionary<string, object>();
                parameter.Add("@PKID", decimal.Parse(treeList.FocusedNode["PkId"].ToString()));
                SqlConnector.GetStoredProcedure(dbName, "DeleteIncome", parameter);
                Tool.ShowSuccess("Мэдээллийг амжилттай устгалаа!");
                RefreshInnerControl();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээлэл устгахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээлэл устгахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Мэдээлэл устгахад алдаа гарлаа!", ex);
            }
            finally { parameter = null; Tool.CloseWaiting(); }
        }

        private void History()
        {
            if (treeList.FocusedNode == null) return;
            treeList.FocusedNode.ExpandAll();
        }

        private void AddDesc()
        {
            if (treeList.FocusedNode == null) return;
            if (treeList.FocusedNode.ParentNode != null)
            {
                Tool.ShowInfo("Анх ирсэн эх хувийг сонгоно уу.");
                return;
            }
            if (treeList.FocusedNode["PkId"] == null) return;
            if (string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString())) return;
            if (!bool.Parse(treeList.FocusedNode["Status"].ToString())) return;

            Info.AddDescription AddDescription = null;
            DialogResult dialogResult = DialogResult.Cancel;
            try
            {
                AddDescription = new Info.AddDescription(dbName, decimal.Parse(treeList.FocusedNode["PkId"].ToString()), treeList.FocusedNode["ShortDesc"].ToString());
                AddDescription.StartPosition = FormStartPosition.CenterParent;
                dialogResult = AddDescription.ShowDialog();
                if (dialogResult.Equals(DialogResult.Cancel)) return;
                RefreshInnerControl();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлбар нэмэхэд алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлбар нэмэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлбар нэмэхэд алдаа гарлаа!", ex);
            }
            finally { AddDescription = null; }
        }

        private void AddReply()
        {
            AddReply addReply = null;
            AddDecision addDecision = null;
            DialogResult dialogResult = DialogResult.Cancel;
            bool isReplyDoc = false;
            decimal pkId;
            try
            {
                if (treeList.FocusedNode == null) return;
                if (treeList.FocusedNode.ParentNode != null)
                {
                    Tool.ShowInfo("Анх ирсэн эх хувийг сонгоно уу.");
                    return;
                }
                if (treeList.FocusedNode["PkId"] == null || string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString())) return;

                pkId = decimal.Parse(treeList.FocusedNode["PkId"].ToString());
                bool.TryParse(treeList.FocusedNode["IsReplyDoc"].ToString(), out isReplyDoc);
                if (isReplyDoc)
                {
                    if (!Tool.permissionId.Equals(1)) return;
                    addReply = new AddReply(dbName, listDocument.Single(t => t.PkId.Equals(pkId)));
                    if (addReply.IsDisposed) return;
                    addReply.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = addReply.ShowDialog();
                }
                else
                {
                    addDecision = new AddDecision(dbName, pkId);
                    addDecision.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = addDecision.ShowDialog();
                }
                if (dialogResult.Equals(DialogResult.Cancel)) return;
                RefreshInnerControl();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хариу бүртгэхэд алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хариу бүртгэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Хариу бүртгэхэд алдаа гарлаа!", ex);
            }
            finally { addReply = null; addDecision = null; }
        }

        private void Print()
        {
            if (treeList.FocusedNode == null) return;
            if (treeList.FocusedNode["PkId"] == null) return;
            if (string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString())) return;

            List<object> parameter = null;
            bool IsReplyDoc = false;
            int reportId = 0;
            string reportStr = "Шууд бүртгэлийн тайлан";
            Document doc = null;
            Document childDoc = null;
            try
            {
                bool.TryParse(treeList.FocusedNode["IsReplyDoc"].ToString(), out IsReplyDoc);
                doc = listDocument.Single(t => t.PkId.Equals(decimal.Parse(treeList.FocusedNode["PkId"].ToString())));
                if (listDocument.Exists(t => t.ParentPkId.Equals(decimal.Parse(treeList.FocusedNode["PkId"].ToString()))))
                    childDoc = listDocument.Single(t => t.ParentPkId.Equals(decimal.Parse(treeList.FocusedNode["PkId"].ToString())));
                if (IsReplyDoc)
                {
                    reportId = 1;
                    reportStr = "Карттай бүртгэлийн тайлан";
                    parameter = new List<object>() { doc.Name, Tool.ConvertNonTimeDateTime(doc.RegDate), doc.RegNum, doc.ControlNum.ToString(),
                        Tool.ConvertNonTimeDateTime(doc.DocDate), doc.DocNum, doc.PageNum.ToString(), doc.Location, doc.OrganizationName, doc.InFromWho, 
                        doc.ToName, doc.DocNoteType, doc.ShortDesc, doc.ControlDirection, childDoc == null ? "" : Tool.ConvertNonTimeDateTime(childDoc.CreatedDate), 
                        childDoc == null ? "" : childDoc.Branch, childDoc == null ? "" : childDoc.ToName, Tool.ConvertNonTimeDateTime(doc.ReturnDate) };
                }
                else
                {
                    parameter = new List<object>() { doc.Name, Tool.ConvertNonTimeDateTime( doc.RegDate), doc.RegNum, Tool.ConvertNonTimeDateTime(doc.DocDate), 
                        doc.DocNum, doc.PageNum.ToString(), doc.Location, doc.OrganizationName, doc.InFromWho, 
                        doc.ToName, doc.DocNoteType, doc.ShortDesc, doc.ControlDirection, 
                        childDoc == null ? "" : Tool.ConvertNonTimeDateTime(childDoc.CreatedDate), 
                        childDoc == null ? "" : childDoc.Branch, childDoc == null ? "" : childDoc.ToName };
                }
                Tool.PrintReport(reportId, reportStr, parameter);
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
            finally { parameter = null; reportStr = null; doc = null; childDoc = null; }
        }

        private void AddFile()
        {
            if (treeList.FocusedNode == null) return;
            if (treeList.FocusedNode.ParentNode != null)
            {
                Tool.ShowInfo("Анх ирсэн эх хувийг сонгоно уу.");
                return;
            }
            if (treeList.FocusedNode["PkId"] == null || 
                string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString())) return;
            if (!bool.Parse(treeList.FocusedNode["Status"].ToString())) return;

            Dictionary<string, string> parameters = null;
            string scannedFile = null;

            Dictionary<string, object> filterStored = null;
            List<string> tableNames = null;
            DataSet ds = null;
            try
            {
                scannedFile = Tool.SetPdf();
                if (string.IsNullOrEmpty(scannedFile)) return;

                parameters = new Dictionary<string, string>();
                parameters.Add("SCANNEDFILE", string.Format("N'{0}'", scannedFile));
                SqlConnector.UpdateByPkId(dbName, "Document", parameters, decimal.Parse(treeList.FocusedNode["PkId"].ToString()));

                filterStored = new Dictionary<string, object>();
                filterStored.Add("@PKID", decimal.Parse(treeList.FocusedNode["PkId"].ToString()));
                filterStored.Add("@IsFirst", "Y");
                tableNames = new List<string>() { "ChildrenDocument" };
                ds = SqlConnector.GetStoredProcedure(dbName, "GetChildrenIncome", filterStored, tableNames);
                if (ds.Tables["ChildrenDocument"] == null) goto Finish;
                if (ds.Tables["ChildrenDocument"].Rows == null || ds.Tables["ChildrenDocument"].Rows.Count.Equals(0)) goto Finish;

                foreach (DataRow dr in ds.Tables["ChildrenDocument"].Rows)
                    SqlConnector.UpdateByPkId(dbName, "Document", parameters, decimal.Parse(dr["PKID"].ToString()));

            Finish:
                RefreshInnerControl();
            }
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
            finally { parameters = null; scannedFile = null; filterStored = null; tableNames = null; ds = null; }
        }

        private void RenewalFunc()
        {
            if (treeList.FocusedNode == null) return;
            if (treeList.FocusedNode.ParentNode != null)
            {
                Tool.ShowInfo("Анх ирсэн эх хувийг сонгоно уу.");
                return;
            }
            if (treeList.FocusedNode["PkId"] == null || string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString()) || 
                !bool.Parse(treeList.FocusedNode["IsReplyDoc"].ToString()) || treeList.FocusedNode["ReturnDate"] == null ||
                string.IsNullOrEmpty(treeList.FocusedNode["ReturnDate"].ToString()) || 
                !bool.Parse(treeList.FocusedNode["Status"].ToString())) return;

            Renewal renewal = null;
            DialogResult dialogResult = DialogResult.Cancel;
            try
            {
                renewal = new Renewal(dbName, decimal.Parse(treeList.FocusedNode["PkId"].ToString()), DateTime.Parse(treeList.FocusedNode["ReturnDate"].ToString()));
                renewal.StartPosition = FormStartPosition.CenterParent;
                dialogResult = renewal.ShowDialog();
                if (dialogResult.Equals(DialogResult.Cancel)) return;
                RefreshInnerControl();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Сунгалт хийхэд алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Сунгалт хийхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Сунгалт хийхэд алдаа гарлаа!", ex);
            }
            finally { renewal = null; }
        }

        internal void SearchIncome(Dictionary<string, object> searchData)
        {
            List<Document> cloneDocument = null;
            try
            {
                Tool.ShowWaiting();
                this.searchData = searchData;
                cloneDocument = listDocument;

                if (searchData.ContainsKey("RegNum"))
                    cloneDocument = cloneDocument.Where(t => t.RegNum.Equals(searchData.Single(y => y.Key.Equals("RegNum")).Value.ToString())).ToList();
                if (searchData.ContainsKey("ControlNum"))
                    cloneDocument = cloneDocument.Where(t => t.ControlNum.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("ControlNum")).Value.ToString()))).ToList();
                if (searchData.ContainsKey("DocNum"))
                    cloneDocument = cloneDocument.Where(t => t.DocNum.Contains(searchData.Single(y => y.Key.Equals("DocNum")).Value.ToString())).ToList();
                if (searchData.ContainsKey("PageNum"))
                    cloneDocument = cloneDocument.Where(t => t.PageNum.Equals(int.Parse(searchData.Single(y => y.Key.Equals("PageNum")).Value.ToString()))).ToList();

                if (searchData.ContainsKey("DocNoteType"))
                    cloneDocument = cloneDocument.Where(t => t.DocNotePkId.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("DocNoteType")).Value.ToString()))).ToList();
                if (searchData.ContainsKey("ShortDesc"))
                    cloneDocument = cloneDocument.Where(t => t.ShortDesc.Contains(searchData.Single(y => y.Key.Equals("ShortDesc")).Value.ToString())).ToList();

                if (searchData.ContainsKey("LocationPkId"))
                    cloneDocument = cloneDocument.Where(t => t.LocationPkId.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("LocationPkId")).Value.ToString())) || 
                        t.OrganizationTypePkId.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("LocationPkId")).Value.ToString()))).ToList();
                if (searchData.ContainsKey("OrganizationTypePkId"))
                    cloneDocument = cloneDocument.Where(t => t.OrganizationTypePkId.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("OrganizationTypePkId")).Value.ToString()))).ToList();
                if (searchData.ContainsKey("InFromWho"))
                    cloneDocument = cloneDocument.Where(t => t.InFromWho.Contains(searchData.Single(y => y.Key.Equals("InFromWho")).Value.ToString())).ToList();

                if (searchData.ContainsKey("ToBrId"))
                    cloneDocument = cloneDocument.Where(t => t.ToBrId.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("ToBrId")).Value.ToString()))).ToList();
                if (searchData.ContainsKey("StaffId"))
                    cloneDocument = cloneDocument.Where(t => t.StaffId.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("StaffId")).Value.ToString()))).ToList();
                if (searchData.ContainsKey("ToStaffId"))
                    cloneDocument = cloneDocument.Where(t => t.ToStaffId.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("ToStaffId")).Value.ToString()))).ToList();

                if (searchData.ContainsKey("RegStart") && searchData.ContainsKey("RegEnd"))
                    cloneDocument = cloneDocument.Where(t => 
                        t.RegDate >= DateTime.Parse(searchData.Single(y => y.Key.Equals("RegStart")).Value.ToString()) &&
                        t.RegDate <= DateTime.Parse(searchData.Single(z => z.Key.Equals("RegEnd")).Value.ToString())).ToList();
                if (searchData.ContainsKey("DocDateStart") && searchData.ContainsKey("DocDateEnd"))
                    cloneDocument = cloneDocument.Where(t =>
                        t.DocDate >= DateTime.Parse(searchData.Single(y => y.Key.Equals("DocDateStart")).Value.ToString()) &&
                        t.DocDate <= DateTime.Parse(searchData.Single(z => z.Key.Equals("DocDateEnd")).Value.ToString())).ToList();
                if (searchData.ContainsKey("MoveDateStart") && searchData.ContainsKey("MoveDateEnd"))
                    cloneDocument = cloneDocument.Where(t =>
                        t.ParentPkId != null &&
                        t.CreatedDate >= DateTime.Parse(searchData.Single(y => y.Key.Equals("MoveDateStart")).Value.ToString()) &&
                        t.CreatedDate <= DateTime.Parse(searchData.Single(z => z.Key.Equals("MoveDateEnd")).Value.ToString())).ToList();

                treeList.DataSource = cloneDocument;
                treeList.RefreshDataSource();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлт хийхэд алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлт хийхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Хайлт хийхэд алдаа гарлаа!", ex);
            }
            finally { cloneDocument = null; Tool.CloseWaiting(); }
        }

        private void PrintList()
        {
            try
            {
                treeList.ExpandAll();
                treeList.ShowRibbonPrintPreview();
                treeList.CollapseAll();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Жагсаалтыг хэвлэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Жагсаалтыг хэвлэхэд алдаа гарлаа!", ex);
            }
        }

        private void PrintBack()
        {
            try { Tool.PrintReport(14, "Карттай бичгийн ар талын хэсэг", null); }
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
        }

        private void GetLastDocument()
        {
            List<Document> listLastDoc = null;
            Dictionary<string, object> parameters = null;
            try
            {
                Tool.ShowWaiting();
                if (listDocument.Count.Equals(0)) return;

                parameters = new Dictionary<string, object>();
                parameters.Add("@XML", xmlStr);
                parameters.Add("@DocType", "I");

                listLastDoc = Tool.GetDocumentFromDb(dbName, parameters, true);
                listLastDoc = Tool.GetLastDocument(listLastDoc);
                listLastDoc = listLastDoc.Where(t => t.Status && t.ClosedDate == null).ToList();
                if (listLastDoc.Count.Equals(0))
                {
                    Tool.ShowInfo("Бичгийн мэдээлэл хоосон байна!");
                    return;
                }
                treeList.DataSource = listLastDoc;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Сүүлийн хүнээр харахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Сүүлийн хүнээр харахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Сүүлийн хүнээр харахад алдаа гарлаа!", ex);
            }
            finally
            {
                listLastDoc = null;
                parameters = null;
                Tool.CloseWaiting();
            }
        }

        #endregion

        #region Event

        private void nvItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                int caseSwitch = int.Parse((sender as DevExpress.XtraNavBar.NavBarItem).Tag.ToString());
                switch (caseSwitch)
                {
                    case 0:
                        RefreshInnerControl();
                        break;
                    case 1:
                        SearchDocument();
                        break;
                    case 2:
                        AddDirectRegister();
                        break;
                    case 3:
                        AddCard();
                        break;
                    case 4:
                        DeleteIncome();
                        break;
                    case 5:
                        History();
                        break;
                    case 6:
                        AddDesc();
                        break;
                    case 7:
                        AddReply();
                        break;
                    case 8:
                        EditIncome();
                        break;
                    case 9:
                        Print();
                        break;
                    case 10:
                        AddFile();
                        break;
                    case 11:
                        RenewalFunc();
                        break;
                    case 12:
                        PrintList();
                        break;
                    case 13:
                        PrintBack();
                        break;
                    case 14:
                        GetLastDocument();
                        break;
                    default:
                        throw new MofException("Товчны код тодорхой бус байна!");
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Цэсний товч дарахад алдаа гарлаа: " + ex.InnerException.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Цэсний товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Цэсний товч дарахад алдаа гарлаа!", ex.Message);
            }
        }

        private void treeList_DoubleClick(object sender, EventArgs e)
        {
            try { EditIncome(); }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг засахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг засахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Мэдээллийг засахад алдаа гарлаа!", ex.Message);
            }
        }

        private void treeList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Delete) || e.KeyCode == Keys.Delete)
                    DeleteIncome();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг устгахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг устгахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Мэдээллийг устгахад алдаа гарлаа!", ex.Message);
            }
        }

        private void treeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList treeList = sender as TreeList;
            Document currentDoc = null;
            try
            {
                if (!(treeList.GetDataRecordByNode(e.Node) is Document)) return;
                currentDoc = treeList.GetDataRecordByNode(e.Node) as Document;
                if (currentDoc == null) return;

                if (!currentDoc.Status)
                    e.Appearance.ForeColor = Color.Green;
                else
                {
                    if (currentDoc.IsReplyDoc && DateTime.Compare(DateTime.Now, (DateTime)currentDoc.ReturnDate) > 0)
                        e.Appearance.ForeColor = Color.Red;
                    else if (currentDoc.IsReplyDoc && DateTime.Compare(DateTime.Now, (DateTime)currentDoc.ReturnDate) < 0)
                        e.Appearance.ForeColor = Color.DarkOrange;
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хүснэгтэнд өгөгдлийг өнгөөр ялгахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хүснэгтэнд өгөгдлийг өнгөөр ялгахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Хүснэгтэнд өгөгдлийг өнгөөр ялгахад алдаа гарлаа!", ex.Message);
            }
            finally { treeList = null; currentDoc = null; }
        }

        private void IncomeList_Disposed(object sender, EventArgs e)
        {
            if (search == null) return;
            search.Dispose();
            search = null;
        }

        private void toolStrip_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripStatusLabel).Tag == null) return;
            int tagId;
            try
            {
                if (search != null)
                {
                    search.Dispose();
                    search = null;
                }
                Tool.ShowWaiting();
                tagId = int.Parse((sender as ToolStripStatusLabel).Tag.ToString());
                switch (tagId)
                {
                    case 0:
                        {
                            InitData();
                            break;
                        }
                    case 1:
                        {
                            treeList.DataSource = Tool.GetOrderRelationList(listDocument.Where(t => t.Status && !t.IsReplyDoc && t.ParentPkId.Equals(null)).ToList());
                            break;
                        }
                    case 2:
                        {
                            treeList.DataSource = Tool.GetOrderRelationList(listDocument.Where(t => !t.Status && t.ParentPkId.Equals(null)).ToList());
                            break;
                        }
                    case 3:
                        {
                            treeList.DataSource = nonBranchDocument.Where(t => t.Status && t.IsReplyDoc).ToList();
                            break;
                        }
                    case 4:
                        {
                            treeList.DataSource = nonBranchDocument.Where(t => t.Status && t.IsReplyDoc && t.ReturnDate <= DateTime.Now).ToList();
                            break;
                        }
                    case 5:
                        {
                            treeList.DataSource = Tool.GetOrderRelationList(listDocument.Where(t => t.ParentPkId.Equals(null) && t.DocNotePkId.Equals(decimal.Parse("47"))).ToList());
                            break;
                        }
                    case 6:
                        {
                            treeList.DataSource = Tool.GetOrderRelationList(listDocument.Where(t => !t.Status && t.ParentPkId.Equals(null) && t.DocNotePkId.Equals(decimal.Parse("47"))).ToList());
                            break;
                        }
                    case 7:
                        {
                            treeList.DataSource = Tool.GetOrderRelationList(listDocument.Where(t => t.Status && t.IsReplyDoc && t.ParentPkId.Equals(null) && t.DocNotePkId.Equals(decimal.Parse("47"))).ToList());
                            break;
                        }
                    case 8:
                        {
                            treeList.DataSource = Tool.GetOrderRelationList(listDocument.Where(t => t.Status && t.IsReplyDoc && t.ReturnDate <= DateTime.Now && t.ParentPkId.Equals(null) && t.DocNotePkId.Equals(decimal.Parse("47"))).ToList());
                            break;
                        }
                    default:
                        throw new MofException("Товчны код тодорхой бус байна!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ToolStrip контролыг дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("ToolStrip контролыг дарахад алдаа гарлаа!", ex.InnerException.Message);
            }
            finally { Tool.CloseWaiting(); }
        }

        #endregion

    }
}