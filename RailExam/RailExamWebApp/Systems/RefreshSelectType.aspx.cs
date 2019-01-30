using System;
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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class RefreshSelectType : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected  void btnOK_Click(object sender, EventArgs e)
        {
            string strSql = "select * from Random_Exam where Is_All_Arrange=0 "
                           + " and Org_ID in (select Org_ID from Org where level_num=2 and Suit_Range=1) "
                           + " and Random_Exam_ID in (select Random_Exam_ID from Random_Exam_Computer_Server "
                           + " where Computer_Server_No=" + PrjPub.ServerNo + " and Has_Paper=0 and Is_Start<2)";
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0 && ddlType.SelectedValue != "1" && ddlType.SelectedValue != "2")
            {
                SessionSet.PageMessage = "路局存在未完全安排微机教室的路局级考试，\r\n此刻下载数据也将无法在考试监控中看见该考试，\r\n暂时不能下载数据！";
                return;
            }

            Response.Redirect("RefreshDownLoad.aspx?type=downloaddata&selectType="+ddlType.SelectedValue);
        }
    }
}
