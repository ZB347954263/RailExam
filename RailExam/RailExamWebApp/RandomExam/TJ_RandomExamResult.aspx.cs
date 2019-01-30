using System;
using System.Collections.Generic;
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
using Excel;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using DataTable = System.Data.DataTable;

namespace RailExamWebApp.RandomExam
{
    public partial class TJ_RandomExamResult : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                dateBeginTime.DateValue = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString("00") + "-01 09:00:00";
                dateEndTime.DateValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                OrganizationBLL orgBll = new OrganizationBLL();
                IList<Organization> orgList = orgBll.GetOrganizationsByLevel(2);

                ListItem item = new ListItem();
                item.Text = "--��ѡ��--";
                item.Value = "0";
                ddlOrg.Items.Add(item);

                int railSystemId = PrjPub.GetRailSystemId();

                foreach (Organization organization in orgList)
                {
                    if(organization.LevelNum == 1)
                    {
                        continue;
                    }

                    if ((railSystemId != 0 && organization.RailSystemID == railSystemId) || organization.SuitRange == 1 || organization.OrganizationId == PrjPub.CurrentLoginUser.StationOrgID || (PrjPub.CurrentLoginUser.SuitRange==1 && railSystemId==0))
                    {
                        item = new ListItem();
                        item.Text = organization.ShortName;
                        item.Value = organization.OrganizationId.ToString();
                        ddlOrg.Items.Add(item);
                    }
                }

