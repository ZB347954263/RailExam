using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class RandomExamTrainClassHa
	{
		private int _randomExamTrainClassID = 0;
		private int _randomExamID = 0;
		private string _archivesID = string.Empty;


		public int RandomExamTrainClassID
		{
			get { return _randomExamTrainClassID; }
			set { _randomExamTrainClassID = value; }
		}

		public int RandomExamID
		{
			get { return _randomExamID; }
			set { _randomExamID = value; }
		}

		public string ArchivesID
		{
			get { return _archivesID; }
			set { _archivesID = value; }
		}

		public RandomExamTrainClassHa() { }

		public RandomExamTrainClassHa(int? randomExamTrainClassID, int? RandomExamID, string archivesID)
        {
			_randomExamTrainClassID = randomExamTrainClassID ?? _randomExamTrainClassID;
            _randomExamID = RandomExamID ?? _randomExamID;
			_archivesID = archivesID;
        }
	}
}
