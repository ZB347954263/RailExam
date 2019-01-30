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
    public class CoursewareDAL
    {
        private static Hashtable _ormTable;

        static CoursewareDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("coursewareid", "COURSEWARE_ID");
            _ormTable.Add("coursewarename", "COURSEWARE_NAME");
            _ormTable.Add("coursewaretypeid", "COURSEWARE_TYPE_ID");
            _ormTable.Add("coursewaretypename", "COURSEWARE_TYPE_NAME");
            _ormTable.Add("provideorg", "PROVIDE_ORG");
            _ormTable.Add("provideorgname", "PROVIDE_ORG_NAME");                   
            _ormTable.Add("publishdate", "PUBLISH_DATE");
            _ormTable.Add("authors", "AUTHORS");
            _ormTable.Add("revisers", "REVISERS");
            _ormTable.Add("keyword", "KEYWORD");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("url", "URL");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("isgroupleader", "IS_GROUP_LEADER");
            _ormTable.Add("techniciantypeid", "TECHNICIAN_TYPE_ID");
            _ormTable.Add("orderindex","ORDER_INDEX");
        }

        public IList<Courseware> GetCourseware(int coursewareID, string coursewareName, int coursewareTypeID, 
            int PrivodeOrg, DateTime publishDate, string authors, string keyWord, string revisers, string url,
            string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Courseware> coursewares = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);

                    coursewares.Add(courseware);
                }
            }

            return coursewares;
        }

        public IList<Courseware> GetCoursewaresByCoursewareTypeIDPath(string coursewareTypeIDPath,int orgID)
        {
            IList<Courseware> coursewares = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, coursewareTypeIDPath);
            db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);
                    coursewares.Add(courseware);
                }
            }

            return coursewares;
        }


        public IList<Courseware> GetCoursewaresByCoursewareTypeID(int coursewareTypeID, int orgID)
        {
            IList<Courseware> coursewares = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_Q_TYPEID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.String, coursewareTypeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);
                    coursewares.Add(courseware);
                }
            }

            return coursewares;
        }

        public IList<Courseware> GetCoursewaresByCoursewareTypeOnline(int orgid,int postid, string idpath, bool isGroupleader, int techniciantypeid)
        {
            IList<Courseware> coursewares = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_Q_Study";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postid);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, idpath);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, isGroupleader);
            db.AddInParameter(dbCommand, "p_tech", DbType.Int32, techniciantypeid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);
                    coursewares.Add(courseware);
                }
            }

            return coursewares;
        }

        public IList<Courseware> GetCoursewaresByTrainTypeIDPath(string trainTypeIDPath,int orgID)
        {
            IList<Courseware> coursewares = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_Q1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, trainTypeIDPath);
            db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);
                    coursewares.Add(courseware);
                }
            }

            return coursewares;
        }

        public IList<Courseware> GetCoursewaresByTrainTypeID(int traintypeid, int orgID)
        {
            IList<Courseware> coursewares = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_Q_TrainTypeID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.String, traintypeid);
            db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);
                    coursewares.Add(courseware);
                }
            }

            return coursewares;
        }

		public IList<Courseware> GetCoursewaresByPostID(int postID, int orgID)
		{
			IList<Courseware> coursewares = new List<Courseware>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_COURSEWARE_Q_POSTID";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_post_id", DbType.String, postID);
			db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Courseware courseware = CreateModelObject(dataReader);
					coursewares.Add(courseware);
				}
			}

			return coursewares;
		}

        public IList<Courseware> GetCoursewares(int coursewareTypeID, int trainTypeID, string coursewareName, string keyWord, string authors,int orgID)
        {
            IList<Courseware> coursewares = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_F";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, coursewareTypeID);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_courseware_name", DbType.String, coursewareName);
            db.AddInParameter(dbCommand, "p_keyword", DbType.String, keyWord);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, authors);
            db.AddInParameter(dbCommand,"p_org_id",DbType.Int32,orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);
                    coursewares.Add(courseware);
                }
            }

            return coursewares;
        }

		public IList<Courseware> GetCoursewaresByPostID(int postID, string coursewareName, string keyWord, string authors, int orgID)
		{
			IList<Courseware> coursewares = new List<Courseware>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_COURSEWARE_F_PostID";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
			db.AddInParameter(dbCommand, "p_courseware_name", DbType.String, coursewareName);
			db.AddInParameter(dbCommand, "p_keyword", DbType.String, keyWord);
			db.AddInParameter(dbCommand, "p_authors", DbType.String, authors);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Courseware courseware = CreateModelObject(dataReader);
					coursewares.Add(courseware);
				}
			}

			return coursewares;
		}

        public Courseware GetCourseware(int coursewareID)
        {
            Courseware courseware = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, coursewareID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    courseware = CreateModelObject(dataReader);
                }
            }

            sqlCommand = "USP_COURSEWARE_TRAIN_TYPE_S";
            DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand1, "p_courseware_id", DbType.Int32, coursewareID);

            sqlCommand = "USP_COURSEWARE_RANGE_ORG_S";
            DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand2, "p_courseware_id", DbType.Int32, coursewareID);

            sqlCommand = "USP_COURSEWARE_RANGE_POST_S";
            DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand3, "p_courseware_id", DbType.Int32, coursewareID);

            IDataReader dataReader1 = db.ExecuteReader(dbCommand1);
            IDataReader dataReader2 = db.ExecuteReader(dbCommand2);
            IDataReader dataReader3 = db.ExecuteReader(dbCommand3);

            ArrayList trainTypeIDAL = new ArrayList();
            ArrayList orgIDAL = new ArrayList();
            ArrayList postIDAL = new ArrayList();
            string strTrainTypeNames = string.Empty;

            CoursewareTypeDAL coursewareTypeDAL = new CoursewareTypeDAL();
            CoursewareType coursewareType = coursewareTypeDAL.GetCoursewareType(courseware.CoursewareTypeID);

            courseware.CoursewareTypeNames = GetCoursewareTypeNames("/" + coursewareType.CoursewareTypeName, coursewareType.ParentId);

            while (dataReader1.Read())
            {
                if (dataReader1["TRAIN_TYPE_ID"].ToString() != "")
                {
                    trainTypeIDAL.Add(DataConvert.ToInt(dataReader1["TRAIN_TYPE_ID"].ToString()));

                    strTrainTypeNames += GetTrainTypeNames("/" + dataReader1["TRAIN_TYPE_NAME"].ToString(), int.Parse(dataReader1["PARENT_ID"].ToString())) + ",";
                }
            }

            while (dataReader2.Read())
            {
                if (dataReader2["ORG_ID"].ToString() != "")
                {
                    orgIDAL.Add(DataConvert.ToInt(dataReader2["ORG_ID"].ToString()));
                }
            }

            while (dataReader3.Read())
            {
                if (dataReader3["POST_ID"].ToString() != "")
                {
                    postIDAL.Add(DataConvert.ToInt(dataReader3["POST_ID"].ToString()));
                }
            }

            if (strTrainTypeNames.Length > 0)
            {
                strTrainTypeNames = strTrainTypeNames.Substring(0, strTrainTypeNames.Length - 1);
            }

            courseware.TrainTypeIDAL = trainTypeIDAL;
            courseware.OrgIDAL = orgIDAL;
            courseware.PostIDAL = postIDAL;
            courseware.TrainTypeNames = strTrainTypeNames;

            return courseware;
        }

        private string GetCoursewareTypeNames(string strName, int nID)
        {
            string strCoursewareTypeName = string.Empty;
            if (nID != 0)
            {
                CoursewareTypeDAL coursewareTypeDAL = new CoursewareTypeDAL();
                CoursewareType coursewareType = coursewareTypeDAL.GetCoursewareType(nID);

                if (coursewareType.ParentId != 0)
                {
                    strCoursewareTypeName = GetCoursewareTypeNames("/" + coursewareType.CoursewareTypeName, coursewareType.ParentId) + strName;
                }
                else
                {
                    strCoursewareTypeName = coursewareType.CoursewareTypeName + strName;
                }
            }

            return strCoursewareTypeName;
        }

        private string GetTrainTypeNames(string strName, int nID)
        {
			string strTrainTypeName = string.Empty;
			if (nID != 0)
			{
				TrainTypeDAL trainTypeDAL = new TrainTypeDAL();
				TrainType trainType = trainTypeDAL.GetTrainTypeInfo(nID);

				if (trainType.ParentID != 0)
				{
					strTrainTypeName = GetTrainTypeNames("/" + trainType.TypeName, trainType.ParentID) + strName;
				}
				else
				{
					strTrainTypeName = trainType.TypeName + strName;
				}
			}
			else
			{
				strTrainTypeName = strName.Replace("/", "");
			}
			return strTrainTypeName;
        }

        public IList<Courseware> GetEmployeeStudyCoursewareInfo(int trainTypeID, int orgID, int postID,bool isGroupleader,int techniciantypeid, int row)
        {
            IList<Courseware> coursewareList = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_WARE_TRAIN_EMPLOYEE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
            db.AddInParameter(dbCommand, "p_row_num", DbType.Int32, row);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, isGroupleader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, techniciantypeid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);

                    coursewareList.Add(courseware);
                }
            }

            return coursewareList;
        }


        public IList<Courseware> GetStudyCoursewareInfoByTrainTypeID(int trainTypeID, int orgID, int postID,int isGroupleader,int techid, int row)
        {
            IList<Courseware> coursewareList = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_WARE_TRAIN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_coursreware_type_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, isGroupleader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, techid); 
            db.AddInParameter(dbCommand, "p_row_num", DbType.Int32, row);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);

                    coursewareList.Add(courseware);
                }
            }

            return coursewareList;
        }


        public IList<Courseware> GetStudyCoursewareInfoByTypeID(int typeid, int orgID, int postID, int isGroupleader, int techid, int row)
        {
            IList<Courseware> coursewareList = new List<Courseware>();
            //--temp code---------------
            if (typeid == postID)
            {
                return coursewareList;
            }
            //--------------------------


            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_WARE_TRAIN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_coursreware_type_id", DbType.Int32, typeid);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, isGroupleader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, techid);
            db.AddInParameter(dbCommand, "p_row_num", DbType.Int32, row);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);

                    coursewareList.Add(courseware);
                }
            }

            return coursewareList;
        }

        public IList<Courseware> GetCoursewareInfoByDate(int row)
        {
            IList<Courseware> coursewareList = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_row_num", DbType.Int32, row);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware courseware = CreateModelObject(dataReader);

                    coursewareList.Add(courseware);
                }
            }

            return coursewareList;
        }

        public int AddCourseware(Courseware courseware)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_courseware_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_courseware_name", DbType.String, courseware.CoursewareName);
            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, courseware.CoursewareTypeID);
            db.AddInParameter(dbCommand, "p_provide_org", DbType.Int32, courseware.ProvideOrg);
            db.AddInParameter(dbCommand, "p_publish_date", DbType.Date, courseware.PublishDate);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, courseware.Authors);
            db.AddInParameter(dbCommand, "p_revisers", DbType.String, courseware.Revisers);
            db.AddInParameter(dbCommand, "p_keyword", DbType.String, courseware.KeyWord);
            db.AddInParameter(dbCommand, "p_description", DbType.String, courseware.Description);
            db.AddInParameter(dbCommand, "p_url", DbType.String, courseware.Url);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, courseware.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, courseware.IsGroupLearder);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, courseware.TechnicianTypeID);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_courseware_id"));

                //sqlCommand = "USP_COURSEWARE_TRAIN_TYPE_D";
                //dbCommand = db.GetStoredProcCommand(sqlCommand);
                //db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, id);
                //db.ExecuteNonQuery(dbCommand, transaction);

                //sqlCommand = "USP_COURSEWARE_RANGE_ORG_D";
                //dbCommand = db.GetStoredProcCommand(sqlCommand);
                //db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, id);
                //db.ExecuteNonQuery(dbCommand, transaction);

                //sqlCommand = "USP_COURSEWARE_RANGE_POST_D";
                //dbCommand = db.GetStoredProcCommand(sqlCommand);
                //db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, id);
                //db.ExecuteNonQuery(dbCommand, transaction);

                for (int i = 0; i < courseware.TrainTypeIDAL.Count; i ++)
                {
                    sqlCommand = "USP_COURSEWARE_TRAIN_TYPE_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, int.Parse(courseware.TrainTypeIDAL[i].ToString()));
                    db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
                for (int i = 0; i < courseware.OrgIDAL.Count; i ++)
                {
                    sqlCommand = "USP_COURSEWARE_RANGE_ORG_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, int.Parse(courseware.OrgIDAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
                for (int i = 0; i < courseware.PostIDAL.Count; i ++)
                {
                    sqlCommand = "USP_COURSEWARE_RANGE_POST_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, int.Parse(courseware.PostIDAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return id;
        }

        public void UpdateCourseware(Courseware courseware)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, courseware.CoursewareID);
            db.AddInParameter(dbCommand, "p_courseware_name", DbType.String, courseware.CoursewareName);
            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, courseware.CoursewareTypeID);
            db.AddInParameter(dbCommand, "p_provide_org", DbType.Int32, courseware.ProvideOrg);
            db.AddInParameter(dbCommand, "p_publish_date", DbType.DateTime, courseware.PublishDate);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, courseware.Authors);
            db.AddInParameter(dbCommand, "p_revisers", DbType.String, courseware.Revisers);
            db.AddInParameter(dbCommand, "p_keyword", DbType.String, courseware.KeyWord);
            db.AddInParameter(dbCommand, "p_description", DbType.String, courseware.Description);
            db.AddInParameter(dbCommand, "p_url", DbType.String, courseware.Url);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, courseware.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, courseware.IsGroupLearder );
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, courseware.TechnicianTypeID);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, courseware.OrderIndex);

            sqlCommand = "USP_COURSEWARE_RANGE_ORG_D";
            DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand2, "p_courseware_id", DbType.Int32, courseware.CoursewareID);

            sqlCommand = "USP_COURSEWARE_RANGE_POST_D";
            DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand3, "p_courseware_id", DbType.Int32, courseware.CoursewareID);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                db.ExecuteNonQuery(dbCommand2, transaction);
                db.ExecuteNonQuery(dbCommand3, transaction);

                for (int i = 0; i < courseware.OrgIDAL.Count; i ++)
                {
                    sqlCommand = "USP_COURSEWARE_RANGE_ORG_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, courseware.CoursewareID);
                    db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, int.Parse(courseware.OrgIDAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                for (int i = 0; i < courseware.PostIDAL.Count; i++)
                {
                    sqlCommand = "USP_COURSEWARE_RANGE_POST_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, courseware.CoursewareID);
                    db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, int.Parse(courseware.PostIDAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                ArrayList objList = new ArrayList();
                CoursewareTrainTypeDAL dal = new CoursewareTrainTypeDAL();
                IList<CoursewareTrainType> objTrainTypeList = dal.GetCoursewareTrainTypeByCoursewareID(courseware.CoursewareID);

                foreach (CoursewareTrainType type in objTrainTypeList)
                {
                    objList.Add(type.TrainTypeID.ToString());
                    if (courseware.TrainTypeIDAL.IndexOf(type.TrainTypeID.ToString()) == -1)
                    {
                        sqlCommand = "USP_COURSEWARE_TRAIN_TYPE_D";
                        DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);
                        db.AddInParameter(dbCommand1, "p_courseware_id", DbType.Int32, courseware.CoursewareID);
                        db.AddInParameter(dbCommand1, "p_train_type_id", DbType.String, type.TrainTypeID);
                        db.ExecuteNonQuery(dbCommand1, transaction);
                    }
                }

                for (int i = 0; i < courseware.TrainTypeIDAL.Count; i++)
                {
                    //新增的培训类别
                    if (objList.IndexOf(courseware.TrainTypeIDAL[i].ToString()) == -1)
                    {
                        sqlCommand = "USP_COURSEWARE_TRAIN_TYPE_I";
                        DbCommand dbCommand6 = db.GetStoredProcCommand(sqlCommand);

                        db.AddInParameter(dbCommand6, "p_courseware_id", DbType.Int32, courseware.CoursewareID);
                        db.AddInParameter(dbCommand6, "p_train_type_id", DbType.Int32, int.Parse(courseware.TrainTypeIDAL[i].ToString()));
                        db.AddOutParameter(dbCommand6, "p_order_index", DbType.Int32, 4);
                        db.ExecuteNonQuery(dbCommand6, transaction);
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
        }

        public void DeleteCourseware(int coursewareID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_COURSEWARE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, coursewareID);

            db.ExecuteNonQuery(dbCommand);
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

        public static Courseware CreateModelObject(IDataReader dataReader)
        {
            return new Courseware(
                DataConvert.ToInt(dataReader[GetMappingFieldName("CoursewareID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CoursewareName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CoursewareTypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CoursewareTypeName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ProvideOrg")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ProvideOrgName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("PublishDate")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Authors")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("keyWord")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Revisers")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Url")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsGroupLeader")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TechnicianTypeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]));
        }
    }
}
