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

namespace RailExamWebApp.RandomExamTai
{
	public partial class ComputerApplyDetail : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
				DDLOrgBind();

                for (int i = 0; i <= 23;i++ )
                {
                    ListItem item = new ListItem();
                    item.Text = i.ToString("00");
                    item.Text = i.ToString("00");
                    ddlBeginHour.Items.Add(item);
                }

                for (int i = 0; i <= 23; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = i.ToString("00");
                    item.Text = i.ToString("00");
                    ddlEndHour.Items.Add(item);
                }

                if (Request.QueryString["mode"] != null)
                {
                    switch (Request.QueryString["mode"].ToString())
                    {
                        case "Insert":

                            dateBeginTime.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
                            dateEndTime.DateValue = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd");
                            ddlBeginHour.SelectedValue = "09";
                            ddlEndHour.SelectedValue = "18";

                            string strQuery = Request.QueryString.Get("strQuery");
                            if(!string.IsNullOrEmpty(strQuery))
                            {
                                string[] str = strQuery.Split('|');
                                DDLOrg.SelectedValue = str[3];
                                DDLOrg_SelectedIndexChanged(null, null);
                                DDLComputerRoom.SelectedValue = str[2];
                                DDLComputerRoom_SelectedIndexChanged(null, null);
                                DDLOrg.Enabled = false;
                                DDLComputerRoom.Enabled = false;

                                DateTime begin = Convert.ToDateTime(str[0]);
                                DateTime end = Convert.ToDateTime(str[1]);

                                dateBeginTime.DateValue = begin.ToString("yyyy-MM-dd");
                                dateEndTime.DateValue = end.ToString("yyyy-MM-dd");
                                ddlBeginHour.SelectedValue = begin.Hour.ToString("00");
                                ddlEndHour.SelectedValue = end.Hour.ToString("00");
                            }

                            break;
                        case "ReadOnlyOne":
                            BindFrom(1);
                            ReadOnlyDetail();
                            break;
                        case "ReadOnlyTwo":
                            BindFrom(2);
                            ReadOnlyDetail();

                            break;
                        case "EditOne":
                            BindFrom(3);
                            break;
                        case "EditTwo":
                            this.txtREJECT_REASON.Enabled = true;
                            this.DDLREPLY_STATUS.Enabled = true;
                            this.DDLOrg.Enabled = false;
                            this.DDLComputerRoom.Enabled = false;
                            this.dateBeginTime.Enabled = false;
                            this.dateEndTime.Enabled = false;
                            this.txtAPPLY_COMPUTER_NUMBER.ReadOnly = true;
                            ddlBeginHour.Enabled = false;
                            ddlEndHour.Enabled = false;
                            BindFrom(4);
                            break;
                    }
                }
			}
			
			LabelBind();
		}
		/// <summary>
		/// 绑定站段
		/// </summary>
		private void DDLOrgBind()
		{
			DataSet ds = new DataSet();
			OracleAccess ora = new OracleAccess();
			if (Request.QueryString["mode"].ToString() == "EditTwo")
			{
				ds = ora.RunSqlDataSet("select * from ORG order by Order_Index");
			}
			else
			{
				ds = ora.RunSqlDataSet("select * from ORG where level_num=2 and Org_ID!=" + PrjPub.CurrentLoginUser.StationOrgID + " order by Order_Index");
			}
			this.DDLOrg.Items.Clear();
			ListItem item = new ListItem("-请选择-", "-1");
			this.DDLOrg.Items.Add(item);
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				this.DDLOrg.Items.Add(new ListItem(dr["SHORT_NAME"].ToString(), dr["ORG_ID"].ToString()));
			}
			this.DDLComputerRoom.Items.Add(new ListItem("-请选择-", "-1"));
		}
		/// <summary>
		/// 绑定教室
		/// </summary>
		/// <param name="orgid"></param>
		protected void DDLBind(int orgid)
		{
			if (orgid < 0)
			{
				ClientScript.RegisterStartupScript(GetType(), "NO", "alert('请选择一个组织机构!')", true);
				return;
			}
			//this.DDLComputerRoom.Dispose();
			DataSet ds = new DataSet();
			OracleAccess ora = new OracleAccess();
			ds=ora.RunSqlDataSet(string.Format("select * from computer_room where ORG_ID={0} and IS_EFFECT=1",orgid));
			this.DDLComputerRoom.Items.Clear();
			this.DDLComputerRoom.Items.Add(new ListItem("-请选择-", "-1"));
			foreach (DataRow dr in ds.Tables[0].Rows)
			{ 
				this.DDLComputerRoom.Items.Add(new ListItem(dr["COMPUTER_ROOM_NAME"].ToString(),dr["COMPUTER_ROOM_ID"].ToString()));
			}
		}
		/// <summary>
		/// 站段选择项索引改变事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DDLOrg_SelectedIndexChanged(object sender, EventArgs e)
		{
			int ORGID = int.Parse(this.DDLOrg.SelectedValue);
			DDLBind(ORGID);
			this.lblCOMPUTER_NUMBER.InnerHtml = string.Empty;
			this.lblBAD_SEAT.InnerHtml = string.Empty;
		}
		/// <summary>
		/// 保存的方法
		/// </summary>
		/// <param name="state"></param>
		private void SaveInfo(string state)
		{
			
		}
		/// <summary>
		/// 页面绑定
		/// </summary>
		private void BindFrom(int type)
		{//1-TabOne中的浏览  2-TabTwo中的浏览 3-TabOne中的编辑 4-TabTwo中的编辑
			if (Request.QueryString["id"] != null)
			{
				string itid=Request.QueryString["id"].ToString();
				int _COMPUTER_ROOM_APPLY_ID = Convert.ToInt32(Request.QueryString["id"]);
				OracleAccess ora = new OracleAccess();
				DataSet ds = new DataSet();
				ds = ora.RunSqlDataSet(string.Format("select * from COMPUTER_ROOM_APPLY where COMPUTER_ROOM_APPLY_ID={0}", _COMPUTER_ROOM_APPLY_ID));
				if (ds.Tables.Count > 0)
				{

                    this.DDLOrg.SelectedValue = ds.Tables[0].Rows[0]["APPLY_ORG_ID"].ToString();
					DDLBind(int.Parse(ds.Tables[0].Rows[0]["APPLY_ORG_ID"].ToString()));
						this.DDLOrg.SelectedValue = ds.Tables[0].Rows[0]["APPLY_ORG_ID"].ToString();
						DDLBind(int.Parse(this.DDLOrg.SelectedValue));
					this.DDLComputerRoom.SelectedValue = ds.Tables[0].Rows[0]["COMPUTER_ROOM_ID"].ToString();
				    DateTime beginDate = DateTime.Parse(ds.Tables[0].Rows[0]["APPLY_START_TIME"].ToString());
				    DateTime endDate = DateTime.Parse(ds.Tables[0].Rows[0]["APPLY_END_TIME"].ToString());
				    this.dateBeginTime.DateValue = beginDate.ToString("yyyy-MM-dd");
				    ddlBeginHour.SelectedValue = beginDate.Hour.ToString("00");
                    this.dateEndTime.DateValue = endDate.ToString("yyyy-MM-dd");
				    ddlEndHour.SelectedValue = endDate.Hour.ToString("00");
					this.txtAPPLY_COMPUTER_NUMBER.Text = ds.Tables[0].Rows[0]["APPLY_COMPUTER_NUMBER"].ToString();
					this.DDLREPLY_STATUS.SelectedIndex = int.Parse(ds.Tables[0].Rows[0]["REPLY_STATUS"].ToString());
					this.txtREJECT_REASON.Text = ds.Tables[0].Rows[0]["REJECT_REASON"].ToString();

                    if(type == 3 && DDLREPLY_STATUS.SelectedIndex >0)
                    {
                        ReadOnlyDetail();
                    }

                    if (type == 4 && DDLREPLY_STATUS.SelectedIndex > 0)
                    {
                        ReadOnlyDetail();
                        if(DateTime.Now<=endDate)
                        {
                            btnSave.Visible = true;
                        }
                    }
				}
			}
		}
		/// <summary>
		/// 只读属性页面
		/// </summary>
		private void ReadOnlyDetail()
		{
			this.DDLOrg.Enabled = false;
			this.DDLComputerRoom.Enabled = false;
			this.dateBeginTime.Enabled = false;
			this.dateEndTime.Enabled = false;
			this.txtAPPLY_COMPUTER_NUMBER.Enabled = false;
			this.btnSave.Visible = false;
		}
		private void LabelBind()
		{
			if (this.DDLComputerRoom.SelectedValue == "-1")
			{
				return;
			}
			OracleAccess ora = new OracleAccess();
			DataSet ds = new DataSet();
			int computerRoomID=int.Parse(this.DDLComputerRoom.SelectedValue);
			string strSql = "select * from COMPUTER_ROOM where COMPUTER_ROOM_ID=" + computerRoomID;
			ds = ora.RunSqlDataSet(strSql);
			if (ds.Tables.Count > 0)
			{
				this.lblCOMPUTER_NUMBER.InnerHtml = ds.Tables[0].Rows[0]["COMPUTER_NUMBER"].ToString();
				this.lblBAD_SEAT.InnerHtml = ds.Tables[0].Rows[0]["BAD_SEAT"].ToString().Split(',').Length.ToString();
			    lblEffectNum.Text =
			        (int.Parse(ds.Tables[0].Rows[0]["COMPUTER_NUMBER"].ToString()) -
			         ds.Tables[0].Rows[0]["BAD_SEAT"].ToString().Split(',').Length).ToString();
			}
		}

		private bool isBlock()
		{
			if (PrjPub.CurrentLoginUser == null)
			{
				Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
				return true;
			}
			if (this.DDLOrg.SelectedValue == "-1")
			{
				ClientScript.RegisterStartupScript(GetType(), "NO", "alert('请选择一个组织机构!')", true);
				this.DDLOrg.Focus();
				return true;
			}
			if (this.DDLComputerRoom.SelectedValue == "-1")
			{
				ClientScript.RegisterStartupScript(GetType(), "NO", "alert('请选择教室！')", true);
				this.DDLComputerRoom.Focus();
				return true;
			}
			if (string.IsNullOrEmpty(this.dateBeginTime.DateValue.ToString()))
			{
				ClientScript.RegisterStartupScript(GetType(), "NO", "alert('设置开始时间！')", true);
				return true;
			}
			if (string.IsNullOrEmpty(this.dateEndTime.DateValue.ToString()))
			{
				ClientScript.RegisterStartupScript(GetType(), "NO", "alert('设置结束时间！')", true);
				return true;
			}
			if (string.IsNullOrEmpty(this.txtAPPLY_COMPUTER_NUMBER.Text.Trim().ToString()))
			{
				ClientScript.RegisterStartupScript(GetType(), "NO", "alert('请填写申请机位！')", true);
				this.txtAPPLY_COMPUTER_NUMBER.Focus();
				return true;
			}

			int allSeat = int.Parse(this.lblCOMPUTER_NUMBER.InnerHtml);
			int badSeat = int.Parse(this.lblBAD_SEAT.InnerHtml);
			int APPLY_COMPUTER_NUMBER = int.Parse(this.txtAPPLY_COMPUTER_NUMBER.Text.Trim().ToString());
			if (APPLY_COMPUTER_NUMBER > (allSeat - badSeat))
			{
				ClientScript.RegisterStartupScript(GetType(), "NO", "alert('申请机位不能大于可用机位，请重新填写申请机位数！')", true);
				this.txtAPPLY_COMPUTER_NUMBER.Focus();
				return true;
			}
			return false;
		}
		/// <summary>
		/// 修改的申请预订记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, ImageClickEventArgs e) 
		{
			
			if (Request.QueryString["mode"] != null)
			{
				string state = Request.QueryString["mode"].ToString();
				if (isBlock())
				{
					return;
				}
				int orgID = PrjPub.CurrentLoginUser.StationOrgID;
				int APPLY_ORG_ID = int.Parse(this.DDLOrg.SelectedValue);
				int COMPUTER_ROOM_ID = int.Parse(this.DDLComputerRoom.SelectedValue);
				int APPLY_COMPUTER_NUMBER = int.Parse(this.txtAPPLY_COMPUTER_NUMBER.Text.Trim().ToString());

                string strBegin = dateBeginTime.DateValue.ToString() + " " + ddlBeginHour.SelectedValue + ":00:00";
                string strEnd = dateEndTime.DateValue.ToString() + " " + ddlEndHour.SelectedValue + ":00:00";

				try
				{
                    DateTime nowBegin = Convert.ToDateTime(strBegin);
                    DateTime nowEnd = Convert.ToDateTime(strEnd);

                    if(nowEnd<= nowBegin)
                    {
                        this.ClientScript.RegisterStartupScript(GetType(), "NO", "alert('申请结束时间不能小于等于开始时间!')", true);
                        return;
                    }
				}
				catch (Exception ex)
				{
					OxMessageBox.MsgBox3(ex.Message);
				    return;
				}

                OracleAccess ora = new OracleAccess();
				try
				{
					if (state == "Insert")
					{
                        //判断被申请微机教室是否被占用
                        //string strSql = "select * from Computer_Room_Apply a "
                        //         + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                        //         + " where a.REPLY_STATUS=1 and a.Computer_Room_ID=" + DDLComputerRoom.SelectedValue
                        //         + " and ((a.Apply_Start_Time >= to_date('" + strBegin + "','YYYY-MM-DD HH24:MI:SS')"
                        //         + " and a.Apply_Start_Time <= to_date('" + strEnd + "','YYYY-MM-DD HH24:MI:SS'))"
                        //         + " or (a.Apply_End_Time >= to_date('" + strBegin + "','YYYY-MM-DD HH24:MI:SS')"
                        //         + " and a.Apply_End_Time <= to_date('" + strEnd + "','YYYY-MM-DD HH24:MI:SS')))";
                        //if(ora.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                        //{
                        //    this.ClientScript.RegisterStartupScript(GetType(), "NO", "alert('该微机教室已被本单位或其他单位占用，不能申请使用该微机教室!')", true);
                        //    return;
                        //}

						DataSet ds = new DataSet();
						ds = ora.RunSqlDataSet("select * from COMPUTER_ROOM_APPLY where ORG_ID=" + orgID + " and APPLY_ORG_ID=" + int.Parse(this.DDLOrg.SelectedValue) + " and COMPUTER_ROOM_ID=" + int.Parse(this.DDLComputerRoom.SelectedValue) + " and REPLY_STATUS=" + 0);
						if (ds.Tables.Count > 0&&ds.Tables[0].Rows.Count>0)
						{
							this.ClientScript.RegisterStartupScript(GetType(), "NO", "alert('当前单位已向该教室提出申请但未回复，不能重复申请!')", true);
							return;
						}
						ora = new OracleAccess();
						ora.ExecuteNonQuery(string.Format("insert into computer_room_apply(COMPUTER_ROOM_APPLY_ID,ORG_ID,"
                            +"apply_org_id,computer_room_id,apply_start_time,apply_end_time,apply_computer_number,REPLY_STATUS) "
                            +"  values({0},{1},{2},{3},to_date('{4}','yyyy-mm-dd hh24:mi:ss'),to_date('{5}','yyyy-mm-dd hh24:mi:ss'),{6},{7})", "COMPUTER_ROOM_APPLY_SEQ.NEXTVAL", orgID, APPLY_ORG_ID, COMPUTER_ROOM_ID, strBegin, strEnd, APPLY_COMPUTER_NUMBER, 0));
					}
					else if (state == "EditOne" || state == "EditTwo")
					{
						if (Request.QueryString["id"] != null)
						{
							string strSql = string.Empty;
							int _ComputerRoomApplyID = Convert.ToInt32(Request.QueryString["id"]);
							int REPLY_STATUS = this.DDLREPLY_STATUS.SelectedIndex;
							string REJECT_REASON = this.txtREJECT_REASON.Text.Trim().ToString();
							if (state == "EditOne")
							{
                                //判断被申请微机教室是否被占用
                                //strSql = "select * from Computer_Room_Apply a "
                                //         + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                                //         + " where a.REPLY_STATUS=1 and a.Computer_Room_ID=" + DDLComputerRoom.SelectedValue
                                //         + " and ((a.Apply_Start_Time >= to_date('" + strBegin + "','YYYY-MM-DD HH24:MI:SS')"
                                //         + " and a.Apply_Start_Time <= to_date('" + strEnd + "','YYYY-MM-DD HH24:MI:SS'))"
                                //         + " or (a.Apply_End_Time >= to_date('" + strBegin + "','YYYY-MM-DD HH24:MI:SS')"
                                //         + " and a.Apply_End_Time <= to_date('" + strEnd + "','YYYY-MM-DD HH24:MI:SS')))";

                                //if (ora.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                                //{
                                //    this.ClientScript.RegisterStartupScript(GetType(), "NO", "alert('该微机教室已被本单位或其他单位占用，不能申请使用该微机教室!')", true);
                                //    return;
                                //}
								ora.ExecuteNonQuery(string.Format("update computer_room_apply SET ORG_ID={0},APPLY_ORG_ID={1},COMPUTER_ROOM_ID={2},APPLY_START_TIME=to_date('{3}','yyyy-mm-dd hh24:mi:ss'),APPLY_END_TIME=to_date('{4}','yyyy-mm-dd hh24:mi:ss'),APPLY_COMPUTER_NUMBER={5},REPLY_STATUS={6},REJECT_REASON='{7}' where COMPUTER_ROOM_APPLY_ID={8}", orgID, APPLY_ORG_ID, COMPUTER_ROOM_ID, strBegin, strEnd, APPLY_COMPUTER_NUMBER, REPLY_STATUS, REJECT_REASON, _ComputerRoomApplyID));

							}
							else
							{
								//只需要修回复状态与回复原因
								switch (REPLY_STATUS)
								{
									case 0:
										strSql = "update computer_room_apply SET REPLY_STATUS=0 where COMPUTER_ROOM_APPLY_ID="+_ComputerRoomApplyID;
										break;
									case 1:

                                        //strSql = "select * from Computer_Room_Apply a "
                                        //+ " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                                        //+ " where a.REPLY_STATUS=1 and a.Computer_Room_ID=" + DDLComputerRoom.SelectedValue
                                        //+ " and ((a.Apply_Start_Time >= to_date('" + strBegin + "','YYYY-MM-DD HH24:MI:SS')"
                                        //+ " and a.Apply_Start_Time <= to_date('" + strEnd + "','YYYY-MM-DD HH24:MI:SS'))"
                                        //+ " or (a.Apply_End_Time >= to_date('" + strBegin + "','YYYY-MM-DD HH24:MI:SS')"
                                        //+ " and a.Apply_End_Time <= to_date('" + strEnd + "','YYYY-MM-DD HH24:MI:SS')))";
                                        //if (ora.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                                        //{
                                        //    this.ClientScript.RegisterStartupScript(GetType(), "NO", "alert('该微机教室已被本单位或其他单位占用，不能回复通过该微机教室的申请!')", true);
                                        //    return;
                                        //}

										strSql = "update computer_room_apply SET REPLY_STATUS=1 where COMPUTER_ROOM_APPLY_ID=" + _ComputerRoomApplyID;

										break;
									case 2:
										if (string.IsNullOrEmpty(this.txtREJECT_REASON.Text.Trim().ToString()))
										{
											this.ClientScript.RegisterStartupScript(GetType(), "NO", "alert('请填写回复原因！')", true);
											this.txtREJECT_REASON.Focus();
											return;
										}
										strSql = "update computer_room_apply SET REPLY_STATUS=2,REJECT_REASON='" + this.txtREJECT_REASON.Text.Trim().ToString() + "' where COMPUTER_ROOM_APPLY_ID=" + _ComputerRoomApplyID;
										break;
								}
								ora.ExecuteNonQuery(strSql);

                                if (REPLY_STATUS == 1)
                                {
                                     ora.ExecuteNonQuery("update Computer_Room set Is_Use=1 where Computer_Room_ID=" + DDLComputerRoom.SelectedValue);
                                }
							}
						}
					}
					
				}
				catch (Exception ex)
				{
					Response.Write("<script>alert('" + ex.Message + "')</script>");
					throw ex;
				}
				ClientScript.RegisterStartupScript(GetType(), "OK", "alert('保存成功！');top.returnValue='true';top.close();", true);
                //Response.Write("<script>top.returnValue='true';top.close();</script>");
			}
		}
		/// <summary>
		/// 保存一条新的申请预订记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSaveAdd_Click(object sender, ImageClickEventArgs e)
		{
			
		}

		protected void DDLComputerRoom_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.lblCOMPUTER_NUMBER.InnerHtml = string.Empty;
			this.lblBAD_SEAT.InnerHtml = string.Empty;
			LabelBind();
		}
		
	}
}
