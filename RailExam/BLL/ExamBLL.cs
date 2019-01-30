using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class ExamBLL
    {
        private static readonly ExamDAL dal = new ExamDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 根据考试类型ID取考试信息
        /// </summary>
        /// <param name="examTypeId"></param>
        /// <returns></returns>
        public IList<Exam> GetExamsByExamTypeID(int examTypeId)
        {
            return dal.GetExamsByExamTypeID(examTypeId);
        }

        public IList<Exam> GetExamByUserId(string UserID, int orgID, int serverNo)
        {
            return dal.GetExamByUserId(UserID,orgID,serverNo);
        }

        public IList<Exam> GetComingExamByUserId(string UserID)
        {
            return dal.GetComingExamByUserId(UserID);
        }

        public IList<Exam> GetHistoryExamByUserId(string UserID)
        {
            return dal.GetHistoryExamByUserId(UserID);
        }

        public IList<Exam> GetNowExam()
        {
            return dal.GetNowExam();
        }

        public IList<Exam> GetComingExam()
        {
            return dal.GetComingExam();
        }

        public IList<Exam> GetHistoryExam()
        {
            return dal.GetHistoryExam();
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
            return dal.GetExams(examTypeId, examName, beginDateTime, endDateTime);
        }

        public IList<Exam> GetExamsByOrgID(string examName, DateTime beginDateTime, DateTime endDateTime,int orgID)
        {
            return dal.GetExamsByOrgID( examName, beginDateTime, endDateTime,orgID);
        }

		public IList<Exam> GetExamsInfoByOrgID(string examName,int CategoryId, DateTime beginDateTime, DateTime endDateTime, int orgID, string isServerCenter)
        {
            IList<Exam> objList = dal.GetExamsInfoByOrgID(examName, CategoryId, beginDateTime, endDateTime, orgID, isServerCenter);
            return objList;
        }

        public IList<Exam> GetExamByCategoryID(int CategoryID)
        {
            IList<Exam> ExamList = dal.GetExamByCategoryID(CategoryID);
            return ExamList;
        }

        public IList<Exam> GetTopExams(string EmployeeID)
        {
            IList<Exam> ExamList = dal.GetTopExams(EmployeeID);
            return ExamList;
        }


        public IList<Exam> GetExamByExamCategoryIDPath(string ExamCategoryIDPath, string ExamName, int CreateMode, string CreatePerson,int orgId)
        {
            OrganizationBLL objOrgBll = new OrganizationBLL();
            IList<Exam> ExamList = dal.GetExamByExamCategoryIDPath(ExamCategoryIDPath, ExamName, CreateMode, CreatePerson, orgId);
            foreach (Exam exam in ExamList)
            {
                exam.StationName = objOrgBll.GetOrganization(exam.OrgId).ShortName;
            }
            return ExamList;
        }

        public Exam GetExam(int ExamId)
        {
            return dal.GetExam(ExamId);
        }

        public int AddExam(Exam exam)
        {
            int id = dal.AddExam(exam);
            objLogBll.WriteLog("新增考试："+  exam.ExamName );
            return id;
        }

        public void UpdateExamPaper(int ExamId, int PaperId)
        {
            dal.UpdateExamPaper(ExamId, PaperId);
            string strName = GetExam(ExamId).ExamName;
            objLogBll.WriteLog("修改考试：" + strName);
        }

        public void UpdateExam(Exam exam)
        {
            dal.UpdateExam(exam);
            objLogBll.WriteLog("修改考试：" + exam.ExamName);
        }

        public void DeleteExam(int ExamId)
        {
            string strName = GetExam(ExamId).ExamName;
            objLogBll.WriteLog("删除考试：" + strName);
            dal.DeleteExam(ExamId);
        }

        
        public IList<Exam> GetExamsInfoDesktop(int orgID)
        {
            OrganizationBLL objOrgBll = new OrganizationBLL();
            IList<Exam> objList = dal.GetExamsInfoDesktop(orgID);
            foreach (Exam exam in objList)
            {
                exam.StationName = objOrgBll.GetOrganization(exam.OrgId).ShortName;
            }
            return objList;
        }

		public IList<Exam> GetIsNotComputerExamsInfo(int orgID)
		{
			return dal.GetIsNotComputerExamsInfo(orgID);
		}

		public IList<Exam> GetListtWithOrg(int OrgID, DateTime DateFrom, DateTime DateTo, int style)
		{
			IList<Exam> objList = dal.GetListWithOrg(OrgID, DateFrom, DateTo,style);

			int sumExamineeCount = 0;

			foreach (Exam exam in objList)
			{
				sumExamineeCount += exam.ExamineeCount;
			}
			Exam obj=new Exam();
			obj.ExamineeCount = sumExamineeCount;
			obj.ExamName = "合计";
			objList.Add(obj);

			return objList;
		}
     }
}
