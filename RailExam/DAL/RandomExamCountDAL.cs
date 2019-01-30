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
    public class RandomExamCountDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static RandomExamCountDAL()
        {
            _ormTable = new Hashtable();
            _ormTable.Add("employeeid", "Employee_ID");
            _ormTable.Add("randomexamid", "random_Exam_ID");          
            _ormTable.Add("count", "Count");          
        }

        public void UpdateRandomExamCount(int employeeID,int randomExamID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_Exam_Count_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32,employeeID);
            db.AddInParameter(dbCommand, "p_random_Exam_ID", DbType.Int32, randomExamID);

            db.ExecuteNonQuery(dbCommand);
        }

        public int GetRandomExamCount(int employeeID, int randomExamID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "USP_Random_Exam_Count_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);
            db.AddInParameter(dbCommand, "p_random_Exam_ID", DbType.Int32, randomExamID);
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
        }

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

        public static RandomExamCount CreateModelObject(IDataReader dataReader)
        {
            return new RandomExamCount(
                DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("Count")]));
        }

    }
}
