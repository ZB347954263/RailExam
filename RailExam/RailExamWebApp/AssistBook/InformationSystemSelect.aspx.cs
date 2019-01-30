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
    public partial class InformationSystemSelect : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
            }
        }

        private void BindTree()
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
                        if (dr["Parent_ID"].ToString() == "0")
                        {
                            tvInformationSystem.Nodes.Add(tvn);
                        }
                        else
                        {
                            tvInformationSystem.FindNodeById(dr["Parent_ID"].ToString()).Nodes.Add(tvn);
                        }
                    }
                    catch
                    {
                        tvInformationSystem.Nodes.Clear();
                        SessionSet.PageMessage = "Êý¾Ý´íÎó£¡";
                        return;
                    }
                }
            }
        }
    }
}
