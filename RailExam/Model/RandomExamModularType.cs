using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class RandomExamModularType
	{
		private int _random_exam_modular_type_id;
		private string _random_exam_modular_type_name;
		private int _level_num;

	    public RandomExamModularType(int? randomexammodulartypeid, string randomexammodulartypename,int? levelnum)
	    {
	    	_random_exam_modular_type_id = randomexammodulartypeid ?? _random_exam_modular_type_id;
	    	_random_exam_modular_type_name = randomexammodulartypename;
	    	_level_num = levelnum ?? _level_num;
	    }

		public RandomExamModularType()
		{ }
		public int RandomExamModularTypeID
		{
			set { _random_exam_modular_type_id = value; }
			get { return _random_exam_modular_type_id; }
		}
		public string RandomExamModularTypeName
		{
			set { _random_exam_modular_type_name = value; }
			get { return _random_exam_modular_type_name; }
		}
		public int LevelNum
		{
			set { _level_num = value; }
			get { return _level_num; }
		}
	}
}
