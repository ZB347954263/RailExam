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

namespace RailExamWebApp.Main
{
    public partial class ShowVersionInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strSql = "select *  from System_Version_Info order by Version desc";
                OracleAccess db = new OracleAccess();
                DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    ListItem item = new ListItem();
                    item.Value = dr["Version"].ToString();
                    item.Text = Convert.ToDecimal(dr["Version"].ToString()).ToString("0.0");
                    ddlVersion.Items.Add(item);
                }

                if(dt.Rows.Count>0)
                {
                    ddlVersion.SelectedIndex = 0;
                    ddlVersion_SelectedIndexChanged(null, null);
                }
            }
        }

        protected  void ddlVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSql = "select *  from System_Version_Info where Version="+ddlVersion.SelectedValue;
            OracleAccess db = new OracleAccess();
            DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

            lblDate.Text = "更新时间为：" + dt.Rows[0]["UpdateDate"];
            lblVersionInfo.InnerHtml = "<span style='word-break:break-all;font-weight: bold;'>" + dt.Rows[0]["VersionInfo"].ToString().Replace("|", "<br>") + "</span><br>";
        }
    }
}
