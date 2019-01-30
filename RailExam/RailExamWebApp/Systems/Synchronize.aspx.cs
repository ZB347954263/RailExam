using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
	public partial class Synchronize : PageBase
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
				SetControlEnable(1, true);
				SetControlEnable(2, true);
				for (int i = 1; i <= 5; i++)
				{
					ListItem item = new ListItem();
					item.Text = i.ToString();
					item.Value = i.ToString();
					ddlInterval.Items.Add(item);
				}
				for (int i = 0; i <= 10; i++)
				{
					ListItem item = new ListItem();
					item.Text = i.ToString();
					item.Value = i.ToString();
					ddlRetryCount.Items.Add(item);
				}
				BindListBox();
				OrgList.SelectedIndex = 0;
				BindGrid();
				BindCommon();
				BindOrgInfo();

				if (PrjPub.HasDeleteRight("同步设置") && PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 0)
				{
					HfDeleteRight.Value = "True";
				}
				else
				{
					HfDeleteRight.Value = "False";
				}
			}

			string strLogID = Request.Form.Get("LogID");
			if (strLogID != null && strLogID != "")
			{
				SynchronizeLogBLL objBll = new SynchronizeLogBLL();
				objBll.DeleteSynchronizeLog(Convert.ToInt32(strLogID));
				BindGrid();
			}
		}

		private void BindListBox()
		{
			OrgList.Items.Clear();
			OrganizationBLL objBll = new OrganizationBLL();
			IList<RailExam.Model.Organization> objList = objBll.GetOrganizationsByLevel(2);
			foreach (RailExam.Model.Organization organization in objList)
			{
				if (organization.LevelNum != 1)
				{
					ListItem item = new ListItem();
					item.Text = organization.ShortName;
					item.Value = organization.OrganizationId.ToString();
					OrgList.Items.Add(item);
				}
			}
		}

		private void BindCommon()
		{
			SynchronizeBLL objBll = new SynchronizeBLL();
			RailExam.Model.Synchronize obj = objBll.GetSynchronize();
			ddlInterval.SelectedValue = obj.DetectInterval.ToString();
			ddlRetryCount.SelectedValue = obj.RetryCount.ToString();
		}

		private void BindOrgInfo()
		{
			OrganizationBLL objBll = new OrganizationBLL();
			txtTime.Text = objBll.GetOrgSynchronizeTime(Convert.ToInt32(OrgList.SelectedValue));
			txtIPAddress.Text = objBll.GetOrgIPAddress(Convert.ToInt32(OrgList.SelectedValue));
			chkUpload.Checked = objBll.IsAutoUpload(Convert.ToInt32(OrgList.SelectedValue));
		}

		private void SetControlEnable(int flag, bool boolean)
		{
			if (flag == 1)
			{
				ddlInterval.Enabled = !boolean;
				ddlRetryCount.Enabled = !boolean;
				btnModify1.Visible = boolean;
				btnSave1.Visible = !boolean;
				btnCancel1.Visible = !boolean;
			}
			else if (flag == 2)
			{
				txtTime.Enabled = !boolean;
				txtIPAddress.Enabled = !boolean;
				chkUpload.Enabled = !boolean;
				btnModify2.Visible = boolean;
				btnSave2.Visible = !boolean;
				btnCancel2.Visible = !boolean;
			}
		}

		protected void OrgList_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindOrgInfo();
			BindGrid();
		}

		protected void btnModify1_Click(object sender, EventArgs e)
		{
			if (!PrjPub.CurrentLoginUser.IsAdmin || PrjPub.CurrentLoginUser.UseType != 0)
			{
				SessionSet.PageMessage = "您没有该操作的权限！";
				return;
			}
			if (txtTime.Enabled)
			{
				SessionSet.PageMessage = "请先保存所选站段的同步时间！";
				return;
			}

			SetControlEnable(1, false);
		}

		protected void btnSave1_Click(object sender, EventArgs e)
		{
			SynchronizeBLL objBll = new SynchronizeBLL();
			RailExam.Model.Synchronize obj = new RailExam.Model.Synchronize();
			obj.DetectInterval = Convert.ToInt32(ddlInterval.SelectedValue);
			obj.RetryCount = Convert.ToInt32(ddlRetryCount.SelectedValue);
			objBll.UpdateSynchronize(obj);
			SetControlEnable(1, true);
		}

		protected void btnCancel1_Click(object sender, EventArgs e)
		{
			SetControlEnable(1, true);
			BindCommon();
		}

		protected void btnModify2_Click(object sender, EventArgs e)
		{
			if (!PrjPub.CurrentLoginUser.IsAdmin || PrjPub.CurrentLoginUser.UseType != 0)
			{
				SessionSet.PageMessage = "您没有该操作的权限！";
				return;
			}
			if (ddlInterval.Enabled)
			{
				SessionSet.PageMessage = "请先保存系统的同步设置！";
				return;
			}

			SetControlEnable(2, false);
		}

		protected void btnSave2_Click(object sender, EventArgs e)
		{
			OrganizationBLL objBll = new OrganizationBLL();
			if (txtTime.Text != "")
			{
				if (txtTime.Text.IndexOf(":") == -1)
				{
					SessionSet.PageMessage = "同步时间格式错误！";
					return;
				}
				else
				{
					if (txtTime.Text.Length > 5 || txtTime.Text.Length < 4)
					{
						SessionSet.PageMessage = "同步时间格式错误！";
						return;
					}
					else
					{
						string[] str = txtTime.Text.Split(':');
						if (str.Length > 2)
						{
							SessionSet.PageMessage = "同步时间格式错误！";
							return;
						}
						else
						{
							try
							{
								int h = Convert.ToInt32(str[0]);
								int m = Convert.ToInt32(str[1]);

								if (h > 23 || h < 0)
								{
									SessionSet.PageMessage = "同步时间格式错误！";
									return;
								}
								if (m > 59 || m < 0)
								{
									SessionSet.PageMessage = "同步时间格式错误！";
									return;
								}
							}
							catch
							{
								SessionSet.PageMessage = "同步时间格式错误！";
								return;
							}
						}
					}
				}
			}
			objBll.UpdateOrgSynchronizeTime(Convert.ToInt32(OrgList.SelectedValue), txtTime.Text);

			string strPath = "C:\\oracle\\product\\10.2.0\\db_1\\NETWORK\\ADMIN\\tnsnames.ora";
			if (txtIPAddress.Text.Trim() != "")
			{
				if (!Regex.IsMatch(txtIPAddress.Text, "^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$"))
				{
					SessionSet.PageMessage = "IP地址格式不正确！";
					return;
				}

				if (File.Exists(strPath))
				{
					string strName = Pub.GetChineseSpell(OrgList.SelectedItem.Text);
					StreamReader objReader = new StreamReader(strPath, true);
					string strContent = objReader.ReadToEnd();
					objReader.Close();
					string strOldIP = objBll.GetOrgIPAddress(Convert.ToInt32(OrgList.SelectedValue));
					if (strContent.IndexOf(strOldIP) == -1 || strOldIP == "")
					{
						//新增
						StreamWriter objWriter = new StreamWriter(strPath, true);
						objWriter.WriteLine(strName + OrgList.SelectedValue + " =");
						objWriter.WriteLine("  (DESCRIPTION =");
						objWriter.WriteLine("    (ADDRESS_LIST =");
						objWriter.WriteLine("      (ADDRESS = (PROTOCOL = TCP)(HOST = " + txtIPAddress.Text + ")(PORT = 1521))");
						objWriter.WriteLine("    )");
						objWriter.WriteLine("    (CONNECT_DATA =");
						objWriter.WriteLine("      (SERVICE_NAME = RailExam)");
						objWriter.WriteLine("    )");
						objWriter.WriteLine("  )");
						objWriter.WriteLine("");
						objWriter.Close();
					}
					else
					{
						//修改
						strContent = strContent.Replace(strOldIP, txtIPAddress.Text);
						StreamWriter objWriter = new StreamWriter(strPath, false);
						objWriter.Write(strContent);
						objWriter.Close();
					}
					objBll.UpdateOrgService(Convert.ToInt32(OrgList.SelectedValue), strName + OrgList.SelectedValue, txtIPAddress.Text,chkUpload.Checked);
				}
			}
			else
			{
				if (File.Exists(strPath))
				{
					string strOldIP = objBll.GetOrgIPAddress(Convert.ToInt32(OrgList.SelectedValue));
					string strNetName = objBll.GetOrgNetName(Convert.ToInt32(OrgList.SelectedValue));

					if (strOldIP != "")
					{
						//删除
						StreamReader objReader = new StreamReader(strPath, true);
						string strContent = objReader.ReadToEnd();
						objReader.Close();
						string str = strNetName + "=" + "\r\n"
							 + "  (DESCRIPTION =" + "\r\n"
							 + "    (ADDRESS_LIST =" + "\r\n"
							 + "      (ADDRESS = (PROTOCOL = TCP)(HOST = " + strOldIP + ")(PORT = 1521))" + "\r\n"
							 + "    )" + "\r\n"
							 + "    (CONNECT_DATA =" + "\r\n"
							 + "      (SERVICE_NAME = RailExam)" + "\r\n"
							 + "    )" + "\r\n"
							 + "  )" + "\r\n";
						strContent = strContent.Replace(str, "");
						StreamWriter objWriter = new StreamWriter(strPath, false);
						objWriter.Write(strContent);
						objWriter.Close();
					}
					objBll.UpdateOrgService(Convert.ToInt32(OrgList.SelectedValue), "", txtIPAddress.Text,chkUpload.Checked);
				}
			}

			SetControlEnable(2, true);
		}

		protected void btnCancel2_Click(object sender, EventArgs e)
		{
			SetControlEnable(2, true);
			BindListBox();
		}

		private void BindGrid()
		{
			SynchronizeLogBLL objBll = new SynchronizeLogBLL();
			IList<SynchronizeLog> objList = objBll.GetSynchronizeLogByOrgID(Convert.ToInt32(OrgList.SelectedValue));
			Grid1.DataSource = objList;
			Grid1.DataBind();
		}
	}
}
