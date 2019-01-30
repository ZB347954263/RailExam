using System;
using System.Data;
using System.Collections;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    /// <summary>
    /// ��Ա��ϢExcel���루Excel�����ڰ�����·ERPϵͳ��
    /// </summary>
    public partial class ImportExcelByBaoERP : System.Web.UI.Page
    {
        // �ر���
        private static readonly string[] requistieColumns = { 
            "���", "��λ", "����", "��λ����", "Ա����", "Ա������", "����", "�Ա�", "����", "��������", 
            "���֤����", "�μӹ���ʱ��", "���뱾��˾ʱ��", "������ò", "���ѧ��" ,"�ڸ�","�ڲ�"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImportEmployeeExcel();
            }
        }

        #region ��ȡExcel

        // ��ȡExcel�ļ��еĵ�һ��Sheet
        private ISheet GetSheet(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook hssfWorkbook = new HSSFWorkbook(file);
                return hssfWorkbook.GetSheetAt(0);
            }
        }

        // ����Excel����ͷ������DataTable��
        private void InitDataTableColumns(DataTable dt, IRow row)
        {
            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i);
                if (cell != null)
                {
                    dt.Columns.Add(cell.ToString().Replace("\n", string.Empty));
                }
            }
        }

        // ��ȡ��һ�У���ͷ�У�
        private DataTable GetHeadLine(ISheet sheet)
        {
            IRow row = sheet.GetRow(0);    // ��ȡ��һ��
            DataTable dt = new DataTable();   // ���ڷ���ֵ��DataTable����
            // ����DataTable�ı�ṹ
            InitDataTableColumns(dt, row);
            // ����ȡ����IRow����д��DataTable��
            DataRow dr = dt.NewRow();
            for (int i = 0, j = 0; i < row.LastCellNum; i++)
            {
                try
                {
                    ICell cell = row.GetCell(i);
                    if (cell != null)
                    {
                        dr[j] = cell.ToString().Replace("\n", "");
                        j++;
                    }
                }
                catch { break; } // ����û��Ҫ���κ������Ĵ���
            }
            dt.Rows.Add(dr);

            return dt;
        }

        // ��֤��ͷ��
        private bool ValidateHeadLine(DataTable dtHeadLine, out string error)
        {
            error = CheckContain(requistieColumns, dtHeadLine);
            return string.IsNullOrEmpty(error);
        }

        // ��֤�ر���(������֤��ͷ�������)
        private string CheckContain(string[] str, DataTable dtInfo)
        {
            string errorMessage = "";

            for (int i = 0; i < str.Length; i++)
            {
                if (!dtInfo.Columns.Contains(str[i]))
                {
                    errorMessage += errorMessage == string.Empty ? str[i] : "," + str[i];
                }
            }

            return errorMessage;
        }

        // ��ȡ������
        private DataTable GetDataRows(ISheet sheet, string jsBlock)
        {
            DataTable dt = new DataTable();   // ���ڷ���ֵ��DataTable����

            IEnumerator rows = sheet.GetRowEnumerator();   // ��ȡ���е�ö����
            rows.MoveNext(); // �������У���ͷ�У���
            IRow row = (HSSFRow)rows.Current;    // ��ȡ��һ��, ����ȷ����ṹ��

            // ����DataTable�ı�ṹ
            InitDataTableColumns(dt, row);

            // ���� ProgressBar.htm ��ʾ����������
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            // ѭ����ȡ������
            int index = 1;
            while (rows.MoveNext())
            {
                if (rows.Current == null) { break; } // �����н���
                IRow rowCurr = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0, j = 0; i < rowCurr.LastCellNum; i++)
                {
                    ICell cell = rowCurr.GetCell(i);
                    if (cell != null)
                    {
                        dr[j] = cell.ToString();
                    }
                    else
                    {
                        dr[j] = "";
                    }
                    j++;
                }
                dt.Rows.Add(dr);

                // ������Ч��
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڶ�ȡExcel�ļ�','" + ((double)((index++) * 100) / (double)sheet.LastRowNum).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }

            return dt;
        }

        #endregion

        // ���� 
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
            string jsBlock = string.Empty;
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
            bool isClose = false;
            string strMessage;

            objErrorBll.DeleteEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(orgID));

            #region ��֤׼������

            Hashtable htOrg = GetOrgInfo();
            Hashtable htPost = GetPostInfo();
            Hashtable htEducationLevel = GetEducationLevel();
            Hashtable htPoliticalStatus = GetPoliticalStatus();
            Hashtable htShopNeedAdd = new Hashtable();
            Hashtable htPostNeedAdd = new Hashtable();
            Hashtable htIdentityCardNo = new Hashtable();
            Hashtable htERP = new Hashtable();

            PostBLL objPostBll = new PostBLL();
            ArrayList objPostNewList = new ArrayList();

            IList<RailExam.Model.EmployeeDetail> objEmployeeInsert = new List<RailExam.Model.EmployeeDetail>();
            IList<RailExam.Model.EmployeeDetail> objEmployeeUpdate = new List<RailExam.Model.EmployeeDetail>();

            #endregion

            // ��ȡExcel��ͷ����
            ISheet sheet = GetSheet(strPath);
            // ��֤��ͷ
            DataTable headLine = GetHeadLine(sheet);
            string error;
            if (!ValidateHeadLine(headLine, out error))
            {
                Response.Write("<script>window.returnValue='" + error + "',window.close();</script>");
                return;
            }
            // ��ȡExcel��������Ϣ
            DataTable excelDataRows = GetDataRows(sheet, jsBlock);

            // ��������֤������

            // ��ӹ�����Ч��
            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('��׼�����Excel����','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (excelDataRows.Rows.Count == 0)
            {
                Response.Write("<script>window.returnValue='Excel��û���κμ�¼����˶�',window.close();</script>");
                return;
            }

            for (int i = 0; i < excelDataRows.Rows.Count; i++)
            {
                // ������Ч��
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڼ��Excel����','" +
                          ((double)((i + 1) * 100) / (double)excelDataRows.Rows.Count).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                RailExam.Model.EmployeeDetail objEmployee = new RailExam.Model.EmployeeDetail();
                bool isUpdate = false;
                DataRow dr = excelDataRows.Rows[i];

                //if (dr["Ա��ID"] != DBNull.Value && dr["Ա��ID"].ToString().Trim() != string.Empty)
                //{
                //    isUpdate = false;
                //    try
                //    {
                //        objEmployee.EmployeeID = Convert.ToInt32(dr["Ա��ID"].ToString().Trim());
                //    }
                //    catch
                //    {
                //        AddError(objErrorList, dr, "Ա��ID��д����");
                //        continue;
                //    }               
                //}
                //else
                //{
                //    isUpdate = true;
                //}

                // ��λ
                if (dr["��λ"].ToString().Trim() != strUnitName &&
                    dr["��λ"].ToString().Trim().Replace("�񻪰�����·�������ι�˾", "") != strUnitName)
                {
                    AddError(objErrorList, dr, "��λ��д����");
                    continue;
                }


                // ����
                if (dr["����"].ToString().Trim() == "")
                {
                    AddError(objErrorList, dr, "���Ų���Ϊ��");
                    continue;
                }

                string strWorkShop = dr["����"].ToString().Trim();
                if (strWorkShop.IndexOf("������·��˾" + strUnitName) >= 0)
                {
                    strWorkShop = dr["����"].ToString().Trim().Replace("������·��˾" + strUnitName, "");
                }

                //��֯����
                string strOrg;
                if (dr["Ա����"].ToString().Trim() == string.Empty)
                {
                    strOrg = strUnitName + "-" + strWorkShop;
                }
                else
                {
                    strOrg = strUnitName + "-" + strWorkShop + "-" + dr["Ա����"].ToString().Trim();
                }
                // �ж�����ƴ�ӵ��ַ����Ƿ��������֯������
                if (!htOrg.ContainsKey(strOrg))
                {
                    if (dr["Ա����"].ToString().Trim() == string.Empty)
                    {
                        if (!htShopNeedAdd.ContainsKey(strWorkShop))
                        {
                            htShopNeedAdd.Add(strWorkShop, new Hashtable());
                        }

                        //�����֯������Ҫ����
                        objEmployee.Memo = strOrg;
                    }
                    else
                    {
                        if (!htShopNeedAdd.ContainsKey(strWorkShop))
                        {
                            htShopNeedAdd.Add(strWorkShop, new Hashtable());
                        }

                        Hashtable htGroupNeedAdd = (Hashtable)htShopNeedAdd[strWorkShop];
                        if (!htGroupNeedAdd.ContainsKey(dr["Ա����"].ToString().Trim()))
                        {
                            htGroupNeedAdd.Add(dr["Ա����"].ToString().Trim(), dr["Ա����"].ToString().Trim());
                            htShopNeedAdd[strWorkShop] = htGroupNeedAdd;
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
                // ����
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

                    objEmployee.EmployeeName = dr["����"].ToString().Trim();
                    objEmployee.PinYinCode = Pub.GetChineseSpell(dr["����"].ToString().Trim());
                }

                //���֤����
                if (dr["���֤����"].ToString().Trim() == string.Empty && dr["Ա������"].ToString().Trim() == string.Empty)
                {
                    AddError(objErrorList, dr, "���֤���ź�Ա�����벻��ͬʱΪ��");
                    continue;
                }
                else if (dr["���֤����"].ToString().Trim() != string.Empty)
                {
                    if (dr["Ա������"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Work_NO='" + dr["Ա������"].ToString().Trim() + "' and Employee_Name='" + dr["����"].ToString().Trim() + "' and Identity_CardNo='" + dr["���֤����"].ToString().Trim() + "'");
                        if (objOldList.Count > 0)
                        {
                            isUpdate = true;
                            objEmployee.EmployeeID = objOldList[0].EmployeeID;
                        }
                        else
                        {
                            isUpdate = false;
                        }

                        if(!isUpdate)
                        {
                            IList<RailExam.Model.Employee> objNowList = objEmployeeBll.GetEmployeeByWhereClause("Work_NO='" + dr["Ա������"].ToString().Trim() + "'");
                            if (objNowList.Count > 0)
                            {
                                AddError(objErrorList, dr, "Ա��������ϵͳ�С�" + objNowList[0].OrgName + "��" + objNowList[0].EmployeeName + "����Ա������ظ�");
                                continue;
                            }

                        }


                        //Ա��������Excel�в����ظ�
                        if (htERP.ContainsKey(dr["Ա������"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "Ա��������Excel�������Ϊ" + htERP[dr["Ա������"].ToString().Trim()] + "��Ա�������ظ�");
                            continue;
                        }
                        else
                        {
                            htERP.Add(dr["Ա������"].ToString().Trim(), dr["���"].ToString().Trim());
                        }

                        objEmployee.WorkNo = dr["Ա������"].ToString().Trim();
                    }
                    else
                    {
                        IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Employee_Name='" + dr["����"].ToString().Trim() + "' and Identity_CardNo='" + dr["���֤����"].ToString().Trim() + "'");
                        if (objOldList.Count > 0)
                        {
                            isUpdate = true;
                            objEmployee.EmployeeID = objOldList[0].EmployeeID;
                        }
                        else
                        {
                            isUpdate = false;
                        }
                    }

                    if (dr["���֤����"].ToString().Trim().Length > 18)
                    {
                        AddError(objErrorList, dr, "���֤���Ų��ܳ���18λ");
                        continue;
                    }

                    //���֤������Excel�в����ظ�
                    if (htIdentityCardNo.ContainsKey(dr["���֤����"].ToString().Trim()))
                    {
                        AddError(objErrorList, dr, "���֤������Excel�������Ϊ" + htIdentityCardNo[dr["���֤����"].ToString().Trim()] + "�����֤�����ظ�");
                        continue;
                    }
                    else
                    {
                        htIdentityCardNo.Add(dr["���֤����"].ToString().Trim(), dr["���"].ToString().Trim());
                    }


                    IList<RailExam.Model.Employee> objList =
                         objEmployeeBll.GetEmployeeByWhereClause("identity_CardNo='" + dr["���֤����"].ToString().Trim() + "'");
                    if (objList.Count > 0)
                    {
                        if (isUpdate)
                        {
                            if (objList.Count > 1)
                            {
                                AddError(objErrorList, dr, "���Ա�����֤������ȫ��ͬ��Ա���Ѿ�����");
                                continue;
                            }
                        }
                        else
                        {
                            AddError(objErrorList, dr, "��Ա�����֤�����롾" + objList[0].OrgName + "���С�" + objList[0].EmployeeName + "�������֤������ȫ��ͬ");
                            continue;
                        }
                    }

                    objEmployee.IdentifyCode = dr["���֤����"].ToString().Trim();
                }
                else if (dr["���֤����"].ToString().Trim() == string.Empty)
                {
                    IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Work_NO='" + dr["Ա������"].ToString().Trim() + "' and Employee_Name='" + dr["����"].ToString().Trim() + "'");
                    if (objOldList.Count > 0)
                    {
                        isUpdate = true;
                        objEmployee.EmployeeID = objOldList[0].EmployeeID;
                    }
                    else
                    {
                        isUpdate = false;
                    }

                    //Ա��������Excel�в����ظ�
                    if (htERP.ContainsKey(dr["Ա������"].ToString().Trim()))
                    {
                        AddError(objErrorList, dr, "Ա��������Excel�������Ϊ" + htERP[dr["Ա������"].ToString().Trim()] + "��Ա�������ظ�");
                        continue;
                    }
                    else
                    {
                        htERP.Add(dr["Ա������"].ToString().Trim(), dr["���"].ToString().Trim());
                    }

                    objEmployee.WorkNo = dr["Ա������"].ToString().Trim();
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
                //����
                if (dr["����"].ToString().Trim().Length > 20)
                {
                    AddError(objErrorList, dr, "���᲻�ܳ���20λ");
                    continue;
                }
                else
                {
                    objEmployee.NativePlace = dr["����"].ToString().Trim();
                }
                //���ѧ��
                if (dr["���ѧ��"].ToString().Trim() == string.Empty)
                {
                    AddError(objErrorList, dr, "���ѧ������Ϊ��");
                    continue;
                }
                else
                {
                    if (!htEducationLevel.ContainsKey(dr["���ѧ��"].ToString().Trim()))
                    {
                        AddError(objErrorList, dr, "���ѧ����ϵͳ�в�����");
                        continue;
                    }
                    else
                    {
                        objEmployee.EducationLevelID = Convert.ToInt32(htEducationLevel[dr["���ѧ��"].ToString().Trim()]);
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
                        string strBirth = dr["��������"].ToString().Trim().Replace(".", "");
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

                        if (Convert.ToDateTime(strBirth) < Convert.ToDateTime("1900-1-1") ||
                            Convert.ToDateTime(strBirth) > Convert.ToDateTime("2000-12-31"))
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

                //���뱾��˾ʱ��
                if (dr["���뱾��˾ʱ��"].ToString().Trim() == string.Empty)
                {
                    AddError(objErrorList, dr, "���뱾��˾ʱ�䲻��Ϊ��");
                    continue;
                }
                else
                {
                    try
                    {
                        string strJoin = dr["���뱾��˾ʱ��"].ToString().Trim().Replace(".", "");
                        if (strJoin.IndexOf("-") >= 0)
                        {
                            objEmployee.WorkDate = Convert.ToDateTime(strJoin);
                        }
                        else
                        {
                            if (strJoin.Length != 8)
                            {
                                AddError(objErrorList, dr, "���뱾��˾ʱ����д����");
                                continue;
                            }
                            else
                            {
                                strJoin = strJoin.Insert(4, "-");
                                strJoin = strJoin.Insert(7, "-");
                                objEmployee.WorkDate = Convert.ToDateTime(strJoin);
                            }
                        }

                        if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1900-1-1"))
                        {
                            AddError(objErrorList, dr, "���뱾��˾ʱ����д����");
                            continue;
                        }
                    }
                    catch
                    {
                        AddError(objErrorList, dr, "���뱾��˾ʱ����д����");
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
                        string strJoin = dr["�μӹ���ʱ��"].ToString().Trim().Replace(".", "");
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

                        if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1900-1-1"))
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
                //ְ��
                if (dr["��λ����"].ToString().Trim() == string.Empty)
                {
                    AddError(objErrorList, dr, "��λ���Ʋ���Ϊ�գ�");
                    continue;
                }
                else
                {
                    IList<RailExam.Model.Post> objPost =
                        objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["��λ����"].ToString().Trim() + "'");
                    if (objPost.Count == 0)
                    {
                        if (htPostNeedAdd.ContainsKey(dr["��λ����"].ToString().Trim()))
                        {
                            objPostNewList = (ArrayList)htPostNeedAdd[dr["��λ����"].ToString().Trim()];
                            int x;
                            if (isUpdate)
                            {
                                x = objEmployeeUpdate.Count;
                            }
                            else
                            {
                                x = objEmployeeInsert.Count;
                            }
                            objPostNewList.Add(x+ "|" + isUpdate);
                            htPostNeedAdd[dr["��λ����"].ToString().Trim()] = objPostNewList;
                        }
                        else
                        {
                            objPostNewList = new ArrayList();
                            int x;
                            if(isUpdate)
                            {
                                x = objEmployeeUpdate.Count;
                            }
                            else
                            {
                                x = objEmployeeInsert.Count;
                            }
                            objPostNewList.Add(x+"|" + isUpdate);
                            htPostNeedAdd.Add(dr["��λ����"].ToString().Trim(), objPostNewList);
                        }
                    }

                    objEmployee.PostID = Convert.ToInt32(htPost[dr["��λ����"].ToString().Trim()]);
                }
                // ��dateAWARD_DATEδ��������У� ���Դ˴�ȱʧ��֤������ϵͳʱ��

                if (dr["�ڸ�"] == DBNull.Value || dr["�ڸ�"].ToString() == "" || dr["�ڸ�"].ToString() == "��" || dr["�ڸ�"].ToString() == "0")
                {
                    objEmployee.IsOnPost = false;
                }
                else if (dr["�ڸ�"] != DBNull.Value && (dr["�ڸ�"].ToString() == "��" || dr["�ڸ�"].ToString() == "1"))
                {
                    objEmployee.IsOnPost = true;
                }

                if (dr["�ڲ�"] == DBNull.Value || dr["�ڲ�"].ToString() == "" || dr["�ڲ�"].ToString() == "��" || dr["�ڲ�"].ToString() == "0")
                {
                    objEmployee.Dimission = false;
                }
                else if (dr["�ڲ�"] != DBNull.Value && (dr["�ڲ�"].ToString() == "��" || dr["�ڲ�"].ToString() == "1"))
                {
                    objEmployee.Dimission = true;
                }

                // ��ֵĬ��ֵ
                if (!isUpdate)
                {
                    //ְ������
                    objEmployee.EmployeeTypeID = 0;
                    // �ڲ�
                    objEmployee.Dimission = true;
                    // �ڸ�
                    objEmployee.IsOnPost = true;
                    // ����ҵְ������
                    objEmployee.EmployeeTransportTypeID = 0;

                    objEmployeeInsert.Add(objEmployee);
                }
                else
                {
                    objEmployeeUpdate.Add(objEmployee);
                }
            }

            // �������
            jsBlock = "<script>SetCompleted('Excel���ݼ�����'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (objErrorList.Count > 0)
            {
                // �������
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
                    objPost.ParentId = 1550;    // 649ͨ��-����, 1550 ����-����
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
                        if (strPost[1] == "false" || strPost[1] == "False")
                        {
                            objEmployeeInsert[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
                        else
                        {
                            objEmployeeUpdate[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ���ְ��','" + ((double)(count * 100) / (double)(htPostNeedAdd.Count)).ToString("0.00") + "'); </script>";
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

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ���Ա����Ϣ','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }


                for (int i = 0; i < objEmployeeUpdate.Count; i++)
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
            objError.WorkNo = dr["Ա������"].ToString().Trim();
            objError.EmployeeName = dr["����"].ToString().Trim();
            objError.Sex = dr["�Ա�"].ToString().Trim();
            objError.OrgPath = dr["����"].ToString().Trim();
            objError.PostPath = dr["��λ����"].ToString().Trim();
            objError.ErrorReason = strError;
            objError.OrgName = dr["��λ"].ToString().Trim();
            objError.GroupName = dr["Ա����"].ToString().Trim();
            objError.IdentifyCode = dr["���֤����"].ToString().Trim();
            objError.NativePlace = dr["����"].ToString().Trim();
            objError.PoliticalStatus = dr["������ò"].ToString().Trim();
            objError.EducationLevel = dr["���ѧ��"].ToString().Trim();
            objError.Birthday = dr["��������"].ToString().Trim();
            objError.BeginDate = dr["���뱾��˾ʱ��"].ToString().Trim();
            objError.WorkDate = dr["�μӹ���ʱ��"].ToString().Trim();

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
        /// ���ѧ��
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
    }
}
