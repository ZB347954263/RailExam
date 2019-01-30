using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class SelectComputeRoom : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strID = Request.QueryString.Get("id");

                RandomExamBLL randomExamBll = new RandomExamBLL();
                RailExam.Model.RandomExam exam = randomExamBll.GetExam(Convert.ToInt32(strID));
                OracleAccess db = new OracleAccess();

                //申请开始时间小于等于考试开始时间，申请结束时间大于等于考试结束时间
                string strSql = "select a.* from (";

                OrganizationBLL orgBll = new OrganizationBLL();
                Organization org = orgBll.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID);
                hfStationSuitRage.Value = org.SuitRange.ToString();

                if (hfStationSuitRage.Value == "1" || hfStationSuitRage.Value == "0")
                {
                    strSql += "select distinct b.Org_ID,b.Short_Name,b.Order_Index"
                      + " from Computer_Room a "
                      + " inner join Org b on a.Org_ID=b.Org_ID "
                      + " where b.Level_Num=2 and a.IS_EFFECT=1 and b.Org_ID<>" + PrjPub.CurrentLoginUser.StationOrgID; // and Is_Use=0
                }
                else
                {
                    strSql += "select distinct b.Org_ID,b.Short_Name,b.Order_Index"
                              + " from Computer_Room_Apply a "
                              + " inner join Org b on a.Apply_Org_ID=b.Org_ID "
                              + " inner join Computer_Room c on a.Computer_Room_ID=c.Computer_Room_ID "
                              + " where REPLY_STATUS=1 and c.IS_EFFECT=1 and a.Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID
                              + " and to_date('" + exam.BeginTime + "','YYYY-MM-DD HH24:MI:SS')>= APPLY_START_TIME"
                              + " and to_date('" + exam.EndTime + "','YYYY-MM-DD HH24:MI:SS')<= APPLY_END_TIME ";// and Is_Use=0
                }


                string str = GetUseSql(db, exam);
                if (db.RunSqlDataSet(str).Tables[0].Rows.Count > 0)
                {
                    strSql += " union all"
                              + " select Org_ID,Short_Name,Order_Index from Org where Org_ID="
                              + PrjPub.CurrentLoginUser.StationOrgID;
                }

                strSql += " ) a order by a.Order_Index";

                DataSet ds = db.RunSqlDataSet(strSql);

                bool bind = false;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem item = new ListItem();
                    item.Text = dr["Short_Name"].ToString();
                    item.Value = dr["Org_ID"].ToString();
                    ddlOrg.Items.Add(item);

                    if(PrjPub.CurrentLoginUser.StationOrgID.ToString() == dr["Org_ID"].ToString())
                    {
                        bind = true;
                    }
                }

                if(bind)
                {
                    ddlOrg.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                }

                ddlOrg_SelectedIndexChanged(null, null);
            }
        }

        //获取查询本站段可用机房的Sql语句
        private string GetUseSql(OracleAccess db, RailExam.Model.RandomExam exam)
        {
            //判断本站微机教室是否被其他站段占用。
            string strSql = "select a.Computer_Room_ID from Computer_Room_Apply a "
                 + " inner join Org b on a.Apply_Org_ID=b.Org_ID "
                 + " inner join Computer_Room c on a.Computer_Room_ID=c.Computer_Room_ID "
                 + " where REPLY_STATUS=1 and a.Apply_Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID
                 + " and ((to_date('" + exam.BeginTime + "','YYYY-MM-DD HH24:MI:SS')>= APPLY_START_TIME"
                 + " and to_date('" + exam.BeginTime + "','YYYY-MM-DD HH24:MI:SS')<= APPLY_END_TIME)"
                 + " or (to_date('" + exam.EndTime + "','YYYY-MM-DD HH24:MI:SS')>= APPLY_START_TIME "
                 + " and to_date('" + exam.EndTime + "','YYYY-MM-DD HH24:MI:SS')<= APPLY_END_TIME)) "
                 + " and c.Is_Use=1 and c.IS_EFFECT=1"; 
            DataSet dsUse = db.RunSqlDataSet(strSql);

            string strUse = string.Empty;
            foreach (DataRow dr in dsUse.Tables[0].Rows)
            {
                #region 判断是否存在该微机教室正在进行的考试，如果存在需要排除
                //strSql = "select * from Random_Exam_Arrange_Detail a "
                //         + "inner join Random_Exam b on a.Random_Exam_ID=b.Random_Exam_ID "
                //         + "where a.Computer_Room_ID=" + dr["Computer_Room_ID"]
                //         + " and (b.Status_ID<=1 or b.Is_Start<=1)";

                //if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                //{
                //    if (strUse == string.Empty)
                //    {
                //        strUse += dr["Computer_Room_ID"].ToString();
                //    }
                //    else
                //    {
                //        strUse += "," + dr["Computer_Room_ID"];
                //    }
                //}
                #endregion

                if (strUse == string.Empty)
                {
                    strUse += dr["Computer_Room_ID"].ToString();
                }
                else
                {
                    strUse += "," + dr["Computer_Room_ID"];
                }
            }

            //排除本站段被其他站段占用的微机教室
            strSql = " select Computer_Room_ID,Computer_Room_Name "
                     + " from Computer_Room c "
                     + " where  c.IS_EFFECT=1 and Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID;
            
            //if(strUse != string.Empty)
            //{
            //    strSql += " and Computer_Room_ID not in (" + strUse + ")";
            //}

            return strSql;
        }

        protected void ddlOrg_SelectedIndexChanged(object sener, EventArgs e)
        {
            string strID = Request.QueryString.Get("id");
            RandomExamBLL randomExamBll = new RandomExamBLL();
            RailExam.Model.RandomExam exam = randomExamBll.GetExam(Convert.ToInt32(strID));

            string strSql;

            OracleAccess db = new OracleAccess();
            //当所选的站段不是登录者站段时
            if (ddlOrg.SelectedValue != PrjPub.CurrentLoginUser.StationOrgID.ToString() && !string.IsNullOrEmpty(ddlOrg.SelectedValue))
            {
                if (hfStationSuitRage.Value == "1" || hfStationSuitRage.Value == "0")
                {
                    strSql = "select c.Computer_Room_ID,c.Computer_Room_Name"
                             + " from Computer_Room c "
                             + " where   c.IS_EFFECT=1 and c.Org_ID=" + ddlOrg.SelectedValue;
                    //+ " and c.Is_Use=0";
                }
                else
                {
                    //申请开始时间小于等于考试开始时间，申请结束时间大于等于考试结束时间
                    strSql = "select c.Computer_Room_ID,c.Computer_Room_Name"
                             + " from Computer_Room_Apply a "
                             + " inner join Computer_Room c on a.Computer_Room_ID=c.Computer_Room_ID"
                             + " where  c.IS_EFFECT=1 and  REPLY_STATUS=1 and a.Apply_Org_ID=" + ddlOrg.SelectedValue
                             + " and a.Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID
                             + " and to_date('" + exam.BeginTime + "','YYYY-MM-DD HH24:MI:SS')>= APPLY_START_TIME"
                             + " and to_date('" + exam.EndTime + "','YYYY-MM-DD HH24:MI:SS')<= APPLY_END_TIME ";// and Is_Use=0
                }
            }
            else
            {
                strSql = GetUseSql(db,exam);
            }

            //排除已经被安排，并且所对应服务器已经生成试卷的微机教室
            strSql +=
                " and c.Computer_Room_ID not in (select a.Computer_Room_ID from Random_Exam_Arrange_Detail a "
                + "inner join Computer_Room b on a.Computer_Room_ID = b.Computer_Room_ID "
                + "inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID"
                + " where Random_Exam_ID=" +strID + " and  to_number(c.Computer_Server_No) "
                + " in (select Computer_server_No from Random_Exam_Computer_Server "
                + "where Random_Exam_ID=" + strID + " and Has_Paper=1 )) ";


            DataSet ds = db.RunSqlDataSet(strSql);
            ddlCompute.Items.Clear();

            ListItem item = new ListItem();
            item.Text = "--请选择--";
            item.Value = "0";
            ddlCompute.Items.Add(item);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                item = new ListItem();
                item.Text = dr["Computer_Room_Name"].ToString();
                item.Value = dr["Computer_Room_ID"].ToString();
                ddlCompute.Items.Add(item);
            }

            ddlCompute.SelectedValue = "0";
        }

        protected  void btnOK_Click(object sener, EventArgs e)
        {
            if(ddlCompute.SelectedValue == "0")
            {
                SessionSet.PageMessage = "请选择微机教室！";
                return;
            }

            string strID = Request.QueryString.Get("id");

            string strUserIds = hfUserId.Value;

            RandomExamArrangeBLL arrangeBll = new RandomExamArrangeBLL();
            RandomExamArrange randomExamArrange = arrangeBll.GetRandomExamArranges(Convert.ToInt32(strID))[0];

            //查询当前考试在所选微机教室下是否存在考生安排明细，如果存在则需要先删除
            string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID +
                            " and Computer_Room_ID=" + ddlCompute.SelectedValue;
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);

            if(ds.Tables[0].Rows.Count > 0)
            {
                //删除前，先记录原已安排在所选危机教室的考生，并与新安排的考生信息拼接起来
                string strOldIds = ds.Tables[0].Rows[0]["User_Ids"].ToString();
                string[] strOld = strOldIds.Split(',');
                for (int j = 0; j < strOld.Length;j++ )
                {
                    if(("|"+strUserIds+"|").IndexOf("|"+strOld[j]+"|") < 0)
                    {
                        strUserIds += "|" + strOld[j];
                    }
                }

                //删除
                strSql = "delete from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID +
                                " and Computer_Room_ID=" + ddlCompute.SelectedValue;
                db.ExecuteNonQuery(strSql);
            }


            //查询当前考试不在本危机教室下的考生安排明细
            strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID +
                            " and Computer_Room_ID<>" + ddlCompute.SelectedValue;
            DataSet dsOther = db.RunSqlDataSet(strSql);

            //遍历当前需要重新安排的考生信息，查询其他考生安排明细是否存在当前需要重新安排的考生，如果存在则需修改去除该考生
            string[] str = strUserIds.Split('|');
            for (int i = 0; i < str.Length; i++ )
            {
                string strReplace = "," + str[i] + ",";
                DataRow[] drs = dsOther.Tables[0].Select("','+User_Ids+',' like '%" + strReplace + "%'");

                if(drs.Length>0)
                {
                    strSql = "update Random_Exam_Arrange_Detail "
                            + "set User_ids = substr(Replace(','||User_ids||',','"+ strReplace +"',','),2,length(Replace(','||User_ids||',','"+ strReplace +"',','))-2) "
                            + "where  ','|| User_ids || ',' like '%"+ strReplace +"%' and Random_Exam_ID=" + strID +
                            " and Computer_Room_ID<>" + ddlCompute.SelectedValue;

                    db.ExecuteNonQuery(strSql);
                }
            }

            //最后删除User_ids为空的考生安排明细信息
            strSql = "delete from Random_Exam_Arrange_Detail where User_ids is null  and Random_Exam_ID=" + strID
                + " and Computer_Room_ID<>" + ddlCompute.SelectedValue;
            db.ExecuteNonQuery(strSql); 


            //添加
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string value = node.Value;
            int id = 0;
            if (value == "Oracle")
            {
                OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                OracleParameter para2 = new OracleParameter("p_random_exam_arrange_de_id", OracleType.Number);
                para2.Direction = ParameterDirection.Output;
                OracleParameter para3 = new OracleParameter("p_random_exam_arrange_Id", OracleType.Number);
                para3.Value = randomExamArrange.RandomExamArrangeId;
                OracleParameter para4 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
                para4.Value = Convert.ToInt32(strID);
                OracleParameter para5 = new OracleParameter("p_computer_room_id", OracleType.Number);
                para5.Value =Convert.ToInt32(ddlCompute.SelectedValue);


                IDataParameter[] paras = new IDataParameter[] { para1, para2, para3, para4, para5};
                id =
                    Pub.RunAddProcedure(false, "USP_RANDOM_EXAM_ARRANGE_DE_I", paras,
                                    System.Text.Encoding.Unicode.GetBytes(strUserIds.Replace("|",",")));
            }


            //向Random_Exam_Computer_Server 机房考试状态表 插入记录，每个相关的服务器都有一条记录
            strSql = "select c.Computer_Server_No from Random_Exam_Arrange_Detail a "
                     + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                     + " inner join Computer_Server c on c.Computer_Server_ID=b.Computer_Server_ID "
                     + " where a.Random_Exam_ID=" + strID;
            DataSet dsComputer = db.RunSqlDataSet(strSql);

            strSql = "select * from Random_Exam_Computer_Server "
                     + " where Random_Exam_ID=" + strID;
            DataSet dsServer = db.RunSqlDataSet(strSql);

            foreach (DataRow dr in dsComputer.Tables[0].Rows)
            {
                DataRow[] drs = dsServer.Tables[0].Select("Computer_Server_No ='" + dr["Computer_Server_No"] + "'");
                if(drs.Length==0)
                {
                    string serverNo = dr["Computer_Server_No"].ToString();
                    strSql = "insert into Random_Exam_Computer_Server"
                     + "(Random_Exam_ID,Computer_Server_No,Status_ID,Is_Start,Has_Paper,"
                     + "Random_Exam_Code,Is_Upload,DownLoaded) "
                     + "values (" + strID + "," + serverNo + ",0,0,0,'',0,0)";
                    db.ExecuteNonQuery(strSql); 
                }              
            }

            //删除没有相关微机教室的Random_Exam_Computer_Server数据
            strSql =
                @"delete from random_exam_computer_server 
                    where Random_Exam_ID="+strID+@"
                    and Computer_Server_No not in (select  to_number(c.Computer_Server_No)
                    from Random_Exam_Arrange_Detail a
                    inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
                    inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID
                    where Random_Exam_ID=" + strID + @")";
            db.ExecuteNonQuery(strSql);

            //判断是否所有考生都被安排微机教室
            int totalCount = randomExamArrange.UserIds.Split(',').Length;
            int nowCount = 0;
            strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID;
            DataSet dsNow = db.RunSqlDataSet(strSql);
            foreach (DataRow dr in dsNow.Tables[0].Rows)
            {
                nowCount += dr["User_Ids"].ToString().Split(',').Length;
            }
            if(totalCount == nowCount)
            {
                strSql = "update Random_Exam set Is_All_Arrange=1 where Random_Exam_ID=" + strID;
                db.ExecuteNonQuery(strSql);
            }

            //更新一次微机教室，将考试版本提高一级
            strSql = "update Random_Exam set version=version+1 where Random_Exam_ID=" +
                           Request.QueryString.Get("id");
            db.ExecuteNonQuery(strSql);


            Response.Write("<script>window.returnValue = 'true',window.close();</script>");
        }
    }
}
