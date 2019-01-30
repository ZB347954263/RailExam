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
    public class AssistBookChapterBLL
    {
        private static readonly AssistBookChapterDAL dal = new AssistBookChapterDAL();
        private SystemLogBLL objLogBill = new SystemLogBLL();

        public IList<AssistBookChapter> GetAssistBookChapterByBookID(int bookID)
        {
            IList<AssistBookChapter> bookChapterList = dal.GetAssistBookChapterByBookID(bookID);
            return bookChapterList;
        }

        /// <summary>
        /// 取得某一章节的子章节信息
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public IList<AssistBookChapter> GetAssistBookChapterByParentID(int parentID)
        {
            IList<AssistBookChapter> bookChapterList = dal.GetAssistBookChapterByParentID(parentID);

            return bookChapterList;
        }

        public AssistBookChapter GetAssistBookChapter(int ChapterID)
        {
            return dal.GetAssistBookChapter(ChapterID);
        }

        public AssistBookChapter GetAssistBookChapterInfo(int ChapterID)
        {
            AssistBookChapter obj = new AssistBookChapter();
            if (dal.GetAssistBookChapter(ChapterID) == null)
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
                return dal.GetAssistBookChapter(ChapterID);
            }
        }

        public void AddAssistBookChapter(AssistBookChapter bookChapter)
        {
            dal.AddAssistBookChapter(bookChapter, SessionSet.EmployeeName);

            AssistBookBLL objBll = new AssistBookBLL();
            string strBookName = objBll.GetAssistBook(bookChapter.AssistBookId).BookName;

            GetIndex(bookChapter.AssistBookId.ToString());

            objLogBill.WriteLog("新增辅导教材《" + strBookName + "》中“" + bookChapter.ChapterName + "”章节基本信息");
        }

        public void UpdateAssistBookChapter(AssistBookChapter bookChapter)
        {
            dal.UpdateAssistBookChapter(bookChapter, SessionSet.EmployeeName);
            AssistBookBLL objBll = new AssistBookBLL();
            string strBookName = objBll.GetAssistBook(bookChapter.AssistBookId).BookName;

            GetIndex(bookChapter.AssistBookId.ToString());

            objLogBill.WriteLog("修改辅导教材《" + strBookName + "》中“" + bookChapter.ChapterName + "”章节基本信息");
        }

        public void DeleteAssistBookChapter(int chapterID)
        {
            dal.DeleteAssistBookChapter(chapterID);
        }

        public void DeleteAssistBookChapter(AssistBookChapter bookChapter)
        {
            string strChapterName = GetAssistBookChapter(bookChapter.ChapterId).ChapterName;

            AssistBookBLL objBookBll = new AssistBookBLL();
            string strBookName = objBookBll.GetAssistBook(bookChapter.AssistBookId).BookName;

            GetIndex(bookChapter.AssistBookId.ToString());

            objLogBill.WriteLog("删除辅导教材《" + strBookName + "》中“" + strChapterName + "”章节基本信息");

            dal.DeleteAssistBookChapter(bookChapter.ChapterId);
        }

        public void DeleteAssistBookChapterByBookID(int bookID)
        {
            dal.DeleteAssistBookChapterByBookID(bookID);
        }

        public void UpdateAssistBookChapterUrl(int chapterID, string url)
        {
            dal.UpateAssistBookChapterUrl(chapterID, url);
        }


        /// <summary>
        /// 发布教材
        /// </summary>
        /// <param name="strID"></param>
        public void GetIndex(string strID)
        {
            string strItem, strChapter;

            AssistBookBLL objBll = new AssistBookBLL();
            RailExam.Model.AssistBook objBook = objBll.GetAssistBook(Convert.ToInt32(strID));

            string strBookName = objBook.BookName;
            string strBookUrl = objBook.url;

            strItem = "var TREE_ITEMS = [ ['" + strBookName + "', 'common.htm?url=cover.htm&chapterid=0',";

            strChapter = "var Tree_Chapter=['0','cover.htm','" + strBookName + "'";

            IList<RailExam.Model.AssistBookChapter> objBookChapter = GetAssistBookChapterByBookID(Convert.ToInt32(strID));

            foreach (RailExam.Model.AssistBookChapter chapter in objBookChapter)
            {
                if (chapter.ParentId == 0)
                {
                    if (chapter.Url == "" || chapter.Url == null)
                    {
                        strItem += "['" + chapter.ChapterName + "', 'empty.htm'";
                        strChapter += ",'" + chapter.ChapterId + "','empty.htm','" + chapter.NamePath + "'";
                    }
                    else
                    {
                        strItem += "['" + chapter.ChapterName + "', '" + chapter.ChapterId + ".htm'";
                        strChapter += ",'" + chapter.ChapterId + "','" + chapter.ChapterId + ".htm','" + chapter.NamePath + "'";
                    }


                    if (GetAssistBookChapterByParentID(chapter.ChapterId).Count > 0)
                    {
                        strItem += ",";
                    }

                    strItem = Get(chapter.ChapterId, strItem);
                    strChapter = GetChapter(chapter.ChapterId, strChapter);
                }
            }

            strItem += "]];";
            string strPath = "../Online/AssistBook/" + strID + "/tree_items.js";
            File.Delete(HttpContext.Current.Server.MapPath(strPath));
            File.AppendAllText(HttpContext.Current.Server.MapPath(strPath), strItem, System.Text.Encoding.UTF8);

            strChapter += "];";
            strPath = "../Online/AssistBook/" + strID + "/tree_chapter.js";
            if (File.Exists(HttpContext.Current.Server.MapPath(strPath)))
            {
                File.Delete(HttpContext.Current.Server.MapPath(strPath));
            }
            File.AppendAllText(HttpContext.Current.Server.MapPath(strPath), strChapter, System.Text.Encoding.UTF8);
 
            string[] strIndex = File.ReadAllLines(HttpContext.Current.Server.MapPath("../Online/AssistBook/" + strID + "/index.html"), System.Text.Encoding.Default);

            for (int i = 0; i < strIndex.Length; i++)
            {
                if (strIndex[i].IndexOf("<title>") != -1)
                {
                    strIndex[i] = "\t<title> " + strBookName + " </title>";
                }
            }

            File.WriteAllLines(HttpContext.Current.Server.MapPath("../Online/AssistBook/" + strID + "/index.html"), strIndex, System.Text.Encoding.UTF8);
        }

        private string Get(int strParentID, string strItem)
        {
            IList<RailExam.Model.AssistBookChapter> objBookChapter = GetAssistBookChapterByParentID(strParentID);

            foreach (RailExam.Model.AssistBookChapter chapter in objBookChapter)
            {
                if (chapter.Url == "" || chapter.Url == null)
                {
                    strItem += "['" + chapter.ChapterName + "', 'empty.htm'";
                }
                else
                {
                    strItem += "['" + chapter.ChapterName + "', '" + chapter.ChapterId + ".htm'";
                }

                if (GetAssistBookChapterByParentID(chapter.ChapterId).Count > 0)
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
            IList<RailExam.Model.AssistBookChapter> objBookChapter = GetAssistBookChapterByParentID(strParentID);

            foreach (RailExam.Model.AssistBookChapter chapter in objBookChapter)
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

    }
}
