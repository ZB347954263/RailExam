using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼�������
    /// </summary>
    public class ItemBLL
    {
        private static readonly ItemDAL dal = new ItemDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// �������
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int AddItem(Item item)
        {
            int id = dal.AddItem(item);
            BookChapterBLL objChapterBll = new BookChapterBLL();
            string strChapterName = objChapterBll.GetBookChapter(item.ChapterId).ChapterName;
            BookBLL objBookBll = new BookBLL();
            string strBookName = objBookBll.GetBook(item.BookId).bookName;
            objLogBll.WriteLog("�����̲ġ�" + strBookName + "���С�" + strChapterName + "����������Ϣ");
            return id;
        }

		public void AddItem(IList<Item> objList)
		{
			dal.AddItem(objList);
		}

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int DeleteItem(int itemId)
        {
            Item obj = GetItem(itemId);
            objLogBll.WriteLog("ɾ���̲ġ�" + obj.BookName + "���С�" + obj.ChapterName + "����������Ϣ");
            return dal.DeleteItem(itemId);
        }

		public void DeleteItemByChapterID(int chapterID,string namePath)
		{
			dal.DeleteItemByChapterID(chapterID);
			objLogBll.WriteLog("ɾ���̲��½ڡ�" + namePath + "���е�����������Ϣ");
		}

		public void DeleteItemByBookID(int bookID, string namePath)
		{
			dal.DeleteItemByBookID(bookID);
			objLogBll.WriteLog("ɾ���̲ġ�" + namePath + "���е�����������Ϣ");
		}

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int DeleteItem(Item item)
        {
            Item obj = GetItem(item.ItemId);
            objLogBll.WriteLog("ɾ���̲ġ�" + obj.BookName + "���С�" + obj.ChapterName + "����������Ϣ");
            return dal.DeleteItem(item.ItemId);
        }

        /// <summary>
        /// �޸Ľ̲Ļ��½����������״̬
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="ChapterId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public int UpdateItemEnabled(int bookId,int ChapterId,int statusId)
        {
            string bookName, chapterName;
            BookBLL objBookBll = new BookBLL();
            bookName = objBookBll.GetBook(bookId).bookName;
            if(ChapterId == 0)
            {
                objLogBll.WriteLog("���̲ġ�"+bookName+"������ȫ������");
            }
            else
            {
                BookChapterBLL objChapterBll = new BookChapterBLL();
                BookChapter objChapter = objChapterBll.GetBookChapter(ChapterId);
                chapterName = objChapter.ChapterName;
                objLogBll.WriteLog("���̲ġ�"+ bookName+"���С�" +��chapterName +"������ȫ������");
            }
            return dal.UpdateItemEnabled(bookId, ChapterId,statusId);
        }

        public int UpdateItemEnabled(int itemId,int statusId)
        {
            return dal.UpdateItemEnabled(itemId, statusId);
        }

        /// <summary>
        /// ɾ�����⼯��
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int DeleteItem(int[] itemIds)
        {
            objLogBll.WriteLog("ɾ����������");
            return dal.DeleteItem(itemIds);
        }

        /// <summary>
        ///��������
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public void UpdateItem(Item item)
        {
            Item obj = GetItem(item.ItemId);
            objLogBll.WriteLog("�޸Ľ̲ġ�" + obj.BookName + "���С�" + obj.ChapterName + "����������Ϣ");
            dal.UpdateItem(item);
        }

		public void UpdateItem(IList<Item> objList)
		{
			dal.UpdateItem(objList);
		}

    	/// <summary>
        ///��ȡ����
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public Item GetItem(int itemId)
        {
			if(itemId == -1)
			{
				return new Item();
			}
			else
			{
				return dal.GetItem(itemId);
			}
        }

        /// <summary>
        ///��ȡ���⼯��
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<Item> GetItems()
        {
            return dal.GetItems();
        }

        public IList<Item> GetItems(string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath,
         int itemTypeId,int paperId, int itemDifficultyId, int itemScore,
         int StatusId, int usageId, int currentPageIndex, int pageSize, string orderBy)
        {
            return dal.GetItems(knowledgeIdPath, bookId, chapterId, trainTypeIdPath, categoryIdPath,
                 itemTypeId, paperId,itemDifficultyId, itemScore,
                StatusId, usageId, currentPageIndex, pageSize, orderBy);
        }


        public IList<Item> GetItems(string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath,
    int itemTypeId, int paperId, int itemDifficultyId, int itemScore,
    int StatusId, int usageId, int startRow, int endRow, ref int nItemcount)
        {
            return dal.GetItems(knowledgeIdPath, bookId, chapterId, trainTypeIdPath, categoryIdPath,
                 itemTypeId, paperId, itemDifficultyId, itemScore,
                StatusId, usageId,  startRow, endRow, ref nItemcount);
        }



        /// <summary>
        ///����ѯ������ȡ���⼯��
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<Item> GetItems(string content,string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath,
            int itemTypeId, int itemDifficultyId, int itemScore,
			int StatusId, int usageId, int currentPageIndex, int pageSize, string orderBy, int orgID,int railSystemId)
        {
            return dal.GetItems(content,knowledgeIdPath, bookId, chapterId, trainTypeIdPath, categoryIdPath,
                 itemTypeId, itemDifficultyId,itemScore,
                StatusId, usageId,currentPageIndex, pageSize, orderBy,orgID,railSystemId);
        }



          public IList<Item> GetItems(string content,string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath,
             int itemTypeId, int itemDifficultyId, int itemScore,
			int StatusId, int usageId, int currentPageIndex, int pageSize, string orderBy, int flag, int orgID,int railSystemId)
        {
            IList<Item> objList = new List<Item>();
            if (flag == -1)
            {
                objList = dal.GetItems(content,knowledgeIdPath, bookId, chapterId, trainTypeIdPath, categoryIdPath,
                   itemTypeId, itemDifficultyId, itemScore,
                               StatusId, usageId, currentPageIndex, pageSize, orderBy, orgID, railSystemId);
            }
            else
            {
                objList = dal.GetEnableItems(currentPageIndex, pageSize, orderBy, orgID, railSystemId);
            }

              foreach (Item item in objList)
              {
                  item.Content = NoHTML(item.Content);
              }
              return objList;
        }        

             public IList<Item> GetOutputItems(string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath,
             int itemTypeId, int itemDifficultyId, int itemScore,
            int StatusId, int usageId, int flag, int orgID, int railSystemId)
        {
            IList<Item> objList = new List<Item>();
            if (flag == -1)
            {
                objList = dal.GetOutputItems(knowledgeIdPath, bookId, chapterId, trainTypeIdPath, categoryIdPath,
                   itemTypeId, itemDifficultyId, itemScore,
                               StatusId, usageId, orgID, railSystemId);
            }
            else
            {
                objList = dal.GetEnableOutputItems(orgID, railSystemId);
            }
            return objList;
        }


        /// <summary>
        /// ��ͼ���ѯ����
        /// </summary>
        /// <returns>ָ��ͼ�����������</returns>
        public IList<Item> GetItemsByBookBookId(int bookId)
        {
            return dal.GetItemsByBookBookId(bookId);
        }

        /// <summary>
        /// ��ͼ���½ڲ�ѯ����
        /// </summary>
        /// <returns>ָ��ͼ���½ڵ���������</returns>
        public IList<Item> GetItemsByBookChapterId(int bookId, int chapterId,int currentPageIndex,int pageSize)
        {
            return dal.GetItemsByBookChapterId(bookId, chapterId,currentPageIndex,pageSize);
        }

		public IList<Item> GetItemsByChapterId(int bookid, int chapterId)
		{
			return dal.GetItemsByChapterId(bookid,chapterId);
		}

    	/// <summary>
        /// ��ȡ��ϰ����
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<Item> GetExerciseItems(int bookId, int chapterId, int typeId)
        {
            return dal.GetExerciseItems(bookId, chapterId, typeId);
        }

        /// <summary>
        /// ��ͼ�顢�½ڻ�ȡ����
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<Item> GetItemsByBookChapterId(int bookId, int chapterId, int categoryId)
        {
            return dal.GetItemsByBookChapterId(bookId, chapterId, categoryId);
        }

        /// <summary>
        /// ����ѯ������ȡ����
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<Item> GetItems(int itemId, int bookId, int chapterId, int categoryId, int organizationId,
            int typeId, int completeTime, int difficultyId, string source, string version,
            decimal score, string content, int answerCount, string selectAnswer,
            string standardAnswer, string description, DateTime outDateDate, int usedCount,
            int statusId, string createPerson, DateTime createTime, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {
            return dal.GetItems(itemId, bookId, chapterId, categoryId, organizationId,
                    typeId, completeTime, difficultyId, source, version,
                    score, content, answerCount, selectAnswer,
                    standardAnswer, description, outDateDate, usedCount,
                    statusId, createPerson, createTime, memo, startRowIndex, maximumRows, orderBy);
        }
         
        /// <summary>
        /// �����Ի�ȡ����
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<Item> GetItemsByStrategy(int RangeType, int DiffId, int RangeId, int ItemTypeId, string strExcludeIDs, int strategyid)
        {
            return dal.GetItemsByStrategy(RangeType, DiffId, RangeId, ItemTypeId, strExcludeIDs, strategyid);
        }

		/// <summary>
		/// �����Ի�ȡ����
		/// </summary>
		/// <returns>���⼯��</returns>
		public IList<Item> GetItemsByStrategyNew(int RangeType, int DiffId, int MaxDiffId ,int RangeId, int ItemTypeId, string strExcludeIDs)
		{
			return dal.GetItemsByStrategyNew(RangeType, DiffId,MaxDiffId,RangeId, ItemTypeId, strExcludeIDs);
		}


        public IList<Item> GetItemsByStrategy(int RangeType,int RangeId)
        {
            return dal.GetItemsByStrategy(RangeType,RangeId);
        }


        /// <summary>
        /// ���������ȡ����
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<Item> GetItemsByStrategyItem(int DiffId, int categoryId, int ItemTypeId, string strExcludeIDs)
        {
            return dal.GetItemsByStrategyItem(DiffId, categoryId, ItemTypeId, strExcludeIDs);
        }

        /// <summary>
        /// ��ȡ��¼��
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return dal.RecordCount;
        }

		public int GetItemsByBookChapterIdPath(string idPath, int itemTypeID)
        {
            return dal.GetItemsByBookChapterIdPath(idPath,itemTypeID);   
        }

        public int GetItemsByBookChapterIdPath(string idPath, int itemTypeID,int diff, int maxdiff)
		{
			return dal.GetItemsByBookChapterIdPath(idPath, itemTypeID,diff,maxdiff);
		}

        public  int GetItemsByBookID(int bookid,int itemTypeID)
        {
            return dal.GetItemsByBookID(bookid,itemTypeID);
        }

		public int GetItemsByBookID(int bookid, int itemTypeID,int diff,int maxdiff)
		{
			return dal.GetItemsByBookID(bookid, itemTypeID, diff,maxdiff);
		}

        ///   <summary>   
        ///   ȥ��HTML���   
        ///   </summary>   
        ///   <param   name="Htmlstring">����HTML��Դ��   </param>   
        ///   <returns>�Ѿ�ȥ���������</returns>   
        public static string NoHTML(string Htmlstring)
        {
            //ɾ���ű�   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //ɾ��HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            //Htmlstring.Replace("<", "");
           // Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        public void GetItemsNoPicutre()
        {
            IList<Item> objList = dal.GetItemsNoPicture();
            foreach (Item item in objList)
            {
                item.Content = NoHTML(item.Content);
                item.SelectAnswer = NoHTML(item.SelectAnswer);
                UpdateItemNoPicutre(item);
            }
        }

        public void UpdateItemNoPicutre(Item item)
        {
            dal.UpdateItem(item);
        }

        public IList<Item> GetItemsByParentItemID(int parentItemID)
        {
            return dal.GetItemsByParentItemID(parentItemID);
        }
    }
}
