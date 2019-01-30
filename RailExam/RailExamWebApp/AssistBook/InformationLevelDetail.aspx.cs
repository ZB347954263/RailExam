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

namespace RailExamWebApp.AssistBook
{
    public partial class InformationLevelDetail : System.Web.UI.Page
    {
        private OracleAccess ora = new OracleAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mode = Convert.ToString(Request.QueryString["mode"]);
                if (mode == "edit")
                {
                    string id = Convert.ToString(Request.QueryString["id"]);

                    OracleAccess ora = new OracleAccess();

                    string sql = String.Format(
                        "select * from INFORMATION_LEVEL t"
                        + " where information_level_id = {0}",
                        id);

                    DataSet ds = ora.RunSqlDataSet(sql);
                    if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1)
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        txtName.Text = Convert.ToString(row["information_level_name"]);
                        txtDescription.Text = Convert.ToString(row["description"]);
                        txtMemo.Text = Convert.ToString(row["memo"]);
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text.Trim()))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('请输入级别名称')", true);
            }
            else
            {
                // 获取界面用户输入值
                string name = txtName.Text.Trim();
                string description = txtDescription.Text.Trim();
                string memo = txtMemo.Text.Trim();

                // 系统自行计算的赋值
                int parentID = 0;
                string idPath = @"/1";  //临时值，用于添加到表中，待新数据插入之后再回头修改.
                int levelNum = 1;
                int orderIndex = 1;

                // 传入的参数
                string id = Convert.ToString(Request.QueryString["id"]);
                string mode = Convert.ToString(Request.QueryString["mode"]);

                // 如果传入的id为0，则表示当前无级别节点信息.
                // 根据业务逻辑，必然是新增第一个节点（新增同级）
                if (id == "0")
                {
                    parentID = 0;   // 所以直接赋值父级id为0.
                    levelNum = 1;
                    orderIndex = 1;
                }
                else
                {
                    string sql = String.Format(
                        "select parent_id,level_num,nvl(order_index,0) order_index from INFORMATION_LEVEL t"
                        + " where information_level_id = {0}",
                        id
                    );
                    DataSet ds = ora.RunSqlDataSet(sql);
                    DataRow row = ds.Tables[0].Rows[0];

                    switch (mode)
                    {
                        case "brother":
                            parentID = Convert.ToInt32(row["parent_id"]);
                            levelNum = Convert.ToInt32(row["level_num"]);
                            orderIndex = GetMaxChildOrderIndex(parentID) + 1;
                            break;
                        case "child":
                            parentID = Int32.Parse(id);
                            levelNum = Convert.ToInt32(row["level_num"]) + 1;
                            orderIndex = GetMaxChildOrderIndex(parentID) + 1;
                            break;
                        case "edit":
                            ;   // 修改只需要修改name,description,memo. 故此处无作为
                            break;
                        default:
                            break;
                    }
                }

                string insert = String.Format(
                    "insert into information_level"
                    + " values(Information_Level_Seq.Nextval,{0},'{1}',{2},{3},'{4}','{5}','{6}')",
                    parentID, idPath, levelNum, orderIndex, name, description, memo
                );

                string update = String.Format(
                    "update information_level set"
                    + " information_level_name = '{0}',"
                    + " description = '{1}',"
                    + " memo = '{2}'"
                    + " where information_level_id = {3}",
                    name, description, memo, id
                );

                try
                {
                    if (mode == "edit")
                    {
                        ora.ExecuteNonQuery(update);
                    }
                    else
                    {
                        ora.ExecuteNonQuery(insert);
                        int currID = GetMaxID();
                        // 修改id_path
                        UpdateIDPath(currID);

                        //当上级节点有资料时，自动移动到新增节点上：
                        string strSql = String.Format("select * from Information where Information_Level_ID={0}", parentID);
                        if (ora.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                        {
                            strSql =
                                string.Format(
                                    "update Information set Information_Level_ID={0} where Information_Level_ID={1}",
                                    currID, parentID);
                            ora.ExecuteNonQuery(strSql);
                        }
                    }

                    Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('保存成功');opener.location.reload();opener=null;close();", true);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('" + ex.Message + "')", true);
                }
            }
        }

        // 获取指定id下的子节点的最大序号
        private int GetMaxChildOrderIndex(int id)
        {
            string sql = String.Format("select nvl(max(order_index),0) from information_level where parent_id = {0}", id);
            OracleAccess ora = new OracleAccess();
            try
            {
                return Convert.ToInt32(ora.RunSqlDataSet(sql).Tables[0].Rows[0][0]);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        // 修改指定id的idpath
        private void UpdateIDPath(int id)
        {
            string sql = String.Format(
                "update information_level t1"
                 + " set t1.id_path = "
                 + " ("
                 + " select id_path||'/'||t1.information_level_id"
                 + " from information_level t2"
                 + " where t2.information_level_id = t1.parent_id"
                 + " )"
                 + " where t1.information_level_id = {0}",
                 id);
            try
            {
                ora.ExecuteNonQuery(sql);
            }
            catch { }
        }

        // 获取当前最大的ID号(即最新一条数据的ID)
        private int GetMaxID()
        {
            string sql = String.Format("select nvl(max(information_level_id),0) from information_level");
            return Convert.ToInt32(ora.RunSqlDataSet(sql).Tables[0].Rows[0][0]);
        }
    }
}
