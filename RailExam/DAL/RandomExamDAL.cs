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
    public class RandomExamDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static RandomExamDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("randomexamid", "Random_Exam_Id");
            _ormTable.Add("categoryid", "Category_ID");
            _ormTable.Add("orgid", "Org_Id");
            _ormTable.Add("examtypeid", "Exam_Type_Id");             
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
            _ormTable.Add("examineecount", "EXAMINEE_COUNT");
            _ormTable.Add("examaveragescore", "EXAM_AVERAGE_SCORE");
            _ormTable.Add("downloaded","DOWNLOADED");
            _ormTable.Add("startmode","START_MODE");
            _ormTable.Add("isstart","IS_START");
            _ormTable.Add("haspaper","HAS_PAPER");
            _ormTable.Add("startmodename","START_MODE_NAME");
            _ormTable.Add("isstartname","IS_START_NAME");
			_ormTable.Add("randomexamcode","RANDOM_EXAM_CODE");
			_ormTable.Add("version","VERSION");
			_ormTable.Add("examstyle","EXAM_STYLE");
			_ormTable.Add("examstylename","EXAM_STYLE_NAME");
			_ormTable.Add("iscomputerexam", "IS_COMPUTEREXAM");
			_ormTable.Add("postid","POST_ID");
			_ormTable.Add("techniciantypeid","TECHNICIAN_TYPE_ID");
			_ormTable.Add("isgroupleader","IS_GROUP_LEADER");
			_ormTable.Add("isupload", "IS_UPLOAD");
			_ormTable.Add("hastrainclass","HAS_TRAIN_CLASS");
			_ormTable.Add("isreset","IS_RESET");
            _ormTable.Add("randomexammodulartypeid", "RANDOM_EXAM_MODULAR_TYPE_ID");
            _ormTable.Add("isreduceerror", "IS_REDUCE_ERROR");
            _ormTable.Add("savestatus","SAVE_STATUS");
            _ormTable.Add("savedate","SAVE_DATE");
            _ormTable.Add("haspaperdetail","HASPAPERDETAIL");

        }

        public IList<RandomExam> GetExamByExamCategoryIDPath(string ExamCategoryIDPath, string ExamName, string CreatePerson, int orgId, int ExamTimeType,int ExamStyleID,int railSystemID)
        {
            IList<RandomExam> exams = new List<RandomExam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_ByCondition";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, ExamCategoryIDPath);
            db.AddInParameter(dbCommand, "p_exam_name", DbType.String, ExamName);            
            db.AddInParameter(dbCommand, "p_create_person", DbType.String, CreatePerson);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgId);
            db.AddInParameter(dbCommand, "p_time_type", DbType.Int32, ExamTimeType);
			db.AddInParameter(dbCommand, "p_exam_style", DbType.Int32, ExamStyleID);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, railSystemID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExam exam = CreateModelObject(dataReader);
					exam.Downloaded = Convert.ToInt32(dataReader[GetMappingFieldName("Downloaded")].ToString());
					exam.StartMode = Convert.ToInt32(dataReader[GetMappingFieldName("StartMode")].ToString());
					exam.IsStart = Convert.ToInt32(dataReader[GetMappingFieldName("IsStart")].ToString());
					exam.HasPaper = dataReader[GetMappingFieldName("HasPaper")].ToString() == "1" ? true : false;
					exam.RandomExamCode = dataReader[GetMappingFieldName("RandomExamCode")].ToString();
					exam.Version = Convert.ToInt32(dataReader[GetMappingFieldName("Version")].ToString());
					exam.ExamStyle = Convert.ToInt32(dataReader[GetMappingFieldName("ExamStyle")].ToString());
					exam.ExamStyleName = dataReader[GetMappingFieldName("ExamStyleName")].ToString();
					exam.IsComputerExam = dataReader[GetMappingFieldName("IsComputerExam")].ToString() == "1" ? true : false;
					exam.PostID = dataReader[GetMappingFieldName("PostID")].ToString();
					exam.TechnicianTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("TechnicianTypeID")].ToString());
					exam.IsGroupLeader = Convert.ToInt32(dataReader[GetMappingFieldName("IsGroupLeader")].ToString());
                    exam.RandomExamModularTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamModularTypeID")].ToString());
                    exam.IsReduceError = dataReader[GetMappingFieldName("IsReduceError")].ToString() == "1" ? true : false;
					exams.Add(exam);
                }
            }

            return exams;
        }

        public RandomExam GetExam(int ExamId)
        {
            RandomExam exam = null;

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "USP_Random_Exam_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_Exam_id", DbType.Int32, ExamId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    exam = CreateModelObject(dataReader);
                    exam.Downloaded = Convert.ToInt32(dataReader[GetMappingFieldName("Downloaded")].ToString());
                    exam.StartMode = Convert.ToInt32(dataReader[GetMappingFieldName("StartMode")].ToString());
                    exam.IsStart = Convert.ToInt32(dataReader[GetMappingFieldName("IsStart")].ToString());
                    exam.HasPaper = dataReader[GetMappingFieldName("HasPaper")].ToString() == "1" ? true:false;
                	exam.RandomExamCode = dataReader[GetMappingFieldName("RandomExamCode")].ToString();
					exam.Version = Convert.ToInt32(dataReader[GetMappingFieldName("Version")].ToString());
					exam.ExamStyle = Convert.ToInt32(dataReader[GetMappingFieldName("ExamStyle")].ToString());
					exam.IsComputerExam = dataReader[GetMappingFieldName("IsComputerExam")].ToString() == "1" ? true : false;
					exam.PostID = dataReader[GetMappingFieldName("PostID")].ToString();
					exam.TechnicianTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("TechnicianTypeID")].ToString());
					exam.IsGroupLeader = Convert.ToInt32(dataReader[GetMappingFieldName("IsGroupLeader")].ToString());
					exam.IsUpload = dataReader[GetMappingFieldName("IsUpload")].ToString() == "1" ? true : false;
					exam.HasTrainClass = dataReader[GetMappingFieldName("HasTrainClass")].ToString() == "1" ? true : false;
					exam.IsReset = dataReader[GetMappingFieldName("IsReset")].ToString() == "1" ? true : false;
                    exam.RandomExamModularTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamModularTypeID")].ToString());
                    exam.IsReduceError = dataReader[GetMappingFieldName("IsReduceError")].ToString() == "1" ? true : false;
                    exam.SaveStatus = Convert.ToInt32(dataReader[GetMappingFieldName("SaveStatus")].ToString());
                    if(dataReader[GetMappingFieldName("SaveDate")] == DBNull.Value)
                    {
                        exam.SaveDate = null;
                    }
                    else
                    {
                        exam.SaveDate = Convert.ToDateTime(dataReader[GetMappingFieldName("SaveDate")].ToString());
                    }
                }
            }

            return exam;
        }


		public RandomExam GetRandomExamServer(int ExamId)
		{
			RandomExam exam = null;

			Database db = DatabaseFactory.CreateDatabase();
			string sqlCommand = "USP_Random_Exam_G_Server";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_Random_Exam_id", DbType.Int32, ExamId);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
					exam = CreateModelObject(dataReader);
					exam.Downloaded = Convert.ToInt32(dataReader[GetMappingFieldName("Downloaded")].ToString());
					exam.StartMode = Convert.ToInt32(dataReader[GetMappingFieldName("StartMode")].ToString());
					exam.IsStart = Convert.ToInt32(dataReader[GetMappingFieldName("IsStart")].ToString());
					exam.HasPaper = dataReader[GetMappingFieldName("HasPaper")].ToString() == "1" ? true : false;
					exam.RandomExamCode = dataReader[GetMappingFieldName("RandomExamCode")].ToString();
					exam.Version = Convert.ToInt32(dataReader[GetMappingFieldName("Version")].ToString());
					exam.ExamStyle = Convert.ToInt32(dataReader[GetMappingFieldName("ExamStyle")].ToString());
					exam.IsComputerExam = dataReader[GetMappingFieldName("IsComputerExam")].ToString() == "1" ? true : false;
					exam.PostID = dataReader[GetMappingFieldName("PostID")].ToString();
					exam.TechnicianTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("TechnicianTypeID")].ToString());
					exam.IsGroupLeader = Convert.ToInt32(dataReader[GetMappingFieldName("IsGroupLeader")].ToString());
                    exam.RandomExamModularTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamModularTypeID")].ToString());
                    exam.IsReduceError = dataReader[GetMappingFieldName("IsReduceError")].ToString() == "1" ? true : false;
				}
			}

			return exam;
		}

        public IList<RandomExam> GetSaveExam(string strWhereClause)
        {
            IList<RandomExam> exams = new List<RandomExam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_G_Save";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_where", DbType.String, strWhereClause);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExam exam = CreateModelObject(dataReader);
                    exam = CreateModelObject(dataReader);
                    exam.Downloaded = Convert.ToInt32(dataReader[GetMappingFieldName("Downloaded")].ToString());
                    exam.StartMode = Convert.ToInt32(dataReader[GetMappingFieldName("StartMode")].ToString());
                    exam.IsStart = Convert.ToInt32(dataReader[GetMappingFieldName("IsStart")].ToString());
                    exam.HasPaper = dataReader[GetMappingFieldName("HasPaper")].ToString() == "1" ? true : false;
                    exam.RandomExamCode = dataReader[GetMappingFieldName("RandomExamCode")].ToString();
                    exam.Version = Convert.ToInt32(dataReader[GetMappingFieldName("Version")].ToString());
                    exam.ExamStyle = Convert.ToInt32(dataReader[GetMappingFieldName("ExamStyle")].ToString());
                    exam.ExamineeCount = Convert.ToInt32(dataReader[GetMappingFieldName("ExamineeCount")].ToString());
                    exam.ExamAverageScore = Convert.ToDecimal(dataReader[GetMappingFieldName("ExamAverageScore")].ToString());
                    exam.ExamStyleName = dataReader[GetMappingFieldName("ExamStyleName")].ToString();
                    exam.IsComputerExam = dataReader[GetMappingFieldName("IsComputerExam")].ToString() == "1" ? true : false;
                    exam.PostID = dataReader[GetMappingFieldName("PostID")].ToString();
                    exam.TechnicianTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("TechnicianTypeID")].ToString());
                    exam.IsGroupLeader = Convert.ToInt32(dataReader[GetMappingFieldName("IsGroupLeader")].ToString());
                    exam.IsUpload = dataReader[GetMappingFieldName("IsUpload")].ToString() == "1" ? true : false;
                    exam.HasTrainClass = dataReader[GetMappingFieldName("HasTrainClass")].ToString() == "1" ? true : false;
                    exam.IsReset = dataReader[GetMappingFieldName("IsReset")].ToString() == "1" ? true : false;
                    exam.RandomExamModularTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamModularTypeID")].ToString());
                    exam.IsReduceError = dataReader[GetMappingFieldName("IsReduceError")].ToString() == "1" ? true : false;
                    exam.SaveStatus = Convert.ToInt32(dataReader[GetMappingFieldName("SaveStatus")].ToString());
                    if (dataReader[GetMappingFieldName("SaveDate")] == DBNull.Value)
                    {
                        exam.SaveDate = null;
                    }
                    else
                    {
                        exam.SaveDate = Convert.ToDateTime(dataReader[GetMappingFieldName("SaveDate")].ToString());
                    }
                    exams.Add(exam);
                }
            }

            return exams;
        }



        public int AddExam(RandomExam exam)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_random_exam_id", DbType.Int32, exam.RandomExamId);
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
            db.AddInParameter(dbCommand, "p_start_mode",DbType.Int32,exam.StartMode);
			db.AddInParameter(dbCommand, "p_exam_style", DbType.Int32, exam.ExamStyle);
			db.AddInParameter(dbCommand, "p_is_computerexam", DbType.Int32, exam.IsComputerExam ? 1 : 0);
			db.AddInParameter(dbCommand, "p_post_id", DbType.String, exam.PostID);
			db.AddInParameter(dbCommand, "p_technician_type_id", DbType.Int32,exam.TechnicianTypeID);
			db.AddInParameter(dbCommand, "p_is_group_leader",DbType.Int32,exam.IsGroupLeader);
			db.AddInParameter(dbCommand, "p_has_train_class", DbType.Int32, exam.HasTrainClass ? 1: 0);
            db.AddInParameter(dbCommand, "p_random_exam_modular_type_id", DbType.Int32, exam.RandomExamModularTypeID);
            db.AddInParameter(dbCommand, "p_is_reduce_error", DbType.Int32, exam.IsReduceError ? 1 : 0);
            db.AddInParameter(dbCommand, "p_save_status", DbType.Int32, exam.SaveStatus);
            db.AddInParameter(dbCommand, "p_save_date", DbType.DateTime, exam.SaveDate);

            int id = 0;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_random_exam_id"));

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

		public DateTime GetRandomExamTimeByExamID(int randomExamID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_G_Time";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
			db.AddOutParameter(dbCommand, "p_time", DbType.DateTime, 8);

			db.ExecuteNonQuery(dbCommand);

			DateTime date = Convert.ToDateTime(db.GetParameterValue(dbCommand, "p_time"));

			return date;
		}


        public void UpdateExam(RandomExam exam)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, exam.RandomExamId);      
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
            db.AddInParameter(dbCommand, "p_start_mode", DbType.Int32, exam.StartMode);
			db.AddInParameter(dbCommand, "p_exam_style", DbType.Int32, exam.ExamStyle);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, exam.Memo);
			db.AddInParameter(dbCommand, "p_is_computerexam", DbType.Int32, exam.IsComputerExam ? 1 : 0);
			db.AddInParameter(dbCommand, "p_post_id", DbType.String, exam.PostID);
			db.AddInParameter(dbCommand, "p_technician_type_id", DbType.Int32, exam.TechnicianTypeID);
			db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, exam.IsGroupLeader);
			db.AddInParameter(dbCommand, "p_has_train_class", DbType.Int32, exam.HasTrainClass ? 1 : 0);
            db.AddInParameter(dbCommand, "p_random_exam_modular_type_id", DbType.Int32, exam.RandomExamModularTypeID);
            db.AddInParameter(dbCommand, "p_is_reduce_error", DbType.Int32, exam.IsReduceError ? 1 : 0);
            db.AddInParameter(dbCommand, "p_save_status", DbType.Int32, exam.SaveStatus);
            db.AddInParameter(dbCommand, "p_save_date", DbType.DateTime, exam.SaveDate);
            db.AddInParameter(dbCommand, "p_category_id", DbType.Int32, exam.CategoryId);

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

            string sqlCommand = "USP_Random_Exam_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_Exam_id", DbType.Int32, ExamId);

            db.ExecuteNonQuery(dbCommand);
        }

		/// <summary>
		/// ÐÞ¸ÄÊÇ·ñÉú³ÉÊÔ¾í×Ö¶Î
		/// </summary>
		/// <param name="randomExamID"></param>
		/// <param name="b"></param>
        public void UpdateHasPaper(int randomExamID,int serverNo, bool b)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_U_Paper";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
            db.AddInParameter(dbCommand, "p_server_no", DbType.Int32, serverNo);
            db.AddInParameter(dbCommand, "p_has_paper", DbType.Int32, b ? 1 : 0);

            db.ExecuteNonQuery(dbCommand);
        }

		public void RefreshRandomExam()
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Refresh_Exam";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.ExecuteNonQuery(dbCommand);
		}

		/// <summary>
		/// ÐÞ¸ÄÊÇ¿ª¿¼Ä£Ê½×Ö¶Î
		/// </summary>
		/// <param name="randomExamID"></param>
		/// <param name="startMode"></param>
        public void UpdateStartMode(int randomExamID,  int startMode)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_U_Mode";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
            db.AddInParameter(dbCommand, "p_start_mode", DbType.Int32, startMode);

            db.ExecuteNonQuery(dbCommand);
        }

		/// <summary>
		/// ÐÞ¸ÄÊÇ¿¼ÊÔÑéÖ¤Âë×Ö¶Î
		/// </summary>
		/// <param name="randomExamID"></param>
		/// <param name="code"></param>
        public void UpdateStartCode(int randomExamID, int serverNo, string code)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_U_Code";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
            db.AddInParameter(dbCommand, "p_server_no", DbType.Int32, serverNo);
			db.AddInParameter(dbCommand, "p_code", DbType.String, code);

			db.ExecuteNonQuery(dbCommand);
		}

		public void StartExamBySecondServer(int randomExamID, string code)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_U_Code_Two";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
			db.AddInParameter(dbCommand, "p_code", DbType.String, code);

			db.ExecuteNonQuery(dbCommand);
		}

        public void RandomExamRefresh()
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_Refresh";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.ExecuteNonQuery(dbCommand);
        }

		/// <summary>
		/// ÐÞ¸ÄÊÇ·ñ¿ª¿¼×Ö¶Î
		/// </summary>
		/// <param name="randomExamID"></param>
		/// <param name="isStart"></param>
        public void UpdateIsStart(int randomExamID, int serverNo, int isStart)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_U_Start";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
            db.AddInParameter(dbCommand, "p_server_no", DbType.Int32, serverNo);
            db.AddInParameter(dbCommand, "p_is_start", DbType.Int32, isStart);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// ÐÞ¸ÄÊÇ·ñ¿ª¿¼×Ö¶Î
        /// </summary>
        /// <param name="randomExamID"></param>
        /// <param name="isStart"></param>
        public void UpdateStatusID(int randomExamID, int serverNo, int StatusID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_U_Status";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
            db.AddInParameter(dbCommand, "p_server_no", DbType.Int32, serverNo);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, StatusID);

            db.ExecuteNonQuery(dbCommand);
        }

		/// <summary>
		/// ÐÞ¸ÄÊÇ·ñÔÚ½áÊø¿¼ÊÔºóÉÏ´«×Ö¶Î
		/// </summary>
		/// <param name="randomExamID"></param>
		/// <param name="isUpload"></param>
        public void UpdateIsUpload(int randomExamID, int serverNo, int isUpload)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_U_UPLOAD";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
            db.AddInParameter(dbCommand, "p_server_no", DbType.Int32, serverNo);
			db.AddInParameter(dbCommand, "p_is_upload", DbType.Int32, isUpload);

			db.ExecuteNonQuery(dbCommand);
		}


		public void UpdateVersion(int randomExamID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_U_Version";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);

			db.ExecuteNonQuery(dbCommand);
		}


        public IList<RandomExam> GetRandomExamsInfo(int orgid)
        {
            IList<RandomExam> exams = new List<RandomExam>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_Exam_ByOrg";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExam exam = CreateModelObject(dataReader);
                    exam.StartMode = Convert.ToInt32(dataReader[GetMappingFieldName("StartMode")].ToString());
                    exam.IsStart = Convert.ToInt32(dataReader[GetMappingFieldName("IsStart")].ToString());
                    exam.HasPaper = dataReader[GetMappingFieldName("HasPaper")].ToString() == "1" ? true : false;
                    exam.StartModeName = dataReader[GetMappingFieldName("StartModeName")].ToString();
                    exam.IsStartName = dataReader[GetMappingFieldName("IsStartName")].ToString();
                    exam.RandomExamModularTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamModularTypeID")].ToString());
                    exam.IsReduceError = dataReader[GetMappingFieldName("IsReduceError")].ToString() == "1" ? true : false;
                    exams.Add(exam);
                }
            }

            return exams;
        }


		public IList<RandomExam> GetControlRandomExamsInfo(int orgid,int serverNo)
		{
			IList<RandomExam> exams = new List<RandomExam>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_ByOrg_Control";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgid);
            db.AddInParameter(dbCommand, "p_server_no", DbType.String, serverNo);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExam exam = CreateModelObject(dataReader);
					exam.StartMode = Convert.ToInt32(dataReader[GetMappingFieldName("StartMode")].ToString());
					exam.IsStart = Convert.ToInt32(dataReader[GetMappingFieldName("IsStart")].ToString());
					exam.HasPaper = dataReader[GetMappingFieldName("HasPaper")].ToString() == "1" ? true : false;
                    exam.HasPaperDetail = dataReader[GetMappingFieldName("HasPaperDetail")].ToString() == "1" ? true : false;
					exam.StartModeName = dataReader[GetMappingFieldName("StartModeName")].ToString();
					exam.IsStartName = dataReader[GetMappingFieldName("IsStartName")].ToString();
					exam.ExamStyleName = dataReader[GetMappingFieldName("ExamStyleName")].ToString();
                    exam.RandomExamModularTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamModularTypeID")].ToString());
                    exam.IsReduceError = dataReader[GetMappingFieldName("IsReduceError")].ToString() == "1" ? true : false;
					exams.Add(exam);
				}
			}

			return exams;
		}


		public IList<RandomExam> GetOverdueNotEndRandomExam(int orgid)
		{
			IList<RandomExam> exams = new List<RandomExam>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Overdue_NotEnd";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgid);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExam exam = CreateModelObject(dataReader);
					exam.StartMode = Convert.ToInt32(dataReader[GetMappingFieldName("StartMode")].ToString());
					exam.IsStart = Convert.ToInt32(dataReader[GetMappingFieldName("IsStart")].ToString());
					exam.HasPaper = dataReader[GetMappingFieldName("HasPaper")].ToString() == "1" ? true : false;
					exam.StartModeName = dataReader[GetMappingFieldName("StartModeName")].ToString();
					exam.IsStartName = dataReader[GetMappingFieldName("IsStartName")].ToString();
                    exam.RandomExamModularTypeID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamModularTypeID")].ToString());
                    exam.IsReduceError = dataReader[GetMappingFieldName("IsReduceError")].ToString() == "1" ? true : false;
					exams.Add(exam);
				}
			}

			return exams;
		}

		public void AddCopyRandomExam(int examId,string examName)
		{
			string strPlace = GetRailName();

			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				string sqlCommand = "USP_Random_Exam_I_Copy";
				DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

				db.AddOutParameter(dbCommand, "v_random_exam_id", DbType.Int32, 4);
				db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examId);
				db.AddInParameter(dbCommand, "p_exam_name", DbType.String, examName);
				db.ExecuteNonQuery(dbCommand, transaction);

				int examID = Convert.ToInt32(db.GetParameterValue(dbCommand, "v_random_exam_id"));

				IList<RandomExamSubject> objSubjects = new List<RandomExamSubject>();
				sqlCommand = "USP_Random_Exam_SUBJECT_Q";
				dbCommand = db.GetStoredProcCommand(sqlCommand);

				db.AddInParameter(dbCommand, "p_Random_Exam_Id", DbType.Int32, examId);

				using (IDataReader dataReader = db.ExecuteReader(dbCommand,transaction))
				{
					while (dataReader.Read())
					{
						RandomExamSubject objSubject = RandomExamSubjectDAL.SubjectNamelObject(dataReader);
						objSubjects.Add(objSubject);
					}
				}

				foreach (RandomExamSubject subject in objSubjects)
				{
					sqlCommand = "USP_random_exam_subject_I";
					dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddInParameter(dbCommand, "p_random_exam_ID", DbType.Int32, examID);
					db.AddOutParameter(dbCommand, "p_random_exam_Subject_Id", DbType.Int32, 4);
					db.AddInParameter(dbCommand, "p_Order_Index", DbType.String, subject.OrderIndex);
					db.AddInParameter(dbCommand, "p_Item_Count", DbType.Int32, subject.ItemCount);
					db.AddInParameter(dbCommand, "p_Item_Type_Id", DbType.Int32, subject.ItemTypeId);
					db.AddInParameter(dbCommand, "p_Remark", DbType.String, subject.Remark);
					db.AddInParameter(dbCommand, "p_Subject_Name", DbType.String, subject.SubjectName);
					db.AddInParameter(dbCommand, "p_Total_Score", DbType.Decimal, subject.TotalScore);
					db.AddInParameter(dbCommand, "p_Unit_Score", DbType.Decimal, subject.UnitScore);
					db.AddInParameter(dbCommand, "p_memo", DbType.String, subject.Memo);

					db.ExecuteNonQuery(dbCommand, transaction);
					int subjectID = (int)db.GetParameterValue(dbCommand, "p_random_exam_subject_Id");

					IList<RandomExamStrategy> objStrategyList = new List<RandomExamStrategy>();
					string sqlCommand1= "USP_Random_Exam_Strategy_Q";
					DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

					db.AddInParameter(dbCommand1, "p_subject_id", DbType.Int32, subject.RandomExamSubjectId);
					db.AddInParameter(dbCommand1, "p_range_id", DbType.Int32, 0);
					db.AddInParameter(dbCommand1, "p_range_type", DbType.Int32, 0);

					using (IDataReader dataReader = db.ExecuteReader(dbCommand1,transaction))
					{
						while (dataReader.Read())
						{
							RandomExamStrategy item = RandomExamStrategyDAL.CreateModelObject(dataReader);
							if (strPlace == "¹þ¶û±õ")
							{
								item.MaxItemDifficultyID = Convert.ToInt32(dataReader[RandomExamStrategyDAL.GetMappingFieldName("MaxItemDifficultyID")].ToString());
								item.MaxItemDifficultyName = dataReader[RandomExamStrategyDAL.GetMappingFieldName("MaxItemDifficultyName")].ToString();
							}
							objStrategyList.Add(item);
						}
					}

					foreach (RandomExamStrategy strategy in objStrategyList)
					{
						string sqlCommand2 = "USP_Random_Exam_Strategy_I_Out";
						DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand2);

						db.AddOutParameter(dbCommand2, "p_Random_Exam_Strategy_Id", DbType.Int32, 4);
						db.AddInParameter(dbCommand2, "p_subject_id", DbType.Int32, subjectID);
						db.AddInParameter(dbCommand2, "p_Range_type", DbType.Int32, strategy.RangeType);
						db.AddInParameter(dbCommand2, "p_Range_id", DbType.Int32, strategy.RangeId);
						db.AddInParameter(dbCommand2, "p_item_type_id", DbType.Int32, strategy.ItemTypeId);
						db.AddInParameter(dbCommand2, "p_range_name", DbType.String, strategy.RangeName);
						db.AddInParameter(dbCommand2, "p_exclude_chapters_id", DbType.String, strategy.ExcludeChapterId);
						db.AddInParameter(dbCommand2, "p_memo", DbType.String, strategy.Memo);
						db.AddInParameter(dbCommand2, "p_Item_Count", DbType.Int32, strategy.ItemCount);
						db.AddInParameter(dbCommand2, "p_Item_diff", DbType.Int32, strategy.ItemDifficultyID);
                        db.AddInParameter(dbCommand2, "p_is_mother_item", DbType.Int32, strategy.IsMotherItem ? 1 : 0);
                        db.AddInParameter(dbCommand2, "p_mother_id", DbType.Int32, strategy.MotherID);

						if (strPlace == "¹þ¶û±õ")
						{
							db.AddInParameter(dbCommand2, "p_max_Item_diff", DbType.Int32, strategy.MaxItemDifficultyID);
						}

						db.ExecuteNonQuery(dbCommand2, transaction);

						int strategyID = (int)db.GetParameterValue(dbCommand2, "p_Random_Exam_Strategy_Id");

                        string sqlCommand5 = "USP_RANDOM_EXAM_Item_Select_C";
                        DbCommand dbCommand5 = db.GetStoredProcCommand(sqlCommand5);

                        db.AddInParameter(dbCommand5, "p_Old_Strategy_Id", DbType.Int32, strategy.RandomExamStrategyId);
                        db.AddInParameter(dbCommand5, "p_new_Strategy_Id", DbType.Int32, strategyID);
                        db.AddInParameter(dbCommand5, "p_new_exam_Id", DbType.Int32, examID);
                        db.AddInParameter(dbCommand5, "p_new_subject_Id", DbType.Int32, subjectID);
                        db.ExecuteNonQuery(dbCommand5, transaction);

						IList<RandomExamItem> items = new List<RandomExamItem>();
						string sqlCommand3 = "USP_random_exam_ITEM_S";
						DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand3);
						db.AddInParameter(dbCommand3, "p_Strategy_Id", DbType.Int32, strategy.RandomExamStrategyId);
						db.AddInParameter(dbCommand3, "p_year", DbType.Int32, DateTime.Now.Year);

						using (IDataReader dataReader = db.ExecuteReader(dbCommand3,transaction))
						{
							while (dataReader.Read())
							{
								items.Add(RandomExamItemDAL.CreateModelObject(dataReader));
							}
						}

						foreach (RandomExamItem item in items)
						{
							string sqlCommand4 = "USP_random_exam_ITEM_I";

							DbCommand dbCommand4 = db.GetStoredProcCommand(sqlCommand4);

							db.AddOutParameter(dbCommand4, "p_random_exam_Item_Id", DbType.Int32, 4);
							db.AddInParameter(dbCommand4, "p_Subject_Id", DbType.Int32, subjectID);
							db.AddInParameter(dbCommand4, "p_Strategy_Id", DbType.Int32,strategyID);
							db.AddInParameter(dbCommand4, "p_random_exam_id", DbType.Int32,examID);
							db.AddInParameter(dbCommand4, "p_item_id", DbType.Int32, item.ItemId);
							db.AddInParameter(dbCommand4, "p_book_id", DbType.Int32, item.BookId);
							db.AddInParameter(dbCommand4, "p_chapter_id", DbType.Int32, item.ChapterId);
							db.AddInParameter(dbCommand4, "p_category_id", DbType.String, item.CategoryId);
							db.AddInParameter(dbCommand4, "p_org_id", DbType.Int32, item.OrganizationId);
							db.AddInParameter(dbCommand4, "p_type_id", DbType.Int32, item.TypeId);
							db.AddInParameter(dbCommand4, "p_complete_time", DbType.Int32, item.CompleteTime);
							db.AddInParameter(dbCommand4, "p_difficulty_id", DbType.Int32, item.DifficultyId);
							db.AddInParameter(dbCommand4, "p_source", DbType.String, item.Source);
							db.AddInParameter(dbCommand4, "p_version", DbType.String, item.Version);
							db.AddInParameter(dbCommand4, "p_score", DbType.Decimal, item.Score);
							db.AddInParameter(dbCommand4, "p_content", DbType.String, item.Content);
							db.AddInParameter(dbCommand4, "p_answer_count", DbType.Int32, item.AnswerCount);
							db.AddInParameter(dbCommand4, "p_select_answer", DbType.String, item.SelectAnswer);
							db.AddInParameter(dbCommand4, "p_standard_answer", DbType.String, item.StandardAnswer);
							db.AddInParameter(dbCommand4, "p_description", DbType.String, item.Description);
							db.AddInParameter(dbCommand4, "p_outdate_date", DbType.DateTime, item.OutDateDate);
							db.AddInParameter(dbCommand4, "p_used_count", DbType.Int32, item.UsedCount);
							db.AddInParameter(dbCommand4, "p_status_id", DbType.Int32, item.StatusId);
							db.AddInParameter(dbCommand4, "p_create_person", DbType.String, item.CreatePerson);
							db.AddInParameter(dbCommand4, "p_create_time", DbType.DateTime, item.CreateTime);
							db.AddInParameter(dbCommand4, "p_memo", DbType.String, item.Memo);
							db.AddInParameter(dbCommand4, "p_year", DbType.Int32, DateTime.Today.Year);
                            db.AddInParameter(dbCommand4, "p_parent_item_id", DbType.Int32, item.ParentItemID);
                            db.AddInParameter(dbCommand4, "p_mother_code", DbType.String, item.MotherCode);
                            db.AddInParameter(dbCommand4, "p_item_index", DbType.Int32, item.ItemIndex);


							db.ExecuteNonQuery(dbCommand4, transaction);
						}
					}
				}

				transaction.Commit();
			}
			catch (System.SystemException ex)
			{
				transaction.Rollback();
				throw ex;
			}
			connection.Close();
		}

		private string GetRailName()
		{
			string strPlace = string.Empty;
			SystemVersionDAL objBll = new SystemVersionDAL();
			if (objBll.GetUsePlace() == 1)
			{
				strPlace = "Îäºº";
			}
			else if (objBll.GetUsePlace() == 2)
			{
				strPlace = "Ì«Ô­";
			}
			else if(objBll.GetUsePlace() == 3)
			{
				strPlace = "¹þ¶û±õ";
			}
			else
			{
				strPlace = "ÖÐÌúÁª¼¯ÎäººÖÐÐÄÕ¾";
			}

			return strPlace;
		}

		public int AddResetRandomExam(int examId)
		{
			string strPlace = GetRailName();

			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			int examID = 0;
			try
			{
				string sqlCommand = "USP_Random_Exam_I_Reset";
				DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

				db.AddOutParameter(dbCommand, "v_random_exam_id", DbType.Int32, 4);
				db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examId);
				db.ExecuteNonQuery(dbCommand, transaction);

				examID = Convert.ToInt32(db.GetParameterValue(dbCommand, "v_random_exam_id"));

				IList<RandomExamSubject> objSubjects = new List<RandomExamSubject>();
				sqlCommand = "USP_Random_Exam_SUBJECT_Q";
				dbCommand = db.GetStoredProcCommand(sqlCommand);

				db.AddInParameter(dbCommand, "p_Random_Exam_Id", DbType.Int32, examId);

				using (IDataReader dataReader = db.ExecuteReader(dbCommand, transaction))
				{
					while (dataReader.Read())
					{
						RandomExamSubject objSubject = RandomExamSubjectDAL.SubjectNamelObject(dataReader);
						objSubjects.Add(objSubject);
					}
				}

				foreach (RandomExamSubject subject in objSubjects)
				{
					sqlCommand = "USP_random_exam_subject_I";
					dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddInParameter(dbCommand, "p_random_exam_ID", DbType.Int32, examID);
					db.AddOutParameter(dbCommand, "p_random_exam_Subject_Id", DbType.Int32, 4);
					db.AddInParameter(dbCommand, "p_Order_Index", DbType.String, subject.OrderIndex);
					db.AddInParameter(dbCommand, "p_Item_Count", DbType.Int32, subject.ItemCount);
					db.AddInParameter(dbCommand, "p_Item_Type_Id", DbType.Int32, subject.ItemTypeId);
					db.AddInParameter(dbCommand, "p_Remark", DbType.String, subject.Remark);
					db.AddInParameter(dbCommand, "p_Subject_Name", DbType.String, subject.SubjectName);
					db.AddInParameter(dbCommand, "p_Total_Score", DbType.Decimal, subject.TotalScore);
					db.AddInParameter(dbCommand, "p_Unit_Score", DbType.Decimal, subject.UnitScore);
					db.AddInParameter(dbCommand, "p_memo", DbType.String, subject.Memo);

					db.ExecuteNonQuery(dbCommand, transaction);
					int subjectID = (int)db.GetParameterValue(dbCommand, "p_random_exam_subject_Id");

					IList<RandomExamStrategy> objStrategyList = new List<RandomExamStrategy>();
					string sqlCommand1 = "USP_Random_Exam_Strategy_Q";
					DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

					db.AddInParameter(dbCommand1, "p_subject_id", DbType.Int32, subject.RandomExamSubjectId);
					db.AddInParameter(dbCommand1, "p_range_id", DbType.Int32, 0);
					db.AddInParameter(dbCommand1, "p_range_type", DbType.Int32, 0);

					using (IDataReader dataReader = db.ExecuteReader(dbCommand1, transaction))
					{
						while (dataReader.Read())
						{
							RandomExamStrategy item = RandomExamStrategyDAL.CreateModelObject(dataReader);
							if (strPlace == "¹þ¶û±õ")
							{
								item.MaxItemDifficultyID = Convert.ToInt32(dataReader[RandomExamStrategyDAL.GetMappingFieldName("MaxItemDifficultyID")].ToString());
								item.MaxItemDifficultyName = dataReader[RandomExamStrategyDAL.GetMappingFieldName("MaxItemDifficultyName")].ToString();
							}
							objStrategyList.Add(item);
						}
					}

					foreach (RandomExamStrategy strategy in objStrategyList)
					{
						string sqlCommand2 = "USP_Random_Exam_Strategy_I_Out";
						DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand2);

						db.AddOutParameter(dbCommand2, "p_Random_Exam_Strategy_Id", DbType.Int32, 4);
						db.AddInParameter(dbCommand2, "p_subject_id", DbType.Int32, subjectID);
						db.AddInParameter(dbCommand2, "p_Range_type", DbType.Int32, strategy.RangeType);
						db.AddInParameter(dbCommand2, "p_Range_id", DbType.Int32, strategy.RangeId);
						db.AddInParameter(dbCommand2, "p_item_type_id", DbType.Int32, strategy.ItemTypeId);
						db.AddInParameter(dbCommand2, "p_range_name", DbType.String, strategy.RangeName);
						db.AddInParameter(dbCommand2, "p_exclude_chapters_id", DbType.String, strategy.ExcludeChapterId);
						db.AddInParameter(dbCommand2, "p_memo", DbType.String, strategy.Memo);
						db.AddInParameter(dbCommand2, "p_Item_Count", DbType.Int32, strategy.ItemCount);
						db.AddInParameter(dbCommand2, "p_Item_diff", DbType.Int32, strategy.ItemDifficultyID);
                        db.AddInParameter(dbCommand2, "p_is_mother_item", DbType.Int32, strategy.IsMotherItem ? 1 : 0);
                        db.AddInParameter(dbCommand2, "p_mother_id", DbType.Int32, strategy.MotherID);


						if (strPlace == "¹þ¶û±õ")
						{
							db.AddInParameter(dbCommand2, "p_max_Item_diff", DbType.Int32, strategy.MaxItemDifficultyID);
						}

						db.ExecuteNonQuery(dbCommand2, transaction);

						int strategyID = (int)db.GetParameterValue(dbCommand2, "p_Random_Exam_Strategy_Id");

                        string sqlCommand5 = "USP_RANDOM_EXAM_Item_Select_C";
                        DbCommand dbCommand5 = db.GetStoredProcCommand(sqlCommand5);

                        db.AddInParameter(dbCommand5, "p_Old_Strategy_Id", DbType.Int32, strategy.RandomExamStrategyId);
                        db.AddInParameter(dbCommand5, "p_new_Strategy_Id", DbType.Int32, strategyID);
                        db.AddInParameter(dbCommand5, "p_new_exam_Id", DbType.Int32, examID);
                        db.AddInParameter(dbCommand5, "p_new_subject_Id", DbType.Int32, subjectID);
                        db.ExecuteNonQuery(dbCommand5, transaction);

						IList<RandomExamItem> items = new List<RandomExamItem>();
						string sqlCommand3 = "USP_random_exam_ITEM_S";
						DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand3);
						db.AddInParameter(dbCommand3, "p_Strategy_Id", DbType.Int32, strategy.RandomExamStrategyId);
						db.AddInParameter(dbCommand3, "p_year", DbType.Int32, DateTime.Now.Year);

						using (IDataReader dataReader = db.ExecuteReader(dbCommand3, transaction))
						{
							while (dataReader.Read())
							{
								items.Add(RandomExamItemDAL.CreateModelObject(dataReader));
							}
						}

						foreach (RandomExamItem item in items)
						{
							string sqlCommand4 = "USP_random_exam_ITEM_I";

							DbCommand dbCommand4 = db.GetStoredProcCommand(sqlCommand4);

							db.AddOutParameter(dbCommand4, "p_random_exam_Item_Id", DbType.Int32, 4);
							db.AddInParameter(dbCommand4, "p_Subject_Id", DbType.Int32, subjectID);
							db.AddInParameter(dbCommand4, "p_Strategy_Id", DbType.Int32, strategyID);
							db.AddInParameter(dbCommand4, "p_random_exam_id", DbType.Int32, examID);
							db.AddInParameter(dbCommand4, "p_item_id", DbType.Int32, item.ItemId);
							db.AddInParameter(dbCommand4, "p_book_id", DbType.Int32, item.BookId);
							db.AddInParameter(dbCommand4, "p_chapter_id", DbType.Int32, item.ChapterId);
							db.AddInParameter(dbCommand4, "p_category_id", DbType.String, item.CategoryId);
							db.AddInParameter(dbCommand4, "p_org_id", DbType.Int32, item.OrganizationId);
							db.AddInParameter(dbCommand4, "p_type_id", DbType.Int32, item.TypeId);
							db.AddInParameter(dbCommand4, "p_complete_time", DbType.Int32, item.CompleteTime);
							db.AddInParameter(dbCommand4, "p_difficulty_id", DbType.Int32, item.DifficultyId);
							db.AddInParameter(dbCommand4, "p_source", DbType.String, item.Source);
							db.AddInParameter(dbCommand4, "p_version", DbType.String, item.Version);
							db.AddInParameter(dbCommand4, "p_score", DbType.Decimal, item.Score);
							db.AddInParameter(dbCommand4, "p_content", DbType.String, item.Content);
							db.AddInParameter(dbCommand4, "p_answer_count", DbType.Int32, item.AnswerCount);
							db.AddInParameter(dbCommand4, "p_select_answer", DbType.String, item.SelectAnswer);
							db.AddInParameter(dbCommand4, "p_standard_answer", DbType.String, item.StandardAnswer);
							db.AddInParameter(dbCommand4, "p_description", DbType.String, item.Description);
							db.AddInParameter(dbCommand4, "p_outdate_date", DbType.DateTime, item.OutDateDate);
							db.AddInParameter(dbCommand4, "p_used_count", DbType.Int32, item.UsedCount);
							db.AddInParameter(dbCommand4, "p_status_id", DbType.Int32, item.StatusId);
							db.AddInParameter(dbCommand4, "p_create_person", DbType.String, item.CreatePerson);
							db.AddInParameter(dbCommand4, "p_create_time", DbType.DateTime, item.CreateTime);
							db.AddInParameter(dbCommand4, "p_memo", DbType.String, item.Memo);
							db.AddInParameter(dbCommand4, "p_year", DbType.Int32, DateTime.Today.Year);
                            db.AddInParameter(dbCommand4, "p_parent_item_id", DbType.Int32, item.ParentItemID);
                            db.AddInParameter(dbCommand4, "p_mother_code", DbType.String, item.MotherCode);
                            db.AddInParameter(dbCommand4, "p_item_index", DbType.Int32, item.ItemIndex);


							db.ExecuteNonQuery(dbCommand4, transaction);
						}
					}
				}

				transaction.Commit();
			}
			catch 
			{
				transaction.Rollback();
				examID = 0;
			}
			connection.Close();

			return examID;
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

        public static RandomExam CreateModelObject(IDataReader dataReader)
        {
            return new RandomExam(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrgId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("AutoSaveInterval")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsUnderControl")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("categoryId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTime")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsAutoScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTypeId")]),                
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
                DataConvert.ToString(dataReader[GetMappingFieldName("memo")]) );
        }
    }
}
