using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class AssistBookTrainTypeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static AssistBookTrainTypeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("asssistbookid", "ASSIST_Book_ID");
            _ormTable.Add("traintypeid", "Train_Type_ID");
            _ormTable.Add("traintypename", "Train_Type_Name");
        }

        public void AddAssistBookTrainType(AssistBookTrainType obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_TRAIN_TYPE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, obj.AssistBookID);
            db.AddInParameter(dbCommand, "p_traintype_id", DbType.Int32, obj.TrainTypeID);


            db.ExecuteNonQuery(dbCommand);
        }

        public void UpdateAssistBookTrainType(AssistBookTrainType obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_TRAIN_TYPE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, obj.AssistBookID);
            db.AddInParameter(dbCommand, "p_traintype_id", DbType.Int32, obj.TrainTypeID);


            db.ExecuteNonQuery(dbCommand);
        }

        public IList<AssistBookTrainType> GetAssistBookTrainTypeByBookID(int bookID)
        {
            IList<AssistBookTrainType> objList = new List<AssistBookTrainType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_TRAIN_TYPE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, bookID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBookTrainType obj = CreateModelObject(dataReader);
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

        public static AssistBookTrainType CreateModelObject(IDataReader dataReader)
        {
            return new AssistBookTrainType(
                DataConvert.ToInt(dataReader[GetMappingFieldName("AssistBookID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainTypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TrainTypeName")]));
        }
    }
}
