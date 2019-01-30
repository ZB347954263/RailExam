using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;

namespace RailExamWebApp.Book
{
    public partial class BookInfo : PageBase
    {
        private ZipOutputStream   zos=null;
        private string strBaseDir = ""; 


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                //ViewState["NowID"] = "false";
                if (PrjPub.HasEditRight("教材管理") && PrjPub.IsServerCenter)//&& PrjPub.CurrentLoginUser.SuitRange == 1
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("教材管理") && PrjPub.IsServerCenter)//&& PrjPub.CurrentLoginUser.SuitRange == 1
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

                OrganizationBLL orgBll = new OrganizationBLL();
                int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);
                HfOrgId.Value = orgID.ToString();

                hfEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();


                if (!string.IsNullOrEmpty(Request.QueryString.Get("postID")))
                {
                    hfPostID.Value = Request.QueryString.Get("postID");
                }

                BindGrid();

            }
            else
            {
                if (Request.Form.Get("Refresh") == "true")
                {
                    //ViewState["NowID"] = "false";
                    BindGrid();
                }
            }

            #region
            //string strGet = Request.Form.Get("Index");
            //if (strGet != null && strGet != "")
            //{
            //    GetIndex(strGet);
            //    BindGrid();
            //}

            //string strCover = Request.Form.Get("RefreshCover");
            //if(strCover!= null && strCover !="")
            //{
            //    string strBookUrl = "../Book/" + strCover + "/cover.htm";

            //    BookBLL objBill = new BookBLL();
            //    objBill.UpdateBookUrl(Convert.ToInt32(strCover), strBookUrl);

            //    string strBookName = objBill.GetBook(Convert.ToInt32(strCover)).bookName;

            //    string strPath = "../Online/Book/" + strCover + "/Cover.htm";
            //    string str = File.ReadAllText(Server.MapPath(strPath), System.Text.Encoding.UTF8);
            //    if (str.IndexOf("booktitle") < 0)
            //    {
            //        str = "<link href='book.css' type='text/css' rel='stylesheet' />"
            //             + "<div id='booktitle'>" + strBookName + "</div>" + "<br>"
            //             + str;
            //        File.WriteAllText(Server.MapPath(strPath), str, System.Text.Encoding.UTF8);
            //    }

            //    BookChapterBLL objChapterBll = new BookChapterBLL();
            //    objChapterBll.GetIndex(strCover);

            //    SystemLogBLL objLogBll = new SystemLogBLL();
            //    objLogBll.WriteLog("编辑教材《" + strBookName + "》前言");

            //    BindGrid();
            //}
            #endregion

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != null && strDeleteID != "")
            {
                DelBook(strDeleteID);
                BindGrid();
            }

            string strUpID = Request.Form.Get("UpID");
            if (strUpID != null && strUpID != "")
            {
                if (Request.QueryString.Get("id") != null)
                {
                    BookBLL objBll = new BookBLL();
                    RailExam.Model.Book obj = objBll.GetBook(Convert.ToInt32(strUpID));
                    obj.OrderIndex = obj.OrderIndex - 1;
                    objBll.UpdateBook(obj);
                }

                if (Request.QueryString.Get("id1") != null)
                {
                    int trainTypeID = Convert.ToInt32(Request.QueryString.Get("id1"));
                    BookTrainTypeBLL objTrainTypeBll = new BookTrainTypeBLL();
                    BookTrainType objTrainType =
                        objTrainTypeBll.GetBookTrainType(Convert.ToInt32(strUpID), trainTypeID);
                    objTrainType.OrderIndex = objTrainType.OrderIndex - 1;
                    objTrainTypeBll.UpdateBookTrainType(objTrainType);
                }
                BindGrid();
            }

            string strDownID = Request.Form.Get("DownID");
            if (strDownID != null && strDownID != "")
            {
                if (Request.QueryString.Get("id") != null)
                {
                    BookBLL objBll = new BookBLL();
                    RailExam.Model.Book obj = objBll.GetBook(Convert.ToInt32(strDownID));
                    obj.OrderIndex = obj.OrderIndex + 1;
                    objBll.UpdateBook(obj);
                }

                if (Request.QueryString.Get("id1") != null)
                {
                    int trainTypeID = Convert.ToInt32(Request.QueryString.Get("id1"));
                    BookTrainTypeBLL objTrainTypeBll = new BookTrainTypeBLL();
                    BookTrainType objTrainType =
                        objTrainTypeBll.GetBookTrainType(Convert.ToInt32(strDownID), trainTypeID);
                    objTrainType.OrderIndex = objTrainType.OrderIndex + 1;
                    objTrainTypeBll.UpdateBookTrainType(objTrainType);
                }
                BindGrid();
            }

            string strRefreshDown = Request.Form.Get("RefreshDown");
            if (strRefreshDown != null && strRefreshDown != "")
            {
                if(!DownloadBook(strRefreshDown))
                {
                    SessionSet.PageMessage = "当前教材不存在电子版教材！";
                    BindGrid();
                    return;
                }
                BindGrid();
            }


            if(!string.IsNullOrEmpty(hfPostID.Value))
            {
                PostBLL post = new PostBLL();
                txtPost.Text = post.GetPost(Convert.ToInt32(hfPostID.Value)).PostName;
            }
        }

        private bool DownloadBook(string bookId)
        {
            BookBLL objBll = new BookBLL();
            RailExam.Model.Book obj = objBll.GetBook(Convert.ToInt32(bookId));
            string filename = Server.MapPath("/RailExamBao/Online/Book/" + bookId + "/");

            if (!Directory.Exists(filename))
            {
                return false;
            }

            string ZipName = Server.MapPath("/RailExamBao/Online/Book/Book.zip");

			GzipCompress(filename,ZipName);

			FileInfo file = new FileInfo(ZipName.ToString());
			this.Response.Clear();
			this.Response.Buffer = true;
			this.Response.Charset = "utf-7";
			this.Response.ContentEncoding = Encoding.UTF7;
			// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
            
			this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(obj.bookName) + ".zip");
			// 添加头信息，指定文件大小，让浏览器能够显示下载进度
			this.Response.AddHeader("Content-Length", file.Length.ToString());
			// 指定返回的是一个不能被客户端读取的流，必须被下载
			this.Response.ContentType = "application/ms-word";
			// 把文件流发送到客户端
			this.Response.WriteFile(file.FullName);

            return true;
        }

		public void  GzipCompress(string   strPath,string   strFileName)
        { 
			zos = new ZipOutputStream(File.Create(strFileName)); // 指定zip文件的绝对路径，包括文件名            
            zos.SetLevel(6);

            strBaseDir=strPath; 
            addZipEntry(strBaseDir); 
            zos.Finish(); 
            zos.Close(); 

        } 

          private void  addZipEntry(string PathStr)
          {
              DirectoryInfo di = new DirectoryInfo(PathStr);
              foreach (DirectoryInfo   item   in   di.GetDirectories())
              {
                  addZipEntry(item.FullName);
              }
              foreach (FileInfo   item   in   di.GetFiles())
              {
                  FileStream fs = File.OpenRead(item.FullName);
                  byte[] buffer = new byte[fs.Length];
                  fs.Read(buffer, 0, buffer.Length);
                  string strEntryName = item.FullName.Replace(strBaseDir, "")
                  ;
                  ZipEntry entry = new ZipEntry(strEntryName);
                  zos.PutNextEntry(entry);
                  zos.Write(buffer, 0, buffer.Length);
                  fs.Close();
              }
          }


        #region  发布教材
        // <summary>
        // 发布教材
        // </summary>
        // <param name="strID"></param>
        //private void GetIndex(string strID)
        //{
        //    string  strItem;

        //    BookBLL objBll = new BookBLL();
        //    RailExam.Model.Book objBook = objBll.GetBook(Convert.ToInt32(strID));

        //    string strBookName = objBook.bookName;
        //    string strBookUrl = objBook.url;

        //    if (strBookUrl == "" || objBook.url == null)
        //    {
        //        strItem = "var TREE_ITEMS = [ ['" + strBookName + "', 'empty.htm',";
        //    }
        //    else
        //    {
        //        strItem = "var TREE_ITEMS = [ ['" + strBookName + "', 'cover.htm',";
        //    }


        //    BookChapterBLL objBookChapterBll = new BookChapterBLL();
        //    IList<RailExam.Model.BookChapter> objBookChapter = objBookChapterBll.GetBookChapterByBookID(Convert.ToInt32(strID));

        //    foreach (RailExam.Model.BookChapter chapter in objBookChapter)
        //    {
        //        if(chapter.ParentId == 0)
        //        {
        //            if (chapter.Url == "" || chapter.Url == null)
        //            {
        //                strItem += "['" + chapter.ChapterName + "', 'empty.htm',";
        //            }
        //            else
        //            {
        //                strItem += "['" + chapter.ChapterName + "', '" + chapter.ChapterId + ".htm',";
        //            }

        //            strItem = Get(chapter.ChapterId, strItem);
        //        }
        //    }

        //    strItem += "]];";

        //    string strPath = "../Online/Book/" + strID + "/tree_items.js";
        //    File.Delete(Server.MapPath(strPath));
        //    File.AppendAllText(Server.MapPath(strPath), strItem, System.Text.Encoding.UTF8);

        //    string[] strIndex = File.ReadAllLines(Server.MapPath("../Online/Book/" + strID + "/index.html"), System.Text.Encoding.Default);

        //    for (int i = 0; i < strIndex.Length; i++)
        //    {
        //        if (strIndex[i].IndexOf("<title>") != -1)
        //        {
        //            strIndex[i] = "\t<title> " + strBookName + " </title>";
        //        }
        //    }

        //    File.WriteAllLines(Server.MapPath("../Online/Book/" + strID + "/index.html"), strIndex, System.Text.Encoding.UTF8);

        //    ViewState["NowID"] = strID;
        //    Response.Write("<script>var re = window.open('Book/"+strID+"/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');re.focus();</script>");
        //}

        //private string Get(int strParentID, string strItem)
        //{
        //    BookChapterBLL objBookChapterBll = new BookChapterBLL();
        //    IList<RailExam.Model.BookChapter> objBookChapter = objBookChapterBll.GetBookChapterByParentID(strParentID);

        //    foreach (RailExam.Model.BookChapter chapter in objBookChapter)
        //    {
        //        if (chapter.Url == "" || chapter.Url == null)
        //        {
        //            strItem += "['" + chapter.ChapterName + "', 'empty.htm'";
        //        }
        //        else
        //        {
        //            strItem += "['" + chapter.ChapterName + "', '" + chapter.ChapterId + ".htm'";
        //        }

        //        strItem = Get(chapter.ChapterId, strItem);
        //    }

        //    strItem += "],";

        //    return strItem;
        //}

        // <summary>
        // 复写Render方法
        // </summary>
        // <param name="writer">书写器</param>
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    base.Render(writer);

        //    if ((string)ViewState["NowID"] != "false")
        //    {
        //        ViewState["NowCheck"] = "false";
        //        writer.Write("<script>var re = window.open('../Online/Book/" + ViewState["NowID"].ToString() + "/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');re.focus();</script>");
        //    }
        //}
        #endregion

        private void DelBook(string strID)
        {
            ItemBLL objItemBll = new ItemBLL();
            objItemBll.UpdateItemEnabled(Convert.ToInt32(strID), 0, 2);

            BookBLL objBll = new BookBLL();
            objBll.DeleteBook(Convert.ToInt32(strID));
        }

        private void BindGrid()
        {
            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> books = new List<RailExam.Model.Book>();
            IList<RailExam.Model.Book> booksViaPosts = new List<RailExam.Model.Book>();

            OrganizationBLL orgBll = new OrganizationBLL();
            int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);

            string strKnowledgeIDPath = Request.QueryString.Get("id");
            if (!string.IsNullOrEmpty(strKnowledgeIDPath))
            {
                if (strKnowledgeIDPath != "0")
                {
                    books = bookBLL.GetBookByKnowledgeID(Convert.ToInt32(strKnowledgeIDPath), orgID);
                }
                else
                {
                    if (PrjPub.CurrentLoginUser.SuitRange == 1)
                    {
                        books = bookBLL.GetAllBookInfo(0);
                    }
                    else
                    {
                        books = bookBLL.GetAllBookInfo(orgID);
                    }
                }
            }

            string strTrainTypeIDPath = Request.QueryString.Get("id1");
            if (!string.IsNullOrEmpty(strTrainTypeIDPath))
            {
                if (strTrainTypeIDPath != "0")
                {
                    books = bookBLL.GetBookByTrainTypeID(Convert.ToInt32(strTrainTypeIDPath), orgID);
                }
                else
                {
                    if (PrjPub.CurrentLoginUser.SuitRange == 1)
                    {
                        books = bookBLL.GetAllBookInfo(0);
                    }
                    else
                    {
                        books = bookBLL.GetAllBookInfo(orgID);
                    }
                }
            }

            OracleAccess oa = new OracleAccess();

            if (!string.IsNullOrEmpty(txtPost.Text.Trim()) || !string.IsNullOrEmpty(hfPostID.Value))
            {
                string postID = this.hfPostID.Value;

                string sql = String.Format(
                    @"select book_id from BOOK_RANGE_POST t 
                        where 
                            post_id = {0} 
                            or 
                            post_id in 
                                (select post_id from POST t where parent_id = {0})",
                    postID
                );

                DataSet dsBookIDs = oa.RunSqlDataSet(sql);
                if (dsBookIDs != null && dsBookIDs.Tables.Count > 0)
                {
                    foreach (RailExam.Model.Book book in books)
                    {
                        DataRow[] drs = dsBookIDs.Tables[0].Select("book_id=" + book.bookId);
                        if (drs.Length > 0)
                        {
                            booksViaPosts.Add(book);
                        }
                    }
                    books.Clear();
                    books = booksViaPosts;
                }
            }

            int railSystemid = PrjPub.RailSystemId();
            if (railSystemid != 0)
            {
                string sql = String.Format(
                    @"select book_id from BOOK_RANGE_ORG t 
                        where 
                            org_Id in (select org_id from org where rail_System_Id={0} and level_num=2) ",
                    railSystemid
                    );

                DataSet dsBookIDs = oa.RunSqlDataSet(sql);
                if (dsBookIDs != null && dsBookIDs.Tables.Count > 0)
                {
                    foreach (RailExam.Model.Book book in books)
                    {
                        DataRow[] drs = dsBookIDs.Tables[0].Select("book_id=" + book.bookId);
                        if (drs.Length > 0)
                        {
                            booksViaPosts.Add(book);
                        }
                    }
                    books.Clear();
                    books = booksViaPosts;
                }
            }

            if (books.Count > 0)
            {
                foreach (RailExam.Model.Book book in books)
                {
                    if(book.authors == null)
                    {
                        book.authors = "-1";
                    }

                    if (book.bookName.Length <= 30)
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                    }
                    else
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 15) + "...</a>";
                    }
                }

                Grid1.DataSource = books;
                Grid1.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> books = new List<RailExam.Model.Book>();
            IList<RailExam.Model.Book> booksViaPosts = new List<RailExam.Model.Book>();

            OrganizationBLL orgBll = new OrganizationBLL();
            int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);

            string strKnowledgeID = Request.QueryString.Get("id");

            if (!string.IsNullOrEmpty(strKnowledgeID))
            {
                if (strKnowledgeID != "0")
                {
                    string[] str1 = strKnowledgeID.Split(new char[] { '/' });
                    int nKnowledgeId = int.Parse(str1[str1.LongLength - 1].ToString());
                    books = bookBLL.GetBookByKnowledgeID(nKnowledgeId, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
                }
                else
                {
                    books = bookBLL.GetBookByKnowledgeID(0, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
                }
            }

            string strTrainTypeID = Request.QueryString.Get("id1");
            if (!string.IsNullOrEmpty(strTrainTypeID))
            {
                if (strTrainTypeID != "0")
                {
                    string[] str2 = strTrainTypeID.Split(new char[] { '/' });
                    int nTrainTypeID = int.Parse(str2[str2.LongLength - 1].ToString());
                    books = bookBLL.GetBookByTrainTypeID(nTrainTypeID, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
                }
                else
                {
                    books = bookBLL.GetBookByTrainTypeID(0, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
                }
            }

            if( string.IsNullOrEmpty(hfPostID.Value))
            {
                txtPost.Text = string.Empty;
            }

            OracleAccess oa = new OracleAccess();
            if (this.txtPost.Text.Trim().Length > 0)
            {                
                string postID = this.hfPostID.Value;

                string sql = String.Format(
                    @"select book_id from BOOK_RANGE_POST t 
                        where 
                            post_id = {0} 
                            or 
                            post_id in 
                                (select post_id from POST t where parent_id = {0})", 
                    postID
                );

                DataSet dsBookIDs = oa.RunSqlDataSet(sql);
                if (dsBookIDs != null && dsBookIDs.Tables.Count > 0)
                {
                    foreach (RailExam.Model.Book book in books)
                    {
                        DataRow[] drs = dsBookIDs.Tables[0].Select("book_id=" + book.bookId);
                        if(drs.Length > 0 )
                        {
                            booksViaPosts.Add(book);
                        }
                    }
                    books.Clear();
                    books = booksViaPosts;
                }
            }


            int railSystemid = PrjPub.RailSystemId();
            if (railSystemid != 0)
            {
                string sql = String.Format(
                    @"select book_id from BOOK_RANGE_ORG t 
                        where 
                            org_Id in (select org_id from org where rail_System_Id={0} and level_num=2) ",
                    railSystemid
                    );

                DataSet dsBookIDs = oa.RunSqlDataSet(sql);
                if (dsBookIDs != null && dsBookIDs.Tables.Count > 0)
                {
                    foreach (RailExam.Model.Book book in books)
                    {
                        DataRow[] drs = dsBookIDs.Tables[0].Select("book_id=" + book.bookId);
                        if (drs.Length > 0)
                        {
                            booksViaPosts.Add(book);
                        }
                    }
                    books.Clear();
                    books = booksViaPosts;
                }
            }

            if (books != null)
            {
                foreach (RailExam.Model.Book book in books)
                {
                    if (book.bookName.Length <= 15)
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                    }
                    else
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 15) + "...</a>";
                    }
                }

                Grid1.DataSource = books;
                Grid1.DataBind();
            }
        }

        protected void Grid1_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("id") == "0" || Request.QueryString.Get("id1") == "0" || PrjPub.CurrentLoginUser.SuitRange == 0)
            {
                Grid1.Levels[0].Columns[1].Visible = false;
            }

            string id = Request.QueryString.Get("id");
            if(!string.IsNullOrEmpty(id))
            {
                KnowledgeBLL objBll=new KnowledgeBLL();
                IList<RailExam.Model.Knowledge> objList = objBll.GetKnowledgesByParentID(Convert.ToInt32(id));

                if(objList.Count>0)
                {
                    Grid1.Levels[0].Columns[1].Visible = false;
                }
            }
        }
    }
}