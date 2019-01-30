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
    public class ExamArrangeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static ExamArrangeDAL()
        {
            _ormTable = new Hashtable();
            _ormTable.Add("examarrangeid", "Exam_Arrange_Id");
            _ormTable.Add("examid", "Exam_Id");
            _ormTable.Add("paperid", "Paper_Id");
            _ormTable.Add("orderindex", "order_Index");
            _ormTable.Add("begintime", "Begin_Time");
            _ormTable.Add("endtime", "End_Time");
            _ormTable.Add("userids", "User_Ids");
            _ormTable.Add("judgeids", "Judge_Ids");
            _ormTable.Add("memo", "MEMO");
        }

        //public int AddExamArrange(ExamArrange examArrange)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();

        //    string sqlCommand = "USP_Exam_Arrange_I";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        //    db.AddOutParameter(dbCommand, "p_Exam_Arrange_Id", DbType.Int32, 4);
        //    db.AddInParameter(dbCommand, "p_Exam_Id", DbType.Int32, examArrange.ExamId);
        //    db.AddInParameter(dbCommand, "p_paper_Id", DbType.Int32, examArrange.PaperId);
        //    db.AddInParameter(dbCommand, "p_order_Index", DbType.Int32, examArrange.OrderIndex);
        //    db.AddInParameter(dbCommand, "p_Begin_Time", DbType.DateTime, examArrange.BeginTime);
        //    db.AddInParameter(dbCommand, "p_End_Time", DbType.DateTime, examArrange.EndTime);
        //    db.AddInParameter(dbCommand, "p_User_Ids", DbType.String, examArrange.UserIds);
        //    db.AddInParameter(dbCommand, "p_Judge_Ids", DbType.String, examArrange.JudgeIds);
        //    db.AddInParameter(dbCommand, "p_memo", DbType.String, examArrange.Memo);

        //    int nRecordAffected = db.ExecuteNonQuery(dbCommand);
        //    examArrange.ExamArrangeId = (int)db.GetParameterValue(dbCommand, "p_Exam_Arrange_Id");         

        //    return nRecordAffected;
        //}

        public int AddExamArrange(ExamArrange examArrange)
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
                OracleParameter para2 = new OracleParameter("p_Exam_Arrange_Id", OracleType.Number);
                para2.Direction = ParameterDirection.Output;
                OracleParameter para3 = new OracleParameter("p_Exam_Id", OracleType.Number);
                para3.Value = examArrange.ExamId;
                OracleParameter para4 = new OracleParameter("p_paper_Id", OracleType.Number);
                para4.Value = examArrange.PaperId;
                OracleParameter para5 = new OracleParameter("p_order_Index", OracleType.Number);
                para5.Value = examArrange.OrderIndex;
                OracleParameter para6 = new OracleParameter("p_Begin_Time", OracleType.DateTime);
                para6.Value = examArrange.BeginTime;
                OracleParameter para7 = new OracleParameter("p_End_Time", OracleType.DateTime);
                para7.Value = examArrange.EndTime;
                OracleParameter para8 = new OracleParameter("p_Judge_Ids", OracleType.NVarChar);
                para8.Value = examArrange.JudgeIds;
                OracleParameter para9 = new OracleParameter("p_memo", OracleType.NVarChar);
                para9.Value = examArrange.Memo;
                IDataParameter[] paras = new IDataParameter[] { para1, para2, para3, para4, para5, para6, para7, para8,para9 };
                id =
                    RunAddProcedure("USP_Exam_Arrange_I", paras,
                                    System.Text.Encoding.Unicode.GetBytes(examArrange.UserIds));
            }
            return id;
        }

        /// <summary>
        /// 执行带有clob,blob,nclob大对象参数类型的存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tempbuff"></param>
        private int RunAddProcedure(string storedProcName, IDataParameter[] parameters, byte[] tempbuff)
        {
            string constring = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;
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

        public int DeleteExamArrange(int ExamArrangeId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Arrange_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Exam_Arrange_id", DbType.Int32, ExamArrangeId);

            return db.ExecuteNonQuery(dbCommand);
        }

        //public void UpdateExamArrangeUser(int ExamArrangeId, string strUserIds)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();

        //    DbConnection connection = db.CreateConnection();
        //    connection.Open();
        //    DbTransaction transaction = connection.BeginTransaction();

        //    try
        //    {
        //        string sqlCommand = "USP_Exam_Arrange_UserId_U";
        //        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        //        db.AddInParameter(dbCommand, "p_Exam_Arrange_Id", DbType.Int32, ExamArrangeId);
        //        db.AddInParameter(dbCommand, "p_User_Ids", DbType.String, strUserIds);
        //        db.ExecuteNonQuery(dbCommand, transaction);
        //        transaction.Commit();
        //    }
        //    catch
        //    {
        //        transaction.Rollback();
        //    }
        //    connection.Close();
        //}

        public void UpdateExamArrangeUser(int ExamArrangeId, string strUserIds)
        {
            XmlDocument doc = new XmlDocument();
            //Request.PhysicalApplicationPath取得config文件路径
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string value = node.Value;
            if (value == "Oracle")
            {
                OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                OracleParameter para2 = new OracleParameter("p_Exam_Arrange_Id", OracleType.Number);
                para2.Value = ExamArrangeId;
                IDataParameter[] paras = new IDataParameter[] { para1, para2 };
                RunUpdateProcedure("USP_Exam_Arrange_UserId_U", paras, System.Text.Encoding.Unicode.GetBytes(strUserIds));
            }
        }

        public void UpdateExamArrangeJudge(int ExamArrangeId, string strJudgeIds)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sqlCommand = "USP_Exam_Arrange_JudgeId_U";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "p_Exam_Arrange_Id", DbType.Int32, ExamArrangeId);
                
                db.AddInParameter(dbCommand, "p_Judge_Ids", DbType.String, strJudgeIds);
                db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();
        }

        public void UpdateExamArrange(IList<ExamArrange> examArranges)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            int nExamId = 0;
           
            try
            {
                foreach (ExamArrange examArrange in examArranges)
                {
                    string sqlCommand = "USP_Exam_Arrange_U";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                    nExamId = examArrange.ExamId;

                    db.AddInParameter(dbCommand, "p_Exam_Arrange_Id", DbType.Int32, examArrange.ExamArrangeId);
                    db.AddInParameter(dbCommand, "p_Begin_Time", DbType.DateTime, examArrange.BeginTime);
                    db.AddInParameter(dbCommand, "p_End_Time", DbType.DateTime, examArrange.EndTime);
                    db.AddInParameter(dbCommand, "p_User_Ids", DbType.String, examArrange.UserIds);
                    db.AddInParameter(dbCommand, "p_Judge_Ids", DbType.String, examArrange.JudgeIds);
                    db.ExecuteNonQuery(dbCommand, transaction);
                }               

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();
        }

        //public int UpdateExamArrange(ExamArrange examArrange)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();

        //    string sqlCommand = "USP_Exam_Arrange_U";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        //    db.AddInParameter(dbCommand, "p_Exam_Arrange_Id", DbType.Int32, examArrange.ExamArrangeId);
        //    db.AddInParameter(dbCommand, "p_Begin_Time", DbType.DateTime, examArrange.BeginTime);
        //    db.AddInParameter(dbCommand, "p_End_Time", DbType.DateTime, examArrange.EndTime);
        //    db.AddInParameter(dbCommand, "p_User_Ids", DbType.String, examArrange.UserIds);
        //    db.AddInParameter(dbCommand, "p_Judge_Ids", DbType.String, examArrange.JudgeIds);
             
        //    return db.ExecuteNonQuery(dbCommand);
        //}

        public void UpdateExamArrange(ExamArrange examArrange)
        {
            XmlDocument doc = new XmlDocument();
            //Request.PhysicalApplicationPath取得config文件路径
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string value = node.Value;
            if (value == "Oracle")
            {
                OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                OracleParameter para2 = new OracleParameter("p_Exam_Arrange_Id", OracleType.Number);
                para2.Value = examArrange.ExamArrangeId;
                OracleParameter para3 = new OracleParameter("p_Begin_Time", OracleType.DateTime);
                para3.Value = examArrange.BeginTime;
                OracleParameter para4 = new OracleParameter("p_End_Time", OracleType.DateTime);
                para4.Value = examArrange.EndTime;
                OracleParameter para5 = new OracleParameter("p_Judge_Ids", OracleType.NVarChar);
                para5.Value = examArrange.JudgeIds;
                IDataParameter[] paras = new IDataParameter[] { para1, para2,para3,para4,para5 };
                RunUpdateProcedure("USP_Exam_Arrange_U", paras, System.Text.Encoding.Unicode.GetBytes(examArrange.UserIds));
            }
        }

        //public int UpdateExamUser(int examID, string strUserIds)
        //{            

        //    Database db = DatabaseFactory.CreateDatabase();

        //    string sqlCommand = "USP_Exam_Arrange_Exam_U";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        //    db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examID);
        //    db.AddInParameter(dbCommand, "p_User_Ids", DbType.String, strUserIds);

        //    return db.ExecuteNonQuery(dbCommand);
        //}

        public void UpdateExamUser(int examID, string strUserIds)
        {
            XmlDocument doc = new XmlDocument();
            //Request.PhysicalApplicationPath取得config文件路径
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string value = node.Value;
            if (value == "Oracle")
            {
                OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                OracleParameter para2 = new OracleParameter("p_exam_id", OracleType.Number);
                para2.Value = examID;
                IDataParameter[] paras = new IDataParameter[] { para1, para2 };
                RunUpdateProcedure("USP_Exam_Arrange_Exam_U", paras, System.Text.Encoding.Unicode.GetBytes(strUserIds));
            }
        }

        /// <summary>
        /// 执行带有clob,blob,nclob大对象参数类型的存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tempbuff"></param>
        private void RunUpdateProcedure(string storedProcName, IDataParameter[] parameters, byte[] tempbuff)
        {
            string constring = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;
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




        public ExamArrange  GetExamArrange(int ExamArrangeId)
        {
            ExamArrange item = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Arrange_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Exam_Arrange_id", DbType.Int32, ExamArrangeId);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    item = CreateModelObject(dataReader);
                    break;
                }
            }

            return item;
        }

        public IList<ExamArrange> GetExamArrangesByExamId(int ExamId)
        {
            IList<ExamArrange> examArranges = new List<ExamArrange>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Arrange_q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Exam_id", DbType.Int32, ExamId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examArranges.Add(CreateModelObject(dataReader));
                }
            }

            return examArranges;
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

        public static ExamArrange CreateModelObject(IDataReader dataReader)
        {
            return new ExamArrange(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamArrangeId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("EndTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("UserIds")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeIds")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
