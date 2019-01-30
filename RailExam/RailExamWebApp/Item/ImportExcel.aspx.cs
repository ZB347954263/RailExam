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
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
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
            //不符合数据的数据源
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


            #region 读取Excel文件

            try
            {
                //生成ExcelApp   
                objApp = new Excel.Application();
                //Excel不显示   
                objApp.Visible = false;
                //生成Books   
                objBooks = objApp.Workbooks;
                //打开Excel文件   
                objBooks.Open(strPath,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing);
                //取得Book   
                objBook = objBooks.get_Item(1);
                //取得Sheets   
                objSheets = objBook.Worksheets;
                //取得Sheet   
                objSheet = (Excel._Worksheet)objSheets.get_Item(1);
                //取得Range   

                int rowNum = objSheet.UsedRange.Rows.Count;
                int colNum = objSheet.UsedRange.Columns.Count;

                // 根据 ProgressBar.htm 显示进度条界面
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

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["序号"]], objSheet.Cells[i, htCol["序号"]]));
                    newRow["序号"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["教材名称"]], objSheet.Cells[i, htCol["教材名称"]]));
                    newRow["教材名称"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["章"]], objSheet.Cells[i, htCol["章"]]));
                    newRow["章"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["节"]], objSheet.Cells[i, htCol["节"]]));
                    newRow["节"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["母题编号"]], objSheet.Cells[i, htCol["母题编号"]]));
                    newRow["母题编号"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["试题内容"]], objSheet.Cells[i, htCol["试题内容"]]));
                    newRow["试题内容"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["试题类型"]], objSheet.Cells[i, htCol["试题类型"]]));
                    newRow["试题类型"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["选项数目"]], objSheet.Cells[i, htCol["选项数目"]]));
                    newRow["选项数目"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["过期时间"]], objSheet.Cells[i, htCol["过期时间"]]));
                    if (range.Value2 != null)
                    {
                        try
                        {
                            newRow["过期时间"] = DateTime.FromOADate(Convert.ToInt32(range.Value2)).ToString("d");
                        }
                        catch
                        {
                            newRow["过期时间"] = range.Value2;
                        }
                    }
                    else
                    {
                        newRow["过期时间"] = "";
                    }

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["关键字"]], objSheet.Cells[i, htCol["关键字"]]));
                    newRow["关键字"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["正确选项"]], objSheet.Cells[i, htCol["正确选项"]]));
                    newRow["正确选项"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["仅用于考试"]], objSheet.Cells[i, htCol["仅用于考试"]]));
                    newRow["仅用于考试"] = range.Value2;

                    int start = 10;

                    //if (!_isWuhan)
                    //{
                    //    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["教材名称"]], objSheet.Cells[i, htCol["教材名称"]]));
                    //    newRow["教材名称"] = range.Value2;

                    //    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["试题难度"]], objSheet.Cells[i, htCol["试题难度"]]));
                    //    newRow["试题难度"] = range.Value2;
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
                    jsBlock = "<script>SetPorgressBar('正在读取Excel文件','" + ((double)((i - 1) * 100) / (double)rowNum).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                ds.Tables.Add(dtItem);

                // 处理完成
                jsBlock = "<script>SetCompleted('Excel数据读取完毕'); </script>";
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
                Response.Write("<script>window.returnValue='请检查Excel文件格式',window.close();</script>");
                return;
            }

            #endregion

            #region 检验数据

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正准备检测Excel数据','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>window.returnValue='Excel中没有任何记录，请核对',window.close();</script>");
                return;
            }

            DataColumn dc1 = ds.Tables[0].Columns.Add("ItemType");
            int index = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["教材名称"].ToString().Trim() != "")
                {
                    AddError(dt, dr, "导入章节试题时，教材名称必须为空");
                    continue;
                }

                // 如果章节ID不为空，则章和节必须为空
                if (dr["章"].ToString().Trim() != "")
                {
                    AddError(dt, dr, "导入章节试题时，章必须为空");
                    continue;
                }

                if (dr["节"].ToString().Trim() != "")
                {
                    AddError(dt, dr, "导入章节试题时，节必须为空");
                    continue;
                }

                //试题内容不能为空
                if (dr["试题内容"].ToString() == "")
                {
                    AddError(dt, dr, "试题内容不能为空");
                    continue;
                }

                //判断试题类型的正确性
                if (dr["试题类型"].ToString() == PrjPub.ITEMTYPE_SINGLECHOOSE.ToString())
                {
                    dr["ItemType"] = "单选";
                }
                else if (dr["试题类型"].ToString() == PrjPub.ITEMTYPE_MULTICHOOSE.ToString())
                {
                    dr["ItemType"] = "多选";
                }
                else if (dr["试题类型"].ToString() == PrjPub.ITEMTYPE_JUDGE.ToString())
                {
                    dr["ItemType"] = "判断";
                }
                else if (dr["试题类型"].ToString() == PrjPub.ITEMTYPE_FILLBLANK.ToString())
                {
                    dr["ItemType"] = "填空";
                }
                else if (dr["试题类型"].ToString() == PrjPub.ITEMTYPE_QUESTION.ToString())
                {
                    dr["ItemType"] = "简答";
                }
                else if (dr["试题类型"].ToString() == PrjPub.ITEMTYPE_DISCUSS.ToString())
                {
                    dr["ItemType"] = "论述";
                }
                else
                {
                    AddError(dt, dr, "试题类型错误");
                    continue;
                }

                //检验过期时间
                if (dr["过期时间"].ToString() == "")
                {
                    dr["过期时间"] = "2050-01-01";
                }
                else
                {
                    try
                    {
                        DateTime date = Convert.ToDateTime(dr["过期时间"].ToString());
                    }
                    catch
                    {
                        AddError(dt, dr, "过期时间格式不正确");
                        continue;
                    }
                }

                //检验选项数目
                int itemCount = 0;
                if (dr["选项数目"].ToString() == "")
                {
                    //如果试题类型为单选、多选和判断，选项数目不能为空
                    if (dr["ItemType"].ToString() == "单选" || dr["ItemType"].ToString() == "多选")
                    {
                        AddError(dt, dr, dr["ItemType"] + "题选项数目不能为空");
                        continue;
                    }
                    else
                    {
                        dr["选项数目"] = "2";
                    }
                }
                else
                {
                    try
                    {
                        itemCount = Math.Abs(Convert.ToInt32(dr["选项数目"].ToString()));
                    }
                    catch
                    {
                        AddError(dt, dr, "选项数目必须为正整数");
                        continue;
                    }
                }

                //判断选项数目是否与现有选项一致
                for (int i = 0; i < itemCount; i++)
                {
                    string str = intToChar(i).ToString();
                    try
                    {
                        //选项不能为空
                        if (dr[str].ToString() == "")
                        {
                            AddError(dt, dr, "选项" + str + "不能为空");
                            continue;
                        }
                    }
                    catch
                    {
                        AddError(dt, dr, "选项" + str + "不存在");
                        continue;
                    }
                }

                if (mode == "0")
                {
                    if (dr["ItemType"].ToString() == "单选" || dr["ItemType"].ToString() == "多选" || dr["ItemType"].ToString() == "判断")
                    {
                        //当试题类型为单选、多选和判断时，正确选项不能为空
                        if (dr["正确选项"].ToString() == "")
                        {
                            AddError(dt, dr, "正确选项不能为空");
                            continue;
                        }
                        //当试题类型为单选、多选和判断，并且正确选项不为空时，检测正确选项填写的正确性
                        else
                        {
                            if (itemCount != 0)
                            {
                                int n = 0;
                                if (dr["ItemType"].ToString() == "单选" || dr["ItemType"].ToString() == "判断")
                                {
                                    n = 0;
                                    for (int i = 0; i < itemCount; i++)
                                    {
                                        string str = intToChar(i).ToString();
                                        if (str == dr["正确选项"].ToString().ToUpper())
                                        {
                                            n = n + 1;
                                        }
                                    }

                                    if (n == 0)
                                    {
                                        AddError(dt, dr, "正确选项填写错误");
                                        continue;
                                    }
                                }
                                else
                                {
                                    char[] chr = dr["正确选项"].ToString().ToCharArray();
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
                                            AddError(dt, dr, "正确选项填写错误");
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
                //    if (dr["试题难度"].ToString() == "")
                //    {
                //        AddError(dt, dr, "试题难度不能为空");
                //        continue;
                //    }
                //    else
                //    {
                //        try
                //        {
                //            int diff = Convert.ToInt32(dr["试题难度"].ToString());
                //            if (diff < 1 || diff > 5)
                //            {
                //                AddError(dt, dr, "试题难度只能在1到5之间");
                //                continue;
                //            }
                //        }
                //        catch
                //        {
                //            AddError(dt, dr, "试题难度必须为数字");
                //            continue;
                //        }
                //    }
                //}

                if (dr["ItemType"].ToString() == "判断")
                {
                    if (dr["A"].ToString() != "对" || dr["B"].ToString() != "错")
                    {
                        AddError(dt, dr, "判断题选项A必须为对，选项B必须为错");
                        continue;
                    }
                }

                if (dr["ItemType"].ToString() == "填空")
                {
                    if (dr["正确选项"].ToString() == "")
                    {
                        AddError(dt, dr, "填空题正确选项（正确答案）不能为空");
                        continue;
                    }
                }

                if (dr["ItemType"].ToString() == "简答" || dr["ItemType"].ToString() == "论述")
                {
                    if (dr["正确选项"].ToString() == "")
                    {
                        AddError(dt, dr, "简答题和论述题正确选项（正确答案）不能为空");
                        continue;
                    }
                    if (dr["正确选项"].ToString().Length > 2000)
                    {
                        AddError(dt, dr, "简答题和论述题正确选项（正确答案）不能超过2000个字符");
                        continue;
                    }
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('正在检测Excel数据','" + ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                index = index + 1;
            }

            // 处理完成
            jsBlock = "<script>SetCompleted('Excel数据检测完毕'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            #endregion

            if (dt.Rows.Count > 0)
            {
                Session["table"] = dt;
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='refresh|请检查Excel文件数据',window.close();</script>");
                return;
            }
            else
            {
                dt.Clear();
                Session["table"] = dt;
            }

            #region 导入数据

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正准备导入试题','0.00'); </script>";
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
                    item.TypeId = Convert.ToInt32(dr["试题类型"].ToString());
                    item.CompleteTime = 60;
                    //if (!_isWuhan)
                    //{
                    //    item.DifficultyId = Convert.ToInt32(dr["试题难度"].ToString());
                    //}
                    //else
                    //{
                    //    item.DifficultyId = 3;
                    //}
                    item.Score = 4;
                    item.Content = dr["试题内容"].ToString();
                    item.AnswerCount = Convert.ToInt32(dr["选项数目"].ToString());

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
                        item.SelectAnswer = "对|错";
                    }
                    else
                    {
                        item.SelectAnswer = "";
                    }

                    if (mode == "0")
                    {

                        if (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || item.TypeId == PrjPub.ITEMTYPE_JUDGE)
                        {
                            item.StandardAnswer = charToInt(Convert.ToChar(dr["正确选项"].ToString().Trim().ToUpper())).ToString();
                        }
                        else if (item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)
                        {
                            string strStandard = "";
                            char[] chr = dr["正确选项"].ToString().Trim().ToUpper().ToCharArray();
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
                        item.StandardAnswer = dr["正确选项"].ToString().Trim();
                    }

                    item.Description = "";
                    item.OutDateDate = Convert.ToDateTime(dr["过期时间"].ToString());
                    item.UsedCount = 0;
                    item.StatusId = 1;
                    item.CreatePerson = PrjPub.CurrentLoginUser.EmployeeName;
                    item.CreateTime = DateTime.Today;
                    item.UsageId = 0;
                    item.Memo = "";
                    item.HasPicture = 0;
                    item.KeyWord = dr["关键字"].ToString();
                    item.UsageId = dr["仅用于考试"].ToString() == "" || dr["仅用于考试"].ToString() == "0" ? 0 : 1;

                    objList.Add(item);
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入试题','" + ((double)(m * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    m = m + 1;
                }

                objBll.AddItem(objList);

                jsBlock = "<script>SetCompleted('试题导入完毕'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                strMessage = "导入成功！";
            }
            catch (Exception ex)
            {
                strMessage = "导入失败!";
            }

            if (File.Exists(strPath))
                File.Delete(strPath);
            Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
            #endregion
        }

        private void ReadExcelByBook()
        {
            //不符合数据的数据源
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

            #region 读取Excel文件

            try
            {
                //生成ExcelApp   
                objApp = new Excel.Application();
                //Excel不显示   
                objApp.Visible = false;
                //生成Books   
                objBooks = objApp.Workbooks;
                //打开Excel文件   
                objBooks.Open(strPath,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing);
                //取得Book   
                objBook = objBooks.get_Item(1);
                //取得Sheets   
                objSheets = objBook.Worksheets;
                //取得Sheet   
                objSheet = (Excel._Worksheet)objSheets.get_Item(1);
                //取得Range   

                int rowNum = objSheet.UsedRange.Rows.Count;
                int colNum = objSheet.UsedRange.Columns.Count;

                // 根据 ProgressBar.htm 显示进度条界面
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

                    errorcol = "序号";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["序号"]], objSheet.Cells[i, htCol["序号"]]));
                    newRow["序号"] = range.Value2;

                    errorcol = "教材名称";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["教材名称"]], objSheet.Cells[i, htCol["教材名称"]]));
                    newRow["教材名称"] = range.Value2;

                    errorcol = "章";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["章"]], objSheet.Cells[i, htCol["章"]]));
                    newRow["章"] = range.Value2;

                    errorcol = "节";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["节"]], objSheet.Cells[i, htCol["节"]]));
                    newRow["节"] = range.Value2;

                    //errorcol = "母题编号";
                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["母题编号"]], objSheet.Cells[i, htCol["母题编号"]]));
                    //newRow["母题编号"] = range.Value2;

                    errorcol = "试题内容";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["试题内容"]], objSheet.Cells[i, htCol["试题内容"]]));
                    newRow["试题内容"] = range.Value2;

                    errorcol = "试题类型";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["试题类型"]], objSheet.Cells[i, htCol["试题类型"]]));
                    newRow["试题类型"] = range.Value2;

                    errorcol = "选项数目";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["选项数目"]], objSheet.Cells[i, htCol["选项数目"]]));
                    newRow["选项数目"] = range.Value2;

                    errorcol = "过期时间";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["过期时间"]], objSheet.Cells[i, htCol["过期时间"]]));
                    if (range.Value2 != null)
                    {
                        try
                        {
                            newRow["过期时间"] = DateTime.FromOADate(Convert.ToInt32(range.Value2)).ToString("d");
                        }
                        catch
                        {
                            newRow["过期时间"] = range.Value2;
                        }
                    }
                    else
                    {
                        newRow["过期时间"] = "";
                    }

                    errorcol = "关键字";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["关键字"]], objSheet.Cells[i, htCol["关键字"]]));
                    newRow["关键字"] = range.Value2;

                    errorcol = "正确选项";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["正确选项"]], objSheet.Cells[i, htCol["正确选项"]]));
                    newRow["正确选项"] = range.Value2;

                    errorcol = "仅用于考试";
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["仅用于考试"]], objSheet.Cells[i, htCol["仅用于考试"]]));
                    newRow["仅用于考试"] = range.Value2;

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
                    jsBlock = "<script>SetPorgressBar('正在读取Excel文件','" + ((double)((i - 1) * 100) / (double)rowNum).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                ds.Tables.Add(dtItem);

                // 处理完成
                jsBlock = "<script>SetCompleted('Excel数据读取完毕'); </script>";
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
                    errormeg = "列【" + errorcol + "】不存在或出错";
                }
                Response.Write("<script>window.returnValue='请检查Excel文件格式:" + errormeg + "',window.close();</script>");
                return;
            }

            #endregion

            #region 检验数据

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正准备检测Excel数据','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>window.returnValue='Excel中没有任何记录，请核对',window.close();</script>");
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
                    if (dr["教材名称"].ToString().Trim() == "")
                    {
                        AddError(dt, dr, "未选定教材导入教材试题时，教材名称不能为空");
                        continue;
                    }
                    else
                    {
                        if (!htBook.ContainsKey(dr["教材名称"].ToString().Trim()))
                        {
                            AddError(dt, dr, "未选定教材导入教材试题时，教材在系统中不存在");
                            continue;
                        }
                        else
                        {
                            if (htBook[dr["教材名称"].ToString().Trim()].ToString().Split('|')[1] !=
                                PrjPub.CurrentLoginUser.StationOrgID.ToString() && PrjPub.CurrentLoginUser.SuitRange != 1)
                            {
                                AddError(dt, dr, "您没有导入该教材试题的权限");
                                continue;
                            }
                            dr["BookID"] = htBook[dr["教材名称"].ToString().Trim()].ToString().Split('|')[0];
                        }
                    }
                }
                else
                {
                    if (dr["教材名称"].ToString().Trim() != strBookName)
                    {
                        AddError(dt, dr, "选定教材导入教材试题时，教材名称必须与选定教材一致");
                        continue;
                    }
                    else
                    {
                        dr["BookID"] = HfBookId.Value;
                    }

                    if (dr["教材名称"].ToString().Trim() == "")
                    {
                        AddError(dt, dr, "导入教材试题时，教材名称不能为空");
                        continue;
                    }
                    else
                    {
                        if (!htBook.ContainsKey(dr["教材名称"].ToString().Trim()))
                        {
                            AddError(dt, dr, "教材在系统中不存在");
                            continue;
                        }
                        else
                        {
                            dr["BookID"] = htBook[dr["教材名称"].ToString().Trim()].ToString().Split('|')[0];
                        }
                    }
                }

                Hashtable hfChapterName = GetChapterName(Convert.ToInt32(dr["BookID"].ToString()));
                // 如果章节ID不为空，则章和节必须为空
                if (dr["章"].ToString().Trim() == "")
                {
                    AddError(dt, dr, "导入教材试题时，章不能为空");
                    continue;
                }
                else
                {
                    if (!hfChapterName.ContainsKey(dr["章"].ToString().Trim()))
                    {
                        AddError(dt, dr, "导入教材试题时，章在当前教材下不存在");
                        continue;
                    }
                }

                if (dr["节"].ToString().Trim() != "")
                {
                    Hashtable hfChapterNameByChapterID =
                        GetChapterNameByChapterID(Convert.ToInt32(hfChapterName[dr["章"].ToString().Trim()]));
                    if (!hfChapterNameByChapterID.ContainsKey(dr["节"].ToString().Trim()))
                    {
                        AddError(dt, dr, "导入教材试题时，节在当前教材的“" + dr["章"].ToString().Trim() + "”下不存在");
                        continue;
                    }
                }

                //if (dr["母题编号"].ToString().Trim() != "")
                //{
                //    if (dr["母题编号"].ToString().Trim().Length > 50)
                //    {
                //        AddError(dt, dr, "母题编号不能超过50个字符");
                //        continue;
                //    }
                //}

                //if (dr["母题编号"].ToString().Trim() != "")
                //{
                //    Hashtable hfChapterNameByChapterID =
                //        GetMotherChapterNameByBookID(Convert.ToInt32(dr["BookID"].ToString()));
                //    if (!hfChapterNameByChapterID.ContainsKey(dr["母题编号"].ToString().Trim()))
                //    {
                //        AddError(dt, dr, "导入教材试题时，母题编号“" + dr["母题编号"].ToString().Trim() + "”在当前教材中不存在");
                //        continue;
                //    }
                //}

                //试题内容不能为空
                if (dr["试题内容"].ToString().Trim() == "")
                {
                    AddError(dt, dr, "试题内容不能为空");
                    continue;
                }

                //判断试题类型的正确性
                if (dr["试题类型"].ToString().Trim() == PrjPub.ITEMTYPE_SINGLECHOOSE.ToString())
                {
                    dr["ItemType"] = "单选";
                }
                else if (dr["试题类型"].ToString().Trim() == PrjPub.ITEMTYPE_MULTICHOOSE.ToString())
                {
                    dr["ItemType"] = "多选";
                }
                else if (dr["试题类型"].ToString().Trim() == PrjPub.ITEMTYPE_JUDGE.ToString())
                {
                    dr["ItemType"] = "判断";
                }
                else if (dr["试题类型"].ToString().Trim() == PrjPub.ITEMTYPE_FILLBLANK.ToString())
                {
                    dr["ItemType"] = "完型填空";
                }
                else if (dr["试题类型"].ToString().Trim() == PrjPub.ITEMTYPE_FILLBLANKDETAIL.ToString())
                {
                    dr["ItemType"] = "完型填空子题";
                }
                else if (dr["试题类型"].ToString().Trim() == PrjPub.ITEMTYPE_QUESTION.ToString())
                {
                    dr["ItemType"] = "填空";
                }
                else if (dr["试题类型"].ToString().Trim() == PrjPub.ITEMTYPE_DISCUSS.ToString())
                {
                    dr["ItemType"] = "简答";
                }
                else if (dr["试题类型"].ToString().Trim() == PrjPub.ITEMTYPE_LUNSHU.ToString())
                {
                    dr["ItemType"] = "论述";
                }
                else
                {
                    AddError(dt, dr, "试题类型错误");
                    continue;
                }

                //检验过期时间
                if (dr["过期时间"].ToString().Trim() == "")
                {
                    dr["过期时间"] = "2050-01-01";
                }
                else
                {
                    try
                    {
                        DateTime date = Convert.ToDateTime(dr["过期时间"].ToString().Trim());
                    }
                    catch
                    {
                        AddError(dt, dr, "过期时间格式不正确");
                        continue;
                    }
                }

                //检验选项数目
                int itemCount = 0;
                if (dr["选项数目"].ToString().Trim() == "")
                {
                    //如果试题类型为单选、多选和判断，选项数目不能为空
                    if (dr["ItemType"].ToString() == "单选" || dr["ItemType"].ToString() == "多选"
                        || dr["ItemType"].ToString() == "完型填空"
                        || dr["ItemType"].ToString() == "完型填空子题")
                    {
                        AddError(dt, dr, dr["ItemType"] + "题选项数目不能为空");
                        continue;
                    }
                    else
                    {
                        dr["选项数目"] = "2";
                    }
                }
                else
                {
                    try
                    {
                        itemCount = Math.Abs(Convert.ToInt32(dr["选项数目"].ToString()));
                    }
                    catch
                    {
                        AddError(dt, dr, "选项数目必须为正整数");
                        continue;
                    }
                }

                if (dr["ItemType"].ToString() != "完型填空")
                {
                    //判断选项数目是否与现有选项一致
                    for (int i = 0; i < itemCount; i++)
                    {
                        string str = intToChar(i).ToString();
                        try
                        {
                            //选项不能为空
                            if (dr[str].ToString() == "")
                            {
                                AddError(dt, dr, "选项" + str + "不能为空");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(dt, dr, "选项" + str + "不存在");
                            continue;
                        }
                    }
                }


                if (dr["ItemType"].ToString() == "单选" || dr["ItemType"].ToString() == "多选" || dr["ItemType"].ToString() == "判断" || dr["ItemType"].ToString() == "完型填空子题")
                {
                    //当试题类型为单选、多选和判断时，正确选项不能为空
                    if (dr["正确选项"].ToString().Trim() == "")
                    {
                        AddError(dt, dr, "正确选项不能为空");
                        continue;
                    }
                    //当试题类型为单选、多选和判断，并且正确选项不为空时，检测正确选项填写的正确性
                    else
                    {
                        if (itemCount != 0)
                        {
                            int n = 0;
                            if (dr["ItemType"].ToString() == "单选" || dr["ItemType"].ToString() == "判断" || dr["ItemType"].ToString() == "完型填空子题")
                            {
                                n = 0;
                                for (int i = 0; i < itemCount; i++)
                                {
                                    string str = intToChar(i).ToString();
                                    if (str == dr["正确选项"].ToString().Trim().ToUpper())
                                    {
                                        n = n + 1;
                                    }
                                }

                                if (n == 0)
                                {
                                    AddError(dt, dr, "正确选项填写错误");
                                    continue;
                                }
                            }
                            else
                            {
                                char[] chr = dr["正确选项"].ToString().Trim().ToCharArray();
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
                                        AddError(dt, dr, "正确选项填写错误");
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

                //if (dr["试题难度"].ToString() == "")
                //{
                //    AddError(dt, dr, "试题难度不能为空");
                //    continue;
                //}
                //else
                //{
                //    try
                //    {
                //        int diff = Convert.ToInt32(dr["试题难度"].ToString());
                //        if (diff < 1 || diff > 5)
                //        {
                //            AddError(dt, dr, "试题难度只能在1到5之间");
                //            continue;
                //        }
                //    }
                //    catch
                //    {
                //        AddError(dt, dr, "试题难度必须为数字");
                //        continue;
                //    }
                //}

                if (dr["ItemType"].ToString() == "判断")
                {
                    if (dr["A"].ToString().Trim() != "对" || dr["B"].ToString().Trim() != "错")
                    {
                        AddError(dt, dr, "判断题选项A必须为对，选项B必须为错");
                        continue;
                    }
                }


                //if (dr["ItemType"].ToString() == "完型填空子题")
                //{
                //    if (dr["正确选项"].ToString() == "")
                //    {
                //        AddError(dt, dr, "填空题正确选项（正确答案）不能为空");
                //        continue;
                //    }
                //}

                if (dr["ItemType"].ToString() == "简答" || dr["ItemType"].ToString() == "论述" || dr["ItemType"].ToString() == "填空")
                {
                    if (dr["正确选项"].ToString().Trim() == "")
                    {
                        AddError(dt, dr, "填空、简答题和论述题正确选项（正确答案）不能为空");
                        continue;
                    }
                    if (dr["正确选项"].ToString().Trim().Length > 2000)
                    {
                        AddError(dt, dr, "填空、简答题和论述题正确选项（正确答案）不能超过2000个字符");
                        continue;
                    }
                }

                if (index == nowindex + 1 && dr["ItemType"].ToString() != "完型填空子题" && nowindex != 0)
                {
                    AddError(dt, ds.Tables[0].Rows[nowindex - 1], "存在没有综合选择题子题的综合选择题");
                }

                if (dr["ItemType"].ToString() == "完型填空")
                {
                    if (nowindex != 0)
                    {
                        ht.Add(nowindex, itemtotalcount + "|" + itemindex);
                    }
                    nowindex = index;
                    itemindex = 0;
                    itemtotalcount = Convert.ToInt32(dr["选项数目"]);
                }
                else
                {
                    if (dr["ItemType"].ToString() != "完型填空子题")
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
                jsBlock = "<script>SetPorgressBar('正在检测Excel数据','" + ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
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
                    AddError(dt, ds.Tables[0].Rows[Convert.ToInt32(dictionaryEntry.Key) - 1], "综合选择题设置子题数量与Excel中实际子题数量不符");
                }
            }

            // 处理完成
            jsBlock = "<script>SetCompleted('Excel数据检测完毕'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            #endregion

            if (dt.Rows.Count > 0)
            {
                Session["table"] = dt;
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='refresh|请检查Excel文件数据',window.close();</script>");
                return;
            }
            else
            {
                dt.Clear();
                Session["table"] = dt;
            }

            #region 导入数据

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正准备导入试题','0.00'); </script>";
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

                    #region 原母题
                    /*
                    if (dr["母题编号"].ToString().Trim() != "")
                    {
                        Hashtable hfMother = GetMotherChapterNameByBookID(item.BookId);
                        item.ChapterId = Convert.ToInt32(hfMother[dr["母题编号"].ToString().Trim()]);
                    }
                    else
                    {
                        Hashtable hfChapterName = GetChapterName(item.BookId);
                        if (dr["节"].ToString().Trim() == "")
                        {
                            item.ChapterId = Convert.ToInt32(hfChapterName[dr["章"].ToString().Trim()]);
                        }
                        else
                        {
                            Hashtable hf =
                                GetChapterNameByChapterID(Convert.ToInt32(hfChapterName[dr["章"].ToString().Trim()]));
                            item.ChapterId = Convert.ToInt32(hf[dr["节"].ToString().Trim()]);
                        }
                    }*/
                    #endregion

                    Hashtable hfChapterName = GetChapterName(item.BookId);
                    if (dr["节"].ToString().Trim() == "")
                    {
                        item.ChapterId = Convert.ToInt32(hfChapterName[dr["章"].ToString().Trim()]);
                    }
                    else
                    {
                        Hashtable hf =
                            GetChapterNameByChapterID(Convert.ToInt32(hfChapterName[dr["章"].ToString().Trim()]));
                        item.ChapterId = Convert.ToInt32(hf[dr["节"].ToString().Trim()]);
                    }

                    item.OrganizationId = PrjPub.CurrentLoginUser.StationOrgID;
                    item.TypeId = Convert.ToInt32(dr["试题类型"].ToString());
                    item.CompleteTime = 60;
                    item.DifficultyId = 3;
                    item.Score = 4;
                    item.Content = dr["试题内容"].ToString();
                    item.AnswerCount = Convert.ToInt32(dr["选项数目"].ToString());

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
                        item.SelectAnswer = "对|错";
                    }
                    else
                    {
                        item.SelectAnswer = "";
                    }

                    if (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || item.TypeId == PrjPub.ITEMTYPE_JUDGE || item.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                    {
                        item.StandardAnswer = charToInt(Convert.ToChar(dr["正确选项"].ToString().Trim().ToUpper())).ToString();
                    }
                    else if (item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)
                    {
                        string strStandard = "";
                        char[] chr = dr["正确选项"].ToString().Trim().ToUpper().ToCharArray();
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
                        item.StandardAnswer = dr["正确选项"].ToString().Trim();
                    }

                    item.Description = "";
                    item.OutDateDate = Convert.ToDateTime(dr["过期时间"].ToString());
                    item.UsedCount = 0;
                    item.StatusId = 1;
                    item.CreatePerson = PrjPub.CurrentLoginUser.EmployeeName;
                    item.CreateTime = DateTime.Today;
                    item.UsageId = 0;
                    item.Memo = "";
                    item.HasPicture = 0;
                    item.KeyWord = dr["关键字"].ToString().Trim();
                    item.MotherCode = string.Empty;
                    item.UsageId = dr["仅用于考试"].ToString() == "" || dr["仅用于考试"].ToString() == "0" ? 0 : 1;

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
                    jsBlock = "<script>SetPorgressBar('正在导入试题','" + ((double)(m * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    m = m + 1;
                }

                objBll.AddItem(objList);

                jsBlock = "<script>SetCompleted('试题导入完毕'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                strMessage = "导入成功！一共导入" + count + "题！";
            }
            catch
            {
                strMessage = "导入失败!";
            }

            if (File.Exists(strPath))
                File.Delete(strPath);
            Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
            #endregion

        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="dt">错误信息DataTable</param>
        /// <param name="dr">数据来源DataRow</param>
        /// <param name="strError">错误原因</param>
        private void AddError(DataTable dt, DataRow dr, string strError)
        {
            DataRow drnew = dt.NewRow();
            drnew["id"] = dr["序号"].ToString();
            drnew["ItemContent"] = dr["试题内容"].ToString();
            drnew["ItemType"] = dr["试题类型"].ToString();
            drnew["AnswerCount"] = dr["选项数目"].ToString();
            drnew["OverDate"] = dr["过期时间"].ToString();
            drnew["Answer"] = dr["正确选项"].ToString().ToUpper();
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
                    hfChapterName.Add(chapter.ChapterName.Replace("（母题）", ""), chapter.ChapterId);
                }
            }
            return hfChapterName;
        }
    }
}
