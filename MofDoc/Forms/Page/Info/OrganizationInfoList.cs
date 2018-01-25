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
using DevExpress.XtraTreeList.Columns;

namespace MofDoc.Forms.Page.Info
{
    public partial class OrganizationInfoList : XtraForm
    {

        #region Properties

        private string dbName;
        private bool isFromRegister = false;
        private DataTable dataTable = null;
        internal decimal? organizationPkId = null;
        internal decimal? locationPkId = null;

        #endregion

        #region Constructor

        public OrganizationInfoList(string dbName, decimal? locationPkId)
        {
            InitializeComponent();
            this.dbName = dbName;
            this.locationPkId = locationPkId;
            this.isFromRegister = locationPkId.Equals(null) ? false : true;
            InitControl();
            InitData();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            TreeListColumn column = null;
            try
            {
                this.Text = "Байгууллагын бүртгэл";
                this.MaximumSize = this.MinimumSize = this.Size;
                nbiChoose.Visible = isFromRegister;

                nbiChoose.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nbi_LinkClicked);
                nbiChoose.Tag = 0;

                nbiAdd.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nbi_LinkClicked);
                nbiAdd.Tag = 1;

                nbiEdit.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nbi_LinkClicked);
                nbiEdit.Tag = 2;

                nbiDelete.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(nbi_LinkClicked);
                nbiDelete.Tag = 3;

                txtSearch.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(txt_EditValueChanging);

                column = treeList.Columns.Add();
                column.Name = column.FieldName = "NAME";
                column.Caption = "Нэршил";
                column.Visible = true;

