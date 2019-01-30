using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    public class ExamResultBLL
    {
        public static ExamResultDAL dal = new ExamResultDAL();

        public int UpdateJudgeId(int examResultId, int judgeId)
        {
            return dal.UpdateJudgeId(examResultId, judgeId);
        }

        public int UpdateExamResult(ExamResult examResult)
        {
            return dal.UpdateExamResult(examResult);
        }

        public int UpdateJudgeBeginTime(int examResultId, DateTime beginTime)
        {
            return dal.UpdateJudgeBeginTime(examResultId, beginTime);
        }

        public int UpdateExamResultAnswers(int examResultId, IList<ExamResultAnswer> examResultAnswers, string strResultCause, string strRemark, decimal oldScore, string EmployeeName)
        {
            return dal.UpdateExamResultAnswers(examResultId, examResultAnswers, strResultCause, strRemark, oldScore, EmployeeName);
        }

        public int UpdateExamResultAndItsAnswers(ExamResult examResult, IList<ExamResultAnswer> examResultAnswers)
        {
            return dal.UpdateExamResultAndItsAnswers(examResult, examResultAnswers);
        }

        /// <summary>
        /// ȡ��¼��
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            return dal.RecordCount;
        }

        /// <summary>
        /// ȡ�����������ID�Ŀ��Կ������
        /// </summary>
        /// <param name="examResultId"></param>
        /// <returns></returns>
        public ExamResult GetExamResult(int examResultId)
        {
            return dal.GetExamResult(examResultId);
        }

        /// <summary>
        /// ȡ�����������ID�Ŀ��Կ��������·�ֲ�վ�Σ�
        /// </summary>
        /// <param name="examResultId"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public ExamResult GetExamResultByOrgID(int examResultId,int orgID)
        {
            return dal.GetExamResultByOrgID(examResultId,orgID);
        }

        public ExamResult GetExamResult(int paperID, int examID, int examineeID)
        {
            return dal.GetExamResult(paperID, examID, examineeID);
        }

        public IList<ExamResult> GetExamResultByExamID(int examID)
        {
            return dal.GetExamResultByExamID(examID);
        }
       

        /// <summary>
        /// ���������Ծ�
        /// </summary>
        /// <returns>�����Ծ�</returns>
        public IList<ExamResult> GetExamResults()
        {
            return dal.GetExamResults();
        }

        /// <summary>
        /// ���ؿ��������Ծ�
        /// </summary>
        /// <param name="examineeId">�������</param>
        /// <param name="count">��ʾ��������¼</param>
        /// <returns>�Ծ��б�</returns>
        public IList<ExamResult> GetExamResults(string examineeId, int count)
        {
            if(string.IsNullOrEmpty(examineeId))
            {
                examineeId = Int32.MinValue.ToString();
            }

            IList<ExamResult> results = dal.GetExamResults(int.Parse(examineeId));
            
            if(count != 0)
            {
                if (results.Count > count)
                {
                    for (int i = count; i < results.Count; i++)
                    {
                        results.RemoveAt(i);
                    }
                }
            }

            return results;
        }

        public int AddExamResult(ExamResult examResult, string[] strAnswers)
        {
            return dal.AddExamResult(examResult,strAnswers);
        }

        public void AddExamResultToServer(ExamResult examResult)
        {
             dal.AddExamResultToServer(examResult);
        }

        public int DeleteExamResult(int examResultId)
        {
            return dal.DeleteExamResult(examResultId);
        }

        public int DeleteExamResults(int[] examResultIds)
        {
            return dal.DeleteExamResults(examResultIds);
        }

        public IList<ExamResult> GetExamResults( int examId, string strOrganizationName, string examineeName,string workno,
            decimal paperTotalScoreLower, decimal paperTotalScoreUpper, int examResultStatusId)
        {
            return dal.GetExamResults( examId, strOrganizationName, examineeName,workno,
                paperTotalScoreLower, paperTotalScoreUpper, examResultStatusId);
        }


        public IList<ExamResult> GetExamResultsByOrgID( int examId, string strOrganizationName, string examineeName,string workno,
            decimal paperTotalScoreLower, decimal paperTotalScoreUpper, int examResultStatusId, int orgID)
        {
            return dal.GetExamResultsByOrgID( examId, strOrganizationName, examineeName,workno,
				paperTotalScoreLower, paperTotalScoreUpper, examResultStatusId, orgID);
        }

        /// <summary>
        /// ���ط��ϲ�ѯ�������Ծ�
        /// </summary>
        /// <returns>���ϲ�ѯ�������Ծ�</returns>
        public IList<ExamResult> GetExamResults(int examId, string organizationName, /*int examId,*/ string examineeName, 
            decimal scoreLower, decimal scoreUpper, int statusId, /*int judgeId,*/ int currentPageIndex, int pageSize, string orderBy)
        {
            return dal.GetExamResults(examId, organizationName, /*examId,*/ examineeName, scoreLower, scoreUpper, statusId, /*judgeId,*/ 
                currentPageIndex, pageSize, orderBy);
        }
    }
}
