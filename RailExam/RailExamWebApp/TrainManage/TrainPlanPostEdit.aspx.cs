using System;
using System.Collections.Generic;
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

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanPostAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
				if (Request.QueryString.Get("mode").Equals("edit"))
				{
					GetInfo();
					IsHasClass();
				}
            }
        }
        private void InsertPost()
        {
            string sqlSel = "select post_ids from zj_train_plan_post where train_plan_id=" + Convert.ToInt32(Request.QueryString.Get("id"));

            string sql =
                string.Format(
                    "insert into zj_train_plan_post(train_plan_post_id,train_plan_id,post_ids,employee_number,last_employee_number,is_work_group_leader) values (zj_train_plan_post_seq.nextval,{0},'{1}',{2},{3},{4})",
                    Convert.ToInt32(Request.QueryString.Get("id")), hfPostIDs.Value, 0, 0, chkIsLeader.Checked == true ? 1 : 0);
            try
            {
                OracleAccess access = new OracleAccess();
               
                
                if ( CheckPost(access.RunSqlDataSet(sqlSel).Tables[0], hfPostIDs.Value))
                {
                    access.ExecuteNonQuery(sql);
                    hfPostIDs.Value = "";
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
                }
                else
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('该职名中存在已选择的项，请重新选择！')", true);
                txtPostName.Text = hfPostNames.Value;
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("mode").Equals("add"))
                InsertPost();
            else
                UpdatePost();
        }

        private bool CheckPost(DataTable dt,string postIDs)
        {
            bool bl = true;
            List<string> lst=new List<string>();  //该计划的所有postID
            foreach (DataRow r in dt.Rows)
            {
                string[] arr = r[0].ToString().Split(',');
                foreach (string str in arr)
                {
                    lst.Add(str);
                }
            }
            string[] newArr = postIDs.Split(',');
            foreach (string s in newArr)
            {
                if(lst.Contains(s))
                {
                    bl = false;
                    break;
                }
            }
            return bl;
        }

        private void UpdatePost()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                string sqlSel = "select post_ids from zj_train_plan_post where  train_plan_post_id!=" + Request.QueryString.Get("id");
                sqlSel += " and train_plan_id in (  select train_plan_id from zj_train_plan_post where train_plan_post_id=" + Request.QueryString.Get("id") + ")";
                if (CheckPost(access.RunSqlDataSet(sqlSel).Tables[0],hfPostIDs.Value))
                {
                    string sql =
                        string.Format(
                            "update zj_train_plan_post set post_ids='{0}',is_work_group_leader={1} where train_plan_post_id={2}",
                            hfPostIDs.Value, chkIsLeader.Checked == true ? 1 : 0, Request.QueryString.Get("id"));
                    access.ExecuteNonQuery(sql);
                    hfPostIDs.Value = "";
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('该职名中存在已选择的项，请重新选择！')", true);
                    txtPostName.Text = hfPostNames.Value;
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(),"","alert('数据保存失败！')",true);
            }
            
        }
        private void GetInfo()
        {
            OracleAccess access = new OracleAccess();
            string sql = "select post_ids, GetPostNameByPostID(post_ids) postName,is_work_group_leader from zj_train_plan_post where train_plan_post_id=" +
                         Convert.ToInt32(Request.QueryString.Get("id"));
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];
            hfPostIDs.Value = dt.Rows[0]["post_ids"].ToString();
            txtPostName.Text = dt.Rows[0]["postName"].ToString();
            hfPostNames.Value = dt.Rows[0]["postName"].ToString();
            chkIsLeader.Checked = Convert.ToInt32(dt.Rows[0]["is_work_group_leader"]).Equals(1);
            this.Title = "修改计划职名";
            current.InnerText = "修改培训计划职名";
        }

		private void IsHasClass()
		{
			OracleAccess access=new OracleAccess();
			string sql = string.Format(@"select count(*) from zj_train_class 
								where TRAIN_PLAN_POST_CLASS_ID in 
								(
								  select TRAIN_PLAN_POST_CLASS_ID from zj_train_plan_post_class where TRAIN_PLAN_POST_ID={0}
								)", Convert.ToInt32(Request.QueryString.Get("id")));
			DataTable dtisclass = access.RunSqlDataSet(sql).Tables[0];
			if (dtisclass != null)
			{
				if (Convert.ToInt32(dtisclass.Rows[0][0]) > 0)
					btnSave.Enabled = false;
			}
			 
		}

		protected void btnPost_Click(object sender, EventArgs e)
		{
			string name = "";
			OracleAccess access = new OracleAccess();
			if (hfPostIDs.Value != "")
			{
				string sql = string.Format("select  post_name from post where post_id in ({0})", hfPostIDs.Value);
				DataTable dt = access.RunSqlDataSet(sql).Tables[0];

				foreach (DataRow r in dt.Rows)
				{
					name += r[0] + ",";

				}
				txtPostName.Text = name.Substring(0, name.Length - 1);
			}
			else
				txtPostName.Text = "";
			hfPostNames.Value = txtPostName.Text;
		}
    }
}
