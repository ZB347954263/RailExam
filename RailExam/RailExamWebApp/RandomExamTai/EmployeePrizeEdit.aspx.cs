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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeePrizeEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                if (Request.QueryString.Get("mode").Equals("edit"))
                    BindInfo();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("mode").Equals("edit"))
                EditInfo();
            else
                AddInfo();
        }
        private void AddInfo()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                StringBuilder sql=new StringBuilder();
                sql.Append("insert into zj_employee_prize values ( employee_prize_seq.nextval,  {0},");
                sql.Append("to_date('{1}','yyyy-mm-dd hh24:mi:ss'),{2},'{3}','{4}','{5}',");
                sql.Append("to_date('{6}','yyyy-mm-dd hh24:mi:ss'),'{7}','{8}')");
                string inssql = string.Format(sql.ToString(),
                                              Convert.ToInt32(Request.QueryString.Get("id")),
                                              prize_date.DateValue.ToString(),
                                              Convert.ToInt32(dropprize_type.SelectedValue),
                                              txtprize_no.Text, txtcontent_brief.Text, txtprize_result.Text,
                                              Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"),
                                              PrjPub.CurrentLoginUser.EmployeeName, txtmemo.Text);
                access.ExecuteNonQuery(inssql);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！');", true);
            }
        }
        private void EditInfo()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                StringBuilder sql = new StringBuilder();
                sql.Append("update zj_employee_prize set   prize_date=to_date('{0}','yyyy-mm-dd hh24:mi:ss'),");
                sql.Append("  prize_type={1},prize_no='{2}',content_brief='{3}',prize_result='{4}',");
                sql.Append("create_date=to_date('{5}','yyyy-mm-dd hh24:mi:ss'),  create_person='{6}',memo='{7}' where employee_prize_id={8}");
                string sqlEdit = string.Format(sql.ToString(), prize_date.DateValue.ToString(),
                                               Convert.ToInt32(dropprize_type.SelectedValue),
                                               txtprize_no.Text, txtcontent_brief.Text, txtprize_result.Text,
                                               Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString(
                                                   "yyyy-MM-dd"),
                                               PrjPub.CurrentLoginUser.EmployeeName, txtmemo.Text,
                                               Convert.ToInt32(Request.QueryString.Get("id")));
                access.ExecuteNonQuery(sqlEdit);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据更新失败！');", true);
            }
        }
        private void BindInfo()
        {
            OracleAccess access = new OracleAccess();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from zj_employee_prize ");
            sql.AppendFormat(" where employee_prize_id={0} ", Convert.ToInt32(Request.QueryString.Get("ID")));
            DataTable dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["prize_date"].ToString() != "")
                    prize_date.DateValue = Convert.ToDateTime(dt.Rows[0]["prize_date"]).ToString("yyyy-MM-dd");
                dropprize_type.SelectedValue = dt.Rows[0]["prize_type"].ToString();
                txtprize_no.Text = dt.Rows[0]["prize_no"].ToString();
                txtcontent_brief.Text = dt.Rows[0]["content_brief"].ToString();
                txtprize_result.Text = dt.Rows[0]["prize_result"].ToString();
                txtmemo.Text = dt.Rows[0]["memo"].ToString();
            }
            this.Title = "修改奖惩情况";
            current.InnerText = "修改奖惩情况";
        }
    }
}
