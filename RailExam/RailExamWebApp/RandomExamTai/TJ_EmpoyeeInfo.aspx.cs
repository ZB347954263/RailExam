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
    public partial class TJ_EmpoyeeInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int railSystemId = PrjPub.RailSystemId();

                string str = "";
                if(railSystemId !=0)
                {
                    str = " and (Rail_System_ID= " + railSystemId +" or org_id="+PrjPub.CurrentLoginUser.StationOrgID+")";
                }
                string strSql = "select * from org where level_num=2 "+ str +" order by parent_id,order_index";
                
                ListItem item = new ListItem();
                item.Text = "--«Î—°‘Ò--";
                item.Value = "0";
                ddlOrg.Items.Add(item);

                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Short_Name"].ToString();
                    item.Value = dr["Org_ID"].ToString();
                    ddlOrg.Items.Add(item);
                }

                if (PrjPub.CurrentLoginUser.SuitRange == 0)
                {
                    ddlOrg.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                    ddlOrg.Enabled = false;
                }

                employeeList.Attributes.Add("onchange", "employeeList_onChange()");
            }
        }
    }
}
