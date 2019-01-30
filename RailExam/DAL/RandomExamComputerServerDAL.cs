using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class RandomExamComputerServerDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static RandomExamComputerServerDAL()
        {
            _ormTable = new Hashtable();

            // 源名称必须小写
            _ormTable.Add("randomexamid", "RANDOM_EXAM_ID");
            _ormTable.Add("computerserverno", "COMPUTER_SERVER_NO");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("isstart", "IS_START");
			_ormTable.Add("haspaper","HAS_PAPER");
			_ormTable.Add("randomexamcode","RANDOM_EXAM_CODE");
			_ormTable.Add("isupload","IS_UPLOAD");
			_ormTable.Add("downloaded","DOWNLOADED");
        }

        public RandomExamComputerServer GetRandomExamComputerServer(int examid,int serverNo)
        {
            RandomExamComputerServer obj = new RandomExamComputerServer();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Server_G");

            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examid);
            db.AddInParameter(dbCommand, "p_server_no", DbType.Int32, serverNo);

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

        public static RandomExamComputerServer CreateModelObject(IDataReader dataReader)
		{
            return new RandomExamComputerServer(
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ComputerServerNo")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("StatusID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsStart")]),
				DataConvert.ToBool(dataReader[GetMappingFieldName("HasPaper")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("RandomExamCode")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsUpload")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("Downloaded")]));
		}
    }
}
