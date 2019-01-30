using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using System.Text;
using System.Web.UI.WebControls;

namespace RailExamWebApp.TrainManage
{
	public partial class StudentAdd : PageBase
	{
	    private static DataTable dtInfo;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}
				bindGrid();
			}
		}

        private void bindGrid()
        {
            OracleAccess oracleAccess = new OracleAccess();
            if (Request.QueryString["classID"] != null)
            {
                DataTable dtPlan =
                    oracleAccess.RunSqlDataSet(
                        string.Format("select  train_plan_id from zj_train_class where train_class_id={0}",
                                      Convert.ToInt32(Request.QueryString["classID"]))).Tables[0];
                if (dtPlan != null && dtPlan.Rows.Count > 0)
                    hidPlanID.Value = dtPlan.Rows[0][0].ToString();
            }
            DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  * from zj_train_plan_employee PE left join EmployeeView EV ");
            strSql.AppendFormat(
                " on PE.Employee_Id=EV.Employee_Id  where PE.TRAIN_CLASS_ID is null and PE.train_plan_id={0} order by PE.TRAIN_PLAN_EMPLOYEE_ID",
                Convert.ToInt32(hidPlanID.Value));


            dt = oracleAccess.RunSqlDataSet(strSql.ToString()).Tables[0];
            dt.Columns.Add("IS_GROUP_LEADERZH", typeof (string));
            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToInt32(r["IS_GROUP_LEADER"]) == 1)
                    r["IS_GROUP_LEADERZH"] = "是";
                else
                    r["IS_GROUP_LEADERZH"] = "否";
            }
            grdStudent.DataSource = dt;
            grdStudent.DataBind();
            usersDic = InitializeUsersDic(dt);
            dtInfo = dt;

        }

	    protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			AddStudent();
			ClientScript.RegisterClientScriptBlock(GetType(), "", "shutRef()", true);
		}

		private void AddStudent()
		{
            List<string> lst=new List<string>();
		    foreach (DataRow r in dtInfo.Rows)
		    {
		        string index = r["train_plan_employee_id"].ToString();
               if( usersDic[index]==true)
                   lst.Add(index);
		    }
		    foreach (GridViewRow gr in grdStudent.Rows)
		    {
                System.Web.UI.HtmlControls.HtmlGenericControl spanID =
                    (System.Web.UI.HtmlControls.HtmlGenericControl)gr.Cells[0].FindControl("spanID");
                string currentValue = spanID.InnerText;
                CheckBox currentCbx = gr.Cells[0].FindControl("item") as CheckBox;
                if(currentCbx.Checked==true && !lst.Contains(currentValue))
                    lst.Add(currentValue);
		    }
		    hidID.Value = string.Join(",", lst.ToArray());
			try
			{
				string sql = string.Format("update zj_train_plan_employee set train_class_id={0} where train_plan_employee_id in ({1}) and train_plan_id={2}",
										  Convert.ToInt32(Request.QueryString["classID"]), hidID.Value,Convert.ToInt32(hidPlanID.Value));
				OracleAccess oracleAccess = new OracleAccess();
				oracleAccess.ExecuteNonQuery(sql);
			}
			catch
			{
				OxMessageBox.MsgBox3("数据保存失败！");
			}
		}

		protected void grdStudent_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
		{
			if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
			{
				if (grdStudent.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
					e.Row.Visible = false;
				else
  					 e.Row.Attributes.Add("onclick", "select(this)");
			}
			if (e.Row != null && e.Row.RowType == DataControlRowType.Header)
				((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick", "chkAll(this)");
 
		}
		
		protected void grdStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
            //记录原来分页面的所有CheckBox的状态
           
          Dictionary<string, bool> d=  RememberOldValues(usersDic, grdStudent);
           
			grdStudent.PageIndex = e.NewPageIndex;
		    bindGrid();
            RePopulateValues(d, grdStudent);
		}

	    private Dictionary<string, bool> usersDic
	    {
            get { return (ViewState["usersdic"] != null) ? (Dictionary<string, bool>)ViewState["usersdic"] : null; }
            set { ViewState["usersdic"] = value; }

	    }
        //初始化Dictionary    
        protected Dictionary<string, bool> InitializeUsersDic(DataTable dt)      
        {        
            Dictionary<string, bool> currentDic = new Dictionary<string, bool>();
                      
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currentDic.Add(dt.Rows[i]["train_plan_employee_id"].ToString(), false);
            }
            return currentDic;
        }

        private Dictionary<string, bool> RememberOldValues(Dictionary<string, bool> dic, GridView gdv)
        {
            for (int i = 0; i < gdv.Rows.Count; i++)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl spanID =
                    (System.Web.UI.HtmlControls.HtmlGenericControl) gdv.Rows[i].Cells[0].FindControl("spanID");

                string currentValue = spanID.InnerText;
                CheckBox currentCbx = gdv.Rows[i].Cells[0].FindControl("item") as CheckBox;
                if (currentCbx.Checked && dic[currentValue] == false)
                {
                    dic[currentValue] = true;
                }
                else if (!currentCbx.Checked && dic[currentValue] == true)
                {
                    dic[currentValue] = false;
                }

            }
            ViewState["usersdic"] = dic;
            return dic;
        }

        public Dictionary<string, bool> RePopulateValues(Dictionary<string, bool> dic, GridView gdv)
         {
             foreach (GridViewRow row in gdv.Rows)
             {
                 System.Web.UI.HtmlControls.HtmlGenericControl spanID =
                    (System.Web.UI.HtmlControls.HtmlGenericControl)row.Cells[0].FindControl("spanID");
                 string currentValue = spanID.InnerText;
                 CheckBox currentCbx = row.Cells[0].FindControl("item") as CheckBox;
                 if (dic[currentValue] == true)
                 {
                     currentCbx.Checked = true;
                 }
                 else           
                 {
                     currentCbx.Checked = false;
                 }
             }
              ViewState["usersdic"] = dic;
             return dic;
         }

        
	}
}
