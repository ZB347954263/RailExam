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

namespace RailExamWebApp.RandomExam
{
    public partial class CheckAttendExamLeft : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
    }
}
