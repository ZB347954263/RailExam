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
    public partial class DownloadFile : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //下载文件的路径
                string path = Server.MapPath("/RailExamBao/Excel/" + Server.HtmlDecode(Request.QueryString.Get("name")));
                System.IO.FileInfo toDownload = new System.IO.FileInfo(path);

                if (toDownload.Exists == true)
                {
                    const long ChunkSize = 10000;
                    byte[] buffer = new byte[ChunkSize];

                    Response.Clear();
                    System.IO.FileStream iStream = System.IO.File.OpenRead(path);
                    long dataLengthToRead = iStream.Length;
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition",
                                       "attachment; filename=new_" + HttpUtility.UrlEncode(toDownload.Name));
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
