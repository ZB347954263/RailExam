using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class CoursewareTypeBLL
    {
        private static readonly CoursewareTypeDAL dal = new CoursewareTypeDAL();
        private SystemLogBLL objLogBill = new SystemLogBLL(); 


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="coursewareTypeId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="coursewareTypeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<CoursewareType> GetCoursewareTypes(int coursewareTypeId, int parentId, string idPath, int levelNum, int orderIndex,
            string coursewareTypeName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<CoursewareType> CoursewareTypeList = dal.GetCoursewareTypes(coursewareTypeId, parentId, idPath, levelNum, orderIndex,
                                                                        coursewareTypeName, description, memo, startRowIndex, maximumRows, orderBy);

            return CoursewareTypeList;
        }

        public IList<CoursewareType> GetCoursewareTypes()
        {
            return dal.GetCoursewareTypes();
        }

        /// <summary>
        /// 取得某课程相关的课程类别信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public IList<CoursewareType> GetCoursewareTypesByCourseID(int courseID)
        {
            return dal.GetCoursewareTypesByCourseID(courseID);
        }

        public CoursewareType GetCoursewareType(int coursewareTypeId)
        {
            if (coursewareTypeId < 1)
            {
                return null;
            }

            return dal.GetCoursewareType(coursewareTypeId);
        }

        /// <summary>
        /// 新增课件类别
        /// </summary>
        /// <param name="coursewareType">新增的课件类别信息</param>
        /// <returns></returns>
        public int AddCoursewareType(CoursewareType coursewareType)
        {
            int id = dal.AddCoursewareType(coursewareType);
            objLogBill.WriteLog("新增课件类别“"+　coursewareType.CoursewareTypeName　+"”");
            return id;
        }

        /// <summary>
        /// 更新课件类别
        /// </summary>
        /// <param name="coursewareType">更新后的课件类别信息</param>
        public void UpdateCoursewareType(CoursewareType coursewareType)
        {
            dal.UpdateCoursewareType(coursewareType);
            objLogBill.WriteLog("修改课件类别“" + coursewareType.CoursewareTypeName + "”");
        }

        /// <summary>
        /// 删除课件类别
        /// </summary>
        /// <param name="coursewareType">要删除的课件类别</param>
        public void DeleteCoursewareType(CoursewareType coursewareType)
        {
            int code = 0;
            string strName = GetCoursewareType(coursewareType.CoursewareTypeId).CoursewareTypeName;
            dal.DeleteCoursewareType(coursewareType.CoursewareTypeId,ref code);
            if (code == 0)
            {
                objLogBill.WriteLog("删除课件类别“" + strName + "”");
            }
        }

        /// <summary>
        /// 删除课件类别
        /// </summary>
        /// <param name="coursewareTypeId">要删除的课件类别ID</param>
        public void DeleteCoursewareType(int coursewareTypeId, ref int errorCode)
        {
            int code = 0;
            string strName = GetCoursewareType(coursewareTypeId).CoursewareTypeName;
            dal.DeleteCoursewareType(coursewareTypeId,ref code);
            errorCode = code;
            if(code == 0)
            {
                objLogBill.WriteLog("删除课件类别“" + strName + "”");
            }
        }
    }
}
