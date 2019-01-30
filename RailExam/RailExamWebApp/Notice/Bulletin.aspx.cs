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

namespace RailExamWebApp.Notice
{
    public partial class Bulletin : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                HfUpdateRight.Value = PrjPub.HasEditRight("信息公告").ToString();
                HfDeleteRight.Value = PrjPub.HasDeleteRight("信息公告").ToString();


                ListItem Li = new ListItem();
                Li.Value = "0";
                Li.Text = "请选择！";
                ddlImportanceName.Items.Add(Li);

                ImportanceBLL importanceBLL = new ImportanceBLL();

                IList<Importance> importances = importanceBLL.GetImportances();
                if (importances != null)
                {
                    foreach (Importance importance in importances)
                    {
                        Li = new ListItem();
                        Li.Value = importance.ImportanceID.ToString();
                        Li.Text = importance.ImportanceName;
                        ddlImportanceName.Items.Add(Li);
                    }
                }

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
            BulletinBLL bulletinBLL = new BulletinBLL();

            DateTime start = new DateTime(2000, 1, 1);
            DateTime end = new DateTime(2100, 12, 31);

            if (dateBeginTime.DateValue.ToString()!= "")
            {
                start = DateTime.Parse(dateBeginTime.DateValue.ToString());
            }
                 if (dateBeginTime.DateValue.ToString()!= "")
            {
                end = DateTime.Parse(dateEndTime.DateValue.ToString());
            }
          

            IList<RailExam.Model.Bulletin> bulletins = bulletinBLL.GetBulletins(txtTitle.Text, int.Parse(ddlImportanceName.SelectedValue),
                txtOrgName.Text, txtEmployeeName.Text, start, end, PrjPub.CurrentLoginUser.IsAdmin,  PrjPub.CurrentLoginUser.EmployeeID);
            if (bulletins != null)
            {
                Grid1.DataSource = bulletins;
                Grid1.DataBind();
            }
        }

        private void DeleteData(int nBulletinID)
        {
            BulletinBLL bulletinBLL = new BulletinBLL();

            bulletinBLL.DeleteBulletin(nBulletinID);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}
