using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class RefreshSnapShotDAL
    {
        public void RefreshSnapShot()
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Refresh_SnapShot";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.ExecuteNonQuery(dbCommand);
        }

        public void RefreshSnapShot(string strPro, int typeID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = strPro;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, typeID);

            db.ExecuteNonQuery(dbCommand);
        }

        public bool IsExistsRefreshSnapShot(string proName,string objectType)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Is_Exists";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_pro_name", DbType.String,proName);
            db.AddInParameter(dbCommand, "p_object_type", DbType.String, objectType);
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32,4);

            db.ExecuteNonQuery(dbCommand);

            int count = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
            
            if(count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CreateSnapShot(int orgID,int typeId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Create_Snapshot_New";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32,orgID);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, typeId);

            db.ExecuteNonQuery(dbCommand);
        }

        public void RefreshOrg(int hours)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Refresh_Org";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_time", DbType.Int32, hours);

            db.ExecuteNonQuery(dbCommand);
        }

        public void DropSnapshot()
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Drop_Snapshot";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.ExecuteNonQuery(dbCommand);
        }

        public IList<Book> GetStationBook()
        {
            IList<Book> bookList = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_G_Station";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = BookDAL.CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }

        public IList<Courseware> GetStationCourseware()
        {
            IList<Courseware> objList = new List<Courseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Courseware_G_Station";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Courseware obj = CoursewareDAL.CreateModelObject(dataReader);

                    objList.Add(obj);
                }
            }

            return objList;
        }

        public void AddTest()
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Z_Test";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddOutParameter(dbCommand, "p_test_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_test_content", DbType.String,"12505,12506");

            db.ExecuteNonQuery(dbCommand);
        }
    }
}
