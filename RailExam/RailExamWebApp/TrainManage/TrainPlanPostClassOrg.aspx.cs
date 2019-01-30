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
                    //�ж��Ƿ���Ҫְ������������Ӧ�༶
                    if(bl)
                        BindPlanClass();
                    else
                        BindPlanClassNotPost(Convert.ToInt32(Request.QueryString.Get("id")));

					//������
                	trNew.Visible = true;
                	trUpdate.Visible = false;
                    trUpdateOrg.Visible = false;
                	divAdd.Visible = true;
                }
                else
                {
                    BindPlanPost(GetPlanID());
                    bool bl = IsHasPost(GetPlanID());
                    //�ж��Ƿ���Ҫְ������������Ӧ�༶
                    //if (bl)
                    //   // BindPlanClass();

                        if (!bl)
                        BindPlanClassNotPost(GetPlanID());

					//������
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

				//����ְ��
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
        /// �󶨼ƻ�ְ��
        /// </summary>
        /// <param name="id"></param>
        private void BindPlanPost(int id)
        {
            dropPlanPost.Items.Clear();
            dropPlanPost.Items.Add(new ListItem("--��ѡ��--", "0"));
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
            dropPlanClass.Items.Add(new ListItem("--��ѡ��--", "0"));
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
            dropPlanClass.Items.Add(new ListItem("--��ѡ��--", "0"));
            dropPlanClass.SelectedValue = "0";
        }

        /// <summary>
        /// �޸���Ϣʱ��ȡplanID
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
			//��OrgID���д���
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
                SessionSet.PageMessage = "����д�ϱ�������";
                return false;
            }

            if (count2 > 0)
            {
                SessionSet.PageMessage = "�ϱ���������Ϊ0��";
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
						////�����ϱ���λ
						string sqlOrg = string.Format(" update zj_train_plan set orgids='{0}' where train_plan_id={1}",
						                              GetOrg(Convert.ToInt32(Request.QueryString.Get("id"))),
						                              Convert.ToInt32(Request.QueryString.Get("id")));
						access.ExecuteNonQuery(sqlOrg);
					
					}
					catch (Exception)
					{
						ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('���ݱ���ʧ�ܣ�')", true);
                        return false;
                    }
				}
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);

			}

            return true;
        }

        /// <summary>
        /// ��ȡ�üƻ���org
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

				UpdatePostClassNum(Request.QueryString.Get("id"), dropPlanClass.SelectedValue);          //���¼ƻ��༶�ļƻ�����
				UpdatePostClassLastNum(Request.QueryString.Get("id"), dropPlanClass.SelectedValue);      //���¼ƻ����ʵ������ (ԭ����°�)
				UpdatePostNum(Request.QueryString.Get("id"), dropPlanClass.SelectedValue);               //����ְ���ļƻ��ϱ�����
				UpdatePostLastNum(Request.QueryString.Get("id"), dropPlanClass.SelectedValue);           //����ְ����ʵ���ϱ�������ԭ����°ࣩ
				DeleteSubjectResult(Request.QueryString.Get("id"));                                      //ɾ��ԭվ�ΰ༶�Ŀ�Ŀ���
				UpdatePlanEmpToOtherClass(dropPlanClass.SelectedValue, Request.QueryString.Get("id"));   //����ԭ�ƻ�վ���е�ѧԱ���°༶��
				InsertSubjectResultByNewClass(dropPlanClass.SelectedValue);                              //�����°�Ŀ�Ŀ���


                access.ExecuteNonQuery(str.ToString());
                ////�޸��ϱ���λ
                string sqlOrg = string.Format(" update zj_train_plan set orgids='{0}' where train_plan_id={1}",
                                             GetOrg(GetPlanID()), GetPlanID());
                access.ExecuteNonQuery(sqlOrg);


                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('���ݱ���ʧ�ܣ�')", true);
                return false;
            }

            return true;
        }

        /// <summary>
        /// ��ȡ������Ϣ
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
            //�ж��Ƿ���Ҫְ������������Ӧ�༶
            if (bl)
                BindPlanClass();
           
            dropPlanClass.SelectedValue = dt.Rows[0]["train_plan_post_class_id"].ToString();
            txtNum.Text = dt.Rows[0]["employee_number"].ToString();
            txtOrg.Text = dt.Rows[0]["full_name"].ToString();
            hfOrg.Value = dt.Rows[0]["org_id"].ToString();
            this.Title = "�޸ļƻ���ѵ��վ��";
			current.InnerText = "�޸ļƻ���ѵ��վ��";
        	oldClassName.Value = dropPlanClass.Items[dropPlanClass.SelectedIndex].Text;
        }

        
        /// <summary>
        /// ���°��е�����
        /// </summary>
        private void UpdatePostClassNum()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                //�ð������
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
                ClientScript.RegisterClientScriptBlock(GetType(),"","alert('�ƻ���ѵ�༶��������ʧ�ܣ�')",true);
            }
        }

        /// <summary>
        /// ����ְ���е�����
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

                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('�ƻ�ְ����������ʧ�ܣ�')", true);
            }
          
        }


        /// <summary>
        /// �ж��Ƿ���Ҫѡ��ְ��
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
		/// ����ԭ�ƻ�վ���е�ѧԱ���°༶��
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
		/// ���¼ƻ����ʵ������ (ԭ����°�)
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
                access.ExecuteNonQuery(sql);    //�°�
				sql = @"
			update zj_train_plan_post_class set LAST_EMPLOYEE_NUMBER=LAST_EMPLOYEE_NUMBER-"+num
            +" where TRAIN_PLAN_POST_CLASS_ID=(select  distinct train_plan_post_class_id from zj_train_plan_post_class_org " +
				      " where train_plan_post_class_org_id=" + oldClassOrgID + ")";
				access.ExecuteNonQuery(sql);    //ԭ��
			}
			catch   
			{
			}
		}

		/// <summary>
		/// ���¼ƻ��༶�ļƻ�����
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
				new OracleAccess().ExecuteNonQuery(sql);    //����ԭ��������
				sql = @"update zj_train_plan_post_class set total_employee_number=total_employee_number+" + num
					+ " where train_plan_post_class_id=" + newClassID;
				new OracleAccess().ExecuteNonQuery(sql);   //�����°������
			}
			catch  
			{
			}
		}

    	/// <summary>
		/// ����ְ����ʵ���ϱ�������ԭ����°ࣩ
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
//                access.ExecuteNonQuery(sql);   //��ְ��

