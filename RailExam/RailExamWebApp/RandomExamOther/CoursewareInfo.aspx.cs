using System;
using System.Collections.Generic;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using System.IO;
using RailExamWebApp.Common.Class;
namespace RailExamWebApp.RandomExamOther
{
	public partial class CoursewareInfo : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}

				if (PrjPub.HasEditRight("课件管理") && PrjPub.IsServerCenter)//&& PrjPub.CurrentLoginUser.SuitRange == 1
				{
					HfUpdateRight.Value = "True";
				}
				else
				{
					HfUpdateRight.Value = "False";
				}
				if (PrjPub.HasDeleteRight("课件管理") && PrjPub.IsServerCenter)//&& PrjPub.CurrentLoginUser.SuitRange == 1
				{
					HfDeleteRight.Value = "True";
				}
				else
				{
					HfDeleteRight.Value = "False";
				}

				OrganizationBLL orgBll = new OrganizationBLL();
				int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);
				HfOrgId.Value = orgID.ToString();

				BindGrid();
			}
			else
			{
				string strDeleteID = Request.Form.Get("DeleteID");
				if (!string.IsNullOrEmpty(strDeleteID))
				{
					DeleteData(int.Parse(strDeleteID));
					BindGrid();
				}

				if (Request.Form.Get("Refresh") == "true")
				{
					BindGrid();
				}
			}

			string strUpID = Request.Form.Get("UpID");
			if (strUpID != null && strUpID != "")
			{
				if (Request.QueryString.Get("type") == "Courseware")
				{
					CoursewareBLL objBll = new CoursewareBLL();
					RailExam.Model.Courseware obj = objBll.GetCourseware(Convert.ToInt32(strUpID));
					obj.OrderIndex = obj.OrderIndex - 1;
					objBll.UpdateCourseware(obj);
				}

				if (Request.QueryString.Get("type") == "TrainType")
				{
					int trainTypeID = Convert.ToInt32(Request.QueryString.Get("id"));
					CoursewareTrainTypeBLL objTrainTypeBll = new CoursewareTrainTypeBLL();
					CoursewareTrainType objTrainType =
						objTrainTypeBll.GetCoursewareTrainType(Convert.ToInt32(strUpID), trainTypeID);
					objTrainType.OrderIndex = objTrainType.OrderIndex - 1;
					objTrainTypeBll.UpdateCoursewareTrainType(objTrainType);
				}
				BindGrid();
			}

			string strDownID = Request.Form.Get("DownID");
			if (strDownID != null && strDownID != "")
			{
				if (Request.QueryString.Get("type") == "Courseware")
				{
					CoursewareBLL objBll = new CoursewareBLL();
					RailExam.Model.Courseware obj = objBll.GetCourseware(Convert.ToInt32(strDownID));
					obj.OrderIndex = obj.OrderIndex + 1;
					objBll.UpdateCourseware(obj);
				}

				if (Request.QueryString.Get("type") == "TrainType")
				{
					int trainTypeID = Convert.ToInt32(Request.QueryString.Get("id"));
					CoursewareTrainTypeBLL objTrainTypeBll = new CoursewareTrainTypeBLL();
					CoursewareTrainType objTrainType =
						objTrainTypeBll.GetCoursewareTrainType(Convert.ToInt32(strDownID), trainTypeID);
					objTrainType.OrderIndex = objTrainType.OrderIndex + 1;
					objTrainTypeBll.UpdateCoursewareTrainType(objTrainType);
				}
				BindGrid();
			}
		}

		private void BindGrid()
		{
			string strIDPath = Request.QueryString["id"];

			CoursewareBLL coursewareBLL = new CoursewareBLL();
			IList<RailExam.Model.Courseware> coursewares = new List<RailExam.Model.Courseware>();

			OrganizationBLL orgBll = new OrganizationBLL();
			int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);

			if (Request.QueryString.Get("type") == "Courseware")
			{
				coursewares = coursewareBLL.GetCoursewaresByCoursewareTypeID(Convert.ToInt32(strIDPath), orgID);
			}
			else if (Request.QueryString.Get("type") == "TrainType")
			{
				coursewares = coursewareBLL.GetCoursewaresByTrainTypeID(Convert.ToInt32(strIDPath), orgID);
			}
			else
			{
				coursewares = coursewareBLL.GetCoursewaresByPostID(Convert.ToInt32(strIDPath), orgID);
			}

			if (coursewares != null)
			{
				foreach (RailExam.Model.Courseware obj in coursewares)
				{
					if (obj.CoursewareName.Length <= 20)
					{
						obj.CoursewareName = "<a onclick=OpenIndex('" + obj.CoursewareID + "') href=# title=" + obj.CoursewareName + " > " + obj.CoursewareName + " </a>";
					}
					else
					{
						obj.CoursewareName = "<a onclick=OpenIndex('" + obj.CoursewareID + "') href=# title=" + obj.CoursewareName + " > " + obj.CoursewareName.Substring(0, 20) + "...</a>";
					}
				}

				Grid1.DataSource = coursewares;
				Grid1.DataBind();
			}
		}

		private void DeleteData(int nCoursewareID)
		{
			CoursewareBLL coursewareBLL = new CoursewareBLL();

			coursewareBLL.DeleteCourseware(nCoursewareID);
		}

		private void DeleteFile(string srcPath)
		{
			string[] fileList = Directory.GetFileSystemEntries(srcPath);

			foreach (string file in fileList)
			{
				if (Directory.Exists(file))
				{
					DeleteFile(file);
				}
				else
				{
					File.Delete(file);
				}
			}
		}


		protected void btnQuery_Click(object sender, EventArgs e)
		{
			// int nID = int.Parse(Request.QueryString["id"]);


			string strKnowledgeID = Request.QueryString.Get("id");

			string[] str1 = strKnowledgeID.Split(new char[] { '/' });

			int nID = int.Parse(str1[str1.LongLength - 1].ToString());

			OrganizationBLL orgBll = new OrganizationBLL();
			int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);

			CoursewareBLL coursewareBLL = new CoursewareBLL();
			IList<RailExam.Model.Courseware> coursewares = new List<RailExam.Model.Courseware>();

			if (Request.QueryString.Get("type") == "Courseware")
			{
				coursewares = coursewareBLL.GetCoursewares(nID, -1, txtCoursewareName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
			}
			else if (Request.QueryString.Get("type") == "TrainType")
			{
				coursewares = coursewareBLL.GetCoursewares(-1, nID, txtCoursewareName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
			}
			else
			{
				coursewares = coursewareBLL.GetCoursewaresByPostID(nID, txtCoursewareName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
			}

			if (coursewares != null)
			{
				foreach (RailExam.Model.Courseware obj in coursewares)
				{
					if (obj.CoursewareName.Length <= 20)
					{
						obj.CoursewareName = "<a onclick=OpenIndex('" + obj.CoursewareID + "') href=# title=" + obj.CoursewareName + " > " + obj.CoursewareName + " </a>";
					}
					else
					{
						obj.CoursewareName = "<a onclick=OpenIndex('" + obj.CoursewareID + "') href=# title=" + obj.CoursewareName + " > " + obj.CoursewareName.Substring(0, 20) + "...</a>";
					}
				}

				Grid1.DataSource = coursewares;
				Grid1.DataBind();
			}
		}

		protected void Grid1_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Get("id") == "0" || Request.QueryString.Get("type") =="Post")
			{
				Grid1.Levels[0].Columns[1].Visible = false;
			}
		}
	}
}
