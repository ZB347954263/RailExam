using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class TJ_ExamResultExport : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dateBeginTime.DateValue = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString("00") +
                                          "-01 09:00:00";
                dateEndTime.DateValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            }

            if (!string.IsNullOrEmpty(hfExamId.Value))
            {
                string[] str = hfExamId.Value.Split('|');
                RandomExamBLL objBll = new RandomExamBLL();

                string strName = string.Empty;
                for (int i = 0; i < str.Length; i++)
                {
                    string examName = objBll.GetExam(Convert.ToInt32(str[i])).ExamName;

                    if (strName == string.Empty)
                    {
                        strName = examName;
                    }
                    else
                    {
                        strName += "|" + examName;
                    }
                }

                txtSelectExam.Text = strName;
            }

            if (Request.Form.Get("RefreshExcel") != null && Request.Form.Get("RefreshExcel") != "")
            {
                DownloadExcel(Request.Form.Get("RefreshExcel"));
            }
        }

        private void DownloadExcel(string name)
        {
            string filename = Server.MapPath("/RailExamBao/Excel/" + name + "/");

            string ZipName = Server.MapPath("/RailExamBao/Excel/Word.zip");

            GzipCompress(filename, ZipName);

            FileInfo file = new FileInfo(ZipName.ToString());
            this.Response.Clear();
            this.Response.Buffer = true;
            this.Response.Charset = "utf-7";
            this.Response.ContentEncoding = Encoding.UTF7;
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(DateTime.Today.ToString("yyyy-MM-dd") + "成绩登记表") + ".zip");
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度
            this.Response.AddHeader("Content-Length", file.Length.ToString());
            // 指定返回的是一个不能被客户端读取的流，必须被下载
            this.Response.ContentType = "application/ms-excel";
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

         protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
               e.Row.Attributes.Add("onclick", "selectArow(this);");
            }
        }

        protected void grdEntity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEntity.PageIndex = e.NewPageIndex;
            grdEntity.DataSource = GetDataSet();
            grdEntity.DataBind();
            for (int i = 0; i < grdEntity.Columns.Count; i++)
            {
                grdEntity.Columns[i].ItemStyle.Wrap = false;
            }
        }



        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSelectExam.Text))
            {
                SessionSet.PageMessage = "请至少选择一个考试！";
                return;
            }

            grdEntity.DataSource = GetDataSet();
            grdEntity.DataBind();
            for (int i = 0; i < grdEntity.Columns.Count; i++)
            {
                grdEntity.Columns[i].ItemStyle.Wrap = false;
            }
        	Session["ExamResultExport"] = grdEntity.DataSource;
            hfSelect.Value = "1";
        }

        private DataSet GetDataSet()
        {
            string strSql = "";

            OracleAccess db = new OracleAccess();

           string strSql1 = string.Format(@"SELECT A.*,
           to_char(a.Begin_Time,'yyyy-mm-dd hh24:mi:ss')||'—'|| to_char(a.End_Time,'yyyy-mm-dd hh24:mi:ss') as Exam_Time_Str,
           GetOrgName(e.org_id) as ORG_NAME,
           GetStationOrgID(e.Org_ID) as Station_org_ID,
           GetOrgName(GetStationOrgID(e.Org_ID)) as Station_org_Name,
           C.EXAM_NAME,
           E.EMPLOYEE_NAME EXAMINEE_NAME,
           case when e.Work_No is not null then e.Work_No else e.IDENTITY_CARDNO end as Work_No,
           GetPostName(e.Post_id) as Post_Name,
           G.STATUS_NAME,
           x.Computer_Room_Name as Exam_Org_Name
           FROM (select a.* from (select * from Random_EXAM_RESULT_Temp where Random_Exam_Id in ({0})) a)  A
           left join (select distinct a.Random_Exam_Result_ID,c.Computer_Server_Name,b.Computer_Room_Name
           from  Random_Exam_Result_Detail_Temp a
           inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
           inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID
           where A.Random_Exam_Id in ({0}) )
           x on a.Random_Exam_Result_ID=x.Random_Exam_Result_ID
           INNER JOIN ORG B on b.org_id = a.org_id
           INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
           INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
           INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
             WHERE A.Random_Exam_Id in ({0})
          and a.status_id > 0      
          and (GetStationOrgID(a.org_id) ={1} or GetStationOrgID(c.org_id) ={1}  or GetStationOrgID(e.org_ID)= {1} )", hfExamId.Value.Replace("|",","),PrjPub.CurrentLoginUser.StationOrgID);


          string strSql2 = string.Format(@"SELECT A.*,
            to_char(a.Begin_Time,'yyyy-mm-dd hh24:mi:ss')||'—'|| to_char(a.End_Time,'yyyy-mm-dd hh24:mi:ss') as Exam_Time_Str,
           GetOrgName(e.org_id) as ORG_NAME,
           GetStationOrgID(e.Org_ID) as Station_org_ID,
           GetOrgName(GetStationOrgID(e.Org_ID)) as Station_org_Name,
           C.EXAM_NAME,
           E.EMPLOYEE_NAME EXAMINEE_NAME,
           case when e.Work_No is not null then e.Work_No else e.IDENTITY_CARDNO end as Work_No,
           GetPostName(e.Post_id) as Post_Name,
           G.STATUS_NAME,
           x.Computer_Room_Name as Exam_Org_Name
           FROM (select a.* from ( select * from Random_Exam_Result where Random_Exam_Id in ({0})) a)  A
           left join (select distinct a.Random_Exam_Result_ID,c.Computer_Server_Name,b.Computer_Room_Name
           from  Random_Exam_Result_Detail a
           inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
           inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID
           where A.Random_Exam_Id in ({0}))
           x on a.Random_Exam_Result_ID=x.Random_Exam_Result_ID
           INNER JOIN ORG B on b.org_id = a.org_id
           INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
           INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
           INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
           WHERE A.Random_Exam_Id in ({0})
           and a.status_id > 0   
           and (GetStationOrgID(a.org_id) ={1} or GetStationOrgID(c.org_id) ={1}  or GetStationOrgID(e.org_ID)= {1} )", hfExamId.Value.Replace("|", ","), PrjPub.CurrentLoginUser.StationOrgID);


            strSql = "select a.* from (" + strSql1 + " union all " + strSql2 + ") a order by a.Org_Name,a.Exam_Name";

            DataSet ds = db.RunSqlDataSet(strSql);

            return ds;
        }
    }
}
