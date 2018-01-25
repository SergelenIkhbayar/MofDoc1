using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using MofDoc.Class;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace MofDoc.Report.Income.Entry
{
    public partial class dsRptInDocs : XtraReport
    {

        #region Properties

        private BindingSource dataSource = null;
        private int count = 0;

        #endregion

        #region Constructor

        public dsRptInDocs(bool isReturn, string reportName, string userName, string startDate, string endDate, string totalDoc, string totalPageNum, List<Document> parameter)
        {
            InitializeComponent();

            dataSource = new BindingSource();
            dataSource.DataSource = parameter;

            xrLblReportName.Text = reportName;
            xrLblUserName.Text = userName;
            xrLblStartDate.Text = startDate;
            xrLblEndDate.Text = endDate;
            xrLblTotalDoc.Text = totalDoc;
            xrLblTotalPage.Text = totalPageNum;

            InitData();

            if (isReturn)
                xrTableCell12.Text = "Хариу өгөх огноо";
        }

        public dsRptInDocs(string reportName, string userName, string startDate, string endDate, string totalDoc, string totalPageNum, List<Document> parameter)
        {
            InitializeComponent();

            dataSource = new BindingSource();
            dataSource.DataSource = parameter;

            xrLblReportName.Text = reportName;
            xrLblUserName.Text = userName;
            xrLblStartDate.Text = startDate;
            xrLblEndDate.Text = endDate;
            xrLblTotalDoc.Text = totalDoc;
            xrLblTotalPage.Text = totalPageNum;

            InitData();
        }

        #endregion

        #region Function and Event

        private void InitData()
        {
            try
            {
                Detail.BeforePrint += new PrintEventHandler(Detail_BeforePrint);
                this.DataSource = dataSource;

                xrTcNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTcDocDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTcDocNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTcFromWhere.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                xrTcDesc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                xrTcPageNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                xrTcDocDate.DataBindings.Add("Text", dataSource, "DocDate", "{0:yyyy-MM-dd}");
                xrTcDocNum.DataBindings.Add("Text", dataSource, "DocNum");
                xrTcFromWhere.DataBindings.Add("Text", dataSource, "InFromWho");
                xrTcDesc.DataBindings.Add("Text", dataSource, "ShortDesc");
                xrTcPageNum.DataBindings.Add("Text", dataSource, "PageNum");

                xrTcRegDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTcRegNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTcControlNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTcStaff.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrTcBranch.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTcReturnDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTcClosedDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                xrTcRegDate.DataBindings.Add("Text", dataSource, "RegDate", "{0:yyyy-MM-dd}");
                xrTcRegNum.DataBindings.Add("Text", dataSource, "RegNum");
                xrTcControlNum.DataBindings.Add("Text", dataSource, "ControlNum");
                xrTcStaff.DataBindings.Add("Text", dataSource, "ToName");
                xrTcBranch.DataBindings.Add("Text", dataSource, "Branch");
                xrTcReturnDate.DataBindings.Add("Text", dataSource, "ReturnDate", "{0:yyyy-MM-dd}");
                xrTcClosedDate.DataBindings.Add("Text", dataSource, "ClosedDate", "{0:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ирсэн бичгийн бүртгэлийн тайланд алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Ирсэн бичгийн бүртгэлийн тайланд алдаа гарлаа!", ex.Message);
            }
        }

        private void Detail_BeforePrint(object sender, PrintEventArgs e)
        {
            try
            {
                /*if (GetCurrentColumnValue("ParentPkId") == null)
                {
                    count++;
                    xrTcNum.Text = count.ToString();
                }
                else xrTcNum.Text = string.Empty;*/

                count++;
                xrTcNum.Text = count.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ирсэн бичгийн бүртгэлийн тайланг дэс дугаарлахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Ирсэн бичгийн бүртгэлийн тайланг дэс дугаарлахад алдаа гарлаа!", ex.Message);
            }
        }

        #endregion
        
    }
}