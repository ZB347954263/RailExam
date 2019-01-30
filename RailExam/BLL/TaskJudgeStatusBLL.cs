using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TaskJudgeStatusBLL
    {
        private static readonly TaskJudgeStatusDAL dal = new TaskJudgeStatusDAL();

        /// <summary>
        /// ��ѯ������ҵ����״̬
        /// </summary>
        /// <returns>������ҵ����״̬</returns>
        public IList<TaskJudgeStatus> GetTaskJudgeStatuses()
        {
            return dal.GetTaskJudgeStatuses();
        }
    }
}
