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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp
{
    public partial class Test : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
//                string strSql = @"select * from Random_Exam_Result_Current a
//                            where Examinee_ID not in (56679,56838,56956,57380) and Random_Exam_ID=17158";
//                OracleAccess db = new OracleAccess();
//                DataSet ds = db.RunSqlDataSet(strSql);

//                string strID = "";
//                foreach(DataRow dr in ds.Tables[0].Rows)
//                {
//                    if(strID=="")
//                    {
//                        strID = dr["Examinee_ID"].ToString();
//                    }
//                    else
//                    {
//                        strID += ","+ dr["Examinee_ID"].ToString();
//                    }
//                }

//                strSql = "select * from Random_Exam_Arrange where Random_Exam_ID=17158";
//                ds = db.RunSqlDataSet(strSql);

//                Response.Write(ds.Tables[0].Rows[0]["User_Ids"].ToString().Split(',').Length);
            }
        }

		[System.Web.Services.WebMethod()]
		public static string aa(string str)
		{
			return str;
		} 
    }
}