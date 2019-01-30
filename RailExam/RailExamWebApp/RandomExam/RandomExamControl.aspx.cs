using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamControl : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.HasEditRight("考试监控") )
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.IsServerCenter)
                {
					hfOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
					if (PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.SuitRange ==1)
                    {
                        hfIsAdmin.Value = "True";
                    }
                    else
                    {
                        hfIsAdmin.Value = "False";
                    }
                }
                else
                {
					hfOrgID.Value = ConfigurationManager.AppSettings["StationID"].ToString();
                    if ((PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.SuitRange == 1) || (PrjPub.CurrentLoginUser.IsAdmin  && PrjPub.CurrentLoginUser.StationOrgID.ToString() == hfOrgID.Value))
                    {
                        hfIsAdmin.Value = "True";
                    }
                    else
                    {
                        hfIsAdmin.Value = "False";
                    }
                }

                hfServerNo.Value = PrjPub.ServerNo.ToString();
            }

            string strRefresh = Request.Form.Get("Refresh");
            if(strRefresh != null && strRefresh != "")
            {
                //RandomExamBLL objBll = new RandomExamBLL();
				//IList<RailExam.Model.RandomExam> objList = objBll.GetControlRandomExamsInfo(SessionSet.OrganizationID);
                //examsGrid.DataSource = objList;
                examsGrid.DataBind();
            }

			if (Request.Form.Get("StudentInfo") != null && Request.Form.Get("StudentInfo") != "")
			{
				DownloadStudentInfoExcel(Request.Form.Get("StudentInfo"));
			}
        }

        protected void searchExamCallBack_Callback(object sender, CallBackEventArgs e)
        {
            examsGrid.DataBind();
            examsGrid.RenderControl(e.Output);
        }

		protected  void btnApply_Click(object sender,EventArgs e)
		{
			RandomExamApplyBLL objbll = new RandomExamApplyBLL();
			IList<RandomExamApply> objList = objbll.GetRandomExamApplyByOrgID(Convert.ToInt32(hfOrgID.Value),PrjPub.ServerNo.ToString());
			if(objList.Count == 0)
			{
				SessionSet.PageMessage = "没有需要回复的请求！";
				return;
			}

			ClientScript.RegisterStartupScript(GetType(),
						"jsSelectFirstNode",
						@"showApply();",
						true);
		}

		private void DownloadStudentInfoExcel(string strID)
		{
			string path = Server.MapPath("/RailExamBao/Excel/Excel.xls");

			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(strID));

			FileInfo file = new FileInfo(path);
			this.Response.Clear();
			this.Response.Buffer = true;
			this.Response.Charset = "utf-7";
			this.Response.ContentEncoding = Encoding.UTF7;
			// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
			this.Response.AddHeader("Content-Disposition",
									"attachment; filename=" + HttpUtility.UrlEncode(objRandomExam.ExamName + "参加考试学员名单") + ".xls");
			// 添加头信息，指定文件大小，让浏览器能够显示下载进度
			this.Response.AddHeader("Content-Length", file.Length.ToString());

			// 指定返回的是一个不能被客户端读取的流，必须被下载
			this.Response.ContentType = "application/ms-excel";

			// 把文件流发送到客户端
			this.Response.WriteFile(file.FullName);
		}
    }
}
