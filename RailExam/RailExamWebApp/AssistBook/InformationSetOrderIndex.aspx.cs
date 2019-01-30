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

namespace RailExamWebApp.AssistBook
{
    public partial class InformationSetOrderIndex : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtOrderIndex.Text = Request.QueryString.Get("NowOrder");
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            OracleAccess db = new OracleAccess();

            int nowOrder = Convert.ToInt32(Request.QueryString.Get("NowOrder"));
            int order = Convert.ToInt32(txtOrderIndex.Text);
            int max = Convert.ToInt32(Request.QueryString.Get("MaxOrder"));
            if (order > max)
            {
                order= max;
            }

            string strType = Request.QueryString.Get("type");

            string strid = !string.IsNullOrEmpty(Request.QueryString.Get("id"))
                               ? Request.QueryString.Get("id")
                               : Request.QueryString.Get("id1");

            string strSql;

            if(order > nowOrder)
            {
                strSql = @"update Information set  " + strType + "_Order_Index=" + strType + "_Order_Index-1 " +
                         "where Information_"+strType+"_ID="+strid+@" and
                         " + strType + "_Order_Index>"+ nowOrder+@" and 
                         " + strType + "_Order_Index<="+  order;          
            }
            else
            {
                strSql = @"update Information set  " + strType + "_Order_Index=" + strType + "_Order_Index+1 " +
                         "where Information_" + strType + "_ID=" + strid + @" and
                         " + strType + "_Order_Index<" + nowOrder + @" and 
                         " + strType + "_Order_Index>=" + order;        
            }
            
            db.ExecuteNonQuery(strSql);

            strSql = "update Information set " + strType + "_Order_Index=" + order + " where Information_ID=" +
                            Request.QueryString.Get("BookID");
            db.ExecuteNonQuery(strSql);

            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }
    }
}
