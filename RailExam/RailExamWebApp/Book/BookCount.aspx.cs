using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExamWebApp.Common.Class;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Book
{
    public partial class BookCount : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strId = Request.QueryString.Get("id");

                DataSet ds = new DataSet();
                OracleAccess oa = new OracleAccess();
                OracleAccess oaCenter = new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
                string strSql;

                if (File.Exists(Server.MapPath("../Online/Book/" + strId + "/index.html")))
                {
                    if (PrjPub.IsServerCenter)
                    {
                        strSql = "select * from Book_Count where Book_ID=" + strId;
                        ds = oa.RunSqlDataSet(strSql);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strSql = "update Book_Count set Count_Num=Count_Num+1 where Book_ID=" + strId;
                        }
                        else
                        {
                            strSql = "insert into Book_Count values(" + strId + ",1)";
                        }

                        oa.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        strSql = "select * from Book_Count where Book_ID=" + strId;
                        ds = oaCenter.RunSqlDataSet(strSql);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strSql = "update Book_Count set Count_Num=Count_Num+1 where Book_ID=" + strId;
                        }
                        else
                        {
                            strSql = "insert into Book_Count values(" + strId + ",1)";
                        }

                        oaCenter.ExecuteNonQuery(strSql);
                    }

                    Response.Redirect("../Online/Book/" + strId + "/index.html");
                }
                else
                {
                    Response.Write("<script>alert('当前服务器无该教材！');top.close();</script>");
                }
            }
        }
    }
}
