using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;

namespace RailExamWebApp.Common.Class
{
	/// <summary>
	/// DBI ��ժҪ˵����
	/// </summary>
	public class DBI
	{
		#region ��Ա����

		/// <summary>
		/// ���ݿ������ַ���
		/// </summary>
		private static string m_strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString;

		/// <summary>
		/// ���ݿ�����
		/// </summary>
		private SqlConnection m_connection = null;

		/// <summary>
		/// ���ݿ�����
		/// </summary>
		private SqlTransaction m_transaction = null;

		/// <summary>
		/// ����������
		/// </summary>
		private bool m_bInTransaction = false;

		/// <summary>
		/// ��������ģʽ
		/// </summary>
		public enum DdlNonDataRow {None, EmptyRowWithEmptyValue, EmptyRowWithZeroValue, TipRowWithEmptyValue, TipRowWithZeroValue};

		#endregion

		#region ���캯��

		public DBI()
		{
		}

		#endregion

		#region ����
		
		public SqlConnection Connection
		{
			get
			{
				return m_connection;
			}
			set
			{
				m_connection = value;
			}
		}

		public SqlTransaction Transaction
		{
			get
			{
				return m_transaction;
			}
			set
			{
				m_transaction = value;
			}
		}

		#endregion

		#region ����

		/// <summary>
		/// ��Ĭ�����ݿ�����
		/// </summary>
		/// <returns></returns>
		public static SqlConnection OpenConnection()
		{
			return OpenConnection(m_strConnString);
		}

