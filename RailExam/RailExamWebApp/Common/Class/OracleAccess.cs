using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Data.OracleClient;

namespace RailExamWebApp.Common.Class
{
	public class OracleAccess
	{
		private OracleConnection  objConnection  = null;
        private OracleCommand     objCommand     = null;
        private OracleDataAdapter objDataAdapter = null;
        private DataSet objDataSet = null;
		private string _strConnection = string.Empty;

		public OracleAccess()
        {
        }

		public OracleAccess(string strConnection)
		{
			_strConnection = strConnection;
		}

        private void Open()
        {
			if (objConnection == null && _strConnection == string.Empty)
            {
				string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;
                _strConnection = strConn;
				objConnection = new OracleConnection(strConn);
                objConnection.Open();
            }
			else
			{
				string strConn = _strConnection;
				objConnection = new OracleConnection(strConn);
				objConnection.Open();
			}

            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
        }

        public void Close()
        {
            if (objConnection != null)
                objConnection.Close();
        }

        public DataSet RunSqlDataSet(string sql)
        {
            Open();
			objDataAdapter = new OracleDataAdapter(sql, objConnection);
            objDataSet = new DataSet();
            objDataAdapter.Fill(objDataSet);
            Close();
            return objDataSet;
        }

        public void ExecuteNonQuery(string strSql)
        {
            Open();
            OracleTransaction trans = objConnection.BeginTransaction();
            try
            {
				objCommand = new OracleCommand(strSql, objConnection);
                objCommand.CommandType = CommandType.Text;
            	objCommand.Transaction = trans;
                objCommand.ExecuteNonQuery();
                trans.Commit();
                Close();
            }
            catch (OracleException ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

		public void ExecuteNonQuery(ArrayList objList)
		{
			Open();
			OracleTransaction trans = objConnection.BeginTransaction();

			try
			{
				for (int i = 0; i < objList.Count; i++)
				{
					objCommand = new OracleCommand(objList[i].ToString(), objConnection);
					objCommand.CommandType = CommandType.Text;
					objCommand.Transaction = trans;
					objCommand.ExecuteNonQuery();
				}
				trans.Commit();
				
			}
			catch (OracleException ex)
			{
				trans.Rollback();
				throw ex;
			}
			finally
			{
				Close();
			}
		}

		public void ExecuteNonQueryPro(string storedProcName, IDataParameter[] parameters)
		{
			try
			{
                Open();
                OracleTransaction tx = objConnection.BeginTransaction();
                OracleCommand cmd = objConnection.CreateCommand();
                cmd.Transaction = tx;

                cmd.Parameters.Clear();
                cmd.CommandText = storedProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (OracleParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
                cmd.ExecuteNonQuery();
                int id = Convert.ToInt32(cmd.Parameters[1].Value);
                tx.Commit();
                Close();
			}
			catch (OracleException ex)
			{
				throw ex;
			}
		}

        public int GetCount(string strName, string strType)
        {
            string strSql = "select   count(*) "
                            + " from   user_objects "
                            + " where   object_type   = '" + strType + "' "
                            + " and  object_name='" + strName + "'";
            DataSet ds = RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

	}
}
