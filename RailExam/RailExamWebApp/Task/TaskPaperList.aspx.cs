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
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.Task
{
    public partial class TaskPaperList : PageBase
    {
        private PaperBLL _paperBLL = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region // ComponentArt Callback Methods

        protected void searchPaperCallBack_Callback(object s, CallBackEventArgs e)
        {
            papersGrid.DataBind();
            papersGrid.RenderControl(e.Output);
        }

        #endregion // End of ComponentArt Callback Methods

        #region // Grid Event Handlers

        protected void papersGrid_OnSortCommand(object s, GridSortCommandEventArgs e)
        {
            papersGrid.Sort = e.SortExpression;
        }

        protected void papersGrid_OnPageIndexChanged(object s, GridPageIndexChangedEventArgs e)
        {
            papersGrid.CurrentPageIndex = e.NewIndex;
        }

        protected void papersGrid_OnDataBinding(object s, EventArgs e)
        {
            if (_paperBLL != null)
            {
                papersGrid.RecordCount = _paperBLL.GetRecordCount();
            }
        }

        #endregion // End of Grid Event Handlers

        #region // ObjectDataSource Event Handlers

        protected void odsPapers_OnObjectCreated(object s, ObjectDataSourceEventArgs e)
        {
            _paperBLL = (PaperBLL)e.ObjectInstance;
        }

        #endregion // End of ObjectDataSource Event Handlers
    }
}