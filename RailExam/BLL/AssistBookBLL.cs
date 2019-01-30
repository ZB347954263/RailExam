using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;
using DSunSoft.Web.Global;


namespace RailExam.BLL
{
    public class AssistBookBLL
    {
        private static readonly AssistBookDAL dal = new AssistBookDAL();
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
        private IList<AssistBook> GetAssistBook(int bookId, string bookName, int knowledgeId, string bookNo, int publishOrg,
            DateTime publishDate, int pageCount, int wordCount, string authors, string keyWords, string revisers,
            string bookmaker, string coverdesigner, string url, string description, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {

            IList<AssistBook> bookList = dal.GetAssistBook(bookId, bookName, knowledgeId, bookNo, publishOrg, publishDate,
                pageCount, wordCount, authors, keyWords, revisers, bookmaker, coverdesigner,
                url, description, memo, startRowIndex, maximumRows, orderBy);

            return bookList;
        }

        /// <summary>
        /// 根据辅导体系查询站段辅导教材
        /// </summary>
        /// <param name="categoryIDPath"></param>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public IList<AssistBook> GetAssistBookByAssistBookCategoryIDPath(string categoryIDPath, int orgid)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByAssistBookCategoryIDPath(categoryIDPath, orgid);
            return bookList;
        }

        /// <summary>
        /// 根据培训类别查询站段辅导教材
        /// </summary>
        /// <param name="trainTypeIDPath"></param>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public IList<AssistBook> GetAssistBookByTrainTypeIDPath(string trainTypeIDPath, int orgid)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByTrainTypeIDPath(trainTypeIDPath, orgid);
            return bookList;
        }

        /// <summary>
        /// 根据辅导体系查询所有辅导教材
        /// </summary>
        /// <param name="categoryIDPath"></param>
        /// <returns></returns>
        public IList<AssistBook> GetAssistBookByAssistBookCategoryIDPath(string categoryIDPath)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByAssistBookCategoryIDPath(categoryIDPath);
            return bookList;
        }

        /// <summary>
        /// 根据培训类别查询所有辅导教材
        /// </summary>
        /// <param name="trainTypeIDPath"></param>
        /// <returns></returns>
        public IList<AssistBook> GetAssistBookByTrainTypeIDPath(string trainTypeIDPath)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByTrainTypeIDPath(trainTypeIDPath);
            return bookList;
        }

        public IList<AssistBook> GetAssistBookByAssistBookCategoryID(int assistBookCategoryID, string bookName, string keyWords, string authors, int orgid)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByAssistBookCategoryID(assistBookCategoryID, bookName, keyWords, authors, orgid);
            return bookList;
        }

        public IList<AssistBook> GetAssistBookByTrainTypeID(int trainTypeID, string bookName, string keyWords, string authors, int orgid)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByTrainTypeID(trainTypeID, bookName, keyWords, authors, orgid);
            return bookList;
        }

        public IList<AssistBook> GetAssistBookByAssistBookCategoryID(int assistBookCategoryID,int orgid)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByAssistBookCategoryID(assistBookCategoryID, orgid);
            return bookList;
        }

        public IList<AssistBook> GetAssistBookByTrainTypeID(int trainTypeID, int orgid)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByTrainTypeID(trainTypeID,orgid);
            return bookList;
        }

        public AssistBook GetAssistBook(int bookID)
        {
            return dal.GetAssistBook(bookID);
        }

        public int AddAssistBook(AssistBook book)
        {
            int bookid = dal.AddAssistBook(book, SessionSet.EmployeeName);

            objLogBll.WriteLog("新增辅导教材《" + book.BookName + "》基本信息");

            return bookid;
        }

        public void UpdateAssistBook(AssistBook book)
        {
            dal.UpdateAssistBook(book, SessionSet.EmployeeName);
            objLogBll.WriteLog("修改辅导教材《" + book.BookName + "》基本信息");
        }

        public void DeleteAssistBook(AssistBook book)
        {
            DeleteAssistBook(book.AssistBookCategoryId);

            objLogBll.WriteLog("删除辅导教材《" + book.BookName + "》基本信息");
        }

        public void DeleteAssistBook(int bookID)
        {
            string strAssistBookName = dal.GetAssistBook(bookID).AssistBookCategoryName;
            dal.DeleteAssistBook(bookID);
            objLogBll.WriteLog("删除辅导教材《" + strAssistBookName + "》基本信息");
        }

        /// <summary>
        /// 取得所有book信息
        /// </summary>
        /// <returns></returns>
        public IList<AssistBook> GetAllAssistBookInfo(int orgID)
        {
            return dal.GetAllAssistBookInfo(orgID);
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
        public IList<AssistBook> GetEmployeeStudyAssistBookInfoByTrainTypeID(int trainTypeID, int orgID, int postID, int isGroupleader, int tech, int row)
        {
            return dal.GetStudyAssistBookInfoByTrainTypeID(trainTypeID, orgID, postID, isGroupleader, tech, row);
        }

        /// <summary>
        /// 取得当前需要学习的课程
        /// </summary>
        /// <param name="assistBookCategoryID">辅导教材体系ID</param>
        /// <param name="orgID">所属单位ID</param>
        /// <param name="postID">岗位ID</param>
        /// <param name="isGroupleader">是否班组长</param>
        /// <param name="tech">技能等级</param>
        /// <param name="row">取得记录条数</param>        
        /// <returns></returns>
        public IList<AssistBook> GetStudyAssistBookInfoByAssistBookCategoryID(int assistBookCategoryID, int orgID, int postID, int isGroupleader, int tech, int row)
        {
            return dal.GetStudyAssistBookInfoByAssistBookCategoryID(assistBookCategoryID, orgID, postID, isGroupleader, tech, row);
        }


        /// <summary>
        /// 修改AssistBook的Url
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="strUrl"></param>
        public void UpdateAssistBookUrl(int bookID, string strUrl)
        {
            dal.UpateAssistBookUrl(bookID, strUrl);
        }
    }
}
