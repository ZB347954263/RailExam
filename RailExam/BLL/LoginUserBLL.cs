using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    public class LoginUserBLL
    {
        private static readonly LoginUserDAL dal = new LoginUserDAL();

        public LoginUser GetLoginUser(string userID, string password,int functiontype)
        {
            if(string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            LoginUser loginUser = dal.GetLoginUser(userID, password);

            if(loginUser != null)
            {
                FunctionRightBLL functionRightBLL = new FunctionRightBLL();
                loginUser.FunctionRights = functionRightBLL.GetFunctionRightsByRoleID(loginUser.RoleID,functiontype);
            }

            return loginUser;
        }
		public LoginUser GetLoginUserByCardID(string cardID, string password, int functiontype)
		{
			if (string.IsNullOrEmpty(cardID) || string.IsNullOrEmpty(password))
				return null;
			LoginUser loginUser = dal.GetLoginUserByCardID(cardID, password);
			if (loginUser != null)
			{
				FunctionRightBLL functionRightBLL = new FunctionRightBLL();
				loginUser.FunctionRights = functionRightBLL.GetFunctionRightsByRoleID(loginUser.RoleID, functiontype);
			}
			return loginUser;
		}

		public LoginUser GetLoginUserByOrgID(int orgID,string userID, string password, int functiontype)
		{
			if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
			{
				return null;
			}

			LoginUser loginUser = dal.GetLoginUserByOrgID(orgID, userID, password);

			if (loginUser != null)
			{
				FunctionRightBLL functionRightBLL = new FunctionRightBLL();
				loginUser.FunctionRights = functionRightBLL.GetFunctionRightsByRoleID(loginUser.RoleID, functiontype);
			}

			return loginUser;
		}
    }
}
