using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Book
{
    public partial class BookChapter : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strBookID = Request.QueryString.Get("id");
                ViewState["BookID"] = strBookID;
                if (!string.IsNullOrEmpty(strBookID))
                {
                    hfBookID.Value = strBookID;
                    BindTree();
                }

                string strPath = Server.MapPath("../Online/Book/" + ViewState["BookID"].ToString());
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                    Directory.CreateDirectory(strPath + "/Upload");
                    CopyTemplate(Server.MapPath("../Online/Book/template/"),
                                 Server.MapPath("../Online/Book/" + ViewState["BookID"].ToString() + "/"));
                }

                if (!string.IsNullOrEmpty(Request.QueryString.Get("Type"))  &&  Request.QueryString.Get("Type")=="Add")
                {
                    #region 保存教材前言
                    //string strBookUrl = "../Book/" + ViewState["BookID"].ToString() + "/cover.htm";

                    //BookBLL objBill = new BookBLL();
                    //objBill.UpdateBookUrl(Convert.ToInt32(ViewState["BookID"].ToString()), strBookUrl);

                    //string strBookName = objBill.GetBook(Convert.ToInt32(ViewState["BookID"].ToString())).bookName;

                    //string srcPath = "../Online/Book/" + ViewState["BookID"].ToString() + "/Cover.htm";
                    //string str = File.ReadAllText(Server.MapPath(srcPath), System.Text.Encoding.UTF8);
                    //if (str.IndexOf("booktitle") < 0)
                    //{
                    //    str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                    //         + "<div id='booktitle'>" + strBookName + "</div>" + "<br>"
                    //         + str;
                    //    File.WriteAllText(Server.MapPath(srcPath), str, System.Text.Encoding.UTF8);
                    //}

                    //BookChapterBLL objChapterBll = new BookChapterBLL();
                    //objChapterBll.GetIndex(ViewState["BookID"].ToString());

                    //SystemLogBLL objLogBll = new SystemLogBLL();
                    //objLogBll.WriteLog("编辑教材《" + strBookName + "》前言");
                    #endregion

                    #region 生成教材封面

                    //string strBookUrl = "../Book/" + ViewState["BookID"].ToString() + "/cover.htm";
                    //BookBLL objBill = new BookBLL();
                    //objBill.UpdateBookUrl(Convert.ToInt32(ViewState["BookID"].ToString()), strBookUrl);

                    //RailExam.Model.Book obj = objBill.GetBook(Convert.ToInt32(ViewState["BookID"].ToString()));

                    //string srcPath = "../Online/Book/" + ViewState["BookID"].ToString() + "/Cover.htm";

                    //if(!File.Exists(Server.MapPath(srcPath)))
                    //{
                    //    File.Create(Server.MapPath(srcPath));
                    //}

                    //string str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                    //             + "<div id='booktitle'>" + obj.bookName + "</div>" + "<br>"
                    //             + "<br><br><br><br><br><br><br><br><br><br><br>"
                    //             + "<div id='orgtitle'>" + obj.publishOrgName + "</div>" + "<br>"
                    //             + "<div id='datetitle'>" + obj.publishDate.ToLongDateString() + "</div>";

                    //File.WriteAllText(Server.MapPath(srcPath), str, System.Text.Encoding.UTF8);
                   #endregion
                }
            }
        }

        private void BindTree()
        {
            //添加书名
            BookBLL bookBLL = new BookBLL();

            RailExam.Model.Book book = bookBLL.GetBook(Convert.ToInt32(ViewState["BookID"].ToString()));

            TreeViewNode tvn1 = new TreeViewNode();
            tvn1.ID = "0";
            tvn1.Value = ViewState["BookID"].ToString();
            tvn1.Text = book.bookName;
            tvn1.ToolTip = book.bookName;
            tvBookChapter.Nodes.Add(tvn1);

            //添加章节
            BookChapterBLL bookChapterBLL = new BookChapterBLL();

            IList<RailExam.Model.BookChapter> bookChapterList = bookChapterBLL.GetBookChapterByBookID(Convert.ToInt32(ViewState["BookID"].ToString()));

            if (bookChapterList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.BookChapter bookChapter in bookChapterList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = bookChapter.ChapterId.ToString();
                    tvn.Value = bookChapter.BookId.ToString();
                    tvn.Text = bookChapter.ChapterName;
                    tvn.ToolTip = bookChapter.ChapterName;

                    if (bookChapter.ParentId == 0)
                    {
                        //tvBookChapter.Nodes.Add(tvn);
                        tvBookChapter.FindNodeById(bookChapter.ParentId.ToString()).Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvBookChapter.FindNodeById(bookChapter.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvBookChapter.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";

                            return;
                        }
                    }
                }
            }

            tvBookChapter.DataBind();
            tvBookChapter.ExpandAll();
        }

        protected void tvBookChapterChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            BookChapterBLL objBookChapter = new BookChapterBLL();
            RailExam.Model.BookChapter obj = new RailExam.Model.BookChapter();
            obj = objBookChapter.GetBookChapterInfo(int.Parse(e.Parameters[0]));
            int cout = tvBookChapter.FindNodeById(obj.ParentId.ToString()).Nodes.Count;

            if (e.Parameters[1] == "MoveUp")
            {
                if (obj.OrderIndex <= cout && obj.OrderIndex >= 2)
                {
                    obj.OrderIndex--;
                    if(e.Parameters[2] == "Edit")
                    {
                        obj.IsEdit = true;
                    }
                    else
                    {
                        obj.IsEdit = false;
                    }
                    objBookChapter.UpdateBookChapter(obj);

                    obj = objBookChapter.GetBookChapter(int.Parse(tvBookChapter.FindNodeById(e.Parameters[0]).PreviousSibling.ID));
                    obj.OrderIndex++;
                    objBookChapter.UpdateBookChapter(obj);
                }
            }
            if (e.Parameters[1] == "MoveDown")
            {
                if (obj.OrderIndex <= cout - 1 && obj.OrderIndex >= 1)
                {
                    obj.OrderIndex++;
                    if (e.Parameters[2] == "Edit")
                    {
                        obj.IsEdit = true;
                    }
                    else
                    {
                        obj.IsEdit = false;
                    }
                    objBookChapter.UpdateBookChapter(obj);

                    obj = objBookChapter.GetBookChapter(int.Parse(tvBookChapter.FindNodeById(e.Parameters[0]).NextSibling.ID));
                    obj.OrderIndex--;
                    objBookChapter.UpdateBookChapter(obj);
                }
            }
			if(e.Parameters[1] =="Insert")
			{
                string strSql = "select Max(Chapter_Id) from Book_Chapter where Book_ID="+ hfBookID.Value 
                    +" and Parent_ID=" + e.Parameters[0];
                OracleAccess db = new OracleAccess();
			    DataSet ds = db.RunSqlDataSet(strSql);

				hfMaxID.Value = ds.Tables[0].Rows[0][0].ToString();
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