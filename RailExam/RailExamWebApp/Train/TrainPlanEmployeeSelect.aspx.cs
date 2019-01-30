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

namespace RailExamWebApp.Train
{
    public partial class TrainPlanEmployeeSelect : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PlanID"] = Request.QueryString.Get("PlanID");
                ViewState["PlanName"] = Request.QueryString.Get("PlanName");
                ViewState["OrgID"] = Request.QueryString.Get("id");
                BindGrid1();
            }
        }

        private void BindGrid1()
        {
            TrainPlanEmployeeBLL objBll = new TrainPlanEmployeeBLL();
            IList<RailExam.Model.Employee> objEmployee = objBll.GetTrainEmployeeInfoByPlanID(Convert.ToInt32(ViewState["PlanID"].ToString()), Convert.ToInt32(ViewState["OrgID"].ToString()));

            Grid1.DataSource = objEmployee;
            Grid1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            TrainPlanEmployeeBLL trainPlanEmployeeBLL = new TrainPlanEmployeeBLL();
            ArrayList objList = trainPlanEmployeeBLL.GetEmployeeList(Convert.ToInt32(ViewState["PlanID"].ToString()));

            GridItemCollection activeItems = Grid1.GetCheckedItems(Grid1.Levels[0].Columns[0]);
            foreach (GridItem activeItem in activeItems)
            {
                if (objList.IndexOf(activeItem[1]) == -1)
                {
                    RailExam.Model.TrainPlanEmployee trainPlanEmployee = new RailExam.Model.TrainPlanEmployee();
                    trainPlanEmployee.TrainPlanID = Convert.ToInt32(ViewState["PlanID"].ToString());
                    trainPlanEmployee.TrainPlanEmployeeID = Convert.ToInt32(activeItem[1]);

                    trainPlanEmployeeBLL.AddTrainPlanEmployee(trainPlanEmployee);
                }
            }
            Grid2.DataBind();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            TrainPlanEmployeeBLL trainPlanEmployeeBLL = new TrainPlanEmployeeBLL();
            GridItemCollection activeItems = Grid2.GetCheckedItems(Grid2.Levels[0].Columns[0]);
            foreach (GridItem activeItem in activeItems)
            {
                RailExam.Model.TrainPlanEmployee trainPlanEmployee = new RailExam.Model.TrainPlanEmployee();
                trainPlanEmployee.TrainPlanID = Convert.ToInt32(ViewState["PlanID"].ToString());
                trainPlanEmployee.TrainPlanEmployeeID = Convert.ToInt32(activeItem[1]);

                trainPlanEmployeeBLL.DeleteTrainPlanEmployee(trainPlanEmployee.TrainPlanID, trainPlanEmployee.TrainPlanEmployeeID);
            }
            BindGrid1();
            Grid2.DataBind();
        }
    }
}
