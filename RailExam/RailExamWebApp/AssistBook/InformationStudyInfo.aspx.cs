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
    public partial class InformationStudyInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string strSql = "";

            OracleAccess db = new OracleAccess();

            string strKnowledgeIDPath = Request.QueryString.Get("id");
            if (!string.IsNullOrEmpty(strKnowledgeIDPath))
            {
                strSql = "select Id_Path from Information_System where Information_System_ID=" + strKnowledgeIDPath;
                DataSet dsSystem = db.RunSqlDataSet(strSql);

                if (dsSystem.Tables[0].Rows.Count > 0)
                {
                    ViewState["SystemIdPath"] = dsSystem.Tables[0].Rows[0][0];
                }
                else
                {
                    ViewState["SystemIdPath"] = string.Empty;
                }

                strSql = "select a.*,c.Employee_Name as CreatePersonName from Information a "
                         + " inner join Information_System  b on a.Information_System_ID=b.Information_System_ID"
                         + " inner join Employee c on a.Create_Person=c.Employee_ID "
                         + " where b.Id_Path||'/' like '" + ViewState["SystemIdPath"] + "/%'";
            }

            string strTrainTypeIDPath = Request.QueryString.Get("id1");
            if (!string.IsNullOrEmpty(strTrainTypeIDPath))
            {
                strSql = "select Id_Path from Information_Level where Information_Level_ID=" + strTrainTypeIDPath;
                DataSet dsLevel = db.RunSqlDataSet(strSql);

                if (dsLevel.Tables[0].Rows.Count > 0)
                {
                    ViewState["LevelIdPath"] = dsLevel.Tables[0].Rows[0][0];
                }
                else
                {
                    ViewState["LevelIdPath"] = string.Empty;
                }

                strSql = "select a.*,c.Employee_Name as CreatePersonName from Information a "
                         + " inner join Information_Level  b on a.Information_Level_ID=b.Information_Level_ID"
                         + " inner join Employee c on a.Create_Person=c.Employee_ID "
                         + " where b.Id_Path||'/' like '" + ViewState["LevelIdPath"] + "/%'";
            }

            if (!string.IsNullOrEmpty(txtBookName.Text.Trim()))
            {
                strSql += " and Information_Name like '%" + txtBookName.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtOrg.Text.Trim()))
            {
                strSql += " and KEYWORDS like '%" + txtOrg.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtBookName.Text.Trim()))
            {
                strSql += " and PUBLISH_ORG like '%" + txtBookName.Text.Trim() + "%'";
            }

            DataSet ds = db.RunSqlDataSet(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Information_Name"].ToString().Length <= 30)
                    {
                        dr["Information_Name"] = "<a onclick=OpenIndex('" + dr["Information_ID"] + "') href=# title=" + dr["Information_Name"] + " > " + dr["Information_Name"] + " </a>";
                    }
                    else
                    {
                        dr["Information_Name"] = "<a onclick=OpenIndex('" + dr["Information_ID"] + "') href=# title=" + dr["Information_Name"] + " > " + dr["Information_Name"].ToString().Substring(0, 15) + "...</a>";
                    }
                }

                Grid1.DataSource = ds;
                Grid1.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}
