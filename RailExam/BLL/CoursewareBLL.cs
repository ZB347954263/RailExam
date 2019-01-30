using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class CoursewareBLL
    {
        private static readonly CoursewareDAL dal = new CoursewareDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        private IList<Courseware> Getcourseware(int coursewareID, string coursewareName, int coursewareTypeID, 
            int provideOrg, DateTime publishDate, string authors, string keyWord, string revisers, string url,
            string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Courseware> coursewareList = dal.GetCourseware(coursewareID, coursewareName, coursewareTypeID, 
                provideOrg, publishDate, authors, keyWord, revisers, url, description, memo, startRowIndex, maximumRows, orderBy);

            return coursewareList;
        }

        /// <summary>
        /// 根据课件类别的IDPath查询课件
        /// </summary>
        /// <param name="coursewareTypeIDPath">课件类别的IDPath</param>
        /// <param name="orgID">当前用户站段编号：当站段编号为1时取所有课件</param>
        /// <returns></returns>
        public IList<Courseware> GetCoursewaresByCoursewareTypeIDPath(string coursewareTypeIDPath,int orgID)
        {
            IList<Courseware> coursewareList = dal.GetCoursewaresByCoursewareTypeIDPath(coursewareTypeIDPath, orgID);
            return coursewareList;
        }

        /// <summary>
        /// 根据培训类别的IDPath查询课件
        /// </summary>
        /// <param name="trainTypeIDPath">培训类别的IDPath</param>
        /// <param name="orgID">当前用户站段编号：当站段编号为1时取所有课件</param>
        /// <returns></returns>
        public IList<Courseware> GetCoursewaresByTrainTypeIDPath(string trainTypeIDPath,int orgID)
        {
            IList<Courseware> coursewareList = dal.GetCoursewaresByTrainTypeIDPath(trainTypeIDPath,orgID);
            return coursewareList;
        }

        /// <summary>
        ///  根据课件类别的ID查询课件
        /// </summary>
        /// <param name="coursewareTypeID">课件类别的ID</param>
        /// <param name="orgID">当前用户站段编号：当站段编号为1时取所有课件</param>
        /// <returns></returns>
        public IList<Courseware> GetCoursewaresByCoursewareTypeID(int coursewareTypeID, int orgID)
        {
            IList<Courseware> coursewareList = dal.GetCoursewaresByCoursewareTypeID(coursewareTypeID, orgID);
            return coursewareList;
        }

        /// <summary>
        /// 根据培训类别的ID查询课件
        /// </summary>
        /// <param name="trainTypeID"><培训类别的ID/param>
        /// <param name="orgID">当前用户站段编号：当站段编号为1时取所有课件</param>
        /// <returns></returns>
        public IList<Courseware> GetCoursewaresByTrainTypeID(int trainTypeID, int orgID)
        {
            IList<Courseware> coursewareList = dal.GetCoursewaresByTrainTypeID(trainTypeID, orgID);
            return coursewareList;
        }

        public IList<Courseware> GetCoursewares(int coursewareTypeID, int trainTypeID, string coursewareName, string keyWords, string authors,int orgID)
        {
            IList<Courseware> coursewareList = dal.GetCoursewares(coursewareTypeID, trainTypeID, coursewareName, keyWords, authors,orgID);
            return coursewareList;
        }

        public IList<Courseware> GetEmployeeStudyCoursewareInfo(int trainTypeID, int orgID, int postID,bool isGroupleader,int techniciantypeid, int row)
        {
            return dal.GetEmployeeStudyCoursewareInfo(trainTypeID, orgID, postID,isGroupleader,techniciantypeid,row);
        }

        public IList<Courseware> GetStudyCoursewareInfoByTrainTypeID(int trainTypeID, int orgID, int postID,int isGroupleader, int techid, int row)
        {
            return dal.GetStudyCoursewareInfoByTrainTypeID(trainTypeID, orgID, postID,isGroupleader,techid,row);
        }

        public IList<Courseware> GetStudyCoursewareInfoByTypeID(int typeid, int orgID, int postID, int isGroupleader, int techid, int row)
        {
            return dal.GetStudyCoursewareInfoByTypeID(typeid, orgID, postID, isGroupleader, techid, row);
        }
        /// <summary>
        /// 根据更新时间倒序返回课件信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public IList<Courseware> GetCoursewareInfoByDate(int row)
        {
            return dal.GetCoursewareInfoByDate(row);
        }

        public Courseware GetCourseware(int coursewareID)
        {
            return dal.GetCourseware(coursewareID);
        }

        public int AddCourseware(Courseware courseware)
        {
            int id = dal.AddCourseware(courseware);
            objLogBll.WriteLog("新增课件《"　+ courseware.CoursewareName + "》基本信息"　);
            return id;
        }

        public void UpdateCourseware(Courseware courseware)
        {
            dal.UpdateCourseware(courseware);
            objLogBll.WriteLog("修改课件《" + courseware.CoursewareName + "》基本信息");
        }

        public void DeleteCourseware(int coursewareID)
        {
            string strName = GetCourseware(coursewareID).CoursewareName;
            objLogBll.WriteLog("删除课件《" + strName + "》基本信息");
            dal.DeleteCourseware(coursewareID);
        }

        public IList<Courseware> GetCoursewaresByCoursewareTypeOnline(int orgid,int postid, string idpath, bool isGroupleader, int techniciantypeid)
        {
            return dal.GetCoursewaresByCoursewareTypeOnline(orgid, postid, idpath, isGroupleader, techniciantypeid);
        }

		public IList<Courseware> GetCoursewaresByPostID(int postID, int orgID)
		{
			return dal.GetCoursewaresByPostID(postID, orgID);
		}

		public IList<Courseware> GetCoursewaresByPostID(int postID, string coursewareName, string keyWord, string authors, int orgID)
		{
			return dal.GetCoursewaresByPostID(postID, coursewareName, keyWord, authors, orgID);
		}
    }
}