                treeList.ParentFieldName = "PARENTPKID";
                treeList.KeyFieldName = "PKID";
                treeList.OptionsSelection.MultiSelect = true;
                treeList.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг өгөгдлийн сангаас авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Мэдээллийг өгөгдлийн сангаас авчрахад алдаа гарлаа!", ex.Message);
            }
            finally { column = null; }
        }

        private void InitData()
        {
            Dictionary<string, string> filter = null;
            try
            {
                Tool.ShowWaiting();
                filter = new Dictionary<string, string>();
                filter.Add("STATUS", "='Y'");
                treeList.BeginUnboundLoad();
                treeList.DataSource = dataTable = SqlConnector.GetTable(dbName, "OrganizationType", new List<String>() { "PKID", "PARENTPKID", "NAME" }, filter);
                treeList.EndUnboundLoad();

                treeList.Focus();
                if (!isFromRegister)
                    treeList.Nodes.FirstNode.Selected = treeList.Nodes.FirstNode.Expanded = true;
                else
                {
                    treeList.SetFocusedNode(treeList.FindNodeByKeyID(locationPkId));
                    treeList.FindNodeByKeyID(locationPkId).Checked = treeList.FindNodeByKeyID(locationPkId).Selected = treeList.FindNodeByKeyID(locationPkId).Expanded = true;
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг өгөгдлийн сангаас авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Мэдээллийг өгөгдлийн сангаас авчрахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Мэдээллийг өгөгдлийн сангаас авчрахад алдаа гарлаа!", ex.Message);
            }
            finally { filter = null; Tool.CloseWaiting(); }
        }

        private void Choose()
        {
            string parentPkId = null;
            try
            {
                parentPkId = treeList.FocusedNode.GetValue("PARENTPKID").ToString();
                if (string.IsNullOrEmpty(parentPkId) || parentPkId.Equals(null))
                {
                    Tool.ShowInfo("Байршил сонгосон байна. Байгууллага сонгоно уу!");
                    return;
                }
                else
                {
                    this.locationPkId = decimal.Parse(parentPkId);
                    this.organizationPkId = decimal.Parse(treeList.FocusedNode.GetValue("PKID").ToString());
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллага сонгоход алдаа гарлаа: " + ex.InnerException.Message);
                throw new MofException("Байгууллага сонгоход алдаа гарлаа!", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллага сонгоход алдаа гарлаа: " + ex.Message);
                throw new MofException("Байгууллага сонгоход алдаа гарлаа!", ex);
            }
            finally { parentPkId = null; }
        }

        private void Add()
        {
            AddOrganizationInfo addOrganizationInfo = null;
            DialogResult dialog = System.Windows.Forms.DialogResult.Cancel;
            DataRow row = null;
            string locationPkId = null;
            try
            {
                locationPkId = treeList.FocusedNode.ParentNode == null ? treeList.FocusedNode["PKID"].ToString() : treeList.FocusedNode["PARENTPKID"].ToString();
                addOrganizationInfo = string.IsNullOrEmpty(locationPkId) ? new AddOrganizationInfo(dbName, null) : new AddOrganizationInfo(dbName, decimal.Parse(locationPkId));
                addOrganizationInfo.StartPosition = FormStartPosition.CenterParent;
                dialog = addOrganizationInfo.ShowDialog();
                if (dialog.Equals(DialogResult.Cancel))
                    return;
                row = dataTable.NewRow();
                row["PKID"] = addOrganizationInfo.organizationInfo.PkId;
                row["PARENTPKID"] = addOrganizationInfo.organizationInfo.ParentPkId;
                row["NAME"] = addOrganizationInfo.organizationInfo.Name;
                dataTable.Rows.Add(row);

                this.locationPkId = addOrganizationInfo.organizationInfo.ParentPkId;
                this.organizationPkId = addOrganizationInfo.organizationInfo.PkId;
                treeList.SetFocusedNode(treeList.FindNodeByKeyID(this.locationPkId));
                treeList.FindNodeByKeyID(this.locationPkId).Checked = treeList.FindNodeByKeyID(this.locationPkId).Selected = treeList.FindNodeByKeyID(this.locationPkId).Expanded = true;
                Tool.ShowSuccess("Амжилттай байгууллагын мэдээлэл нэмлээ!");
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын мэдээллийг нэмэхэд алдаа гарлаа: " + ex.InnerException.Message);
                throw new MofException("Байгууллагын мэдээллийг нэмэхэд алдаа гарлаа!", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын мэдээллийг нэмэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Байгууллагын мэдээллийг нэмэхэд алдаа гарлаа!", ex);
            }
            finally { addOrganizationInfo = null; locationPkId = null; row = null; }
        }

        private void Edit()
        {
            AddOrganizationInfo addOrganizationInfo = null;
            DialogResult dialog = System.Windows.Forms.DialogResult.Cancel;
            DataRow row = null;
            string locationFocusedPkId = null;
            try
            {
                locationFocusedPkId = treeList.FocusedNode.GetValue("PARENTPKID").ToString();
                if (string.IsNullOrEmpty(locationFocusedPkId) || locationFocusedPkId.Equals(null))
                {
                    Tool.ShowInfo("Байршил сонгосон байна.Байгууллага засах тул байгууллага сонгоно уу!");
                    return;
                }
                this.locationPkId = decimal.Parse(locationFocusedPkId);
                this.organizationPkId = decimal.Parse(treeList.FocusedNode.GetValue("PKID").ToString());
                addOrganizationInfo = new AddOrganizationInfo(dbName, locationPkId, organizationPkId, treeList.FocusedNode.GetValue("NAME").ToString());
                dialog = addOrganizationInfo.ShowDialog();
                if (dialog.Equals(DialogResult.Cancel))
                    return;
                row = dataTable.Select(string.Format("PKID={0}", this.organizationPkId)).First();
                row["NAME"] = addOrganizationInfo.organizationInfo.Name;
                Tool.ShowSuccess("Амжилттай байгууллагын мэдээлэл заслаа!");
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын мэдээллийг засахад алдаа гарлаа: " + ex.InnerException.Message);
                throw new MofException("Байгууллагын мэдээллийг засахад алдаа гарлаа!", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын мэдээллийг засахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Байгууллагын мэдээллийг засахад алдаа гарлаа!", ex);
            }
            finally { addOrganizationInfo = null; locationFocusedPkId = null; row = null; }
        }

        private void Delete()
        {
            string locationFocusedPkId = null;
            Dictionary<string, string> filters = null;
            DataRow row = null;
            DialogResult dialog = System.Windows.Forms.DialogResult.No;
            try
            {
                locationFocusedPkId = treeList.FocusedNode.GetValue("PARENTPKID").ToString();
                if (string.IsNullOrEmpty(locationFocusedPkId) || locationFocusedPkId.Equals(null))
                {
                    Tool.ShowInfo("Байршил сонгосон байна.Байгууллага устгах тул байгууллага сонгоно уу!");
                    return;
                }

                dialog = MessageBox.Show("Байгууллагын мэдээлэл устгахдаа итгэлтэй байна уу?", "Байгууллагын мэдээлэл устгах", MessageBoxButtons.YesNo);
                if (dialog.Equals(DialogResult.No))
                    return;

                this.locationPkId = decimal.Parse(locationFocusedPkId);
                this.organizationPkId = decimal.Parse(treeList.FocusedNode.GetValue("PKID").ToString());
                filters = new Dictionary<string, string>();
                filters.Add("PKID", string.Format("= {0}", organizationPkId.ToString()));
                filters.Add("PARENTPKID", string.Format("= {0}", locationPkId.ToString()));

                SqlConnector.Delete(dbName, "OrganizationType", filters);
                row = dataTable.Select(string.Format("PKID={0}", this.organizationPkId)).First();
                row.Delete();
                Tool.ShowSuccess("Амжилттай байгууллагын мэдээлэл устгалаа!");
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын мэдээллийг устгахад алдаа гарлаа: " + ex.InnerException.Message);
                throw new MofException("Байгууллагын мэдээллийг устгахад алдаа гарлаа!", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Байгууллагын мэдээллийг устгахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Байгууллагын мэдээллийг устгахад алдаа гарлаа!", ex);
            }
            finally { locationFocusedPkId = null; filters = null; row = null; }
        }

        #endregion

        #region Event

        private void nbi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                int caseSwitch = int.Parse((sender as DevExpress.XtraNavBar.NavBarItem).Tag.ToString());
                switch (caseSwitch)
                {
                    case 0:
                        Choose();
                        break;
                    case 1:
                        Add();
                        break;
                    case 2:
                        Edit();
                        break;
                    case 3:
                        Delete();
                        break;
                    default:
                        throw new MofException("Товчны код тодорхой бус байна!");
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Цэсний товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Цэсний товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Цэсний товч дарахад алдаа гарлаа!", ex.Message);
            }
        }

        private void txt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        #endregion

    }
}