using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Caching;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using ET99_FULLLib;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Exam
{
    public partial class StudentLogin : PageBase
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        private string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        /// <summary>   
        /// 获取Mac地址信息   
        /// </summary>   
        /// <param name="IP"></param>   
        /// <returns></returns>   
        public static string GetCustomerMac(string IP)
        {
            Int32 ldest = inet_addr(IP);
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            string mac_dest = "";

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }

            return mac_dest;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString.Get("type1");
            if (type == "StudySelected")
            {
                this.btnStudy.Visible = false;
            }
            else
            {
                if (Request.QueryString.Get("Type") == "middle" && Request.QueryString.Get("IsFinger")=="1")//!PrjPub.IsServerCenter &&
                {
                    string errorMessage = "";
                    string mac_dest = "";
                    try
                    {
                        mac_dest = GetCustomerMac(GetClientIP());
                    }
                    catch
                    {
                        errorMessage = "无法获取客户端MAC地址！";
                        mac_dest = "";
                    }

                    //errorMessage = mac_dest;
                    //mac_dest = "";

                    string employeeId = "";
                    string examId = "";
                    lblMAC.Text = mac_dest;
                    if(!string.IsNullOrEmpty(mac_dest))
                    {
                        string strSql = "select * from Computer_Room_Detail"
                                        + " where MAC_Address='" + mac_dest + "'";

                        OracleAccess db = new OracleAccess();

                        DataSet ds = db.RunSqlDataSet(strSql);

                        if(ds.Tables[0].Rows.Count>0)
                        {
                            string computerId = ds.Tables[0].Rows[0]["Computer_Room_ID"].ToString();
                            string computerSeat = ds.Tables[0].Rows[0]["Computer_Room_Seat"].ToString();

                            strSql = "select * from Random_Exam_Result_Detail_Temp "
                                     + " where Computer_Room_ID=" + computerId
                                     + " and Computer_Room_Seat=" + computerSeat
                                     +" and FingerPrint is not null and Is_Remove=0";

                            DataSet dsResult = db.RunSqlDataSet(strSql);

                            if(dsResult.Tables[0].Rows.Count>0)
                            {
                                employeeId = dsResult.Tables[0].Rows[0]["Employee_ID"].ToString();
                                examId = dsResult.Tables[0].Rows[0]["Random_Exam_ID"].ToString();

                                strSql = "select a.* from System_User a "
                                         + " inner join Employee b on a.Employee_ID=b.Employee_ID "
                                         + " where a.Employee_ID=" + employeeId;

                                DataSet dsSystem = db.RunSqlDataSet(strSql);

                                LoginUserBLL loginUserBLL = new LoginUserBLL();
                                LoginUser loginUser;
                                if(dsSystem.Tables[0].Rows.Count > 0)
                                {
                                    loginUser = loginUserBLL.GetLoginUser(dsSystem.Tables[0].Rows[0]["User_ID"].ToString(), dsSystem.Tables[0].Rows[0]["Password"].ToString(), 1);
                                }
                                else
                                {
                                    strSql = "select * from  Employee  "
                                        + " where Employee_ID=" + employeeId;
                                    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                                    string strUserId;
                                    if(dr["Work_No"] == DBNull.Value)
                                    {
                                        strUserId = dr["Identity_CardNo"].ToString();
                                    }
                                    else
                                    {
                                        strUserId = dr["Work_No"].ToString();
                                    }

                                    strSql = "insert into System_User values("
                                             + "'" + strUserId + "','111111',"
                                             + employeeId + ",0,null)";
                                    db.ExecuteNonQuery(strSql);

                                    //strSql = "begin dbms_mview.refresh('System_User','?'); end;";
                                    //db.ExecuteNonQuery(strSql);

                                    loginUser = loginUserBLL.GetLoginUser(strUserId, "111111", 1);
                                }

                                if (string.IsNullOrEmpty(Request.QueryString.Get("type1")))
                                {
                                    string strUser = string.Empty;
                                    string strCacheKey = loginUser.EmployeeID.ToString();

                                    strUser = Convert.ToString(Cache[strCacheKey]);

                                    SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
                                    IList<SystemUserLogin> objList = objloginBll.GetSystemUserLogin(loginUser.EmployeeID);

                                    if (strUser == string.Empty || objList.Count == 0)
                                    {
                                        TimeSpan SessTimeOut = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);

                                        Cache.Insert(strCacheKey, strCacheKey, null, DateTime.MaxValue, SessTimeOut,
                                                     CacheItemPriority.NotRemovable, null);
                                        Session["User"] = strCacheKey;

                                        if (objList.Count == 0)
                                        {
                                            SystemUserLogin objLogin = new SystemUserLogin();
                                            objLogin.EmployeeID = loginUser.EmployeeID;
                                            objLogin.IPAddress = Pub.GetRealIP();
                                            objloginBll.DeleteSystemUserLogin(loginUser.EmployeeID);
                                            objloginBll.AddSystemUserLogin(objLogin);
                                        }
                                    }
                                }

                                PrjPub.CurrentStudent = loginUser;
                                PrjPub.WelcomeInfo = loginUser.OrgName + "：" + loginUser.EmployeeName + "，您好！";
                                PrjPub.StudentID = loginUser.EmployeeID.ToString();
                                hfEmployeeID.Value = loginUser.EmployeeID.ToString();
                                Session["StudentOrdID"] = loginUser.OrgID;
                                SessionSet.UserID = loginUser.UserID;
                                SessionSet.EmployeeID = loginUser.EmployeeID;
                                SessionSet.EmployeeName = loginUser.EmployeeName;
                                SessionSet.OrganizationID = loginUser.OrgID;
                                SessionSet.OrganizationName = loginUser.OrgName;
                                SessionSet.StationOrgID = loginUser.StationOrgID;

                                OrganizationBLL objOrgBll = new OrganizationBLL();
                                //控件显示
                                lblUserName.Text = "姓&nbsp;名："+DateTime.Now;
                                lblOrg.Text = objOrgBll.GetOrganization(loginUser.StationOrgID).ShortName;

                                lblPassword.Text = "单&nbsp;位：";
                                lblEmployeeName.Text = loginUser.EmployeeName;

                                trCard.Visible = false;
                                lbl.Visible = true;
                                lblPost.Visible = true;
                                lblPost.Text = loginUser.PostName;


                                txtUserName.Visible = false;
                                txtPassword.Visible = false;
                                ImageButtonLogin.Visible = false;
                                btnExit.Visible = true;
                                lblOrg.Visible = true;
                                lblEmployeeName.Visible = true;
                                //btnModifyPsw.Visible = true;
                                btnExam.Visible = true;
                            }
                        }
                        //else
                        //{
                        //    errorMessage = "系统中不存在当前客户端的MAC地址！";
                        //}
                    }

                    ClientScript.RegisterStartupScript(GetType(),
                            "jsSelectFirstNode",
                            @"ShowStudentExam('" + errorMessage + "','" + employeeId + "','" + examId + "');",
                            true);

                }
            }

            lbl1.Text = "员工编码：";
        }
		protected void ImageButtonLogin_Click(object sender, EventArgs e)
		{
			#region 验证服务器端狗信息

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PID"]))
            {

                ET99FullClass et99 = new ET99FullClass();
                Random random = new Random((int) (DateTime.Now.Ticks >> 32));
                string strServerRandomData = string.Empty;
                string strServerRandomResult = string.Empty;
                string strServerPid = string.Empty;
                string strServerUserpin = string.Empty;
                //string strServerSn = string.Empty;
                string strServerKey = string.Empty;

                for (int i = 0; i < 20; i++)
                {
                    strServerRandomData += random.Next(9).ToString();
                }

                try
                {
                    strServerPid = ConfigurationManager.AppSettings["PID"];
                    et99.FindToken(strServerPid);
                }
                catch
                {
                    SessionSet.PageMessage = "服务器端未检测到加密锁！";
                    return;
                }
                try
                {
                    et99.OpenToken(strServerPid, 1);
                }
                catch
                {
                    SessionSet.PageMessage = "服务器端打开加密锁失败！";
                    et99.CloseToken();
                    return;
                }
                try
                {
                    strServerUserpin = ConfigurationManager.AppSettings["USERPIN"];
                    et99.VerifyPIN(0, strServerUserpin);
                }
                catch
                {
                    SessionSet.PageMessage = "服务器端加密锁USERPIN不正确！";
                    et99.CloseToken();
                    return;
                }

                try
                {
                    strServerKey = ConfigurationManager.AppSettings["KEY1"];
                    strServerRandomResult = et99.MD5HMAC(1, strServerRandomData, 20).ToString();
                    if (strServerRandomResult != et99.Soft_MD5HMAC(1, strServerRandomData, strServerKey).ToString())
                    {
                        SessionSet.PageMessage = "服务器端加密锁密钥不正确！";
                        return;
                    }
                }
                catch
                {
                    SessionSet.PageMessage = "服务器端验证加密锁密钥出错！";
                    return;
                }
            }

		    #endregion

            if(txtCardID.Text.Trim() == string.Empty && txtUserName.Text.Trim() == string.Empty)
            {
                SessionSet.PageMessage = "请输入员工编码或身份证号！";
                return;
            }

			LoginUserBLL loginUserBLL = new LoginUserBLL();
			LoginUser loginUser;

			if (PrjPub.IsServerCenter)
			{
				loginUser = loginUserBLL.GetLoginUser(txtUserName.Text, txtPassword.Text, 0);
				if(loginUser==null)
					loginUser = loginUserBLL.GetLoginUserByCardID(txtCardID.Text, txtPassword.Text, 0);
			}
			else
			{
				loginUser = loginUserBLL.GetLoginUser(txtUserName.Text, txtPassword.Text, 1);
				if (loginUser == null)
					loginUser = loginUserBLL.GetLoginUserByCardID(txtCardID.Text, txtPassword.Text, 1);
			}

			if (loginUser == null)
			{
				SessionSet.PageMessage = "您输入的员工编码或身份证号或密码不正确！";
				return;
			}

            if (string.IsNullOrEmpty(Request.QueryString.Get("type1")))
            {
                string strUser = string.Empty;
                string strCacheKey = loginUser.EmployeeID.ToString();

                strUser = Convert.ToString(Cache[strCacheKey]);

                SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
                IList<SystemUserLogin> objList = objloginBll.GetSystemUserLogin(loginUser.EmployeeID);

                if (strUser == string.Empty || objList.Count == 0)
                {
                    TimeSpan SessTimeOut = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);

                    Cache.Insert(strCacheKey, strCacheKey, null, DateTime.MaxValue, SessTimeOut,
                                 CacheItemPriority.NotRemovable, null);
                    Session["User"] = strCacheKey;

                    if (objList.Count == 0)
                    {
                        SystemUserLogin objLogin = new SystemUserLogin();
                        objLogin.EmployeeID = loginUser.EmployeeID;
                        objLogin.IPAddress = Pub.GetRealIP();
                        objloginBll.DeleteSystemUserLogin(loginUser.EmployeeID);
                        objloginBll.AddSystemUserLogin(objLogin);
                    }
                }
                else
                {
                    SessionSet.PageMessage = "该用户已经登录，不能重复登录！";
                    return;
                }
            }

		    PrjPub.CurrentStudent = loginUser;
			PrjPub.WelcomeInfo = loginUser.OrgName + "：" + loginUser.EmployeeName + "，您好！";
			PrjPub.StudentID = loginUser.EmployeeID.ToString();
			hfEmployeeID.Value = loginUser.EmployeeID.ToString();
			Session["StudentOrdID"] = loginUser.OrgID;
			SessionSet.UserID = loginUser.UserID;
			SessionSet.EmployeeID = loginUser.EmployeeID;
			SessionSet.EmployeeName = loginUser.EmployeeName;
			SessionSet.OrganizationID = loginUser.OrgID;
			SessionSet.OrganizationName = loginUser.OrgName;
			SessionSet.StationOrgID = loginUser.StationOrgID;

			OrganizationBLL objOrgBll = new OrganizationBLL();
			//控件显示
			lblUserName.Text = "姓&nbsp;名：";
			lblOrg.Text = objOrgBll.GetOrganization(loginUser.StationOrgID).ShortName;

			lblPassword.Text = "单&nbsp;位：";
			lblEmployeeName.Text = loginUser.EmployeeName;

			trCard.Visible = false;
			lbl.Visible = true;
			lblPost.Visible = true;
			lblPost.Text = loginUser.PostName;

			if (Request.QueryString.Get("Type") == "middle")
			{
				txtUserName.Visible = false;
				txtPassword.Visible = false;
				ImageButtonLogin.Visible = false;
				btnExit.Visible = true;
				lblOrg.Visible = true;
				lblEmployeeName.Visible = true;
				//btnModifyPsw.Visible = true;
				btnExam.Visible = true;

				//ClientScript.RegisterStartupScript(GetType(),
				//        "jsSelectFirstNode",
				//        @"ShowExamList();",
				//        true);

				//ClientScript.RegisterStartupScript(GetType(), "import", "inputCallback.callback('middle');", true);
			}
			else if (Request.QueryString.Get("Type") == "right")
			{
				txtUserName.Visible = false;
				txtPassword.Visible = false;
				ImageButtonLogin.Visible = false;
				btnExit.Visible = true;
				lblOrg.Visible = true;
				lblEmployeeName.Visible = true;
				btnModifyPsw.Visible = true;
				btnResult.Visible = true;

				//ClientScript.RegisterStartupScript(GetType(),
				//    "jsSelectFirstNode",
				//    @"ShowResultList();",
				//    true);
				
				//ClientScript.RegisterStartupScript(GetType(), "import", "inputCallback.callback('right');", true);
			}
			else if (Request.QueryString.Get("Type") == "left")
			{
				//将此人的登录次数+1
                //if (loginUser.EmployeeID != 0)
                //{
                //    EmployeeBLL objEmpBll = new EmployeeBLL();
                //    Employee employee = objEmpBll.GetEmployee(loginUser.EmployeeID);

                //    employee.LoginCount = employee.LoginCount + 1;
                //    if (PrjPub.IsServerCenter)
                //    {
                //        objEmpBll.UpdateEmployee(employee);
                //    }
                //    else
                //    {
                //        objEmpBll.UpdateEmployeeInStation(employee);
                //    }
                //}

			    txtUserName.Visible = false;
				txtPassword.Visible = false;
				ImageButtonLogin.Visible = false;
				btnExit.Visible = true;
				lblOrg.Visible = true;
				lblEmployeeName.Visible = true;
				btnModifyPsw.Visible = true;
				btnExam.Visible = false;
				btnStudy.Visible = true;
			}

            string type = Request.QueryString.Get("type1");
            if (type == "StudySelected")
            {
                btnStudy.Visible = true;
            }
		}

		protected void btnExit_Click(object sender, EventArgs e)
		{
			if(PrjPub.CurrentStudent != null)
			{
				string strCacheKey = PrjPub.CurrentStudent.EmployeeID.ToString();
				string strUser = Convert.ToString(Cache[strCacheKey]);
				if (strUser != string.Empty)
				{
					Cache.Remove(strCacheKey);
					SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
					objloginBll.DeleteSystemUserLogin(Convert.ToInt32(strCacheKey));
				}
			}

			PrjPub.CurrentStudent = null;
			PrjPub.WelcomeInfo = string.Empty;
			PrjPub.StudentID = string.Empty;
			Session.Remove("StudentOrdID");

			//控件显示
			lblUserName.Text = "用户名";
			txtUserName.Text = string.Empty;

			lblPassword.Text = "密 码";

			txtUserName.Visible = true;
			lblOrg.Visible = false;
			txtPassword.Visible = true;
			lblEmployeeName.Visible = false;
			ImageButtonLogin.Visible = true;
			btnExit.Visible = false;
			btnModifyPsw.Visible = false;
			btnResult.Visible = false;

			Response.Write("<script> top.returnValue = 'true';window.close();</script>");
		}

		protected void inputCallback_Callback(object sender, CallBackEventArgs e)
		{
			hfType.Value = e.Parameters[0];
			hfType.RenderControl(e.Output);
		}
    }
}
