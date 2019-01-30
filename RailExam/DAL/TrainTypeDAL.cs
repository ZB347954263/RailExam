using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;
using System.Text;

namespace RailExam.DAL
{
    public class TrainTypeDAL
    {
        private static Hashtable _ormTable;

        static TrainTypeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("traintypeid", "TRAIN_TYPE_ID");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("typename", "TYPE_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("istemplate","IS_TEMPLATE");
            _ormTable.Add("ispromotion","IS_PROMOTION");
            _ormTable.Add("memo", "MEMO");
        }

        public IList<TrainType> GetTrainTypeInfo(int trainTypeID,
                                         int parentID,
                                         int levelNum,
                                         string iDPath,
                                         int orderIndex,
                                         string typeName,
                                         string description,
                                         bool isTemplate,
                                         bool isPromotion,
                                         string Memo,
                                         int startRowIndex,
                                         int maximumRows,
                                         string orderBy)
        {
            IList<TrainType> trainTypeList = new List<TrainType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            if (isTemplate == true)
            {
                db.AddInParameter(dbCommand, "p_is_template", DbType.Int32, 1);
            }
            if (parentID != 0)
            {
                db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, parentID);
            }

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainType trainType = CreateModelObject(dataReader);

                    trainTypeList.Add(trainType);
                }
            }

            return trainTypeList;
        }

        public IList<TrainType> GetTrainTypes()
        {
            IList<TrainType> trainTypeList = new List<TrainType>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "TRAIN_TYPE");
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "LEVEL_NUM, ORDER_INDEX ASC");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainType trainType = CreateModelObject(dataReader);

                    trainTypeList.Add(trainType);
                }
            }

            return trainTypeList;
        }

		public IList<TrainType> GetTrainTypeByWhereClause(string whereClause,string orderby)
		{
			IList<TrainType> trainTypeList = new List<TrainType>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_TrainType_WhereClause");

			db.AddInParameter(dbCommand, "p_sql", DbType.String, whereClause);
			db.AddInParameter(dbCommand, "p_order_by", DbType.String, orderby);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					TrainType trainType = CreateModelObject(dataReader);

					trainTypeList.Add(trainType);
				}
			}

			return trainTypeList;
		}


        public IList<TrainType> GetTrainTypeByParentId(int trainTypeID)
        {
            IList<TrainType> trainTypeList = new List<TrainType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_ByParentID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainType trainType = CreateModelObject(dataReader);

                    trainTypeList.Add(trainType);
                }
            }

            return trainTypeList; 
        }

        public IList<TrainType> GetTrainStandardTypeInfo(int postID, int flag)
        {
            IList<TrainType> trainTypeList = new List<TrainType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_traintype_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
            db.AddInParameter(dbCommand, "p_flag", DbType.Int32, flag);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainType trainType = CreateModelObject(dataReader);

                    trainTypeList.Add(trainType);
                }
            }

            return trainTypeList;
        }

        public TrainType GetTrainTypeInfo(int trainTypeID)
        {
            TrainType trainType = new TrainType();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_flag", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    trainType = CreateModelObject(dataReader);
                }
            }

            return trainType;
        }

        public void AddTrainType(TrainType trainType)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_traintype_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, trainType.ParentID);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 50);
            db.AddOutParameter(dbCommand, "p_order_index",DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_type_name", DbType.String, trainType.TypeName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, trainType.Description);
            db.AddInParameter(dbCommand, "p_is_template", DbType.Int32, trainType.IsTemplate ? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_promotion", DbType.Int32, trainType.IsPromotion ? 1 : 0);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, trainType.Memo);

            db.ExecuteNonQuery(dbCommand);

            trainType.LevelNum = (int)db.GetParameterValue(dbCommand, "p_level_num");
            trainType.IDPath = (string)db.GetParameterValue(dbCommand, "p_id_path");
            trainType.OrderIndex = (int)db.GetParameterValue(dbCommand, "p_order_index");
        }

        public void DeleteTrainType(int trainTypeID,ref int errorCode)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_traintype_id", DbType.Int32, trainTypeID);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trainsaction = connection.BeginTransaction();
            try
            {
                db.ExecuteNonQuery(dbCommand,trainsaction);
                trainsaction.Commit();
                errorCode = 0;
            }
            catch(OracleException ex)
            {
                trainsaction.Rollback();
                errorCode = ex.Code;
            }
            connection.Close();
        }

        public void UpdateTrainType(TrainType trainType)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_TYPE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_traintype_id", DbType.Int32, trainType.TrainTypeID);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, trainType.ParentID);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 50);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, trainType.OrderIndex);
            db.AddInParameter(dbCommand, "p_type_name", DbType.String, trainType.TypeName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, trainType.Description);
            db.AddInParameter(dbCommand, "p_is_template", DbType.Int32, trainType.IsTemplate ? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_promotion", DbType.Int32, trainType.IsPromotion ? 1 : 0);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, trainType.Memo);

            db.ExecuteNonQuery(dbCommand);

            trainType.LevelNum = (int)db.GetParameterValue(dbCommand, "p_level_num");
            trainType.IDPath = (string)db.GetParameterValue(dbCommand, "p_id_path");
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

        public static TrainType CreateModelObject(IDataReader dataReader)
        {
            return new TrainType(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainTypeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IDPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TypeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsTemplate")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsPromotion")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
