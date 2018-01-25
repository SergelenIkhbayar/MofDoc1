using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System.Threading;
using MofDoc.Class;

namespace MofDoc.Forms
{
    public partial class Home : DevExpress.XtraEditors.XtraForm
    {

        #region Properties

        internal static Dictionary<int, XtraTabPage> pageList = null;
        private XtraTabPage mainTab = null;
        private Page.MainPage mainPage = null;

        #endregion

        #region Constructor Function

        public Home()
        {
            InitializeComponent();
            InitControl();
            InitTile();
        }

        #endregion

        #region Function

        private void InitControl()
        {
            this.Icon = Properties.Resources.info;
            this.Text = Properties.Resources.SystemFullName;
            this.StartPosition = FormStartPosition.CenterScreen;
            pageList = new Dictionary<int, XtraTabPage>();
            this.KeyPreview = true;
            this.KeyDown += Tool.Form_KeyDown;
            this.MinimumSize = this.Size;

            tabHome.Image = Properties.Resources.home;
            picTop.Image = Properties.Resources.Logo;
            tileCtl.ScrollMode = TileControlScrollMode.ScrollButtons;
            tileCtl.ItemDoubleClick += new TileItemClickEventHandler(tileCtl_ItemDoubleClick);

            toolStripStatusLabelDb.ForeColor = Color.Red;
            toolStripStatusLabelDb.Text = SqlConnector.serverIpAddress;
            toolStripStatusLabelDbUser.ForeColor = Color.Red;
            toolStripStatusLabelDbUser.Text = SqlConnector.serverAdminName;

            SubMenuItemQuit.Tag = 0;
            SubMenuItemQuit.Click += new EventHandler(AllMenuItem_Click);

            SubMenuItemConfigDb.Tag = 1;
            SubMenuItemConfigDb.Click += new EventHandler(AllMenuItem_Click);

            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(galleryControl, true, false);
        }

        private void InitTile()
        {
            TileGroup tileGroup = null;
            DataSet ds = null;
            ITileItem item = null;
            try
            {
                tileCtl.Groups.Clear();
                ds = Class.SqlConnector.GetByQuery(Properties.Resources.MOFDMSDatabases);
                if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0) return;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tileGroup = new TileGroup();
                    item = new TileItem();
                    item.Properties.IsLarge = true;

                    item.Elements.Add(GetTileElement(row["year"].ToString()));
                    item.Elements.Add(GetTileElement(row["year"].ToString(), row["name"].ToString()));
                    item.Tag = row["name"].ToString();

                    tileGroup.Items.Add(item);
                    tileCtl.Groups.Add(tileGroup);
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Дизайн зурж чадсангүй: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Дизайн зурж чадсангүй: " + ex.Message);
                Tool.ShowError("Дизайн зурж чадсангүй!", ex.Message);
            }
            finally { tileGroup = null; ds = null; item = null; }
        }

        private DevExpress.XtraEditors.TileItemElement GetTileElement(string year)
        {
            DevExpress.XtraEditors.TileItemElement tileItemElement = null;
            try
            {
                tileItemElement = new DevExpress.XtraEditors.TileItemElement();
                tileItemElement.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI Light", 17F);
                tileItemElement.Appearance.Hovered.Options.UseFont = true;
                tileItemElement.Appearance.Hovered.Options.UseTextOptions = true;
                tileItemElement.Appearance.Hovered.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
                tileItemElement.Appearance.Hovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
                tileItemElement.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI Light", 17F);
                tileItemElement.Appearance.Normal.Options.UseFont = true;
                tileItemElement.Appearance.Normal.Options.UseTextOptions = true;
                tileItemElement.Appearance.Normal.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
                tileItemElement.Appearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
                tileItemElement.Appearance.Selected.Font = new System.Drawing.Font("Segoe UI Light", 17F);
                tileItemElement.Appearance.Selected.Options.UseFont = true;
                tileItemElement.Appearance.Selected.Options.UseTextOptions = true;
                tileItemElement.Appearance.Selected.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
                tileItemElement.Appearance.Selected.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
                tileItemElement.Text = year;
                tileItemElement.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.Manual;
                tileItemElement.TextLocation = new System.Drawing.Point(4, 0);
                return tileItemElement;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetTileElement холбогдож чадсангүй: " + ex.Message);
                throw ex;
            }
            finally { tileItemElement = null; }
        }

