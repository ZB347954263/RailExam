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

namespace RailExamWebApp.RandomExamTai
{
    public partial class TJ_EmployeeWorkerGroupHeaderByOrgDetail : PageBase
    {
        OracleAccess oracle = new OracleAccess();
        const string SQL = @"select t.employee_id,
                                    t.employee_name,
                                    t.sex,
                                    p.post_name,
                                    o.short_name
                                from EMPLOYEE t
                                left join post p on p.post_id = t.post_id
                                left join org o on o.org_id = getstationorgid(t.org_id)";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string stationName = Server.UrlDecode(Request["StationName"]);
                int colIndex = Int32.Parse(Request["ColIndex"]);
                //查询脚本
                string sql = String.Empty;
                // 查询其它班组信息(非班组长总数列, ByLevelID)
                if (colIndex >= 2)
                {
                    int levelID = GetWorkgroupleaderLevelIdByColIndex(colIndex);

                    // 各站段信息(ByOrgID)
                    if (stationName != "合计")
                    {
                        int orgID = GetOrgIDByStationName(stationName);
                        if (orgID != -1 && levelID != -1)
                        {
                            sql = GetSQLForSelectByOrgIDAndLevelID(orgID, levelID);
                        }
                    }
                    // 所有站段的合计信息(无OrgID)
                    else
                    {
                        sql = GetSQLForSelectTotalByLevelID(levelID);
                    }
                }
                // 查询班组长总数列信息(无LevelID)
                else
                {
                    // 各站段信息(ByOrgID)
                    if (stationName != "合计")
                    {
                        int orgID = GetOrgIDByStationName(stationName);
                        sql = GetSQLForSelectByOrgID(orgID);
                    }
                    // 合计信息(无OrgID)
                    else
                    {
                        sql = GetSQLForSelectTotal();
                    }
                }
                //得到SQL脚本后执行查询
                if (!String.IsNullOrEmpty(sql))
                {
                    BindDetail(sql);
                    ViewState["dt"] = this.GridView1.DataSource;
                }
            }
        }

        // 通过前一个页面的colIndex参数获取对应的列信息(即WORKGROUPLEADER_LEVEL表中的workgroupleader_level_id)
        private int GetWorkgroupleaderLevelIdByColIndex(int colIndex)
        {
            int orderIndex = colIndex - 2;
            string sql = String.Format("select workgroupleader_level_id from WORKGROUPLEADER_LEVEL t where order_index = {0}", orderIndex);
            try
            {
                DataSet ds = oracle.RunSqlDataSet(sql);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                return -1;
            }
        }

        // 通过站段名称获取OrgID
        private int GetOrgIDByStationName(string stationName)
        {
            string sql = String.Format("select org_id from org where short_name = '{0}'", stationName);
            try
            {
                DataSet ds = oracle.RunSqlDataSet(sql);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                return -1;
            }
        }

        #region 获取查询详细信息的SQL脚本

        // 获取指定站段的班组长的详细信息
        private string GetSQLForSelectByOrgID(int orgID)
        {
            return SQL + String.Format(" where getstationorgid(org_id) = {0} and workgroupleader_type_id > 0", orgID);
        }

        // 获取指定站段和指定班组的详细信息
        private string GetSQLForSelectByOrgIDAndLevelID(int orgID, int levelID)
        {
            return SQL + String.Format(" where getstationorgid(org_id) = {0} and workgroupleader_type_id = {1}", orgID, levelID);
        }

        // 获取指定班组的合计详细信息
        private string GetSQLForSelectTotalByLevelID(int levelID)
        {
            return SQL + String.Format(" where workgroupleader_type_id = {0}", levelID);
        }

        // 获取所有的合计的详细信息
        private string GetSQLForSelectTotal()
        {
            return SQL + " where workgroupleader_type_id > 0";
        }

        #endregion

        // 绑定详细信息
        private void BindDetail(string sql)
        {
            try
            {
                DataSet ds = oracle.RunSqlDataSet(sql);
                this.GridView1.DataSource = ds.Tables[0];
                this.GridView1.DataBind();
            }
            catch { }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.GridView1.DataSource = ViewState["dt"];
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "SelectedRow(this)");
                e.Row.Attributes.Add("ondblclick", "OpenEmployeeArchives(" + (e.Row.FindControl("lblID") as Label).Text + ")");
            }
        }
    }

}
