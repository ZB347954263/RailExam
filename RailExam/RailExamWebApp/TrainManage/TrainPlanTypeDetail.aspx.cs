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

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanTypeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString.Get("id");
                if (!String.IsNullOrEmpty(id))
                {
                    OracleAccess oa = new OracleAccess();
                    string sql = "select trainplan_type_name from ZJ_TRAINPLAN_TYPE t where trainplan_type_id = " + id;
                    DataSet ds = oa.RunSqlDataSet(sql);
                    if (ds != null && ds.Tables.Count == 1)
                    {
                        this.txtName.Text = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text.Trim();
            OracleAccess oa = new OracleAccess();
            string sql;
            string id = Request.QueryString.Get("id");
            if (String.IsNullOrEmpty(id))
            {
                sql = String.Format("insert into ZJ_TRAINPLAN_TYPE values({0},'{1}')", "train_plan_type_seq.NEXTVAL", name);

            }
            else
            {
                sql = String.Format("update ZJ_TRAINPLAN_TYPE set trainplan_type_name = '{0}' where trainplan_type_id = {1}", name, id);
            }
            try
            {
                oa.ExecuteNonQuery(sql);
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('编辑成功');opener.location.reload();close();", true);
            }
            catch
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('有异常输入，编辑不成功');", true);
            }
        }
    }
}
