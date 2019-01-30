using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class PaperStatusBLL
    {
        private static PaperStatusDAL dal = new PaperStatusDAL();

        /// <summary>
        /// 获取所有试卷状态
        /// </summary>
        /// <returns>所有试卷状态</returns>
        public IList<PaperStatus> GetPaperStatuses()
        {
            return dal.GetPaperStatuses();
        }

        /// <summary>
        /// 作为查询使用的试卷状态下拉列表
        /// </summary>
        /// <param name="bForSearchUse">
        /// 是否供查询用，对于：
        /// 一、是，则添加一提示项；
        /// 二、否，则不添加提示项；
        /// </param>
        /// <returns></returns>
        public IList<PaperStatus> GetPaperStatuses(bool bForSearchUse)
        {
            IList<PaperStatus> statuses = new List<PaperStatus>(dal.GetPaperStatuses());

            if (bForSearchUse)
            {
                PaperStatus status = new PaperStatus();

                status.StatusName = "-状态-";
                status.PaperStatusId = -1;
                statuses.Insert(0, status);
            }

            return statuses;
        }
    }
}
