using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class ResetRandomExam : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("examID")));

                hfName.Value = obj.ExamName;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string examId = Request.QueryString.Get("examID");
                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(examId));
                if (objRandomExam.IsStart != 2)
                {
                    SessionSet.PageMessage = "考试还未结束不能生成补考试卷！";
                    return;
                }

                //OrganizationBLL OrgBll = new OrganizationBLL();
                //Organization org = OrgBll.GetOrganization(objRandomExam.OrgId);
                //if (org.SuitRange != 1 && !objRandomExam.IsUpload)
                //{
                //    Grid1.DataBind();
                //    SessionSet.PageMessage = "考试为站段考试，还未上传考试成绩不能生成补考试卷！";
                //    return;
                //}

                if (objRandomExam.HasTrainClass)
                {
                    string strTrainClassID = "";
                    RandomExamTrainClassBLL trainClassBLL = new RandomExamTrainClassBLL();
                    IList<RandomExamTrainClass> trainClasses =
                        trainClassBLL.GetRandomExamTrainClassByRandomExamID(Convert.ToInt32(examId));
                    foreach (RandomExamTrainClass trainClass in trainClasses)
                    {
                        if (strTrainClassID == "")
                        {
                            strTrainClassID = "'" + trainClass.TrainClassID + "'";
                        }
                        else
                        {
                            strTrainClassID = strTrainClassID + ",'" + trainClass.TrainClassID + "'";
                        }
                    }
                }

                string OrganizationName = "";
                string strExamineeName = "";
                decimal dScoreLower = 0;
                decimal dScoreUpper = 1000;

                IList<RandomExamResult> examResults = null;
                RandomExamResultBLL bllExamResult = new RandomExamResultBLL();

                examResults = bllExamResult.GetRandomExamResults(objRandomExam.RandomExamId, OrganizationName, "",strExamineeName, string.Empty, dScoreLower,
                            dScoreUpper, objRandomExam.OrgId);


                string strID = string.Empty;
                string strNoPass = string.Empty;
                foreach (RandomExamResult result in examResults)
                {
                    if (strID == string.Empty)
                    {
                        strID = result.ExamineeId.ToString();
                    }
                    else
                    {
                        strID = strID + "," + result.ExamineeId;
                    }

                    //当补考考生不为未参加考试考生时
                    if (ddlSelect.SelectedValue != "2")
                    {
                        if (result.Score < objRandomExam.PassScore)
                        {
                            if (strNoPass == string.Empty)
                            {
                                strNoPass = result.ExamineeId.ToString();
                            }
                            else
                            {
                                strNoPass = strNoPass + "," + result.ExamineeId;
                            }
                        }
                    }
                }

                RandomExamArrangeBLL objArrangebll = new RandomExamArrangeBLL();
                IList<RandomExamArrange> objArrangeList =
                            objArrangebll.GetRandomExamArranges(objRandomExam.RandomExamId);
                string strChooseID = string.Empty;
                if (objArrangeList.Count > 0)
                {
                    strChooseID = objArrangeList[0].UserIds;
                }
                string[] str = strChooseID.Split(',');

                string strNoResult = string.Empty;
                //当补考考生不为不及格考生时
                if (ddlSelect.SelectedValue != "1")
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (("," + strID + ",").IndexOf(("," + str[i] + ",")) < 0)
                        {
                            if (strNoResult == string.Empty)
                            {
                                strNoResult = str[i];
                            }
                            else
                            {
                                strNoResult = strNoResult + "," + str[i];
                            }
                        }
                    }
                }

                string strTotal = string.Empty;
                if (strNoResult == string.Empty && strNoPass == string.Empty)
                {
                    strTotal = string.Empty;
                }
                else if (strNoResult == string.Empty && strNoPass != string.Empty)
                {
                    strTotal = strNoPass;
                }
                else if (strNoResult != string.Empty && strNoPass == string.Empty)
                {
                    strTotal = strNoResult;
                }
                else if (strNoResult != string.Empty && strNoPass != string.Empty)
                {
                    strTotal = strNoPass + "," + strNoResult;
                }

                if (strTotal == string.Empty)
                {
                    SessionSet.PageMessage = "所选考试无考试不及格和未参加考试学员，不需生成补考考试！";
                    return;
                }

                int nowExamID = objBll.AddResetRandomExam(objRandomExam.RandomExamId);

                if (nowExamID == 0)
                {
                    SessionSet.PageMessage = "复制失败！";
                    return;
                }

                RandomExamArrange objArrange = new RandomExamArrange();
                objArrange.RandomExamId = nowExamID;
                objArrange.UserIds = strTotal;
                objArrange.Memo = string.Empty;
                int newArrangeId = objArrangebll.AddRandomExamArrange(objArrange);

                OracleAccess db = new OracleAccess();
                string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + objRandomExam.RandomExamId;
                DataSet ds = db.RunSqlDataSet(strSql);

                str = strTotal.Split(',');

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string strArrange = string.Empty;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (("," + dr["User_Ids"] + ",").IndexOf("," + str[i] + ",") >= 0)
                        {
                            if (strArrange == string.Empty)
                            {
                                strArrange = str[i];
                            }
                            else
                            {
                                strArrange += "," + str[i];
                            }
                        }
                    }

                    if (strArrange != string.Empty)
                    {
                        XmlDocument doc = new XmlDocument();
                        //Request.PhysicalApplicationPath取得config文件路径
                        doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
                        XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
                        string value = node.Value;
                        int id = 0;
                        if (value == "Oracle")
                        {
                            OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                            OracleParameter para2 = new OracleParameter("p_random_exam_arrange_de_id", OracleType.Number);
                            para2.Direction = ParameterDirection.Output;
                            OracleParameter para3 = new OracleParameter("p_random_exam_arrange_Id", OracleType.Number);
                            para3.Value = newArrangeId;
                            OracleParameter para4 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
                            para4.Value = nowExamID;
                            OracleParameter para5 = new OracleParameter("p_computer_room_id", OracleType.Number);
                            para5.Value = Convert.ToInt32(dr["Computer_Room_ID"].ToString());


                            IDataParameter[] paras = new IDataParameter[] { para1, para2, para3, para4, para5 };
                            id =
                                Pub.RunAddProcedure(false, "USP_RANDOM_EXAM_ARRANGE_DE_I", paras,
                                                System.Text.Encoding.Unicode.GetBytes(strArrange));
                        }
                    }
                }

                //向Random_Exam_Computer_Server 机房考试状态表 插入记录
                strSql = "select c.Computer_Server_No from Random_Exam_Arrange_Detail a "
                         + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                         + " inner join Computer_Server c on c.Computer_Server_ID=b.Computer_Server_ID "
                         + " where a.Random_Exam_ID=" + nowExamID;
                DataSet dsComputer = db.RunSqlDataSet(strSql);

                string serverNo = "";
                foreach (DataRow dr in dsComputer.Tables[0].Rows)
                {
                    if (serverNo != dr["Computer_Server_No"].ToString())
                    {
                        serverNo = dr["Computer_Server_No"].ToString();
                        strSql = "insert into Random_Exam_Computer_Server"
                         + "(Random_Exam_ID,Computer_Server_No,Status_ID,Is_Start,Has_Paper,"
                         + "Random_Exam_Code,Is_Upload,DownLoaded) "
                         + "values (" + nowExamID + "," + serverNo + ",0,0,0,'',0,0)";
                        db.ExecuteNonQuery(strSql);
                    }
                }

                Response.Write("<script>top.returnValue='true';top.close();</script>");
            }
            catch
            {
                SessionSet.PageMessage = "复制失败！";
            } 
        }
    }
}
