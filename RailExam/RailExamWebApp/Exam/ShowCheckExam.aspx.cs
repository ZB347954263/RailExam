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

namespace RailExamWebApp.Exam
{
    public partial class ShowCheckExam : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = "select a.*,case when a.Exam_Style=1 then '≤ª¥Êµµøº ‘' else '¥Êµµøº ‘' end as ExamStyleName,"
                          + " e.Short_Name as StationName,d.Employee_Name "
                          + " from Random_Exam_Check b"
                          + " inner join Random_Exam_Result c on b.Random_Exam_Result_ID=c.Random_Exam_Result_ID"
                          + " inner join Employee d on c.Examinee_ID=d.Employee_ID "
                          + " inner join Random_Exam a  on c.Random_Exam_ID=a.Random_Exam_ID "
                          + " inner join Org e on a.Org_ID=e.Org_ID "
                          + " where 1=2 order by Begin_Time desc";
                hfSelect.Value = sql;
            }

            string str = Request.Form.Get("Refresh");

            if(!string.IsNullOrEmpty(str))
            {
                hfSelect.Value = GetSql();
                grdEntity.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            hfSelect.Value = GetSql();
            grdEntity.DataBind();
        }

        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
                {
                    e.Row.Visible = false;
                }
                else
                {
                    e.Row.Attributes.Add("onclick", "selectArow(this);");
                }
            }
        }

        private string GetSql()
        {
            string sql = "select a.*,case when a.Exam_Style=1 then '≤ª¥Êµµøº ‘' else '¥Êµµøº ‘' end as ExamStyleName,"
                          + " e.Short_Name as StationName,d.Employee_Name "
                          + " from Random_Exam_Check b"
                          + " inner join Random_Exam_Result c on b.Random_Exam_Result_ID=c.Random_Exam_Result_ID"
                          + " inner join Employee d on c.Examinee_ID=d.Employee_ID "
                          + " inner join Random_Exam a  on c.Random_Exam_ID=a.Random_Exam_ID "
                          + " inner join Org e on a.Org_ID=e.Org_ID "
                         + " where b.Org_ID="+ Request.QueryString.Get("OrgID")+" order by Begin_Time desc";

            return sql;
        }

        protected void grdEntity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEntity.PageIndex = e.NewPageIndex;
            hfSelect.Value = GetSql();
            grdEntity.DataBind();
        }

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
            if (db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["Random_Exam_ID"] = -1;
                db.Rows.Add(row);
            }
        }
    }
}
