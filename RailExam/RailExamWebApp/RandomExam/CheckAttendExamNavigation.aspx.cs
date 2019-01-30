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
    public partial class CheckAttendExamNavigation :  PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void FillPaper()
        {
            RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
            OracleAccess db = new OracleAccess();

            IList<RandomExamItem> PaperItems = new List<RandomExamItem>();
            PaperItems = randomItemBLL.GetItemsCurrentCheck();

            string strSql = "select * from System_Exam where System_Exam_ID=1";
            DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

            int examNumber;
            if (dt.Rows.Count > 0)
            {
                examNumber = Convert.ToInt32(dt.Rows[0]["Exam_Number"]);
            }
            else
            {
                examNumber = 10;
            }

            int totalCount = examNumber;
            int otherCount = totalCount/4;
            int singleCount = totalCount - otherCount*3;

            int m = 4;
            if(otherCount==0)
            {
                m = 1;
            }

            for (int i = 0; i < m; i++)
            {
                int itemCount = 0;
                int b = 0, e = 0;
                string typeName = string.Empty;

                if (i == 0)
                {
                    itemCount = singleCount;
                    typeName = "��һ���⣺��ѡ";
                    b = 0;
                    e = singleCount;
                }
                else if (i == 1)
                {
                    itemCount = otherCount;
                    typeName = "�ڶ����⣺��ѡ";
                    b = singleCount;
                    e = singleCount + otherCount;
                }
                else if (i == 2)
                {
                    itemCount = otherCount;
                    typeName = "�������⣺�ж�";
                    b = singleCount + otherCount;
                    e = singleCount + otherCount*2;
                }
                else if (i == 3)
                {
                    itemCount = otherCount;
                    typeName = "���Ĵ��⣺�ۺ�ѡ����";
                    b = singleCount + otherCount*2;
                    e = singleCount + otherCount*3;
                }

                Response.Write("<br>");
                Response.Write("<span class='StudentLeftInfo'><b> " + typeName + "</b></span>");
                Response.Write("<br>");

                if (PaperItems != null)
                {
                    Response.Write("<table width='100%'  border='1' style='background-color:#ffffff'>");
                    int z = 1;
                    int tempK = 0;
                    int count = 1;
                    for (int j = b; j < e; j++)
                    {
                        if (j >= PaperItems.Count)
                        {
                            continue;
                        }

                        RandomExamItem paperItem = PaperItems[j];
                        int k = j + 1 - b;


                        if (paperItem.TypeId != PrjPub.ITEMTYPE_FILLBLANKDETAIL &&
                            paperItem.TypeId != PrjPub.ITEMTYPE_FILLBLANK)
                        {
                            z = 1;

                            if (k % 5 == 1)
                            {
                                Response.Write("</tr >");
                                Response.Write("<tr><td class='StudentTableInfo' id='Item" + i + j + "' >"
                                               + "<a href='CheckAttendExamNew.aspx?employeeID=" +
                                               Request.QueryString.Get("employeeID")
                                               + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + k +
                                               "</b></a></td>");
                            }
                            else
                            {
                                Response.Write("<td class='StudentTableInfo' id='Item" + i + j + "' >"
                                               + "<a href='CheckAttendExamNew.aspx?employeeID=" +
                                               Request.QueryString.Get("employeeID")
                                               + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + k +
                                               "</b></a></td>");
                            }
                        }
                        else
                        {
                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                z = 1;
                                tempK++;

                                string str = "select * from Random_Exam_Item_" +
                                         DateTime.Today.Year + " where Random_Exam_ID=" + paperItem.RandomExamId
                                         + " and  Parent_Item_ID=" + paperItem.ItemId + " order by Item_Index";
                                DataSet ds = db.RunSqlDataSet(str);

                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    if (count%3 == 1)
                                    {
                                        Response.Write("</tr >");
                                        Response.Write("<tr><td class='StudentTableInfo' id='Item" + i + j + z+"' >"
                                                       + "<a href='CheckAttendExamNew.aspx?employeeID=" +
                                                       Request.QueryString.Get("employeeID")
                                                       + "#Test" + i + j +z+
                                                       "' target='ifExamInfo' style='cursor: hand;'><b>" +
                                                       tempK + "-(" + z + ")</b></a></td>");
                                    }
                                    else
                                    {
                                        Response.Write("<td class='StudentTableInfo' id='Item" + i + j +z+ "' >"
                                                       + "<a href='CheckAttendExamNew.aspx?employeeID=" +
                                                       Request.QueryString.Get("employeeID")
                                                       + "#Test" + i + j + z +
                                                       "' target='ifExamInfo' style='cursor: hand;'><b>" +
                                                       tempK + "-(" + z + ")</b></a></td>");
                                    }

                                    z++;
                                    count++;
                                }
                            }
                        }
                    }

                    Response.Write("</tr >");
                    Response.Write("</table>");
                }
                else
                {
                    SessionSet.PageMessage = "δ�ҵ���¼��";
                }
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
    }
}
