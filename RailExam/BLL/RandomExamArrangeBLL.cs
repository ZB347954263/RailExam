using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class RandomExamArrangeBLL
    {
        private static readonly RandomExamArrangeDAL dal = new RandomExamArrangeDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        public int AddRandomExamArrange(RandomExamArrange randomExamArrange)
        {
            return dal.AddRandomExamArrange(randomExamArrange);
        }

        public void UpdateRandomExamArrange(int RandomExamId, string strUserIds)
        {
            dal.UpdateRandomExamArrange(RandomExamId, strUserIds);
        }

		public int AddRandomExamArrangeToServer(RandomExamArrange randomExamArrange)
		{
			return dal.AddRandomExamArrangeToServer(randomExamArrange);
		}

		public void UpdateRandomExamArrangeToServer(int RandomExamId, string strUserIds)
		{
			dal.UpdateRandomExamArrangeToServer(RandomExamId, strUserIds);
		}

        public IList<RandomExamArrange> GetRandomExamArranges(int RandomExamId)
        {
            return dal.GetRandomExamArranges(RandomExamId);
        }

		public void DeleteRandomExamArrangeByRandomExamID(int ExamId)
		{
			dal.DeleteRandomExamArrangeByRandomExamID(ExamId);
		}

        public void UpdateRandomExamArrangeDetailToServer(int randomExamDetailId,string strUserIds)
        {
            dal.UpdateRandomExamArrangeDetailToServer(randomExamDetailId,strUserIds);
        }

        public void RefreshRandomExamArrange()
        {
            dal.RefreshRandomExamArrange();
        }

        public void RunUpdateProcedure(bool isToCenter, string storedProcName, IDataParameter[] parameters, byte[] tempbuff)
        {
            dal.RunUpdateProcedure(isToCenter, storedProcName, parameters, tempbuff);
        }
    }
}
