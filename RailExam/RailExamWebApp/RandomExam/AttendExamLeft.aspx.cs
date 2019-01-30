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
using System.Xml;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class AttendExamLeft : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string employeeId = Request.QueryString.Get("EmployeeID");

                DataSet ds = Pub.GetPhotoDateSet(employeeId);

                if(ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        myImagePhoto.ImageUrl = "../RandomExamTai/ShowImage.aspx?EmployeeID=" + employeeId;
                    }
                    else
                    {
                        myImagePhoto.ImageUrl = "../images/empty.jpg";
                    }
                }
                else
                {
                    myImagePhoto.ImageUrl = "../images/empty.jpg";
                }
            }
        }


        public void FillPaper()
        {
            string strId = Request.QueryString.Get("id");

            RandomExamBLL randomExamBLL = new RandomExamBLL();
            RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(int.Parse(strId));
            ViewState["Year"] = randomExam.BeginTime.Year.ToString();

            RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
            RailExam.Model.RandomExamResultCurrent randomExamResult = objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(Request.QueryString.Get("employeeID")), Convert.ToInt32(strId));

            int RandomExamId = Convert.ToInt32(strId);
            int randomExamResultId = randomExamResult.RandomExamResultId;

            RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
            RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
            IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

            RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
            IList<RandomExamResultAnswerCurrent> examResultAnswers = new List<RandomExamResultAnswerCurrent>();
            examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersCurrent(randomExamResultId);

            OracleAccess db = new OracleAccess();

            if (randomExamSubjects != null)
            {
                for (int i = 0; i < randomExamSubjects.Count; i++)
                {
                    RandomExamSubject paperSubject = randomExamSubjects[i];
                    IList<RandomExamItem> PaperItems = new List<RandomExamItem>();
                    PaperItems = randomItemBLL.GetItemsCurrent(paperSubject.RandomExamSubjectId, randomExamResultId, Convert.ToInt32(ViewState["Year"].ToString()));

                    Response.Write("<br>");
                    Response.Write("<span class='StudentLeftInfo'><b> 第" + GetNo(i) + "大题：" + paperSubject.SubjectName + "</b></span>");
                    Response.Write("<br>");

                    if (PaperItems != null)
                    {
                        Response.Write("<table width='100%'  border='1'>");
                        int z = 1;
                        int tempK = 0;
                        int count = 1;
                        for (int j = 0; j < PaperItems.Count; j++)
                        {
                            RandomExamItem paperItem = PaperItems[j];
                            int k = j + 1;


                            if (paperItem.TypeId != PrjPub.ITEMTYPE_FILLBLANKDETAIL && paperItem.TypeId != PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                z = 1;

                                if (k % 5 == 1)
                                {
                                    Response.Write("</tr >");
                                    Response.Write("<tr><td class='StudentTableInfo' id='Item" + i + j + "' >"
                                        + "<a href='AttendExamNew.aspx?id=" + strId + "&employeeID=" + Request.QueryString.Get("employeeID")
                                        + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + k + "</b></a></td>");
                                }
                                else
                                {
                                    Response.Write("<td class='StudentTableInfo' id='Item" + i + j + "' >"
                                        + "<a href='AttendExamNew.aspx?id=" + strId + "&employeeID=" + Request.QueryString.Get("employeeID")
                                        + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + k + "</b></a></td>");
                                }
                            }
                            else
                            {
                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                                {
                                    z = 1;
                                    tempK++;
                                }

                                if (count % 3 == 1)
                                {
                                    Response.Write("</tr >");
                                    Response.Write("<tr><td class='StudentTableInfo' id='Item" + i + j + "' >"
                                        + "<a href='AttendExamNew.aspx?id=" + strId + "&employeeID=" + Request.QueryString.Get("employeeID")
                                        + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + tempK + "-(" + z + ")</b></a></td>");
                                }
                                else
                                {
                                    Response.Write("<td class='StudentTableInfo' id='Item" + i + j + "' >"
                                        + "<a href='AttendExamNew.aspx?id=" + strId + "&employeeID=" + Request.QueryString.Get("employeeID")
                                        + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + tempK + "-(" + z + ")</b></a></td>");
                                }

                                z++;
                                count++;
                            }
                        }

                        Response.Write("</tr >");
                        Response.Write("</table>");
                    }
                }

                //ClientScript.RegisterStartupScript(GetType(), "StartStyle", "<script>StartStyle()</script>");
            }
            else
            {
                SessionSet.PageMessage = "未找到记录！";
            }
        }

        private string GetNo(int i)
        {
            string strReturn = "";
            switch (i.ToString())
            {
                case "0":
                    strReturn = "一";
                    break;
                case "1":
                    strReturn = "二";
                    break;
                case "2":
                    strReturn = "三";
                    break;
                case "3":
                    strReturn = "四";
                    break;
                case "4":
                    strReturn = "五";
                    break;
                case "5":
                    strReturn = "六";
                    break;
                case "6":
                    strReturn = "七";
                    break;
                case "7":
                    strReturn = "八";
                    break;
                case "8":
                    strReturn = "九";
                    break;
                case "9":
                    strReturn = "十";
                    break;
                case "10":
                    strReturn = "十一";
                    break;
                case "11":
                    strReturn = "十二";
                    break;
                case "12":
                    strReturn = "十三";
                    break;
                case "13":
                    strReturn = "十四";
                    break;
                case "14":
                    strReturn = "十五";
                    break;
                case "15":
                    strReturn = "十六";
                    break;
                case "16":
                    strReturn = "十七";
                    break;
                case "17":
                    strReturn = "十八";
                    break;
                case "18":
                    strReturn = "十九";
                    break;
                case "19":
                    strReturn = "二十";
                    break;
            }
            return strReturn;
        }

        private string GetDate()
        {
            string strDate = DateTime.Now.Year.ToString("00") + DateTime.Now.Month.ToString("00") +
                 DateTime.Now.Day.ToString("00")
                 + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") +
                 DateTime.Now.Second.ToString("00");
            return strDate;
        }

        protected void imageCallback_Callback(object sender, CallBackEventArgs e)
        {
            try
            {
                string examId = e.Parameters[0];
                string employeeid = e.Parameters[1];
                byte[] b = Convert.FromBase64String(e.Parameters[2]);

                MemoryStream ms = new MemoryStream(b);
                System.Drawing.Image currentImage = System.Drawing.Image.FromStream(ms);
                System.Drawing.Image thumbnail = currentImage.GetThumbnailImage(170, 130, null, IntPtr.Zero);

                MemoryStream memoryStream = new MemoryStream();
                thumbnail.Save(memoryStream, ImageFormat.Jpeg);
                byte[] byteImage = new byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(byteImage, 0, Convert.ToInt32(memoryStream.Length));
                memoryStream.Close();

                //FileStream fs = new FileStream(filename, FileMode.Open);
                //int len = int.Parse(fs.Length.ToString());
                //byte[] byteImage = new Byte[len];
                //fs.Read(byteImage, 0, len);
                //fs.Close();

                //添加
                XmlDocument doc = new XmlDocument();
                doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
                XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
                string value = node.Value;

                if (value == "Oracle")
                {
                    OracleParameter para1 = new OracleParameter("p_photo", OracleType.Blob);
                    OracleParameter para2 = new OracleParameter("r_id", OracleType.Number);
                    OracleParameter para3 = new OracleParameter("r_exam_id", OracleType.Number);
                    para1.Value = byteImage;
                    para2.Value = Convert.ToInt32(employeeid);
                    para3.Value = Convert.ToInt32(examId);

                    IDataParameter[] paras = new IDataParameter[] {para1, para2, para3};

                    Pub.RunAddProcedureBlob(false, "USP_Photo_U", paras, byteImage);
                }
            }
            catch
            {
                int i = 1;
            }
        }
    }
}
