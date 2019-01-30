using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class ItemDifficultyDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        /// <summary>
        /// 查询结果记录数
        /// </summary>
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

        public static ItemDifficulty CreateModelObject(IDataReader dataReader)
        {
            return new ItemDifficulty(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemDifficultyId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("DifficultyName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DifficultyValue")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        static ItemDifficultyDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("itemdifficultyid", "ITEM_DIFFICULTY_ID");
            _ormTable.Add("difficultyname", "DIFFICULTY_NAME");
            _ormTable.Add("difficultyvalue", "DIFFICULTY_VALUE");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 新增试题难度
        /// </summary>
        /// <param name="itemDifficulty">试题难度</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddItemDifficulty(ItemDifficulty itemDifficulty)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_DIFFICULTY_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_difficulty_name", DbType.String, itemDifficulty.DifficultyName);
            db.AddInParameter(dbCommand, "p_difficulty_value", DbType.String, itemDifficulty.DifficultyValue);
            db.AddInParameter(dbCommand, "p_description", DbType.String, itemDifficulty.Description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, itemDifficulty.IsDefault);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, itemDifficulty.Memo);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            itemDifficulty.ItemDifficultyId = (int)db.GetParameterValue(dbCommand, "p_item_difficulty_id");

            return nRecordAffected;
        }

        /// <summary>
        /// 删除试题难度
        /// </summary>
        /// <param name="itemDifficultyId">试题难度ID</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteItemDifficulty(int itemDifficultyId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_DIFFICULTY_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, itemDifficultyId);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 修改试题难度
        /// </summary>
        /// <param name="itemDifficulty">试题难度</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemDifficulty(ItemDifficulty itemDifficulty)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_DIFFICULTY_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, itemDifficulty.ItemDifficultyId);
            db.AddInParameter(dbCommand, "p_difficulty_name", DbType.String, itemDifficulty.DifficultyName);
            db.AddInParameter(dbCommand, "p_difficulty_value", DbType.String, itemDifficulty.DifficultyValue);
            db.AddInParameter(dbCommand, "p_description", DbType.String, itemDifficulty.Description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, itemDifficulty.IsDefault);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, itemDifficulty.Memo);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 按试题难度ID取试题难度
        /// </summary>
        /// <returns>试题难度</returns>
        public ItemDifficulty GetItemDifficulty(int itemDifficultyId)
        {
            ItemDifficulty itemDifficulty = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_DIFFICULTY_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, itemDifficultyId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemDifficulty = CreateModelObject(dataReader);
                    break;
                }
            }

            return itemDifficulty;
        }
        
        /// <summary>
        /// 查询所有试题难度
        /// </summary>
        /// <returns>所有试题难度</returns>
        public IList<ItemDifficulty> GetItemDifficulties()
        {
            IList<ItemDifficulty> itemDifficulties = new List<ItemDifficulty>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_DIFFICULTY_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemDifficulties.Add(CreateModelObject(dataReader));
                }
            }

            return itemDifficulties;
        }


        /// <summary>
        /// 查询符合条件的试题难度
        /// </summary>
        /// <param name="itemDifficultyId"></param>
        /// <param name="difficultyName"></param>
        /// <param name="difficultyValue"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>符合条件的试题难度</returns>
        public IList<ItemDifficulty> GetItemDifficulties(int itemDifficultyId, string difficultyName,
            int difficultyValue, string description, int isDefault, string memo)
        {
            IList<ItemDifficulty> itemDifficulties = new List<ItemDifficulty>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_DIFFICULTY_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, itemDifficultyId);
            db.AddInParameter(dbCommand, "p_difficulty_name", DbType.String, difficultyName);
            db.AddInParameter(dbCommand, "p_difficulty_value", DbType.String, difficultyValue);
            db.AddInParameter(dbCommand, "p_description", DbType.String, description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, isDefault);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemDifficulties.Add(CreateModelObject(dataReader));
                }
            }

            return itemDifficulties;
        }
    }
}