using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;
using System.Text;

namespace RailExam.DAL
{
    public class TrainStandardDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainStandardDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("trainstandardid", "TRAIN_STANDARD_ID");
            _ormTable.Add("postid", "POST_ID");
            _ormTable.Add("typeid", "TYPE_ID");
            _ormTable.Add("traintime", "TRAIN_TIME");
            _ormTable.Add("traincontent", "TRAIN_CONTENT");
            _ormTable.Add("trainform", "TRAIN_FORM");
            _ormTable.Add("examform", "EXAM_FORM");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 新增培训规范
        /// </summary>
        public void AddTrainStandard(TrainStandard obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_STANDARD_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_train_standard_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, obj.PostID);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, obj.TypeID);
            db.AddInParameter(dbCommand, "p_train_time", DbType.String, obj.TrainTime);
            db.AddInParameter(dbCommand, "p_train_content", DbType.String, obj.TrainContent);
            db.AddInParameter(dbCommand, "p_train_form", DbType.String, obj.TrainForm);
            db.AddInParameter(dbCommand, "p_exam_form", DbType.String, obj.ExamForm);
            db.AddInParameter(dbCommand, "p_description", DbType.String, obj.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除培训规范
        /// </summary>
        public void DeleteTrainStandard(int TrainStandardID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_STANDARD_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_standard_id", DbType.Int32, TrainStandardID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新培训规范
        /// </summary>
        public void UpdateTrainStandard(TrainStandard obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_STANDARD_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_standard_id", DbType.Int32, obj.TrainStandardID);
            db.AddInParameter(dbCommand, "p_train_time", DbType.String, obj.TrainTime);
            db.AddInParameter(dbCommand, "p_train_content", DbType.String, obj.TrainContent);
            db.AddInParameter(dbCommand, "p_train_form", DbType.String, obj.TrainForm);
            db.AddInParameter(dbCommand, "p_exam_form", DbType.String, obj.ExamForm);
            db.AddInParameter(dbCommand, "p_description", DbType.String, obj.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }

        public IList<TrainStandard> GetTrainStandardInfo(int trainStandardID,
                                                 int postID,
                                                 int typeID,
                                                 string trainTime,
                                                 string trainContent,
                                                 string trainForm,
                                                 string examForm,
                                                 string description,
                                                 string memo,
                                                 int startRowIndex,
                                                 int maximumRows,
                                                 string orderBy)
        {
            IList<TrainStandard> train = new List<TrainStandard>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_STANDARD_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, typeID); 
            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainStandard obj = CreateModelObject(dataReader);

                    train.Add(obj);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return train;
        }

        /// <summary>
        /// 根据培训规范ID确定唯一的培训规范
        /// </summary>
        /// <param name="trainstandardid">培训规范ID</param>
        /// <returns></returns>
        public TrainStandard GetTrainStandardInfo(int trainstandardid)
        {
            TrainStandard obj = new TrainStandard();
            TrainTypeDAL objTrainType = new TrainTypeDAL();
            PostDAL objPost = new PostDAL();
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_STANDARD_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_standard_id", DbType.Int32, trainstandardid);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, 0);
           
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    obj = CreateModelObject(dataReader);


                    obj.PostName = objPost.GetPost(obj.PostID).PostName;
                    obj.TypeName = objTrainType.GetTrainTypeInfo(obj.TypeID).TypeName;

                }
            }

            return obj;
        }

        /// <summary>
        /// 根据培训岗位ID，培训类别ID确定唯一的培训规范
        /// </summary>
        /// <param name="postid">培训岗位ID</param>
        /// <param name="typeid">培训类别ID</param>
        /// <returns></returns>
        public TrainStandard GetTrainStandardInfo(int postid,int typeid)
        {
            TrainStandard obj = new TrainStandard();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_STANDARD_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_standard_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postid);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, typeid);

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

        public static TrainStandard CreateModelObject(IDataReader dataReader)
        {
            return new TrainStandard(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainStandardID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PostID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TrainTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TrainContent")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TrainForm")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamForm")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
