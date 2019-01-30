using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
namespace RailExamWebApp.AssistBook
{
    public partial class AssistBookChapterDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfChapterID.Value = Request.QueryString["id"];

            if (fvBookChapter.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvBookChapter.FindControl("hfParentID")).Value = Request.QueryString["ParentID"];
                    ((HiddenField)fvBookChapter.FindControl("hfBookID")).Value = Request.QueryString["BookID"];
                }
                else
                {
                    ((HiddenField)fvBookChapter.FindControl("hfParentId")).Value = hfInsert.Value;
                    ((HiddenField)fvBookChapter.FindControl("hfBookID")).Value = Request.QueryString["BookID"];
                }
            }

            string strRefresh = Request.Form.Get("Refresh");
            if (strRefresh != null & strRefresh != "")
            {
                string strPath = "../Online/AssistBook/" + Request.QueryString["BookID"].ToString() + "/" + Request.QueryString["id"].ToString() + ".htm";

                AssistBookChapterBLL objBill = new AssistBookChapterBLL();
                objBill.UpdateAssistBookChapterUrl(Convert.ToInt32(Request.QueryString["id"].ToString()), strPath);
                RailExam.Model.AssistBookChapter objBookChapter = objBill.GetAssistBookChapter(Convert.ToInt32(Request.QueryString["id"].ToString()));
                string strChapterName = objBookChapter.ChapterName;

                string str = File.ReadAllText(Server.MapPath(strPath), Encoding.UTF8);

                if (str.IndexOf("chaptertitle") < 0)
                {
                    if (objBookChapter.LevelNum < 3)
                    {
                        str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                           + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                           + "<div id='chaptertitle" + objBookChapter.LevelNum + "'>" + strChapterName + "</div>" +
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


                AssistBookBLL objBookBill = new AssistBookBLL();
                string strBookName = objBookBill.GetAssistBook(Convert.ToInt32(Request.QueryString["BookID"].ToString())).BookName;

                SystemLogBLL objLogBll = new SystemLogBLL();
                objLogBll.WriteLog("编辑教材《" + strBookName + "》中“" + strChapterName + "”的章节内容");

                fvBookChapter.DataBind();
                Grid1.DataBind();

                objBill.GetIndex(Request.QueryString["BookID"].ToString());
            }

            string strRefreshGrid = Request.Form.Get("RefreshGrid");
            if (strRefreshGrid != null & strRefreshGrid != "")
            {
                fvBookChapter.DataBind();
                Grid1.DataBind();
            }

            string strDeleteBookChapterUpdateID = Request.Form.Get("DeleteBookChapterUpdateID");
            if (!string.IsNullOrEmpty(strDeleteBookChapterUpdateID))
            {
                DeleteData(int.Parse(strDeleteBookChapterUpdateID));
                Grid1.DataBind();
            }
        }

        private void DeleteData(int nBookChapterUpdateID)
        {
            AssistBookUpdateBLL BookChapterUpdateBLL = new AssistBookUpdateBLL();

            BookChapterUpdateBLL.DeleteAssistBookUpdate(nBookChapterUpdateID);
        }

        protected void fvBookChapter_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvBookChapterChangeCallBack.callback(-1, 'Rebuild');                        
            if(window.parent.tvBookChapter.get_nodes().get_length() > 0)
            {
                window.parent.tvBookChapter.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvBookChapter_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
                ClientScript.RegisterStartupScript(GetType(),
                 "jsSelectFirstNode",
                 @"window.parent.tvBookChapterChangeCallBack.callback(" + e.Keys["ChapterId"] + @", 'Rebuild');                        
                    if(window.parent.tvBookChapter.get_nodes().get_length() > 0)
                    {
                        window.parent.tvBookChapter.get_nodes().getNode(0).select();
                    }",
                  true);
        }

        protected void fvBookChapter_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvBookChapterChangeCallBack.callback(-1, 'Rebuild');                        
            if(window.parent.tvBookChapter.get_nodes().get_length() > 0)
            {
                window.parent.tvBookChapter.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}
