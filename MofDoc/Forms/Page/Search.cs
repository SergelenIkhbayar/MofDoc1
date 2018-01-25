using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MofDoc.Class;
using DevExpress.XtraEditors.Controls;

namespace MofDoc.Forms.Page
{
    public partial class Search : XtraForm
    {

        #region Properties

        private object sender = null;
        private string dbName = null;
        private DataTable locationDt = null;
        private DataTable organizationDt = null;
        private Enum.DocumentType documentType;

        #endregion

        #region Constructor

        public Search(object form,Enum.DocumentType documentType, string dbName)
        {
            InitializeComponent();

            this.sender = form;
            this.dbName = dbName;
            this.documentType = documentType;

            InitControl();
            InitData();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Хайлт хийх";
            this.MaximumSize = this.MinimumSize = this.Size;

            btnCancel.Click += new EventHandler(btn_Click);
            btnCancel.Tag = 0;

            btnSearch.Click += new EventHandler(btn_Click);
            btnSearch.Tag = 1;

            txtRegNum.Tag = "RegNum";
            txtRegNum.KeyDown += new KeyEventHandler(txt_KeyDown);

            txtControlNum.Tag = "ControlNum";
            txtControlNum.KeyDown += new KeyEventHandler(txt_KeyDown);

            txtDocNum.Tag = "DocNum";
            txtDocNum.KeyDown += new KeyEventHandler(txt_KeyDown);

            txtPageNum.Tag = "PageNum";
            txtPageNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            txtPageNum.Properties.Mask.EditMask = "[0-9]+";
            txtPageNum.KeyDown += new KeyEventHandler(txt_KeyDown);

            txtFromWho.Tag = "InFromWho";
            txtFromWho.KeyDown += new KeyEventHandler(txt_KeyDown);

            lkUpDocNoteType.Properties.DisplayMember = "NAME";
            lkUpDocNoteType.Properties.ValueMember = "PKID";
            lkUpDocNoteType.Properties.PopulateColumns();
            lkUpDocNoteType.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Бичгийн төрөл"));
            lkUpDocNoteType.Tag = "DocNoteType";
            lkUpDocNoteType.KeyDown += new KeyEventHandler(lkUp_KeyDown);

            memoDesc.Tag = "ShortDesc";
            memoDesc.KeyDown += new KeyEventHandler(memoEdit_KeyDown);

            lkUpLocation.Properties.DisplayMember = "NAME";
            lkUpLocation.Properties.ValueMember = "PKID";
            lkUpLocation.Properties.PopulateColumns();
            lkUpLocation.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байршлын нэр"));
            lkUpLocation.Tag = 0;
            lkUpLocation.EditValueChanged += new EventHandler(lkUp_EditValueChanged);
            lkUpLocation.KeyDown += new KeyEventHandler(lkUp_KeyDown);

            lkUpOrganizationName.Properties.DisplayMember = "NAME";
            lkUpOrganizationName.Properties.ValueMember = "PKID";
            lkUpOrganizationName.Properties.PopulateColumns();
            lkUpOrganizationName.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Байгууллагын нэр"));
            lkUpOrganizationName.KeyDown += new KeyEventHandler(lkUp_KeyDown);

            lkUpBranch.Properties.DisplayMember = "NAME";
            lkUpBranch.Properties.ValueMember = "BR_ID";
            lkUpBranch.Properties.PopulateColumns();
            lkUpBranch.Properties.Columns.Add(new LookUpColumnInfo("NAME", "Газар, алба"));
            lkUpBranch.KeyDown += new KeyEventHandler(lkUp_KeyDown);

            lkUpName.Properties.DisplayMember = "FNAME";
            lkUpName.Properties.ValueMember = "ST_ID";
            lkUpName.Properties.PopulateColumns();
            lkUpName.Properties.Columns.Add(new LookUpColumnInfo("LNAME", "Овог"));
            lkUpName.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));
            lkUpName.Properties.Columns.Add(new LookUpColumnInfo("SHORTNAME", "Газар, алба"));
            lkUpName.KeyDown += new KeyEventHandler(lkUp_KeyDown);

