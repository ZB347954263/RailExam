﻿using System;
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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Systems
{
    public partial class FunctionDefine : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SystemRoleRightBLL systemRoleRightBLL = new SystemRoleRightBLL();

            IList<SystemRoleRight> systemRoleRights = new List<SystemRoleRight>();

            int roleID = int.Parse(Request.QueryString["id"]);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //string str = GridView1.Rows[i].Cells[0].Text;
                SystemRoleRight systemRoleRight = new SystemRoleRight();

                systemRoleRight.RoleID = roleID;

                systemRoleRight.FunctionID = ((Label)GridView1.Rows[i].FindControl("lblFunctionID")).Text;

                systemRoleRight.FunctionRight = ((RadioButtonList)GridView1.Rows[i].FindControl("rblFunctionRight")).SelectedIndex;
				
				systemRoleRight.RangeRight = int.Parse(((Label)GridView1.Rows[i].FindControl("lblRangeRight")).Text);

                systemRoleRights.Add(systemRoleRight);
            }

            systemRoleRightBLL.UpdateRoleRight(roleID, systemRoleRights);
			ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('保存成功！');window.close();", true);
            //Response.Write("<script>window.close();</script>");
        }
    }
}
