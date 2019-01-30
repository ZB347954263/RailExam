using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class RandomExamTrainClassHaBLL
	{
		private static readonly RandomExamTrainClassHaDAL dal = new RandomExamTrainClassHaDAL();

		public IList<RandomExamTrainClassHa> GetRandomExamTrainClassByRandomExamID(int randomExamID)
		{
			return dal.GetRandomExamsTrainClassByRandomExamID(randomExamID);
		}


		public void AddRandomExamTrainClass(RandomExamTrainClassHa trainClass)
		{
			dal.AddRandomExamTrainClass(trainClass);
		}

		public void UpdateRandomExamTrainClass(RandomExamTrainClassHa trainClass)
		{
			dal.UpdateRandomExamTrainClass(trainClass);
		}

		public RandomExamTrainClassHa GetRandomExamTrainClassByRandomExamTrainClassID(int id)
		{
			return dal.GetRandomExamsTrainClassByRandomExamTrainClassID(id);
		}

		public void DeleteRandomExamTrainClassByRandomExamID(int randomExamID)
		{
			dal.DeleteRandomExamTrainClassByRandomExamID(randomExamID);
		}

		public IList<RandomExamTrainClassHa> GetRandomExamTrainClassCount(string trainclassID, int trainclasssubjectID, int postID)
		{
			return dal.GetRandomExamTrainClassCount(trainclassID, trainclasssubjectID, postID);
		}
	}
}
