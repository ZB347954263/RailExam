using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TaskJudgeStatusBLL
    {
        private static readonly TaskJudgeStatusDAL dal = new TaskJudgeStatusDAL();

        /// <summary>
        /// 查询所有作业评分状态
        /// </summary>
        /// <returns>所有作业评分状态</returns>
        public IList<TaskJudgeStatus> GetTaskJudgeStatuses()
        {
            return dal.GetTaskJudgeStatuses();
        }
    }
}
