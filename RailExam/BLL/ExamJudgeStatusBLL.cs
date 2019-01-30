using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼�����������״̬
    /// </summary>
    public class ExamJudgeStatusBLL
    {
        private static readonly ExamJudgeStatusDAL dal = new ExamJudgeStatusDAL();

        /// <summary>
        /// ������������״̬
        /// </summary>
        /// <param name="examJudgeStatus">��������״̬</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int AddExamJudgeStatus(ExamJudgeStatus examJudgeStatus)
        {
            return dal.AddExamJudgeStatus(examJudgeStatus);
        }

        /// <summary>
        /// ɾ����������״̬
        /// </summary>
        /// <param name="examJudgeStatusId">��������״̬ID</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int DeleteExamJudgeStatus(int examJudgeStatusId)
        {
            return dal.DeleteExamJudgeStatus(examJudgeStatusId);
        }

        /// <summary>
        /// �޸Ŀ�������״̬
        /// </summary>
        /// <param name="examJudgeStatus">��������״̬</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int UpdateExamJudgeStatus(ExamJudgeStatus examJudgeStatus)
        {
            return dal.UpdateExamJudgeStatus(examJudgeStatus);
        }

        /// <summary>
        /// ����������״̬IDȡ��������״̬
        /// </summary>
        /// <param name="examJudgeStatusId">��������״̬ID</param>
        /// <returns>��������״̬</returns>
        public ExamJudgeStatus GetExamJudgeStatus(int examJudgeStatusId)
        {
            return dal.GetExamJudgeStatus(examJudgeStatusId);
        }

        /// <summary>
        /// ��ѯ���п�������״̬
        /// </summary>
        /// <returns>���п�������״̬</returns>
        public IList<ExamJudgeStatus> GetExamJudgeStatuses()
        {
            return dal.GetExamJudgeStatuses();
        }

        /// <summary>
        /// ��ѯ���������Ŀ�������״̬
        /// </summary>
        /// <param name="examJudgeStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>���������Ŀ�������״̬</returns>
        public IList<ExamJudgeStatus> GetExamJudgeStatuses(int examJudgeStatusId, string statusName,
            string description, int isDefault, decimal scoreRate, string memo)
        {
            return dal.GetExamJudgeStatuses(examJudgeStatusId, statusName,
                                      description, isDefault, scoreRate, memo);
        }
    }
}