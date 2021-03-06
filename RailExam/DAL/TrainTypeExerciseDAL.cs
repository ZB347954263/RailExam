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
    public class TrainTypeExerciseDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainTypeExerciseDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("traintypeid", "TRAIN_TYPE_ID");
            _ormTable.Add("paperid", "PAPER_ID");
        }

        public void AddTrainTypeExercise(TrainTypeExercise obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_EXERCISE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, obj.TrainTypeID);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, obj.PaperID);

            db.ExecuteNonQuery(dbCommand);
        }

        public void UpdateTrainTypeExercise(TrainTypeExercise obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_EXERCISE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, obj.TrainTypeID);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, obj.PaperID);

            db.ExecuteNonQuery(dbCommand);
        }

        public void DelTrainTypeExercise(int trainTypeID,int paperID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_EXERCISE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, paperID);

            db.ExecuteNonQuery(dbCommand);
        }

        public IList<TrainTypeExercise> GetTrainTypeExerciseByTrainTypeID(int trainTypeID)
        {
            IList<TrainTypeExercise> objList = new List<TrainTypeExercise>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_EXERCISE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainTypeExercise obj = CreateModelObject(dataReader);

                    objList.Add(obj);
                }
            }

            return objList;
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

        public static TrainTypeExercise CreateModelObject(IDataReader dataReader)
        {
            return new TrainTypeExercise(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainTypeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperID")]),
                new Paper(DataConvert.ToInt(dataReader[PaperDAL.GetMappingFieldName("PaperId")]),
                DataConvert.ToInt(dataReader[PaperDAL.GetMappingFieldName("OrgId")]),
                DataConvert.ToInt(dataReader[PaperDAL.GetMappingFieldName("CategoryId")]),
                 DataConvert.ToString(dataReader[PaperDAL.GetMappingFieldName("PaperName")]),
                DataConvert.ToInt(dataReader[PaperDAL.GetMappingFieldName("CreateMode")]),
                DataConvert.ToInt(dataReader[PaperDAL.GetMappingFieldName("StrategyId")]),
                DataConvert.ToString(dataReader[PaperDAL.GetMappingFieldName("CategoryName")]),
                DataConvert.ToString(dataReader[PaperDAL.GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[PaperDAL.GetMappingFieldName("Memo")]),
                DataConvert.ToDecimal(dataReader[PaperDAL.GetMappingFieldName("TotalScore")]),
                DataConvert.ToInt(dataReader[PaperDAL.GetMappingFieldName("ItemCount")]),
                DataConvert.ToInt(dataReader[PaperDAL.GetMappingFieldName("UsedCount")]),
                DataConvert.ToInt(dataReader[PaperDAL.GetMappingFieldName("StatusID")]),
                DataConvert.ToString(dataReader[PaperDAL.GetMappingFieldName("createperson")]),
                DateTime.Parse(dataReader[PaperDAL.GetMappingFieldName("createtime")].ToString())));
        }
    }
}
