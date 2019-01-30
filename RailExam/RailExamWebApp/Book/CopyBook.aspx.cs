using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using System.Collections.Generic;

namespace RailExamWebApp.Book
{
    public partial class CopyBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //if(PrjPub.CurrentLoginUser == null)
                //{
                //    Response.Write("<script>alert('您还没有登录!');window.close();</script>");
                //}
                //else
                //{
                //    if (!PrjPub.IsServerCenter || PrjPub.CurrentLoginUser.OrgID != 1 || !PrjPub.CurrentLoginUser.IsAdmin)
                //    {
                //        Response.Write("<script>alert('您没有权限浏览本页面!');window.close();</script>");
                //    }
                //}
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            string[] fileList = Directory.GetFileSystemEntries(Server.MapPath("/RailExamBao/Online/Book/"));

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    if (Path.GetFileName(file) != "template")
                    {
                        CopyTemplate(Server.MapPath("/RailExamBao/Online/Book/template/"), file+"\\");
                    }
                }
            }
            SessionSet.PageMessage = "复制成功！";
            //CopyTemplate(Server.MapPath("/RailExamBao/Online/Book/template/"), Server.MapPath("/RailExamBao/Online/Book/39/"));
        }

        private void CopyTemplate(string srcPath, string aimPath)
        {
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }

            string[] fileList = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    CopyTemplate(file, aimPath + Path.GetFileName(file) + "\\");
                }
                else
                {
                    if (Path.GetFileName(file) == "tree_items.js" || Path.GetFileName(file) == "cover.htm" || Path.GetFileName(file) == "tree_chapter.js")
                    {
                        if(!File.Exists(aimPath + Path.GetFileName(file)))
                        {
                            File.Copy(file, aimPath + Path.GetFileName(file), true);
                        }
                    }
                    else
                    {
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                    }
                }
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            BookBLL objbll = new BookBLL();
            IList<RailExam.Model.Book> objList = objbll.GetAllBookInfo(0);

            ArrayList objBookIDList = new ArrayList();
            foreach (RailExam.Model.Book book in objList)
            {
                objBookIDList.Add(book.bookId.ToString());
            }

            string[] fileList = Directory.GetFileSystemEntries(Server.MapPath("/RailExamBao/Online/Book/"));

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    if (objBookIDList.IndexOf(Path.GetFileName(file)) == -1 && Path.GetFileName(file) != "template")
                    {
                          Directory.Delete(file,true);
                    }
                }
            }
            SessionSet.PageMessage = "整理成功！";
        }

        protected void btnCopyAssist_Click(object sender, EventArgs e)
        {
            string[] fileList = Directory.GetFileSystemEntries(Server.MapPath("/RailExamBao/Online/AssistBook/"));

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    if (Path.GetFileName(file) != "template")
                    {
                        CopyTemplate(Server.MapPath("/RailExamBao/Online/AssistBook/template/"), file + "\\");
                    }
                }
            }
            SessionSet.PageMessage = "复制成功！";
        }

        protected void btnDelAssist_Click(object sender, EventArgs e)
        {
            AssistBookBLL objbll = new AssistBookBLL();
            IList<RailExam.Model.AssistBook> objList = objbll.GetAllAssistBookInfo(0);

            ArrayList objBookIDList = new ArrayList();
            foreach (RailExam.Model.AssistBook book in objList)
            {
                objBookIDList.Add(book.AssistBookId.ToString());
            }

            string[] fileList = Directory.GetFileSystemEntries(Server.MapPath("/RailExamBao/Online/AssistBook/"));

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    if (objBookIDList.IndexOf(Path.GetFileName(file)) == -1 && Path.GetFileName(file) != "template")
                    {
                        Directory.Delete(file, true);
                    }
                }
            }
            SessionSet.PageMessage = "整理成功！";
        }

        protected void btnCopyHtm_Click(object sender, EventArgs e)
        {
            BookBLL objbll = new BookBLL();
            IList<RailExam.Model.Book> objList = objbll.GetAllBookInfo(0);

            ArrayList objBookIDList = new ArrayList();
            foreach (RailExam.Model.Book book in objList)
            {
                objBookIDList.Add(book.bookId.ToString());
            }

            string[] fileList = Directory.GetFileSystemEntries(Server.MapPath("/RailExamBao/Online/Book/"));

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    if (objBookIDList.IndexOf(Path.GetFileName(file)) !=-1 && Path.GetFileName(file) != "template")
                    {
                        CopyHtm(file, Path.GetFileName(file));
                    }
                }
            }
            SessionSet.PageMessage = "复制成功！";
        }

        private void CopyHtm(string strPath,string bookid)
        {
            string[] fileList = Directory.GetFileSystemEntries(strPath);

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                }
                else
                {
                    if (Path.GetFileName(file).IndexOf("htm") != -1 && Path.GetFileName(file).IndexOf("html") == -1 && Path.GetFileName(file) != "cover.htm" && Path.GetFileName(file) != "empty.htm" && Path.GetFileName(file) != "common.htm")
                    {
                        string str = File.ReadAllText(file, Encoding.UTF8);

                        str = str.Replace("<link href='book.css' type='text/css' rel='stylesheet' />", "");

                        str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                              + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                              + str + "</body>";

                        File.WriteAllText(file, str, Encoding.UTF8);
                    }
                    else if (Path.GetFileName(file) == "cover.htm")
                    {
                        BookBLL objBill = new BookBLL();
                        RailExam.Model.Book obj = objBill.GetBook(Convert.ToInt32(bookid));

                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }

                        string str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                                     + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                                     + "<br><br><br><br><br><br><br>"
                                     + "<div id='booktitle'>" + obj.bookName + "</div>" + "<br>"
                                     + "<br><br><br><br><br><br><br><br><br><br><br>"
                                     + "<div id='orgtitle'>" + obj.publishOrgName + "</div>" + "<br>"
                                     + "<div id='datetitle'>" + obj.publishDate.ToLongDateString() + "</div>"
                                     + "</body>";

                        File.AppendAllText(file, str, System.Text.Encoding.UTF8);
                    }
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            BookBLL objbll = new BookBLL();
            BookChapterBLL objChapterbll = new BookChapterBLL();
            IList<RailExam.Model.Book> objList = objbll.GetAllBookInfo(0);

            ArrayList objBookIDList = new ArrayList();
            foreach (RailExam.Model.Book book in objList)
            {
                if(Directory.Exists(Server.MapPath("../Online/Book/"+book.bookId+"/")))
                {
                    SaveBookCover(book.bookId.ToString());
                    objChapterbll.GetIndex(book.bookId.ToString());
                }
            }
            SessionSet.PageMessage = "发布成功！";
        }

        private void SaveBookCover(string bookid)
        {
            string strBookUrl = "../Book/" + bookid + "/cover.htm";
            BookBLL objBill = new BookBLL();
            objBill.UpdateBookUrl(Convert.ToInt32(bookid), strBookUrl);

            string srcPath = "../Online/Book/" + bookid + "/cover.htm";

            RailExam.Model.Book obj = objBill.GetBook(Convert.ToInt32(bookid));

            if (File.Exists(Server.MapPath(srcPath)))
            {
                File.Delete(Server.MapPath(srcPath));
            }

            string str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                         + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                         + "<br><br><br><br><br><br><br>"
                         + "<div id='booktitle'>" + obj.bookName + "</div>" + "<br>"
                         + "<br><br><br><br><br><br><br><br><br><br><br><br>"
                         + "<div id='orgtitle'>" + obj.publishOrgName + "</div>" + "<br>"
                         + "</body>";

            File.AppendAllText(Server.MapPath(srcPath), str, System.Text.Encoding.UTF8);
        }

        protected void btnItem_Click(object sender, EventArgs e)
        {
            ItemBLL objBll = new ItemBLL();
            objBll.GetItemsNoPicutre();
            SessionSet.PageMessage = "整理成功！";
        }

        protected void btnUpper_Click(object sender, EventArgs e)
        {
            EmployeeBLL objbll = new EmployeeBLL();
            IList<Employee> objList = objbll.GetAllEmployees();
            foreach (Employee employee in objList)
            {
                employee.PinYinCode = Pub.GetChineseSpell(employee.EmployeeName);
                objbll.UpdateEmployeeWithOutLog(employee);
            }
            SessionSet.PageMessage = "生成成功！";
        }

		protected void btnSelectName_Click(object sender, EventArgs e)
		{
			EmployeeDetailBLL objbll =new EmployeeDetailBLL();
			IList<EmployeeDetail> objList = objbll.GetEmployeeByWhereClause("1=1");
			int i = 0;
			foreach (EmployeeDetail detail in objList)
			{
				if(detail.EmployeeName.IndexOf("\0\0")>=0)
				{
					i = i + 1;
				}
			}

			SessionSet.PageMessage = i.ToString();
		}

		protected void btnUpdateName_Click(object sender, EventArgs e)
		{
			EmployeeDetailBLL objbll = new EmployeeDetailBLL();
			IList<EmployeeDetail> objList = objbll.GetEmployeeByWhereClause("1=1");
			foreach (EmployeeDetail detail in objList)
			{
				if(detail.EmployeeName.IndexOf("\0\0")>=0)
				{
					detail.EmployeeName = detail.EmployeeName.Replace("\0\0", "");
					objbll.UpdateEmployee(detail);
				}
			}

			SessionSet.PageMessage = "更新成功！";
		}
    }
}
