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
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainCourseBookDAL dal = new TrainCourseBookDAL();

        /// <summary>
        /// ������ѵ�γ̵Ľ̲���Ϣ
        /// </summary>
        /// <param name="trainCourseBook"></param>
        public void AddTrainCourseBook(TrainCourseBook trainCourseBook)
        {
            dal.AddTrainCourseBook(trainCourseBook);
        }

        /// <summary>
        /// ɾ����ѵ�γ̵Ľ̲���Ϣ
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
        /// ������ѵ�γ̵Ľ̲���Ϣ
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
        /// ������ѵ�γ�IDȷ��Ψһ�Ŀγ���ƣ��̲ģ���Ϣ
        /// </summary>
        /// <param name="trainCourseBookChapterID"></param>
        /// <returns></returns>
        public TrainCourseBook GetTrainCourseBookInfo(int trainCourseBookChapterID)
        {
            return dal.GetTrainCourseBookInfo(trainCourseBookChapterID);
        }

        /// <summary>
        /// ���ݿγ�IDȡ������صĽ̲���Ϣ
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
