using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamComputerSeatDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string id = Request.QueryString.Get("id");

                OracleAccess db = new OracleAccess();

                DataRow dr ;
                string strSql = " select a.*,b.Exam_Name,c.Employee_Name,d.Computer_Number,d.Bad_Seat,"
                                +"GetOrgName(d.Org_ID)||'-'||d.Computer_Room_Name Room_Name,d.Computer_Server_ID"
                                +" from Random_Exam_Result_Detail_Temp a "
                                + " inner join Random_Exam b on a.Random_Exam_ID=b.Random_Exam_ID "
                                + " inner join Employee c on a.Employee_ID=c.Employee_ID "
                                + " inner join Computer_Room d on a.Computer_Room_ID=d.Computer_Room_ID"
                                + " where Random_Exam_Result_Detail_ID=" + id;
                DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    strSql = " select a.*,b.Exam_Name,c.Employee_Name,d.Computer_Number,d.Bad_Seat,"
                                + "GetOrgName(d.Org_ID)||'-'||d.Computer_Room_Name Room_Name,d.Computer_Server_ID"
                                + " from Random_Exam_Result_Detail a "
                                + " inner join Random_Exam b on a.Random_Exam_ID=b.Random_Exam_ID "
                                + " inner join Employee c on a.Employee_ID=c.Employee_ID "
                                + " inner join Computer_Room d on a.Computer_Room_ID=d.Computer_Room_ID"
                                + " where Random_Exam_Result_Detail_ID=" + id;
                    dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    ViewState["Old"] = true;
                }
                else
                {
                    dr = dt.Rows[0];

                    ViewState["Old"] = false;
                }


                hfOldRoomID.Value = dr["Computer_Room_ID"].ToString();
                hfEmployeeID.Value = dr["Employee_ID"].ToString();
                hfExamID.Value = dr["Random_Exam_ID"].ToString();
                lblExam.Text = dr["Exam_Name"].ToString();
                lblName.Text = dr["Employee_Name"].ToString();
                lblRoom.Text = dr["Room_Name"].ToString();
                lblOldSeat.Text = dr["Computer_Room_Seat"].ToString() == "0"
                                      ? "无机位"
                                      : dr["Computer_Room_Seat"].ToString();

                string strRoomId = dr["Computer_Room_ID"].ToString();
                string strExamId = dr["Random_Exam_ID"].ToString();
                int computerNum = Convert.ToInt32(dr["Computer_Number"]);
                string strBad = dr["Bad_Seat"] == DBNull.Value ? string.Empty:dr["Bad_Seat"].ToString();

                //Random_Exam_ID=" + this.randomExamID + " and  被占用的机位应该不局限于本场考试
                if(ViewState["Old"].ToString()=="True")
                {
                    strSql = " select * from Random_Exam_Result_Detail "
                         + " where  Computer_Room_ID=" + strRoomId
                         + " and Is_Remove=0 and computer_room_seat!=0";
                }
                else
                {
                    strSql = " select * from Random_Exam_Result_Detail_Temp "
                             + " where  Computer_Room_ID=" + strRoomId
                             + " and Is_Remove=0 and computer_room_seat!=0";
                }

                DataSet ds = db.RunSqlDataSet(strSql);
                
                //被占用的机位
                foreach (DataRow drs in ds.Tables[0].Rows)
                {
                    if(strBad == string.Empty)
                    {
                        strBad = drs["Computer_Room_Seat"].ToString();
                    }
                    else
                    {
                        strBad += "," + drs["Computer_Room_Seat"];
                    }
                }

                ListItem item = new ListItem();
                item.Text = "--请选择--";
                item.Value = "0";
                ddlSeat.Items.Add(item);

                for(int i=1; i<=computerNum; i++)
                {
                    if((","+strBad+",").IndexOf(","+i+",")<0)
                    {
                        item = new ListItem();
                        item.Text = i.ToString();
                        item.Value = i.ToString();
                        ddlSeat.Items.Add(item); 
                    }
                }

                strSql = "select * from Computer_Room where Computer_Server_ID=" + dr["Computer_Server_ID"];
                DataSet dsRoom = db.RunSqlDataSet(strSql);
                foreach (DataRow drRoom in dsRoom.Tables[0].Rows)
                {
                    ListItem item1 = new ListItem();
                    item1.Text = drRoom["Computer_Room_Name"].ToString();
                    item1.Value = drRoom["Computer_Room_ID"].ToString();
                    ddlRoom.Items.Add(item1);
                }

                ddlRoom.SelectedValue = dr["Computer_Room_ID"].ToString();
            }
        }

        protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleAccess db = new OracleAccess();
            string strSql = "select * from Computer_Room where Computer_Room_ID=" + ddlRoom.SelectedValue;
            DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

            int computerNum = Convert.ToInt32(dr["Computer_Number"]);
            string strBad = dr["Bad_Seat"] == DBNull.Value ? string.Empty : dr["Bad_Seat"].ToString();

            //Random_Exam_ID=" + this.randomExamID + " and  被占用的机位应该不局限于本场考试
            if (ViewState["Old"].ToString() == "True")
            {
                strSql = " select * from Random_Exam_Result_Detail "
                         + " where  Computer_Room_ID=" + ddlRoom.SelectedValue
                         + " and Is_Remove=0 and computer_room_seat!=0";
            }
            else
            {
                strSql = " select * from Random_Exam_Result_Detail_Temp "
                         + " where  Computer_Room_ID=" + ddlRoom.SelectedValue
                         + " and Is_Remove=0 and computer_room_seat!=0";
            }

            DataSet ds = db.RunSqlDataSet(strSql);

            //被占用的机位
            foreach (DataRow drs in ds.Tables[0].Rows)
            {
                if (strBad == string.Empty)
                {
                    strBad = drs["Computer_Room_Seat"].ToString();
                }
                else
                {
                    strBad += "," + drs["Computer_Room_Seat"];
                }
            }

            ddlSeat.Items.Clear();
            ListItem item = new ListItem();
            item.Text = "--请选择--";
            item.Value = "0";
            ddlSeat.Items.Add(item);

            for (int i = 1; i <= computerNum; i++)
            {
                if (("," + strBad + ",").IndexOf("," + i + ",") < 0)
                {
                    item = new ListItem();
                    item.Text = i.ToString();
                    item.Value = i.ToString();
                    ddlSeat.Items.Add(item);
                }
            }
        }

        protected  void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                btnOK.Visible = false;

                if (ddlSeat.SelectedValue == "0")
                {
                    SessionSet.PageMessage = "请选择机位！";
                    return;
                }

                string id = Request.QueryString.Get("id");
                string strSql;

                OracleAccess db = new OracleAccess();
                if (ViewState["Old"].ToString() == "True")
                {
                    strSql = "select * from Random_Exam_Result_Detail where Random_Exam_Result_Detail_ID=" + id;
                }
                else
                {
                    strSql = "select * from Random_Exam_Result_Detail_Temp where Random_Exam_Result_Detail_ID=" + id;
                }
                DataSet ds = db.RunSqlDataSet(strSql);
                if(ds.Tables[0].Rows.Count>0)
                {
                    if(ds.Tables[0].Rows[0]["FingerPrint"] == DBNull.Value)
                    {
                        SessionSet.PageMessage = "当前学生考试不是指纹考试，无需调整机位！";
                        return;
                    }
                }

                if (hfOldRoomID.Value == ddlRoom.SelectedValue)
                {
                    if (ViewState["Old"].ToString() == "True")
                    {
                        strSql = "update Random_Exam_Result_Detail "
                                 + " set Computer_Room_Seat =" + ddlSeat.SelectedValue
                                 + " where Random_Exam_Result_Detail_ID=" + id;
                    }
                    else
                    {
                        strSql = "update Random_Exam_Result_Detail_Temp "
                                 + " set Computer_Room_Seat =" + ddlSeat.SelectedValue
                                 + " where Random_Exam_Result_Detail_ID=" + id;
                    }

                    db.ExecuteNonQuery(strSql);
                }
                else
                {
                    OracleAccess dbCenter =
                        new OracleAccess(
                            System.Configuration.ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);

                    //从旧机房的安排明细中，删除员工ID
                    string strReplace = "," + hfEmployeeID.Value + ",";
                    strSql = "update Random_Exam_Arrange_Detail "
                             + "set User_ids = substr(Replace(','||User_ids||',','" + strReplace +
                             "',','),2,length(Replace(','||User_ids||',','" + strReplace + "',','))-2) "
                             + "where  ','|| User_ids || ',' like '%" + strReplace + "%' and Random_Exam_ID=" +
                             hfExamID.Value +
                             " and Computer_Room_ID=" + hfOldRoomID.Value;
                    dbCenter.ExecuteNonQuery(strSql);

                    //查询新机房是否存在安排明细
                    strSql = " select * from  Random_Exam_Arrange_Detail "
                             +  " where Random_Exam_ID=" + hfExamID.Value + " and Computer_Room_ID=" + ddlRoom.SelectedValue;
                    DataSet dsNew = dbCenter.RunSqlDataSet(strSql);

                    if(dsNew.Tables[0].Rows.Count>0)
                    {
                        //在新机房的安排明细中，添加员工ID
                        strSql = "update Random_Exam_Arrange_Detail set User_Ids=User_Ids||'," + hfEmployeeID.Value + "'"
                                 + " where Random_Exam_ID=" + hfExamID.Value + " and Computer_Room_ID=" + ddlRoom.SelectedValue;
                        dbCenter.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        RandomExamArrangeBLL arrangeBll = new RandomExamArrangeBLL();
                        RandomExamArrange randomExamArrange = arrangeBll.GetRandomExamArranges(Convert.ToInt32(hfExamID.Value))[0];

                        OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                        OracleParameter para2 = new OracleParameter("p_random_exam_arrange_de_id", OracleType.Number);
                        para2.Direction = ParameterDirection.Output;
                        OracleParameter para3 = new OracleParameter("p_random_exam_arrange_Id", OracleType.Number);
                        para3.Value = randomExamArrange.RandomExamArrangeId;
                        OracleParameter para4 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
                        para4.Value = Convert.ToInt32(hfExamID.Value);
                        OracleParameter para5 = new OracleParameter("p_computer_room_id", OracleType.Number);
                        para5.Value = Convert.ToInt32(ddlRoom.SelectedValue);


                        IDataParameter[] paras = new IDataParameter[] { para1, para2, para3, para4, para5 };
                        Pub.RunAddProcedure(true, "USP_RANDOM_EXAM_ARRANGE_DE_I", paras,
                                            System.Text.Encoding.Unicode.GetBytes(hfEmployeeID.Value));
                    }


                    //下载安排明细

                    strSql = "begin dbms_mview.refresh('Random_Exam_Arrange_Detail','?'); end;";
                    db.ExecuteNonQuery(strSql);

                    if (ViewState["Old"].ToString() == "True")
                    {
                        strSql = "update Random_Exam_Result_Detail "
                                 + " set Computer_Room_ID=" + ddlRoom.SelectedValue + " , Computer_Room_Seat =" +
                                 ddlSeat.SelectedValue
                                 + " where Random_Exam_Result_Detail_ID=" + id;
                    }
                    else
                    {
                        strSql = "update Random_Exam_Result_Detail_Temp "
                                 + " set Computer_Room_ID=" + ddlRoom.SelectedValue + " , Computer_Room_Seat =" +
                                 ddlSeat.SelectedValue
                                 + " where Random_Exam_Result_Detail_ID=" + id;
                    }
                    db.ExecuteNonQuery(strSql);
                }

                Response.Write("<script>window.returnValue = 'true',window.close();</script>");
            }
            catch (Exception)
            {
                btnOK.Visible = true;
                SessionSet.PageMessage = "调整失败！";
                return;
            }
        }
    }
}
