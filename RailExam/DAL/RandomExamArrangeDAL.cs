using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Xml;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class RandomExamArrangeDAL
    {
         private static Hashtable _ormTable;
        private int _recordCount = 0;

        static RandomExamArrangeDAL()
        {
            _ormTable = new Hashtable();
            _ormTable.Add("randomexamarrangeid", "random_Exam_Arrange_Id");
            _ormTable.Add("randomexamid", "random_Exam_ID");          
            _ormTable.Add("userids", "User_Ids");          
            _ormTable.Add("memo", "MEMO");
        }

        //public int AddRandomExamArrange(RandomExamArrange randomExamArrange)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();

        //    string sqlCommand = "USP_random_Exam_Arrange_I";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        //    db.AddOutParameter(dbCommand, "p_random_Exam_Arrange_Id", DbType.Int32, 4);
        //    db.AddInParameter(dbCommand, "p_random_Exam_ID", DbType.Int32, randomExamArrange.RandomExamId);
        //    db.AddInParameter(dbCommand, "p_User_Ids", DbType.String, randomExamArrange.UserIds);
        //    db.AddInParameter(dbCommand, "p_memo", DbType.String, randomExamArrange.Memo);

        //    int nRecordAffected = db.ExecuteNonQuery(dbCommand);
        //    randomExamArrange.RandomExamArrangeId = (int)db.GetParameterValue(dbCommand, "p_random_Exam_Arrange_Id");         

        //    return nRecordAffected;
        //}

        public int AddRandomExamArrange(RandomExamArrange randomExamArrange)
        {
            XmlDocument doc = new XmlDocument();
            //Request.PhysicalApplicationPath取得config文件路径
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string  value = node.Value;
            int id=0;
            if(value =="Oracle" )
            {
                OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                OracleParameter para2 = new OracleParameter("p_random_Exam_Arrange_Id", OracleType.Number);
                para2.Direction = ParameterDirection.Output;
                OracleParameter para3 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
                para3.Value = randomExamArrange.RandomExamId;
                OracleParameter para4 = new OracleParameter("p_memo", OracleType.NVarChar);
                para4.Value = randomExamArrange.Memo;
                IDataParameter[] paras = new IDataParameter[] { para1, para2,para3,para4 };
                id =
                    RunAddProcedure(false,"USP_random_Exam_Arrange_I", paras,
                                    System.Text.Encoding.Unicode.GetBytes(randomExamArrange.UserIds));
            }
            return id;
        }

		public int AddRandomExamArrangeToServer(RandomExamArrange randomExamArrange)
		{
			XmlDocument doc = new XmlDocument();
			//Request.PhysicalApplicationPath取得config文件路径
			doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
			XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
			string value = node.Value;
			int id = 0;
			if (value == "Oracle")
			{
				OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
				OracleParameter para2 = new OracleParameter("p_random_Exam_Arrange_Id", OracleType.Number);
				para2.Direction = ParameterDirection.Output;
				OracleParameter para3 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
				para3.Value = randomExamArrange.RandomExamId;
				OracleParameter para4 = new OracleParameter("p_memo", OracleType.NVarChar);
				para4.Value = randomExamArrange.Memo;
				IDataParameter[] paras = new IDataParameter[] { para1, para2, para3, para4 };
				id =
					RunAddProcedure(true,"USP_random_Exam_Arrange_I_Ser", paras,
									System.Text.Encoding.Unicode.GetBytes(randomExamArrange.UserIds));
			}

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_random_Exam_Arrange_R");
            db.ExecuteNonQuery(dbCommand);

			return id;
		}

        /// <summary>
        /// 执行带有clob,blob,nclob大对象参数类型的存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tempbuff"></param>
        private int RunAddProcedure(bool isToCenter,string storedProcName, IDataParameter[] parameters, byte[] tempbuff)
        {
			string constring = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;

			if (isToCenter)
			{
				constring = ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString;
			} 
			OracleConnection Connection = new OracleConnection(constring);

            Connection.Open();
            OracleTransaction tx = Connection.BeginTransaction();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.Transaction = tx;
            string type = " declare ";
            type = type + " xx  Clob;";
            string createtemp = type + " begin ";
            createtemp = createtemp + " dbms_lob.createtemporary(xx, false, 0); ";
            string setvalue = "";
            setvalue = setvalue + ":templob := xx;";
            cmd.CommandText = createtemp + setvalue + " end;";
            cmd.Parameters.Add(new OracleParameter("templob", OracleType.Clob)).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            OracleLob tempLob = (OracleLob)cmd.Parameters["templob"].Value;
            tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
            int abc = tempbuff.Length;

            double b = abc / 2;
            double a = Math.Ceiling(b);
            abc = (int)(a * 2);
            tempLob.Write(tempbuff, 0, abc);
            tempLob.EndBatch();
            parameters[0].Value = tempLob;

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
            Connection.Close();
            return id;
        }



        //public int UpdateRandomExamArrange(int RandomExamId, string strUserIds)
        //{            
        //    Database db = DatabaseFactory.CreateDatabase();
        //    string sqlCommand = "USP_random_Exam_Arrange_U";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    db.AddInParameter(dbCommand, "p_random_Exam_ID", DbType.Int32, RandomExamId);
        //    db.AddInParameter(dbCommand, "p_User_Ids", DbType.String, strUserIds);
        //    return db.ExecuteNonQuery(dbCommand);
        //}

        public void UpdateRandomExamArrange(int RandomExamId, string strUserIds)
        {
            XmlDocument doc = new XmlDocument();
            //Request.PhysicalApplicationPath取得config文件路径
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string value = node.Value;
            if (value == "Oracle")
            {
                OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                OracleParameter para2 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
                para2.Value = RandomExamId;
                IDataParameter[] paras = new IDataParameter[] { para1, para2 };
               RunUpdateProcedure(false,"USP_random_Exam_Arrange_U", paras, System.Text.Encoding.Unicode.GetBytes(strUserIds));
            }
        }

		public void UpdateRandomExamArrangeToServer(int RandomExamId, string strUserIds)
		{
			XmlDocument doc = new XmlDocument();
			//Request.PhysicalApplicationPath取得config文件路径
			doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
			XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
			string value = node.Value;
			if (value == "Oracle")
			{
				OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
				OracleParameter para2 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
				para2.Value = RandomExamId;
				IDataParameter[] paras = new IDataParameter[] { para1, para2 };
				RunUpdateProcedure(true,"USP_random_Exam_Arrange_U", paras, System.Text.Encoding.Unicode.GetBytes(strUserIds));
			}


            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_random_Exam_Arrange_R");
            db.ExecuteNonQuery(dbCommand);
		}

        public void UpdateRandomExamArrangeDetailToServer(int RandomExamDetailId, string strUserIds)
        {
            XmlDocument doc = new XmlDocument();
            //Request.PhysicalApplicationPath取得config文件路径
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string value = node.Value;
            if (value == "Oracle")
            {
                OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                OracleParameter para2 = new OracleParameter("p_detail_id", OracleType.Number);
                para2.Value = RandomExamDetailId;
                IDataParameter[] paras = new IDataParameter[] { para1, para2 };
                RunUpdateProcedure(true, "USP_random_Exam_Arrange_D_U", paras, System.Text.Encoding.Unicode.GetBytes(strUserIds));
            }


            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_random_Exam_Arrange_R");
            db.ExecuteNonQuery(dbCommand);
        }

        public void RefreshRandomExamArrange()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_random_Exam_Arrange_R");
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 执行带有clob,blob,nclob大对象参数类型的存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tempbuff"></param>
        public void RunUpdateProcedure(bool isToCenter,string storedProcName, IDataParameter[] parameters, byte[] tempbuff)
        {
			string constring = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;

			if(isToCenter)
			{
				constring = ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString;
			}

            OracleConnection Connection = new OracleConnection(constring);

            Connection.Open();
            OracleTransaction tx = Connection.BeginTransaction();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.Transaction = tx;
            string type = " declare ";
            type = type + " xx  Clob;";
            string createtemp = type + " begin ";
            createtemp = createtemp + " dbms_lob.createtemporary(xx, false, 0); ";
            string setvalue = "";
            setvalue = setvalue + ":templob := xx;";
            cmd.CommandText = createtemp + setvalue + " end;";
            cmd.Parameters.Add(new OracleParameter("templob", OracleType.Clob)).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            OracleLob tempLob = (OracleLob)cmd.Parameters["templob"].Value;
            tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
            int abc = tempbuff.Length;

            double b = abc / 2;
            double a = Math.Ceiling(b);
            abc = (int)(a * 2);
            tempLob.Write(tempbuff, 0, abc);
            tempLob.EndBatch();
            parameters[0].Value = tempLob;

            cmd.Parameters.Clear();
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            cmd.ExecuteNonQuery();
            tx.Commit();
            Connection.Close();
        }


        public IList<RandomExamArrange> GetRandomExamArranges(int RandomExamId)
        {
            IList<RandomExamArrange> examArranges = new List<RandomExamArrange>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_Exam_Arrange_q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_Exam_ID", DbType.Int32, RandomExamId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examArranges.Add(CreateModelObject(dataReader));
                }
            }

            return examArranges;
        }

		public void DeleteRandomExamArrangeByRandomExamID(int ExamId)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Arrange_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, ExamId);

			db.ExecuteNonQuery(dbCommand);
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

        public static RandomExamArrange CreateModelObject(IDataReader dataReader)
        {
            return new RandomExamArrange(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamArrangeId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamId")]),           
                DataConvert.ToString(dataReader[GetMappingFieldName("UserIds")]),               
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
