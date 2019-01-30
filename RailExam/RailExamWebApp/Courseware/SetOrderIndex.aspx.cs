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

namespace RailExamWebApp.Courseware
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
            if (Request.QueryString.Get("TrainTypeID") != null)
            {
                int bookID = Convert.ToInt32(Request.QueryString.Get("CoursewareID"));
                int trainTypeID = Convert.ToInt32(Request.QueryString.Get("TrainTypeID"));
                CoursewareTrainTypeBLL objBll = new CoursewareTrainTypeBLL();
                CoursewareTrainType obj = objBll.GetCoursewareTrainType(bookID, trainTypeID);
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
                objBll.UpdateCoursewareTrainType(obj);
            }
            else
            {
                CoursewareBLL objBll = new CoursewareBLL();
                RailExam.Model.Courseware obj = objBll.GetCourseware(Convert.ToInt32(Request.QueryString.Get("CoursewareID")));
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
                objBll.UpdateCourseware(obj);
            }

            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }
    }
}
