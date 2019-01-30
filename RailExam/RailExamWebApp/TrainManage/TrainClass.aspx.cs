using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using RailExamWebApp.Common.Class;
using DSunSoft.Web.UI;
using DSunSoft.Web.Global;

namespace RailExamWebApp.TrainManage
{
    public partial class TrainClass : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OracleAccess oracleAccess;
                hfSelect.Value = GetSql();
                
                BindDropPlan();
                BindDropPlanClass(Convert.ToInt32(TRAIN_PLAN_ID.SelectedValue),"add");
              
                if (string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    hfID.Value = "";
                    lblPerson.Text = PrjPub.CurrentLoginUser.EmployeeName;
                    dateMake.DateValue = DateTime.Now.ToString("yyyy-MM-dd");
					//����labal
                	lblPlan.Visible = false;
                	lblPlanClass.Visible = false;
                }
                else
                {
                    hfID.Value = Request.QueryString["ID"];
                    oracleAccess = new OracleAccess();
                    DataSet ds = oracleAccess.RunSqlDataSet(string.Format("select * from ZJ_TRAIN_CLASS where TRAIN_CLASS_ID={0}", Request.QueryString["ID"]));
                    TRAIN_CLASS_CODE.Text = ds.Tables[0].Rows[0]["TRAIN_CLASS_CODE"].ToString();
                    TRAIN_CLASS_NAME.Text = ds.Tables[0].Rows[0]["TRAIN_CLASS_NAME"].ToString();
					//TRAIN_PLAN_ID.SelectedValue =
					//    GetPlanIDByPostClassID(
					//        Convert.ToInt32(ds.Tables[0].Rows[0]["train_plan_post_class_id"].ToString() == ""
					//                            ? 0
					//                            : ds.Tables[0].Rows[0]["train_plan_post_class_id"])).
					//        ToString();
					//dropPlanClass.Items.Clear();
					//BindDropPlanClass(Convert.ToInt32(TRAIN_PLAN_ID.SelectedValue),"");
					//dropPlanClass.Enabled = false;
					//dropPlanClass.SelectedValue = ds.Tables[0].Rows[0]["train_plan_post_class_id"].ToString();
                    dateBegin.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["BEGIN_DATE"]).ToString("yyyy-MM-dd");
                    dateEnd.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["END_DATE"]).ToString("yyyy-MM-dd");
                    dateMake.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["MAKEDATE"]).ToString("yyyy-MM-dd");
                    oracleAccess = new OracleAccess();
                    ds = oracleAccess.RunSqlDataSet(string.Format("select Employee_Name from Employee where Employee_ID={0}", ds.Tables[0].Rows[0]["MAKER_ID"]));
                    lblPerson.Text = ds.Tables[0].Rows[0][0].ToString();

					//����������
                	TRAIN_PLAN_ID.Visible = false;
                	dropPlanClass.Visible = false;
					ds = oracleAccess.RunSqlDataSet(@"select *  from zj_train_plan where train_plan_id in
                                 (select distinct train_plan_id from zj_train_class where train_class_id=" + Request.QueryString.Get("ID") + ")");
                    lblPlan.Text = ds.Tables[0].Rows[0]["train_plan_name"].ToString();

                    if (ds.Tables[0].Rows[0]["SPONSOR_UNIT_ID"].ToString() != PrjPub.CurrentLoginUser.StationOrgID.ToString())
                    {
                        btnSave.Enabled = false;
                        hfEdit.Value = "False";
                        GridSubject.Levels[0].Columns[8].Visible = false;
                    }

                	ds =
						oracleAccess.RunSqlDataSet(@"select  class_name from  zj_train_plan_post_class  where  train_plan_post_class_id=
									 (select distinct train_plan_post_class_id from zj_train_class where train_class_id=" +
                		                           Request.QueryString.Get("ID")+")");
					lblPlanClass.Text = ds.Tables[0].Rows[0][0].ToString();

                    if(Request.QueryString.Get("type")=="view")
                    {
                        btnSave.Enabled = false;
                        hfEdit.Value = "False";
                    }
                }
			 
					BindGridStudent();
					BindGridSubject();
					BindGridResult();
				 
            }
        }
        private string GetSql()
        {
            //StringBuilder sql = new StringBuilder("select * from zj_train_plan_view where 1=1");
            //if (ddlYear.SelectedValue != "--��ѡ��--")
            //{
            //    sql.AppendFormat(" and Year={0}", ddlYear.SelectedValue);
            //}
            //if (txtTrainPlanName.Text.Length != 0)
            //{
            //    sql.AppendFormat(" and TRAIN_PLAN_NAME like '%{0}%'", txtTrainPlanName.Text);
            //}
            //if (ddlTrainPlanType.SelectedValue != "--��ѡ��--")
            //{
            //    sql.AppendFormat(" and TRAIN_PLAN_TYPE_ID={0}", ddlTrainPlanType.SelectedValue);

            //}
            //if (ddlTrainPlanPhase.SelectedValue != "0")
            //{
            //    sql.AppendFormat(" and TRAIN_PLAN_PHASE_ID={0}", ddlTrainPlanPhase.SelectedValue);

            //}

            return "select * from ZJ_Train_Plan_Employee_View";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime beginDate = Convert.ToDateTime(dateBegin.DateValue);
            DateTime endDate = Convert.ToDateTime(dateEnd.DateValue);
            if (endDate < beginDate)
            {
                SessionSet.PageMessage = "�������ڲ��ܴ��ڽ������ڣ�";
                return;
            }
            if (hfID.Value == "")
                hfID.Value = InsertInfo();
            else
                UpdateInfo();

            if (hfID.Value == "")
            {
                return;
            }

            UpdateClassIDByPostClassID();   //Ϊ�°༶���ѧԱ
        	hidClassID.Value = hfID.Value;
            InsertResult(Convert.ToInt32(hfID.Value));  //������Ŀ���
            BindGridStudent();
			BindGridSubject();
			BindGridResult();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainClassList.aspx");
        }
        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
            //    {
            //        e.Row.Visible = false;
            //    }
            //    else
            //    {
            //        e.Row.Attributes.Add("onclick", "selectArow('" + e.Row.RowIndex + "');");
            //    }
            //}
        }

        protected void grdEntity_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            //DataTable db = e.ReturnValue as DataTable;
            //if (db.Rows.Count == 0)
            //{
            //    DataRow row = db.NewRow();
            //    row["TRAIN_PLAN_ID"] = -1;
            //    db.Rows.Add(row);
            //}
        }


		private void BindGridSubject()
		{
		    DataTable dt = GetSubjectOrWhere();
			dt.Columns.Add("isComputer", typeof (string));
			foreach (DataRow r in dt.Rows)
			{
				if (Convert.ToInt32(r["exam_on_computer"]) == 1)
					r["isComputer"] = "��";
				else
					r["isComputer"] = "��";
			}
			GridSubject.DataSource = dt;
			GridSubject.DataBind();

            if (Request.QueryString.Get("type") == "view")
            {
                GridSubject.Levels[0].Columns[8].Visible = false;
            }
		}

		private DataTable GetSubjectOrWhere()
		{
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
				hidClassID.Value = Request.QueryString["ID"];
			OracleAccess oracleAccess = new OracleAccess();
			System.Text.StringBuilder strSql=new StringBuilder();
			strSql.Append(" select ST.*,CS.TRAIN_CLASS_ID,CS.TRAIN_CLASS_NAME from zj_train_class_subject ST ");
			strSql.Append(" left join zj_train_class CS on CS.TRAIN_CLASS_ID=ST.TRAIN_CLASS_ID ");
			strSql.AppendFormat(" where ST.train_class_id={0}  order by ST.train_class_subject_id desc",
				hidClassID.Value == "" ? 0 : Convert.ToInt32(hidClassID.Value));

            DataTable dt=  oracleAccess.RunSqlDataSet(strSql.ToString()).Tables[0];
		    dt.Columns.Add("book_name", typeof (string));
            foreach (DataRow r in dt.Rows)
            {
                r["book_name"] = GetBookBySubjectID(Convert.ToInt32(r["train_class_subject_id"]));
            }
		    return dt;
		}

        private string GetBookBySubjectID(int subjectID)
        {
            string bookName = "";
            OracleAccess oracleAccess = new OracleAccess();
            DataTable dt =
               oracleAccess.RunSqlDataSet(
                   "select sb.*,b.book_name  from zj_train_class_subject_book sb left join  book b on b.book_id=sb.book_id  where sb.train_class_subject_id=" +
                   subjectID)
                   .Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                            bookName = dt.Rows[i]["book_name"].ToString();
                        else
                            bookName += "|" + dt.Rows[i]["book_name"];
                    }
                }
            }
            return bookName;
        }

        private  void DeleteSubject(int subjectID)
		{
			OracleAccess oracleAccess = new OracleAccess();
			oracleAccess.RunSqlDataSet(string.Format("delete from zj_train_class_subject where train_class_subject_id={0}",
			                                         subjectID));
		}

		private void BindGridStudent()
		{
			OracleAccess access = new OracleAccess();

            DataTable dtOrg = new DataTable();

            if(hfID.Value != "")
            {
                string str =
                    "select b.SPONSOR_UNIT_ID,b.UNDERTAKE_UNIT_ID from ZJ_Train_Class a inner join ZJ_Train_Plan b on a.Train_Plan_ID=b.Train_Plan_ID where Train_Class_ID=" +
                    hfID.Value;
                dtOrg = access.RunSqlDataSet(str).Tables[0];
            }

			StringBuilder strSql=new StringBuilder();
			strSql.Append(" select getorgname(E.Org_Id),V.*,E.work_no from ZJ_Train_Plan_Employee_View V ");
			strSql.Append("  left join Employee E on E.Employee_Id=V.employee_id ");
            strSql.AppendFormat(" where Train_Class_Id={0}", hfID.Value == "" ? 0 : Convert.ToInt32(hfID.Value));

            if (dtOrg.Rows.Count > 0)
            {
                //����վ�ε�λ��¼ʱ��ֻ�ܲ鿴����λ��ѧԱ����
                if (dtOrg.Rows[0]["SPONSOR_UNIT_ID"].ToString() != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                    && dtOrg.Rows[0]["UNDERTAKE_UNIT_ID"].ToString() != PrjPub.CurrentLoginUser.StationOrgID.ToString() 
                    && PrjPub.CurrentLoginUser.StationOrgID != 200)
                {
                    strSql.AppendFormat("  and GetStationOrgID(e.Org_ID)={0} ", PrjPub.CurrentLoginUser.StationOrgID);
                }
            }

		    strSql.Append("  order by train_plan_employee_id ");
			DataTable dt = access.RunSqlDataSet(strSql.ToString()).Tables[0];
			dt.Columns.Add("unit", typeof (string));
			dt.Columns.Add("workshop", typeof(string));
			dt.Columns.Add("group", typeof(string));

			foreach (DataRow r in dt.Rows)
			{
				string[] strArr = r["getorgname(E.Org_Id)"].ToString().Split('-');
				if (strArr.Length>0)
					r["unit"] = strArr[0];
				if (strArr.Length>1)
					r["workshop"] = strArr[1];
				if (strArr.Length > 2)
					r["group"] = strArr[2];
			}
			grdStudent.DataSource = dt;
			grdStudent.DataBind();

            if(Request.QueryString.Get("type")=="view")
            {
                grdStudent.Levels[0].Columns[9].Visible = false;
            }
			
		}

		protected void btnPostBack_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(hidSubject.Value))
			{
				try
				{
					DeleteSubject(Convert.ToInt32(hidSubject.Value));
				    DeleteSubjectResult(Convert.ToInt32(hfID.Value), Convert.ToInt32(hidSubject.Value));  //ɾ���ÿ�Ŀ�Ľ��
					//BindGridResult();
				}
				catch 
				{
					OxMessageBox.MsgBox3("����ɾ��ʧ�ܣ�");
				}
			}
			try
			{
				BindGridSubject();
				postStudent();
				BindGridResult();
			}
			catch (Exception)
			{

				OxMessageBox.MsgBox3("���ݼ���ʧ�ܣ������ԣ�");
			}
            if(hfSelectedIDs.Value!="")
            {
                OracleAccess access = new OracleAccess();
                string sql = string.Format("update zj_train_plan_employee set train_class_id=null  where train_plan_employee_id in ({0})",
                                           hfSelectedIDs.Value);
                access.ExecuteNonQuery(sql);
                BindGridStudent();
                DeleteResultByStuID(hfSelectedIDs.Value);
                BindGridResult();
                hfSelectedIDs.Value = "";
            }

		}

		private void postStudent()
		{
			string[] strArry = hidStudent.Value.Split('|');
            if (strArry[0] != null && strArry[0].Equals("del"))
            {
                deleteStudent();
                DeleteResultByStuID(strArry[1]);   //ɾ��ѧԱ�Ľ��
            }
		    BindGridStudent();
		}

		private void deleteStudent()
		{
			try
			{
				string[] strArry = hidStudent.Value.Split('|');
				OracleAccess access = new OracleAccess();
				string sql = string.Format("update zj_train_plan_employee set train_class_id=null  where train_plan_employee_id={0}",
										   Convert.ToInt32(strArry[1]));
				access.ExecuteNonQuery(sql);
			}
			catch  
			{
				OxMessageBox.MsgBox3("����ɾ��ʧ�ܣ�");
			}
			
		}

		private void BindGridResult()
		{
			#region ԭ����
			//OracleAccess access=new OracleAccess();
			//StringBuilder strEmp=new StringBuilder();
			//strEmp.Append("  select SR.train_class_subject_result_id, SR.train_class_subject_id,CS.PASS_RESULT,SR.result ,SR.ispass, ");
			//strEmp.Append("  EE.Employee_ID,EE.Employee_Name ,PT.Post_Name ,EE.WORK_NO ,getorgname(EE.ORG_ID) ");
			//strEmp.Append(" from zj_train_class_subject_result SR   left join zj_train_class_subject CS ");
			//strEmp.Append(" on CS.TRAIN_CLASS_SUBJECT_ID=SR.TRAIN_CLASS_SUBJECT_ID left join employee EE on EE.EMPLOYEE_ID=SR.Employee_Id");
			//strEmp.AppendFormat(" left join post PT on PT.POST_ID=EE.Post_Id where SR.train_class_id={0} order by EE.Employee_ID", hidClassID.Value == "" ? 0 : Convert.ToInt32(hidClassID.Value));
			//DataTable dtEmp = access.RunSqlDataSet(strEmp.ToString()).Tables[0];
			//dtEmp.Columns.Add("isMore", typeof (int));
			//dtEmp.Columns.Add("unit", typeof (string));
			//dtEmp.Columns.Add("workshop", typeof(string));
			//dtEmp.Columns.Add("group", typeof(string));

			//string sqlSubject =
			//    string.Format("select train_class_subject_id,subject_name from zj_train_class_subject where train_class_id={0}",
			//    hidClassID.Value == "" ? 0 : Convert.ToInt32(hidClassID.Value));

			//DataTable dtSub= access.RunSqlDataSet(sqlSubject.ToString()).Tables[0];

			//foreach (DataRow r in dtSub.Rows)
			//{
			//    dtEmp.Columns.Add(r["subject_name"].ToString(), typeof (string));
			//    foreach (DataRow empr in dtEmp.Rows)
			//    {

			//        if (empr["train_class_subject_id"].Equals(r["train_class_subject_id"]))
			//        {
			//            if(empr["result"].ToString()=="")
			//                empr[r["subject_name"].ToString()] = "";
			//            else
			//                empr[r["subject_name"].ToString()] = Convert.ToDouble(empr["result"]);

			//        }
			//        empr["isMore"] = 0;
			//    }
			//}

			// if(dtEmp.Columns.Count>14)
			// {
			//    string empID = string.Empty;
			//    foreach (DataRow r in dtEmp.Rows)
			//    {
			//        if (empID != r["Employee_ID"].ToString())
			//        {
			//            string sql = string.Format("Employee_ID={0}  ", Convert.ToInt32(r["Employee_ID"]));
			//            DataRow[] arr = dtEmp.Select(sql.ToString());
			//            if (arr.Length > 1)
			//            {
			//                for (int i = 14; i < dtEmp.Columns.Count; i++)
			//                {
			//                    List<string > lst = new List<string>();
			//                    for (int j = 0; j < arr.Length; j++)
			//                    {
			//                        if (!string.IsNullOrEmpty(arr[j][dtEmp.Columns[i].ColumnName].ToString()))
			//                        {
			//                            string strRes = Convert.ToDouble(arr[j][dtEmp.Columns[i].ColumnName]).ToString();
			//                            if (!string.IsNullOrEmpty(strRes))
			//                            {
			//                                if (Convert.ToDouble(strRes) < Convert.ToDouble(arr[j]["PASS_RESULT"]))
			//                                    strRes = "<font color='red'>" + strRes + "</font>";
			//                                lst.Add(strRes);
			//                            }
			//                        }
			//                    }
			//                    r[dtEmp.Columns[i].ColumnName] = string.Join("/", lst.ToArray());
			//                }
			//            }
			//            r["isMore"] = 1;
			//            empID = r["Employee_ID"].ToString();

			//             string[] strArr=r["getorgname(EE.ORG_ID)"].ToString().Split('-');
			//             if (strArr.Length>0)
			//                r["unit"] = strArr[0];
			//            if (strArr.Length > 1)
			//                r["workshop"] = strArr[1];
			//            if (strArr.Length > 2)
			//                r["group"] = strArr[2];
			//        }
			//    }
			// }

			// DataRow[] arrRow = dtEmp.Select("isMore=0");
			// for (int i = 0; i < arrRow.Length; i++)
			// {
			//     dtEmp.Rows.Remove(arrRow[i]);
			// }

			//DataTable newDT=new DataTable();
			//newDT.Columns.Add("Employee_Name", typeof (string));
			//newDT.Columns.Add("unit", typeof(string));
			//newDT.Columns.Add("workshop", typeof(string));
			//newDT.Columns.Add("group", typeof(string));
			//newDT.Columns.Add("Post_Name", typeof(string));
			//newDT.Columns.Add("WORK_NO", typeof(string));
			//if (dtEmp.Columns.Count > 14)
			//{
			//    for (int i = 14; i < dtEmp.Columns.Count; i++)
			//    {s
			//        newDT.Columns.Add(dtEmp.Columns[i].ColumnName, typeof(string));
			//    }
			//}
			//foreach (DataRow dr in dtEmp.Rows)
			//{
			//    DataRow r = newDT.NewRow();
			//    r["Employee_Name"] = dr["Employee_Name"];
			//    r["unit"] = dr["unit"];
			//    r["workshop"] = dr["workshop"];
			//    r["group"] = dr["group"];
			//    r["Post_Name"] = dr["Post_Name"];
			//    r["WORK_NO"] = dr["WORK_NO"];
			//    if (dtEmp.Columns.Count > 14)
			//    {
			//        for (int i = 14; i < dtEmp.Columns.Count; i++)
			//        {
			//            r[dtEmp.Columns[i].ToString()] = dr[dtEmp.Columns[i].ToString()];
			//        }
			//    }
			//    newDT.Rows.Add(r);
			//}
			//newDT.Columns["Employee_Name"].ColumnName = "����";
			//newDT.Columns["Post_Name"].ColumnName = "������λ";
			//newDT.Columns["WORK_NO"].ColumnName = "Ա������";
			//newDT.Columns["unit"].ColumnName = "��λ";
			//newDT.Columns["workshop"].ColumnName = "����";
			//newDT.Columns["group"].ColumnName = "����";
			//GridResult.EnableViewState = false;
			//GridResult.DataSource = newDT;
			//GridResult.DataBind();
			#endregion

			#region �·���

			OracleAccess access = new OracleAccess();

            DataTable dtOrg = new DataTable();
            if (hfID.Value != "")
            {
                string str1 =
                    "select b.SPONSOR_UNIT_ID,b.UNDERTAKE_UNIT_ID from ZJ_Train_Class a inner join ZJ_Train_Plan b on a.Train_Plan_ID=b.Train_Plan_ID where Train_Class_ID=" +hfID.Value;
                dtOrg = access.RunSqlDataSet(str1).Tables[0];
            }

			//�İ�Ŀ�Ŀ
			string sqlSubject =
				string.Format("select train_class_subject_id,subject_name from zj_train_class_subject where train_class_id={0} order by train_class_subject_id desc",
				hidClassID.Value == "" ? 0 : Convert.ToInt32(hidClassID.Value));
			DataTable dtSub = access.RunSqlDataSet(sqlSubject.ToString()).Tables[0];

			string sql = @" select r.employee_id,e.employee_name as ����,getorgname(e.org_id) as ��֯����,p.post_name as ������λ,e.work_no as Ա������";
		    int x = 0;
            Hashtable ht = new Hashtable();
			foreach (DataRow r in dtSub.Rows)
			{
				sql +=string.Format(@",
                        case when r.train_class_subject_id={0} then 
                            case when result<s.pass_result then '<font color=''red''>'||result||'</font>' else to_char(result) end   
                        end as  {1}
                       ", r["train_class_subject_id"], "subject"+x);

                ht.Add("SUBJECT" + x, r["subject_name"]);

			    x++;
			}

			sql += string.Format(@"  
                      from zj_train_class_subject_result r
					 left join employee e on r.employee_id=e.employee_id
					 left join post p on p.post_id=e.post_id
					 left join zj_train_class_subject s on s.train_class_subject_id=r.train_class_subject_id 
				     where r.train_class_id={0}    
                    ", hidClassID.Value == "" ? 0 : Convert.ToInt32(hidClassID.Value));

            if (dtOrg.Rows.Count > 0)
            {
                //����վ�ε�λ��¼ʱ��ֻ�ܲ鿴����λ��ѧԱ����
                if (dtOrg.Rows[0]["SPONSOR_UNIT_ID"].ToString() != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                    && dtOrg.Rows[0]["UNDERTAKE_UNIT_ID"].ToString() != PrjPub.CurrentLoginUser.StationOrgID.ToString()
                    && PrjPub.CurrentLoginUser.StationOrgID != 200)
                {
                    sql += string.Format("  and GetStationOrgID(e.Org_ID)={0} ", PrjPub.CurrentLoginUser.StationOrgID);
                }
            }

		    sql += string.Format("order by r.employee_id,train_class_subject_result_id desc");

			try
			{
				DataTable dt = access.RunSqlDataSet(sql).Tables[0];
				//��Table�����ݲ���
				DataTable dtNew = dt.Copy();
                dtNew.Rows.Clear();
				foreach (DataRow r in dt.Rows)
				{
					DataRow dr = dtNew.NewRow();
					foreach (DataColumn c in dt.Columns)
					{
						dr[c.ToString()] = r[c.ToString()];
					}
					string str = "employee_id='" + r["employee_id"] + "'";


					DataRow[] arr = dtNew.Select(str);
					if (arr.Length == 0)
					{
						//�±���û���ظ���ѧԱ��Ϣ
						DataRow[] arrMore = dt.Select(str); //��ѧԱ�Ķ����ɼ�
						if (arrMore.Length > 0)
						{
							Dictionary<string, string> dic = new Dictionary<string, string>();
							foreach (DataRow dataRow in arrMore)
							{
								foreach (DataRow row in dtSub.Rows)
								{
									int i = dtNew.Columns.IndexOf(row["subject_name"].ToString());
									if (i > -1)
									{
										if (dataRow[i].ToString().Trim() != "")
										{
											if (!dic.ContainsKey(row["subject_name"].ToString()))
												dic.Add(row["subject_name"].ToString(), dataRow[i].ToString());
											else
												dic[row["subject_name"].ToString()] += "/" + dataRow[i];
											dr[row["subject_name"].ToString()] = dic[row["subject_name"].ToString()];
										}
									}
								}
							}
						}
						dtNew.Rows.Add(dr);
					}

				}

                for (int i = 0; i < dtNew.Columns.Count; i++)
                {
                    if (ht.ContainsKey(dtNew.Columns[i].ColumnName))
                    {
                        dtNew.Columns[i].ColumnName = ht[dtNew.Columns[i].ColumnName].ToString();
                    }
                }


				//���ӵ�λ�����䣬����
				DataTable dtInfo = new DataTable();
				dtInfo.Columns.Add("����", typeof (string));
				dtInfo.Columns.Add("��λ", typeof (string));
				dtInfo.Columns.Add("����", typeof (string));
				dtInfo.Columns.Add("����", typeof (string));
				dtNew.Columns.Remove("employee_id");
				foreach (DataColumn cl in dtNew.Columns)
				{
					if (!cl.ColumnName.Equals("����"))
						dtInfo.Columns.Add(cl.ColumnName, typeof (string));
				}
				foreach (DataRow r in dtNew.Rows)
				{
					DataRow dr = dtInfo.NewRow();
					string[] strArr = r["��֯����"].ToString().Split('-');
					if (strArr.Length > 0)
						dr["��λ"] = strArr[0];
					if (strArr.Length > 1)
						dr["����"] = strArr[1];
					if (strArr.Length > 2)
						dr["����"] = strArr[2];

					foreach (DataColumn cl in dtNew.Columns)
					{
						dr[cl.ColumnName] = r[cl.ColumnName];
					}
					dtInfo.Rows.Add(dr);
				}
				dtInfo.Columns.Remove("��֯����");

				GridResult.EnableViewState = false;
				GridResult.DataSource = dtInfo;
				GridResult.DataBind();
			}
			catch 
			{ 
			}

			#endregion

		}

        protected  void TRAIN_PLAN_ID_OnSelectedIndexChanged(object sender,EventArgs e)
        {
            BindDropPlanClass(Convert.ToInt32(TRAIN_PLAN_ID.SelectedValue),"add");
            SetInfoPlanClass();
        }
        protected void dropPlanClass_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetInfoPlanClass();
        }


        /// <summary>
        /// ������ѵ�ƻ���ȡ��Ӧ����ѵ����Ϣ
        /// </summary>
        private DataTable  GetAllPostClassByPlanClassID(int placClassID)
        {
            OracleAccess access=new OracleAccess();
            string sql = "select *  from zj_train_plan_post_class where  train_plan_post_class_id=" + placClassID;
            return access.RunSqlDataSet(sql).Tables[0];
        }

        private void BindDropPlan()
        {
             OracleAccess oracleAccess = new OracleAccess();
            DataSet ds1 =
                oracleAccess.RunSqlDataSet("select * from ZJ_TRAIN_PLAN where (SPONSOR_UNIT_ID=" +
                                           PrjPub.CurrentLoginUser.StationOrgID + " or undertake_unit_id=" +
                                           PrjPub.CurrentLoginUser.StationOrgID + 
										   ")  and  train_plan_id in (select distinct train_plan_id from zj_train_plan_post_class where "+
										   "  train_plan_post_class_id not in (select train_plan_post_class_id from zj_train_class)) "
										   +"  order by TRAIN_PLAN_ID");
            TRAIN_PLAN_ID.DataSource = ds1.Tables[0].DefaultView;
            TRAIN_PLAN_ID.DataTextField = "TRAIN_PLAN_NAME";
            TRAIN_PLAN_ID.DataValueField = "TRAIN_PLAN_ID";
            TRAIN_PLAN_ID.DataBind();

            TRAIN_PLAN_ID.Items.Add(new ListItem("--��ѡ��--","0"));
            TRAIN_PLAN_ID.SelectedValue = "0";
        }

        private void BindDropPlanClass(int planID,string mode)
        {
            dropPlanClass.Items.Clear();
            OracleAccess access=new OracleAccess();
            StringBuilder str=new StringBuilder();
            str.Append("select c.train_plan_post_class_id,(case  when p.post_ids is not null then  getpostnamebypostid(p.post_ids)||'-'||c.class_name ");
            str.Append(" else  c.class_name  end ) class_name  from  zj_train_plan_post_class  c");
            str.Append(" left join zj_train_plan_post p on c.train_plan_post_id=p.train_plan_post_id ");
            str.AppendFormat( " where c.train_plan_id={0}  ", planID);
            if(mode=="add")
            str.Append(
                " and  c.train_plan_post_class_id not in (select train_plan_post_class_id from zj_train_class ) ");
            str.Append("  order by p.train_plan_post_id ,c.train_plan_post_class_id  ");
            DataTable dt =
                access.RunSqlDataSet(str.ToString()).Tables[0];
             if(dt.Rows.Count>0)
             {
                 dropPlanClass.DataSource = dt;
                dropPlanClass.DataTextField = "class_name";
                dropPlanClass.DataValueField = "train_plan_post_class_id";
                dropPlanClass.DataBind();
            } 
     
        }

        /// <summary>
        /// ��ȡ�ƻ���ѵ��Ļ�����Ϣ����������ѵ��
        /// </summary>
        private void SetInfoPlanClass()
        {
            if (dropPlanClass.SelectedValue != "")
            {
                if (Convert.ToInt32(dropPlanClass.SelectedValue) > 0)
                {
                    OracleAccess oracleAccess = new OracleAccess();
                    DataTable dt = GetAllPostClassByPlanClassID(Convert.ToInt32(dropPlanClass.SelectedValue));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            TRAIN_CLASS_NAME.Text = dt.Rows[0]["class_name"].ToString();
                            if (dt.Rows[0]["begin_date"].ToString() != "")
                                dateBegin.DateValue = Convert.ToDateTime(dt.Rows[0]["begin_date"]).ToString("yyy-MM-dd");
                            if (dt.Rows[0]["end_date"].ToString() != "")
                                dateEnd.DateValue = Convert.ToDateTime(dt.Rows[0]["end_date"]).ToString("yyy-MM-dd");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ����������Ϣ��������������ID
        /// </summary>
        /// <returns></returns>
        private string InsertInfo()
        {
            int count = 0;
            string sqlSelect = "select count(*) from ZJ_TRAIN_CLASS where TRAIN_CLASS_NAME='" + TRAIN_CLASS_NAME.Text.Trim() + "'";
            DataSet dsCount = new OracleAccess().RunSqlDataSet(sqlSelect);
            if (dsCount != null && dsCount.Tables[0] != null)
            {
                if (Convert.ToInt32(dsCount.Tables[0].Rows[0][0]) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('����ѵ��������ϵͳ���Ѵ��ڣ����������룡')", true);
                    count = 1;
                }
            }

            sqlSelect = "select count(*) from ZJ_TRAIN_CLASS where TRAIN_CLASS_CODE='" + TRAIN_CLASS_CODE.Text.Trim() + "'";
            dsCount = new OracleAccess().RunSqlDataSet(sqlSelect);
            if (dsCount != null && dsCount.Tables[0] != null)
            {
                if (Convert.ToInt32(dsCount.Tables[0].Rows[0][0]) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('����ѵ������ϵͳ���Ѵ��ڣ����������룡')", true);
                    count = 1;
                }
            }

            string ID = "";
            if (count == 0)
            {
                try
                {
                    OracleAccess oracleAccess = new OracleAccess();
                    string sql =
                        string.Format(
                            "insert into ZJ_TRAIN_CLASS(TRAIN_CLASS_ID,TRAIN_CLASS_CODE,TRAIN_CLASS_NAME,MAKEDATE,TRAIN_PLAN_ID,BEGIN_DATE,END_DATE,MAKER_ID,TRAIN_CLASS_STATUS_ID,train_plan_post_class_id) values({0},'{1}','{2}',TO_DATE ('{3}', 'YYYY-mm-dd'),{4},TO_DATE ('{5}', 'YYYY-mm-dd'),TO_DATE ('{6}', 'YYYY-mm-dd'),{7},1,{8})"
                            , "TRAIN_CLASS_SEQ.NextVal", TRAIN_CLASS_CODE.Text, TRAIN_CLASS_NAME.Text,
                            DateTime.Now.ToString("yyyy-MM-dd"), TRAIN_PLAN_ID.SelectedValue, dateBegin.DateValue,
                            dateEnd.DateValue, PrjPub.CurrentLoginUser.EmployeeID,
                            Convert.ToInt32(dropPlanClass.SelectedValue));

                    oracleAccess.ExecuteNonQuery(sql);

                    oracleAccess = new OracleAccess();
                    sql = "select MAx(TRAIN_CLASS_ID) from ZJ_TRAIN_CLASS";
                    ID = oracleAccess.RunSqlDataSet(sql).Tables[0].Rows[0][0].ToString();
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('����ɹ���')", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('���ݱ���ʧ�ܣ�')", true);
                }
            }
            return ID;
        }

        /// <summary>
        /// ���»�����Ϣ
        /// </summary>
        private void UpdateInfo()
        {
            int count = 0;
            string sqlSelect = "select count(*) from ZJ_TRAIN_CLASS where TRAIN_CLASS_NAME='" + TRAIN_CLASS_NAME.Text.Trim() + "'  and TRAIN_CLASS_ID!=" + hfID.Value;
            DataSet dsCount = new OracleAccess().RunSqlDataSet(sqlSelect);
            if (dsCount != null && dsCount.Tables[0] != null)
            {
                if (Convert.ToInt32(dsCount.Tables[0].Rows[0][0]) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('����ѵ��������ϵͳ���Ѵ��ڣ����������룡')", true);
                    count = 1;
                }
            }

            sqlSelect = "select count(*) from ZJ_TRAIN_CLASS where TRAIN_CLASS_CODE='" + TRAIN_CLASS_CODE.Text.Trim() + "'  and TRAIN_CLASS_ID!=" + hfID.Value;
            dsCount = new OracleAccess().RunSqlDataSet(sqlSelect);
            if (dsCount != null && dsCount.Tables[0] != null)
            {
                if (Convert.ToInt32(dsCount.Tables[0].Rows[0][0]) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('����ѵ������ϵͳ���Ѵ��ڣ����������룡')", true);
                    count = 1;
                }
            }


            if (count == 0)
            {
                try
                {
                    OracleAccess oracleAccess = new OracleAccess();
                    string sql =
                        string.Format(
                            "Update ZJ_TRAIN_CLASS set TRAIN_CLASS_CODE='{0}',TRAIN_CLASS_NAME='{1}',MAKER_ID={2}  where train_class_id={3}"
                            , TRAIN_CLASS_CODE.Text, TRAIN_CLASS_NAME.Text,
                            PrjPub.CurrentLoginUser.EmployeeID, hfID.Value);
                    oracleAccess.ExecuteNonQuery(sql);
                    //ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('�����޸ĳɹ���')", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('�����޸�ʧ�ܣ�')", true);
                }
            }
        }

        /// <summary>
        /// ����ѡ��ļƻ���ѵ���ѧԱ��ӵ������İ༶��
        /// </summary>
        private void UpdateClassIDByPostClassID()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                access.ExecuteNonQuery(
                    "update zj_train_plan_employee set train_class_id=null where train_class_id=" +
                    Convert.ToInt32(hfID.Value));
                string sql =
                    "update zj_train_plan_employee set train_class_id="+Convert.ToInt32(hfID.Value)+" where train_plan_post_class_id=" +
                    Convert.ToInt32(dropPlanClass.SelectedValue);
                access.ExecuteNonQuery(sql);
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// ����classID��ȡPlanID
        /// </summary>
        /// <param name="classID"></param>
        /// <returns></returns>
        private int GetPlanIDByPostClassID(int classID)
        {
            OracleAccess access=new OracleAccess();
            DataTable dt =
                access.RunSqlDataSet(
                    " select train_plan_id from zj_train_plan_post_class where train_plan_post_class_id=" + classID).
                    Tables[0];
            int planID = 0;
            if(dt!=null)
            {
                if(dt.Rows.Count>0 && dt.Rows[0][0].ToString().Length>0)
                {
                    planID = Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            return planID;
        }

        /// <summary>
        /// ɾ���ÿ�Ŀ�Ľ��
        /// </summary>
        /// <param name="classID"></param>
        /// <param name="resultID"></param>
        private void DeleteSubjectResult(int classID,int resultID)
        {
            OracleAccess access=new OracleAccess();
            access.ExecuteNonQuery("delete from zj_train_class_subject_result where train_class_id=" + classID +
                                   " and  train_class_subject_id=" + resultID);
  
        }

        /// <summary>
        /// ɾ��ѧԱ��Ӧ�Ŀ�Ŀ���
        /// </summary>
        /// <param name="stuID"></param>
        private void DeleteResultByStuID(string stuID)
        {
            OracleAccess access=new OracleAccess();
            access.ExecuteNonQuery("delete from zj_train_class_subject_result where train_class_id=" +
                                   Convert.ToInt32(hidClassID.Value) +
                                   " and  employee_id in (select employee_id from zj_train_plan_employee where train_plan_employee_id in (" +
                                   stuID + "))");

        }

        private void InsertResult(int classID)
        {
            OracleAccess access=new OracleAccess();

            try
            {
                access.ExecuteNonQuery("delete from zj_train_class_subject_result where train_class_id="+classID);
                StringBuilder sqlInsert = new StringBuilder();
                DataTable dtSub = GetSubjectByClassID(classID);
                foreach (DataRow sr in dtSub.Rows)
                {
                    DataTable dtEmp = GetEmpByClassID(classID);
                    if (dtEmp.Rows.Count > 0)
                    {
                        sqlInsert.Append("begin ");
                        foreach (DataRow r in dtEmp.Rows)
                        {
                            sqlInsert.Append(" insert into zj_train_class_subject_result(train_class_subject_result_id, ");
                            sqlInsert.Append(" train_class_id,train_class_subject_id,employee_id,exam_date,result,ispass)");
                            sqlInsert.AppendFormat(" values (TRAIN_CLASS_SUBJECT_RESULT_SEQ.nextval,{0},{1},{2},null,null,0);",
                                                   classID, sr["train_class_subject_id"],
                                                   Convert.ToInt32(r[0]));
                        }
                        sqlInsert.Append(" end; ");
                        access.ExecuteNonQuery(sqlInsert.ToString());
                        sqlInsert.Length = 0;
                    }
                }
            }
            catch (Exception)
            {
              
            }
          
           
        }

        private DataTable GetEmpByClassID(int classID)
        {
            OracleAccess access = new OracleAccess();
            return access.RunSqlDataSet(" select employee_id from zj_train_plan_employee where train_class_id=" + classID).Tables[0];
        }
        private DataTable GetSubjectByClassID(int classID)
        {
            OracleAccess access=new OracleAccess();
            return access.RunSqlDataSet(" select * from zj_train_class_subject where train_class_id="+classID).Tables[0];
        }
    }
}
