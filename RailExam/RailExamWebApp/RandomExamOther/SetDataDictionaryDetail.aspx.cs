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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class SetDataDictionaryDetail : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				FillPage();
			}
		}

		/// <summary>
		/// ���ҳ��
		/// </summary>
		protected void FillPage()
		{
			string value = Request.QueryString["TypeValue"];
			string id = Request.QueryString["ID"];
			trType.Visible = false;
			trMemo.Visible = true;

			//�޸� ��Ҫ���
			if(!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(id))
			{
				if (value == "education_level")
				{
					EducationLevelBLL objBLL = new EducationLevelBLL();
					EducationLevel objEducationLevel = objBLL.GetEducationLevelByEducationLevelID(Convert.ToInt32(id));
					txtItemName.Text = objEducationLevel.EducationLevelName;
					txtMemo.Text = objEducationLevel.Memo;
				}
				if (value == "political_status")
				{
					PoliticalStatusBLL objBLL = new PoliticalStatusBLL();
					PoliticalStatus objPoliticalStatus = objBLL.GetPoliticalStatusByPoliticalStatusID(Convert.ToInt32(id));
					txtItemName.Text = objPoliticalStatus.PoliticalStatusName;
					txtMemo.Text = objPoliticalStatus.Memo;
				}
				if (value == "workgroupleader_level")
				{
					WorkGroupLeaderLevelBLL objBLL=new WorkGroupLeaderLevelBLL();
					WorkGroupLeaderLevel objWorkGroupLeaderLevel =
						objBLL.GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(Convert.ToInt32(id));
					txtItemName.Text = objWorkGroupLeaderLevel.LevelName;
					txtMemo.Text = objWorkGroupLeaderLevel.Memo;
				}
				if(value=="technician_type")
				{
					TechnicianTypeBLL objBLL=new TechnicianTypeBLL();
					TechnicianType objTechnicianType=objBLL.GetTechnicianTypeByTechnicianTypeID(Convert.ToInt32(id));
					txtItemName.Text = objTechnicianType.TypeName;
					txtMemo.Text = objTechnicianType.Memo;
				}
				if (value == "technician_title_type")
				{
					trType.Visible = true;
					trMemo.Visible = false;
					TechnicianTitleTypeBLL objBLL = new TechnicianTitleTypeBLL();
					TechnicianTitleType objTechnicianTitleType = objBLL.GetTechnicianTitleTypeByTechnicianTitleTypeID(Convert.ToInt32(id));
					txtItemName.Text = objTechnicianTitleType.TypeName;
					ddlType.SelectedValue = objTechnicianTitleType.TypeLevel.ToString();
					txtMemo.ReadOnly = true;
				}
				if (value == "education_employee_type")
				{
					trType.Visible = false;
					trMemo.Visible = false;
					EducationEmployeeTypeBLL objBLL = new EducationEmployeeTypeBLL();
					EducationEmployeeType obj = objBLL.GetEducationEmployeeTypeByEducationEmployeeTypeID(Convert.ToInt32(id));
					txtItemName.Text = obj.TypeName;
					txtMemo.ReadOnly = true;
				}
				if (value == "committee_head_ship")
				{
					trType.Visible = false;
					trMemo.Visible = false;
					CommitteeHeadShipBLL objBLL = new CommitteeHeadShipBLL();
					CommitteeHeadShip obj = objBLL.GetCommitteeHeadShipByCommitteeHeadShipID(Convert.ToInt32(id));
					txtItemName.Text = obj.CommitteeHeadShipName;
					txtMemo.ReadOnly = true;
				}
				if (value == "random_exam_modular_type")
				{
					trType.Visible = false;
					trMemo.Visible = false;
					RandomExamModularTypeBLL objBLL = new RandomExamModularTypeBLL();
					RandomExamModularType obj = objBLL.GetRandomExamModularTypeByTypeID(Convert.ToInt32(id));
					txtItemName.Text = obj.RandomExamModularTypeName;
					txtMemo.ReadOnly = true;
                }
                if (value == "trainplan_type")
                {
                    trType.Visible = false;
                    trMemo.Visible = false;
                    OracleAccess oracle=new OracleAccess();
                    DataTable dt =
                        oracle.RunSqlDataSet(string.Format(
                            "select * from zj_trainplan_type where trainplan_type_id={0}", Convert.ToInt32(id))).Tables[
                                0];
                    txtItemName.Text = dt.Rows[0]["trainplan_type_name"].ToString();
                }

				if(value=="safe_level")
				{
					trType.Visible = false;
					trMemo.Visible = false;
					OracleAccess access=new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_safe_level where  safe_level_id ="+id).Tables[0];
					txtItemName.Text = dt.Rows[0]["safe_level_name"].ToString();
				}
				if (value == "certificate")
				{
					trType.Visible = false;
					trMemo.Visible = false;
					OracleAccess access=new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_certificate where  certificate_id =" + id).Tables[0];
					txtItemName.Text = dt.Rows[0]["certificate_name"].ToString();
				}
				if (value == "certificate_level")
				{
					trType.Visible = true;
					trMemo.Visible = false;
					OracleAccess access=new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_certificate_level where  certificate_level_id =" + id).Tables[0];
					txtItemName.Text = dt.Rows[0]["certificate_level_name"].ToString();
					ddlType.DataSource = access.RunSqlDataSet("select * from zj_certificate order by order_index").Tables[0];
					ddlType.DataTextField = "certificate_name";
					ddlType.DataValueField = "certificate_id";
					ddlType.DataBind();
					ddlType.SelectedValue = dt.Rows[0]["certificate_id"].ToString();
				}
				if (value == "certificate_unit")
				{
					trType.Visible = false;
					trMemo.Visible = false;
					OracleAccess access = new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_certificate_unit where  certificate_unit_id =" + id).Tables[0];
					txtItemName.Text = dt.Rows[0]["certificate_unit_name"].ToString();
				}
				if (value == "train_unit")
				{
					trType.Visible = false;
					trMemo.Visible = false;
					OracleAccess access = new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_train_unit where  train_unit_id =" + id).Tables[0];
					txtItemName.Text = dt.Rows[0]["train_unit_name"].ToString();
				}
			}
			else
			{
				if (value == "technician_title_type")
				{
					trType.Visible = true;
					trMemo.Visible = false;
				}
				if (value == "education_employee_type")
				{
					trMemo.Visible = false;
				}
				if (value == "committee_head_ship")
				{
					trMemo.Visible = false;
				}
				if (value == "random_exam_modular_type")
				{
					trMemo.Visible = false;
				}
                if (value == "trainplan_type")
                {
                    trMemo.Visible = false;
                }
				if (value == "safe_level")
				{
					trMemo.Visible = false;
				}
				if (value == "certificate")
				{
					trMemo.Visible = false;
				}
				if (value == "certificate_level")
				{
					trMemo.Visible = false;
					trType.Visible = true;
					OracleAccess access=new OracleAccess();
					ddlType.DataSource = access.RunSqlDataSet("select * from zj_certificate order by order_index").Tables[0];
					ddlType.DataTextField = "certificate_name";
					ddlType.DataValueField = "certificate_id";
					ddlType.DataBind();
				}
				if (value == "certificate_unit")
				{
					trMemo.Visible = false;
				}
				if (value == "train_unit")
				{
					trMemo.Visible = false;
				}
			}

		}
		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void InsertButton_Click(object sender, EventArgs e)
		{
			string value = Request.QueryString["TypeValue"];
			string id = Request.QueryString["ID"];
			string mode = Request.QueryString["Mode"];

			if (value == "education_level" && mode=="Update")
			{
				EducationLevelBLL objBLL = new EducationLevelBLL();
				EducationLevel obj = objBLL.GetEducationLevelByEducationLevelID(Convert.ToInt32(id));
				obj.EducationLevelID = Convert.ToInt32(id);
				string educationName = obj.EducationLevelName;

				if(objBLL.GetEducationLevelByWhereClause("Education_Level_ID !="+ id + " and Education_Level_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ��Ļ��̶ȣ����������룡";
					txtItemName.Focus();
					return;
				}

				obj.EducationLevelName = txtItemName.Text;
				obj.Memo = txtMemo.Text;
				objBLL.UpdateEducationLevel(obj);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸��Ļ��̶�:��" + educationName + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "political_status" && mode == "Update")
			{
				PoliticalStatusBLL objBLL = new PoliticalStatusBLL();
				PoliticalStatus obj = objBLL.GetPoliticalStatusByPoliticalStatusID(Convert.ToInt32(id));
				obj.PoliticalStatusID = Convert.ToInt32(id);
				string politicalStatusName = obj.PoliticalStatusName;

				if (objBLL.GetPoliticalStatusByWhereClause("Political_Status_ID !=" + id + " and Political_Status_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�������ò�����������룡";
					txtItemName.Focus();
					return;
				}

				obj.PoliticalStatusName = txtItemName.Text;
				obj.Memo = txtMemo.Text;
				objBLL.UpdatePoliticalStatus(obj);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸�������ò:��" + politicalStatusName + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "workgroupleader_level" && mode == "Update")
			{
				WorkGroupLeaderLevelBLL objBLL = new WorkGroupLeaderLevelBLL();
				WorkGroupLeaderLevel obj = objBLL.GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(Convert.ToInt32(id));
				obj.WorkGroupLeaderLevelID = Convert.ToInt32(id);
				string levelName = obj.LevelName;

				if (objBLL.GetWorkGroupLeaderLevelByWhereClause("WorkGroupLeader_Level_ID !=" + id + " and Level_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸð��鳤������������룡";
					txtItemName.Focus();
					return;
				}

				obj.LevelName = txtItemName.Text;
				obj.Memo = txtMemo.Text;
				objBLL.UpdateWorkGroupLeaderLevel(obj);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸İ��鳤���:��" + levelName + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "technician_type" && mode == "Update")
			{
				TechnicianTypeBLL objBLL = new TechnicianTypeBLL();
				TechnicianType obj = objBLL.GetTechnicianTypeByTechnicianTypeID(Convert.ToInt32(id));
				obj.TechnicianTypeID = Convert.ToInt32(id);
				string typeName = obj.TypeName;

				if (objBLL.GetTechnicianTypeByWhereClause("Technician_Type_ID !=" + id + " and Type_Name='" + txtItemName.Text + "'","").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸù��˼��ܵȼ������������룡";
					txtItemName.Focus();
					return;
				}

				obj.TypeName = txtItemName.Text;
				obj.Memo = txtMemo.Text;
				objBLL.UpdateTechnicianType(obj,"");
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸Ĺ��˼��ܵȼ�:��" + typeName + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "technician_title_type" && mode == "Update")
			{
				TechnicianTitleTypeBLL objBLL = new TechnicianTitleTypeBLL();
				TechnicianTitleType obj = objBLL.GetTechnicianTitleTypeByTechnicianTitleTypeID(Convert.ToInt32(id));
				obj.TechnicianTitleTypeID = Convert.ToInt32(id);
				string typeTitleName = obj.TypeName;

				if (objBLL.GetTechnicianTitleTypeByWhereClause("Technician_Title_Type_ID !=" + id + " and Type_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸøɲ�����ְ�ƣ����������룡";
					txtItemName.Focus();
					return;
				}

				obj.TypeName = txtItemName.Text;
				obj.TypeLevel = Convert.ToInt32(ddlType.SelectedValue);
				objBLL.UpdateTechnicianTitleType(obj);
				txtMemo.ReadOnly = true;
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸ĸɲ�����ְ��:��" + typeTitleName + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}

			if (value == "education_employee_type" && mode == "Update")
			{
				EducationEmployeeTypeBLL objBLL = new EducationEmployeeTypeBLL();
				EducationEmployeeType obj = objBLL.GetEducationEmployeeTypeByEducationEmployeeTypeID(Convert.ToInt32(id));
				obj.EducationEmployeeTypeID = Convert.ToInt32(id);
				string typeTitleName = obj.TypeName;

				if (objBLL.GetAllEducationEmployeeTypeByWhereClause("Education_Employee_Type_ID !=" + id + " and Education_Employee_Type_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�ְ����Ա���ͣ����������룡";
					txtItemName.Focus();
					return;
				}
				obj.TypeName = txtItemName.Text;
				objBLL.UpdateEducationEmployeeType(obj);
				txtMemo.ReadOnly = true;
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸ĸ�ְ����Ա����:��" + typeTitleName + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "committee_head_ship" && mode == "Update")
			{
				CommitteeHeadShipBLL objBLL = new CommitteeHeadShipBLL();
				CommitteeHeadShip obj = objBLL.GetCommitteeHeadShipByCommitteeHeadShipID(Convert.ToInt32(id));
				obj.CommitteeHeadShipID = Convert.ToInt32(id);
				string ShipName = obj.CommitteeHeadShipName;

				if (objBLL.GetAllCommitteeHeadShipByWhereClause("committee_head_ship_id !=" + id + " and committee_head_ship_name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�ְ��ίԱ��ְ�����������룡";
					txtItemName.Focus();
					return;
				}
				obj.CommitteeHeadShipName = txtItemName.Text;
				objBLL.UpdateCommitteeHeadShip(obj);
				txtMemo.ReadOnly = true;
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸�ְ��ίԱ��ְ��:��" + ShipName + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "random_exam_modular_type" && mode == "Update")
			{
				RandomExamModularTypeBLL objBLL = new RandomExamModularTypeBLL();
				RandomExamModularType obj = objBLL.GetRandomExamModularTypeByTypeID(Convert.ToInt32(id));
				obj.RandomExamModularTypeID = Convert.ToInt32(id);
				string TpyeName = obj.RandomExamModularTypeName;

				if (objBLL.GetAllRandomExamModularTypeByWhereClause("random_exam_modular_type_id !=" + id + " and random_exam_modular_type_name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�ģ�鿼��������������룡";
					txtItemName.Focus();
					return;
				}
				obj.RandomExamModularTypeName = txtItemName.Text;
				objBLL.UpdateRandomExamModularType(obj);
				txtMemo.ReadOnly = true;
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸�ģ�鿼�����:��" + TpyeName + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
            if (value == "trainplan_type" && mode == "Update")
            {
                OracleAccess oracle=new OracleAccess();
                DataTable dt =
                    oracle.RunSqlDataSet(
                        string.Format(
                            "select * from zj_trainplan_type where trainplan_type_id!={0} and trainplan_type_name='{1}'",
                            Convert.ToInt32(id), txtItemName.Text)).Tables[0];
                if(dt.Rows.Count>0)
                {
                    SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ���ѵ������������룡";
                    txtItemName.Focus();
                    return;
                }
                DataTable dt1 =
                    oracle.RunSqlDataSet(
                        string.Format(
                            "select * from zj_trainplan_type where trainplan_type_id={0}",
                            Convert.ToInt32(id))).Tables[0];
                oracle.ExecuteNonQuery(
                    string.Format("update zj_trainplan_type set trainplan_type_name='{0}' where trainplan_type_id={1}",
                                  txtItemName.Text, (Convert.ToInt32(id))));
                SystemLogBLL objLogBll = new SystemLogBLL();
                objLogBll.WriteLog("�޸�ģ�鿼�����:��" + dt1.Rows[0]["trainplan_type_name"] + "��Ϊ��" + txtItemName.Text + "��");
                Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
            }

			if (value == "safe_level" && mode=="Update")
			{
				OracleAccess access=new OracleAccess();
				DataTable dt = access.RunSqlDataSet(
					string.Format("select count(1) from zj_safe_level where safe_level_id!={0} and safe_level_name='{1}'",
					              Convert.ToInt32(id), txtItemName.Text.Trim())).Tables[0];
				if(Convert.ToInt32(dt.Rows[0][0])>0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸð�ȫ�ȼ������������룡";
					txtItemName.Focus();
					return;
				}
				DataTable dt1 = access.RunSqlDataSet("select * from zj_safe_level where  safe_level_id =" + id).Tables[0];
				access.ExecuteNonQuery(
					string.Format("update zj_safe_level set safe_level_name='{0}' where safe_level_id={1}", txtItemName.Text.Trim(),
					              Convert.ToInt32(id)));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸İ�ȫ�ȼ�:��" + dt1.Rows[0]["safe_level_name"] + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");

			}

			if (value == "certificate" && mode == "Update")
			{
				OracleAccess access=new OracleAccess();
				DataTable dt = access.RunSqlDataSet(
					string.Format("select count(1) from zj_certificate where certificate_id!={0} and certificate_name='{1}'",
					              Convert.ToInt32(id), txtItemName.Text.Trim())).Tables[0];
				if(Convert.ToInt32(dt.Rows[0][0])>0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�֤�飬���������룡";
					txtItemName.Focus();
					return;
				}
				DataTable dt1 = access.RunSqlDataSet("select * from zj_certificate where  certificate_id =" + id).Tables[0];
				access.ExecuteNonQuery(
					string.Format("update zj_certificate set certificate_name='{0}' where certificate_id={1}", txtItemName.Text.Trim(),
					              Convert.ToInt32(id)));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸�֤��:��" + dt1.Rows[0]["certificate_name"] + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");

			}

			if (value == "certificate_level" && mode == "Update")
			{
				OracleAccess access=new OracleAccess();
				DataTable dt1 = access.RunSqlDataSet("select * from zj_certificate_level where  certificate_level_id =" + id).Tables[0];
				access.ExecuteNonQuery(
					string.Format("update zj_certificate_level set certificate_level_name='{0}',certificate_id={2} where certificate_level_id={1}", txtItemName.Text.Trim(),
					              Convert.ToInt32(id),ddlType.SelectedValue));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸�֤��ȼ�:��" + dt1.Rows[0]["certificate_level_name"] + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");

			}
			if (value == "certificate_unit" && mode == "Update")
			{
				OracleAccess access = new OracleAccess();
				DataTable dt = access.RunSqlDataSet(
					string.Format("select count(1) from zj_certificate_unit where certificate_unit_id!={0} and certificate_unit_name='{1}'",
								  Convert.ToInt32(id), txtItemName.Text.Trim())).Tables[0];
				if (Convert.ToInt32(dt.Rows[0][0]) > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ÷�֤��λ�����������룡";
					txtItemName.Focus();
					return;
				}
				DataTable dt1 = access.RunSqlDataSet("select * from zj_certificate_unit where  certificate_unit_id =" + id).Tables[0];
				access.ExecuteNonQuery(
					string.Format("update zj_certificate_unit set certificate_unit_name='{0}' where certificate_unit_id={1}", txtItemName.Text.Trim(),
								  Convert.ToInt32(id)));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸ķ�֤��λ:��" + dt1.Rows[0]["certificate_unit_name"] + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");

			}
			if (value == "train_unit" && mode == "Update")
			{
				OracleAccess access = new OracleAccess();
				DataTable dt = access.RunSqlDataSet(
					string.Format("select count(1) from zj_train_unit where train_unit_id!={0} and train_unit_name='{1}'",
								  Convert.ToInt32(id), txtItemName.Text.Trim())).Tables[0];
				if (Convert.ToInt32(dt.Rows[0][0]) > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ���ѵ��λ�����������룡";
					txtItemName.Focus();
					return;
				}
				DataTable dt1 = access.RunSqlDataSet("select * from zj_train_unit where  train_unit_id =" + id).Tables[0];
				access.ExecuteNonQuery(
					string.Format("update zj_train_unit set train_unit_name='{0}' where train_unit_id={1}", txtItemName.Text.Trim(),
								  Convert.ToInt32(id)));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�޸���ѵ��λ:��" + dt1.Rows[0]["train_unit_name"] + "��Ϊ��" + txtItemName.Text + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");

			}




			if (value == "education_level" && mode == "Insert")
			{
				EducationLevelBLL objBLL = new EducationLevelBLL();
				if (objBLL.GetEducationLevelByWhereClause("Education_Level_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ��Ļ��̶ȣ����������룡";
					txtItemName.Focus();
					return;
				}
				objBLL.InsertEducationLevel(txtItemName.Text, txtMemo.Text);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�����Ļ��̶�:" + txtItemName.Text);
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "political_status" && mode == "Insert")
			{
				PoliticalStatusBLL objBLL = new PoliticalStatusBLL();
				if (objBLL.GetPoliticalStatusByWhereClause("Political_Status_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�������ò�����������룡";
					txtItemName.Focus();
					return;
				}
				objBLL.InsertPoliticalStatus(txtItemName.Text,txtMemo.Text);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("����������ò:" + txtItemName.Text);
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "workgroupleader_level" && mode == "Insert")
			{
				WorkGroupLeaderLevelBLL objBLL = new WorkGroupLeaderLevelBLL();
				if (objBLL.GetWorkGroupLeaderLevelByWhereClause("Level_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸð��鳤������������룡";
					txtItemName.Focus();
					return;
				}
				objBLL.InsertWorkGroupLeaderLevel(txtItemName.Text, txtMemo.Text);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�������鳤���:" + txtItemName.Text);
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "technician_type" && mode == "Insert")
			{
				TechnicianTypeBLL objBLL = new TechnicianTypeBLL();
				if (objBLL.GetTechnicianTypeByWhereClause("Type_Name='" + txtItemName.Text + "'","").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸù��˼��ܵȼ������������룡";
					txtItemName.Focus();
					return;
				}
				objBLL.InsertTechnicianType(txtItemName.Text, txtMemo.Text,"");
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�������˼��ܵȼ�:" + txtItemName.Text);
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "technician_title_type" && mode == "Insert")
			{
				TechnicianTitleTypeBLL objBLL = new TechnicianTitleTypeBLL();
				if (objBLL.GetTechnicianTitleTypeByWhereClause("Type_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸøɲ�����ְ�ƣ����������룡";
					txtItemName.Focus();
					return;
				}
				TechnicianTitleType obj = new TechnicianTitleType();
				obj.TypeName = txtItemName.Text;
				obj.TypeLevel = Convert.ToInt32(ddlType.SelectedValue);
				objBLL.InsertTechnicianTitleType(obj);
				txtMemo.ReadOnly = true;
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("�����ɲ�����ְ��:" + txtItemName.Text);
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "education_employee_type" && mode == "Insert")
			{
				EducationEmployeeTypeBLL objBLL = new EducationEmployeeTypeBLL();
				if (objBLL.GetAllEducationEmployeeTypeByWhereClause("Education_Employee_Type_Name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸøɲ�����ְ�ƣ����������룡";
					txtItemName.Focus();
					return;
				}
				EducationEmployeeType obj = new EducationEmployeeType();
				obj.TypeName = txtItemName.Text;
				objBLL.InsertEducationEmployeeType(obj);
				txtMemo.Visible = false;
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("����ְ����Ա����:" + txtItemName.Text);
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "committee_head_ship" && mode == "Insert")
			{
				CommitteeHeadShipBLL objBLL = new CommitteeHeadShipBLL();
				if (objBLL.GetAllCommitteeHeadShipByWhereClause("committee_head_ship_name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�ְ��ίԱ��ְ�����������룡";
					txtItemName.Focus();
					return;
				}
				CommitteeHeadShip obj = new CommitteeHeadShip();
				obj.CommitteeHeadShipName = txtItemName.Text;
				objBLL.InsertCommitteeHeadShip(obj);
				txtMemo.Visible = false;
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("����ְ��ίԱ��ְ��:" + txtItemName.Text);
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
			if (value == "random_exam_modular_type" && mode == "Insert")
			{
				RandomExamModularTypeBLL objBLL = new RandomExamModularTypeBLL();
				if (objBLL.GetAllRandomExamModularTypeByWhereClause("random_exam_modular_type_name='" + txtItemName.Text + "'").Count > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�ģ�鿼��������������룡";
					txtItemName.Focus();
					return;
				}
				RandomExamModularType obj = new RandomExamModularType();
				obj.RandomExamModularTypeName = txtItemName.Text;
				objBLL.InsertRandomExamModularType(obj);
				txtMemo.Visible = false;
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("����ģ�鿼�����:" + txtItemName.Text);
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
			}
            if (value == "trainplan_type" && mode == "Insert")
            {
                OracleAccess oracle = new OracleAccess();
                DataTable dt =
                    oracle.RunSqlDataSet(
                        string.Format(
                            "select * from zj_trainplan_type where  trainplan_type_name='{0}'",
                            txtItemName.Text)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ���ѵ������������룡";
                    txtItemName.Focus();
                    return;
                }

                oracle.ExecuteNonQuery(
                    string.Format(
                        "insert into zj_trainplan_type values(TRAIN_PLAN_TYPE_SEQ.Nextval,'{0}')",
                        txtItemName.Text.Trim()));
                SystemLogBLL objLogBll = new SystemLogBLL();
                objLogBll.WriteLog("����ģ�鿼�����:��" +txtItemName.Text.Trim()+"��");
                Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
            }
			if(value=="safe_level" && mode=="Insert")
			{
				OracleAccess access=new OracleAccess();
				DataTable dt = access.RunSqlDataSet(
					string.Format("select count(1) from zj_safe_level where safe_level_name='{0}'", txtItemName.Text.Trim())).Tables[0];
				if(Convert.ToInt32(dt.Rows[0][0])>0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸð�ȫ�ȼ������������룡";
					txtItemName.Focus();
					return;
				}

				try
				{
					DataTable dtcount = access.RunSqlDataSet("select count(1) from zj_safe_level").Tables[0];
					string strIndex = "(select max(order_index)+1 from zj_safe_level)";
					if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
						strIndex = "1";
			    	string sql =
					string.Format(
						"insert into zj_safe_level values(safe_level_seq.nextval,'{0}',{1})",
						txtItemName.Text.Trim(),strIndex);
				access.ExecuteNonQuery(sql);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("������ȫ�ȼ�:��" + txtItemName.Text.Trim() + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
				}
				catch
				{
					SessionSet.PageMessage = "��������ʧ�ܣ�";
				}

			}
			if (value == "certificate" && mode == "Insert")
			{
				OracleAccess access=new OracleAccess();
				DataTable dt = access.RunSqlDataSet(
					string.Format("select count(1) from zj_certificate where certificate_name='{0}'", txtItemName.Text.Trim())).Tables[0];
				if(Convert.ToInt32(dt.Rows[0][0])>0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ�֤�飬���������룡";
					txtItemName.Focus();
					return;
				}

				try
				{
					DataTable dtcount = access.RunSqlDataSet("select count(1) from zj_certificate").Tables[0];
					string strIndex = "(select max(order_index)+1 from zj_certificate)";
					if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
						strIndex = "1";
			    	string sql =
					string.Format(
						"insert into zj_certificate values(zj_certificate_seq.nextval,'{0}',{1})",
						txtItemName.Text.Trim(),strIndex);
				access.ExecuteNonQuery(sql);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("����֤��:��" + txtItemName.Text.Trim() + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
				}
				catch
				{
					SessionSet.PageMessage = "��������ʧ�ܣ�";
				}

			}
			if (value == "certificate_level" && mode == "Insert")
			{
				OracleAccess access=new OracleAccess();
				try
				{
					DataTable dtcount = access.RunSqlDataSet("select count(1) from zj_certificate_level").Tables[0];
					string strIndex = "(select max(order_index)+1 from zj_certificate_level)";
					if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
						strIndex = "1";
			    	string sql =
					string.Format(
						"insert into zj_certificate_level values(zj_certificate_level_seq.nextval,'{0}','{1}',{2})",
					Convert.ToInt32(ddlType.SelectedValue), txtItemName.Text.Trim(),strIndex);
				access.ExecuteNonQuery(sql);
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("����֤�鼶��:��" + txtItemName.Text.Trim() + "��");
				Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
				}
				catch
				{
					SessionSet.PageMessage = "��������ʧ�ܣ�";
				}

			}
			if (value == "certificate_unit" && mode == "Insert")
			{
				OracleAccess access = new OracleAccess();
				DataTable dt = access.RunSqlDataSet(
					string.Format("select count(1) from zj_certificate_unit where certificate_unit_name='{0}'", txtItemName.Text.Trim())).Tables[0];
				if (Convert.ToInt32(dt.Rows[0][0]) > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ÷�֤��λ�����������룡";
					txtItemName.Focus();
					return;
				}

				try
				{
					DataTable dtcount = access.RunSqlDataSet("select count(1) from zj_certificate_unit").Tables[0];
					string strIndex = "(select max(order_index)+1 from zj_certificate_unit)";
					if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
						strIndex = "1";
					string sql =
					string.Format(
						"insert into zj_certificate_unit values(zj_certificate_unit_seq.nextval,'{0}',{1})",
					 txtItemName.Text.Trim(), strIndex);
					access.ExecuteNonQuery(sql);
					SystemLogBLL objLogBll = new SystemLogBLL();
					objLogBll.WriteLog("������֤��λ:��" + txtItemName.Text.Trim() + "��");
					Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
				}
				catch
				{
					SessionSet.PageMessage = "��������ʧ�ܣ�";
				}

			}
			if (value == "train_unit" && mode == "Insert")
			{
				OracleAccess access = new OracleAccess();
				DataTable dt = access.RunSqlDataSet(
					string.Format("select count(1) from zj_train_unit where train_unit_name='{0}'", txtItemName.Text.Trim())).Tables[0];
				if (Convert.ToInt32(dt.Rows[0][0]) > 0)
				{
					SessionSet.PageMessage = "ϵͳ���Ѵ��ڸ���ѵ��λ�����������룡";
					txtItemName.Focus();
					return;
				}

				try
				{
					DataTable dtcount = access.RunSqlDataSet("select count(1) from zj_train_unit").Tables[0];
					string strIndex = "(select max(order_index)+1 from zj_train_unit)";
					if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
						strIndex = "1";
					string sql =
					string.Format(
						"insert into zj_train_unit values(zj_train_unit_seq.nextval,'{0}',{1})",
					 txtItemName.Text.Trim(), strIndex);
					access.ExecuteNonQuery(sql);
					SystemLogBLL objLogBll = new SystemLogBLL();
					objLogBll.WriteLog("������ѵ��λ:��" + txtItemName.Text.Trim() + "��");
					Response.Write("<script>window.opener.gridCallBack.callback('');window.close();</script>");
				}
				catch
				{
					SessionSet.PageMessage = "��������ʧ�ܣ�";
				}

			}

		}
	}
}
