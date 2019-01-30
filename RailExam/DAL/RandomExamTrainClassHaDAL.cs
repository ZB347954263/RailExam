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
	public class RandomExamTrainClassHaDAL
	{
		private static Hashtable _ormTable;
        private int _recordCount = 0;

		static RandomExamTrainClassHaDAL()
        {
            _ormTable = new Hashtable();

			_ormTable.Add("randomexamtrainclassid", "Random_Exam_TRAIN_CLASS_ID");
			_ormTable.Add("randomexamid", "Random_Exam_ID");
			_ormTable.Add("archivesid","ARCHIVES_ID");
		}

		public IList<RandomExamTrainClassHa> GetRandomExamsTrainClassByRandomExamID(int randomExamID)
		{
			IList<RandomExamTrainClassHa> exams = new List<RandomExamTrainClassHa>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Train_G_ExamID";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, randomExamID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamTrainClassHa exam = CreateModelObject(dataReader);
					exams.Add(exam);
				}
			}

			return exams;
		}

		public RandomExamTrainClassHa GetRandomExamsTrainClassByRandomExamTrainClassID(int id)
		{
			RandomExamTrainClassHa trainClass = new RandomExamTrainClassHa();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Train_Class_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_id", DbType.Int32, id);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					trainClass = CreateModelObject(dataReader);
				}
			}

			return trainClass;
		}

		public void AddRandomExamTrainClass(RandomExamTrainClassHa trainClass)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Train_Class_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_id", DbType.Int32, trainClass.RandomExamTrainClassID);
			db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, trainClass.RandomExamID);
			db.AddInParameter(dbCommand, "p_class_id", DbType.String, trainClass.ArchivesID);

			db.ExecuteNonQuery(dbCommand);
		}

		public void UpdateRandomExamTrainClass(RandomExamTrainClassHa trainClass)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Train_Class_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_id", DbType.Int32, trainClass.RandomExamTrainClassID);
			db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, trainClass.RandomExamID);
			db.AddInParameter(dbCommand, "p_class_id", DbType.String, trainClass.ArchivesID);

			db.ExecuteNonQuery(dbCommand);
		}

		public IList<RandomExamTrainClassHa> GetRandomExamTrainClassCount(string trainclassID, int trainclasssubjectID, int postID)
		{
			IList<RandomExamTrainClassHa> exams = new List<RandomExamTrainClassHa>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Train_Class_Q";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_train_class_id", DbType.String, trainclassID);
			db.AddInParameter(dbCommand, "p_subject_id", DbType.Int32, trainclasssubjectID);
			db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamTrainClassHa exam = CreateModelObject(dataReader);
					exams.Add(exam);
				}
			}

			return exams;
		}

		public void DeleteRandomExamTrainClassByRandomExamID(int randomExamID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Train_Class_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, randomExamID);

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

		public static RandomExamTrainClassHa CreateModelObject(IDataReader dataReader)
        {
			return new RandomExamTrainClassHa(
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamTrainClassID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamID")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ArchivesID")]));
        }
	}
}
