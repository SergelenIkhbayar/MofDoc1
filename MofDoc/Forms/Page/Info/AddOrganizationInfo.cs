using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MofDoc.Class;
using System.Data.SqlClient;

namespace MofDoc.Forms.Page.Info
{
    public partial class AddOrganizationInfo : XtraForm
    {

        #region Properties

        private string dbName = null;
        private decimal? locationPkId = null;
        private decimal? organizationPkId = null;
        private string organizationName = null;
        internal OrganizationInfo organizationInfo = null;

        #endregion

        #region Constructor

        public AddOrganizationInfo(string dbName, decimal? locationPkId)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.locationPkId = locationPkId;
            InitControl();
            InitData();
        }

        public AddOrganizationInfo(string dbName, decimal? locationPkId, decimal? organizationPkId, string organizationName)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.locationPkId = locationPkId;
            this.organizationPkId = organizationPkId;
            this.organizationName = organizationName;
            InitControl();
            InitData();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Байгууллага бүртгэх";
            this.MaximumSize = this.MinimumSize = this.Size;
            this.FormClosing += new FormClosingEventHandler(AddOrganizationInfo_FormClosing);

            lkUpLocation.Properties.DisplayMember = "NAME";
            lkUpLocation.Properties.ValueMember = "PKID";
            lkUpLocation.Properties.PopulateColumns();
            lkUpLocation.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байршлын нэр"));

            btnCancel.Tag = 0;
            btnCancel.Click += new EventHandler(btn_Click);

            btnRegister.Tag = 1;
            btnRegister.Click += new EventHandler(btn_Click);
        }

        private void InitData()
        {
            Dictionary<string, string> filter = null;
            DataTable dt = null;
            try
            {
                Tool.ShowWaiting();

                filter = new Dictionary<string, string>();
                filter.Add("STATUS", "='Y'");
                filter.Add("PARENTPKID", "IS NULL");
                dt = SqlConnector.GetTable(dbName, "OrganizationType", new List<String>() { "PKID", "PARENTPKID", "NAME" }, filter);
                if (dt == null || dt.Rows == null || dt.Rows.Count.Equals(0))
                    return;

                lkUpLocation.Properties.DataSource = dt;
                if (!locationPkId.Equals(null))
                    lkUpLocation.EditValue = locationPkId;
                if (!string.IsNullOrEmpty(organizationName))
                {
                    lkUpLocation.Enabled = false;
                    txtOrganizationName.Text = organizationName;
                    btnRegister.Text = "Засах";
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Байршилын мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Байршилын мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Байршилын мэдээллийг авчрахад алдаа гарлаа!", ex.Message);
            }
            finally { filter = null; dt = null; Tool.CloseWaiting(); }
        }

        private void WorkOnOrganizationInfo()
        {
            Dictionary<string, string> parameters = null;
            Dictionary<string, string> filters = null;
            string dateTime = null;
            decimal pkId = 0;
            try
            {
                if (string.IsNullOrEmpty(txtOrganizationName.Text))
                {
                    Tool.ShowInfo("Байгууллагын нэр хоосон байна. Бөглөнө үү!");
                    return;
                }
                Tool.ShowWaiting();
                dateTime = Tool.DateTimeNow();
                if (organizationPkId.Equals(null))
                {
                    pkId = Tool.NewPkId();
                    parameters = new Dictionary<string, string>();
                    parameters.Add("PKID", pkId.ToString());
                    parameters.Add("PARENTPKID", lkUpLocation.EditValue.ToString());
                    parameters.Add("NAME", string.Format("N'{0}'", txtOrganizationName.Text));
                    parameters.Add("CREATEDBY", Tool.userStaffId.ToString());
                    parameters.Add("CREATEDDATE", string.Format("'{0}'", dateTime));
                    parameters.Add("STATUS", "'Y'");

                    SqlConnector.Insert(dbName, "OrganizationType", parameters);
                    organizationInfo = new OrganizationInfo(pkId, decimal.Parse(lkUpLocation.EditValue.ToString()), txtOrganizationName.Text);
                }
                else
                {
                    if (organizationName.Equals(txtOrganizationName.Text) || organizationName == txtOrganizationName.Text)
                    {
                        Tool.ShowInfo("Байгууллагын өмнөхтэйгээ адилхан байна! Засаж оруулна уу.");
                        return;
                    }
                    parameters = new Dictionary<string, string>();
                    parameters.Add("NAME", string.Format("N'{0}'", txtOrganizationName.Text));
                    parameters.Add("MODIFIEDBY", string.Format("{0}", Tool.userStaffId));
                    parameters.Add("MODIFIEDDATE", string.Format("'{0}'", dateTime));

                    filters = new Dictionary<string, string>();
                    filters.Add("PKID", string.Format("={0}", organizationPkId.ToString()));

                    SqlConnector.Update(dbName, "OrganizationType", parameters, filters);
                    organizationInfo = new OrganizationInfo((decimal)organizationPkId, (decimal)locationPkId, txtOrganizationName.Text);
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын бүртгэлийг нэмэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын бүртгэлийг нэмэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Байгууллагын бүртгэлийг нэмэхэд алдаа гарлаа!", ex);
            }
            finally { parameters = null; filters = null; dateTime = null; Tool.CloseWaiting(); }
        }

        #endregion

        #region Event

        private void AddOrganizationInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult.Equals(DialogResult.Cancel))
                organizationInfo = null;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = null;
            try
            {
                btn = sender as SimpleButton;
                if (btn.Tag.Equals(0)) this.Close();
                else WorkOnOrganizationInfo();
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
            finally { btn = null; }
        }

        #endregion

    }
}