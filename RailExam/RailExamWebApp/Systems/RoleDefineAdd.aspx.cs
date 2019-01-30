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
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class RoleDefineAdd : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ListItem item = new ListItem();
                item.Text = "所有铁路系统";
                item.Value = "0";
                ddlRailSystem.Items.Add(item);

                string strSql = "select * from Rail_System";
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Rail_System_Name"].ToString();
                    item.Value = dr["Rail_System_ID"].ToString();
                    ddlRailSystem.Items.Add(item);
                }

                if(!string.IsNullOrEmpty(Request.QueryString.Get("id")))
                {
                    SystemRoleBLL objBll = new SystemRoleBLL();
                    SystemRole role = objBll.GetRole(Convert.ToInt32(Request.QueryString.Get("id")));

                    txtRoleNameInsert.Text = role.RoleName;
                    txtDescriptionInsert.Text = role.Description;
                    txtMemoInsert.Text = role.Memo;
                    chIsAdminInsert.Checked = role.IsAdmin;
                    ddlRailSystem.SelectedValue = role.RailSystemID.ToString();
                }
            }
        }

        protected  void InsertButton_Click(object sender,EventArgs e)
        {
            SystemRoleBLL objBll = new SystemRoleBLL();

            if (string.IsNullOrEmpty(Request.QueryString.Get("id")))
            {
                SystemRole role = new SystemRole();

                role.IsAdmin = chIsAdminInsert.Checked;
                role.RailSystemID = Convert.ToInt32(ddlRailSystem.SelectedValue);
                role.RoleName = txtRoleNameInsert.Text;
                role.Description = txtDescriptionInsert.Text;
                role.Memo = txtMemoInsert.Text;

                objBll.AddRole(role);
            }
            else
            {
                SystemRole role = objBll.GetRole(Convert.ToInt32(Request.QueryString.Get("id")));

                role.IsAdmin = chIsAdminInsert.Checked;
                role.RailSystemID = Convert.ToInt32(ddlRailSystem.SelectedValue);
                role.RoleName = txtRoleNameInsert.Text;
                role.Description = txtDescriptionInsert.Text;
                role.Memo = txtMemoInsert.Text;

                objBll.UpdateRole(role);
            }

            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
        }
    }
}
