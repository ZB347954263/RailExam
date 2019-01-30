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
                //��¼��ǰ�������ڵص�OrgID
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
                    typeName = "��ѡ";
                    b = 0;
                    e = singleCount;
                }
                else if(i==1)
                {
                    itemCount = otherCount;
                    typeName = "��ѡ";
                    b = singleCount;
                    e = singleCount + otherCount;
                }
                else if (i == 2)
                {
                    itemCount = otherCount;
                    typeName = "�ж�";
                    b = singleCount+otherCount;
                    e = singleCount + otherCount*2;
                }
                else if (i == 3)
                {
                    itemCount = otherCount;
                    typeName = "�ۺ�ѡ����";
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
                Response.Write("��" + typeName + "");
                Response.Write("  ����" + itemCount + "�⣩</td></tr >");

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
                                       + "<a href='#Test" + i + j + "' id='Empty" + i + j + "' onclick='clickEmpty(this)'style='cursor: hand;' title='���ѡ��'>"
                                       + "<img src='../images/clear.png'  style='border:0'/></a>"
                                       + "</td></tr >");
                            }
                        }

                        // ��֯�û���



                        if (paperItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)   //��ѡ
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
                        else if (paperItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperItem.TypeId == PrjPub.ITEMTYPE_JUDGE)    //��ѡ
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
                                        + "<a href='#Test" + i + j + z + "' id='Empty" + i + j + z + "' onclick='clickEmpty(this)'style='cursor: hand;' title='���ѡ��'>"
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
            Response.Write(" <div class='ExamButton'><input id='btnClose' class='button' name='btnSave' type='button' value='�ύ���'  onclick='SaveRecord()'/> </div><br><br><br><br><br><br>");
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
                    strReturn = "һ";
                    break;
                case "1":
                    strReturn = "��";
                    break;
                case "2":
                    strReturn = "��";
                    break;
                case "3":
                    strReturn = "��";
                    break;
                case "4":
                    strReturn = "��";
                    break;
                case "5":
                    strReturn = "��";
                    break;
                case "6":
                    strReturn = "��";
                    break;
                case "7":
                    strReturn = "��";
                    break;
                case "8":
                    strReturn = "��";
                    break;
                case "9":
                    strReturn = "ʮ";
                    break;
                case "10":
                    strReturn = "ʮһ";
                    break;
                case "11":
                    strReturn = "ʮ��";
                    break;
                case "12":
                    strReturn = "ʮ��";
                    break;
                case "13":
                    strReturn = "ʮ��";
                    break;
                case "14":
                    strReturn = "ʮ��";
                    break;
                case "15":
                    strReturn = "ʮ��";
                    break;
                case "16":
                    strReturn = "ʮ��";
                    break;
                case "17":
                    strReturn = "ʮ��";
                    break;
                case "18":
                    strReturn = "ʮ��";
                    break;
                case "19":
                    strReturn = "��ʮ";
                    break;
            }
            return strReturn;
        }
        #endregion
    }
}

