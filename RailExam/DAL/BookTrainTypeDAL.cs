using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class BookTrainTypeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static BookTrainTypeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("bookid", "Book_ID");
            _ormTable.Add("traintypeid", "Train_Type_ID");
            _ormTable.Add("traintypename", "Train_Type_Name");
            _ormTable.Add("orderindex","ORDER_INDEX");
        }

        public void AddBookTrainType(BookTrainType obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_TRAIN_TYPE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, obj.BookID);
            db.AddInParameter(dbCommand, "p_traintype_id", DbType.Int32, obj.TrainTypeID);
            db.AddOutParameter(dbCommand,"p_order_index",DbType.Int32,4);

            db.ExecuteNonQuery(dbCommand);
        }

        public void UpdateBookTrainType(BookTrainType obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_TRAIN_TYPE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, obj.BookID);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, obj.TrainTypeID);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, obj.OrderIndex);

            db.ExecuteNonQuery(dbCommand);
        }

        public IList<BookTrainType> GetBookTrainTypeByBookID(int bookID)
        {
            IList<BookTrainType> objList = new List<BookTrainType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_TRAIN_TYPE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    BookTrainType obj = CreateModelObject(dataReader);
                    objList.Add(obj);
                }
            }

            return objList;
        }

        public BookTrainType GetBookTrainType(int bookID, int trainTypeID)
        {
           BookTrainType obj = new BookTrainType();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_TRAIN_TYPE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);

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

        public static BookTrainType CreateModelObject(IDataReader dataReader)
        {
            return new BookTrainType(
                DataConvert.ToInt(dataReader[GetMappingFieldName("BookID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainTypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TrainTypeName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]));
        }
    }
}
