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
using RailExam.Model;

namespace RailExamWebApp.Book
{
    public partial class SetOrderIndex : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtOrderIndex.Text = Request.QueryString.Get("NowOrder");
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if(Request.QueryString.Get("TrainTypeID") != null)
            {
                int bookID = Convert.ToInt32(Request.QueryString.Get("BookID"));
                int trainTypeID = Convert.ToInt32(Request.QueryString.Get("TrainTypeID"));
                BookTrainTypeBLL objBll = new BookTrainTypeBLL();
                BookTrainType obj = objBll.GetBookTrainType(bookID, trainTypeID);
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
                objBll.UpdateBookTrainType(obj);
            }
            else
            {
                BookBLL objBll = new BookBLL();
                RailExam.Model.Book obj = objBll.GetBook(Convert.ToInt32(Request.QueryString.Get("BookID")));
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
                objBll.UpdateBook(obj);
            }

            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }
    }
}
