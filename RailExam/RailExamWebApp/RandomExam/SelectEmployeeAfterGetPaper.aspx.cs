using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using Microsoft.Office.Interop.Owc11;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class SelectEmployeeAfterGetPaper : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
                try
                {
                    OracleAccess db1 = new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
                    db1.RunSqlDataSet("select * from Org where level_num=2");
                }
                catch
                {
                    Response.Write("<script>alert('��ǰվ�η������޷�����·�ַ�������������ʱ��ӿ�����');window.close();</script>");
                    return;
                }

				ViewState["ExamID"] = Request.QueryString.Get("RandomExamID");
				string strId = Request.QueryString.Get("RandomExamID");
				if (strId != null && strId != "")
				{
					RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
					IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));

					if (ExamArranges.Count > 0)
					{
                        string strSql = "select a.* from Random_Exam_Arrange_Detail a "
                               + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                               + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                               + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                               + " and Random_Exam_ID=" + strId;

                        OracleAccess db = new OracleAccess();
                        DataSet ds = db.RunSqlDataSet(strSql);

					    string strChooseID=string.Empty;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (string.IsNullOrEmpty(strChooseID))
                            {
                                strChooseID += dr["User_Ids"].ToString();
                            }
                            else
                            {
                                strChooseID += "," + dr["User_Ids"];
                            }
                        }

                        GetAddIs(strId, ExamArranges[0].UserIds);

                        ViewState["ChooseId"] = strChooseID;
						ViewState["UpdateMode"] = 1;
					}
					else
					{
						ViewState["ChooseId"] = "";
						ViewState["UpdateMode"] = 0;
					}
				}
				BindChoosedGrid(ViewState["ChooseId"].ToString());
			}

			string strRefresh = Request.Form.Get("Refresh");
			if (strRefresh != "" && strRefresh != null)
			{
				string strId = ViewState["ExamID"].ToString();
				if (strId != null && strId != "")
				{
					RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
					IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));

					if (ExamArranges.Count > 0)
					{
                        //string strAddIds = string.Empty;

                        //��ѯ���������п���������ϸ
                        OracleAccess db = new OracleAccess();
                        /*
                        string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strId;
                        DataSet dsAll = db.RunSqlDataSet(strSql);

                        //������ӿ�����Ŀ���������ѯ���������Щ����
                        string[] str = ExamArranges[0].UserIds.Split(',');
                        for (int i = 0; i < str.Length; i++)
                        {
                            string strReplace = "," + str[i] + ",";
                            DataRow[] drs = dsAll.Tables[0].Select("','+User_Ids+',' like '%" + strReplace + "%'");

                            //��������п���������ϸ�У���ѯ������ǰ�༭�Ŀ���ID��˵���ÿ���Ϊ�����
                            if (drs.Length == 0)
                            {
                                if (strAddIds == string.Empty)
                                {
                                    strAddIds += str[i];
                                }
                                else
                                {
                                    strAddIds += "," + str[i];
                                }
                            }
                        }

                        if(hfAddIds.Value == "")
                        {
                            hfAddIds.Value = strAddIds.Replace(",", "|");
                        }
                        else
                        {
                            hfAddIds.Value += "|" + strAddIds.Replace(",", "|");
                        }

                        ViewState["AddIds"] = hfAddIds.Value;
                         * */

                        //��ѯ�����ڵ�ǰ�������µ�ǰ���ԵĿ�����Ϣ
                        string strSql = "select a.* from Random_Exam_Arrange_Detail a "
                               + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                               + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                               + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                               + " and Random_Exam_ID=" + strId;
                        DataSet ds = db.RunSqlDataSet(strSql);

                        string strChooseID = string.Empty;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (string.IsNullOrEmpty(strChooseID))
                            {
                                strChooseID += dr["User_Ids"].ToString();
                            }
                            else
                            {
                                strChooseID += "," + dr["User_Ids"];
                            }
                        }

                        GetAddIs(strId, ExamArranges[0].UserIds);

                        /*

                        //�������˿�������Ĭ�Ͻ��ÿ����Ŀ��������ڱ�վ�ε�ĳһ��΢������
                        if(ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(strAddIds))
                        {
                            if(PrjPub.IsServerCenter)
                            {
                                strSql = "update Random_Exam_Arrange_Detail "
                                     + "set User_ids = User_ids || '," + strAddIds + "' "
                                     + "where   Random_Exam_Arrange_Detail_ID =" + ds.Tables[0].Rows[0]["Random_Exam_Arrange_Detail_ID"];
                                db.ExecuteNonQuery(strSql);
                            }
                            else
                            {
                                eaBll.UpdateRandomExamArrangeDetailToServer(Convert.ToInt32(ds.Tables[0].Rows[0]["Random_Exam_Arrange_Detail_ID"]), strAddIds);
                            }
                                                    

                            strChooseID = strChooseID + "," + strAddIds;
                        }
                         * */

                        ViewState["ChooseId"] = strChooseID;
                        ViewState["UpdateMode"] = 1;
					}
					else
					{
						ViewState["ChooseId"] = "";
						ViewState["UpdateMode"] = 0;
					}

                    BindChoosedGrid(ViewState["ChooseId"].ToString());
				}
			}
		}

        private string GetAddIs(string strId,string userIds)
        {
            HasExamId();

            string strHasExamId = ViewState["HasExamId"].ToString();

            string strAddIds = string.Empty;

            //��ѯ���������п���������ϸ
            OracleAccess db = new OracleAccess();
            string strSql = "select a.* from Random_Exam_Arrange_Detail a "
                               + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                               + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                               + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                               + " and Random_Exam_ID=" + strId;
            DataSet dsAll = db.RunSqlDataSet(strSql);

            string strNow = "";
            foreach (DataRow dr in dsAll.Tables[0].Rows)
            {
                if(strNow == "")
                {
                    strNow += dr["User_Ids"].ToString();
                }
                else
                {
                    strNow += "," + dr["User_Ids"];
                }
            }

            //������ǰ��λ����������ϸ����ѯ���������Щ����
            string[] str = strNow.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                string strReplace = "," + str[i] + ",";

                //���ĳ���������������Ծ�Ŀ����У���ÿ���Ϊ����
                if ((","+strHasExamId+",").IndexOf(strReplace) == -1)
                {
                    if (strAddIds == string.Empty)
                    {
                        strAddIds += str[i];
                    }
                    else
                    {
                        strAddIds += "," + str[i];
                    }
                }
            }
            hfAddIds.Value = strAddIds.Replace(",", "|");
            ViewState["AddIds"] = hfAddIds.Value;
            return strAddIds;
        }

		protected void SaveChoose()
		{
			string strId = ViewState["ExamID"].ToString();

            string strEndId = "";

            //for (int i = 0; i < this.gvChoose.Rows.Count; i++)
            //{
            //    string strEmId = ((Label)this.gvChoose.Rows[i].FindControl("LabelEmployeeID")).Text;

            //    if (strEndId.Length == 0)
            //    {
            //        strEndId += strEmId;
            //    }
            //    else
            //    {
            //        if (strEndId == "0")
            //        {
            //            strEndId = strEmId;
            //        }
            //        else
            //        {
            //            strEndId += "," + strEmId;
            //        }
            //    }
            //}
            //if (ViewState["HasExamId"].ToString() != "" && strEndId != "")
            //{
            //    strEndId = ViewState["HasExamId"].ToString() + "," + strEndId;
            //}
            //else
            //{
            //    strEndId = "";
            //}

            //if (strEndId == "")
            //{
            //    strEndId = "0";
            //}

            OracleAccess db = new OracleAccess();
            //��ѯ�����ڵ�ǰվ�ο��ԵĿ�����Ϣ
		    string strSql = "select a.* from Random_Exam_Arrange_Detail a "
                            + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
		                    + " where Random_Exam_ID=" + strId;
            DataSet ds = db.RunSqlDataSet(strSql);

		    foreach (DataRow dr in ds.Tables[0].Rows)
		    {
                if (strEndId.Length == 0)
                {
                    strEndId += dr["User_Ids"].ToString();
                }
                else
                {
                    strEndId += "," + dr["User_Ids"];
                }
		    }


			//����
			if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "0")
			{
				RandomExamArrange examArrange = new RandomExamArrange();
				examArrange.RandomExamId = int.Parse(strId);
				examArrange.UserIds = strEndId;
				examArrange.Memo = "";
				RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
				if(!PrjPub.IsServerCenter)
				{
					examArrangeBLL.AddRandomExamArrangeToServer(examArrange);
				}
				else
				{
					examArrangeBLL.AddRandomExamArrange(examArrange);
				}
				ViewState["UpdateMode"] = 1;
				SessionSet.PageMessage = "����ɹ���";
				return;
			}

			//�޸�
			if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "1")
			{
				RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
				if (!PrjPub.IsServerCenter)
				{
					examArrangeBLL.UpdateRandomExamArrangeToServer(int.Parse(strId), strEndId);
				}
				else
				{
					examArrangeBLL.UpdateRandomExamArrange(int.Parse(strId), strEndId);
				}
				SessionSet.PageMessage = "����ɹ���";
				return;
			}
		}

		//�����������Ծ����Ա��Ϣ
		private void HasExamId()
		{
			string strExamId = ViewState["ExamID"].ToString();

			RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
			IList<RandomExamResultCurrent> examResults = objResultCurrentBll.GetRandomExamResultInfo(Convert.ToInt32(strExamId)); 
			string strId = "";
			for (int i = 0; i < examResults.Count; i++)
			{
				string strEmId = examResults[i].ExamineeId.ToString();

				if((","+strId+",").IndexOf(","+strEmId+",") < 0 )
				{
					if (strId.Length == 0)
					{
						strId += strEmId;
					}
					else
					{
						strId += "," + strEmId;
					}
				}
			}
			ViewState["HasExamId"] = strId;
		}

		private void BindChoosedGrid(string strId)
		{
			HasExamId();

            //string[] str = ViewState["HasExamId"].ToString().Split(',');
            //for (int i = 0; i < str.Length; i++)
            //{
            //    string strEmId = str[i];
            //    string strOldAllId = "," + strId + ",";
            //    if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
            //    {
            //        if (strId.Length == 0)
            //        {
            //            strId += strEmId;
            //        }
            //        else
            //        {
            //            strId = strEmId + "," + strId;
            //        }
            //    }
            //}

			EmployeeBLL psBLL = new EmployeeBLL();
			DataSet ds = new DataSet();

			IList<Employee> objList = new List<Employee>();

            //str = strId.Split(',');
            //if (str[0] != "")
            //{
            //    int n = 0;
            //    for (int i = str.Length-1; i >=0 ; i--)
            //    {
            //        Employee obj = psBLL.GetChooseEmployeeInfo(str[i]);
            //        obj.RowNum = n+1;
            //        objList.Add(obj);
            //        n++;
            //    }
            //    ds.Tables.Add(ConvertToDataTable((IList)objList));
            //    gvChoose.DataSource = objList;
            //    gvChoose.DataBind();
            //}
            //else
            //{
            //    BindEmptyGrid2();
            //}

            string[] str1 = strId.Split(',');

            if (str1[0] != "")
            {
                OracleAccess db = new OracleAccess();
                string strSql;
                for (int i = 0; i < str1.Length; i++)
                {
                    if(string.IsNullOrEmpty(str1[i]))
                    {
                        continue;
                    }

                    Employee obj = psBLL.GetChooseEmployeeInfo(str1[i]);
                    obj.RowNum = i + 1;
                    //if (string.IsNullOrEmpty(obj.WorkNo))
                    //{
                    //    strSql = "select identity_cardno from Employee where Employee_ID=" + str1[i];
                    //    obj.WorkNo = db.RunSqlDataSet(strSql).Tables[0].Rows[0][0].ToString();
                    //}
                    objList.Add(obj);
                }

                ds.Tables.Add(ConvertToDataTable((IList)objList));

                if (ViewState["Sort"] != null)
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["Sort"].ToString();
                }

                DataColumn dc = ds.Tables[0].Columns.Add("ComputeRoom");

                strSql = "select a.*,c.Short_Name||'-'||b.Computer_Room_Name as ComputeRoom "
                    + " from Random_Exam_Arrange_Detail a "
                    + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                    + " inner join Org c on b.Org_ID=c.Org_ID "
                    + " where Random_Exam_ID='" + ViewState["ExamID"] + "'";
                DataSet dsDetail = db.RunSqlDataSet(strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string strUser = "," + dr["EmployeeID"] + ",";
                    DataRow[] drs = dsDetail.Tables[0].Select("','+User_Ids+',' like '%" + strUser + "%'");

                    if (drs.Length > 0)
                    {
                        dr["ComputeRoom"] = drs[0]["ComputeRoom"].ToString();
                    }
                    else
                    {
                        dr["ComputeRoom"] = string.Empty;
                    }
                }

                gvChoose.DataSource = ds;
                gvChoose.DataBind();
            }
            else
            {
                BindEmptyGrid2();
            }
		}

		private void BindEmptyGrid2()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("RowNum", typeof(string)));
			dt.Columns.Add(new DataColumn("EmployeeID", typeof(int)));
			dt.Columns.Add(new DataColumn("OrgName", typeof(string)));
			dt.Columns.Add(new DataColumn("StrWorkNo", typeof(string)));
			dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
			dt.Columns.Add(new DataColumn("PostName", typeof(string)));
            dt.Columns.Add(new DataColumn("ComputeRoom", typeof(string)));

			DataRow dr = dt.NewRow();

			dr["EmployeeID"] = 0;
			dr["OrgName"] = "";
			dr["StrWorkNo"] = "";
			dr["EmployeeName"] = "";
			dr["PostName"] = "";
			dr["RowNum"] = "";
            dr["ComputeRoom"] = "";
			dt.Rows.Add(dr);

			gvChoose.DataSource = dt;
			gvChoose.DataBind();

			CheckBox CheckBox1 = (CheckBox)this.gvChoose.Rows[0].FindControl("chkSelect2");
			CheckBox1.Visible = false;
		}

		public static DataTable ConvertToDataTable(IList list)
		{
			DataTable dt = null;

			if (list.Count > 0)
			{
				dt = new DataTable(list[0].GetType().Name);
				Type type = list[0].GetType();

				foreach (PropertyInfo pi in type.GetProperties())
				{
					dt.Columns.Add(pi.Name, pi.PropertyType);
				}

				DataRow dr = null;
				foreach (Object o in list)
				{
					dr = dt.NewRow();

					foreach (DataColumn dc in dt.Columns)
					{
						dr[dc] = type.GetProperty(dc.ColumnName).GetValue(o, null);
					}
					dt.Rows.Add(dr);
				}
				dt.AcceptChanges();
			}

			return dt;
		}

		protected void ButtonOutPut_Click(object sender, EventArgs e)
		{
			string strAllId = ViewState["ChooseId"].ToString();
			if (strAllId == "")
			{
				return;
			}

			string strOldAllId = "," + strAllId + ",";
		    string strRemove = string.Empty;
			for (int i = 0; i < this.gvChoose.Rows.Count; i++)
			{
				CheckBox CheckBox1 = (CheckBox)this.gvChoose.Rows[i].FindControl("chkSelect2");
				string strEmId = ((Label)this.gvChoose.Rows[i].FindControl("LabelEmployeeID")).Text;
				if (CheckBox1.Checked)
				{
					strOldAllId = strOldAllId.Replace(strEmId + ",", "");

                    if (strRemove == string.Empty)
                    {
                        strRemove = strEmId;
                    }
                    else
                    {
                        strRemove += "," + strEmId;
                    }
				}
			}

			int n = strOldAllId.Length;
			if (n == 1)
			{
				ViewState["ChooseId"] = "";
			}
			else
			{
				ViewState["ChooseId"] = strOldAllId.Substring(1, n - 2);
			}

            //�Ƴ�������Ҫ�������������ϸ
            string strID = ViewState["ExamID"].ToString();
            OracleAccess db = new OracleAccess();
            //��ѯ��ǰ�������п���������ϸ
            string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID;
            DataSet dsOther = db.RunSqlDataSet(strSql);

            //������ǰ��Ҫ�Ƴ��Ŀ�����Ϣ����ѯ����������ϸ�Ƿ���ڵ�ǰ��Ҫ�Ƴ��Ŀ�����������������޸�ȥ���ÿ���
            string[] str = strRemove.ToString().Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                string strReplace = "," + str[i] + ",";
                DataRow[] drs = dsOther.Tables[0].Select("','+User_Ids+',' like '%" + strReplace + "%'");

                if (drs.Length > 0)
                {
                    strSql = "update Random_Exam_Arrange_Detail "
                             + "set User_ids = substr(Replace(','||User_ids||',','" + strReplace +
                             "',','),2,length(Replace(','||User_ids||',','" + strReplace + "',','))-2) "
                             + "where  ','|| User_ids || ',' like '%" + strReplace + "%' and Random_Exam_ID=" +
                             strID;

                    if (PrjPub.IsServerCenter)
                    {
                        db.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        string strConn = ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString;
                        OracleAccess dbCenter = new OracleAccess(strConn);
                        dbCenter.ExecuteNonQuery(strSql);

                        RandomExamArrangeBLL objBll =new RandomExamArrangeBLL();
                        objBll.RefreshRandomExamArrange();
                    }
                }

                hfAddIds.Value =
                    ("," + (hfAddIds.Value.Replace("|", ",")) + ",").Replace(strReplace, ",").TrimStart(',').TrimEnd(',').Replace(",","|");
            }

			BindChoosedGrid(ViewState["ChooseId"].ToString());

			SaveChoose();
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(ViewState["ExamID"].ToString()));

			SpreadsheetClass xlsheet = new SpreadsheetClass();
			Worksheet ws = (Worksheet)xlsheet.Worksheets[1];
			ws.Cells.Font.set_Size(10);
			ws.Cells.Font.set_Name("����");

			ws.Cells[1, 1] = objRandomExam.ExamName + " �μӿ���ѧԱ����";
			Range rang1 = ws.get_Range(ws.Cells[1, 1], ws.Cells[1, 7]);
			rang1.set_MergeCells(true);
			rang1.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
			rang1.Font.set_Name("����");


			//write headertext
			ws.Cells[2, 1] = "���";
			((Range)ws.Cells[2, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


			ws.Cells[2, 2] = "����";
			ws.get_Range(ws.Cells[2, 2], ws.Cells[2, 2]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 2], ws.Cells[2, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[2, 3] = "Ա������";
			ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[2, 4] = "ְ��";
			ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[2, 5] = "��֯����";
			ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			for (int j = 0; j < gvChoose.Rows.Count; j++)
			{
				ws.Cells[3 + j, 1] = ((Label)gvChoose.Rows[j].FindControl("lblNo")).Text;

				ws.Cells[3 + j, 2] = ((Label)gvChoose.Rows[j].FindControl("LabelName")).Text;
				ws.get_Range(ws.Cells[3 + j, 2], ws.Cells[3 + j, 2]).set_MergeCells(true);
				ws.get_Range(ws.Cells[3 + j, 2], ws.Cells[3 + j, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

				ws.Cells[3 + j, 3] = ((Label)gvChoose.Rows[j].FindControl("LabelWorkNo")).Text;
				ws.get_Range(ws.Cells[3 + j, 3], ws.Cells[3 + j, 3]).set_MergeCells(true);
				ws.get_Range(ws.Cells[3 + j, 3], ws.Cells[3 + j, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);


				ws.Cells[3 + j, 4] = ((Label)gvChoose.Rows[j].FindControl("LabelPostName")).Text;
				ws.get_Range(ws.Cells[3 + j, 4], ws.Cells[3 + j, 4]).set_MergeCells(true);
				ws.get_Range(ws.Cells[3 + j, 4], ws.Cells[3 + j, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

				ws.Cells[3 + j, 5] = ((Label)gvChoose.Rows[j].FindControl("Labelorgid")).Text;
				ws.get_Range(ws.Cells[3 + j, 5], ws.Cells[3 + j, 7]).set_MergeCells(true);
				ws.get_Range(ws.Cells[3 + j, 5], ws.Cells[3 + j, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
			}

			ws.Name = "1-1";
			ws.Cells.Columns.AutoFit();

			try
			{
				((Worksheet)xlsheet.Worksheets[1]).Activate();

				string path = Server.MapPath("../Excel/Excel.xls");
				if (File.Exists(path))
					File.Delete(path);
				xlsheet.Export(path, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportAsAppropriate);

				FileInfo file = new FileInfo(path);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition",
										"attachment; filename=" + HttpUtility.UrlEncode(objRandomExam.ExamName + "�μӿ���ѧԱ����") + ".xls");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����s
				this.Response.ContentType = "application/ms-excel";

				// ���ļ������͵��ͻ���
				this.Response.WriteFile(file.FullName);
			}
			catch
			{
				SessionSet.PageMessage = "ϵͳ���󣬵���Excel�ļ�ʧ�ܣ�";
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
            //�Ƴ�������Ҫ�������������ϸ
            string strID = ViewState["ExamID"].ToString();
            OracleAccess db = new OracleAccess();
            //��ѯ��ǰ�������п���������ϸ
            string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID;
            DataSet dsOther = db.RunSqlDataSet(strSql);

            //������ǰ��Ҫ�Ƴ��Ŀ�����Ϣ����ѯ����������ϸ�Ƿ���ڵ�ǰ��Ҫ�Ƴ��Ŀ�����������������޸�ȥ���ÿ���
            string[] str = ViewState["AddIds"].ToString().Split('|');
            for (int i = 0; i < str.Length; i++)
            {
                string strReplace = "," + str[i] + ",";
                DataRow[] drs = dsOther.Tables[0].Select("','+User_Ids+',' like '%" + strReplace + "%'");

                if (drs.Length > 0)
                {
                    strSql = "update Random_Exam_Arrange_Detail "
                             + "set User_ids = substr(Replace(','||User_ids||',','" + strReplace +
                             "',','),2,length(Replace(','||User_ids||',','" + strReplace + "',','))-2) "
                             + "where  ','|| User_ids || ',' like '%" + strReplace + "%' and Random_Exam_ID=" + strID;

                    if (PrjPub.IsServerCenter)
                    {
                        db.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        string strConn = ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString;
                        OracleAccess dbCenter = new OracleAccess(strConn);
                        dbCenter.ExecuteNonQuery(strSql);

                        RandomExamArrangeBLL objBll = new RandomExamArrangeBLL();
                        objBll.RefreshRandomExamArrange();
                    }
                }
            }

            //��ѯ�����ڵ�ǰվ�ο��ԵĿ�����Ϣ
            strSql = "select a.* from Random_Exam_Arrange_Detail a "
                     + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
		             + " where Random_Exam_ID=" + ViewState["ExamID"];
            DataSet ds = db.RunSqlDataSet(strSql);

		    string strEndId = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (strEndId.Length == 0)
                {
                    strEndId += dr["User_Ids"].ToString();
                }
                else
                {
                    strEndId += "," + dr["User_Ids"];
                }
            }

			RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
			if (!PrjPub.IsServerCenter)
			{
                examArrangeBLL.UpdateRandomExamArrangeToServer(int.Parse(ViewState["ExamID"].ToString()), strEndId);
			}
			else
			{
                examArrangeBLL.UpdateRandomExamArrange(int.Parse(ViewState["ExamID"].ToString()), strEndId);
			}


			Response.Write("<script>top.returnValue ='true';top.close();</script>");
		}
	}
}
