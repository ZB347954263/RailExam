using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class RandomExamStrategyDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static RandomExamStrategyDAL()
        {
            _ormTable = new Hashtable();
            _ormTable.Add("rangetype", "range_type");
            _ormTable.Add("randomexamstrategyid", "Random_Exam_Strategy_Id");
            _ormTable.Add("subjectid", "Subject_Id");
            _ormTable.Add("rangeid", "Range_Id");
            _ormTable.Add("itemtypeid", "Item_Type_Id");
            _ormTable.Add("itemtypename", "Type_Name");
            _ormTable.Add("rangename", "range_name");
            _ormTable.Add("memo", "memo");
            _ormTable.Add("excludechapterid", "Exclude_Chapters_Id");
            _ormTable.Add("itemcount", "Item_Count");  
			_ormTable.Add("itemdifficultyid","Item_Difficulty_ID");
			_ormTable.Add("itemdifficultyname", "Item_Difficulty_Name");
            _ormTable.Add("maxitemdifficultyid", "MAX_Item_Difficulty_ID");
            _ormTable.Add("maxitemdifficultyname", "MAX_Item_Difficulty_Name");
			_ormTable.Add("subjectname","Subject_Name");
            _ormTable.Add("ismotheritem","IS_MOTHER_ITEM");
            _ormTable.Add("motherid", "MOTHER_ID");
            _ormTable.Add("totalitemcount", "TOTAL_ITEM_COUNT");
            _ormTable.Add("selectcount", "SELECT_COUNT");
        }

        public void AddRandomExamStrategy(RandomExamStrategy item)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            int nRecordAffected = 0;
            try
            {
                string sqlCommand = "USP_Random_Exam_Strategy_I";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                //db.AddOutParameter(dbCommand, "p_Random_Exam_Strategy_Id", DbType.Int32, 4);
                db.AddInParameter(dbCommand, "p_Random_Exam_Strategy_Id", DbType.Int32, item.RandomExamStrategyId);
                db.AddInParameter(dbCommand, "p_subject_id", DbType.Int32, item.SubjectId);
                db.AddInParameter(dbCommand, "p_Range_type", DbType.Int32, item.RangeType);
                db.AddInParameter(dbCommand, "p_Range_id", DbType.Int32, item.RangeId);
                db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, item.ItemTypeId);
                db.AddInParameter(dbCommand, "p_range_name", DbType.String, item.RangeName);
                db.AddInParameter(dbCommand, "p_exclude_chapters_id", DbType.String, item.ExcludeChapterId);
                db.AddInParameter(dbCommand, "p_Item_Count", DbType.Int32, item.ItemCount);
				db.AddInParameter(dbCommand, "p_Item_diff", DbType.Int32, item.ItemDifficultyID);
                db.AddInParameter(dbCommand, "p_is_mother_item", DbType.Int32, item.IsMotherItem?1:0);
                db.AddInParameter(dbCommand, "p_mother_id", DbType.Int32, item.MotherID);
                db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);

				db.ExecuteNonQuery(dbCommand, transaction);

                //nRecordAffected = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Random_Exam_Strategy_Id"));

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }

            connection.Close();

            //return nRecordAffected;
        }

        public int UpdateRandomExamStrategy(RandomExamStrategy item)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            int nRecordAffected = 0;
            try
            {
                string sqlCommand = "USP_Random_Exam_Strategy_U";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "p_Random_Exam_Strategy_Id", DbType.Int32, item.RandomExamStrategyId);
                db.AddInParameter(dbCommand, "p_subject_id", DbType.Int32, item.SubjectId);
                db.AddInParameter(dbCommand, "p_Range_Type", DbType.Int32, item.RangeType);
                db.AddInParameter(dbCommand, "p_Range_id", DbType.Int32, item.RangeId);
                db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, item.ItemTypeId);
                db.AddInParameter(dbCommand, "p_range_name", DbType.String, item.RangeName);
                db.AddInParameter(dbCommand, "p_exclude_chapters_id", DbType.String, item.ExcludeChapterId);
                db.AddInParameter(dbCommand, "p_Item_Count", DbType.Int32, item.ItemCount);
				db.AddInParameter(dbCommand, "p_Item_diff", DbType.Int32, item.ItemDifficultyID);
                db.AddInParameter(dbCommand, "p_is_mother_item", DbType.Int32, item.IsMotherItem ? 1 : 0);
                db.AddInParameter(dbCommand, "p_mother_id", DbType.Int32, item.MotherID);
                db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);
               
                nRecordAffected = db.ExecuteNonQuery(dbCommand, transaction);               

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }

            connection.Close();

            return nRecordAffected;
        }

        public int DeleteRandomExamStrategy(int RandomExamStrategyID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            int i = 0;
            try
            {
                string sqlCommand = "USP_Random_Exam_Strategy_D";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "p_Random_Exam_Strategy_Id", DbType.Int32, RandomExamStrategyID);

                i = db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }

            connection.Close();
            return i;
        }

        public RandomExamStrategy GetRandomExamStrategy(int ExamStrategyId)
        {
            SystemVersionDAL systemVersionDal = new SystemVersionDAL();
            int usePlace = systemVersionDal.GetUsePlace();

            RandomExamStrategy item = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_Strategy_g";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_Exam_Strategy_Id", DbType.Int32, ExamStrategyId);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    item = CreateModelObject(dataReader);
                    if(usePlace == 3)
                    {
                        item.MaxItemDifficultyID = Convert.ToInt32(dataReader[GetMappingFieldName("MaxItemDifficultyID")].ToString());
                        item.MaxItemDifficultyName = dataReader[GetMappingFieldName("MaxItemDifficultyName")].ToString();
                    }
                    break;
                }
            }

            return item;
        }

		public IList<RandomExamStrategy> GetRandomExamStrategy(int randomExamID, int rangeType, int rangeID)
		{
            SystemVersionDAL systemVersionDal = new SystemVersionDAL();
            int usePlace = systemVersionDal.GetUsePlace();

			IList<RandomExamStrategy> items = new List<RandomExamStrategy>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Strategy_g_new";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
			db.AddInParameter(dbCommand, "p_range_type", DbType.Int32, rangeType);
			db.AddInParameter(dbCommand, "p_range_id", DbType.Int32, rangeID);
			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
                    RandomExamStrategy item = CreateModelObject(dataReader);
                    if (usePlace == 3)
                    {
                        item.MaxItemDifficultyID = Convert.ToInt32(dataReader[GetMappingFieldName("MaxItemDifficultyID")].ToString());
                        item.MaxItemDifficultyName = dataReader[GetMappingFieldName("MaxItemDifficultyName")].ToString();
                    }
                    items.Add(item);
                }
			}

			return items;
		}

        public IList<RandomExamStrategy> GetRandomExamStrategys(int SubjectID)
        {
            SystemVersionDAL systemVersionDal = new SystemVersionDAL();
            int usePlace = systemVersionDal.GetUsePlace();

            IList<RandomExamStrategy> items = new List<RandomExamStrategy>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_Strategy_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_subject_id", DbType.Int32, SubjectID);
			db.AddInParameter(dbCommand, "p_range_id", DbType.Int32, 0);
			db.AddInParameter(dbCommand, "p_range_type", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExamStrategy item = CreateModelObject(dataReader);
                    if (usePlace == 3)
                    {
                        item.MaxItemDifficultyID = Convert.ToInt32(dataReader[GetMappingFieldName("MaxItemDifficultyID")].ToString());
                        item.MaxItemDifficultyName = dataReader[GetMappingFieldName("MaxItemDifficultyName")].ToString();
                    }
                    items.Add(item);
                }
            }

            return items;
        }

        public IList<RandomExamStrategy> GetTotalRandomExamStrategys(int SubjectID)
        {
            SystemVersionDAL systemVersionDal = new SystemVersionDAL();
            int usePlace = systemVersionDal.GetUsePlace();

            IList<RandomExamStrategy> items = new List<RandomExamStrategy>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_Strategy_Q_T";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_subject_id", DbType.Int32, SubjectID);
            db.AddInParameter(dbCommand, "p_range_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_range_type", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExamStrategy item = CreateModelObject(dataReader);
                    if (usePlace == 3)
                    {
                        item.MaxItemDifficultyID = Convert.ToInt32(dataReader[GetMappingFieldName("MaxItemDifficultyID")].ToString());
                        item.MaxItemDifficultyName = dataReader[GetMappingFieldName("MaxItemDifficultyName")].ToString();
                    }
                    item.TotalItemCount = Convert.ToInt32(dataReader[GetMappingFieldName("TotalItemCount")].ToString());
                    item.SelectCount = Convert.ToInt32(dataReader[GetMappingFieldName("SelectCount")].ToString());
                    items.Add(item);
                }
            }

            return items;
        }

		public IList<RandomExamStrategy> GetRandomExamStrategyBySubjectIDAndRangeID(int SubjectID, int rangeID,int rangeType)
		{
            SystemVersionDAL systemVersionDal = new SystemVersionDAL();
            int usePlace = systemVersionDal.GetUsePlace();

			IList<RandomExamStrategy> items = new List<RandomExamStrategy>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Strategy_Q";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_subject_id", DbType.Int32, SubjectID);
			db.AddInParameter(dbCommand, "p_range_id", DbType.Int32, rangeID);
			db.AddInParameter(dbCommand, "p_range_type", DbType.Int32, rangeType);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
                    RandomExamStrategy item = CreateModelObject(dataReader);
                    if (usePlace == 3)
                    {
                        item.MaxItemDifficultyID = Convert.ToInt32(dataReader[GetMappingFieldName("MaxItemDifficultyID")].ToString());
                        item.MaxItemDifficultyName = dataReader[GetMappingFieldName("MaxItemDifficultyName")].ToString();
                    } 
                    items.Add(item);
				}
			}

			return items;
		}

        public IList<RandomExamStrategy> GetRandomExamStrategysByExamID(int RandomExamID)
        {
            SystemVersionDAL systemVersionDal = new SystemVersionDAL();
            int usePlace = systemVersionDal.GetUsePlace();

            IList<RandomExamStrategy> items = new List<RandomExamStrategy>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_Strategy_F";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_RANDOM_EXAM_ID", DbType.Int32, RandomExamID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExamStrategy item = CreateModelObject(dataReader);
                    if (usePlace == 3)
                    {
                        item.MaxItemDifficultyID = Convert.ToInt32(dataReader[GetMappingFieldName("MaxItemDifficultyID")].ToString());
                        item.MaxItemDifficultyName = dataReader[GetMappingFieldName("MaxItemDifficultyName")].ToString();
                    } 
                    items.Add(item);
                }
            }

            return items;
        }


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

        public static RandomExamStrategy CreateModelObject(IDataReader dataReader)
        {

            return new RandomExamStrategy(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RangeType")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamStrategyId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("SubjectId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RangeId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemTypeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ItemTypeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("RangeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("memo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExcludeChapterId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemCount")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ItemDifficultyID")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ItemDifficultyName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("SubjectName")]));
        }
    }
}
