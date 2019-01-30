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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Main
{
    public partial class Admin_Top : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if(!IsPostBack)
			{
				string strStationID = ConfigurationManager.AppSettings["StationID"];

				if(strStationID == "200")
				{
					lblDate.Text = PrjPub.GetRailName() + "服务器";
				}
				else
				{
					OrganizationBLL orgBll = new OrganizationBLL();
					Organization organization = orgBll.GetOrganization(Convert.ToInt32(strStationID));
					lblDate.Text = organization.ShortName + "服务器";
				}
                string strSql = "select * from Computer_Server where Computer_Server_No='" + PrjPub.ServerNo + "'";
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblServerName.Text = ds.Tables[0].Rows[0]["Computer_Server_Name"].ToString();
                }

                lblServerNo.Text = PrjPub.ServerNo.ToString();
			}
        }
    }
}
