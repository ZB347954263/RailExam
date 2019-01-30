using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Common
{
    public partial class GetKnowledgeBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strKnowledgeId = Request.QueryString.Get("id");
            string strflag = Request.QueryString.Get("flag");
			string strItemTypeID = Request.QueryString.Get("itemTypeID");
            ItemBLL objItemBll = new ItemBLL();
            string strBook = "";

            if (!string.IsNullOrEmpty(strKnowledgeId))
            {
                ComponentArt.Web.UI.TreeView tvBookBook = new ComponentArt.Web.UI.TreeView();

                BookBLL bookBLL = new BookBLL();
                IList<RailExam.Model.Book> bookList = null;
                if (strflag != null && strflag=="2")
                {
                    int knowledgeID = Convert.ToInt32(strKnowledgeId);
                    int postID = Convert.ToInt32(Request.QueryString.Get("PostID"));
                    int orgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
                    int  leader = Convert.ToInt32(Request.QueryString.Get("Leader"));
                    int techID = Convert.ToInt32(Request.QueryString.Get("Tech"));
                    bookList = bookBLL.GetEmployeeStudyBookInfoByKnowledgeID(knowledgeID, orgID, postID, leader, techID, 0);
                }
                else
                {
					if(PrjPub.CurrentLoginUser.SuitRange == 0 && Request.QueryString.Get("source") == "itemlist")
					{
						bookList = bookBLL.GetBookByKnowledgeIDPath(strKnowledgeId, PrjPub.CurrentLoginUser.StationOrgID);

                        if (!string.IsNullOrEmpty(Request.QueryString.Get("postId")))
                        {
                            string postID = Request.QueryString.Get("postId");
                            OracleAccess oa = new OracleAccess();

                            string sql = String.Format(
                                @"select book_id from BOOK_RANGE_POST t 
                            where 
                            post_id = {0} 
                            or 
                            post_id in 
                                (select post_id from POST t where parent_id = {0})",
                                postID
                            );

                            IList<RailExam.Model.Book> booksViaPosts = new List<RailExam.Model.Book>();
                            DataSet dsBookIDs = oa.RunSqlDataSet(sql);
                            if (dsBookIDs != null && dsBookIDs.Tables.Count > 0)
                            {
                                foreach (RailExam.Model.Book book in bookList)
                                {
                                    DataRow[] drs = dsBookIDs.Tables[0].Select("book_id=" + book.bookId);
                                    if (drs.Length > 0)
                                    {
                                        booksViaPosts.Add(book);
                                    }
                                }
                                bookList.Clear();
                                bookList = booksViaPosts;
                            }
                        }
					}
					else
					{
                        if (!string.IsNullOrEmpty(Request.QueryString.Get("RandomExamID")))
                        {
                            string examId = Request.QueryString.Get("RandomExamID");
                            RandomExamBLL objBll = new RandomExamBLL();
                            RailExam.Model.RandomExam objexam = objBll.GetExam(Convert.ToInt32(examId));
                            string strPost = objexam.PostID;

                            if(objexam.AutoSaveInterval == 1)
                            {
                                strPost = "";
                            }

                            bookList = bookBLL.GetBookByKnowledgeIDPath(strKnowledgeId);

                            OracleAccess oa = new OracleAccess();
                            if (strPost != "")
                            {
                                string sql =
                                    @"select book_id from BOOK_RANGE_POST t 
                                     where  post_id in (" +
                                    strPost + @")";
                                DataTable dt = oa.RunSqlDataSet(sql).Tables[0];

                                IList<RailExam.Model.Book> objList = new List<RailExam.Model.Book>();
                                foreach (RailExam.Model.Book book in bookList)
                                {
                                    DataRow[] dr = dt.Select("book_id=" + book.bookId);
                                    if (dr.Length > 0)
                                    {
                                        objList.Add(book);
                                    }
                                }
                                bookList.Clear();
                                bookList = objList;
                            }

                            if(objexam.HasTrainClass)
                            {
                                string sql = "select * from ZJ_Train_Class_Subject_Book a "
                                             +
                                             " inner join ZJ_Train_Class_Subject b on a.Train_Class_Subject_ID = b.Train_Class_Subject_ID "
                                             +
                                             " where b.Train_Class_ID in (select Train_Class_ID from Random_Exam_Train_Class "
                                             + " where Random_Exam_ID=" + objexam.RandomExamId + ")";
                                DataTable dt = oa.RunSqlDataSet(sql).Tables[0];

                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (strBook == "")
                                    {
                                        strBook += dr["Book_ID"].ToString();
                                    }
                                    else
                                    {
                                        strBook += "," + dr["Book_ID"];
                                    }
                                }
                            }
                        }
                        else
                        {
                            bookList = bookBLL.GetBookByKnowledgeIDPath(strKnowledgeId);

                            OracleAccess oa = new OracleAccess();
                            IList<RailExam.Model.Book> booksViaPosts = new List<RailExam.Model.Book>();
                            
                            if (!string.IsNullOrEmpty(Request.QueryString.Get("postId")))
                            {
                                string postID = Request.QueryString.Get("postId");

                                string sql = String.Format(
                                    @"select book_id from BOOK_RANGE_POST t 
                            where 
                            post_id = {0} 
                            or 
                            post_id in 
                                (select post_id from POST t where parent_id = {0})",
                                    postID
                                );

                                DataSet dsBookIDs = oa.RunSqlDataSet(sql);
                                if (dsBookIDs != null && dsBookIDs.Tables.Count > 0)
                                {
                                    foreach (RailExam.Model.Book book in bookList)
                                    {
                                        DataRow[] drs = dsBookIDs.Tables[0].Select("book_id=" + book.bookId);
                                        if (drs.Length > 0)
                                        {
                                            booksViaPosts.Add(book);
                                        }
                                    }
                                    bookList.Clear();
                                    bookList = booksViaPosts;
                                }
                            }

                            //铁路系统权限
                            int railSystemid = PrjPub.RailSystemId();
                            if (railSystemid != 0)
                            {
                                string sql = String.Format(
                                    @"select book_id from BOOK_RANGE_ORG t 
                                        where 
                                         org_Id in (select org_id from org where rail_System_Id={0} and level_num=2) ",
                                    railSystemid
                                    );

                                DataSet dsBookIDs = oa.RunSqlDataSet(sql);
                                if (dsBookIDs != null && dsBookIDs.Tables.Count > 0)
                                {
                                    foreach (RailExam.Model.Book book in bookList)
                                    {
                                        DataRow[] drs = dsBookIDs.Tables[0].Select("book_id=" + book.bookId);
                                        if (drs.Length > 0)
                                        {
                                            booksViaPosts.Add(book);
                                        }
                                    }
                                    bookList.Clear();
                                    bookList = booksViaPosts;
                                }
                            }
                        }
					}
                }

                if (bookList.Count > 0)
                {
                    TreeViewNode tvn = null;

                    foreach (RailExam.Model.Book book in bookList)
                    {
                        tvn = new TreeViewNode();
                        tvn.ID = book.bookId.ToString();
                        tvn.Value = book.bookId.ToString();
                        if(Request.QueryString.Get("item") != null &&  Request.QueryString.Get("item")=="no")
                        {
                            tvn.Text = book.bookName;
                        }
                        else
                        {
							int n = objItemBll.GetItemsByBookID(book.bookId, Convert.ToInt32(strItemTypeID));
                            if (n > 0)
                            {
								tvn.Text = book.bookName + "（" + n + "题）";
                            }
                            else
                            {
                                tvn.Text = book.bookName;
                            }
                        }

                        if((","+strBook+",").IndexOf(","+book.bookId+",")>=0)
                        {
                            tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/RedBook.gif";
                        }
                        else
                        {
                            tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Book.gif";
                        }

                        tvn.ToolTip = book.bookName;
                        tvn.Attributes.Add("isBook", "true");
                        
                        if (strflag != null && (strflag == "2" || strflag == "3" || strflag=="4"))
                        {
                            tvn.ShowCheckBox = true;

                            if(strflag == "4")
                            {
                                string strBookIds = Request.QueryString.Get("bookIds");
                                if(("|"+strBookIds+"|").IndexOf("|"+book.bookId+"|")>=0)
                                {
                                    tvn.Checked = true;
                                }
                            }
                        }
                         


                        //没有题目数量显示
                        if (Request.QueryString.Get("item") != null && Request.QueryString.Get("item") == "no")
                        {
                            if (strflag != null)
                            {
                                if (strflag == "2")
                                {
                                    tvn.ContentCallbackUrl = "../Common/GetBookChapter.aspx?item=no&flag=" + strflag + "&id=" + book.bookId;
                                }
                                //屏蔽教材
                                else if(strflag == "1")
                                {
                                    tvn.ContentCallbackUrl = "../Common/GetBookChapter.aspx?item=no&flag=" + strflag + "&id=" + book.bookId+"&StrategyID=" + Request.QueryString.Get("StrategyID");
                                }
                            }
                            else
                            {
                                tvn.ContentCallbackUrl = "../Common/GetBookChapter.aspx?item=no&id=" + book.bookId;
                            }
                        }
                        else
                        {
                            if (strflag != null)
                            {
                                if (strflag == "2")
                                {
                                    tvn.ContentCallbackUrl = "../Common/GetBookChapter.aspx?itemTypeID="+ strItemTypeID +"&flag=" + strflag + "&id=" + book.bookId;
                                }
                                //屏蔽教材
                                else if (strflag == "1")
                                {
									tvn.ContentCallbackUrl = "../Common/GetBookChapter.aspx?itemTypeID=" + strItemTypeID + "&flag=" + strflag + "&id=" + book.bookId + "&StrategyID=" + Request.QueryString.Get("StrategyID");
                                }
                            }
                            else
                            {
								tvn.ContentCallbackUrl = "../Common/GetBookChapter.aspx?itemTypeID=" + strItemTypeID + "&id=" + book.bookId;
                            }
                        }

                        tvBookBook.Nodes.Add(tvn);
                    }
                }

                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "text/xml";
                Response.Cache.SetNoStore();

                string strXmlEncoding = string.Empty;
                try
                {
                    strXmlEncoding = System.Configuration.ConfigurationManager.AppSettings["CallbackEncoding"];
                }
                catch
                {
                    strXmlEncoding = "gb2312";
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Error Accessing Web.Config File!\r\n"
                        + "Using \"gb2312\"!");
#endif
                }
                if (string.IsNullOrEmpty(strXmlEncoding))
                {
                    strXmlEncoding = "gb2312";
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("CallbackEncoding Empty in Web.Config File!\r\n"
                        + "Using \"gb2312\"!");
#endif
                }
                else
                {
                    try
                    {
                        System.Text.Encoding enc = System.Text.Encoding.GetEncoding(strXmlEncoding);
                    }
                    catch
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine("Invalid Encoding in Web.Config File!\r\n"
                            + "Using \"gb2312\"!");
#endif
                        strXmlEncoding = "gb2312";
                    }
                }

                Response.Write("<?xml version=\"1.0\" encoding=\"" + strXmlEncoding + "\" standalone=\"yes\" ?>\r\n"
                    + tvBookBook.GetXml());
                Response.Flush();
                Response.End();
            }
        }
    }
}
