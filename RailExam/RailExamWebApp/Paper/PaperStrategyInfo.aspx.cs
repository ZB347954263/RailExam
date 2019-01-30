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
using DSunSoft.Web.Global;
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Paper
{
    public partial class PaperStrategyInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }           
                if (PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange != 1)
                {
                    HfUpdateRight.Value = "False";
                    HfDeleteRight.Value = "False";
                }
                else
                {
                    HfUpdateRight.Value = PrjPub.HasEditRight("组卷策略").ToString();
                    HfDeleteRight.Value = PrjPub.HasDeleteRight("组卷策略").ToString();
                }
                HfPaperCategoryId.Value = Request.QueryString.Get("id");

            }

            if (IsPostBack)
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    DeleteData(int.Parse(strDeleteID));
                    Grid1.DataBind();
                }

                if (Request.Form.Get("Refresh") == "true")
                 {
                     
                    Grid1.DataBind();
                 }
                    
                 
            }
        }

        private void DeleteData(int nPaperStrategyID)
        {
            PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();

            paperStrategyBLL.DeletePaperStrategy(nPaperStrategyID);
        }
    }
}