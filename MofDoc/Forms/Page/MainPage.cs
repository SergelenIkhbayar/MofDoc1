using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MofDoc.Class;
using MofDoc.Enum;
using Oracle.ManagedDataAccess.Client;
using DevExpress.XtraTab;
using MofDoc.Forms.Page.Income;
using MofDoc.Forms.Page.Info;
using MofDoc.Forms.Page.Outcome;
using MofDoc.Forms.Page.Domestic;

namespace MofDoc.Forms.Page
{
    public partial class MainPage : UserControl
    {

        #region Properties

        private int year;
        private string domainId = null;
        private string dbName = null;
        internal static Dictionary<FormType, XtraTabPage> contentList = null;
        internal static DataTable allHead = null;
        internal static DataTable branchInfo = null;
        internal static DataTable allUser = null;
        internal static DataTable availableUsers = null;

        #endregion

        #region Constructor

        public MainPage(int year, string domainId, string dbName)
        {
            InitializeComponent();
            this.year = year;
            this.domainId = domainId;
            this.dbName = dbName;
            InitControl();
        }

        #endregion

        #region Function

        public void GetPermission()
        {
            DataSet ds = null;
            List<OracleParameter> inputParameter = null;
            List<OracleParameter> outputParameter = null;
            List<string> tableNames = null;
            try
            {
                Tool.ShowWaiting();
                inputParameter = new List<OracleParameter>();
                inputParameter.AddRange(new OracleParameter[]{
                    new OracleParameter("domain_user", OracleDbType.Varchar2, 200, domainId.ToLower(), ParameterDirection.Input)
                });

                outputParameter = new List<OracleParameter>();
                outputParameter.AddRange(new OracleParameter[]{
                    new OracleParameter("userInfo", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output),
                    new OracleParameter("branchInfo", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output),
                    new OracleParameter("outCursor", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output),
                    new OracleParameter("allStaff", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output),
                    new OracleParameter("allHead", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output)
                });

                tableNames = new List<string>();
                tableNames.Add("UserInfo");
                tableNames.Add("BranchInfo");
                tableNames.Add("AvailableUser");
                tableNames.Add("AllUser");
                tableNames.Add("AllHead");

                ds = OracleConnector.GetByProcedure("getUserInfo", inputParameter, outputParameter, tableNames);
                if (ds == null || ds.Tables == null ||
                    ds.Tables["UserInfo"] == null || ds.Tables["UserInfo"].Rows == null || ds.Tables["UserInfo"].Rows.Count.Equals(0) ||
                    ds.Tables["BranchInfo"] == null || ds.Tables["BranchInfo"].Rows == null || ds.Tables["BranchInfo"].Rows.Count.Equals(0) ||
                    ds.Tables["AvailableUser"] == null || ds.Tables["AvailableUser"].Rows == null || ds.Tables["AvailableUser"].Rows.Count.Equals(0) ||
                    ds.Tables["AllUser"] == null || ds.Tables["AllUser"].Rows == null || ds.Tables["AllUser"].Rows.Count.Equals(0))
                    throw new MofException("Өгөгдөл харах эрхгүй байна эсвэл хэрэглэгчийн мэдээлэл CMC буюу хүний нөөцийн системээс олдсонгүй!");

                allHead = ds.Tables["AllHead"];
                branchInfo = ds.Tables["BranchInfo"];
                allUser = ds.Tables["AllUser"];
                availableUsers = ds.Tables["AvailableUser"];
                Tool.userDomainId = domainId;
                Tool.userStaffId = decimal.Parse(ds.Tables["UserInfo"].Rows[0].ItemArray[0].ToString());
                Tool.userBrId = decimal.Parse(ds.Tables["UserInfo"].Rows[0].ItemArray[1].ToString());
                Tool.userMainBrId = decimal.Parse(ds.Tables["UserInfo"].Rows[0].ItemArray[2].ToString());
                Tool.userLName = ds.Tables["UserInfo"].Rows[0].ItemArray[3].ToString();
                Tool.userFName = ds.Tables["UserInfo"].Rows[0].ItemArray[4].ToString();
                Tool.permissionId = decimal.Parse(ds.Tables["UserInfo"].Rows[0].ItemArray[5].ToString());

                Tool.xmlStr = ds.GetXml();
                DrawContent(FormType.IncomeList);
                InitMailNotification();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Эрхийн мэдээлэлд алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Эрхийн мэдээлэлд алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Эрхийн мэдээлэлд алдаа гарлаа!", ex.Message);
            }
            finally
            {
                ds = null; inputParameter = null; outputParameter = null;
                tableNames = null; Tool.CloseWaiting();
            }
        }

