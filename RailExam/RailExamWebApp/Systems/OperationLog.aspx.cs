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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class OperationLog : PageBase
    {
        private IList<SystemLog> systemLogs = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                return;
            }
            if (!IsPostBack)
            {
                HfDeleteRight.Value = PrjPub.HasDeleteRight("操作日志").ToString();
                BindGrid();

            }

            if (IsPostBack)
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    DeleteData(int.Parse(strDeleteID));
                    BindGrid();
                }
            }
        }

        private void BindGrid()
        {
            DateTime start = new DateTime(2000, 1, 1);
            DateTime end = new DateTime(2100, 12, 31);
            if (dateBeginTime.DateValue.ToString() != string.Empty)
            {
                start = DateTime.Parse(dateBeginTime.DateValue.ToString());
            }
            if (dateEndTime.DateValue.ToString() != string.Empty)
            {
                end = DateTime.Parse(dateEndTime.DateValue.ToString());
            }

            SystemLogBLL systemLogBLL = new SystemLogBLL();
            string strFlag = "";
            if (PrjPub.CurrentLoginUser.OrgID == 1
                || PrjPub.CurrentLoginUser.StationOrgID == 200)   // StationOrgID: 200 表示包神铁路人力资源部职工培训中心
            {
                strFlag = "";
            }
            else
            {
                strFlag = PrjPub.CurrentLoginUser.OrgName;
            }

            systemLogs = systemLogBLL.GetLogs(
                txtOrgName.Text, 
                txtUserID.Text, 
                txtEmployeeName.Text,
                start, 
                end, 
                txtActionContent.Text, 
                txtMemo.Text, 
                strFlag
            );

            if (systemLogs != null)
            {
                Grid1.DataSource = systemLogs;
                Grid1.DataBind();
            }
        }

        private void DeleteData(int nLogID)
        {
            SystemLogBLL systemLogBLL = new SystemLogBLL();

            systemLogBLL.DeleteLog(nLogID);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();

            btnDeleteQuery.Visible = true;
        }

        protected void btnDeleteQuery_Click(object sender, EventArgs e)
        {
            DateTime start = new DateTime(2000, 1, 1);
            DateTime end = new DateTime(2100, 12, 31);
            if (dateBeginTime.DateValue.ToString() != string.Empty)
            {
                start = DateTime.Parse(dateBeginTime.DateValue.ToString());
            }
            if (dateEndTime.DateValue.ToString() != string.Empty)
            {
                end = DateTime.Parse(dateEndTime.DateValue.ToString());
            }

            SystemLogBLL systemLogBLL = new SystemLogBLL();
            string strFlag = "";
            if (PrjPub.CurrentLoginUser.OrgID == 1)
            {
                strFlag = "";
            }
            else
            {
                strFlag = PrjPub.CurrentLoginUser.OrgName;
            }

            systemLogs = systemLogBLL.GetLogs(txtOrgName.Text, txtUserID.Text, txtEmployeeName.Text,
               start, end, txtActionContent.Text, txtMemo.Text, strFlag);

            systemLogBLL.DeleteLogs(systemLogs);

            systemLogs = null;

            Grid1.DataSource = systemLogs;
            Grid1.DataBind();

            btnDeleteQuery.Visible = false;
        }

        protected void btnDelSelected_Click(object sender, EventArgs e)
        {
            if (hfSelectedIDs.Value != "")
            {
                string sql = "delete from SYSTEM_LOG where LOG_ID in (" + hfSelectedIDs.Value + ") ";
                try
                {
                    new OracleAccess().ExecuteNonQuery(sql);
                    BindGrid();
                }
                catch
                {
                }

            }
        }
    }
}