using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel;
using System.Threading;
using MofDoc.Forms.Dialog;
using MofDoc.Forms.Page;
using MofDoc.Enum;
using MofDoc.Report.Income;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using DevExpress.XtraReports.UI;
using System.DirectoryServices;
using System.Net.Mail;
using DevExpress.XtraEditors;
using MofDoc.Report.Income.Entry;
using DevExpress.XtraSplashScreen;

namespace MofDoc.Class
{
    public class Tool
    {
        /* 
        -- PERMISSION ID:
        -- 1 - ADMINISTRATOR
        -- 2 - DIVISION DOCUMENTATOR/HEAD
        -- 3 - DEPARTMENT DOCUMENTATOR/HEAD
        -- 4 - MINISTER/VICE MINISTER/STATE SECRETARY SUPPORTER
        -- 5 - NORMAL EMPLOYEE 
        -- 6 - HUMAN RESOURCE
        */

        private static Thread thread = null;
        private static List<Permission> permissions = null;

        internal static string userDomainId = null;
        internal static decimal userStaffId = 0;
        internal static decimal userBrId = 0;
        internal static decimal userMainBrId = 0;
        internal static string userFName = null;
        internal static string userLName = null;
        internal static string xmlStr = null;
        internal static decimal permissionId = 0;

        private delegate void SendNEMailOnSaveDelegate();
        private static MailParameter mailParameter = null;
        private static List<DateType> dateTypes = null;
        private static int count = 0;

        private static SplashScreenManager splashManager = null;
        private static SplashFormProperties splashProperties = null;

        internal static void ShowWaiting()
        {
            /*thread = new Thread(ShowFunction);
            thread.Start();*/

            if (splashManager == null)
            {
                splashProperties = new SplashFormProperties();
                splashManager = new SplashScreenManager(typeof(WaiMessage), splashProperties);
            }

            if (splashManager.IsSplashFormVisible) return;
            splashManager.ShowWaitForm();
        }

        /*private static void ShowFunction()
        {
            using (ActionProgress actionProgress = new ActionProgress())
            {
                actionProgress.StartPosition = FormStartPosition.CenterParent;
                actionProgress.ShowDialog();
            }
        }*/

        internal static void CloseWaiting()
        {
            /*if (thread == null) return;
            if (thread.IsAlive)
                thread.Abort();
            thread = null;*/

            if (splashManager == null)
            {
                splashProperties = new SplashFormProperties();
                splashManager = new SplashScreenManager(typeof(WaiMessage), splashProperties);
            }
            if (!splashManager.IsSplashFormVisible) return;
            splashManager.CloseWaitForm();
        }

        internal static void ShowError(string shortDesc, string longdesc)
        {
            CloseWaiting();
            using (ErrorMessage dialog = new ErrorMessage(shortDesc, longdesc))
            {
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.ShowDialog();
            }
        }

        internal static void ShowSuccess()
        {
            CloseWaiting();
            using (SuccessMessage dialog = new SuccessMessage())
            {
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.ShowDialog();
            }
        }

        internal static void ShowSuccess(string desc)
        {
            CloseWaiting();
            using (SuccessMessage dialog = new SuccessMessage(desc))
            {
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.ShowDialog();
            }
        }

        internal static void ShowInfo()
        {
            CloseWaiting();
            using (InfoMessage dialog = new InfoMessage())
            {
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.ShowDialog();
            }
        }

        internal static void ShowInfo(string desc)
        {
            CloseWaiting();
            using (InfoMessage dialog = new InfoMessage(desc))
            {
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.ShowDialog();
            }
        }

        internal static bool ValidateConnection(string dbAddress, string dbUsername, string dbPassword)
        {
            string connStr = null;
            SqlConnection mssqlConn = null;
            try
            {
                connStr = string.Format("Initial Catalog=master; Data Source={0}; User ID={1}; Password={2}", dbAddress, dbUsername, dbPassword);
                mssqlConn = new SqlConnection(connStr);
                mssqlConn.Open();
                return true;
            }
            catch (MofException ex) { throw ex; }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холболтын үеийн алдаа: {1}", dbAddress, ex.Message));
                throw new MofException("Өгөгдлийн сангийн холболтыг шалгахад алдаа гарлаа!", ex);
            }
            finally
            {
                connStr = null;
                if (mssqlConn != null)
                {
                    mssqlConn.Close();
                    mssqlConn.Dispose();
                    mssqlConn = null;
                }
            }
        }

