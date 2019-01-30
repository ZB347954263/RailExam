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
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class SelectBookOrg : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindItemCategoryTree();
			}
		}

		private void BindItemCategoryTree()
		{
			OrganizationBLL organizationsBLL = new OrganizationBLL();

			IList<RailExam.Model.Organization> organizations = organizationsBLL.GetOrganizationsByLevel(2);
			IList<RailExam.Model.Organization> organizationsnew = new List<Organization>();
			foreach (Organization organization in organizations)
			{
				if(organization.LevelNum == 2)
				{
					organizationsnew.Add(organization);
				}
			}

			if (organizationsnew.Count > 0)
			{
				TreeViewNode tvn = null;

				foreach (Organization organization in organizationsnew)
				{
					tvn = new TreeViewNode();
					tvn.ID = organization.OrganizationId.ToString();
					tvn.Value = organization.OrganizationId.ToString();
					tvn.Text = organization.ShortName;
					tvn.ToolTip = organization.FullName;

					tvOrganization.Nodes.Add(tvn);
				}
			}

			tvOrganization.DataBind();


			if (tvOrganization.Nodes.Count > 0)
			{
				tvOrganization.Nodes[0].Expanded = true;
			}
		}


		protected void btnOk_Click(object sender, EventArgs e)
		{
			int selectOrgID = Convert.ToInt32(hfOrg.Value);
			int bookID = Convert.ToInt32(Request.QueryString.Get("BookID"));

			ArrayList objOrgList = new ArrayList();
			OrganizationBLL orgBll = new OrganizationBLL();
			IList<Organization> objOrg = orgBll.GetOrganizationsByWhereClause("id_path || '/' like '/1/" + selectOrgID + "/%'");
			foreach (Organization organization in objOrg)
			{
				objOrgList.Add(organization.OrganizationId);
			}
			
			BookBLL objBookBll = new BookBLL();
			RailExam.Model.Book objBook = objBookBll.GetBook(bookID);
			objBook.publishOrg = selectOrgID;
			objBook.orgidAL = objOrgList;
			objBookBll.UpdateBook(objBook);

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}
	}
}
