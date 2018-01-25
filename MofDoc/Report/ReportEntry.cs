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
using MofDoc.Enum;
using MofDoc.Class;
using MofDoc.Forms.Page;
using MofDoc.Report.Income.Entry;
using DevExpress.XtraReports.UI;

namespace MofDoc.Report
{
    public partial class ReportEntry : XtraForm
    {

        #region Properties

        private string dbName = null;
        private DocumentType documentType;
        private Dictionary<string, decimal> reportTypes = null;
        private DataTable locationDt = null;
        private DataTable organizationDt = null;
        private Dictionary<string, object> parameters = null;

        #endregion

        #region Constructor

        public ReportEntry(DocumentType documentType, string dbName)
        {
            this.dbName = dbName;
            this.documentType = documentType;

            InitializeComponent();
            InitControl();
            InitData();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = Properties.Resources.ReportEntry;
            this.MaximumSize = this.MinimumSize = this.Size;

            if (documentType.Equals(DocumentType.Income))
                lcgReportType.Text = "Ирсэн бичгийн тайлангийн төрөл";
            else if(documentType.Equals(DocumentType.Outcome))
                lcgReportType.Text = "Явсан бичгийн тайлангийн төрөл";

            txtPageNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPageNum.Properties.Mask.EditMask = "[0-9]+";

            btnCancel.Click += new EventHandler(btn_Click);
            btnCancel.Tag = 0;

            btnReport.Enabled = false;
            btnReport.Click += new EventHandler(btn_Click);
            btnReport.Tag = 1;

            lkUpReportType.Properties.DisplayMember = "Key";
            lkUpReportType.Properties.ValueMember = "Value";
            lkUpReportType.Properties.PopulateColumns();
            lkUpReportType.Properties.Columns.Add(new LookUpColumnInfo("Key", "Тайлангийн төрөл"));
            lkUpReportType.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpReportType.Tag = 0;

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
            lkUpOrganization.Enabled = false;

            lkUpBranch.Properties.DisplayMember = "NAME";
            lkUpBranch.Properties.ValueMember = "BR_ID";
            lkUpBranch.Properties.PopulateColumns();
            lkUpBranch.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Газар, алба"));

            lkUpStaff.Properties.DisplayMember = "FNAME";
            lkUpStaff.Properties.ValueMember = "ST_ID";
            lkUpStaff.Properties.PopulateColumns();
            lkUpStaff.Properties.Columns.Add(new LookUpColumnInfo("LNAME", "Овог"));
            lkUpStaff.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));
            lkUpStaff.Properties.Columns.Add(new LookUpColumnInfo("SHORTNAME", "Газар, алба"));

