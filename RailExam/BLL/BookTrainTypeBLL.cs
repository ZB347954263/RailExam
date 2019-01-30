using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    public class BookTrainTypeBLL
    {
        private static readonly BookTrainTypeDAL dal = new BookTrainTypeDAL();
 
        public void AddBookTrainType(BookTrainType bookTrainType)
        {
            dal.AddBookTrainType(bookTrainType);
        }

        public void UpdateBookTrainType(BookTrainType bookTrainType)
        {
            dal.UpdateBookTrainType(bookTrainType);
        }

        public IList<BookTrainType> GetBookTrainTypeByBookID(int bookID)
        {
            return dal.GetBookTrainTypeByBookID(bookID);
        }

        public BookTrainType GetBookTrainType(int bookID,int trainTypeID)
        {
            return dal.GetBookTrainType(bookID,trainTypeID);
        }
    }
}
