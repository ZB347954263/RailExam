using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class SaveRandomExamArrange : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SaveArrange();
			}
		}

		private void SaveArrange()
		{
			OracleAccess db = new OracleAccess();
			// 根据 ProgressBar.htm 显示进度条界面
			string templateFileName = Path.Combine(Server.MapPath("../RandomExam/"), "ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string jsBlock;

			string strExamID = Request.QueryString.Get("examID");
			string strTrainClassID = Request.QueryString.Get("trainclassID");
			string strPostID = Request.QueryString.Get("PostID");

			string[] str = strTrainClassID.Split(',');
			string strArchivesID = string.Empty;
			for (int j = 0; j < str.Length; j++)
			{
				if(strArchivesID == string.Empty)
				{
					strArchivesID = "'" + str[j] + "'";
				}
				else
				{
					strArchivesID = strArchivesID + ",'" + str[j] + "'";
				}
			}


			string strSql = "";

			Hashtable htUserID = new Hashtable();
			string strUserIDs = string.Empty;
			int i = 1;
			strSql = "select  distinct b.Employee_ID  "
					 + "from CheckRoll_Info a "
					 + " inner join Employee@LINK_RAILEXAM b on a.Employee_ID=b.Employee_ID"
					 + " where a.Archives_ID in (" + strArchivesID + ") and b.Post_ID in (" + strPostID + ")";
			int count = db.RunSqlDataSet(strSql).Tables[0].Rows.Count;

			for (int j = 0; j < str.Length; j++)
			{
				strSql = "select  b.Employee_ID "
								+ "from CheckRoll_Info a "
								+ " inner join Employee@LINK_RAILEXAM b on a.Employee_ID=b.Employee_ID"
								+ " where a.Archives_ID='" + str[j] + "' and b.Post_ID in (" + strPostID + ")";
				DataSet ds = db.RunSqlDataSet(strSql);

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					if (!htUserID.ContainsKey(dr["Employee_ID"].ToString()))
					{
						if (strUserIDs == string.Empty)
						{
							strUserIDs = dr["Employee_ID"].ToString();
						}
						else
						{
							strUserIDs = strUserIDs + "," + dr["Employee_ID"];
						}

						htUserID.Add(dr["Employee_ID"].ToString(), dr["Employee_ID"].ToString());

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('正在添加考生','" + ((i * 100) / ((double)count)).ToString("0.00") + "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();

						i++;
					}
				}
			}

			RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
			RandomExamArrange objArrange = new RandomExamArrange();
			objArrange.RandomExamId = Convert.ToInt32(strExamID);
			objArrange.Memo = "";
			objArrange.UserIds = strUserIDs;
			objArrangeBll.AddRandomExamArrange(objArrange);

			jsBlock = "<script>SetCompleted('处理完成。'); </script>";
			Response.Write(jsBlock);
			Response.Flush();

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}
	}
}
