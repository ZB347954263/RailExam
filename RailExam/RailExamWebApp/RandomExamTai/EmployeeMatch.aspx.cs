using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
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
    public partial class EmployeeMatch : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfID.Value = Request.QueryString.Get("ID");
                BindGrid();

                string type = Request.QueryString.Get("Type");
                if (type == "0" || !PrjPub.IsServerCenter)
                {
                    int columnsCount = this.grdEntity.Levels[0].Columns.Count;
                    this.grdEntity.Levels[0].Columns[columnsCount - 1].Visible = false;
                }

                hfIsServerCenter.Value = PrjPub.IsServerCenter.ToString();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string mode = Request.Form["__EVENTARGUMENT"];
            if (mode != "ref")
                DeleteInfo();
            BindGrid();
        }
        private void BindGrid()
        {
            grdEntity.DataSource = GetInfo();
            grdEntity.DataBind();
        }
        private DataTable GetInfo()
        {

            DataTable dt = new DataTable();
            OracleAccess access = new OracleAccess();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from zj_employee_match ");
            sql.AppendFormat(" where employee_id={0} ", Convert.ToInt32(Request.QueryString.Get("ID")));
            dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
            dt.Columns.Add("match_date1", typeof(string));
            dt.Columns.Add("create_date1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r["match_date"].ToString() != "")
                        r["match_date1"] = Convert.ToDateTime(r["match_date"].ToString()).ToString("yyyy-MM-dd");
                    r["create_date1"] = Convert.ToDateTime(r["create_date"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            return dt;
        }
        private void DeleteInfo()
        {
            try
            {
                string id = Request.Form["__EVENTARGUMENT"];
                string sql = string.Format("delete from zj_employee_match where employee_match_id={0}", Convert.ToInt32(id));
                OracleAccess access = new OracleAccess();
                access.ExecuteNonQuery(sql);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据删除成功！')", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据删除失败！')", true);
            }
        }
    }
}
