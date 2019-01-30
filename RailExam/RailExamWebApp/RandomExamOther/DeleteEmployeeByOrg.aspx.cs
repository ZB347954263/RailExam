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

namespace RailExamWebApp.RandomExamOther
{
    public partial class DeleteEmployeeByOrg : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                OrganizationBLL objOrgBll = new OrganizationBLL();
                IList<RailExam.Model.Organization> objOrgList = objOrgBll.GetOrganizationsByLevel(2);

                ListItem item = new ListItem();
                item.Text = "--«Î—°‘Ò--";
                item.Value = "0";
                ddlOrg.Items.Add(item);

                foreach (Organization organization in objOrgList)
                {
                    if (organization.OrganizationId != 1 && organization.OrganizationId != 200)
                    {
                        item = new ListItem();
                        item.Text = organization.ShortName;
                        item.Value = organization.OrganizationId.ToString();
                        ddlOrg.Items.Add(item);
                    }
                }
            }
        }
    }
}
