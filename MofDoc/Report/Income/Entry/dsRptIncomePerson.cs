using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using System.Drawing.Printing;
using MofDoc.Class;
using System.Collections.Generic;

namespace MofDoc.Report.Income.Entry
{
    public partial class dsRptIncomePerson : XtraReport
    {

        #region Properties

        private BindingSource dataSource = null;
        private int count = 0;

        #endregion

        #region Constructor

        //ДЭЛГЭРЭНГҮЙ ТАЙЛАН
        public dsRptIncomePerson(string startDate, string endDate, List<DocumentReport> parameter)
        {
            InitializeComponent();

            xrLblStartDate.Text = startDate;
            xrLblEndDate.Text = endDate;

            dataSource = new BindingSource();
            dataSource.DataSource = parameter;
            InitData();
        }

        //ТУХАЙН ХҮНЭЭР ТАЙЛАНГ ХАРАХ
        public dsRptIncomePerson(string staffName, string startDate, string endDate, List<DocumentReport> parameter)
        {
            InitializeComponent();

            xrLblStartDate.Text = startDate;
            xrLblEndDate.Text = endDate;

            dataSource = new BindingSource();
            dataSource.DataSource = parameter;

            InitData();
        }

        //ГАЗАР, ХЭЛТСЭЭР ТАЙЛАНГ ХАРАХ
        public dsRptIncomePerson(decimal branchId, string startDate, string endDate, List<DocumentReport> parameter)
        {
            InitializeComponent();

            xrLblStartDate.Text = startDate;
            xrLblEndDate.Text = endDate;

            dataSource = new BindingSource();
            dataSource.DataSource = parameter;
            InitData();
        }

        #endregion

        #region Function and Event

        private void InitData()
        {
            try
            {
                this.DataSource = dataSource;
                Detail.BeforePrint += new PrintEventHandler(Detail_BeforePrint);

                xrTcStaff.DataBindings.Add("Text", dataSource, "Name");
                xrTcCard.DataBindings.Add("Text", dataSource, "Card");
                xrTcDirect.DataBindings.Add("Text", dataSource, "Direct");
                xrTcIncome.DataBindings.Add("Text", dataSource, "Income");
                xrTcDecCard.DataBindings.Add("Text", dataSource, "CardDecision");
                xrTcDecExpired.DataBindings.Add("Text", dataSource, "ExpiredDecision");
                xrTcDecDirect.DataBindings.Add("Text", dataSource, "DirectDecision");
                xrTcDecision.DataBindings.Add("Text", dataSource, "Decision");
                xrTcProgressTime.DataBindings.Add("Text", dataSource, "ProgressTime");
                xrTcProgressNonTime.DataBindings.Add("Text", dataSource, "ProgressNonTime");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг гаргахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Тайлангийн мэдээллийг гаргахад алдаа гарлаа!", ex.Message);
            }
        }

        private void Detail_BeforePrint(object sender, PrintEventArgs e)
        {
            try
            {
                if (GetCurrentColumnValue("IsBranch") == null)
                {
                    xrTcNum.Text = string.Empty;
                    xrTcStaff.Font = new Font(xrTcStaff.Font, FontStyle.Bold);
                }
                else if (bool.Parse(GetCurrentColumnValue("IsBranch").ToString()))
                {
                    count++;
                    xrTcNum.Text = count.ToString();
                    xrTcStaff.Font = new Font(xrTcStaff.Font, FontStyle.Bold);
                }
                else
                {
                    xrTcNum.Text = string.Empty;
                    xrTcStaff.Font = new Font(xrTcStaff.Font, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайланг дэс дугаарлахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Тайланг дэс дугаарлахад алдаа гарлаа!", ex.Message);
            }
        }

        #endregion

    }
}