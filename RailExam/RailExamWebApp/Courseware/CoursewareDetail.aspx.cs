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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using System.IO;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Courseware
{
	public partial class CoursewareDetail : PageBase
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
				string strCoursewareID = Request.QueryString.Get("id");
				hfMode.Value = Request.QueryString.Get("mode");
				if (!string.IsNullOrEmpty(strCoursewareID))//Update
				{
					FillPage(int.Parse(strCoursewareID));
				}
				else//Add
				{
					datePublishDate.DateValue = DateTime.Now.ToString("yyyy-MM-dd");

					if (Request.QueryString.Get("coursewareTypeID")!=null)
					{
						if (!string.IsNullOrEmpty(Request.QueryString.Get("coursewareTypeID")))
						{
							hfCoursewareTypeID.Value = Request.QueryString.Get("coursewareTypeID");
							KnowledgeBLL objBll = new KnowledgeBLL();
							RailExam.Model.Knowledge obj = objBll.GetKnowledge(Convert.ToInt32(hfCoursewareTypeID.Value));
							txtCoursewareTypeName.Text = txtCoursewareTypeName.Text + GetKnowledgeName("/" + obj.KnowledgeName, obj.ParentId);
							ImgSelectCoursewareType.Visible = false;
						}
					}
					//string strId1 = Request.QueryString.Get("CoursewareTypeId");
					//if (! string.IsNullOrEmpty(strId1))
					//{
					//    CoursewareTypeBLL coursewareTypeBLL = new CoursewareTypeBLL();
					//    CoursewareType coursewareType = coursewareTypeBLL.GetCoursewareType(int.Parse(strId1));
					//    if (coursewareType != null)
					//    {
					//        txtCoursewareTypeName.Text = coursewareType.CoursewareTypeName;
					//    }
					//    hfCoursewareTypeID.Value = strId1;
					//}

					//string strTypeId1 = Request.QueryString.Get("TrainTypeId");
					//if (! string.IsNullOrEmpty(strTypeId1))
					//{
					//    TrainTypeBLL trainTypeBLL = new TrainTypeBLL();
					//    TrainType trainType = trainTypeBLL.GetTrainTypeInfo(int.Parse(strTypeId1));
					//    if (trainType != null)
					//    {
					//        txtTrainTypeName.Text = trainType.TypeName;
					//    }
					//    hfTrainTypeID.Value = strTypeId1;
					//}
					//txtProvideOrgName.Text = SessionSet.OrganizationName;
					ViewState["AddFlag"] = 1;

					ArrayList orgIDAL = new ArrayList();
					ArrayList postIDAL = new ArrayList();

					if (PrjPub.CurrentLoginUser.SuitRange == 0)
					{
						OrganizationBLL orgBll = new OrganizationBLL();
						txtProvideOrgName.Text = orgBll.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID).ShortName;
						hfProvideOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();

						IList<Organization> objOrganizationList =
							orgBll.GetOrganizationsByWhereClause("ID_Path || '/' like '/1/" + PrjPub.CurrentLoginUser.StationOrgID + "/%' ");
						foreach (Organization organization in objOrganizationList)
						{
							orgIDAL.Add(organization.OrganizationId);
						}
					}

					BindOrganizationTree(orgIDAL);
					BindPostTree(postIDAL);
				}
			}
		}

		private void FillPage(int nCoursewareID)
		{
			CoursewareBLL coursewareBLL = new CoursewareBLL();

			ArrayList trainTypeIDAL = new ArrayList();
			ArrayList orgIDAL = new ArrayList();
			ArrayList postIDAL = new ArrayList();

			RailExam.Model.Courseware courseware = coursewareBLL.GetCourseware(nCoursewareID);

			if (courseware != null)
			{
				txtCoursewareName.Text = courseware.CoursewareName;
				txtCoursewareTypeName.Text = courseware.CoursewareTypeNames;
				hfCoursewareTypeID.Value = courseware.CoursewareTypeID.ToString();

				txtTrainTypeName.Text = courseware.TrainTypeNames;

				txtProvideOrgName.Text = courseware.ProvideOrgName;
				hfProvideOrgID.Value = courseware.ProvideOrg.ToString();

				datePublishDate.DateValue = courseware.PublishDate.ToString("yyyy-MM-dd");
				txtAuthors.Text = courseware.Authors;
				txtRevisers.Text = courseware.Revisers;
				txtKeyWord.Text = courseware.KeyWord;
				txtDescription.Text = courseware.Description;
				string strUrl = string.Empty;
				strUrl = "ViewCourseware.aspx?id=" + courseware.CoursewareID;
				hlUrl.Text = strUrl;
				hlUrl.NavigateUrl = strUrl;
				ddlIsGroup.SelectedValue = courseware.IsGroupLearder.ToString();
				ddlTech.SelectedValue = courseware.TechnicianTypeID.ToString();
				hfOrderIndex.Value = courseware.OrderIndex.ToString();

				txtMemo.Text = courseware.Memo;

				trainTypeIDAL = courseware.TrainTypeIDAL;
				orgIDAL = courseware.OrgIDAL;
				postIDAL = courseware.PostIDAL;
			}
			//tvOrg.Nodes.Clear();
			//tvPost.Nodes.Clear();

			BindOrganizationTree(orgIDAL);
			BindPostTree(postIDAL);

			string strUpdate = PrjPub.HasEditRight("课件管理").ToString();
			string strOrgID = PrjPub.CurrentLoginUser.OrgID.ToString();

			if (courseware != null)
			{
				//if (strUpdate == "True" && strOrgID == courseware.ProvideOrg.ToString())
				if (strUpdate == "True" && PrjPub.CurrentLoginUser.SuitRange == 1)
				{
					SaveButton.Visible = true;
					CancelButton.Visible = false;
				}
				else
				{
					SaveButton.Visible = false;
					CancelButton.Visible = true;
				}
			}

			string strMode = Request.QueryString.Get("mode");
			if (strMode == "ReadOnly")
			{
				SaveButton.Visible = false;
				CancelButton.Visible = true;

				//隐藏图片

				txtCoursewareName.ReadOnly = true;
				txtAuthors.ReadOnly = true;
				txtRevisers.ReadOnly = true;
				txtKeyWord.ReadOnly = true;
				txtDescription.ReadOnly = true;
				txtMemo.ReadOnly = true;
			}
			else
			{
				SaveButton.Visible = true;
				CancelButton.Visible = false;

				for (int i = 0; i < trainTypeIDAL.Count; i++)
				{
					if (i == 0)
					{
						hfTrainTypeID.Value = trainTypeIDAL[0].ToString();
					}
					else
					{
						hfTrainTypeID.Value += "," + trainTypeIDAL[i].ToString();
					}
				}
			}

			ViewState["AddFlag"] = 0;
		}

		private void BindOrganizationTree(ArrayList orgidAL)
		{
			OrganizationBLL organizationBLL = new OrganizationBLL();
			IList<Organization> organizationList = new List<Organization>();
			if (PrjPub.CurrentLoginUser.SuitRange == 1)
			{
				organizationList = organizationBLL.GetOrganizations();
			}
			else
			{
				organizationList =
									organizationBLL.GetOrganizations(PrjPub.CurrentLoginUser.StationOrgID);
			}

			if (organizationList.Count > 0)
			{
				TreeViewNode tvn = null;

				foreach (Organization organization in organizationList)
				{
					tvn = new TreeViewNode();
					tvn.ID = organization.OrganizationId.ToString();
					tvn.Value = organization.OrganizationId.ToString();
					tvn.Text = organization.ShortName;
					tvn.ToolTip = organization.FullName;
					tvn.ShowCheckBox = true;

					if (orgidAL.Count > 0)
					{
						if (orgidAL.IndexOf(organization.OrganizationId) != -1)
						{
							tvn.Checked = true;
						}
					}


					if (PrjPub.CurrentLoginUser.SuitRange == 1)
					{
						if (organization.ParentId == 0)
						{
							tvOrg.Nodes.Add(tvn);
						}
						else
						{
							try
							{
								tvOrg.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
							}
							catch
							{
								tvOrg.Nodes.Clear();
								SessionSet.PageMessage = "数据错误！";
								return;
							}
						}
					}
					else
					{
						if (organization.ParentId == 1)
						{
							tvOrg.Nodes.Add(tvn);
						}
						else
						{
							try
							{
								tvOrg.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
							}
							catch
							{
								tvOrg.Nodes.Clear();
								SessionSet.PageMessage = "数据错误！";
								return;
							}
						}
					}
				}
			}

			tvOrg.DataBind();

			if (tvOrg.Nodes.Count > 0)
			{
				tvOrg.Nodes[0].Expanded = true;
			}
		}

		private void BindPostTree(ArrayList postidAL)
		{
			PostBLL PostBLL = new PostBLL();
           // IList<RailExam.Model.Post> postsList = PostBLL.GetPosts(0, 0, 0, "", 0, "", "", 0, 0, "","", 0, 200, "");
			IList<RailExam.Model.Post> postsList = PostBLL.GetPosts();
			if (postsList.Count > 0)
			{
				TreeViewNode tvn = null;

				foreach (Post post in postsList)
				{
					tvn = new TreeViewNode();
					tvn.ID = post.PostId.ToString();
					tvn.Value = post.PostId.ToString();
					tvn.Text = post.PostName;
					tvn.ToolTip = post.PostName;
					tvn.ShowCheckBox = true;

					if (postidAL.Count > 0)
					{
						if (postidAL.IndexOf(post.PostId) != -1)
						{
							tvn.Checked = true;
						}
					}

					if (post.ParentId == 0)
					{
						tvPost.Nodes.Add(tvn);
					}
					else
					{
						try
						{
							tvPost.FindNodeById(post.ParentId.ToString()).Nodes.Add(tvn);
						}
						catch
						{
							tvPost.Nodes.Clear();
							SessionSet.PageMessage = "数据错误！";
							return;
						}
					}
				}
			}

			tvPost.DataBind();
			//tvPost.ExpandAll();
		}

		protected void SaveButton_Click(object sender, EventArgs e)
		{
			string strFullName = string.Empty;//, strUrl, strContentType, strSize

			CoursewareBLL coursewareBLL = new CoursewareBLL();


			if (ViewState["AddFlag"].ToString() == "1")     //新增
			{
				strFullName = File1.FileName;//直接取得文件名



				if (!string.IsNullOrEmpty(strFullName))
				{
					FileInfo fi = new FileInfo(File1.FileName);

					//判断只能上传.flv与.swf格式

                    //if (fi.Extension.ToLower() != ".flv" && fi.Extension.ToLower() != ".swf")
                    //{
                    //    SessionSet.PageMessage = "文件格式应为FLV或SWF视频! 现在格式为：" + fi.Extension.ToLower();
                    //    return;
                    //}

					if (fi.Name.Replace(fi.Extension, "") != txtCoursewareName.Text)
					{
						SessionSet.PageMessage = "上传文件名与课件名不一致，请确认上传文件名！";
						return;
					}
				}
				RailExam.Model.Courseware courseware = new RailExam.Model.Courseware();

				courseware.CoursewareName = txtCoursewareName.Text;
				courseware.CoursewareTypeID = int.Parse(hfCoursewareTypeID.Value);
				courseware.ProvideOrg = SessionSet.OrganizationID;
				courseware.PublishDate = DateTime.Parse(datePublishDate.DateValue.ToString());
				courseware.Authors = txtAuthors.Text;
				courseware.Revisers = txtRevisers.Text;
				courseware.KeyWord = txtKeyWord.Text;
				courseware.Description = txtDescription.Text;
				courseware.Memo = txtMemo.Text;
				courseware.IsGroupLearder = Convert.ToInt32(ddlIsGroup.SelectedValue);
				courseware.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);

				ArrayList al = new ArrayList();
				courseware.OrgIDAL = GetOrg(tvOrg.Nodes, al);

				ArrayList al1 = new ArrayList();

				foreach (TreeViewNode tn in tvPost.Nodes)
				{
					if (tn.Checked)
					{
						al1.Add(tn.ID);
					}

					if (tn.Nodes.Count > 0)
					{
						foreach (TreeViewNode tns in tn.Nodes)
						{
							if (tns.Checked)
							{
								al1.Add(tns.ID);
							}

							if (tns.Nodes.Count > 0)
							{
								foreach (TreeViewNode tnss in tns.Nodes)
								{
									if (tnss.Checked)
									{
										al1.Add(tnss.ID);
									}
								}
							}
						}
					}
				}
				courseware.PostIDAL = al1;

				ArrayList trainTypeIDList = new ArrayList();
				string[] strType = hfTrainTypeID.Value.Split(',');
				for (int i = 0; i < strType.Length; i++)
				{
                    if(!string.IsNullOrEmpty(strType[i]))
                    {
                        trainTypeIDList.Add(strType[i]);
                    }
				}
				courseware.TrainTypeIDAL = trainTypeIDList;

				int nCoursewareID = coursewareBLL.AddCourseware(courseware);

				if (!string.IsNullOrEmpty(strFullName))
				{
					//strUrl = File1.PostedFile.FileName;//先取得全部的上传文件路径个名字，然后再利用SubString方法来得到用户名，现在看来是没有必要了


					//strContentType = File1.PostedFile.ContentType;//获取文件MIME内容类型
					//strFileType = strFullName.Substring(strFullName.LastIndexOf(".") + 1);//获取文件名字 . 后面的字符作为文件类型


					//strSize = File1.PostedFile.ContentLength.ToString();


					Directory.CreateDirectory(Server.MapPath("../Online/Courseware/" + nCoursewareID));
					File1.SaveAs(Server.MapPath("../Online/Courseware/" + nCoursewareID + "/") + strFullName);//将文件保存在跟目录的UP文件夹下

					courseware.CoursewareID = nCoursewareID;
					courseware.Url = "/RailExamBao/Online/Courseware/" + nCoursewareID + "/" + strFullName;

					coursewareBLL.UpdateCourseware(courseware);
				}
				Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
			}
			else    //修改
			{
				string strCoursewareID = Request.QueryString.Get("id");

				RailExam.Model.Courseware courseware = coursewareBLL.GetCourseware(Convert.ToInt32(strCoursewareID));
				courseware.CoursewareID = int.Parse(strCoursewareID);
				courseware.CoursewareName = txtCoursewareName.Text;
				courseware.CoursewareTypeID = int.Parse(hfCoursewareTypeID.Value);
				courseware.ProvideOrg = int.Parse(hfProvideOrgID.Value);
				courseware.PublishDate = DateTime.Parse(datePublishDate.DateValue.ToString());
				courseware.Authors = txtAuthors.Text;
				courseware.Revisers = txtRevisers.Text;
				courseware.KeyWord = txtKeyWord.Text;
				courseware.Description = txtDescription.Text;
				courseware.IsGroupLearder = Convert.ToInt32(ddlIsGroup.SelectedValue);
				courseware.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
				courseware.OrderIndex = Convert.ToInt32(hfOrderIndex.Value);

				strFullName = File1.FileName;//直接取得文件名



				//strUrl = File1.PostedFile.FileName;//先取得全部的上传文件路径个名字，然后再利用SubString方法来得到用户名，现在看来是没有必要了


				//strContentType = File1.PostedFile.ContentType;//获取文件MIME内容类型
				//strFileType = strFullName.Substring(strFullName.LastIndexOf(".") + 1);//获取文件名字 . 后面的字符作为文件类型


				//strSize = File1.PostedFile.ContentLength.ToString();

				if (!string.IsNullOrEmpty(strFullName))
				{
					FileInfo fi = new FileInfo(File1.FileName);

					//如果是武汉，就判断只能上传.flv与.swf格式

                    //if (fi.Extension.ToLower() != ".flv" && fi.Extension.ToLower() != ".swf")
                    //{
                    //    SessionSet.PageMessage = "文件格式应为FLV或SWF视频! 现在格式为：" + fi.Extension.ToLower();
                    //    return;
                    //}

					if (fi.Name.Replace(fi.Extension, "") != txtCoursewareName.Text)
					{
						SessionSet.PageMessage = "上传文件名与课件名不一致，请确认上传文件名！";
						return;
					}

					if (File.Exists(Server.MapPath(courseware.Url)))
					{
						//string[] filelist = Directory.GetFileSystemEntries(Server.MapPath("../Online/Courseware/" + courseware.CoursewareID + "/"));
						//foreach (string file in filelist)
						//{
						//    if(!Directory.Exists(file))
						//    {
						//        File.Delete(file);
						//    }
						//} 
						File.Delete(Server.MapPath(courseware.Url));
					}

					if (!Directory.Exists(Server.MapPath("../Online/Courseware/" + strCoursewareID)))
					{
						Directory.CreateDirectory(Server.MapPath("../Online/Courseware/" + strCoursewareID));
					}

					File1.SaveAs(Server.MapPath("../Online/Courseware/" + strCoursewareID + "/") + strFullName);//将文件保存在跟目录的UP文件夹下

					courseware.Url = "/RailExamBao/Online/Courseware/" + strCoursewareID + "/" + strFullName;
				}
				courseware.Memo = txtMemo.Text;

				ArrayList al = new ArrayList();
				courseware.OrgIDAL = GetOrg(tvOrg.Nodes, al);

				ArrayList al1 = new ArrayList();

				foreach (TreeViewNode tn in tvPost.Nodes)
				{
					if (tn.Checked)
					{
						al1.Add(tn.ID);
					}

					if (tn.Nodes.Count > 0)
					{
						foreach (TreeViewNode tns in tn.Nodes)
						{
							if (tns.Checked)
							{
								al1.Add(tns.ID);
							}

							if (tns.Nodes.Count > 0)
							{
								foreach (TreeViewNode tnss in tns.Nodes)
								{
									if (tnss.Checked)
									{
										al1.Add(tnss.ID);
									}
								}
							}
						}
					}
				}
				courseware.PostIDAL = al1;

				ArrayList trainTypeIDList = new ArrayList();
				string[] strType = hfTrainTypeID.Value.Split(',');
				for (int i = 0; i < strType.Length; i++)
				{
                    if (!string.IsNullOrEmpty(strType[i]))
                    {
                        trainTypeIDList.Add(strType[i]);
                    }
                }
				courseware.TrainTypeIDAL = trainTypeIDList;

				coursewareBLL.UpdateCourseware(courseware);

				Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
			}

		}

		private ArrayList GetOrg(TreeViewNodeCollection nodes, ArrayList al)
		{
			foreach (TreeViewNode tn in nodes)
			{
				if (tn.Checked)
				{
					al.Add(tn.ID);
				}

				if (tn.Nodes.Count > 0)
				{
					GetOrg(tn.Nodes, al);
				}
			}
			return al;
		}

		private string GetKnowledgeName(string strName, int nID)
		{
			string str = strName;

			if (nID != 0)
			{

				KnowledgeBLL objBll = new KnowledgeBLL();
				RailExam.Model.Knowledge obj = objBll.GetKnowledge(nID);

				if (obj.ParentId != 0)
				{
					str = GetKnowledgeName("/" + obj.KnowledgeName, obj.ParentId) + strName;
				}
				else
				{
					str = obj.KnowledgeName + strName;
				}
			}

			return str;
		}
	}
}
