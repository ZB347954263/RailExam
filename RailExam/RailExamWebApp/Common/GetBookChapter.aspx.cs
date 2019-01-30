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
    public partial class GetBookChapter : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strBookChapterId = Request.QueryString.Get("id");
            ItemBLL objItemBll = new ItemBLL();
			string strItemTypeID = Request.QueryString.Get("itemTypeID");

            if (!string.IsNullOrEmpty(strBookChapterId))
            {
                ComponentArt.Web.UI.TreeView tvBookChapterChapter = new ComponentArt.Web.UI.TreeView();

                BookChapterBLL bookChapterBLL = new BookChapterBLL();
                IList<RailExam.Model.BookChapter> bookChapterList = bookChapterBLL.GetBookChapterByBookID(int.Parse(strBookChapterId));

                if (bookChapterList.Count > 0)
                {
                    TreeViewNode tvn = null;

                    foreach (RailExam.Model.BookChapter bookChapter in bookChapterList)
                    {
                        if(bookChapter.IsMotherItem)
                        {
                            continue;
                        }
                        tvn = new TreeViewNode();
                        tvn.ID = bookChapter.ChapterId.ToString();
                        tvn.Value = bookChapter.ChapterId.ToString();
                        if(Request.QueryString.Get("item") != null &&  Request.QueryString.Get("item")=="no")
                        {
                            tvn.Text = bookChapter.ChapterName;
                        }
                        else
                        {
                            int n = objItemBll.GetItemsByBookChapterIdPath(bookChapter.IdPath,Convert.ToInt32(strItemTypeID));
                            if (n > 0)
                            {
								tvn.Text = bookChapter.ChapterName + "（" + n + "题）";
                            }
                            else
                            {
                                tvn.Text = bookChapter.ChapterName;
                            }
                        }

                        tvn.ToolTip = bookChapter.ChapterName;

                        string strflag = Request.QueryString.Get("flag");
                        if (strflag != null && (strflag == "2" || strflag=="1"))
                        {
                            tvn.ShowCheckBox = true;
                        }

                        if(Request.QueryString.Get("state")!=null)
                        {
                            tvn.Checked = Convert.ToBoolean(Request.QueryString.Get("state"));
                        }
                        else
                        {
                            if (Request.QueryString.Get("StrategyID") != null)
                            {
                                string str = "," + Request.QueryString.Get("StrategyID") + ",";
                                if (str.IndexOf("," + tvn.ID + ",") != -1)
                                {
                                    tvn.Checked = true;
                                }
                            }
                        }

                        tvn.Attributes.Add("isChapter", "true");
                        tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Chapter.gif";

                        if (bookChapter.ParentId == 0)
                        {
                            tvBookChapterChapter.Nodes.Add(tvn);
                        }
                        else
                        {
                            tvBookChapterChapter.FindNodeById(bookChapter.ParentId.ToString()).Nodes.Add(tvn);
                        }
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
                    + tvBookChapterChapter.GetXml());

                Response.Flush();
                Response.End();
            }
        }
    }
}
