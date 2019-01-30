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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ShowImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string employeeID = Request.QueryString.Get("EmployeeID");
                DataSet ds = Pub.GetPhotoDateSet(employeeID);

                if(ds.Tables[0].Rows.Count>0)
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        Response.Clear();
                        Response.ContentType = "image/jpeg";
                        Response.BinaryWrite((byte[])ds.Tables[0].Rows[0][0]);
                        Response.End();
                    }
                }
            }
        }
    }
}
