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
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Exam
{
    public partial class StudentFingerExam : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string employeeId = Request.QueryString.Get("employeeId");
                string examId = Request.QueryString.Get("examId");

                RandomExamBLL randomBll = new RandomExamBLL();
                lblExam.Text = randomBll.GetExam(Convert.ToInt32(examId)).ExamName;

                string strSql = "select GetOrgName(GetStationOrgID(org_id)) StationOrgName,"
                                + "GetWorkShopName(org_id) WorkShopName,b.Post_Name,a.* from employee a"
                                + " inner join Post b on a.Post_ID=b.Post_ID "
                                + " where Employee_ID=" + employeeId;

                OracleAccess db = new OracleAccess();
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                lblName.Text = dr["Employee_Name"].ToString();
                lblSex.Text = dr["Sex"].ToString();
                lblOrgName.Text = dr["StationOrgName"].ToString();
                lblWorkShop.Text = dr["WorkShopName"].ToString();
                lblPost.Text = dr["Post_Name"].ToString();
                lblIdentityCard.Text = dr["Identity_CardNo"].ToString();
                lblPostNo.Text = dr["Work_No"].ToString();

                DataSet ds = Pub.GetPhotoDateSet(employeeId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        myImagePhoto.ImageUrl = "/RailExamBao/RandomExamTai/ShowImage.aspx?EmployeeID=" + employeeId;
                    }
                    else
                    {
                        myImagePhoto.ImageUrl = "../../images/empty.jpg";
                    }
                }
                else
                {
                    myImagePhoto.ImageUrl = "../../images/empty.jpg";
                }

                hfEmployeeID.Value = employeeId;
                hfExamID.Value = examId;
            }
        }
    }
}
