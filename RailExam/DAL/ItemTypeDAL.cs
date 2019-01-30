using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class ItemTypeDAL
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

        public static ItemType CreateModelObject(IDataReader dataReader)
        {
            return new ItemType(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemTypeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TypeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ImageFileName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsValid")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

        static ItemTypeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("itemtypeid", "ITEM_TYPE_ID");
            _ormTable.Add("typename", "TYPE_NAME");
            _ormTable.Add("imagefilename", "IMAGE_FILE_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("isvalid", "IS_VALID");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 新增试题类别
        /// </summary>
        /// <param name="itemType">试题类别</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddItemType(ItemType itemType)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_TYPE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_item_type_id", DbType.Int32, 2);
            db.AddInParameter(dbCommand, "p_type_name", DbType.String, itemType.TypeName);
            db.AddInParameter(dbCommand, "p_image_file_name", DbType.String, itemType.ImageFileName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, itemType.Description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, itemType.IsDefault);
            db.AddInParameter(dbCommand, "p_is_valid", DbType.Int32, itemType.IsValid);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, itemType.Memo);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            itemType.ItemTypeId = (int)db.GetParameterValue(dbCommand, "p_item_type_id");

            return nRecordAffected;
        }

        /// <summary>
        /// 删除试题类别
        /// </summary>
        /// <param name="itemTypeId">试题类别ID</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteItemType(int itemTypeId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_TYPE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeId);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 修改试题类别
        /// </summary>
        /// <param name="itemType">试题类别</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemType(ItemType itemType)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_TYPE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemType.ItemTypeId);
            db.AddInParameter(dbCommand, "p_type_name", DbType.String, itemType.TypeName);
            db.AddInParameter(dbCommand, "p_image_file_name", DbType.String, itemType.ImageFileName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, itemType.Description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, itemType.IsDefault);
            db.AddInParameter(dbCommand, "p_is_valid", DbType.Int32, itemType.IsValid);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, itemType.Memo);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 按试题类别ID取试题类别
        /// </summary>
        /// <param name="itemTypeId">题类别ID</param>
        /// <returns>试题类别</returns>
        public ItemType GetItemType(int itemTypeId)
        {
            ItemType itemType = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_TYPE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemType = CreateModelObject(dataReader);
                    break;
                }
            }

            return itemType;
        }

        /// <summary>
        /// 查询所有试题类别
        /// </summary>
        /// <returns>所有试题类别</returns>
        public IList<ItemType> GetItemTypes()
        {
            IList<ItemType> itemTypes = new List<ItemType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_TYPE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemTypes.Add(CreateModelObject(dataReader));
                }
            }

            return itemTypes;
        }

        /// <summary>
        /// 查询符合条件的试题类别
        /// </summary>
        /// <param name="itemTypeId"></param>
        /// <param name="typeName"></param>
        /// <param name="imageFileName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="isValid"></param>
        /// <param name="memo"></param>
        /// <returns>符合条件的试题类别</returns>
        public IList<ItemType> GetItemTypes(int itemTypeId, string typeName, string imageFileName, 
            string description, int isDefault, int isValid, string memo)
        {
            IList<ItemType> itemTypes = new List<ItemType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_TYPE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeId);
            db.AddInParameter(dbCommand, "p_type_name", DbType.String, typeName);
            db.AddInParameter(dbCommand, "p_image_file_name", DbType.String, imageFileName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, isDefault);
            db.AddInParameter(dbCommand, "p_is_valid", DbType.Int32, isValid);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemTypes.Add(CreateModelObject(dataReader));
                }
            }

            return itemTypes;
        }
    }
}