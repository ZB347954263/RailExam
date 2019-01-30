using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Train
{
    public partial class TrainCourseBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["StandardID"] = Request.QueryString.Get("StandardID");
                ViewState["CourseID"] = Request.QueryString.Get("CourseID");
                ViewState["CourseName"] = Request.QueryString.Get("CourseName");
                //ViewState["CourseID"] = "1";
                //ViewState["PostID"] = "1";
                if (Request.QueryString.Get("StandardID") != null && Request.QueryString.Get("StandardID") != "")
                {
                    GetPost();
                    BindBookTree();
                }
                else
                {
                    lblTitle1.Text = "教材信息";
                    BindBookTree();
                }

                BindChooseBookTree();
                lblTitle2.Text = Request.QueryString.Get("CourseName") + "课程教材";
            }
        }

        private void GetPost()
        {
            TrainStandardBLL objTrainStandardBll = new TrainStandardBLL();
            RailExam.Model.TrainStandard objTrainStandard =
                objTrainStandardBll.GetTrainStandardInfo(Convert.ToInt32(ViewState["StandardID"].ToString()));

            ViewState["PostID"] = objTrainStandard.PostID;
            lblTitle1.Text = objTrainStandard.PostName + "岗位相关教材";
        }

        private void BindBookTree()
        {
            tvBook.Nodes.Clear();
            BookBLL objBookBll = new BookBLL();
            IList<RailExam.Model.Book> objBookList = null;

            if (Request.QueryString.Get("StandardID") != null && Request.QueryString.Get("StandardID") != "")
            {
                objBookList = objBookBll.GetTrainCourseBookChapterTree(Convert.ToInt32(ViewState["PostID"].ToString()));
            }
            else
            {
                objBookList = objBookBll.GetAllBookInfo(1);
            }

            ArrayList objList = GetChapterList();

            if (objBookList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.Book book in objBookList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = book.bookId.ToString();
                    tvn.Value = book.bookId.ToString();
                    tvn.Text = book.bookName;
                    tvn.ToolTip = book.bookName;
                    tvn.ShowCheckBox = true;

                    tvBook.Nodes.Add(tvn);

                    if (objList.Count > 0)
                    {
                        if (objList.IndexOf(book.bookId.ToString()) != -1)
                        {
                            tvn.Checked = true;
                        }
                    }

                    BookChapterBLL objBookChapterBll = new BookChapterBLL();
                    IList<RailExam.Model.BookChapter> objBookChapterList =
                        objBookChapterBll.GetTrainCourseBookChapterTree(book.bookId);

                    if (objBookChapterList.Count > 0)
                    {
                        foreach (BookChapter objBookChapter in objBookChapterList)
                        {
                            tvn = new TreeViewNode();
                            tvn.ID = objBookChapter.ChapterId.ToString();
                            tvn.Value = book.bookId.ToString();
                            tvn.Text = objBookChapter.ChapterName;
                            tvn.ToolTip = objBookChapter.ChapterName;
                            tvn.ShowCheckBox = true;

                            if (objList.Count > 0)
                            {
                                if (objList.IndexOf(objBookChapter.ChapterId) != -1)
                                {
                                    tvn.Checked = true;
                                }
                            }

                            if (objBookChapter.ParentId == 0)
                            {
                                try
                                {
                                    tvBook.FindNodeById(book.bookId.ToString()).Nodes.Add(tvn);
                                }
                                catch
                                {
                                    tvBook.Nodes.Clear();
                                    Response.Write("数据错误！");
                                    return;
                                }
                            }
                            else
                            {
                                try
                                {
                                    tvBook.FindNodeById(objBookChapter.ParentId.ToString()).Nodes.Add(tvn);
                                }
                                catch
                                {
                                    tvBook.Nodes.Clear();
                                    Response.Write("数据错误！");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            tvBook.DataBind();
            tvBook.ExpandAll();
        }

        protected void btnChoose_Click(object sender, EventArgs e)
        {
            AddTrainCourseBook(tvBook.Nodes);
            BindChooseBookTree();
        }

        private void AddTrainCourseBook(TreeViewNodeCollection tvNodes)
        {
            ArrayList objList = GetChapterList();
            foreach (TreeViewNode node in tvNodes)
            {
                if (node.Checked == true)
                {
                    if (objList.IndexOf(node.ID) == -1)
                    {
                        TrainCourseBookBLL trainCourseBookBLL = new TrainCourseBookBLL();
                        RailExam.Model.TrainCourseBook trainCourseBook = new RailExam.Model.TrainCourseBook();

                        if (node.ParentNode == null)
                        {
                            trainCourseBook.ChapterID = string.Empty;
                        }
                        else
                        {
                            trainCourseBook.ChapterID = node.ID;
                        }
                        trainCourseBook.CourseID = Convert.ToInt32(ViewState["CourseID"].ToString());
                        trainCourseBook.BookID = Convert.ToInt32(node.Value);

                        trainCourseBookBLL.AddTrainCourseBook(trainCourseBook);
                    }
                }

                if (node.Nodes.Count > 0)
                {
                    AddTrainCourseBook(node.Nodes);
                }
            }
        }

        /// <summary>
        /// 获取当前课程中教材章节ID的列表


        /// </summary>
        /// <returns></returns>
        private ArrayList GetChapterList()
        {
            ArrayList objList = new ArrayList();

            TrainCourseBookBLL trainCourseBookBLL = new TrainCourseBookBLL();
            IList<RailExam.Model.TrainCourseBook> trainCourseBookList =
                trainCourseBookBLL.GetTrainCourseBookChapter(Convert.ToInt32(ViewState["CourseID"].ToString()), "order by a.Level_Num,a.order_index Asc,a.train_course_book_chapter_id");

            if (trainCourseBookList.Count > 0)
            {
                foreach (RailExam.Model.TrainCourseBook trainCourseBook in trainCourseBookList)
                {
                    if (trainCourseBook.ChapterID != null)
                    {
                        objList.Add(trainCourseBook.ChapterID);
                    }
                    else
                    {
                        objList.Add(trainCourseBook.BookID.ToString());
                    }
                }
            }
            return objList;
        }


        private void BindChooseBookTree()
        {
            tvChooseBook.Nodes.Clear();

            TrainCourseBookBLL trainCourseBookBLL = new TrainCourseBookBLL();
            IList<RailExam.Model.TrainCourseBook> trainCourseBookList =
                trainCourseBookBLL.GetTrainCourseBookChapter(Convert.ToInt32(ViewState["CourseID"].ToString()), "order by a.Level_Num,a.order_index Asc,a.train_course_book_chapter_id");
            
            Pub.BuildComponentArtTreeView(tvChooseBook, (IList)trainCourseBookList, "TrainCourseBookChapterID", "ParentID", "ChapterName", "ChapterName", "TrainCourseBookChapterID", null, null, null);

            ShowCheckBox(tvChooseBook.Nodes);

            tvChooseBook.ExpandAll();
        }

        private void ShowCheckBox(TreeViewNodeCollection nodes)
        {
            foreach (TreeViewNode node in nodes)
            {
                node.ShowCheckBox = true;

                if (node.Nodes.Count > 0)
                {
                    ShowCheckBox(node.Nodes);
                }
            }
        }

        protected void tvChooseBookChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            tvChooseBook.Nodes.Clear();
            BindChooseBookTree();
            tvChooseBook.RenderControl(e.Output);
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("StandardID") != null && Request.QueryString.Get("StandardID") != "")
            {
                Response.Redirect("TrainCourseEdit.aspx?CourseID=" + ViewState["CourseID"].ToString() + "&name=" + ViewState["CourseName"].ToString() + "&StandardID=" + ViewState["StandardID"].ToString());
            }
            else
            {
                Response.Redirect("TrainCommonCourseEdit.aspx?CourseID=" + ViewState["CourseID"].ToString() + "&name=" + ViewState["CourseName"].ToString());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("StandardID") != null && Request.QueryString.Get("StandardID") != "")
            {
                Response.Redirect("TrainCourseCourseware.aspx?CourseID=" + ViewState["CourseID"].ToString() + "&CourseName=" +
                                  ViewState["CourseName"].ToString() + "&StandardID=" + ViewState["StandardID"].ToString());
            }
            else
            {
                Response.Redirect("TrainCourseCourseware.aspx?CourseID=" + ViewState["CourseID"].ToString() + "&CourseName=" +
                                  ViewState["CourseName"].ToString());
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true' ;window.opener.form1.submit();window.close();</script>");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            DelTrainCourseBook(tvChooseBook.Nodes);
            BindBookTree();
            BindChooseBookTree();
        }

        private void DelTrainCourseBook(TreeViewNodeCollection tvNodes)
        {
            foreach (TreeViewNode node in tvNodes)
            {
                if (node.Checked == true)
                {
                    TrainCourseBookBLL obj = new TrainCourseBookBLL();
                    int n = 0;
                    foreach (TreeViewNode node1 in node.Nodes)
                    {
                        if (node1.Checked == true)
                        {
                            n = n + 1;
                        }
                    }

                    if (n != node.Nodes.Count)
                    {
                        Response.Write("<script>alert('要移除已选教材章节的父节点时必须选中其所有子节点！');</script>");
                        return;
                    }
                    else
                    {
                        obj.DeleteTrainCourseBook(Convert.ToInt32(node.ID));
                    }
                    obj.DeleteTrainCourseBook(Convert.ToInt32(node.ID));
                }

                if (node.Nodes.Count > 0)
                {
                    DelTrainCourseBook(node.Nodes);
                }
            }
        }

        private void IsDel(TreeViewNodeCollection tvNodes)
        {
            int n = 0;

            foreach (TreeViewNode node in tvNodes)
            {
                if (node.Checked == false)
                {
                    n = n + 1;
                }

                if (n == 0)
                {
                    if (node.Nodes.Count > 0)
                    {
                        IsDel(node.Nodes);
                    }
                }
                else
                {
                    Response.Write("<script>alert('要移除已选教材章节的父节点时必须选中其所有子节点！');</script>");
                    return;
                }
            }
        }
    }
}