using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using RailExamWebApp.Common.Class;
using Word;
using CheckBox = System.Web.UI.WebControls.CheckBox;

namespace RailExamWebApp.Common
{
    public partial class SelectMoreEmployee : System.Web.UI.Page
    {
        private static DataTable dtAll;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SelectedEmpIDs();
                BindStationStart();
                BindOrgStart();
                BindWorkShopStart();
                bindWorkByStation();
                BindSystemStart();
                BindTypeStart();
                BindPostStart();
                DisEnableDrop();
               // hfSelect.Value = GetSql();
                hfSelect.Value = "select * from EmployeeView where 1=2";
               
                grdEntity.DataBind();
                OracleAccess access=new OracleAccess();
           
               // dtAll = access.RunSqlDataSet(GetSql()).Tables[0];

				//获取该计划培训班已经选择的学员
            	GetEmployeeByPlanClassID();
            }
        }
        private void BindStationStart()
        {
            ddlStation.Items.Clear();

            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlStation.Items.Add(i);

            OracleAccess oracleAccess = new OracleAccess();
            DataSet ds1 = oracleAccess.RunSqlDataSet("select * from Org where level_Num=2 order by order_index");
            ddlStation.DataSource = ds1.Tables[0].DefaultView;
            ddlStation.DataTextField = "SHORT_NAME";
            ddlStation.DataValueField = "ORG_ID";
            ddlStation.DataBind();
            if (PrjPub.CurrentLoginUser.RoleID != 1)
            {
                ddlStation.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                ddlStation.Enabled = false;
            }

        }

        private void BindWorkShopStart()
        {
            ddlWorkShop.Items.Clear();
            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlWorkShop.Items.Add(i);

        }


        private void BindOrgStart()
        {
            ddlOrg.Items.Clear();
            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlOrg.Items.Add(i);
        }

        private void BindSystemStart()
        {
            ddlSystem.Items.Clear();

           

            OracleAccess oracleAccess = new OracleAccess();
            DataSet ds1 = oracleAccess.RunSqlDataSet("select POST_ID,POST_NAME from Post where POST_LEVEL=1");
            ddlSystem.DataSource = ds1.Tables[0].DefaultView;
            ddlSystem.DataTextField = "POST_NAME";
            ddlSystem.DataValueField = "POST_ID";
            ddlSystem.DataBind();
            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlSystem.Items.Add(i);
            ddlSystem.SelectedValue = "0";
        }

        private void BindTypeStart()
        {
            ddlType.Items.Clear();
            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlType.Items.Add(i);
        }

        private void BindPostStart()
        {
            ddlPost.Items.Clear();
            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlPost.Items.Add(i);
        }
        private string GetSql()
        {
			int planClassID = 0;
        	int planOrgID = 0;
			if (Request.QueryString.Get("planClassOrgID") != "")
				planClassID = Convert.ToInt32(Request.QueryString.Get("planClassID"));
			//if(Request.QueryString.Get("planClassOrgID")!="")
			//    planOrgID = Convert.ToInt32(Request.QueryString.Get("planClassOrgID"));
//            string sql =
//                @"select * from EmployeeView  where employee_id 
//                      not in (select employee_id from  zj_train_plan_employee where train_plan_id=(select distinct train_plan_id from zj_train_plan_post_class  where train_plan_post_class_id=" +
//                planClassID + "))"; //+
			string sql =
	   @"select * from EmployeeView  where employee_id 
                      not in (select employee_id from  zj_train_plan_employee where  train_plan_post_class_id=" +
	   planClassID + ")"; //+
            string orgPath = "";
            if (ddlOrg.SelectedValue != "0")
            {
                sql += string.Format(" and Org_ID={0}", ddlOrg.SelectedValue);
            }
            else
            {
                if (ddlWorkShop.SelectedValue != "0")
                {
                     sql += string.Format(" and '/'||id_path||'/' like '%/{0}/{1}/%'", ddlStation.SelectedValue,ddlWorkShop.SelectedValue);
                }
                else
                {
                     sql += string.Format(" and '/'||id_path||'/' like '%/{0}/%'", ddlStation.SelectedValue);
                 }
            }


            if (ddlPost.SelectedValue != "0")
            {
                sql += string.Format(" and Post_ID={0}", ddlPost.SelectedValue);
            }
            else
            {
                if (ddlType.SelectedValue != "0")
                {
                     sql += string.Format(" and '/'||Post_Path||'/' like '%/{0}/{1}/%'",ddlSystem.SelectedValue, ddlType.SelectedValue);
 
                }
                else
                {
                     if (ddlSystem.SelectedValue != "0")
                        sql += string.Format(" and '/'||Post_Path||'/' like '%/{0}/%'", ddlSystem.SelectedValue);
                 }
            }


            if (ddlGroupLeader.SelectedValue != "-1")
            {
                sql += string.Format(" and IS_GROUP_LEADER={0}", ddlGroupLeader.SelectedValue);
            }

            if (txtEmployeeName.Text.Length != 0)
            {
                sql += string.Format(" and Employee_Name like '%{0}%'", txtEmployeeName.Text);
            }
            if (txtPinyinCode.Text.Length != 0)
            {
                sql += string.Format(" and PINYIN_CODE like '%{0}%'", txtPinyinCode.Text.ToUpper());
            }

            return sql;
        }
        protected void ddlStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStation.SelectedValue == "0")
            {
                BindWorkShopStart();
                BindOrgStart();
            }
            else
            {
                BindWorkShopStart();
                OracleAccess oracleAccess = new OracleAccess();
                DataSet ds1 = oracleAccess.RunSqlDataSet(string.Format("select ORG_ID,SHORT_NAME from Org where PARENT_ID='{0}'", ddlStation.SelectedValue));
                foreach (DataRow row in ds1.Tables[0].Rows)
                {

                    ListItem i = new ListItem();
                    i.Text = row["SHORT_NAME"].ToString();
                    i.Value = row["ORG_ID"].ToString();
                    ddlWorkShop.Items.Add(i);
                }
                //ddlWorkShop.DataSource = ds1.Tables[0].DefaultView;
                //ddlWorkShop.DataTextField = "SHORT_NAME";
                //ddlWorkShop.DataValueField = "ORG_ID";
                //ddlWorkShop.DataBind();
            }
        }

        protected void ddlWorkShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlWorkShop.SelectedValue == "0")
            {
                BindOrgStart();
            }
            else
            {
                BindOrgStart();
                OracleAccess oracleAccess = new OracleAccess();
                DataSet ds1 = oracleAccess.RunSqlDataSet(string.Format("select ORG_ID,SHORT_NAME from Org where PARENT_ID='{0}'", ddlWorkShop.SelectedValue));
                foreach (DataRow row in ds1.Tables[0].Rows)
                {

                    ListItem i = new ListItem();
                    i.Text = row["SHORT_NAME"].ToString();
                    i.Value = row["ORG_ID"].ToString();
                    ddlOrg.Items.Add(i);
                }
                //ddlOrg.DataSource = ds1.Tables[0].DefaultView;
                //ddlOrg.DataTextField = "SHORT_NAME";
                //ddlOrg.DataValueField = "ORG_ID";
                //ddlOrg.DataBind();
            }
        }

        protected void ddlSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSystem.SelectedValue == "0")
            {
                BindTypeStart();
                BindPostStart();
            }
            else
            {
                BindTypeStart();
                OracleAccess oracleAccess = new OracleAccess();
                DataSet ds1 = oracleAccess.RunSqlDataSet(string.Format("select POST_ID,POST_NAME from Post where PARENT_ID='{0}'", ddlSystem.SelectedValue));
                foreach (DataRow row in ds1.Tables[0].Rows)
                {

                    ListItem i = new ListItem();
                    i.Text = row["POST_NAME"].ToString();
                    i.Value = row["POST_ID"].ToString();
                    ddlType.Items.Add(i);
                }
                //ddlType.DataSource = ds1.Tables[0].DefaultView;
                //ddlType.DataTextField = "POST_NAME";
                //ddlType.DataValueField = "POST_ID";
                //ddlType.DataBind();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "0")
            {
                BindPostStart();
            }
            else
            {
                BindPostStart();
                OracleAccess oracleAccess = new OracleAccess();
                DataSet ds1 = oracleAccess.RunSqlDataSet(string.Format("select POST_ID,POST_NAME from Post where PARENT_ID='{0}'", ddlType.SelectedValue));
                foreach (DataRow row in ds1.Tables[0].Rows)
                {

                    ListItem i = new ListItem();
                    i.Text = row["POST_NAME"].ToString();
                    i.Value = row["POST_ID"].ToString();
                    ddlPost.Items.Add(i);
                }
                //ddlPost.DataSource = ds1.Tables[0].DefaultView;
                //ddlPost.DataTextField = "POST_NAME";
                //ddlPost.DataValueField = "POST_ID";
                //ddlPost.DataBind();
            }
        }
        protected void btnQuery_Click(object sender, ImageClickEventArgs e)
        {
			OracleAccess access = new OracleAccess();
			//记录已经选择的项
        	GetCheckWhenPage();


			//记录已经选择的项
           
           dtAll = access.RunSqlDataSet(GetSql()).Tables[0];

			//把查询数据记录到ViewState中
        	DataTable dt = access.RunSqlDataSet(GetSql()).Tables[0];
		   SetInfoToViewState(dt);

			//全部添加
        	ViewState["allID"] = dt;

        	SetSelectedEmpID();

			hfSelect.Value = GetSql();
			grdEntity.DataBind();
        }
        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
                {
                    e.Row.Visible = false;
                }
                else
                {

                   // ((CheckBox)e.Row.FindControl("item")).Attributes.Add("onclick", "check(this," + grdEntity.DataKeys[e.Row.RowIndex]["EMPLOYEE_ID"] + ")");
                   
                    e.Row.Attributes.Add("onclick", "selectArow(this);");
                }
            }
            if (e.Row != null && e.Row.RowType == DataControlRowType.Header)
                ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick", "chkAll(this)");
           

        }
        protected void grdEntity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string Id = e.CommandArgument.ToString();
            //if (string.IsNullOrEmpty(Id))
            //{
            //    return;
            //}

            //if (e.CommandName == "del")
            //{
            //    try
            //    {
            //        OracleAccess oracleAccess = new OracleAccess();
            //        string sql = "delete from zj_train_plan where train_plan_id=" + Id;
            //        oracleAccess.ExecuteNonQuery(sql);
            //    }
            //    catch
            //    {
            //        ClientScript.RegisterStartupScript(GetType(), "Error", "alert('该培训计划已被引用，不能删除！');", true);
            //        return;
            //    }

            //    ClientScript.RegisterStartupScript(GetType(), "OK", "alert('删除成功！');", true);
            //    hfSelect.Value = GetSql();
            //    grdEntity.DataBind();
            //}
            
        }
        protected void grdEntity_RowDataBoundDataRow(object sender, GridViewRowEventArgs e)
        {
			//Label lblWorkNo = e.Row.FindControl("lblWork_NO") as Label;
			//Label lblID = e.Row.FindControl("lblidentity_cardno") as Label;
			//if (lblWorkNo.Text == "")
			//    lblWorkNo.Text = lblID.Text;

            if (e.Row.Cells [8].Text == "0")
                e.Row.Cells[8].Text = "否";
            else
                e.Row.Cells[8].Text = "是";

        	SetCheckWhenPage(e, grdEntity);
        }
        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
            if (db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["Employee_ID"] = -1;
                db.Rows.Add(row);
            }
        }

        private void bindWorkByStation()
        {
            OracleAccess oracleAccess=new OracleAccess();
            DataSet ds2 = oracleAccess.RunSqlDataSet(string.Format("select ORG_ID,SHORT_NAME from Org where PARENT_ID='{0}'", ddlStation.SelectedValue));
            foreach (DataRow row in ds2.Tables[0].Rows)
            {

                ListItem lst = new ListItem();
                lst.Text = row["SHORT_NAME"].ToString();
                lst.Value = row["ORG_ID"].ToString();
                ddlWorkShop.Items.Add(lst);
            }
        }

        

 

         
 

    	protected void grdEntity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }



        protected void btnOK_Click(object sender, EventArgs e)
        {
			GetCheckWhenPage();
			List<string> lst = new List<string>();
			if (ViewState["dtInfo"] != null)
			{
				Dictionary<string, bool> dic = ViewState["dtInfo"] as Dictionary<string, bool>;
				foreach (string s in dic.Keys)
				{
					if (dic[s])
						lst.Add(s);
				}
			}

			hfid.Value = string.Join(",", lst.ToArray());
			//ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue = '"+hfid.Value+"';window.close();", true);
			string acriptStr =string.Format(@"var win = window.dialogArguments;var btn = win.document.getElementById('btnUpdateEmp');
                                 win.document.getElementById('hfSelectEmpID').value='{0}';btn.click();alert('学员添加成功！');", hfid.Value);
			if (hfid.Value != "")
				ClientScript.RegisterClientScriptBlock(GetType(), "", acriptStr, true);
			GetCheckWhenPage();
        }

        private void SelectedEmpIDs()
        {
            string[] arr = Request.QueryString.Get("EmpID").Split(',');
            ViewState["SelectedEmpID"] = arr;
        }
         

        private void DisEnableDrop()
        {
            OracleAccess access=new OracleAccess();
           StringBuilder str=new StringBuilder();
            str.Append( "select post_ids from zj_train_plan_post p right join zj_train_plan_post_class c on c.train_plan_id=p.train_plan_id ");
            str.AppendFormat("where train_plan_post_class_id={0}", Request.QueryString.Get("planClassID"));
            DataTable dt = access.RunSqlDataSet(str.ToString()).Tables[0];
            List<string> lst=new List<string>();
            if(dt.Rows.Count>0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    lst.Add(r["post_ids"].ToString());
                }
            }
            string allPostIDs = string.Join(",", lst.ToArray());
            if(allPostIDs!="")
            {
                ddlSystem.Enabled = false;
                ddlType.Enabled = false;
                DataTable dtAllPostName = access.RunSqlDataSet("select post_id,post_Name from post where post_id in (" + allPostIDs + ") order by post_id").Tables[0];
                foreach (DataRow dr in dtAllPostName.Rows)
                {
                    ddlPost.Items.Add(new ListItem(dr["post_Name"].ToString(), dr["post_id"].ToString()));
                }
            }
            
        }


		/// <summary>
		/// 把数据放入ViewState中,当换查询时把新数据放在ViewState
		/// </summary>
		private void SetInfoToViewState(DataTable dtInfo)
		{
			if (ViewState["dtInfo"] == null)
			{
				Dictionary<string, bool> dic = new Dictionary<string, bool>();
				if (dtInfo != null)
				{
					foreach (DataRow r in dtInfo.Rows)
					{
						dic.Add(r["EMPLOYEE_ID"].ToString(), false);
					}
					ViewState["dtInfo"] = dic;
				}

			}
			else
			{
				Dictionary<string, bool> dic = ViewState["dtInfo"] as Dictionary<string, bool>;
				if (dtInfo != null)
				{
					foreach (DataRow r in dtInfo.Rows)
					{
						if (!dic.ContainsKey(r["EMPLOYEE_ID"].ToString()))
							dic.Add(r["EMPLOYEE_ID"].ToString(), false);
					}
				}
			}
			
		}

		/// <summary>
		/// 记录页面已经选择的数据记录,点击查询，确定，分页时使用
		/// </summary>
		private void GetCheckWhenPage()
		{
			Dictionary<string, bool> dic = ViewState["dtInfo"] as Dictionary<string, bool>;
			foreach (GridViewRow r in grdEntity.Rows)
			{
				System.Web.UI.HtmlControls.HtmlGenericControl spanID =
					(System.Web.UI.HtmlControls.HtmlGenericControl) r.FindControl("spanID");
				CheckBox currentCbx = r.FindControl("item") as CheckBox;
					if (dic != null)
					{
						if (dic.ContainsKey(spanID.InnerText))
						{
							if (currentCbx.Checked)
								dic[spanID.InnerText] = true;
							else
								dic[spanID.InnerText] = false;
						}	
					}
			}
			ViewState["dtInfo"] = dic;
		}

		/// <summary>
		/// 分页之后把选择的数据赋值
		/// </summary>
		private void SetCheckWhenPage(GridViewRowEventArgs e,GridView gv)
		{
			if(e.Row.RowType==DataControlRowType.DataRow)
			{
				Dictionary<string, bool> dic = ViewState["dtInfo"] as Dictionary<string, bool>;
				CheckBox currentCbx = e.Row.Cells[0].FindControl("item") as CheckBox;
				System.Web.UI.HtmlControls.HtmlGenericControl spanID =
					(System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.Cells[0].FindControl("spanID");
                  System.Web.UI.HtmlControls.HtmlImage img =
					(System.Web.UI.HtmlControls.HtmlImage)e.Row.Cells[0].FindControl("imgchecked");

				if (dic != null)
				{
					if (dic.ContainsKey(spanID.InnerText))
					{
						if (dic[spanID.InnerText])
						{
							currentCbx.Checked = true;
							currentCbx.Style.Add("display", "block");
							//img.Style.Add("display","block");
						}
					}
				}
			}
 
		}

 

    	protected void grdEntity_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GetCheckWhenPage();
		}
		protected void grdEntity_PageIndexChanged(object sender, EventArgs e)
		{
			 
		}

		 

		/// <summary>
		/// 获取该计划培训站段已经选择的所有学员
		/// </summary>
		private void GetEmployeeByPlanClassID()
		{
			//if (Request.QueryString.Get("planClassOrgID") != "")
			//{
			//    OracleAccess access = new OracleAccess();
			//    string sql = " select  employee_id from zj_train_plan_employee where TRAIN_PLAN_POST_CLASS_ORG_ID=" + Request.QueryString.Get("planClassOrgID");
			//    DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			//    if (dt != null && dt.Rows.Count > 0)
			//    {
			//        List<string> lst = new List<string>();
			//        foreach (DataRow r in dt.Rows)
			//        {
			//            lst.Add(r["employee_id"].ToString());
			//        }
			//        ViewState["selectedID"] = lst;
			//    }
			//}
		}

		/// <summary>
		/// 选中改站段已经选择的学员
		/// </summary>
		private void SetSelectedEmpID()
		{
			//if (ViewState["dtInfo"] != null && ViewState["selectedID"] != null)
			//{
			//    List<string> l = ViewState["selectedID"] as List<string>;
			//    Dictionary<string, bool> dic = ViewState["dtInfo"] as Dictionary<string, bool>;
			//    foreach (string s in l)
			//    {
			//        if(dic.ContainsKey(s))
			//        {
			//            dic[s] = true;
			//        }
			//    }
			//}
		}

		protected void btnAllOK_Click(object sender, EventArgs e)
		{
			List<string> lstID=new List<string>();
			if (ViewState["allID"] != null)
			{
				DataTable dt = ViewState["allID"] as DataTable;
				foreach (DataRow r in dt.Rows)
				{
						lstID.Add(r["employee_id"].ToString());
				}
			}

			hfid.Value = string.Join(",", lstID.ToArray());
			string acriptStr = string.Format(@"var win = window.dialogArguments;var btn = win.document.getElementById('btnUpdateEmp');
                                 win.document.getElementById('hfSelectEmpID').value='{0}';btn.click();alert('学员添加成功！');", hfid.Value);
			if (hfid.Value != "")
				ClientScript.RegisterClientScriptBlock(GetType(), "", acriptStr, true);
		}
    }
}
