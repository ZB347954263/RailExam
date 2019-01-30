using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class CoursewareTrainTypeBLL
    {
        private static readonly CoursewareTrainTypeDAL dal = new CoursewareTrainTypeDAL();

        public void AddCoursewareTrainType(CoursewareTrainType coursewareTrainType)
        {
            dal.AddCoursewareTrainType(coursewareTrainType);
        }

        public void UpdateCoursewareTrainType(CoursewareTrainType coursewareTrainType)
        {
            dal.UpdateCoursewareTrainType(coursewareTrainType);
        }

        public IList<CoursewareTrainType> GetCoursewareTrainTypeByCoursewareID(int coursewareID)
        {
            return dal.GetCoursewareTrainTypeByCoursewareID(coursewareID);
        }

        public CoursewareTrainType GetCoursewareTrainType(int coursewareID, int trainTypeID)
        {
            return dal.GetCoursewareTrainType(coursewareID, trainTypeID);
        }
    }
}
