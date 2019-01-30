using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.Book
{
    public partial class ItemEnabled : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewState["BookID"] = Request.QueryString.Get("BookID");
                ViewState["ChapterID"] = Request.QueryString.Get("ChapterID");
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            ItemBLL  objBll = new ItemBLL();
            objBll.UpdateItemEnabled(Convert.ToInt32(ViewState["BookID"].ToString()),
                                     Convert.ToInt32(ViewState["ChapterID"].ToString()),2);
            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }
    }
}
