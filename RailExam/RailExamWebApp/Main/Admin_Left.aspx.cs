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
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Main
{
    public partial class Admin_Left : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null && PrjPub.CurrentStudent == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }

                string sbInnerHtml = "<ul id='navigation'>";

                if (!string.IsNullOrEmpty(Request.QueryString.Get("type")) && Request.QueryString.Get("type")=="employee")
                {
                    OracleAccess oa = new OracleAccess();
                    string sql = "select * from SYSTEM_FUNCTION t where function_type = 4 order by function_id";
                    DataSet ds = oa.RunSqlDataSet(sql);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (row["function_id"].ToString().Length == 2)
                            {
                                sbInnerHtml += "<li><a class='head'>" + row["menu_name"] + "</a>";
                                sbInnerHtml += "<ul>";
                                //添加二级功能菜单
                                foreach (DataRow row1 in ds.Tables[0].Rows)
                                {
                                    if (row1["function_id"].ToString().Length == 4 &&
                                        row1["function_id"].ToString().IndexOf(row["function_id"].ToString()) == 0)
                                    {
                                        sbInnerHtml += @"<li><a href='" + row1["page_url"].ToString() + "' target='I2'>" + row1["menu_name"].ToString() + "</a></li>";
                                    }
                                }
                                sbInnerHtml += "</ul>";
                            }
                            sbInnerHtml += "</li>";
                        }
                        sbInnerHtml += "</ul>";

                        this.menu.InnerHtml = sbInnerHtml;
                    }

                }
                else 
                {
                    foreach (FunctionRight functionRight in PrjPub.CurrentLoginUser.FunctionRights)
                    {
                        if (PrjPub.IsWuhan())
                        {
                            if (functionRight.Function.FunctionID == "0406")
                            {
                                continue;
                            }
                        }
                        if (functionRight.Right == 0)
                        {
                            continue;
                        }


                        if (functionRight.Function.FunctionID.Length == 2)
                        {
                            sbInnerHtml += "<li><a class='head'>" + functionRight.Function.MenuName + "</a>";
                            sbInnerHtml += "<ul>";
                            //添加二级功能菜单
                            foreach (FunctionRight functionRight1 in PrjPub.CurrentLoginUser.FunctionRights)
                            {
                                if (functionRight1.Right == 0)
                                {
                                    continue;
                                }

                                if (functionRight1.Function.FunctionID.Length == 4 &&
                                    functionRight1.Function.FunctionID.IndexOf(functionRight.Function.FunctionID) == 0)
                                {
                                    sbInnerHtml += @"<li><a href='" + functionRight1.Function.PageUrl + "' target='I2'>" + functionRight1.Function.MenuName + "</a></li>";
                                }
                            }
                            sbInnerHtml += "</ul>";
                        }
                        sbInnerHtml += "</li>";

                    }

                    sbInnerHtml += "</ul>";

                    this.menu.InnerHtml = sbInnerHtml;
                }
            }
        }
    }
}
