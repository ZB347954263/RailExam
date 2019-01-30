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

namespace RailExamWebApp.Online.Study
{
    public partial class Study : System.Web.UI.Page
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

			if (PrjPub.IsWuhan())
			{
				hfIswuhan.Value = "1";
			}
			else
			{
				hfIswuhan.Value = "0";
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
		}
    }
}
