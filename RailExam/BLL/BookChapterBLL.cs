using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    public class BookChapterBLL
    {
        private static readonly BookChapterDAL dal = new BookChapterDAL();
        private  SystemLogBLL objLogBill = new SystemLogBLL();

        public IList<BookChapter> GetBookChapterByBookID(int bookID)
        {
            IList<BookChapter> bookChapterList = dal.GetBookChapterByBookID(bookID);
            return bookChapterList;
        }

        /// <summary>
        /// 取得某一章节的字章节信息
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public IList<BookChapter> GetBookChapterByParentID(int parentID)
        {
            IList<BookChapter> bookChapterList = dal.GetBookChapterByParentID(parentID);

            return bookChapterList;
        }

        /// <summary>
        /// 取得某一Book对应的章节信息
        /// </summary>
        /// <returns></returns>  
        public IList<BookChapter> GetTrainCourseBookChapterTree(int bookID)
        {
            return dal.GetTrainCourseBookChapterTree(bookID);
        }

        public BookChapter GetBookChapter(int ChapterID)
        {
            return dal.GetBookChapter(ChapterID);
        }

        public BookChapter GetBookChapterInfo(int ChapterID)
        {
            BookChapter obj = new BookChapter();
            if(dal.GetBookChapter(ChapterID)== null)
            {
                obj.ChapterId = 0;
                obj.ChapterName = "";
                obj.Description = "";
                obj.ReferenceRegulation = "";
                obj.Url = "";
                obj.Memo = "";
                obj.LastPerson = "";
                return obj;
            }
            else
            {
                return dal.GetBookChapter(ChapterID);
            }
        }

        public void AddBookChapter(BookChapter bookChapter)
        {
            dal.AddBookChapter(bookChapter, SessionSet.EmployeeName);

            BookBLL objBll = new BookBLL();
            if(bookChapter.IsEdit)
            {
                objBll.UpdateBookVersion(bookChapter.BookId);
            }
            string strBookName = objBll.GetBook(bookChapter.BookId).bookName;

            GetIndex(bookChapter.BookId.ToString());
            
            objLogBill.WriteLog("新增教材《" + strBookName + "》中“" + bookChapter.ChapterName + "”章节基本信息");
        }

        public void UpdateBookChapter(BookChapter bookChapter)
        {
            dal.UpdateBookChapter(bookChapter, SessionSet.EmployeeName);
            BookBLL objBll = new BookBLL();
            if (bookChapter.IsEdit)
            {
                objBll.UpdateBookVersion(bookChapter.BookId);
            }
            string strBookName = objBll.GetBook(bookChapter.BookId).bookName;
            
            GetIndex(bookChapter.BookId.ToString());
            
            objLogBill.WriteLog("修改教材《" + strBookName + "》中“" + bookChapter.ChapterName + "”章节基本信息");
        }

        public void DeleteBookChapter(int chapterID)
        {
            dal.DeleteBookChapter(chapterID);
        }

        public void DeleteBookChapter(BookChapter bookChapter)
        {

            ItemBLL objItemBll = new ItemBLL();

            objItemBll.UpdateItemEnabled(bookChapter.BookId, bookChapter.ChapterId,2);

            string strChapterName = GetBookChapter(bookChapter.ChapterId).ChapterName;

            BookBLL objBookBll = new BookBLL();
            string strBookName = objBookBll.GetBook(bookChapter.BookId).bookName;

            objLogBill.WriteLog("删除教材《" + strBookName + "》中“" + strChapterName + "”章节基本信息");

            dal.DeleteBookChapter(bookChapter.ChapterId);

            GetIndex(bookChapter.BookId.ToString());
        }

        public void DeleteBookChapterByBookID(int bookID)
        {
            dal.DeleteBookChapterByBookID(bookID);
        }

        public void UpdateBookChapterUrl(int chapterID,string url)
        {
            dal.UpateBookChapterUrl(chapterID,url);
        }


        /// <summary>
        /// 发布教材
        /// </summary>
        /// <param name="strID"></param>
        public void GetIndex(string strID)
        {
            string strItem,strChapter;

            BookBLL objBll = new BookBLL();
            RailExam.Model.Book objBook = objBll.GetBook(Convert.ToInt32(strID));

            string strBookName = objBook.bookName;
            string strBookUrl = objBook.url;
            string strVersion = objBook.Version.ToString();

            WriteXml(strID,strVersion);

            strItem = "var TREE_ITEMS = [ ['" + strBookName + "', 'common.htm?url=cover.htm&chapterid=0&bookid="+ strID +"',";

            strChapter = "var Tree_Chapter=['0','cover.htm','"+ strBookName+"'";

            IList<RailExam.Model.BookChapter> objBookChapter = GetBookChapterByBookID(Convert.ToInt32(strID));

            foreach (RailExam.Model.BookChapter chapter in objBookChapter)
            {
                if (chapter.ParentId == 0)
                {
                    if (chapter.Url == "" || chapter.Url == null)
                    {
						strItem += "['" + chapter.ChapterName + "', 'common.htm?url=empty.htm&chapterid=" + chapter.ChapterId + "&bookid=" + strID + "'";
                        strChapter += ",'" + chapter.ChapterId + "','empty.htm','"+chapter.NamePath+"'";
                    }
                    else
                    {
						strItem += "['" + chapter.ChapterName + "', 'common.htm?url=" + chapter.ChapterId + ".htm&chapterid=" + chapter.ChapterId + "&bookid=" + strID + "'";
                        strChapter += ",'" + chapter.ChapterId + "','" + chapter.ChapterId + ".htm','" + chapter.NamePath + "'";
                    }

                    if (GetBookChapterByParentID(chapter.ChapterId).Count > 0)
                    {
                        strItem += ",";
                    }

                    strItem = Get(chapter.ChapterId, strItem);

                    strChapter = GetChapter(chapter.ChapterId, strChapter);
                }
            }

            strItem += "]];";
            string strPath = "../Online/Book/" + strID + "/tree_items.js";
            File.Delete(HttpContext.Current.Server.MapPath(strPath));
            File.AppendAllText(HttpContext.Current.Server.MapPath(strPath), strItem, System.Text.Encoding.UTF8);

            strChapter += "];";
            strPath = "../Online/Book/" + strID + "/tree_chapter.js";
            if(File.Exists(HttpContext.Current.Server.MapPath(strPath)))
            {
                File.Delete(HttpContext.Current.Server.MapPath(strPath));
            }
            File.AppendAllText(HttpContext.Current.Server.MapPath(strPath), strChapter, System.Text.Encoding.UTF8);

            string[] strIndex = File.ReadAllLines(HttpContext.Current.Server.MapPath("../Online/Book/" + strID + "/index.html"), System.Text.Encoding.Default);

            for (int i = 0; i < strIndex.Length; i++)
            {
                if (strIndex[i].IndexOf("<title>") != -1)
                {
                    strIndex[i] = "\t<title> " + strBookName + " </title>";
                }

				if(strIndex[i].IndexOf("common.htm?url=cover.htm&chapterid=0") != -1)
				{
					strIndex[i] =
						strIndex[i].Replace("common.htm?url=cover.htm&chapterid=0", "common.htm?url=cover.htm&chapterid=0&bookid=" + strID);
				}
            }

            File.WriteAllLines(HttpContext.Current.Server.MapPath("../Online/Book/" + strID + "/index.html"), strIndex, System.Text.Encoding.UTF8);
        }

        private string Get(int strParentID, string strItem)
        {
            IList<RailExam.Model.BookChapter> objBookChapter = GetBookChapterByParentID(strParentID);

            foreach (RailExam.Model.BookChapter chapter in objBookChapter)
            {
                if (chapter.Url == "" || chapter.Url == null)
                {
					strItem += "['" + chapter.ChapterName + "', 'common.htm?url=empty.htm&chapterid=" + chapter.ChapterId + "&bookid=" + chapter.BookId + "'";
                }
                else
                {
					strItem += "['" + chapter.ChapterName + "', 'common.htm?url=" + chapter.ChapterId + ".htm&chapterid=" + chapter.ChapterId + "&bookid=" + chapter.BookId + "'";
                }

                if(GetBookChapterByParentID(chapter.ChapterId).Count>0)
                {
                    strItem += ",";
                }

                strItem = Get(chapter.ChapterId, strItem);
            }

            strItem += "],";

            return strItem;
        }

        private string GetChapter(int strParentID, string strChapter)
        {
            IList<RailExam.Model.BookChapter> objBookChapter = GetBookChapterByParentID(strParentID);

            foreach (RailExam.Model.BookChapter chapter in objBookChapter)
            {
                if (chapter.Url == "" || chapter.Url == null)
                {
                    strChapter += ",'" + chapter.ChapterId + "','empty.htm','" + chapter.NamePath + "'";
                }
                else
                {
                    strChapter += ",'" + chapter.ChapterId + "','" + chapter.ChapterId + ".htm','" + chapter.NamePath + "'";
                }
                strChapter = GetChapter(chapter.ChapterId, strChapter);
            }
            return strChapter;
        }

        private void WriteXml(string strID,string strVersion)
        {
            ArrayList objList = new ArrayList();
            string str = "";
            int i = 0;
            StreamReader objReader = new StreamReader( HttpContext.Current.Server.MapPath("../Online/Book/" + strID + "/version.xml"), System.Text.Encoding.Default);
            while ((str = objReader.ReadLine()) != null)
            {
                if (str.IndexOf("<NowVersion>") != -1)
                {
                    str = str.Substring(0, str.IndexOf("<")) + "<NowVersion>" + strVersion + "</NowVersion>";
                }
                objList.Add(str);

                i = i + 1;
            }

            objReader.Close();

            StreamWriter objWriter = new StreamWriter(HttpContext.Current.Server.MapPath("../Online/Book/" + strID + "/version.xml"), false, System.Text.Encoding.UTF8);
            for (int j = 0; j < i; j++)
            {
                objWriter.WriteLine(objList[j]);
            }
            objWriter.Close();
        }

		public int GetMaxChapterIDByBookID(int bookId)
		{
			return dal.GetMaxChapterIDByBookID(bookId);
		}

		public IList<BookChapter> GetBookChapterByIDPath(string idPath)
		{
			return dal.GetBookChapterByIDPath(idPath);
		}
    }
}