using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MofDoc.Class;
using System.DirectoryServices;

namespace MofDoc.Forms
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {

        #region Properties

        private bool IsAuthenticate = false;
        internal string domainId = null;

        #endregion

        #region Constructor

        public Login(int year)
        {
            InitializeComponent();
            InitControl(year);
        }

        #endregion

        #region Function

        private void InitControl(int year)
        {
            this.Text = string.Format("{0} оны бичиг хэргийн мэдээлэл рүү нэвтрэх", year);
            this.Icon = Properties.Resources.Login;
            this.MaximumSize = this.MinimumSize = this.Size;
            this.FormClosing += new FormClosingEventHandler(Login_FormClosing);

            List<string> listAuthentication = new List<string>();
            listAuthentication.AddRange(new string[]{
                "Windows Authentication", "Domain Authentication(User)"
            });
            lkUpAuth.Properties.DataSource = listAuthentication;
            lkUpAuth.EditValue = "Windows Authentication";
            lkUpAuth.EditValueChanged += new EventHandler(lkUpAuth_EditValueChanged);

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Login_KeyDown);

            btnCancel.Tag = 0;
            btnCancel.Click += new EventHandler(btn_Click);

            btnLogin.Tag = 1;
            btnLogin.Click += new EventHandler(btn_Click);

            AuthControl(true);
            txtLoginPass.Properties.PasswordChar = char.Parse("*");
        }

        private void ClearControl()
        {
            txtLoginName.Text = txtLoginPass.Text = string.Empty;
            ckRemember.Checked = false;
        }

        private void AuthControl(bool isWindows)
        {
            if (isWindows)
            {
                ClearControl();
                txtLoginName.Enabled = txtLoginPass.Enabled = false;
                ckRemember.Enabled = false;
            }
            else
            {
                txtLoginName.Enabled = txtLoginPass.Enabled = true;
                ckRemember.Enabled = true;
            }
        }

        private bool isFilledControl()
        {
            bool isActionProgress = false;
            if (string.IsNullOrEmpty(txtLoginName.Text))
            {
                txtLoginName.ErrorText = string.Format("{0} домайн хэрэглэгчийн нэрийг бөглөнө үү!", Properties.Resources.DomainLowerCase);
                if (!isActionProgress)
                {
                    isActionProgress = true;
                    txtLoginName.Focus();
                }
            }
            if (string.IsNullOrEmpty(txtLoginPass.Text))
            {
                txtLoginPass.ErrorText = string.Format("{0} домайн хэрэглэгчийн нууц үгийг бөглөнө үү!", Properties.Resources.DomainLowerCase);
                if (!isActionProgress)
                {
                    isActionProgress = true;
                    txtLoginPass.Focus();
                }
            }
            return !isActionProgress;
        }

        private void Authenticate()
        {
            string tempName = null;
            bool isWinAuth = lkUpAuth.EditValue.Equals("Windows Authentication");
            try
            {
                if (isWinAuth)
                {
                    if (!Environment.GetEnvironmentVariable("USERDNSDOMAIN").Equals(Properties.Resources.Domain))
                        throw new MofException(string.Format("{0} домайнд холбогдоогүй байна!", Properties.Resources.DomainLowerCase));

                    domainId = Environment.UserName;
                    Tool.ShowSuccess(string.Format("{0} хэрэглэгч амжилттай нэвтэрлээ!", domainId));
                    IsAuthenticate = true;
                    this.Dispose();
                }
                else
                {
                    if (!isFilledControl())
                        return;
                    Tool.ShowWaiting();
                    using (DirectoryEntry deDirEntry = new DirectoryEntry(Properties.Resources.DomainLink,
                                         txtLoginName.Text,
                                         txtLoginPass.Text,
                                         AuthenticationTypes.Secure))
                    {
                        try
                        {
                            tempName = deDirEntry.Name;
                            domainId = deDirEntry.Username;
                        }
                        catch (DirectoryServicesCOMException ex)
                        {
                            if (ex.ErrorCode.Equals(-2147023570))
                            {
                                System.Diagnostics.Debug.WriteLine("Нэвтрэхэд алдаа гарлаа: " + ex.Message);
                                throw new MofException(string.Format("{0} домайны {1} хэрэглэгчийн нэр эсвэл нууц үг буруу байна!", Properties.Resources.DomainLowerCase, txtLoginName.Text));
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Нэвтрэхэд алдаа гарлаа: " + ex.Message);
                                throw new MofException(string.Format("{0} домайнтай холбогдож чадахгүй байна, холболтыг шалгана уу!", Properties.Resources.DomainLowerCase));
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Нэвтрэхэд алдаа гарлаа: " + ex.Message);
                            throw ex;
                        }
                    }
                    Tool.CloseWaiting();
                    Tool.ShowSuccess(string.Format("{0} хэрэглэгч амжилттай нэвтэрлээ!", domainId));
                    IsAuthenticate = true;
                    this.Dispose();
                }
            }
            catch (MofException ex) { Tool.CloseWaiting(); Tool.ShowError("Нэвтрэх боломжгүй байна!", ex.Message); }
            catch (Exception ex) { Tool.CloseWaiting(); Tool.ShowError("Нэвтрэх боломжгүй байна!", ex.Message); }
            finally { tempName = null; }
        }

        #endregion

        #region Event

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Escape))
                this.Dispose();
            if (e.KeyData.Equals(Keys.Enter))
                Authenticate();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if ((sender as DevExpress.XtraEditors.SimpleButton).Tag.Equals(0)) Dispose();
            else Authenticate();
        }

        private void lkUpAuth_EditValueChanged(object sender, EventArgs e)
        {
            if (lkUpAuth.EditValue.Equals("Windows Authentication"))
                AuthControl(true);
            else AuthControl(false);
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = IsAuthenticate ? DialogResult.OK : DialogResult.Cancel;
        }

        #endregion

    }
}