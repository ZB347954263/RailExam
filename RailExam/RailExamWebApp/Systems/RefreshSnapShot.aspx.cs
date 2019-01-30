using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class RefreshSnapShot : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strStationID = ConfigurationManager.AppSettings["StationID"];

                lbl.Text = "��ǰ������Ϊ��";
                if (strStationID == "200")
                {
                    lbl.Text += PrjPub.GetRailName() + "������";
                }
                else
                {
                    OrganizationBLL orgBll = new OrganizationBLL();
                    RailExam.Model.Organization organization = orgBll.GetOrganization(Convert.ToInt32(strStationID));
                    lbl.Text += organization.ShortName + "������";
                }

                lbl.Text += "&nbsp;&nbsp;&nbsp;���������Ϊ��" + PrjPub.ServerNo;

            }
        }

      

        protected void DownloadItem()
        {
            string srcPath = @"\\" + ConfigurationManager.AppSettings["ServerIP"] + "\\Item\\";
            string aimPath = Server.MapPath("../Online/Item/");

            CopyItem(srcPath, aimPath);
            SystemLogBLL objLogBll = new SystemLogBLL();
            objLogBll.WriteLog("�������µ��ӽ̲�");
        }

        private void CopyItem(string srcPath, string aimPath)
        {
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }

            string[] fileList = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in fileList)
            {
                //����ļ����ļ���
                if (Directory.Exists(file))
                {
                    if (Path.GetFileName(file) == "small")
                    {
                        CopyItem(file, aimPath + Path.GetFileName(file) + "\\");
                    }
                }
                else
                {
                    File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
        }
    }
}
