using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MofDoc.Report.Income
{
    public partial class dsRptCCFS : DevExpress.XtraReports.UI.XtraReport
    {
        public dsRptCCFS(string RegUser, string RegDate, string RegNum, string ConNum, string DocDate, string DocNum, string PageNum, string Pro, string OrgType, 
            string FromWho, string ToWho, string DocType, string Meaning, string UZ, string ForDate, string ForDept, string ForUser, string ReturnDate)
        {
            InitializeComponent();
            xrLabel42.Text = ConNum;
            xrLabel41.Text = RegNum;
            xrLabel40.Text = RegUser;
            xrLabel39.Text = RegDate;
            xrLabel36.Text = DocDate;
            xrLabel38.Text = DocNum;
            xrLabel37.Text = PageNum;
            xrLabel30.Text = Pro;
            xrLabel29.Text = OrgType;
            xrLabel15.Text = FromWho;
            xrLabel28.Text = ToWho;
            xrLabel8.Text = DocType;
            xrLabel32.Text = Meaning;
            xrLabel33.Text = UZ;
            xrLabel35.Text = ForDate;
            xrLabel34.Text = ForDept;
            xrLabel13.Text = ForUser;
            xrLabel14.Text = ReturnDate;
        }

    }
}
