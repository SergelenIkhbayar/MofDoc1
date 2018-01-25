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
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace MofDoc.Forms.Page.Outcome
{
    public partial class OutcomeList : UserControl
    {

        #region Properties

        internal string FormCaption = null;
        private int year;
        private string dbName = null;
        private string xmlStr = null;
        private List<Document> listDocument = null;
        private List<Document> currentDocument = null;
        private DialogResult dialogResult = DialogResult.Cancel;
        private Search search = null;
        private Dictionary<string, object> searchData = null;

        #endregion

        #region Constructor

        public OutcomeList(string xmlStr, string dbName, int year)
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
            FormCaption = FormCaption = string.Format("{0}({1} - {2})", MofDoc.Properties.Resources.OutcomeListCaption, Tool.ConvertNonTimeDateTime(DateTime.Now.AddDays(-3), "yyyy/MM/dd"), Tool.ConvertNonTimeDateTime(DateTime.Now, "yyyy/MM/dd"));
            listDocument = new List<Document>();

            nvItemRefresh.Tag = 0;
            nvItemRefresh.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemSearch.Tag = 1;
            nvItemSearch.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemAddOutcome.Tag = 2;
            nvItemAddOutcome.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemOutcomeDelete.Tag = 3;
            nvItemOutcomeDelete.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemOutcomeEdit.Tag = 4;
            nvItemOutcomeEdit.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            nvItemPrintList.Tag = 5;
            nvItemPrintList.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nvItem_LinkClicked);

            toolStripLblAnswer.ForeColor = Color.DarkOrange;
            toolStripLblDecision.ForeColor = Color.Green;
            toolStripLblExpired.ForeColor = Color.Red;
            this.Disposed += new EventHandler(OutcomeList_Disposed);
        }

        private void InitTreeList()
        {
            TreeListColumn newColumn = null;
            try
            {
                treeList.ParentFieldName = "ParentPkId";
                treeList.KeyFieldName = "PkId";
                treeList.OptionsSelection.MultiSelect = true;
                treeList.Appearance.FocusedRow.Font = new Font("Arial", 9, FontStyle.Bold);
                treeList.DoubleClick += new EventHandler(treeList_DoubleClick);
                treeList.KeyDown += new KeyEventHandler(treeList_KeyDown);
                treeList.Columns.Clear();

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Бүртгэлийн №";
                newColumn.Name = newColumn.FieldName = "RegNum";
                newColumn.Width = 30;
                newColumn.SortOrder = System.Windows.Forms.SortOrder.Descending;
                newColumn.OptionsColumn.AllowEdit = false;
                newColumn.Visible = true;

                newColumn = treeList.Columns.Add();
                newColumn.Caption = "Бүртгэсэн огноо";
                newColumn.Name = newColumn.FieldName = "RegDate";
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
                newColumn.Caption = "Хариу өгөх огноо";
                newColumn.Name = newColumn.FieldName = "ReturnDate";
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
                parameters.Add("@DocType", "O");
                ds = SqlConnector.GetStoredProcedure(dbName, "GetUserData", parameters, null);

                if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows == null || ds.Tables[0].Rows.Count.Equals(0))
                    return;
                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    document = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), 
                        reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), 
                        reader[16].ToString(), reader[17].ToString(), reader[18].ToString(), reader[19].ToString(), reader[20].ToString(), reader[21].ToString(), reader[22].ToString(), reader[23].ToString(), 
                        reader[24].ToString(), reader[25].ToString(), reader[26].ToString(), reader[27].ToString(), reader[28].ToString());
                    listDocument.Add(document);
                }
                currentDocument = listDocument.Where(t => DateTime.Now.AddDays(-3) <= t.RegDate && t.RegDate <= DateTime.Now).ToList();
                treeList.DataSource = currentDocument;

                toolStripTotal.Text = listDocument.Count.ToString();
                toolStripNonAnswer.Text = listDocument.Count(t => t.Status && !t.IsReplyDoc && t.ParentPkId.Equals(null)).ToString();
                toolStripDecision.Text = listDocument.Count(t => !t.Status && t.ParentPkId.Equals(null)).ToString();
                toolStripAnswer.Text = listDocument.Count(t => t.Status && t.IsReplyDoc && t.ParentPkId.Equals(null)).ToString();
                toolStripExpired.Text = listDocument.Count(t => t.Status && t.IsReplyDoc && t.ReturnDate <= DateTime.Now && t.ParentPkId.Equals(null)).ToString();

                if (search != null)
                    if (!search.IsDisposed && searchData != null)
                        SearchOutcome(searchData);

                /*ds = SqlConnector.GetStoredProcedure(dbName, "GetStatusOutcome", null, null);
                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    if (reader[0].ToString().Equals("Total"))
                        toolStripTotal.Text = reader[1].ToString();
                    else if (reader[0].ToString().Equals("NoAnswer"))
                        toolStripNonAnswer.Text = reader[1].ToString();
                    else if (reader[0].ToString().Equals("Answer"))
                        toolStripAnswer.Text = reader[1].ToString();
                    else if (reader[0].ToString().Equals("Decision"))
                        toolStripDecision.Text = reader[1].ToString();
                    else if (reader[0].ToString().Equals("Expired"))
                        toolStripExpired.Text = reader[1].ToString();
                    else throw new MofException("Бичгийн төрөл олдсонгүй!");
                }*/
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
            nvItemAddOutcome.Enabled = false;
            nvItemOutcomeDelete.Enabled = false;
            nvItemOutcomeEdit.Enabled = false;
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

        private void RefreshControlInner()
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
                if (search == null) { search = new Search(this.ParentForm, Enum.DocumentType.Outcome, dbName); }
                if (search.IsDisposed) { search = new Search(this.ParentForm, Enum.DocumentType.Outcome, dbName); }
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

        internal void AddOutcome()
        {
            OutcomeRegister outcomeRegister = null;
            try
            {
                outcomeRegister = new OutcomeRegister(dbName);
                if (outcomeRegister.IsDisposed) return;
                outcomeRegister.StartPosition = FormStartPosition.CenterParent;
                dialogResult = outcomeRegister.ShowDialog();
                if (dialogResult.Equals(DialogResult.Cancel)) return;
                RefreshControlInner();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичгийн мэдээллийг нэмэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичгийн мэдээллийг нэмэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Явсан бичгийн мэдээллийг нэмэхэд алдаа гарлаа!", ex);
            }
            finally { outcomeRegister = null; dialogResult = DialogResult.Cancel; }
        }

        internal void DeleteOutcome()
        {
            DialogResult dialogResult = System.Windows.Forms.DialogResult.Cancel;
            try
            {
                if (!Tool.permissionId.Equals(1)) return;
                if (treeList.FocusedNode == null) return;
                if (treeList.FocusedNode["PkId"] == null) return;
                if (string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString())) return;
                if (!bool.Parse(treeList.FocusedNode["Status"].ToString())) return;

                dialogResult = MessageBox.Show(this.Parent, "Мэдээллийг устгахдаа итгэлтэй байна уу?", "Явсан бичгийн бүртгэл устгах", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult.Equals(DialogResult.Cancel))
                    return;

                Tool.ShowWaiting();
                SqlConnector.DeleteByPkId(dbName, "Document", decimal.Parse(treeList.FocusedNode["PkId"].ToString()));
                Tool.ShowSuccess("Мэдээллийг амжилттай устгалаа!");
                RefreshControlInner();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичиг устгахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичиг устгахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Явсан бичиг устгахад алдаа гарлаа!", ex);
            }
            finally { Tool.CloseWaiting(); }
        }

        private void EditOutcome()
        {
            DialogResult dialogResult = DialogResult.Cancel;
            OutcomeRegister outcomeRegister = null;
            DataTable dataTab = null;
            try
            {
                if (treeList.FocusedNode == null) return;
                if (treeList.FocusedNode["PkId"] == null) return;
                if (string.IsNullOrEmpty(treeList.FocusedNode["PkId"].ToString())) return;
                if (!bool.Parse(treeList.FocusedNode["Status"].ToString())) return;

                if (Tool.permissionId.Equals(decimal.One)) { }
                else if (treeList.FocusedNode.ParentNode == null)
                {
                    Tool.ShowInfo("Бичгийн эх хувийг засах боломжгүй. Засах бол бичгийн хэрэгт хандана уу.");
                    return;
                }
                else if (Tool.permissionId.Equals(2))
                {
                    if (!Tool.userBrId.Equals(decimal.Parse(treeList.FocusedNode["ToBrId"].ToString())))
                    {
                        Tool.ShowInfo("Зөвхөн өөрийн хэлтэстээс явсан мэдээллийг зассан уу.");
                        return;
                    }
                }
                else if (Tool.permissionId.Equals(3))
                {
                    dataTab = MainPage.branchInfo.Select(string.Format("BR_MAIN_ID1 = {0}", Tool.userMainBrId)).CopyToDataTable();
                    if (dataTab.Select(string.Format("BR_ID = {0}", treeList.FocusedNode["ToBrId"])).Length.Equals(0))
                    {
                        Tool.ShowInfo("Зөвхөн өөрийн газраас явсан мэдээллийг зассан уу.");
                        return;
                    }
                }
                else if (Tool.permissionId.Equals(4))
                {
                    if (MainPage.availableUsers.Select(string.Format("ST_ID = {0}", treeList.FocusedNode["ToStaffId"])).Length.Equals(0))
                    {
                        Tool.ShowInfo("Зөвхөн удирдах албаас явсан мэдээллийг зассан уу.");
                        return;
                    }
                }
                else if (Tool.permissionId.Equals(5))
                {
                    if (!Tool.userStaffId.Equals(decimal.Parse(treeList.FocusedNode["ToStaffId"].ToString())))
                    {
                        Tool.ShowInfo("Зөвхөн өөрийн илгээсэн мэдээллийг зассан уу.");
                        return;
                    }
                }
                else throw new MofException("Эрхийн код олдсонгүй!");

                outcomeRegister = new OutcomeRegister(dbName, listDocument.SingleOrDefault(t => t.PkId.Equals(decimal.Parse(treeList.FocusedNode["PkId"].ToString()))));
                if (outcomeRegister.IsDisposed) return;
                outcomeRegister.StartPosition = FormStartPosition.CenterParent;
                dialogResult = outcomeRegister.ShowDialog();
                if (dialogResult.Equals(DialogResult.Cancel))
                    return;
                RefreshControlInner();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичиг засахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Явсан бичиг засахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Явсан бичиг засахад алдаа гарлаа!", ex);
            }
            finally { outcomeRegister = null; dataTab = null; }
        }

        private void PrintList()
        {
            try { treeList.ShowRibbonPrintPreview(); }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Жагсаалтыг хэвлэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Жагсаалтыг хэвлэхэд алдаа гарлаа!", ex);
            }
        }

        internal void SearchOutcome(Dictionary<string, object> searchData)
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
                        RefreshControlInner();
                        break;
                    case 1:
                        SearchDocument();
                        break;
                    case 2:
                        AddOutcome();
                        break;
                    case 3:
                        DeleteOutcome();
                        break;
                    case 4:
                        EditOutcome();
                        break;
                    case 5:
                        PrintList();
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
            try { EditOutcome(); }
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
                    DeleteOutcome();
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

        private void OutcomeList_Disposed(object sender, EventArgs e)
        {
            if (search == null) return;
            search.Dispose();
            search = null;
        }

        #endregion

    }
}