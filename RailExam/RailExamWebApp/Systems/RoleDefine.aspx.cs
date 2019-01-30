using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class RoleDefine : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                if (PrjPub.HasEditRight("角色权限") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("角色权限") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    DeleteData(int.Parse(strDeleteID));
                    Grid1.DataBind();
                }

                if (Request.Form.Get("Refresh") == "true")
                {
                    Grid1.DataBind();
                }
            }
        }

        private void DeleteData(int nRoleID)
        {
            SystemUserBLL objBll = new SystemUserBLL();
            IList<SystemUser> objList = objBll.GetUsersByRoleID(nRoleID);
            if (objList.Count > 0)
            {
                SessionSet.PageMessage = "该角色已被引用，不能删除！";
                return;
            }
            SystemRoleBLL systemRoleBLL = new SystemRoleBLL();
            systemRoleBLL.DeleteRole(nRoleID);
        }
    }
}