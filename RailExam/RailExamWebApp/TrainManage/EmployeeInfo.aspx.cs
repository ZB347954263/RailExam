using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
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
    public partial class EmployeeInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                hfID.Value = Request.QueryString.Get("classOrgID");
				BindGridStudent();

                string strSql = "select * from ZJ_Train_Plan_Post_Class_Org where Train_Plan_Post_Class_Org_ID= " +
                                hfID.Value;
                OracleAccess db = new OracleAccess();
                string strClassID = db.RunSqlDataSet(strSql).Tables[0].Rows[0]["Train_Plan_Post_Class_ID"].ToString();

                HasExam.Value = IsHasExamByClassID(Convert.ToInt32(strClassID)).ToString();

                if (PrjPub.HasDeleteRight("培训计划") && PrjPub.IsServerCenter)
                {
                    btnDelAll.Enabled = true;
                    grdEntity.Levels[0].Columns[10].Visible = true;
                }
                else
                {
                    btnDelAll.Enabled = false;
                    grdEntity.Levels[0].Columns[10].Visible = false;
                }
            }

            string strRefresh = Request.Form.Get("hfRefresh");
            if (!string.IsNullOrEmpty(strRefresh))
            {
                DownloadExcel(strRefresh);
            }
        }

        private void DownloadExcel(string strName)
        {
            string[] str = strName.Split('|');
            string filename = Server.MapPath("/RailExamBao/Excel/" + str[0] + ".xls");
            if (File.Exists(filename))
            {
                FileInfo file = new FileInfo(filename.ToString());
                this.Response.Clear();
                this.Response.Buffer = true;
                this.Response.Charset = "utf-7";
                this.Response.ContentEncoding = Encoding.UTF7;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(str[1]) + ".xls");
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                this.Response.AddHeader("Content-Length", file.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                this.Response.ContentType = "application/ms-excel";
                // 把文件流发送到客户端
                this.Response.WriteFile(file.FullName);
            }
        }

        private DataTable BindGridStudent()
		{
			OracleAccess access = new OracleAccess();
			DataTable dt =
				access.RunSqlDataSet(
                    "select getorgname(E.Org_Id),V.*,e.Work_No from ZJ_Train_Plan_Employee_View V left join Employee E on E.Employee_Id=V.employee_id where train_plan_post_class_org_id=" + Request.QueryString.Get("classOrgID") + " order by nlssort(V.employee_name,'NLS_SORT=SCHINESE_PINYIN_M')").Tables[0];
			dt.Columns.Add("unit", typeof(string));
			dt.Columns.Add("workshop", typeof(string));
			dt.Columns.Add("group", typeof(string));
            dt.Columns.Add("isHasExam", typeof(string));
			foreach (DataRow r in dt.Rows)
			{
				string[] strArr = r["getorgname(E.Org_Id)"].ToString().Split('-');
				if (strArr.Length > 0)
					r["unit"] = strArr[0];
				if (strArr.Length > 1)
					r["workshop"] = strArr[1];
				if (strArr.Length > 2)
					r["group"] = strArr[2];
			}
			grdEntity.DataSource = dt;
			grdEntity.DataBind();
			return dt;
		}

        private int IsHasExamByClassID(int postClassID)
        {
            OracleAccess access = new OracleAccess();
            string sql = @"
                    select count(*) from random_exam_train_class where train_class_id
				 in (select distinct train_class_id from zj_train_class where train_plan_post_class_id=" + postClassID + ")";
            DataSet ds = access.RunSqlDataSet(sql);
            if (ds != null)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                    return 1;
            }
            return 0;
        }

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			hfIsRef.Value = "true";
			
			UpdatePostClassOrgNum(hfplanEmpID.Value); //更新培训站段的实际人数
			UpdatePostClassNum(hfplanEmpID.Value);    //更新培训计划班中的实际上报人数
			UpdatePostNum(hfplanEmpID.Value);         //更新培训职名中的实际上报人数
			DeleteAllResultByEmpID(hfplanEmpID.Value); //删除该学员对应的科目结果
			DropEmp(hfplanEmpID.Value);                //删除学员
		}
		protected void btnDelAll_Click(object sender, EventArgs e)
		{
			hfIsRef.Value = "true";

			//循环更新
			string[] arrplanEmpIDs = hfSelectedIDs.Value.Split(',');
			if(arrplanEmpIDs.Length>0)
			{
				foreach (string id in arrplanEmpIDs)
				{
					UpdatePostClassOrgNum(id);    //更新培训站段的实际人数
					UpdatePostClassNum(id);       //更新培训计划班中的实际上报人数
					UpdatePostNum(id);            //更新培训职名中的实际上报人数
					DeleteAllResultByEmpID(id);   //删除该学员对应的科目结果
					DropEmp(id);                  //删除学员
				}
			}
		}

		/// <summary>
		/// 删除所选择的学员
		/// </summary>
		private void DropEmp(string planEmpID)
		{
			try
			{
				OracleAccess access = new OracleAccess();
				string sql = "delete from zj_train_plan_employee where TRAIN_PLAN_EMPLOYEE_ID=" + planEmpID;
				access.ExecuteNonQuery(sql);
                //ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('学员删除成功！')", true);
				BindGridStudent();
			}
			catch (Exception)
			{
				ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('学员更新失败！')", true);
			}

		}


		/// <summary>
		/// 更新培训计划站段中的实际上报人数
		/// </summary>
		private void UpdatePostClassOrgNum(string planEmpID)
		{
			try
			{
				string sqlUpdate =
						  string.Format(
							  @"update zj_train_plan_post_class_org set last_employee_number=last_employee_number-1 where train_plan_post_class_org_id
                               in (select TRAIN_PLAN_POST_CLASS_ORG_ID from zj_train_plan_employee where TRAIN_PLAN_EMPLOYEE_ID=" + planEmpID + ")");
				OracleAccess access = new OracleAccess();
				access.ExecuteNonQuery(sqlUpdate);
			}
			catch
			{}
		}

		/// <summary>
		/// 更新培训计划班中的实际上报人数
		/// </summary>
		private void UpdatePostClassNum(string planEmpID)
		{
			try
			{
				string sql =string.Format(@"
               update  zj_train_plan_post_class set last_employee_number=
                 (select sum(last_employee_number) from zj_train_plan_post_class_org where train_plan_post_class_id=
                     (select TRAIN_PLAN_POST_CLASS_ID from zj_train_plan_employee where TRAIN_PLAN_EMPLOYEE_ID={0})
                 )
                where  train_plan_post_class_id
               in (select TRAIN_PLAN_POST_CLASS_ID from zj_train_plan_employee where TRAIN_PLAN_EMPLOYEE_ID={0})", planEmpID);
			   
				OracleAccess access = new OracleAccess();
				access.ExecuteNonQuery(sql);

			}
			catch  
			{
 			}

		}


		/// <summary>
		/// 更新培训职名中的实际上报人数
		/// </summary>
		private void UpdatePostNum(string planEmpID)
		{
			try
			{
				string sql = string.Format(@"
                                    update zj_train_plan_post set last_employee_number=
                ( select sum(last_employee_number) from zj_train_plan_post_class where train_plan_post_id =
                    (
                          select distinct train_plan_post_id  from zj_train_plan_post_class where train_plan_post_class_id=
                           (select train_plan_post_class_id from zj_train_plan_employee where TRAIN_PLAN_EMPLOYEE_ID={0})
                     )
                   )
              
                where train_plan_post_id in
				(select train_plan_post_id from  zj_train_plan_post_class where train_plan_post_class_id in
				  (select train_plan_post_class_id from zj_train_plan_employee where TRAIN_PLAN_EMPLOYEE_ID={0}))", planEmpID);
				OracleAccess access = new OracleAccess();
				access.ExecuteNonQuery(sql);

			}
			catch  
			{
 			}
		}

		/// <summary>
		/// 删除该学员对应的科目结果
		/// </summary>
		/// <param name="planEmpID"></param>
		private void DeleteAllResultByEmpID(string planEmpID)
		{
			try
			{
				OracleAccess access = new OracleAccess();
				string sql =
					string.Format(
						@"delete from zj_train_class_subject_result 
		    	 where TRAIN_CLASS_ID in
		    	 (select TRAIN_CLASS_ID from zj_train_plan_employee where TRAIN_PLAN_EMPLOYEE_ID={0})
			    and EMPLOYEE_ID in (select EMPLOYEE_ID from zj_train_plan_employee where TRAIN_PLAN_EMPLOYEE_ID={0})",
						planEmpID);
				access.ExecuteNonQuery(sql);
			}
			catch
			{
			}
		}

    }
}
