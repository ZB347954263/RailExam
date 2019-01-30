using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

namespace RailExamWebApp.Item
{
    public partial class ImportExcel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
                    return;
                }
                HfChapterId.Value = Request.QueryString.Get("ChapterID");
                HfBookId.Value = Request.QueryString.Get("BookID");

                if (HfChapterId.Value != "-1" && !string.IsNullOrEmpty(HfChapterId.Value))
                {
                    ReadExcelByBook();
                }
                else
                {
                    ReadExcelByBook();
                }
            }
        }


        private void ReadExcel()
        {
            //���������ݵ�����Դ
            DataTable dt = new DataTable();
            DataColumn dcnew1 = dt.Columns.Add("id");
            DataColumn dcnew2 = dt.Columns.Add("ItemContent");
            DataColumn dcnew3 = dt.Columns.Add("ItemType");
            DataColumn dcnew4 = dt.Columns.Add("AnswerCount");
            DataColumn dcnew5 = dt.Columns.Add("OverDate");
            DataColumn dcnew6 = dt.Columns.Add("Answer");
            DataColumn dcnew7 = dt.Columns.Add("ErrorReason");

            string mode = Request.QueryString.Get("Mode");
            string strFileName = Server.UrlDecode(Request.QueryString.Get("FileName"));
            string jsBlock;
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
            bool isClose = false;
            string strMessage;


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

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�̲�����"]], objSheet.Cells[i, htCol["�̲�����"]]));
                    newRow["�̲�����"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��"]], objSheet.Cells[i, htCol["��"]]));
                    newRow["��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��"]], objSheet.Cells[i, htCol["��"]]));
                    newRow["��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ĸ����"]], objSheet.Cells[i, htCol["ĸ����"]]));
                    newRow["ĸ����"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��������"]], objSheet.Cells[i, htCol["��������"]]));
                    newRow["��������"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��������"]], objSheet.Cells[i, htCol["��������"]]));
                    newRow["��������"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ѡ����Ŀ"]], objSheet.Cells[i, htCol["ѡ����Ŀ"]]));
                    newRow["ѡ����Ŀ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����ʱ��"]], objSheet.Cells[i, htCol["����ʱ��"]]));
                    if (range.Value2 != null)
                    {
                        try
                        {
                            newRow["����ʱ��"] = DateTime.FromOADate(Convert.ToInt32(range.Value2)).ToString("d");
                        }
                        catch
                        {
                            newRow["����ʱ��"] = range.Value2;
                        }
                    }
                    else
                    {
                        newRow["����ʱ��"] = "";
                    }

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�ؼ���"]], objSheet.Cells[i, htCol["�ؼ���"]]));
                    newRow["�ؼ���"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��ȷѡ��"]], objSheet.Cells[i, htCol["��ȷѡ��"]]));
                    newRow["��ȷѡ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�����ڿ���"]], objSheet.Cells[i, htCol["�����ڿ���"]]));
                    newRow["�����ڿ���"] = range.Value2;

                    int start = 10;

                    //if (!_isWuhan)
                    //{
                    //    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�̲�����"]], objSheet.Cells[i, htCol["�̲�����"]]));
                    //    newRow["�̲�����"] = range.Value2;

                    //    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�����Ѷ�"]], objSheet.Cells[i, htCol["�����Ѷ�"]]));
                    //    newRow["�����Ѷ�"] = range.Value2;
                    //    start = 11;
                    //}

                    for (int j = start; j < htCol.Count; j++)
                    {
                        string str = intToChar(j - start).ToString();
                        range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol[str]], objSheet.Cells[i, htCol[str]]));
                        newRow[str] = range.Value2;
                    }

                    dtItem.Rows.Add(newRow);

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

            DataColumn dc1 = ds.Tables[0].Columns.Add("ItemType");
            int index = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["�̲�����"].ToString().Trim() != "")
                {
                    AddError(dt, dr, "�����½�����ʱ���̲����Ʊ���Ϊ��");
                    continue;
                }

                // ����½�ID��Ϊ�գ����ºͽڱ���Ϊ��
                if (dr["��"].ToString().Trim() != "")
                {
                    AddError(dt, dr, "�����½�����ʱ���±���Ϊ��");
                    continue;
                }

                if (dr["��"].ToString().Trim() != "")
                {
                    AddError(dt, dr, "�����½�����ʱ���ڱ���Ϊ��");
                    continue;
                }

                //�������ݲ���Ϊ��
                if (dr["��������"].ToString() == "")
                {
                    AddError(dt, dr, "�������ݲ���Ϊ��");
                    continue;
                }

                //�ж��������͵���ȷ��
                if (dr["��������"].ToString() == PrjPub.ITEMTYPE_SINGLECHOOSE.ToString())
                {
                    dr["ItemType"] = "��ѡ";
                }
                else if (dr["��������"].ToString() == PrjPub.ITEMTYPE_MULTICHOOSE.ToString())
                {
                    dr["ItemType"] = "��ѡ";
                }
                else if (dr["��������"].ToString() == PrjPub.ITEMTYPE_JUDGE.ToString())
                {
                    dr["ItemType"] = "�ж�";
                }
                else if (dr["��������"].ToString() == PrjPub.ITEMTYPE_FILLBLANK.ToString())
                {
                    dr["ItemType"] = "���";
                }
                else if (dr["��������"].ToString() == PrjPub.ITEMTYPE_QUESTION.ToString())
                {
                    dr["ItemType"] = "���";
                }
                else if (dr["��������"].ToString() == PrjPub.ITEMTYPE_DISCUSS.ToString())
                {
                    dr["ItemType"] = "����";
                }
                else
                {
                    AddError(dt, dr, "�������ʹ���");
                    continue;
                }

                //�������ʱ��
                if (dr["����ʱ��"].ToString() == "")
                {
                    dr["����ʱ��"] = "2050-01-01";
                }
                else
                {
                    try
                    {
                        DateTime date = Convert.ToDateTime(dr["����ʱ��"].ToString());
                    }
                    catch
                    {
                        AddError(dt, dr, "����ʱ���ʽ����ȷ");
                        continue;
                    }
                }

                //����ѡ����Ŀ
                int itemCount = 0;
                if (dr["ѡ����Ŀ"].ToString() == "")
                {
                    //�����������Ϊ��ѡ����ѡ���жϣ�ѡ����Ŀ����Ϊ��
                    if (dr["ItemType"].ToString() == "��ѡ" || dr["ItemType"].ToString() == "��ѡ")
                    {
                        AddError(dt, dr, dr["ItemType"] + "��ѡ����Ŀ����Ϊ��");
                        continue;
                    }
                    else
                    {
                        dr["ѡ����Ŀ"] = "2";
                    }
                }
                else
                {
                    try
                    {
                        itemCount = Math.Abs(Convert.ToInt32(dr["ѡ����Ŀ"].ToString()));
                    }
                    catch
                    {
                        AddError(dt, dr, "ѡ����Ŀ����Ϊ������");
                        continue;
                    }
                }

                //�ж�ѡ����Ŀ�Ƿ�������ѡ��һ��
                for (int i = 0; i < itemCount; i++)
                {
                    string str = intToChar(i).ToString();
                    try
                    {
                        //ѡ���Ϊ��
                        if (dr[str].ToString() == "")
                        {
                            AddError(dt, dr, "ѡ��" + str + "����Ϊ��");
                            continue;
                        }
                    }
                    catch
                    {
                        AddError(dt, dr, "ѡ��" + str + "������");
                        continue;
                    }
                }

                if (mode == "0")
                {
                    if (dr["ItemType"].ToString() == "��ѡ" || dr["ItemType"].ToString() == "��ѡ" || dr["ItemType"].ToString() == "�ж�")
                    {
                        //����������Ϊ��ѡ����ѡ���ж�ʱ����ȷѡ���Ϊ��
                        if (dr["��ȷѡ��"].ToString() == "")
                        {
                            AddError(dt, dr, "��ȷѡ���Ϊ��");
                            continue;
                        }
                        //����������Ϊ��ѡ����ѡ���жϣ�������ȷѡ�Ϊ��ʱ�������ȷѡ����д����ȷ��
                        else
                        {
                            if (itemCount != 0)
                            {
                                int n = 0;
                                if (dr["ItemType"].ToString() == "��ѡ" || dr["ItemType"].ToString() == "�ж�")
                                {
                                    n = 0;
                                    for (int i = 0; i < itemCount; i++)
                                    {
                                        string str = intToChar(i).ToString();
                                        if (str == dr["��ȷѡ��"].ToString().ToUpper())
                                        {
                                            n = n + 1;
                                        }
                                    }

                                    if (n == 0)
                                    {
                                        AddError(dt, dr, "��ȷѡ����д����");
                                        continue;
                                    }
                                }
                                else
                                {
                                    char[] chr = dr["��ȷѡ��"].ToString().ToCharArray();
                                    bool flag = true;
                                    for (int i = 0; i < chr.Length; i++)
                                    {
                                        n = 0;
                                        for (int j = 0; j < itemCount; j++)
                                        {
                                            string str = intToChar(j).ToString();
                                            if (str == chr[i].ToString().ToUpper())
                                            {
                                                n = n + 1;
                                            }
                                        }

                                        if (n == 0)
                                        {
                                            AddError(dt, dr, "��ȷѡ����д����");
                                            flag = false;
                                            break;
                                        }
                                    }

                                    if (!flag)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                }

                //if (!_isWuhan)
                //{
                //    if (dr["�����Ѷ�"].ToString() == "")
                //    {
                //        AddError(dt, dr, "�����ѶȲ���Ϊ��");
                //        continue;
                //    }
                //    else
                //    {
                //        try
                //        {
                //            int diff = Convert.ToInt32(dr["�����Ѷ�"].ToString());
                //            if (diff < 1 || diff > 5)
                //            {
                //                AddError(dt, dr, "�����Ѷ�ֻ����1��5֮��");
                //                continue;
                //            }
                //        }
                //        catch
                //        {
                //            AddError(dt, dr, "�����Ѷȱ���Ϊ����");
                //            continue;
                //        }
                //    }
                //}

                if (dr["ItemType"].ToString() == "�ж�")
                {
                    if (dr["A"].ToString() != "��" || dr["B"].ToString() != "��")
                    {
                        AddError(dt, dr, "�ж���ѡ��A����Ϊ�ԣ�ѡ��B����Ϊ��");
                        continue;
                    }
                }

                if (dr["ItemType"].ToString() == "���")
                {
                    if (dr["��ȷѡ��"].ToString() == "")
                    {
                        AddError(dt, dr, "�������ȷѡ���ȷ�𰸣�����Ϊ��");
                        continue;
                    }
                }

                if (dr["ItemType"].ToString() == "���" || dr["ItemType"].ToString() == "����")
                {
                    if (dr["��ȷѡ��"].ToString() == "")
                    {
                        AddError(dt, dr, "��������������ȷѡ���ȷ�𰸣�����Ϊ��");
                        continue;
                    }
                    if (dr["��ȷѡ��"].ToString().Length > 2000)
                    {
                        AddError(dt, dr, "��������������ȷѡ���ȷ�𰸣����ܳ���2000���ַ�");
                        continue;
                    }
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڼ��Excel����','" + ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                index = index + 1;
            }

            // �������
            jsBlock = "<script>SetCompleted('Excel���ݼ�����'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            #endregion

            if (dt.Rows.Count > 0)
            {
                Session["table"] = dt;
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='refresh|����Excel�ļ�����',window.close();</script>");
                return;
            }
            else
            {
                dt.Clear();
                Session["table"] = dt;
            }

            #region ��������

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('��׼����������','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            try
            {
                ItemBLL objBll = new ItemBLL();
                IList<RailExam.Model.Item> objList = new System.Collections.Generic.List<RailExam.Model.Item>();
                int m = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RailExam.Model.Item item = new RailExam.Model.Item();
                    item.BookId = Convert.ToInt32(HfBookId.Value);
                    item.ChapterId = Convert.ToInt32(HfChapterId.Value);
                    item.OrganizationId = PrjPub.CurrentLoginUser.StationOrgID;
                    item.TypeId = Convert.ToInt32(dr["��������"].ToString());
                    item.CompleteTime = 60;
                    //if (!_isWuhan)
                    //{
                    //    item.DifficultyId = Convert.ToInt32(dr["�����Ѷ�"].ToString());
                    //}
                    //else
                    //{
                    //    item.DifficultyId = 3;
                    //}
                    item.Score = 4;
                    item.Content = dr["��������"].ToString();
                    item.AnswerCount = Convert.ToInt32(dr["ѡ����Ŀ"].ToString());

                    if (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)
                    {
                        string strAnswer = "";
                        for (int i = 0; i < item.AnswerCount; i++)
                        {
                            string str = intToChar(i).ToString();
                            if (strAnswer == "")
                            {
                                strAnswer = dr[str].ToString();
                            }
                            else
                            {
                                strAnswer = strAnswer + "|" + dr[str];
                            }
                        }

                        item.SelectAnswer = strAnswer;
                    }
                    else if (item.TypeId == PrjPub.ITEMTYPE_JUDGE)
                    {
                        item.SelectAnswer = "��|��";
                    }
                    else
                    {
                        item.SelectAnswer = "";
                    }

                    if (mode == "0")
                    {

                        if (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || item.TypeId == PrjPub.ITEMTYPE_JUDGE)
                        {
                            item.StandardAnswer = charToInt(Convert.ToChar(dr["��ȷѡ��"].ToString().Trim().ToUpper())).ToString();
                        }
                        else if (item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)
                        {
                            string strStandard = "";
                            char[] chr = dr["��ȷѡ��"].ToString().Trim().ToUpper().ToCharArray();
                            for (int i = 0; i < chr.Length; i++)
                            {
                                if (strStandard == "")
                                {
                                    strStandard = charToInt(chr[i]).ToString();
                                }
                                else
                                {
                                    strStandard = strStandard + "|" + charToInt(chr[i]).ToString();
                                }
                            }

                            item.StandardAnswer = strStandard;
                        }
                        else
                        {
                            item.StandardAnswer = "";
                        }
                    }


                    if (item.TypeId == PrjPub.ITEMTYPE_FILLBLANK || item.TypeId == PrjPub.ITEMTYPE_QUESTION || item.TypeId == PrjPub.ITEMTYPE_DISCUSS)
                    {
                        item.StandardAnswer = dr["��ȷѡ��"].ToString().Trim();
                    }

                    item.Description = "";
                    item.OutDateDate = Convert.ToDateTime(dr["����ʱ��"].ToString());
                    item.UsedCount = 0;
                    item.StatusId = 1;
                    item.CreatePerson = PrjPub.CurrentLoginUser.EmployeeName;
                    item.CreateTime = DateTime.Today;
                    item.UsageId = 0;
                    item.Memo = "";
                    item.HasPicture = 0;
                    item.KeyWord = dr["�ؼ���"].ToString();
                    item.UsageId = dr["�����ڿ���"].ToString() == "" || dr["�����ڿ���"].ToString() == "0" ? 0 : 1;

                    objList.Add(item);
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ�������','" + ((double)(m * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    m = m + 1;
                }

                objBll.AddItem(objList);

                jsBlock = "<script>SetCompleted('���⵼�����'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                strMessage = "����ɹ���";
            }
            catch (Exception ex)
            {
                strMessage = "����ʧ��!";
            }

            if (File.Exists(strPath))
                File.Delete(strPath);
            Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
            #endregion
        }

        private void ReadExcelByBook()
        {
            //���������ݵ�����Դ
            DataTable dt = new DataTable();
            DataColumn dcnew1 = dt.Columns.Add("id");
            DataColumn dcnew2 = dt.Columns.Add("ItemContent");
            DataColumn dcnew3 = dt.Columns.Add("ItemType");
            DataColumn dcnew4 = dt.Columns.Add("AnswerCount");
            DataColumn dcnew5 = dt.Columns.Add("OverDate");
            DataColumn dcnew6 = dt.Columns.Add("Answer");
            DataColumn dcnew7 = dt.Columns.Add("ErrorReason");

            string strFileName = Server.UrlDecode(Request.QueryString.Get("FileName"));
            string jsBlock;
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
            bool isClose = false;
            string strMessage;


            Excel.Application objApp = null;
            Excel._Workbook objBook = null;
            Excel.Workbooks objBooks = null;
            Excel.Sheets objSheets = null;
            Excel._Worksheet objSheet = null;
            Excel.Range range = null;
            DataSet ds = new DataSet();
            string errorcol = string.Empty;

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

                    errorcol = "���";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���"]], objSheet.Cells[i, htCol["���"]]));
                    newRow["���"] = range.Value2;

                    errorcol = "�̲�����";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�̲�����"]], objSheet.Cells[i, htCol["�̲�����"]]));
                    newRow["�̲�����"] = range.Value2;

                    errorcol = "��";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��"]], objSheet.Cells[i, htCol["��"]]));
                    newRow["��"] = range.Value2;

                    errorcol = "��";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��"]], objSheet.Cells[i, htCol["��"]]));
                    newRow["��"] = range.Value2;

                    //errorcol = "ĸ����";
                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ĸ����"]], objSheet.Cells[i, htCol["ĸ����"]]));
                    //newRow["ĸ����"] = range.Value2;

                    errorcol = "��������";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��������"]], objSheet.Cells[i, htCol["��������"]]));
                    newRow["��������"] = range.Value2;

                    errorcol = "��������";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��������"]], objSheet.Cells[i, htCol["��������"]]));
                    newRow["��������"] = range.Value2;

                    errorcol = "ѡ����Ŀ";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["ѡ����Ŀ"]], objSheet.Cells[i, htCol["ѡ����Ŀ"]]));
                    newRow["ѡ����Ŀ"] = range.Value2;

                    errorcol = "����ʱ��";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����ʱ��"]], objSheet.Cells[i, htCol["����ʱ��"]]));
                    if (range.Value2 != null)
                    {
                        try
                        {
                            newRow["����ʱ��"] = DateTime.FromOADate(Convert.ToInt32(range.Value2)).ToString("d");
                        }
                        catch
                        {
                            newRow["����ʱ��"] = range.Value2;
                        }
                    }
                    else
                    {
                        newRow["����ʱ��"] = "";
                    }

                    errorcol = "�ؼ���";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�ؼ���"]], objSheet.Cells[i, htCol["�ؼ���"]]));
                    newRow["�ؼ���"] = range.Value2;

                    errorcol = "��ȷѡ��";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��ȷѡ��"]], objSheet.Cells[i, htCol["��ȷѡ��"]]));
                    newRow["��ȷѡ��"] = range.Value2;

                    errorcol = "�����ڿ���";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["�����ڿ���"]], objSheet.Cells[i, htCol["�����ڿ���"]]));
                    newRow["�����ڿ���"] = range.Value2;

                    int start = 11;

                    for (int j = start; j < htCol.Count; j++)
                    {
                        string str = intToChar(j - start).ToString();
                        errorcol = str;
                        range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol[str]], objSheet.Cells[i, htCol[str]]));
                        newRow[str] = range.Value2;
                    }

                    dtItem.Rows.Add(newRow);

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

                string errormeg = string.Empty;
                if (!string.IsNullOrEmpty(errorcol))
                {
                    errormeg = "�С�" + errorcol + "�������ڻ����";
                }
                Response.Write("<script>window.returnValue='����Excel�ļ���ʽ:" + errormeg + "',window.close();</script>");
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

            DataColumn dc1 = ds.Tables[0].Columns.Add("ItemType");
            DataColumn dc2 = ds.Tables[0].Columns.Add("BookID");
            int index = 1;
            Hashtable htBook = GetBookName();

            string strBookName = "";
            if (!string.IsNullOrEmpty(HfBookId.Value) && HfBookId.Value != "-1")
            {
                BookBLL objbookbll = new BookBLL();
                RailExam.Model.Book objbook = objbookbll.GetBook(Convert.ToInt32(HfBookId.Value));
                strBookName = objbook.bookName;
            }

            int itemindex = 0;
            int nowindex = 0;
            int itemtotalcount = 0;
            Hashtable ht = new Hashtable();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (strBookName == "")
                {
                    if (dr["�̲�����"].ToString().Trim() == "")
                    {
                        AddError(dt, dr, "δѡ���̲ĵ���̲�����ʱ���̲����Ʋ���Ϊ��");
                        continue;
                    }
                    else
                    {
                        if (!htBook.ContainsKey(dr["�̲�����"].ToString().Trim()))
                        {
                            AddError(dt, dr, "δѡ���̲ĵ���̲�����ʱ���̲���ϵͳ�в�����");
                            continue;
                        }
                        else
                        {
                            if (htBook[dr["�̲�����"].ToString().Trim()].ToString().Split('|')[1] !=
                                PrjPub.CurrentLoginUser.StationOrgID.ToString() && PrjPub.CurrentLoginUser.SuitRange != 1)
                            {
                                AddError(dt, dr, "��û�е���ý̲������Ȩ��");
                                continue;
                            }
                            dr["BookID"] = htBook[dr["�̲�����"].ToString().Trim()].ToString().Split('|')[0];
                        }
                    }
                }
                else
                {
                    if (dr["�̲�����"].ToString().Trim() != strBookName)
                    {
                        AddError(dt, dr, "ѡ���̲ĵ���̲�����ʱ���̲����Ʊ�����ѡ���̲�һ��");
                        continue;
                    }
                    else
                    {
                        dr["BookID"] = HfBookId.Value;
                    }

                    if (dr["�̲�����"].ToString().Trim() == "")
                    {
                        AddError(dt, dr, "����̲�����ʱ���̲����Ʋ���Ϊ��");
                        continue;
                    }
                    else
                    {
                        if (!htBook.ContainsKey(dr["�̲�����"].ToString().Trim()))
                        {
                            AddError(dt, dr, "�̲���ϵͳ�в�����");
                            continue;
                        }
                        else
                        {
                            dr["BookID"] = htBook[dr["�̲�����"].ToString().Trim()].ToString().Split('|')[0];
                        }
                    }
                }

                Hashtable hfChapterName = GetChapterName(Convert.ToInt32(dr["BookID"].ToString()));
                // ����½�ID��Ϊ�գ����ºͽڱ���Ϊ��
                if (dr["��"].ToString().Trim() == "")
                {
                    AddError(dt, dr, "����̲�����ʱ���²���Ϊ��");
                    continue;
                }
                else
                {
                    if (!hfChapterName.ContainsKey(dr["��"].ToString().Trim()))
                    {
                        AddError(dt, dr, "����̲�����ʱ�����ڵ�ǰ�̲��²�����");
                        continue;
                    }
                }

                if (dr["��"].ToString().Trim() != "")
                {
                    Hashtable hfChapterNameByChapterID =
                        GetChapterNameByChapterID(Convert.ToInt32(hfChapterName[dr["��"].ToString().Trim()]));
                    if (!hfChapterNameByChapterID.ContainsKey(dr["��"].ToString().Trim()))
                    {
                        AddError(dt, dr, "����̲�����ʱ�����ڵ�ǰ�̲ĵġ�" + dr["��"].ToString().Trim() + "���²�����");
                        continue;
                    }
                }

                //if (dr["ĸ����"].ToString().Trim() != "")
                //{
                //    if (dr["ĸ����"].ToString().Trim().Length > 50)
                //    {
                //        AddError(dt, dr, "ĸ���Ų��ܳ���50���ַ�");
                //        continue;
                //    }
                //}

                //if (dr["ĸ����"].ToString().Trim() != "")
                //{
                //    Hashtable hfChapterNameByChapterID =
                //        GetMotherChapterNameByBookID(Convert.ToInt32(dr["BookID"].ToString()));
                //    if (!hfChapterNameByChapterID.ContainsKey(dr["ĸ����"].ToString().Trim()))
                //    {
                //        AddError(dt, dr, "����̲�����ʱ��ĸ���š�" + dr["ĸ����"].ToString().Trim() + "���ڵ�ǰ�̲��в�����");
                //        continue;
                //    }
                //}

                //�������ݲ���Ϊ��
                if (dr["��������"].ToString().Trim() == "")
                {
                    AddError(dt, dr, "�������ݲ���Ϊ��");
                    continue;
                }

                //�ж��������͵���ȷ��
                if (dr["��������"].ToString().Trim() == PrjPub.ITEMTYPE_SINGLECHOOSE.ToString())
                {
                    dr["ItemType"] = "��ѡ";
                }
                else if (dr["��������"].ToString().Trim() == PrjPub.ITEMTYPE_MULTICHOOSE.ToString())
                {
                    dr["ItemType"] = "��ѡ";
                }
                else if (dr["��������"].ToString().Trim() == PrjPub.ITEMTYPE_JUDGE.ToString())
                {
                    dr["ItemType"] = "�ж�";
                }
                else if (dr["��������"].ToString().Trim() == PrjPub.ITEMTYPE_FILLBLANK.ToString())
                {
                    dr["ItemType"] = "�������";
                }
                else if (dr["��������"].ToString().Trim() == PrjPub.ITEMTYPE_FILLBLANKDETAIL.ToString())
                {
                    dr["ItemType"] = "�����������";
                }
                else if (dr["��������"].ToString().Trim() == PrjPub.ITEMTYPE_QUESTION.ToString())
                {
                    dr["ItemType"] = "���";
                }
                else if (dr["��������"].ToString().Trim() == PrjPub.ITEMTYPE_DISCUSS.ToString())
                {
                    dr["ItemType"] = "���";
                }
                else if (dr["��������"].ToString().Trim() == PrjPub.ITEMTYPE_LUNSHU.ToString())
                {
                    dr["ItemType"] = "����";
                }
                else
                {
                    AddError(dt, dr, "�������ʹ���");
                    continue;
                }

                //�������ʱ��
                if (dr["����ʱ��"].ToString().Trim() == "")
                {
                    dr["����ʱ��"] = "2050-01-01";
                }
                else
                {
                    try
                    {
                        DateTime date = Convert.ToDateTime(dr["����ʱ��"].ToString().Trim());
                    }
                    catch
                    {
                        AddError(dt, dr, "����ʱ���ʽ����ȷ");
                        continue;
                    }
                }

                //����ѡ����Ŀ
                int itemCount = 0;
                if (dr["ѡ����Ŀ"].ToString().Trim() == "")
                {
                    //�����������Ϊ��ѡ����ѡ���жϣ�ѡ����Ŀ����Ϊ��
                    if (dr["ItemType"].ToString() == "��ѡ" || dr["ItemType"].ToString() == "��ѡ"
                        || dr["ItemType"].ToString() == "�������"
                        || dr["ItemType"].ToString() == "�����������")
                    {
                        AddError(dt, dr, dr["ItemType"] + "��ѡ����Ŀ����Ϊ��");
                        continue;
                    }
                    else
                    {
                        dr["ѡ����Ŀ"] = "2";
                    }
                }
                else
                {
                    try
                    {
                        itemCount = Math.Abs(Convert.ToInt32(dr["ѡ����Ŀ"].ToString()));
                    }
                    catch
                    {
                        AddError(dt, dr, "ѡ����Ŀ����Ϊ������");
                        continue;
                    }
                }

                if (dr["ItemType"].ToString() != "�������")
                {
                    //�ж�ѡ����Ŀ�Ƿ�������ѡ��һ��
                    for (int i = 0; i < itemCount; i++)
                    {
                        string str = intToChar(i).ToString();
                        try
                        {
                            //ѡ���Ϊ��
                            if (dr[str].ToString() == "")
                            {
                                AddError(dt, dr, "ѡ��" + str + "����Ϊ��");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(dt, dr, "ѡ��" + str + "������");
                            continue;
                        }
                    }
                }


                if (dr["ItemType"].ToString() == "��ѡ" || dr["ItemType"].ToString() == "��ѡ" || dr["ItemType"].ToString() == "�ж�" || dr["ItemType"].ToString() == "�����������")
                {
                    //����������Ϊ��ѡ����ѡ���ж�ʱ����ȷѡ���Ϊ��
                    if (dr["��ȷѡ��"].ToString().Trim() == "")
                    {
                        AddError(dt, dr, "��ȷѡ���Ϊ��");
                        continue;
                    }
                    //����������Ϊ��ѡ����ѡ���жϣ�������ȷѡ�Ϊ��ʱ�������ȷѡ����д����ȷ��
                    else
                    {
                        if (itemCount != 0)
                        {
                            int n = 0;
                            if (dr["ItemType"].ToString() == "��ѡ" || dr["ItemType"].ToString() == "�ж�" || dr["ItemType"].ToString() == "�����������")
                            {
                                n = 0;
                                for (int i = 0; i < itemCount; i++)
                                {
                                    string str = intToChar(i).ToString();
                                    if (str == dr["��ȷѡ��"].ToString().Trim().ToUpper())
                                    {
                                        n = n + 1;
                                    }
                                }

                                if (n == 0)
                                {
                                    AddError(dt, dr, "��ȷѡ����д����");
                                    continue;
                                }
                            }
                            else
                            {
                                char[] chr = dr["��ȷѡ��"].ToString().Trim().ToCharArray();
                                bool flag = true;
                                for (int i = 0; i < chr.Length; i++)
                                {
                                    n = 0;
                                    for (int j = 0; j < itemCount; j++)
                                    {
                                        string str = intToChar(j).ToString();
                                        if (str == chr[i].ToString().ToUpper())
                                        {
                                            n = n + 1;
                                        }
                                    }

                                    if (n == 0)
                                    {
                                        AddError(dt, dr, "��ȷѡ����д����");
                                        flag = false;
                                        break;
                                    }
                                }

                                if (!flag)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                //if (dr["�����Ѷ�"].ToString() == "")
                //{
                //    AddError(dt, dr, "�����ѶȲ���Ϊ��");
                //    continue;
                //}
                //else
                //{
                //    try
                //    {
                //        int diff = Convert.ToInt32(dr["�����Ѷ�"].ToString());
                //        if (diff < 1 || diff > 5)
                //        {
                //            AddError(dt, dr, "�����Ѷ�ֻ����1��5֮��");
                //            continue;
                //        }
                //    }
                //    catch
                //    {
                //        AddError(dt, dr, "�����Ѷȱ���Ϊ����");
                //        continue;
                //    }
                //}

                if (dr["ItemType"].ToString() == "�ж�")
                {
                    if (dr["A"].ToString().Trim() != "��" || dr["B"].ToString().Trim() != "��")
                    {
                        AddError(dt, dr, "�ж���ѡ��A����Ϊ�ԣ�ѡ��B����Ϊ��");
                        continue;
                    }
                }


                //if (dr["ItemType"].ToString() == "�����������")
                //{
                //    if (dr["��ȷѡ��"].ToString() == "")
                //    {
                //        AddError(dt, dr, "�������ȷѡ���ȷ�𰸣�����Ϊ��");
                //        continue;
                //    }
                //}

                if (dr["ItemType"].ToString() == "���" || dr["ItemType"].ToString() == "����" || dr["ItemType"].ToString() == "���")
                {
                    if (dr["��ȷѡ��"].ToString().Trim() == "")
                    {
                        AddError(dt, dr, "��ա���������������ȷѡ���ȷ�𰸣�����Ϊ��");
                        continue;
                    }
                    if (dr["��ȷѡ��"].ToString().Trim().Length > 2000)
                    {
                        AddError(dt, dr, "��ա���������������ȷѡ���ȷ�𰸣����ܳ���2000���ַ�");
                        continue;
                    }
                }

                if (index == nowindex + 1 && dr["ItemType"].ToString() != "�����������" && nowindex != 0)
                {
                    AddError(dt, ds.Tables[0].Rows[nowindex - 1], "����û���ۺ�ѡ����������ۺ�ѡ����");
                }

                if (dr["ItemType"].ToString() == "�������")
                {
                    if (nowindex != 0)
                    {
                        ht.Add(nowindex, itemtotalcount + "|" + itemindex);
                    }
                    nowindex = index;
                    itemindex = 0;
                    itemtotalcount = Convert.ToInt32(dr["ѡ����Ŀ"]);
                }
                else
                {
                    if (dr["ItemType"].ToString() != "�����������")
                    {
                        if (nowindex != 0)
                        {
                            ht.Add(nowindex, itemtotalcount + "|" + itemindex);
                        }
                        nowindex = 0;
                    }
                    else
                    {
                        itemindex++;
                    }
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڼ��Excel����','" + ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                index = index + 1;
            }

            if (nowindex != 0)
            {
                ht.Add(nowindex, itemtotalcount + "|" + itemindex);
            }

            foreach (DictionaryEntry dictionaryEntry in ht)
            {
                string[] str = dictionaryEntry.Value.ToString().Split('|');

                if (Convert.ToInt32(str[0]) != Convert.ToInt32(str[1]))
                {
                    AddError(dt, ds.Tables[0].Rows[Convert.ToInt32(dictionaryEntry.Key) - 1], "�ۺ�ѡ������������������Excel��ʵ��������������");
                }
            }

            // �������
            jsBlock = "<script>SetCompleted('Excel���ݼ�����'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            #endregion

            if (dt.Rows.Count > 0)
            {
                Session["table"] = dt;
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='refresh|����Excel�ļ�����',window.close();</script>");
                return;
            }
            else
            {
                dt.Clear();
                Session["table"] = dt;
            }

            #region ��������

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('��׼����������','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            try
            {
                ItemBLL objBll = new ItemBLL();
                IList<RailExam.Model.Item> objList = new System.Collections.Generic.List<RailExam.Model.Item>();
                int m = 1;
                itemindex = 1;
                int count = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RailExam.Model.Item item = new RailExam.Model.Item();
                    item.BookId = Convert.ToInt32(dr["BookID"].ToString());

                    #region ԭĸ��
                    /*
                    if (dr["ĸ����"].ToString().Trim() != "")
                    {
                        Hashtable hfMother = GetMotherChapterNameByBookID(item.BookId);
                        item.ChapterId = Convert.ToInt32(hfMother[dr["ĸ����"].ToString().Trim()]);
                    }
                    else
                    {
                        Hashtable hfChapterName = GetChapterName(item.BookId);
                        if (dr["��"].ToString().Trim() == "")
                        {
                            item.ChapterId = Convert.ToInt32(hfChapterName[dr["��"].ToString().Trim()]);
                        }
                        else
                        {
                            Hashtable hf =
                                GetChapterNameByChapterID(Convert.ToInt32(hfChapterName[dr["��"].ToString().Trim()]));
                            item.ChapterId = Convert.ToInt32(hf[dr["��"].ToString().Trim()]);
                        }
                    }*/
                    #endregion

                    Hashtable hfChapterName = GetChapterName(item.BookId);
                    if (dr["��"].ToString().Trim() == "")
                    {
                        item.ChapterId = Convert.ToInt32(hfChapterName[dr["��"].ToString().Trim()]);
                    }
                    else
                    {
                        Hashtable hf =
                            GetChapterNameByChapterID(Convert.ToInt32(hfChapterName[dr["��"].ToString().Trim()]));
                        item.ChapterId = Convert.ToInt32(hf[dr["��"].ToString().Trim()]);
                    }

                    item.OrganizationId = PrjPub.CurrentLoginUser.StationOrgID;
                    item.TypeId = Convert.ToInt32(dr["��������"].ToString());
                    item.CompleteTime = 60;
                    item.DifficultyId = 3;
                    item.Score = 4;
                    item.Content = dr["��������"].ToString();
                    item.AnswerCount = Convert.ToInt32(dr["ѡ����Ŀ"].ToString());

                    if (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE || item.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                    {
                        string strAnswer = "";
                        for (int i = 0; i < item.AnswerCount; i++)
                        {
                            string str = intToChar(i).ToString();
                            if (strAnswer == "")
                            {
                                strAnswer = dr[str].ToString();
                            }
                            else
                            {
                                strAnswer = strAnswer + "|" + dr[str];
                            }
                        }

                        item.SelectAnswer = strAnswer;
                    }
                    else if (item.TypeId == PrjPub.ITEMTYPE_JUDGE)
                    {
                        item.SelectAnswer = "��|��";
                    }
                    else
                    {
                        item.SelectAnswer = "";
                    }

                    if (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || item.TypeId == PrjPub.ITEMTYPE_JUDGE || item.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                    {
                        item.StandardAnswer = charToInt(Convert.ToChar(dr["��ȷѡ��"].ToString().Trim().ToUpper())).ToString();
                    }
                    else if (item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)
                    {
                        string strStandard = "";
                        char[] chr = dr["��ȷѡ��"].ToString().Trim().ToUpper().ToCharArray();
                        for (int i = 0; i < chr.Length; i++)
                        {
                            if (strStandard == "")
                            {
                                strStandard = charToInt(chr[i]).ToString();
                            }
                            else
                            {
                                strStandard = strStandard + "|" + charToInt(chr[i]).ToString();
                            }
                        }

                        item.StandardAnswer = strStandard;
                    }
                    else
                    {
                        item.StandardAnswer = "";
                    }


                    if (item.TypeId == PrjPub.ITEMTYPE_QUESTION || item.TypeId == PrjPub.ITEMTYPE_DISCUSS || item.TypeId == PrjPub.ITEMTYPE_LUNSHU)
                    {
                        item.StandardAnswer = dr["��ȷѡ��"].ToString().Trim();
                    }

                    item.Description = "";
                    item.OutDateDate = Convert.ToDateTime(dr["����ʱ��"].ToString());
                    item.UsedCount = 0;
                    item.StatusId = 1;
                    item.CreatePerson = PrjPub.CurrentLoginUser.EmployeeName;
                    item.CreateTime = DateTime.Today;
                    item.UsageId = 0;
                    item.Memo = "";
                    item.HasPicture = 0;
                    item.KeyWord = dr["�ؼ���"].ToString().Trim();
                    item.MotherCode = string.Empty;
                    item.UsageId = dr["�����ڿ���"].ToString() == "" || dr["�����ڿ���"].ToString() == "0" ? 0 : 1;

                    if (item.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                    {
                        itemindex = 1;
                    }
                    else if (item.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                    {
                        item.ItemIndex = itemindex;
                        itemindex++;
                    }

                    if (item.TypeId != PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                    {
                        count++;
                    }

                    objList.Add(item);
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ�������','" + ((double)(m * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    m = m + 1;
                }

                objBll.AddItem(objList);

                jsBlock = "<script>SetCompleted('���⵼�����'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                strMessage = "����ɹ���һ������" + count + "�⣡";
            }
            catch
            {
                strMessage = "����ʧ��!";
            }

            if (File.Exists(strPath))
                File.Delete(strPath);
            Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
            #endregion

        }

        /// <summary>
        /// ��Ӵ�����Ϣ
        /// </summary>
        /// <param name="dt">������ϢDataTable</param>
        /// <param name="dr">������ԴDataRow</param>
        /// <param name="strError">����ԭ��</param>
        private void AddError(DataTable dt, DataRow dr, string strError)
        {
            DataRow drnew = dt.NewRow();
            drnew["id"] = dr["���"].ToString();
            drnew["ItemContent"] = dr["��������"].ToString();
            drnew["ItemType"] = dr["��������"].ToString();
            drnew["AnswerCount"] = dr["ѡ����Ŀ"].ToString();
            drnew["OverDate"] = dr["����ʱ��"].ToString();
            drnew["Answer"] = dr["��ȷѡ��"].ToString().ToUpper();
            drnew["ErrorReason"] = strError;
            dt.Rows.Add(drnew);
        }

        private char intToChar(int intCol)
        {
            return Convert.ToChar('A' + intCol);
        }

        private int charToInt(char chr)
        {
            return Convert.ToInt32(chr) - Convert.ToInt32('A');
        }

        private Hashtable GetBookName()
        {
            Hashtable htBook = new Hashtable();
            BookBLL objBll = new BookBLL();
            IList<RailExam.Model.Book> objBookList = objBll.GetAllBookInfo(0);
            foreach (RailExam.Model.Book book in objBookList)
            {
                if (!htBook.ContainsKey(book.bookName))
                {
                    htBook.Add(book.bookName, book.bookId + "|" + book.publishOrg);
                }
            }

            return htBook;
        }

        private Hashtable GetChapterName(int bookID)
        {
            Hashtable hfChapterName = new Hashtable();
            BookChapterBLL objBll = new BookChapterBLL();
            IList<RailExam.Model.BookChapter> objChapterList = objBll.GetBookChapterByBookID(bookID);
            foreach (BookChapter chapter in objChapterList)
            {
                if (chapter.LevelNum == 1)
                {
                    hfChapterName.Add(chapter.ChapterName, chapter.ChapterId);
                }
            }
            return hfChapterName;
        }

        private Hashtable GetChapterNameByChapterID(int chapterID)
        {
            Hashtable hfChapterName = new Hashtable();
            BookChapterBLL objBll = new BookChapterBLL();
            IList<RailExam.Model.BookChapter> objChapterList = objBll.GetBookChapterByParentID(chapterID);
            foreach (BookChapter chapter in objChapterList)
            {
                if (!chapter.IsMotherItem)
                {
                    hfChapterName.Add(chapter.ChapterName, chapter.ChapterId);
                }
            }
            return hfChapterName;
        }

        private Hashtable GetMotherChapterNameByBookID(int bookID)
        {
            Hashtable hfChapterName = new Hashtable();
            BookChapterBLL objBll = new BookChapterBLL();
            IList<RailExam.Model.BookChapter> objChapterList = objBll.GetBookChapterByBookID(bookID);
            foreach (BookChapter chapter in objChapterList)
            {
                if (chapter.IsMotherItem)
                {
                    hfChapterName.Add(chapter.ChapterName.Replace("��ĸ�⣩", ""), chapter.ChapterId);
                }
            }
            return hfChapterName;
        }
    }
}
