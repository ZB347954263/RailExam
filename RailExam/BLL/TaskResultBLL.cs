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
        /// �����ѵĿ����ҵ���
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int AddTaskResult(TaskResult taskResult, string[] strAnswers)
        {
            return dal.AddTaskResult(taskResult, strAnswers);
        }

        /// <summary>
        /// �������Ż�ȡ��ѵĿ����ҵ���
        /// </summary>
        /// <returns>��ҵ���</returns>
        public TaskResult GetTaskResult(int taskResultId)
        {
            return dal.GetTaskResult(taskResultId);

        }

        /// <summary>
        /// ����ѯ������ȡ��ѵĿ����ҵ���
        /// </summary>
        /// <returns>�������</returns>
        public TaskResult GetTaskResult(int paperID, int trainTypeId, int employeeId)
        {
            return dal.GetTaskResult(paperID, trainTypeId, employeeId);
        }

        /// <summary>
        /// ����ѯ������ȡ��ѵĿ����ҵ���
        /// </summary>
        /// <returns>�������</returns>
        public IList<TaskResult> GetTaskResults(int organizationId, int paperID, int trainTypeId, string employeeName,
           decimal scoreLower, decimal scoreUpper, int statusId, int currentPageIndex, int pageSize, string orderBy)
        {
            return dal.GetTaskResults(organizationId, paperID, trainTypeId, employeeName,
                scoreLower, scoreUpper, statusId, currentPageIndex, pageSize, orderBy);
        }

        /// <summary>
        /// ��ȡ��¼��
        /// </summary>
        public int GetRecordCount()
        {
            return dal.RecordCount;
        }

        /// <summary>
        /// ��������ʼʱ��
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int UpdateJudgeBeginTime(int taskResultId, DateTime dateTime)
        {
            return dal.UpdateJudgeBeginTime(taskResultId, dateTime);
        }

        /// <summary>
        /// ������ѵĿ����ҵ��
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int UpdateTaskResultAnswers(int taskResultId, IList<TaskResultAnswer> answers)
        {
            return dal.UpdateTaskResultAnswers(taskResultId, answers);
        }

        /// <summary>
        /// ������ѵĿ����ҵ�ɼ�����ʹ�
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int UpdateTaskResultAndItsAnswers(TaskResult taskResult, IList<TaskResultAnswer> taskResultAnswers)
        {
            return dal.UpdateTaskResultAndItsAnswers(taskResult, taskResultAnswers);
        }

        /// <summary>
        /// ������ѵĿ����ҵ�ɼ����
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int UpdateTaskResult(TaskResult taskResult)
        {
            return dal.UpdateTaskResult(taskResult);
        }
    }
}
