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
                //    HfUpdateRight.Value = PrjPub.HasEditRight("��������").ToString();
				//    HfDeleteRight.Value = PrjPub.HasDeleteRight("��������").ToString();
                //}

                hfRailSystemID.Value = PrjPub.GetRailSystemId().ToString();

				if(PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
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

				if (PrjPub.HasEditRight("��������") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

				if (PrjPub.HasDeleteRight("��������") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

				ListItem item = new ListItem();
				item.Value = Session["StationOrgID"].ToString();
				item.Text = "--��ѡ��--";
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
					//    SessionSet.PageMessage = "���п����μӿ��ԣ��ÿ��Բ��ܱ�ɾ����";
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
                    //    SessionSet.PageMessage = "�ÿ����Ѿ������Ծ��������°���΢�����ң�";
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
                            SessionSet.PageMessage = "�ÿ��Ի�δѡ���������ܰ���΢�����ң�";
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
                            SessionSet.PageMessage = "�ÿ���û�б�վ�ο��������밲��΢�����ң�";
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
				case "0": strReturn = "һ";
					break;
				case "1": strReturn = "��";
					break;
				case "2": strReturn = "��";
					break;
				case "3": strReturn = "��";
					break;
				case "4": strReturn = "��";
					break;
				case "5": strReturn = "��";
					break;
				case "6": strReturn = "��";
					break;
				case "7": strReturn = "��";
					break;
				case "8": strReturn = "��";
					break;
				case "9": strReturn = "ʮ";
					break;
				case "10": strReturn = "ʮһ";
					break;
				case "11": strReturn = "ʮ��";
					break;
				case "12": strReturn = "ʮ��";
					break;
				case "13": strReturn = "ʮ��";
					break;
				case "14": strReturn = "ʮ��";
					break;
				case "15": strReturn = "ʮ��";
					break;
				case "16": strReturn = "ʮ��";
					break;
				case "17": strReturn = "ʮ��";
					break;
				case "18": strReturn = "ʮ��";
					break;
				case "19": strReturn = "��ʮ";
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
			//    // ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
			//    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strName) + ".doc");
			//    // ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
			//    this.Response.AddHeader("Content-Length", file.Length.ToString());
			//    // ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
			//    this.Response.ContentType = "application/ms-word";
			//    // ���ļ������͵��ͻ���
			//    this.Response.WriteFile(file.FullName);
			//}

			string filename = Server.MapPath("/RailExamBao/Excel/" + strName + "�Ծ�/");
			string ZipName = Server.MapPath("/RailExamBao/Excel/Blank.zip");

			GzipCompress(filename, ZipName);

			FileInfo file = new FileInfo(ZipName.ToString());
			this.Response.Clear();
			this.Response.Buffer = true;
			this.Response.Charset = "utf-7";
			this.Response.ContentEncoding = Encoding.UTF7;
			// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
			this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strName + "�Ծ�") + ".zip");
			// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
			this.Response.AddHeader("Content-Length", file.Length.ToString());
			// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
			this.Response.ContentType = "application/ms-word";
			// ���ļ������͵��ͻ���
			this.Response.WriteFile(file.FullName);
		}

		public void GzipCompress(string sourceFile, string disPath)
		{
			Crc32 crc = new Crc32();
			ZipOutputStream s = new ZipOutputStream(File.Create(disPath)); // ָ��zip�ļ��ľ���·���������ļ���            
			s.SetLevel(6); // 0 - store only to 9 - means best compression 

			#region ѹ��һ���ļ�
			//FileStream fs = File.OpenRead(sourceFile); // �ļ��ľ���·���������ļ���
			//byte[] buffer = new byte[fs.Length];
			//fs.Read(buffer, 0, buffer.Length);
			//ZipEntry entry = new ZipEntry(extractFileName(sourceFile)); //����ZipEntry�Ĳ������������·��������ʾ�ļ���zip�ĵ�������·��
			//entry.DateTime = DateTime.Now;
			//entry.Size = fs.Length;
			//fs.Close();
			//crc.Reset();
			//crc.Update(buffer); 
			//entry.Crc = crc.Value; 
			//s.PutNextEntry(entry);
			//s.Write(buffer, 0, buffer.Length);
			#endregion

			//ѹ������ļ�
			DirectoryInfo di = new DirectoryInfo(sourceFile);
			#region �ݹ�
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
