using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class CoursewareTypeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static CoursewareTypeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("coursewaretypeid", "COURSEWARE_TYPE_ID");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("coursewaretypename", "COURSEWARE_TYPE_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 查询组织机构
        /// </summary>
        /// <param name="coursewareTypeId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="coursewareTypeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<CoursewareType> GetCoursewareTypes(int coursewareTypeId, int parentId, string idPath, int levelNum, int orderIndex,
             string coursewareTypeName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<CoursewareType> coursewareTypes = new List<CoursewareType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_TYPE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    CoursewareType coursewareType = CreateModelObject(dataReader);

                    coursewareTypes.Add(coursewareType);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return coursewareTypes;
        }

        public IList<CoursewareType> GetCoursewareTypes()
        {
            IList<CoursewareType> coursewareTypes = new List<CoursewareType>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "COURSEWARE_TYPE");
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "LEVEL_NUM, ORDER_INDEX ASC");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    CoursewareType coursewareType = CreateModelObject(dataReader);

                    coursewareTypes.Add(coursewareType);
                }
            }

            _recordCount = coursewareTypes.Count;

            return coursewareTypes;
        }

        /// <summary>
        /// 取得某课程相关的课程类别信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public IList<CoursewareType> GetCoursewareTypesByCourseID(int courseID)
        {
            IList<CoursewareType> coursewareTypes = new List<CoursewareType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_TYPE";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, courseID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    CoursewareType coursewareType = CreateModelObject(dataReader);

                    coursewareTypes.Add(coursewareType);
                }
            }
            return coursewareTypes;
        }

        public CoursewareType GetCoursewareType(int coursewareTypeId)
        {
            CoursewareType coursewareType = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_TYPE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, coursewareTypeId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    coursewareType = CreateModelObject(dataReader);
                }
            }

            return coursewareType;
        }

        public int AddCoursewareType(CoursewareType coursewareType)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_TYPE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_Courseware_Type_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, coursewareType.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32,4);
            db.AddInParameter(dbCommand, "p_Courseware_Type_name", DbType.String, coursewareType.CoursewareTypeName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, coursewareType.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, coursewareType.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Courseware_Type_id"));

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();

            return id;
        }

        public void UpdateCoursewareType(CoursewareType coursewareType)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_TYPE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, coursewareType.CoursewareTypeId);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, coursewareType.ParentId);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, coursewareType.IdPath);
            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, coursewareType.LevelNum);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, coursewareType.OrderIndex);
            db.AddInParameter(dbCommand, "p_courseware_type_name", DbType.String, coursewareType.CoursewareTypeName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, coursewareType.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, coursewareType.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();
        }

        public void DeleteCoursewareType(int coursewareTypeId, ref int  errorCode)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_TYPE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Courseware_Type_id", DbType.Int32, coursewareTypeId);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();
                errorCode = 0;
            }
            catch(OracleException ex)
            {
                transaction.Rollback();
                errorCode = ex.Code;
            }
            connection.Close();
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

        public static CoursewareType CreateModelObject(IDataReader dataReader)
        {
            return new CoursewareType(
                DataConvert.ToInt(dataReader[GetMappingFieldName("CoursewareTypeId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CoursewareTypeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
