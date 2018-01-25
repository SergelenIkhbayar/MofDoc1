using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MofDoc.Class;

namespace MofDoc.Forms
{
    public partial class ConfigDb : Form
    {

        #region Properties

        private bool isUpdated = false;

        #endregion

        #region Constructor

        public ConfigDb()
        {
            InitializeComponent();
            InitControl();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Text = "Өгөгдлийн сангийн холболт";
            this.Icon = Properties.Resources.info;
            this.MaximumSize = this.MinimumSize = this.Size;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(ConfigDb_KeyDown);
            this.FormClosing += new FormClosingEventHandler(ConfigDb_FormClosing);
            txtDbPassword.Properties.PasswordChar = char.Parse("*");

            txtDbAddress.Text = SqlConnector.serverIpAddress;
            txtDbUsername.Text = SqlConnector.serverAdminName;
            txtDbPassword.Text = SqlConnector.serverAdminPass;

            btnBack.Tag = 0;
            btnBack.Click += new EventHandler(btn_Click);

            btnUpdate.Tag = 1;
            btnUpdate.Click += new EventHandler(btn_Click);
        }

        private void UpdateSqlConnection()
        {
            bool isActionProgress = false;
            try
            {
                if (string.IsNullOrEmpty(txtDbAddress.Text))
                {
                    txtDbAddress.ErrorText = "Энэ хэсгийг заавал бөглөнө үү";
                    if (!isActionProgress)
                    {
                        isActionProgress = true;
                        txtDbAddress.Focus();
                    }
                }
                if (string.IsNullOrEmpty(txtDbUsername.Text))
                {
                    txtDbUsername.ErrorText = "Энэ хэсгийг заавал бөглөнө үү";
                    if (!isActionProgress)
                    {
                        isActionProgress = true;
                        txtDbUsername.Focus();
                    }
                }
                if (isActionProgress)
                    return;
                if (SqlConnector.serverIpAddress.Equals(txtDbAddress.Text) && SqlConnector.serverAdminName.Equals(txtDbUsername.Text) && SqlConnector.serverAdminPass.Equals(txtDbPassword.Text))
                {
                    isUpdated = false;
                    Tool.ShowInfo("Өөрчлөлт хийгээгүй байна!");
                    this.Dispose();
                    return;
                }
                Tool.ShowWaiting();
                if (!Tool.ValidateConnection(txtDbAddress.Text, txtDbUsername.Text, txtDbPassword.Text))
                    return;
                SqlConnector.serverIpAddress = txtDbAddress.Text;
                SqlConnector.serverAdminName = txtDbUsername.Text;
                SqlConnector.serverAdminPass = txtDbPassword.Text;
                SqlConnector.ResetConnection();
                Tool.CloseWaiting();
                Tool.ShowSuccess("Амжилттай өгөгдлийн сангийн мэдээллийг шинэчлэлээ!");
                isUpdated = true;
                this.Dispose();
            }
            catch (MofException ex) { Tool.ShowError(ex.Message, ex.InnerException.Message); }
            catch (Exception ex) { Tool.ShowError("Өгөгдлийн сангийн мэдээллийг шинэчлэхэд алдаа гарлаа!", ex.Message); }
            finally { Tool.CloseWaiting(); }
        }

        #endregion

        #region Event

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                if ((sender as DevExpress.XtraEditors.SimpleButton).Tag.Equals(0))
                    this.Dispose();
                else
                    UpdateSqlConnection();
            }
            catch (MofException ex) { Tool.ShowError(ex.Message, ex.InnerException.Message); }
            catch (Exception ex) { Tool.ShowError("Алдаа!", ex.Message); }
        }

        private void ConfigDb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Escape))
                (sender as Form).Dispose();
            else if (e.KeyData.Equals(Keys.Enter))
                UpdateSqlConnection();
        }

        private void ConfigDb_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = isUpdated ? DialogResult.OK : DialogResult.Cancel;
        }

        #endregion

    }
}