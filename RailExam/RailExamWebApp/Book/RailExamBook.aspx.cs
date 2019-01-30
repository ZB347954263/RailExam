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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Book
{
    public partial class RailExamBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
                    return;
                }

                string employeeId = PrjPub.CurrentLoginUser.EmployeeID.ToString();
                string orgId = PrjPub.CurrentLoginUser.OrgID.ToString();
                string stationId = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                string hasEdit=string.Empty, deleteEdit=string.Empty;
                string employeeName = PrjPub.CurrentLoginUser.EmployeeName;
                string orgName = PrjPub.CurrentLoginUser.OrgName;
                string suitRange = PrjPub.CurrentLoginUser.SuitRange.ToString();
                string strSession;

                string page=string.Empty;
                string strtype = Request.QueryString.Get("type");
                if(strtype == "1")
                {
                    hasEdit = PrjPub.HasEditRight("������ϵ").ToString();
                    deleteEdit = PrjPub.HasDeleteRight("������ϵ").ToString();

                    page = "knowledge/knowledge.aspx";
                }
                else if(strtype == "2")
                {
                    hasEdit = PrjPub.HasEditRight("�������").ToString();
                    deleteEdit = PrjPub.HasDeleteRight("�������").ToString();

                    page = "Train/TrainTypes.aspx";
                }
                else if(strtype == "3")
                {
                    hasEdit = PrjPub.HasEditRight("���Ϲ���").ToString();
                    deleteEdit = PrjPub.HasDeleteRight("���Ϲ���").ToString();

                    page = "Book/Book.aspx";
                }

                strSession = employeeId + "|" + orgId + "|" + stationId + "|" + hasEdit + "|" + deleteEdit + "|" +
                             employeeName + "|" + orgName + "|" + suitRange;

                Response.Redirect("/RailExam/"+page+"?type=1&strSession="+strSession);
            }
        }
    }
}
