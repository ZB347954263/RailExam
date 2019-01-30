using System;
using System.Collections.Generic;
using System.Text;

namespace DSunSoft.Data
{
	public static class DataConvert
	{
		public static int? ToInt(object value)
		{
			return Convert.IsDBNull(value) ? null : (int?)(Convert.ToInt32(value));
		}

		public static bool? ToBool(object value)
		{
			return Convert.IsDBNull(value) ? null : (bool?)(Convert.ToBoolean(value));
		}

        public static decimal? ToDecimal(object value)
        {
            return Convert.IsDBNull(value) ? null : (decimal?)(Convert.ToDecimal(value));
        }

		public static string ToString(object value)
		{
			return value as string;
		}

		public static DateTime? ToDateTime(object value)
		{
			return Convert.IsDBNull(value) ? null : (DateTime?)(Convert.ToDateTime(value));
		}
	}
}
