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
    public partial class EmployeeMatchEdit : System.Web.UI.Page
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
                sql.Append("insert into zj_employee_match values");
                sql.Append("(employee_match_seq.nextval ,{0},to_date('{1}','yyyy-mm-dd hh24:mi:ss'),'{2}','{3}',");
                sql.Append("'{4}',{5},{6},{7},{8},'{9}',to_date('{10}','yyyy-mm-dd hh24:mi:ss'),'{11}')");
                string inssql = string.Format(
                    sql.ToString(), Convert.ToInt32(Request.QueryString.Get("id")), match_date.DateValue.ToString(),
                    dropunit.SelectedItem.Text, txtmatch_project.Text, dropmatch_type.SelectedItem.Text,
                    Convert.ToDouble(txttotal_score.Text),
                    txtlilun_score.Text.Trim() == "" ? 0 : Convert.ToDouble(txtlilun_score.Text),
                    txtshizuo_score.Text.Trim() == "" ? 0 : Convert.ToDouble(txtshizuo_score.Text),
                    Convert.ToInt32(txtmatch_rank.Text), txtmemo.Text,
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"),
                    PrjPub.CurrentLoginUser.EmployeeName);
                 access.ExecuteNonQuery(inssql);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据输入有误！');", true);
            }

        }
        private void EditInfo()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                StringBuilder sql = new StringBuilder();
                sql.Append(" update zj_employee_match set ");
                sql.Append(" match_date=to_date('{0}','yyyy-mm-dd hh24:mi:ss'),");
                sql.Append(" unit='{1}',match_project='{2}',match_type='{3}',");
                sql.Append(" total_score={4},lilun_score={5},shizuo_score={6},");
                sql.Append(" match_rank={7},memo='{8}',create_date=to_date('{9}','yyyy-mm-dd hh24:mi:ss'),");
                sql.Append(" create_person='{10}' where employee_match_id={11}");
                string sqlEdit = string.Format(sql.ToString(), match_date.DateValue.ToString(),
                                               dropunit.SelectedItem.Text, txtmatch_project.Text,
                                               dropmatch_type.SelectedItem.Text,
                                               Convert.ToDouble(txttotal_score.Text),
                                               txtlilun_score.Text.Trim() == ""
                                                   ? 0
                                                   : Convert.ToDouble(txtlilun_score.Text),
                                               txtshizuo_score.Text.Trim() == ""
                                                   ? 0
                                                   : Convert.ToDouble(txtshizuo_score.Text),
                                               Convert.ToInt32(txtmatch_rank.Text), txtmemo.Text,
                                               Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString(
                                                   "yyyy-MM-dd"),
                                               PrjPub.CurrentLoginUser.EmployeeName,
                                               Convert.ToInt32(Request.QueryString.Get("id")));
                access.ExecuteNonQuery(sqlEdit);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据输入有误！');", true);
            }
        }
        private void BindInfo()
        {
            OracleAccess access = new OracleAccess();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from zj_employee_match ");
            sql.AppendFormat(" where employee_match_id={0} ", Convert.ToInt32(Request.QueryString.Get("ID")));
            DataTable dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["match_date"].ToString() != "")
                    match_date.DateValue = Convert.ToDateTime(dt.Rows[0]["match_date"]).ToString("yyyy-MM-dd");
                else
                    match_date.DateValue = "";

                dropunit.SelectedValue = dt.Rows[0]["unit"].ToString();
                dropmatch_type.SelectedValue = dt.Rows[0]["match_type"].ToString();
                txtmatch_project.Text = dt.Rows[0]["match_project"].ToString();
                txttotal_score.Text = dt.Rows[0]["total_score"].ToString();
                txtlilun_score.Text = dt.Rows[0]["lilun_score"].ToString();
                txtshizuo_score.Text = dt.Rows[0]["shizuo_score"].ToString();
                txtmatch_rank.Text = dt.Rows[0]["match_rank"].ToString();
                txtmemo.Text = dt.Rows[0]["memo"].ToString();
            }
            this.Title = "修改技能竞赛";
            current.InnerText = "修改技能竞赛";
        }
    }
}
