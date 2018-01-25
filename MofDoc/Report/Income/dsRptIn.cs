using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MofDoc.Report.Income
{
    public partial class dsRptIn : DevExpress.XtraReports.UI.XtraReport
    {
        public dsRptIn(string RegUser, string RegDate, string RegNum, string DocDate, string DocNum, string PageNum, string Pro, string OrgType, string FromWho, string ToWho, string DocType, string Meaning, string UZ, string ForDate, string ForDept, string ForUser)
        {
            InitializeComponent();
            xrLabel10.Text = RegNum;
            xrLabel8.Text = RegUser;
            xrLabel7.Text = RegDate;
            xrLabel16.Text = DocDate;
            xrLabel17.Text = DocNum;
            xrLabel15.Text = PageNum;
            xrLabel38.Text = Pro;
            xrLabel31.Text = OrgType;
            xrLabel36.Text = FromWho;
            xrLabel30.Text = ToWho;
            xrLabel32.Text = DocType;
            xrLabel35.Text = Meaning;
            xrLabel33.Text = UZ;
            xrLabel39.Text = ForDate;
            xrLabel1.Text = ForDept;
            xrLabel34.Text = ForUser;
        }
    }
}