            lkUpToName.Properties.DisplayMember = "FNAME";
            lkUpToName.Properties.ValueMember = "ST_ID";
            lkUpToName.Properties.PopulateColumns();
            lkUpToName.Properties.Columns.Add(new LookUpColumnInfo("LNAME", "Овог"));
            lkUpToName.Properties.Columns.Add(new LookUpColumnInfo("FNAME", "Нэр"));
            lkUpToName.Properties.Columns.Add(new LookUpColumnInfo("SHORTNAME", "Газар, алба"));
            lkUpToName.KeyDown += new KeyEventHandler(lkUp_KeyDown);

            dateRegStart.DateTime = DateTime.Now.AddDays(-30);
            dateRegEnd.DateTime = DateTime.Now;

            dateRegStart.KeyDown += new KeyEventHandler(date_KeyDown);
            dateRegStart.Tag = "RegStart";
            dateRegEnd.KeyDown += new KeyEventHandler(date_KeyDown);
            dateRegEnd.Tag = "RegEnd";

            dateDocStart.KeyDown += new KeyEventHandler(date_KeyDown);
            dateDocStart.Tag = "DocDateStart";
            dateDocEnd.KeyDown += new KeyEventHandler(date_KeyDown);
            dateDocEnd.Tag = "DocDateEnd";

            dateMoveStart.KeyDown += new KeyEventHandler(date_KeyDown);
            dateMoveStart.Tag = "MoveDateStart";
            dateMoveEnd.KeyDown += new KeyEventHandler(date_KeyDown);
            dateMoveEnd.Tag = "MoveDateEnd";

