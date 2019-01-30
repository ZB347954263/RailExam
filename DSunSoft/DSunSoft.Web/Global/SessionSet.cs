using System.Web;

namespace DSunSoft.Web.Global
{
	public static class SessionSet
	{
		static SessionSet()
		{
			//��¼��־
			Login = false;

			//ҳ����Ϣ
			PageMessage = string.Empty;

			//�û�ID
			UserID = string.Empty;

			//ְԱID
			EmployeeID = 0;
			
			//ְԱ����
			EmployeeName = string.Empty;

			//����ID
			OrganizationID = 0;

			//��������
			OrganizationName = string.Empty;

            //վ��ID
		    StationOrgID = 0;
		}

		public static bool Login
		{
			get
			{
				return (bool)HttpContext.Current.Session["Login"];
			}
			set
			{
				HttpContext.Current.Session["Login"] = value;
			}
		}

		public static string PageMessage
		{
			get
			{
				return (string)HttpContext.Current.Session["PageMessage"];
			}
			set
			{
				HttpContext.Current.Session["PageMessage"] = value;
			}
		}

		public static string UserID
		{
			get
			{
				return (string)HttpContext.Current.Session["UserID"];
			}
			set
			{
				HttpContext.Current.Session["UserID"] = value;
			}
		}

		public static int EmployeeID
		{
			get
			{
				return (int)HttpContext.Current.Session["EmployeeID"];
			}
			set
			{
				HttpContext.Current.Session["EmployeeID"] = value;
			}
		}

		public static string EmployeeName
		{
			get
			{
				return (string)HttpContext.Current.Session["EmployeeName"];
			}
			set
			{
				HttpContext.Current.Session["EmployeeName"] = value;
			}
		}

		public static int OrganizationID
		{
			get
			{
				return (int)HttpContext.Current.Session["OrganizationID"];
			}
			set
			{
				HttpContext.Current.Session["OrganizationID"] = value;
			}
		}

		public static string OrganizationName
		{
			get
			{
				return (string)HttpContext.Current.Session["OrganizationName"];
			}
			set
			{
				HttpContext.Current.Session["OrganizationName"] = value;
			}
		}

        public static int StationOrgID
        {
            get
            {
                return (int)HttpContext.Current.Session["StationOrgID"];
            }
            set
            {
                HttpContext.Current.Session["StationOrgID"] = value;
            }
        }
	}
}
