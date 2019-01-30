using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class FunctionRightDAL
    {
        private static Hashtable _ormTable;

        static FunctionRightDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("functionright", "FUNCTION_RIGHT");
        }

        public IList<FunctionRight> GetFunctionRightsByRoleID(int roleID,int functiontype)
        {
            IList<FunctionRight> functionRights = new List<FunctionRight>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_FUNCTION_RIGHT_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);
            db.AddInParameter(dbCommand,"p_function_type",DbType.Int32,functiontype);

            using(IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while(dataReader.Read())
                {
                    FunctionRight functionRight = CreateModelObject(dataReader);

                    functionRights.Add(functionRight);
                }
            }

            return functionRights;
        }

        public static string GetMappingFieldName(string propertyName)
        {
            return (string)_ormTable[propertyName.ToLower()];
        }

        public static FunctionRight CreateModelObject(IDataReader dataReader)
        {
            return new FunctionRight(
                SystemFunctionDAL.CreateModelObject(dataReader),
                DataConvert.ToInt(dataReader[GetMappingFieldName("FunctionRight")]));
        }
    }
}
