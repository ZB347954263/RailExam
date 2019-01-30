using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：考试评分状态
    /// </summary>
    public class ExamJudgeStatusBLL
    {
        private static readonly ExamJudgeStatusDAL dal = new ExamJudgeStatusDAL();

        /// <summary>
        /// 新增考试评分状态
        /// </summary>
        /// <param name="examJudgeStatus">考试评分状态</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddExamJudgeStatus(ExamJudgeStatus examJudgeStatus)
        {
            return dal.AddExamJudgeStatus(examJudgeStatus);
        }

        /// <summary>
        /// 删除考试评分状态
        /// </summary>
        /// <param name="examJudgeStatusId">考试评分状态ID</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteExamJudgeStatus(int examJudgeStatusId)
        {
            return dal.DeleteExamJudgeStatus(examJudgeStatusId);
        }

        /// <summary>
        /// 修改考试评分状态
        /// </summary>
        /// <param name="examJudgeStatus">考试评分状态</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateExamJudgeStatus(ExamJudgeStatus examJudgeStatus)
        {
            return dal.UpdateExamJudgeStatus(examJudgeStatus);
        }

        /// <summary>
        /// 按考试评分状态ID取考试评分状态
        /// </summary>
        /// <param name="examJudgeStatusId">考试评分状态ID</param>
        /// <returns>考试评分状态</returns>
        public ExamJudgeStatus GetExamJudgeStatus(int examJudgeStatusId)
        {
            return dal.GetExamJudgeStatus(examJudgeStatusId);
        }

        /// <summary>
        /// 查询所有考试评分状态
        /// </summary>
        /// <returns>所有考试评分状态</returns>
        public IList<ExamJudgeStatus> GetExamJudgeStatuses()
        {
            return dal.GetExamJudgeStatuses();
        }

        /// <summary>
        /// 查询符合条件的考试评分状态
        /// </summary>
        /// <param name="examJudgeStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>符合条件的考试评分状态</returns>
        public IList<ExamJudgeStatus> GetExamJudgeStatuses(int examJudgeStatusId, string statusName,
            string description, int isDefault, decimal scoreRate, string memo)
        {
            return dal.GetExamJudgeStatuses(examJudgeStatusId, statusName,
                                      description, isDefault, scoreRate, memo);
        }
    }
}