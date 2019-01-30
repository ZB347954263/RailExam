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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp
{
	public partial class RefreshDataDefault : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//RefreshSnapShotBLL objBll = new RefreshSnapShotBLL();
				//objBll.RefreshOrg(1);

				OrganizationBLL objOrgBll = new OrganizationBLL();
				IList<RailExam.Model.Organization> objOrgList = objOrgBll.GetOrganizationsByLevel(2);

				foreach (Organization organization in objOrgList)
				{
					if (organization.OrganizationId != 1 && organization.OrganizationId != 32 && organization.OrganizationId != 200)
					{
						ListItem item = new ListItem();
						item.Text = organization.ShortName;
						item.Value = organization.OrganizationId.ToString();
						ddlOrg.Items.Add(item);
					}
				}
			}
		}

		protected void ImageButtonLogin_Click(object sender, ImageClickEventArgs e)
		{
			OrgConfigBLL orgConfigBLL = new OrgConfigBLL();
			OrgConfig orgConfig = orgConfigBLL.GetOrgConfig();
			if(ddlOrg.SelectedValue != orgConfig.OrgID.ToString())
			{
				SessionSet.PageMessage = "当前所选单位与数据库中标识单位不一致，请核对后重新选择！";
				return;
			}

			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.Load(Server.MapPath("web.config"));
			System.Xml.XmlNode node;
			node = doc.SelectSingleNode("//appSettings/add[@key='StationID']");
			node.Attributes["value"].Value = ddlOrg.SelectedValue;
			doc.Save(Server.MapPath("web.config"));   

			if (Request.QueryString.Get("Type") == "teacher")
			{
				Response.Redirect("LoginTeacher.aspx");
			}
			else
			{
				Response.Redirect("Login.aspx");
			}
		}
	}
}
