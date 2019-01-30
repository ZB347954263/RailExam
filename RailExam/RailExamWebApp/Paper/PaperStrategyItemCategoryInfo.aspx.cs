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

namespace RailExamWebApp.Paper
{
    public partial class PaperStrategyItemCategoryInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfStrategySubjectID.Value = Request.QueryString.Get("id");
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    DeleteData(int.Parse(strDeleteID));
                 }
                    Grid1.DataBind();
                
            }
        }

        private void DeleteData(int nBookID)
        {
            PaperStrategyItemCategoryBLL paperStrategyItemCategoryBLL = new PaperStrategyItemCategoryBLL();

            paperStrategyItemCategoryBLL.DeletePaperStrategyItemCategory(nBookID);
        }
    }
}