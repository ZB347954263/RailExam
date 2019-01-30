using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using BookBLL=RailExam.BLL.BookBLL;
using BookChapterBLL=RailExam.BLL.BookChapterBLL;
using KnowledgeBLL=RailExam.BLL.KnowledgeBLL;
using OrganizationBLL=RailExam.BLL.OrganizationBLL;
using RailExam.BLL;

namespace RailExamWebApp.Item
{
	public partial class ItemBookDetail : PageBase
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

				string strKnowledgeID = Request.QueryString.Get("knowledgeId");
				if (strKnowledgeID != null && strKnowledgeID != string.Empty)
				{
					hfKnowledgeID.Value = strKnowledgeID;
					KnowledgeBLL objBll = new KnowledgeBLL();
					RailExam.Model.Knowledge obj = objBll.GetKnowledge(Convert.ToInt32(strKnowledgeID));
					txtKnowledgeName.Text = txtKnowledgeName.Text + GetKnowledgeName("/" + obj.KnowledgeName, obj.ParentId);
					ImgSelectKnowledge.Visible = false;
				}

				ArrayList objOrgList = new ArrayList();
				if (PrjPub.CurrentLoginUser.SuitRange == 0)
				{
					OrganizationBLL orgBll = new OrganizationBLL();
					txtPublishOrgName.Text = orgBll.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID).ShortName;
					hfPublishOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();

					IList<Organization> objOrganizationList =
						orgBll.GetOrganizationsByWhereClause("ID_Path || '/' like '/1/" + PrjPub.CurrentLoginUser.StationOrgID + "/%' ");
					foreach (Organization organization in objOrganizationList)
					{
						objOrgList.Add(organization.OrganizationId);
					}
				}

				//txtPublishOrgName.Text = PrjPub.CurrentLoginUser.OrgName;
				//hfPublishOrgID.Value = PrjPub.CurrentLoginUser.OrgID.ToString();
				BindOrganizationTree(objOrgList);

				ArrayList objList = new ArrayList();
				if (!string.IsNullOrEmpty(Request.QueryString.Get("PostId")))
				{
					objList.Add(Convert.ToInt32(Request.QueryString.Get("PostId")));
				}
				BindPostTree(objList);
			}
		}

		private string GetKnowledgeName(string strName, int nID)
		{
			string str = "";

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
			//tvOrg.ExpandAll();
			if (tvOrg.Nodes.Count > 0)
			{
				tvOrg.Nodes[0].Expanded = true;
			}
		}

		private void BindPostTree(ArrayList postidAL)
		{
			PostBLL PostBLL = new PostBLL();
			IList<Post> postsList = PostBLL.GetPosts();

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

		private void CopyTemplate(string srcPath, string aimPath)
		{
			if (!Directory.Exists(aimPath))
			{
				Directory.CreateDirectory(aimPath);
			}

			string[] fileList = Directory.GetFileSystemEntries(srcPath);

			foreach (string file in fileList)
			{
				if (Directory.Exists(file))
				{
					CopyTemplate(file, aimPath + Path.GetFileName(file) + "\\");
				}
				else
				{
					File.Copy(file, aimPath + Path.GetFileName(file), true);
				}
			}
		}

		private int SaveNewBook()
		{
			BookBLL kBLL = new BookBLL();


			RailExam.Model.Book book = new RailExam.Model.Book();

			book.bookName = txtBookName.Text;
			book.knowledgeName = txtKnowledgeName.Text;
			book.knowledgeId = int.Parse(hfKnowledgeID.Value);
			book.publishOrg = int.Parse(hfPublishOrgID.Value);
			book.publishDate = DateTime.Today;
			book.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
			book.IsGroupLearder = Convert.ToInt32(ddlIsGroup.SelectedValue);

			ArrayList alTrainTypeID = new ArrayList();

			string strTrainTypeID = hfTrainTypeID.Value;

			string[] str1 = strTrainTypeID.Split(new char[] { ',' });

			for (int i = 0; i < str1.Length; i++)
			{
				alTrainTypeID.Add(str1[i]);
			}

			book.trainTypeidAL = alTrainTypeID;

			ArrayList al = new ArrayList();
			book.orgidAL = GetOrg(tvOrg.Nodes, al);


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


			book.postidAL = al1;

			string strNewID = kBLL.AddBook(book).ToString();

			string strPath = Server.MapPath("../Online/Book/" + strNewID);
			Directory.CreateDirectory(strPath);
			Directory.CreateDirectory(strPath + "/Upload");
			CopyTemplate(Server.MapPath("../Online/Book/template/"), Server.MapPath("../Online/Book/" + strNewID + "/"));

			SaveBookCover(strNewID);

			return Convert.ToInt32(strNewID);
		}

		protected void SaveNextButton_Click(object sender, EventArgs e)
		{
			int newID = SaveNewBook();
			ClientScript.RegisterStartupScript(GetType(), "1", "<script>SaveNext(" + newID + ");</script>", false);
		}

		private void SaveBookCover(string bookid)
		{
			string strBookUrl = "../Book/" + bookid + "/cover.htm";
			BookBLL objBill = new BookBLL();
			objBill.UpdateBookUrl(Convert.ToInt32(bookid), strBookUrl);

			string srcPath = "../Online/Book/" + bookid + "/cover.htm";

			RailExam.Model.Book obj = objBill.GetBook(Convert.ToInt32(bookid));

			if (File.Exists(Server.MapPath(srcPath)))
			{
				File.Delete(Server.MapPath(srcPath));
			}

			string str = "<link href='book.css' type='text/css' rel='stylesheet' />"
						 + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
						 + "<br><br><br><br><br><br><br>"
						 + "<div id='booktitle'>" + obj.bookName + "</div>" + "<br>"
						 + "<br><br><br><br><br><br><br><br><br><br><br>"
						 + "<div id='orgtitle'>" + obj.publishOrgName + "</div>" + "<br>"
						 + "<div id='authortitle'>" + obj.authors + "</div>"
						 + "</body>";

			File.AppendAllText(Server.MapPath(srcPath), str, System.Text.Encoding.UTF8);

			BookChapterBLL objChapterBll = new BookChapterBLL();
			objChapterBll.GetIndex(bookid);
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
	}
}
