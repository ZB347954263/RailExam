using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.BLL;
using RailExam.Model;
using System.Collections.Generic;
using RailExamWebApp.Common.Class;
using System.Data.Common;

namespace RailExamWebApp.Systems
{
	public partial class ImportExcel : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (string.IsNullOrEmpty(Request.QueryString.Get("ImportType")))
				{
                    ImportEmployeeExcel();
				}
			}
        }

        #region �򵥵���
        private  void ImportEmployeeOther()
        {
            EmployeeBLL objEmployeeBll = new EmployeeBLL();
            EmployeeErrorBLL objErrorBll = new EmployeeErrorBLL();
            IList<EmployeeError> objErrorList = new List<EmployeeError>();

            string orgID = Request.QueryString.Get("OrgID");
            OrganizationBLL orgBll = new OrganizationBLL();
            RailExam.Model.Organization org = orgBll.GetOrganization(Convert.ToInt32(orgID));
            string strUnitName = org.ShortName;
            string strFileName = Server.UrlDecode(Request.QueryString.Get("FileName"));
            string jsBlock;
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
            bool isClose = false;
            string strMessage;

            objErrorBll.DeleteEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(orgID));

            Excel.Application objApp = null;
            Excel._Workbook objBook = null;
            Excel.Workbooks objBooks = null;
            Excel.Sheets objSheets = null;
            Excel._Worksheet objSheet = null;
            Excel.Range range = null;
            DataSet ds = new DataSet();


            #region ��ȡExcel�ļ�

            try
            {
                //����ExcelApp   
                objApp = new Excel.Application();
                //Excel����ʾ   
                objApp.Visible = false;
                //����Books   
                objBooks = objApp.Workbooks;
                //��Excel�ļ�   
                objBooks.Open(strPath,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing);
                //ȡ��Book   
                objBook = objBooks.get_Item(1);
                //ȡ��Sheets   
                objSheets = objBook.Worksheets;
                //ȡ��Sheet   
                objSheet = (Excel._Worksheet)objSheets.get_Item(1);
                //ȡ��Range   

                int rowNum = objSheet.UsedRange.Rows.Count;
                int colNum = objSheet.UsedRange.Columns.Count;

                // ���� ProgressBar.htm ��ʾ����������
                string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
                StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
                string html = reader.ReadToEnd();
                reader.Close();
                Response.Write(html);
                Response.Flush();
                System.Threading.Thread.Sleep(200);

                DataTable dtItem = new DataTable();
                Hashtable htCol = new Hashtable();
                for (int i = 1; i <= colNum; i++)
                {
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[1, i], objSheet.Cells[1, i]));
                    DataColumn dc = dtItem.Columns.Add(range.Value2.ToString());
                    htCol[range.Value2.ToString()] = i;
                }

                DataRow newRow = null;
                for (int i = 2; i <= rowNum; i++)
                {
                    newRow = dtItem.NewRow();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���"]], objSheet.Cells[i, htCol["���"]]));
                    newRow["���"] = range.Value2;
                    dtItem.Rows.Add(newRow);

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["Ա��ID"]], objSheet.Cells[i, htCol["Ա��ID"]]));
                    newRow["Ա��ID"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["վ������"]], objSheet.Cells[i, htCol["վ������"]]));
                    newRow["վ������"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
                    newRow["����"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
                    string str;
                    try
                    {
                        str = range.Value2.ToString().Replace("\0", "");
                        newRow["����"] = str;
                    }
                    catch
                    {
                        newRow["����"] = range.Value2;
                    }

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
                    try
                    {
                        str = range.Value2.ToString().Replace("\0", "");
                        newRow["����"] = str;
                    }
                    catch
                    {
                        newRow["����"] = range.Value2;
                    }


                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����֤��"]], objSheet.Cells[i, htCol["����֤��"]]));
                    newRow["����֤��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�Ա�"]], objSheet.Cells[i, htCol["�Ա�"]]));
                    try
                    {
                        str = range.Value2.ToString().Replace("\0", "");
                        newRow["�Ա�"] = str;
                    }
                    catch
                    {
                        newRow["�Ա�"] = range.Value2;
                    }


                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ְ��"]], objSheet.Cells[i, htCol["ְ��"]]));
                    newRow["ְ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�Ƿ���鳤"]], objSheet.Cells[i, htCol["�Ƿ���鳤"]]));
                    newRow["�Ƿ���鳤"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���ܵȼ�"]], objSheet.Cells[i, htCol["���ܵȼ�"]]));
                    newRow["���ܵȼ�"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��Ա��λ״̬"]], objSheet.Cells[i, htCol["��Ա��λ״̬"]]));
                    newRow["��Ա��λ״̬"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��λ��ѵ�ϸ�֤���"]], objSheet.Cells[i, htCol["��λ��ѵ�ϸ�֤���"]]));
                    try
                    {
                        str = range.Value2.ToString().Replace("?", "");
                        newRow["��λ��ѵ�ϸ�֤���"] = str;
                    }
                    catch
                    {
                        newRow["��λ��ѵ�ϸ�֤���"] = range.Value2;
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڶ�ȡExcel�ļ�','" + ((double)((i - 1) * 100) / (double)rowNum).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                ds.Tables.Add(dtItem);

                // �������
                jsBlock = "<script>SetCompleted('Excel���ݶ�ȡ���'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            catch
            {
                isClose = true;
            }
            finally
            {
                objBook.Close(Type.Missing, strPath, Type.Missing);
                objBooks.Close();
                objApp.Application.Workbooks.Close();
                objApp.Application.Quit();
                objApp.Quit();
                GC.Collect();
            }

            if (isClose)
            {
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='����Excel�ļ���ʽ',window.close();</script>");
                return;
            }

            #endregion

            #region ��������

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('��׼�����Excel����','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>window.returnValue='Excel��û���κμ�¼����˶�',window.close();</script>");
                return;
            }


            Hashtable htOrg = GetOrgInfo();
            Hashtable htPost = GetPostInfo();

            Hashtable htSkillLevel = GetSkillLevel();

            Hashtable htShopNeedAdd = new Hashtable();
            Hashtable htEmployeeName = new Hashtable(); //Ϊ���Excel�������Ƿ��ظ�
            Hashtable htWorkNo = new Hashtable(); //Ϊ���Excel�й���֤�����Ƿ��ظ�
            Hashtable htPostNo = new Hashtable(); //Ϊ���Excel��Ա�������Ƿ��ظ�
            Hashtable htPostNeedAdd = new Hashtable();

            PostBLL objPostBll = new PostBLL();
            IList<RailExam.Model.Employee> objEmployeeInsert = new List<RailExam.Model.Employee>();
            IList<RailExam.Model.Employee> objEmployeeUpdate = new List<RailExam.Model.Employee>();
            try
            {
                int index = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڼ��Excel����','" +
                              ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    index = index + 1;

                    RailExam.Model.Employee objEmployee = new RailExam.Model.Employee();

                    bool isUpdate = false;
                    if (dr["Ա��ID"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Employee_ID=" + dr["Ա��ID"].ToString().Trim());
                        if (objOldList.Count > 0)
                        {
                            isUpdate = true;
                        }
                        else
                        {
                            AddError(objErrorList, dr, "��Ա��ID��ϵͳ�в�����");
                            continue;
                        }
                    }

                    //վ������
                    if (dr["վ������"].ToString().Trim() != strUnitName)
                    {
                        AddError(objErrorList, dr, "վ��������д����");
                        continue;
                    }

                    if (dr["����"].ToString().Trim() == "")
                    {
                        AddError(objErrorList, dr, "���䲻��Ϊ��");
                        continue;
                    }

                    //��֯����
                    string strOrg;
                    if (dr["����"].ToString().Trim() == string.Empty)
                    {
                        strOrg = dr["վ������"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
                    }
                    else
                    {
                        strOrg = dr["վ������"].ToString().Trim() + "-" + dr["����"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
                    }

                    if (!htOrg.ContainsKey(strOrg))
                    {
                        if (dr["����"].ToString().Trim() == string.Empty)
                        {
                            if (!htShopNeedAdd.ContainsKey(dr["����"].ToString().Trim()))
                            {
                                htShopNeedAdd.Add(dr["����"].ToString().Trim(), new Hashtable());
                            }

                            //�����֯������Ҫ����
                            objEmployee.Memo = strOrg;
                        }
                        else
                        {
                            if (!htShopNeedAdd.ContainsKey(dr["����"].ToString().Trim()))
                            {
                                htShopNeedAdd.Add(dr["����"].ToString().Trim(), new Hashtable());
                            }

                            Hashtable htGroupNeedAdd = (Hashtable)htShopNeedAdd[dr["����"].ToString().Trim()];
                            if (!htGroupNeedAdd.ContainsKey(dr["����"].ToString().Trim()))
                            {
                                htGroupNeedAdd.Add(dr["����"].ToString().Trim(), dr["����"].ToString().Trim());
                                htShopNeedAdd[dr["����"].ToString().Trim()] = htGroupNeedAdd;
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
                    if (dr["����"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "Ա����������Ϊ��");
                        continue;
                    }
                    else
                    {
                        if (dr["����"].ToString().Trim().Length > 20)
                        {
                            AddError(objErrorList, dr, "Ա���������ܳ���20λ");
                            continue;
                        }

                        //������Excel�в����ظ�

                        if (htEmployeeName.ContainsKey(dr["����"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "Ա��������Excel�������Ϊ" + htEmployeeName[dr["����"].ToString()] + "��Ա�������ظ�");
                            continue;
                        }
                        else
                        {
                            htEmployeeName.Add(dr["����"].ToString().Trim(), dr["���"].ToString().Trim());
                        }

                        objEmployee.EmployeeName = dr["����"].ToString().Trim();
                        objEmployee.PinYinCode = Pub.GetChineseSpell(dr["����"].ToString().Trim());
                    }

                    //����֤��
                    if (dr["����֤��"].ToString().Trim() != string.Empty)
                    {
                        if (dr["����֤��"].ToString().Trim().Length > 14)
                        {
                            AddError(objErrorList, dr, "����֤�Ų��ܳ���14λ");
                            continue;
                        }

                        //����֤����Excel�в����ظ�
                        if (htWorkNo.ContainsKey(dr["����֤��"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "����֤����Excel�������Ϊ" + htWorkNo[dr["����֤��"].ToString()] + "�Ĺ���֤���ظ�");
                            continue;
                        }
                        else
                        {
                            htWorkNo.Add(dr["����֤��"].ToString().Trim(), dr["���"].ToString().Trim());
                        }

                        objEmployee.PostNo = dr["����֤��"].ToString().Trim();
                    }
                    else
                    {
                        objEmployee.PostNo = "";
                    }

                    //�Ա�
                    if (dr["�Ա�"].ToString().Trim() != "��" && dr["�Ա�"].ToString().Trim() != "Ů")
                    {
                        AddError(objErrorList, dr, "�Ա����Ϊ�л�Ů");
                        continue;
                    }
                    else
                    {
                        objEmployee.Sex = dr["�Ա�"].ToString().Trim();
                    }

                
                    //ְ��
                    if (dr["ְ��"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "ְ������Ϊ�գ�");
                        continue;
                    }
                    else
                    {
                        IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["ְ��"].ToString().Trim() + "'");
                        if (objPost.Count == 0)
                        {
                            AddError(objErrorList, dr, "ְ����ϵͳ�в����ڣ�");
                            continue;
                        }

                        objEmployee.PostID = Convert.ToInt32(htPost[dr["ְ��"].ToString().Trim()]);
                    }



                    //���鳤����
                    if (dr["�Ƿ���鳤"].ToString().Trim() == string.Empty || dr["�Ƿ���鳤"].ToString().Trim()=="��")
                    {
                        objEmployee.IsGroupLeader = 0;
                    }
                    else
                    {
                        objEmployee.IsGroupLeader = 1;
                    }


                    //��Ա��λ״̬
                    if (dr["��Ա��λ״̬"].ToString().Trim().IndexOf("�ڸ�") < 0)
                    {
                        objEmployee.IsOnPost = false;
                    }
                    else
                    {
                        objEmployee.IsOnPost = true;
                    }

                    //��λ��ѵ�ϸ�֤���

                    if (dr["��λ��ѵ�ϸ�֤���"].ToString().Trim() != string.Empty)
                    {
                        if (dr["��λ��ѵ�ϸ�֤���"].ToString().Trim().Length > 20)
                        {
                            AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤��Ų��ܳ���20λ");
                            continue;
                        }

                        //����֤����Excel�в����ظ�

                        if (htPostNo.ContainsKey(dr["��λ��ѵ�ϸ�֤���"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤�����Excel�������Ϊ" + htPostNo[dr["��λ��ѵ�ϸ�֤���"].ToString().Trim()] + "�ĸ�λ��ѵ�ϸ�֤����ظ�");
                            continue;
                        }
                        else
                        {
                            htPostNo.Add(dr["��λ��ѵ�ϸ�֤���"].ToString().Trim(), dr["���"].ToString().Trim());
                        }

                        IList<RailExam.Model.Employee> objView;

                        if(!isUpdate)
                        {
                            objView =
                                objEmployeeBll.GetEmployeeByWhereClause("Work_No='" + dr["��λ��ѵ�ϸ�֤���"].ToString().Trim() + "'");
                        }
                        else
                        {
                            objView =
                                objEmployeeBll.GetEmployeeByWhereClause("Employee_ID<>" + dr["Ա��ID"] + " and Work_No='" + dr["��λ��ѵ�ϸ�֤���"].ToString().Trim() + "'");
                        }

                        if (objView.Count > 0)
                        {
                            AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤�������ϵͳ�д���");
                            continue;
                        }

                        objEmployee.WorkNo = dr["��λ��ѵ�ϸ�֤���"].ToString().Trim();
                    }
                    else
                    {
                        objEmployee.WorkNo = string.Empty;
                        AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤��Ų���Ϊ�գ����ȷʵû�У������빤��֤�ţ�");
                        continue;
                    }

                    //���ܵȼ�
					if (dr["���ܵȼ�"].ToString().Trim() != string.Empty)
					{
						if (!htSkillLevel.ContainsKey(dr["���ܵȼ�"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "���ܵȼ���ϵͳ�в����ڣ�");
							continue;
						}
						else
						{
							objEmployee.TechnicianTypeID = Convert.ToInt32(htSkillLevel[dr["���ܵȼ�"].ToString().Trim()]);
						}
					}
					else
					{
						objEmployee.TechnicianTypeID = 1;
					}


                    if (dr["Ա��ID"].ToString().Trim() == string.Empty)
                    {
                        objEmployeeInsert.Add(objEmployee);
                    }
                    else
                    {
                        try
                        {
                            int employeeID = Convert.ToInt32(dr["Ա��ID"].ToString().Trim());
                            objEmployee.EmployeeID = employeeID;
                            objEmployeeUpdate.Add(objEmployee);

                        }
                        catch
                        {
                            AddError(objErrorList, dr, "Ա��ID��д����");
                            continue;
                        }
                    }
                }

                // �������
                jsBlock = "<script>SetCompleted('Excel���ݼ�����'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            catch (Exception ex)
            {
                Response.Write(EnhancedStackTrace(ex));
            }

            #endregion

            if (objErrorList.Count > 0)
            {
                jsBlock = "<script>SetCompleted('����ͳ�Ʋ�����Ҫ������ݣ���ȴ�......'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                objErrorBll.AddEmployeeError(objErrorList);

                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='refresh|����Excel����',window.close();</script>");
                return;
            }


            if (!string.IsNullOrEmpty(Request.QueryString.Get("mode")))
            {
                Response.Write("<script>window.returnValue='refresh|���ݼ��ɹ�',window.close();</script>");
                return;
            }

            #region ��������

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('���ڵ��벿�����ơ�����','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                Hashtable htWorkshop = GetWorkShop(db, transaction);
                int count = 1;
                foreach (System.Collections.DictionaryEntry obj in htShopNeedAdd)
                {
                    int nWorkShopID;
                    if (!htWorkshop.ContainsKey(obj.Key.ToString()))
                    {
                        RailExam.Model.Organization objshop = new RailExam.Model.Organization();
                        objshop.FullName = obj.Key.ToString();
                        objshop.ShortName = obj.Key.ToString();
                        objshop.ParentId = Convert.ToInt32(orgID);
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

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ��벿�����ơ�����','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                htWorkshop = GetWorkShop(db, transaction);
                Hashtable htNowOrg = GetOrgInfo(db, transaction);

                foreach (RailExam.Model.Employee objEmployee in objEmployeeInsert)
                {
                    if (objEmployee.Memo.ToString() != string.Empty)
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

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ��벿�����ơ�����','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                foreach (RailExam.Model.Employee objEmployee in objEmployeeUpdate)
                {
                    if (objEmployee.Memo.ToString() != string.Empty)
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

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ��벿�����ơ�����','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڵ���ɲ�ְ��','0.00'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                count = 1;
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

                    ArrayList objPostList = (ArrayList)objPostNeed.Value;
                    for (int i = 0; i < objPostList.Count; i++)
                    {
                        string[] strPost = objPostList[i].ToString().Split('|');
                        if (strPost[1] == "false")
                        {
                            objEmployeeInsert[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
                        else
                        {
                            objEmployeeUpdate[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ���ɲ�ְ��','" + ((double)(count * 100) / (double)(htPostNeedAdd.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڵ���Ա����Ϣ','0.00'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                count = 1;
                for (int i = 0; i < objEmployeeInsert.Count; i++)
                {
                    int employeeid = objEmployeeBll.AddEmployee(db, transaction, objEmployeeInsert[i]);

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ���Ա����Ϣ','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }


                for (int i = 0; i < objEmployeeUpdate.Count; i++)
                {
                    objEmployeeBll.UpdateEmployee(db, transaction, objEmployeeUpdate[i]);
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ���Ա����Ϣ','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                strMessage = "����ɹ�!";
                transaction.Commit();
            }
            catch (Exception ex)
            {
                strMessage = "����ʧ��!";
                transaction.Rollback();
                //Response.Write(EnhancedStackTrace(ex));
            }
            finally
            {
                connection.Close();
            }

            if (File.Exists(strPath))
                File.Delete(strPath);
            Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
            #endregion
        }
        #endregion

        #region ��������
        private void ImportEmployeeExcel()
		{
			EmployeeBLL objEmployeeBll = new EmployeeBLL();
			EmployeeDetailBLL objEmployeeDetailBll = new EmployeeDetailBLL();
			EmployeeErrorBLL objErrorBll = new EmployeeErrorBLL();
			IList<EmployeeError> objErrorList = new List<EmployeeError>();


			string orgID = Request.QueryString.Get("OrgID");
			OrganizationBLL orgBll = new OrganizationBLL();
			RailExam.Model.Organization org = orgBll.GetOrganization(Convert.ToInt32(orgID));
			string strUnitName = org.ShortName;
			string strFileName = Server.UrlDecode(Request.QueryString.Get("FileName"));
			string jsBlock;
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
			bool isClose = false;
			string strMessage;

			objErrorBll.DeleteEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(orgID));

			Excel.Application objApp = null;
			Excel._Workbook objBook = null;
			Excel.Workbooks objBooks = null;
			Excel.Sheets objSheets = null;
			Excel._Worksheet objSheet = null;
			Excel.Range range = null;
			DataSet ds = new DataSet();

			#region ��ȡExcel�ļ�

			try
			{
				//����ExcelApp   
				objApp = new Excel.Application();
				//Excel����ʾ   
				objApp.Visible = false;
				//����Books   
				objBooks = objApp.Workbooks;
				//��Excel�ļ�   
				objBooks.Open(strPath,
							  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
							  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
							  Type.Missing, Type.Missing, Type.Missing,
							  Type.Missing, Type.Missing, Type.Missing);
				//ȡ��Book   
				objBook = objBooks.get_Item(1);
				//ȡ��Sheets   
				objSheets = objBook.Worksheets;
				//ȡ��Sheet   
				objSheet = (Excel._Worksheet)objSheets.get_Item(1);
				//ȡ��Range   

				int rowNum = objSheet.UsedRange.Rows.Count;
				int colNum = objSheet.UsedRange.Columns.Count;

				// ���� ProgressBar.htm ��ʾ����������
				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				DataTable dtItem = new DataTable();
				Hashtable htCol = new Hashtable();
				for (int i = 1; i <= colNum; i++)
				{
					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[1, i], objSheet.Cells[1, i]));
					DataColumn dc = dtItem.Columns.Add(range.Value2.ToString());
					htCol[range.Value2.ToString()] = i;
				}

				DataRow newRow = null;
				for (int i = 2; i <= rowNum; i++)
				{
					newRow = dtItem.NewRow();

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���"]], objSheet.Cells[i, htCol["���"]]));
					newRow["���"] = range.Value2;
					dtItem.Rows.Add(newRow);

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["Ա��ID"]], objSheet.Cells[i, htCol["Ա��ID"]]));
                    newRow["Ա��ID"] = range.Value2;
					
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��λ"]], objSheet.Cells[i, htCol["��λ"]]));
					newRow["��λ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
					newRow["����"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
					string str;
					try
					{
						str = range.Value2.ToString().Replace("\0", "");
						newRow["����"] = str;
					}
					catch
					{
						newRow["����"] = range.Value2;
					}

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
					try
					{
						str = range.Value2.ToString().Replace("\0", "");
						newRow["����"] = str;
					}
					catch
					{
						newRow["����"] = range.Value2;
					}

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���֤��"]], objSheet.Cells[i, htCol["���֤��"]]));
					try
					{
						str = range.Value2.ToString().Replace("?", "");
						newRow["���֤��"] = str;
					}
					catch
					{
						newRow["���֤��"] = range.Value2;
					}

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����֤��"]], objSheet.Cells[i, htCol["����֤��"]]));
                    //newRow["����֤��"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�Ա�"]], objSheet.Cells[i, htCol["�Ա�"]]));
					try
					{
						str = range.Value2.ToString().Replace("\0", "");
						newRow["�Ա�"] = str;
					}
					catch
					{
						newRow["�Ա�"] = range.Value2;
					}

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
                    //newRow["����"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
                    //newRow["����"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����״��"]], objSheet.Cells[i, htCol["����״��"]]));
                    //newRow["����״��"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�Ļ��̶�"]], objSheet.Cells[i, htCol["�Ļ��̶�"]]));
					newRow["�Ļ��̶�"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["������ò"]], objSheet.Cells[i, htCol["������ò"]]));
					newRow["������ò"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��ҵѧУ"]], objSheet.Cells[i, htCol["��ҵѧУ"]]));
                    newRow["��ҵѧУ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��ѧרҵ"]], objSheet.Cells[i, htCol["��ѧרҵ"]]));
                    newRow["��ѧרҵ"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["������ַ"]], objSheet.Cells[i, htCol["������ַ"]]));
                    //newRow["������ַ"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��������"]], objSheet.Cells[i, htCol["��������"]]));
                    //newRow["��������"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ְ�񼶱�"]], objSheet.Cells[i, htCol["ְ�񼶱�"]]));
                    //newRow["ְ�񼶱�"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��������"]], objSheet.Cells[i, htCol["��������"]]));
					newRow["��������"] = range.Text.ToString();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��·ʱ��"]], objSheet.Cells[i, htCol["��·ʱ��"]]));
					newRow["��·ʱ��"] = range.Text.ToString();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�μӹ���ʱ��"]], objSheet.Cells[i, htCol["�μӹ���ʱ��"]]));
					newRow["�μӹ���ʱ��"] = range.Text.ToString();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ְ������"]], objSheet.Cells[i, htCol["ְ������"]]));
                    newRow["ְ������"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ְ������"]], objSheet.Cells[i, htCol["ְ������"]]));
                    //newRow["ְ������"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ְ��"]], objSheet.Cells[i, htCol["ְ��"]]));
					newRow["ְ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�ִ��¸�λ"]], objSheet.Cells[i, htCol["�ִ��¸�λ"]]));
                    newRow["�ִ��¸�λ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�ڶ�ְ��"]], objSheet.Cells[i, htCol["�ڶ�ְ��"]]));
                    newRow["�ڶ�ְ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����ְ��"]], objSheet.Cells[i, htCol["����ְ��"]]));
                    newRow["����ְ��"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���鳤����"]], objSheet.Cells[i, htCol["���鳤����"]]));
					newRow["���鳤����"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ְ�̸ɲ�����"]], objSheet.Cells[i, htCol["ְ�̸ɲ�����"]]));
                    //newRow["ְ�̸ɲ�����"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���鳤��������"]], objSheet.Cells[i, htCol["���鳤��������"]]));
                    newRow["���鳤��������"] = range.Text.ToString();

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ְ��ίԱ��ְ��"]], objSheet.Cells[i, htCol["ְ��ίԱ��ְ��"]]));
					newRow["ְ��ίԱ��ְ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ְ����Ա����"]], objSheet.Cells[i, htCol["ְ����Ա����"]]));
                    newRow["ְ����Ա����"] = range.Text.ToString();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�ڲ�"]], objSheet.Cells[i, htCol["�ڲ�"]]));
					newRow["�ڲ�"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�ڸ�"]], objSheet.Cells[i, htCol["�ڸ�"]]));
                    newRow["�ڸ�"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����ҵְ������"]], objSheet.Cells[i, htCol["����ҵְ������"]]));
					newRow["����ҵְ������"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�ɲ�����ְ��"]], objSheet.Cells[i, htCol["�ɲ�����ְ��"]]));
					newRow["�ɲ�����ְ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���˼��ܵȼ�"]], objSheet.Cells[i, htCol["���˼��ܵȼ�"]]));
					newRow["���˼��ܵȼ�"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��λ��ѵ�ϸ�֤���"]], objSheet.Cells[i, htCol["��λ��ѵ�ϸ�֤���"]]));
					try
					{
						str = range.Value2.ToString().Replace("?", "");
						newRow["��λ��ѵ�ϸ�֤���"] = str;
					}
					catch
					{
						newRow["��λ��ѵ�ϸ�֤���"] = range.Value2;
					}

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��λ��ѵ�ϸ�֤�䷢����"]], objSheet.Cells[i, htCol["��λ��ѵ�ϸ�֤�䷢����"]]));
                    newRow["��λ��ѵ�ϸ�֤�䷢����"] = range.Text.ToString();


                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�����������"]], objSheet.Cells[i, htCol["�����������"]]));
					newRow["�����������"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���ܵȼ�ȡ��ʱ��"]], objSheet.Cells[i, htCol["���ܵȼ�ȡ��ʱ��"]]));
                    newRow["���ܵȼ�ȡ��ʱ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����ְ��Ƹ��ʱ��"]], objSheet.Cells[i, htCol["����ְ��Ƹ��ʱ��"]]));
                    newRow["����ְ��Ƹ��ʱ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����ְ��ʱ��"]], objSheet.Cells[i, htCol["����ְ��ʱ��"]]));
                    newRow["����ְ��ʱ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��ҵʱ��"]], objSheet.Cells[i, htCol["��ҵʱ��"]]));
                    newRow["��ҵʱ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ѧУ���"]], objSheet.Cells[i, htCol["ѧУ���"]]));
                    newRow["ѧУ���"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��ע"]], objSheet.Cells[i, htCol["��ע"]]));
                    newRow["��ע"] = range.Value2;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('���ڶ�ȡExcel�ļ�','" + ((double)((i - 1) * 100) / (double)rowNum).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
				}

				ds.Tables.Add(dtItem);

				// �������
				jsBlock = "<script>SetCompleted('Excel���ݶ�ȡ���'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			catch
			{
				isClose = true;
			}
			finally
			{
				objBook.Close(Type.Missing, strPath, Type.Missing);
				objBooks.Close();
				objApp.Application.Workbooks.Close();
				objApp.Application.Quit();
				objApp.Quit();
				GC.Collect();
			}

			if (isClose)
			{
				if (File.Exists(strPath))
					File.Delete(strPath);
				Response.Write("<script>window.returnValue='����Excel�ļ���ʽ',window.close();</script>");
				return;
			}

			#endregion

			#region ��������

			System.Threading.Thread.Sleep(10);
			jsBlock = "<script>SetPorgressBar('��׼�����Excel����','0.00'); </script>";
			Response.Write(jsBlock);
			Response.Flush();

			if (ds.Tables[0].Rows.Count == 0)
			{
				Response.Write("<script>window.returnValue='Excel��û���κμ�¼����˶�',window.close();</script>");
				return;
			}


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
            Hashtable htUniversityType = GetUniversityType();
            Hashtable htWorkShop = GetWorkShop();
            Hashtable htWorkGroup = GetWorkGroup();
            //Hashtable htEmployeeLevel = GetEmployeeLevel();
            //Hashtable htTeacherType = GetTeacherType();

			Hashtable htShopNeedAdd = new Hashtable();
			Hashtable htEmployeeName = new Hashtable(); //Ϊ���Excel�������Ƿ��ظ�
			Hashtable htWorkNo = new Hashtable(); //Ϊ���Excel�й���֤�����Ƿ��ظ�
			Hashtable htPostNo = new Hashtable(); //Ϊ���Excel��Ա�������Ƿ��ظ�
			Hashtable htPostNeedAdd = new Hashtable();
			Hashtable htIdentityCardNo = new Hashtable();

			PostBLL objPostBll = new PostBLL();

            IList<RailExam.Model.EmployeeDetail> objEmployeeInsert = new List<RailExam.Model.EmployeeDetail>();
            IList<RailExam.Model.EmployeeDetail> objEmployeeUpdate = new List<RailExam.Model.EmployeeDetail>();


			try
			{
				int index = 1;
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('���ڼ��Excel����','" +
							  ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					index = index + 1;

					RailExam.Model.EmployeeDetail objEmployee = new RailExam.Model.EmployeeDetail();

				    bool isUpdate = false;
                    if (dr["Ա��ID"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Employee_ID=" + dr["Ա��ID"].ToString().Trim());
                        if(objOldList.Count > 0)
                        {
                            isUpdate = true;
                        }
                        else
                        {
                            AddError(objErrorList, dr, "��Ա��ID��ϵͳ�в�����");
                            continue; 
                        }
                    }

				    //��λ����
					if (dr["��λ"].ToString().Trim() != strUnitName)
					{
						AddError(objErrorList, dr, "��λ��д����");
						continue;
					}

                    if (dr["����"].ToString().Trim() == "")
                    {
                        AddError(objErrorList, dr, "���䲻��Ϊ��");
                        continue;
                    }
                    else
                    {
                        if (!htWorkShop.ContainsKey(dr["����"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "�����ڱ���λ�����ڣ�");
                            continue;
                        }
                    }

                    //if (dr["����"].ToString().Trim() != "")
                    //{
                    //    if (!htWorkGroup.ContainsKey(dr["����"].ToString().Trim()))
                    //    {
                    //        AddError(objErrorList, dr, "�����ڱ���λ�����ڣ�");
                    //        continue;
                    //    }
                    //}

					//��֯����
					string strOrg;
					if (dr["����"].ToString().Trim() == string.Empty)
					{
                        strOrg = dr["��λ"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
					}
					else
					{
                        strOrg = dr["��λ"].ToString().Trim() + "-" + dr["����"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
					}

					if (!htOrg.ContainsKey(strOrg))
					{
						if (dr["����"].ToString().Trim() == string.Empty)
						{
                            if (!htShopNeedAdd.ContainsKey(dr["����"].ToString().Trim()))
							{
                                htShopNeedAdd.Add(dr["����"].ToString().Trim(), new Hashtable());
							}

							//�����֯������Ҫ����
							objEmployee.Memo = strOrg;
						}
						else
						{
                            if (!htShopNeedAdd.ContainsKey(dr["����"].ToString().Trim()))
							{
                                htShopNeedAdd.Add(dr["����"].ToString().Trim(), new Hashtable());
							}

                            Hashtable htGroupNeedAdd = (Hashtable)htShopNeedAdd[dr["����"].ToString().Trim()];
							if (!htGroupNeedAdd.ContainsKey(dr["����"].ToString().Trim()))
							{
								htGroupNeedAdd.Add(dr["����"].ToString().Trim(), dr["����"].ToString().Trim());
                                htShopNeedAdd[dr["����"].ToString().Trim()] = htGroupNeedAdd;
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
					if (dr["����"].ToString().Trim() == string.Empty)
					{
						AddError(objErrorList, dr, "Ա����������Ϊ��");
						continue;
					}
					else
					{
						if (dr["����"].ToString().Trim().Length > 20)
						{
							AddError(objErrorList, dr, "Ա���������ܳ���20λ");
							continue;
						}

						//������Excel�в����ظ�

						//if (htEmployeeName.ContainsKey(dr["����"].ToString().Trim()))
						//{
						//    AddError(objErrorList, dr, "Ա��������Excel�������Ϊ" + htEmployeeName[dr["����"].ToString()] + "��Ա�������ظ�");
						//    continue;
						//}
						//else
						//{
						//    htEmployeeName.Add(dr["����"].ToString().Trim(), dr["���"].ToString().Trim());
						//}

						//�����ڱ���λ�����в����ظ�

                        //IList<RailExam.Model.Employee> objView =
                        //    objEmployeeBll.GetEmployeeByWhereClause("GetStationOrgID(org_id)=" + orgID + " and Employee_Name ='" +
                        //                                            dr["����"].ToString().Trim() + "'");
                        //if (objView.Count > 0)
                        //{
                        //    AddError(objErrorList, dr, "Ա����������ϵͳ�д���");
                        //    continue;
                        //}

						objEmployee.EmployeeName = dr["����"].ToString().Trim();
						objEmployee.PinYinCode = Pub.GetChineseSpell(dr["����"].ToString().Trim());
					}

					//���֤�Ų���Ϊ��
					if (dr["���֤��"].ToString().Trim() == string.Empty)
					{
						AddError(objErrorList, dr, "���֤�Ų���Ϊ��");
						continue;
					}
					else
					{
                        if (dr["���֤��"].ToString().Trim().Length > 18)
                        {
                            AddError(objErrorList, dr, "���֤�Ų��ܳ���18λ");
                            continue;
                        }

                        //���֤����Excel�в����ظ�
                        if (htIdentityCardNo.ContainsKey(dr["���֤��"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "���֤����Excel�������Ϊ" + htIdentityCardNo[dr["���֤��"].ToString().Trim()] + "�����֤���ظ�");
                            continue;
                        }
                        else
                        {
                            htIdentityCardNo.Add(dr["���֤��"].ToString().Trim(), dr["���"].ToString().Trim());
                        }


                        IList<RailExam.Model.Employee> objList =
                             objEmployeeBll.GetEmployeeByWhereClause("identity_CardNo='" + dr["���֤��"].ToString().Trim() + "'");
                        if (objList.Count > 0)
                        {
                            if (isUpdate)
                            {
                                if (objList.Count > 1)
                                {
                                    AddError(objErrorList, dr, "���Ա�����֤����ȫ��ͬ��Ա���Ѿ�����");
                                    continue;
                                }
                            }
                            else
                            {
                                AddError(objErrorList, dr, "��Ա�����֤���롾" + objList[0].OrgName + "���С�" + objList[0].EmployeeName + "�������֤����ȫ��ͬ");
                                continue;
                            }
                        }

						objEmployee.IdentifyCode = dr["���֤��"].ToString().Trim();
					}

                    #region ����֤��
                    //if (dr["����֤��"].ToString().Trim() != string.Empty)
                    //{
                    //    if (dr["����֤��"].ToString().Trim().Length > 14)
                    //    {
                    //        AddError(objErrorList, dr, "����֤�Ų��ܳ���14λ");
                    //        continue;
                    //    }

                    //    //����֤����Excel�в����ظ�
                    //    //if (htWorkNo.ContainsKey(dr["����֤��"].ToString().Trim()))
                    //    //{
                    //    //    AddError(objErrorList, dr, "����֤����Excel�������Ϊ" + htWorkNo[dr["����֤��"].ToString()] + "�Ĺ���֤���ظ�");
                    //    //    continue;
                    //    //}
                    //    //else
                    //    //{
                    //    //    htWorkNo.Add(dr["����֤��"].ToString().Trim(), dr["���"].ToString().Trim());
                    //    //}

                    //    //IList<RailExam.Model.Employee> objView =
                    //    //    objEmployeeBll.GetEmployeeByWhereClause("Post_No='" + dr["����֤��"].ToString().Trim() + "'");
                    //    //if (objView.Count > 0)
                    //    //{
                    //    //    AddError(objErrorList, dr, "Ա������֤������ϵͳ�д���");
                    //    //    continue;
                    //    //}

                    //    objEmployee.PostNo = dr["����֤��"].ToString().Trim();
                    //}
                    //else
                    //{
                    //    objEmployee.PostNo = "";
                    //}
                    #endregion


					//�Ա�
					if (dr["�Ա�"].ToString().Trim() != "��" && dr["�Ա�"].ToString().Trim() != "Ů")
					{
						AddError(objErrorList, dr, "�Ա����Ϊ�л�Ů");
						continue;
					}
					else
					{
						objEmployee.Sex = dr["�Ա�"].ToString().Trim();
                    }

                    #region ����
                    ////����
                    //if (dr["����"].ToString().Trim().Length > 20)
                    //{
                    //    AddError(objErrorList, dr, "���᲻�ܳ���20λ");
                    //    continue;
                    //}
                    //else
                    //{
                    //    objEmployee.NativePlace = dr["����"].ToString().Trim();
                    //}

                    ////����
                    //if (dr["����"].ToString().Trim().Length > 10)
                    //{
                    //    AddError(objErrorList, dr, "���岻�ܳ���10λ");
                    //    continue;
                    //}
                    //else
                    //{
                    //    objEmployee.Folk = dr["����"].ToString().Trim();
                    //}

                    ////����״��
                    //if (dr["����״��"].ToString().Trim() == "δ��")
                    //{
                    //    objEmployee.Wedding = 0;
                    //}
                    //else
                    //{
                    //    objEmployee.Wedding = 1;
                    //}
                    #endregion

                    //���Ļ��̶�
					if (dr["�Ļ��̶�"].ToString().Trim() == string.Empty)
					{
						AddError(objErrorList, dr, "�Ļ��̶Ȳ���Ϊ��");
						continue;
					}
					else
					{
						if (!htEducationLevel.ContainsKey(dr["�Ļ��̶�"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "�Ļ��̶���ϵͳ�в�����");
							continue;
						}
						else
						{
							objEmployee.EducationLevelID = Convert.ToInt32(htEducationLevel[dr["�Ļ��̶�"].ToString().Trim()]);
						}
					}

					//������ò
					if (dr["������ò"].ToString().Trim() != string.Empty)
					{
						if (!htPoliticalStatus.ContainsKey(dr["������ò"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "������ò��ϵͳ�в�����");
							continue;
						}
						else
						{
							objEmployee.PoliticalStatusID = Convert.ToInt32(htPoliticalStatus[dr["������ò"].ToString().Trim()]);
						}
					}
                    else
					{
					    objEmployee.PoliticalStatusID = 1;
					}


                    #region ����
                    ////������ַ
                    //if (dr["������ַ"].ToString().Trim().Length > 100)
                    //{
                    //    AddError(objErrorList, dr, "������ַ���ܳ���100λ");
                    //    continue;
                    //}
                    //else
                    //{
                    //    objEmployee.Address = dr["������ַ"].ToString().Trim();
                    //}

                    ////��������
                    //if (dr["��������"].ToString().Trim().Length > 6)
                    //{
                    //    AddError(objErrorList, dr, "�������벻�ܳ���6λ");
                    //    continue;
                    //}
                    //else
                    //{
                    //    objEmployee.PostCode = dr["��������"].ToString().Trim();
                    //}

                    ////ְ�񼶱�
                    //if (dr["ְ�񼶱�"].ToString().Trim() != string.Empty)
                    //{
                    //    if (!htEmployeeLevel.ContainsKey(dr["ְ�񼶱�"].ToString().Trim()))
                    //    {
                    //        AddError(objErrorList, dr, "ְ�񼶱���ϵͳ�в�����");
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        objEmployee.EmployeeLevelID = Convert.ToInt32(htEmployeeLevel[dr["ְ�񼶱�"].ToString().Trim()]);
                    //    }
                    //}
                    #endregion

                    //��������
                    if (dr["��������"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "�������ڲ���Ϊ��");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string strBirth = dr["��������"].ToString().Trim();
                            if (strBirth.IndexOf("-") >= 0)
                            {
                                objEmployee.Birthday = Convert.ToDateTime(strBirth);
                            }
                            else
                            {
                                if (strBirth.Length != 8)
                                {
                                    AddError(objErrorList, dr, "����������д����");
                                    continue;
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
                                AddError(objErrorList, dr, "����������д����");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "����������д����");
                            continue;
                        }
                    }

				    //��·��������
                    if (dr["��·ʱ��"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "��·ʱ�䲻��Ϊ��");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string strJoin = dr["��·ʱ��"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.WorkDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "��·ʱ����д����");
                                    continue;
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
                                AddError(objErrorList, dr, "��·ʱ����д����");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "��·ʱ����д����");
                            continue;
                        }
                    }


				    //�μӹ�������
                    if (dr["�μӹ���ʱ��"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "�μӹ���ʱ�䲻��Ϊ��");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string strJoin = dr["�μӹ���ʱ��"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.BeginDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "�μӹ���ʱ����д����");
                                    continue;
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
                                AddError(objErrorList, dr, "�μӹ���ʱ����д����");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "�μӹ���ʱ����д����");
                            continue;
                        }
                    }

				    //ְ������
					if (dr["ְ������"].ToString().Trim() == "")
					{
                        AddError(objErrorList, dr, "ְ�����Ͳ���Ϊ�գ�");
						continue;
					}
					else
					{
                        if (!htEmployeeType.ContainsKey(dr["ְ������"].ToString().Trim()))
						{
                            AddError(objErrorList, dr, "ְ��������ϵͳ�в����ڣ�");
							continue;
						}
						else
						{
                            objEmployee.EmployeeTypeID = Convert.ToInt32(htEmployeeType[dr["ְ������"].ToString().Trim()]);
						}
					}


                    //ְ��
					if (dr["ְ��"].ToString().Trim() == string.Empty)
					{
                        AddError(objErrorList, dr, "ְ������Ϊ�գ�");
						continue;
					}
					else
					{
						IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["ְ��"].ToString().Trim() + "'");
						if (objPost.Count == 0)
						{
                            AddError(objErrorList, dr, "ְ����ϵͳ�в����ڣ�");
							continue;
						}

                        objEmployee.PostID = Convert.ToInt32(htPost[dr["ְ��"].ToString().Trim()]);
					}

                    //����ְ��ʱ��
                    if (dr["����ְ��ʱ��"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "����ְ��ʱ�䲻��Ϊ��");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string strJoin = dr["����ְ��ʱ��"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.PostDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "����ְ��ʱ����д����");
                                    continue;
                                }
                                else
                                {
                                    strJoin = strJoin.Insert(4, "-");
                                    strJoin = strJoin.Insert(7, "-");
                                    objEmployee.PostDate = Convert.ToDateTime(strJoin);
                                }
                            }

                            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                            {
                                AddError(objErrorList, dr, "����ְ��ʱ����д����");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "����ְ��ʱ����д����");
                            continue;
                        }
                    }

                    //ְ��
                    if (dr["�ִ��¸�λ"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["�ִ��¸�λ"].ToString().Trim() + "'");
                        if (objPost.Count == 0)
                        {
                            AddError(objErrorList, dr, "�ִ��¸�λ��ϵͳ�в����ڣ�");
                            continue;
                        }

                        objEmployee.NowPostID = Convert.ToInt32(htPost[dr["�ִ��¸�λ"].ToString().Trim()]);
                    }
                    else
                    {
                        objEmployee.NowPostID = null;
                    }

				    //ְ��
                    if (dr["�ڶ�ְ��"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["�ڶ�ְ��"].ToString().Trim() + "'");
                        if (objPost.Count == 0)
                        {
                            AddError(objErrorList, dr, "�ڶ�ְ����ϵͳ�в����ڣ�");
                            continue;
                        }

                        objEmployee.SecondPostID = Convert.ToInt32(htPost[dr["�ڶ�ְ��"].ToString().Trim()]);
                    }
                    else
                    {
                        objEmployee.SecondPostID = null;
                    }

                    //ְ��
                    if (dr["����ְ��"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["����ְ��"].ToString().Trim() + "'");
                        if (objPost.Count == 0)
                        {
                            AddError(objErrorList, dr, "����ְ����ϵͳ�в����ڣ�");
                            continue;
                        }

                        objEmployee.ThirdPostID = Convert.ToInt32(htPost[dr["����ְ��"].ToString().Trim()]);
                    }
                    else
                    {
                        objEmployee.ThirdPostID = null;
                    }



					//���鳤����
					if (dr["���鳤����"].ToString().Trim() == string.Empty)
					{
						objEmployee.IsGroupLeader = 0;
					}
					else
					{
						if (!htWorkGroupLeaderType.ContainsKey(dr["���鳤����"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "���鳤������ϵͳ�в����ڣ�");
							continue;
						}

						objEmployee.WorkGroupLeaderTypeID = Convert.ToInt32(htWorkGroupLeaderType[dr["���鳤����"].ToString().Trim()]);
						objEmployee.IsGroupLeader = 1;
					}


                    if (dr["���鳤��������"].ToString().Trim() != string.Empty)
                    {
                        //���鳤��������
                        try
                        {
                            string strJoin = dr["���鳤��������"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.GroupNoDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "���鳤����������д����");
                                    continue;
                                }
                                else
                                {
                                    strJoin = strJoin.Insert(4, "-");
                                    strJoin = strJoin.Insert(7, "-");
                                    objEmployee.GroupNoDate = Convert.ToDateTime(strJoin);
                                }
                            }

                            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                            {
                                AddError(objErrorList, dr, "���鳤����������д����");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "���鳤����������д����");
                            continue;
                        }
                    }
                    else
                    {
                        if (dr["���鳤����"].ToString().Trim() != string.Empty)
                        {
                            AddError(objErrorList, dr, "�����鳤���Ͳ�Ϊ��ʱ�����鳤�������ڲ���Ϊ��");
                            continue;
                        }
                    }


				    //ְ�̸ɲ�����
					//if (dr["��������"].ToString().Trim() != "ְ��������") //�������Ʋ�Ϊվ��ְ�̿�
					//{
					//    if (dr["ְ�̸ɲ�����"].ToString().Trim() != string.Empty)
					//    {
					//        AddError(objErrorList, dr, "�������������Ͳ�Ϊ��վ�ν����ơ�ʱ��ְ�̸ɲ����ͱ���Ϊ�գ�");
					//        continue;
					//    }
					//}
					//else
					//{
					if (dr["ְ����Ա����"].ToString().Trim() != string.Empty)
					{
                        if (!htEducationEmployeeType.ContainsKey(dr["ְ����Ա����"].ToString().Trim()))
						{
                            AddError(objErrorList, dr, "ְ����Ա������ϵͳ�в����ڣ�");
							continue;
						}
						else
						{
                            objEmployee.EducationEmployeeTypeID = Convert.ToInt32(htEducationEmployeeType[dr["ְ����Ա����"].ToString().Trim()]);
						}
					}
                    else
					{
					    objEmployee.EducationEmployeeTypeID = -1;
					}
					//}

					//ְ��ίԱ��ְ��
					if (dr["ְ��ίԱ��ְ��"].ToString().Trim() != string.Empty)
					{
						if (!htCommitteeHeadship.ContainsKey(dr["ְ��ίԱ��ְ��"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "ְ��ίԱ��ְ����ϵͳ�в����ڣ�");
							continue;
						}
						else
						{
							objEmployee.CommitteeHeadShipID = Convert.ToInt32(htCommitteeHeadship[dr["ְ��ίԱ��ְ��"].ToString().Trim()]);
						}
					}
					else
					{
					    objEmployee.CommitteeHeadShipID = -1;
					}

					//��ʦ���
                    //if (dr["��ʦ���"].ToString().Trim() != string.Empty)
                    //{
                    //    if (!htTeacherType.ContainsKey(dr["��ʦ���"].ToString().Trim()))
                    //    {
                    //        AddError(objErrorList, dr, "��ʦ�����ϵͳ�в�����");
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        objEmployee.TeacherTypeID = Convert.ToInt32(htTeacherType[dr["��ʦ���"].ToString().Trim()]);
                    //    }
                    //}

                    //�ڲ�
                    if (dr["�ڲ�"].ToString().Trim() == "���ڲ�")
					{
                        objEmployee.Dimission = false;
					}
                    else if (dr["�ڲ�"].ToString().Trim() == "�ڲ�")
					{
						objEmployee.Dimission = true;
					}
                    else
                    {
                        AddError(objErrorList, dr, "�ڲ�ֻ����ڲᡱ�͡����ڲᡱ��");
                        continue; 
                    }

                    //�ڸ�
                    if (dr["�ڸ�"].ToString().Trim() == "���ڸ�")
                    {
                        objEmployee.IsOnPost = false;
                    }
                    else if(dr["�ڸ�"].ToString().Trim() == "�ڸ�")
                    {
                        objEmployee.IsOnPost = true;
                    }
                    else
                    {
                        AddError(objErrorList, dr, "�ڸ�ֻ����ڸڡ��͡����ڸڡ���");
                        continue;
                    }


                    //����ҵְ������
					if (dr["����ҵְ������"].ToString().Trim() == string.Empty)
					{
					    objEmployee.EmployeeTransportTypeID = -1;
					}
					else
					{
                        if (!htEmployeeTransportType.ContainsKey(dr["����ҵְ������"].ToString().Trim()))
						{
                            AddError(objErrorList, dr, "����ҵְ��������ϵͳ�в����ڣ�");
							continue;
						}
						else
						{
                            objEmployee.EmployeeTransportTypeID = Convert.ToInt32(htEmployeeTransportType[dr["����ҵְ������"].ToString().Trim()]);
						}
					}

					//�ּ���ְ������
					if (objEmployee.EmployeeTypeID == 0)
					{
						if (dr["�ɲ�����ְ��"].ToString().Trim() != string.Empty)
						{
                            AddError(objErrorList, dr, "���ɲ����˱�ʶΪ�����ˡ�ʱ���ɲ�����ְ�Ʊ���Ϊ�գ�");
							continue;
						}
					}
					else
					{
                        if (dr["�ɲ�����ְ��"].ToString().Trim() != string.Empty)
						{
                            if (!htTechnicalTitle.ContainsKey(dr["�ɲ�����ְ��"].ToString().Trim()))
							{
                                AddError(objErrorList, dr, "�ɲ�����ְ����ϵͳ�в����ڣ�");
								continue;
							}
							else
							{
                                objEmployee.TechnicalTitleID = Convert.ToInt32(htTechnicalTitle[dr["�ɲ�����ְ��"].ToString().Trim()]);
							}

                            if (dr["����ְ��Ƹ��ʱ��"].ToString().Trim() != string.Empty)
                            {
                                //����ְ��Ƹ��ʱ��
                                try
                                {
                                    string strJoin = dr["����ְ��Ƹ��ʱ��"].ToString().Trim();
                                    if (strJoin.IndexOf("-") >= 0)
                                    {
                                        objEmployee.TechnicalTitleDate = Convert.ToDateTime(strJoin);
                                    }
                                    else
                                    {
                                        if (strJoin.Length != 8)
                                        {
                                            AddError(objErrorList, dr, "����ְ��Ƹ��ʱ����д����");
                                            continue;
                                        }
                                        else
                                        {
                                            strJoin = strJoin.Insert(4, "-");
                                            strJoin = strJoin.Insert(7, "-");
                                            objEmployee.TechnicalTitleDate = Convert.ToDateTime(strJoin);
                                        }
                                    }

                                    if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                                    {
                                        AddError(objErrorList, dr, "����ְ��Ƹ��ʱ����д����");
                                        continue;
                                    }
                                }
                                catch
                                {
                                    AddError(objErrorList, dr, "����ְ��Ƹ��ʱ����д����");
                                    continue;
                                }
                            }
                            else
                            {
                                objEmployee.TechnicalTitleDate = null;
                                //AddError(objErrorList, dr, "���ɲ�����ְ�Ʋ�Ϊ��ʱ������ְ��Ƹ��ʱ�䲻��Ϊ��");
                                //continue;
                            }
						}
					}

					//�����ȼ�
					if (objEmployee.EmployeeTypeID == 0)
					{
						if (dr["���˼��ܵȼ�"].ToString().Trim() != string.Empty)
						{
                            if (!htSkillLevel.ContainsKey(dr["���˼��ܵȼ�"].ToString().Trim()))
							{
                                AddError(objErrorList, dr, "���˼��ܵȼ���ϵͳ�в����ڣ�");
								continue;
							}
							else
							{
                                objEmployee.TechnicianTypeID = Convert.ToInt32(htSkillLevel[dr["���˼��ܵȼ�"].ToString().Trim()]);
							}

                            if (dr["���ܵȼ�ȡ��ʱ��"].ToString().Trim() != string.Empty)
                            {
                                //���鳤��������
                                try
                                {
                                    string strJoin = dr["���ܵȼ�ȡ��ʱ��"].ToString().Trim();
                                    if (strJoin.IndexOf("-") >= 0)
                                    {
                                        objEmployee.TechnicalDate = Convert.ToDateTime(strJoin);
                                    }
                                    else
                                    {
                                        if (strJoin.Length != 8)
                                        {
                                            AddError(objErrorList, dr, "���ܵȼ�ȡ��ʱ����д����");
                                            continue;
                                        }
                                        else
                                        {
                                            strJoin = strJoin.Insert(4, "-");
                                            strJoin = strJoin.Insert(7, "-");
                                            objEmployee.TechnicalDate = Convert.ToDateTime(strJoin);
                                        }
                                    }

                                    if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                                    {
                                        AddError(objErrorList, dr, "���ܵȼ�ȡ��ʱ����д����");
                                        continue;
                                    }
                                }
                                catch
                                {
                                    AddError(objErrorList, dr, "���ܵȼ�ȡ��ʱ����д����");
                                    continue;
                                }
                            }
                            else
                            {
                                objEmployee.TechnicalDate = null;
                                //AddError(objErrorList, dr, "�����˼��ܵȼ���Ϊ��ʱ�����ܵȼ�ȡ��ʱ�䲻��Ϊ��");
                                //continue;
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
					if (dr["��λ��ѵ�ϸ�֤���"].ToString().Trim() != string.Empty)
					{
						if (dr["��λ��ѵ�ϸ�֤���"].ToString().Trim().Length > 20)
						{
							AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤��Ų��ܳ���20λ");
							continue;
						}

                        //��λ��ѵ�ϸ�֤�����Excel�в����ظ�
						if (htPostNo.ContainsKey(dr["��λ��ѵ�ϸ�֤���"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤�����Excel�������Ϊ" + htPostNo[dr["��λ��ѵ�ϸ�֤���"].ToString().Trim()] + "�ĸ�λ��ѵ�ϸ�֤����ظ�");
							continue;
						}
						else
						{
							htPostNo.Add(dr["��λ��ѵ�ϸ�֤���"].ToString().Trim(), dr["���"].ToString().Trim());
						}

                        IList<RailExam.Model.Employee> objView;

                        if (!isUpdate)
                        {
                            objView =
                                objEmployeeBll.GetEmployeeByWhereClause("Work_No='" + dr["��λ��ѵ�ϸ�֤���"].ToString().Trim() + "'");
                        }
                        else
                        {
                            objView =
                                objEmployeeBll.GetEmployeeByWhereClause("Employee_ID<>" + dr["Ա��ID"] + " and Work_No='" + dr["��λ��ѵ�ϸ�֤���"].ToString().Trim() + "'");
                        }

                        if (objView.Count > 0)
                        {
                            AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤�������ϵͳ�д���");
                            continue;
                        }

                        objEmployee.WorkNo = dr["��λ��ѵ�ϸ�֤���"].ToString().Trim();
					}
					else
					{
						objEmployee.WorkNo = string.Empty;
                        //AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤��Ų���Ϊ�գ����ȷʵû�У������빤��֤�Ż����֤�ţ�");
                        //continue;
					}

                    //��λ��ѵ�ϸ�֤�䷢����
                    if(dr["��λ��ѵ�ϸ�֤�䷢����"].ToString().Trim()!= string.Empty)
				    {
				        try
				        {
				            string strJoin = dr["��λ��ѵ�ϸ�֤�䷢����"].ToString().Trim();
				            if (strJoin.IndexOf("-") >= 0)
				            {
				                objEmployee.PostNoDate = Convert.ToDateTime(strJoin);
				            }
				            else
				            {
				                if (strJoin.Length != 8)
				                {
				                    AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤�䷢������д����");
				                    continue;
				                }
				                else
				                {
				                    strJoin = strJoin.Insert(4, "-");
				                    strJoin = strJoin.Insert(7, "-");
				                    objEmployee.PostNoDate = Convert.ToDateTime(strJoin);
				                }
				            }

				            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
				            {
				                AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤�䷢������д����");
				                continue;
				            }
				        }
				        catch
				        {
                            AddError(objErrorList, dr, "��λ��ѵ�ϸ�֤�䷢������д����");
				            continue;
				        }
				    }

				    if (dr["�����������"].ToString().Trim() != string.Empty)
					{
                        if (dr["�����������"].ToString().Trim().Length > 20)
						{
                            AddError(objErrorList, dr, "����������Ų��ܳ���20λ");
							continue;
						}
                        objEmployee.TechnicalCode = dr["�����������"].ToString().Trim();
					}

                    //��ҵѧУ
                    if (dr["��ҵѧУ"].ToString().Trim().Length > 50)
                    {
                        AddError(objErrorList, dr, "��ҵѧУ���ܳ���50λ");
                        continue;
                    }
                    else
                    {
                        objEmployee.GraduateUniversity = dr["��ҵѧУ"].ToString().Trim();
                    }

                    //��ѧרҵ
                    if (dr["��ѧרҵ"].ToString().Trim().Length > 50)
                    {
                        AddError(objErrorList, dr, "��ѧרҵ���ܳ���50λ");
                        continue;
                    }
                    else
                    {
                        objEmployee.StudyMajor = dr["��ѧרҵ"].ToString().Trim();
                    }

                    //��ҵʱ��
                    if (dr["��ҵʱ��"].ToString().Trim() != string.Empty)
                    {
                        try
                        {
                            string strJoin = dr["��ҵʱ��"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.GraduatDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "��ҵʱ����д����");
                                    continue;
                                }
                                else
                                {
                                    strJoin = strJoin.Insert(4, "-");
                                    strJoin = strJoin.Insert(7, "-");
                                    objEmployee.GraduatDate = Convert.ToDateTime(strJoin);
                                }
                            }

                            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                            {
                                AddError(objErrorList, dr, "��ҵʱ����д����");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "��ҵʱ����д����");
                            continue;
                        }
                    }

                    if (dr["ѧУ���"].ToString().Trim() != string.Empty)
                    {
                        if (!htUniversityType.ContainsKey(dr["ѧУ���"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "ѧУ�����ϵͳ�в�����");
                            continue;
                        }
                        else
                        {
                            objEmployee.UniversityType = Convert.ToInt32(htUniversityType[dr["ѧУ���"].ToString().Trim()]);
                        }
                    }
                    else
                    {
                        objEmployee.UniversityType = 0;
                    }


                    if (dr["Ա��ID"].ToString().Trim() == string.Empty)
                    {
                       objEmployeeInsert.Add(objEmployee);
                    }
                    else
                    {
                        try
                        {
                            int employeeID = Convert.ToInt32(dr["Ա��ID"].ToString().Trim());
                            objEmployee.EmployeeID = employeeID;
                            objEmployeeUpdate.Add(objEmployee);

                        }
                        catch
                        {
                            AddError(objErrorList, dr, "Ա��ID��д����");
                            continue;
                        }
                    }
				}

				// �������
				jsBlock = "<script>SetCompleted('Excel���ݼ�����'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			catch (Exception ex)
			{
				Response.Write(EnhancedStackTrace(ex));
			}

			#endregion

			if (objErrorList.Count > 0)
			{
				jsBlock = "<script>SetCompleted('����ͳ�Ʋ�����Ҫ������ݣ���ȴ�......'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				objErrorBll.AddEmployeeError(objErrorList);

				if (File.Exists(strPath))
					File.Delete(strPath);
				Response.Write("<script>window.returnValue='refresh|����Excel����',window.close();</script>");
				return;
			}


			if (!string.IsNullOrEmpty(Request.QueryString.Get("mode")))
			{
				Response.Write("<script>window.returnValue='refresh|���ݼ��ɹ�',window.close();</script>");
				return;
			}

			#region ��������

			System.Threading.Thread.Sleep(10);
			jsBlock = "<script>SetPorgressBar('���ڵ��벿�����ơ�����','0.00'); </script>";
			Response.Write(jsBlock);
			Response.Flush();

			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				Hashtable htWorkshop = GetWorkShop(db, transaction);
				int count = 1;
				foreach (System.Collections.DictionaryEntry obj in htShopNeedAdd)
				{
					int nWorkShopID;
					if (!htWorkshop.ContainsKey(obj.Key.ToString()))
					{
						RailExam.Model.Organization objshop = new RailExam.Model.Organization();
						objshop.FullName = obj.Key.ToString();
						objshop.ShortName = obj.Key.ToString();
						objshop.ParentId = Convert.ToInt32(orgID);
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

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('���ڵ��벿�����ơ�����','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					count = count + 1;
				}

				htWorkshop = GetWorkShop(db, transaction);
				Hashtable htNowOrg = GetOrgInfo(db, transaction);

				foreach (RailExam.Model.EmployeeDetail objEmployee in objEmployeeInsert)
				{
					if (objEmployee.Memo.ToString() != string.Empty)
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


					System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ��벿�����ơ�����','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					count = count + 1;
				}

                foreach (RailExam.Model.EmployeeDetail objEmployee in objEmployeeUpdate)
                {
                    if (objEmployee.Memo.ToString() != string.Empty)
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

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ��벿�����ơ�����','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('���ڵ���ɲ�ְ��','0.00'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				count = 1;
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

					ArrayList objPostList = (ArrayList)objPostNeed.Value;
					for (int i = 0; i < objPostList.Count; i++)
					{
					    string[] strPost = objPostList[i].ToString().Split('|');
					    if(strPost[1] == "false")
					    {
                            objEmployeeInsert[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
					    else
					    {
                            objEmployeeUpdate[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
					}

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('���ڵ���ɲ�ְ��','" + ((double)(count * 100) / (double)(htPostNeedAdd.Count)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					count = count + 1;
				}

				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('���ڵ���Ա����Ϣ','0.00'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

                OracleAccess access = new OracleAccess();
			    string strSql;

				count = 1;
				for (int i = 0; i < objEmployeeInsert.Count; i++)
				{
					int employeeid = objEmployeeDetailBll.AddEmployee(db, transaction, objEmployeeInsert[i]);

                    //strSql = "select * from Employee_Photo where Employee_ID=" + employeeid;
                    //DataSet dsPhoto = access.RunSqlDataSet(strSql);
                    //if(dsPhoto.Tables[0].Rows.Count==0)
                    //{
                    //    strSql = "insert into Employee_Photo values(" + employeeid + ",null)";
                    //    access.ExecuteNonQuery(strSql);
                    //}

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('���ڵ���Ա����Ϣ','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count+objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					count = count + 1;
				}


                for (int i = 0; i < objEmployeeUpdate.Count; i++ )
                {
                    objEmployeeDetailBll.UpdateEmployee(db, transaction, objEmployeeUpdate[i]);
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ���Ա����Ϣ','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1; 
                }

                strMessage = "����ɹ�!";
				transaction.Commit();
			}
			catch (Exception ex)
			{
				strMessage = "����ʧ��!";
				transaction.Rollback();
				//Response.Write(EnhancedStackTrace(ex));
			}
			finally
			{
				connection.Close();
			}

			if (File.Exists(strPath))
				File.Delete(strPath);
			Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
			#endregion
		}

		#endregion

		/// <summary>
		/// ��Ӵ�����Ϣ
		/// </summary>
		/// <param name="objErrorList">������ϢList</param>
		/// <param name="dr">������ԴDataRow</param>
		/// <param name="strError">����ԭ��</param>
		private void AddError(IList<EmployeeError> objErrorList, DataRow dr, string strError)
		{
            EmployeeError objError = new EmployeeError();
            objError.OrgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
            objError.ExcelNo = Convert.ToInt32(dr["���"].ToString().Trim());
            objError.WorkNo = dr["��λ��ѵ�ϸ�֤���"].ToString().Trim();
            objError.EmployeeName = dr["����"].ToString().Trim();
            objError.Sex = dr["�Ա�"].ToString().Trim();
            objError.OrgPath = dr["����"].ToString().Trim();
            objError.PostPath = dr["ְ��"].ToString().Trim();
            objError.ErrorReason = strError;
            objError.OrgName = dr["��λ"].ToString().Trim();
            objError.GroupName = dr["����"].ToString().Trim();
            objError.IdentifyCode = dr["���֤��"].ToString().Trim();
            //objError.PostNo = dr["����֤��"].ToString().Trim();
            //objError.NativePlace = dr["����"].ToString().Trim();
            //objError.Folk = dr["����"].ToString().Trim();
            //objError.Wedding = dr["����״��"].ToString().Trim();
            objError.PoliticalStatus = dr["������ò"].ToString().Trim();
            objError.EducationLevel = dr["�Ļ��̶�"].ToString().Trim();
            //objError.GraduateUniversity = dr["��(��)ҵѧУ(��λ)"].ToString().Trim();
            //objError.StudyMajor = dr["��ѧרҵ"].ToString().Trim();
            //objError.Address = dr["������ַ"].ToString().Trim();
            //objError.EmployeeLevel = dr["ְ�񼶱�"].ToString().Trim();
            objError.Birthday = dr["��������"].ToString().Trim();
            objError.BeginDate = dr["��·ʱ��"].ToString().Trim();
            objError.WorkDate = dr["�μӹ���ʱ��"].ToString().Trim();
            objError.EmployeeType = dr["ְ������"].ToString().Trim();
            objError.WorkGroupLeader = dr["���鳤����"].ToString().Trim();
            //objError.TeacherType = dr["��ʦ���"].ToString().Trim();
            objError.OnPost = dr["�ڸ�"].ToString().Trim();
            objError.TechnicalTitle = dr["�ɲ�����ְ��"].ToString().Trim();
            objError.TechnicalSkill = dr["���˼��ܵȼ�"].ToString().Trim();
            objError.Address = dr["�����������"].ToString().Trim();
            objError.EducationEmployee = dr["ְ����Ա����"].ToString().Trim();
            objError.CommitteeHeadShip = dr["ְ��ίԱ��ְ��"].ToString().Trim();
            objError.EmployeeTransportType = dr["����ҵְ������"].ToString().Trim();

            objErrorList.Add(objError);
		}


		/// <summary>
		/// ��ȡ��ǰ�ĵ�λ���Ƶ���֯������Ϣ
		/// </summary>
		/// <returns></returns>
		private Hashtable GetOrgInfo()
		{
			Hashtable htOrg = new Hashtable();

			OrgImportBLL objBll = new OrgImportBLL();
			IList<OrgImport> objList = objBll.GetOrgImport(Convert.ToInt32(Request.QueryString.Get("OrgID")));

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
			IList<OrgImport> objList = objBll.GetOrgImport(db, trans, Convert.ToInt32(Request.QueryString.Get("OrgID")));

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
        private Hashtable GetWorkShop()
        {
            Hashtable htOrg = new Hashtable();

            OrganizationBLL objBll = new OrganizationBLL();
            IList<RailExam.Model.Organization> objList =
                objBll.GetOrganizationsByWhereClause("level_num=3 and GetStationOrgID(org_id)=" + Request.QueryString.Get("OrgID"));

            foreach (RailExam.Model.Organization obj in objList)
            {
                htOrg[obj.ShortName] = obj.OrganizationId;
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
				objBll.GetOrganizationsByWhereClause(db, trans, "level_num=3 and GetStationOrgID(org_id)=" + Request.QueryString.Get("OrgID"));

			foreach (RailExam.Model.Organization obj in objList)
			{
				htOrg[obj.ShortName] = obj.OrganizationId;
			}

			return htOrg;
		}

        private Hashtable GetWorkGroup()
        {
            Hashtable htOrg = new Hashtable();

            OrganizationBLL objBll = new OrganizationBLL();
            IList<RailExam.Model.Organization> objList =
                objBll.GetOrganizationsByWhereClause("level_num=4 and GetStationOrgID(org_id)=" + Request.QueryString.Get("OrgID"));

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

			htEmployeeType.Add("����", 0);
			htEmployeeType.Add("�����ɲ�", 1);
            htEmployeeType.Add("����ɲ�", 2);
            htEmployeeType.Add("�����ɲ�", 3);

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
			
            OracleAccess db = new OracleAccess();
		    string strSql = "select * from ZJ_EDUCATION_EMPLOYEE_TYPE";
		    DataSet ds = db.RunSqlDataSet(strSql);

		    foreach (DataRow dr in ds.Tables[0].Rows)
		    {
                htEducationEmployeeType.Add(dr["EDUCATION_EMPLOYEE_TYPE_NAME"].ToString(), dr["EDUCATION_EMPLOYEE_TYPE_ID"].ToString());
            }

			return htEducationEmployeeType;
		}

		/// <summary>
		/// ְ��ίԱ��ְ��

		/// </summary>
		/// <returns></returns>
		private Hashtable GetCommitteeHeadship()
		{
			Hashtable htCommitteeHeadship = new Hashtable();
            OracleAccess db = new OracleAccess();
            string strSql = "select * from ZJ_COMMITTEE_HEAD_SHIP";
            DataSet ds = db.RunSqlDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                htCommitteeHeadship.Add(dr["COMMITTEE_HEAD_SHIP_NAME"].ToString(), dr["COMMITTEE_HEAD_SHIP_ID"].ToString());
            }
			return htCommitteeHeadship;
		}

		/// <summary>
		/// ����ҵ�ĸɲ����˱�ʶ
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeTransportType()
		{
			Hashtable htEmployeeTransportType = new Hashtable();
            htEmployeeTransportType.Add("������Ա", 0);
			htEmployeeTransportType.Add("������Ա", 1);
			htEmployeeTransportType.Add("������Ա", 2);
            htEmployeeTransportType.Add("������Ա", 3);

			return htEmployeeTransportType;
		}

        private Hashtable GetUniversityType()
        {
            Hashtable hfUniversityType = new Hashtable();
            hfUniversityType.Add("ȫ����", 1);
            hfUniversityType.Add("�������", 2);
            hfUniversityType.Add("��ѧ����", 3);
            hfUniversityType.Add("��У����", 4);
            hfUniversityType.Add("����ѧϰ", 5);
            hfUniversityType.Add("���ѧϰ", 6);
            hfUniversityType.Add("ְУѧϰ", 7);
            hfUniversityType.Add("ҵУѧϰ", 8);
            hfUniversityType.Add("ҹУѧϰ", 9);
            hfUniversityType.Add("���˽���", 10);
            return hfUniversityType;
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


		public static string EnhancedStackTrace(Exception ex)
		{
			return EnhancedStackTrace(new StackTrace(ex, true));
		}

		public static string EnhancedStackTrace(StackTrace st)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Environment.NewLine);
			sb.Append("---- Stack Trace ----");
			sb.Append(Environment.NewLine);
			for (int i = 0; i < st.FrameCount; i++)
			{
				StackFrame sf = st.GetFrame(i);
				MemberInfo mi = sf.GetMethod();
				sb.Append(StackFrameToString(sf));
			}
			sb.Append(Environment.NewLine);
			return sb.ToString();
		}

		public static string StackFrameToString(StackFrame sf)
		{
			StringBuilder sb = new StringBuilder();
			int intParam;
			MemberInfo mi = sf.GetMethod();
			sb.Append("   ");
			sb.Append(mi.DeclaringType.Namespace);
			sb.Append(".");
			sb.Append(mi.DeclaringType.Name);
			sb.Append(".");
			sb.Append(mi.Name);
			sb.Append("(");
			intParam = 0;
			foreach (ParameterInfo param in sf.GetMethod().GetParameters())
			{
				intParam += 1;
				sb.Append(param.Name);
				sb.Append(" As ");
				sb.Append(param.ParameterType.Name);
			}
			sb.Append(")");
			sb.Append(Environment.NewLine);
			sb.Append("       ");
			if (string.IsNullOrEmpty(sf.GetFileName()))
			{
				sb.Append("(unknown file)");
				sb.Append(": N ");
				sb.Append(String.Format("{0:#00000}", sf.GetNativeOffset()));
			}
			else
			{
				sb.Append(System.IO.Path.GetFileName(sf.GetFileName()));
				sb.Append(": line ");
				sb.Append(String.Format("{0:#0000}", sf.GetFileLineNumber()));
				sb.Append(", col ");
				sb.Append(String.Format("{0:#00}", sf.GetFileColumnNumber()));
				if (sf.GetILOffset() != StackFrame.OFFSET_UNKNOWN)
				{
					sb.Append(", IL ");
					sb.Append(String.Format("{0:#0000}", sf.GetILOffset()));
				}
			}
			sb.Append(Environment.NewLine);
			return sb.ToString();
		}
	}
}
