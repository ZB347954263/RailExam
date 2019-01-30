using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class RandomExamTrainClassBLL
	{
		private static readonly RandomExamTrainClassDAL dal = new RandomExamTrainClassDAL();

		public IList<RandomExamTrainClass> GetRandomExamTrainClassByRandomExamID(int randomExamID)
		{
			return dal.GetRandomExamsTrainClassByRandomExamID(randomExamID);
		}


		public void AddRandomExamTrainClass(RandomExamTrainClass trainClass)
		{
			dal.AddRandomExamTrainClass(trainClass);
		}

		public void UpdateRandomExamTrainClass(RandomExamTrainClass trainClass)
		{
			dal.UpdateRandomExamTrainClass(trainClass);
		}

		public RandomExamTrainClass GetRandomExamTrainClassByRandomExamTrainClassID(int id)
		{
			return dal.GetRandomExamsTrainClassByRandomExamTrainClassID(id);
		}

		public void DeleteRandomExamTrainClassByRandomExamID(int randomExamID)
		{
			dal.DeleteRandomExamTrainClassByRandomExamID(randomExamID);
		}

		public IList<RandomExamTrainClass> GetRandomExamTrainClassCount(int trainclassID,int trainclasssubjectID, int postID)
		{
			return dal.GetRandomExamTrainClassCount(trainclassID, trainclasssubjectID, postID);
		}
	}
}
