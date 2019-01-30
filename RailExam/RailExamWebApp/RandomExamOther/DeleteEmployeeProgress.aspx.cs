using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp.RandomExamOther
{
    public partial class DeleteEmployeeProgress : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                deleteEmployee();
            }
        }

        private void deleteEmployee()
        {
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            SystemUserBLL systemUserBll = new SystemUserBLL();
            EmployeeBLL objBll = new EmployeeBLL();

            IList<Employee> objList = objBll.GetEmployeeByWhereClause("GetStationOrgID(Org_ID)=" + Request.QueryString.Get("OrgID"));

            string jsBlock;
            int NowCount = 0;
            foreach (Employee employee in objList)
            {
                IList<SystemUser> systemUser = systemUserBll.GetUsersByEmployeeID(employee.EmployeeID);
                if(systemUser.Count > 0)
                {
                    if (systemUser[0].RoleID != 123 && systemUser[0].RoleID != 1 && systemUser[0].RoleID !=2)
                    {
                        systemUserBll.DeleteUser(systemUserBll.GetUserByEmployeeID(employee.EmployeeID));
                        objBll.DeleteEmployee(employee.EmployeeID);
                    }
                }
                else
                {
                    objBll.DeleteEmployee(employee.EmployeeID);
                }

                NowCount = NowCount + 1;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('删除员工','" + ((double)(NowCount * 100) / ((double)objList.Count)).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }

            jsBlock = "<script>SetCompleted('处理完成。'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }
    }
}
