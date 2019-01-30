using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class SystemExam : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (PrjPub.HasEditRight("考场规则"))
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                }

                string strSql = "select * from System_Exam where System_Exam_ID=1";
                OracleAccess db = new OracleAccess();
                DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

                if(dt.Rows.Count ==0)
                {
                    txtMesage.Text = string.Empty;
                    txtTime.Text = string.Empty;
                    txtNumber.Text = string.Empty;
                }
                else
                {
                    txtMesage.Text = dt.Rows[0]["Exam_Message"].ToString();
                    txtTime.Text = dt.Rows[0]["Exam_Time"].ToString();
                    txtNumber.Text = dt.Rows[0]["Exam_Number"].ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int time;
            try
            {
                time = Convert.ToInt32(txtTime.Text);
                if(time < 0)
                {
                    SessionSet.PageMessage = "模拟考试时间请输入正整数！";
                    return;
                }

                if(time>99)
                {
                    SessionSet.PageMessage = "模拟考试时间请输入小于99的正整数！";
                    return;
                }
            }
            catch
            {
                SessionSet.PageMessage = "模拟考试时间请输入正整数！";
                return;
            }

            int number;
            try
            {
                number = Convert.ToInt32(txtNumber.Text);
                if(number < 0)
                {
                    SessionSet.PageMessage = "模拟考试题数请输入正整数！";
                    return;
                }

                if(number>99)
                {
                    SessionSet.PageMessage = "模拟考试题数请输入小于99的正整数！";
                    return;
                }
            }
            catch
            {
                SessionSet.PageMessage = "模拟考试题数请输入正整数！";
                return;
            }

            string strSql = "select * from System_Exam where System_Exam_ID=1";
            OracleAccess db = new OracleAccess();
            DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

            if(dt.Rows.Count ==0)
            {
                strSql = "insert into System_Exam Values(1,'" + txtMesage.Text + "'," + time + "," + number + ")";
            }
            else
            {
                strSql = "update System_Exam set Exam_Message='" + txtMesage.Text + "',Exam_Time=" + time + ",Exam_Number="+number+" where System_Exam_ID=1";
            }

            db.ExecuteNonQuery(strSql);

            SessionSet.PageMessage = "保存成功！";
        }
    }
}
