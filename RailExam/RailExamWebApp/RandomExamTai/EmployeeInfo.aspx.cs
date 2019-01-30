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
using RailExam.Model;
using RailExamWebApp.Common.Class;
using RailExam.BLL;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using System.Collections.Generic; 

namespace RailExamWebApp.RandomExamTai
{
	public partial class EmployeeInfo : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (PrjPub.CurrentLoginUser == null)
			{
				Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
				return;
			}

			if (!IsPostBack)
			{
			    hfLoginID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();
                if (PrjPub.HasEditRight("职员管理") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("职员管理") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }


			    string strQuery = Request.QueryString.Get("strQuery");
                if(!string.IsNullOrEmpty(strQuery))
                {
                    string[] str = strQuery.Split('|');
                    txtName.Text = str[0];
                    ddlSex.SelectedValue = str[1];
                    ddlStatus.SelectedValue = str[2];
                    txtPinYin.Text = str[3];
                    txtTechnicalCode.Text = str[4];
                    hfPostID.Value = str[5];
                }

                gridBind();
			}

            string strRefresh = Request.Form.Get("Refresh");
            if (!string.IsNullOrEmpty(strRefresh))
            {
                gridBind();
            }

            string strUpdate = Request.Form.Get("UpdatePsw");
            if (!string.IsNullOrEmpty(strUpdate))
            {
                SystemUserBLL objBll = new SystemUserBLL();
                SystemUser obj = objBll.GetUserByEmployeeID(Convert.ToInt32(strUpdate));
                if (obj != null)
                {
                    obj.Password = "111111";
                    if (PrjPub.IsServerCenter)
                    {
                        objBll.UpdateUser(obj);
                    }
                    else
                    {
                        objBll.UpdateUserPsw(obj.UserID, "111111");
                    }
                    SessionSet.PageMessage = "初始化密码成功！";
                }
                else
                {
                    SessionSet.PageMessage = "该员工登录帐户不存在，初始化密码失败！";
                }

				gridBind();
            }

