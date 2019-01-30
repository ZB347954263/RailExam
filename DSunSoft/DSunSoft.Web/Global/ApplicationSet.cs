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
				//�����¼�û���
				AllowUserCount = int.Parse(allowUserCount);

				//��ǰ�û���
				UserCount = 0;

				//�����û���Ϣ
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
