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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;

namespace RailExamWebApp.RandomExamTai
{
	public partial class ComputerManageInfo : PageBase
	{ 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                if (PrjPub.HasEditRight("΢��������Ϣ") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.HasDeleteRight("΢��������Ϣ") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

			   //string orgid  =  Request.QueryString["id"];
			   //initComputerRoomInfo(orgid);
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("../Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
					return;
				}

			    string strQuery = Request.QueryString.Get("strQuery");
                if(string.IsNullOrEmpty(strQuery))
                {
                    GetSql();
                }
                else
                {
                    string[] str = strQuery.Split('|');

                    if(!string.IsNullOrEmpty(str[0]))
                    {
                        hfSelectOrg.Value = str[0];
                        OrganizationBLL orgbll = new OrganizationBLL();
                        txtOrg.Text = orgbll.GetOrganization(Convert.ToInt32(str[0])).ShortName;
                    }

                    txtCOMPUTER_ROOM.Text = str[1];
                    txtAddress.Text = str[2];
                    ddl.SelectedValue = str[3];
                    btnQuery_Click(null,null);
                }

				hfOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
				hfRoleID.Value = PrjPub.CurrentLoginUser.RoleID.ToString();
			}
		}

        private void initComputerRoomInfo(string orgid)
        { 
            DataSet orgDs;
            OracleAccess ora = new OracleAccess();
            if (string.IsNullOrEmpty(orgid))
            {
                grdEntity = null;
                grdEntity.DataBind();
            }
            else
            {
                orgDs = ora.RunSqlDataSet("select a.*,b.COMPUTER_SERVER_NO from COMPUTER_ROOM a "
                             +" inner join COMPUTER_SERVER b on a.COMPUTER_SERVER_ID=b.COMPUTER_SERVER_ID "
                             +" where a.ORG_ID=" + orgid); 
                grdEntity.DataSource = orgDs.Tables.Count>0?orgDs.Tables[0]:null;
                grdEntity.DataBind();
            }
        }

        private string GetStr()
        {
            string str = "";
            int railSystemId = PrjPub.GetRailSystemId();

            if(railSystemId !=0)
            {
                str = " and a.Org_ID in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                      " and level_num=2)";
            }

            return str;
        }
		 

		private void GetSql()
		{
			hfSql.Value = @"
                            select a.*,getorgname(a.org_id) orgName,
										case when is_use=0 then '��' else '��' end as is_used ,
                                         case when Is_Effect=0 then '��' else '��' end as IsEffect ,
										b.COMPUTER_SERVER_NO from COMPUTER_ROOM a 
										inner join COMPUTER_SERVER b on a.COMPUTER_SERVER_ID=b.COMPUTER_SERVER_ID 
                                        where Is_Effect=1 "+GetStr()+@"
                                        order by computer_room_name       
                          ";
		}

		/// <summary>
		/// ѡ�����Ǳ�ɫ
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
			//string Id = e.CommandArgument.ToString();
			//if (e.CommandName == "del")
			//{
			//    try
			//    {
			//        int roomID = 0;
			//        int.TryParse(Id, out roomID);
			//        OracleAccess ora = new OracleAccess();
			//        ora.ExecuteNonQuery("delete from COMPUTER_ROOM where COMPUTER_ROOM_ID="+roomID);   
			//    }
			//    catch
			//    {
			//        ClientScript.RegisterStartupScript(GetType(), "Error", "alert('��΢����������ʹ�ã�����ɾ����')", true);
			//        return;
			//    }
			//    this.ClientScript.RegisterStartupScript(this.Page.GetType(), " ", "<script language='javascript'>alert('ɾ���ɹ���');</script>");

			//    initComputerRoomInfo(Request.QueryString.Get("id"));
			//}
		}

		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnQuery_Click(object sender, EventArgs e)
		{
		    string newSql =
		        @"
                            select a.*,getorgname(a.org_id) orgName,
										case when is_use=0 then '��' else '��' end as is_used ,
                                        case when Is_Effect=0 then '��' else '��' end as IsEffect ,
										b.COMPUTER_SERVER_NO from COMPUTER_ROOM a 
										inner join COMPUTER_SERVER b on a.COMPUTER_SERVER_ID=b.COMPUTER_SERVER_ID where 1=1" +GetStr();
			if(hfSelectOrg.Value!="" && txtOrg.Text!="" && hfSelectOrg.Value!="1")
			{
				newSql += " and a.ORG_ID="+hfSelectOrg.Value;
			}
			if(txtCOMPUTER_ROOM.Text!="")
			{
				newSql += " and a.COMPUTER_ROOM_NAME like '%" + txtCOMPUTER_ROOM.Text + "%'";
			}
			if(txtAddress.Text!="")
			{
				newSql += " and a.ADDRESS like '%" + txtAddress.Text + "%'";
			}

            if(ddl.SelectedValue != "")
            {
                newSql += " and a.Is_Effect =" + ddl.SelectedValue;
            }

			newSql += "  order by computer_room_name ";
			hfSql.Value = newSql;
			grdEntity.EnableViewState = false;
		}

		 
		protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			DataTable db = e.ReturnValue as DataTable;
			if (db.Rows.Count == 0)
			{
				DataRow row = db.NewRow();
				row["COMPUTER_ROOM_ID"] = -1;
				db.Rows.Add(row);
			}
		}

		protected void btnDel_Click(object sender, EventArgs e)
		{
			 try
			 {
				 int roomID = 0;
				 if (hfComID.Value != "")
					 int.TryParse(hfComID.Value, out roomID);
				 OracleAccess ora = new OracleAccess();
				 ora.ExecuteNonQuery("delete from COMPUTER_ROOM where COMPUTER_ROOM_ID=" + roomID);
			 }
			 catch
			 {
				 //ClientScript.RegisterStartupScript(GetType(), "Error", "alert('��΢����������ʹ�ã�����ɾ����')", true);
                 SessionSet.PageMessage = "��΢����������ʹ�ã�����ɾ����";
				 return;
			 }
			 //this.ClientScript.RegisterStartupScript(this.Page.GetType(), " ", "<script language='javascript'>alert('ɾ���ɹ���');</script>");
			
            btnQuery_Click(null, null);
		}

		 
	}
}