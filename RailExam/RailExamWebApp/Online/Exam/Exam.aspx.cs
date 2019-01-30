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
using ET99_FULLLib;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Exam
{
    public partial class Exam : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			//if (PrjPub.IsEvaluation)
			//{
			//    DateTime strEnd = Convert.ToDateTime(PrjPub.EvaluationDate);

			//    if (DateTime.Today > strEnd)
			//    {
			//        Response.Write(ViewState["OverTime"].ToString());
			//    }
			//}

            if(!IsPostBack)
            {
				//hf.Value = "0";
            	hfIsWuhan.Value = PrjPub.IsWuhan().ToString();
            	hfIsShowResult.Value = ConfigurationManager.AppSettings["IsShowResult"];
            }
        }

		//protected void ImageButtonLogin_Click(object sender, ImageClickEventArgs e)
		//{
		//    #region ��֤�������˹���Ϣ
		//    ET99FullClass et99 = new ET99FullClass();
		//    Random random = new Random((int)(DateTime.Now.Ticks >> 32));
		//    string strServerRandomData = string.Empty;
		//    string strServerRandomResult = string.Empty;
		//    string strServerPid = string.Empty;
		//    string strServerUserpin = string.Empty;
		//    //string strServerSn = string.Empty;
		//    string strServerKey = string.Empty;

		//    for (int i = 0; i < 20; i++)
		//    {
		//        strServerRandomData += random.Next(9).ToString();
		//    }

		//    try
		//    {
		//        strServerPid = ConfigurationManager.AppSettings["PID"];
		//        et99.FindToken(strServerPid);
		//    }
		//    catch
		//    {
		//        SessionSet.PageMessage = "��������δ��⵽��������";
		//        return;
		//    }
		//    try
		//    {
		//        et99.OpenToken(strServerPid, 1);
		//    }
		//    catch
		//    {
		//        SessionSet.PageMessage = "�������˴򿪼�����ʧ�ܣ�";
		//        et99.CloseToken();
		//        return;
		//    }
		//    try
		//    {
		//        strServerUserpin = ConfigurationManager.AppSettings["USERPIN"];
		//        et99.VerifyPIN(0, strServerUserpin);
		//    }
		//    catch
		//    {
		//        SessionSet.PageMessage = "�������˼�����USERPIN����ȷ��";
		//        et99.CloseToken();
		//        return;
		//    }

		//    try
		//    {
		//        strServerKey = ConfigurationManager.AppSettings["KEY1"];
		//        strServerRandomResult = et99.MD5HMAC(1, strServerRandomData, 20).ToString();
		//        if (strServerRandomResult != et99.Soft_MD5HMAC(1, strServerRandomData, strServerKey).ToString())
		//        {
		//            SessionSet.PageMessage = "�������˼�������Կ����ȷ��";
		//            return;
		//        }
		//    }
		//    catch
		//    {
		//        SessionSet.PageMessage = "����������֤��������Կ����";
		//        return;
		//    }
		//    #endregion

		//    LoginUserBLL loginUserBLL = new LoginUserBLL();
		//    LoginUser loginUser;

		//    if (PrjPub.IsServerCenter)
		//    {
		//        loginUser = loginUserBLL.GetLoginUser(txtUserName.Text, txtPassword.Text, 0);
		//    }
		//    else
		//    {
		//        loginUser = loginUserBLL.GetLoginUser(txtUserName.Text, txtPassword.Text, 1);
		//    }

		//    if (loginUser == null)
		//    {
		//        SessionSet.PageMessage = "��������û��������벻��ȷ��";
		//        return;
		//    }

		//    PrjPub.CurrentStudent = loginUser;
		//    PrjPub.WelcomeInfo = loginUser.OrgName + "��" + loginUser.EmployeeName + "�����ã�";
		//    PrjPub.StudentID = loginUser.EmployeeID.ToString();
		//    Session["StudentOrdID"] = loginUser.OrgID;

		//    OrganizationBLL objOrgBll = new OrganizationBLL();
		//    //�ؼ���ʾ
		//    lblUserName.Text = "��&nbsp;λ:";
		//    lblOrg.Text = objOrgBll.GetOrganization(loginUser.StationOrgID).ShortName;

		//    lblPassword.Text = "��&nbsp;��:";
		//    lblEmployeeName.Text = loginUser.EmployeeName;

		//    txtUserName.Visible = false;
		//    txtPassword.Visible = false;
		//    ImageButtonLogin.Visible = false;
		//    ImageButtonLogout.Visible = true;
		//    lblOrg.Visible = true;
		//    lblEmployeeName.Visible = true;
		//    btnModify.Visible = true;

		//    hf.Value = "1";
		//}

		//protected void ImageButtonLogout_Click(object sender, ImageClickEventArgs e)
		//{
		//    PrjPub.CurrentStudent = null;
		//    PrjPub.WelcomeInfo = string.Empty;
		//    PrjPub.StudentID = string.Empty;
		//    Session.Remove("StudentOrdID");

		//    //�ؼ���ʾ
		//    lblUserName.Text = "�û���";
		//    txtUserName.Text = string.Empty;

		//    lblPassword.Text = "�� ��";

		//    txtUserName.Visible = true;
		//    lblOrg.Visible = false;
		//    txtPassword.Visible = true;
		//    lblEmployeeName.Visible = false;
		//    ImageButtonLogin.Visible = true;
		//    ImageButtonLogout.Visible = false;
		//    btnModify.Visible = false;
		//    hf.Value = "0";
		//}

		protected void Callback1_Callback(object sender, CallBackEventArgs e)
		{
			if (PrjPub.CurrentStudent != null)
			{
				string strCacheKey = PrjPub.CurrentStudent.EmployeeID.ToString();
				string strUser = Convert.ToString(HttpContext.Current.Cache[strCacheKey]);
				if (strUser != string.Empty)
				{
					HttpContext.Current.Cache.Remove(strCacheKey);
				}

				if(!string.IsNullOrEmpty(e.Parameters[0]))
				{
					SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
					objloginBll.DeleteSystemUserLogin(Convert.ToInt32(e.Parameters[0]));
				}
			}
		}
    }
}
