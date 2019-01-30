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
    public class ExamDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static ExamDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("examid", "Exam_ID");
            _ormTable.Add("categoryid", "Category_ID");
            _ormTable.Add("orgid", "Org_Id");
            _ormTable.Add("examtypeid", "Exam_Type_Id");
            _ormTable.Add("typename", "type_Name");
            _ormTable.Add("categoryname", "Category_NAME");
            _ormTable.Add("examname", "Exam_Name");
            _ormTable.Add("examtime", "Exam_Time");
            _ormTable.Add("begintime", "Begin_Time");
            _ormTable.Add("endtime", "End_Time");
            _ormTable.Add("exammodeid", "Exam_Mode_Id");
            _ormTable.Add("minexamtimes", "Min_Exam_Times");
            _ormTable.Add("maxexamtimes", "Max_Exam_Times");
            _ormTable.Add("converttotalscore", "Convert_Total_Score");
            _ormTable.Add("passscore", "Pass_Score");
            _ormTable.Add("autosaveinterval", "Auto_Save_Interval");
            _ormTable.Add("isundercontrol", "Is_Under_Control");
            _ormTable.Add("isautoscore", "Is_Auto_Score");
            _ormTable.Add("canseeanswer", "Can_See_Answer");
            _ormTable.Add("canseescore", "Can_See_Score");
            _ormTable.Add("ispublicscore", "Is_Public_Score");
            _ormTable.Add("statusid", "status_Id");
            _ormTable.Add("createperson", "create_Person");
            _ormTable.Add("createtime", "create_Time");
            _ormTable.Add("description", "description");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("paperid", "paper_Id");
            _ormTable.Add("createmode", "Create_Mode");
            _ormTable.Add("examineecount", "EXAMINEE_COUNT");
            _ormTable.Add("examaveragescore", "EXAM_AVERAGE_SCORE");
            _ormTable.Add("examtype","Exam_Type");
            _ormTable.Add("downloaded","DOWNLOADED");
			_ormTable.Add("examstylename","EXAM_STYLE_NAME");
			_ormTable.Add("startmodename","START_MODE_NAME");
        }

        public IList<Exam> GetNowExam()
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Now";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exams.Add(exam);
                }
            }

            return exams;
        }

        public IList<Exam> GetComingExam()
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Coming";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exams.Add(exam);
                }
            }

            return exams;
        }

        public IList<Exam> GetHistoryExam()
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_History";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exams.Add(exam);
                }
            }

            return exams;
        }

        public IList<Exam> GetTopExams(string EmployeeID)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Top";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_JUDGE_ID", DbType.String, EmployeeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exams.Add(exam);
                }
            }

            return exams;
        }


        public IList<Exam> GetExamsInfoDesktop(int orgID)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Desktop";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exam.ExamType = Convert.ToInt32(dataReader[GetMappingFieldName("ExamType")].ToString());
                    exams.Add(exam);
                }
            }

            return exams;
        }


        public IList<Exam> GetExamByUserId(string UserID,int orgID,int serverNo)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_By_user";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_User_id", DbType.Int32, Convert.ToInt32(UserID));
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_server_no", DbType.Int32, serverNo);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exam.ExamType = Convert.ToInt32(dataReader[GetMappingFieldName("ExamType")].ToString());
                	exam.ExamStyleName = dataReader[GetMappingFieldName("ExamStyleName")].ToString();
					exam.StartModeName = dataReader[GetMappingFieldName("StartModeName")].ToString();
					exams.Add(exam);
                }
            }

            return exams;
        }

        public IList<Exam> GetComingExamByUserId(string UserID)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_By_user_C";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_User_id", DbType.String, UserID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exams.Add(exam);
                }
            }

            return exams;
        }

        public IList<Exam> GetHistoryExamByUserId(string UserID)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_By_user_h";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_User_id", DbType.String, UserID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exams.Add(exam);
                }
            }

            return exams;
        }

        public Exam GetExam(int ExamId)
        {
            Exam exam = null;

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "USP_Exam_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Exam_id", DbType.Int32, ExamId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    exam = CreateModelObject(dataReader);
                    exam.Downloaded = Convert.ToInt32(dataReader[GetMappingFieldName("Downloaded")].ToString());
                }
            }

            return exam;
        }

        public IList<Exam> GetExamByCategoryID(int CategoryId)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Category_id", DbType.Int32, CategoryId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exams.Add(exam);
                }
            }

            return exams;
        }

        public IList<Exam> GetExamByExamCategoryIDPath(string ExamCategoryIDPath, string ExamName, int CreateMode, string CreatePerson, int orgId)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_ByCondition";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, ExamCategoryIDPath);
            db.AddInParameter(dbCommand, "p_exam_name", DbType.String, ExamName);
            db.AddInParameter(dbCommand, "p_create_mode", DbType.Int32, CreateMode);
            db.AddInParameter(dbCommand, "p_create_person", DbType.String, CreatePerson);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgId);
            

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Exam exam = CreateModelObject(dataReader);
                    exams.Add(exam);
                }
            }

            return exams;
        }

        /// <summary>
        /// 根据考试类型ID取考试信息
        /// </summary>
        /// <param name="examTypeId"></param>
        /// <returns></returns>
        public IList<Exam> GetExamsByExamTypeID(int examTypeId)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "USP_EXAM_GRADE_LIST_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_type_id", DbType.Int32, examTypeId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                Exam exam = null;
                while (dataReader.Read())
                {
                    exam = CreateModelObject(dataReader);
                    exam.ExamineeCount = (int)dataReader[GetMappingFieldName("ExamineeCount")];

                    exams.Add(exam);
                }
            }

            return exams;
        }

        /// <summary>
        /// 根据考试类型ID、考试名称、开始时间、结束时间获取考试信息
        /// </summary>
        /// <param name="examTypeId"></param>
        /// <param name="examName"></param>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public IList<Exam> GetExams(int examTypeId, string examName, DateTime beginDateTime, DateTime endDateTime)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_GRADE_LIST_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_type_id", DbType.Int32, examTypeId);
            db.AddInParameter(dbCommand, "p_exam_name", DbType.String, examName);
            if (beginDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, beginDateTime);
            }
            if(endDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, endDateTime);
            }

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                Exam exam = null;
                while (dataReader.Read())
                {
                    exam = CreateModelObject(dataReader);
                    exam.ExamineeCount = int.Parse(dataReader[GetMappingFieldName("ExamineeCount")].ToString());                   
                    exam.ExamAverageScore = (decimal)dataReader[GetMappingFieldName("ExamAverageScore")];
                  
                    exams.Add(exam);
                }
            }

            return exams;
        }


        public IList<Exam> GetExamsByOrgID(string examName, DateTime beginDateTime, DateTime endDateTime,int orgID)
        {
            IList<Exam> exams = new List<Exam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_GRADE_LIST_ORG";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_type_id", DbType.Int32, 1);
            if(examName != null)
            {
                db.AddInParameter(dbCommand, "p_exam_name", DbType.String, examName);
            }
            if (beginDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, beginDateTime);
            }
            if (endDateTime > DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, endDateTime);
            }
            db.AddInParameter(dbCommand,"p_org_id",DbType.Int32,orgID);
            db.AddOutParameter(dbCommand, "p_net_name", DbType.String,50);
            db.AddOutParameter(dbCommand,"p_count",DbType.Int32,4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                Exam exam = null;
                while (dataReader.Read())
                {
                    exam = CreateModelObject(dataReader);
                    exam.ExamineeCount = int.Parse(dataReader[GetMappingFieldName("ExamineeCount")].ToString());
                    exam.ExamAverageScore = (decimal)dataReader[GetMappingFieldName("ExamAverageScore")];
                    exam.ExamType = Convert.ToInt32(dataReader[GetMappingFieldName("ExamType")].ToString());
                    exams.Add(exam);
                }
            }

            return exams;
        }

		public IList<Exam> GetExamsInfoByOrgID(string examName,int CategoryId, DateTime beginDateTime, DateTime endDateTime, int orgID, string isServerCenter)
        {
            IList<Exam> exams = new List<Exam>();

		    try
		    {
                Database db = DatabaseFactory.CreateDatabase();

                string sqlCommand;
                //路局服务器  或  次服务器（有两台服务器）
                if (Convert.ToBoolean(isServerCenter))
                {
                    sqlCommand = "USP_EXAM_GRADE_LIST_ORG1";
                }
                else
                {
                    sqlCommand = "USP_EXAM_GRADE_LIST_ORG_Ser";
                }

                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "p_exam_type_id", DbType.Int32, 1);
                if (examName != null)
                {
                    db.AddInParameter(dbCommand, "p_exam_name", DbType.String, examName);
                }
                else
                {
                    db.AddInParameter(dbCommand, "p_exam_name", DbType.String, string.Empty);
                }

                db.AddInParameter(dbCommand, "p_categoryId", DbType.Int32, CategoryId);

                if (beginDateTime > DateTime.MinValue)
                {
                    db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, beginDateTime);
                }
                if (endDateTime > DateTime.MinValue)
                {
                    db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, endDateTime);
                }
                db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
                //db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    Exam exam = null;
                    while (dataReader.Read())
                    {
                        exam = CreateModelObject(dataReader);
                        exam.ExamineeCount = int.Parse(dataReader[GetMappingFieldName("ExamineeCount")].ToString());
                        exam.ExamAverageScore = (decimal)dataReader[GetMappingFieldName("ExamAverageScore")];
                        exam.ExamType = Convert.ToInt32(dataReader[GetMappingFieldName("ExamType")].ToString());
                        exams.Add(exam);
                    }
                }
		    }
		    catch		    
            {
		    }
            return exams;
        }

		/// <summary>
		/// 返回考试列表
		/// </summary>
		/// <param name="OrgID">站段ID</param>
		/// <param name="DateFrom">考试开始时间</param>
		/// <param name="DateTo">考试结束时间</param>
		/// <returns></returns>
		public IList<Exam> GetListWithOrg(int OrgID, DateTime DateFrom, DateTime DateTo,int style)
		{
			IList<Exam> objList = new List<Exam>();
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EXAM_GRADE_Org_List";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "p_random_exam_orgid", DbType.Int32, OrgID);
			db.AddInParameter(dbCommand, "p_random_exam_dateFrom", DbType.DateTime, DateFrom);
			db.AddInParameter(dbCommand, "p_random_exam_dateTo", DbType.DateTime, DateTo);
            db.AddInParameter(dbCommand, "p_exam_style", DbType.Int32, style);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Exam exam = CreateModelObject(dataReader);
					exam.ExamineeCount = int.Parse(dataReader[GetMappingFieldName("ExamineeCount")].ToString());
					exam.ExamAverageScore = (decimal)dataReader[GetMappingFieldName("ExamAverageScore")];
					exam.ExamType = Convert.ToInt32(dataReader[GetMappingFieldName("ExamType")].ToString());
                    exam.ExamStyleName = dataReader[GetMappingFieldName("ExamStyleName")].ToString();
					objList.Add(exam);
				}
			}
			return objList;
		}

		public IList<Exam> GetIsNotComputerExamsInfo(int orgID)
		{
			IList<Exam> exams = new List<Exam>();

			Database db = DatabaseFactory.CreateDatabase();


			string sqlCommand;

			sqlCommand = "USP_EXAM_G_IsNot_ComputerExam";

			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				Exam exam = null;
				while (dataReader.Read())
				{
					exam = CreateModelObject(dataReader);
					exam.ExamineeCount = int.Parse(dataReader[GetMappingFieldName("ExamineeCount")].ToString());
					exam.ExamAverageScore = (decimal)dataReader[GetMappingFieldName("ExamAverageScore")];
					exam.ExamType = Convert.ToInt32(dataReader[GetMappingFieldName("ExamType")].ToString());
					exams.Add(exam);
				}
			}

			return exams;
		}

        public void UpdateExamPaper(int ExamId, int PaperId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Paper_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, ExamId);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, PaperId);
            db.AddInParameter(dbCommand, "p_ORDER_INDEX", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_EXAM_PAPER_TYPE_ID", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, "");

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
        }

        public int AddExam(Exam exam)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_exam_id", DbType.Int32, exam.ExamId);
            db.AddInParameter(dbCommand, "p_Category_id", DbType.Int32, exam.CategoryId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, exam.OrgId);
            db.AddInParameter(dbCommand, "p_exam_type_id", DbType.Int32, exam.ExamTypeId);
            db.AddInParameter(dbCommand, "p_exam_name", DbType.String, exam.ExamName);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, exam.ExamTime);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, exam.BeginTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, exam.EndTime);
            db.AddInParameter(dbCommand, "p_exam_mode_id", DbType.Int32, exam.ExamModeId);
            db.AddInParameter(dbCommand, "p_min_exam_times", DbType.Int32, exam.MinExamTimes);
            db.AddInParameter(dbCommand, "p_max_exam_times", DbType.Int32, exam.MaxExamTimes);
            db.AddInParameter(dbCommand, "p_convert_total_score", DbType.Decimal, exam.ConvertTotalScore);
            db.AddInParameter(dbCommand, "p_pass_score", DbType.Decimal, exam.PassScore);
            db.AddInParameter(dbCommand, "p_auto_save_interval", DbType.Int32, exam.AutoSaveInterval);
            db.AddInParameter(dbCommand, "p_is_under_control", DbType.Int32, exam.IsUnderControl ? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_auto_score", DbType.Int32, exam.IsAutoScore ? 1 : 0);
            db.AddInParameter(dbCommand, "p_can_see_answer", DbType.Int32, exam.CanSeeAnswer ? 1 : 0);
            db.AddInParameter(dbCommand, "p_can_see_score", DbType.Int32, exam.CanSeeScore ? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_public_score", DbType.Int32, exam.IsPublicScore ? 1 : 0);
            db.AddInParameter(dbCommand, "p_description", DbType.String, exam.Description);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, exam.StatusId);
            db.AddInParameter(dbCommand, "p_create_person", DbType.String, exam.CreatePerson);
            db.AddInParameter(dbCommand, "p_create_time", DbType.DateTime, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, exam.Memo);

            int id = 0;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Exam_id"));

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return id;
        }

        public void UpdateExam(Exam exam)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, exam.ExamId);
            //db.AddInParameter(dbCommand, "p_Category_id", DbType.Int32, exam.CategoryId);
            //db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, exam.OrgId);
            db.AddInParameter(dbCommand, "p_exam_type_id", DbType.Int32, exam.ExamTypeId);
            db.AddInParameter(dbCommand, "p_exam_name", DbType.String, exam.ExamName);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, exam.ExamTime);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, exam.BeginTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, exam.EndTime);
            db.AddInParameter(dbCommand, "p_exam_mode_id", DbType.Int32, exam.ExamModeId);
            db.AddInParameter(dbCommand, "p_min_exam_times", DbType.Int32, exam.MinExamTimes);
            db.AddInParameter(dbCommand, "p_max_exam_times", DbType.Int32, exam.MaxExamTimes);
            db.AddInParameter(dbCommand, "p_convert_total_score", DbType.Decimal, exam.ConvertTotalScore);
            db.AddInParameter(dbCommand, "p_pass_score", DbType.Decimal, exam.PassScore);
            db.AddInParameter(dbCommand, "p_auto_save_interval", DbType.Int32, exam.AutoSaveInterval);
            db.AddInParameter(dbCommand, "p_is_under_control", DbType.Int32, exam.IsUnderControl ? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_auto_score", DbType.Int32, exam.IsAutoScore ? 1 : 0);
            db.AddInParameter(dbCommand, "p_can_see_answer", DbType.Int32, exam.CanSeeAnswer ? 1 : 0);
            db.AddInParameter(dbCommand, "p_can_see_score", DbType.Int32, exam.CanSeeScore ? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_public_score", DbType.Int32, exam.IsPublicScore ? 1 : 0);
            db.AddInParameter(dbCommand, "p_description", DbType.String, exam.Description);
            //db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, exam.StatusId);
            //db.AddInParameter(dbCommand, "p_create_person", DbType.String, exam.CreatePerson);
            //db.AddInParameter(dbCommand, "p_create_time", DbType.DateTime, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, exam.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
        }

        public void DeleteExam(int ExamId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Exam_id", DbType.Int32, ExamId);

            db.ExecuteNonQuery(dbCommand);
        }

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

        public static Exam CreateModelObject(IDataReader dataReader)
        {
            return new Exam(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrgId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("AutoSaveInterval")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsUnderControl")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("categoryId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTime")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsAutoScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTypeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("typeName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamModeId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("MinExamTimes")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("CanSeeAnswer")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("CanSeeScore")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsPublicScore")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("PassScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("MaxExamTimes")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("description")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("EndTime")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("ConvertTotalScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("statusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("createperson")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("createtime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("memo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CreateMode")]));
        }
    }
}
