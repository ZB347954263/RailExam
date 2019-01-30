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
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
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

			#region ���Ա����Ϣ
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
			Hashtable htPostNo = new Hashtable(); //Ϊ���Excel��Ա�������Ƿ��ظ�
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

			//��λ����
			if (objError.OrgName != txtOrg.Text)
			{
				SessionSet.PageMessage = "��λ������д����";
				return;
			}

			if (objError.OrgPath == "")
			{
				SessionSet.PageMessage = "�������Ʋ���Ϊ��";
				return;
			}

			//��֯����
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

					//�����֯������Ҫ����
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

					//�����֯������Ҫ����
					objEmployee.Memo = strOrg;
				}
			}
			else
			{
				objEmployee.OrgID = Convert.ToInt32(htOrg[strOrg]);
				objEmployee.Memo = string.Empty;
			}

			//��������Ϊ��
			if (string.IsNullOrEmpty(objError.EmployeeName))
			{
				SessionSet.PageMessage = "Ա����������Ϊ��";
				return;
			}
			else
			{
				if (objError.EmployeeName.Length > 20)
				{
					SessionSet.PageMessage = "Ա���������ܳ���20λ";
					return;
				}

				objEmployee.EmployeeName = objError.EmployeeName;
				objEmployee.PinYinCode = Pub.GetChineseSpell(objError.EmployeeName);
			}

			//���֤�Ų���Ϊ��
			if (string.IsNullOrEmpty(objError.IdentifyCode))
			{
				SessionSet.PageMessage = "���֤�Ų���Ϊ��";
				return;
			}
			else
			{
				if (objError.IdentifyCode.Length > 18)
				{
					SessionSet.PageMessage = "���֤�Ų��ܳ���18λ";
					return;
				}

				objEmployee.IdentifyCode = objError.IdentifyCode;
			}

			//����֤��
			if (!string.IsNullOrEmpty(objError.PostNo))
			{
				if (objError.PostNo.Length > 14)
				{
					SessionSet.PageMessage = "����֤�Ų��ܳ���14λ";
					return;
				}

				objEmployee.PostNo = objError.PostNo;
			}
			else
			{
				objEmployee.PostNo = "";
			}

			//�Ա�
			if (objError.Sex != "��" && objError.Sex != "Ů")
			{
				SessionSet.PageMessage = "�Ա����Ϊ�л�Ů";
				return;
			}
			else
			{
				objEmployee.Sex = objError.Sex;
			}

			//����
			if(!string.IsNullOrEmpty(objError.NativePlace))
			{
				if (objError.NativePlace.Length > 20)
				{
					SessionSet.PageMessage = "���᲻�ܳ���20λ";
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


			//����
			if (!string.IsNullOrEmpty(objError.Folk))
			{
				if (objError.Folk.Length > 10)
				{
					SessionSet.PageMessage = "���岻�ܳ���10λ";
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
			
			//����״��
			if (objError.Wedding == "δ��")
			{
				objEmployee.Wedding = 0;
			}
			else
			{
				objEmployee.Wedding = 1;
			}

			//���Ļ��̶�
			if (string.IsNullOrEmpty(objError.EducationLevel))
			{
				SessionSet.PageMessage = "���Ļ��̶Ȳ���Ϊ��";
				return;
			}
			else
			{
				if (!htEducationLevel.ContainsKey(objError.EducationLevel))
				{
					SessionSet.PageMessage = "���Ļ��̶���ϵͳ�в�����";
					return;
				}
				else
				{
					objEmployee.EducationLevelID = Convert.ToInt32(htEducationLevel[objError.EducationLevel]);
				}
			}

			//������ò
			if (!string.IsNullOrEmpty(objError.PoliticalStatus))
			{
				if (!htPoliticalStatus.ContainsKey(objError.PoliticalStatus))
				{
					SessionSet.PageMessage = "������ò��ϵͳ�в�����";
					return;
				}
				else
				{
					objEmployee.PoliticalStatusID = Convert.ToInt32(htPoliticalStatus[objError.PoliticalStatus]);
				}
			}

			//��(��)ҵѧУ(��λ)
			if(!string.IsNullOrEmpty(objError.GraduateUniversity))
			{
				if (objError.GraduateUniversity.Length > 50)
				{
					SessionSet.PageMessage = "��(��)ҵѧУ(��λ)���ܳ���50λ";
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

			//��ѧרҵ
			if(!string.IsNullOrEmpty(objEmployee.StudyMajor))
			{
				if (objEmployee.StudyMajor.Length > 50)
				{
					SessionSet.PageMessage = "��ѧרҵ���ܳ���50λ";
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

			//������ַ
			if(!string.IsNullOrEmpty(objError.Address))
			{
				if (objError.Address.Length > 100)
				{
					SessionSet.PageMessage = "������ַ���ܳ���100λ";
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

			//��������
			if(!string.IsNullOrEmpty(objEmployee.PostCode))
			{
				if (objError.PostCode.Length > 6)
				{
					SessionSet.PageMessage = "�������벻�ܳ���6λ";
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

			//ְ�񼶱�
			if (!string.IsNullOrEmpty(objError.EmployeeLevel))
			{
				if (!htEmployeeLevel.ContainsKey(objError.EmployeeLevel))
				{
					SessionSet.PageMessage = "ְ�񼶱���ϵͳ�в�����";
					return;
				}
				else
				{
					objEmployee.EmployeeLevelID = Convert.ToInt32(htEmployeeLevel[objError.EmployeeLevel]);
				}
			}

			//��������
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
						SessionSet.PageMessage = "����������д����";
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
					SessionSet.PageMessage = "����������д����";
					return;
				}
			}
			catch
			{
				SessionSet.PageMessage = "����������д����";
				return;
			}

			//��·��������
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
						SessionSet.PageMessage = "��·����������д����";
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
					SessionSet.PageMessage = "��·����������д����";
					return;
				}
			}
			catch
			{
				SessionSet.PageMessage = "��·����������д����";
				return;
			}


			//�μӹ�������
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
						SessionSet.PageMessage = "�μӹ���������д����";
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
					SessionSet.PageMessage = "�μӹ���������д����";
					return;
				}
			}
			catch
			{
				SessionSet.PageMessage = "�μӹ���������д����";
				return;
			}

			//�ɲ����˱�ʶ
			if (string.IsNullOrEmpty(objError.EmployeeType ))
			{
				SessionSet.PageMessage = "�ɲ����˱�ʶ����Ϊ�գ�";
				return;
			}
			else
			{
				if (!htEmployeeType.ContainsKey(objError.EmployeeType))
				{
					SessionSet.PageMessage = "�ɲ����˱�ʶ��ϵͳ�в����ڣ�";
					return;
				}
				else
				{
					objEmployee.EmployeeTypeID = Convert.ToInt32(htEmployeeType[objError.EmployeeType]);
				}
			}

			if(objEmployee.EmployeeTypeID == 1)
			{
				//��λ
				if (string.IsNullOrEmpty(objError.PostPath))
				{
					SessionSet.PageMessage = "��λ����Ϊ�գ�";
					return;
				}
				else
				{
					IList<RailExam.Model.Post> objPost =
						objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + objError.PostPath + "'");
					if (objPost.Count == 0)
					{
						SessionSet.PageMessage = "��λ��ϵͳ�в����ڣ�";
						return;
					}

					objEmployee.PostID = Convert.ToInt32(htPost[objError.PostPath]);
				}
			}
			else
			{
				//��λ
				if (string.IsNullOrEmpty(objError.PostPath))
				{
					SessionSet.PageMessage = "ְ����Ϊ�գ�";
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



			//���鳤����
			if (string.IsNullOrEmpty(objError.WorkGroupLeader))
			{
				objEmployee.IsGroupLeader = 0;
			}
			else
			{
				if (!htWorkGroupLeaderType.ContainsKey(objError.WorkGroupLeader))
				{
					SessionSet.PageMessage = "���鳤������ϵͳ�в����ڣ�";
					return;
				}

				objEmployee.WorkGroupLeaderTypeID = Convert.ToInt32(htWorkGroupLeaderType[objError.WorkGroupLeader]);
				objEmployee.IsGroupLeader = 1;
			}


			if (!string.IsNullOrEmpty(objError.EducationEmployee))
			{
				if (!htEducationEmployeeType.ContainsKey(objError.EducationEmployee))
				{
					SessionSet.PageMessage = "ְ�̸ɲ�������ϵͳ�в����ڣ�";
					return;
				}
				else
				{
					objEmployee.EducationEmployeeTypeID = Convert.ToInt32(htEducationEmployeeType[objError.EducationEmployee]);
				}
			}

			//ְ��ίԱ��ְ��
			if (!string.IsNullOrEmpty(objError.CommitteeHeadShip))
			{
				if (!htCommitteeHeadship.ContainsKey(objError.CommitteeHeadShip))
				{
					SessionSet.PageMessage = "ְ��ίԱ��ְ����ϵͳ�в����ڣ�";
					return;
				}
				else
				{
					objEmployee.CommitteeHeadShipID = Convert.ToInt32(htCommitteeHeadship[objError.CommitteeHeadShip]);
				}
			}

			//��ʦ���
			if (!string.IsNullOrEmpty(objError.TeacherType))
			{
				if (!htTeacherType.ContainsKey(objError.TeacherType))
				{
					SessionSet.PageMessage = "��ʦ�����ϵͳ�в�����";
					return;
				}
				else
				{
					objEmployee.TeacherTypeID = Convert.ToInt32(htTeacherType[objError.TeacherType]);
				}
			}

			//��Ա��λ״̬
			if (objError.OnPost != "�ڸڹ���")
			{
				objEmployee.Dimission = true;
			}
			else
			{
				objEmployee.Dimission = false;
			}

			//�ڸ�ְ������λ����
			if (string.IsNullOrEmpty(objError.EmployeeTransportType))
			{
				//SessionSet.PageMessage = "����λ����Ϊ������ҵ��ʱ���ڸ�ְ������λ���鲻��Ϊ�գ�";
				//return;
			}
			else
			{
				if (!htEmployeeTransportType.ContainsKey(objError.EmployeeTransportType))
				{
					SessionSet.PageMessage = "�ڸ�ְ������λ������ϵͳ�в����ڣ�";
					return;
				}
				else
				{
					objEmployee.EmployeeTransportTypeID = Convert.ToInt32(htEmployeeTransportType[objError.EmployeeTransportType]);
				}
			}

			//�ּ���ְ������
			if (objEmployee.EmployeeTypeID == 1)
			{

				if (!string.IsNullOrEmpty(objError.TechnicalTitle))
				{
					SessionSet.PageMessage = "���ɲ����˱�ʶΪ�����ˡ�ʱ���ּ���ְ�����Ʊ���Ϊ�գ�";
					return;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(objError.TechnicalTitle))
				{
					if (!htTechnicalTitle.ContainsKey(objError.TechnicalTitle))
					{
						SessionSet.PageMessage = "�ּ���ְ��������ϵͳ�в����ڣ�";
						return;
					}
					else
					{
						objEmployee.TechnicalTitleID = Convert.ToInt32(htTechnicalTitle[objError.TechnicalTitle]);
					}
				}
			}

			//�����ȼ�
			if (objEmployee.EmployeeTypeID == 1)
			{
				if (!string.IsNullOrEmpty(objError.TechnicalSkill))
				{
					if (!htSkillLevel.ContainsKey(objError.TechnicalSkill))
					{
						SessionSet.PageMessage = "�����ȼ���ϵͳ�в����ڣ�";
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

			//��λ��ѵ�ϸ�֤���

			if (!string.IsNullOrEmpty(objError.SalaryNo))
			{
				if (objError.SalaryNo.Length > 20)
				{
					SessionSet.PageMessage = "��λ��ѵ�ϸ�֤��Ų��ܳ���20λ";
					return;
				}

				//����֤����Excel�в����ظ�

				if (htPostNo.ContainsKey(objError.SalaryNo))
				{
					SessionSet.PageMessage = "��λ��ѵ�ϸ�֤�����Excel�������Ϊ" + htPostNo[objError.SalaryNo] + "�ĸ�λ��ѵ�ϸ�֤����ظ�";
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
					SessionSet.PageMessage = "��λ��ѵ�ϸ�֤�������ϵͳ�д���";
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
					SessionSet.PageMessage = "���ʱ�Ų��ܳ���20λ";
					return;
				}

				//����֤����Excel�в����ظ�

				if (htSalaryNo.ContainsKey(objError.WorkNo))
				{
					SessionSet.PageMessage = "���ʱ����Excel�������Ϊ" + htSalaryNo[objError.WorkNo] + "�Ĺ��ʱ���ظ�";
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
					SessionSet.PageMessage = "���ʱ������ϵͳ�д���";
					return;
				}

				objEmployee.WorkNo = objError.WorkNo;
			}
			else
			{
				SessionSet.PageMessage = "���ʱ�Ų���Ϊ�գ�";
				return;
			}


			#endregion

			if (type == 1)
			{
				#region ����
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
					SessionSet.PageMessage = "����ʧ�ܣ�";
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
				#region �޸�
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
					SessionSet.PageMessage = "�޸�ʧ�ܣ�";
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
				SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
				return;
			}

			if (hfOrg.Value == "")
			{
				SessionSet.PageMessage = "��ѡ��λ��";
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
		/// ��ȡ��ǰ�ĵ�λ���Ƶ���֯������Ϣ
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
		/// ��ȡ��ǰ�ĵ�λ���Ƶ���֯������Ϣ
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
		/// ��ȡ��ǰ�ĵ�λ���Ƶ���֯������Ϣ
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
		/// ְ������
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
		/// ���Ļ��̶�
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
		/// ������ò
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
		/// Ա������
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeType()
		{
			Hashtable htEmployeeType = new Hashtable();

			htEmployeeType.Add("����", 1);
			htEmployeeType.Add("�ɲ�", 2);

			return htEmployeeType;
		}

		/// <summary>
		/// ���鳤����
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
		/// ְ�̸ɲ�����
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEducationEmployeeType()
		{
			Hashtable htEducationEmployeeType = new Hashtable();
			htEducationEmployeeType.Add("����ɲ�", 1);
			htEducationEmployeeType.Add("רְ��Ա", 2);
			htEducationEmployeeType.Add("����", 3);
			return htEducationEmployeeType;
		}

		/// <summary>
		/// ְ��ίԱ��ְ��

		/// </summary>
		/// <returns></returns>
		private Hashtable GetCommitteeHeadship()
		{
			Hashtable htCommitteeHeadship = new Hashtable();
			htCommitteeHeadship.Add("����", 1);
			htCommitteeHeadship.Add("������", 2);
			htCommitteeHeadship.Add("ίԱ", 3);
			return htCommitteeHeadship;
		}

		/// <summary>
		/// ����ҵ�ĸɲ����˱�ʶ
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeTransportType()
		{
			Hashtable htEmployeeTransportType = new Hashtable();
			htEmployeeTransportType.Add("������Ա", 1);
			htEmployeeTransportType.Add("������Ա", 2);
			htEmployeeTransportType.Add("������Ա", 3);
			htEmployeeTransportType.Add("���̼�����Ա", 4);
			htEmployeeTransportType.Add("����������Ա", 5);
			htEmployeeTransportType.Add("������Ա", 6);
			return htEmployeeTransportType;
		}

		/// <summary>
		///�ּ���ְ������

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
		/// �����ȼ�

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
		/// ְ�񼶱�
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeLevel()
		{
			Hashtable htEmployeeLevel = new Hashtable();

			htEmployeeLevel.Add("����", 1);
			htEmployeeLevel.Add("����", 2);
			htEmployeeLevel.Add("����", 3);
			htEmployeeLevel.Add("����", 4);
			htEmployeeLevel.Add("����", 5);
			htEmployeeLevel.Add("����", 6);
			htEmployeeLevel.Add("����", 7);
			htEmployeeLevel.Add("����", 8);
			htEmployeeLevel.Add("��Ա", 9);
			htEmployeeLevel.Add("�ɼ�", 10);
			htEmployeeLevel.Add("����", 11);
			htEmployeeLevel.Add("����Ա", 12);
			htEmployeeLevel.Add("����", 13);

			return htEmployeeLevel;
		}

		/// <summary>
		/// ְ�񼶱�
		/// </summary>
		/// <returns></returns>
		private Hashtable GetTeacherType()
		{
			Hashtable hfTeacherType = new Hashtable();

			hfTeacherType.Add("��ְ��ʦ", 1);
			hfTeacherType.Add("רְ��ʦ", 2);
			hfTeacherType.Add("����ɲ�", 3);

			return hfTeacherType;
		}

		#endregion

		protected void btnBind_Click(object sender, EventArgs e)
		{
			BindGrid();
		}
	}
}
