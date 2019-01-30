using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.Drawing.Imaging;
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

namespace RailExamWebApp.RandomExam
{
    public partial class DealPaperProgress : PageBase
    {
        public string load = "false";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Get("type") == "Get")
                {
                    GetPaper();
                    load = "true";
                }

                if (Request.QueryString.Get("type") == "GetAfter")
                {
                    GetPaperAfter();
                    load = "true";
                }

                if (Request.QueryString.Get("type") == "Upload")
                {
                    UploadPaper();
                    load = "true";
                }
            }
        }

        private void GetPaper()
        {
            // 根据 ProgressBar.htm 显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            string jsBlock;

            ViewState["BeginTime"] = DateTime.Now.ToString();
            string strId = Request.QueryString.Get("RandomExamID");

            //获取当前考试的生成试卷的状态和次数
            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

            int year = obj.BeginTime.Year;
            int ExamCount = obj.MaxExamTimes;

            RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
            IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));
            string strChooseID = "";
            OracleAccess db = new OracleAccess(); 
            OracleAccess dbCenter = new OracleAccess(System.Configuration.ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);

            string strSql;
            if (ExamArranges.Count > 0)
            {
                strSql = "select a.* from Random_Exam_Arrange_Detail a "
                          + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                          + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                          + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                          + " and Random_Exam_ID=" + strId;

                DataSet ds = db.RunSqlDataSet(strSql);

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
            }
            else
            {
                strChooseID = "";
            }

            if (strChooseID == "")
            {
                Response.Write("<script>top.returnValue='本场考试未在本单位安排考生！';window.close();</script>");
                return;
            }

            if (db.GetCount("RANDOM_EXAM_ITEM_TEMP_" + year, "TABLE") == 0)
            {
                strSql = "create table RANDOM_EXAM_ITEM_TEMP_" + year + "  as select * from RANDOM_EXAM_ITEM_" + year + " where 1=2 ";
                db.ExecuteNonQuery(strSql);
            }
            strSql = "insert into RANDOM_EXAM_ITEM_TEMP_" + year + " select * from RANDOM_EXAM_ITEM_" + year + " where Random_Exam_ID=" + strId;
            db.ExecuteNonQuery(strSql);

            if (!PrjPub.IsServerCenter)
            {
                if (dbCenter.GetCount("RANDOM_EXAM_ITEM_TEMP_" + year, "TABLE") == 0)
                {
                    strSql = "create table RANDOM_EXAM_ITEM_TEMP_" + year + "  as select * from RANDOM_EXAM_ITEM_" + year + " where 1=2 ";
                    dbCenter.ExecuteNonQuery(strSql);
                }
                strSql = "insert into RANDOM_EXAM_ITEM_TEMP_" + year + " select * from RANDOM_EXAM_ITEM_" + year + " where Random_Exam_ID=" + strId;
                dbCenter.ExecuteNonQuery(strSql);
            }

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正在计算生成试卷数量，请等待......','" + ((1 * 100) / ((double)2)+ "'); </script>");
            Response.Write(jsBlock);
            Response.Flush();


            //每次生成试卷之前删除已生成的考试试卷
            RandomExamResultCurrentBLL randomExamResultBLL = new RandomExamResultCurrentBLL();
            randomExamResultBLL.DelRandomExamResultCurrent(Convert.ToInt32(strId));
            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正在计算生成试卷数量，请等待......','" + ((2 * 100) / ((double)2) + "'); </script>");
            Response.Write(jsBlock);
            Response.Flush();

            System.Threading.Thread.Sleep(200);
            jsBlock = string.Empty;

            string[] str = strChooseID.Split(',');

            RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
            //定义全局答卷对象List
            IList<RandomExamResultAnswerCurrent> randomExamResultAnswersCurrentAll = new List<RandomExamResultAnswerCurrent>();
            //定义一个考生一次答卷对象List
            IList<RandomExamResultAnswerCurrent> randomExamResultAnswers = null;

            int progressNum = 1;
            for (int n = 1; n <= ExamCount; n++)
            {
                for (int m = 0; m < str.Length; m++)
                {
                    RandomExamResultCurrent randomExamResult = new RandomExamResultCurrent();

                    randomExamResult.RandomExamId = int.Parse(strId);
                    randomExamResult.AutoScore = 0;
                    randomExamResult.BeginDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
                    randomExamResult.CurrentDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
                    randomExamResult.ExamTime = 0;
                    randomExamResult.EndDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
                    randomExamResult.Score = 0;
                    randomExamResult.OrganizationId = int.Parse(ConfigurationManager.AppSettings["StationID"]);
                    randomExamResult.Memo = "";
                    randomExamResult.StatusId = 0;
                    randomExamResult.AutoScore = 0;
                    randomExamResult.CorrectRate = 0;
                    randomExamResult.ExamineeId = int.Parse(str[m]);
                    randomExamResult.ExamSeqNo = n;

                    int nRandomExamResultPK = randomExamResultBLL.AddRandomExamResultCurrent(randomExamResult);
                    ViewState["RandomExamResultPK"] = nRandomExamResultPK;

                    strSql = "select a.* from Random_Exam_Arrange_Detail a "
                          + " where ','||User_Ids||',' like '%," + str[m] + ",%' "
                          + " and Random_Exam_ID=" + strId;
                    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    //strSql = "insert into Random_Exam_Result_Detail(Random_Exam_Result_Detail_ID,"
                    //         + "Random_Exam_Result_ID,Random_Exam_ID,Employee_ID,Computer_Room_SEAT,Computer_Room_ID) "
                    //         + "values(Random_Exam_Result_Detail_SEQ.NextVal,"
                    //         + nRandomExamResultPK + ","
                    //         + randomExamResult.RandomExamId + ","
                    //         + randomExamResult.ExamineeId + ","
                    //         + "0," + dr["Computer_Room_ID"] + ") ";
                    //db.ExecuteNonQuery(strSql);

                    strSql = "insert into Random_Exam_Result_Detail_Temp(Random_Exam_Result_Detail_ID,"
                         + "Random_Exam_Result_ID,Random_Exam_ID,Employee_ID,Computer_Room_SEAT,Computer_Room_ID,Is_Remove) "
                         + "values(Random_Exam_Result_Detail_SEQ.NextVal,"
                         + nRandomExamResultPK + ","
                         + randomExamResult.RandomExamId + ","
                         + randomExamResult.ExamineeId + ","
                         + "0," + dr["Computer_Room_ID"] + ",0) ";
                    db.ExecuteNonQuery(strSql);

                    RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
                    RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
                    RandomExamStrategyBLL strategyBLL = new RandomExamStrategyBLL();

                    IList<RandomExamSubject> randomExamSubjects =
                        subjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));

                    if (randomExamSubjects != null)
                    {
                        Hashtable hashTableItemIds = new Hashtable();
                        Hashtable htSubjectItemIds = new Hashtable();
                        for (int i = 0; i < randomExamSubjects.Count; i++)
                        {
                            RandomExamSubject paperSubject = randomExamSubjects[i];
                            int nSubjectId = paperSubject.RandomExamSubjectId;
                            //  int nItemCount = paperSubject.ItemCount;

                            IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
                            for (int j = 0; j < strategys.Count; j++)
                            {
                                int nStrategyId = strategys[j].RandomExamStrategyId;
                                int nItemCount = strategys[j].ItemCount;
                                IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId, year);

                                // IList<RandomExamItem> itemList = randomItemBLL.GetItemsBySubjectId(nSubjectId);

                                Random ObjRandom = new Random();
                                Hashtable hashTable = new Hashtable();
                                Hashtable hashTableCount = new Hashtable();
                                int index = 0;
                                while (hashTable.Count < nItemCount)
                                {
                                    int k = ObjRandom.Next(itemList.Count);
                                    hashTableCount[index] = k;
                                    index = index + 1;
                                    int itemID = itemList[k].ItemId;
                                    int examItemID = itemList[k].RandomExamItemId;
                                    if (!hashTableItemIds.ContainsKey(itemID))
                                    {
                                        hashTable[examItemID] = examItemID;
                                        hashTableItemIds[itemID] = itemID;
                                        htSubjectItemIds[examItemID] = examItemID;
                                    }
                                    //if (hashTableCount.Count == itemList.Count && hashTable.Count < nItemCount)
                                    //{
                                    //    SessionSet.PageMessage = "随机考试在设定的取题范围内的试题量不够，请重新设置取题范围！";
                                    //    return;
                                    //}
                                }
                            }
                        }


                        foreach (int key in htSubjectItemIds.Keys)
                        {
                            string strItemId = htSubjectItemIds[key].ToString();
                            RandomExamItem item = randomItemBLL.GetRandomExamItem(Convert.ToInt32(strItemId), year);

                            string nowSelectAnswer = string.Empty;
                            string nowStandardAnswer = string.Empty;
                            if(item.TypeId != PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                Pub.GetNowAnswer(item, out nowSelectAnswer, out nowStandardAnswer);
                            }

                            RandomExamResultAnswerCurrent randomExamResultAnswer = new RandomExamResultAnswerCurrent();
                            randomExamResultAnswer.RandomExamResultId = nRandomExamResultPK;
                            randomExamResultAnswer.RandomExamItemId = int.Parse(strItemId);
                            randomExamResultAnswer.JudgeStatusId = 0;
                            randomExamResultAnswer.JudgeRemark = string.Empty;
                            randomExamResultAnswer.ExamTime = 0;
                            randomExamResultAnswer.Answer = string.Empty;
                            randomExamResultAnswer.SelectAnswer = nowSelectAnswer;
                            randomExamResultAnswer.StandardAnswer = nowStandardAnswer;
                            randomExamResultAnswerBLL.AddExamResultAnswerCurrent(randomExamResultAnswer);

                            //完型填空子题
                            IList<RandomExamItem> randomExamItems = randomItemBLL.GetItemsByParentItemID(item.ItemId, obj.RandomExamId, year);
                            foreach (RandomExamItem randomExamItem in randomExamItems)
                            {
                                Pub.GetNowAnswer(randomExamItem, out nowSelectAnswer, out nowStandardAnswer);

                                randomExamResultAnswer = new RandomExamResultAnswerCurrent();
                                randomExamResultAnswer.RandomExamResultId = nRandomExamResultPK;
                                randomExamResultAnswer.RandomExamItemId = randomExamItem.RandomExamItemId;
                                randomExamResultAnswer.JudgeStatusId = 0;
                                randomExamResultAnswer.JudgeRemark = string.Empty;
                                randomExamResultAnswer.ExamTime = 0;
                                randomExamResultAnswer.Answer = string.Empty;
                                randomExamResultAnswer.SelectAnswer = nowSelectAnswer;
                                randomExamResultAnswer.StandardAnswer = nowStandardAnswer;
                                randomExamResultAnswerBLL.AddExamResultAnswerCurrent(randomExamResultAnswer);
                            }

                            System.Threading.Thread.Sleep(10);
                            jsBlock = "<script>SetPorgressBar('正在生成试卷，请等待......','" + ((progressNum * 100) / ((double)ExamCount * str.Length * htSubjectItemIds.Count)).ToString("0.00") + "'); </script>";
                            Response.Write(jsBlock);
                            Response.Flush();

                            progressNum++;
                        }
                    }
                    else
                    {
                        SessionSet.PageMessage = "未找到记录！";
                    }
                }
            }

            bool isRefresh = true;
            try
            {
                objBll.UpdateHasPaper(Convert.ToInt32(strId), PrjPub.ServerNo, true);
            }
            catch
            {
                strSql =
                    @"  update Random_Exam_Computer_Server set  has_paper=1
                      where random_exam_id=" + strId + @" and Computer_server_no='" + PrjPub.ServerNo + @"'";
                dbCenter.ExecuteNonQuery(strSql);
                strSql = @"select count(*)  from Random_Exam_Computer_Server where has_paper=1 and  random_exam_id=" + strId;
                int count = Convert.ToInt32(dbCenter.RunSqlDataSet(strSql).Tables[0].Rows[0][0]);
                if (count > 0)
                {
                    strSql = @"update Random_Exam set has_paper=1 where random_exam_id=" + strId;
                    dbCenter.ExecuteNonQuery(strSql);
                }
                else
                {
                    strSql = @"update Random_Exam set has_paper=0 where random_exam_id=" + strId;
                    dbCenter.ExecuteNonQuery(strSql);
                }
            }


            //如果考试是随到随考，考试状态自动变为正在进行
            if (obj.StartMode == 1)
            {
                try
                {
                    objBll.UpdateIsStart(Convert.ToInt32(strId), PrjPub.ServerNo, 1);
                    isRefresh = false;
                }
                catch
                {
                    strSql =
                        @"  update Random_Exam_Computer_Server set  Is_Start=1
                      where random_exam_id=" + strId + @" and Computer_server_no='" + PrjPub.ServerNo + @"'";
                    dbCenter.ExecuteNonQuery(strSql);
                    strSql = @"select count(*)  from Random_Exam_Computer_Server where random_exam_id=" + strId;
                    int totalcount = Convert.ToInt32(dbCenter.RunSqlDataSet(strSql).Tables[0].Rows[0][0]);
                    strSql = @"select count(*)  from Random_Exam_Computer_Server where Is_Start=0 and  random_exam_id=" + strId;
                    int count = Convert.ToInt32(dbCenter.RunSqlDataSet(strSql).Tables[0].Rows[0][0]);
                    if (totalcount == count)
                    {
                        strSql = @"update Random_Exam set Is_Start=0 where random_exam_id=" + strId;
                        dbCenter.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        strSql = @"select count(*)  from Random_Exam_Computer_Server where Is_Start=1 and  random_exam_id=" + strId;
                        count = Convert.ToInt32(dbCenter.RunSqlDataSet(strSql).Tables[0].Rows[0][0]);
                        if (count > 0)
                        {
                            strSql = @"update Random_Exam set Is_Start=1 where random_exam_id=" + strId;
                            dbCenter.ExecuteNonQuery(strSql);
                        }
                        else
                        {
                            strSql = @"select count(*)  from Random_Exam_Computer_Server where Is_Start=2 and  random_exam_id=" + strId;
                            count = Convert.ToInt32(dbCenter.RunSqlDataSet(strSql).Tables[0].Rows[0][0]);
                            if (count == totalcount)
                            {
                                strSql = @"update Random_Exam set Is_Start=2 where random_exam_id=" + strId;
                                dbCenter.ExecuteNonQuery(strSql);
                            }
                        }
                    }
                }
            }
            else
            {
                isRefresh = false;
            }

            if(isRefresh)
            {
                 objBll.RandomExamRefresh();
            }

            SystemLogBLL objLogBll = new SystemLogBLL();
            objLogBll.WriteLog("“" + obj.ExamName + "”生成所有考试试卷");

            Response.Write("<script>top.returnValue='true';top.close();</script>");
        }

        private void GetPaperAfter()
        {
            // 根据 ProgressBar.htm 显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            ViewState["BeginTime"] = DateTime.Now.ToString();
            string strId = Request.QueryString.Get("RandomExamID");

            //获取当前考试的生成试卷的状态和次数
            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

            int year = obj.BeginTime.Year;
            int ExamCount = obj.MaxExamTimes;

            System.Threading.Thread.Sleep(10);
            string jsBlock = "<script>SetPorgressBar('正在计算生成试卷数量，请等待......','" + ((1 * 100) / ((double)1) + "'); </script>");
            Response.Write(jsBlock);
            Response.Flush();

            if (!PrjPub.IsServerCenter)
            {
                RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
                objArrangeBll.RefreshRandomExamArrange();
            }

            //RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
            //IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));
            //string strChooseID = "";
            //if (ExamArranges.Count > 0)
            //{
            //    strChooseID = ExamArranges[0].UserIds;
            //}
            //else
            //{
            //    strChooseID = "";
            //}

            //RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
            //IList<RandomExamResultCurrent> examResults = objResultCurrentBll.GetRandomExamResultInfo(Convert.ToInt32(strId));
            //for (int i = 0; i < examResults.Count; i++)
            //{
            //    strChooseID = ("," + strChooseID + ",").Replace("," + examResults[i].ExamineeId + ",", ",");
            //}

            //strChooseID = strChooseID.TrimStart(',').TrimEnd(',');

            string strChooseID = Request.QueryString.Get("addIds");

            if (strChooseID == string.Empty)
            {
                return;
            }

            string[] str = strChooseID.Split('|');

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正在计算生成试卷数量，请等待......','" + ((2 * 100) / ((double)2) + "'); </script>");
            Response.Write(jsBlock);
            Response.Flush();

            OracleAccess db = new OracleAccess();
            string strSql;

            RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
            //定义全局答卷对象List
            IList<RandomExamResultAnswerCurrent> randomExamResultAnswersCurrentAll = new List<RandomExamResultAnswerCurrent>();
            //定义一个考生一次答卷对象List
            IList<RandomExamResultAnswerCurrent> randomExamResultAnswers = null;

            System.Threading.Thread.Sleep(200);
            jsBlock = string.Empty;
            int progressNum = 1;
            for (int n = 1; n <= ExamCount; n++)
            {
                for (int m = 0; m < str.Length; m++)
                {
                    RandomExamResultCurrentBLL randomExamResultBLL = new RandomExamResultCurrentBLL();
                    RandomExamResultCurrent randomExamResult = new RandomExamResultCurrent();

                    randomExamResult.RandomExamId = int.Parse(strId);
                    randomExamResult.AutoScore = 0;
                    randomExamResult.BeginDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
                    randomExamResult.CurrentDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
                    randomExamResult.ExamTime = 0;
                    randomExamResult.EndDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
                    randomExamResult.Score = 0;
                    randomExamResult.OrganizationId = int.Parse(ConfigurationManager.AppSettings["StationID"]);
                    randomExamResult.Memo = "";
                    randomExamResult.StatusId = 0;
                    randomExamResult.AutoScore = 0;
                    randomExamResult.CorrectRate = 0;
                    randomExamResult.ExamineeId = int.Parse(str[m]);
                    randomExamResult.ExamSeqNo = n;

                    int nRandomExamResultPK = randomExamResultBLL.AddRandomExamResultCurrent(randomExamResult);
                    ViewState["RandomExamResultPK"] = nRandomExamResultPK;

                    strSql = "select a.* from Random_Exam_Arrange_Detail a "
                          + " where ','||User_Ids||',' like '%," + str[m] + ",%' "
                          + " and Random_Exam_ID=" + strId;
                    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];


                    //strSql = "insert into Random_Exam_Result_Detail(Random_Exam_Result_Detail_ID,"
                    //         + "Random_Exam_Result_ID,Random_Exam_ID,Employee_ID,Computer_Room_SEAT,Computer_Room_ID) "
                    //         + "values(Random_Exam_Result_Detail_SEQ.NextVal,"
                    //         + nRandomExamResultPK + ","
                    //         + randomExamResult.RandomExamId + ","
                    //         + randomExamResult.ExamineeId + ","
                    //         + "0," + dr["Computer_Room_ID"] + ") ";
                    //db.ExecuteNonQuery(strSql);


                    strSql = "insert into Random_Exam_Result_Detail_Temp(Random_Exam_Result_Detail_ID,"
                            + "Random_Exam_Result_ID,Random_Exam_ID,Employee_ID,Computer_Room_SEAT,Computer_Room_ID,Is_Remove) "
                            + "values(Random_Exam_Result_Detail_SEQ.NextVal,"
                            + nRandomExamResultPK + ","
                            + randomExamResult.RandomExamId + ","
                            + randomExamResult.ExamineeId + ","
                            + "0," + dr["Computer_Room_ID"] + ",0) ";
                    db.ExecuteNonQuery(strSql);

                    RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
                    RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
                    RandomExamStrategyBLL strategyBLL = new RandomExamStrategyBLL();

                    IList<RandomExamSubject> randomExamSubjects =
                        subjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));

                    if (randomExamSubjects != null)
                    {
                        Hashtable hashTableItemIds = new Hashtable();
                        Hashtable htSubjectItemIds = new Hashtable();
                        for (int i = 0; i < randomExamSubjects.Count; i++)
                        {
                            RandomExamSubject paperSubject = randomExamSubjects[i];
                            int nSubjectId = paperSubject.RandomExamSubjectId;
                            //  int nItemCount = paperSubject.ItemCount;

                            IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
                            for (int j = 0; j < strategys.Count; j++)
                            {
                                int nStrategyId = strategys[j].RandomExamStrategyId;
                                int nItemCount = strategys[j].ItemCount;
                                IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId, year);

                                // IList<RandomExamItem> itemList = randomItemBLL.GetItemsBySubjectId(nSubjectId);

                                Random ObjRandom = new Random();
                                Hashtable hashTable = new Hashtable();
                                Hashtable hashTableCount = new Hashtable();
                                int index = 0;
                                while (hashTable.Count < nItemCount)
                                {
                                    int k = ObjRandom.Next(itemList.Count);
                                    hashTableCount[index] = k;
                                    index = index + 1;
                                    int itemID = itemList[k].ItemId;
                                    int examItemID = itemList[k].RandomExamItemId;
                                    if (!hashTableItemIds.ContainsKey(itemID))
                                    {
                                        hashTable[examItemID] = examItemID;
                                        hashTableItemIds[itemID] = itemID;
                                        htSubjectItemIds[examItemID] = examItemID;
                                    }
                                    //if (hashTableCount.Count == itemList.Count && hashTable.Count < nItemCount)
                                    //{
                                    //    SessionSet.PageMessage = "随机考试在设定的取题范围内的试题量不够，请重新设置取题范围！";
                                    //    return;
                                    //}
                                }
                            }
                        }
                        randomExamResultAnswers = new List<RandomExamResultAnswerCurrent>();
                        foreach (int key in htSubjectItemIds.Keys)
                        {
                            string strItemId = htSubjectItemIds[key].ToString();

                            RandomExamItem item = randomItemBLL.GetRandomExamItem(Convert.ToInt32(strItemId), year);

                            string nowSelectAnswer = string.Empty;
                            string nowStandardAnswer = string.Empty;
                            if (item.TypeId != PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                Pub.GetNowAnswer(item, out nowSelectAnswer, out nowStandardAnswer);
                            }

                            RandomExamResultAnswerCurrent randomExamResultAnswer = new RandomExamResultAnswerCurrent();
                            randomExamResultAnswer.RandomExamResultId = nRandomExamResultPK;
                            randomExamResultAnswer.RandomExamItemId = int.Parse(strItemId);
                            randomExamResultAnswer.JudgeStatusId = 0;
                            randomExamResultAnswer.JudgeRemark = string.Empty;
                            randomExamResultAnswer.ExamTime = 0;
                            randomExamResultAnswer.Answer = string.Empty;
                            randomExamResultAnswer.SelectAnswer = nowSelectAnswer;
                            randomExamResultAnswer.StandardAnswer = nowStandardAnswer;
                            randomExamResultAnswerBLL.AddExamResultAnswerCurrent(randomExamResultAnswer);

                            //完型填空子题
                            IList<RandomExamItem> randomExamItems = randomItemBLL.GetItemsByParentItemID(item.ItemId, obj.RandomExamId, year);
                            foreach (RandomExamItem randomExamItem in randomExamItems)
                            {
                                Pub.GetNowAnswer(randomExamItem, out nowSelectAnswer, out nowStandardAnswer);

                                randomExamResultAnswer = new RandomExamResultAnswerCurrent();
                                randomExamResultAnswer.RandomExamResultId = nRandomExamResultPK;
                                randomExamResultAnswer.RandomExamItemId = randomExamItem.RandomExamItemId;
                                randomExamResultAnswer.JudgeStatusId = 0;
                                randomExamResultAnswer.JudgeRemark = string.Empty;
                                randomExamResultAnswer.ExamTime = 0;
                                randomExamResultAnswer.Answer = string.Empty;
                                randomExamResultAnswer.SelectAnswer = nowSelectAnswer;
                                randomExamResultAnswer.StandardAnswer = nowStandardAnswer;
                                randomExamResultAnswerBLL.AddExamResultAnswerCurrent(randomExamResultAnswer);
                            }

                            System.Threading.Thread.Sleep(10);
                            jsBlock = "<script>SetPorgressBar('正在生成试卷，请等待......','" + ((progressNum * 100) / ((double)ExamCount * str.Length * htSubjectItemIds.Count)).ToString("0.00") + "'); </script>";
                            Response.Write(jsBlock);
                            Response.Flush();

                            progressNum++;
                        }
                    }
                    else
                    {
                        SessionSet.PageMessage = "未找到记录！";
                    }
                }
            }

            //临时添加考生无需更改考试状态，因为考试状态肯定是正在进行和生成试卷
            //objBll.UpdateHasPaper(Convert.ToInt32(strId), PrjPub.ServerNo, true);
            ////如果考试是随到随考，考试状态自动变为正在进行
            //if (obj.StartMode == 1)
            //{
            //    objBll.UpdateIsStart(Convert.ToInt32(strId), PrjPub.ServerNo, 1);
            //}

            SystemLogBLL objLogBll = new SystemLogBLL();
            objLogBll.WriteLog("“" + obj.ExamName + "”生成新增考生试卷");

            Response.Write("<script>top.returnValue='true';top.close();</script>");
        }

        private void UploadPaper()
        {
            // 根据 ProgressBar.htm 显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            string strId = Request.QueryString.Get("RandomExamID");
            //获取当前考试的生成试卷的状态和次数
            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

            string typeid = Request.QueryString.Get("typeid");
            string strSql, strKey = "0";
            OracleAccess db = new OracleAccess();

            int orgId = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);

            try
            {
                RandomExamResultBLL objResultBll = new RandomExamResultBLL();

                strSql = "select SYNCHRONIZE_LOG_SEQ.NextVal@link_sf from dual";
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                strKey = dr[0].ToString();

                strSql = "insert into  SYNCHRONIZE_LOG@link_sf values(" + strKey + ","
                    + orgId + ",6,sysdate,null,1," + PrjPub.ServerNo + ")";
                db.ExecuteNonQuery(strSql);

                strSql =
                                 @"select Examinee_ID,Random_Exam_Result_ID from Random_Exam_Result@link_sf where Random_Exam_ID=" + obj.RandomExamId + @"
                                      and org_id=" + PrjPub.StationID + @" and examinee_id in (select employee_Id from Random_Exam_Result_Detail a
                                      inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
                                      inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID
                                      where to_number(c.Computer_Server_No)=" + PrjPub.ServerNo + @" and a.Random_Exam_ID=" + obj.RandomExamId + ")";
                DataSet dsResult = db.RunSqlDataSet(strSql);


                IList<RandomExamResult> randomExamResults = objResultBll.GetRandomExamResultByExamID(obj.RandomExamId);
                int count = randomExamResults.Count + dsResult.Tables[0].Rows.Count + 1;
                System.Threading.Thread.Sleep(10);
                string jsBlock = "<script>SetPorgressBar('正在上传考试答卷，请等待......','" + ((1 * 100) / ((double)count)).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();


                string strUrl = "ftp://" + PrjPub.ServerIP + "/Photo";
                Uri directoryToDownload = new Uri(strUrl);
                FtpClient ftpSession = FtpClient.GetFtpClient(directoryToDownload.Host);

                if (ftpSession == null)
                {
                    Pub.AddFtp();
                    ftpSession = FtpClient.GetFtpClient(directoryToDownload.Host);
                }

                ftpSession.MakeDirectory("/Photo/" + obj.RandomExamId);

                int progressNum = 2;

                foreach (DataRow drResult in dsResult.Tables[0].Rows)
                {
                    ftpSession.DeleteFile(new Uri(strUrl + "/" + obj.RandomExamId + "/" + drResult["Examinee_ID"] + "_" + drResult["Random_Exam_Result_ID"] + "_00.jpg"));
                    ftpSession.DeleteFile(new Uri(strUrl + "/" + obj.RandomExamId + "/" + drResult["Examinee_ID"] + "_" + drResult["Random_Exam_Result_ID"] + "_01.jpg"));
                    ftpSession.DeleteFile(new Uri(strUrl + "/" + obj.RandomExamId + "/" + drResult["Examinee_ID"] + "_" + drResult["Random_Exam_Result_ID"] + "_02.jpg"));
                    ftpSession.DeleteFile(new Uri(strUrl + "/" + obj.RandomExamId + "/" + drResult["Examinee_ID"] + "_" + drResult["Random_Exam_Result_ID"] + "_03.jpg"));

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在上传考试答卷，请等待......','" + ((progressNum * 100) / ((double)count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    progressNum++;
                }

                //先删除路局的考试成绩和答卷
                objResultBll.DeleteRandomExamResultServer(obj.RandomExamId);

                foreach (RandomExamResult randomExamResult in randomExamResults)
                {
                    if(randomExamResult.IsTemp == 1)
                    {
                        //从中间提交表到正式表
                        objResultBll.RemoveResultAnswerTemp(randomExamResult.RandomExamResultId);
                    }

                    //获取路局的主键ID
                    strSql = "select Random_Exam_Result_Seq.Nextval@link_sf from dual";
                    DataRow drSeq = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                    int serverId = Convert.ToInt32(drSeq[0]);

                    //将成绩插入路局
                    objResultBll.InsertRandomExamResultServer(randomExamResult.RandomExamResultId, serverId,obj.RandomExamId);

                    if (typeid == "2")
                    {
                        //将答卷插入路局
                        objResultBll.InsertRandomExamResultAnswerServer(obj.RandomExamId, randomExamResult.RandomExamResultId, serverId);
                    }

                    strSql = "select * from Random_Exam_Result_Photo where Random_Exam_ID=" + obj.RandomExamId +
                             " and Random_Exam_Result_ID=" + randomExamResult.RandomExamResultId;
                    DataSet ds = db.RunSqlDataSet(strSql);
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        DataRow drPhoto = ds.Tables[0].Rows[0];

                        int employeeId = Convert.ToInt32(drPhoto["Employee_ID"]);
                        Uri ftpUri = new Uri(strUrl + "/" + obj.RandomExamId + "/");
                        if(drPhoto["FingerPrint"] != DBNull.Value)
                        {
                            SavePhotoToServer(employeeId,(byte[])drPhoto["FingerPrint"],0,serverId,ftpSession,ftpUri);
                        }

                        if (drPhoto["Photo1"] != DBNull.Value)
                        {
                            SavePhotoToServer(employeeId, (byte[])drPhoto["Photo1"], 1, serverId, ftpSession, ftpUri);
                        }

                        if (drPhoto["Photo2"] != DBNull.Value)
                        {
                            SavePhotoToServer(employeeId, (byte[])drPhoto["Photo2"], 2, serverId, ftpSession, ftpUri);
                        }

                        if (drPhoto["Photo3"] != DBNull.Value)
                        {
                            SavePhotoToServer(employeeId, (byte[])drPhoto["Photo3"], 3, serverId, ftpSession, ftpUri);
                        }
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在上传考试答卷，请等待......','" + ((progressNum * 100) / ((double)count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    progressNum++;
                }

                if (typeid == "1")
                {
                    //只上传成绩须更新时间
                    strSql = "update Random_Exam_Computer_Server@link_sf set  "
                                    + "Last_Upload_Date=sysdate   where random_exam_id=" + obj.RandomExamId
                                    + " and Computer_server_no=" + PrjPub.ServerNo;
                    db.ExecuteNonQuery(strSql);
                }
                else
                {
                    //如果考试已经结束上传，将上传标志置为已经上传
                    if (obj.IsStart == 2)
                    {
                        objBll.UpdateIsUpload(obj.RandomExamId, PrjPub.ServerNo, 1);
                    }
                    else
                    {
                        objBll.UpdateIsUpload(obj.RandomExamId, PrjPub.ServerNo, 0);
                    }
                }

                strSql = "update SYNCHRONIZE_LOG@link_sf set SYNCHRONIZE_STATUS_ID=2,End_Time=sysdate where SYNCHRONIZE_LOG_ID=" + strKey;
                db.ExecuteNonQuery(strSql);
            }
            catch(Exception ex)
            {
                strSql = "update SYNCHRONIZE_LOG@link_sf set SYNCHRONIZE_STATUS_ID=3,End_Time=sysdate where SYNCHRONIZE_LOG_ID=" + strKey;
                db.ExecuteNonQuery(strSql);
                Response.Write("<script>alert('"+ ex.Message.Replace("\n","\r\n") +"');window.close();</script>");
                return;
            }

            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }

        public System.Drawing.Image FromBytes(byte[] bs)
        {
            if (bs == null) return null;
            try
            {
                MemoryStream ms = new MemoryStream(bs);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Close();
                return returnImage;

            }
            catch { return null; }
        }

        private void SavePhotoToServer(int employeeid, byte[] ph, int index, int serverId, FtpClient ftpSession, Uri ftpUri)
        {
            string uploadPath = Server.MapPath("/RailExamBao/Photo/") + employeeid + "_" + serverId + "_";

            string fileName = string.Empty;
            fileName = uploadPath + "0" + index + ".jpg";
            System.Drawing.Image image = FromBytes(ph);
            if (image != null)
            {
                System.Drawing.Image thumbnail = image.GetThumbnailImage(170, 130, null, IntPtr.Zero);
                //保存本地
                thumbnail.Save(fileName, ImageFormat.Jpeg);

                ftpSession.UploadFile(fileName, ftpUri);

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }
    }
}
