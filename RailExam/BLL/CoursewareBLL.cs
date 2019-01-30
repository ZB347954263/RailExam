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
        /// ���ݿμ�����IDPath��ѯ�μ�
        /// </summary>
        /// <param name="coursewareTypeIDPath">�μ�����IDPath</param>
        /// <param name="orgID">��ǰ�û�վ�α�ţ���վ�α��Ϊ1ʱȡ���пμ�</param>
        /// <returns></returns>
        public IList<Courseware> GetCoursewaresByCoursewareTypeIDPath(string coursewareTypeIDPath,int orgID)
        {
            IList<Courseware> coursewareList = dal.GetCoursewaresByCoursewareTypeIDPath(coursewareTypeIDPath, orgID);
            return coursewareList;
        }

        /// <summary>
        /// ������ѵ����IDPath��ѯ�μ�
        /// </summary>
        /// <param name="trainTypeIDPath">��ѵ����IDPath</param>
        /// <param name="orgID">��ǰ�û�վ�α�ţ���վ�α��Ϊ1ʱȡ���пμ�</param>
        /// <returns></returns>
        public IList<Courseware> GetCoursewaresByTrainTypeIDPath(string trainTypeIDPath,int orgID)
        {
            IList<Courseware> coursewareList = dal.GetCoursewaresByTrainTypeIDPath(trainTypeIDPath,orgID);
            return coursewareList;
        }

        /// <summary>
        ///  ���ݿμ�����ID��ѯ�μ�
        /// </summary>
        /// <param name="coursewareTypeID">�μ�����ID</param>
        /// <param name="orgID">��ǰ�û�վ�α�ţ���վ�α��Ϊ1ʱȡ���пμ�</param>
        /// <returns></returns>
        public IList<Courseware> GetCoursewaresByCoursewareTypeID(int coursewareTypeID, int orgID)
        {
            IList<Courseware> coursewareList = dal.GetCoursewaresByCoursewareTypeID(coursewareTypeID, orgID);
            return coursewareList;
        }

        /// <summary>
        /// ������ѵ����ID��ѯ�μ�
        /// </summary>
        /// <param name="trainTypeID"><��ѵ����ID/param>
        /// <param name="orgID">��ǰ�û�վ�α�ţ���վ�α��Ϊ1ʱȡ���пμ�</param>
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
        /// ���ݸ���ʱ�䵹�򷵻ؿμ���Ϣ
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
            objLogBll.WriteLog("�����μ���"��+ courseware.CoursewareName + "��������Ϣ"��);
            return id;
        }

        public void UpdateCourseware(Courseware courseware)
        {
            dal.UpdateCourseware(courseware);
            objLogBll.WriteLog("�޸Ŀμ���" + courseware.CoursewareName + "��������Ϣ");
        }

        public void DeleteCourseware(int coursewareID)
        {
            string strName = GetCourseware(coursewareID).CoursewareName;
            objLogBll.WriteLog("ɾ���μ���" + strName + "��������Ϣ");
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