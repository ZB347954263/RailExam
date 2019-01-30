using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class PaperStatusBLL
    {
        private static PaperStatusDAL dal = new PaperStatusDAL();

        /// <summary>
        /// ��ȡ�����Ծ�״̬
        /// </summary>
        /// <returns>�����Ծ�״̬</returns>
        public IList<PaperStatus> GetPaperStatuses()
        {
            return dal.GetPaperStatuses();
        }

        /// <summary>
        /// ��Ϊ��ѯʹ�õ��Ծ�״̬�����б�
        /// </summary>
        /// <param name="bForSearchUse">
        /// �Ƿ񹩲�ѯ�ã����ڣ�
        /// һ���ǣ������һ��ʾ�
        /// �������������ʾ�
        /// </param>
        /// <returns></returns>
        public IList<PaperStatus> GetPaperStatuses(bool bForSearchUse)
        {
            IList<PaperStatus> statuses = new List<PaperStatus>(dal.GetPaperStatuses());

            if (bForSearchUse)
            {
                PaperStatus status = new PaperStatus();

                status.StatusName = "-״̬-";
                status.PaperStatusId = -1;
                statuses.Insert(0, status);
            }

            return statuses;
        }
    }
}
