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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeInfoOther : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.HasEditRight("职员管理") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("职员管理") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

                string strSql = @"select b.Employee_ID as EmployeeID,b.Employee_Name as EmployeeName,b.Work_No as WorkNo,
                                    GetOrgName(b.Org_ID) as OrgName,d.Post_Name as PostName,c.Role_Name as RoleName
                                    from system_user a 
                                    inner join employee b on a.employee_id=b.employee_id 
                                    inner join system_role c on a.Role_ID=c.Role_ID 
                                    inner join Post d on b.Post_ID=d.Post_ID
                                    where GetStationOrgID(b.Org_ID)=" + Request.QueryString.Get("id") + " and a.Role_ID>0 order by a.Role_ID";
                OracleAccess oa = new OracleAccess();
                DataSet ds = oa.RunSqlDataSet(strSql);

                Grid1.DataSource = ds;
                Grid1.DataBind();
            }
        }
    }
}

