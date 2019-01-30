using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class TrainEmployeeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainEmployeeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("trainemployeeid", "TRAIN_EMPLOYEE_ID");
            _ormTable.Add("employeeid", "EMPLOYEE_ID"); 
            _ormTable.Add("traintypeid", "TRAIN_TYPE_ID");

        }

        /// <summary>
        /// 新增培训类别
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainEmployee(TrainEmployee obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_EMPLOYEE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_train_employee_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, obj.EmployeeID);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, obj.TrainTypeID);

            db.ExecuteNonQuery(dbCommand);
        }

        public void UpdateTrainEmployee(TrainEmployee obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_EMPLOYEE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_employee_id", DbType.Int32, obj.TrainEmployeeID);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, obj.EmployeeID);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, obj.TrainTypeID);

            db.ExecuteNonQuery(dbCommand);
        }

        public TrainEmployee GetTrainEmployeeByEmployee(int employeeID)
        {
            TrainEmployee obj = new TrainEmployee();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_EMPLOYEE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);

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

        public static TrainEmployee CreateModelObject(IDataReader dataReader)
        {
            return new TrainEmployee(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainEmployeeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainTypeID")]));
        }
    }
}
