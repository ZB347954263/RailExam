using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using ICSharpCode.SharpZipLib.Zip;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public partial class InformationInfo : PageBase
    {
        private ZipOutputStream zos = null;
        private string strBaseDir = ""; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }

                if (PrjPub.HasEditRight("资料管理") && PrjPub.IsServerCenter && (PrjPub.CurrentLoginUser.RoleID == 123 ||PrjPub.CurrentLoginUser.RoleID == 1))//&& PrjPub.CurrentLoginUser.SuitRange == 1
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("资料管理") && PrjPub.IsServerCenter && (PrjPub.CurrentLoginUser.RoleID == 123 || PrjPub.CurrentLoginUser.RoleID == 1))//&& PrjPub.CurrentLoginUser.SuitRange == 1
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

                HfOrgId.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();

                hfEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();

                BindGrid();
            }
            else
            {
                if (Request.Form.Get("Refresh") == "true")
                {
                    BindGrid();
                }
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != null && strDeleteID != "")
            {
                DelBook(strDeleteID);
                BindGrid();
            }

            string strUpID = Request.Form.Get("UpID");
            if (strUpID != null && strUpID != "")
            {
                OracleAccess db = new OracleAccess();
                if (Request.QueryString.Get("id") != null)
                {
                    string strSql = "select * from Information where System_Order_Index<"
                                    + "(select System_Order_Index from Information where  Information_ID=" + strUpID +")"
                                    + " and Information_System_ID=" + Request.QueryString.Get("id");
                    //在当前领域下存在比上移资料顺序小的资料
                    if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                    {
                        //将当前领域下位置在前的资料顺序+1
                       strSql =
                            "update Information set System_Order_Index=System_Order_Index+1 where System_Order_Index="
                            + "(select System_Order_Index from Information where  Information_ID=" + strUpID + ")-1 "
                            + " and Information_System_ID=" + Request.QueryString.Get("id");
                        db.ExecuteNonQuery(strSql);

                        //将上移资料顺序-1
                        strSql = "update Information set System_Order_Index=System_Order_Index-1 where Information_ID=" + strUpID;
                        db.ExecuteNonQuery(strSql);
                    }
                }

                if (Request.QueryString.Get("id1") != null)
                {
                    string strSql = "select * from Information where Level_Order_Index<"
                                    + "(select Level_Order_Index from Information where  Information_ID=" + strUpID + ")"
                                    + " and Information_Level_ID=" + Request.QueryString.Get("id1");
                     //在当前等级下存在比上移资料顺序小的资料
                     if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                     {
                         //将当前等级下位置在前的资料顺序+1
                         strSql =
                             "update Information set Level_Order_Index=Level_Order_Index+1 where Level_Order_Index="
                             + "(select Level_Order_Index from Information where  Information_ID=" + strUpID + ")-1 "
                             + " and Information_Level_ID=" + Request.QueryString.Get("id1");
                         db.ExecuteNonQuery(strSql);

                         //将上移资料顺序-1
                         strSql = "update Information set Level_Order_Index=Level_Order_Index-1 where Information_ID=" +
                                  strUpID;
                         db.ExecuteNonQuery(strSql);
                     }
                }
                BindGrid();
            }

            string strDownID = Request.Form.Get("DownID");
            if (strDownID != null && strDownID != "")
            {
                OracleAccess db = new OracleAccess();
                if (Request.QueryString.Get("id") != null)
                {
                    string strSql = "select * from Information where System_Order_Index>"
                                    + "(select System_Order_Index from Information where  Information_ID=" + strDownID + ")"
                                    + " and Information_System_ID=" + Request.QueryString.Get("id");
                    //在当前领域下存在比上移资料顺序大的资料
                    if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                    {
                        //将当前领域下位置在前的资料顺序-1
                        strSql =
                            "update Information set System_Order_Index=System_Order_Index-1 where System_Order_Index="
                            + "(select System_Order_Index from Information where  Information_ID=" + strDownID + ")+1 "
                            + " and Information_System_ID=" + Request.QueryString.Get("id");
                        db.ExecuteNonQuery(strSql);

                        //将上移资料顺序+1
                        strSql =
                            "update Information set System_Order_Index=System_Order_Index+1 where Information_ID=" + strDownID;
                        db.ExecuteNonQuery(strSql);
                    }
                }

                if (Request.QueryString.Get("id1") != null)
                {
                     string strSql = "select * from Information where Level_Order_Index>"
                                    + "(select Level_Order_Index from Information where  Information_ID=" + strDownID + ")"
                                    + " and Information_Level_ID=" + Request.QueryString.Get("id1");
                     //在当前等级下存在比上移资料顺序小的资料
                     if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                     {
                         //将当前等级下位置在前的资料顺序-1
                         strSql =
                             "update Information set Level_Order_Index=Level_Order_Index-1 where Level_Order_Index="
                             + "(select Level_Order_Index from Information where  Information_ID=" + strDownID + ")+1 "
                             + " and Information_Level_ID=" + Request.QueryString.Get("id1");
                         db.ExecuteNonQuery(strSql);

                         //将上移资料顺序+1
                         strSql = "update Information set Level_Order_Index=Level_Order_Index+1 where Information_ID=" + strDownID;
                         db.ExecuteNonQuery(strSql);
                     }
                }
                BindGrid();
            }

            string strRefreshDown = Request.Form.Get("RefreshDown");
            if (strRefreshDown != null && strRefreshDown != "")
            {
                if (!DownloadBook(strRefreshDown))
                {
                    SessionSet.PageMessage = "当前资料不存在电子版资料！";
                    BindGrid();
                    return;
                }
                BindGrid();
            }
        }

        private bool DownloadBook(string bookId)
        {
            BookBLL objBll = new BookBLL();
            RailExam.Model.Book obj = objBll.GetBook(Convert.ToInt32(bookId));
            string filename = Server.MapPath("/RailExamBao/Online/Book/" + bookId + "/");

            if (!Directory.Exists(filename))
            {
                return false;
            }

            string ZipName = Server.MapPath("/RailExamBao/Online/Book/Book.zip");

            GzipCompress(filename, ZipName);

            FileInfo file = new FileInfo(ZipName.ToString());
            this.Response.Clear();
            this.Response.Buffer = true;
            this.Response.Charset = "utf-7";
            this.Response.ContentEncoding = Encoding.UTF7;
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名

            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(obj.bookName) + ".zip");
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度
            this.Response.AddHeader("Content-Length", file.Length.ToString());
            // 指定返回的是一个不能被客户端读取的流，必须被下载
            this.Response.ContentType = "application/ms-word";
            // 把文件流发送到客户端
            this.Response.WriteFile(file.FullName);

            return true;
        }

        public void GzipCompress(string strPath, string strFileName)
        {
            zos = new ZipOutputStream(File.Create(strFileName)); // 指定zip文件的绝对路径，包括文件名            
            zos.SetLevel(6);

            strBaseDir = strPath;
            addZipEntry(strBaseDir);
            zos.Finish();
            zos.Close();
        }

        private void addZipEntry(string PathStr)
        {
            DirectoryInfo di = new DirectoryInfo(PathStr);
            foreach (DirectoryInfo item in di.GetDirectories())
            {
                addZipEntry(item.FullName);
            }
            foreach (FileInfo item in di.GetFiles())
            {
                FileStream fs = File.OpenRead(item.FullName);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                string strEntryName = item.FullName.Replace(strBaseDir, "")
                ;
                ZipEntry entry = new ZipEntry(strEntryName);
                zos.PutNextEntry(entry);
                zos.Write(buffer, 0, buffer.Length);
                fs.Close();
            }
        }


        private void DelBook(string strID)
        {
            OracleAccess db = new OracleAccess();

            string strSql = "delete from Information where Information_ID=" + strID;

            db.RunSqlDataSet(strSql);
        }

        private void BindGrid()
        {
            string strSql="";

            OracleAccess db = new OracleAccess();

            string strKnowledgeIDPath = Request.QueryString.Get("id");
            if (!string.IsNullOrEmpty(strKnowledgeIDPath))
            {
                strSql = "select Id_Path from Information_System where Information_System_ID=" + strKnowledgeIDPath;
                DataSet dsSystem = db.RunSqlDataSet(strSql);

                if (dsSystem.Tables[0].Rows.Count > 0)
                {
                    ViewState["SystemIdPath"] = dsSystem.Tables[0].Rows[0][0];
                }
                else
                {
                    ViewState["SystemIdPath"] = string.Empty;
                }

                strSql = "select a.*,c.Employee_Name as CreatePersonName from Information a "
                         + " inner join Information_System  b on a.Information_System_ID=b.Information_System_ID"
                         + " inner join Employee c on a.Create_Person=c.Employee_ID "
                         + " where b.Id_Path||'/' like '" + ViewState["SystemIdPath"] + "/%' order by a.System_Order_Index";
            }

            string strTrainTypeIDPath = Request.QueryString.Get("id1");
            if (!string.IsNullOrEmpty(strTrainTypeIDPath))
            {
                strSql = "select Id_Path from Information_Level where Information_Level_ID=" + strTrainTypeIDPath;
                DataSet dsLevel = db.RunSqlDataSet(strSql);

                if (dsLevel.Tables[0].Rows.Count > 0)
                {
                    ViewState["LevelIdPath"] = dsLevel.Tables[0].Rows[0][0];
                }
                else
                {
                    ViewState["LevelIdPath"] = string.Empty;
                }

                strSql = "select a.*,c.Employee_Name as CreatePersonName from Information a "
                         + " inner join Information_Level  b on a.Information_Level_ID=b.Information_Level_ID"
                         + " inner join Employee c on a.Create_Person=c.Employee_ID "
                         + " where b.Id_Path||'/' like '" + ViewState["LevelIdPath"] + "/%' order by a.Level_Order_Index";
            }

            if (!string.IsNullOrEmpty(txtBookName.Text.Trim()))
            {
                strSql += " and Information_Name like '%" + txtBookName.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtOrg.Text.Trim()))
            {
                strSql += " and KEYWORDS like '%" + txtOrg.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtBookName.Text.Trim()))
            {
                strSql += " and PUBLISH_ORG like '%" + txtBookName.Text.Trim() + "%'";
            }

            DataSet ds = db.RunSqlDataSet(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Information_Name"].ToString().Length <= 30)
                    {
                        dr["Information_Name"] = "<a onclick=OpenIndex('" + dr["Information_ID"] + "') href=# title=" + dr["Information_Name"] + " > " + dr["Information_Name"] + " </a>";
                    }
                    else
                    {
                        dr["Information_Name"] = "<a onclick=OpenIndex('" + dr["Information_ID"] + "') href=# title=" + dr["Information_Name"] + " > " + dr["Information_Name"].ToString().Substring(0, 15) + "...</a>";
                    }
                }

                Grid1.DataSource = ds;
                Grid1.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string strSql = "";

            OracleAccess db = new OracleAccess();

            string strKnowledgeIDPath = Request.QueryString.Get("id");
            if (!string.IsNullOrEmpty(strKnowledgeIDPath))
            {
                strSql = "select a.*,c.Employee_Name as CreatePersonName from Information a "
                         + " inner join Information_System  b on a.Information_System_ID=b.Information_System_ID"
                         + " inner join Employee c on a.Create_Person=c.Employee_ID "
                         + " where b.Id_Path||'/' like '" + ViewState["SystemIdPath"] + "/%'";
            }

            string strTrainTypeIDPath = Request.QueryString.Get("id1");
            if (!string.IsNullOrEmpty(strTrainTypeIDPath))
            {
                strSql = "select a.*,c.Employee_Name as CreatePersonName from Information a "
                         + " inner join Information_Level  b on a.Information_Level_ID=b.Information_Level_ID"
                         + " inner join Employee c on a.Create_Person=c.Employee_ID "
                         + " where b.Id_Path||'/' like '" + ViewState["LevelIdPath"] + "/%'";
            }

            if(!string.IsNullOrEmpty(txtBookName.Text.Trim()))
            {
                strSql += " and Information_Name like '%" + txtBookName.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtOrg.Text.Trim()))
            {
                strSql += " and KEYWORDS like '%" + txtOrg.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtBookName.Text.Trim()))
            {
                strSql += " and PUBLISH_ORG like '%" + txtBookName.Text.Trim() + "%'";
            }

            DataSet ds = db.RunSqlDataSet(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Information_Name"].ToString().Length <= 30)
                    {
                        dr["Information_Name"] = "<a onclick=OpenIndex('" + dr["Information_ID"] + "') href=# title=" + dr["Information_Name"] + " > " + dr["Information_Name"] + " </a>";
                    }
                    else
                    {
                        dr["Information_Name"] = "<a onclick=OpenIndex('" + dr["Information_ID"] + "') href=# title=" + dr["Information_Name"] + " > " + dr["Information_Name"].ToString().Substring(0, 15) + "...</a>";
                    }
                }

                Grid1.DataSource = ds;
                Grid1.DataBind();
            }

        }

        protected void Grid1_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("id") == "0" || Request.QueryString.Get("id1") == "0" || PrjPub.CurrentLoginUser.SuitRange == 0)
            {
                Grid1.Levels[0].Columns[1].Visible = false;
            }

            OracleAccess db = new OracleAccess();
            string id = Request.QueryString.Get("id");
            if (!string.IsNullOrEmpty(id))
            {
                string strSql = "select * from Information_System where Parent_ID=" + id;

                if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                {
                    Grid1.Levels[0].Columns[1].Visible = false;
                    Grid1.Levels[0].Columns[2].Visible = false;
                }
                else
                {
                    Grid1.Levels[0].Columns[1].Visible = true;
                    Grid1.Levels[0].Columns[2].Visible = false;
                }
            }

            id = Request.QueryString.Get("id1");
            if (!string.IsNullOrEmpty(id))
            {
                string strSql = "select * from Information_Level where Parent_ID=" + id;

                if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                {
                    Grid1.Levels[0].Columns[1].Visible = false;
                    Grid1.Levels[0].Columns[2].Visible = false;
                }
                else
                {
                    Grid1.Levels[0].Columns[1].Visible = false;
                    Grid1.Levels[0].Columns[2].Visible = true;
                }
            }
        }
    }
}