		public static SqlConnection OpenConnectionForMWDP()
		{
			SqlConnection connection = null;

			try
			{
				connection = new SqlConnection(m_strConnString);				
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return connection;
		}


		/// <summary>
		/// ��ָ�������ַ��������ݿ�����
		/// </summary>
		/// <param name="strConnString"></param>
		/// <returns></returns>
		public static SqlConnection OpenConnection(string strConnString)
		{
			SqlConnection connection = null;

			try
			{
				connection = new SqlConnection(m_strConnString);
				connection.Open();
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return connection;
		}

		public void OpenConnectionForReader()
		{
			m_connection = new SqlConnection(m_strConnString);
			m_connection.Open();
		}

		public void CloseConnectionForReader()
		{
			if(m_connection != null)
			{
				if(m_connection.State == ConnectionState.Open)
��				{
����				m_connection.Close();
��				}

				m_connection.Dispose();
				m_connection = null;
			}

		}

		/// <summary>
		/// ��ֻ����ʽִ��һ����ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public SqlDataReader ExecuteReader(string strSql)
		{
			SqlDataReader reader;

			try
			{
				reader = SqlHelper.ExecuteReader(m_connection, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return reader;
		}

		/// <summary>
		/// ִ��һ����ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public static DataSet ExecuteQuery(string strSql)
		{
			SqlConnection connection = null;
			DataSet ds;

			try
			{
				connection = OpenConnection();

				ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(connection != null)
				{
					if(connection.State == ConnectionState.Open)
��					{
����					connection.Close();
��					}

					connection.Dispose();
				}
			}

			return ds;
		}

		/// <summary>
		/// ִ��һ����ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public static DataSet ExecuteQuery(string strSql, string strTableName)
		{
			SqlConnection connection = null;
			DataSet ds;

			try
			{
				connection = OpenConnection();

				ds = SqlHelper.ExecuteDataset(connection, CommandType.Text, strSql);
				ds.Tables[0].TableName = strTableName;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(connection != null)
				{
					if(connection.State == ConnectionState.Open)
��					{
����					connection.Close();
��					}

					connection.Dispose();
				}
			}

			return ds;
		}

		/// <summary>
		/// ִ��һ����ѯSQL��䣬���ص�һ�е�һ�е��ַ���ֵ
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public static object ExecuteScalar(string strSql)
		{
			SqlConnection connection = null;
			object objValue;

			try
			{
				connection = OpenConnection();

				objValue = SqlHelper.ExecuteScalar(connection, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(connection != null)
				{
					if(connection.State == ConnectionState.Open)
��					{
����					connection.Close();
��					}

					connection.Dispose();
				}
			}

			return objValue;
		}

		/// <summary>
		/// ִ��һ���ǲ�ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		public static int ExecuteNonQuery(string strSql)
		{
			SqlConnection connection = null;
			int nAffectedRecordCount;

			try
			{
				connection = OpenConnection();

				nAffectedRecordCount = SqlHelper.ExecuteNonQuery(connection, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(connection != null)
				{
					if(connection.State == ConnectionState.Open)
��					{
����					connection.Close();
��					}

					connection.Dispose();
				}
			}

			return nAffectedRecordCount;
		}

		public static bool FillDataTable(DataSet ds, string strTableName, string strSql)
		{
			SqlConnection connection = null;
			bool bSuccess = true;

			try
			{
				connection = OpenConnection();

				SqlHelper.FillDataset(connection, CommandType.Text, strSql, ds, new string[]{strTableName});
			}
			catch(Exception ex)
			{
				bSuccess = false;
				throw ex;
			}
			finally
			{
				if(connection != null)
				{
					if(connection.State == ConnectionState.Open)
��					{
����					connection.Close();
��					}

					connection.Dispose();
				}
			}

			return bSuccess;
		}

		/// <summary>
		/// ִ�ж����ǲ�ѯSQL���
		/// </summary>
		/// <param name="alSql"></param>
		public static int ExecuteNonQuery(ArrayList alSql)
		{
			SqlConnection connection = null;
			int nAffectedRecordCount = 0;

			try
			{
				connection = OpenConnection();

				foreach(string strSql in alSql)
				{
					nAffectedRecordCount += SqlHelper.ExecuteNonQuery(connection, CommandType.Text, strSql);
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(connection != null)
				{
					if(connection.State == ConnectionState.Open)
��					{
����					connection.Close();
��					}

					connection.Dispose();
				}
			}

			return nAffectedRecordCount;
		}

		/// <summary>
		/// ����һ��������ִ�ж����ǲ�ѯSQL���
		/// </summary>
		/// <param name="alSql"></param>
		public static int ExecuteMultiNonQueryInTrans(ArrayList alSql)
		{
			SqlConnection connection = null;
			SqlTransaction transaction = null;
			int nAffectedRecordCount = 0;

			try
			{
				connection = OpenConnection();
				transaction = connection.BeginTransaction();

				foreach(string strSql in alSql)
				{
					nAffectedRecordCount += SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, strSql);
				}

				transaction.Commit();
			}
			catch(Exception ex)
			{
				if(transaction != null)
				{
					transaction.Rollback();
				}

				throw ex;
			}
			finally
			{
				if(connection != null)
				{
					if(connection.State == ConnectionState.Open)
��					{
����					connection.Close();
��					}

					connection.Dispose();
				}
			}

			return nAffectedRecordCount;
		}

		/// <summary>
		/// ��Ĭ�����ݿ����ӣ�����ʼ������
		/// </summary>
		/// <returns></returns>
		public bool Trans_OpenConnection()
		{
			return Trans_OpenConnection(m_strConnString);
		}

		/// <summary>
		/// ��ָ�������ַ��������ݿ����ӣ�����ʼ������
		/// </summary>
		/// <param name="strConnString">���ݿ������ַ���</param>
		/// <returns></returns>
		public bool Trans_OpenConnection(string strConnString)
		{
			try
			{
				m_connection = new SqlConnection(strConnString);
				m_connection.Open();

				m_transaction = m_connection.BeginTransaction();
				m_bInTransaction = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return m_bInTransaction;
		}

		/// <summary>
		/// ����������ִ��һ����ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public DataSet Trans_ExecuteQuery(string strSql)
		{
			if(! m_bInTransaction)
			{
				return null;
			}

			DataSet ds;

			try
			{
				ds = SqlHelper.ExecuteDataset(m_transaction, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return ds;
		}

		/// <summary>
		/// ����������ִ��һ����ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public object Trans_ExecuteScalar(string strSql)
		{
			if(! m_bInTransaction)
			{
				return null;
			}

			object objValue;

			try
			{
				objValue = SqlHelper.ExecuteScalar(m_transaction, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}

			if(objValue is System.DBNull)
			{
				objValue = null;
			}

			return objValue;
		}

		/// <summary>
		/// ����������ִ��һ���ǲ�ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		public int Trans_ExecuteNonQuery(string strSql)
		{
			if(! m_bInTransaction)
			{
				return 0;
			}

			int nAffectedRecordCount;

			try
			{
				nAffectedRecordCount = SqlHelper.ExecuteNonQuery(m_transaction, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				//				m_transaction.Rollback();
				throw ex;
			}

			return nAffectedRecordCount;
		}

		public bool Trans_Commit()
		{
			if(! m_bInTransaction)
			{
				return false;
			}

			try
			{
				m_transaction.Commit();
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return true;
		}

		public bool Trans_Rollback()
		{
			if(! m_bInTransaction)
			{
				return false;
			}

			try
			{
				m_transaction.Rollback();
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return true;
		}

		public bool Trans_CloseConnection()
		{
			if(! m_bInTransaction)
			{
				return false;
			}

			if(m_connection != null)
			{
				if(m_connection.State == ConnectionState.Open)
��				{
����				m_connection.Close();
��				}

				m_connection.Dispose();
				m_connection = null;
			}

			m_bInTransaction = false;

			return true;
		}

		/// <summary>
		/// ��Ĭ�����ݿ����ӣ�׼����ͬһ�����ݿ������п�ʼ������
		/// </summary>
		/// <returns></returns>
		public bool Batch_OpenConnection()
		{
			return Batch_OpenConnection(m_strConnString);
		}

		/// <summary>
		/// ��ָ�������ַ��������ݿ����ӣ�׼����ͬһ�����ݿ������п�ʼ������
		/// </summary>
		/// <param name="strConnString">���ݿ������ַ���</param>
		/// <returns></returns>
		public bool Batch_OpenConnection(string strConnString)
		{
			bool bSuccess = true;

			try
			{
				m_connection = new SqlConnection(strConnString);
				m_connection.Open();
			}
			catch(Exception ex)
			{
				bSuccess = false;
				throw ex;
			}

			return bSuccess;
		}

		/// <summary>
		/// ����������ִ��һ����ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public DataSet Batch_ExecuteQuery(string strSql)
		{
			if(m_connection == null || m_connection.State != ConnectionState.Open)
			{
				return null;
			}

			DataSet ds;

			try
			{
				ds = SqlHelper.ExecuteDataset(m_connection, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return ds;
		}

		/// <summary>
		/// ����������ִ��һ����ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public object Batch_ExecuteScalar(string strSql)
		{
			if(m_connection == null || m_connection.State != ConnectionState.Open)
			{
				return null;
			}

			object objValue;

			try
			{
				objValue = SqlHelper.ExecuteScalar(m_connection, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}

			if(objValue is System.DBNull)
			{
				objValue = null;
			}

			return objValue;
		}

		/// <summary>
		/// ����������ִ��һ���ǲ�ѯSQL���
		/// </summary>
		/// <param name="strSql"></param>
		public int Batch_ExecuteNonQuery(string strSql)
		{
			if(m_connection == null || m_connection.State != ConnectionState.Open)
			{
				return 0;
			}

			int nAffectedRecordCount;

			try
			{
				nAffectedRecordCount = SqlHelper.ExecuteNonQuery(m_connection, CommandType.Text, strSql);
			}
			catch(Exception ex)
			{
				throw ex;
			}

			return nAffectedRecordCount;
		}

		/// <summary>
		/// ���������йر����ݿ�����
		/// </summary>
		public void Batch_CloseConnection()
		{
			if(m_connection != null)
			{
				if(m_connection.State == ConnectionState.Open)
��				{
����				m_connection.Close();
��				}

				m_connection.Dispose();
				m_connection = null;
			}
		}

		/// <summary>
		/// ��ȡ���е�ȫ������
		/// </summary>
		/// <param name="TableName">����</param>
		/// <returns></returns>
		public static ArrayList GetTableColumns(string strTableName)
		{
			ArrayList arColumnName = null;

			string strSql = "select syscolumns.name from syscolumns left join sysobjects on syscolumns.id = sysobjects.id where sysobjects.name='" + strTableName + "'";

			DataSet ds = ExecuteQuery(strSql);
			
			for(int i = 0; i < GetRowCount(ds); i ++)
			{
				arColumnName.Add(GetString(ds, i, "name"));
			}

			return arColumnName;
		}

		/// <summary>
		/// ��ȡ���ݼ�����
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public static int GetRowCount(DataSet ds)
		{
			return ds.Tables[0].Rows.Count;
		}

		/// <summary>
		/// ��ȡ���ݼ���һ�е�һ���ֶ�ֵ������object
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public static object GetValue(DataSet ds)
		{
			return GetValue(ds, 0, 0);
		}

		/// <summary>
		/// ��ȡ���ݼ���һ��ĳ�ֶ�ֵ������object
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static object GetValue(DataSet ds, string strFieldName)
		{
			return GetValue(ds, 0, strFieldName);
		}

		/// <summary>
		/// ��ȡ���ݼ�ĳһ��ĳ�ֶ�ֵ������object
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="nRowIndex"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static object GetValue(DataSet ds, int nRowIndex, string strFieldName)
		{
			if(nRowIndex >= ds.Tables[0].Rows.Count)
			{
				return null;
			}

			return ds.Tables[0].Rows[nRowIndex][strFieldName];
		}

		public static object GetValue(DataSet ds, int nRowIndex, int nFieldIndex)
		{
			if(nRowIndex >= ds.Tables[0].Rows.Count
				|| ds.Tables[0].Rows[nRowIndex].ItemArray.Length <= nFieldIndex)
			{
				return null;
			}

			return ds.Tables[0].Rows[nRowIndex][nFieldIndex];
		}

		/// <summary>
		/// ��ȡ���ݼ���һ�е�һ���ֶ�ֵ������string
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public static string GetString(DataSet ds)
		{
			object obj = GetValue(ds);
			if(obj == null)
			{
				return string.Empty;
			}

			return GetValue(ds).ToString();
		}

		/// <summary>
		/// ��ȡ���ݼ���һ��ĳ�ֶ�ֵ������string
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static string GetString(DataSet ds, string strFieldName)
		{
			return GetValue(ds, 0, strFieldName).ToString();
		}

		/// <summary>
		/// ��ȡ���ݼ�ĳһ��ĳ�ֶ�ֵ������string
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="nRowIndex"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static string GetString(DataSet ds, int nRowIndex, string strFieldName)
		{
			return GetValue(ds, nRowIndex, strFieldName).ToString();
		}

		/// <summary>
		/// ��ȡ���ݼ���һ�е�һ�ֶ�ֵ������int
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public static int GetInt(DataSet ds)
		{
			return (int)GetValue(ds);
		}

		/// <summary>
		/// ��ȡ���ݼ���һ��ĳ�ֶ�ֵ������int
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static int GetInt(DataSet ds, string strFieldName)
		{
			return (int)GetValue(ds, 0, strFieldName);
		}

		/// <summary>
		/// ��ȡ���ݼ�ĳһ��ĳ�����ֶ�ֵ������int
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="nRowIndex"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static int GetInt(DataSet ds, int nRowIndex, string strFieldName)
		{
			return (int)GetValue(ds, nRowIndex, strFieldName);
		}

		/// <summary>
		/// ��ȡ���ݼ���һ�е�һ�ֶ�ֵ������decimal
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public static decimal GetDecimal(DataSet ds)
		{
			return (decimal)GetValue(ds);
		}

		/// <summary>
		/// ��ȡ���ݼ���һ��ĳ�ֶ�ֵ������decimal
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static decimal GetDecimal(DataSet ds, string strFieldName)
		{
			return (decimal)GetValue(ds, 0, strFieldName);
		}

		/// <summary>
		/// ��ȡ���ݼ�ĳһ��ĳ�����ֶ�ֵ������decimal
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="nRowIndex"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static decimal GetDecimal(DataSet ds, int nRowIndex, string strFieldName)
		{
			return (decimal)GetValue(ds, nRowIndex, strFieldName);
		}

		/// <summary>
		/// ��ȡ���ݼ���һ�е�һ�ֶ�ֵ������DateTime
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public static DateTime GetDateTime(DataSet ds)
		{
			return (DateTime)GetValue(ds);
		}

		/// <summary>
		/// ��ȡ���ݼ���һ��ĳ�ֶ�ֵ������DateTime
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static DateTime GetDateTime(DataSet ds, string strFieldName)
		{
			return (DateTime)GetValue(ds, 0, strFieldName);
		}

		/// <summary>
		/// ��ȡ���ݼ�ĳһ��ĳ�����ֶ�ֵ������DateTime
		/// </summary>
		/// <param name="ds"></param>
		/// <param name="nRowIndex"></param>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public static DateTime GetDateTime(DataSet ds, int nRowIndex, string strFieldName)
		{
			return (DateTime)GetValue(ds, nRowIndex, strFieldName);
		}

		#endregion
	}
}