            txtRegNum.Select();
            this.Disposed += new EventHandler(Search_Disposed);
        }

        private void InitData()
        {
            Dictionary<string, string> filter = null;
            DataTable info = null;
            try
            {
                filter = new Dictionary<string, string>();
                filter.Add("STATUS", "='Y'");
                info = SqlConnector.GetTable(dbName, "OrganizationType", new List<String>() { "PKID", "PARENTPKID", "NAME" }, filter);
                locationDt = info.Select("PARENTPKID IS NULL").CopyToDataTable();
                organizationDt = info.Select("PARENTPKID IS NOT NULL").CopyToDataTable();
                lkUpLocation.Properties.DataSource = locationDt;
                lkUpOrganizationName.Properties.DataSource = organizationDt;

                lkUpBranch.Properties.DataSource = MainPage.branchInfo;
                lkUpName.Properties.DataSource = MainPage.allUser;
                lkUpToName.Properties.DataSource = MainPage.allUser;

                lkUpDocNoteType.Properties.DataSource = SqlConnector.GetTable(dbName, "DocNoteType", new List<String>() { "PKID", "NAME" }, filter);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Хайлтын мэдээллийг авчрахад алдаа гарлаа!", ex.Message);
            }
            finally { filter = null; info = null; }
        }

        private Dictionary<string, object> PrepareSearch()
        {
            Dictionary<string, object> searchData = null;
            try
            {
                searchData = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(txtRegNum.Text.Trim()))
                    searchData.Add(txtRegNum.Tag.ToString(), txtRegNum.Text);
                if (!string.IsNullOrEmpty(txtControlNum.Text.Trim()))
                    searchData.Add(txtControlNum.Tag.ToString(), txtControlNum.Text);
                if (!string.IsNullOrEmpty(txtDocNum.Text.Trim()))
                    searchData.Add(txtDocNum.Tag.ToString(), txtDocNum.Text);
                if (!string.IsNullOrEmpty(txtPageNum.Text.Trim()))
                    searchData.Add(txtPageNum.Tag.ToString(), txtPageNum.Text);

                if (lkUpDocNoteType.EditValue != null)
                    searchData.Add(lkUpDocNoteType.Tag.ToString(), lkUpDocNoteType.EditValue);
                if (!string.IsNullOrEmpty(memoDesc.Text))
                    searchData.Add(memoDesc.Tag.ToString(), memoDesc.Text);

                if (lkUpLocation.EditValue != null)
                    searchData.Add("LocationPkId", lkUpLocation.EditValue);
                if(lkUpOrganizationName.EditValue != null)
                    searchData.Add("OrganizationTypePkId", lkUpOrganizationName.EditValue);
                if (!string.IsNullOrEmpty(txtFromWho.Text))
                    searchData.Add(txtFromWho.Tag.ToString(), txtFromWho.Text);

                if (lkUpBranch.EditValue != null)
                    searchData.Add("ToBrId", lkUpBranch.EditValue);
                if (lkUpName.EditValue != null)
                    searchData.Add("StaffId", lkUpName.EditValue);
                if(lkUpToName.EditValue != null)
                    searchData.Add("ToStaffId", lkUpToName.EditValue);

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
                System.Diagnostics.Debug.WriteLine("Хайлтын мэдээллийг бэлтгэхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын мэдээллийг бэлтгэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Хайлтын мэдээллийг бэлтгэхэд алдаа гарлаа!", ex);
            }
            finally { searchData = null; }
        }

        private void SearchData()
        {
            Dictionary<string, object> searchData = null;
            try
            {
                searchData = PrepareSearch();
                if (searchData.Count.Equals(0)) 
                    Tool.BackFromSearch(this.sender, documentType);
                else 
                    Tool.SearchData(this.sender, documentType, searchData);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа!", ex);
            }
            finally { searchData = null; }
        }

        #endregion

        #region Event

        private void btn_Click(object sender, EventArgs e)
        {
            int tagId = 0;
            try
            {
                if ((sender as SimpleButton).Tag == null) return;
                tagId = int.Parse((sender as SimpleButton).Tag.ToString());
                if (tagId.Equals(0))
                    this.Dispose();
                else SearchData();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа!", ex.Message);
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode == Keys.Enter)
                    SearchData();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Txt, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Txt, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Txt, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа!", ex.Message);
            }
        }

        private void lkUp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode == Keys.Enter)
                    SearchData();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("LkUp, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LkUp, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("LkUp, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа!", ex.Message);
            }
        }

        private void date_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode == Keys.Enter) 
                    SearchData();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("LkUp, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LkUp, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("LkUp, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа!", ex.Message);
            }
        }

        private void memoEdit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Enter) || e.KeyCode == Keys.Enter)
                    SearchData();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Memo, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Memo, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Memo, Хайлтын мэдээлэлтэй ажиллахад алдаа гарлаа!", ex.Message);
            }
        }

        private void lkUp_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit control = null;
            DataRow[] dr = null;
            try
            {
                control = sender as LookUpEdit;
                if (control.EditValue == null) return;
                if (control.Tag == null) return;
                if (control.Tag.Equals(0))
                {
                    dr = organizationDt.Select(string.Format("PARENTPKID = {0}", control.EditValue));
                    if (dr == null || dr.Length.Equals(0))
                    {
                        Tool.ShowInfo(string.Format(MofDoc.Properties.Resources.AddOrganizationInfo, control.Text));
                        lkUpOrganizationName.Properties.DataSource = null;
                        txtFromWho.Enabled = lkUpOrganizationName.Enabled = false;
                        txtFromWho.Text = string.Empty;
                        return;
                    }
                    lkUpOrganizationName.Properties.DataSource = dr.CopyToDataTable();
                    txtFromWho.Enabled = lkUpOrganizationName.Enabled = true;
                    //lkUpOrganizationName.EditValue = dr.CopyToDataTable().DefaultView[0].Row[0];
                }
                else if (control.Tag.Equals(1))
                {

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

        private void Search_Disposed(object sender, EventArgs e)
        {
            try { Tool.BackFromSearch(this.sender, documentType); }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын цонхыг хаахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтын цонхыг хаахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Хайлтын цонхыг хаахад алдаа гарлаа!", ex.Message);
            }
        }

        #endregion

    }
}