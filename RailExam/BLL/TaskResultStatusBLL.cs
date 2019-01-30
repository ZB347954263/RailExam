using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TaskResultStatusBLL
    {
        private static readonly TaskResultStatusDAL dal = new TaskResultStatusDAL();

        /// <summary>
        /// ��ѯ������ҵ���״̬
        /// </summary>
        /// <returns>������ҵ���״̬</returns>
        public IList<TaskResultStatus> GetTaskResultStatuses()
        {
            return dal.GetTaskResultStatuses();
        }

        /// <summary>
        /// ��Ϊ��ѯ����ʹ�õ���ҵ���״̬
        /// </summary>
        /// <param name="bForSearchUse">
        /// �Ƿ񹩲�ѯ�ã����ڣ�
        /// һ���ǣ������һ��ʾ�
        /// �������������ʾ�
        /// </param>
        /// <returns>��Ϊ��ѯ����ʹ�õ���ҵ���״̬</returns>
        public IList<TaskResultStatus> GetTaskResultStatuses(bool bForSearchUse)
        {            
            IList<TaskResultStatus> statuses = new List<TaskResultStatus>(dal.GetTaskResultStatuses());

            if (bForSearchUse)
            {
                TaskResultStatus status = new TaskResultStatus();

                status.StatusName = "-״̬-";
                status.TaskResultStatusId = -1;
                statuses.Insert(0, status);
            }

            return statuses;
        }
    }
}
