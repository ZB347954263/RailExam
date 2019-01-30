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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Courseware
{
    public partial class CoursewareCount : PageBase
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

                if (Directory.Exists(Server.MapPath("../Online/Courseware/" + strId)))
                {
                    if (PrjPub.IsServerCenter)
                    {
                        strSql = "select * from Courseware_Count where Courseware_ID=" + strId;
                        ds = oa.RunSqlDataSet(strSql);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strSql = "update Courseware_Count set Count_Num=Count_Num+1 where Courseware_ID=" + strId;
                        }
                        else
                        {
                            strSql = "insert into Courseware_Count values(" + strId + ",1)";
                        }

                        oa.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        strSql = "select * from Courseware_Count where Courseware_ID=" + strId;
                        ds = oaCenter.RunSqlDataSet(strSql);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strSql = "update Courseware_Count set Count_Num=Count_Num+1 where Courseware_ID=" + strId;
                        }
                        else
                        {
                            strSql = "insert into Courseware_Count values(" + strId + ",1)";
                        }

                        oaCenter.ExecuteNonQuery(strSql);
                    }

                    Response.Redirect("ViewCourseware.aspx?id=" + strId);
                }
                else
                {
                    Response.Write("<script>alert('当前服务器无该课件！');top.close();</script>");
                }
            }
        }
    }
}
