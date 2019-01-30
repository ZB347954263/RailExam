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
using System.Text;
using System.Collections.Generic;

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGridClassOrg(Convert.ToInt32(Request.QueryString.Get("planID")), PrjPub.CurrentLoginUser.StationOrgID);
                GetAllEmp(Convert.ToInt32(Request.QueryString.Get("planID")));
            }
        }

        protected void btnDropEmp_Click(object sender, EventArgs e)
        {
             DropEmp();

            //更新实际上报人数
            UpdatePostClassOrgNum(Convert.ToInt32(hfOrgID.Value),0);
            UpdatePostClassNum(Convert.ToInt32(hfClassID.Value),
                               GetPostClassNum(Convert.ToInt32(hfClassID.Value)));
            UpdatePostNum(GetPostIDByClassID(Convert.ToInt32(hfClassID.Value)),
                          GetPostNum(Convert.ToInt32(hfClassID.Value)));

            GetAllEmp(Convert.ToInt32(Request.QueryString.Get("planID")));
            BindGridClassOrg(Convert.ToInt32(Request.QueryString.Get("planID")), PrjPub.CurrentLoginUser.StationOrgID);

            DeleteAllResultByClassID(Convert.ToInt32(hfClassID.Value));
        }

        protected void btnUpdateEmp_Click(object sender,EventArgs e)
        {
			int num = InsertEmp();

			//更新实际上报人数
			UpdatePostClassOrgNum(Convert.ToInt32(hfOrgID.Value), num);
			UpdatePostClassNum(Convert.ToInt32(hfClassID.Value),
								GetPostClassNum(Convert.ToInt32(hfClassID.Value)));
			UpdatePostNum(GetPostIDByClassID(Convert.ToInt32(hfClassID.Value)),
						  GetPostNum(Convert.ToInt32(hfClassID.Value)));
			////////////////////////
			//更新实际班级的学员
			UpdateClassEmpByPostClassEmp(Convert.ToInt32(hfClassID.Value));

			GetAllEmp(Convert.ToInt32(Request.QueryString.Get("planID")));
			BindGridClassOrg(Convert.ToInt32(Request.QueryString.Get("planID")), PrjPub.CurrentLoginUser.StationOrgID);

			DeleteAllResultByClassID(Convert.ToInt32(hfClassID.Value));
			InsertResult(Convert.ToInt32(hfClassID.Value));
        }

        /// <summary>
        /// 绑定培训班站段
        /// </summary>
        private void BindGridClassOrg(int planID,int orgID)
        {
            OracleAccess access = new OracleAccess();
            StringBuilder str = new StringBuilder();
            str.Append("select o.* ,p.train_plan_post_id,getpostnamebypostid(p.post_ids) postName,c.train_plan_post_class_id,c.class_name,org.full_name,c.begin_date,c.end_date from  zj_train_plan_post_class_org o ");
            str.Append("left join zj_train_plan_post_class c on c.train_plan_post_class_id=o.train_plan_post_class_id ");
            str.Append(" left join zj_train_plan_post p on p.train_plan_post_id=c.train_plan_post_id ");
            str.Append("left join org on org.org_id=o.org_id where o.train_plan_post_class_id in ");
            str.Append(" (select train_plan_post_class_id from zj_train_plan_post_class where ");
            str.AppendFormat(" train_plan_id={0}) and o.org_id={1} order by c.train_plan_post_id, c.train_plan_post_class_id", planID, orgID);
            DataTable dt = access.RunSqlDataSet(str.ToString()).Tables[0];
            dt.Columns.Add("begin_date1", typeof(string));
            dt.Columns.Add("end_date1", typeof(string));
            dt.Columns.Add("canUp", typeof (string));
        	dt.Columns.Add("link", typeof (string));
        	dt.Columns.Add("isHasExam", typeof (string));
            foreach (DataRow r in dt.Rows)
            {
                r["begin_date1"] = Convert.ToDateTime(r["begin_date"]).ToString("yyyy-MM-dd");
                r["end_date1"] = Convert.ToDateTime(r["end_date"]).ToString("yyyy-MM-dd");
                if (Convert.ToDateTime(r["end_date"]).AddDays(-2) >= DateTime.Today)
                    r["canUp"] = 1;
                else
                    r["canUp"] = 0;
				if (IsHasExamByClassID(Convert.ToInt32(r["train_plan_post_class_id"])) == 1)
					r["isHasExam"] = 1;
				else
					r["isHasExam"] = 0;
				r["link"] = "<a href='#' title='显示学员信息' onclick='LinkEmp(" + r["train_plan_post_class_org_id"] + ")'>&nbsp;" + r["last_employee_number"] + "&nbsp;</a>";
            }



            grdClassOrg.DataSource = dt;
            grdClassOrg.DataBind();
        }

        /// <summary>
        /// 获取该计划的学员ID给隐藏控件
        /// </summary>
        /// <param name="planID"></param>
        private void GetAllEmp(int planID)
        {
            OracleAccess access=new OracleAccess();
            string sql = " select  employee_id from zj_train_plan_employee where train_plan_id=" + planID;
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];
            if(dt!=null && dt.Rows.Count>0)
            {
                List<string> lst=new List<string>();
                foreach (DataRow r in dt.Rows)
                {
                    lst.Add(r["employee_id"].ToString());
                }
                hfAllEmpID.Value = string.Join(",", lst.ToArray());
            }
        }

        /// <summary>
        /// 新增学员
        /// </summary>
        private int InsertEmp()
        {
            List<string> lstOld = new List<string>();
            List<string> newEmpID = new List<string>();
            try
            {
                string[] employeeIDs = hfSelectEmpID.Value.Split(',');

				//string[] OldEmpIDs = hfAllEmpID.Value.Split(',');
               
				//foreach (string str in OldEmpIDs)
				//{
				//    lstOld.Add(str);
				//}
                foreach (string s in employeeIDs)
                {
                  //  if(!lstOld.Contains(s))
                        newEmpID.Add(s);
                }

				if (newEmpID.Count > 0)
				{
					string sql = " declare p_count number; begin ";
					foreach (string employeeID in newEmpID)
					{
						if (employeeID.Length != 0)
						{
							sql +=
								string.Format(
									" select count(1) into p_count from ZJ_TRAIN_PLAN_EMPLOYEE where TRAIN_PLAN_ID={0} and EMPLOYEE_ID={1} and TRAIN_PLAN_POST_CLASS_ID={2};if  p_count =0 then insert into ZJ_TRAIN_PLAN_EMPLOYEE(TRAIN_PLAN_EMPLOYEE_ID,TRAIN_PLAN_ID,EMPLOYEE_ID,train_plan_post_class_id,TRAIN_PLAN_POST_CLASS_ORG_ID) values(Train_Plan_Employee_SEQ.NextVal,{0},{1},{2},{3}); end if;",
									Convert.ToInt32(Request.QueryString.Get("planID")), employeeID, Convert.ToInt32(hfClassID.Value),
									Convert.ToInt32(hfOrgID.Value));
						}
					}
					sql += " end;";
					OracleAccess oracleAccess = new OracleAccess();
					oracleAccess.ExecuteNonQuery(sql);
					//ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('学员添加成功！')", true);
				}

            }
            catch (Exception)
            {
               ClientScript.RegisterClientScriptBlock(GetType(),"","alert('学员添加失败！')",true);
            }
            /////// 通过orgid 查询所对应的人数加上新加的人数
            OracleAccess acc=new OracleAccess();
			//int Num =Convert.ToInt32(
			//    acc.RunSqlDataSet(
			//        "select last_employee_number from zj_train_plan_post_class_org where train_plan_post_class_org_id=" +
			//        Convert.ToInt32(hfOrgID.Value)).Tables[0].Rows[0][0]);
			int Num = Convert.ToInt32(
							acc.RunSqlDataSet(
								"select count(1) from zj_train_plan_employee where train_plan_post_class_org_id=" +
								Convert.ToInt32(hfOrgID.Value)).Tables[0].Rows[0][0]);
          
            //return Num + newEmpID.Count;
        	return Num;
        }

        /// <summary>
        /// 移除学员
        /// </summary>
        private void DropEmp()
        {
            try
            {
                OracleAccess access = new OracleAccess();
            string sql =
                string.Format(
                    "delete from zj_train_plan_employee where train_plan_id={0} and  train_plan_post_class_id={1} and TRAIN_PLAN_POST_CLASS_ORG_ID={2}",
                    Convert.ToInt32(Request.QueryString.Get("planID")), Convert.ToInt32(hfClassID.Value),Convert.ToInt32(hfOrgID.Value));
                access.ExecuteNonQuery(sql);
              //  ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('学员删除成功！')", true);
                BindGridClassOrg(Convert.ToInt32(Request.QueryString.Get("planID")), PrjPub.CurrentLoginUser.StationOrgID);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('学员更新失败！')", true);
            }

        }

        /// <summary>
        /// 更新培训计划站段中的实际上报人数
        /// </summary>
        private void UpdatePostClassOrgNum(int orgID,int Num)
        {
            try
            {
      string sqlUpdate =
                string.Format(
                    "update zj_train_plan_post_class_org set last_employee_number={0} where train_plan_post_class_org_id={1}",
                    Num, orgID);
            OracleAccess access=new OracleAccess();
                access.ExecuteNonQuery(sqlUpdate);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('计划站段中人数更新失败！')", true);
            }
       
        }

        /// <summary>
        /// 更新培训计划班中的实际上报人数
        /// </summary>
        private void UpdatePostClassNum(int classID,int Num)
        {
            try
            {
                string sql =
               string.Format(
                   "update  zj_train_plan_post_class set last_employee_number={0}  where  train_plan_post_class_id={1}",
                   Num, classID);
                OracleAccess access = new OracleAccess();
                access.ExecuteNonQuery(sql);

            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('计划培训班中人数更新失败！')", true);
            }
           
        }

        /// <summary>
        /// 更新培训职名中的实际上报人数
        /// </summary>
        private void UpdatePostNum(int postID,int Num)
        {
            try
            {
                string sql =
               string.Format(
                   "update zj_train_plan_post set last_employee_number={0} where train_plan_post_id={1}",
                   Num, postID);
                OracleAccess access = new OracleAccess();
                access.ExecuteNonQuery(sql);

            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('计划职名中人数更新失败！')", true);
            }
        }

        /// <summary>
        /// 获取某一培训班的人数
        /// </summary>
        /// <param name="classID"></param>
        /// <returns></returns>
        private int GetPostClassNum(int classID)
        {
            int num = 0;
            OracleAccess access=new OracleAccess();
            string sql =
                string.Format(
                    "select sum(last_employee_number) allNum from zj_train_plan_post_class_org where train_plan_post_class_id={0}",classID);
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];
            if (dt != null && dt.Rows[0][0].ToString().Length > 0)
                num = Convert.ToInt32(dt.Rows[0][0]);
            return num;
        }

        /// <summary>
        /// 获取计划职名中的人数
        /// </summary>
        /// <param name="classID"></param>
        /// <returns></returns>
        private int GetPostNum(int classID)
        {
            string sql = "select sum(last_employee_number) allNum from zj_train_plan_post_class where train_plan_post_id";
            sql += "= ( select  train_plan_post_id  from zj_train_plan_post_class where train_plan_post_class_id="+classID+")";
            OracleAccess access=new OracleAccess();
            DataTable dt= access.RunSqlDataSet(sql).Tables[0];
            int num = 0;
            if (dt != null && dt.Rows[0][0].ToString().Length > 0)
                num = Convert.ToInt32(dt.Rows[0][0]);
            return num;
        }

        /// <summary>
        /// 通过培训班获取职名ID
        /// </summary>
        /// <param name="classID"></param>
        /// <returns></returns>
        private int GetPostIDByClassID(int classID)
        {
            string sql = "select train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id="+classID;
            DataTable dt = new OracleAccess().RunSqlDataSet(sql).Tables[0];
            int postID = 0;
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Length > 0)
                postID = Convert.ToInt32(dt.Rows[0][0]);
            return postID;
        }

        /// <summary>
        /// 通过计划培训班的学员，更新实际培训班的学员
        /// </summary>
        private void UpdateClassEmpByPostClassEmp(int postClassID)
        {
            OracleAccess  access=new OracleAccess();
            string sql = "select *  from zj_train_class where train_plan_post_class_id=" + postClassID;
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];
            if(dt!=null)
            {
                if(dt.Rows.Count>0)
                {
                    //该计划培训班对应的有实际班
                    sql =
                        "update zj_train_plan_employee set train_class_id=" + dt.Rows[0]["train_class_id"] + " where train_plan_post_class_id=" +
                        postClassID;
                    access.ExecuteNonQuery(sql);
                }
            }
        }

        /// <summary>
        /// 删除该班对应的所有科目结果
        /// </summary>
        /// <param name="classID"></param>
        private void DeleteAllResultByClassID(int classID)
        {
            try
            {
                  OracleAccess access=new OracleAccess();
            access.ExecuteNonQuery(
                "delete from zj_train_class_subject_result where train_class_id in (select distinct train_class_id from zj_train_class where train_plan_post_class_id=" +
                classID + ") ");
            }
            catch
            {
            }
        }

        private void InsertResult(int classID)
        {
            OracleAccess access = new OracleAccess();

            try
            {
               
                DataTable dtClass =
                    access.RunSqlDataSet(
                        "select distinct train_class_id from zj_train_class where train_plan_post_class_id=" + classID + " and train_class_id is not null").
                        Tables[0];    //实际班级
                foreach (DataRow rc in dtClass.Rows)
                {
                    access.ExecuteNonQuery("delete from zj_train_class_subject_result where train_class_id=" + Convert.ToInt32(rc[0]));
                    StringBuilder sqlInsert = new StringBuilder();
                    DataTable dtSub = GetSubjectByClassID(Convert.ToInt32(rc[0]));
                    foreach (DataRow sr in dtSub.Rows)
                    {
                        DataTable dtEmp = GetEmpByClassID(classID);
                        if (dtEmp.Rows.Count > 0)
                        {
                            sqlInsert.Append("begin ");
                            foreach (DataRow r in dtEmp.Rows)
                            {
                                sqlInsert.Append(" insert into zj_train_class_subject_result(train_class_subject_result_id, ");
                                sqlInsert.Append(" train_class_id,train_class_subject_id,employee_id,exam_date,result,ispass)");
                                sqlInsert.AppendFormat(" values (TRAIN_CLASS_SUBJECT_RESULT_SEQ.nextval,{0},{1},{2},null,null,0);",
                                                       Convert.ToInt32(rc[0]), sr["train_class_subject_id"],
                                                       Convert.ToInt32(r[0]));
                            }
                            sqlInsert.Append(" end; ");
                            access.ExecuteNonQuery(sqlInsert.ToString());
                            sqlInsert.Length = 0;
                        }
                    }
                }
               
            }
            catch (Exception)
            {

            }
        }
        private DataTable GetEmpByClassID(int classID)
        {
            OracleAccess access = new OracleAccess();
            return access.RunSqlDataSet(" select employee_id from zj_train_plan_employee where train_plan_post_class_id=" + classID).Tables[0];
        }
        private DataTable GetSubjectByClassID(int classID)
        {
            OracleAccess access = new OracleAccess();
            return access.RunSqlDataSet(" select * from zj_train_class_subject where train_class_id=" + classID).Tables[0];
        }

		protected void btnRef_Click(object sender, EventArgs e)
		{
			BindGridClassOrg(Convert.ToInt32(Request.QueryString.Get("planID")), PrjPub.CurrentLoginUser.StationOrgID);
			GetAllEmp(Convert.ToInt32(Request.QueryString.Get("planID")));
		}

		/// <summary>
		/// 判断是否存在该培训班的考试
		/// </summary>
		/// <param name="classID"></param>
		/// <returns></returns>
		private int IsHasExamByClassID(int postClassID)
		{
			OracleAccess access=new OracleAccess();
			string sql = @"
                    select count(*) from random_exam_train_class where train_class_id
				 in (select distinct train_class_id from zj_train_class where train_plan_post_class_id="+postClassID+")";
			DataSet ds = access.RunSqlDataSet(sql);
			if(ds!=null)
			{
				if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
					return 1;
			}
			return 0;
		}
    }
}
