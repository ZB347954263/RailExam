using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
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
using RailExamWebApp.Common.Class;
using System.Text;

namespace RailExamWebApp.RandomExam
{
    public partial class UpdateEmployee : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Get("type") == "exam")
                {
                    updateExam();
                }
            }

        }

        private void updateExam()
        {
            // 根据 ProgressBar.htm 显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);
            string jsBlock;

            try
            {
                string strId = Request.QueryString.Get("RandomExamID");
                RandomExamBLL examBll = new RandomExamBLL();
                RailExam.Model.RandomExam exam = examBll.GetExam(Convert.ToInt32(strId));

                IList<RandomExamResult> examResults = null;
                RandomExamResultBLL bllExamResult = new RandomExamResultBLL();
                examResults = bllExamResult.GetRandomExamResults(int.Parse(strId), "", "", "", "", 0, 1000, Convert.ToInt32(Request.QueryString.Get("OrgID")));
                string strChooseID = string.Empty;
                foreach (RandomExamResult randomExamResult in examResults)
                {
                    if (string.IsNullOrEmpty(strChooseID))
                    {
                        strChooseID += randomExamResult.ExamineeId;
                    }
                    else
                    {
                        strChooseID += "," + randomExamResult.ExamineeId;
                    }
                }

                OracleAccess access = new OracleAccess();
                string strSql =
                    @"select ER.Begin_Time,E.Is_Computerexam,ER.Examinee_ID,
                        case when z.computer_room_seat=0 then GetOrgName(ER.org_ID)||CR.Computer_Room_Name||'微机教室'
                        else GetOrgName(ER.org_ID)||CR.Computer_Room_Name||'微机教室-'||z.computer_room_seat||'机位' end Computer_Room_Name, 
                        case when y.subject_name is not null then y.subject_name else
                        case when  MT.RANDOM_EXAM_MODULAR_TYPE_NAME is not null then (MT.RANDOM_EXAM_MODULAR_TYPE_NAME||'-'|| E.EXAM_NAME) 
                        else E.EXAM_NAME end  end as subject, 
                        ER.Score,ER.Is_Pass,E.RANDOM_EXAM_ID 
                        from random_exam_result ER 
                        inner join random_exam_result_detail z on ER.Random_Exam_Result_Id=z.Random_Exam_Result_Id and z.is_remove=1
                        left join random_exam E on E.RANDOM_EXAM_ID=ER.RANDOM_EXAM_ID 
                        left join random_exam_train_class x on E.Random_Exam_ID=x.Random_Exam_ID
                        left join Zj_Train_Class_Subject y on x.train_class_subject_id=y.train_class_subject_id
                        left join computer_room CR on CR.COMPUTER_ROOM_ID=z.Computer_Room_Id 
                        left join random_exam_modular_type MT on MT.RANDOM_EXAM_MODULAR_TYPE_ID=E.Random_Exam_Modular_Type_Id
                        Inner join (
                        select b.*,min(a.end_time) end_time from Random_Exam_Result a
                        inner join
                       (select distinct  max(a.score) score, a.examinee_id, a.Random_Exam_Id
                        from Random_EXAM_RESULT a where  a.status_id > 0.
                        group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                        and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                        group by  b.examinee_id, b.Random_Exam_Id,b.score ) F on ER.examinee_id = f.examinee_id
                        and ER.score = f.score  and ER.Random_Exam_Id = f.Random_Exam_Id
                        and ER.end_time=f.end_time 
                        where ER.Random_Exam_ID=" + strId ;



                DataTable dtSel = access.RunSqlDataSet(strSql).Tables[0];
                if (dtSel != null && dtSel.Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow r in dtSel.Rows)
                    {
                        if (("," + strChooseID + ",").IndexOf("," + r["Examinee_ID"] + ",") < 0)
                        {
                            System.Threading.Thread.Sleep(10);
                            jsBlock = "<script>SetPorgressBar('更新考生考试档案','" + ((i * 100) / ((double)dtSel.Rows.Count)).ToString("0.00") + "'); </script>";
                            Response.Write(jsBlock);
                            Response.Flush();

                            i++;
                            continue;
                        }
                        access.ExecuteNonQuery(" delete from zj_employee_exam where Random_Exam_ID=" + strId +
                                               " and  employee_id=" + r["Examinee_ID"]);

                        string exam_date = r["Begin_Time"].ToString();
                        int exam_style = Convert.ToInt32(r["Is_Computerexam"]);
                        string exam_address = r["Computer_Room_Name"].ToString();
                        string exam_subject = r["subject"].ToString();
                        double exam_score = Convert.ToDouble(r["Score"]);
                        int exam_result = Convert.ToInt32(r["Is_Pass"]);
                        int random_exam_id = Convert.ToInt32(r["RANDOM_EXAM_ID"]);

                        StringBuilder sqlInsert = new StringBuilder();
                        sqlInsert.Append(
                            "insert into zj_employee_exam(employee_exam_id,employee_id,exam_date,exam_style,exam_address,");
                        sqlInsert.Append("exam_subject,exam_score ,exam_result,create_date,create_person,random_exam_id");
                        sqlInsert.Append(")  values(employee_exam_seq.nextval,{0},to_date('{1}','yyyy-mm-dd hh24:mi:ss'),{2},");
                        sqlInsert.Append(" '{3}','{4}','{5}',{6},to_date('{7}','yyyy-mm-dd hh24:mi:ss'),'{8}',{9})");
                        string sqlIns = string.Format(sqlInsert.ToString(), r["Examinee_ID"], exam_date, exam_style, exam_address,
                                                      exam_subject, exam_score, exam_result,DateTime.Now, PrjPub.CurrentLoginUser.EmployeeName,
                                                      random_exam_id);

                        access.ExecuteNonQuery(sqlIns);

                        System.Threading.Thread.Sleep(10);
                        jsBlock = "<script>SetPorgressBar('更新考生考试档案','" + ((i * 100) / ((double)dtSel.Rows.Count)).ToString("0.00") + "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();

                        i++;
                    }
                }

                if(exam.HasTrainClass)
                {
                    jsBlock = string.Empty;

                    RandomExamTrainClassBLL objBll=new RandomExamTrainClassBLL();
                    IList<RandomExamTrainClass> objList =
                        objBll.GetRandomExamTrainClassByRandomExamID(Convert.ToInt32(strId));

                    string strClassID = "";
                    foreach(RandomExamTrainClass trainClass in objList)
                    {
                        strClassID += (strClassID == string.Empty)
                                          ? trainClass.TrainClassID.ToString()
                                          : "," + trainClass.TrainClassID;
                    }

                    StringBuilder sqlClass = new StringBuilder();
                    sqlClass.Append(" select C.TRAIN_CLASS_ID,begin_date,end_date,TP.TRAIN_PLAN_TYPE_ID,TP.Location,C.Employee_ID ");
                    sqlClass.Append(" from zj_train_class TC right join (select  train_plan_id, train_class_id,Employee_ID from zj_train_plan_employee");
                    sqlClass.AppendFormat(" where train_class_id in ({0}) ", strClassID);
                    sqlClass.Append("  ) C on C.TRAIN_CLASS_ID=TC.TRAIN_CLASS_ID ");
                    sqlClass.Append(" left join zj_train_plan TP on TP.TRAIN_PLAN_ID=C.TRAIN_PLAN_ID");
                    access = new OracleAccess();
                    DataTable dtClass = access.RunSqlDataSet(sqlClass.ToString()).Tables[0];
                    if (dtClass != null && dtClass.Rows.Count > 0)
                    {
                        int i = 1;
                        foreach (DataRow r in dtClass.Rows)
                        {
                            if (("," + strChooseID + ",").IndexOf("," + r["Employee_ID"] + ",") < 0)
                            {
                                System.Threading.Thread.Sleep(10);
                                jsBlock = "<script>SetPorgressBar('更新考生培训档案','" + ((i * 100) / ((double)dtClass.Rows.Count)).ToString("0.00") + "'); </script>";
                                Response.Write(jsBlock);
                                Response.Flush();

                                i++;
                                continue;
                            }

                            access.ExecuteNonQuery(" delete from zj_employee_train where Train_class_ID in (" + strClassID + ") and  employee_id=" + r["Employee_ID"]);

                            int trainClassID = Convert.ToInt32(r["TRAIN_CLASS_ID"]);
                            string beginDate = r["begin_date"].ToString();
                            string endDate = r["end_date"].ToString();
                            int trainPlanTypeID = Convert.ToInt32(r["TRAIN_PLAN_TYPE_ID"]);
                            string location = r["Location"].ToString();
                            int hour = 0;
                            string classSubject = string.Empty;

                            DataTable dtSubject =
                                access.RunSqlDataSet(
                                    "select subject_name,class_hour from zj_train_class_subject where train_class_id=" +
                                    trainClassID).Tables[0];
                            if (dtSubject != null && dtSubject.Rows.Count > 0)
                            {
                                List<string> lst = new List<string>();
                                foreach (DataRow rhour in dtSubject.Rows)
                                {
                                    lst.Add(rhour["subject_name"].ToString());
                                    if (rhour["class_hour"].ToString() != "")
                                        hour += Convert.ToInt32(rhour["class_hour"]);
                                }
                                classSubject = string.Join(",", lst.ToArray());
                            }

                            StringBuilder sqlInsert = new StringBuilder();
                            sqlInsert.Append("insert into zj_employee_train values(EMPLOYEE_TRAIN_SEQ.NEXTVAL,");
                            sqlInsert.Append("{0},{1},to_date('{2}','yyyy-mm-dd hh24:mi:ss'),to_date('{3}','yyyy-mm-dd hh24:mi:ss'),");
                            sqlInsert.Append("{4},'{5}',{6},'{7}',to_date('{8}','yyyy-mm-dd hh24:mi:ss'),'{9}')");
                            string sqlIns = string.Format(sqlInsert.ToString(), r["Employee_ID"], trainClassID, beginDate, endDate,
                                                          trainPlanTypeID, location, hour, classSubject, DateTime.Now,PrjPub.CurrentLoginUser.EmployeeName);

                            access.ExecuteNonQuery(sqlIns);

                            jsBlock = "<script>SetPorgressBar('更新考生培训档案','" + ((i * 100) / ((double)dtClass.Rows.Count)).ToString("0.00") + "'); </script>";
                            Response.Write(jsBlock);
                            Response.Flush();

                            i++;
                        }
                    }  
                }

                Response.Write("<script>top.returnValue='true';top.close();</script>");
            }
            catch (Exception)
            {
                Response.Write("<script>top.returnValue='false';top.close();</script>");
            }
        }
    }
}
