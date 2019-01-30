using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class RandomExamTrainClass
	{
		private int _randomExamTrainClassID = 0;
		private int _randomExamID = 0;
		private int _trainClassID = 0;
		private int _trainClassSubjectID = 0;


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

		public int TrainClassID
		{
			get { return _trainClassID; }
			set { _trainClassID = value; }
		}

		public int TrainClassSubjectID
		{
			get { return _trainClassSubjectID; }
			set { _trainClassSubjectID = value; }
		}

		public RandomExamTrainClass() { }

		public RandomExamTrainClass(int? randomExamTrainClassID, int? RandomExamID, int? trainClassID, int? trainClassSubjectID )
        {
			_randomExamTrainClassID = randomExamTrainClassID ?? _randomExamTrainClassID;
            _randomExamID = RandomExamID ?? _randomExamID;
			_trainClassID = trainClassID ?? _trainClassID;
			_trainClassSubjectID = trainClassSubjectID ?? _trainClassSubjectID;
        }
	}
}
