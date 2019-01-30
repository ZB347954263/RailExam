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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerRoomUse : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                dateBeginTime.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
                dateEndTime.DateValue = Convert.ToDateTime(DateTime.Today.AddMonths(1).Year.ToString("0000") + "-" + DateTime.Today.AddMonths(1).Month.ToString("00") +
                                    "-01").ToString("yyyy-MM-dd");


                for (int i = 1; i <= 24; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = i.ToString("00");
                    item.Text = i.ToString("00");
                    ddlBeginHour.Items.Add(item);
                }

                for (int i = 1; i <= 24; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = i.ToString("00");
                    item.Text = i.ToString("00");
                    ddlEndHour.Items.Add(item);
                }

                ddlBeginHour.SelectedValue = "09";
                ddlEndHour.SelectedValue = "18";

                BindGrid();
            }
        }

        private void BindGrid()
        {
           string strBegin = dateBeginTime.DateValue.ToString() + " " + ddlBeginHour.SelectedValue + ":00:00";
           string strEnd = dateEndTime.DateValue.ToString() + " " + ddlEndHour.SelectedValue + ":00:00";

           string strSql = "select a.COMPUTER_ROOM_ID,c.COMPUTER_ROOM_Name,Apply_Start_Time,Apply_End_Time, "
                            + "'被'|| b.Short_Name || '占用'   UseStatus,COMPUTER_ROOM_APPLY_ID,d.Short_Name orgName,1 as UseStatusID "
                            +"from Computer_Room_Apply a "
                            + " inner join org b on a.org_id=b.org_id"
                            + " inner join Computer_Room c on a.Computer_Room_ID=c.Computer_Room_ID "
                             + " inner join org d on c.org_id=d.org_id"
                            + " where  a.REPLY_STATUS=1 and a.Computer_Room_ID=" + Request.QueryString.Get("roomId")
                            + " and  a.Apply_End_Time >= to_date('" + strBegin + "','YYYY-MM-DD HH24:MI:SS')"
                            + " and a.Apply_Start_Time <= to_date('" + strEnd + "','YYYY-MM-DD HH24:MI:SS') "
                            +" order by a.Apply_Start_Time ";

           OracleAccess oracle = new OracleAccess();
            DataTable db = oracle.RunSqlDataSet(strSql).Tables[0];
           if (db.Rows.Count == 0)
           {
               DataRow row = db.NewRow();
               row["COMPUTER_ROOM_APPLY_ID"] = -1;
               db.Rows.Add(row);
           }
           else
           {
               DateTime beginDate = Convert.ToDateTime(strBegin);
               DateTime endDate = Convert.ToDateTime(strEnd);

               DataTable dt = db.Clone();

               int n = 0;
               for (int i = 0; i < db.Rows.Count; i++)
               {
                   DataRow drNew = dt.NewRow();
                   drNew["COMPUTER_ROOM_APPLY_ID"] = n;
                   drNew["orgName"] = db.Rows[i]["orgName"].ToString();
                   drNew["Computer_Room_ID"] = db.Rows[i]["Computer_Room_ID"].ToString();
                   drNew["Computer_Room_Name"] = db.Rows[i]["Computer_Room_Name"].ToString();

                   DateTime begin = Convert.ToDateTime(db.Rows[i]["Apply_Start_Time"].ToString());
                   DateTime end = Convert.ToDateTime(db.Rows[i]["Apply_End_Time"].ToString());

                   if (i == 0)
                   {
                       if (begin <= beginDate)
                       {
                           drNew["Apply_Start_Time"] = beginDate;
                           drNew["Apply_End_Time"] = end;
                           drNew["UseStatus"] = db.Rows[i]["UseStatus"].ToString();
                           drNew["UseStatusID"] = 1;
                           dt.Rows.Add(drNew);
                           n++;
                       }
                       else if (begin > beginDate)
                       {
                           drNew["Apply_Start_Time"] = beginDate;
                           drNew["Apply_End_Time"] = end;
                           drNew["UseStatus"] = "空闲";
                           drNew["UseStatusID"] = 0;
                           dt.Rows.Add(drNew);
                           n++;
                       }
                   }
                   else
                   {
                       DateTime lastbegin = Convert.ToDateTime(db.Rows[i - 1]["Apply_Start_Time"].ToString());
                       DateTime lastend = Convert.ToDateTime(db.Rows[i - 1]["Apply_End_Time"].ToString());

                       //当上一个的结束时间小于开始时间，那么上一个的结束时间到开始时间空闲
                       if (lastend < begin)
                       {
                           drNew["Apply_Start_Time"] = lastend;
                           drNew["Apply_End_Time"] = begin;
                           drNew["UseStatus"] = "空闲";
                           drNew["UseStatusID"] = 0;
                           dt.Rows.Add(drNew);
                           n++;
                       }

                       //当结束时间小于查询截止时间，那么需添加一行占用信息
                       if (end < endDate)
                       {
                           drNew = dt.NewRow();
                           drNew["COMPUTER_ROOM_APPLY_ID"] = n;
                           drNew["orgName"] = db.Rows[i]["orgName"].ToString();
                           drNew["Computer_Room_ID"] = db.Rows[i]["Computer_Room_ID"].ToString();
                           drNew["Computer_Room_Name"] = db.Rows[i]["Computer_Room_Name"].ToString();
                           drNew["Apply_Start_Time"] = begin;
                           drNew["Apply_End_Time"] = end;
                           drNew["UseStatus"] = db.Rows[i]["UseStatus"].ToString();
                           drNew["UseStatusID"] = 1;
                           dt.Rows.Add(drNew);
                           n++;

                       }
                   }

                   if (i == db.Rows.Count - 1)
                   {
                       //当最后一个时间的结束时间大于等于查询截止时间，那么最后一个开始时间到查询截止时间不空闲
                       if (end >= endDate)
                       {
                           drNew = dt.NewRow();
                           drNew["COMPUTER_ROOM_APPLY_ID"] = n;
                           drNew["orgName"] = db.Rows[i]["orgName"].ToString();
                           drNew["Computer_Room_ID"] = db.Rows[i]["Computer_Room_ID"].ToString();
                           drNew["Computer_Room_Name"] = db.Rows[i]["Computer_Room_Name"].ToString();

                           drNew["Apply_Start_Time"] = begin;
                           drNew["Apply_End_Time"] = endDate;
                           drNew["UseStatus"] = db.Rows[i]["UseStatus"].ToString();
                           drNew["UseStatusID"] = 1;
                           dt.Rows.Add(drNew);
                           n++;
                       }
                       //当最后一个时间的结束时间小于查询截止时间，那么最后一个结束时间到查询截止时间空闲
                       else if (end < endDate)
                       {
                           drNew = dt.NewRow();
                           drNew["COMPUTER_ROOM_APPLY_ID"] = n;
                           drNew["orgName"] = db.Rows[i]["orgName"].ToString();
                           drNew["Computer_Room_ID"] = db.Rows[i]["Computer_Room_ID"].ToString();
                           drNew["Computer_Room_Name"] = db.Rows[i]["Computer_Room_Name"].ToString();

                           drNew["Apply_Start_Time"] = end;
                           drNew["Apply_End_Time"] = endDate;
                           drNew["UseStatus"] = "空闲";
                           drNew["UseStatusID"] = 0;
                           dt.Rows.Add(drNew);
                           n++;
                       }
                   }
               }
               grdEntity.DataSource = dt;
               grdEntity.DataBind();

               strSql = "select Org_ID from Computer_Room where Computer_Room_ID=" + Request.QueryString.Get("roomId");
               DataRow dr = oracle.RunSqlDataSet(strSql).Tables[0].Rows[0];
               ViewState["Org_ID"] = dr[0].ToString();
               if(PrjPub.CurrentLoginUser.StationOrgID.ToString() == dr[0].ToString())
               {
                   grdEntity.Columns[0].Visible = false;
               }
           }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            string Id = Request.Form["__EVENTARGUMENT"];
            string strBegin = "", strEnd = "";
            foreach (GridViewRow row in grdEntity.Rows)
            {
                if (grdEntity.DataKeys[row.RowIndex].Values[0].ToString()== Id)
                {
                    strBegin = row.Cells[5].Text;
                    strEnd = row.Cells[6].Text;
                    break;
                }
            }

            string strQuery = strBegin + "|" + strEnd + "|" + Request.QueryString.Get("roomId") + "|" + ViewState["Org_ID"];
            ClientScript.RegisterStartupScript(GetType(), "apply", "showApply('" + strQuery + "');", true);
        }

        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
                {
                    e.Row.Visible = false;
                }
                else
                {
                    e.Row.Attributes.Add("onclick", "selectArow('" + e.Row.RowIndex + "');");
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}
