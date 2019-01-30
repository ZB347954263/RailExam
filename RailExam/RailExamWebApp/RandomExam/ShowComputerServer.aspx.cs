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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class ShowComputerServer : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                 string strId = Request.QueryString.Get("id");
                 string strSql = @"select c.Short_Name OrganizationName,b.Computer_Server_No ComputerServerNo,
                            b.Computer_Server_Name ComputerServerName,
                            case when a.Downloaded =1 then 'true' else 'false' end  DownloadedStatus,
                            case when a.Has_Paper=1 then 'true' else 'false' end  HasPaperStatus,
                            case when a.Is_Upload=1 then 'true' else 'false' end  IsUploadStatus,
                            case when Is_Start=0 then '未开始' when Is_Start=1 then '正在进行' 
                            else '已结束' end StatusName,
                            to_char(Last_Upload_Date,'yyyy-MM-dd  HH24:MI:SS') LastUploadDate
                            from Random_Exam_Computer_Server a
                            inner join Computer_Server b on to_char(a.Computer_Server_No)=b.Computer_Server_No
                            inner join Org c on b.Org_ID=c.Org_ID
                            where Random_Exam_ID=" + strId+" order by c.Order_Index";

                 //RandomExamBLL objBll =new RandomExamBLL();
                 //RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));
                 //if(obj.OrgId != PrjPub.CurrentLoginUser.StationOrgID)
                 //{
                 //    strSql += " and b.Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID;
                 //}

                 OracleAccess db = new OracleAccess();
                 DataSet ds = db.RunSqlDataSet(strSql);

                 gvChoose.DataSource = ds;
                 gvChoose.DataBind();
            }
        }
    }
}