        private DevExpress.XtraEditors.TileItemElement GetTileElement(string year, string name)
        {
            DevExpress.XtraEditors.TileItemElement tileItemElement = null;
            try
            {
                tileItemElement = new DevExpress.XtraEditors.TileItemElement();
                tileItemElement.Appearance.Hovered.Font = new System.Drawing.Font("Segoe UI", 9F);
                tileItemElement.Appearance.Hovered.Options.UseFont = true;
                tileItemElement.Appearance.Hovered.Options.UseTextOptions = true;
                tileItemElement.Appearance.Hovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                tileItemElement.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
                tileItemElement.Appearance.Normal.Options.UseFont = true;
                tileItemElement.Appearance.Normal.Options.UseTextOptions = true;
                tileItemElement.Appearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                tileItemElement.Appearance.Selected.Font = new System.Drawing.Font("Segoe UI", 9F);
                tileItemElement.Appearance.Selected.Options.UseFont = true;
                tileItemElement.Appearance.Selected.Options.UseTextOptions = true;
                tileItemElement.Appearance.Selected.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                tileItemElement.Text = string.Format("\n{0} холбогдсоноор {1} оны бичиг хэрэгтэй ажиллах боломжтой болно. Энд 2 дарж нэвтэрнэ үү.", name, year);
                tileItemElement.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.Manual;
                tileItemElement.TextLocation = new System.Drawing.Point(4, 27);
                return tileItemElement;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetTileElement холбогдож чадсангүй: " + ex.Message);
                throw ex;
            }
            finally { tileItemElement = null; }
        }

        private void DrawTab(int year, string domainId, string dbName)
        {
            try
            {
                if (pageList.ContainsKey(year))
                    tabCtl.SelectedTabPage = pageList.Single(t => t.Key.Equals(year)).Value;
                else
                {
                    mainPage = new Page.MainPage(year, domainId, dbName);
                    mainPage.Dock = DockStyle.Fill;
                    mainTab = new XtraTabPage();
                    mainTab.Text = string.Format("{0} оны бичиг хэрэг", year.ToString());
                    mainTab.Controls.Add(mainPage);

                    tabCtl.TabPages.Add(mainTab);
                    tabCtl.SelectedTabPage = mainTab;

                    mainPage.GetPermission();
                    pageList.Add(year, mainTab);
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Шинэ tab үүсгэж чадсангүй: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Шинэ tab үүсгэж чадсангүй: " + ex.Message);
                Tool.ShowError("Шинэ tab үүсгэж чадсангүй: ", ex.Message);
            }
        }

        #endregion

        #region Event

        private void AllMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Tag.Equals(0))
                this.Dispose();
            else if ((sender as ToolStripMenuItem).Tag.Equals(1))
            {
                ConfigDb configDb = new ConfigDb();
                configDb.StartPosition = FormStartPosition.CenterParent;
                if (configDb.ShowDialog().Equals(DialogResult.Cancel))
                    return;
                toolStripStatusLabelDb.Text = SqlConnector.serverIpAddress;
                toolStripStatusLabelDbUser.Text = SqlConnector.serverAdminName;
                InitTile();
            }
        }

        private void tileCtl_ItemDoubleClick(object sender, TileItemEventArgs e)
        {
            int docYear = int.Parse(e.Item.Text);
            if (pageList.ContainsKey(docYear))
            {
                tabCtl.SelectedTabPage = pageList.Single(t => t.Key.Equals(docYear)).Value;
                return;
            }

            Login login = new Login(docYear);
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog().Equals(DialogResult.Cancel))
                return;
            DrawTab(docYear, login.domainId, e.Item.Tag.ToString());
        }

        #endregion

    }
}