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
    public class TrainPlanStatusDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainPlanStatusDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("trainplanstatusid","TRAIN_PLAN_STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("memo", "MEMO");
        }

        public IList<TrainPlanStatus> GetAllTrainPlanStatusInfo()
        {
            IList<TrainPlanStatus> objPlanList = new List<TrainPlanStatus>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_STATUS_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_status_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainPlanStatus obj = CreateModelObject(dataReader);

                    objPlanList.Add(obj);
                }
            }
            return objPlanList;
        }

        public TrainPlanStatus GetTrainPlanStatusInfo(int trainPlanStatusID)
        {
            TrainPlanStatus obj = new TrainPlanStatus();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_STATUS_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_status_id", DbType.Int32, trainPlanStatusID);

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

        public static TrainPlanStatus CreateModelObject(IDataReader dataReader)
        {
            return new TrainPlanStatus(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainPlanStatusID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
