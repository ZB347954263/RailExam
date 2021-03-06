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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class RoleDefineDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Request.QueryString.Get("mode"))
                {
                    case "Edit":
                        FormView1.ChangeMode(FormViewMode.Edit);
                        break;

                    case "Insert":
                        FormView1.ChangeMode(FormViewMode.Insert);
                        break;

                    case "ReadOnly":
                        FormView1.ChangeMode(FormViewMode.ReadOnly);
                        break;
                }
            }
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            if (FormView1.CurrentMode == FormViewMode.Edit)
            {
                DropDownList dropDownList = ((DropDownList)FormView1.FindControl("ddlRailSystem"));
                ListItem item = new ListItem();
                item.Text = "所有铁路系统";
                item.Value = "0";
                dropDownList.Items.Add(item);

                string strSql = "select * from Rail_System";
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Rail_System_Name"].ToString();
                    item.Value = dr["Rail_System_ID"].ToString();
                    dropDownList.Items.Add(item);
                }

               dropDownList.SelectedValue = ((HiddenField)FormView1.FindControl("hfRailSystem")).Value;
            }
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
        }

        protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
        }

        protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
        }
    }
}