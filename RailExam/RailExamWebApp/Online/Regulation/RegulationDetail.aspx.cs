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
using DSunSoft.Web.UI;

namespace RailExamWebApp.Online.Regulation
{
    public partial class RegulationDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strRegulationID = Request.QueryString["id"];

                RegulationBLL regulationBLL = new RegulationBLL();
                RailExam.Model.Regulation regulation = regulationBLL.GetRegulationByRegulationID(int.Parse(strRegulationID));

                ViewState["RegulationName"] = regulation.RegulationName;
            }
        }
    }
}
