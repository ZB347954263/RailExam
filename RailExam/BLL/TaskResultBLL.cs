using System;
using System.Collections.Generic;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    public class TaskResultBLL
    {
        public static TaskResultDAL dal = new TaskResultDAL();

        /// <summary>
        /// 添加培训目标作业结果
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int AddTaskResult(TaskResult taskResult, string[] strAnswers)
        {
            return dal.AddTaskResult(taskResult, strAnswers);
        }

        /// <summary>
        /// 按结果编号获取培训目标作业结果
        /// </summary>
        /// <returns>作业结果</returns>
        public TaskResult GetTaskResult(int taskResultId)
        {
            return dal.GetTaskResult(taskResultId);

        }

        /// <summary>
        /// 按查询条件获取培训目标作业结果
        /// </summary>
        /// <returns>结果集合</returns>
        public TaskResult GetTaskResult(int paperID, int trainTypeId, int employeeId)
        {
            return dal.GetTaskResult(paperID, trainTypeId, employeeId);
        }

        /// <summary>
        /// 按查询条件获取培训目标作业结果
        /// </summary>
        /// <returns>结果集合</returns>
        public IList<TaskResult> GetTaskResults(int organizationId, int paperID, int trainTypeId, string employeeName,
           decimal scoreLower, decimal scoreUpper, int statusId, int currentPageIndex, int pageSize, string orderBy)
        {
            return dal.GetTaskResults(organizationId, paperID, trainTypeId, employeeName,
                scoreLower, scoreUpper, statusId, currentPageIndex, pageSize, orderBy);
        }

        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRecordCount()
        {
            return dal.RecordCount;
        }

        /// <summary>
        /// 更新评卷开始时间
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int UpdateJudgeBeginTime(int taskResultId, DateTime dateTime)
        {
            return dal.UpdateJudgeBeginTime(taskResultId, dateTime);
        }

        /// <summary>
        /// 更新培训目标作业答案
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int UpdateTaskResultAnswers(int taskResultId, IList<TaskResultAnswer> answers)
        {
            return dal.UpdateTaskResultAnswers(taskResultId, answers);
        }

        /// <summary>
        /// 更新培训目标作业成绩结果和答案
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int UpdateTaskResultAndItsAnswers(TaskResult taskResult, IList<TaskResultAnswer> taskResultAnswers)
        {
            return dal.UpdateTaskResultAndItsAnswers(taskResult, taskResultAnswers);
        }

        /// <summary>
        /// 更新培训目标作业成绩结果
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int UpdateTaskResult(TaskResult taskResult)
        {
            return dal.UpdateTaskResult(taskResult);
        }
    }
}
