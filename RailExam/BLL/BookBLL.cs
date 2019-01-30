using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    public  class BookBLL
    {
        private static readonly BookDAL dal = new BookDAL();
        private static SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="knowledgeId"></param>
        /// <param name="bookNo"></param>
        /// <param name="publishOrg"></param>
        /// <param name="publishDate"></param>
        /// <param name="pageCount"></param>
        /// <param name="wordCount"></param>
        /// <param name="authors"></param>
        /// <param name="keyWords"></param>
        /// <param name="bookName"></param>
        /// <param name="description">简介</param>
        /// <param name="memo">备注</param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        private IList<Book> GetBook(int bookId, string bookName, int knowledgeId, string bookNo, int publishOrg, 
            DateTime publishDate, int pageCount, int wordCount, string authors, string keyWords, string revisers,
            string bookmaker, string coverdesigner, string url, string description, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {

            IList<Book> bookList = dal.GetBook(bookId, bookName, knowledgeId, bookNo, publishOrg, publishDate,
                pageCount, wordCount, authors, keyWords, revisers, bookmaker, coverdesigner,
                url,description, memo, startRowIndex, maximumRows, orderBy);

            return bookList;
        }

        public IList<Book> GetBookByKnowledgeIDPath(string knowledgeIDPath, int orgid)
        {
            IList<Book> bookList = dal.GetBookByKnowledgeIDPath(knowledgeIDPath,orgid);
            return bookList;
        }

        public IList<Book> GetBookByTrainTypeIDPath(string trainTypeIDPath, int orgid)
        {
            IList<Book> bookList = dal.GetBookByTrainTypeIDPath(trainTypeIDPath, orgid);
            return bookList;
        }

        public IList<Book> GetBookByKnowledgeIDPath(string knowledgeIDPath )
        {
            IList<Book> bookList = dal.GetBookByKnowledgeIDPath(knowledgeIDPath );
            return bookList;
        }

        public IList<Book> GetBookByTrainTypeIDPath(string trainTypeIDPath )
        {
            IList<Book> bookList = dal.GetBookByTrainTypeIDPath(trainTypeIDPath );
            return bookList;
        }


        public IList<Book> GetBookByKnowledgeID(int knowledgeID, string bookName, string keyWords, string authors, int orgid)
        {
            IList<Book> bookList = dal.GetBookByKnowledgeID(knowledgeID, bookName, keyWords, authors, orgid);
            return bookList;
        }

        public IList<Book> GetBookByKnowledgeID(int knowledgeID, int orgid)
        {
            IList<Book> bookList = dal.GetBookByKnowledgeID(knowledgeID, orgid);
            return bookList;
        }

        public IList<Book> GetBookByTrainTypeID(int trainTypeID, string bookName, string keyWords, string authors, int orgid)
        {
            IList<Book> bookList = dal.GetBookByTrainTypeID(trainTypeID, bookName, keyWords, authors, orgid);
            return bookList;
        }

        public IList<Book> GetBookByTrainTypeID(int trainTypeID,  int orgid)
        {
            IList<Book> bookList = dal.GetBookByTrainTypeID(trainTypeID, orgid);
            return bookList;
        }


		public IList<Book> GetBookByName(string strName)
		{
			IList<Book> bookList = dal.GetBookByName(strName);
			return bookList;
		}

        public Book GetBook(int bookID)
        {
            return dal.GetBook(bookID);
        }

        public int AddBook(Book book)
        {
            int bookid = dal.AddBook(book, SessionSet.EmployeeName);

            objLogBll.WriteLog("新增教材《" + book.bookName + "》基本信息");

            return bookid;
        }

        public void UpdateBook(Book book)
        {
            dal.UpdateBook(book, SessionSet.EmployeeName);
            objLogBll.WriteLog("修改教材《" + book.bookName + "》基本信息");
        }

        public void DeleteBook(Book book)
        {
            DeleteBook(book.bookId);

            objLogBll.WriteLog("删除教材《" + book.bookName + "》基本信息");
        }

        public void DeleteBook(int bookID)
        {
            string strBookName = dal.GetBook(bookID).bookName;
            dal.DeleteBook(bookID);
            objLogBll.WriteLog("删除教材《" + strBookName + "》基本信息");
        }

        /// <summary>
        /// 取得某一培训岗位对应的Book信息
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        public IList<Book> GetTrainCourseBookChapterTree(int postID)
        {
            return dal.GetTrainCourseBookChapterTree(postID);
        }

        /// <summary>
        /// 取得所有book信息
        /// </summary>
        /// <returns></returns>
        public IList<Book> GetAllBookInfo(int orgID)
        {
            return dal.GetAllBookInfo(orgID);
        }

        /// <summary>
        /// 取得某学员当前需要学习的课程
        /// </summary>
        /// <param name="trainTypeID">培训类别ID</param>
        /// <param name="orgID">所属单位ID</param>
        /// <param name="postID">岗位ID</param>
        /// <returns></returns>
        public IList<Book> GetEmployeeStudyBookInfo(int trainTypeID, int orgID, int postID,bool isGroupleader,int techniciantypeid, int row)
        {
            return dal.GetEmployeeStudyBookInfo(trainTypeID, orgID, postID, isGroupleader,techniciantypeid,row);
        }

        /// <summary>
        /// 取得当前需要学习的课程
        /// </summary>
        /// <param name="trainTypeID">培训类别ID</param>
        /// <param name="orgID">所属单位ID</param>
        /// <param name="postID">岗位ID</param>
        /// <param name="isGroupleader">是否班组长</param>
        /// <param name="tech">技能等级</param>
        /// <param name="row">取得记录条数</param>        
        /// <returns></returns>
        public IList<Book> GetEmployeeStudyBookInfoByTrainTypeID(int trainTypeID, int orgID, int postID,int isGroupleader,int tech,int row)
        {
            return dal.GetEmployeeStudyBookInfoByTrainTypeID(trainTypeID, orgID, postID, isGroupleader, tech, row);
        }

        /// <summary>
        /// 取得当前需要学习的课程
        /// </summary>
        /// <param name="knowledgeID">教材体系ID</param>
        /// <param name="orgID">所属单位ID</param>
        /// <param name="postID">岗位ID</param>
        /// <param name="isGroupleader">是否班组长</param>
        /// <param name="tech">技能等级</param>
        /// <param name="row">取得记录条数</param>        
        /// <returns></returns>
        public IList<Book> GetEmployeeStudyBookInfoByKnowledgeID(int knowledgeID, int orgID, int postID, int isGroupleader, int tech, int row)
        {
            return dal.GetEmployeeStudyBookInfoByKnowledgeID(knowledgeID, orgID, postID, isGroupleader, tech, row);
        }

        /// <summary>
        /// 按更新时间倒序返回图书信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public IList<Book> GetBookInfoByDate(int row)
        {
            return dal.GetBookInfoByDate(row);
        }

        public int GetBookMaxID()
        {
            return dal.GetBookMaxID();
        }

        /// <summary>
        /// 修改Book的Url
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="strUrl"></param>
        public void UpdateBookUrl(int bookID,string strUrl)
        {
            dal.UpateBookUrl(bookID,strUrl);
        }

        public IList<Book> GetBookByKnowledgeOnline(int orgid, int postid, string idpath, bool isGroupleader, int techniciantypeid)
        {
            return dal.GetBookByKnowledgeOnline(orgid,postid, idpath, isGroupleader, techniciantypeid);
        }

        public void UpdateBookVersion(int bookID)
        {
            dal.UpdateBookVersion(bookID);
        }

		public IList<Book> GetBookByPostID(int postID, string bookName, string keyWords, string authors, int orgid)
		{
			return dal.GetBookByPostID(postID, bookName, keyWords, authors, orgid);
		}

		public IList<Book> GetBookByPostID(int postID, int orgid)
		{
			return dal.GetBookByPostID(postID, orgid);
		}
    }
}
