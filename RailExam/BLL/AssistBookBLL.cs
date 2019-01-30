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
        /// ��ȡ����
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
        /// <param name="description">���</param>
        /// <param name="memo">��ע</param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
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
        /// ���ݸ�����ϵ��ѯվ�θ����̲�
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
        /// ������ѵ����ѯվ�θ����̲�
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
        /// ���ݸ�����ϵ��ѯ���и����̲�
        /// </summary>
        /// <param name="categoryIDPath"></param>
        /// <returns></returns>
        public IList<AssistBook> GetAssistBookByAssistBookCategoryIDPath(string categoryIDPath)
        {
            IList<AssistBook> bookList = dal.GetAssistBookByAssistBookCategoryIDPath(categoryIDPath);
            return bookList;
        }

        /// <summary>
        /// ������ѵ����ѯ���и����̲�
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

            objLogBll.WriteLog("���������̲ġ�" + book.BookName + "��������Ϣ");

            return bookid;
        }

        public void UpdateAssistBook(AssistBook book)
        {
            dal.UpdateAssistBook(book, SessionSet.EmployeeName);
            objLogBll.WriteLog("�޸ĸ����̲ġ�" + book.BookName + "��������Ϣ");
        }

        public void DeleteAssistBook(AssistBook book)
        {
            DeleteAssistBook(book.AssistBookCategoryId);

            objLogBll.WriteLog("ɾ�������̲ġ�" + book.BookName + "��������Ϣ");
        }

        public void DeleteAssistBook(int bookID)
        {
            string strAssistBookName = dal.GetAssistBook(bookID).AssistBookCategoryName;
            dal.DeleteAssistBook(bookID);
            objLogBll.WriteLog("ɾ�������̲ġ�" + strAssistBookName + "��������Ϣ");
        }

        /// <summary>
        /// ȡ������book��Ϣ
        /// </summary>
        /// <returns></returns>
        public IList<AssistBook> GetAllAssistBookInfo(int orgID)
        {
            return dal.GetAllAssistBookInfo(orgID);
        }

        /// <summary>
        /// ȡ�õ�ǰ��Ҫѧϰ�Ŀγ�
        /// </summary>
        /// <param name="trainTypeID">��ѵ���ID</param>
        /// <param name="orgID">������λID</param>
        /// <param name="postID">��λID</param>
        /// <param name="isGroupleader">�Ƿ���鳤</param>
        /// <param name="tech">���ܵȼ�</param>
        /// <param name="row">ȡ�ü�¼����</param>        
        /// <returns></returns>
        public IList<AssistBook> GetEmployeeStudyAssistBookInfoByTrainTypeID(int trainTypeID, int orgID, int postID, int isGroupleader, int tech, int row)
        {
            return dal.GetStudyAssistBookInfoByTrainTypeID(trainTypeID, orgID, postID, isGroupleader, tech, row);
        }

        /// <summary>
        /// ȡ�õ�ǰ��Ҫѧϰ�Ŀγ�
        /// </summary>
        /// <param name="assistBookCategoryID">�����̲���ϵID</param>
        /// <param name="orgID">������λID</param>
        /// <param name="postID">��λID</param>
        /// <param name="isGroupleader">�Ƿ���鳤</param>
        /// <param name="tech">���ܵȼ�</param>
        /// <param name="row">ȡ�ü�¼����</param>        
        /// <returns></returns>
        public IList<AssistBook> GetStudyAssistBookInfoByAssistBookCategoryID(int assistBookCategoryID, int orgID, int postID, int isGroupleader, int tech, int row)
        {
            return dal.GetStudyAssistBookInfoByAssistBookCategoryID(assistBookCategoryID, orgID, postID, isGroupleader, tech, row);
        }


        /// <summary>
        /// �޸�AssistBook��Url
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="strUrl"></param>
        public void UpdateAssistBookUrl(int bookID, string strUrl)
        {
            dal.UpateAssistBookUrl(bookID, strUrl);
        }
    }
}
