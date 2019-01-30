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
    public class TrainPlanDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainPlanDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("trainplanid","TRAIN_PLAN_ID");
            _ormTable.Add("trainname", "TRAIN_NAME");
            _ormTable.Add("traincontent", "TRAIN_CONTENT");
            _ormTable.Add("begindate", "BEGIN_DATE");
            _ormTable.Add("enddate", "END_DATE");
            _ormTable.Add("hasexam","HAS_EXAM");
            _ormTable.Add("examform", "EXAM_FORM");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("statusname","STATUS_NAME");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 新增培训课程
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainPlan(TrainPlan obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_train_plan_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_train_name", DbType.String, obj.TrainName);
            db.AddInParameter(dbCommand, "p_train_content", DbType.String, obj.TrainContent );
            db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, obj.BeginDate);
            db.AddInParameter(dbCommand, "p_end_date", DbType.Date, obj.EndDate);
            db.AddInParameter(dbCommand, "p_has_exam", DbType.Int32, obj.HasExam ? 1:0 );
            db.AddInParameter(dbCommand, "p_exam_form", DbType.String, obj.ExamForm);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, obj.StatusID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);

            obj.TrainPlanID = (int) db.GetParameterValue(dbCommand, "p_train_plan_id");
        }

        /// <summary>
        /// 删除培训课程
        /// </summary>
        /// <param name="trainPlanID"></param>
        public void DeleteTrainPlan(int trainPlanID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, trainPlanID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新培训课程
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainPlan(TrainPlan obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, obj.TrainPlanID);
            db.AddInParameter(dbCommand, "p_train_name", DbType.String, obj.TrainName);
            db.AddInParameter(dbCommand, "p_train_content", DbType.String, obj.TrainContent);
            db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, obj.BeginDate);
            db.AddInParameter(dbCommand, "p_end_date", DbType.Date, obj.EndDate);
            db.AddInParameter(dbCommand, "p_has_exam", DbType.Int32, obj.HasExam ? 1 : 0);
            db.AddInParameter(dbCommand, "p_exam_form", DbType.String, obj.ExamForm);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, obj.StatusID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);
            db.ExecuteNonQuery(dbCommand);
        }


        public IList<TrainPlan> GetTrainPlanInfo(int trainPlanID,
                                                    string trainName,
                                                    string trainContent,
                                                    DateTime beginDate,
                                                    DateTime endDate,
                                                    bool hasExam,
                                                    string examForm,
                                                    int statusID,
                                                    string memo,
                                                    int startRowIndex,
                                                    int maximumRows,
                                                    string orderBy)
        {
            IList<TrainPlan> objPlanList = new List<TrainPlan>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainPlan obj = CreateModelObject(dataReader);

                    objPlanList.Add(obj);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return objPlanList;
        }

        public IList<TrainPlan> GetAllTrainPlanInfo()
        {
            IList<TrainPlan> objPlanList = new List<TrainPlan>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainPlan obj = CreateModelObject(dataReader);

                    objPlanList.Add(obj);
                }
            }
            return objPlanList;
        }

        public TrainPlan GetTrainPlanInfo(int trainPlanID)
        {
            TrainPlan obj = new TrainPlan();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, trainPlanID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    obj = CreateModelObject(dataReader);
                }
            }

            return obj;
        }

        /// <summary>
        /// 查询结果记录数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
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
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' + orderBysOfOneCondition[1];
                    }
                }
            }

            return mappingOrderBy;
        }

        public static TrainPlan CreateModelObject(IDataReader dataReader)
        {
            return new TrainPlan(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainPlanID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TrainName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TrainContent")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginDate")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("EndDate")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("HasExam")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamForm")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StatusID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
