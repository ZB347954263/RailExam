using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
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
    public partial class CheckAttendExamNew : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["BeginTime"] = DateTime.Now.ToString();
                //记录当前考试所在地的OrgID
                ViewState["OrgID"] = ConfigurationManager.AppSettings["StationID"];
                ViewState["EmployeeID"] = Request.QueryString.Get("employeeID");

                string strSql = "select * from System_Exam where System_Exam_ID=1";
                OracleAccess db = new OracleAccess();
                DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

                int examtime;
                int examNumber;
                if (dt.Rows.Count > 0)
                {
                    examtime = Convert.ToInt32(dt.Rows[0]["Exam_Time"]);
                    examNumber = Convert.ToInt32(dt.Rows[0]["Exam_Number"]);
                }
                else
                {
                    examtime = 10;
                    examNumber = 10;
                }
                ViewState["ExamTime"] = examtime.ToString();
                ViewState["ExamNumber"] = examNumber.ToString();

                HiddenFieldExamTime.Value = DateTime.Now.AddMinutes(examtime).ToString();
                HfExamTime.Value = (examtime * 60).ToString();

                HiddenFieldMaxCount.Value = "1";

            }
        }

        protected void FillPaper()
        {
            OracleAccess db = new OracleAccess();
            RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
            IList<RandomExamItem> PaperItems = randomItemBLL.GetItemsCurrentCheck();

            int totalCount = Convert.ToInt32(ViewState["ExamNumber"]);
            int otherCount = totalCount/4;
            int singleCount = totalCount - otherCount*3;

            int m = 4;
            if(otherCount==0)
            {
                m = 1;
            }

            for (int i = 0; i < m; i++)
            {
                int itemCount=0;
                int b=0, e=0;
                string typeName=string.Empty;

                if(i==0)
                {
                    itemCount = singleCount;
                    typeName = "单选";
                    b = 0;
                    e = singleCount;
                }
                else if(i==1)
                {
                    itemCount = otherCount;
                    typeName = "多选";
                    b = singleCount;
                    e = singleCount + otherCount;
                }
                else if (i == 2)
                {
                    itemCount = otherCount;
                    typeName = "判断";
                    b = singleCount+otherCount;
                    e = singleCount + otherCount*2;
                }
                else if (i == 3)
                {
                    itemCount = otherCount;
                    typeName = "综合选择题";
                    b = singleCount + otherCount * 2;
                    e = singleCount + otherCount*3;
                }

                Response.Write("<table width='95%' class='ExamContent'>");
                Response.Write(" <tr> <td class='ExamBigTitle'" );
                if(i==3)
                {
                    Response.Write(" colspan='3' ");
                }
                Response.Write(">" + GetNo(i) + "");
                Response.Write("、" + typeName + "");
                Response.Write("  （共" + itemCount + "题）</td></tr >");

                if (PaperItems != null)
                {
                    int z = 1;
                    int x = 1;
                    for (int j = b; j < e; j++)
                    {
                        if(j>=PaperItems.Count)
                        {
                            continue;
                        }
                        RandomExamItem paperItem = PaperItems[j];
                        int k = j + 1- b;

                        if (paperItem.TypeId != PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                        {
                            z = 1;

                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                k = x;
                                x++;

                                Response.Write("<tr><td id='Item" + i + j + "' class='ExamItem' colspan='3'><a name='Test" + i + j + "' id='Test" + i + j + "'></a>&nbsp;&nbsp;&nbsp;" + k +
                                     ".&nbsp; " + paperItem.Content +
                                     "&nbsp;&nbsp; </td></tr >");
                            }
                            else
                            {
                                Response.Write("<tr><td id='Item" + i + j + "' class='ExamItem' colspan='3'><a name='Test" + i + j + "' id='Test" + i + j + "'></a>&nbsp;&nbsp;&nbsp;" + k +
                                       ".&nbsp; " + paperItem.Content +
                                       "&nbsp;&nbsp;"
                                       + "<a href='#Test" + i + j + "' id='Empty" + i + j + "' onclick='clickEmpty(this)'style='cursor: hand;' title='清空选择'>"
                                       + "<img src='../images/clear.png'  style='border:0'/></a>"
                                       + "</td></tr >");
                            }
                        }

                        // 组织用户答案



                        if (paperItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)   //多选
                        {
                            string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                            for (int n = 0; n < strAnswer.Length; n++)
                            {
                                string strN = intToString(n + 1);
                                string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
                                               "-" + n.ToString();
                                string strName = i.ToString() + j.ToString();


                                Response.Write(
                                     "<tr><td class='ExamItemAnswer' colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='checkbox' id='Answer" +
                                     strij + "' name='Answer" + strName + "'><label for='Answer" + strij + "'> " + strN + "." + strAnswer[n] +
                                     "</label></td></tr>");
                            }
                        }
                        else if (paperItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperItem.TypeId == PrjPub.ITEMTYPE_JUDGE)    //单选
                        {
                            string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                            for (int n = 0; n < strAnswer.Length; n++)
                            {
                                string strN = intToString(n + 1);
                                string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
                                               "-" + n.ToString();
                                string strName = i.ToString() + j.ToString();

                                Response.Write(
                                    "<tr><td class='ExamItemAnswer' colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='Radio' id='RAnswer" +
                                    strij + "' name='RAnswer" + strName + "'> <label for='RAnswer" + strij + "' >" + strN + "." + strAnswer[n] +
                                    "</label></td></tr>");
                            }
                        }
                        else if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                        {
                            string str = "select * from Random_Exam_Item_"+
                                         DateTime.Today.Year + " where Random_Exam_ID="+ paperItem.RandomExamId 
                                         +" and  Parent_Item_ID=" + paperItem.ItemId+" order by Item_Index";
                            DataSet ds = db.RunSqlDataSet(str);

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                string[] strAnswer = dr["Select_Answer"].ToString().Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                   "-" + n.ToString()+"-"+z.ToString();
                                    string strName = i.ToString() + j.ToString()+z.ToString();

                                    if (n == 0)
                                    {
                                        int row = strAnswer.Length % 2 == 0 ? strAnswer.Length / 2 : strAnswer.Length / 2 + 1;
                                        Response.Write("<tr><td id='Item" + i + j +z+ "' class='ExamItem' style='width:10%' RowSpan='" + row + "'><a name='Test" + i + j + z+ "' id='Test" + i + j + z+ "'></a>"
                                        + "&nbsp;(" + z + ").&nbsp;"
                                        + "<a href='#Test" + i + j + z + "' id='Empty" + i + j + z + "' onclick='clickEmpty(this)'style='cursor: hand;' title='清空选择'>"
                                        + "<img src='../images/clear.png'  style='border:0'/></a>"
                                        + "</td>");
                                    }

                                    if (n % 2 == 0 && n != 0)
                                    {
                                        Response.Write("<tr>");
                                    }

                                    Response.Write(
                                        "<td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='Radio' id='RAnswer" +
                                        strij + "' name='RAnswer" + strName + "'><label for='RAnswer" + strij + "'> " + strN + "." + strAnswer[n] +
                                        "</label></td>");

                                    if (n % 2 == 1)
                                    {
                                        Response.Write("</tr>");
                                    }
                                }
                                z++;
                            }
                        }
                    }
                }
                Response.Write("</table>");
            }
            Response.Write(" <div class='ExamButton'><input id='btnClose' class='button' name='btnSave' type='button' value='提交答卷'  onclick='SaveRecord()'/> </div><br><br><br><br><br><br>");
            ClientScript.RegisterStartupScript(GetType(), "StartStyle", "<script>StartStyle()</script>");
        }

        #region Common function

        private string intToString(int intCol)
        {
            if (intCol < 27)
            {
                return Convert.ToChar(intCol + 64).ToString();
            }
            else
            {
                return Convert.ToChar((intCol - 1) / 26 + 64).ToString() + Convert.ToChar((intCol - 1) % 26 + 64 + 1).ToString();
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
        #endregion
    }
}

