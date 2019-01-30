using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
	public class RandomExamResultAnswerStationBLL
	{
		private static readonly RandomExamResultAnswerStationDAL dal = new RandomExamResultAnswerStationDAL();

		public int AddExamResultAnswerStation(RandomExamResultAnswerStation examResultAnswer)
		{
			return dal.AddExamResultAnswerStation(examResultAnswer);
		}

		public int AddExamResultAnswerStation(IList<RandomExamResultAnswerStation> examResultAnswers)
		{
			return dal.AddExamResultAnswerStation(examResultAnswers);
		}


		public int UpdateExamResultAnswerStation(RandomExamResultAnswerStation examResultAnswer)
		{
			return dal.UpdateExamResultAnswerStation(examResultAnswer);
		}


		public int UpdateExamResultAnswerStation(IList<RandomExamResultAnswerStation> examResultAnswers)
		{
			return dal.UpdateExamResultAnswerStation(examResultAnswers);
		}
	}
}
