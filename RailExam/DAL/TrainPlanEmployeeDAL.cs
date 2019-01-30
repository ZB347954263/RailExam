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
    public class TrainPlanEmployeeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainPlanEmployeeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("trainplanid", "TRAIN_PLAN_ID");
            _ormTable.Add("trainplanemployeeid","TRAIN_PLAN_EMPLOYEE_ID");
            _ormTable.Add("process", "PROCESS");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 添加培训计划员工
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainPlanEmployee(TrainPlanEmployee obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_EMPLOYEE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, obj.TrainPlanID);
            db.AddInParameter(dbCommand, "p_train_employee_id", DbType.Int32, obj.TrainPlanEmployeeID);
            db.AddInParameter(dbCommand, "p_process", DbType.VarNumeric, obj.Process);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, obj.StatusID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新培训计划员工
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainPlanEmployee(TrainPlanEmployee obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_EMPLOYEE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, obj.TrainPlanID);
            db.AddInParameter(dbCommand, "p_train_employee_id", DbType.Int32, obj.TrainPlanEmployeeID);
            db.AddInParameter(dbCommand, "p_process", DbType.VarNumeric, obj.Process);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, obj.StatusID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除培训计划员工
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="employeeid"></param>
        public void DeleteTrainPlanEmployee(int planid, int employeeid)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_EMPLOYEE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, planid);
            db.AddInParameter(dbCommand, "p_train_employee_id", DbType.Int32, employeeid);

            db.ExecuteNonQuery(dbCommand);
        }

        public IList<TrainPlanEmployee> GetTrainPlanEmployeeInfo(int trainPlanID,
                                                    int trainEmployeeID,
                                                    decimal process,
                                                    int statusID,
                                                    string memo,
                                                    int startRowIndex,
                                                    int maximumRows,
                                                    string orderBy)
        {
            IList<TrainPlanEmployee> objEmployeeList = new List<TrainPlanEmployee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_EMPLOYEE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainPlanEmployee obj = CreateModelObject(dataReader);

                    objEmployeeList.Add(obj);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return objEmployeeList;
        }

        /// <summary>
        /// 根据计划ID和员工ID返回唯一的培训计划员工信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <param name="trainEmployeeID"></param>
        /// <returns></returns>
        public TrainPlanEmployee GetTrainPlanEmployeeInfo(int trainPlanID, int trainEmployeeID)
        {
            TrainPlanEmployee obj = new TrainPlanEmployee();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_EMPLOYEE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, trainPlanID);
            db.AddInParameter(dbCommand, "p_train_employee_id", DbType.Int32, trainEmployeeID);

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
        /// 返回某一培训计划所有的培训计划员工信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public IList<TrainPlanEmployee> GetTrainPlanEmployeeInfoByPlanID(int trainPlanID)
        {
            IList<TrainPlanEmployee> objEmployeeList = new List<TrainPlanEmployee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_EMPLOYEE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, trainPlanID);
            db.AddInParameter(dbCommand, "p_train_employee_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainPlanEmployee obj = CreateModelObject(dataReader);
                    objEmployeeList.Add(obj);
                }
            }

            return objEmployeeList;
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

        public static TrainPlanEmployee CreateModelObject(IDataReader dataReader)
        {
            return new TrainPlanEmployee(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainPlanID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainPlanEmployeeID")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("Process")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StatusID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
				new Employee(
                    DataConvert.ToInt(dataReader[EmployeeDAL.GetMappingFieldName("EmployeeID")]),
                    DataConvert.ToInt(dataReader[EmployeeDAL.GetMappingFieldName("OrgID")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("OrgName")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("WorkNo")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("EmployeeName")]),
                    DataConvert.ToInt(dataReader[EmployeeDAL.GetMappingFieldName("PostID")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("PostName")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("Sex")]),
                    DataConvert.ToDateTime(dataReader[EmployeeDAL.GetMappingFieldName("Birthday")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("NativePlace")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("Folk")]),
                    DataConvert.ToInt(dataReader[EmployeeDAL.GetMappingFieldName("Wedding")]),
                    DataConvert.ToDateTime(dataReader[EmployeeDAL.GetMappingFieldName("BeginDate")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("WorkPhone")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("HomePhone")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("MobilePhone")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("Email")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("Address")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("PostCode")]),
                    DataConvert.ToBool(dataReader[EmployeeDAL.GetMappingFieldName("Dimission")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("Memo")]),
                    DataConvert.ToInt(dataReader[EmployeeDAL.GetMappingFieldName("IsGroupLeader")]),
                    DataConvert.ToInt(dataReader[EmployeeDAL.GetMappingFieldName("TechnicianTypeID")]),
                    DataConvert.ToString(dataReader[EmployeeDAL.GetMappingFieldName("PostNo")]),
					DataConvert.ToInt(dataReader[EmployeeDAL.GetMappingFieldName("LoginCount")]),
					DataConvert.ToInt(dataReader[EmployeeDAL.GetMappingFieldName("LoginTime")])
					));
        }
    }
}
