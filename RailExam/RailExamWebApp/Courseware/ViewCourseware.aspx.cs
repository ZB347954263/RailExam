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
	public partial class ViewCourseware : PageBase
	{
		public string _fileType = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				string strCoursewareID = Request.QueryString.Get("id");

				CoursewareBLL objBll = new CoursewareBLL();
				RailExam.Model.Courseware obj = objBll.GetCourseware(Convert.ToInt32(strCoursewareID));
				if(File.Exists(Server.MapPath(obj.Url)))
				{
					string strFileName = obj.Url.Replace("/RailExamBao/Online/Courseware/"+obj.CoursewareID+"/", "");

					FileInfo f = new FileInfo(strFileName);

					if(f.Extension == ".swf")
					{
						_fileType = "swf"; 
					}
					else if(f.Extension == ".flv")
					{
						_fileType = "flv";
					}
					else
					{
						_fileType = "";
					}
				}

				hfUrl.Value = obj.Url;
				lblName.Text = "¡¶" + obj.CoursewareName + "¡·";
				lblType.Text = obj.CoursewareTypeName;
				lblTrainType.Text = obj.TrainTypeNames;
				lblOrg.Text = obj.ProvideOrgName;
				lblDate.Text = obj.PublishDate.ToLongDateString();
				lblAuthor.Text = obj.Authors;
				lblChecker.Text = obj.Revisers;
				lblKeyword.Text = obj.KeyWord;
				lblContent.Text = obj.Description;
				lblMemo.Text = obj.Memo;
			}

		    string str = Request.Form.Get("refresh");
            if(!string.IsNullOrEmpty(str))
            {
                Download();
            }
		}


        protected void Download()
        {
            if (File.Exists(Server.MapPath(hfUrl.Value)))
            {
                string strFileName = hfUrl.Value.Replace("/RailExamBao/Online/Courseware/" + Request.QueryString.Get("id") + "/", "");

                FileInfo f = new FileInfo(strFileName);

                if (f.Extension != ".swf" && f.Extension != ".flv")
                {
                    Response.Redirect("CoursewareUploadHaErBin.aspx?id=" + Request.QueryString.Get("id"));
                }
            }
        }
	}
}
