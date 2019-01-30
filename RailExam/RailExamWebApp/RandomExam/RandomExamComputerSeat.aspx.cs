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

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamComputerSeat : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGrid();
            }

            string str = Request.Form.Get("refresh");
            if(!string.IsNullOrEmpty(str))
            {
                BindGrid();
            }

            string strResetID = Request.Form.Get("reset");
            if (!string.IsNullOrEmpty(strResetID))
            {
                OracleAccess db = new OracleAccess();

                string strSql =
                    "select Computer_Room_Seat from  Random_Exam_Result_Detail  where Random_Exam_Result_Detail_ID=" + strResetID;

                if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                {
                    strSql = "update Random_Exam_Result_Detail set FingerPrint=null, Computer_Room_Seat=0 where Random_Exam_Result_Detail_ID=" +
                        strResetID;
                    db.ExecuteNonQuery(strSql);
                }
                else
                {
                    strSql = "update Random_Exam_Result_Detail_Temp set FingerPrint=null, Computer_Room_Seat=0 where Random_Exam_Result_Detail_ID=" +
                        strResetID;
                    db.ExecuteNonQuery(strSql);
                }
                BindGrid();
                SessionSet.PageMessage = "重置成功！";
            }
        }

        private void BindGrid()
        {
            try
            {
                string strSql =
                @"select a.* from (
                    select a.RANDOM_EXAM_RESULT_DETAIL_ID,c.Employee_Name 姓名,Work_No 员工编码,Identity_CardNo 身份证号码,
                    getorgname(getstationorgid(c.org_id)) 单位,getworkshopname(c.org_id) 车间,
                    GetOrgName(d.Org_ID)||'-'||d.Computer_Room_Name 微机教室,
                    case when a.Computer_Room_Seat=0 then '未指纹验证分配机位' else  to_char(a.Computer_Room_Seat) end 机位,b.Exam_SEQ_NO,a.Is_Remove
                    from (select * from Random_Exam_Result_Detail_Temp where Is_Remove=0 and  Random_Exam_ID=" + Request.QueryString.Get("RandomExamID") + @" 
                    union all select * from Random_Exam_Result_Detail where Is_Remove=0 and Random_Exam_ID=" + Request.QueryString.Get("RandomExamID") + @") a
                    left join Random_Exam_Result_Current b on a.Random_Exam_Result_ID=b.Random_Exam_Result_ID
                    left join (select examinee_id,min(exam_seq_no) as max_seq_no from Random_EXAM_RESULT_Current
                    where Random_Exam_Id =" + Request.QueryString.Get("RandomExamID") + @" group by examinee_id ) e
                    on b.examinee_id=e.examinee_id and b.exam_seq_no=e.max_seq_no
                    inner join Employee c on a.Employee_ID=c.Employee_ID
                    inner join Computer_Room d on a.Computer_Room_ID=d.Computer_Room_ID
                    where  a.Random_Exam_ID=" + Request.QueryString.Get("RandomExamID") + @" and Is_Remove=0) a 
                    order by a.机位,a.Is_Remove";

                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                grdEntity.DataSource = ds;
                grdEntity.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "selectArow(this);");
            }
        }

        protected void grdEntity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEntity.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}
