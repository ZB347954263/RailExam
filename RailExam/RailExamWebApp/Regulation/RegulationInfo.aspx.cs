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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Regulation
{
    public partial class RegulationInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HfUpdateRight.Value = PrjPub.HasEditRight("政策法规").ToString();
                HfDeleteRight.Value = PrjPub.HasDeleteRight("政策法规").ToString();
                BindGrid();
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    DeleteData(int.Parse(strDeleteID));
                    BindGrid();
                }

                if (Request.Form.Get("Refresh") == "true")
                {
                    BindGrid();
                }
            }
        }

        private void BindGrid()
        {
            string strIDPath = Request.QueryString["id"];

            RegulationBLL regulationBLL = new RegulationBLL();
            IList<RailExam.Model.Regulation> regulations = regulationBLL.GetRegulationsByCategoryIDPath(strIDPath);

            if (regulations != null)
            {
                Grid1.DataSource = regulations;
                Grid1.DataBind();
            }
        }

        private void DeleteData(int nRegulationID)
        {
            RegulationBLL regulationBLL = new RegulationBLL();

            regulationBLL.DeleteRegulation(nRegulationID);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
           // int nID = int.Parse(Request.QueryString["id"]);

            RegulationBLL regulationBLL = new RegulationBLL();
            IList<RailExam.Model.Regulation> regulations = regulationBLL.GetRegulations(txtRegulationName.Text, txtRegulationNo.Text, int.Parse(ddlStatus.SelectedValue));

            if (regulations != null)
            {
                Grid1.DataSource = regulations;
                Grid1.DataBind();
            }
        }
    }
}