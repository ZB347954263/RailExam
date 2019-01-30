using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：系统功能
    /// </summary>
    public class SystemFunctionBLL
	{
        private static readonly SystemFunctionDAL dal = new SystemFunctionDAL();

        public IList<SystemFunction> GetSystemFunctions(string functionID, string functionName,
            string description, string pageUrl, string menuName, int toolbarNo, string toolbarName,
            bool isDefault, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<SystemFunction> systemFunctionList = dal.GetSystemFunctions(functionID, functionName, description,
				pageUrl, menuName, toolbarNo, toolbarName, isDefault, memo, startRowIndex, maximumRows, orderBy);

            return systemFunctionList;
        }

        public IList<SystemFunction> GetSystemFunctions()
        {
            IList<SystemFunction> systemFunctionList = dal.GetSystemFunctions();

            return systemFunctionList;
        }

        public SystemFunction GetSystemFunction(string functionID)
        {
            if(string.IsNullOrEmpty(functionID))
            {
                return null;
            }

            return dal.GetSystemFunction(functionID);
        }

        public int GetCount(string functionID, string functionName, string description, string pageUrl,
			string menuName, int toolbarNo, string toolbarName, bool isDefault, string memo)
        {
            return dal.RecordCount;
        }
	}
}
