using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;
using System.Data;
using DSunSoft.Web.Global;

namespace RailExamWebApp.Book
{
    public partial class BookChapterDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfChapterID.Value = Request.QueryString["id"];
        	hfIsWuhan.Value = PrjPub.IsWuhan().ToString();

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
                if(Request.QueryString["Mode"] == "Edit")
                {
                    ((HiddenField)fvBookChapter.FindControl("hfIsEdit")).Value = "true";
                }
                else
                {
                    ((HiddenField)fvBookChapter.FindControl("hfIsEdit")).Value = "false";
                }
            }
            else if (fvBookChapter.CurrentMode == FormViewMode.Edit)
            {
                if (Request.QueryString["Mode"] == "Edit")
                {
                    ((HiddenField)fvBookChapter.FindControl("hfIsEdit")).Value = "true";
                }
                else
                {
                    ((HiddenField)fvBookChapter.FindControl("hfIsEdit")).Value = "false";
                }
            }


            string strRefresh = Request.Form.Get("Refresh");
            if (strRefresh != null & strRefresh != "")
            {
                string strPath = "../Online/Book/" + Request.QueryString["BookID"].ToString() + "/" + Request.QueryString["id"].ToString() + ".htm";

                BookChapterBLL objBill = new BookChapterBLL();
                objBill.UpdateBookChapterUrl(Convert.ToInt32(Request.QueryString["id"].ToString()), strPath);
               RailExam.Model.BookChapter objBookChapter = objBill.GetBookChapter(Convert.ToInt32(Request.QueryString["id"].ToString()));
                string strChapterName = objBookChapter.ChapterName;

               string  str =File.ReadAllText(Server.MapPath(strPath), Encoding.UTF8);

                if(str.IndexOf("chaptertitle") < 0)
                {
                    if(objBookChapter.LevelNum < 3)
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


                BookBLL objBookBill = new BookBLL();
                if(Request.QueryString.Get("Mode") == "Edit")
                {
                    objBookBill.UpdateBookVersion(Convert.ToInt32(Request.QueryString["BookID"].ToString()));
                }
                string strBookName = objBookBill.GetBook(Convert.ToInt32(Request.QueryString["BookID"].ToString())).bookName;

                SystemLogBLL objLogBll = new SystemLogBLL();
                objLogBll.WriteLog("编辑教材《" + strBookName + "》中“" + strChapterName + "”的章节内容");

                fvBookChapter.DataBind();
                Grid1.DataBind();

                objBill.GetIndex(Request.QueryString["BookID"].ToString());

                ItemBLL objBll = new ItemBLL();
                IList<RailExam.Model.Item> objList =
                    objBll.GetItemsByBookChapterId(Convert.ToInt32(Request.QueryString["BookID"]), Convert.ToInt32(Request.QueryString["id"]),0,0);

                if (objList.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"var ret = window.showModalDialog('/RailExamBao/Book/ItemEnabled.aspx?BookID=" + Request.QueryString["BookID"] + @"&ChapterID=" + Request.QueryString["id"] + @"','','help:no; status:no;dialogWidth:300px;dialogHeight:120px;');",
                        true);
                }
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
            BookUpdateBLL BookChapterUpdateBLL = new BookUpdateBLL();

            BookChapterUpdateBLL.DeleteBookUpdate(nBookChapterUpdateID);
        }

        protected void fvBookChapter_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            bool isMotherItem = Convert.ToBoolean(e.Values["IsMotherItem"]);
            int ParentId = Convert.ToInt32(e.Values["ParentId"]);
            string strName = e.Values["ChapterName"].ToString();
            int bookID = Convert.ToInt32(e.Values["BookId"]);

            if (isMotherItem)
            {
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet("select * from Book_Chapter where Chapter_ID=" + ParentId);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    //SessionSet.PageMessage = "章不能为复习要点！";
                    //e.Cancel = true;
                    //ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"setImageBtnVisiblityInsert()", true);
                    //return;
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["Is_Mother_Item"].ToString() == "1")
                    {
                        SessionSet.PageMessage = "当前节点的父节点为复习要点，不能为复习要点新增下级复习要点！";
                        e.Cancel = true;
                        ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"setImageBtnVisiblityInsert()", true);
                        return;
                    }
                }

                ds = db.RunSqlDataSet("select * from Book_Chapter where Book_ID="+ bookID +" and  Chapter_Name='" + strName+"'");

                if(ds.Tables[0].Rows.Count > 0 )
                {
                    SessionSet.PageMessage = "同一教材下复习要点名称不能重复！";
                    e.Cancel = true;
                    ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"setImageBtnVisiblityInsert()", true);
                    return;
                }

            }
        }

        protected void fvBookChapter_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            string strId = e.Keys["ChapterId"].ToString();
            bool isMotherItem = Convert.ToBoolean(e.NewValues["IsMotherItem"]);
            int ParentId = Convert.ToInt32(e.NewValues["ParentId"]);
            string strName = e.NewValues["ChapterName"].ToString();
            int bookID = Convert.ToInt32(e.NewValues["BookId"]);

            if (isMotherItem)
            {
                BookChapterBLL objBll = new BookChapterBLL();

                //RailExam.Model.BookChapter chapter = objBll.GetBookChapter(Convert.ToInt32(strId));
                //if(chapter.ParentId == 0)
                //{
                //    SessionSet.PageMessage = "章不能为复习要点！";
                //    e.Cancel = true;
                //    ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"setImageBtnVisiblityUpdate()", true);
                //    return;
                //}
                
                IList<RailExam.Model.BookChapter> objList = objBll.GetBookChapterByParentID(Convert.ToInt32(strId));

                if(objList.Count>0)
                {
                    SessionSet.PageMessage = "当前节点存在下级节点，不能设置为复习要点！";
                    e.Cancel = true;
                    ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"setImageBtnVisiblityUpdate()", true);
                    return;
                }

                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet("select * from Book_Chapter where Chapter_ID<>"+ strId +" and Book_ID=" + bookID + " and  Chapter_Name='" + strName + "'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    SessionSet.PageMessage = "同一教材下复习要点名称不能重复！";
                    e.Cancel = true;
                    ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"setImageBtnVisiblityInsert()", true);
                    return;
                }
            }
        }

        protected void fvBookChapter_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            string strId = e.Values["ParentId"].ToString();
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvBookChapterChangeCallBack.callback(" + strId + @", 'Insert');                        
            if(window.parent.tvBookChapter.get_nodes().get_length() > 0)
            {
                window.parent.tvBookChapter.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvBookChapter_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ItemBLL objBll = new ItemBLL();
            IList<RailExam.Model.Item> objList =
                objBll.GetItemsByBookChapterId(Convert.ToInt32(e.Keys["BookId"].ToString()),Convert.ToInt32(e.Keys["ChapterId"].ToString()),0,0);

            if (objList.Count > 0 )
            {
                ClientScript.RegisterStartupScript(GetType(),
                    "jsSelectFirstNode",
                    @"var ret = window.showModalDialog('/RailExamBao/Book/ItemEnabled.aspx?BookID=" + e.Keys["BookId"] + @"&ChapterID=" + e.Keys["ChapterId"] + @"','','help:no; status:no;dialogWidth:300px;dialogHeight:120px;');
                     window.parent.tvBookChapterChangeCallBack.callback(" + e.Keys["ChapterId"] + @", 'Rebuild');                        
                    if(window.parent.tvBookChapter.get_nodes().get_length() > 0)
                    {
                        window.parent.tvBookChapter.get_nodes().getNode(0).select();
                    } " ,         
                    true);
            }
            else
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