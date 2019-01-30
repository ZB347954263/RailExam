using System;
using System.Text;
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
using RailExamWebApp.Common.Class;
using System.Diagnostics;

namespace RailExamWebApp.Courseware 
{
	public partial class CoursewareUploadHaErBin : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string strCoursewareID = Request.QueryString.Get("id");

				CoursewareBLL objBll = new CoursewareBLL();
				RailExam.Model.Courseware obj = objBll.GetCourseware(Convert.ToInt32(strCoursewareID));

				string strFileName = obj.Url.Replace("/RailExamBao/Online/Courseware/" + obj.CoursewareID + "/", "");
				FileInfo f = new FileInfo(strFileName);

				//下载文件的路径
				string path = Server.MapPath(obj.Url);

				//下载文件的名称
				string filename = f.Name;

				System.IO.FileInfo toDownload = new System.IO.FileInfo(path);

				if (toDownload.Exists == true)
				{
					const long ChunkSize = 10000;
					byte[] buffer = new byte[ChunkSize];

					Response.Clear();
					System.IO.FileStream iStream = System.IO.File.OpenRead(path);
					long dataLengthToRead = iStream.Length;
					Response.ContentType = "application/octet-stream";
					Response.AddHeader("Content-Disposition", "attachment; filename=new_" + HttpUtility.UrlEncode(toDownload.Name));
					while (dataLengthToRead > 0 && Response.IsClientConnected)
					{
						int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));
						Response.OutputStream.Write(buffer, 0, lengthRead);
						Response.Flush();
						dataLengthToRead = dataLengthToRead - lengthRead;
					}
					iStream.Close();
					Response.Close();
				}
			}

		}
	}
}
