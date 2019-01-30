using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class ItemStatusDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        /// <summary>
        /// ��ѯ�����¼��
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

        public static ItemStatus CreateModelObject(IDataReader dataReader)
        {
            return new ItemStatus(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemStatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

        /// <summary>
        /// �ղ������캯��
        /// </summary>
        static ItemStatusDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("itemstatusid", "ITEM_STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// ��������״̬
        /// </summary>
        /// <param name="itemStatus">����״̬</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int AddItemStatus(ItemStatus itemStatus)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_STATUS_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_item_status_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_status_name", DbType.String, itemStatus.StatusName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, itemStatus.Description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, itemStatus.IsDefault);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, itemStatus.Memo);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            itemStatus.ItemStatusId = (int)db.GetParameterValue(dbCommand, "p_item_status_id");

            return nRecordAffected;
        }

        /// <summary>
        /// ɾ������״̬
        /// </summary>
        /// <param name="itemStatusId">����״̬ID</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int DeleteItemStatus(int itemStatusId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_STATUS_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_status_id", DbType.Int32, itemStatusId);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// �޸�����״̬
        /// </summary>
        /// <param name="itemStatus">����״̬</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int UpdateItemStatus(ItemStatus itemStatus)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_STATUS_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_status_id", DbType.Int32, itemStatus.ItemStatusId);
            db.AddInParameter(dbCommand, "p_status_name", DbType.String, itemStatus.StatusName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, itemStatus.Description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, itemStatus.IsDefault);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, itemStatus.Memo);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ������״̬IDȡ����״̬
        /// </summary>
        /// <param name="itemStatusId">����״̬ID</param>
        /// <returns>����״̬</returns>
        public ItemStatus GetItemStatus(int itemStatusId)
        {
            ItemStatus itemStatus = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_STATUS_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_status_id", DbType.Int32, itemStatusId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemStatus = CreateModelObject(dataReader);
                    break;
                }
            }

            return itemStatus;
            
        }

        /// <summary>
        /// ��ѯ��������״̬
        /// </summary>
        /// <returns>��������״̬</returns>
        public IList<ItemStatus> GetItemStatuss()
        {
            IList<ItemStatus> itemStatuss = new List<ItemStatus>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_STATUS_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemStatuss.Add(CreateModelObject(dataReader));
                }
            }

            return itemStatuss;
        }

        /// <summary>
        /// ��ѯ��������������״̬
        /// </summary>
        /// <param name="itemStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>��������������״̬</returns>
        public IList<ItemStatus> GetItemStatuss(int itemStatusId, string statusName,
            string description, int isDefault, string memo)
        {
            IList<ItemStatus> itemStatuss = new List<ItemStatus>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_STATUS_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_status_id", DbType.Int32, itemStatusId);
            db.AddInParameter(dbCommand, "p_status_name", DbType.String, statusName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, isDefault);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemStatuss.Add(CreateModelObject(dataReader));
                }
            }

            return itemStatuss;
        }
    }
}