using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Exam
{
    public partial class ExamSuccess : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    string strExamType = Request.QueryString.Get("ExamType");
                    string strExamResultID = Request.QueryString.Get("ExamResultID");
                    if (strExamType == "1")
                    {
                        RandomExamResultBLL objBll = new RandomExamResultBLL();
                        RailExam.Model.RandomExamResult  obj = new RandomExamResult();
                        if(string.IsNullOrEmpty(Request.QueryString.Get("IsResult")))
                        {
                            //正常提交
                             obj =
                                objBll.GetRandomExamResultTemp(Convert.ToInt32(strExamResultID));
                        }
                        else
                        {
                            //后台终止考试或结束考试
                            obj =
                                objBll.GetRandomExamResult(Convert.ToInt32(strExamResultID));
                        }
                        RandomExamBLL objExamBll = new RandomExamBLL();
                        RailExam.Model.RandomExam objRandomExam = objExamBll.GetExam(obj.RandomExamId);

                        if (objRandomExam.CanSeeScore)
                        {
                            hfIsShowResult.Value = "1";
                            lblScore.Text = "成绩：" + System.String.Format("{0:0.##}", obj.Score) + "分";

                            if (objRandomExam.CanSeeAnswer)
                            {
                                hfIsShowAnswer.Value = "1";
                            }

                            //if (lblScore.Text == string.Empty)
                            //{
                            //    lblScore.Text = "成绩：" + String.Format("{0:0.##}", Request.QueryString.Get("nowScore")) + "分";
                            //}
                        }

                        if(objRandomExam.IsPublicScore)
                        {
                            if (obj.Score >= objRandomExam.PassScore)
                            {
                                lblPass.Text = "祝贺您本次考试合格通过！";
                            }
                            else
                            {
                                lblPass.Text = "您本次考试未合格未通过！";
                            }
                        }
                        else
                        {
                            lblPass.Text = "";
                        }

                        if (string.IsNullOrEmpty(Request.QueryString.Get("IsResult")))
                        {
                            //正常提交
                            try
                            {
                                OracleAccess dbPhoto = new OracleAccess();
                                string strSql = "select * from Random_Exam_Result_Detail_Temp where Random_Exam_Result_ID=" +
                                         strExamResultID + " and Random_Exam_ID=" + obj.RandomExamId;
                                DataSet dsPhoto = dbPhoto.RunSqlDataSet(strSql);

                                if (dsPhoto.Tables[0].Rows.Count > 0)
                                {
                                    DataRow drPhoto = dsPhoto.Tables[0].Rows[0];
                                    int employeeId = Convert.ToInt32(drPhoto["Employee_ID"]);
                                    if (drPhoto["FingerPrint"] != DBNull.Value)
                                    {
                                        Pub.SavePhotoToLocal(obj.RandomExamId, employeeId, (byte[])drPhoto["FingerPrint"], 0, Convert.ToInt32(strExamResultID));
                                    }
                                    if (drPhoto["Photo1"] != DBNull.Value)
                                    {
                                        Pub.SavePhotoToLocal(obj.RandomExamId, employeeId, (byte[])drPhoto["Photo1"], 1, Convert.ToInt32(strExamResultID));
                                    }
                                    if (drPhoto["Photo2"] != DBNull.Value)
                                    {
                                        Pub.SavePhotoToLocal(obj.RandomExamId, employeeId, (byte[])drPhoto["Photo2"], 2, Convert.ToInt32(strExamResultID));
                                    }
                                    if (drPhoto["Photo3"] != DBNull.Value)
                                    {
                                        Pub.SavePhotoToLocal(obj.RandomExamId, employeeId, (byte[])drPhoto["Photo3"], 3, Convert.ToInt32(strExamResultID));
                                    }
                                }
                            }
                            catch
                            {
                                hfOrgID.Value = ConfigurationManager.AppSettings["StationID"];
                            }
                        }
                    }

                    hfOrgID.Value = ConfigurationManager.AppSettings["StationID"];
                }
                catch
                {
                    lblPass.Text = "";
                }
            }
        }
    }
}
