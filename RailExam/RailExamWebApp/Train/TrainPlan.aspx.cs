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
using DSunSoft.Web.UI;

namespace RailExamWebApp.Train
{
    public partial class TrainPlan : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnAddTrainPlan.Attributes.Add("onclick", "AddRecord();");

            string strRefresh = Request.Form.Get("Refresh");
            if (strRefresh != null & strRefresh != "")
            {
                Grid1.DataBind();
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != null & strDeleteID != "")
            {
                DeletePlan(strDeleteID);
                Grid1.DataBind();
            }
        }

        private static void DeletePlan(string strID)
        {
            TrainPlanBLL objTrainPlanBll = new TrainPlanBLL();
            objTrainPlanBll.DeleteTrainPlan(Convert.ToInt32(strID));
        }
    }
}
