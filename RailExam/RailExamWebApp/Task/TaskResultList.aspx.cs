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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp.Task
{
    public partial class TaskResultList : PageBase
    {
        private TaskResultBLL _taskResultBLL = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        #region // ComponentArt Callback Methods

        protected void searchTaskResultCallBack_Callback(object sender, CallBackEventArgs e)
        {
            taskResultGrid.DataBind();
            taskResultGrid.RenderControl(e.Output);
        }

        #endregion // End of ComponentArt Callback Methods

        #region // Ctrls Event Handlers

        protected void taskResultGrid_OnDataBinding(object s, EventArgs e)
        {
            if (_taskResultBLL != null)
            {
                taskResultGrid.RecordCount = _taskResultBLL.GetRecordCount();
            }
        }

        protected void taskResultGrid_OnSortCommand(object s, GridSortCommandEventArgs e)
        {
            taskResultGrid.Sort = e.SortExpression;
        }

        protected void taskResultGrid_OnPageIndexChanged(object s, GridPageIndexChangedEventArgs e)
        {
            taskResultGrid.CurrentPageIndex = e.NewIndex;
        }

        protected void odsTaskResults_OnDataObjectCreated(object s, ObjectDataSourceEventArgs e)
        {
            _taskResultBLL = (TaskResultBLL)e.ObjectInstance;
        }

        #endregion // End of Ctrls Event Handlers
    }
}