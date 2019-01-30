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
                            //�����ύ
                             obj =
                                objBll.GetRandomExamResultTemp(Convert.ToInt32(strExamResultID));
                        }
                        else
                        {
                            //��̨��ֹ���Ի��������
                            obj =
                                objBll.GetRandomExamResult(Convert.ToInt32(strExamResultID));
                        }
                        RandomExamBLL objExamBll = new RandomExamBLL();
                        RailExam.Model.RandomExam objRandomExam = objExamBll.GetExam(obj.RandomExamId);

                        if (objRandomExam.CanSeeScore)
                        {
                            hfIsShowResult.Value = "1";
                            lblScore.Text = "�ɼ���" + System.String.Format("{0:0.##}", obj.Score) + "��";

                            if (objRandomExam.CanSeeAnswer)
                            {
                                hfIsShowAnswer.Value = "1";
                            }

                            //if (lblScore.Text == string.Empty)
                            //{
                            //    lblScore.Text = "�ɼ���" + String.Format("{0:0.##}", Request.QueryString.Get("nowScore")) + "��";
                            //}
                        }

                        if(objRandomExam.IsPublicScore)
                        {
                            if (obj.Score >= objRandomExam.PassScore)
                            {
                                lblPass.Text = "ף�������ο��Ժϸ�ͨ����";
                            }
                            else
                            {
                                lblPass.Text = "�����ο���δ�ϸ�δͨ����";
                            }
                        }
                        else
                        {
                            lblPass.Text = "";
                        }

                        if (string.IsNullOrEmpty(Request.QueryString.Get("IsResult")))
                        {
                            //�����ύ
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
