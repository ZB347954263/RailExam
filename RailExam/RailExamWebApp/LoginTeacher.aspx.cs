using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Runtime.InteropServices;
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
using RailExam.Model;
using RailExamWebApp.Common.Class;
using System.Collections.Generic;

namespace RailExamWebApp
{
    public partial class LoginTeacher : PageBase
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
            Session.Abandon();
            //if (!PrjPub.IsServerCenter)
            //{
            //    RefreshSnapShotBLL objBll = new RefreshSnapShotBLL();
            //    if (!objBll.IsExistsRefreshSnapShot("BOOK", "MATERIALIZED VIEW"))
            //    {
            //        Response.Redirect("RefreshDataDefault.aspx?Type=teacher");
            //    }
            //}

			//if(PrjPub.IsEvaluation)
			//{
			//    DateTime strEnd = Convert.ToDateTime(PrjPub.EvaluationDate);

			//    if(DateTime.Today > strEnd)
			//    {
			//        Response.Write(ViewState["OverTime"].ToString());
			//    }
			//}

			if (!PrjPub.IsServerCenter)
			{
				try
				{
                    //if (PrjPub.IsWuhan())
                    //{
                    //    SynchronizeLogBLL objlogbll = new SynchronizeLogBLL();
                    //    IList<SynchronizeLog> objList =
                    //        objlogbll.GetSynchronizeLogByOrgIDAndTypeID(Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]), 6);
                    //    if (objList.Count == 0)
                    //    {
                    //        Response.Redirect("/RailExamBao/Common/OtherError.aspx?error=请先使用同步器上传成绩答卷后，再使用本系统");
                    //    }
                    //    else
                    //    {
                    //        foreach (SynchronizeLog log in objList)
                    //        {
                    //            if (DateTime.Today.Year == log.BeginTime.Year)
                    //            {
                    //                if (DateTime.Today.Month > log.BeginTime.Month && DateTime.Today.Day > 25)
                    //                {
                    //                    Response.Redirect("/RailExamBao/Common/OtherError.aspx?error=请先使用同步器上传成绩答卷后，再使用本系统");
                    //                }
                    //            }
                    //            else if (DateTime.Today.Year > log.BeginTime.Year)
                    //            {
                    //                if (DateTime.Today.Day > 25)
                    //                {
                    //                    Response.Redirect("/RailExamBao/Common/OtherError.aspx?error=请先使用同步器上传成绩答卷后，再使用本系统");
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

					try
					{

                        OracleAccess oaCenter = new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
                        string sql = "select * from Org where org_id=1";
                        DataSet ds = oaCenter.RunSqlDataSet(sql);

                        SystemVersionBLL objSystemBll = new SystemVersionBLL();
                        if (objSystemBll.GetVersion() != objSystemBll.GetVersionToServer())
                        {
                            ClientScript.RegisterStartupScript(GetType(),
                                "jsSelectFirstNode",
                                @"ShowInfo();",
                                true);
                        }
					}
					catch 
					{
						if (ConfigurationManager.AppSettings["StationID"].ToString() == "")
						{
							Response.Redirect("RefreshDataDefault.aspx?Type=teacher");
						}
					}

					if (ConfigurationManager.AppSettings["StationID"].ToString() == "")
					{
						Response.Redirect("RefreshDataDefault.aspx?Type=teacher");
					}
				}
				catch
				{
					if (ConfigurationManager.AppSettings["StationID"].ToString() == "")
					{
						Response.Redirect("RefreshDataDefault.aspx?Type=teacher");
					}
				}
			}
        }

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
			}

			SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
			objloginBll.DeleteSystemUserLogin(Convert.ToInt32(e.Parameters[0]));
		}

        protected void btnExam_Click(object sender, EventArgs e)
        {
            string IsFinger = "0";
            string errorMessage = "";
            string mac_dest = "";
            try
            {
                mac_dest = GetCustomerMac(GetClientIP());
            }
            catch
            {
                mac_dest = "";
            }

            if (!string.IsNullOrEmpty(mac_dest))
            {
                string strSql = "select * from Computer_Room_Detail"
                                + " where MAC_Address='" + mac_dest + "'";

                OracleAccess db = new OracleAccess();

                DataSet ds = db.RunSqlDataSet(strSql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string computerId = ds.Tables[0].Rows[0]["Computer_Room_ID"].ToString();
                    string computerSeat = ds.Tables[0].Rows[0]["Computer_Room_Seat"].ToString();

                    strSql = "select * from Random_Exam_Result_Detail_Temp "
                             + " where Computer_Room_ID=" + computerId
                             + " and Computer_Room_Seat=" + computerSeat
                             + " and FingerPrint is not null and Is_Remove=0";

                    DataSet dsResult = db.RunSqlDataSet(strSql);

                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        IsFinger = "1";
                    }
                }
            }

            ClientScript.RegisterStartupScript(GetType(),
                            "jsSelectFirstNode",
                            @"ShowExam('" + IsFinger + "');",
                            true);
        }
    }
}
