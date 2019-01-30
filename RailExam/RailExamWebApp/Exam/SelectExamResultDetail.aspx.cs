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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class SelectExamResultDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
			{
				if (PrjPub.HasEditRight("�ɼ���ѯ"))
				{
					HfUpdateRight.Value = "True";
				}
				else
				{
					HfUpdateRight.Value = "False";
				}

				if(PrjPub.HasDeleteRight("��������") && PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 0)
				{
					hfDeleteRight.Value = "True";
				}
				else
				{
					hfDeleteRight.Value = "False";
				}

				//����Ƿ���·����ֻҪ���гɼ���ѯȨ�޵��û������Է��ʳɼ���ѯҳ��
				if (PrjPub.IsServerCenter)
				{
					hfIsAdmin.Value = "True";
				}
				//����Ƿ���·����Ǳ�վ�ε��û����ܷ��ʳɼ���ѯҳ��
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

                dateStartDateTime.DateValue = DateTime.Today.Year +"-"+DateTime.Today.Month + "-01";
			    dateEndDateTime.DateValue = DateTime.Today.ToString("yyyy-MM-dd");

                if(!string.IsNullOrEmpty(Request.QueryString.Get("begin")))
                {
                    dateStartDateTime.DateValue = Request.QueryString.Get("begin");
                }

                if (!string.IsNullOrEmpty(Request.QueryString.Get("end")))
                {
                    dateEndDateTime.DateValue = Request.QueryString.Get("end");
                }

				hfIsServer.Value = PrjPub.IsServerCenter.ToString();
				//hfHasTwoServer.Value = PrjPub.HasTwoServer().ToString();
				//hfIsMainServer.Value = PrjPub.IsMainServer().ToString();

                if (Request.QueryString.Get("Orgid") != "1" && !string.IsNullOrEmpty(Request.QueryString.Get("Orgid")))
                {
                    BindGrid();
                }
			}

			string strDeleteID = Request.Form.Get("DeleteID");
			if (!string.IsNullOrEmpty(strDeleteID))
			{
				RandomExamBLL examBLL = new RandomExamBLL();
				RailExam.Model.RandomExam obj = examBLL.GetExam(Convert.ToInt32(strDeleteID));

				if(obj.HasTrainClass)
				{
					SessionSet.PageMessage = "����ɾ������ѵ��Ŀ��ԣ�";
				    BindGrid();
                    return;
				}

				examBLL.DeleteExam(int.Parse(strDeleteID));
			    BindGrid();
			}
        }

        private void BindGrid()
        {
            ExamBLL examBll = new ExamBLL();

            int categoryId = hfCategoryId.Value == "" ? -1 : Convert.ToInt32(hfCategoryId.Value);
            DateTime beginDate = dateStartDateTime.DateValue == null
                                     ? Convert.ToDateTime("0001-01-01")
                                     : Convert.ToDateTime(dateStartDateTime.DateValue);
            DateTime endDate = dateEndDateTime.DateValue == null
                                     ? Convert.ToDateTime("0001-01-01")
                                     : Convert.ToDateTime(dateEndDateTime.DateValue);
            int orgId = Convert.ToInt32(Request.QueryString.Get("Orgid"));
            examsGrid.DataSource = examBll.GetExamsInfoByOrgID(txtExamName.Text, categoryId, beginDate, endDate, orgId,
                                                               hfIsServer.Value);
            examsGrid.DataBind();
        }

        protected void searchButton_onClick(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}
