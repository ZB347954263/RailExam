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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public partial class InformationChapter : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strInformationId = Request.QueryString.Get("id");
                ViewState["InformationID"] = strInformationId;
                if (!string.IsNullOrEmpty(strInformationId))
                {
                    hfBookID.Value = strInformationId;
                    BindTree();
                }

                string strPath = Server.MapPath("../Online/AssistBook/" + ViewState["InformationID"].ToString());
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                    Directory.CreateDirectory(strPath + "/Upload");
                    CopyTemplate(Server.MapPath("../Online/AssistBook/template/"),
                                 Server.MapPath("../Online/AssistBook/" + ViewState["InformationID"].ToString() + "/"));
                }
            }

            string strRefresh = Request.Form.Get("Refresh");
            if (strRefresh != null & strRefresh != "")
            {
                OracleAccess db = new OracleAccess();
                string strPath = "../Online/AssistBook/" + ViewState["InformationID"] + "/" + strRefresh + ".htm";

                string strSql = "update Information_Chapter set url='" + strPath + "' where Chapter_ID=" + strRefresh;
                db.ExecuteNonQuery(strSql);

                strSql = "select * from Information_Chapter where Chapter_ID=" + strRefresh;
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                string strChapterName = dr["Chapter_Name"].ToString();

               string  str =File.ReadAllText(Server.MapPath(strPath), Encoding.UTF8);

                if(str.IndexOf("chaptertitle") < 0)
                {
                    if (Convert.ToInt32(dr["Level_Num"].ToString()) < 3)
                    {
                        str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                           + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                           + "<div id='chaptertitle" + dr["Level_Num"] + "'>" + strChapterName + "</div>" +
                           "<br>"
                           + str + "</body>";
                    }
                    else
                    {
                        str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                              + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                              + "<div id='chaptertitle3'>" + strChapterName + "</div>" +
                              "<br>"
                              + str + "</body>";
                    }

                    File.WriteAllText(Server.MapPath(strPath), str, Encoding.UTF8);
                }


                strSql = "update Information set version=version+1 where Information_ID=" + ViewState["InformationID"];
                db.ExecuteNonQuery(strSql);

                InformationShow show = new InformationShow();
                show.GetIndex(ViewState["InformationID"].ToString());
            }
        }

        private void BindTree()
        {
            //添加书名
            OracleAccess db = new OracleAccess();

            string strSql = "select * from Information where Information_ID=" + ViewState["InformationID"];

            DataRow drInformation = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

            TreeViewNode tvn1 = new TreeViewNode();
            tvn1.ID = "0";
            tvn1.Value = ViewState["InformationID"].ToString();
            tvn1.Text = drInformation["Information_Name"].ToString();
            tvBookChapter.Nodes.Add(tvn1);

            //添加章节
            strSql = "select * from Information_Chapter where Information_ID=" + ViewState["InformationID"]+" Order by Level_Num,Order_Index";

            DataSet ds = db.RunSqlDataSet(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = dr["Chapter_ID"].ToString();
                    tvn.Value = dr["Information_ID"].ToString();
                    tvn.Text = dr["Chapter_Name"].ToString();

                    try
                    {
                        tvBookChapter.FindNodeById(dr["Parent_ID"].ToString()).Nodes.Add(tvn);
                    }
                    catch
                    {
                        tvBookChapter.Nodes.Clear();
                        SessionSet.PageMessage = "数据错误！";
                        return;
                    }
                }
            }

            tvBookChapter.DataBind();
            tvBookChapter.ExpandAll();
        }

        protected void tvBookChapterChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            OracleAccess db = new OracleAccess();
            
            if (e.Parameters[1] == "MoveUp")
            {
                string strSql = "select * from Information_Chapter where Chapter_ID=" + e.Parameters[0];
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                int cout = tvBookChapter.FindNodeById(dr["Parent_ID"].ToString()).Nodes.Count;
                int orderIndex = Convert.ToInt32(dr["Order_Index"].ToString());

                if (orderIndex <= cout && orderIndex >= 2)
                {
                    if (e.Parameters[2] == "Edit")
                    {
                        strSql = "update Information set  version=version+1 where Information_ID=" + dr["Information_ID"];
                        db.ExecuteNonQuery(strSql);
                    }

                    strSql = "update Information_Chapter set  Order_Index=Order_Index-1 where Chapter_ID=" + e.Parameters[0];
                    db.ExecuteNonQuery(strSql);

                    strSql = "update Information_Chapter set  Order_Index=Order_Index+1 where Chapter_ID=" + int.Parse(tvBookChapter.FindNodeById(e.Parameters[0]).PreviousSibling.ID);
                    db.ExecuteNonQuery(strSql);
                }
            }

            if (e.Parameters[1] == "MoveDown")
            {
                string strSql = "select * from Information_Chapter where Chapter_ID=" + e.Parameters[0];
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                int cout = tvBookChapter.FindNodeById(dr["Parent_ID"].ToString()).Nodes.Count;
                int orderIndex = Convert.ToInt32(dr["Order_Index"].ToString());

                if (orderIndex <= cout - 1 && orderIndex >= 1)
                {
                    if (e.Parameters[2] == "Edit")
                    {
                        strSql = "update Information set  version=version+1 where Information_ID=" + dr["Information_ID"];
                        db.ExecuteNonQuery(strSql);
                    }

                    strSql = "update Information_Chapter set  Order_Index=Order_Index+1 where Chapter_ID=" + e.Parameters[0];
                    db.ExecuteNonQuery(strSql);

                    strSql = "update Information_Chapter set  Order_Index=Order_Index-1 where Chapter_ID=" + int.Parse(tvBookChapter.FindNodeById(e.Parameters[0]).NextSibling.ID);
                    db.ExecuteNonQuery(strSql);
                }
            }
            if (e.Parameters[1] == "Insert")
            {
                string strSql = "select Max(Chapter_Id) from Information_Chapter where Information_ID=" + hfBookID.Value
                    + " and Parent_ID=" + e.Parameters[0];
                DataSet ds = db.RunSqlDataSet(strSql);

                hfMaxID.Value = ds.Tables[0].Rows[0][0].ToString();
                hfMaxID.RenderControl(e.Output);
            }

            if (e.Parameters[1] == "delete")
            {
                string strSql = "select * from Information_Chapter where Chapter_ID=" + e.Parameters[0];
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                strSql = "delete from Information_Chapter where Chapter_ID=" + e.Parameters[0];
                db.ExecuteNonQuery(strSql);

                InformationShow show = new InformationShow();
                show.GetIndex(dr["Information_ID"].ToString());

                hfMaxID.Value = e.Parameters[2];
                hfMaxID.RenderControl(e.Output);
            }

            if (e.Parameters[1] == "edit")
            {
                hfMaxID.Value = e.Parameters[0];
                hfMaxID.RenderControl(e.Output);
            }

            tvBookChapter.Nodes.Clear();
            BindTree();
            tvBookChapter.RenderControl(e.Output);
        }

        protected void tvBookChapterMoveCallBack_Callback(object sender, CallBackEventArgs e)
        {
            TreeViewNode node = tvBookChapter.FindNodeById(e.Parameters[0]);

            if (node != null && e.Parameters[1] == "CanMoveUp")
            {
                if (node.PreviousSibling != null)
                {
                    hfCanMove.Value = "true";
                    hfCanMove.RenderControl(e.Output);
                }
                else
                {
                    hfCanMove.Value = string.Empty;
                    hfCanMove.RenderControl(e.Output);
                }
            }
            else if (node != null && e.Parameters[1] == "CanMoveDown")
            {
                if (node.NextSibling != null)
                {
                    hfCanMove.Value = "true";
                    hfCanMove.RenderControl(e.Output);
                }
                else
                {
                    hfCanMove.Value = string.Empty;
                    hfCanMove.RenderControl(e.Output);
                }
            }
        }

        private static void CopyTemplate(string srcPath, string aimPath)
        {
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }

            string[] fileList = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    CopyTemplate(file, aimPath + Path.GetFileName(file) + "\\");
                }
                else
                {
                    File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
        }
    }
}
