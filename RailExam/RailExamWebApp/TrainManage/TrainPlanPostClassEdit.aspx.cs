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

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanClassPostEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString.Get("mode").Equals("add"))
                    BindPlanPost(Convert.ToInt32(Request.QueryString.Get("id")));
                if (Request.QueryString.Get("mode").Equals("edit"))
                    GetPostClassInfo();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("mode").Equals("add"))
                InsertPostClass();
            else
                UpdatePostClass();
        }

        /// <summary>
        /// 获取培训班信息
        /// </summary>
        private void GetPostClassInfo()
        {
            OracleAccess access=new OracleAccess();
            DataTable dtPost =access.RunSqlDataSet( "select train_plan_id from zj_train_plan_post_class where train_plan_post_class_id=" +
                                                   Convert.ToInt32(Request.QueryString.Get("id"))).Tables[0];
            BindPlanPost(Convert.ToInt32(dtPost.Rows[0]["train_plan_id"]));

            DataTable dt =
                access.RunSqlDataSet("select * from zj_train_plan_post_class  where train_plan_post_class_id=" +
                                     Convert.ToInt32(Request.QueryString.Get("id"))).Tables[0];
            if(dt!=null && dt.Rows.Count>0)
            {
                if (dt.Rows[0]["train_plan_post_id"].ToString() == "")
                    dropPlanPost.SelectedValue = "0";
                else
                    dropPlanPost.SelectedValue = dt.Rows[0]["train_plan_post_id"].ToString();
                txtPostClassName.Text = dt.Rows[0]["class_name"].ToString();
                beginDate.DateValue = Convert.ToDateTime(dt.Rows[0]["begin_date"]).ToString("yyyy-MM-dd");
                endDate.DateValue = Convert.ToDateTime(dt.Rows[0]["end_date"]).ToString("yyyy-MM-dd");
            }
  
            this.Title = "修改计划培训班";
            current.InnerText = "修改计划培训班";
            IsHasPost(Convert.ToInt32(dt.Rows[0]["train_plan_id"]));
        }

        /// <summary>
        /// 新增培训班
        /// </summary>
        private void InsertPostClass()
        {
          
            StringBuilder str=new StringBuilder();
			//str.Append("select count(*) from zj_train_plan_post_class where ");
			//str.AppendFormat(
			//    "(( begin_date<=to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{0}','yyyy-mm-dd hh24:mi:ss')<= end_date ) or (  begin_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss')<= end_date ) )",
			//    beginDate.DateValue, endDate.DateValue);
			//str.AppendFormat(" and train_plan_id = {0}",Convert.ToInt32(Request.QueryString.Get("id")));
			//if (dropPlanPost.Enabled)
			//    str.AppendFormat(" and train_plan_post_id={0}", Convert.ToInt32(dropPlanPost.SelectedValue));
			//if (Convert.ToInt32(new OracleAccess().RunSqlDataSet(str.ToString()).Tables[0].Rows[0][0]) > 0)
			//    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('开始日期和结束日期不能交叉！')", true);
			//else
			//{
                str.Length = 0;
                str.Append(
                    "insert into zj_train_plan_post_class(train_plan_post_class_id,train_plan_post_id,class_name,");
                str.Append("begin_date,end_date,total_employee_number,train_plan_id,last_employee_number) ");
                str.Append(
                    " values (train_plan_post_class_seq.nextval,{0},'{1}',to_date('{2}','yyyy-mm-dd hh24:mi:ss'),");
                str.Append("to_date('{3}','yyyy-mm-dd hh24:mi:ss'),{4},{5},{6})");
                string sql = string.Format(str.ToString(), dropPlanPost.SelectedValue == "0"
                                                               ? "null"
                                                               : dropPlanPost.SelectedValue, txtPostClassName.Text,
                                           beginDate.DateValue,
                                           endDate.DateValue, 0, Convert.ToInt32(Request.QueryString.Get("id")), 0);
                try
                {
                    OracleAccess access = new OracleAccess();
                    access.ExecuteNonQuery(sql);
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);

                }
            //}
        }

        /// <summary>
        /// 修改培训班
        /// </summary>
        private void UpdatePostClass()
        {
            StringBuilder str = new StringBuilder();
			//str.Append("select count(*) from zj_train_plan_post_class where ");
			//str.AppendFormat(
			//    "(( begin_date<=to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{0}','yyyy-mm-dd hh24:mi:ss')<= end_date ) or (  begin_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss')<= end_date ) )",
			//    beginDate.DateValue, endDate.DateValue);
			//str.AppendFormat(" and train_plan_id = (select train_plan_id from zj_train_plan_post_class where train_plan_post_class_id={0}) and train_plan_post_class_id!={0}", Convert.ToInt32(Request.QueryString.Get("id")));
			//if (dropPlanPost.Enabled)
			//    str.AppendFormat(" and train_plan_post_id={0}", Convert.ToInt32(dropPlanPost.SelectedValue));
			//if (Convert.ToInt32(new OracleAccess().RunSqlDataSet(str.ToString()).Tables[0].Rows[0][0]) > 0)
			//    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('开始日期和结束日期不能交叉！')", true);
			//else
			//{
                str.Length = 0;
                str.Append("update zj_train_plan_post_class set train_plan_post_id={0},");
                str.Append(" class_name='{1}',begin_date=to_date('{2}','yyyy-mm-dd hh24:mi:ss'),");
                str.Append(" end_date=to_date('{3}','yyyy-mm-dd hh24:mi:ss')  where train_plan_post_class_id={4}");
                string sql = string.Format(str.ToString(), dropPlanPost.SelectedValue == "0"
                                                               ? "null"
                                                               : dropPlanPost.SelectedValue, txtPostClassName.Text,
                                           beginDate.DateValue,
                                           endDate.DateValue, Convert.ToInt32(Request.QueryString.Get("id")));
                try
                {
                    OracleAccess access = new OracleAccess();
                    access.ExecuteNonQuery(sql);
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);

                }
         //   }
        }
        
        /// <summary>
        /// 绑定职名
        /// </summary>
        private void BindPlanPost(int id)
        {
            dropPlanPost.Items.Clear();
            dropPlanPost.Items.Add(new ListItem("--请选择--", "0"));
            dropPlanPost.SelectedValue = "0";
            OracleAccess access=new OracleAccess();
            DataTable dt =
                access.RunSqlDataSet(
                    "  select train_plan_post_id,getpostnamebypostid(post_ids) postName from zj_train_plan_post where train_plan_id=" + id + " order by train_plan_post_id").Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                ListItem l = new ListItem(r["postName"].ToString(), r["train_plan_post_id"].ToString());
                dropPlanPost.Items.Add(l);
            }
            IsHasPost(id);
        }

        /// <summary>
        /// 判断是否需要选择职名
        /// </summary>
        /// <param name="planID"></param>
        private void IsHasPost(int planID)
        {
            OracleAccess access = new OracleAccess();
            DataTable dt = access.RunSqlDataSet("select has_post from zj_train_plan where train_plan_id=" + planID).Tables[0];
            if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                dropPlanPost.Enabled = false;
            else
                spanPost.Attributes.Add("class", "require");
        }
    }
}