            string strDelete = Request.Form.Get("Delete");
            if (!string.IsNullOrEmpty(strDelete))
            {
                try
                {
                    OracleAccess db = new OracleAccess();
                    if (PrjPub.CurrentLoginUser.EmployeeID != 0)
                    {
                        //判断该员工是否参加过考试
                        string strIsArrange =
                            string.Format(
                                "select count(1) from random_exam_arrange where  ','||user_ids||','  like '%,{0},%'",
                                strDelete);
                        if (Convert.ToInt32(db.RunSqlDataSet(strIsArrange).Tables[0].Rows[0][0]) > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "NO", "alert('该员工已参加考试，不能删除！');", true);
                            return;
                        }

                        string strSql = "delete from Employee where Employee_ID=" + strDelete;
                        db.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        string strSql = "select * from Employee where Employee_ID=" + strDelete;
                        DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                        EmployeeBLL employeebll = new EmployeeBLL();
                        employeebll.DeleteEmployee(Convert.ToInt32(strDelete));

                        SystemLogBLL systemLogBLL = new SystemLogBLL();
                        systemLogBLL.WriteLog("删除员工：" + dr["Employee_Name"] + "（" + dr["Work_No"] + "）基本信息");
                    }
                    gridBind();
                }
                catch
                {
                    SessionSet.PageMessage = "该员工已被引用，不能删除！";
                }
            }

            if (!string.IsNullOrEmpty(hfPostID.Value))
            {
                PostBLL post = new PostBLL();
                txtPost.Text = post.GetPost(Convert.ToInt32(hfPostID.Value)).PostName;
            }
		}


		private void gridBind()
		{
            string strIDPath = Request.QueryString["idpath"];
            OrganizationBLL orgBll = new OrganizationBLL();
            IList<RailExam.Model.Organization> orgList = orgBll.GetOrganizationsByWhereClause(" id_path||'/'='" + strIDPath + "'||'/'");
            if (orgList.Count > 0)
            {
                hfOrgID.Value = orgBll.GetStationOrgID(orgList[0].OrganizationId).ToString();
            }

			hfSelect.Value = GetSql();
			grdEntity.DataBind();
		}
 

		/// <summary>
		/// 选中行是变色
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
					e.Row.Attributes.Add("onclick", "selectArow('" + e.Row.RowIndex + "');");
				}
			}
		}


		protected void grdEntity_RowCommand(object sender, GridViewCommandEventArgs e)
		{

		}

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnQuery_Click(object sender, EventArgs e)
		{
			if (PrjPub.CurrentLoginUser == null)
			{
				Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
				return;
			}

			gridBind();
		}

		private string GetSql()
		{
			string id = Request.QueryString.Get("ID");

			
			string employeename = this.txtName.Text.Trim().ToString();
			string sex = this.ddlSex.SelectedValue.ToString();
			string strPath = Request.QueryString.Get("idpath");
			DataSet ds = new DataSet();
			OracleAccess ora = new OracleAccess();
			string strSql = string.Empty;

			if(id == "1" || id=="0")
			{
                strSql = "select a.EMPLOYEE_ID,EMPLOYEE_NAME,SEX,a.POST_ID,ISONPOST,a.MEMO,c.Post_Name,d.FingerNum,"
					+ " (case when IsONPost=1 then '是' else '否' end) as IsOnPostName,"
					+ " (case when o.photo is not null then 1 else 0 end  ) as isPhoto "
					+ " from Employee a "
                    + " inner join Post c on a.Post_ID=c.Post_ID "
					+ " inner join Org b on a.Org_ID=b.Org_ID "
					+ " left join employee_photo o on o.EMPLOYEE_ID=a.EMPLOYEE_ID "
                    + " left join (select employee_id,count(*) FingerNum from Employee_FingerPrint group by Employee_ID) d "
                    + " on a.Employee_ID=d.Employee_ID "
					+ " where 1=1";

                if(string.IsNullOrEmpty(employeename))
                {
                    strSql += " and 1=2";
                }

                if(!PrjPub.IsServerCenter)
                {
                    strSql += " and GetStationOrgID(a.org_ID)=" + ConfigurationManager.AppSettings["StationID"];
                }
			}
			else
			{
                strSql = "select a.EMPLOYEE_ID,EMPLOYEE_NAME,SEX,a.POST_ID,ISONPOST,a.MEMO,c.Post_Name,d.FingerNum,"
                    + " (case when IsONPost=1 then '是' else '否' end) as IsOnPostName ,"
					+ " (case when o.photo is not null then 1 else 0 end  ) as isPhoto "
                    + " from Employee a "
                    + " inner join Post c on a.Post_ID=c.Post_ID "
                    + " inner join Org b on a.Org_ID=b.Org_ID "
                    + " left join employee_photo o on o.EMPLOYEE_ID=a.EMPLOYEE_ID "
                    + " left join (select a.employee_id,count(*) FingerNum from Employee_FingerPrint a "
                    +" inner join Employee b on a.Employee_ID=b.Employee_ID "
                    + " inner join Org c on b.Org_ID=c.Org_ID "
                    + " where c.id_Path||'/' like '%" + strPath + "/%' "
                    +" group by a.Employee_ID) d "
                    + " on a.Employee_ID=d.Employee_ID "
                    + " where b.id_Path||'/' like '%" + strPath + "/%' ";
			}

            if(PrjPub.CurrentLoginUser.EmployeeID != 0)
            {
                strSql += " and a.Employee_ID != 0";
            }

            if(!string.IsNullOrEmpty(txtPost.Text) || !string.IsNullOrEmpty(hfPostID.Value))
            {
                strSql += " and (a.Post_ID=" + hfPostID.Value +
                          " or a.Post_ID in (select Post_ID from Post where Parent_ID=" + hfPostID.Value + "))";
            }

            if(!string.IsNullOrEmpty(txtPinYin.Text))
            {
                strSql += " and Upper(a.PinYin_Code) like '%" + txtPinYin.Text.ToUpper() + "%'";
            }

            if (!string.IsNullOrEmpty(txtTechnicalCode.Text))
            {
                strSql += " and a.Technical_Code like '%" + txtTechnicalCode.Text + "%'";
            }

            if (!string.IsNullOrEmpty(employeename))
            {
                strSql += " and a.Employee_Name like '%" + employeename + "%'";
            }

            if (ddlSex.SelectedValue != "")
            {
                strSql += " and Sex='" + sex + "'";
            }

            if(ddlFinger.SelectedValue == "1")
            {
                strSql += " and d.FingerNum>0";
            }
            else if (ddlFinger.SelectedValue == "0")
            {
                strSql += " and d.FingerNum is null";
            }

            if(ddlStatus.SelectedValue != "-1")
            {
                strSql += " and IsOnPost=" + ddlStatus.SelectedValue;
            }

            strSql += " order by b.Level_Num,b.Order_Index";

			return strSql;
		}

		protected void grdEntity_PageIndexChanging1(object sender, GridViewPageEventArgs e)
		{

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

		protected void grdEntity_RowDataBoundDataRow(object sender, GridViewRowEventArgs e)
		{
			if(e.Row.RowType==DataControlRowType.DataRow)
			{
				Label lbl = e.Row.FindControl("lblIsPhoto") as Label;
				if(lbl.Text=="1")
					e.Row.Cells[1].Text="<font color='Green'>" + e.Row.Cells[1].Text + "</font>";
			}
		}
	}
}
