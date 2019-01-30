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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class SetCopyRandomExamName : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				RandomExamBLL objBll = new RandomExamBLL();
				txtName.Text = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("examID"))).ExamName;
				ViewState["ExamName"] = txtName.Text;
			}
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
			    btnOK.Visible = false;
				RandomExamBLL objBll = new RandomExamBLL();
				RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("examID")));
                //if (objRandomExam.HasTrainClass)
                //{
                //    SessionSet.PageMessage = "不能复制新增培训班考试！";
                //    return;
                //}

				if(objRandomExam.IsReset)
				{
					SessionSet.PageMessage = "不能复制新增补考考试！";
					return;
				}

				if(ViewState["ExamName"].ToString() == txtName.Text.Trim())
				{
					SessionSet.PageMessage = "考试名称不能与原考试名称重复！";
					return;
				}

				objBll.AddCopyRandomExam(Convert.ToInt32(Request.QueryString.Get("examID")), txtName.Text);
				Response.Write("<script>top.returnValue='true';top.close();</script>");
			}
			catch
			{
				SessionSet.PageMessage = "复制失败！";
			    btnOK.Visible = true;
			}
		}
	}
}
