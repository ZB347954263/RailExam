using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Configuration;

namespace DSunSoft.Web.Global
{
	public static class ApplicationSet
	{
		static ApplicationSet()
		{
			string allowUserCount = ConfigurationManager.AppSettings["AllowUserCount"];

			if (allowUserCount != null && allowUserCount != "0")
			{
				//允许登录用户数
				AllowUserCount = int.Parse(allowUserCount);

				//当前用户数
				UserCount = 0;

				//在线用户信息
				UserOnline = new ArrayList();
			}
		}

		public static int AllowUserCount
		{
			get
			{
				return (int)HttpContext.Current.Application["AllowUserCount"];
			}
			set
			{
				HttpContext.Current.Application["AllowUserCount"] = value;
			}
		}

		public static int UserCount
		{
			get
			{
				return (int)HttpContext.Current.Application["UserCount"];
			}
			set
			{
				HttpContext.Current.Application["UserCount"] = value;
			}
		}

		public static ArrayList UserOnline
		{
			get
			{
				return (ArrayList)HttpContext.Current.Application["UserOnline"];
			}
			set
			{
				HttpContext.Current.Application["UserOnline"] = value;
			}
		}
	}
}
