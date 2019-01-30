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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public partial class InformationStudy : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInformationSystem();
                BindInformationLevel();
            }
        }

        private void BindInformationSystem()
        {
            OracleAccess db = new OracleAccess();

            string strSql = "select * from Information_System Order by Level_Num,Order_Index ";

            DataSet ds = db.RunSqlDataSet(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = dr["Information_System_ID"].ToString();
                    tvn.Value = dr["Information_System_ID"].ToString();
                    tvn.Text = dr["Information_System_Name"].ToString();

                    try
                    {
                        if(dr["Parent_ID"].ToString()=="0")
                        {
                            tvAssist.Nodes.Add(tvn);
                        }
                        else
                        {
                            tvAssist.FindNodeById(dr["Parent_ID"].ToString()).Nodes.Add(tvn);
                        }
                    }
                    catch
                    {
                        tvAssist.Nodes.Clear();
                        SessionSet.PageMessage = "数据错误！";
                        return;
                    }
                }
            }
        }

        private void BindInformationLevel()
        {
            OracleAccess db = new OracleAccess();

            string strSql = "select * from Information_Level Order by Level_Num,Order_Index ";

            DataSet ds = db.RunSqlDataSet(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = dr["Information_Level_ID"].ToString();
                    tvn.Value = dr["Information_Level_ID"].ToString();
                    tvn.Text = dr["Information_Level_Name"].ToString();

                    try
                    {
                        if (dr["Parent_ID"].ToString() == "0")
                        {
                            tvTrainType.Nodes.Add(tvn);
                        }
                        else
                        {
                            tvTrainType.FindNodeById(dr["Parent_ID"].ToString()).Nodes.Add(tvn);
                        }
                    }
                    catch
                    {
                        tvTrainType.Nodes.Clear();
                        SessionSet.PageMessage = "数据错误！";
                        return;
                    }
                }
            }
        }
    }
}
