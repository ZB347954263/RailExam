using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class ItemConfigDAL
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
                orderByCondition = System.Text.RegularExpressions.Regex.Replace(s.ToLower(), "\\s+asc$", ",asc");
                orderByCondition = System.Text.RegularExpressions.Regex.Replace(orderByCondition, "\\s+desc$", ",desc");
                orderByCondition = orderByCondition.Trim();

                string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ',' });

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

        public static ItemConfig CreateModelObject(IDataReader dataReader)
        {
            ItemConfig config = new ItemConfig(
                DataConvert.ToInt(dataReader[GetMappingFieldName("DefaultTypeId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DefaultDifficultyId")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("DefaultScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DefaultAnswerCount")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DefaultCompleteTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("DefaultSource")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("DefaultVersion")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("DefaultOutDateDate")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DefaultStatusId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DefaultRemindDays")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DefaultUsageId")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ItemConfigId")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("HasPicture")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeId")])
                );

            try
            {
                config.DefaultDifficultyName = DataConvert.ToString(dataReader[GetMappingFieldName("DefaultDifficultyName")]);
                config.DefaultStatusName = DataConvert.ToString(dataReader[GetMappingFieldName("DefaultStatusName")]);
                config.DefaultTypeName = DataConvert.ToString(dataReader[GetMappingFieldName("DefaultTypeName")]);
            }
            catch
            {

            }

            return config;
        }

        static ItemConfigDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("defaulttypeid", "DEFAULT_TYPE_ID");
            _ormTable.Add("defaultdifficultyid", "DEFAULT_DIFFICULTY_ID");
            _ormTable.Add("defaultscore", "DEFAULT_SCORE");
            _ormTable.Add("defaultanswercount", "DEFAULT_ANSWER_COUNT");
            _ormTable.Add("defaultcompletetime", "DEFAULT_COMPLETE_TIME");
            _ormTable.Add("defaultsource", "DEFAULT_SOURCE");
            _ormTable.Add("defaultversion", "DEFAULT_VERSION");
            _ormTable.Add("defaultoutdatedate", "DEFAULT_OUTDATE_DATE");
            _ormTable.Add("defaultstatusid", "DEFAULT_STATUS_ID");
            _ormTable.Add("defaultreminddays", "ADVANCE_REMIND_DAYS");
            _ormTable.Add("defaultdifficultyname", "DEFAULT_DIFFICULTY_NAME");
            _ormTable.Add("defaultstatusname", "DEFAULT_STATUS_NAME");
            _ormTable.Add("defaulttypename", "DEFAULT_TYPE_NAME");
            _ormTable.Add("defaultusageid", "DEFAULT_USAGE_ID");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("asc", "ASC");
            _ormTable.Add("desc", "DESC");
			_ormTable.Add("itemconfigid", "ITEM_CONFIG_ID");
            _ormTable.Add("haspicture","HASPICTURE");
			_ormTable.Add("employeeid", "EMPLOYEE_ID");
        }

        /// <summary>
        /// 新增试题设置
        /// </summary>
        /// <param name="itemConfig">试题设置</param>
        /// <returns>数据库受影响的行数</returns>
		public int AddItemConfig(ItemConfig itemConfig)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ITEM_CONFIG_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_default_type_id", DbType.Int32, itemConfig.DefaultTypeId);
			db.AddInParameter(dbCommand, "p_default_difficulty_id", DbType.Int32, itemConfig.DefaultDifficultyId);
			db.AddInParameter(dbCommand, "p_default_score", DbType.Int32, itemConfig.DefaultScore);
			db.AddInParameter(dbCommand, "p_default_answer_count", DbType.Int32, itemConfig.DefaultAnswerCount);
			db.AddInParameter(dbCommand, "p_default_complete_time", DbType.Int32, itemConfig.DefaultCompleteTime);
			db.AddInParameter(dbCommand, "p_default_source", DbType.String, itemConfig.DefaultSource);
			db.AddInParameter(dbCommand, "p_default_version", DbType.String, itemConfig.DefaultVersion);
			db.AddInParameter(dbCommand, "p_default_outdate_date", DbType.DateTime, itemConfig.DefaultOutDateDate);
			db.AddInParameter(dbCommand, "p_default_status_id", DbType.Int32, itemConfig.DefaultStatusId);
			db.AddInParameter(dbCommand, "p_advance_remind_days", DbType.Int32, itemConfig.DefaultRemindDays);
			db.AddInParameter(dbCommand, "p_default_usage_id", DbType.Int32, itemConfig.DefaultUsageId);
			db.AddInParameter(dbCommand, "p_item_config_id", DbType.Int32,8);
			db.AddInParameter(dbCommand, "p_has_picture", DbType.Int32, itemConfig.HasPicture);
			db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, itemConfig.EmployeeId);

			return db.ExecuteNonQuery(dbCommand);
		}
				
		

        /// <summary>
        /// 修改试题设置
        /// </summary>
        /// <param name="itemConfig">试题设置</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemConfig(ItemConfig itemConfig)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_CONFIG_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_default_type_id", DbType.Int32, itemConfig.DefaultTypeId);
            db.AddInParameter(dbCommand, "p_default_difficulty_id", DbType.Int32, itemConfig.DefaultDifficultyId);
            db.AddInParameter(dbCommand, "p_default_score", DbType.Int32, itemConfig.DefaultScore);
            db.AddInParameter(dbCommand, "p_default_answer_count", DbType.Int32, itemConfig.DefaultAnswerCount);
            db.AddInParameter(dbCommand, "p_default_complete_time", DbType.Int32, itemConfig.DefaultCompleteTime);
            db.AddInParameter(dbCommand, "p_default_source", DbType.String, itemConfig.DefaultSource);
            db.AddInParameter(dbCommand, "p_default_version", DbType.String, itemConfig.DefaultVersion);
            db.AddInParameter(dbCommand, "p_default_outdate_date", DbType.DateTime, itemConfig.DefaultOutDateDate);
            db.AddInParameter(dbCommand, "p_default_status_id", DbType.Int32, itemConfig.DefaultStatusId);
            db.AddInParameter(dbCommand, "p_advance_remind_days", DbType.Int32, itemConfig.DefaultRemindDays);
            db.AddInParameter(dbCommand, "p_default_usage_id", DbType.Int32, itemConfig.DefaultUsageId);
			db.AddOutParameter(dbCommand, "p_item_config_id", DbType.Int32, itemConfig.ItemConfigId);
            db.AddInParameter(dbCommand,"p_has_picture",DbType.Int32,itemConfig.HasPicture);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, itemConfig.EmployeeId);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 获取试题设置
        /// </summary>
        /// <returns>试题设置</returns>
        public ItemConfig GetItemConfig()
        {
            ItemConfig itemType = new ItemConfig();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_ITEM_CONFIG_ALL");
			db.AddInParameter(dbCommand, "p_employee_id",DbType.Int32,8);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemType = CreateModelObject(dataReader);
                    itemType.HasPicture = Convert.ToInt32(dataReader[GetMappingFieldName("HasPicture")]);
                    break;
                }
            }

            _recordCount = 1;

            return itemType;
        }
		/// <summary>
		/// 带参数的重载
		/// </summary>
		/// <param name="employeeID"></param>
		/// <returns></returns>
		public ItemConfig GetItemConfig(int employeeID)
		{
			ItemConfig itemType = new ItemConfig();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_ITEM_CONFIG_ALL");
			db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);
			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					itemType = CreateModelObject(dataReader);
					itemType.HasPicture = Convert.ToInt32(dataReader[GetMappingFieldName("HasPicture")]);
					break;
				}
			}

			_recordCount = 1;
			itemType.EmployeeId = employeeID;
			return itemType;
		}
    }
}