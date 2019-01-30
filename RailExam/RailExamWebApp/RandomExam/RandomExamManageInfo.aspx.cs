using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DSunSoft.Web.Global;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using mshtml;
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using System.IO;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamManageInfo : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange != 1)
                //{
                //    HfUpdateRight.Value = "False";
                //    HfDeleteRight.Value = "False";
                //}
                //else
                //{
                //    HfUpdateRight.Value = PrjPub.HasEditRight("新增考试").ToString();
				//    HfDeleteRight.Value = PrjPub.HasDeleteRight("新增考试").ToString();
                //}

                hfRailSystemID.Value = PrjPub.GetRailSystemId().ToString();

				if(PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}

            	if(PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 0)
                {
                    hfIsAdmin.Value = "True";
                }
                else
                {
                    hfIsAdmin.Value = "False";
                }

				if (PrjPub.HasEditRight("新增考试") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

				if (PrjPub.HasDeleteRight("新增考试") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

				ListItem item = new ListItem();
				item.Value = Session["StationOrgID"].ToString();
				item.Text = "--请选择--";
				ddlOrg.Items.Add(item);

				if(PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.UseType == 0 && PrjPub.CurrentLoginUser.IsAdmin)
				{
					ddlOrg.Visible = true;
					lblOrg.Visible = true;

					OrganizationBLL objOrgBll = new OrganizationBLL();
					IList<Organization> objList = objOrgBll.GetOrganizationsByLevel(2);
					foreach (Organization organization in objList)
					{
						ListItem litem = new ListItem();
						litem.Value = organization.OrganizationId.ToString();
						litem.Text = organization.ShortName;
						ddlOrg.Items.Add(litem);
					}
				}
				else
				{
					ddlOrg.Visible = false;
					lblOrg.Visible = false;
				}

                hfOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                HfExamCategoryId.Value = Request.QueryString.Get("id");
                Grid1.DataBind();
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
					//RandomExamResultBLL reBll = new RandomExamResultBLL();
					//IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(int.Parse(strDeleteID));

					//if (examResults.Count > 0)
					//{
					//    SessionSet.PageMessage = "已有考生参加考试，该考试不能被删除！";
					//    Grid1.DataBind();
					//    return;
					//}


                    DeleteData(int.Parse(strDeleteID));
                    Grid1.DataBind();
                }

                if (Request.Form.Get("Refresh") == "true")
                {
                    Grid1.DataBind();
                }

                if (Request.Form.Get("OutPut") != null && Request.Form.Get("OutPut") != string.Empty)
                {
					OutputWord(Request.Form.Get("OutPut"));
                }

				if(Request.Form.Get("ResetID") != null && Request.Form.Get("ResetID") != string.Empty)
				{
                    Grid1.DataBind();
				}

                if (Request.Form.Get("arrangeID") != null && Request.Form.Get("arrangeID") != string.Empty)
                {
                    string strId = Request.Form.Get("arrangeID");
                    RandomExamBLL objBll = new RandomExamBLL();
                    RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));

                    //if(objExam.HasPaper)
                    //{
                    //    SessionSet.PageMessage = "该考试已经生成试卷，不能重新安排微机教室！";
                    //    Grid1.DataBind();
                    //    return;
                    //}

                    if(!PrjPub.IsServerCenter)
                    {
                        Grid1.DataBind();
                        return;
                    }

                    if (PrjPub.CurrentLoginUser.RoleID != 1)
                    {
                        OracleAccess db = new OracleAccess();
                        RandomExamArrangeBLL arrangeBll = new RandomExamArrangeBLL();
                        IList<RandomExamArrange> arrangeList = arrangeBll.GetRandomExamArranges(Convert.ToInt32(strId));

                        if(arrangeList.Count== 0)
                        {
                            SessionSet.PageMessage = "该考试还未选择考生，不能安排微机教室！";
                            Grid1.DataBind();
                            return;  
                        }

                        RandomExamArrange arrange = arrangeList[0];

                        bool hasOrg = false;
                        string[] str = arrange.UserIds.Split(',');
                        EmployeeBLL employeebll = new EmployeeBLL();
                        OrganizationBLL orgBll =new OrganizationBLL();
                        for (int i = 0; i < str.Length; i++)
                        {
                            Employee obj = employeebll.GetEmployee(Convert.ToInt32(str[i]));

                            if(orgBll.GetStationOrgID(obj.OrgID) == PrjPub.CurrentLoginUser.StationOrgID)
                            {
                                hasOrg = true;
                                break;
                            }
                        }

                        if (!hasOrg)
                        {
                            SessionSet.PageMessage = "该考试没有本站段考生，无须安排微机教室！";
                            Grid1.DataBind();
                            return; 
                        }
                    }

                    Grid1.DataBind();
                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"showArrange(" + strId + ");",
                        true);
                }
            }
        }

		private void DeleteData(int nExamID)
		{
			RandomExamBLL examBLL = new RandomExamBLL();

			examBLL.DeleteExam(nExamID);

		    string strPath = Server.MapPath("/RailExamBao/Online/Photo/" + nExamID + "/");
            if (Directory.Exists(strPath))
            {
                string[] filenames = Directory.GetFiles(strPath);
                for (int i = 0; i < filenames.Length; i++)
                {
                    File.Delete(filenames[i]);
                }
            }
		}

		protected void btnQuery_Click(object sender, EventArgs e)
		{
			Grid1.DataBind();
			ClientScript.RegisterStartupScript(GetType(),
						"jsSelectFirstNode",
						@"document.getElementById('query').style.display='';",
						true);
		}

		private string GetNo(int i)
		{
			string strReturn = string.Empty;

			switch (i.ToString())
			{
				case "0": strReturn = "一";
					break;
				case "1": strReturn = "二";
					break;
				case "2": strReturn = "三";
					break;
				case "3": strReturn = "四";
					break;
				case "4": strReturn = "五";
					break;
				case "5": strReturn = "六";
					break;
				case "6": strReturn = "七";
					break;
				case "7": strReturn = "八";
					break;
				case "8": strReturn = "九";
					break;
				case "9": strReturn = "十";
					break;
				case "10": strReturn = "十一";
					break;
				case "11": strReturn = "十二";
					break;
				case "12": strReturn = "十三";
					break;
				case "13": strReturn = "十四";
					break;
				case "14": strReturn = "十五";
					break;
				case "15": strReturn = "十六";
					break;
				case "16": strReturn = "十七";
					break;
				case "17": strReturn = "十八";
					break;
				case "18": strReturn = "十九";
					break;
				case "19": strReturn = "二十";
					break;
			}
			return strReturn;
		}

		private string intToString(int intCol)
		{
			if (intCol < 27)
			{
				return Convert.ToChar(intCol + 64).ToString();
			}
			else
			{
				return Convert.ToChar((intCol - 1) / 26 + 64).ToString() + Convert.ToChar((intCol - 1) % 26 + 64 + 1).ToString();
			}
		}

		private void OutputWord(string strName)
		{
			//string filename = Server.MapPath("/RailExamBao/Excel/Word.doc");
			//if (File.Exists(filename))
			//{
			//    FileInfo file = new FileInfo(filename.ToString());
			//    this.Response.Clear();
			//    this.Response.Buffer = true;
			//    this.Response.Charset = "utf-7";
			//    this.Response.ContentEncoding = Encoding.UTF7;
			//    // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
			//    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strName) + ".doc");
			//    // 添加头信息，指定文件大小，让浏览器能够显示下载进度
			//    this.Response.AddHeader("Content-Length", file.Length.ToString());
			//    // 指定返回的是一个不能被客户端读取的流，必须被下载
			//    this.Response.ContentType = "application/ms-word";
			//    // 把文件流发送到客户端
			//    this.Response.WriteFile(file.FullName);
			//}

			string filename = Server.MapPath("/RailExamBao/Excel/" + strName + "试卷/");
			string ZipName = Server.MapPath("/RailExamBao/Excel/Blank.zip");

			GzipCompress(filename, ZipName);

			FileInfo file = new FileInfo(ZipName.ToString());
			this.Response.Clear();
			this.Response.Buffer = true;
			this.Response.Charset = "utf-7";
			this.Response.ContentEncoding = Encoding.UTF7;
			// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
			this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strName + "试卷") + ".zip");
			// 添加头信息，指定文件大小，让浏览器能够显示下载进度
			this.Response.AddHeader("Content-Length", file.Length.ToString());
			// 指定返回的是一个不能被客户端读取的流，必须被下载
			this.Response.ContentType = "application/ms-word";
			// 把文件流发送到客户端
			this.Response.WriteFile(file.FullName);
		}

		public void GzipCompress(string sourceFile, string disPath)
		{
			Crc32 crc = new Crc32();
			ZipOutputStream s = new ZipOutputStream(File.Create(disPath)); // 指定zip文件的绝对路径，包括文件名            
			s.SetLevel(6); // 0 - store only to 9 - means best compression 

			#region 压缩一个文件
			//FileStream fs = File.OpenRead(sourceFile); // 文件的绝对路径，包括文件名
			//byte[] buffer = new byte[fs.Length];
			//fs.Read(buffer, 0, buffer.Length);
			//ZipEntry entry = new ZipEntry(extractFileName(sourceFile)); //这里ZipEntry的参数必须是相对路径名，表示文件在zip文档里的相对路径
			//entry.DateTime = DateTime.Now;
			//entry.Size = fs.Length;
			//fs.Close();
			//crc.Reset();
			//crc.Update(buffer); 
			//entry.Crc = crc.Value; 
			//s.PutNextEntry(entry);
			//s.Write(buffer, 0, buffer.Length);
			#endregion

			//压缩多个文件
			DirectoryInfo di = new DirectoryInfo(sourceFile);
			#region 递归
			//foreach (DirectoryInfo item in di.GetDirectories())
			//{
			//    addZipEntry(item.FullName);
			//}
			#endregion
			foreach (FileInfo item in di.GetFiles())
			{
				FileStream fs = File.OpenRead(item.FullName);
				byte[] buffer = new byte[fs.Length];
				fs.Read(buffer, 0, buffer.Length);
				string strEntryName = item.FullName.Replace(sourceFile, "");
				ZipEntry entry = new ZipEntry(strEntryName);
				s.PutNextEntry(entry);
				s.Write(buffer, 0, buffer.Length);
				fs.Close();
			}

			s.Finish();
			s.Close();
		}

		private char intToChar(int intCol)
		{
			return Convert.ToChar('A' + intCol);
		}
	}
}
