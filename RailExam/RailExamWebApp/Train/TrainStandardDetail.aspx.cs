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
    public partial class TrainStandardDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = Request.QueryString.Get("id");

                TrainStandardBLL objTrainStandardBLL = new TrainStandardBLL();
                int nTypeID = objTrainStandardBLL.GetTrainStandardInfo(Convert.ToInt32(str)).TypeID;

                TrainTypeBLL objTrainTypeBLL = new TrainTypeBLL();
                int nLevel = objTrainTypeBLL.GetTrainTypeInfo(nTypeID).LevelNum;

                if (nLevel == 1)
                {
                    fvTrainStandard.Visible = false;
                }
                else
                {
                    fvTrainStandard.Visible = true;
                }
            }
        }
    }
}