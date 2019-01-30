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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using RailExamWebApp.Common.Class;


namespace RailExamWebApp.RandomExamOther
{
	public partial class SetDataDictionary :PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				OrgList.SelectedIndex = 0;
				BindGrid(0);
				if (PrjPub.HasDeleteRight("数据字典") && PrjPub.CurrentLoginUser.IsAdmin)//&& PrjPub.CurrentLoginUser.UseType == 0
				{
					HfDeleteRight.Value = "True";
				}
				else
				{
					HfDeleteRight.Value = "False";
				}
				if (PrjPub.HasEditRight("数据字典") && PrjPub.CurrentLoginUser.IsAdmin)//&& PrjPub.CurrentLoginUser.UseType == 0
				{
					HfUpdateRight.Value = "True";
				}
				else
				{
					HfUpdateRight.Value = "False";
				}
			}

			//上移
			//string editUpID = Request.Form.Get("UpID");
			//string editDownID = Request.Form.Get("DownID");
			//if(!string.IsNullOrEmpty(editUpID))
			//{
			//    UpData(editUpID);
			//}

			////下移
			//else if (!string.IsNullOrEmpty(editDownID))
			//{
			//    DownData(editDownID);
			//}

			//新增和修改过后刷新数据
			//if (Request.Form.Get("Refresh") == "true")
			//{
			//    //BindGrid(OrgList.SelectedIndex);
			//    gridCallBack_Callback(null, null);
			//}

			//为orglist添加onchange事件 
			OrgList.Attributes.Add("onchange", "OrgList_onChange()");
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="strLogID"></param>
		protected void DeleteData(string strLogID)
		{
			EmployeeDetailBLL employeeDetailBLL = new EmployeeDetailBLL();
			if (OrgList.SelectedIndex == 0)
			{
				if (employeeDetailBLL.GetEmployeeByWhere("Education_Level_ID=" + strLogID) > 0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				EducationLevelBLL objBll = new EducationLevelBLL();
				string educationName = objBll.GetEducationLevelByEducationLevelID(Convert.ToInt32(strLogID)).EducationLevelName;
				objBll.DeleteEducationLevel(Convert.ToInt32(strLogID),"");
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[0].Text + ":" + educationName);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 1)
			{
				if (employeeDetailBLL.GetEmployeeByWhere("POLITICAL_STATUS_ID=" + strLogID) > 0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				PoliticalStatusBLL objBll = new PoliticalStatusBLL();
				string politicalName = objBll.GetPoliticalStatusByPoliticalStatusID(Convert.ToInt32(strLogID)).PoliticalStatusName;
				objBll.DeletePoliticalStatus(Convert.ToInt32(strLogID));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[1].Text + ":" + politicalName);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 2)
			{
				if (employeeDetailBLL.GetEmployeeByWhere("workgroupleader_type_id=" + strLogID) > 0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				WorkGroupLeaderLevelBLL objBll = new WorkGroupLeaderLevelBLL();
				string wrokgroupName = objBll.GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(Convert.ToInt32(strLogID)).LevelName;
				objBll.DeleteWorkGroupLeaderLevel(Convert.ToInt32(strLogID));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[2].Text + ":" + wrokgroupName);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 3)
			{
				if (employeeDetailBLL.GetEmployeeByWhere("TECHNICIAN_TYPE_ID=" + strLogID) > 0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				TechnicianTypeBLL objBll = new TechnicianTypeBLL();
				string technicianName = objBll.GetTechnicianTypeByTechnicianTypeID(Convert.ToInt32(strLogID)).TypeName;
				objBll.DeleteTechnicianType(Convert.ToInt32(strLogID),"");
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[3].Text + ":" + technicianName);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 4)
			{
				if (employeeDetailBLL.GetEmployeeByWhere("TECHNICAL_TITLE_ID=" + strLogID) > 0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				
				TechnicianTitleTypeBLL objBll = new TechnicianTitleTypeBLL();
				string technicianTitleName =
					objBll.GetTechnicianTitleTypeByTechnicianTitleTypeID(Convert.ToInt32(strLogID)).TypeName;
				objBll.DeleteTechnicianTitleType(Convert.ToInt32(strLogID));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[4].Text + ":" + technicianTitleName);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 5)
			{
				if (employeeDetailBLL.GetEmployeeByWhere("education_employee_type_id="+strLogID) > 0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				EducationEmployeeTypeBLL objBll = new EducationEmployeeTypeBLL();
				string technicianTitleName =
					objBll.GetEducationEmployeeTypeByEducationEmployeeTypeID(Convert.ToInt32(strLogID)).TypeName;
				objBll.DeleteEducationEmployeeType(Convert.ToInt32(strLogID));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[5].Text + ":" + technicianTitleName);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 6)
			{
				if (employeeDetailBLL.GetEmployeeByWhere("committee_head_ship_id="+strLogID) > 0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				CommitteeHeadShipBLL objBll = new CommitteeHeadShipBLL();
				string ShipName =
					objBll.GetCommitteeHeadShipByCommitteeHeadShipID(Convert.ToInt32(strLogID)).CommitteeHeadShipName;
				objBll.DeleteCommitteeHeadShip(Convert.ToInt32(strLogID));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[6].Text + ":" + ShipName);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 7)
			{
				RandomExamModularTypeBLL objBll = new RandomExamModularTypeBLL();
				if (objBll.GetRandomExam(Convert.ToInt32(strLogID))>0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				string TypeName =
					objBll.GetRandomExamModularTypeByTypeID(Convert.ToInt32(strLogID)).RandomExamModularTypeName;
				objBll.DeleteCommitteeHeadShip(Convert.ToInt32(strLogID));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[7].Text + ":" + TypeName);
				BindGrid(OrgList.SelectedIndex);
			}
            if (OrgList.SelectedIndex == 8)
            {
                
                OracleAccess oracle = new OracleAccess();
                DataTable dt =
                    oracle.RunSqlDataSet(
                        string.Format(" select * from zj_train_plan where train_plan_type_id={0}",
                                      Convert.ToInt32(strLogID))).Tables[0];
                if(dt.Rows.Count>0)
                {
                    hfMessage.Value = "该数据字典已被引用，不能删除！";
                    BindGrid(OrgList.SelectedIndex);
                    return;
                }
                string TypeName =
                    oracle.RunSqlDataSet(
                        string.Format("select trainplan_type_name from zj_trainplan_type where trainplan_type_id={0}",
                                      Convert.ToInt32(strLogID))).Tables[0].Rows[0]["trainplan_type_name"].ToString();
                oracle.ExecuteNonQuery(string.Format("delete from zj_trainplan_type  where trainplan_type_id={0}",
                                                     Convert.ToInt32(strLogID)));
                SystemLogBLL objLogBll = new SystemLogBLL();
                objLogBll.WriteLog("删除" + OrgList.Items[8].Text + ":" + TypeName);
                BindGrid(OrgList.SelectedIndex);
            }
			if(OrgList.SelectedIndex == 9)
			{
				OracleAccess access=new OracleAccess();
				DataTable dt = access.RunSqlDataSet("select count(1) from employee where safe_level_id="+strLogID).Tables[0];
				if(Convert.ToInt32(dt.Rows[0][0])>0)
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				string sql = "select safe_level_name from zj_safe_level where safe_level_id="+strLogID;
				DataTable dt1 = access.RunSqlDataSet(sql).Tables[0];
				try
				{
					access.ExecuteNonQuery(@"
                                           update zj_safe_level set order_index=order_index-1 where order_index>
                                          (select order_index from zj_safe_level where  safe_level_id=" + strLogID + ")");
					access.ExecuteNonQuery("delete from zj_safe_level where safe_level_id=" + strLogID);

				}
				catch  
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[9].Text + ":" + dt1.Rows[0][0]);
				BindGrid(OrgList.SelectedIndex);
			} 
			if (OrgList.SelectedIndex == 10)
			{
 				OracleAccess access = new OracleAccess();

				string sql = "select certificate_name from zj_certificate where certificate_id=" + strLogID;
				DataTable dt1 = access.RunSqlDataSet(sql).Tables[0];
				try
				{
					access.ExecuteNonQuery(
						@"
                                           update zj_certificate set order_index=order_index-1 where order_index>
                                          (select order_index from zj_certificate where  certificate_id=" +
						strLogID + ")");
					access.ExecuteNonQuery("delete from zj_certificate where certificate_id=" + strLogID);

				}
				catch
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[10].Text + ":" + dt1.Rows[0][0]);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 11)
			{
				OracleAccess access = new OracleAccess();

				string sql = "select certificate_level_name from zj_certificate_level where certificate_level_id=" + strLogID;
				DataTable dt1 = access.RunSqlDataSet(sql).Tables[0];
				try
				{
					access.ExecuteNonQuery(
						@"
                                           update zj_certificate_level set order_index=order_index-1 where order_index>
                                          (select order_index from zj_certificate_level where  certificate_level_id=" +
						strLogID + ")");
					access.ExecuteNonQuery("delete from zj_certificate_level where certificate_level_id=" + strLogID);

				}
				catch
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[11].Text + ":" + dt1.Rows[0][0]);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 12)
			{
				OracleAccess access = new OracleAccess();

				string sql = "select certificate_unit_name from zj_certificate_unit where certificate_unit_id=" + strLogID;
				DataTable dt1 = access.RunSqlDataSet(sql).Tables[0];
				try
				{
					access.ExecuteNonQuery(
						@"
                                           update zj_certificate_unit set order_index=order_index-1 where order_index>
                                          (select order_index from zj_certificate_unit where  certificate_unit_id=" +
						strLogID + ")");
					access.ExecuteNonQuery("delete from zj_certificate_unit where certificate_unit_id=" + strLogID);

				}
				catch
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[12].Text + ":" + dt1.Rows[0][0]);
				BindGrid(OrgList.SelectedIndex);
			}
			if (OrgList.SelectedIndex == 13)
			{
				OracleAccess access = new OracleAccess();

				string sql = "select train_unit_name from zj_train_unit where train_unit_id=" + strLogID;
				DataTable dt1 = access.RunSqlDataSet(sql).Tables[0];
				try
				{
					access.ExecuteNonQuery(
						@"
                                           update zj_train_unit set order_index=order_index-1 where order_index>
                                          (select order_index from zj_train_unit where  train_unit_id=" +
						strLogID + ")");
					access.ExecuteNonQuery("delete from zj_train_unit where train_unit_id=" + strLogID);

				}
				catch
				{
					hfMessage.Value = "该数据字典已被引用，不能删除！";
					BindGrid(OrgList.SelectedIndex);
					return;
				}
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("删除" + OrgList.Items[13].Text + ":" + dt1.Rows[0][0]);
				BindGrid(OrgList.SelectedIndex);
			}


		}

		/// <summary>
		/// 上移
		/// </summary>
		/// <param name="editUpID"></param>
		protected void UpData(string editUpID)
		{
			if (OrgList.SelectedIndex == 0)
				{
					EducationLevelBLL objBll = new EducationLevelBLL();
					EducationLevel objEducationLevel = objBll.GetEducationLevelByEducationLevelID(Convert.ToInt32(editUpID));
					objEducationLevel.OrderIndex = objEducationLevel.OrderIndex - 1;
					objBll.UpdateEducationLevel(objEducationLevel);
					BindGrid(OrgList.SelectedIndex);
				}
				else if (OrgList.SelectedIndex == 1)
				{
					PoliticalStatusBLL objBll = new PoliticalStatusBLL();
					PoliticalStatus obj = objBll.GetPoliticalStatusByPoliticalStatusID(Convert.ToInt32(editUpID));
					obj.OrderIndex = obj.OrderIndex - 1;
					objBll.UpdatePoliticalStatus(obj);
					BindGrid(OrgList.SelectedIndex);
				}
				else if(OrgList.SelectedIndex==2)
				{
					WorkGroupLeaderLevelBLL objBll = new WorkGroupLeaderLevelBLL();
					WorkGroupLeaderLevel obj = objBll.GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(Convert.ToInt32(editUpID));
					obj.OrderIndex = obj.OrderIndex - 1;
					objBll.UpdateWorkGroupLeaderLevel(obj);
					BindGrid(OrgList.SelectedIndex);
				}
				else if (OrgList.SelectedIndex == 4)
				{
					TechnicianTitleTypeBLL objBll = new TechnicianTitleTypeBLL();
					TechnicianTitleType obj = objBll.GetTechnicianTitleTypeByTechnicianTitleTypeID(Convert.ToInt32(editUpID));
					obj.OrderIndex = obj.OrderIndex - 1;
					objBll.UpdateTechnicianTitleType(obj);
					BindGrid(OrgList.SelectedIndex);
				}
				else if (OrgList.SelectedIndex == 7)
				{
					RandomExamModularTypeBLL objBll = new RandomExamModularTypeBLL();
					RandomExamModularType obj = objBll.GetRandomExamModularTypeByTypeID(Convert.ToInt32(editUpID));
					obj.LevelNum = obj.LevelNum - 1;
					objBll.UpdateRandomExamModularType(obj);
					BindGrid(OrgList.SelectedIndex);
				}
			else if(OrgList.SelectedIndex==9)
			{
				try
				{
				OracleAccess access=new OracleAccess();
				DataTable dt = access.RunSqlDataSet("select * from zj_safe_level where safe_level_id=" + editUpID).Tables[0];
				int index = Convert.ToInt32(dt.Rows[0]["order_index"]) -1;
				access.ExecuteNonQuery(" update zj_safe_level set order_index=order_index+1 where order_index=" + index);
				access.ExecuteNonQuery("  update zj_safe_level set order_index=" + index + " where safe_level_id=" + editUpID);
				BindGrid(OrgList.SelectedIndex);
				}
				catch 
				{
				}

			}
			else if (OrgList.SelectedIndex == 10)
			{
				try
				{
					OracleAccess access = new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_certificate where certificate_id=" + editUpID).Tables[0];
					int index = Convert.ToInt32(dt.Rows[0]["order_index"]) - 1;
					access.ExecuteNonQuery(" update zj_certificate set order_index=order_index+1 where order_index=" + index);
					access.ExecuteNonQuery("  update zj_certificate set order_index=" + index + " where certificate_id=" + editUpID);
					BindGrid(OrgList.SelectedIndex);
				}
				catch
				{
				}

			}
			else if (OrgList.SelectedIndex == 11)
			{
				try
				{
					OracleAccess access = new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_certificate_level where certificate_level_id=" + editUpID).Tables[0];
					int index = Convert.ToInt32(dt.Rows[0]["order_index"]) - 1;
					access.ExecuteNonQuery(" update zj_certificate_level set order_index=order_index+1 where order_index=" + index);
					access.ExecuteNonQuery("  update zj_certificate_level set order_index=" + index + " where certificate_level_id=" + editUpID);
					BindGrid(OrgList.SelectedIndex);
				}
				catch
				{
				}

			}
			else if (OrgList.SelectedIndex == 12)
			{
				try
				{
					OracleAccess access = new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_certificate_unit where certificate_unit_id=" + editUpID).Tables[0];
					int index = Convert.ToInt32(dt.Rows[0]["order_index"]) - 1;
					access.ExecuteNonQuery(" update zj_certificate_unit set order_index=order_index+1 where order_index=" + index);
					access.ExecuteNonQuery("  update zj_certificate_unit set order_index=" + index + " where certificate_unit_id=" + editUpID);
					BindGrid(OrgList.SelectedIndex);
				}
				catch
				{
				}

			}
			else if (OrgList.SelectedIndex == 13)
			{
				try
				{
					OracleAccess access = new OracleAccess();
					DataTable dt = access.RunSqlDataSet("select * from zj_train_unit where train_unit_id=" + editUpID).Tables[0];
					int index = Convert.ToInt32(dt.Rows[0]["order_index"]) - 1;
					access.ExecuteNonQuery(" update zj_train_unit set order_index=order_index+1 where order_index=" + index);
					access.ExecuteNonQuery("  update zj_train_unit set order_index=" + index + " where train_unit_id=" + editUpID);
					BindGrid(OrgList.SelectedIndex);
				}
				catch
				{
				}

			}

		}

		/// <summary>
		/// 下移
		/// </summary>
		/// <param name="editDownID"></param>
		protected void DownData(string editDownID)
		{
			if (OrgList.SelectedIndex == 0)
				{
					EducationLevelBLL objBll = new EducationLevelBLL();
					EducationLevel objEducationLevel = objBll.GetEducationLevelByEducationLevelID(Convert.ToInt32(editDownID));
					objEducationLevel.OrderIndex = objEducationLevel.OrderIndex + 1;
					objBll.UpdateEducationLevel(objEducationLevel);
					BindGrid(OrgList.SelectedIndex);
				}
				else if (OrgList.SelectedIndex == 1)
				{
					PoliticalStatusBLL objBll = new PoliticalStatusBLL();
					PoliticalStatus obj = objBll.GetPoliticalStatusByPoliticalStatusID(Convert.ToInt32(editDownID));
					obj.OrderIndex = obj.OrderIndex + 1;
					objBll.UpdatePoliticalStatus(obj);
					BindGrid(OrgList.SelectedIndex);
				}
				else if (OrgList.SelectedIndex == 2)
				{
					WorkGroupLeaderLevelBLL objBll = new WorkGroupLeaderLevelBLL();
					WorkGroupLeaderLevel obj = objBll.GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(Convert.ToInt32(editDownID));
					obj.OrderIndex = obj.OrderIndex + 1;
					objBll.UpdateWorkGroupLeaderLevel(obj);
					BindGrid(OrgList.SelectedIndex);
				}
				else if (OrgList.SelectedIndex == 4)
				{
					TechnicianTitleTypeBLL objBll = new TechnicianTitleTypeBLL();
					TechnicianTitleType obj = objBll.GetTechnicianTitleTypeByTechnicianTitleTypeID(Convert.ToInt32(editDownID));
					obj.OrderIndex = obj.OrderIndex +1;
					objBll.UpdateTechnicianTitleType(obj);
					BindGrid(OrgList.SelectedIndex);
				}
				else if (OrgList.SelectedIndex == 7)
				{
					RandomExamModularTypeBLL objBll = new RandomExamModularTypeBLL();
					RandomExamModularType obj = objBll.GetRandomExamModularTypeByTypeID(Convert.ToInt32(editDownID));
					obj.LevelNum = obj.LevelNum + 1;
					objBll.UpdateRandomExamModularType(obj);
					BindGrid(OrgList.SelectedIndex);
				}
				else if (OrgList.SelectedIndex == 9)
				{
					try
					{
						OracleAccess access = new OracleAccess();
						DataTable dt = access.RunSqlDataSet("select * from zj_safe_level where safe_level_id=" + editDownID).Tables[0];
						int index = Convert.ToInt32(dt.Rows[0]["order_index"]) + 1;
						access.ExecuteNonQuery(" update zj_safe_level set order_index=order_index-1 where order_index=" + index);
						access.ExecuteNonQuery("update zj_safe_level set order_index=" + index + " where safe_level_id=" + editDownID);
						BindGrid(OrgList.SelectedIndex);
					}
					catch
					{
					}

				}
				else if (OrgList.SelectedIndex == 10)
				{
					try
					{
						OracleAccess access = new OracleAccess();
						DataTable dt = access.RunSqlDataSet("select * from zj_certificate where certificate_id=" + editDownID).Tables[0];
						int index = Convert.ToInt32(dt.Rows[0]["order_index"]) + 1;
						access.ExecuteNonQuery(" update zj_certificate set order_index=order_index-1 where order_index=" + index);
						access.ExecuteNonQuery("update zj_certificate set order_index=" + index + " where certificate_id=" + editDownID);
						BindGrid(OrgList.SelectedIndex);
					}
					catch
					{
					}

				}
				else if (OrgList.SelectedIndex == 11)
				{
					try
					{
						OracleAccess access = new OracleAccess();
						DataTable dt = access.RunSqlDataSet("select * from zj_certificate_level where certificate_level_id=" + editDownID).Tables[0];
						int index = Convert.ToInt32(dt.Rows[0]["order_index"]) + 1;
						access.ExecuteNonQuery(" update zj_certificate_level set order_index=order_index-1 where order_index=" + index);
						access.ExecuteNonQuery("update zj_certificate_level set order_index=" + index + " where certificate_level_id=" + editDownID);
						BindGrid(OrgList.SelectedIndex);
					}
					catch
					{
					}

				}
				else if (OrgList.SelectedIndex == 12)
				{
					try
					{
						OracleAccess access = new OracleAccess();
						DataTable dt = access.RunSqlDataSet("select * from zj_certificate_unit where certificate_unit_id=" + editDownID).Tables[0];
						int index = Convert.ToInt32(dt.Rows[0]["order_index"]) + 1;
						access.ExecuteNonQuery(" update zj_certificate_unit set order_index=order_index-1 where order_index=" + index);
						access.ExecuteNonQuery("update zj_certificate_unit set order_index=" + index + " where certificate_unit_id=" + editDownID);
						BindGrid(OrgList.SelectedIndex);
					}
					catch
					{
					}

				}
				else if (OrgList.SelectedIndex == 13)
				{
					try
					{
						OracleAccess access = new OracleAccess();
						DataTable dt = access.RunSqlDataSet("select * from zj_train_unit where train_unit_id=" + editDownID).Tables[0];
						int index = Convert.ToInt32(dt.Rows[0]["order_index"]) + 1;
						access.ExecuteNonQuery(" update zj_train_unit set order_index=order_index-1 where order_index=" + index);
						access.ExecuteNonQuery("update zj_train_unit set order_index=" + index + " where train_unit_id=" + editDownID);
						BindGrid(OrgList.SelectedIndex);
					}
					catch
					{
					}

				}

		}

		protected void Grid1_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid2_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid3_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid4_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid5_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid6_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid7_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid8_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
        protected void Grid9_NeedRebind(object sender, EventArgs e)
        {
            BindGrid(OrgList.SelectedIndex);
        }
		protected void Grid10_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid11_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid12_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid13_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		protected void Grid14_NeedRebind(object sender, EventArgs e)
		{
			BindGrid(OrgList.SelectedIndex);
		}
		private void BindGrid(int n)
		{
			if (n == 0)
			{
				EducationLevelBLL objBLL = new EducationLevelBLL();
				IList<EducationLevel> objList = objBLL.GetAllEducationLevel();
				Grid1.DataSource = objList;
				Grid1.DataBind();
				Grid1.Visible = true;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
                Grid9.Visible = false;
			}
			if (n == 1)
			{
				PoliticalStatusBLL objBLL = new PoliticalStatusBLL();
				IList<PoliticalStatus> objList = objBLL.GetAllPoliticalStatus();
				Grid2.DataSource = objList;
				Grid2.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = true;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
                Grid9.Visible = false;
			}
			if (n == 2)
			{
				WorkGroupLeaderLevelBLL objBLL = new WorkGroupLeaderLevelBLL();
				IList<WorkGroupLeaderLevel> objList = objBLL.GetAllWorkGroupLeaderLevel();
				Grid3.DataSource = objList;
				Grid3.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = true;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
                Grid9.Visible = false;
			}
			if (n == 3)
			{
				TechnicianTypeBLL objBLL = new TechnicianTypeBLL();
				IList<TechnicianType> objList = objBLL.GetAllTechnicianType();
				Grid4.DataSource = objList;
				Grid4.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = true;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
                Grid9.Visible = false;
			}
			if(n==4)
			{
				TechnicianTitleTypeBLL objBLL=new TechnicianTitleTypeBLL();
				IList<TechnicianTitleType> objList = objBLL.GetAllTechnicianTitleType();
				Grid5.DataSource = objList;
				Grid5.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = true;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
                Grid9.Visible = false;
			}
			if (n == 5)
			{
				EducationEmployeeTypeBLL objBLL = new EducationEmployeeTypeBLL();
				IList<EducationEmployeeType> objList = objBLL.GetAllEducationEmployeeType();
				Grid6.DataSource = objList;
				Grid6.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = true;
				Grid7.Visible = false;
				Grid8.Visible = false;
                Grid9.Visible = false;
			}
			if (n == 6)
			{
				CommitteeHeadShipBLL objBLL = new CommitteeHeadShipBLL();
				IList<CommitteeHeadShip> objList = objBLL.GetAllCommitteeHeadShip();
				Grid7.DataSource = objList;
				Grid7.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = true;
				Grid8.Visible = false;
                Grid9.Visible = false;
			}
			if (n == 7)
			{
				RandomExamModularTypeBLL objBLL = new RandomExamModularTypeBLL();
				IList<RandomExamModularType> objList = objBLL.GetAllRandomExamModularType();
				Grid8.DataSource = objList;
				Grid8.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = true;
                Grid9.Visible = false;
			}
            if (n == 8)
            {
               OracleAccess oracle=new OracleAccess();
               DataTable dt= oracle.RunSqlDataSet("select * from zj_trainplan_type order by trainplan_type_id").Tables[0];
                Grid9.DataSource = dt;
                Grid9.DataBind();
                Grid1.Visible = false;
                Grid2.Visible = false;
                Grid3.Visible = false;
                Grid4.Visible = false;
                Grid5.Visible = false;
                Grid6.Visible = false;
                Grid7.Visible = false;
                Grid8.Visible = false;
                Grid9.Visible = true;
            }
			if (n == 9)
			{
				OracleAccess oracle = new OracleAccess();
				DataTable dt = oracle.RunSqlDataSet("select * from zj_safe_level order by order_index").Tables[0];
				Grid10.DataSource = dt;
				Grid10.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
				Grid9.Visible = false;
				Grid10.Visible = true;
			}
			if(n==10)
			{
				OracleAccess oracle = new OracleAccess();
				DataTable dt = oracle.RunSqlDataSet("select * from zj_certificate order by order_index").Tables[0];
				Grid11.DataSource = dt;
				Grid11.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
				Grid9.Visible = false;
				Grid10.Visible = false;
				Grid11.Visible = true;
			}
			if (n == 11)
			{
				OracleAccess oracle = new OracleAccess();
				DataTable dt = oracle.RunSqlDataSet(@"select a.*,b.certificate_name from zj_certificate_level a 
                                                      left join zj_certificate b on a.certificate_id=b.certificate_id order by a.order_index").Tables[0];
				Grid12.DataSource = dt;
				Grid12.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
				Grid9.Visible = false;
				Grid10.Visible = false;
				Grid11.Visible = false;
				Grid12.Visible = true;
			}
			if (n == 12)
			{
				OracleAccess oracle = new OracleAccess();
				DataTable dt = oracle.RunSqlDataSet(" select * from zj_certificate_unit order by order_index").Tables[0];
				Grid13.DataSource = dt;
				Grid13.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
				Grid9.Visible = false;
				Grid10.Visible = false;
				Grid11.Visible = false;
				Grid12.Visible = false;
				Grid13.Visible = true;
			}
			if (n == 13)
			{
				OracleAccess oracle = new OracleAccess();
				DataTable dt = oracle.RunSqlDataSet(" select * from zj_train_unit order by order_index").Tables[0];
				Grid14.DataSource = dt;
				Grid14.DataBind();
				Grid1.Visible = false;
				Grid2.Visible = false;
				Grid3.Visible = false;
				Grid4.Visible = false;
				Grid5.Visible = false;
				Grid6.Visible = false;
				Grid7.Visible = false;
				Grid8.Visible = false;
				Grid9.Visible = false;
				Grid10.Visible = false;
				Grid11.Visible = false;
				Grid12.Visible = false;
				Grid13.Visible = false;
				Grid14.Visible = true;
			}

		}

		protected void gridCallBack_Callback(object sender, CallBackEventArgs e)
		{
			hfMessage.Value = "";
			int n = OrgList.SelectedIndex;
			try
			{
				if (e.Parameters[0] != "")
				{
					if (e.Parameters[1] == "Up")
						UpData(e.Parameters[0]);
					else if (e.Parameters[1] == "Down")
						DownData(e.Parameters[0]);
					else
					{
						DeleteData(e.Parameters[0]);
					}
				}
				else if (e.Parameters[0] == "")
				{
					BindGrid(n);
				}
				if (n == 0)
				{
					Grid1.RenderControl(e.Output);
				}
				else if (n == 1)
				{
					Grid2.RenderControl(e.Output);
				}
				else if (n == 2)
				{
					Grid3.RenderControl(e.Output);
				}
				else if (n == 3)
				{
					Grid4.RenderControl(e.Output);
				}
				else if (n == 4)
				{
					Grid5.RenderControl(e.Output);
				}
				else if (n == 5)
				{
					Grid6.RenderControl(e.Output);
				}
				else if (n == 6)
				{
					Grid7.RenderControl(e.Output);
				}
				else if (n == 7)
				{
					Grid8.RenderControl(e.Output);
				}
                else if (n == 8)
                {
                    Grid9.RenderControl(e.Output);
                }
				else if (n == 9)
				{
					Grid10.RenderControl(e.Output);
				}
				else if (n == 10)
				{
					Grid11.RenderControl(e.Output);
				}
				else if (n == 11)
				{
					Grid12.RenderControl(e.Output);
				}
				else if (n == 12)
				{
					Grid13.RenderControl(e.Output);
				}
				else if (n == 13)
				{
					Grid14.RenderControl(e.Output);
				}
			}
			catch
			{
				BindGrid(n);
			}

			hfMessage.RenderControl(e.Output);
		}
 
	}
}
