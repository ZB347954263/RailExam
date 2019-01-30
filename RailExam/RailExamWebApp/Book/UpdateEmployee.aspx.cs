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
    public partial class UpdateEmployee : PageBase 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(PrjPub.CurrentLoginUser.RoleID != 1)
                {
                    Response.Write("<script>alert( '您没有该操作的权限！');window.close();</script>");
                }
            }
        }

        protected  void btnOK_Click(object sender, EventArgs e)
        {
            OracleAccess oracle =new OracleAccess();

            string strSql = "update Book set Authors=" + hfNewEmployeeID.Value + " where Authors=" + hfEmployeeID.Value;

            oracle.ExecuteNonQuery(strSql);

            Response.Write("<script>window.opener.frames['ifBookInfo'].form1.Refresh.value='true';window.opener.frames['ifBookInfo'].form1.submit();window.close();</script>");
        }
    }
}
