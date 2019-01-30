using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TaskResultStatusBLL
    {
        private static readonly TaskResultStatusDAL dal = new TaskResultStatusDAL();

        /// <summary>
        /// 查询所有作业结果状态
        /// </summary>
        /// <returns>所有作业结果状态</returns>
        public IList<TaskResultStatus> GetTaskResultStatuses()
        {
            return dal.GetTaskResultStatuses();
        }

        /// <summary>
        /// 作为查询条件使用的作业结果状态
        /// </summary>
        /// <param name="bForSearchUse">
        /// 是否供查询用，对于：
        /// 一、是，则添加一提示项；
        /// 二、否，则不添加提示项；
        /// </param>
        /// <returns>作为查询条件使用的作业结果状态</returns>
        public IList<TaskResultStatus> GetTaskResultStatuses(bool bForSearchUse)
        {            
            IList<TaskResultStatus> statuses = new List<TaskResultStatus>(dal.GetTaskResultStatuses());

            if (bForSearchUse)
            {
                TaskResultStatus status = new TaskResultStatus();

                status.StatusName = "-状态-";
                status.TaskResultStatusId = -1;
                statuses.Insert(0, status);
            }

            return statuses;
        }
    }
}
