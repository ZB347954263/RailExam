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
    public partial class GetPaper : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPaperCategoryId = Request.QueryString.Get("id");
            string strflag = Request.QueryString.Get("flag");

            if (!string.IsNullOrEmpty(strPaperCategoryId))
            {
                int nPaperCategoryId = -1;

                try
                {
                    nPaperCategoryId = Convert.ToInt32(strPaperCategoryId);
                }
                catch
                {

                }

                ComponentArt.Web.UI.TreeView tv = new ComponentArt.Web.UI.TreeView();
                PaperBLL paperBLL = new PaperBLL();

                IList<RailExam.Model.Paper> papers = paperBLL.GetPaperByCategoryID(nPaperCategoryId);

                if (papers.Count > 0)
                {
                    TreeViewNode tvn = null;

                    foreach (RailExam.Model.Paper paper in papers)
                    {
                        tvn = new TreeViewNode();
                        tvn.ID = paper.PaperId.ToString();
                        tvn.Value = paper.PaperId.ToString();
                        tvn.Text = paper.PaperName;
                        tvn.ToolTip = paper.PaperName;
                        tvn.Attributes.Add("isPaper", "true");
                        tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Book.gif";

                        if (strflag != null && strflag == "1")
                        {
                            tvn.ShowCheckBox = true;
                        }

                        tv.Nodes.Add(tvn);
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
                    + tv.GetXml());
                Response.Flush();
                Response.End();
            }
        }
    }
}