        private void InitControl()
        {
            contentList = new Dictionary<FormType, XtraTabPage>();
            this.Tag = "MainPage";

            SubMenuItemQuit.Tag = 0;
            SubMenuItemQuit.Click += new EventHandler(SubMenuItem_Click);

            MenuItemIncome.Tag = 1;
            MenuItemIncome.Click += new EventHandler(SubMenuItem_Click);

            MenuItemOrganization.Tag = 2;
            MenuItemOrganization.Click += new EventHandler(SubMenuItem_Click);

            MenuItemOutcome.Tag = 3;
            MenuItemOutcome.Click += new EventHandler(SubMenuItem_Click);

            SubMenuIncomeReport.Tag = 4;
            SubMenuIncomeReport.Click += new EventHandler(SubMenuItem_Click);

            SubMenuOutcomeReport.Tag = 5;
            SubMenuOutcomeReport.Click += new EventHandler(SubMenuItem_Click);

            MenuItemDomestic.Tag = 6;
            MenuItemDomestic.Click += new EventHandler(SubMenuItem_Click);
            MenuItemDomestic.Enabled = !Tool.permissionId.Equals(4);
        }

        private void InitMailNotification()
        {
            MailParameter mailParam = null;
            List<MailParameter> mailParams = null;
            List<Document> listDocument = null;
            Document document = null;
            DataSet ds = null;
            Dictionary<string, string> parameters = null;
            Dictionary<string, object> parameterStored = null;
            DataTable dtable = null;
            string headId, domainId, userName, branchId = null;
            try
            {
                Tool.ShowWaiting();
                parameters = new Dictionary<string, string>();
                parameters.Add("SENTDATE LIKE ", string.Format("'{0}'", Tool.ConvertNonTimeDateTime(DateTime.Now)));
                dtable = SqlConnector.GetTable(dbName, "MailNotification", parameters);
                if (!dtable.Rows.Count.Equals(0)) return;

                parameterStored = new Dictionary<string, object>();
                parameterStored.Add("@XML", Tool.xmlStr);
                ds = SqlConnector.GetStoredProcedure(dbName, "GetMailNotification", parameterStored, null);
                if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows == null || ds.Tables[0].Rows.Count.Equals(0))
                    return;

                listDocument = new List<Document>();
                mailParams = new List<MailParameter>();
                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    document = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(),
                        reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(),
                        reader[19].ToString(), reader[20].ToString(), reader[21].ToString(), reader[22].ToString(), reader[23].ToString(), reader[24].ToString(), reader[25].ToString(), reader[26].ToString(), reader[27].ToString(), reader[28].ToString(),
                        reader[29].ToString());
                    listDocument.Add(document);
                }

