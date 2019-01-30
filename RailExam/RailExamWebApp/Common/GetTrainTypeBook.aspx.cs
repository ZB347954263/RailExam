using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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

namespace RailExamWebApp.Common
{
    public partial class GetTrainTypeBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strKnowledgeId = Request.QueryString.Get("id");
            ItemBLL objItemBll = new ItemBLL();
			string strItemTypeID = Request.QueryString.Get("itemTypeID");

			if (!string.IsNullOrEmpty(strKnowledgeId))
            {
                ComponentArt.Web.UI.TreeView tvBookBook = new ComponentArt.Web.UI.TreeView();
                string strflag = Request.QueryString.Get("flag");
                BookBLL bookBLL = new BookBLL();
                IList<RailExam.Model.Book> bookList = null;

                if (strflag != null && strflag == "2")
                {
                    int trainTypeID = Convert.ToInt32(strKnowledgeId);
                    int postID = Convert.ToInt32(Request.QueryString.Get("PostID"));
                    int orgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
                    int leader = Convert.ToInt32(Request.QueryString.Get("Leader"));
                    int techID = Convert.ToInt32(Request.QueryString.Get("Tech"));
                    bookList = bookBLL.GetEmployeeStudyBookInfoByTrainTypeID(trainTypeID, orgID, postID, leader, techID, 0);
                }
                else
                {
                     bookList = bookBLL.GetBookByTrainTypeIDPath(strKnowledgeId);
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
                                tvn.Text = book.bookName + "（" + n + "）";
                            }
                            else
                            {
                                tvn.Text = book.bookName;
                            }
                        }
                        
                        tvn.ToolTip = book.bookName;
                        tvn.Attributes.Add("isBook", "true");
                        tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Book.gif";

                        if (strflag != null && (strflag == "2" || strflag == "3"))
                        {
                            tvn.ShowCheckBox = true;
                        }
                        if (Request.QueryString.Get("item") != null && Request.QueryString.Get("item") == "no")
                        {
                            if (strflag != null)
                            {
                                if (strflag != "3")
                                {
                                    tvn.ContentCallbackUrl = "../Common/GetBookChapter.aspx?item=no&flag=" + strflag + "&id=" +
                                                             book.bookId;
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
                                if (strflag != "3")
                                {
									tvn.ContentCallbackUrl = "../Common/GetBookChapter.aspx?itemTypeID=" + strItemTypeID + "&flag=" + strflag + "&id=" +
                                                             book.bookId;
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
