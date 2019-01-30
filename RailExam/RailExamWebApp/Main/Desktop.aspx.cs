using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExam.BLL;
using RailExam.DAL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Main
{
    public partial class Desktop : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("EmployeeDesktop.aspx");
                    return;
                }

                if (PrjPub.CurrentLoginUser.IsDangan)
                {
                    Response.Redirect("DanganDesktop.aspx");
                    return;
                }

                if (PrjPub.HasEditRight("成绩查询"))
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
				if (PrjPub.HasEditRight("考试监控"))
				{
					HfUpdateRightControl.Value = "True";
				}
				else
				{
					HfUpdateRightControl.Value = "False";
				}

                if (PrjPub.HasEditRight("培训计划"))
                {
                    HfUpdateRightPlan.Value = "True";
                }
                else
                {
                    HfUpdateRightPlan.Value = "False";
                }


				if (PrjPub.IsServerCenter)
				{
					hfNowOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
					if (PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 0)
					{
						hfIsAdminControl.Value = "True";
					}
					else
					{
						hfIsAdminControl.Value = "False";
					}
					hfIsAdmin.Value = "True";
				}
				else
				{
					hfNowOrgID.Value = ConfigurationManager.AppSettings["StationID"].ToString();
					if ((PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 0) || (PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 1 && PrjPub.CurrentLoginUser.StationOrgID.ToString() == hfNowOrgID.Value))
					{
						hfIsAdminControl.Value = "True";
					}
					else
					{
						hfIsAdminControl.Value = "False";
					}
					if ((PrjPub.CurrentLoginUser.StationOrgID.ToString() == hfNowOrgID.Value) || PrjPub.CurrentLoginUser.UseType == 0)
					{
						hfIsAdmin.Value = "True";
					}
					else
					{
						hfIsAdmin.Value = "False";
					}
				}

            	lblIP.Text = Pub.GetRealIP();

				SystemVersionBLL objVersionBll = new SystemVersionBLL();
                if (PrjPub.IsServerCenter)
                {
                    this.lblVersion.Text = objVersionBll.GetVersion().ToString("0.0");
                    EmployeeTransferBLL objTrasferBll = new EmployeeTransferBLL();
                    IList<EmployeeTransfer> objList = objTrasferBll.GetEmployeeTransferToByOrgID(PrjPub.CurrentLoginUser.StationOrgID);
                    lblVersion.Text = lblVersion.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "当前有" + objList.Count + "位员工需要调入";

                    int orgID = PrjPub.CurrentLoginUser.StationOrgID;
                    grdEntity2Bind(orgID);
                    BindGridEmp();
                    BindGrdPlan();
                }
                else
                {
                    this.lblVersion.Text = "<a href=# onclick='showVersion()' >" + objVersionBll.GetVersion().ToString("0.0") +"</a>";
                }
            }

			string strRefresh = Request.Form.Get("Refresh");
			if (strRefresh != null && strRefresh != "")
			{
				examsGrid.DataBind();
				Grid1.DataBind();
			}


			if (Request.Form.Get("RefreshComp") != null && Request.Form.Get("RefreshComp") == "true")
			{
				int orgID = PrjPub.CurrentLoginUser.StationOrgID;
				grdEntity2Bind(orgID);
			}
			if (Request.Form.Get("RefreshEmp") != null && Request.Form.Get("RefreshEmp") == "true")
			{
				BindGridEmp();
			}
        	BindGrdPlan();
            BindGridUpdate();
        }

        protected void searchExamCallBack_Callback(object sender, CallBackEventArgs e)
        {
            examsGrid.DataBind();
            examsGrid.RenderControl(e.Output);
        }


		private void grdEntity2Bind(int ORGID)
		{
			DataSet ds = new DataSet();
			OracleAccess OrA = new OracleAccess();
			if (ORGID < 0)
			{
				this.grdEntity2 = null;
				grdEntity2.DataBind();
			}
			else
			{
                ds = OrA.RunSqlDataSet(string.Format("select * from computer_room_apply_two_view where REPLY_STATUS=0 and APPLY_ORG_ID={0}", ORGID));
				grdEntity2.DataSource = ds.Tables.Count > 0 ? ds.Tables[0] : null;
				grdEntity2.DataBind();

                if (ds.Tables[0].Rows.Count != 0)
                {
                    TabStrip2.Tabs[0].Text = "微机教室预订提醒（" + ds.Tables[0].Rows.Count + "）";
                }
			}
		}

	 
		 /// ////////////
		 
		private void IntoApply_Click()
		{
			if (PrjPub.CurrentLoginUser == null)
			{
				Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
				return;
			}
			else
			{
				int orgID = PrjPub.CurrentLoginUser.OrgID;
				grdEntity2Bind(orgID);
			}
		}
		protected void btnDelete_Click(object sender, EventArgs e)
		{
			string argument = Request.Form["__EVENTARGUMENT"];
			OracleAccess ora = new OracleAccess();
			try
			{
				ora.ExecuteNonQuery("delete COMPUTER_ROOM_APPLY where COMPUTER_ROOM_APPLY_ID=" + argument);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			OxMessageBox.MsgBox3("删除成功！");
			IntoApply_Click();
		}

	
		private DataTable GetPlanInfo()
		{
			OracleAccess access = new OracleAccess();
			string sql = @"select * from zj_train_plan_view where  train_plan_id in (select distinct b.Train_Plan_ID from Zj_Train_Plan_Post_Class_Org a
                                 inner join Zj_Train_Plan_Post_Class b on a.Train_Plan_Post_Class_ID=b.Train_Plan_Post_Class_ID
                                 where org_id="+PrjPub.CurrentLoginUser.StationOrgID  +")"+
                               //"  and (train_plan_id  in (select distinct train_plan_id from zj_train_class where train_class_id" +
                               //" not  in (select distinct train_class_id from  random_exam_train_class)) " +
                               //"or train_plan_id not in (select distinct train_plan_id from zj_train_class))"+
                               "  and sysdate<=enddate order by train_plan_id desc";
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
            if (dt.Rows.Count != 0)
            {
                TabStrip2.Tabs[1].Text = "上报培训计划提醒（" + dt.Rows.Count + "）";
            }
			return dt;
		}
		private void BindGrdPlan()
		{
			 grdPlan.DataSource = GetPlanInfo();
			 grdPlan.DataBind();

            if(!PrjPub.IsServerCenter)
            {
                grdPlan.Levels[0].Columns[0].Visible = false;
            }
		}


		private void BindGridEmp()
		{
			EmployeeTransferBLL objBll = new EmployeeTransferBLL();
			IList<EmployeeTransfer> objList = objBll.GetEmployeeTransferToByOrgID(PrjPub.CurrentLoginUser.StationOrgID);

			grdEmployee.DataSource = objList;
			grdEmployee.DataBind();

            if (objList.Count!=0)
            {
                TabStrip2.Tabs[2].Text = "职员调入提醒（" + objList.Count + "）";
            }
		}

        private void BindGridUpdate()
        {
            string str = string.Empty;
            int railSystemId = PrjPub.GetRailSystemId();
            if (railSystemId != 0)
            {
                str =
                    " and book_id in (select distinct book_id from Book_Range_Org where Org_ID in (select Org_ID from Org where Rail_System_ID=" +
                    railSystemId + " and level_Num=2))";
            }
            else
            {
                if (PrjPub.CurrentLoginUser.SuitRange == 0)
                {
                    str = " and book_id in (select distinct book_id from Book_Range_Org where Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                }
            }

            OracleAccess access = new OracleAccess();
            string sql = @"select Book_Update_ID as bookChapterUpdateId,Book_ID as BookId,Chapter_ID as ChapterId,
                            Update_Object as UpdateObject,Book_Name_Bak as BookNameBak,Chapter_Name_Bak as ChapterName,
                            Update_Person as UpdatePerson,Update_Date as UpdateDate,Update_Cause as UpdateCause,
                            Update_Content as UpdateContent
                            from Book_Update  
                            where Update_Date>sysdate-10 " + str +" order by Update_Date desc";
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];
            if (dt.Rows.Count != 0)
            {
                TabStrip2.Tabs[3].Text = "教材更新记录提醒（" + dt.Rows.Count + "）";
            }

            dgUpdate.DataSource = dt;
            dgUpdate.DataBind();
        }
    }
}
