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

namespace RailExamWebApp.AssistBook
{
    public partial class SetOrderIndex : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtOrderIndex.Text = Request.QueryString.Get("NowOrder");
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            AssistBookBLL objBll = new AssistBookBLL();
            RailExam.Model.AssistBook obj = objBll.GetAssistBook(Convert.ToInt32(Request.QueryString.Get("BookID")));
            int order = Convert.ToInt32(txtOrderIndex.Text);
            int max = Convert.ToInt32(Request.QueryString.Get("MaxOrder"));
            if (order > max)
            {
                obj.OrderIndex = max;
            }
            else
            {
                obj.OrderIndex = order;
            }
            objBll.UpdateAssistBook(obj);

            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }
    }
}