                item = new ListItem();
                item.Text = "--��ѡ��--";
                item.Value = "0";
                ddlTrainClass.Items.Add(item);
            }

            if(!string.IsNullOrEmpty(hfExamId.Value))
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
        }

        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
               e.Row.Attributes.Add("onclick", "selectArow(this);");
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if(ddlType.SelectedValue =="2")
                {
                    return;
                }

                TableCellCollection tcl = e.Row.Cells;
                tcl.Clear();
                int n = 0;

                int m = 1;
                tcl.Add(new TableHeaderCell());
                if (ddlMode.SelectedValue == "1")
                {
                    tcl[n].Text  = "վ������";
                }
                else if (ddlMode.SelectedValue == "2")
                {
                    tcl[n].Text  = "��λ-����";
                }
                else if (ddlMode.SelectedValue == "3")
                {
                    tcl[n].Text  = "ְ��";
                }
                else if (ddlMode.SelectedValue == "4")
                {
                    tcl[n].Text  = "���֣��������ƣ�";
                }
                else if (ddlMode.SelectedValue == "5")
                {
                    tcl[n].Text  = "�Ļ��̶�";
                }
                else if (ddlMode.SelectedValue == "6")
                {
                    tcl[n].Text  = "����ṹ";
                }
                if (ddlMode.SelectedValue == "7")
                {
                    tcl[n].Text = "��λ";
                }
                tcl[n].Wrap = false;

                if (ddlMode.SelectedValue == "7")
                {
                    tcl.Add(new TableHeaderCell());
                    tcl[n + 1].Text = "����";
                    tcl[n + 1].Wrap = false;

                    m = 2;
                }


                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "ʵ��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "����";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "��߷�";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "��ͷ�";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "100��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "90-99��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "80-89��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "70-79��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "60-69��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "50-59��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "40-49��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "30-39��";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                tcl[n + m].Text = "30������";
                tcl[n + m].Wrap = false;

                m++;
                tcl.Add(new TableHeaderCell());
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.Append("60������</th></tr><tr class='HeadingRow' style='FONT-WEIGHT: bold; FONT-SIZE: 12px; COLOR: #2d61ba; FONT-FAMILY: ����' >");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>");
                str.Append("<th align='center' scope='col'>��<br>��</th>");
                str.Append("<th align='center' scope='col' >��<br>��</th>"); 
                
                tcl[n + m].Text = str.ToString();
                tcl[n + m].Wrap = false;

                if (ddlMode.SelectedValue == "7")
                {
                    e.Row.Cells[n].RowSpan = 2;

                    e.Row.Cells[n + 1].RowSpan = 2;
                    e.Row.Cells[n + 2].RowSpan = 2;
                    e.Row.Cells[n + 3].RowSpan = 2;
                    e.Row.Cells[n + 4].RowSpan = 2;
                    e.Row.Cells[n + 5].RowSpan = 2;
                   
                    e.Row.Cells[n + 6].ColumnSpan = 2;
                    e.Row.Cells[n + 7].ColumnSpan = 2;
                    e.Row.Cells[n + 8].ColumnSpan = 2;
                    e.Row.Cells[n + 9].ColumnSpan = 2;
                    e.Row.Cells[n + 10].ColumnSpan = 2;
                    e.Row.Cells[n + 11].ColumnSpan = 2;
                    e.Row.Cells[n + 12].ColumnSpan = 2;
                    e.Row.Cells[n + 13].ColumnSpan = 2;
                    e.Row.Cells[n + 14].ColumnSpan = 2;
                    e.Row.Cells[n + 15].ColumnSpan = 2;
                }
                else
                {
                    e.Row.Cells[n].RowSpan = 2;

                    e.Row.Cells[n + 1].RowSpan = 2;
                    e.Row.Cells[n + 2].RowSpan = 2;
                    e.Row.Cells[n + 3].RowSpan = 2;
                    e.Row.Cells[n + 4].RowSpan = 2;

                    e.Row.Cells[n + 5].ColumnSpan = 2;
                    e.Row.Cells[n + 6].ColumnSpan = 2;
                    e.Row.Cells[n + 7].ColumnSpan = 2;
                    e.Row.Cells[n + 8].ColumnSpan = 2;
                    e.Row.Cells[n + 9].ColumnSpan = 2;
                    e.Row.Cells[n + 10].ColumnSpan = 2;
                    e.Row.Cells[n + 11].ColumnSpan = 2;
                    e.Row.Cells[n + 12].ColumnSpan = 2;
                    e.Row.Cells[n + 13].ColumnSpan = 2;
                    e.Row.Cells[n + 14].ColumnSpan = 2;
                }
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


        protected void btnClass_Click(object sender, EventArgs e)
        {
            ddlTrainClass.Items.Clear();
            string strSql = "select * from ZJ_Train_Class where Train_Plan_ID=" + hfTrainPlan.Value;
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);

            ListItem item = new ListItem();
            item.Text = "--��ѡ��--";
            item.Value = "0";
            ddlTrainClass.Items.Add(item);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                item = new ListItem();
                item.Text = dr["Train_Class_Name"].ToString();
                item.Value = dr["Train_Class_ID"].ToString();
                ddlTrainClass.Items.Add(item);
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSelectExam.Text))
            {
                SessionSet.PageMessage = "������ѡ��һ�����ԣ�";
                return;
            }

            grdEntity.DataSource = GetDataSet();
            grdEntity.DataBind();
            for (int i = 0; i < grdEntity.Columns.Count; i++)
            {
                grdEntity.Columns[i].ItemStyle.Wrap = false;
            }
        	ViewState["dt"] = grdEntity.DataSource;
        }

        private DataSet GetDataSet()
        {
            string strSql = "";

            string strOrg = "";
            if (PrjPub.CurrentLoginUser.SuitRange == 0)
            {
                strOrg = " and GetStationOrgID(e.Org_ID)=" + PrjPub.CurrentLoginUser.StationOrgID;
            }

            string strExam = "(select * from Random_EXAM_RESULT union all select * from Random_EXAM_RESULT_Temp)";

            OracleAccess db = new OracleAccess();
            if (ddlType.SelectedValue == "1")
            {
                string strTotal;

                #region ʵ������
                /*strTotal =
                    @"SELECT 
                         '�ϼ�',
                         g.countNum as ʵ��,trunc(Avg(f.Score),2) as  ����,
                         max(f.Score) ��߷�,min(f.Score) as ��ͷ�,
                        Sum(case when f.Score=100 then 1 else 0 end ) as ""100��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score=100 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when f.Score>=90 and f.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=90 and f.Score<100 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when f.Score>=80 and f.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=80 and f.Score<90 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when f.Score>=70 and f.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=70 and f.Score<80 then 1 else 0 end )  *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when f.Score>=60 and f.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=60 and f.Score<70 then 1 else 0 end )  *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when f.Score>=50 and f.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=50 and f.Score<60 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when f.Score>=40 and f.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=40 and f.Score<50 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when f.Score>=30 and f.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=30 and f.Score<40 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when f.Score<30 then 1 else 0 end ) as ""30������"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score<30 then 1 else 0 end )*100/g.countNum as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when f.Score<60 then 1 else 0 end ) as ""60������"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when  f.Score<60 then 1 else 0 end )*100/g.countNum as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                        FROM Random_EXAM_RESULT A
                         INNER JOIN ORG B on b.org_id = a.org_id
                         INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                         INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                         INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                         Inner join (
                         select b.*,max(a.begin_time) begin_time from Random_Exam_Result a
                         inner join
                         (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                          from Random_EXAM_RESULT a where  a.status_id > 0
                          group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                          and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                          group by    b.examinee_id, b.Random_Exam_Id,b.score ) F on a.examinee_id = f.examinee_id
                          and a.score = f.score  and a.Random_Exam_Id = f.Random_Exam_Id
                          and a.begin_time=f.begin_time
                        inner join (select  a.StationOrgID,count(*) countNum from (select
                         GetOrgName(GetStationOrgID(e.org_id)) StationOrgID,count(*)  from Random_EXAM_RESULT  a
                          inner join EMPLOYEE E on e.employee_id = a.examinee_id
                         where a.Random_Exam_ID in (" +
                    hfExamId.Value.Replace("|", ",") +
                    @")
                          group by  GetOrgName(GetStationOrgID(e.org_id)),a.Examinee_Id) a
                          group by a.StationOrgID)  g on GetOrgName(GetStationOrgID(e.org_id))=g.StationOrgID
                          where a.Random_Exam_ID in (" +
                    hfExamId.Value.Replace("|", ",") + @") " + strOrg + @" and a.status_id > 0 group by g.countNum";*/
                #endregion

                strTotal =
                   @"SELECT 
                     '�ϼ�',
                     count(*) as ʵ��,trunc(Avg(a.Score),2) as  ����,
                     max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                    Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                    case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                    Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                    case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                    Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                    case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                    Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                    case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                    Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                    case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                    Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                     case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                    Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                     case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                    Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                     case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                   Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                    case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                   Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                    case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                    FROM "+strExam+@" A
                     INNER JOIN ORG B on b.org_id = a.org_id
                     INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                     INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                     INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                     Inner join (
                    select  b.examinee_id,min(a.begin_time) begin_time from " + strExam + @" a
                     inner join
                     (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                      from " + strExam + @" a where  a.status_id > 0
                      group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                      and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                      where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                      group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                      and a.begin_time=f.begin_time
                      where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @" and a.status_id > 0 ";

                if (ddlMode.SelectedValue == "1")
                {
                    //վ��

                    #region ʵ������
                    /* strSql =
                        @" SELECT 
                         GetOrgName(GetStationOrgID(e.org_id)) as վ������,
                         g.countNum||'-'||count(*)  as ʵ��,  trunc(Avg(f.Score),2) as  ����,
                         max(f.Score) ��߷�,min(f.Score) as ��ͷ�,
                        Sum(case when f.Score=100 then 1 else 0 end ) as ""100��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score=100 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when f.Score>=90 and f.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=90 and f.Score<100 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when f.Score>=80 and f.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=80 and f.Score<90 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when f.Score>=70 and f.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=70 and f.Score<80 then 1 else 0 end )  *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when f.Score>=60 and f.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=60 and f.Score<70 then 1 else 0 end )  *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when f.Score>=50 and f.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=50 and f.Score<60 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when f.Score>=40 and f.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=40 and f.Score<50 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when f.Score>=30 and f.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score>=30 and f.Score<40 then 1 else 0 end ) *100/g.countNum as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when f.Score<30 then 1 else 0 end ) as ""30������"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when f.Score<30 then 1 else 0 end )*100/g.countNum as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when f.Score<60 then 1 else 0 end ) as ""60������"",
                        case when g.countNum=0 then '0%' else to_char(cast(Sum(case when  f.Score<60 then 1 else 0 end )*100/g.countNum as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                        FROM Random_EXAM_RESULT A
                         INNER JOIN ORG B on b.org_id = a.org_id
                         INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                         INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                         INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                         Inner join (
                         select b.*,max(a.begin_time) begin_time from Random_Exam_Result a
                         inner join
                         (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                          from Random_EXAM_RESULT a where  a.status_id > 0
                          group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                          and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                          group by    b.examinee_id, b.Random_Exam_Id,b.score ) F on a.examinee_id = f.examinee_id
                          and a.score = f.score  and a.Random_Exam_Id = f.Random_Exam_Id
                          and a.begin_time=f.begin_time
                          inner join (select  a.StationOrgID,count(*) countNum from (select
                         GetOrgName(GetStationOrgID(e.org_id)) StationOrgID,count(*)  from Random_EXAM_RESULT  a
                          inner join EMPLOYEE E on e.employee_id = a.examinee_id
                         where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                          group by  GetOrgName(GetStationOrgID(e.org_id)),a.Examinee_Id) a
                          group by a.StationOrgID)  g on GetOrgName(GetStationOrgID(e.org_id))=g.StationOrgID
                          where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @" and a.status_id > 0
                          group by  GetOrgName(GetStationOrgID(e.org_id)),g.countNum order by  trunc(Avg(f.Score),2) desc";
                     */
                    #endregion

                    strSql =
                      @" SELECT 
                         GetOrgName(GetStationOrgID(e.org_id)) as վ������,
                         count(*)  as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                        FROM " + strExam + @" A
                         INNER JOIN ORG B on b.org_id = a.org_id
                         INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                         INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                         INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                         Inner join (
                        select  b.examinee_id,min(a.begin_time) begin_time from " + strExam + @" a
                         inner join
                         (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                          from " + strExam + @" a where  a.status_id > 0
                          group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                          and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                          where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                          group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                          and a.begin_time=f.begin_time
                          where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @" and a.status_id > 0
                          group by  GetOrgName(GetStationOrgID(e.org_id)) order by  trunc(Avg(a.Score),2) desc";
                }
                else if (ddlMode.SelectedValue == "2")
                {
                    //����
                    strSql =
                        @"SELECT 
                         GetOrgName(GetStationOrgID(e.org_id))||'-'|| GetWorkShopName(e.org_id) as ""վ��-����"",
                         count(*) as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                         FROM " + strExam + @" A
                         INNER JOIN ORG B on b.org_id = a.org_id
                         INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                         INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                         INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                         Inner join (
                         select  b.examinee_id,min(a.begin_time) begin_time from " + strExam + @" a
                         inner join
                         (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                          from " + strExam + @" a where  a.status_id > 0
                          group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                          and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                          where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                          group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                          and a.begin_time=f.begin_time
                          where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @"  and a.status_id > 0
                          group by  GetOrgName(GetStationOrgID(e.org_id)),GetWorkShopName(e.org_id)
                          order by trunc(Avg(a.Score),2) desc";
                }
                else if (ddlMode.SelectedValue == "3")
                {
                    //ְ��
                    strSql = @"SELECT 
                             x.Post_Name ְ��,
                         count(*) as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                             FROM " + strExam + @" A
                             INNER JOIN ORG B on b.org_id = a.org_id
                             INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                             INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                             inner join Post x  on x.Post_ID=e.Post_ID
                             INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                             Inner join (
                             select  b.examinee_id,min(a.begin_time) begin_time from " + strExam + @" a
                             inner join
                             (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                              from " + strExam + @" a where  a.status_id > 0
                              group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                              and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                              where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                              group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                              and a.begin_time=f.begin_time
                             where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @"  and a.status_id > 0
                              group by   x.Post_Name 
                              order by  trunc(Avg(a.Score),2) desc";
                }
                else if (ddlMode.SelectedValue == "4")
                {
                    //����(��������)
                    strTotal =
                      @"SELECT 
                         '�ϼ�',
                         count(*) as ʵ��,trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                        FROM " + strExam + @" A
                         INNER JOIN ORG B on b.org_id = a.org_id
                         INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                         INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                         INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                         Inner join (
                         select b.*,max(a.begin_time) begin_time from " + strExam + @" a
                         inner join
                         (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                          from " + strExam + @" a where  a.status_id > 0
                          group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                          and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                          group by    b.examinee_id, b.Random_Exam_Id,b.score ) F on a.examinee_id = f.examinee_id
                          and a.score = f.score  and a.Random_Exam_Id = f.Random_Exam_Id
                          and a.begin_time=f.begin_time
                          where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @" and a.status_id > 0 ";

                    strSql = @"SELECT 
                             c.Exam_Name ""���֣��������ƣ�"",
                         count(*) as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                             FROM " + strExam + @" A
                             INNER JOIN ORG B on b.org_id = a.org_id
                             INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                             INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                             inner join Post x  on x.Post_ID=e.Post_ID
                             INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                             Inner join (
                             select b.*,max(a.begin_time) begin_time from " + strExam + @" a
                             inner join
                             (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                              from " + strExam + @" a where  a.status_id > 0
                              group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                              and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                              group by    b.examinee_id, b.Random_Exam_Id,b.score ) F on a.examinee_id = f.examinee_id
                              and a.score = f.score  and a.Random_Exam_Id = f.Random_Exam_Id
                              and a.begin_time=f.begin_time
                             where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @"  and a.status_id > 0
                              group by   c.Exam_Name
                              order by  trunc(Avg(a.Score),2) desc";
                }
                else if (ddlMode.SelectedValue == "5")
                {
                    //�Ļ��̶�
                    strSql = @"SELECT 
                              substr(y.Order_Index||y.Education_Level_Name,2,length(y.Order_Index||y.Education_Level_Name)) �Ļ��̶�,
                         count(*) as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                             FROM "+strExam+@" A
                             INNER JOIN ORG B on b.org_id = a.org_id
                             INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                             INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                             left join Education_Level y on y.Education_Level_ID=e.Education_Level_ID
                             INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                             Inner join (
                             select  b.examinee_id,min(a.begin_time) begin_time from "+strExam+@" a
                             inner join
                             (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                              from "+strExam+@" a where  a.status_id > 0
                              group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                              and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                              where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                              group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                              and a.begin_time=f.begin_time
                             where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @"  and a.status_id > 0
                              group by    y.Order_Index||y.Education_Level_Name
                              order by  y.Order_Index||y.Education_Level_Name";
                }
                else if (ddlMode.SelectedValue == "6")
                {
                    //����ṹ
                    strSql = @"SELECT 
                             GetBirthday(e.birthday) as ����ṹ,
                         count(*) as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                             FROM "+strExam+@" A
                             INNER JOIN ORG B on b.org_id = a.org_id
                             INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                             INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                             INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                             Inner join (
                            select  b.examinee_id,min(a.begin_time) begin_time from "+strExam+@" a
                             inner join
                             (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                              from "+strExam+@" a where  a.status_id > 0
                              group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                              and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                              where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                              group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                              and a.begin_time=f.begin_time
                             where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @"  and a.status_id > 0
                              group by   GetBirthday(e.birthday) 
                              order by  GetBirthday(e.birthday) desc";
                }
                else if (ddlMode.SelectedValue == "7")
                {
                    //վ�ι���
                    strTotal = @"SELECT 
                         '�ϼ�','',
                         count(*) as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                        FROM "+strExam+@" A
                         INNER JOIN ORG B on b.org_id = a.org_id
                         INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                         INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                         INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                         Inner join (
                         select  b.examinee_id,min(a.begin_time) begin_time from "+strExam+@" a
                         inner join
                         (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                          from "+strExam+@" a where  a.status_id > 0
                          group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                          and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                          where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                          group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                          and a.begin_time=f.begin_time
                          where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @" and a.status_id > 0";

                    strSql = @"select a.վ������,a.����,a.ʵ��,a.����,a.��߷�,a.��ͷ�,
                               a.""100��"",a.""100�ֱ���"",a.""90�֡�99��"",a.""90�֡�99�ֱ���"",
                               a.""80�֡�89��"",a.""80�֡�89�ֱ���"",a.""70�֡�79��"",a.""70�֡�79�ֱ���"",
                               a.""60�֡�69��"",a.""60�֡�69�ֱ���"",a.""50�֡�59��"",a.""50�֡�59�ֱ���"",
                               a.""40�֡�49��"",a.""40�֡�49�ֱ���"",a.""30�֡�39��"",a.""30�֡�39�ֱ���"",
                               a.""30������"",a.""30�����±���"",a.""60������"",a.""60�����±���""
                        from (
                        SELECT   GetStationOrgID(e.org_id) stationorgId, 999999999 random_exam_id,
                        GetOrgName(GetStationOrgID(e.org_id)) as վ������,'С��' as ����,
                         count(*) as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                             FROM "+strExam+@" A
                             INNER JOIN ORG B on b.org_id = a.org_id
                             INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                             INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                             INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                             Inner join (
                             select  b.examinee_id,min(a.begin_time) begin_time from "+strExam+@" a
                             inner join
                             (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                              from "+strExam+@" a where  a.status_id > 0
                              group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                              and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                              where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                              group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                              and a.begin_time=f.begin_time
                             where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @"  and a.status_id > 0
                              group by     GetStationOrgID(e.org_id),GetOrgName(GetStationOrgID(e.org_id))
                             union all 
                        SELECT   GetStationOrgID(e.org_id) stationorgId,  c.random_exam_id,
                        GetOrgName(GetStationOrgID(e.org_id)) as վ������,to_char(c.Exam_Name) as ����,
                         count(*) as ʵ��,  trunc(Avg(a.Score),2) as  ����,
                         max(a.Score) ��߷�,min(a.Score) as ��ͷ�,
                        Sum(case when a.Score=100 then 1 else 0 end ) as ""100��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score=100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""100�ֱ���"",
                        Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) as ""90�֡�99��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=90 and a.Score<100 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""90�֡�99�ֱ���"",
                        Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) as ""80�֡�89��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=80 and a.Score<90 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""80�֡�89�ֱ���"",
                        Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end ) as ""70�֡�79��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=70 and a.Score<80 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""70�֡�79�ֱ���"",
                        Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end ) as ""60�֡�69��"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=60 and a.Score<70 then 1 else 0 end )  *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""60�֡�69�ֱ���"",
                        Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) as ""50�֡�59��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=50 and a.Score<60 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""50�֡�59�ֱ���"",
                        Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) as ""40�֡�49��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=40 and a.Score<50 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""40�֡�49�ֱ���"",
                        Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) as ""30�֡�39��"",
                         case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score>=30 and a.Score<40 then 1 else 0 end ) *100/count(*) as decimal(18,1)),'fm990.0')||'%' end as ""30�֡�39�ֱ���"",
                       Sum(case when a.Score<30 then 1 else 0 end ) as ""30������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when a.Score<30 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""30�����±���"",
                       Sum(case when a.Score<60 then 1 else 0 end ) as ""60������"",
                        case when count(*)=0 then '0%' else to_char(cast(Sum(case when  a.Score<60 then 1 else 0 end )*100/count(*) as decimal(18,1)),'fm990.0')||'%'  end as ""60�����±���""
                             FROM "+strExam+@" A
                             INNER JOIN ORG B on b.org_id = a.org_id
                             INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                             INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                             INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                             Inner join (
                             select  b.examinee_id,min(a.begin_time) begin_time from "+strExam+@" a
                             inner join
                             (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                              from "+strExam+@" a where  a.status_id > 0
                              group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                              and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                              where  a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @")
                              group by    b.examinee_id ) F on a.examinee_id = f.examinee_id                        
                              and a.begin_time=f.begin_time
                             where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @"  and a.status_id > 0
                             group by    GetStationOrgID(e.org_id),GetOrgName(GetStationOrgID(e.org_id)),c.random_exam_id,C.Exam_Name) a 
                              order by    a.StationOrgID,a.Random_Exam_Id desc";
                }

                DataSet dsTotal = db.RunSqlDataSet(strTotal);
                DataRow dr = dsTotal.Tables[0].Rows[0];
                DataSet ds = db.RunSqlDataSet(strSql);
                DataRow drNew = ds.Tables[0].NewRow();
                for(int i=0; i<dsTotal.Tables[0].Columns.Count; i++)
                {
                    drNew[i] = dr[i];
                }
                ds.Tables[0].Rows.InsertAt(drNew,0);

                return ds;
            }
            else
            {
                strSql =
                    @"SELECT 
                         e.Employee_Name ����,e.Work_No Ա������,e.identity_CardNo as ���֤����,
                         x.education_level_name as �Ļ��̶�,y.post_Name as ְ��,
                        trunc(months_between(sysdate,e.birthday)/12) ����,
                         f.Score ����
                         FROM "+strExam+@" A
                         INNER JOIN ORG B on b.org_id = a.org_id
                         INNER JOIN Random_EXAM C on c.Random_Exam_Id = a.Random_Exam_Id
                         INNER JOIN EMPLOYEE E on e.employee_id = a.examinee_id
                         inner join Education_Level x on e.Education_level_id=x.Education_level_id
                         inner join Post y on e.Post_ID=y.Post_ID
                         INNER JOIN EXAM_Result_STATUS G on g.exam_result_status_id =a.status_id
                         Inner join (
                         select b.*,max(a.begin_time) begin_time from "+strExam+@" a
                         inner join
                         (select   max(a.score) score, a.examinee_id, a.Random_Exam_Id
                          from "+strExam+@" a where  a.status_id > 0
                          group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                          and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                          group by    b.examinee_id, b.Random_Exam_Id,b.score ) F on a.examinee_id = f.examinee_id
                          and a.score = f.score  and a.Random_Exam_Id = f.Random_Exam_Id
                          and a.begin_time=f.begin_time
                          where a.Random_Exam_ID in (" + hfExamId.Value.Replace("|", ",") + @") " + strOrg + @"  and a.status_id > 0
                          order by f.Score desc";
                return db.RunSqlDataSet(strSql);
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlType.SelectedValue == "1")
            {
                ddlMode.Enabled = true;
            }
            else
            {
                ddlMode.Enabled = false;
            }
        }

		protected void btnExcel_Click(object sender, EventArgs e)
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
				filename = Server.MapPath("/RailExamBao/Excel/ExamResult.xls");

				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}

				int isNum = 0;        //���֤�ŵ���
				int isNum2 = 0;       //Ա��������
				//�����õ��ı������,��ֵ����Ԫ��   
			    int index=1;
			    objSheet.Cells.Font.Size = 10;
				objSheet.Cells[index,1] = "��\r\n��";
				DataTable dt = ((DataSet) ViewState["dt"]).Tables[0];

                int col = 2;

                if(ddlType.SelectedValue=="1")
                {
                    range = objSheet.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    if(ddlMode.SelectedValue=="1")
                    {
                        objSheet.Cells[index, col] = "��λ����";
                    }
                    else if (ddlMode.SelectedValue == "2")
                    {
                        objSheet.Cells[index, col] = "��λ-����";
                    }
                    else if (ddlMode.SelectedValue == "3")
                    {
                        objSheet.Cells[index, col] = "ְ��";
                    }
                    else if (ddlMode.SelectedValue == "4")
                    {
                        objSheet.Cells[index, col] = "���֣��������ƣ�";
                    }
                    else if (ddlMode.SelectedValue == "5")
                    {
                        objSheet.Cells[index, col] = "�Ļ��̶�";
                    }
                    else if (ddlMode.SelectedValue == "6")
                    {
                        objSheet.Cells[index, col] = "����ṹ";
                    }
                    else if (ddlMode.SelectedValue == "7")
                    {
                        objSheet.Cells[index, col] = "��λ";
                    }
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index+1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    if (ddlMode.SelectedValue == "7")
                    {
                        col++;
                        objSheet.Cells[index, col] = "����";
                        range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index + 1, col]);
                        range.Merge(0);
                        range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        range.Font.Bold = true;
                    }

                    col++;
                    objSheet.Cells[index, col] = "ʵ������";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "����";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "��߷�";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    col++;
                    objSheet.Cells[index, col] = "��ͷ�";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "100��";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index , col+1]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "90-99��";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "80-89��";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    col++;
                    objSheet.Cells[index, col] = "70-79��";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    col++;
                    objSheet.Cells[index, col] = "60-69��";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "50-59��"; 
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    col++;
                    objSheet.Cells[index, col] = "40-49��"; 
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.Font.Bold = true;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "30-39��"; 
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "30������";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index, col] = "60������";
                    range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col + 1]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    objSheet.Cells[index + 1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    col++;
                    objSheet.Cells[index+1, col] = "��\r\n��";
                    range = objSheet.get_Range(objSheet.Cells[index + 1, col], objSheet.Cells[index + 1, col]);
                    range.Merge(0);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;

                    index++;
                }
                else
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        objSheet.Cells[index, col] = dt.Columns[i].ColumnName;
                        range = objSheet.get_Range(objSheet.Cells[index, col], objSheet.Cells[index, col]);
                        range.Merge(0);
                        range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        if (dt.Columns[i].ColumnName.Equals("���֤����"))
                            isNum = col;
                        if (dt.Columns[i].ColumnName.Equals("Ա������"))
                            isNum2 = col;
                        col++;
                    } 
                }
				index++;

               //ͬ��������������  
				
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					int col2 = 2;
                    if(i !=0 )
                    {
                        objSheet.Cells[index + i, 1] = i;
                        range = objSheet.get_Range(objSheet.Cells[index + i, 1], objSheet.Cells[index + i, 1]);
                        range.Merge(0);
                        range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    }

					for (int j = 0; j < dt.Columns.Count; j++)
					{
						objSheet.Cells[index + i, col2] = dt.Rows[i][j].ToString();
                        range = objSheet.get_Range(objSheet.Cells[index + i, col2], objSheet.Cells[index + i, col2]);
                        range.Merge(0);
                        range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        if (dt.Rows[i][j].ToString().IndexOf("%")>=0)
                        {
                            range.NumberFormatLocal = "0.0%";
                        }

						if (col2 == isNum || col2==isNum2)
							objSheet.Cells[index + i, col2] = "'" + dt.Rows[i][j];
						col2++;
					}
				}
				objSheet.Cells.Columns.AutoFit();

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

			string filenames = Server.MapPath("/RailExamBao/Excel/ExamResult.xls");

			if (File.Exists(filenames))
			{
				FileInfo file = new FileInfo(filenames.ToString());
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("���Խ������") + ".xls");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
				this.Response.ContentType = "application/ms-excel";
				// ���ļ������͵��ͻ���
				this.Response.WriteFile(file.FullName);
			}
		}
    }
}
