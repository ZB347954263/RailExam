using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using RailExam.BLL;
using RailExamWebApp.Common.Class;
using DSunSoft.Web.UI;
using DSunSoft.Web.Global;

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanAdd : PageBase
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                hfSelect.Value = GetSql();
                OracleAccess oracleAccess = new OracleAccess();
                DataSet ds1 = oracleAccess.RunSqlDataSet("select * from ZJ_TRAINPLAN_TYPE");
                ddlTrainPlanType.DataSource = ds1.Tables[0].DefaultView;
                ddlTrainPlanType.DataTextField = "TRAINPLAN_TYPE_NAME";
                ddlTrainPlanType.DataValueField = "TRAINPLAN_TYPE_ID";
                ddlTrainPlanType.DataBind();
                //培训项目
                BindDropProject();
                //主办单位
                oracleAccess = new OracleAccess();
                DataSet ds2 = oracleAccess.RunSqlDataSet("select * from ORG where Level_Num=2 and Is_Effect=1  order by Order_Index");
                ddlZhuBan.DataSource = ds2.Tables[0].DefaultView;
                ddlZhuBan.DataTextField = "SHORT_NAME";
                ddlZhuBan.DataValueField = "ORG_ID";
                ddlZhuBan.DataBind();
            	ddlZhuBan.Enabled = false;
            	ddlZhuBan.SelectedValue =PrjPub.CurrentLoginUser.StationOrgID.ToString();
                //承办单位
                ddlChengBan.DataSource = ds2.Tables[0].DefaultView;
                ddlChengBan.DataTextField = "SHORT_NAME";
                ddlChengBan.DataValueField = "ORG_ID";
                ddlChengBan.DataBind();
                ddlChengBan.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();

                if (string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    hfID.Value = "";
                    lblYear.Text = DateTime.Now.Year.ToString();
                    lblPerson.Text = PrjPub.CurrentLoginUser.EmployeeName;
                    dateMake.DateValue = DateTime.Now.ToString("yyyy-MM-dd");
                    hfOrgIDs.Value = "";
                }
                else
                {
                    hfID.Value = Request.QueryString["ID"];
                    oracleAccess = new OracleAccess();
                    DataSet ds = oracleAccess.RunSqlDataSet(string.Format("select * from zj_train_plan where train_plan_id={0}", Request.QueryString["ID"]));
                    lblYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                    txtTrainPlanName.Text = ds.Tables[0].Rows[0]["TRAIN_PLAN_NAME"].ToString();
                    ddlTrainPlanType.SelectedValue = ds.Tables[0].Rows[0]["train_plan_type_id"].ToString();
                    txtLoation.Text = ds.Tables[0].Rows[0]["LOCATION"].ToString();
                    ddlZhuBan.SelectedValue = ds.Tables[0].Rows[0]["SPONSOR_UNIT_ID"].ToString();
                    ddlChengBan.SelectedValue = ds.Tables[0].Rows[0]["UNDERTAKE_UNIT_ID"].ToString();
                    txtXieBan.Text = ds.Tables[0].Rows[0]["ASSIST_UNIT"].ToString();
                    dateBegin.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["BEGINDATE"]).ToString("yyyy-MM-dd");
                    dateEnd.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"]).ToString("yyyy-MM-dd");
                    dateMake.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["MAKEDATE"]).ToString("yyyy-MM-dd");
                    hfOrgIDs.Value = ds.Tables[0].Rows[0]["OrgIDs"].ToString();
                    BindDropProject();
                    dropProject.SelectedValue = ds.Tables[0].Rows[0]["train_plan_project_id"].ToString();
                    if (ds.Tables[0].Rows[0]["has_post"].ToString() == "1")
                        chkHasPost.Checked = true;

                    //当主办单位和承办单位都不等于当前登录单位时
                    //if (ddlZhuBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                    //    && ddlChengBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString())
                    //{
                    //    SixPage.Visible = false;
                    //}

                    ds = oracleAccess.RunSqlDataSet(string.Format("select Employee_Name from Employee where Employee_ID={0}", ds.Tables[0].Rows[0]["MAKERID"]));
                    lblPerson.Text = ds.Tables[0].Rows[0][0].ToString();
                    //判断是否需要职名
                    IsHasPost(Convert.ToInt32(hfID.Value));
                    //绑定职名
                    BindgrdPost();
                    //绑定培训班
                    BindGrdClass();
                    //绑定培训班站段
                    BindGridClassOrg();

                    LoadTable();
                }
                if (hfID.Value.Length != 0)
                {
                    DataTable dt = BindGridStudent();
                    GetAllEmpIDTohf(dt);
                }
                if (hfOrgIDs.Value.Length != 0)
                {
                    string[] orgIDs = hfOrgIDs.Value.Split(',');
                    string sql = string.Empty;
                    foreach (string orgID in orgIDs)
                    {
                        if (orgID.Length != 0)
                        {
                            sql += sql.Length == 0 ? orgID : "," + orgID;
                        }
                    }

                    sql = string.Format("select SHORT_NAME from Org where Org_ID in({0})", sql);
                    oracleAccess = new OracleAccess();
                    DataSet ds = oracleAccess.RunSqlDataSet(sql);
                    string short_Names = string.Empty;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        short_Names += short_Names.Length == 0 ? row["SHORT_NAME"] : "," + row["SHORT_NAME"];
                    }

                    txtOrgIDs.Text = short_Names;
                }
                //判断是否只有查看的功能
                DisenableAdd();
            }
        }

        private string GetSql()
        {
            return "select * from ZJ_Train_Plan_Employee_View";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime beginDate = Convert.ToDateTime(dateBegin.DateValue);
            DateTime endDate = Convert.ToDateTime(dateEnd.DateValue);
            if (endDate < beginDate)
            {
                SessionSet.PageMessage = "开班日期不能大于结束日期！";
                return;
            }
 
            if (hfID.Value == "")
              hfID.Value=  AddInfo();
            else
                UpdateInfo();
            if (hfID.Value == "")
                return;
            IsHasPost(Convert.ToInt32(hfID.Value));

            LoadTable();
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <returns></returns>
        private string AddInfo()
        {
            int count = 0;
            string sqlSelect = "select count(*) from ZJ_TRAIN_PLAN where TRAIN_PLAN_NAME='"+txtTrainPlanName.Text.Trim()+"'";
            DataSet dsCount = new OracleAccess().RunSqlDataSet(sqlSelect);
            if(dsCount!=null && dsCount.Tables[0]!=null)
            {
                if(Convert.ToInt32(dsCount.Tables[0].Rows[0][0])>0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('该计划名称在系统中已存在，请重新输入！')", true);
                    count=1;
                }
            }

            string NewID = "";
            if (count == 0)
            {
                string sql =
                    string.Format(
                        "insert into ZJ_TRAIN_PLAN(TRAIN_PLAN_ID,YEAR,TRAIN_PLAN_TYPE_ID,TRAIN_PLAN_NAME,LOCATION,BEGINDATE,ENDDATE,MAKEDATE,MAKERID,TRAIN_PLAN_PHASE_ID,SPONSOR_UNIT_ID,UNDERTAKE_UNIT_ID,ASSIST_UNIT,ORGIDS,train_plan_project_id,has_post) values({0},{1},{2},'{3}','{4}',TO_DATE ('{5}', 'YYYY-mm-dd'),TO_DATE ('{6}', 'YYYY-mm-dd'),TO_DATE ('{7}', 'YYYY-mm-dd'),{8},1,{9},{10},'{11}','{12}',{13},{14})"
                        , "TRAIN_PLAN_SEQ.NextVal", DateTime.Now.Year, ddlTrainPlanType.SelectedValue,
                        txtTrainPlanName.Text, txtLoation.Text, dateBegin.DateValue, dateEnd.DateValue,
                        DateTime.Now.ToString("yyyy-MM-dd"), PrjPub.CurrentLoginUser.EmployeeID, ddlZhuBan.SelectedValue,
                        ddlChengBan.SelectedValue, txtXieBan.Text, hfOrgIDs.Value,
                        dropProject.SelectedValue == "0" ? "null" : dropProject.SelectedValue,
                        chkHasPost.Checked == true ? 1 : 0);
                try
                {

                    OracleAccess oracleAccess = new OracleAccess();
                    oracleAccess.ExecuteNonQuery(sql);
                    oracleAccess = new OracleAccess();
                    string sqlS = "select MAx(TRAIN_PLAN_ID) from ZJ_TRAIN_PLAN";
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存成功！')", true);
                    NewID = oracleAccess.RunSqlDataSet(sqlS).Tables[0].Rows[0][0].ToString();
                }
                catch (Exception)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);
                }
            }

            return NewID;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void UpdateInfo()
        {
            int count = 0;
            string sqlSelect = "select count(*) from ZJ_TRAIN_PLAN where TRAIN_PLAN_NAME='" + txtTrainPlanName.Text.Trim() + "'  and TRAIN_PLAN_ID!=" + hfID.Value;
            DataSet dsCount = new OracleAccess().RunSqlDataSet(sqlSelect);
            if (dsCount != null && dsCount.Tables[0] != null)
            {
                if (Convert.ToInt32(dsCount.Tables[0].Rows[0][0]) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('该计划名称在系统中已存在，请重新输入！')", true);
                    count = 1;
                }
            }

            if (count == 0)
            {
                string sql = string.Format(
                    "update ZJ_TRAIN_PLAN set YEAR={1},TRAIN_PLAN_TYPE_ID={2},TRAIN_PLAN_NAME='{3}',LOCATION='{4}',BEGINDATE=TO_DATE ('{5}', 'YYYY-mm-dd'),ENDDATE=TO_DATE ('{6}', 'YYYY-mm-dd'),SPONSOR_UNIT_ID={8},UNDERTAKE_UNIT_ID={9},ASSIST_UNIT='{10}',ORGIDS='{11}',train_plan_project_id={12},has_post={13} where train_plan_id={14}"
                    , "", DateTime.Now.Year, ddlTrainPlanType.SelectedValue, txtTrainPlanName.Text, txtLoation.Text,
                    Convert.ToDateTime(dateBegin.DateValue).ToShortDateString(),
                    Convert.ToDateTime(dateEnd.DateValue).ToShortDateString(),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ddlZhuBan.SelectedValue, ddlChengBan.SelectedValue,
                    txtXieBan.Text, hfOrgIDs.Value,
                    dropProject.SelectedValue == "0" ? "null" : dropProject.SelectedValue,
                    chkHasPost.Checked == true ? 1 : 0, hfID.Value);
                try
                {

                    OracleAccess oracleAccess = new OracleAccess();
                    oracleAccess.ExecuteNonQuery(sql);
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据更新成功！')", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据更新失败！')", true);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainPlanList.aspx");
        }
        protected void btnUpDate_Click(object sender, EventArgs e)
        {
            string[] employeeIDs = Request.Form["__EVENTARGUMENT"].Split(',');
            string sql = " declare p_count number; begin ";
            foreach (string employeeID in employeeIDs)
            {
                if (employeeID.Length != 0)
                {
                    sql += string.Format(" select count(1) into p_count from ZJ_TRAIN_PLAN_EMPLOYEE where TRAIN_PLAN_ID={0} and EMPLOYEE_ID={1};if  p_count =0 then insert into ZJ_TRAIN_PLAN_EMPLOYEE(TRAIN_PLAN_EMPLOYEE_ID,TRAIN_PLAN_ID,EMPLOYEE_ID) values(Train_Plan_Employee_SEQ.NextVal,{0},{1}); end if;", hfID.Value, employeeID);
                }
            }
            sql += " end;";
            OracleAccess oracleAccess = new OracleAccess();
            oracleAccess.ExecuteNonQuery(sql);
            string argument = Request.Form["__EVENTARGUMENT"];
           
            DataTable dt = BindGridStudent();
            GetAllEmpIDTohf(dt);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string argument = Request.Form["__EVENTARGUMENT"];
          

           
            int postClassID = GetPostClassIDByEmpID(Convert.ToInt32(argument));
            int postClassOrgID = GetPostClassOrgIDByEmpID(Convert.ToInt32(argument));

        	try
        	{
             DeleteResultByTrainEmpID(argument);   //删除科目结果

            OracleAccess oracleAccess = new OracleAccess();
            oracleAccess.ExecuteNonQuery("delete from ZJ_Train_Plan_Employee where TRAIN_PLAN_EMPLOYEE_ID=" + argument);
            //ClientScript.RegisterClientScriptBlock(GetType(),"","alert('学员删除成功！')",true);
           DataTable dt= BindGridStudent();
           GetAllEmpIDTohf(dt);


            ///////////////////更新实际人数
            UpdatePostClassOrgNum(postClassOrgID, GetPostClassOrgNum(postClassOrgID));
            UpdatePostClassNum(postClassID, GetPostClassNum(postClassID));
            UpdatePostLastNum(GetPostIDByEmpID(postClassID), GetPostNum(postClassID));
        	}
        	catch  
        	{
				ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('学员删除失败！')", true);

        	}

           

           

            BindgrdPost();
            BindGrdClass();
            BindGridClassOrg();
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string[] orgIDs = Request.Form["__EVENTARGUMENT"].Split(',');
            string sql = string.Empty;
            foreach (string orgID in orgIDs)
            {
                if (orgID.Length != 0)
                {
                    sql += sql.Length == 0 ? orgID : "," + orgID;
                }
            }

            sql = string.Format("select SHORT_NAME from Org where Org_ID in({0})", sql);
            OracleAccess oracleAccess = new OracleAccess();
            DataSet ds = oracleAccess.RunSqlDataSet(sql);
            string short_Names = string.Empty;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                short_Names += short_Names.Length == 0 ? row["SHORT_NAME"] : "," + row["SHORT_NAME"];
            }

            txtOrgIDs.Text = short_Names;
            hfOrgIDs.Value = Request.Form["__EVENTARGUMENT"];
        }
        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
            //    {
            //        e.Row.Visible = false;
            //    }
            //    else
            //    {
            //        e.Row.Attributes.Add("onclick", "selectArow('" + e.Row.RowIndex + "');");
            //    }
            //}
        }

        protected void grdEntity_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
            if (db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["TRAIN_PLAN_ID"] = -1;
                db.Rows.Add(row);
            }
        }

        private void GetAllEmpIDTohf(DataTable dt)
        {
            List<string> lst=new List<string>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(r["EMPLOYEE_ID"].ToString());
                
            }
            hfAllEmpID.Value = string.Join(",", lst.ToArray());
        }

        protected  void ddlTrainPlanType_SelectedIndexChanged(object sender,EventArgs e)
        {
            BindDropProject();
        }

        /// <summary>
        /// 绑定培训项目
        /// </summary>
        private void BindDropProject()
        {
            dropProject.Items.Clear();
            OracleAccess access=new OracleAccess();
            dropProject.DataSource = access.RunSqlDataSet("select * from  zj_trainplan_project where  trainplan_type_id=" +
                                 Convert.ToInt32(ddlTrainPlanType.SelectedValue));
            dropProject.DataTextField = "trainplan_project_name";
            dropProject.DataValueField = "trainplan_project_id";
            dropProject.DataBind();

            //dropProject.Items.Add(new ListItem("--请选择--","0"));
            //dropProject.SelectedValue = "0";
        }

        /// <summary>
        /// 绑定职名
        /// </summary>
        private void BindgrdPost()
        {
            OracleAccess access=new OracleAccess();
           DataTable dt=  access.RunSqlDataSet(
                "select t.*,GetPostNameByPostID(t.post_ids) postName, case is_work_group_leader  when 1 then '是' else '否' end isleader from zj_train_plan_post t where train_plan_id=" +
                Convert.ToInt32(hfID.Value) + " order by train_plan_post_id desc").Tables[0];
           grdPost.DataSource = dt;
           grdPost.DataBind();
          
        }

        protected void btnUpPost_Click(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTARGUMENT"] != "" && Request.Form["__EVENTARGUMENT"] != "ref")
            {
                 OracleAccess access = new OracleAccess();
                DataTable dt= access.RunSqlDataSet("select count(1) from zj_train_plan_post_class where train_plan_post_id=" +
                                     Convert.ToInt32(Request.Form["__EVENTARGUMENT"])).Tables[0];
               if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                   ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('该职名被引用！')", true);
               else
               {
                   try
                   {

                       access.ExecuteNonQuery(" delete from zj_train_plan_post where train_plan_post_id=" +
                                              Convert.ToInt32(Request.Form["__EVENTARGUMENT"]));
                       //ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('删除成功！')", true);
                   }
                   catch (Exception)
                   {
                       ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('删除失败！')", true);
                   }
               }
            }
           
             BindgrdPost();
            LoadTable();
        }

        protected void btnUpPostClass_Click(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTARGUMENT"] != "" && Request.Form["__EVENTARGUMENT"] != "ref")
            {
                int postClassID = Convert.ToInt32(Request.Form["__EVENTARGUMENT"]);
                OracleAccess access = new OracleAccess();
                DataTable dt =
                    access.RunSqlDataSet(
                        "select count(1) from zj_train_plan_post_class_org  where train_plan_post_class_id=" +
                        Convert.ToInt32(Request.Form["__EVENTARGUMENT"])).Tables[0];
                if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('该培训班被引用，不能删除！')", true);
                else
                {

                    try
                    {

                        access.ExecuteNonQuery(" delete from zj_train_plan_post_class where train_plan_post_class_id=" +
                                               Convert.ToInt32(Request.Form["__EVENTARGUMENT"]));

                        //////更新该班所对应的职名实际人数

                        UpdatePostNum(postClassID, GetPostIDByClassID(postClassID));
                        //ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('删除成功！')", true);
                    }
                    catch (Exception)
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('删除失败！')", true);
                    }

                }
            }
            BindGrdClass();
            LoadTable();
        }

        /// <summary>
        /// 绑定培训班
        /// </summary>
        private void BindGrdClass()
        {
            OracleAccess access = new OracleAccess();
            StringBuilder str=new StringBuilder();
            str.Append("select c.*,p.post_ids,GetPostNameByPostID(p.post_ids) postName from zj_train_plan_post_class c  left join zj_train_plan_post p ");
            str.Append("on p.train_plan_post_id=c.train_plan_post_id ");
            str.AppendFormat("where c.train_plan_id={0}  order by c.train_plan_post_id,c.train_plan_post_class_id ", Convert.ToInt32(hfID.Value));
            DataTable dt = access.RunSqlDataSet(str.ToString()).Tables[0];
            dt.Columns.Add("begin_date1", typeof (string));
            dt.Columns.Add("end_date1", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                r["begin_date1"] = Convert.ToDateTime(r["begin_date"]).ToString("yyyy-MM-dd");
                r["end_date1"] = Convert.ToDateTime(r["end_date"]).ToString("yyyy-MM-dd");
            }
            grdClass.DataSource = dt;
            grdClass.DataBind();
        }

        protected  void btnUpPostClassOrg_Click(object sender,EventArgs e)
        {
            if (Request.Form["__EVENTARGUMENT"] != "" && Request.Form["__EVENTARGUMENT"] != "ref")
            {
                try
                {
                    OracleAccess access = new OracleAccess();
                    string sql1 =
                       "select train_plan_post_class_id from zj_train_plan_post_class_org where train_plan_post_class_org_id=" +
                       Convert.ToInt32(Request.Form["__EVENTARGUMENT"]);

                    int classID = Convert.ToInt32(access.RunSqlDataSet(sql1).Tables[0].Rows[0][0]);

                   
                    access.ExecuteNonQuery(" delete from zj_train_plan_post_class_org where train_plan_post_class_org_id=" +
                                           Convert.ToInt32(Request.Form["__EVENTARGUMENT"]));
                  
                    //更新班级的人数
                    UpdatePostClassNum(classID);
                    //更新职名的人数
                     string sql2 = "select train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id="+classID;
                    int postID = 0;
                    DataTable dt = access.RunSqlDataSet(sql2).Tables[0];
                    if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Length > 0)
                        postID = Convert.ToInt32(dt.Rows[0][0]);
                    UpdatePostNum(classID, postID);

                    /////更新实际人数 
                    //更新班中实际人数
                    UpdatePostClassNum(classID, GetPostClassNum(classID));
                    //更新职名的实际人数
                    UpdatePostLastNum(postID, GetPostNum(classID));

                    /////删除该站段的学员
                    DeleteEmpByOrgID(Convert.ToInt32(Request.Form["__EVENTARGUMENT"]));

                    ////更新培训计划中的OrgIDs
                    UpdateTrainPlanOrgs();
                    //ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('删除成功！')", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('删除失败！')", true);
                }
            }
            BindGridClassOrg();
            BindgrdPost();
            BindGrdClass();
            BindGridStudent();
            LoadTable();
        }

        /// <summary>
        /// 绑定培训班站段
        /// </summary>
        private void BindGridClassOrg()
        {
            OracleAccess access = new OracleAccess();
            string strAnd = "";

            //当主办单位和承办单位都不等于当前登录单位时
            //if (ddlZhuBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
            //    && ddlChengBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString())
            //{
            //    strAnd = string.Format(" where org_id={0}", PrjPub.CurrentLoginUser.StationOrgID);
            //}

            StringBuilder str = new StringBuilder();
        	str.Append(" select * from ( ");
			str.Append("select o.* ,p.post_ids,GetPostNameByPostID(p.post_ids) postName,c.class_name,org.full_name,c.begin_date,c.end_date,zp.sponsor_unit_id from  zj_train_plan_post_class_org o ");
            str.Append("left join zj_train_plan_post_class c on c.train_plan_post_class_id=o.train_plan_post_class_id ");
            str.Append(" left join zj_train_plan_post p on p.train_plan_post_id=c.train_plan_post_id ");
			str.Append("left join org on org.org_id=o.org_id  left join zj_train_plan zp on zp.train_plan_id=c.train_plan_id  where o.train_plan_post_class_id in ");
            str.Append(" (select train_plan_post_class_id from zj_train_plan_post_class where ");
			str.AppendFormat(" train_plan_id={0})" , Convert.ToInt32(hfID.Value));

            //当是站段单位登录时，只能查看本单位
            if (ddlZhuBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                && ddlChengBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString() 
                && PrjPub.CurrentLoginUser.StationOrgID != 200)
            {
                str.AppendFormat("  and o.org_id={0} ", PrjPub.CurrentLoginUser.StationOrgID);
            }

            str.AppendFormat(" order by o.train_plan_post_class_org_id ,c.train_plan_post_id, o.org_id desc,c.train_plan_post_class_id asc");

        	str.AppendFormat(" ) temp {0}", strAnd);
			
			DataTable dt = access.RunSqlDataSet(str.ToString()).Tables[0];
            dt.Columns.Add("begin_date1", typeof(string));
            dt.Columns.Add("end_date1", typeof(string));
			dt.Columns.Add("link", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                r["begin_date1"] = Convert.ToDateTime(r["begin_date"]).ToString("yyyy-MM-dd");
                r["end_date1"] = Convert.ToDateTime(r["end_date"]).ToString("yyyy-MM-dd");
				r["link"] = "<a href='#' title='显示学员信息' onclick='LinkEmp(" + r["train_plan_post_class_org_id"] + ")'>&nbsp;" + r["last_employee_number"] + "&nbsp;</a>";

            }
            grdClassOrg.DataSource = dt;
            grdClassOrg.DataBind();
        }

        private DataTable BindGridStudent()
        {
            OracleAccess access=new OracleAccess();
            string strAdd = "";

            //当主办单位和承办单位都不等于当前登录单位时
            if (ddlZhuBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                && ddlChengBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                && PrjPub.CurrentLoginUser.StationOrgID != 200)
            {
                strAdd = " and GetStationOrgID(E.org_ID)=" + PrjPub.CurrentLoginUser.StationOrgID;
            }

            DataTable dt =
                access.RunSqlDataSet(
					@"select getorgname(E.Org_Id),V.*,E.work_no,zc.class_name,getpostnamebypostid(zp.post_ids) postnames ,getorgname(zg.org_id) zgorg
            from ZJ_Train_Plan_Employee_View V left join Employee E on E.Employee_Id=V.employee_id 
            left join zj_train_plan_post_class  zc on zc.train_plan_post_class_id= v.train_plan_post_class_id 
            left join zj_train_plan_post zp on zp.train_plan_post_id=zc.train_plan_post_id
               left join zj_train_plan_post_class_org zg on zg.train_plan_post_class_org_id=v.train_plan_post_class_org_id 
               where V.TRAIN_PLAN_ID=" + hfID.Value + strAdd).Tables[0];

            dt.Columns.Add("unit", typeof(string));
            dt.Columns.Add("workshop", typeof(string));
            dt.Columns.Add("group", typeof(string));
			
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

            if (Request.QueryString.Get("type") == "view")
            {
                grdEntity.Levels[0].Columns[13].Visible = false;
            }

            return dt;
        }

        /// <summary>
        /// 判断是否需要选择职名
        /// </summary>
        /// <param name="postID"></param>
        private void IsHasPost(int postID)
        {
            OracleAccess access=new OracleAccess();
           DataTable dt= access.RunSqlDataSet("select has_post from zj_train_plan where train_plan_id=" + postID).Tables[0];
           if (Convert.ToInt32(dt.Rows[0][0]) == 0)
               btnAddPost.Enabled = false;
           else
               btnAddPost.Enabled = true;
        }

        /// <summary>
        /// 更新班中的人数
        /// </summary>
        private void UpdatePostClassNum(int planClassID)
        {
            try
            {
                OracleAccess access = new OracleAccess();
                //该班的人数
                string sqlNum =
                    string.Format(
                        "select sum(employee_number) allNum from zj_train_plan_post_class_org where train_plan_post_class_id={0}",
                       planClassID);
                DataTable dt = access.RunSqlDataSet(sqlNum).Tables[0];
                int allNum = 0;
                if(dt!=null && dt.Rows[0][0].ToString().Length>0)
                    allNum=Convert.ToInt32(dt.Rows[0][0]);
                string sql =
                    string.Format(
                        "update zj_train_plan_post_class set total_employee_number={0} where train_plan_post_class_id={1}",
                        allNum, planClassID);
                access.ExecuteNonQuery(sql);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('计划培训班级人数更新失败！')", true);
            }
        }

        /// <summary>
        /// 更新职名中的人数
        /// </summary>
        private void UpdatePostNum(int planClassID,int planPostID)
        {
            try
            {
                OracleAccess access = new OracleAccess();
                string sqlNum =
                    " select sum(total_employee_number) allNum from zj_train_plan_post_class where train_plan_post_id=";
                sqlNum += " (select train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id=" +
                            planClassID + ")";
                int allNum = 0;
                DataTable dt = access.RunSqlDataSet(sqlNum).Tables[0];
                if (dt.Rows[0][0].ToString().Length > 0)
                {
                    allNum = Convert.ToInt32(dt.Rows[0][0]);
                }
                string sql =
                       string.Format(" update zj_train_plan_post set employee_number={0} where train_plan_post_id={1}",
                                     allNum, planPostID);
                access.ExecuteNonQuery(sql);
            }
            catch (Exception)
            {

                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('计划职名人数更新失败！')", true);
            }

        }

        /// <summary>
        /// 通过学员ID，获取培训站段id
        /// </summary>
        /// <param name="planEmpID"></param>
         private int GetPostClassOrgIDByEmpID(int planEmpID)
         {
             OracleAccess access=new OracleAccess();
           DataTable dt= access.RunSqlDataSet(
                "select train_plan_post_class_org_id from zj_train_plan_employee where train_plan_employee_id=" +
                planEmpID).Tables[0];
            int orgID = 0;
            if (dt!=null && dt.Rows.Count>0)
            {
                if (dt.Rows[0][0].ToString().Length>0)
                {
                    orgID = Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            return orgID;
         }
         /// <summary>
         /// 通过学员ID，获取培训班id
         /// </summary>
         /// <param name="planEmpID"></param>
         private int GetPostClassIDByEmpID(int planEmpID)
         {
             OracleAccess access = new OracleAccess();
             DataTable dt = access.RunSqlDataSet(
                  "select train_plan_post_class_id from zj_train_plan_employee where train_plan_employee_id=" +
                  planEmpID).Tables[0];
             int classID = 0;
             if (dt != null && dt.Rows.Count > 0)
             {
                 if (dt.Rows[0][0].ToString().Length > 0)
                 {
                     classID = Convert.ToInt32(dt.Rows[0][0]);
                 }
             }
             return classID;
         }

         /// <summary>
         /// 通过培训班id，获取PostID
         /// </summary>
         /// <param name="planEmpID"></param>
         private int GetPostIDByEmpID(int postClassID)
         {
             OracleAccess access = new OracleAccess();
             DataTable dt = access.RunSqlDataSet(
                  "select train_plan_post_id from  zj_train_plan_post_class where train_plan_post_class_id = " +
                  postClassID).Tables[0];
             int postID = 0;
             if (dt != null && dt.Rows.Count > 0)
             {
                 if (dt.Rows[0][0].ToString().Length > 0)
                 {
                     postID = Convert.ToInt32(dt.Rows[0][0]);
                 }
             }
             return postID;
         }

         /// <summary>
         /// 更新培训计划站段中的实际上报人数
         /// </summary>
         private void UpdatePostClassOrgNum(int orgID, int Num)
         {
             try
             {
                 string sqlUpdate =
                           string.Format(
                               "update zj_train_plan_post_class_org set last_employee_number={0} where train_plan_post_class_org_id={1}",
                               Num, orgID);
                 OracleAccess access = new OracleAccess();
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
         private void UpdatePostClassNum(int classID, int Num)
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
         private void UpdatePostLastNum(int postID, int Num)
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
         /// 获取某站段的人数
         /// </summary>
         /// <param name="postClassID"></param>
         /// <returns></returns>
         private int GetPostClassOrgNum(int postOrgID)
         {
             int num = 0;
             OracleAccess access = new OracleAccess();
             string sql =
                 string.Format(
                     "select count( train_plan_employee_id) allNum from zj_train_plan_employee where train_plan_post_class_org_id={0}", postOrgID);
             DataTable dt = access.RunSqlDataSet(sql).Tables[0];
             if (dt != null && dt.Rows[0][0].ToString().Length > 0)
                 num = Convert.ToInt32(dt.Rows[0][0]);
             return num;
         }

         /// <summary>
         /// 获取某一培训班的人数
         /// </summary>
         /// <param name="postClassID"></param>
         /// <returns></returns>
         private int GetPostClassNum(int postClassID)
         {
             int num = 0;
             OracleAccess access = new OracleAccess();
             string sql =
                 string.Format(
                     "select sum(last_employee_number) allNum from zj_train_plan_post_class_org where train_plan_post_class_id={0}", postClassID);
             DataTable dt = access.RunSqlDataSet(sql).Tables[0];
             if (dt != null && dt.Rows[0][0].ToString().Length > 0)
                 num = Convert.ToInt32(dt.Rows[0][0]);
             return num;
         }

         /// <summary>
         /// 获取计划职名中的人数
         /// </summary>
         /// <param name="postClassID"></param>
         /// <returns></returns>
        private int GetPostNum(int postClassID)
         {
             string sql = "select sum(last_employee_number) allNum from zj_train_plan_post_class where train_plan_post_id";
             sql += "= ( select  train_plan_post_id  from zj_train_plan_post_class where train_plan_post_class_id=" + postClassID + ")";
             OracleAccess access = new OracleAccess();
             DataTable dt = access.RunSqlDataSet(sql).Tables[0];
             int num = 0;
             if (dt != null && dt.Rows[0][0].ToString().Length > 0)
                 num = Convert.ToInt32(dt.Rows[0][0]);
             return num;
         }

        /// <summary>
        /// 删除该站段的培训学员
        /// </summary>
        /// <param name="orgID"></param>
        private void DeleteEmpByOrgID(int orgID)
        {
            try
            {
                OracleAccess access = new OracleAccess();
                access.ExecuteNonQuery("delete from zj_train_plan_employee where train_plan_post_class_org_id=" + orgID);
            }
            catch
            {
               // ClientScript.RegisterClientScriptBlock(GetType(),"","alert('删除学员失败！')",true);
            }
        }

        /// <summary>
        /// 通过classid获取PostID
        /// </summary>
        /// <param name="postClassID"></param>
        /// <returns></returns>
        private int GetPostIDByClassID(int postClassID)
        {
            OracleAccess access=new OracleAccess();
            string sql2 = "select train_plan_post_id from zj_train_plan_post_class where train_plan_post_class_id=" + postClassID;
            int postID = 0;
            DataTable dt = access.RunSqlDataSet(sql2).Tables[0];
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Length > 0)
                postID = Convert.ToInt32(dt.Rows[0][0]);
            return postID;
        }

        /// <summary>
        /// 更新培训计划中的OrgIDs
        /// </summary>
        /// <returns></returns>
        private void UpdateTrainPlanOrgs()
        {
            List<string> lst = new List<string>();
            DataTable dt =
                new OracleAccess().RunSqlDataSet("select org_id from zj_train_plan_post_class_org where train_plan_post_class_id in (select train_plan_post_class_id from zj_train_plan_post_class where train_plan_id=" +
                                                 Convert.ToInt32(hfID.Value) + ")").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {


                foreach (DataRow r in dt.Rows)
                {
                    if (!lst.Equals(r["org_id"]))
                        lst.Add(r["org_id"].ToString());
                }

            }
            string orgs = "";
            if (lst.Count > 0)
                orgs = string.Join(",", lst.ToArray());
            try
            {
                OracleAccess access = new OracleAccess();
                string sqlOrg = string.Format(" update zj_train_plan set orgids='{0}' where train_plan_id={1}",
                                               orgs, Convert.ToInt32(hfID.Value));
                access.ExecuteNonQuery(sqlOrg);
            }
            catch (Exception)
            {
            }
           
        }

        /// <summary>
        /// 如果是查看，就禁用部分功能
        /// </summary>
        private void DisenableAdd()
        {
            string isView = Request.QueryString.Get("type");
            if (isView != null)
            {
                if (isView.Equals("view"))
                {
                    btnSave.Enabled = false;
                    btnAddPost.Enabled = false;
                    btnAddPostClass.Enabled = false;
                    btnAddClassOrg.Enabled = false;
                    btnDelStu.Enabled = false;
                    grdPost.Levels[0].Columns[5].Visible = false;
                    grdClass.Levels[0].Columns[7].Visible = false;
                    grdClassOrg.Levels[0].Columns[8].Visible = false;
                    grdEntity.Levels[0].Columns[9].Visible = false;
                }
            }
        }

        
       protected  void btnDelStu_Click(object sender,EventArgs e)
       {
          if(hfSelectedIDs.Value.Trim().Length>0)
          {
              string[] empID = hfSelectedIDs.Value.Split(',');
              foreach (string sID in empID)
              {
                  int postClassID = GetPostClassIDByEmpID(Convert.ToInt32(sID));
                  int postClassOrgID = GetPostClassOrgIDByEmpID(Convert.ToInt32(sID));

                  DeleteResultByTrainEmpID(sID);   //删除科目结果

                  OracleAccess oracleAccess = new OracleAccess();
                  oracleAccess.ExecuteNonQuery("delete from ZJ_Train_Plan_Employee where TRAIN_PLAN_EMPLOYEE_ID=" + sID);

                  DataTable dt = BindGridStudent();
                  GetAllEmpIDTohf(dt);

                

                  ///////////////////更新实际人数
                  UpdatePostClassOrgNum(postClassOrgID, GetPostClassOrgNum(postClassOrgID));
                  UpdatePostClassNum(postClassID, GetPostClassNum(postClassID));
                  UpdatePostLastNum(GetPostIDByEmpID(postClassID), GetPostNum(postClassID));
              }
          }

           BindgrdPost();
           BindGrdClass();
           BindGridClassOrg();
       }


       private OracleAccess access;
       StringBuilder html = new StringBuilder();
        /// <summary>
        /// 加载报表
        /// </summary>
       private void LoadTable()
       {
           html.AppendFormat(" <table id='TBLP'> <tr class='HeadingRow'>  <th colspan='{0}'>培训工种 </th>", 2);
           html.Append(LoadTableHeader());
           html.Append(" <th> 小计</th></tr>");
           html.Append(" <tr> <td rowspan='2'>序号 </td> <td rowspan='2'>单位</td>");
           html.Append(LoadTableHeader2());
           html.Append(" </tr> <tr>");
           html.Append(LoadTabHeader3());
           html.Append("<td> &nbsp;</td>   </tr>");
           html.Append(LoadTabInfo());
           html.Append(" </table>");
           divTab.InnerHtml = html.ToString();
       }

       /// <summary>
       /// 通过计划ID获取职名信息
       /// </summary>
       /// <param name="planID"></param>
       private DataTable GetPostByPlanId(int planID)
       {
           access = new OracleAccess();
           string sql = "select train_plan_post_id,post_ids,getpostnamebypostid(post_ids) postName from zj_train_plan_post where train_plan_id=" + planID + " order by train_plan_post_id";
           return access.RunSqlDataSet(sql).Tables[0];
       }

       private DataTable GetPlanClassByPostId(int postID)
       {
           int n = 0;
           access = new OracleAccess();
           string sql =
               "select train_plan_post_id,train_plan_post_class_id,begin_date,end_date,class_name from zj_train_plan_post_class where   train_plan_post_id= " +
               postID + " order by begin_date,end_date,train_plan_post_class_id";
           return access.RunSqlDataSet(sql).Tables[0];
       }

       /// <summary>
       /// 加载表格的标头（职名）
       /// </summary>
       private string LoadTableHeader()
       {
           bool bl=true;
           StringBuilder strHeader = new StringBuilder();
           DataTable dt = GetPostByPlanId(Convert.ToInt32(Request.QueryString.Get("ID")));
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   //全部有职名
                   foreach (DataRow r in dt.Rows)
                   {

                       DataTable dtClass = GetPlanClassByPostId(Convert.ToInt32(r["train_plan_post_id"]));
                       strHeader.AppendFormat("<th colspan='{0}' > {1} </th>", dtClass.Rows.Count, r["postName"]);
                   }
               }
               else
               {
                   //全部没有职名
                   DataTable dtClass = GetAllClassByPlanID(Convert.ToInt32(Request.QueryString.Get("ID")));
                   strHeader.AppendFormat("<th colspan='{0}' > {1} </th>", dtClass.Rows.Count, "&nbsp;");
                   bl = false;
               }
               DataTable dtClassNotPost = GetAllClassNotPost(Convert.ToInt32(Request.QueryString.Get("ID")));
               if (dtClassNotPost.Rows.Count > 0 && bl)
               {  
                   //部分职名，部分没职名
                   strHeader.AppendFormat("<th colspan='{0}' > {1} </th>", dtClassNotPost.Rows.Count, "&nbsp;");
               }
           }
           return strHeader.ToString();
       }

       /// <summary>
       /// 加载表头 （班级）
       /// </summary>
       /// <returns></returns>
       private string LoadTableHeader2()
       {
           bool bl = true;
           StringBuilder strHeader = new StringBuilder();
           DataTable dt = GetPostByPlanId(Convert.ToInt32(Request.QueryString.Get("ID")));
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   foreach (DataRow r in dt.Rows)
                   {
                       DataTable dtClass = GetPlanClassByPostId(Convert.ToInt32(r["train_plan_post_id"]));
                       foreach (DataRow rclass in dtClass.Rows)
                       {
                           strHeader.AppendFormat("<td> {0} </td>", rclass["class_name"]);
                       }
                       if(dtClass.Rows.Count==0)
                           strHeader.AppendFormat("<td> {0} </td>", "&nbsp; ");
                   }
                  // strHeader.Append("<td> &nbsp; </td>");
               }
               else
               {
                   DataTable dtClass = GetAllClassByPlanID(Convert.ToInt32(Request.QueryString.Get("ID")));
                   foreach (DataRow rclass in dtClass.Rows)
                   {
                       strHeader.AppendFormat("<td> {0} </td>", rclass["class_name"]);
                   }
                 //  strHeader.Append("<td> &nbsp; </td>");
                   bl = false;
               }

               DataTable dtClassNotPost = GetAllClassNotPost(Convert.ToInt32(Request.QueryString.Get("ID")));
               if(dtClassNotPost.Rows.Count>0 && bl)
               {
                   foreach (DataRow rclass in dtClassNotPost.Rows)
                   {
                       strHeader.AppendFormat("<td> {0} </td>", rclass["class_name"]);
                   }
                   // strHeader.Append("<td> &nbsp; </td>");
               }
               strHeader.Append("<td> &nbsp; </td>");
           }
           return strHeader.ToString();

       }

       /// <summary>
       /// 加载表头 （开始时间，结束时间）
       /// </summary>
       private string LoadTabHeader3()
       {
           bool bl = true;
           StringBuilder strHeader = new StringBuilder();
           DataTable dt = GetPostByPlanId(Convert.ToInt32(Request.QueryString.Get("ID")));
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   foreach (DataRow r in dt.Rows)
                   {
                       DataTable dtClass = GetPlanClassByPostId(Convert.ToInt32(r["train_plan_post_id"]));
                       foreach (DataRow rclass in dtClass.Rows)
                       {
                           string beginDate = Convert.ToDateTime(rclass["begin_date"]).Month + "." +
                                              Convert.ToDateTime(rclass["begin_date"]).Day;
                           string endDate = Convert.ToDateTime(rclass["end_date"]).Month + "." +
                                              Convert.ToDateTime(rclass["end_date"]).Day;
                           strHeader.AppendFormat("<td> {0}-{1} </td>", beginDate, endDate);
                       }
                       if (dtClass.Rows.Count == 0)
                           strHeader.AppendFormat("<td> {0} </td>", "&nbsp; ");
                   }
               }
               else
               {
                   DataTable dtClass = GetAllClassByPlanID(Convert.ToInt32(Request.QueryString.Get("ID")));
                   foreach (DataRow rclass in dtClass.Rows)
                   {
                       string beginDate = Convert.ToDateTime(rclass["begin_date"]).Month + "." +
                                               Convert.ToDateTime(rclass["begin_date"]).Day;
                       string endDate = Convert.ToDateTime(rclass["end_date"]).Month + "." +
                                          Convert.ToDateTime(rclass["end_date"]).Day;
                       strHeader.AppendFormat("<td> {0}-{1} </td>", beginDate, endDate);
                   }
                   bl = false;
               }

               DataTable dtClassNotPost = GetAllClassNotPost(Convert.ToInt32(Request.QueryString.Get("ID")));
               if(dtClassNotPost.Rows.Count>0 && bl)
               {
                   foreach (DataRow rclass in dtClassNotPost.Rows)
                   {
                       string beginDate = Convert.ToDateTime(rclass["begin_date"]).Month + "." +
                                               Convert.ToDateTime(rclass["begin_date"]).Day;
                       string endDate = Convert.ToDateTime(rclass["end_date"]).Month + "." +
                                          Convert.ToDateTime(rclass["end_date"]).Day;
                       strHeader.AppendFormat("<td> {0}-{1} </td>", beginDate, endDate);
                   }
               }
           }
           return strHeader.ToString();
       }

       private string LoadTabInfo()
       {
           bool bl = true;
           StringBuilder strInfo = new StringBuilder();
           DataTable dtOrgInfo = GetAllClassOrgInfo();
           DataTable dt = GetClassOrgByPlanID(Convert.ToInt32(Request.QueryString.Get("ID")));
           DataTable dtP = GetPostByPlanId(Convert.ToInt32(Request.QueryString.Get("ID")));

           if (dt != null)
           {
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   strInfo.AppendFormat("<tr onclick='selectCol(this)'> <td>{0}</td>", i + 1);
                   strInfo.AppendFormat(" <td>{0}</td>", dt.Rows[i]["short_name"]);
                   if (dtP != null)
                   {
                       int count = 0;
                       if (dtP.Rows.Count > 0)
                       {
                           foreach (DataRow r in dtP.Rows)
                           {
                               DataTable dtClass =
                                   GetPlanClassByPostId(Convert.ToInt32(r["train_plan_post_id"]));

                               foreach (DataRow rclass in dtClass.Rows)
                               {
                                   string select = "train_plan_post_class_id=" +
                                                   Convert.ToInt32(rclass["train_plan_post_class_id"]) +
                                                   " and  org_id=" + Convert.ToInt32(dt.Rows[i]["org_id"]);
                                   DataRow[] arr = dtOrgInfo.Select(select);

                                   int EmpNum = 0;
                                   if (arr.Length > 0)
                                       for (int j = 0; j < arr.Length; j++)
                                       {
                                           EmpNum += Convert.ToInt32(arr[j]["employee_number"]);
                                       }
                                      
                                   strInfo.AppendFormat(" <td>{0}</td>", EmpNum);
                                   count += EmpNum;
                               }
                               if (dtClass.Rows.Count == 0)
                                   strInfo.AppendFormat("<td> {0} </td>", 0);

                           }
                       }
                       else
                       {
                           DataTable dtClass = GetAllClassByPlanID(Convert.ToInt32(Request.QueryString.Get("ID")));
                           foreach (DataRow rclass in dtClass.Rows)
                           {
                               string select = "train_plan_post_class_id=" +
                                                 Convert.ToInt32(rclass["train_plan_post_class_id"]) +
                                                 " and  org_id=" + Convert.ToInt32(dt.Rows[i]["org_id"]);
                               DataRow[] arr = dtOrgInfo.Select(select);

                               int EmpNum = 0;
                               if (arr.Length > 0)
                                   for (int j = 0; j < arr.Length; j++)
                                   {
                                       EmpNum += Convert.ToInt32(arr[j]["employee_number"]);
                                   }

                               strInfo.AppendFormat(" <td>{0}</td>", EmpNum);
                               count += EmpNum;
                           }
                           bl = false;
                       }

                       DataTable dtClassNotPost = GetAllClassNotPost(Convert.ToInt32(Request.QueryString.Get("ID")));
                       if (dtClassNotPost.Rows.Count>0 && bl)
                       {
                           foreach (DataRow rclass in dtClassNotPost.Rows)
                           {
                               string select = "train_plan_post_class_id=" +
                                                 Convert.ToInt32(rclass["train_plan_post_class_id"]) +
                                                 " and  org_id=" + Convert.ToInt32(dt.Rows[i]["org_id"]);
                               DataRow[] arr = dtOrgInfo.Select(select);

                               int EmpNum = 0;
                               if (arr.Length > 0)
                                   for (int j = 0; j < arr.Length; j++)
                                   {
                                       EmpNum += Convert.ToInt32(arr[j]["employee_number"]);
                                   }

                               strInfo.AppendFormat(" <td>{0}</td>", EmpNum);
                               count += EmpNum;
                           }
                       }
                       strInfo.AppendFormat("<td>{0}</td>  </tr>", count);
                   }
               }
           }
           return strInfo.ToString();
       }

       /// <summary>
       /// 获取该计划的所有站段
       /// </summary>
       /// <returns></returns>
       private DataTable GetClassOrgByPlanID(int planID)
       {
           access = new OracleAccess();
           string sql = "select distinct  og.org_id,g.short_name from zj_train_plan_post_class_org og left join org g on og.org_id=g.org_id  ";
           sql +=
               "left join zj_train_plan_post_class c on c.train_plan_post_class_id=og.train_plan_post_class_id where c.train_plan_id=" +
               planID;

           if (ddlZhuBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
             && ddlChengBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
             && PrjPub.CurrentLoginUser.StationOrgID != 200)
           {
               sql += "  and og.org_id=" + PrjPub.CurrentLoginUser.StationOrgID;
           }

           return access.RunSqlDataSet(sql).Tables[0];
       }

       /// <summary>
       /// 获取所有的站段信息
       /// </summary>
       /// <returns></returns>
       private DataTable GetAllClassOrgInfo()
       {
           string str = "select * from zj_train_plan_post_class_org";

           if (ddlZhuBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                && ddlChengBan.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                && PrjPub.CurrentLoginUser.StationOrgID != 200)
           {
               str += "  where org_id=" + PrjPub.CurrentLoginUser.StationOrgID;
           }

           return access.RunSqlDataSet(str).Tables[0];
       }

        /// <summary>
        /// 通过计划获取班级
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        private DataTable GetAllClassByPlanID(int planID)
        {
            return access.RunSqlDataSet("select * from zj_train_plan_post_class where train_plan_id="+planID+" order by begin_date").Tables[0];
        }

        /// <summary>
        /// 获取没有职名的班级
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        private DataTable GetAllClassNotPost(int planID)
        {
            return
                access.RunSqlDataSet(
                    "select * from zj_train_plan_post_class where train_plan_post_id is null and train_plan_id=" +
                    planID+ " order by begin_date,end_date,train_plan_post_class_id").Tables[0];
        }

        /// <summary>
        /// 删除该学员的科目结果
        /// </summary>
        /// <param name="empID"></param>
        private void DeleteResultByTrainEmpID(string empID)
        {
            access=new OracleAccess();
            string sql = "delete from zj_train_class_subject_result  where  train_class_id ";
            sql += " in (select train_class_id from zj_train_plan_employee where  train_plan_employee_id in ("+empID+"))";
            sql += " and employee_id in (select employee_id from zj_train_plan_employee where train_plan_employee_id in ("+empID+"))";
            try
            {
                access.ExecuteNonQuery(sql);
            }
            catch (Exception)
            {
                
            }
        }

		protected void btnRef_Click(object sender, EventArgs e)
		{
			BindgrdPost();
			BindGrdClass();
			BindGridClassOrg();
			BindGridStudent();
			LoadTable();
		}
    }
}
