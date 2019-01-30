using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    public class FunctionRightBLL
    {
        private static readonly FunctionRightDAL dal = new FunctionRightDAL();

        public IList<FunctionRight> GetFunctionRightsByRoleID(int roleID,int functiontype)
        {
            IList<FunctionRight> functionRightList = dal.GetFunctionRightsByRoleID(roleID,functiontype);

            return functionRightList;
        }
    }
}
