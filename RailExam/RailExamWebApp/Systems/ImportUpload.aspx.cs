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

namespace RailExamWebApp.Systems
{
	public partial class ImportUpload : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnExam_Click(object sender, EventArgs e)
		{
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            Response.Redirect("/RailExamBao/Systems/ImportExcelByBaoERP.aspx?FileName=" + strFileName + "&OrgID= " + Request.QueryString.Get("OrgID") + "&mode=exam");
		}
		
		protected void btnInput_Click(object sender, EventArgs e)
		{
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			// Response.Redirect("/RailExamBao/Systems/ImportExcel.aspx?FileName=" + strFileName + "&OrgID= " + Request.QueryString.Get("OrgID"));
            /*
             * 上面为原系统人员信息Excel导入跳转。
             * 下面为包神铁路人员信息Excel导入跳转。
             * 撰于 2014-03-07
             */
            Response.Redirect("/RailExamBao/Systems/ImportExcelByBaoERP.aspx?FileName=" + strFileName + "&OrgID= " + Request.QueryString.Get("OrgID"));
		}

        // 批量导入员工照片（打包成rar压缩文件）
        protected void btnImportPhoto_Click(object sender, EventArgs e)
        {
            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/photo/" + strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            Response.Redirect("/RailExamBao/Systems/ImportPhoto4Bao.aspx?FileName=" + strFileName);
        }
	}
}
