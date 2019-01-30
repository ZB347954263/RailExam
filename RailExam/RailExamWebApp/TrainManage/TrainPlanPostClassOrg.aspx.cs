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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using System.Text;

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanPostClassOrg : PageBase
    {
    	//private static string oldClassName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString.Get("mode").Equals("add"))
                {
                    BindPlanPost(Convert.ToInt32(Request.QueryString.Get("id")));
                   bool bl= IsHasPost(Convert.ToInt32(Request.QueryString.Get("id")));
                    //判断是否需要职名，并加载相应班级
                    if(bl)
                        BindPlanClass();
                    else
                        BindPlanClassNotPost(Convert.ToInt32(Request.QueryString.Get("id")));

					//隐藏行
                	trNew.Visible = true;
                	trUpdate.Visible = false;
                    trUpdateOrg.Visible = false;
                	divAdd.Visible = true;
                }
                else
                {
                    BindPlanPost(GetPlanID());
                    bool bl = IsHasPost(GetPlanID());
                    //判断是否需要职名，并加载相应班级
                    //if (bl)
                    //   // BindPlanClass();

                        if (!bl)
                        BindPlanClassNotPost(GetPlanID());

					//隐藏行
					trNew.Visible = false;
					trUpdate.Visible = true;
                    trUpdateOrg.Visible = true;
                	divAdd.Visible = false;
                }
             
                if(Request.QueryString.Get("mode").Equals("edit"))
                    GetInfo();

				gridInfo.DataSource = GetTempInfo();
				gridInfo.DataBind();
				ViewState["dt"] = gridInfo.DataSource;
            }
			if (hfOrgIDs.Value != "")
			{
				DataTable dt = GetTempInfo();
				foreach (GridViewRow row in gridInfo.Rows)
				{
					Label lblOrgID = row.FindControl("lblOrgID") as Label;
					Label lblOrgName = row.FindControl("lblOrgName") as Label;
					TextBox txtNum = row.FindControl("txtUpNum") as TextBox;

					if (lblOrgName.Text != "")
					{
						DataRow r = dt.NewRow();
						r["orgID"] = lblOrgID.Text;
						r["orgName"] = lblOrgName.Text;
						r["upNum"] = txtNum.Text;
						if ((","+hfOrgIDs.Value+",").IndexOf("," + lblOrgID.Text + ",") >= 0)
							dt.Rows.Add(r);
					}
				}

				string[] arrID = hfOrgIDs.Value.Split(',');
				if (arrID.Length > 0)
				{
					foreach (string s in arrID)
					{
						if (!string.IsNullOrEmpty(s.Replace(",", "")))
						{
							if (dt.Select("orgID='" + s + "'").Length == 0)
							{
								DataRow r = dt.NewRow();
								r["orgID"] = s;
								r["orgName"] = GetOrgName(s);
								r["upNum"] = "0";
								dt.Rows.Add(r);
							}
						}
					}
				}
				gridInfo.DataSource = dt;
				gridInfo.DataBind();
				ViewState["dt"] = gridInfo.DataSource;

				//加载职名
				string orgNames = "";
				foreach (DataRow r in dt.Rows)
				{
					if (!string.IsNullOrEmpty(r["orgName"].ToString().Replace(",", "")))
						orgNames += r["orgName"] + ",";
				}
				if (orgNames.Length > 0)
					txtMprOrgName.Text = orgNames.Substring(0, orgNames.Length - 1);
				else
				{
					txtMprOrgName.Text = "";
					hfOrg.Value = "";
				}
			}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
			if (Request.QueryString.Get("mode").Equals("add"))
			{
			    if(!InsertPlanClassOrg())
			    {
			        btnSave.Enabled = true;
			        return;
			    }
			}
			else
			{
				if (IsHasResult())
				{
                    if(!UpdatePlanClassOrg())
                    {
                        btnSave.Enabled = true;
                        return;
                    }
                }
				else
				{
                    btnSave.Enabled = true;
                    return;
                }
			}
        	UpdatePostClassNum();
            UpdatePostNum();
        }

        protected void dropPlanPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPlanClass();
        }
        /// <summary>
        /// 绑定计划职名
        /// </summary>
        /// <param name="id"></param>
        private void BindPlanPost(int id)
        {
            dropPlanPost.Items.Clear();
            dropPlanPost.Items.Add(new ListItem("--请选择--", "0"));
            dropPlanPost.SelectedValue = "0";

            OracleAccess access=new OracleAccess();
            DataTable dt =
                access.RunSqlDataSet(
                    "select train_plan_post_id ,getpostnamebypostid(post_ids) postName from zj_train_plan_post where train_plan_id=" + id + " order by train_plan_post_id").Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                ListItem l = new ListItem(r["postName"].ToString(), r["train_plan_post_id"].ToString());
                dropPlanPost.Items.Add(l);
            }
           
        }

        private void BindPlanClass()
        {
            dropPlanClass.Items.Clear();
            dropPlanClass.Items.Add(new ListItem("--请选择--", "0"));
            dropPlanClass.SelectedValue = "0";

            OracleAccess access = new OracleAccess();
            DataTable dt =
                access.RunSqlDataSet(
                    "select train_plan_post_class_id,class_name from zj_train_plan_post_class where train_plan_post_id=" +
                    Convert.ToInt32(dropPlanPost.SelectedValue) + " order by train_plan_post_class_id").Tables[0];

            foreach (DataRow r in dt.Rows)
            {
                ListItem l = new ListItem(r["class_name"].ToString(), r["train_plan_post_class_id"].ToString());
                dropPlanClass.Items.Add(l);
            }
            
        }
        private void BindPlanClassNotPost(int planID)
        {
            OracleAccess access = new OracleAccess();
            dropPlanClass.DataSource =
                access.RunSqlDataSet(
                    "select train_plan_post_class_id,class_name from zj_train_plan_post_class where train_plan_id=" +
                    planID);
            dropPlanClass.DataTextField = "class_name";
            dropPlanClass.DataValueField = "train_plan_post_class_id";
            dropPlanClass.DataBind();
            dropPlanClass.Items.Add(new ListItem("--请选择--", "0"));
            dropPlanClass.SelectedValue = "0";
        }

        /// <summary>
        /// 修改信息时获取planID
        /// </summary>
        /// <returns></returns>
        private int GetPlanID()
        {
            OracleAccess access=new OracleAccess();
            string sql =
                "select c.train_plan_id from zj_train_plan_post_class_org o left join zj_train_plan_post_class c on o.train_plan_post_class_id=c.train_plan_post_class_id where o.train_plan_post_class_org_id=" +
                Convert.ToInt32(Request.QueryString.Get("id"));
            return
                Convert.ToInt32(
                    access.RunSqlDataSet(sql).Tables[0].Rows[0][0]);
        }

        private bool InsertPlanClassOrg()
        {
			//对OrgID进行处理
            int count1 = 0;
            int count2 = 0;
			Hashtable hs=new Hashtable();
			foreach (GridViewRow row in gridInfo.Rows)
			{
				Label lblOrgID = row.FindControl("lblOrgID") as Label;
				Label lblOrgName = row.FindControl("lblOrgName") as Label;
                TextBox txtUpNum = row.FindControl("txtUpNum") as TextBox;

                if (lblOrgName.Text != "")
                {
                    if (string.IsNullOrEmpty(txtUpNum.Text))
                    {
                        count1 = count1 + 1;
                    }
                    else
                    {
                        if (txtUpNum.Text == "0")
                        {
                            count2 = count2 + 1;
                        }
                    }

                    hs.Add(lblOrgID.Text, txtUpNum.Text);
                }
			}

            if(count1>0)
            {
                SessionSet.PageMessage = "请填写上报人数！";
                return false;
            }

            if (count2 > 0)
            {
                SessionSet.PageMessage = "上报人数不能为0！";
                return false;
            }

			if (hs.Count > 0)
			{
				foreach (DictionaryEntry dic in hs)
				{
					StringBuilder str = new StringBuilder();
					try
					{
					str.Append("insert into zj_train_plan_post_class_org(train_plan_post_class_org_id,");
					str.Append("train_plan_post_class_id,employee_number,last_employee_number,is_delcare,org_id");
					str.AppendFormat(") values (train_plan_post_class_org_seq.nextval,{0},{1},{2},{3},{4})",
						Convert.ToInt32(dropPlanClass.SelectedValue),dic.Value.ToString().Trim()==""?0: Convert.ToInt32(dic.Value), 0, 0,
					                 Convert.ToInt32(dic.Key));
					}
					catch  
					{
					}
				
					try
					{
						OracleAccess access = new OracleAccess();
						access.ExecuteNonQuery(str.ToString());
						////新增上报单位
						string sqlOrg = string.Format(" update zj_train_plan set orgids='{0}' where train_plan_id={1}",
						                              GetOrg(Convert.ToInt32(Request.QueryString.Get("id"))),
						                              Convert.ToInt32(Request.QueryString.Get("id")));
						access.ExecuteNonQuery(sqlOrg);
					
					}
					catch (Exception)
					{
						ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);
                        return false;
                    }
				}
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);

			}

            return true;
        }

        /// <summary>
        /// 获取该计划的org
        /// </summary>
        /// <returns></returns>
        private string GetOrg(int planID)
        {
             List<string> lst=new List<string>();
            DataTable dt =
                new OracleAccess().RunSqlDataSet("select org_id from zj_train_plan_post_class_org where train_plan_post_class_id in (select train_plan_post_class_id from zj_train_plan_post_class where train_plan_id=" +
                                                 planID + ")").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
               
                
                foreach (DataRow r in dt.Rows)
                {
                    if (!lst.Contains(r["org_id"].ToString()))
                        lst.Add(r["org_id"].ToString());
                }
                
            }
            if (lst.Count > 0)
                return string.Join(",", lst.ToArray());
            else
                return "";
        }

        private  bool UpdatePlanClassOrg()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("update zj_train_plan_post_class_org set train_plan_post_class_id={0},", Convert.ToInt32(dropPlanClass.SelectedValue));
            str.AppendFormat("employee_number={0} ,org_id={1} where train_plan_post_class_org_id={2}",
                             Convert.ToInt32(txtNum.Text), Convert.ToInt32(hfOrg.Value),
                             Convert.ToInt32(Request.QueryString.Get("id")));

            
            try
            {
                OracleAccess access = new OracleAccess();

				UpdatePostClassNum(Request.QueryString.Get("id"), dropPlanClass.SelectedValue);          //更新计划班级的计划人数
				UpdatePostClassLastNum(Request.QueryString.Get("id"), dropPlanClass.SelectedValue);      //更新计划班的实际人数 (原班和新班)
				UpdatePostNum(Request.QueryString.Get("id"), dropPlanClass.SelectedValue);               //更新职名的计划上报人数
				UpdatePostLastNum(Request.QueryString.Get("id"), dropPlanClass.SelectedValue);           //更新职名的实际上报人数（原班和新班）
				DeleteSubjectResult(Request.QueryString.Get("id"));                                      //删除原站段班级的科目结果
				UpdatePlanEmpToOtherClass(dropPlanClass.SelectedValue, Request.QueryString.Get("id"));   //更新原计划站段中的学员到新班级中
				InsertSubjectResultByNewClass(dropPlanClass.SelectedValue);                              //新增新班的科目结果


                access.ExecuteNonQuery(str.ToString());
                ////修改上报单位
                string sqlOrg = string.Format(" update zj_train_plan set orgids='{0}' where train_plan_id={1}",
                                             GetOrg(GetPlanID()), GetPlanID());
                access.ExecuteNonQuery(sqlOrg);


                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取基本信息
        /// </summary>
        private void GetInfo()
        {
             OracleAccess access = new OracleAccess();
            StringBuilder str=new StringBuilder();
            str.Append("select c.train_plan_post_id, c.train_plan_post_class_id,o.employee_number,o.org_id,org.full_name  from zj_train_plan_post_class_org o ");
            str.Append("left join zj_train_plan_post_class c on c.train_plan_post_class_id=o.train_plan_post_class_id ");
            str.Append("  left join org on org.org_id=o.org_id ");
            str.Append(" where train_plan_post_class_org_id=" + Convert.ToInt32(Request.QueryString.Get("id")));
            str.Append(" order by train_plan_post_class_id,org.order_index");
            DataTable dt = access.RunSqlDataSet(str.ToString()).Tables[0];
            dropPlanPost.SelectedValue = dt.Rows[0]["train_plan_post_id"].ToString();
            bool bl = IsHasPost(GetPlanID());
            //判断是否需要职名，并加载相应班级
            if (bl)
                BindPlanClass();
           
            dropPlanClass.SelectedValue = dt.Rows[0]["train_plan_post_class_id"].ToString();
            txtNum.Text = dt.Rows[0]["employee_number"].ToString();
            txtOrg.Text = dt.Rows[0]["full_name"].ToString();
            hfOrg.Value = dt.Rows[0]["org_id"].ToString();
            this.Title = "修改计划培训班站段";
			current.InnerText = "修改计划培训班站段";
        	oldClassName.Value = dropPlanClass.Items[dropPlanClass.SelectedIndex].Text;
        }

        
        /// <summary>
        /// 更新班中的人数
        /// </summary>
        private void UpdatePostClassNum()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                //该班的人数
                string sqlNum =
                    string.Format(
                        "select sum(employee_number) allNum from zj_train_plan_post_class_org where train_plan_post_class_id={0}",
                        Convert.ToInt32(dropPlanClass.SelectedValue));
                int allNum = Convert.ToInt32(access.RunSqlDataSet(sqlNum).Tables[0].Rows[0][0]);

                string sql =
                    string.Format(
                        "update zj_train_plan_post_class set total_employee_number={0} where train_plan_post_class_id={1}",
                        allNum, Convert.ToInt32(dropPlanClass.SelectedValue));
                access.ExecuteNonQuery(sql);
            }
            catch 
            {
                ClientScript.RegisterClientScriptBlock(GetType(),"","alert('计划培训班级人数更新失败！')",true);
            }
        }

        /// <summary>
        /// 更新职名中的人数
        /// </summary>
        private void UpdatePostNum()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                string sqlNum =
                    " select sum(total_employee_number) allNum from zj_train_plan_post_class where train_plan_post_id=";
                sqlNum += " (select train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id=" +
                          Convert.ToInt32(dropPlanClass.SelectedValue) + ")";
                DataTable dt = access.RunSqlDataSet(sqlNum).Tables[0];
                if (dt.Rows[0][0].ToString().Length > 0)
                {
                    int allNum = Convert.ToInt32(dt.Rows[0][0]);

                    string sql =
                        string.Format(" update zj_train_plan_post set employee_number={0} where train_plan_post_id={1}",
                                      allNum, Convert.ToInt32(dropPlanPost.SelectedValue));
                    access.ExecuteNonQuery(sql);
                }
            }
            catch (Exception)
            {

                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('计划职名人数更新失败！')", true);
            }
          
        }


        /// <summary>
        /// 判断是否需要选择职名
        /// </summary>
        /// <param name="planID"></param>
        private bool IsHasPost(int planID)
        {
            bool bl = true;
            OracleAccess access = new OracleAccess();
            DataTable dt = access.RunSqlDataSet("select has_post from zj_train_plan where train_plan_id=" + planID).Tables[0];
            if (Convert.ToInt32(dt.Rows[0][0]) == 0)
            {
                dropPlanPost.Enabled = false;
                bl = false;
            }
            else
                spanPost.Attributes.Add("class", "require");
            return bl;
        }

		/// <summary>
		/// 更新原计划站段中的学员到新班级中
		/// </summary>
		/// <param name="planClassID"></param>
		/// <param name="planOrgID"></param>
		private void UpdatePlanEmpToOtherClass(string planClassID,string planOrgID)
		{
			OracleAccess access=new OracleAccess();
			try
			{
				string sql = string.Format(@"
            update zj_train_plan_employee set TRAIN_CLASS_ID=
			(select distinct train_class_id  from zj_train_class where train_plan_post_class_id={0})
			,TRAIN_PLAN_POST_CLASS_ID={0}
			where TRAIN_PLAN_POST_CLASS_ORG_ID={1}
              ", planClassID, planOrgID);
				access.ExecuteNonQuery(sql);
			}
			catch 
			{
			}
		}

		/// <summary>
		/// 更新计划班的实际人数 (原班和新班)
		/// </summary>
		private void UpdatePostClassLastNum(string oldClassOrgID,string newClassID)
		{
			OracleAccess access=new OracleAccess();
			int num = 0;
			try
			{
				string sql =
					"select last_employee_number from zj_train_plan_post_class_org where train_plan_post_class_org_id=" +
					oldClassOrgID;
				num = Convert.ToInt32(access.RunSqlDataSet(sql).Tables[0].Rows[0][0]);
				sql=@"update zj_train_plan_post_class set LAST_EMPLOYEE_NUMBER=LAST_EMPLOYEE_NUMBER+"+
				     num + "  where TRAIN_PLAN_POST_CLASS_ID=" + newClassID;
                access.ExecuteNonQuery(sql);    //新班
				sql = @"
			update zj_train_plan_post_class set LAST_EMPLOYEE_NUMBER=LAST_EMPLOYEE_NUMBER-"+num
            +" where TRAIN_PLAN_POST_CLASS_ID=(select  distinct train_plan_post_class_id from zj_train_plan_post_class_org " +
				      " where train_plan_post_class_org_id=" + oldClassOrgID + ")";
				access.ExecuteNonQuery(sql);    //原班
			}
			catch   
			{
			}
		}

		/// <summary>
		/// 更新计划班级的计划人数
		/// </summary>
		/// <param name="oldClassOrgID"></param>
		/// <param name="newClassID"></param>
		private void UpdatePostClassNum(string oldClassOrgID, string newClassID)
		{
			 
			string sql = "select employee_number from zj_train_plan_post_class_org where train_plan_post_class_org_id=" +
			             oldClassOrgID;
			int num = 0;
			DataTable dt = new OracleAccess().RunSqlDataSet(sql).Tables[0];
			if(dt!=null)
			{
				if (dt.Rows[0][0] != "")
					num = Convert.ToInt32(dt.Rows[0][0]);
			}
			try
			{
				sql = @"
               update zj_train_plan_post_class set total_employee_number=total_employee_number-" + num
		+ " where train_plan_post_class_id=(select distinct train_plan_post_class_id from zj_train_plan_post_class_org where train_plan_post_class_org_id=" +
	    oldClassOrgID + ")";
				new OracleAccess().ExecuteNonQuery(sql);    //更新原来的人数
				sql = @"update zj_train_plan_post_class set total_employee_number=total_employee_number+" + num
					+ " where train_plan_post_class_id=" + newClassID;
				new OracleAccess().ExecuteNonQuery(sql);   //更新新班的人数
			}
			catch  
			{
			}
		}

    	/// <summary>
		/// 更新职名的实际上报人数（原班和新班）
		/// </summary>
		private void UpdatePostLastNum( string oldClassOrgID, string newClassID)
		{
			OracleAccess access=new OracleAccess();
			int num = 0;
			try
			{
//                string sql = "select last_employee_number from zj_train_plan_post_class_org where train_plan_post_class_org_id=" +
//                             oldClassOrgID;
//                num = Convert.ToInt32(access.RunSqlDataSet(sql).Tables[0].Rows[0][0]);

//                sql = @"update zj_train_plan_post set last_employee_number=last_employee_number+" +
//                      num +
//                      " where train_plan_post_id in (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id=" +
//                      newClassID + ")";
//                access.ExecuteNonQuery(sql);   //新职名

//                sql=string.Format(@"
//			update zj_train_plan_post set last_employee_number=last_employee_number-"+num
//           +"where train_plan_post_id in (select distinct train_plan_post_id from zj_train_plan_post_class where" +
//            " train_plan_post_class_id in (select distinct train_plan_post_class_id from zj_train_plan_post_class_org" +
//          " where train_plan_post_class_org_id={0}))", oldClassOrgID);
//                access.ExecuteNonQuery(sql);    //原来的职名

				string sql =string.Format(@"
                         update zj_train_plan_post set last_employee_number=
			(select sum(last_employee_number) from zj_train_plan_post_class where train_plan_post_id=
			  (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id={0}))
			where train_plan_post_id= (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id={0})
                              ",newClassID);
				access.ExecuteNonQuery(sql);   //新职名

				newClassID =
					string.Format(
						"(select distinct train_plan_post_class_id from zj_train_plan_post_class_org where train_plan_post_class_org_id={0})",
						oldClassOrgID);
				sql = string.Format(@"
                         update zj_train_plan_post set last_employee_number=
			(select sum(last_employee_number) from zj_train_plan_post_class where train_plan_post_id=
			  (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id={0}))
			where train_plan_post_id= (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id={0})
                              ", newClassID);
				access.ExecuteNonQuery(sql);   //原来的职名

			}
			catch 
			{
			}
		}

		/// <summary>
		/// 更新职名的计划人数
		/// </summary>
		/// <param name="oldClassOrgID"></param>
		/// <param name="newClassID"></param>
		private void UpdatePostNum(string oldClassOrgID, string newClassID)
		{
			string sql = "select employee_number from zj_train_plan_post_class_org where train_plan_post_class_org_id=" +
						 oldClassOrgID;
			int num = 0;
			DataTable dt = new OracleAccess().RunSqlDataSet(sql).Tables[0];
			if (dt != null)
			{
				if (dt.Rows[0][0] != "")
					num = Convert.ToInt32(dt.Rows[0][0]);
			}
			OracleAccess access = new OracleAccess();

			try
			{
				sql = string.Format(@"
							update zj_train_plan_post set employee_number=employee_number-" + num
				+ "where train_plan_post_id in (select distinct train_plan_post_id from zj_train_plan_post_class where" +
				" train_plan_post_class_id in (select distinct train_plan_post_class_id from zj_train_plan_post_class_org" +
				" where train_plan_post_class_org_id={0}))", oldClassOrgID);
				access.ExecuteNonQuery(sql);      //原来的职名

				sql = @"update zj_train_plan_post set employee_number=employee_number+" +
			   num +
			  " where train_plan_post_id in (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id=" +
			  newClassID + ")";
				access.ExecuteNonQuery(sql);   //新职名

			}
			catch
			{
			}

		}

    	/// <summary>
		///删除原站段班级的科目结果
		/// </summary>
		private void DeleteSubjectResult(string classOrgID)
		{
			OracleAccess access = new OracleAccess();
			try
			{
				string sql = @"
             delete from zj_train_class_subject_result where
			train_class_id in 
			(select distinct train_class_id from  zj_train_class where train_plan_post_class_id
			  in (select distinct train_plan_post_class_id from zj_train_plan_post_class_org where train_plan_post_class_org_id=" + classOrgID + " )) " +
			 " and EMPLOYEE_ID in (select EMPLOYEE_ID from zj_train_plan_employee where TRAIN_PLAN_POST_CLASS_ORG_ID="+classOrgID+")";
				access.ExecuteNonQuery(sql);
			}
			catch
			{
			}

		}

		/// <summary>
		/// 新增新班的科目结果
		/// </summary>
		/// <param name="classID"></param>
		private void InsertSubjectResultByNewClass(string classID)
		{
			DataTable dtSubject=new DataTable();
			DataTable dtEmp=new DataTable();
			OracleAccess access=new OracleAccess();
			string sql = "select train_class_id from zj_train_class where train_plan_post_class_id=" + classID;
			DataTable dtClass= access.RunSqlDataSet(sql).Tables[0];    //是否有实际班
			if (dtClass.Rows.Count > 0)
			{
				sql = " select train_class_subject_id from zj_train_class_subject where train_class_id=" + dtClass.Rows[0][0];
				dtSubject = access.RunSqlDataSet(sql).Tables[0]; //是否有科目
				sql = " select employee_id from zj_train_plan_employee where train_class_id=" + dtClass.Rows[0][0];
				dtEmp = access.RunSqlDataSet(sql).Tables[0]; //是否有学员
			}
			try
			{
				if (dtClass.Rows.Count > 0)      //有实际班级
				{

					if (dtSubject.Rows.Count > 0)     //有科目
					{
						sql = "";
						foreach (DataRow rSub in dtSubject.Rows)
						{
							if (dtEmp.Rows.Count > 0)
							{
								foreach (DataRow rEmp in dtEmp.Rows)
								{
									//string sqlIs = "select count(1) from zj_train_class_subject_result where employee_id=" + rEmp["employee_id"] +
									//               " and TRAIN_CLASS_ID=" + dtClass.Rows[0][0];
									//DataTable dtIs =
									//    access.RunSqlDataSet(sqlIs).
									//        Tables[0];
									//if (Convert.ToInt32(dtIs.Rows[0][0]) == 0)
									//{
									sql =
										string.Format(
											@"
                                    insert  into zj_train_class_subject_result(train_class_subject_result_id,train_class_id,train_class_subject_id,employee_id,ispass)
					 values (train_class_subject_result_seq.nextval,{0},{1},{2},{3})
                                    ",
											Convert.ToInt32(dtClass.Rows[0][0]), Convert.ToInt32(rSub["train_class_subject_id"]),
											Convert.ToInt32(rEmp["employee_id"]), 0);
									new OracleAccess().ExecuteNonQuery(sql);
									//}

								}
							}

						}

					}
				}
			}
			catch
			{
			}
			
		}

		/// <summary>
		/// 判断该班是否有成绩
		/// </summary>
		/// <returns></returns>
		private bool IsHasResult()
		{
			string sql =
				string.Format(
					@"
                        select count(1) from zj_train_class_subject_result 
		where result is not null and train_class_id in
     (select distinct train_class_id from zj_train_class where train_plan_post_class_id in (
		 (select distinct train_plan_post_class_id from zj_train_plan_post_class_org 
       where train_plan_post_class_org_id={0}))) "
					, Request.QueryString.Get("id"));
			OracleAccess access=new OracleAccess();
			DataTable dt= access.RunSqlDataSet(sql).Tables[0];
			if(dt.Rows.Count>0)
			{
				if (Convert.ToInt32(dt.Rows[0][0]) > 0)
				{
					ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('" + oldClassName.Value + "存在考试，不能修改！')", true);
					return false;
				}
			}
			return true;
		}

		
		private string GetOrgName(string orgID)
		{
			string sql = "select short_name from org where org_id=" + orgID;
			return new OracleAccess().RunSqlDataSet(sql).Tables[0].Rows[0][0].ToString();
		}


		private DataTable GetTempInfo()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("orgID", typeof(string));
			dt.Columns.Add("orgName", typeof(string));
			dt.Columns.Add("upNum", typeof(string));
			DataRow r = dt.NewRow();
			r["orgID"] = "-1";
			dt.Rows.Add(r);
			return dt;
		}

		protected void gridInfo_RowDataBoundDataRow(object sender, GridViewRowEventArgs e)
		{
			if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
			{
				if (gridInfo.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
				{
					e.Row.Visible = false;
				}
			}
		}

		 

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (hfdelOrgID.Value != "")
			{
				DataTable dt = ViewState["dt"] as DataTable;
				DataRow[] arr = dt.Select("orgID='" + hfdelOrgID.Value + "'");
				if (arr.Length > 0)
					dt.Rows.Remove(arr[0]);
				gridInfo.DataSource = dt;
				gridInfo.DataBind();
				ViewState["dt"] = dt;
				hfOrgIDs.Value = hfOrgIDs.Value.Replace(hfdelOrgID.Value, "");
				//加载职名
				string orgNames = "";
				foreach (DataRow r in dt.Rows)
				{
					if (!string.IsNullOrEmpty(r["orgName"].ToString().Replace(",", "")))
						orgNames += r["orgName"] + ",";
				}
				if (orgNames.Length > 0)
					txtMprOrgName.Text = orgNames.Substring(0, orgNames.Length - 1);
				else
				{
					txtMprOrgName.Text = "";
					hfOrg.Value = "";
				}
			}
		}
    }
}
