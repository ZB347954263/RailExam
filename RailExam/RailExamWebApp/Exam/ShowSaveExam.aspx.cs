using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class ShowSaveExam : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.HasEditRight("成绩查询"))
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                //UseType=0为路局角色
                if (PrjPub.HasDeleteRight("新增考试") && PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.IsAdmin)// && PrjPub.CurrentLoginUser.UseType == 0
                {
                    hfDeleteRight.Value = "True";
                }
                else
                {
                    hfDeleteRight.Value = "False";
                }

                //如果是访问路局则只要是有成绩查询权限的用户均可以访问成绩查询页面
                if (PrjPub.IsServerCenter)
                {
                    hfIsAdmin.Value = "True";
                }
                //如果是访问路局则非本站段的用户不能访问成绩查询页面
                else
                {
                    hfOrgID.Value = ConfigurationManager.AppSettings["StationID"].ToString();
                    if ((PrjPub.CurrentLoginUser.StationOrgID.ToString() == hfOrgID.Value) || PrjPub.CurrentLoginUser.UseType == 0)
                    {
                        hfIsAdmin.Value = "True";
                    }
                    else
                    {
                        hfIsAdmin.Value = "False";
                    }
                }

                hfIsServer.Value = PrjPub.IsServerCenter.ToString();

                //hfWhereCluase.Value = "a.Org_ID=" + Request.QueryString.Get("orgID")
                //        +" and (Save_Status=1 or (Save_Status=2 and sysdate>Save_Date))";
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (!string.IsNullOrEmpty(strDeleteID))
            {
                RandomExamBLL examBLL = new RandomExamBLL();
                RailExam.Model.RandomExam obj = examBLL.GetExam(Convert.ToInt32(strDeleteID));

                if (obj.HasTrainClass)
                {
                    SessionSet.PageMessage = "不能删除有培训班的考试！";
                    examsGrid.DataBind();
                    return;
                }

                examBLL.DeleteExam(int.Parse(strDeleteID));
                examsGrid.DataBind();
            }
        }
    }
}
