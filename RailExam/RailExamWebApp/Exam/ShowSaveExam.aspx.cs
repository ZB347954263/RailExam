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
                if (PrjPub.HasEditRight("�ɼ���ѯ"))
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                //UseType=0Ϊ·�ֽ�ɫ
                if (PrjPub.HasDeleteRight("��������") && PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.IsAdmin)// && PrjPub.CurrentLoginUser.UseType == 0
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
                    SessionSet.PageMessage = "����ɾ������ѵ��Ŀ��ԣ�";
                    examsGrid.DataBind();
                    return;
                }

                examBLL.DeleteExam(int.Parse(strDeleteID));
                examsGrid.DataBind();
            }
        }
    }
}
