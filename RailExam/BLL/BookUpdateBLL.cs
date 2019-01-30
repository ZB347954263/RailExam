using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class BookUpdateBLL
    {
        private static readonly BookUpdateDAL dal = new BookUpdateDAL();

        public IList<BookUpdate> GetBookUpdateByChapterID(int chapterID,int bookID)
        {
            IList<BookUpdate> BookUpdateList = dal.GetBookUpdateByChapterID(chapterID,bookID);
            
            return BookUpdateList;
        }

        public IList<BookUpdate> GetBookUpdateBySelect(int bookID,string bookname,string person, DateTime begin,DateTime end ,string updateobject)
        {
            return dal.GetBookUpdateBySelect(bookID, bookname, person, begin, end, updateobject);
        }

        public BookUpdate GetBookUpdate(int BookUpdateID)
        {
            return dal.GetBookUpdate(BookUpdateID);
        }

        public void AddBookUpdate(BookUpdate BookUpdate)
        {
            dal.AddBookUpdate(BookUpdate);
        }

        public void UpdateBookUpdate(BookUpdate BookUpdate)
        {
            dal.UpdateBookUpdate(BookUpdate);
        }

        public void DeleteBookUpdate(int BookUpdateID)
        {
            dal.DeleteBookUpdate(BookUpdateID);
        }
    }
}
