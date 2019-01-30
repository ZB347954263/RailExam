using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class RandomExamBLL
    {
        private static readonly RandomExamDAL dal = new RandomExamDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        public IList<RandomExam> GetExamByExamCategoryIDPath(string ExamCategoryIDPath, string ExamName, string CreatePerson, int orgId, int ExamTimeType,int ExamStyleID,int railSystemID)
        {
            OrganizationBLL objOrgBll = new OrganizationBLL();
            IList<RandomExam> ExamList = dal.GetExamByExamCategoryIDPath(ExamCategoryIDPath, ExamName, CreatePerson, orgId, ExamTimeType, ExamStyleID, railSystemID);
            foreach (RandomExam exam in ExamList)
            {
                exam.StationName = objOrgBll.GetOrganization(exam.OrgId).ShortName;
            }
            return ExamList;
        }

        public RandomExam GetExam(int ExamId)
        {
            return dal.GetExam(ExamId);
        }

		public RandomExam GetExamServer(int ExamId)
		{
			return dal.GetRandomExamServer(ExamId);
		}

        public int AddExam(RandomExam exam)
        {
            int id = dal.AddExam(exam);
            objLogBll.WriteLog("新增随机考试：" + exam.ExamName);
            return id;
        }

        public void UpdateExam(RandomExam exam)
        {
            dal.UpdateExam(exam);
            objLogBll.WriteLog("修改随机考试：" + exam.ExamName);
        }

        public void DeleteExam(int ExamId)
        {
            string strName = GetExam(ExamId).ExamName;
            objLogBll.WriteLog("删除随机考试：" + strName);
            dal.DeleteExam(ExamId);
        }

        public IList<RandomExam> GetRandomExamsInfo(int orgid)
        {
            OrganizationBLL objOrgBll = new OrganizationBLL();
            IList<RandomExam> ExamList = dal.GetRandomExamsInfo(orgid);
            foreach (RandomExam exam in ExamList)
            {
                exam.StationName = objOrgBll.GetOrganization(exam.OrgId).ShortName;
            }
            return ExamList;
        }

		public IList<RandomExam> GetControlRandomExamsInfo(int orgid,int serverNo)
		{
			OrganizationBLL objOrgBll = new OrganizationBLL();
            IList<RandomExam> ExamList = dal.GetControlRandomExamsInfo(orgid, serverNo);
			foreach (RandomExam exam in ExamList)
			{
				exam.StationName = objOrgBll.GetOrganization(exam.OrgId).ShortName;
			}
			return ExamList;
		}

        //修改是否生成试卷状态
        public void UpdateHasPaper(int randomExamID, int serverNo, bool b)
        {
            dal.UpdateHasPaper(randomExamID, serverNo, b);
        }

        //修改考试是否开始
        public void UpdateIsStart(int randomExamID, int serverNo, int isStart)
        {
            dal.UpdateIsStart(randomExamID, serverNo, isStart);
        }

        public void UpdateStatusID(int randomExamID, int serverNo, int StatusID)
        {
            dal.UpdateStatusID(randomExamID, serverNo, StatusID);
        }


        //修改考试验证码
        public void UpdateStartCode(int randomExamID, int serverNo, string code)
		{
            dal.UpdateStartCode(randomExamID, serverNo, code);
		}

        //修改考试是否上传
    	public void UpdateIsUpload(int randomExamID, int serverNo, int isUpload)
		{
            dal.UpdateIsUpload(randomExamID, serverNo, isUpload);
		}

        public void UpdateStartMode(int randomExamID, int startMode)
        {
            dal.UpdateStartMode(randomExamID, startMode);
        }


        public void StartExamBySecondServer(int randomExamID, string code)
        {
            dal.StartExamBySecondServer(randomExamID, code);
        }

        public void RandomExamRefresh()
        {
            dal.RandomExamRefresh();
        }


		/// <summary>
		/// 查找到期但还没有结束的考试
		/// </summary>
		/// <param name="orgid"></param>
		/// <returns></returns>
		public IList<RandomExam> GetOverdueNotEndRandomExam(int orgid)
		{
			OrganizationBLL objOrgBll = new OrganizationBLL();
			IList<RandomExam> ExamList = dal.GetOverdueNotEndRandomExam(orgid);
			foreach (RandomExam exam in ExamList)
			{
				exam.StationName = objOrgBll.GetOrganization(exam.OrgId).ShortName;
			}
			return ExamList;
		}

		public void UpdateVersion(int randomExamID)
		{
			dal.UpdateVersion(randomExamID);
		}

		public void RefreshRandomExam()
		{
			dal.RefreshRandomExam();
		}

		public DateTime GetRandomExamTimeByExamID(int randomExamID)
		{
			return dal.GetRandomExamTimeByExamID(randomExamID);
		}

		/// <summary>
		/// 复制新增
		/// </summary>
		/// <param name="examID">被复制的考试ID</param>
		public void AddCopyRandomExam(int examID, string examName)
		{
			dal.AddCopyRandomExam(examID,examName);
		}

		/// <summary>
		/// 生成补考考试
		/// </summary>
		/// <param name="examID">被复制的考试ID</param>
		public int AddResetRandomExam(int examID)
		{
			return dal.AddResetRandomExam(examID);
		}

        public IList<RandomExam> GetSaveExam(string strWhereClause)
        {
            return dal.GetSaveExam(strWhereClause);
        }
    }
}
