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

namespace RailExamWebApp.Courseware
{
	public partial class ViewSwfFull : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string strCoursewareID = Request.QueryString.Get("id");

				CoursewareBLL objBll = new CoursewareBLL();
				RailExam.Model.Courseware obj = objBll.GetCourseware(Convert.ToInt32(strCoursewareID));

				hfUrl.Value = obj.Url;
			}
		}
	}
}
