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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Book
{
    public partial class BookCoursewareRank : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGridBook();
            BindGridCourse();
        }

        private void BindGridBook()
        {
            string str = string.Empty;
            int railSystemId = PrjPub.GetRailSystemId();
            if (railSystemId != 0)
            {
                str =
                    " where b.book_id in (select distinct book_id from Book_Range_Org where Org_ID in (select Org_ID from Org where Rail_System_ID=" +
                    railSystemId + " and level_Num=2))";
            }
            else
            {
                if (PrjPub.CurrentLoginUser.SuitRange == 0)
                {
                    str = " where b.book_id in (select distinct book_id from Book_Range_Org where Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                }
            }

            OracleAccess db = new OracleAccess();
            string strSql = "select a.Count_Num,b.*,c.Short_Name as publishOrgName,d.Employee_Name as AuthorsName "
                        +" from Book_Count a "
                        + " inner join Book b on a.BooK_ID=b.Book_ID "
                        + " inner join Org c on b.Publish_Org=c.Org_ID"
                        + " left join Employee d on b.Authors=d.Employee_ID"
                        + str
                        + " order by a.Count_Num desc";
            DataSet ds = db.RunSqlDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["Book_Name"] = "<a onclick=OpenIndex('" + dr["Book_ID"] + "') href=# title=" + dr["Book_Name"] + " > " + dr["Book_Name"] + " </a>";
            }

            Grid1.DataSource = ds;
            Grid1.DataBind();
        }

        private void BindGridCourse()
        {
            string str = string.Empty;
            int railSystemId = PrjPub.GetRailSystemId();
            if (railSystemId != 0)
            {
                str =
                    " where b.Courseware_id in (select distinct Courseware_id from Courseware_Range_Org where Org_ID in (select Org_ID from Org where Rail_System_ID=" +
                    railSystemId + " and level_Num=2))";
            }
            else
            {
                if (PrjPub.CurrentLoginUser.SuitRange == 0)
                {
                    str = " where b.Courseware_id in (select distinct Courseware_id from Courseware_Range_Org where Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                }
            }

            OracleAccess db = new OracleAccess();
            string strSql = "select a.Count_Num,b.*,c.Short_Name as publishOrgName "
                        + " from Courseware_Count a "
                        + " inner join Courseware b on a.Courseware_ID=b.Courseware_ID "
                        + " inner join Org c on b.Provide_Org=c.Org_ID"
                        + str
                        + " order by a.Count_Num desc";
            DataSet ds = db.RunSqlDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["Courseware_Name"] = "<a onclick=OpenCourse('" + dr["Courseware_ID"] + "') href=# title=" + dr["Courseware_Name"] + " > " + dr["Courseware_Name"] + " </a>";
            }

            Grid2.DataSource = ds;
            Grid2.DataBind();
        }
    }
}