            lkUpToStaff.Properties.DisplayMember = "FNAME";
            lkUpToStaff.Properties.ValueMember = "ST_ID";
            lkUpToStaff.Properties.PopulateColumns();
            lkUpToStaff.Properties.Columns.Add(new LookUpColumnInfo("LNAME", "Овог"));
            lkUpToStaff.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));
            lkUpToStaff.Properties.Columns.Add(new LookUpColumnInfo("SHORTNAME", "Газар, алба"));

            dateRegStart.DateTime = DateTime.Now.AddDays(-90);
            dateRegEnd.DateTime = DateTime.Now;
        }

        private void InitData()
        {
            Dictionary<string, string> filter = null;
            DataTable info = null;
            try
            {
                Tool.ShowWaiting();
                parameters = new Dictionary<string, object>();
                parameters.Add("@XML", Tool.xmlStr);
                reportTypes = new Dictionary<string, decimal>();

                if (documentType.Equals(DocumentType.Income))
                {
                    parameters.Add("@DocType", "I");
                    reportTypes.Add("Тухайн хүнээр тайланг харах", 9);
                    reportTypes.Add("Газар, хэлтсээр тайланг харах", 10);
                    reportTypes.Add("Газар/хэлтсийн хаагдаагүй бичиг сүүлийн хүнээр харах", 14);
                    if (Tool.permissionId.Equals(decimal.One))
                    {
                        reportTypes.Add("Ирсэн бичгийн бүртгэл", 1);
                        reportTypes.Add("Карттай бичгийн бүртгэл", 2);
                        reportTypes.Add("Шууд шилжүүлсэн бичгийн бүртгэл", 3);
                        reportTypes.Add("Хугацаа хэтэрч хаагдсан карт", 4);
                        reportTypes.Add("Хугацаа хэтэрсэн хаагдаагүй карт", 5);
                        reportTypes.Add("Хугацаандаа буй карт", 6);
                        reportTypes.Add("Хаагдсан шууд шилжүүлсэн бичиг", 7);
                        reportTypes.Add("Хаагдаагүй шууд шилжүүлсэн бичиг", 8);
                        reportTypes.Add("Дэлгэрэнгүй тайлан", 11);
                        reportTypes.Add("Хураангуй тайлан", 12);
                        reportTypes.Add("Өргөдөл", 13);
                    }
                }
                else if (documentType.Equals(DocumentType.Outcome))
                {
                    parameters.Add("@DocType", "O");

                    reportTypes.Add("Явсан бичгийн бүртгэл", 1);
                    reportTypes.Add("Хугацаатай бичгийн жагсаалт", 2);
                    reportTypes.Add("Дотоод товьёог", 3);
                    reportTypes.Add("Хугацаа хэтэрч хаагдсан хариутай бичгийн жагсаалт", 4);
                    reportTypes.Add("Сангийн яамаас явсан хариутай асуудлын тоо, шийдвэрлэсэн байдлын тухай мэдээ", 5);
                }
                lkUpReportType.Properties.DataSource = reportTypes;

                filter = new Dictionary<string, string>();
                filter.Add("STATUS", "='Y'");
                info = SqlConnector.GetTable(dbName, "OrganizationType", new List<String>() { "PKID", "PARENTPKID", "NAME" }, filter);
                locationDt = info.Select("PARENTPKID IS NULL").CopyToDataTable();
                organizationDt = info.Select("PARENTPKID IS NOT NULL").CopyToDataTable();
                lkUpLocation.Properties.DataSource = locationDt;

                if (Tool.permissionId.Equals(decimal.One))
                {
                    lkUpBranch.Properties.DataSource = MainPage.branchInfo;
                    lkUpStaff.Properties.DataSource = lkUpToStaff.Properties.DataSource = MainPage.allUser;
                }
                else if (Tool.permissionId.Equals(3))
                {
                    lkUpBranch.Properties.DataSource = MainPage.branchInfo.Select(string.Format("BR_MAIN_ID1 = {0}", Tool.userMainBrId)).CopyToDataTable();
                    lkUpToStaff.Properties.DataSource = MainPage.allUser.Select(string.Format("ST_ID = {0}", Tool.userStaffId)).CopyToDataTable();
                }
                else
                {
                    lkUpBranch.Properties.DataSource = MainPage.branchInfo.Select(string.Format("BR_ID = {0}", Tool.userBrId)).CopyToDataTable();
                    lkUpToStaff.Properties.DataSource = MainPage.allUser.Select(string.Format("ST_ID = {0}", Tool.userStaffId)).CopyToDataTable();
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Тайлангийн мэдээллийг авчрахад алдаа гарлаа!", ex.Message);
            }
            finally
            {
                filter = null; info = null; Tool.CloseWaiting();
            }
        }

        private Dictionary<string, object> PrepareReport()
        {
            Dictionary<string, object> searchData = null;
            try
            {
                searchData = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(txtRegNum.Text.Trim()))
                    searchData.Add("RegNum", txtRegNum.Text);
                if (!string.IsNullOrEmpty(txtControlNum.Text.Trim()))
                    searchData.Add("ControlNum", txtControlNum.Text);
                if (!string.IsNullOrEmpty(txtDocNum.Text.Trim()))
                    searchData.Add("DocNum", txtDocNum.Text);
                if (!string.IsNullOrEmpty(txtPageNum.Text.Trim()))
                    searchData.Add("PageNum", txtPageNum.Text);

                if (lkUpLocation.EditValue != null)
                    searchData.Add("LocationPkId", lkUpLocation.EditValue);
                if (lkUpOrganization.EditValue != null)
                    searchData.Add("OrganizationTypePkId", lkUpOrganization.EditValue);
                if (!string.IsNullOrEmpty(txtFromWho.Text))
                    searchData.Add(txtFromWho.Tag.ToString(), txtFromWho.Text);

                if (lkUpBranch.EditValue != null)
                    searchData.Add("ToBrId", lkUpBranch.EditValue);
                if (lkUpStaff.EditValue != null)
                    searchData.Add("StaffId", lkUpStaff.EditValue);
                if (lkUpToStaff.EditValue != null)
                    searchData.Add("ToStaffId", lkUpToStaff.EditValue);
                if (!string.IsNullOrEmpty(txtDesc.Text))
                    searchData.Add("ShortDesc", txtDesc.Text);

                if (dateRegStart.EditValue != null)
                    searchData.Add("RegStart", ((System.DateTime)(dateRegStart.EditValue)).Date);
                if (dateRegEnd.EditValue != null)
                    searchData.Add("RegEnd", ((System.DateTime)(dateRegEnd.EditValue)).Date.AddHours(23));
                if (dateDocStart.EditValue != null)
                    searchData.Add("DocDateStart", ((System.DateTime)(dateDocStart.EditValue)).Date);
                if (dateDocEnd.EditValue != null)
                    searchData.Add("DocDateEnd", ((System.DateTime)(dateDocEnd.EditValue)).Date.AddHours(23));
                if (dateMoveStart.EditValue != null)
                    searchData.Add("MoveDateStart", ((System.DateTime)(dateMoveStart.EditValue)).Date);
                if (dateMoveEnd.EditValue != null)
                    searchData.Add("MoveDateEnd", ((System.DateTime)(dateMoveEnd.EditValue)).Date.AddHours(23));

                return searchData;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг бэлтгэхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг бэлтгэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээллийг бэлтгэхэд алдаа гарлаа!", ex);
            }
            finally { searchData = null; }
        }

        private void InitReport()
        {
            decimal reportId;
            Dictionary<string, object> searchData = null;
            List<Document> cloneDocument = null;
            List<Document> cloneDocumentNew = null;
            int reportInt, totalDoc, totalPageNum = 0;
            string userFullName = null;
            DataTable dt = null;
            List<DocumentReport> documentReports = null;
            try
            {
                Tool.ShowWaiting();
                reportId = decimal.Parse(lkUpReportType.EditValue.ToString());

                //ТУХАЙН ХҮНЭЭР ТАЙЛАНГ ХАРАХ - ТУХАЙН ХҮН НЬ ХООСОН БАЙНА!
                if (reportId.Equals(9) && lkUpToStaff.EditValue == null)
                {
                    lcgTab.SelectedTabPage = lcgBranchOrStaff;
                    lkUpToStaff.ErrorText = "Тухайн хүнээ сонгоно уу.";
                    lkUpToStaff.Focus();
                    return;
                }

                reportInt = int.Parse(decimal.Add(reportId, 1).ToString());
                searchData = PrepareReport();

                userFullName = string.IsNullOrEmpty(Tool.userLName) ? Tool.userFName : string.Format("{0}.{1}", Tool.userLName[0], Tool.userFName);
                cloneDocument = ckHasChildren.Checked ? Tool.GetNonDocumentFromDb(dbName, parameters) : Tool.GetDocumentFromDb(dbName, parameters, true);
                if (searchData.ContainsKey("RegNum"))
                    cloneDocument = cloneDocument.Where(t => t.RegNum.Equals(searchData.Single(y => y.Key.Equals("RegNum")).Value.ToString())).ToList();
                if (searchData.ContainsKey("ControlNum"))
                    cloneDocument = cloneDocument.Where(t => t.ControlNum.Equals(decimal.Parse(searchData.Single(y => y.Key.Equals("ControlNum")).Value.ToString()))).ToList();
                if (searchData.ContainsKey("DocNum"))
                    cloneDocument = cloneDocument.Where(t => t.DocNum.Contains(searchData.Single(y => y.Key.Equals("DocNum")).Value.ToString())).ToList();
                if (searchData.ContainsKey("PageNum"))
                    cloneDocument = cloneDocument.Where(t => t.PageNum.Equals(int.Parse(searchData.Single(y => y.Key.Equals("PageNum")).Value.ToString()))).ToList();

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
                if (searchData.ContainsKey("ShortDesc"))
                    cloneDocument = cloneDocument.Where(t => t.ShortDesc.Contains(searchData.Single(y => y.Key.Equals("ShortDesc")).Value.ToString())).ToList();

                cloneDocumentNew = cloneDocument;
                if (searchData.ContainsKey("RegStart") && searchData.ContainsKey("RegEnd"))
                {
                    if (reportId.Equals(5))
                    {
                        cloneDocument = cloneDocument.Where(t =>
                            t.ReturnDate >= DateTime.Parse(searchData.Single(y => y.Key.Equals("RegStart")).Value.ToString()) &&
                            t.ReturnDate <= DateTime.Parse(searchData.Single(z => z.Key.Equals("RegEnd")).Value.ToString())).ToList();
                    }
                    else
                    {
                        cloneDocument = cloneDocument.Where(t =>
                            t.RegDate >= DateTime.Parse(searchData.Single(y => y.Key.Equals("RegStart")).Value.ToString()) &&
                            t.RegDate <= DateTime.Parse(searchData.Single(z => z.Key.Equals("RegEnd")).Value.ToString())).ToList();
                    }
                }
                if (searchData.ContainsKey("DocDateStart") && searchData.ContainsKey("DocDateEnd"))
                    cloneDocument = cloneDocument.Where(t =>
                        t.DocDate >= DateTime.Parse(searchData.Single(y => y.Key.Equals("DocDateStart")).Value.ToString()) &&
                        t.DocDate <= DateTime.Parse(searchData.Single(z => z.Key.Equals("DocDateEnd")).Value.ToString())).ToList();
                if (searchData.ContainsKey("MoveDateStart") && searchData.ContainsKey("MoveDateEnd"))
                    cloneDocument = cloneDocument.Where(t =>
                        t.ParentPkId != null &&
                        t.CreatedDate >= DateTime.Parse(searchData.Single(y => y.Key.Equals("MoveDateStart")).Value.ToString()) &&
                        t.CreatedDate <= DateTime.Parse(searchData.Single(z => z.Key.Equals("MoveDateEnd")).Value.ToString())).ToList();

                if (cloneDocument.Count.Equals(0))
                {
                    Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна. Шалгах шалгуураа зөв оруулна уу.");
                    return;
                }
                cloneDocument.ForEach(t => { t.InFromWho = string.Format("{0}, {1}, {2}", t.Location, t.OrganizationName, t.InFromWho); });

                if (documentType.Equals(DocumentType.Income))
                {
                    //cloneDocument = ckHasChildren.Checked ? Tool.GetOrderRelationList(cloneDocument) : cloneDocument;

                    //ИРСЭН БИЧГИЙН ТАЙЛАН
                    if (reportId.Equals(1))
                    {
                        //ИРСЭН БИЧГИЙН БҮРТГЭЛ
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }

                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(2))
                    {
                        //КАРТТАЙ БИЧГИЙН БҮРТГЭЛ
                        cloneDocument = cloneDocument.Where(t => t.ControlNum != null).ToList();

                        if (cloneDocument.Count.Equals(0))
                        {
                            Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна!");
                            return;
                        }
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }

                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(3))
                    {
                        //ШУУД ШИЛЖҮҮЛСЭН БИЧГИЙН БҮРТГЭЛ
                        cloneDocument = cloneDocument.Where(t => t.ControlNum == null).ToList();

                        if (cloneDocument.Count.Equals(0))
                        {
                            Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна!");
                            return;
                        }
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }

                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(4))
                    {
                        //ХУГАЦАА ХЭТЭРСЭН ХААГДСАН КАРТ
                        cloneDocument = cloneDocument.Where(t => t.ControlNum != null && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate).ToList();
                        cloneDocument.ForEach(t => { t.DocDate = t.ReturnDate; });

                        if (cloneDocument.Count.Equals(0))
                        {
                            Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна!");
                            return;
                        }
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }

                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(5))
                    {
                        //ХУГАЦАА ХЭТЭРЧ ХААГДААГҮЙ КАРТ
                        cloneDocument = cloneDocument.Where(t => t.ControlNum != null && t.Status && t.ClosedDate == null).ToList();
                        cloneDocument.ForEach(t => { t.DocDate = t.ReturnDate; });

                        if (cloneDocument.Count.Equals(0))
                        {
                            Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна!");
                            return;
                        }
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }

                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(6))
                    {
                        //ХУГАЦААНДАА БУЙ КАРТТАЙ БИЧИГ
                        cloneDocument = cloneDocument.Where(t => t.ControlNum != null && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now).ToList();
                        cloneDocument.ForEach(t => { t.DocDate = t.ReturnDate; });

                        if (cloneDocument.Count.Equals(0))
                        {
                            Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна!");
                            return;
                        }
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }

                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(7))
                    {
                        //ХААГДСАН ШУУД ШИЛЖҮҮЛСЭН БИЧИГ
                        cloneDocument = cloneDocument.Where(t => t.ControlNum == null && !t.Status && t.ClosedDate != null).ToList();

                        if (cloneDocument.Count.Equals(0))
                        {
                            Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна!");
                            return;
                        }
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }
                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(8))
                    {
                        //ХААГДААГҮЙ ШУУД ШИЛЖҮҮЛСЭН БИЧИГ
                        cloneDocument = cloneDocument.Where(t => t.ControlNum == null && t.Status && t.ClosedDate == null).ToList();

                        if (cloneDocument.Count.Equals(0))
                        {
                            Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна!");
                            return;
                        }
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }
                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(9))
                    {
                        //ТУХАЙН ХҮНЭЭР ТАЙЛАНГ ХАРАХ
                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { Tool.GetDocumentReport(cloneDocument, lkUpToStaff.EditValue) },
                            new List<object> { Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue) });
                    }
                    else if (reportId.Equals(10))
                    {
                        //ГАЗАР, ХЭЛТСЭЭР ТАЙЛАНГ ХАРАХ
                        if (lkUpBranch.EditValue != null)
                            Tool.PrintReport(reportInt, lkUpReportType.Text,
                                new List<object> { Tool.GetDocumentReport(cloneDocument, decimal.Parse(lkUpBranch.EditValue.ToString())) },
                                new List<object> { Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue) });
                        else
                        {
                            dt = lkUpBranch.Properties.DataSource as DataTable;
                            documentReports = new List<DocumentReport>();
                            foreach (DataRow row in dt.Rows)
                                documentReports.AddRange(Tool.GetDocumentReport(cloneDocument, decimal.Parse(row["BR_ID"].ToString())));

                            Tool.PrintReport(reportInt, lkUpReportType.Text,
                                new List<object> { documentReports },
                                new List<object> { Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue) });
                        }
                    }
                    else if (reportId.Equals(11))
                    {
                        //ДЭЛГЭРЭНГҮЙ ТАЙЛАН
                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { Tool.GetDocumentReport(cloneDocument, cloneDocumentNew,  true,
                                searchData.Single(y => y.Key.Equals("RegStart")).Value.ToString(), searchData.Single(y => y.Key.Equals("RegEnd")).Value.ToString())},
                            new List<object> { Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue) });
                    }
                    else if (reportId.Equals(12))
                    {
                        //ХУРААНГУЙ ТАЙЛАН
                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { Tool.GetDocumentReport(cloneDocument, cloneDocumentNew, false, 
                                searchData.Single(y => y.Key.Equals("RegStart")).Value.ToString(), searchData.Single(y => y.Key.Equals("RegEnd")).Value.ToString()) },
                            new List<object> { Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue) });
                    }
                    if (reportId.Equals(13))
                    {
                        //ӨРГӨДӨЛ
                        cloneDocument = cloneDocument.Where(t => t.DocNotePkId == decimal.Parse("47")).ToList();
                        cloneDocument = cloneDocument.OrderBy(t => t.DocDate).ToList();
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }

                        Tool.PrintReport(2, lkUpReportType.Text,
                            new List<object> { cloneDocument },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    else if (reportId.Equals(14))
                    {
                        //ГАЗАР/ХЭЛТСИЙН ХААГДААГҮЙ БИЧИГ СҮҮЛИЙН ХҮНЭЭР ХАРАХ
                        if (lkUpBranch.EditValue == null)
                        {
                            lcgTab.SelectedTabPage = lcgBranchOrStaff;
                            lkUpBranch.ErrorText = "Тухайн газар, хэлтсээ сонгоно уу.";
                            lkUpBranch.Focus();
                            return;
                        }

                        cloneDocument = cloneDocument.Where(t => t.Status && t.ClosedDate == null).ToList();
                        if (cloneDocument.Count.Equals(0))
                        {
                            Tool.ShowInfo("Тайлангийн мэдээлэл хоосон байна!");
                            return;
                        }
                        totalDoc = cloneDocument.Count;
                        foreach (Document item in cloneDocument)
                        {
                            if (item.PageNum == null) continue;
                            totalPageNum += (int)item.PageNum;
                        }

                        Tool.PrintReport(reportInt, lkUpReportType.Text,
                            new List<object> { Tool.GetLastDocument(cloneDocument) },
                            new List<object> { lkUpReportType.Text.ToUpper(), userFullName, Tool.ConvertNonTimeDateTime(dateRegStart.EditValue), 
                                Tool.ConvertNonTimeDateTime(dateRegEnd.EditValue), totalDoc, totalPageNum });
                    }
                    //else throw new MofException("Тайлангийн код олдсонгүй.");
                }
                else if (documentType.Equals(DocumentType.Outcome))
                {

                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Тайлангийн мэдээллийг авчрахад алдаа гарлаа!", ex.Message);
            }
            finally { searchData = null; cloneDocument = null; userFullName = null; dt = null; documentReports = null; Tool.CloseWaiting(); }
        }

        #endregion

        #region Event

        private void lkUp_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit control = sender as LookUpEdit;
            DataRow[] dr = null;
            try
            {
                if (control.Tag.Equals(0))
                {
                    if (btnReport.Enabled) return;
                    btnReport.Enabled = true;
                }
                else if (control.Tag.Equals(1))
                {
                    dr = organizationDt.Select(string.Format("PARENTPKID = {0}", control.EditValue));
                    if (dr == null || dr.Length.Equals(0))
                    {
                        Tool.ShowInfo(string.Format(MofDoc.Properties.Resources.AddOrganizationInfo, control.Text));
                        lkUpOrganization.Properties.DataSource = null;
                        txtFromWho.Enabled = lkUpOrganization.Enabled = false;
                        txtFromWho.Text = string.Empty;
                        return;
                    }
                    lkUpOrganization.Properties.DataSource = dr.CopyToDataTable();
                    txtFromWho.Enabled = lkUpOrganization.Enabled = true;
                    //lkUpOrganization.EditValue = dr.CopyToDataTable().DefaultView[0].Row[0];
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("LookUpEdit контролын мэдээллийг сонгоход алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LookUpEdit контролын мэдээллийг сонгоход алдаа гарлаа: " + ex.Message);
                Tool.ShowError("LookUpEdit контролын мэдээллийг сонгоход алдаа гарлаа!", ex.Message);
            }
            finally { control = null; dr = null; }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            int tagId = int.Parse((sender as SimpleButton).Tag.ToString());
            if (tagId.Equals(0)) this.Dispose();
            else InitReport();
        }

        #endregion

    }
}