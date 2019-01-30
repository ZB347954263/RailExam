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
using RailExamWebApp.Systems;

/// <summary>
/// PrjPub 的摘要说明




/// </summary>
namespace RailExamWebApp.Common.Class
{
    public static class PrjPub
    {
    	public const bool IsEvaluation = false;
    	public const string EvaluationDate = "2008-11-01";
        public const int DEFAULT_INT_IN_DB = -1;
        public const string DEFAULT_STRING_IN_DB = "null";
        public const string BOOKUPDATEOBJECT_BOOKINFO = "教材基本信息";
        public const string BOOKUPDATEOBJECT_BOOKCOVER = "教材前言";
        public const string BOOKUPDATEOBJECT_DELBOOK = "删除教材";
        public const string BOOKUPDATEOBJECT_INSERTCHAPTERINFO = "新增章节基本信息";
        public const string BOOKUPDATEOBJECT_UPDATECHAPTERINFO = "修改章节基本信息";
        public const string BOOKUPDATEOBJECT_CHAPTERCONTENT = "章节内容";
        public const string BOOKUPDATEOBJECT_DELCHAPTER = "删除章节基本信息";

        public const string ASSISTBOOKUPDATEOBJECT_BOOKINFO = "辅导教材基本信息";
        public const string ASSISTBOOKUPDATEOBJECT_BOOKCOVER = "辅导教材前言";
        public const string ASSISTBOOKUPDATEOBJECT_DELBOOK = "删除辅导教材";
        public const string ASSISTBOOKUPDATEOBJECT_INSERTCHAPTERINFO = "新增辅导教材章节基本信息";
        public const string ASSISTBOOKUPDATEOBJECT_UPDATECHAPTERINFO = "修改辅导教材章节基本信息";
        public const string ASSISTBOOKUPDATEOBJECT_CHAPTERCONTENT = "辅导教材章节内容";
        public const string ASSISTBOOKUPDATEOBJECT_DELCHAPTER = "删除辅导教材章节基本信息";

        public const int ResetDataBase = 2;
        public const int DownloadData = 3;
        public const int DownloadBook = 4;
        public const int DownloadItem = 5;
        public const int UploadResult = 6;

        public const int Downloading = 1;
        public const int DownloadSuccess = 2;
        public const int DownloadFailed = 3;

		/// <summary>
		/// 单选


		/// </summary>
    	public const int ITEMTYPE_SINGLECHOOSE = 1;
    	/// <summary>
    	/// 多选


    	/// </summary>
		public const int ITEMTYPE_MULTICHOOSE = 2;
    	/// <summary>
    	/// 判断
    	/// </summary>
		public const int ITEMTYPE_JUDGE = 3;
		/// <summary>
		/// 综合选择大题
		/// </summary>
		public const int ITEMTYPE_FILLBLANK = 4;
    	/// <summary>
    	/// 综合选择子题
    	/// </summary>
        public const int ITEMTYPE_FILLBLANKDETAIL = 5;
        /// <summary>
        /// 填空
        /// </summary>
		public const int ITEMTYPE_QUESTION = 6;
    	/// <summary>
    	/// 简答
    	/// </summary>
		public const int ITEMTYPE_DISCUSS = 7;
        /// <summary>
        /// 论述
        /// </summary>
        public const int ITEMTYPE_LUNSHU= 8;

		/// <summary>
		/// 考试模式为随到随考(StartMode = 1)
		/// </summary>
    	public const int START_MODE_NO_CONTROL = 1;
    	/// <summary>
		/// 考试模式为手动控制(StartMode = 2)
    	/// </summary>
		public const int START_MODE_CONTROL = 2;

        static PrjPub()
        {

        }

        #region 全局属性，封装了常用的Session变量

        public static bool IsServerCenter
        {
            get
            {
				if(HttpContext.Current.Application["IsServerCenter"] == null)
				{
					HttpContext.Current.Application["IsServerCenter"] = CheckServerCenter();
				}
				return (bool)HttpContext.Current.Application["IsServerCenter"];
            }
            set
            {
                HttpContext.Current.Application["IsServerCenter"] = value;
            }
        }

        //当前登录的后台用户





        public static LoginUser CurrentLoginUser
        {
            get
            {
                return (LoginUser)HttpContext.Current.Session["CurrentLoginUser"];
            }
            set
            {
                HttpContext.Current.Session["CurrentLoginUser"] = value;
            }
        }

        //当前登录的学员





        public static LoginUser CurrentStudent
        {
            get
            {
                return (LoginUser)HttpContext.Current.Session["CurrentStudent"];
            }
            set
            {
                HttpContext.Current.Session["CurrentStudent"] = value;
            }
        }