                foreach (Document doc in listDocument)
                {
                    if (doc.ToStaffId.Equals(null)) continue;
                    if (MainPage.allUser.Select(string.Format("ST_ID = {0}", doc.ToStaffId)).Length == 0) continue;
                    domainId = MainPage.allUser.Select(string.Format("ST_ID = {0}", doc.ToStaffId))[0].ItemArray[4].ToString();
                    branchId = MainPage.allUser.Select(string.Format("ST_ID = {0}", doc.ToStaffId))[0].ItemArray[1].ToString();

                    if (MainPage.allHead.Select(string.Format("BR_ID = {0}", branchId)).Length.Equals(0))
                    {
                        if (MainPage.branchInfo.Select(string.Format("BR_ID = {0}", branchId)).Length.Equals(0))
                        {
                            headId = string.Empty;
                            userName = string.Empty;
                        }
                        else
                        {
                            branchId = MainPage.branchInfo.Select(string.Format("BR_ID = {0}", branchId))[0].ItemArray[2].ToString();
                            headId = MainPage.allHead.Select(string.Format("BR_ID = {0}", branchId))[0].ItemArray[4].ToString();
                            userName = MainPage.allHead.Select(string.Format("BR_ID = {0}", branchId))[0].ItemArray[3].ToString();
                        }
                    }
                    else
                    {
                        headId = MainPage.allHead.Select(string.Format("BR_ID = {0}", branchId))[0].ItemArray[4].ToString();
                        userName = MainPage.allHead.Select(string.Format("BR_ID = {0}", branchId))[0].ItemArray[3].ToString();
                    }

                    if (string.IsNullOrEmpty(domainId)) continue;
                    mailParam = new MailParameter(domainId, doc.RegNum, doc.ControlNum.ToString(), Tool.ConvertNonTimeDateTime(doc.DocDate),
                        doc.DocNum, string.Format("{0} - {1}", doc.Location , doc.OrganizationName), doc.InFromWho,
                        doc.ScannedFile, Tool.ConvertNonTimeDateTime(doc.ReturnDate), doc.ShortDesc, doc.ExpiredDays);
                    mailParam.toWhoSecond = doc.ToName;
                    mailParam.toWhoFirst = userName;
                    mailParams.Add(mailParam);

                    if (domainId.Equals(headId)) continue;
                    mailParam = new MailParameter(headId, doc.RegNum, doc.ControlNum.ToString(), Tool.ConvertNonTimeDateTime(doc.DocDate),
                        doc.DocNum, string.Format("{0} - {1}", doc.Location, doc.OrganizationName), doc.InFromWho,
                        doc.ScannedFile, Tool.ConvertNonTimeDateTime(doc.ReturnDate), doc.ShortDesc, doc.ExpiredDays);
                    mailParam.toWhoSecond = doc.ToName;
                    mailParam.toWhoFirst = userName;
                    mailParams.Add(mailParam);
                }
                Tool.SendMail(mailParams);
                parameters.Clear();
                parameters.Add("SENTBY", Tool.userStaffId.ToString());
                parameters.Add("SENTDATE", string.Format("'{0}'", Tool.ConvertNonTimeDateTime(DateTime.Now)));
                SqlConnector.Insert(dbName, "MailNotification", parameters);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Майлаар сануулга илгээхэд алдаа гарлаа1: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Майлаар сануулга илгээхэд алдаа гарлаа2: " + ex.Message);
                throw new MofException("Майлаар сануулга илгээхэд алдаа гарлаа!!", ex);
            }
            finally
            {
                mailParam = null; listDocument = null;
                document = null; ds = null; parameters = null;
                parameterStored = null; dtable = null; 
                headId = domainId = userName = branchId = null;
                mailParams = null;
                Tool.CloseWaiting();
            }
        }

        private void QuitPage()
        {
            try
            {
                Home.pageList.Remove(year);
                Parent.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Гарахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Гарахад алдаа гарлаа!!", ex);
            }
        }

        private void DrawContent(FormType formType)
        {
            IncomeList incomeList = null;
            OutcomeList outcomeList = null;
            DomesticList domesticList = null;
            try
            {
                if (formType.Equals(FormType.IncomeList))
                {
                    if (contentList.ContainsKey(FormType.IncomeList))
                    {
                        mainTabCtl.SelectedTabPage = contentList.Single(t => t.Key.Equals(FormType.IncomeList)).Value;
                        return;
                    }
                    else
                    {
                        incomeList = new IncomeList(Tool.xmlStr, dbName, year);
                        incomeList.Dock = DockStyle.Fill;

                        XtraTabPage contentTab = new XtraTabPage();
                        contentTab.Text = incomeList.FormCaption;
                        contentTab.Controls.Add(incomeList);

                        mainTabCtl.TabPages.Add(contentTab);
                        mainTabCtl.SelectedTabPage = contentTab;

                        contentList.Add(FormType.IncomeList, contentTab);
                    }
                }
                else if (formType.Equals(FormType.OutcomeList))
                {
                    if (contentList.ContainsKey(FormType.OutcomeList))
                    {
                        mainTabCtl.SelectedTabPage = contentList.Single(t => t.Key.Equals(FormType.OutcomeList)).Value;
                        return;
                    }
                    else
                    {
                        outcomeList = new OutcomeList(Tool.xmlStr, dbName, year);
                        outcomeList.Dock = DockStyle.Fill;

                        XtraTabPage contentTab = new XtraTabPage();
                        contentTab.Text = outcomeList.FormCaption;
                        contentTab.Controls.Add(outcomeList);

                        mainTabCtl.TabPages.Add(contentTab);
                        mainTabCtl.SelectedTabPage = contentTab;

                        contentList.Add(FormType.OutcomeList, contentTab);
                    }
                }
                else
                {
                    if (contentList.ContainsKey(FormType.DomesticList))
                    {
                        mainTabCtl.SelectedTabPage = contentList.Single(t => t.Key.Equals(FormType.DomesticList)).Value;
                        return;
                    }
                    else
                    {
                        domesticList = new DomesticList(Tool.xmlStr, dbName, year);
                        domesticList.Dock = DockStyle.Fill;

                        XtraTabPage contentTab = new XtraTabPage();
                        contentTab.Text = domesticList.FormCaption;
                        contentTab.Controls.Add(domesticList);

                        mainTabCtl.TabPages.Add(contentTab);
                        mainTabCtl.SelectedTabPage = contentTab;

                        contentList.Add(FormType.DomesticList, contentTab);
                    }
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шинэ content-tab үүсгэж чадсангүй: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шинэ content-tab үүсгэж чадсангүй: " + ex.Message);
                Tool.ShowError("Шинэ content-tab үүсгэж чадсангүй: ", ex.Message);
            }
            finally { incomeList = null; outcomeList = null; }
        }

        #endregion

        #region Event

        private void SubMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = null;
            OrganizationInfoList organizationInfoList = null;
            Report.ReportEntry reportEntry = null;
            try
            {
                menuItem = sender as ToolStripMenuItem;
                if (menuItem.Tag.Equals(0))
                    QuitPage();
                else if (menuItem.Tag.Equals(1))
                    DrawContent(FormType.IncomeList);
                else if (menuItem.Tag.Equals(2))
                {
                    organizationInfoList = new OrganizationInfoList(dbName, null);
                    organizationInfoList.StartPosition = FormStartPosition.CenterParent;
                    organizationInfoList.ShowDialog();
                }
                else if (menuItem.Tag.Equals(3))
                    DrawContent(FormType.OutcomeList);
                else if (menuItem.Tag.Equals(4))
                {
                    reportEntry = new Report.ReportEntry(DocumentType.Income, dbName);
                    reportEntry.StartPosition = FormStartPosition.CenterParent;
                    reportEntry.ShowDialog();
                }
                else if (menuItem.Tag.Equals(5))
                {
                    reportEntry = new Report.ReportEntry(DocumentType.Outcome, dbName);
                    reportEntry.StartPosition = FormStartPosition.CenterParent;
                    reportEntry.ShowDialog();
                }
                else if (menuItem.Tag.Equals(6))
                    DrawContent(FormType.DomesticList);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("(MainPage)Менюний товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("(MainPage)Менюний товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Хуудсанд алдаа гарлаа!", ex.Message);
            }
            finally { menuItem = null; organizationInfoList = null; reportEntry = null; }
        }

        #endregion

    }
}