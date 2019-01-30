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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerServerCount : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dateStartDateTime.DateValue = "2012-01-01";
                dateEndDateTime.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
                BindData();
            }
        }

        protected void BindData()
        {
            hfSelect.Value = GetSql();

            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(hfSelect.Value);

            ViewState["Grid"] = ds;
            examsGrid.DataBind();
        }

        private string GetSql()
        {
            int orgID;
            string strWhere = string.Empty;
            if (PrjPub.IsServerCenter)
            {
                orgID = PrjPub.CurrentLoginUser.StationOrgID;

                if (orgID != 200)
                {
                    strWhere = " where a.Org_ID=" + orgID;
                }
            }
            else
            {
                orgID = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);
                strWhere = " where a.Org_ID=" + orgID;
            }



            string _DateFrom = dateStartDateTime.DateValue.ToString();
            string _DateTo = dateEndDateTime.DateValue.ToString();

            string strSql =
                @"select e.Short_Name,Computer_Server_Name,b.ʹ���˴�,c.������λʹ���˴�,d.������λʹ������
                    from Computer_Server a
                    left join (select b.Computer_Server_ID,count(*) ʹ���˴� from Random_Exam_Result_Detail a
                                inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
                                inner join Random_Exam_Result d on a.Random_Exam_Result_ID=d.Random_Exam_Result_ID
                                where d.Begin_Time>=to_date('" + _DateFrom + @"','yyyy-MM-dd') and d.Begin_Time-1<to_date('" + _DateTo + @"','yyyy-MM-dd')
                                group by b.Computer_Server_ID) b on a.Computer_Server_ID=b.Computer_Server_ID
                    left join (select b.Computer_Server_ID,count(*) ������λʹ���˴� from Random_Exam_Result_Detail a
                                inner join Random_Exam c on a.Random_exam_ID=c.Random_Exam_ID           
                                inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
                                inner join Random_Exam_Result d on a.Random_Exam_Result_ID=d.Random_Exam_Result_ID
                                where d.Begin_Time>=to_date('" + _DateFrom + @"','yyyy-MM-dd') and d.Begin_Time-1<to_date('" + _DateTo + @"','yyyy-MM-dd')
                                and c.Org_ID != " + orgID + @"
                                group by b.Computer_Server_ID) c on a.Computer_Server_ID=c.Computer_Server_ID
                    left join (select b.Computer_Server_ID,Sum(ceil(a.End_Time-a.Begin_Time)) ������λʹ������ from 
                                Random_Exam a
                                inner join (select distinct a.Random_Exam_ID,Computer_Room_ID from Random_Exam_Result_Detail a
                                inner join Random_Exam c on a.Random_exam_ID=c.Random_Exam_ID  
                                inner join Random_Exam_Result d on a.Random_Exam_Result_ID=d.Random_Exam_Result_ID
                                where d.Begin_Time>=to_date('" + _DateFrom + @"','yyyy-MM-dd') and d.Begin_Time-1<to_date('" + _DateTo + @"','yyyy-MM-dd')         
                                and c.Org_ID != " + orgID + @") c on a.Random_Exam_ID=c.Random_Exam_ID
                                inner join Computer_Room b on b.Computer_Room_ID=c.Computer_Room_ID
                                group by b.Computer_Server_ID) d on a.Computer_Server_ID=d.Computer_Server_ID
                    inner join Org e on a.Org_ID=e.Org_ID" + strWhere + @"
                    order by e.Order_Index,a.Computer_Server_No";

            return strSql;
        }

        protected void examsGrid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (examsGrid.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
                    e.Row.Visible = false;
                else
                    e.Row.Attributes.Add("onclick", "selectArow(this);");

            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void ObjectDataSourceBook_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable dt = e.ReturnValue as DataTable;
            if (dt.Rows.Count == 0)
            {
                DataRow r = dt.NewRow();
                r["Computer_Server_Name"] = "-1";
                dt.Rows.Add(r);
            }
        }

        protected void btnOutPut_Click(object sender, EventArgs e)
        {
            OutPut();

            string filename = Server.MapPath("/RailExamBao/Excel/CompuerServerCount.xls");

            if (File.Exists(filename))
            {
                FileInfo file = new FileInfo(filename.ToString());
                this.Response.Clear();
                this.Response.Buffer = true;
                this.Response.Charset = "utf-7";
                this.Response.ContentEncoding = Encoding.UTF7;
                // ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("������ʹ�����ͳ��") + ".xls");
                // ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
                this.Response.AddHeader("Content-Length", file.Length.ToString());
                // ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
                this.Response.ContentType = "application/ms-excel";
                // ���ļ������͵��ͻ���
                this.Response.WriteFile(file.FullName);
            }
        }

        private void OutPut()
        {
            Excel.Application objApp = new Excel.ApplicationClass();
            Excel.Workbooks objbooks = objApp.Workbooks;
            Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1];//ȡ��sheet1 
            Excel.Range range;
            string filename = "";

            try
            {
                //����.xls�ļ�����·���� 
                filename = Server.MapPath("/RailExamBao/Excel/CompuerServerCount.xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }

                //�����õ��ı������,��ֵ����Ԫ��   

                objSheet.Cells[1, 1] = "վ������";

                objSheet.Cells[1, 2] = "����������";
                range = objSheet.get_Range(objSheet.Cells[1, 2], objSheet.Cells[1, 4]);
                range.Merge(0);

                objSheet.Cells[1, 5] = "ʹ���˴�";

                objSheet.Cells[1, 6] = "������λʹ���˴�";

                objSheet.Cells[1, 7] = "������λʹ������";

                DataSet ds = (DataSet)ViewState["Grid"];

                int i = 0;
                //ͬ��������������  
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objSheet.Cells[2 + i, 1] = dr["Short_Name"].ToString();

                    objSheet.Cells[2 + i, 2] = dr["Computer_Server_Name"].ToString();
                    range = objSheet.get_Range(objSheet.Cells[2 + i, 2], objSheet.Cells[2 + i, 4]);
                    range.Merge(0);

                    objSheet.Cells[2 + i, 5] = dr["ʹ���˴�"].ToString();

                    objSheet.Cells[2 + i, 6] = dr["������λʹ���˴�"].ToString();

                    objSheet.Cells[2 + i, 7] = dr["������λʹ������"].ToString();

                    i++;
                }

                //���ɼ�,����̨����   
                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);
            }
            catch
            {
                SessionSet.PageMessage = "ϵͳ���󣬵���Excel�ļ�ʧ�ܣ�";
            }
            finally
            {
                objbook.Close(Type.Missing, filename, Type.Missing);
                objbooks.Close();
                objApp.Application.Workbooks.Close();
                objApp.Application.Quit();
                objApp.Quit();
                GC.Collect();
            }

        }
    }
}
