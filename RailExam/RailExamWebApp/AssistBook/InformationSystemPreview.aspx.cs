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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public partial class InformationSystemPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Convert.ToString(Request.QueryString["id"]);

                OracleAccess ora = new OracleAccess();

                string sql = String.Format(
                    "select * from INFORMATION_SYSTEM t"
                    + " where information_system_id = {0}",
                    id);

                DataSet ds = ora.RunSqlDataSet(sql);
                if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    ltrlLevelName.Text = Convert.ToString(row["information_system_name"]);
                    ltrlDescription.Text = String.IsNullOrEmpty(Convert.ToString(row["description"])) ? "(��������)" : Convert.ToString(row["description"]);
                    ltrlMemo.Text = String.IsNullOrEmpty(Convert.ToString(row["memo"])) ? "(���ޱ�ע)" : Convert.ToString(row["memo"]);
                }
            }
        }
    }
}
