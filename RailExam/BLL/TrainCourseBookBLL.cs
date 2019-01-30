using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainCourseBookBLL
    {
        /// <summary>
        /// 内部成员
        /// </summary>
        private static readonly TrainCourseBookDAL dal = new TrainCourseBookDAL();

        /// <summary>
        /// 新增培训课程的教材信息
        /// </summary>
        /// <param name="trainCourseBook"></param>
        public void AddTrainCourseBook(TrainCourseBook trainCourseBook)
        {
            dal.AddTrainCourseBook(trainCourseBook);
        }

        /// <summary>
        /// 删除培训课程的教材信息
        /// </summary>
        /// <param name="trainCourseBookChapterID"></param>
        public void DeleteTrainCourseBook(int trainCourseBookChapterID)
        {
            dal.DeleteTrainCourseBook(trainCourseBookChapterID);
        }

        public void DeleteTrainCourseBook(TrainCourseBook trainCourseBook)
        {
            dal.DeleteTrainCourseBook(trainCourseBook.BookID);
        }

        /// <summary>
        /// 更新培训课程的教材信息
        /// </summary>
        /// <param name="trainCourseBook"></param>
        public void UpdateTrainCourseBook(TrainCourseBook trainCourseBook)
        {
            dal.UpdateTrainCourseBook(trainCourseBook);
        }

        public IList<TrainCourseBook> GetTrainCourseBookInfo(int trainCourseBookChapterID,
                                                    int courseID,
                                                    int bookID,
                                                    string  chapterID,
                                                    string studyDemand,
                                                    decimal studyHours,
                                                    string memo,
                                                    int startRowIndex,
                                                    int maximumRows,
                                                    string orderBy)
        {
            return dal.GetTrainCourseBookInfo(trainCourseBookChapterID,
                                         courseID,
                                         bookID,
                                         chapterID,
                                         studyDemand,
                                         studyHours,
                                         memo,
                                         startRowIndex,
                                         maximumRows,
                                         orderBy);
        }

        /// <summary>
        /// 根据培训课程ID确定唯一的课程设计（教材）信息
        /// </summary>
        /// <param name="trainCourseBookChapterID"></param>
        /// <returns></returns>
        public TrainCourseBook GetTrainCourseBookInfo(int trainCourseBookChapterID)
        {
            return dal.GetTrainCourseBookInfo(trainCourseBookChapterID);
        }

        /// <summary>
        /// 根据课程ID取得其相关的教材信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public IList<TrainCourseBook> GetTrainCourseBookChapter(int courseID,string orderby)
        {
            return dal.GetTrainCourseBookChapter(courseID,orderby);
        }

        public TrainCourseBook GetTrainCourseBookInfo(int courseID,int bookID,string chapterID)
        {
            return dal.GetTrainCourseBookInfo(courseID,bookID, chapterID);
        }
    }
}
