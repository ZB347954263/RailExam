using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;


namespace RailExam.DAL
{
	public class RandomExamModularTypeDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;
		public int RecordCount
		{
			get
			{
				return _recordCount;
			}
		}
		static RandomExamModularTypeDAL()
		{
			_ormTable = new Hashtable();
			_ormTable.Add("randomexammodulartypeid", "random_exam_modular_type_id");
			_ormTable.Add("randomexammodulartypename", "random_exam_modular_type_name");
			_ormTable.Add("levelnum", "level_num");
		}
		public static string GetMappingFieldName(string propertyName)
		{
			return (string)_ormTable[propertyName.ToLower()];
		}
		public static RandomExamModularType CreateModelObject(IDataReader dataReader)
		{
			return new RandomExamModularType(
				DataConvert.ToInt(dataReader[GetMappingFieldName("randomexammodulartypeid")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("randomexammodulartypename")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("levelnum")])
				);
		}
		public IList<RandomExamModularType> GetAllRandomExamModularType()
		{
			IList<RandomExamModularType> objList = new List<RandomExamModularType>();

			Database db = DatabaseFactory.CreateDatabase();
			string sql = "select * from random_exam_modular_type order by level_num";
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, sql))
			{
				while (dataReader.Read())
				{
					RandomExamModularType obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}
			return objList;
		}
		public IList<RandomExamModularType> GetAllRandomExamModularTypeByWhereClause(string sql)
		{
			IList<RandomExamModularType> objList = new List<RandomExamModularType>();
			Database db = DatabaseFactory.CreateDatabase();
			string sqlStr = string.Format("select * from random_exam_modular_type where 1=1 and {0}", sql);
			using (IDataReader dataRead = db.ExecuteReader(CommandType.Text, sqlStr))
			{
				while (dataRead.Read())
				{
					RandomExamModularType obj = CreateModelObject(dataRead);
					objList.Add(obj);
				}
			}
			return objList;
		}
		public RandomExamModularType GetRandomExamModularTypeByTypeID(int modularTypeID)
		{
			RandomExamModularType objList = new RandomExamModularType();
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("select * from random_exam_modular_type where random_exam_modular_type_id={0}", modularTypeID);
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, sql))
			{
				while (dataReader.Read())
				{
					objList = CreateModelObject(dataReader);
				}
			}
			return objList;
		}
		public object GetRandomExam(int modularTypeID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("select count(exam_type_id) from random_exam where exam_type_id={0}", modularTypeID);
			return db.ExecuteScalar(CommandType.Text, sql);
		}
		public void InsertRandomExamModularType(RandomExamModularType obj)
		{
			Database db = DatabaseFactory.CreateDatabase();
			if(Convert.ToInt32(db.ExecuteScalar(CommandType.Text,"select count(level_num) from random_exam_modular_type"))>0)
			{
				string sqlMaxID = "select max(level_num) from random_exam_modular_type ";
				obj.LevelNum=Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sqlMaxID))+1;
			}
			else
			{
				obj.LevelNum = 0;
			}
			string sql = string.Format("insert into random_exam_modular_type values({0},'{1}',{2})", "RANDOM_EXAM_MODULAR_TYPE_SEQ.nextval", obj.RandomExamModularTypeName,obj.LevelNum);
			db.ExecuteNonQuery(CommandType.Text, sql);
		}
		public void UpdateRandomExamModularType(RandomExamModularType obj)
		{
			Database db = DatabaseFactory.CreateDatabase();
		
			int orderIndex = GetRandomExamModularTypeByTypeID(obj.RandomExamModularTypeID).LevelNum;
			if (obj.LevelNum > orderIndex)
			{
				int id =Convert.ToInt32( db.ExecuteScalar(CommandType.Text, string.Format("select random_exam_modular_type_id from random_exam_modular_type where level_num={0}", obj.LevelNum)));
				db.ExecuteNonQuery(CommandType.Text, string.Format("update random_exam_modular_type set level_num=level_num-1 where random_exam_modular_type_id={0}", id));
			}
			else if (obj.LevelNum < orderIndex)
			{
				int id = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, string.Format("select random_exam_modular_type_id from random_exam_modular_type where level_num={0}", obj.LevelNum)));
				db.ExecuteNonQuery(CommandType.Text, string.Format("update random_exam_modular_type set level_num=level_num+1 where random_exam_modular_type_id={0}", id));
			}
	
				string sql = string.Format(" update random_exam_modular_type set random_exam_modular_type_name='{0}',level_num={1} where random_exam_modular_type_id={2}", obj.RandomExamModularTypeName, obj.LevelNum, obj.RandomExamModularTypeID);
				db.ExecuteNonQuery(CommandType.Text, sql);
		}
		public void DeleteRandomExamModularType(int id)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sqlup =
				@" update random_exam_modular_type set level_num=level_num-1 where 
                level_num>(select level_num from random_exam_modular_type where random_exam_modular_type_id="+id+")";
			db.ExecuteNonQuery(CommandType.Text, sqlup);
			string sql = string.Format("delete from random_exam_modular_type where random_exam_modular_type_id={0}", id);
			db.ExecuteNonQuery(CommandType.Text, sql);
		}
	}
}
