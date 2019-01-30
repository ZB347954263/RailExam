using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class MatchImport : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                return;
            }

			txtOrg.Text = hfOrgName.Value;

			if (!IsPostBack && !Grid1.IsCallback)
			{
				lbltitle.Visible = false;
				Grid1.Visible = false;

				if (PrjPub.CurrentLoginUser.SuitRange != 1)
				{
					ImgSelectOrg.Visible = false;
					OrganizationBLL objbll = new OrganizationBLL();
					txtOrg.Text = objbll.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID).ShortName;
					hfOrg.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
				}
			}
			else
			{
				OrganizationBLL objbll = new OrganizationBLL();
				txtOrg.Text = objbll.GetOrganization(Convert.ToInt32(hfOrg.Value)).ShortName;
			}

			string str = Request.Form.Get("Refresh");
			if (str != null && str == "refresh")
			{
				BindGrid();
			}

			string strAdd = Request.Form.Get("add");
			if(!string.IsNullOrEmpty(strAdd))
			{
				Import(1, strAdd);
				BindGrid();
			}

			string strUpdate = Request.Form.Get("update");
			if (!string.IsNullOrEmpty(strUpdate))
			{
				Import(2,strUpdate);
				BindGrid();
			}
		}

		private void  BindGrid()
		{
			EmployeeErrorBLL objErrorBll = new EmployeeErrorBLL();
			Grid1.DataSource = objErrorBll.GetEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(hfOrg.Value));
			Grid1.DataBind();
			Grid1.Visible = true;
		}

		private void Import(int type,string strID)
		{
			EmployeeErrorBLL objBll = new EmployeeErrorBLL();
			EmployeeError objError = new EmployeeError();

			objError = objBll.GetEmployeeError(Convert.ToInt32(strID));

			#region 检测员工信息
			Hashtable htOrg = GetOrgInfo();
			Hashtable htPost = GetPostInfo();
			Hashtable htEducationLevel = GetEducationLevel();
			Hashtable htPoliticalStatus = GetPoliticalStatus();
			Hashtable htEmployeeType = GetEmployeeType();
			Hashtable htWorkGroupLeaderType = GetWorkGroupLeaderType();
			Hashtable htEducationEmployeeType = GetEducationEmployeeType();
			Hashtable htCommitteeHeadship = GetCommitteeHeadship();
			Hashtable htEmployeeTransportType = GetEmployeeTransportType();
			Hashtable htTechnicalTitle = GetTechnicalTitle();
			Hashtable htSkillLevel = GetSkillLevel();
			Hashtable htEmployeeLevel = GetEmployeeLevel();
			Hashtable htTeacherType = GetTeacherType();

			Hashtable htShopNeedAdd = new Hashtable();
			Hashtable htPostNo = new Hashtable(); //为检测Excel中员工编码是否重复
			Hashtable htPostNeedAdd = new Hashtable();
			Hashtable htSalaryNo = new Hashtable();

			PostBLL objPostBll = new PostBLL();

			EmployeeBLL objEmployeeBll = new EmployeeBLL();
			EmployeeDetailBLL objDetailBll = new EmployeeDetailBLL();
			RailExam.Model.EmployeeDetail objEmployee = new RailExam.Model.EmployeeDetail();

			if (type == 2)
			{
				objEmployee = objDetailBll.GetEmployee(objError.EmployeeID);
			}

			//单位名称
			if (objError.OrgName != txtOrg.Text)
			{
				SessionSet.PageMessage = "单位名称填写错误";
				return;
			}

			if (objError.OrgPath == "")
			{
				SessionSet.PageMessage = "部门名称不能为空";
				return;
			}

			//组织机构
			string strOrg;
			if (string.IsNullOrEmpty(objError.GroupName))
			{
				strOrg = objError.OrgName + "-" + objError.OrgPath;
			}
			else
			{
				strOrg = objError.OrgName + "-" + objError.OrgPath + "-" + objError.GroupName;
			}

			if (!htOrg.ContainsKey(strOrg))
			{
				if (string.IsNullOrEmpty(objError.GroupName))
				{
					if (!htShopNeedAdd.ContainsKey(objError.OrgPath))
					{
						htShopNeedAdd.Add(objError.OrgPath, new Hashtable());
					}

					//如果组织机构需要新增
					objEmployee.Memo = strOrg;
				}
				else
				{
					if (!htShopNeedAdd.ContainsKey(objError.OrgPath))
					{
						htShopNeedAdd.Add(objError.OrgPath, new Hashtable());
					}

					Hashtable htGroupNeedAdd = (Hashtable) htShopNeedAdd[objError.OrgPath];
					if (!htGroupNeedAdd.ContainsKey(objError.GroupName))
					{
						htGroupNeedAdd.Add(objError.GroupName, objError.GroupName);
						htShopNeedAdd[objError.OrgPath] = htGroupNeedAdd;
					}

					//如果组织机构需要新增
					objEmployee.Memo = strOrg;
				}
			}
			else
			{
				objEmployee.OrgID = Convert.ToInt32(htOrg[strOrg]);
				objEmployee.Memo = string.Empty;
			}

			//姓名不能为空
			if (string.IsNullOrEmpty(objError.EmployeeName))
			{
				SessionSet.PageMessage = "员工姓名不能为空";
				return;
			}
			else
			{
				if (objError.EmployeeName.Length > 20)
				{
					SessionSet.PageMessage = "员工姓名不能超过20位";
					return;
				}

				objEmployee.EmployeeName = objError.EmployeeName;
				objEmployee.PinYinCode = Pub.GetChineseSpell(objError.EmployeeName);
			}

			//身份证号不能为空
			if (string.IsNullOrEmpty(objError.IdentifyCode))
			{
				SessionSet.PageMessage = "身份证号不能为空";
				return;
			}
			else
			{
				if (objError.IdentifyCode.Length > 18)
				{
					SessionSet.PageMessage = "身份证号不能超过18位";
					return;
				}

				objEmployee.IdentifyCode = objError.IdentifyCode;
			}

			//工作证号
			if (!string.IsNullOrEmpty(objError.PostNo))
			{
				if (objError.PostNo.Length > 14)
				{
					SessionSet.PageMessage = "工作证号不能超过14位";
					return;
				}

				objEmployee.PostNo = objError.PostNo;
			}
			else
			{
				objEmployee.PostNo = "";
			}

			//性别
			if (objError.Sex != "男" && objError.Sex != "女")
			{
				SessionSet.PageMessage = "性别必须为男或女";
				return;
			}
			else
			{
				objEmployee.Sex = objError.Sex;
			}

			//籍贯
			if(!string.IsNullOrEmpty(objError.NativePlace))
			{
				if (objError.NativePlace.Length > 20)
				{
					SessionSet.PageMessage = "籍贯不能超过20位";
					return;
				}
				else
				{
					objEmployee.NativePlace = objError.NativePlace;
				}
			}
			else
			{
				objEmployee.NativePlace = string.Empty;
			}


			//民族
			if (!string.IsNullOrEmpty(objError.Folk))
			{
				if (objError.Folk.Length > 10)
				{
					SessionSet.PageMessage = "民族不能超过10位";
					return;
				}
				else
				{
					objEmployee.Folk = objError.Folk;
				}
			}
			else
			{
				objEmployee.Folk = string.Empty;
			}
			
			//婚姻状况
			if (objError.Wedding == "未婚")
			{
				objEmployee.Wedding = 0;
			}
			else
			{
				objEmployee.Wedding = 1;
			}

			//现文化程度
			if (string.IsNullOrEmpty(objError.EducationLevel))
			{
				SessionSet.PageMessage = "现文化程度不能为空";
				return;
			}
			else
			{
				if (!htEducationLevel.ContainsKey(objError.EducationLevel))
				{
					SessionSet.PageMessage = "现文化程度在系统中不存在";
					return;
				}
				else
				{
					objEmployee.EducationLevelID = Convert.ToInt32(htEducationLevel[objError.EducationLevel]);
				}
			}

			//政治面貌
			if (!string.IsNullOrEmpty(objError.PoliticalStatus))
			{
				if (!htPoliticalStatus.ContainsKey(objError.PoliticalStatus))
				{
					SessionSet.PageMessage = "政治面貌在系统中不存在";
					return;
				}
				else
				{
					objEmployee.PoliticalStatusID = Convert.ToInt32(htPoliticalStatus[objError.PoliticalStatus]);
				}
			}

			//毕(肄)业学校(单位)
			if(!string.IsNullOrEmpty(objError.GraduateUniversity))
			{
				if (objError.GraduateUniversity.Length > 50)
				{
					SessionSet.PageMessage = "毕(肄)业学校(单位)不能超过50位";
					return;
				}
				else
				{
					objEmployee.GraduateUniversity = objError.GraduateUniversity;
				}
			}
			else
			{
				objError.GraduateUniversity = string.Empty;
			}

			//所学专业
			if(!string.IsNullOrEmpty(objEmployee.StudyMajor))
			{
				if (objEmployee.StudyMajor.Length > 50)
				{
					SessionSet.PageMessage = "所学专业不能超过50位";
					return;
				}
				else
				{
					objEmployee.StudyMajor = objEmployee.StudyMajor;
				}
			}
			else
			{
				objEmployee.StudyMajor = string.Empty;
			}

			//工作地址
			if(!string.IsNullOrEmpty(objError.Address))
			{
				if (objError.Address.Length > 100)
				{
					SessionSet.PageMessage = "工作地址不能超过100位";
					return;
				}
				else
				{
					objEmployee.Address = objError.Address;
				}
			}
			else
			{
				objEmployee.Address = string.Empty;
			}

			//邮政编码
			if(!string.IsNullOrEmpty(objEmployee.PostCode))
			{
				if (objError.PostCode.Length > 6)
				{
					SessionSet.PageMessage = "邮政编码不能超过6位";
					return;
				}
				else
				{
					objEmployee.PostCode = objError.PostCode;
				}
			}
			else
			{
				objEmployee.PostCode = string.Empty;
			}

			//职务级别
			if (!string.IsNullOrEmpty(objError.EmployeeLevel))
			{
				if (!htEmployeeLevel.ContainsKey(objError.EmployeeLevel))
				{
					SessionSet.PageMessage = "职务级别在系统中不存在";
					return;
				}
				else
				{
					objEmployee.EmployeeLevelID = Convert.ToInt32(htEmployeeLevel[objError.EmployeeLevel]);
				}
			}

			//出生日期
			try
			{
				string strBirth = objError.Birthday;
				if (strBirth.IndexOf("-") >= 0)
				{
					objEmployee.Birthday = Convert.ToDateTime(strBirth);
				}
				else
				{
					if (strBirth.Length != 8)
					{
						SessionSet.PageMessage = "出生日期填写错误";
						return;
					}
					else
					{
						strBirth = strBirth.Insert(4, "-");
						strBirth = strBirth.Insert(7, "-");
						objEmployee.Birthday = Convert.ToDateTime(strBirth);
					}
				}

				if (Convert.ToDateTime(strBirth) < Convert.ToDateTime("1775-1-1") ||
				    Convert.ToDateTime(strBirth) > Convert.ToDateTime("1993-12-31"))
				{
					SessionSet.PageMessage = "出生日期填写错误";
					return;
				}
			}
			catch
			{
				SessionSet.PageMessage = "出生日期填写错误";
				return;
			}

			//入路工作日期
			try
			{
				string strJoin = objError.WorkDate;
				if (strJoin.IndexOf("-") >= 0)
				{
					objEmployee.WorkDate = Convert.ToDateTime(strJoin);
				}
				else
				{
					if (strJoin.Length != 8)
					{
						SessionSet.PageMessage = "入路工作日期填写错误";
						return;
					}
					else
					{
						strJoin = strJoin.Insert(4, "-");
						strJoin = strJoin.Insert(7, "-");
						objEmployee.WorkDate = Convert.ToDateTime(strJoin);
					}
				}

				if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
				{
					SessionSet.PageMessage = "入路工作日期填写错误";
					return;
				}
			}
			catch
			{
				SessionSet.PageMessage = "入路工作日期填写错误";
				return;
			}


			//参加工作日期
			try
			{
				string strJoin = objError.BeginDate;
				if (strJoin.IndexOf("-") >= 0)
				{
					objEmployee.BeginDate = Convert.ToDateTime(strJoin);
				}
				else
				{
					if (strJoin.Length != 8)
					{
						SessionSet.PageMessage = "参加工作日期填写错误";
						return;
					}
					else
					{
						strJoin = strJoin.Insert(4, "-");
						strJoin = strJoin.Insert(7, "-");
						objEmployee.BeginDate = Convert.ToDateTime(strJoin);
					}
				}

				if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
				{
					SessionSet.PageMessage = "参加工作日期填写错误";
					return;
				}
			}
			catch
			{
				SessionSet.PageMessage = "参加工作日期填写错误";
				return;
			}

			//干部工人标识
			if (string.IsNullOrEmpty(objError.EmployeeType ))
			{
				SessionSet.PageMessage = "干部工人标识不能为空！";
				return;
			}
			else
			{
				if (!htEmployeeType.ContainsKey(objError.EmployeeType))
				{
					SessionSet.PageMessage = "干部工人标识在系统中不存在！";
					return;
				}
				else
				{
					objEmployee.EmployeeTypeID = Convert.ToInt32(htEmployeeType[objError.EmployeeType]);
				}
			}

			if(objEmployee.EmployeeTypeID == 1)
			{
				//岗位
				if (string.IsNullOrEmpty(objError.PostPath))
				{
					SessionSet.PageMessage = "岗位不能为空！";
					return;
				}
				else
				{
					IList<RailExam.Model.Post> objPost =
						objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + objError.PostPath + "'");
					if (objPost.Count == 0)
					{
						SessionSet.PageMessage = "岗位在系统中不存在！";
						return;
					}

					objEmployee.PostID = Convert.ToInt32(htPost[objError.PostPath]);
				}
			}
			else
			{
				//岗位
				if (string.IsNullOrEmpty(objError.PostPath))
				{
					SessionSet.PageMessage = "职务不能为空！";
					return;
				}
				else
				{
					IList<RailExam.Model.Post> objPost =
						objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + objError.PostPath + "'");
					if (objPost.Count == 0)
					{
						htPostNeedAdd.Add(objError.PostPath,objError.PostPath);
					}
					else
					{
						objEmployee.PostID = objPost[0].PostId;
					}
				}
			}



			//班组长类型
			if (string.IsNullOrEmpty(objError.WorkGroupLeader))
			{
				objEmployee.IsGroupLeader = 0;
			}
			else
			{
				if (!htWorkGroupLeaderType.ContainsKey(objError.WorkGroupLeader))
				{
					SessionSet.PageMessage = "班组长类型在系统中不存在！";
					return;
				}

				objEmployee.WorkGroupLeaderTypeID = Convert.ToInt32(htWorkGroupLeaderType[objError.WorkGroupLeader]);
				objEmployee.IsGroupLeader = 1;
			}


			if (!string.IsNullOrEmpty(objError.EducationEmployee))
			{
				if (!htEducationEmployeeType.ContainsKey(objError.EducationEmployee))
				{
					SessionSet.PageMessage = "职教干部类型在系统中不存在！";
					return;
				}
				else
				{
					objEmployee.EducationEmployeeTypeID = Convert.ToInt32(htEducationEmployeeType[objError.EducationEmployee]);
				}
			}

			//职教委员会职务
			if (!string.IsNullOrEmpty(objError.CommitteeHeadShip))
			{
				if (!htCommitteeHeadship.ContainsKey(objError.CommitteeHeadShip))
				{
					SessionSet.PageMessage = "职教委员会职务在系统中不存在！";
					return;
				}
				else
				{
					objEmployee.CommitteeHeadShipID = Convert.ToInt32(htCommitteeHeadship[objError.CommitteeHeadShip]);
				}
			}

			//教师类别
			if (!string.IsNullOrEmpty(objError.TeacherType))
			{
				if (!htTeacherType.ContainsKey(objError.TeacherType))
				{
					SessionSet.PageMessage = "教师类别在系统中不存在";
					return;
				}
				else
				{
					objEmployee.TeacherTypeID = Convert.ToInt32(htTeacherType[objError.TeacherType]);
				}
			}

			//人员岗位状态
			if (objError.OnPost != "在岗工作")
			{
				objEmployee.Dimission = true;
			}
			else
			{
				objEmployee.Dimission = false;
			}

			//在岗职工按岗位分组
			if (string.IsNullOrEmpty(objError.EmployeeTransportType))
			{
				//SessionSet.PageMessage = "当单位名称为“运输业”时，在岗职工按岗位分组不能为空！";
				//return;
			}
			else
			{
				if (!htEmployeeTransportType.ContainsKey(objError.EmployeeTransportType))
				{
					SessionSet.PageMessage = "在岗职工按岗位分组在系统中不存在！";
					return;
				}
				else
				{
					objEmployee.EmployeeTransportTypeID = Convert.ToInt32(htEmployeeTransportType[objError.EmployeeTransportType]);
				}
			}

			//现技术职务名称
			if (objEmployee.EmployeeTypeID == 1)
			{

				if (!string.IsNullOrEmpty(objError.TechnicalTitle))
				{
					SessionSet.PageMessage = "当干部工人标识为“工人”时，现技术职务名称必须为空！";
					return;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(objError.TechnicalTitle))
				{
					if (!htTechnicalTitle.ContainsKey(objError.TechnicalTitle))
					{
						SessionSet.PageMessage = "现技术职务名称在系统中不存在！";
						return;
					}
					else
					{
						objEmployee.TechnicalTitleID = Convert.ToInt32(htTechnicalTitle[objError.TechnicalTitle]);
					}
				}
			}

			//技术等级
			if (objEmployee.EmployeeTypeID == 1)
			{
				if (!string.IsNullOrEmpty(objError.TechnicalSkill))
				{
					if (!htSkillLevel.ContainsKey(objError.TechnicalSkill))
					{
						SessionSet.PageMessage = "技术等级在系统中不存在！";
						return;
					}
					else
					{
						objEmployee.TechnicianTypeID = Convert.ToInt32(htSkillLevel[objError.TechnicalSkill]);
					}
				}
				else
				{
					objEmployee.TechnicianTypeID = 1;
				}
			}
			else
			{
				objEmployee.TechnicianTypeID = 1;
			}

			//岗位培训合格证编号

			if (!string.IsNullOrEmpty(objError.SalaryNo))
			{
				if (objError.SalaryNo.Length > 20)
				{
					SessionSet.PageMessage = "岗位培训合格证编号不能超过20位";
					return;
				}

				//工作证号在Excel中不能重复

				if (htPostNo.ContainsKey(objError.SalaryNo))
				{
					SessionSet.PageMessage = "岗位培训合格证编号在Excel中与序号为" + htPostNo[objError.SalaryNo] + "的岗位培训合格证编号重复";
					return;
				}
				else
				{
					htPostNo.Add(objError.SalaryNo, objError.ExcelNo);
				}

				IList<RailExam.Model.Employee> objView = new List<RailExam.Model.Employee>();

				if(type== 1)
				{
					objView = objEmployeeBll.GetEmployeeByWhereClause("Home_Phone='" + objError.SalaryNo + "'");
				}
				else if (type == 2)
				{
					objView = objEmployeeBll.GetEmployeeByWhereClause("a.Employee_ID != " + objEmployee.EmployeeID + " and Home_Phone='" + objError.SalaryNo + "'");
				}

				if (objView.Count > 0)
				{
					SessionSet.PageMessage = "岗位培训合格证编号已在系统中存在";
					return;
				}

				objEmployee.HomePhone = objError.SalaryNo;
			}
			else
			{
				objEmployee.HomePhone = string.Empty;
			}


			if (!string.IsNullOrEmpty(objError.WorkNo))
			{
				if (objError.WorkNo.Length > 20)
				{
					SessionSet.PageMessage = "工资编号不能超过20位";
					return;
				}

				//工作证号在Excel中不能重复

				if (htSalaryNo.ContainsKey(objError.WorkNo))
				{
					SessionSet.PageMessage = "工资编号在Excel中与序号为" + htSalaryNo[objError.WorkNo] + "的工资编号重复";
					return;
				}
				else
				{
					htSalaryNo.Add(objError.WorkNo, objError.ExcelNo);
				}


				IList<RailExam.Model.EmployeeDetail> objView = new List<RailExam.Model.EmployeeDetail>();

				if (type == 1)
				{
					objView = objDetailBll.GetEmployeeByWhereClause("GetStationOrgID(a.Org_ID)=" + hfOrg.Value + " and Work_No='" + objError.WorkNo + "'");
				}
				else if (type == 2)
				{
					objView = objDetailBll.GetEmployeeByWhereClause("a.Employee_ID != " + objEmployee.EmployeeID + " and GetStationOrgID(a.Org_ID)=" + hfOrg.Value + " and Work_No='" + objError.WorkNo + "'");
				}

				if (objView.Count > 0)
				{
					SessionSet.PageMessage = "工资编号已在系统中存在";
					return;
				}

				objEmployee.WorkNo = objError.WorkNo;
			}
			else
			{
				SessionSet.PageMessage = "工资编号不能为空！";
				return;
			}


			#endregion

			if (type == 1)
			{
				#region 新增
				Database db = DatabaseFactory.CreateDatabase();

				DbConnection connection = db.CreateConnection();
				connection.Open();
				DbTransaction transaction = connection.BeginTransaction();
				OrganizationBLL orgBll = new OrganizationBLL();
				try
				{
					Hashtable htWorkshop = GetWorkShop(db, transaction);
					foreach (System.Collections.DictionaryEntry obj in htShopNeedAdd)
					{
						int nWorkShopID;
						if (!htWorkshop.ContainsKey(obj.Key.ToString()))
						{
							RailExam.Model.Organization objshop = new RailExam.Model.Organization();
							objshop.FullName = obj.Key.ToString();
							objshop.ShortName = obj.Key.ToString();
							objshop.ParentId = Convert.ToInt32(hfOrg.Value);
							objshop.Memo = "";
							nWorkShopID = orgBll.AddOrganization(db, transaction, objshop);
						}
						else
						{
							nWorkShopID = Convert.ToInt32(htWorkshop[obj.Key.ToString()]);
						}

						Hashtable htGroup = (Hashtable)obj.Value;
						if (htGroup.Count != 0)
						{
							foreach (System.Collections.DictionaryEntry objGroupNeedAdd in htGroup)
							{
								RailExam.Model.Organization objGroup = new RailExam.Model.Organization();
								objGroup.FullName = objGroupNeedAdd.Key.ToString();
								objGroup.ShortName = objGroupNeedAdd.Key.ToString();
								objGroup.ParentId = nWorkShopID;
								objGroup.Memo = "";
								orgBll.AddOrganization(db, transaction, objGroup);
							}
						}
					}

					htWorkshop = GetWorkShop(db, transaction);
					Hashtable htNowOrg = GetOrgInfo(db, transaction);

					if (!string.IsNullOrEmpty(objEmployee.Memo))
					{
						if (objEmployee.Memo.Split('-').Length == 2)
						{
							objEmployee.OrgID = Convert.ToInt32(htWorkshop[objEmployee.Memo.Split('-')[1]]);
						}
						else
						{
							objEmployee.OrgID = Convert.ToInt32(htNowOrg[objEmployee.Memo.ToString()].ToString().Split('-')[0]);
						}
					}

					if (objEmployee.OrgID == 0)
					{
						throw new Exception("aaaa");
					}

					objEmployee.Memo = "";


					foreach (System.Collections.DictionaryEntry objPostNeed in htPostNeedAdd)
					{
						RailExam.Model.Post objPost = new RailExam.Model.Post();
						objPost.ParentId = 373;
						objPost.PostName = objPostNeed.Key.ToString();
						objPost.Technician = 0;
						objPost.Promotion = 0;
						objPost.Description = string.Empty;
						objPost.Memo = string.Empty;
						int postID = objPostBll.AddPost(db, transaction, objPost);

						objEmployee.PostID = postID;
					}

					objDetailBll.AddEmployee(db, transaction, objEmployee);


					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					SessionSet.PageMessage = "新增失败！";
					return;
				}
				finally
				{
					connection.Close();
				}

				#endregion
			}
			else
			{
				#region 修改
				Database db = DatabaseFactory.CreateDatabase();

				DbConnection connection = db.CreateConnection();
				connection.Open();
				DbTransaction transaction = connection.BeginTransaction();
				OrganizationBLL orgBll = new OrganizationBLL();
				try
				{
					Hashtable htWorkshop = GetWorkShop(db, transaction);
					foreach (System.Collections.DictionaryEntry obj in htShopNeedAdd)
					{
						int nWorkShopID;
						if (!htWorkshop.ContainsKey(obj.Key.ToString()))
						{
							RailExam.Model.Organization objshop = new RailExam.Model.Organization();
							objshop.FullName = obj.Key.ToString();
							objshop.ShortName = obj.Key.ToString();
							objshop.ParentId = Convert.ToInt32(hfOrg.Value);
							objshop.Memo = "";
							nWorkShopID = orgBll.AddOrganization(db, transaction, objshop);
						}
						else
						{
							nWorkShopID = Convert.ToInt32(htWorkshop[obj.Key.ToString()]);
						}

						Hashtable htGroup = (Hashtable)obj.Value;
						if (htGroup.Count != 0)
						{
							foreach (System.Collections.DictionaryEntry objGroupNeedAdd in htGroup)
							{
								RailExam.Model.Organization objGroup = new RailExam.Model.Organization();
								objGroup.FullName = objGroupNeedAdd.Key.ToString();
								objGroup.ShortName = objGroupNeedAdd.Key.ToString();
								objGroup.ParentId = nWorkShopID;
								objGroup.Memo = "";
								orgBll.AddOrganization(db, transaction, objGroup);
							}
						}
					}

					htWorkshop = GetWorkShop(db, transaction);
					Hashtable htNowOrg = GetOrgInfo(db, transaction);

					if (!string.IsNullOrEmpty(objEmployee.Memo))
					{
						if (objEmployee.Memo.Split('-').Length == 2)
						{
							objEmployee.OrgID = Convert.ToInt32(htWorkshop[objEmployee.Memo.Split('-')[1]]);
						}
						else
						{
							objEmployee.OrgID = Convert.ToInt32(htNowOrg[objEmployee.Memo.ToString()].ToString().Split('-')[0]);
						}
					}

					if (objEmployee.OrgID == 0)
					{
						throw new Exception("aaaa");
					}

					objEmployee.Memo = "";

					foreach (System.Collections.DictionaryEntry objPostNeed in htPostNeedAdd)
					{
						RailExam.Model.Post objPost = new RailExam.Model.Post();
						objPost.ParentId = 373;
						objPost.PostName = objPostNeed.Key.ToString();
						objPost.Technician = 0;
						objPost.Promotion = 0;
						objPost.Description = string.Empty;
						objPost.Memo = string.Empty;
						int postID = objPostBll.AddPost(db, transaction, objPost);

						objEmployee.PostID = postID;
					}


					objDetailBll.UpdateEmployee(db, transaction, objEmployee);


					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					SessionSet.PageMessage = "修改失败！";
					return;
				}
				finally
				{
					connection.Close();
				}

				#endregion

				SystemUserBLL objUserBll = new SystemUserBLL();
				SystemUser objUser = objUserBll.GetUserByEmployeeID(objEmployee.EmployeeID);
				objUser.UserID = objEmployee.WorkNo;
				objUserBll.UpdateUser(objUser);
			}

			objBll.DeleteEmployeeError(Convert.ToInt32(strID));
		}

		protected void btnInput_Click(object sender, EventArgs e)
		{
			if (File1.FileName == "")
			{
				SessionSet.PageMessage = "请浏览选择Excel文件！";
				return;
			}

			if (hfOrg.Value == "")
			{
				SessionSet.PageMessage = "请选择单位！";
				return;
			}
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			ClientScript.RegisterStartupScript(GetType(), "import",
							"showProgressBar('" + strFileName + "','" + hfOrg.Value + "')", true);
		}

		#region Common
		/// <summary>
		/// 获取当前的单位名称的组织机构信息
		/// </summary>
		/// <returns></returns>
		private Hashtable GetOrgInfo()
		{
			Hashtable htOrg = new Hashtable();

			OrgImportBLL objBll = new OrgImportBLL();
			IList<OrgImport> objList = objBll.GetOrgImport(Convert.ToInt32(hfOrg.Value));

			foreach (OrgImport obj in objList)
			{
				htOrg[obj.OrgNamePath] = obj.OrgID;
			}

			return htOrg;
		}

		/// <summary>
		/// 获取当前的单位名称的组织机构信息
		/// </summary>
		/// <returns></returns>
		private Hashtable GetOrgInfo(Database db, DbTransaction trans)
		{
			Hashtable htOrg = new Hashtable();

			OrgImportBLL objBll = new OrgImportBLL();
			IList<OrgImport> objList = objBll.GetOrgImport(db, trans, Convert.ToInt32(hfOrg.Value));

			foreach (OrgImport obj in objList)
			{
				htOrg[obj.OrgNamePath] = obj.OrgID;
			}

			return htOrg;
		}

		/// <summary>
		/// 获取当前的单位名称的组织机构信息
		/// </summary>
		/// <returns></returns>
		private Hashtable GetWorkShop(Database db, DbTransaction trans)
		{
			Hashtable htOrg = new Hashtable();

			OrganizationBLL objBll = new OrganizationBLL();
			IList<RailExam.Model.Organization> objList =
				objBll.GetOrganizationsByWhereClause(db, trans, "level_num=3 and GetStationOrgID(org_id)=" + hfOrg.Value);

			foreach (RailExam.Model.Organization obj in objList)
			{
				htOrg[obj.ShortName] = obj.OrganizationId;
			}

			return htOrg;
		}

		/// <summary>
		/// 职务名称
		/// </summary>
		/// <returns></returns>
		private Hashtable GetPostInfo()
		{
			Hashtable htPostInfo = new Hashtable();
			PostBLL postBLL = new PostBLL();
			IList<RailExam.Model.Post> objPostList = postBLL.GetPostsByLevel(3);

			foreach (RailExam.Model.Post post in objPostList)
			{
				if (!htPostInfo.ContainsKey(post.PostName))
				{
					htPostInfo.Add(post.PostName, post.PostId);
				}
			}

			return htPostInfo;
		}

		/// <summary>
		/// 现文化程度
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEducationLevel()
		{
			EducationLevelBLL objBll = new EducationLevelBLL();
			IList<EducationLevel> objList = objBll.GetAllEducationLevel();

			Hashtable htEducationLevel = new Hashtable();

			foreach (EducationLevel type in objList)
			{
				htEducationLevel.Add(type.EducationLevelName, type.EducationLevelID);
			}
			return htEducationLevel;
		}

		/// <summary>
		/// 政治面貌
		/// </summary>
		/// <returns></returns>
		private Hashtable GetPoliticalStatus()
		{
			PoliticalStatusBLL objBll = new PoliticalStatusBLL();
			IList<PoliticalStatus> objList = objBll.GetAllPoliticalStatus();

			Hashtable htPoliticalStatus = new Hashtable();

			foreach (PoliticalStatus type in objList)
			{
				htPoliticalStatus.Add(type.PoliticalStatusName, type.PoliticalStatusID);
			}
			return htPoliticalStatus;
		}

		/// <summary>
		/// 员工类型
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeType()
		{
			Hashtable htEmployeeType = new Hashtable();

			htEmployeeType.Add("工人", 1);
			htEmployeeType.Add("干部", 2);

			return htEmployeeType;
		}

		/// <summary>
		/// 班组长类型
		/// </summary>
		/// <returns></returns>
		private Hashtable GetWorkGroupLeaderType()
		{
			WorkGroupLeaderLevelBLL objBll = new WorkGroupLeaderLevelBLL();
			IList<WorkGroupLeaderLevel> objList = objBll.GetAllWorkGroupLeaderLevel();

			Hashtable htWorkGroupLeaderType = new Hashtable();

			foreach (WorkGroupLeaderLevel type in objList)
			{
				htWorkGroupLeaderType.Add(type.LevelName, type.WorkGroupLeaderLevelID);
			}
			return htWorkGroupLeaderType;
		}

		/// <summary>
		/// 职教干部类型
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEducationEmployeeType()
		{
			Hashtable htEducationEmployeeType = new Hashtable();
			htEducationEmployeeType.Add("管理干部", 1);
			htEducationEmployeeType.Add("专职教员", 2);
			htEducationEmployeeType.Add("其他", 3);
			return htEducationEmployeeType;
		}

		/// <summary>
		/// 职教委员会职务

		/// </summary>
		/// <returns></returns>
		private Hashtable GetCommitteeHeadship()
		{
			Hashtable htCommitteeHeadship = new Hashtable();
			htCommitteeHeadship.Add("主任", 1);
			htCommitteeHeadship.Add("副主任", 2);
			htCommitteeHeadship.Add("委员", 3);
			return htCommitteeHeadship;
		}

		/// <summary>
		/// 运输业的干部工人标识
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeTransportType()
		{
			Hashtable htEmployeeTransportType = new Hashtable();
			htEmployeeTransportType.Add("生产人员", 1);
			htEmployeeTransportType.Add("服务人员", 2);
			htEmployeeTransportType.Add("其他人员", 3);
			htEmployeeTransportType.Add("工程技术人员", 4);
			htEmployeeTransportType.Add("行政管理人员", 5);
			htEmployeeTransportType.Add("政工人员", 6);
			return htEmployeeTransportType;
		}

		/// <summary>
		///现技术职务名称

		/// </summary>
		/// <returns></returns>
		private Hashtable GetTechnicalTitle()
		{
			TechnicianTitleTypeBLL objBll = new TechnicianTitleTypeBLL();
			IList<TechnicianTitleType> objList = objBll.GetAllTechnicianTitleType();

			Hashtable htTechnicalTitle = new Hashtable();

			foreach (TechnicianTitleType type in objList)
			{
				htTechnicalTitle.Add(type.TypeName, type.TechnicianTitleTypeID);
			}
			return htTechnicalTitle;
		}

		/// <summary>
		/// 技术等级

		/// </summary>
		/// <returns></returns>
		private Hashtable GetSkillLevel()
		{
			TechnicianTypeBLL objBll = new TechnicianTypeBLL();
			IList<TechnicianType> objList = objBll.GetAllTechnicianType();

			Hashtable htSkillLevel = new Hashtable();

			foreach (TechnicianType type in objList)
			{
				htSkillLevel.Add(type.TypeName, type.TechnicianTypeID);
			}
			return htSkillLevel;
		}

		/// <summary>
		/// 职务级别
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeLevel()
		{
			Hashtable htEmployeeLevel = new Hashtable();

			htEmployeeLevel.Add("正局", 1);
			htEmployeeLevel.Add("副局", 2);
			htEmployeeLevel.Add("正部", 3);
			htEmployeeLevel.Add("副部", 4);
			htEmployeeLevel.Add("正处", 5);
			htEmployeeLevel.Add("副处", 6);
			htEmployeeLevel.Add("正科", 7);
			htEmployeeLevel.Add("副科", 8);
			htEmployeeLevel.Add("科员", 9);
			htEmployeeLevel.Add("股级", 10);
			htEmployeeLevel.Add("干事", 11);
			htEmployeeLevel.Add("办事员", 12);
			htEmployeeLevel.Add("其他", 13);

			return htEmployeeLevel;
		}

		/// <summary>
		/// 职务级别
		/// </summary>
		/// <returns></returns>
		private Hashtable GetTeacherType()
		{
			Hashtable hfTeacherType = new Hashtable();

			hfTeacherType.Add("兼职教师", 1);
			hfTeacherType.Add("专职教师", 2);
			hfTeacherType.Add("管理干部", 3);

			return hfTeacherType;
		}

		#endregion

		protected void btnBind_Click(object sender, EventArgs e)
		{
			BindGrid();
		}
	}
}