//                sql=string.Format(@"
//			update zj_train_plan_post set last_employee_number=last_employee_number-"+num
//           +"where train_plan_post_id in (select distinct train_plan_post_id from zj_train_plan_post_class where" +
//            " train_plan_post_class_id in (select distinct train_plan_post_class_id from zj_train_plan_post_class_org" +
//          " where train_plan_post_class_org_id={0}))", oldClassOrgID);
//                access.ExecuteNonQuery(sql);    //ԭ����ְ��

				string sql =string.Format(@"
                         update zj_train_plan_post set last_employee_number=
			(select sum(last_employee_number) from zj_train_plan_post_class where train_plan_post_id=
			  (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id={0}))
			where train_plan_post_id= (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id={0})
                              ",newClassID);
				access.ExecuteNonQuery(sql);   //��ְ��

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
				access.ExecuteNonQuery(sql);   //ԭ����ְ��

			}
			catch 
			{
			}
		}

		/// <summary>
		/// ����ְ���ļƻ�����
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
				access.ExecuteNonQuery(sql);      //ԭ����ְ��

				sql = @"update zj_train_plan_post set employee_number=employee_number+" +
			   num +
			  " where train_plan_post_id in (select distinct train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id=" +
			  newClassID + ")";
				access.ExecuteNonQuery(sql);   //��ְ��

			}
			catch
			{
			}

		}

    	/// <summary>
		///ɾ��ԭվ�ΰ༶�Ŀ�Ŀ���
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
		/// �����°�Ŀ�Ŀ���
		/// </summary>
		/// <param name="classID"></param>
		private void InsertSubjectResultByNewClass(string classID)
		{
			DataTable dtSubject=new DataTable();
			DataTable dtEmp=new DataTable();
			OracleAccess access=new OracleAccess();
			string sql = "select train_class_id from zj_train_class where train_plan_post_class_id=" + classID;
			DataTable dtClass= access.RunSqlDataSet(sql).Tables[0];    //�Ƿ���ʵ�ʰ�
			if (dtClass.Rows.Count > 0)
			{
				sql = " select train_class_subject_id from zj_train_class_subject where train_class_id=" + dtClass.Rows[0][0];
				dtSubject = access.RunSqlDataSet(sql).Tables[0]; //�Ƿ��п�Ŀ
				sql = " select employee_id from zj_train_plan_employee where train_class_id=" + dtClass.Rows[0][0];
				dtEmp = access.RunSqlDataSet(sql).Tables[0]; //�Ƿ���ѧԱ
			}
			try
			{
				if (dtClass.Rows.Count > 0)      //��ʵ�ʰ༶
				{

					if (dtSubject.Rows.Count > 0)     //�п�Ŀ
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
		/// �жϸð��Ƿ��гɼ�
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
					ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('" + oldClassName.Value + "���ڿ��ԣ������޸ģ�')", true);
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
				//����ְ��
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
