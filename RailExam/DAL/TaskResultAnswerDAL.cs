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
    public class TaskResultAnswerDAL
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

        public static TaskResultAnswer CreateModelObject(IDataReader dataReader)
       {
           return new TaskResultAnswer(
               DataConvert.ToInt(dataReader[GetMappingFieldName("TaskResultId")]),
             

               DataConvert.ToInt(dataReader[GetMappingFieldName("PaperItemId")]),

               DataConvert.ToString(dataReader[GetMappingFieldName("Answer")]),

               DataConvert.ToInt(dataReader[GetMappingFieldName("taskTime")]),

               DataConvert.ToInt(dataReader[GetMappingFieldName("judgeScore")]),    
               DataConvert.ToInt(dataReader[GetMappingFieldName("judgeStatusId")]),
               DataConvert.ToString(dataReader[GetMappingFieldName("JudgeRemark")])    );
       }

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public TaskResultAnswerDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("taskresultid", "Task_Result_Id");
            _ormTable.Add("paperitemid", "PAPER_ITEM_ID");
            _ormTable.Add("answer", "ANSWER");
            _ormTable.Add("tasktime", "TASK_TIME");
            _ormTable.Add("judgescore", "JUDGE_SCORE");
            _ormTable.Add("judgestatusid", "JUDGE_STATUS_ID");
            _ormTable.Add("judgeremark", "JUDGE_REMARK"); 
           
        }

        /// <summary>
        /// 查询给定作业的结果答案
        /// </summary>
        /// <param name="examResultId"></param>
        /// <returns></returns>
        public IList<TaskResultAnswer> GetTaskResultAnswers(int examResultId)
        {
            IList<TaskResultAnswer> examResultAnswers = new List<TaskResultAnswer>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_task_RESULT_ANSWER_q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Task_Result_Id", DbType.Int32, examResultId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResultAnswers.Add(CreateModelObject(dataReader));
                }
            }

            return examResultAnswers;
        }
    }
}
