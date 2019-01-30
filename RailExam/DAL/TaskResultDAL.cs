using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
   public class TaskResultDAL
    {
       private static Hashtable _ormTable;
       private int _recordCount = 0;

       public int RecordCount
       {
           get { return _recordCount; }
       }

       public static string GetMappingFieldName(string propertyName)
       {
           return (string)_ormTable[propertyName.ToLower()];
       }

       public static string GetMappingOrderBy(string orderBy)
       {
           orderBy = orderBy.Trim();

           if (string.IsNullOrEmpty(orderBy))
           {
               return string.Empty;
           }

           string mappingOrderBy = string.Empty;
           string[] orderByConditions = orderBy.Split(new char[] { ',' });

           foreach (string s in orderByConditions)
           {
               string orderByCondition = s.Trim();

               string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ' ' });

               if (orderBysOfOneCondition.Length == 0)
               {
                   continue;
               }
               else
               {
                   if (mappingOrderBy != string.Empty)
                   {
                       mappingOrderBy += ',';
                   }

                   if (orderBysOfOneCondition.Length == 1)
                   {
                       mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]);
                   }
                   else
                   {
                       mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' +
                                         orderBysOfOneCondition[1];
                   }
               }
           }

           return mappingOrderBy;
       }

       public static TaskResult CreateModelObject(IDataReader dataReader)
       {
           return new TaskResult(
               DataConvert.ToInt(dataReader[GetMappingFieldName("TaskResultId")]),
               DataConvert.ToInt(dataReader[GetMappingFieldName("TrainTypeId")]),
         
               DataConvert.ToInt(dataReader[GetMappingFieldName("PaperId")]),
              
               DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeId")]),
              
               DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginTime")]),
               DataConvert.ToDateTime(dataReader[GetMappingFieldName("CurrentTime")]),
               DataConvert.ToDateTime(dataReader[GetMappingFieldName("EndTime")]),
               DataConvert.ToInt(dataReader[GetMappingFieldName("UsedTime")]),
               DataConvert.ToDecimal(dataReader[GetMappingFieldName("AutoScore")]),
               DataConvert.ToDecimal(dataReader[GetMappingFieldName("Score")]),
               DataConvert.ToInt(dataReader[GetMappingFieldName("JudgeId")]),

               DataConvert.ToDateTime(dataReader[GetMappingFieldName("JudgeBeginTime")]),
               DataConvert.ToDateTime(dataReader[GetMappingFieldName("JudgeEndTime")]),
               DataConvert.ToDecimal(dataReader[GetMappingFieldName("CorrectRate")]),
               DataConvert.ToBool(dataReader[GetMappingFieldName("IsPass")]),
               DataConvert.ToInt(dataReader[GetMappingFieldName("StatusId")]),               
               DataConvert.ToString(dataReader[GetMappingFieldName("Memo")])
               );
       }

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public TaskResultDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("taskresultid", "Task_Result_Id");
            _ormTable.Add("traintypeid", "Train_Type_Id"); 
            _ormTable.Add("paperid", "PAPER_ID");           
            _ormTable.Add("employeeid", "Employee_Id");           
            _ormTable.Add("begintime", "BEGIN_TIME");
            _ormTable.Add("currenttime", "CURRENT_TIME");
            _ormTable.Add("endtime", "END_TIME");
            _ormTable.Add("usedtime", "USED_TIME");
            _ormTable.Add("autoscore", "AUTO_SCORE");
            _ormTable.Add("score", "SCORE");
            _ormTable.Add("judgeid", "JUDGE_ID");           
            _ormTable.Add("judgebegintime", "JUDGE_BEGIN_TIME");
            _ormTable.Add("judgeendtime", "JUDGE_END_TIME");
            _ormTable.Add("correctrate", "CORRECT_RATE");
            _ormTable.Add("ispass", "IS_PASS");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("memo", "MEMO");

            _ormTable.Add("traintypename", "TRAIN_TYPE_NAME");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
            _ormTable.Add("organizationname", "ORGANIZATION_NAME");
            _ormTable.Add("judgename", "JUDGE_NAME");
            _ormTable.Add("papername", "PAPER_NAME");
            _ormTable.Add("statusname", "STATUS_NAME");
        }

       public TaskResult GetTaskResult(int taskResultId)
       {
           TaskResult taskResult = null;

           Database db = DatabaseFactory.CreateDatabase();

           string sqlCommand = "USP_TASK_RESULT_G";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "p_task_result_id", DbType.Int32, taskResultId);

           using (IDataReader dataReader = db.ExecuteReader(dbCommand))
           {
               while (dataReader.Read())
               {
                   taskResult = CreateModelObject(dataReader);
                   taskResult.TrainTypeName =
                       Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("TrainTypeName")]));
                   taskResult.EmployeeName =
                       Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]));
                   taskResult.OrganizationName =
                       Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("OrganizationName")]));
                   taskResult.PaperName =
                       Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("PaperName")]));
                   taskResult.JudgeName =
                       Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("JudgeName")]));
                   taskResult.StatusName =
                       Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]));

                   break;
               }
           }

           return taskResult;
       }

       /// <summary>
       /// 取给定考生ID、试卷id、考试id的考生考试结果
       /// </summary>
       /// <returns></returns>
       public TaskResult GetTaskResult(int paperID, int trainTypeId, int employeeId)
       {
           TaskResult taskResult = null;

           Database db = DatabaseFactory.CreateDatabase();

           string sqlCommand = "USP_Task_RESULT_Q";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "p_TRAIN_TYPE_ID", DbType.Int32, trainTypeId);
           db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, paperID);
           db.AddInParameter(dbCommand, "p_EMPLOYEE_ID", DbType.Int32, employeeId);

           using (IDataReader dataReader = db.ExecuteReader(dbCommand))
           {
               while (dataReader.Read())
               {
                   taskResult = CreateModelObject(dataReader);
                   break;
               }
           }

           return taskResult;
       }

       /// <summary>
       /// 按指定的查询条件取作业人的作业结果
       /// </summary>
       /// <returns>符合条件的作业结果</returns>
       public IList<TaskResult> GetTaskResults(int organizationId, int paperID, int trainTypeId, string employeeName,
           decimal scoreLower, decimal scoreUpper, int statusId, int currentPageIndex, int pageSize, string orderBy)
       {
           IList<TaskResult> results = new List<TaskResult>();
           Database db = DatabaseFactory.CreateDatabase();
           DbCommand dbCommand = db.GetStoredProcCommand("USP_TASK_RESULT_S");

           db.AddInParameter(dbCommand, "p_organization_id", DbType.Int32, organizationId);
           db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, paperID);
           db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeId);
           db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
           db.AddInParameter(dbCommand, "p_score_lower", DbType.Decimal, scoreLower);
           db.AddInParameter(dbCommand, "p_score_upper", DbType.Decimal, scoreUpper);
           db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, statusId);
           db.AddInParameter(dbCommand, "p_current_page_index", DbType.Int32, currentPageIndex);
           db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, pageSize);
           db.AddInParameter(dbCommand, "P_order_by", DbType.String, GetMappingOrderBy(orderBy));
           db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

           using (IDataReader dataReader = db.ExecuteReader(dbCommand))
           {
               TaskResult result = null;

               while (dataReader.Read())
               {
                   result = CreateModelObject(dataReader);
                   result.EmployeeName = Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]));
                   result.OrganizationName = Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("OrganizationName")]));
                   result.JudgeName = Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("JudgeName")]));
                   result.PaperName = Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("PaperName")]));
                   result.StatusName = Convert.ToString(DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]));

                   results.Add(result);
               }
           }

           _recordCount = (int)db.GetParameterValue(dbCommand, "p_count");

           return results;
       }

       public int AddTaskResult(TaskResult taskResult, string[] strAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Task_RESULT_I");

            db.AddOutParameter(dbCommand, "p_Task_result_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_Train_Type_Id", DbType.Int32, taskResult.TrainTypeId);            
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, taskResult.PaperId);
            db.AddInParameter(dbCommand, "p_Employee_Id", DbType.Int32, taskResult.EmployeeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, taskResult.BeginTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, taskResult.CurrentTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, taskResult.EndTime);
            db.AddInParameter(dbCommand, "p_Used_time", DbType.Int32, taskResult.UsedTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, taskResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, taskResult.Score);
            db.AddInParameter(dbCommand, "p_judge_id", DbType.Int32, taskResult.JudgeId);
            db.AddInParameter(dbCommand, "p_judge_begin_time", DbType.DateTime, taskResult.JudgeBeginTime);
            db.AddInParameter(dbCommand, "p_judge_end_time", DbType.DateTime, taskResult.JudgeEndTime);
            db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, taskResult.CorrectRate);
            db.AddInParameter(dbCommand, "p_is_pass", DbType.Int32,0);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, taskResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, taskResult.Memo);

            int id = 0;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Task_result_id"));

                for (int n = 0; n < strAnswers.Length; n++)
                {
                    string str2 = strAnswers[n].ToString();
                    string[] str3 = str2.Split(new char[] { '|' });
                    string strPaperItemId = str3[0].ToString();

                    string strTrueAnswer = str2.ToString().Substring(strPaperItemId.Length + 1);

                    string sqlCommand1 = "USP_Task_RESULT_ANSWER_NS_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand1);

                    db.AddInParameter(dbCommand, "p_Task_result_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, int.Parse(strPaperItemId));
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, strTrueAnswer);
                    db.AddInParameter(dbCommand, "p_Task_time", DbType.Int32, 0);
                    db.AddOutParameter(dbCommand, "p_judge_score", DbType.Decimal, 4);
                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, 1);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, "");
                    db.ExecuteNonQuery(dbCommand, transaction);

                }

                string sqlCommand2 = "USP_Task_RESULT_Update";
                DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand2);

                db.AddInParameter(dbCommand2, "p_Task_result_id", DbType.Int32, id);               
                db.AddInParameter(dbCommand2, "p_paper_id", DbType.Int32, taskResult.PaperId);
               
                db.ExecuteNonQuery(dbCommand2, transaction); 

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;

            }
            connection.Close();

            return id;
        }

        public int UpdateJudgeBeginTime(int taskResultId, DateTime dateTime)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_TASK_RESULT_JUDGE_TIME_U");

            db.AddInParameter(dbCommand, "p_task_result_id", DbType.Int32, taskResultId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, dateTime);

            return db.ExecuteNonQuery(dbCommand);
        }

        public int UpdateTaskResultAnswers(int taskResultId, IList<TaskResultAnswer> answers)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            int nRecordAffected = 0;

            try
            {
                decimal totalJudgeScore = 0.0M;
                DbCommand dbCommand;

                foreach (TaskResultAnswer answer in answers)
                {
                    dbCommand = db.GetStoredProcCommand("USP_TASK_RESULT_ANSWER_SSR_U");

                    db.AddInParameter(dbCommand, "p_task_result_id", DbType.Int32, taskResultId);
                    db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, answer.PaperItemId);
                    db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, answer.JudgeScore);

                    totalJudgeScore += answer.JudgeScore;

                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, answer.JudgeStatusId);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, answer.JudgeRemark);
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                dbCommand = db.GetStoredProcCommand("USP_TASK_RESULT_JUDGE_SCORE_U");
                db.AddInParameter(dbCommand, "p_task_result_id", DbType.Int32, taskResultId);
                db.AddInParameter(dbCommand, "p_score", DbType.Decimal, totalJudgeScore);

                nRecordAffected += db.ExecuteNonQuery(dbCommand);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return nRecordAffected;
        }

        public int UpdateTaskResultAndItsAnswers(TaskResult taskResult, IList<TaskResultAnswer> taskResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_TASK_RESULT_U");

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, taskResult.TaskResultId);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, taskResult.TrainTypeId);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, taskResult.PaperId);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, taskResult.EmployeeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, taskResult.BeginTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, taskResult.CurrentTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, taskResult.EndTime);
            db.AddInParameter(dbCommand, "p_used_time", DbType.Int32, taskResult.UsedTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, taskResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, taskResult.Score);
            db.AddInParameter(dbCommand, "p_judge_id", DbType.Int32, taskResult.JudgeId);
            db.AddInParameter(dbCommand, "p_judge_begin_time", DbType.DateTime, taskResult.JudgeBeginTime);
            db.AddInParameter(dbCommand, "p_judge_end_time", DbType.DateTime, taskResult.JudgeEndTime);
            db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, taskResult.CorrectRate);
            db.AddInParameter(dbCommand, "p_is_pass", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, taskResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, taskResult.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            int nRecordAffected = 0;

            try
            {
                nRecordAffected += db.ExecuteNonQuery(dbCommand, transaction);

                foreach (TaskResultAnswer answer in taskResultAnswers)
                {
                    dbCommand = db.GetStoredProcCommand("USP_TASK_RESULT_ANSWER_U");

                    db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, answer.TaskResultId);
                    db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, answer.PaperItemId);
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, answer.Answer);
                    db.AddInParameter(dbCommand, "p_task_time", DbType.Int32, answer.TaskTime);
                    db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, answer.JudgeScore);
                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, answer.JudgeStatusId);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, answer.JudgeRemark);

                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return nRecordAffected;
        }

        public int UpdateTaskResult(TaskResult taskResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_TASK_RESULT_U");

            db.AddInParameter(dbCommand, "p_task_result_id", DbType.Int32, taskResult.TaskResultId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, taskResult.BeginTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, taskResult.EndTime);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, taskResult.Score);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, taskResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, taskResult.Memo);

            return db.ExecuteNonQuery(dbCommand);
        }
    }
}
