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

namespace RailExamWebApp.Item
{
	public partial class ItemUpload : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}


		protected void btnInput_Click(object sender, EventArgs e)
		{
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			Response.Redirect("/RailExamBao/Item/ImportExcel.aspx?FileName=" + strFileName + "&Mode=" + Request.QueryString.Get("Mode") + "&BookID=" + Request.QueryString.Get("BookID") + "&ChapterID=" + Request.QueryString.Get("ChapterID"));
		}
	}
}