        //当前登录的学员ID
        public static string StudentID
        {
            get
            {
                return (string)HttpContext.Current.Session["StudentID"];
            }
            set
            {
                HttpContext.Current.Session["StudentID"] = value;
            }
        }

        //登录信息
        public static string WelcomeInfo
        {
            get
            {
                return (string)HttpContext.Current.Session["WelcomeInfo"];
            }
            set
            {
                HttpContext.Current.Session["WelcomeInfo"] = value;
            }
        }

        public  static  int FillUpdateRecord
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["FillUpdateRecord"]);
            }
        }

        public static int ServerNo
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ServerNo"]);
            }
        }

        public static int RailSystemId()
        {
            int railSystemId = 0;
            if (PrjPub.IsServerCenter)
            {
                SystemRoleBLL roleBll = new SystemRoleBLL();
                SystemRole role = roleBll.GetRole(PrjPub.CurrentLoginUser.RoleID);
                railSystemId = role.RailSystemID;
            }
            return railSystemId;
        }

        #endregion

		#region 公用方法

		public static bool HasEditRight(string functionName)
		{
			foreach (FunctionRight functionRight in CurrentLoginUser.FunctionRights)
			{
				if(functionRight.Function.FunctionName == functionName)
				{
					return (functionRight.Right > 1);
				}
			}

			return false;
		}

		public static bool HasDeleteRight(string functionName)
		{
			foreach (FunctionRight functionRight in CurrentLoginUser.FunctionRights)
			{
				if (functionRight.Function.FunctionName == functionName)
				{
					return (functionRight.Right > 2);
				}
			}

			return false;
		}

        public static  int GetRailSystemId()
        {
            int railSystemId = 0;
            if (PrjPub.IsServerCenter)
            {
                SystemRoleBLL roleBll = new SystemRoleBLL();
                SystemRole role = roleBll.GetRole(PrjPub.CurrentLoginUser.RoleID);
                railSystemId = role.RailSystemID;
            }
            return railSystemId;
        }
        
        public static  bool CheckServerCenter()
        {
            RefreshSnapShotBLL  objBll = new RefreshSnapShotBLL();
            if (ConfigurationManager.AppSettings["ServerType"] == "0" && !objBll.IsExistsRefreshSnapShot("USP_REFRESH_SNAPSHOT", "PROCEDURE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// 武汉或太原

		/// </summary>
		/// <returns></returns>
		public static bool IsWuhan()
		{
			SystemVersionBLL objBll = new SystemVersionBLL();
			if (objBll.GetUsePlace() == 1 || objBll.GetUsePlace() == 2 || objBll.GetUsePlace() == 4)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 武汉
		/// </summary>
		/// <returns></returns>
		public static bool IsWuhanOnly()
		{
			SystemVersionBLL objBll = new SystemVersionBLL();
			if (objBll.GetUsePlace() == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static string GetRailName()
		{
            //SystemVersionBLL objBll = new SystemVersionBLL();
            //if(objBll.GetUsePlace() == 1)
            //{
            //    return "武汉";
            //}
            //else if(objBll.GetUsePlace() == 2)
            //{
            //    return "太原";
            //}
            //else if(objBll.GetUsePlace() == 3)
            //{
            //    return "哈尔滨";
            //}
            //else
            //{
            //    return "中铁联集武汉中心站";
            //}

            return "神华包神铁路集团公司";
		}

        public static string GetRailNameBao()
        {
            return "神华包神铁路集团公司"; // 上次更新日期：2014-03-13
        }

        public static  bool IsCreateSnapShot()
        {
             RefreshSnapShotBLL objBll = new RefreshSnapShotBLL();
             return objBll.IsExistsRefreshSnapShot("BOOK", "MATERIALIZED VIEW");
        }


		//是否可以显示前台
		public static bool IsShowOnline()
		{
			string strIP = Pub.GetRealIP();
			RandomExamApplyBLL objBll = new RandomExamApplyBLL();
			IList<RandomExamApply> objList = objBll.GetRandomExamApplyByIPAddress(strIP);
			if(objList.Count > 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// 是否有两台服务器
		/// </summary>
		/// <returns></returns>
		public static bool HasTwoServer()
		{
			if(ConfigurationManager.AppSettings["HasTwoServer"] == "0")
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// 是否为主服务器

		/// </summary>
		/// <returns></returns>
		public  static  bool IsMainServer()
		{
			if (ConfigurationManager.AppSettings["IsMainServer"] == "0")
			{
				return false;
			}
			else
			{
				return true;
			}
		}

        public static string ServerIP
        {
            get
            {
                return ConfigurationManager.AppSettings["ServerIP"];
            }
        }

        public static int StationID
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);
            }
        }

		#endregion
	}
}
