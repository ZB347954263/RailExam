using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.DAL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class RefreshDownLoad : PageBase
    {
        private DateTime _beginTime;
        private Uri _directoryToDownload;
        private FtpClient _ftpSession;
        private bool _flag = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                string strType = Request.QueryString.Get("type");

                if (strType == "downloaddata")
                {
                    Title = "下载基础数据";
                    DownLoadData();
                }
                else if (strType == "createData")
                {
                    Title = "初始化数据库";
                    CreateData();
                }
                else if (strType == "downloadItem")
                {
                    Title = "下载试题图片";
                    DownLoadItem();
                }
            }
        }

        private void CreateData()
        {
            DateTime beginTime = DateTime.Now;
            SynchronizeLog obj = new SynchronizeLog();
            SynchronizeLogBLL objlogdal = new SynchronizeLogBLL();
            RefreshSnapShotBLL refreshbll = new RefreshSnapShotBLL();

            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);
            string jsBlock;

            try
            {
                int orgID = PrjPub.CurrentLoginUser.StationOrgID;
                refreshbll.DropSnapShot();
                jsBlock = "<script>SetPorgressBar('正在初始化数据库','" + ((double)(100) / (double)7).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                for (int i = 2; i <= 7; i++)
                {
                    refreshbll.CreateSnapShot(orgID, i);
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在初始化数据库','" + ((double)(i * 100) / (double)7).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                obj.OrgID = PrjPub.CurrentLoginUser.StationOrgID;
                obj.SynchronizeTypeID = PrjPub.ResetDataBase;
                obj.SynchronizeStatusID = PrjPub.DownloadSuccess;
                obj.BeginTime = beginTime;
                obj.EndTime = DateTime.Now;
                objlogdal.AddSynchronizeLog(obj);
                Response.Write("<script>alert('初始化数据库成功！');top.close();</script>");

            }
            catch (Exception ex)
            {
                obj.OrgID = PrjPub.CurrentLoginUser.StationOrgID;
                obj.SynchronizeTypeID = PrjPub.ResetDataBase;
                obj.SynchronizeStatusID = PrjPub.DownloadFailed;
                obj.BeginTime = beginTime;
                obj.EndTime = DateTime.Now;
                objlogdal.AddSynchronizeLog(obj);
                Response.Write("<script>alert('初始化数据库失败！请检查站段服务器网络连接是否正常！');top.close();</script>");

            }
        }

        private void DownLoadData()
        {
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);
            string jsBlock;

            DateTime beginTime = DateTime.Now;
            SynchronizeLog obj = new SynchronizeLog();
            SynchronizeLogBLL objlogdal = new SynchronizeLogBLL();
            RefreshSnapShotBLL refreshbll = new RefreshSnapShotBLL();

            try
            {
                string strInfo = string.Empty;
                string proName = string.Empty;
                int num = 5;
                int selectType = Convert.ToInt32(Request.QueryString.Get("selectType"));
                if (selectType == 0)
                {
                    strInfo = "下载考试试卷";
                    proName = "USP_Refresh_SnapShot_Exam";
                    num = 6;
                }
                else if (selectType == 1)
                {
                    strInfo = "下载教材与试题";
                    proName = "USP_Refresh_SnapShot_Book";
                    num = 5;
                }
                else if (selectType == 2)
                {
                    strInfo = "下载职员基本信息和档案信息";
                    proName = "USP_Refresh_SnapShot_Employee";
                    num = 4;
                }
                else if (selectType == 3)
                {
                    strInfo = "下载基础数据";
                    proName = "USP_Refresh_SnapShot_All";
                    num = 9;
                }

                if (selectType == 0 || selectType == 3)
                {
                    for (int i = 1; i <= num - 1; i++)
                    {
                        refreshbll.RefreshSnapShot(proName, i);

                        System.Threading.Thread.Sleep(10);
                        jsBlock = "<script>SetPorgressBar('正在" + strInfo + "','" + ((double)(i * 100) / (double)num).ToString("0.00") + "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();
                    }

                    string strSql =
                  @"select a.User_Ids from Random_Exam_Arrange_Detail  a
                       where a.Random_Exam_ID in (select Random_Exam_ID from Random_Exam_Computer_Server a 
                       where Computer_Server_No='" + PrjPub.ServerNo + "' and (Has_Paper=0 or Is_Start<2))";
                    OracleAccess dbCenter = new OracleAccess(System.Configuration.ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
                    string strwhere = string.Empty;
                    DataSet ds = dbCenter.RunSqlDataSet(strSql);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string[] struserIds = dr["User_Ids"].ToString().Split(',');

                        for (int i = 0; i < struserIds.Length; i++)
                        {
                            if (struserIds[i] == string.Empty)
                            {
                                continue;
                            }

                            if ((" or " + strwhere + " or ").IndexOf(" or Employee_ID=" + struserIds[i] + " or ") <= 0)
                            {
                                strwhere += strwhere == string.Empty
                                                ? "Employee_ID=" + struserIds[i]
                                                : " or Employee_ID=" + struserIds[i];
                            }
                        }
                    }

                    OracleAccess db = new OracleAccess();
                    if (db.GetCount("EMPLOYEE_FINGERPRINT", "MATERIALIZED VIEW") > 0)
                    {
                        strSql = "drop materialized view  Employee_FingerPrint";
                        db.ExecuteNonQuery(strSql);
                    }
                    if (strwhere == string.Empty)
                    {
                        strSql =
                            "create materialized view   Employee_FingerPrint   refresh force on demand as   select   *   from   Employee_FingerPrint@link_sf where 1=2";
                        db.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        strSql =
                                    "create materialized view   Employee_FingerPrint   refresh force on demand as   select   *   from   Employee_FingerPrint@link_sf where  " +
                                    strwhere;
                        db.ExecuteNonQuery(strSql);
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在" + strInfo + "','" + ((double)(num * 100) / (double)num).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }
                else
                {
                    for (int i = 1; i <= num; i++)
                    {
                        refreshbll.RefreshSnapShot(proName, i);

                        System.Threading.Thread.Sleep(10);
                        jsBlock = "<script>SetPorgressBar('正在" + strInfo + "','" + ((double)(i * 100) / (double)num).ToString("0.00") + "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();
                    }
                }


                obj.OrgID = PrjPub.CurrentLoginUser.StationOrgID;
                obj.SynchronizeTypeID = PrjPub.DownloadData;
                obj.SynchronizeStatusID = PrjPub.DownloadSuccess;
                obj.BeginTime = beginTime;
                obj.EndTime = DateTime.Now;
                objlogdal.AddSynchronizeLog(obj);
                Response.Write("<script>alert('下载成功！');top.close();</script>");
            }
            catch
            {
                obj.OrgID = PrjPub.CurrentLoginUser.StationOrgID;
                obj.SynchronizeTypeID = PrjPub.DownloadData;
                obj.SynchronizeStatusID = PrjPub.DownloadFailed;
                obj.BeginTime = beginTime;
                obj.EndTime = DateTime.Now;
                objlogdal.AddSynchronizeLog(obj);
                Response.Write("<script>alert('同步数据失败！请检查站段服务器网络连接是否正常！');top.close();</script>");
            }
        }


        private void DownLoadItem()
        {
            _beginTime = DateTime.Now;
            string localDir = "D:\\wwwroot\\RailExamBao\\Online\\Item\\"; ;

            int count;
            try
            {
                string strUrl = "ftp://" + ConfigurationManager.AppSettings["SeverIP"] + "/Item";
                _directoryToDownload = new Uri(strUrl);
                _ftpSession = FtpClient.GetFtpClient(_directoryToDownload.Host);

                string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
                StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
                string html = reader.ReadToEnd();
                reader.Close();
                Response.Write(html);
                Response.Flush();
                System.Threading.Thread.Sleep(200);

                count = downloadFilesCount(_directoryToDownload.AbsolutePath, 0);
            }
            catch
            {
                Response.Write("<script>alert('请先连接FTP!！');top.close();</script>");
                return;
            }

            SynchronizeLog obj = new SynchronizeLog();
            SynchronizeLogBLL objlogdal = new SynchronizeLogBLL();

            try
            {
                _flag = true;
                int i = 1;
                downloadFiles(_directoryToDownload.AbsolutePath, localDir, count, i);
                if (_flag)
                {
                    obj.OrgID = PrjPub.CurrentLoginUser.StationOrgID;
                    obj.SynchronizeTypeID = PrjPub.DownloadItem;
                    obj.SynchronizeStatusID = PrjPub.DownloadSuccess;
                    obj.BeginTime = _beginTime;
                    obj.EndTime = DateTime.Now;
                    objlogdal.AddSynchronizeLog(obj);
                    Response.Write("<script>alert('下载成功！');top.close();</script>");
                }
            }
            catch (Exception ex)
            {
                obj.OrgID = PrjPub.CurrentLoginUser.StationOrgID;
                obj.SynchronizeTypeID = PrjPub.DownloadItem;
                obj.SynchronizeStatusID = PrjPub.DownloadFailed;
                obj.BeginTime = _beginTime;
                obj.EndTime = DateTime.Now;
                objlogdal.AddSynchronizeLog(obj);
                Response.Write("<script>alert('下载失败！');top.close();</script>");
            }
        }

        private void downloadFiles(string serverDirectory, string localDirectory, int count, int index)
        {
            if (!Directory.Exists(localDirectory))
            {
                Directory.CreateDirectory(localDirectory);
            }
            Uri uri = new Uri("ftp://" + _directoryToDownload.Host + "/" + serverDirectory + "/");
            foreach (FileStruct fs in _ftpSession.GetDirectoryList(uri.AbsolutePath))
            {
                if (index >= count)
                {
                    return;
                }
                downloadFiles(serverDirectory + "/" + fs.Name, localDirectory + "\\" + fs.Name, count, index);
            }

            foreach (FileStruct fs in _ftpSession.GetFileList(uri.AbsolutePath))
            {
                if (index >= count)
                {
                    return;
                }
                string localFile = localDirectory + "\\" + fs.Name;
                if (!File.Exists(localFile))
                {
                    File.Delete(localFile);
                }

                System.Threading.Thread.Sleep(10);
                string jsBlock = "<script>SetPorgressBar('正在下载：" + fs.Name + "','" + ((double)(index * 100) / (double)count).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                index++;

                _ftpSession.DownloadFile(new Uri("ftp://" + _directoryToDownload.Host + "/" + serverDirectory + "/" + fs.Name), localFile);
            }
        }

        private int downloadFilesCount(string serverDirectory, int n)
        {
            Uri uri = new Uri("ftp://" + _directoryToDownload.Host + "/" + serverDirectory + "/");
            foreach (FileStruct fs in _ftpSession.GetDirectoryList(uri.AbsolutePath))
            {
                n = downloadFilesCount(serverDirectory + "/" + fs.Name, n);
            }
            foreach (FileStruct fs in _ftpSession.GetFileList(uri.AbsolutePath))
            {
                n = n + 1;
            }

            return n;
        }
    }
}