        internal static bool ValidateConnection(Enum.DatabaseType databaseType, string dbAddress, string dbUsername, string dbPassword)
        {
            string connStr = null;
            SqlConnection mssqlConn = null;
            OracleConnection oracleConn = null;
            try
            {
                if (databaseType.Equals(Enum.DatabaseType.MSSQL))
                {
                    connStr = string.Format("Initial Catalog=master; Data Source={0}; User ID={1}; Password={2}", dbAddress, dbUsername, dbPassword);
                    mssqlConn = new SqlConnection(connStr);
                    mssqlConn.Open();
                    return true;
                }
                else if (databaseType.Equals(Enum.DatabaseType.Oracle))
                {
                    connStr = string.Format("Data Source={0};User ID={1};Password={2};", dbAddress, dbUsername, dbPassword);
                    oracleConn = new OracleConnection(connStr);
                    oracleConn.Open();
                    return true;
                }
                else { throw new MofException(string.Format("Бүртгэгдээгүй өгөгдлийн сан шалгуулсан байна: {0}", databaseType.ToString())); }
            }
            catch (MofException ex) { throw ex; }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} сервертэй холболтын үеийн алдаа: {1}", dbAddress, ex.Message));
                throw new MofException("Өгөгдлийн сангийн холболтыг шалгахад алдаа гарлаа!", ex);
            }
            finally
            {
                connStr = null;
                if (mssqlConn != null)
                {
                    mssqlConn.Close();
                    mssqlConn.Dispose();
                    mssqlConn = null;
                }
                if (oracleConn != null)
                {
                    oracleConn.Close();
                    oracleConn.Dispose();
                    oracleConn = null;
                }
            }
        }

        internal static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            Forms.Home homePage = null;
            try
            {
                homePage = sender as Forms.Home;
                if (e.KeyCode.Equals(Keys.Escape))
                {
                    if (homePage.tabCtl.TabPages == null || homePage.tabCtl.TabPages.Count.Equals(1))
                        homePage.Dispose();
                }
                else if (e.KeyCode.Equals(Keys.F1))
                {
                    if (homePage.tabCtl.TabPages == null
                        || homePage.tabCtl.TabPages.Count.Equals(1)
                        || homePage.tabCtl.SelectedTabPage.Controls[0] == null
                        || !homePage.tabCtl.SelectedTabPage.Controls[0].Tag.Equals("MainPage"))
                        return;

                    if (homePage.tabCtl.SelectedTabPage.Controls[0] is Forms.Page.MainPage)
                    {
                        var mainPage = ((MofDoc.Forms.Page.MainPage)(homePage.tabCtl.SelectedTabPage.Controls[0]));
                        if (mainPage.mainTabCtl.TabPages == null || mainPage.mainTabCtl.SelectedTabPage.Controls[0] == null)
                            return;
                        if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Income.IncomeList)
                        {
                            var incomeList = ((MofDoc.Forms.Page.Income.IncomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                            incomeList.SearchDocument();
                        }
                        else if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Outcome.OutcomeList)
                        {
                            var outcomeList = ((MofDoc.Forms.Page.Outcome.OutcomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                            outcomeList.SearchDocument();
                        }
                    }
                }
                else if (e.KeyCode.Equals(Keys.F2))
                {
                    if (!permissionId.Equals(1)) return;
                    if (homePage.tabCtl.TabPages == null
                        || homePage.tabCtl.TabPages.Count.Equals(1)
                        || homePage.tabCtl.SelectedTabPage.Controls[0] == null
                        || !homePage.tabCtl.SelectedTabPage.Controls[0].Tag.Equals("MainPage"))
                        return;

                    if (homePage.tabCtl.SelectedTabPage.Controls[0] is Forms.Page.MainPage)
                    {
                        var mainPage = ((MofDoc.Forms.Page.MainPage)(homePage.tabCtl.SelectedTabPage.Controls[0]));
                        if (mainPage.mainTabCtl.TabPages == null || mainPage.mainTabCtl.SelectedTabPage.Controls[0] == null)
                            return;
                        if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Income.IncomeList)
                        {
                            var incomeList = ((MofDoc.Forms.Page.Income.IncomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                            incomeList.AddDirectRegister();
                        }
                        else if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Outcome.OutcomeList)
                        {
                            var outcomeList = ((MofDoc.Forms.Page.Outcome.OutcomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                            outcomeList.AddOutcome();
                        }
                    }
                }
                else if (e.KeyCode.Equals(Keys.F3))
                {
                    if (!permissionId.Equals(1)) return;
                    if (homePage.tabCtl.TabPages == null
                        || homePage.tabCtl.TabPages.Count.Equals(1)
                        || homePage.tabCtl.SelectedTabPage.Controls[0] == null
                        || !homePage.tabCtl.SelectedTabPage.Controls[0].Tag.Equals("MainPage"))
                        return;

                    if (homePage.tabCtl.SelectedTabPage.Controls[0] is Forms.Page.MainPage)
                    {
                        var mainPage = ((MofDoc.Forms.Page.MainPage)(homePage.tabCtl.SelectedTabPage.Controls[0]));
                        if (mainPage.mainTabCtl.TabPages == null || mainPage.mainTabCtl.SelectedTabPage.Controls[0] == null)
                            return;
                        if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Income.IncomeList)
                        {
                            var incomeList = ((MofDoc.Forms.Page.Income.IncomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                            incomeList.AddCard();
                        }
                    }
                }
                else if (e.KeyCode.Equals(Keys.F5))
                {
                    if (homePage.tabCtl.TabPages == null
                        || homePage.tabCtl.TabPages.Count.Equals(1)
                        || homePage.tabCtl.SelectedTabPage.Controls[0] == null
                        || !homePage.tabCtl.SelectedTabPage.Controls[0].Tag.Equals("MainPage"))
                        return;

                    if (homePage.tabCtl.SelectedTabPage.Controls[0] is Forms.Page.MainPage)
                    {
                        var mainPage = ((MofDoc.Forms.Page.MainPage)(homePage.tabCtl.SelectedTabPage.Controls[0]));
                        if (mainPage.mainTabCtl.TabPages == null || mainPage.mainTabCtl.SelectedTabPage.Controls[0] == null)
                            return;
                        if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Income.IncomeList)
                        {
                            var incomeList = ((MofDoc.Forms.Page.Income.IncomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                            incomeList.RefreshControl();
                        }

                        if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Outcome.OutcomeList)
                        {
                            var outcomeList = ((MofDoc.Forms.Page.Outcome.OutcomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                            outcomeList.RefreshControl();
                        }
                    }
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Hotkey буюу түргэн товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError(ex.Message, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Hotkey буюу түргэн товч дарахад алдаа гарлаа: " + ex.Message);
                Tool.ShowError("Hotkey буюу түргэн товч дарахад алдаа гарлаа!", ex.Message);
            }
            finally { homePage = null; }
        }

        internal static string DateTimeNow() { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }

        internal static string ConvertDateTime(DateTime dateTime)
        {
            try { return dateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа!", ex);
            }
        }

        internal static string ConvertDateTime(object dateTime)
        {
            DateTime currentDateTime;
            try
            {
                if (!DateTime.TryParse(dateTime.ToString(), out currentDateTime))
                    throw new MofException("Огноог хөрвүүлж чадсангүй!");
                return currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа!", ex);
            }
        }

        internal static string ConvertNonTimeDateTime(object dateTime)
        {
            DateTime currentDateTime;
            try
            {
                if (!DateTime.TryParse(dateTime.ToString(), out currentDateTime))
                    throw new MofException("Огноог хөрвүүлж чадсангүй!");
                return currentDateTime.ToString("yyyy-MM-dd");
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа!", ex);
            }
        }

        internal static string ConvertNonTimeDateTime(object dateTime, string format)
        {
            DateTime currentDateTime;
            try
            {
                if (!DateTime.TryParse(dateTime.ToString(), out currentDateTime))
                    throw new MofException("Огноог хөрвүүлж чадсангүй!");
                return currentDateTime.ToString(format);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Програмын огноог өгөгдлийн сангийн формат руу хөрвүүлэхэд алдаа гарлаа!", ex);
            }
        }

        internal static decimal NewPkId()
        {
            Random random = null;
            string pkId = null;
            int randomNum = 0;
            try
            {
                if (count % 2 == 0)
                {
                    random = new Random();
                    randomNum = random.Next(0, 10);
                    pkId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    pkId += randomNum;
                }
                else
                {
                    random = new Random();
                    randomNum = random.Next(0, 10);
                    pkId = DateTime.Now.AddSeconds(randomNum).ToString("yyyyMMddHHmmssfff");
                    pkId += randomNum;
                }
                count++;
                return decimal.Parse(pkId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Програмаас PKID-ыг гаргахад(Generate PKID) алдаа гарлаа: " + ex.Message);
                throw new MofException("Програмаас PKID-ыг гаргахад(Generate PKID) алдаа гарлаа!", ex);
            }
            finally { random = null; pkId = null; }
        }

        internal static string SetPdf()
        {
            OpenFileDialog openDialog = null;
            CookieContainer cookies = null;
            NameValueCollection queryStr = null;
            string targetPath, tmpFileName = null;
            try
            {
                openDialog = new OpenFileDialog();
                openDialog.Filter = "PDF файл (*.pdf)|*.pdf;*.PDF |Бүх файл (*.*)|*.*";
                if (!openDialog.ShowDialog().Equals(DialogResult.OK)) return null;

                cookies = new CookieContainer();
                queryStr = new NameValueCollection();
                targetPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
                if (!System.IO.Directory.Exists(targetPath))
                    System.IO.Directory.CreateDirectory(targetPath);

                tmpFileName = DateTime.Now.Ticks.ToString("x") + ".pdf";
                if (targetPath.Trim().Substring(targetPath.Trim().Length - 1, 1) != "\\")
                    targetPath = targetPath.Trim() + "\\";

                System.IO.File.Copy(openDialog.FileName, targetPath + tmpFileName, true);
                queryStr["uname"] = "batgerel";
                queryStr["passwd"] = "baagii";

                Tool.UploadFileEx(targetPath + tmpFileName, "http://cmc/docs/batgerel.php", "uploaded", "image/pjpeg", queryStr, cookies);
                return tmpFileName;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Файл хавсаргахад алдаа гарлаа: " + ex.InnerException.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Файл хавсаргахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Файл хавсаргахад алдаа гарлаа!", ex);
            }
            finally { openDialog = null; cookies = null; queryStr = null; tmpFileName = null; }
        }

        private static void UploadFileEx(string uploadfile, string url, string fileFormName, string contenttype, NameValueCollection querystring, CookieContainer cookies)
        {
            StreamReader sr = null;
            FileStream fileStream = null;
            try
            {
                if ((fileFormName == null) ||
                    (fileFormName.Length == 0))
                {
                    fileFormName = "file";
                }

                if ((contenttype == null) ||
                    (contenttype.Length == 0))
                {
                    contenttype = "application/octet-stream";
                }


                string postdata;
                postdata = "?";
                if (querystring != null)
                {
                    foreach (string key in querystring.Keys)
                    {
                        postdata += key + "=" + querystring.Get(key) + "&";
                    }
                }
                Uri uri = new Uri(url + postdata);


                string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
                webrequest.CookieContainer = cookies;
                webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
                webrequest.Method = "POST";


                // Build up the post message header
                StringBuilder sb = new StringBuilder();
                sb.Append("--");
                sb.Append(boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data; name=\"");
                sb.Append(fileFormName);
                sb.Append("\"; filename=\"");
                sb.Append(Path.GetFileName(uploadfile));
                sb.Append("\"");
                sb.Append("\r\n");
                sb.Append("Content-Type: ");
                sb.Append(contenttype);
                sb.Append("\r\n");
                sb.Append("\r\n");

                string postHeader = sb.ToString();
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

                // Build the trailing boundary string as a byte array
                // ensuring the boundary appears on a line by itself
                //byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

                fileStream = new FileStream(uploadfile, FileMode.Open, FileAccess.Read);
                long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
                webrequest.ContentLength = length;

                Stream requestStream = webrequest.GetRequestStream();

                // Write out our post header
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                // Write out the file contents
                byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    requestStream.Write(buffer, 0, bytesRead);


                // Write out the trailing boundary
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                WebResponse responce = webrequest.GetResponse();
                Stream s = responce.GetResponseStream();
                sr = new StreamReader(s);

                //return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Файл хавсаргахад алдаа гарлаа: " + ex.InnerException.Message);
                throw new MofException("Файл хавсаргахад алдаа гарлаа!", ex);
            }
            finally { sr = null; fileStream = null; }
        }

        internal static void PrintReport(int reportId, string reportDesc, List<object> parameter)
        {
            try { PrintReport(reportId, reportDesc, parameter, null); }
            catch (MofException ex) { throw ex; }
            catch (Exception ex) { throw new MofException(string.Format("{0} гаргахад алдаа гарлаа: {1}", reportDesc, ex.Message), ex); }
        }

        internal static void PrintReport(int reportId, string reportDesc, List<object> parameter, List<object> subParameter)
        {
            ReportPrintTool reportTool = null;
            dsRptIn directRegisterReport = null;
            dsRptCCFS cardReport = null;

            dsRptInDocs incomeReport = null;
            dsRptDirect incomeDirect = null;
            dsRptIncomePerson incomePerson = null;
            dsControlCardBackS controlcardBack = null;
            try
            {
                if (reportId.Equals(0))
                {
                    directRegisterReport = new dsRptIn(parameter[0].ToString(), parameter[1].ToString(), parameter[2].ToString(), parameter[3].ToString(), parameter[4].ToString(), parameter[5].ToString(),
                        parameter[6].ToString(), parameter[7].ToString(), parameter[8].ToString(), parameter[9].ToString(), parameter[10].ToString(), parameter[11].ToString(), parameter[12].ToString(), parameter[13].ToString(),
                        parameter[14].ToString(), parameter[15].ToString());
                    directRegisterReport.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(directRegisterReport);
                }
                else if (reportId.Equals(1))
                {
                    cardReport = new dsRptCCFS(parameter[0].ToString(), parameter[1].ToString(), parameter[2].ToString(), parameter[3].ToString(), parameter[4].ToString(), parameter[5].ToString(),
                        parameter[6].ToString(), parameter[7].ToString(), parameter[8].ToString(), parameter[9].ToString(), parameter[10].ToString(), parameter[11].ToString(), parameter[12].ToString(), parameter[13].ToString(),
                        parameter[14].ToString(), parameter[15].ToString(), parameter[16].ToString(), parameter[17].ToString());
                    cardReport.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(cardReport);
                }
                else if (reportId.Equals(2))
                {
                    incomeReport = new dsRptInDocs(subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeReport.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeReport);
                }
                else if (reportId.Equals(3))
                {
                    incomeReport = new dsRptInDocs(subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeReport.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeReport);
                }
                else if (reportId.Equals(4))
                {
                    incomeDirect = new dsRptDirect(subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeDirect.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeDirect);
                }
                else if (reportId.Equals(5))
                {
                    incomeReport = new dsRptInDocs(true, subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeReport.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeReport);
                }
                else if (reportId.Equals(6))
                {
                    incomeReport = new dsRptInDocs(true, subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeReport.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeReport);
                }
                else if (reportId.Equals(7))
                {
                    incomeReport = new dsRptInDocs(true, subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeReport.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeReport);
                }
                else if (reportId.Equals(8))
                {
                    incomeDirect = new dsRptDirect(subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeDirect.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeDirect);
                }
                else if (reportId.Equals(9))
                {
                    incomeDirect = new dsRptDirect(subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeDirect.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeDirect);
                }
                else if (reportId.Equals(10))
                {
                    incomePerson = new dsRptIncomePerson(subParameter[0].ToString(), subParameter[1].ToString(), parameter[0] as List<DocumentReport>);
                    incomePerson.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomePerson);
                }
                else if (reportId.Equals(11))
                {
                    incomePerson = new dsRptIncomePerson(subParameter[0].ToString(), subParameter[1].ToString(), parameter[0] as List<DocumentReport>);
                    incomePerson.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomePerson);
                }
                else if (reportId.Equals(12))
                {
                    incomePerson = new dsRptIncomePerson(subParameter[0].ToString(), subParameter[1].ToString(), parameter[0] as List<DocumentReport>);
                    incomePerson.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomePerson);
                }
                else if (reportId.Equals(13))
                {
                    incomePerson = new dsRptIncomePerson(subParameter[0].ToString(), subParameter[1].ToString(), parameter[0] as List<DocumentReport>);
                    incomePerson.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomePerson);
                }
                else if (reportId.Equals(14))
                {
                    controlcardBack = new dsControlCardBackS();
                    controlcardBack.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(controlcardBack);
                }
                else if (reportId.Equals(15))
                {
                    incomeReport = new dsRptInDocs(subParameter[0].ToString(), subParameter[1].ToString(), subParameter[2].ToString(),
                        subParameter[3].ToString(), subParameter[4].ToString(), subParameter[5].ToString(), parameter[0] as List<Document>);
                    incomeReport.PrintingSystem.ShowMarginsWarning = false;
                    reportTool = new ReportPrintTool(incomeReport);
                }
                reportTool.ShowPreviewDialog();
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} гаргахад алдаа гарлаа: {1}", reportDesc, ex.InnerException.Message));
                throw new MofException(string.Format("{0} гаргахад алдаа гарлаа: {1}", reportDesc, ex.Message), ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} гаргахад алдаа гарлаа: {1}", reportDesc, ex.Message));
                throw new MofException(string.Format("{0} гаргахад алдаа гарлаа: {1}", reportDesc, ex.Message), ex);
            }
            finally
            {
                reportTool = null; directRegisterReport = null; cardReport = null;
                incomeReport = null; incomeDirect = null; incomePerson = null;
                controlcardBack = null;
            }
        }

        internal static void SendMail(MailParameter mailParam)
        {
            if (mailParam == null) return;
            SendNEMailOnSaveDelegate sendMail = null;
            try
            {
                mailParameter = mailParam;
                if (string.IsNullOrEmpty(mailParameter.DomainId)) return;
                sendMail = new SendNEMailOnSaveDelegate(SendNotificationEMail);
                sendMail.BeginInvoke(new AsyncCallback(EndSendNotificationEMail), sendMail);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Mail илгээхэд алдаа гарлаа!", ex);
            }
            finally { sendMail = null; }
        }

        internal static void SendMail(MailParameter mailParam, string toStaffDomain)
        {
            if (mailParam == null) return;
            SendNEMailOnSaveDelegate sendMail = null;
            try
            {
                mailParameter = mailParam;
                sendMail = new SendNEMailOnSaveDelegate(SendNotificationEMail);
                sendMail.BeginInvoke(new AsyncCallback(EndSendNotificationEMail), sendMail);
                sendMail = null;
                Thread.Sleep(1000);

                if (string.IsNullOrEmpty(toStaffDomain) || mailParam.DomainId.Equals(toStaffDomain)) return;
                mailParameter.DomainId = toStaffDomain;
                sendMail = new SendNEMailOnSaveDelegate(SendNotificationEMail);
                sendMail.BeginInvoke(new AsyncCallback(EndSendNotificationEMail), sendMail);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Mail илгээхэд алдаа гарлаа!", ex);
            }
            finally { sendMail = null; }
        }

        internal static void SendMail(List<MailParameter> mailParams)
        {
            if (mailParams == null) return;
            if (mailParams.Count.Equals(0)) return;
            try
            {
                foreach (MailParameter item in mailParams)
                {
                    if (string.IsNullOrEmpty(item.DomainId)) continue;
                    mailParameter = item;
                    SendNotificationEMail();
                    mailParameter = null;
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Mail илгээхэд алдаа гарлаа!", ex);
            }
            finally { mailParameter = null; }
        }

        private static void SendNotificationEMail()
        {
            DirectoryEntry directoryEntry = null;
            DirectorySearcher directorySearcher = null;
            SearchResult searchResult = null;
            ResultPropertyValueCollection valueCollection = null;
            string recipientAddress = string.Empty;
            string senderAddress = string.Empty;
            string filePathStr = string.Empty;
            string greetings = string.Empty;
            decimal expiredDays = 0;

            SmtpClient smtpClient = null;
            MailMessage mailMessage = null;
            try
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээх процесс эхлэлээ!");

                if (string.IsNullOrEmpty(mailParameter.ExpiredDays))
                    greetings = @"<span lang=MN><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=MN>Таны нэр дээр дараах бичиг ирлээ. Үүнд:<o:p></o:p></span></p><p class=MsoNormal><span lang=MN><o:p>&nbsp;</o:p></span></p>";
                else
                {
                    expiredDays = decimal.Parse(mailParameter.ExpiredDays);
                    if (expiredDays > 0)
                    {
                        greetings = string.Format(@"<span lang=MN><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=MN>Таны нэр дээр дараах бичиг байна. Бичгийн буцах хугацаа <b>{0}</b> өдрийн дараа дуусах болно.
                            <o:p></o:p></span></p><p class=MsoNormal><span lang=MN><o:p>&nbsp;</o:p></span></p>", expiredDays);
                    }
                    else
                    {
                        greetings = string.Format(@"<span lang=MN><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=MN>Таны нэр дээр дараах бичиг байна. Бичгийн буцах хугацаа <b>{0}</b> өдөр хэтэрсэн байна.
                            <o:p></o:p></span></p><p class=MsoNormal><span lang=MN><o:p>&nbsp;</o:p></span></p>", expiredDays.ToString().Replace("-", ""));
                    }
                }

                filePathStr = string.IsNullOrEmpty(mailParameter.FilePath) ? @"<span style='color:#FFC000'>Файл хавсаргаагүй байна</span>"
                    : string.Format("<a href={0}><span style='color:#FFC000'>Эх бичигтэй танилцах</span></a>", mailParameter.FilePath);

                directoryEntry = new DirectoryEntry(Properties.Resources.MailDomainLink);
                directorySearcher = new DirectorySearcher(directoryEntry);
                directorySearcher.Filter = "(&(objectClass=top)(anr=" + mailParameter.DomainId + "))";
                directorySearcher.PropertiesToLoad.Add("mail");
                searchResult = directorySearcher.FindOne();
                foreach (string propName in searchResult.Properties.PropertyNames)
                {
                    valueCollection = searchResult.Properties[propName];
                    foreach (Object propertyValue in valueCollection)
                    {
                        if (propName == "mail")
                            recipientAddress = propertyValue.ToString();
                    }
                }

                directorySearcher = new DirectorySearcher(directoryEntry);
                directorySearcher.Filter = "(&(objectClass=top)(anr=" + userDomainId + "))";
                searchResult = directorySearcher.FindOne();
                foreach (string propName in searchResult.Properties.PropertyNames)
                {
                    valueCollection = searchResult.Properties[propName];
                    foreach (Object propertyValue in valueCollection)
                    {
                        if (propName == "mail")
                        {
                            senderAddress = propertyValue.ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(recipientAddress) || string.IsNullOrEmpty(senderAddress)) return;
                smtpClient = new SmtpClient();
                smtpClient.Host = Properties.Resources.MailHostIpAddress;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("mofdoc", "document15*");

                mailMessage = new MailMessage(new MailAddress("mofdoc@mof.gov.mn"), new MailAddress(recipientAddress));
                mailMessage.Body = @"<html xmlns:v=urn:schemas-microsoft-com:vml xmlns:o=urn:schemas-microsoft-com:office:office xmlns:w=urn:schemas-microsoft-com:office:word 
                                        xmlns:m=http://schemas.microsoft.com/office/2004/12/omml xmlns=http://www.w3.org/TR/REC-html40><head><meta http-equiv=Content-Type content=text/html; charset=utf-8><meta name=Generator content=Microsoft Word 14 
                                        (filtered medium)><!--[if !mso]><style>v\:* {behavior:url(#default#VML);}
                                        o\:* {behavior:url(#default#VML);}
                                        w\:* {behavior:url(#default#VML);}
                                        .shape {behavior:url(#default#VML);}
                                        </style><![endif]--><style><!--
                                        /* Font Definitions */
                                        @font-face
	                                        {font-family:Calibri;
	                                        panose-1:2 15 5 2 2 2 4 3 2 4;}
                                        @font-face
	                                        {font-family:Tahoma;
	                                        panose-1:2 11 6 4 3 5 4 4 2 4;}
                                        /* Style Definitions */
                                        p.MsoNormal, li.MsoNormal, div.MsoNormal
	                                        {margin:0in;
	                                        margin-bottom:.0001pt;
	                                        font-size:11.0pt;
	                                        font-family:Calibri,sans-serif;}
                                        a:link, span.MsoHyperlink
	                                        {mso-style-priority:99;
	                                        color:blue;
	                                        text-decoration:underline;}
                                        a:visited, span.MsoHyperlinkFollowed
	                                        {mso-style-priority:99;
	                                        color:purple;
	                                        text-decoration:underline;}
                                        p.MsoAcetate, li.MsoAcetate, div.MsoAcetate
	                                        {mso-style-priority:99;
	                                        mso-style-link:Balloon Text Char;
	                                        margin:0in;
	                                        margin-bottom:.0001pt;
	                                        font-size:8.0pt;
	                                        font-family:Tahoma,sans-serif;}
                                        span.BalloonTextChar
	                                        {mso-style-name:Balloon Text Char;
	                                        mso-style-priority:99;
	                                        mso-style-link:Balloon Text;
	                                        font-family:Tahoma,sans-serif;}
                                        span.EmailStyle19
	                                        {mso-style-type:personal;
	                                        font-family:Calibri,sans-serif;
	                                        color:windowtext;}
                                        span.EmailStyle20
	                                        {mso-style-type:personal-reply;
	                                        font-family:Calibri,sans-serif;
	                                        color:#1F497D;}
                                        .MsoChpDefault
	                                        {mso-style-type:export-only;
	                                        font-size:10.0pt;}
                                        @page WordSection1
	                                        {size:8.5in 11.0in;
	                                        margin:1.0in 1.25in 1.0in 1.25in;}
                                        div.WordSection1
	                                        {page:WordSection1;}
                                        --></style><!--[if gte mso 9]><xml>
                                        <o:shapedefaults v:ext=edit spidmax=1026 />
                                        </xml><![endif]--><!--[if gte mso 9]><xml>
                                        <o:shapelayout v:ext=edit>
                                        <o:idmap v:ext=edit data=1 />
                                        </o:shapelayout></xml><![endif]--></head><body lang=EN-US link=blue vlink=purple><div class=WordSection1><p class=MsoNormal><span lang=MN>Сайн байна уу?<o:p></o:p></span></p><p class=MsoNormal>
                                        <span lang=MN><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=MN>Танд энэ өдрийн мэнд хүргье.<o:p></o:p></span></p><p class=MsoNormal>" + greetings +
                                        @"<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 style='border-collapse:collapse'><tr style='height:47.85pt'>
                                        <td width=92 style='width:68.9pt;border:solid white 1.0pt;border-bottom:solid white 3.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:47.85pt'>
                                        <p class=MsoNormal align=center style='text-align:center'><b><span lang=MN style='color:white'>Файлын зам<o:p></o:p></span></b></p></td>
                                        <td width=92 style='width:68.9pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:47.85pt'>
                                        <p class=MsoNormal align=center style='text-align:center'><b><span lang=MN style='color:white'>Бичгийн огноо<o:p></o:p></span></b></p></td>
                                        <td width=92 style='width:68.9pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:47.85pt'>
                                        <p class=MsoNormal align=center style='text-align:center'><b><span lang=MN style='color:white'>Хаанаас<o:p></o:p></span></b></p>
                                        </td>
                                        <td width=92 style='width:68.9pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:47.85pt'>
                                        <p class=MsoNormal align=center style='text-align:center'><b><span lang=MN style='color:white'>Хэнээс<o:p></o:p></span></b></p></td>
                                        <td width=92 style='width:68.9pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:47.85pt'>
                                        <p class=MsoNormal align=center style='text-align:center'><b><span lang=MN style='color:white'>Бичиг №<o:p></o:p></span></b></p></td>
                                        <td width=92 style='width:68.9pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:47.85pt'>
                                        <p class=MsoNormal align=center style='text-align:center'><b><span lang=MN style='color:white'>Бүртгэл №<o:p></o:p></span></b></p></td>
                                        <td width=92 style='width:68.9pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:47.85pt'>
                                        <p class=MsoNormal align=center style='text-align:center'><b><span lang=MN style='color:white'>Хяналт №<o:p></o:p></span></b></p></td>
                                        <td width=130 style='width:97.8pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:47.85pt'>
                                        <p class=MsoNormal align=center style='text-align:center'><b><span lang=MN style='color:white'>Хариу өгөх огноо<o:p></o:p></span></b></p></td></tr><tr style='height:15.95pt'>
                                        <td width=92 valign=top style='width:68.9pt;border-top:none;border-left:solid white 1.0pt;border-bottom:solid white 1.0pt;border-right:solid white 3.0pt;background:#4F81BD;padding:0in 5.4pt 0in 5.4pt;height:15.95pt'>
                                        <p class=MsoNormal><b><span style='color:#FFC000'>" + filePathStr +
                                        @"</span><span style='color:white'><o:p></o:p></span></b></p></td>
                                        <td width=92 valign=top style='width:68.9pt;border-top:none;border-left:none;border-bottom:solid white 1.0pt;border-right:solid white 1.0pt;background:#A7BFDE;padding:0in 5.4pt 0in 5.4pt;height:15.95pt'>
                                        <p class=MsoNormal><span style='color:#1F497D'>" + mailParameter.DocDate +
                                        @"</span><o:p></o:p></p></td>
                                        <td width=92 valign=top style='width:68.9pt;border-top:none;border-left:none;border-bottom:solid white 1.0pt;border-right:solid white 1.0pt;background:#A7BFDE;padding:0in 5.4pt 0in 5.4pt;height:15.95pt'>
                                        <p class=MsoNormal><span lang=MN style='color:#1F497D'>" + mailParameter.FromWhere +
                                        @"</span><span lang=MN><o:p></o:p></span></p></td>
                                        <td width=92 valign=top style='width:68.9pt;border-top:none;border-left:none;border-bottom:solid white 1.0pt;border-right:solid white 1.0pt;background:#A7BFDE;padding:0in 5.4pt 0in 5.4pt;height:15.95pt'>
                                        <p class=MsoNormal><span lang=MN style='color:#1F497D'>" + mailParameter.FromWho +
                                        @"</span><span lang=MN><o:p></o:p></span></p></td>
                                        <td width=92 valign=top style='width:68.9pt;border-top:none;border-left:none;border-bottom:solid white 1.0pt;border-right:solid white 1.0pt;background:#A7BFDE;padding:0in 5.4pt 0in 5.4pt;height:15.95pt'>
                                        <p class=MsoNormal><span lang=MN style='color:#1F497D'>" + mailParameter.DocNum +
                                        @"</span><span lang=MN><o:p></o:p></span></p></td>
                                        <td width=92 valign=top style='width:68.9pt;border-top:none;border-left:none;border-bottom:solid white 1.0pt;border-right:solid white 1.0pt;background:#A7BFDE;padding:0in 5.4pt 0in 5.4pt;height:15.95pt'>
                                        <p class=MsoNormal><span lang=MN style='color:#1F497D'>" + mailParameter.RegNum +
                                        @"</span><span lang=MN><o:p></o:p></span></p></td>
                                        <td width=92 valign=top style='width:68.9pt;border-top:none;border-left:none;border-bottom:solid white 1.0pt;border-right:solid white 1.0pt;background:#A7BFDE;padding:0in 5.4pt 0in 5.4pt;height:15.95pt'>
                                        <p class=MsoNormal><span lang=MN style='color:#1F497D'>" + mailParameter.ControlNum +
                                        @"</span><span lang=MN><o:p></o:p></span></p></td>
                                        <td width=130 valign=top style='width:97.8pt;border-top:none;border-left:none;border-bottom:solid white 1.0pt;border-right:solid white 1.0pt;background:#A7BFDE;padding:0in 5.4pt 0in 5.4pt;height:15.95pt'>
                                        <p class=MsoNormal><span lang=MN style='color:#1F497D'>" + mailParameter.ReturnDate +
                                        @"</span><span lang=MN><o:p></o:p></span></p></td></tr></table>
                                        <p class=MsoNormal><span lang=MN><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=MN>Хариуцсан ажилтан: " + mailParameter.toWhoSecond +
                                        @"<o:p></o:p></p>
                                        <p class=MsoNormal><span lang=MN><o:p>&nbsp;</o:p></span></p><p class=MsoNormal><span lang=MN>Хүндэтгэсэн, " + userFName +
                                        @"<o:p></o:p></p>
                                        </div></body></html>";

                mailMessage.Subject = mailParameter.ShortDesc.Length < 40 ? mailParameter.ShortDesc : string.Format("{0}...", mailParameter.ShortDesc.Substring(0, 40));
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээх процесст алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээх процесст алдаа гарлаа: " + ex.Message);
                throw new MofException("Mail илгээхэд алдаа гарлаа!", ex);
            }
            finally
            {
                directoryEntry = null; directorySearcher = null; searchResult = null; valueCollection = null;
                recipientAddress = null; senderAddress = null; smtpClient = null; filePathStr = null; mailMessage = null;
            }
        }

        private static void EndSendNotificationEMail(IAsyncResult result)
        {
            SendNEMailOnSaveDelegate sendMail = null;
            try
            {
                sendMail = result.AsyncState as SendNEMailOnSaveDelegate;
                sendMail.EndInvoke(result);
                System.Diagnostics.Debug.WriteLine("Mail илгээх процесс амжилттай дууслаа!");
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээх процесс буцахдаа алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Mail илгээх процесс буцахдаа алдаа гарлаа: " + ex.Message);
                throw new MofException("Mail илгээх процесс буцахдаа алдаа гарлаа!", ex);
            }
            finally { sendMail = null; }
        }

        internal static void SearchData(object sender, DocumentType documentType, Dictionary<string, object> searchData)
        {
            Forms.Home homePage = null;
            MofDoc.Forms.Page.MainPage mainPage = null;
            MofDoc.Forms.Page.Income.IncomeList incomeList = null;
            MofDoc.Forms.Page.Outcome.OutcomeList outcomeList = null;
            MofDoc.Forms.Page.Domestic.DomesticList domesticList = null;
            try
            {
                homePage = sender as Forms.Home;
                if (!(homePage.tabCtl.SelectedTabPage.Controls[0] is Forms.Page.MainPage)) return;
                mainPage = ((MofDoc.Forms.Page.MainPage)(homePage.tabCtl.SelectedTabPage.Controls[0]));
                if (mainPage.mainTabCtl.TabPages == null || mainPage.mainTabCtl.SelectedTabPage.Controls[0] == null)
                    return;

                if (documentType.Equals(DocumentType.Income))
                {
                    if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Income.IncomeList)
                    {
                        incomeList = ((MofDoc.Forms.Page.Income.IncomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                        incomeList.SearchIncome(searchData);
                    }
                    else throw new MofException("Хуудас олдсонгүй!");
                }
                else if (documentType.Equals(DocumentType.Outcome))
                {
                    if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Outcome.OutcomeList)
                    {
                        outcomeList = ((MofDoc.Forms.Page.Outcome.OutcomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                        outcomeList.SearchOutcome(searchData);
                    }
                    else throw new MofException("Хуудас олдсонгүй!");
                }
                else
                {
                    if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Domestic.DomesticList)
                    {
                        domesticList = ((MofDoc.Forms.Page.Domestic.DomesticList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                        domesticList.SearchDomestic(searchData);
                    }
                    else throw new MofException("Хуудас олдсонгүй!");
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлт хийхэд алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлт хийхэд алдаа гарлаа: " + ex.Message);
                throw new MofException("Хайлт хийхэд алдаа гарлаа!", ex);
            }
            finally { homePage = null; mainPage = null; incomeList = null; outcomeList = null; }
        }

        internal static void BackFromSearch(object sender, DocumentType documentType)
        {
            Forms.Home homePage = null;
            MofDoc.Forms.Page.MainPage mainPage = null;
            MofDoc.Forms.Page.Income.IncomeList incomeList = null;
            MofDoc.Forms.Page.Outcome.OutcomeList outcomeList = null;
            MofDoc.Forms.Page.Domestic.DomesticList domesticList = null;
            try
            {
                homePage = sender as Forms.Home;
                if (homePage.tabCtl.SelectedTabPage == null) return;
                if (!(homePage.tabCtl.SelectedTabPage.Controls[0] is Forms.Page.MainPage)) return;
                mainPage = ((MofDoc.Forms.Page.MainPage)(homePage.tabCtl.SelectedTabPage.Controls[0]));
                if (mainPage.mainTabCtl.TabPages == null || mainPage.mainTabCtl.SelectedTabPage.Controls[0] == null)
                    return;

                if (documentType.Equals(DocumentType.Income))
                {
                    if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Income.IncomeList)
                    {
                        incomeList = ((MofDoc.Forms.Page.Income.IncomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                        incomeList.RefreshControl();
                    }
                    else throw new MofException("Хуудас олдсонгүй!");
                }
                else if (documentType.Equals(DocumentType.Outcome))
                {
                    if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Outcome.OutcomeList)
                    {
                        outcomeList = ((MofDoc.Forms.Page.Outcome.OutcomeList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                        outcomeList.RefreshControl();
                    }
                    else throw new MofException("Хуудас олдсонгүй!");
                }
                else
                {
                    if (mainPage.mainTabCtl.SelectedTabPage.Controls[0] is Forms.Page.Domestic.DomesticList)
                    {
                        domesticList = ((MofDoc.Forms.Page.Domestic.DomesticList)(mainPage.mainTabCtl.SelectedTabPage.Controls[0]));
                        domesticList.RefreshControl();
                    }
                    else throw new MofException("Хуудас олдсонгүй!");
                }
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтаас гарахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хайлтаас гарахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Хайлтаас гарахад алдаа гарлаа!", ex);
            }
            finally { homePage = null; mainPage = null; incomeList = null; outcomeList = null; }
        }

        internal static List<DateType> GetDateType()
        {
            if (dateTypes == null)
                dateTypes = new List<DateType>();
            if (dateTypes.Count.Equals(0))
            {
                dateTypes.AddRange(new DateType[]{
                    new DateType(1, "Яаралтай", 7),
                    new DateType(2, "Хариутай", 10),
                    new DateType(3, "Гомдол", 14),
                    new DateType(4, "Зөвлөмж", 7),
                    new DateType(5, "Хугацаатай", 0),
                    new DateType(6, "Нягтлан томилох", 14)
                });
            }
            return dateTypes;
        }

        internal static int GetDateTypeDay(object Id)
        {
            try { return dateTypes.Single(t => t.Id.Equals(decimal.Parse(Id.ToString()))).Day; }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Хугацааны төрлөөс олдсонгүй!: " + ex.Message);
                throw new MofException("Хугацааны төрлийг сонгоход алдаа гарлаа!", ex);
            }
        }

        internal static List<Document> GetOrderRelationList(List<Document> listDocument)
        {
            List<Document> orderedListDoc = null;
            List<Document> parentListDoc = null;
            try
            {
                orderedListDoc = new List<Document>();
                parentListDoc = listDocument.Where(t => t.ParentPkId == null).ToList();
                foreach (Document parent in parentListDoc)
                {
                    orderedListDoc.Add(parent);
                    RecursiveOrderRelationList(listDocument, orderedListDoc, parent.PkId);
                }
                return orderedListDoc;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа!", ex);
            }
            finally { orderedListDoc = null; parentListDoc = null; }
        }

        private static void RecursiveOrderRelationList(List<Document> allDoc, List<Document> orderedDoc, decimal parentPkId)
        {
            Document currentDoc = null;
            try
            {
                if (allDoc.Any(t => t.ParentPkId.Equals(parentPkId)))
                {
                    currentDoc = allDoc.Single(t => t.ParentPkId.Equals(parentPkId));
                    if (currentDoc == null) return;
                    orderedDoc.Add(currentDoc);
                    RecursiveOrderRelationList(allDoc, orderedDoc, currentDoc.PkId);
                }
                else return;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа!", ex);
            }
            finally { currentDoc = null; }
        }

        internal static List<Document> GetLastDocument(List<Document> listDocument)
        {
            List<Document> lastListDoc = null;
            try
            {
                lastListDoc = new List<Document>();
                foreach (Document parent in listDocument)
                    RecursiveGetLastDocument(listDocument, lastListDoc, parent.PkId);
                return lastListDoc;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээлэлд сүүлийн хүн дээр очиход алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээлэлд сүүлийн хүн дээр очиход алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээлэлд сүүлийн хүн дээр очиход алдаа гарлаа!", ex);
            }
            finally { lastListDoc = null; }
        }

        private static void RecursiveGetLastDocument(List<Document> allDoc, List<Document> lastDoc, decimal parentPkId)
        {
            Document currentDoc = null;
            try
            {
                if (allDoc.Any(t => t.ParentPkId.Equals(parentPkId)))
                {
                    currentDoc = allDoc.Single(t => t.ParentPkId.Equals(parentPkId));
                    RecursiveOrderRelationList(allDoc, lastDoc, currentDoc.PkId);
                }
                else
                    currentDoc = allDoc.Single(t => t.PkId.Equals(parentPkId));
                lastDoc.Add(currentDoc);
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээлэлд дэс дараалалжуулахад алдаа гарлаа!", ex);
            }
            finally { currentDoc = null; }
        }

        internal static List<DocumentReport> GetDocumentReport(List<Document> docReport, object staffId)
        {
            List<DocumentReport> reportList = null;
            DocumentReport report = null;
            List<Document> filteredDoc = null;
            decimal employeeId;
            try
            {
                employeeId = decimal.Parse(staffId.ToString());
                reportList = new List<DocumentReport>();
                filteredDoc = docReport.Where(t => t.ToStaffId.Equals(employeeId)).ToList();

                report = new DocumentReport();
                report.Name = filteredDoc.First(t => t.ToStaffId.Equals(employeeId)).ToName;
                report.IsBranch = null;
                report.Card = filteredDoc.Count(t => t.ControlNum != null && t.ToStaffId.Equals(employeeId));
                report.Direct = filteredDoc.Count(t => t.ControlNum == null && t.ToStaffId.Equals(employeeId));
                report.Income = filteredDoc.Count(t => t.ToStaffId.Equals(employeeId));
                report.CardDecision = filteredDoc.Count(t => t.ControlNum != null && !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(employeeId));
                report.ExpiredDecision = filteredDoc.Count(t => t.ControlNum != null && !t.Status && t.ReturnDate < t.ClosedDate && t.ToStaffId.Equals(employeeId));
                report.DirectDecision = filteredDoc.Count(t => t.ControlNum == null && !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(employeeId));
                report.Decision = filteredDoc.Count(t => !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(employeeId));
                report.ProgressTime = filteredDoc.Count(t => t.ControlNum != null && t.ToStaffId.Equals(employeeId) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                report.ProgressNonTime = filteredDoc.Count(t => t.ControlNum != null && t.ToStaffId.Equals(employeeId) && t.Status && t.ClosedDate == null && t.ReturnDate < DateTime.Now);
                reportList.Add(report);

                return reportList;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг тоолоход алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг тоолоход алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээллийг тоолоход алдаа гарлаа!", ex);
            }
            finally { reportList = null; report = null; filteredDoc = null; }
        }

        internal static List<DocumentReport> GetDocumentReport(List<Document> docReport, decimal brId)
        {
            List<DocumentReport> reportList = null;
            DocumentReport report = null;
            List<Document> filteredDoc = null;
            List<decimal> staffList = null;
            try
            {
                reportList = new List<DocumentReport>();
                foreach (DataRow row in MainPage.branchInfo.Rows)
                {
                    if (!brId.Equals(decimal.Parse(row["BR_ID"].ToString()))) continue;
                    report = new DocumentReport();
                    report.IsBranch = null;
                    report.Name = row["NAME"].ToString();

                    report.Card = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId));
                    report.Direct = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId));
                    report.Income = docReport.Count(t => t.ToBrId.Equals(brId));

                    report.CardDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                    report.ExpiredDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ReturnDate < t.ClosedDate);
                    report.DirectDecision = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                    report.Decision = docReport.Count(t => t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);

                    report.ProgressTime = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                    report.ProgressNonTime = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null && t.ReturnDate < DateTime.Now);
                    reportList.Add(report);

                    if (!docReport.Any(t => t.ToBrId.Equals(brId))) continue;
                    filteredDoc = docReport.Where(t => t.ToBrId.Equals(brId)).ToList();
                    staffList = (from p in filteredDoc select (decimal)p.ToStaffId).Distinct().ToList();

                    foreach (decimal entry in staffList)
                    {
                        report = new DocumentReport();
                        report.Name = filteredDoc.First(t => t.ToStaffId.Equals(entry)).ToName;
                        report.IsBranch = false;

                        report.Card = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry));
                        report.Direct = filteredDoc.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry));
                        report.Income = filteredDoc.Count(t => t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry));

                        report.CardDecision = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(entry));
                        report.ExpiredDecision = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ReturnDate < t.ClosedDate && t.ToStaffId.Equals(entry));
                        report.DirectDecision = filteredDoc.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(entry));
                        report.Decision = filteredDoc.Count(t => t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(entry));

                        report.ProgressTime = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                        report.ProgressNonTime = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry) && t.Status && t.ClosedDate == null && t.ReturnDate < DateTime.Now);
                        reportList.Add(report);
                    }
                }
                return reportList;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг тоолоход алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг тоолоход алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээллийг тоолоход алдаа гарлаа!", ex);
            }
            finally { reportList = null; report = null; filteredDoc = null; staffList = null; }
        }

        internal static List<DocumentReport> GetDocumentReport(List<Document> docReport, List<Document> cloneDocumentNew, bool isWide, string regStart, string regEnd)
        {
            List<DocumentReport> reportList = null;
            DocumentReport report = null;
            List<Document> filteredDoc = null;
            List<decimal> staffList = null;
            decimal brId;
            List<Branch> branchInfoGrouped = null;
            List<Branch> childBranchInfo = null;
            try
            {
                reportList = new List<DocumentReport>();
                branchInfoGrouped = new List<Branch>();

                var products = MainPage.branchInfo.AsEnumerable();
                var productGroups =
                    from p in products
                    orderby p.Field<string>("BR_ID")
                    group p by p.Field<string>("BR_MAIN_ID1")
                        into g 
                        select new { Category = g.Key, Products = g };
                foreach (var g in productGroups)
                    foreach (var w in g.Products)
                        branchInfoGrouped.Add(new Branch(decimal.Parse(w.Field<string>("BR_ID")), w.Field<string>("NAME"), decimal.Parse(w.Field<string>("BR_MAIN_ID1"))));

                if (isWide)
                {
                    foreach (Branch row in branchInfoGrouped)
                    {
                        brId = row.BrId;
                        report = new DocumentReport();
                        report.IsBranch = true;
                        report.Name = row.Name;

                        if (row.BrId.Equals(row.ParentId))
                        {
                            report.Card = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId));
                            report.Direct = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId));
                            report.Income = docReport.Count(t => t.ToBrId.Equals(brId));

                            report.CardDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                            report.ExpiredDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate);
                            report.DirectDecision = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                            report.Decision = docReport.Count(t => t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);

                            report.ProgressTime = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                            report.ProgressNonTime = cloneDocumentNew.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null 
                                && t.ReturnDate >= DateTime.Parse(regStart) && t.ReturnDate <= DateTime.Parse(regEnd));

                            if (branchInfoGrouped.Count(t => t.ParentId.Equals(row.BrId)) > 1)
                            {
                                childBranchInfo = branchInfoGrouped.Where(t => !t.BrId.Equals(row.BrId) && t.ParentId.Equals(row.BrId)).ToList();
                                foreach (Branch child in childBranchInfo)
                                {
                                    report.Card += docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId));
                                    report.Direct += docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(child.BrId));
                                    report.Income += docReport.Count(t => t.ToBrId.Equals(child.BrId));

                                    report.CardDecision += docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId) && !t.Status && t.ClosedDate != null);
                                    report.ExpiredDecision += docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId) && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate);
                                    report.DirectDecision += docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(child.BrId) && !t.Status && t.ClosedDate != null);
                                    report.Decision += docReport.Count(t => t.ToBrId.Equals(child.BrId) && !t.Status && t.ClosedDate != null);

                                    report.ProgressTime += docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                                    report.ProgressNonTime += cloneDocumentNew.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId) && t.Status && t.ClosedDate == null
                                        && t.ReturnDate >= DateTime.Parse(regStart) && t.ReturnDate <= DateTime.Parse(regEnd));
                                }
                            }
                            reportList.Add(report);
                        }
                        else
                        {

                            report.Card = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId));
                            report.Direct = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId));
                            report.Income = docReport.Count(t => t.ToBrId.Equals(brId));

                            report.CardDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                            report.ExpiredDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate);
                            report.DirectDecision = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                            report.Decision = docReport.Count(t => t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);

                            report.ProgressTime = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                            report.ProgressNonTime = cloneDocumentNew.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null
                                && t.ReturnDate >= DateTime.Parse(regStart) && t.ReturnDate <= DateTime.Parse(regEnd));
                            reportList.Add(report);
                        }

                        if (!docReport.Any(t => t.ToBrId.Equals(brId))) continue;
                        filteredDoc = docReport.Where(t => t.ToBrId.Equals(brId)).ToList();
                        staffList = (from p in filteredDoc select (decimal)p.ToStaffId).Distinct().ToList();

                        foreach (decimal entry in staffList)
                        {
                            report = new DocumentReport();
                            report.Name = filteredDoc.First(t => t.ToStaffId.Equals(entry)).ToName;
                            report.IsBranch = false;

                            report.Card = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry));
                            report.Direct = filteredDoc.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry));
                            report.Income = filteredDoc.Count(t => t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry));

                            report.CardDecision = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(entry));
                            report.ExpiredDecision = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate && t.ToStaffId.Equals(entry));
                            report.DirectDecision = filteredDoc.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(entry));
                            report.Decision = filteredDoc.Count(t => t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null && t.ToStaffId.Equals(entry));

                            report.ProgressTime = filteredDoc.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                            report.ProgressNonTime = cloneDocumentNew.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.ToStaffId.Equals(entry) && t.Status && t.ClosedDate == null
                                && t.ReturnDate >= DateTime.Parse(regStart) && t.ReturnDate <= DateTime.Parse(regEnd));
                            reportList.Add(report);
                        }
                    }
                }
                else
                {
                    foreach (Branch row in branchInfoGrouped)
                    {
                        brId = row.BrId;
                        report = new DocumentReport();
                        report.IsBranch = true;
                        report.Name = row.Name;

                        if (row.BrId.Equals(row.ParentId))
                        {
                            //report.Card = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId));
                            report.Direct = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId));
                            report.Income = docReport.Count(t => t.ToBrId.Equals(brId));

                            report.CardDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                            report.ExpiredDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate);
                            report.DirectDecision = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                            report.Decision = docReport.Count(t => t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);

                            report.ProgressTime = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                            report.ProgressNonTime = cloneDocumentNew.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null
                                && t.ReturnDate >= DateTime.Parse(regStart) && t.ReturnDate <= DateTime.Parse(regEnd));

                            report.Card = report.CardDecision + report.ProgressTime + report.ProgressNonTime;

                            if (branchInfoGrouped.Count(t => t.ParentId.Equals(row.BrId)) > 1)
                            {
                                childBranchInfo = branchInfoGrouped.Where(t => !t.BrId.Equals(row.BrId) && t.ParentId.Equals(row.BrId)).ToList();
                                foreach (Branch child in childBranchInfo)
                                {
                                    //report.Card += docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId));
                                    report.Direct += docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(child.BrId));
                                    report.Income += docReport.Count(t => t.ToBrId.Equals(child.BrId));

                                    report.CardDecision += docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId) && !t.Status && t.ClosedDate != null);
                                    report.ExpiredDecision += docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId) && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate);
                                    report.DirectDecision += docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(child.BrId) && !t.Status && t.ClosedDate != null);
                                    report.Decision += docReport.Count(t => t.ToBrId.Equals(child.BrId) && !t.Status && t.ClosedDate != null);

                                    report.ProgressTime += docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                                    report.ProgressNonTime += cloneDocumentNew.Count(t => t.ControlNum != null && t.ToBrId.Equals(child.BrId) && t.Status && t.ClosedDate == null
                                        && t.ReturnDate >= DateTime.Parse(regStart) && t.ReturnDate <= DateTime.Parse(regEnd));

                                    report.Card = report.CardDecision + report.ProgressTime + report.ProgressNonTime;
                                }
                            }
                            reportList.Add(report);
                        }
                        else
                        {
                            //report.Card = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId));
                            report.Direct = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId));
                            report.Income = docReport.Count(t => t.ToBrId.Equals(brId));

                            report.CardDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                            report.ExpiredDecision = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate);
                            report.DirectDecision = docReport.Count(t => t.ControlNum == null && t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);
                            report.Decision = docReport.Count(t => t.ToBrId.Equals(brId) && !t.Status && t.ClosedDate != null);

                            report.ProgressTime = docReport.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                            report.ProgressNonTime = cloneDocumentNew.Count(t => t.ControlNum != null && t.ToBrId.Equals(brId) && t.Status && t.ClosedDate == null
                                && t.ReturnDate >= DateTime.Parse(regStart) && t.ReturnDate <= DateTime.Parse(regEnd));
                            report.Card = report.CardDecision + report.ProgressTime + report.ProgressNonTime;
                            reportList.Add(report);
                        }
                    }
                }

                report = new DocumentReport();
                report.IsBranch = null;
                report.Name = "Нийт Сангийн яаманд ирсэн: ";

                report.Direct = docReport.Count(t => t.ControlNum == null);
                report.Income = docReport.Count;

                report.CardDecision = docReport.Count(t => t.ControlNum != null && !t.Status && t.ClosedDate != null);
                report.ExpiredDecision = docReport.Count(t => t.ControlNum != null && !t.Status && t.ReturnDate.Value.AddHours(22) < t.ClosedDate);
                report.DirectDecision = docReport.Count(t => t.ControlNum == null && !t.Status && t.ClosedDate != null);
                report.Decision = docReport.Count(t => !t.Status && t.ClosedDate != null);

                report.ProgressTime = docReport.Count(t => t.ControlNum != null && t.Status && t.ClosedDate == null && t.ReturnDate > DateTime.Now);
                report.ProgressNonTime = cloneDocumentNew.Count(t => t.ControlNum != null && t.Status && t.ClosedDate == null
                    && t.ReturnDate >= DateTime.Parse(regStart) && t.ReturnDate <= DateTime.Parse(regEnd));
                report.Card = report.CardDecision + report.ProgressTime + report.ProgressNonTime;
                reportList.Add(report);
                return reportList;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг тоолоход алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг тоолоход алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээллийг тоолоход алдаа гарлаа!", ex);
            }
            finally { reportList = null; report = null; filteredDoc = null; staffList = null; branchInfoGrouped = null; childBranchInfo = null;}
        }

        internal static List<Document> GetDocumentFromDb(string dbName, Dictionary<string, object> parameters, bool isReport)
        {
            List<Document> listDocument = null;
            Document document = null;
            DataSet ds = null;
            try
            {
                listDocument = new List<Document>();
                ds = isReport ? SqlConnector.GetStoredProcedure(dbName, "GetUserReport", parameters, null) : SqlConnector.GetStoredProcedure(dbName, "GetUserData", parameters, null);
                if (ds == null)
                    throw new MofException("Ирсэн мэдээлэл хоосон байна! Эрхгүй байна эсвэл алдаа гарлаа.");
                if (ds.Tables[0] == null)
                    throw new MofException("Ирсэн мэдээлэл хоосон байна! Эрхгүй байна эсвэл алдаа гарлаа.");
                if (ds.Tables[0].Rows.Count.Equals(0) || ds.Tables[0].Rows.Count == 0)
                    throw new MofException("Ирсэн мэдээлэл хоосон байна! Эрхгүй байна эсвэл алдаа гарлаа.");
                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    document = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(),
                        reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(),
                        reader[19].ToString(), reader[20].ToString(), reader[21].ToString(), reader[22].ToString(), reader[23].ToString(), reader[24].ToString(), reader[25].ToString(), reader[26].ToString(), reader[27].ToString(), reader[28].ToString());
                    listDocument.Add(document);
                }
                return listDocument;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээллийг авчрахад алдаа гарлаа!", ex);
            }
            finally { listDocument = null; ds = null; document = null; }
        }

        internal static List<Document> GetNonDocumentFromDb(string dbName, Dictionary<string, object> parameters)
        {
            List<Document> listDocument = null;
            Document document = null;
            DataSet ds = null;
            try
            {
                listDocument = new List<Document>();
                ds = SqlConnector.GetStoredProcedure(dbName, "GetUserNonReport", parameters, null);
                if (ds == null)
                    throw new MofException("Ирсэн мэдээлэл хоосон байна! Эрхгүй байна эсвэл алдаа гарлаа.");
                if (ds.Tables[0] == null)
                    throw new MofException("Ирсэн мэдээлэл хоосон байна! Эрхгүй байна эсвэл алдаа гарлаа.");
                if (ds.Tables[0].Rows.Count.Equals(0) || ds.Tables[0].Rows.Count == 0)
                    throw new MofException("Ирсэн мэдээлэл хоосон байна! Эрхгүй байна эсвэл алдаа гарлаа.");
                foreach (DataRow reader in ds.Tables[0].Rows)
                {
                    document = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(),
                        reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(),
                        reader[19].ToString(), reader[20].ToString(), reader[21].ToString(), reader[22].ToString(), reader[23].ToString(), reader[24].ToString(), reader[25].ToString(), reader[26].ToString(), reader[27].ToString(), reader[28].ToString());
                    listDocument.Add(document);
                }
                return listDocument;
            }
            catch (MofException ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлангийн мэдээллийг авчрахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлангийн мэдээллийг авчрахад алдаа гарлаа!", ex);
            }
            finally { listDocument = null; ds = null; document = null; }
        }

        internal static string GetStringWithoutRenew(string desc)
        {
            try
            {
                int index = desc.IndexOf("; Сунгалтын шалтгаан");
                if (index < 0) 
                    return desc;
                else 
                    return desc.Substring(0, index);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлбар утгыг таслахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлбар утгыг таслахад алдаа гарлаа!", ex);
            }
        }
        internal static string GetRenewalString(string desc)
        {
            try
            {
                int index = desc.IndexOf("; Сунгалтын шалтгаан");
                if (index < 0)
                    return "";
                else
                    return desc.Substring(index, desc.Length - index);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Тайлбар утгыг таслахад алдаа гарлаа: " + ex.Message);
                throw new MofException("Тайлбар утгыг таслахад алдаа гарлаа!", ex);
            }
        }
    }
}