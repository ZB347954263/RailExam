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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Train
{
    public partial class TrainAimDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
				if (!string.IsNullOrEmpty(Request.QueryString.Get("id")))
				{
					ViewState["TrainAimID"] = Request.QueryString.Get("id");
					TrainTypeBLL trainTypeBLL = new TrainTypeBLL();
					TrainType trainType = trainTypeBLL.GetTrainTypeInfo(Convert.ToInt32(ViewState["TrainAimID"].ToString()));

					lblDescription.Text = trainType.Description;
					lblMemo.Text = trainType.Memo;

				}
				else
				{
					ViewState["TrainAimID"] = Request.QueryString.Get("id1");
					PostBLL postBll =new PostBLL();
					Post post = postBll.GetPost(Convert.ToInt32(ViewState["TrainAimID"].ToString()));
					lblDescription.Text = post.Description;
					lblMemo.Text = post.Memo;
				}

                BindGrid1();
                BindGrid2();
            }

            string strRefresh = Request.Form.Get("Refresh");
            if (!string.IsNullOrEmpty(strRefresh))
            {
                BindGrid1();
                BindGrid2();
            }

            //string strDeleteID = Request.Form.Get("DeleteID");
            //if (strDeleteID != null & strDeleteID != "")
            //{
            //    DeleteExercise(strDeleteID);
            //    BindGrid1();
            //    BindGrid2();
            //}
        }

        //private void DeleteExercise(string strPaperID)
        //{
        //    TrainTypeExerciseBLL objBll = new TrainTypeExerciseBLL();
        //    objBll.DelTrainTypeExercise(Convert.ToInt32(ViewState["TrainTypeID"].ToString()), Convert.ToInt32(strPaperID));
        //}

        private void BindGrid1()
        {
			BookBLL bookBLL = new BookBLL();
			IList<RailExam.Model.Book> bookList  = new List<RailExam.Model.Book>();
			if (!string.IsNullOrEmpty(Request.QueryString.Get("id")))
			{
				TrainTypeBLL trainTypeBLL = new TrainTypeBLL();
				TrainType trainType = trainTypeBLL.GetTrainTypeInfo(Convert.ToInt32(ViewState["TrainAimID"].ToString()));
				bookList = bookBLL.GetBookByTrainTypeIDPath(trainType.IDPath);
			}
			else
			{
				bookList = bookBLL.GetEmployeeStudyBookInfoByKnowledgeID(-1, PrjPub.CurrentLoginUser.StationOrgID,
				                                                         Convert.ToInt32(ViewState["TrainAimID"].ToString()), 1, 5,
				                                                         0);
			}

        	if (bookList.Count > 0)
            {
                foreach (RailExam.Model.Book book in bookList)
                {
                    if (book.bookName.Length <= 15)
                    {
                        book.bookName = "<a onclick=EditBook(" + book.bookId + ") href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                    }
                    else
                    {
                        book.bookName = "<a onclick=EditBook(" + book.bookId + ") href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 15) + " </a>";
                    }
                }

                gvBook.DataSource = bookList;
                gvBook.DataBind();
            }
        }

        private void BindGrid2()
        {
			CoursewareBLL coursewareBLL = new CoursewareBLL();
			IList<RailExam.Model.Courseware> coursewareList = new List<RailExam.Model.Courseware>();

			if (!string.IsNullOrEmpty(Request.QueryString.Get("id")))
			{
				TrainTypeBLL trainTypeBLL = new TrainTypeBLL();
				TrainType trainType = trainTypeBLL.GetTrainTypeInfo(Convert.ToInt32(ViewState["TrainAimID"].ToString()));
				 coursewareList = coursewareBLL.GetCoursewaresByTrainTypeIDPath(trainType.IDPath, 1);
			}
			else
			{
				coursewareList = coursewareBLL.GetCoursewaresByCoursewareTypeOnline(PrjPub.CurrentLoginUser.StationOrgID,
				                                                              Convert.ToInt32(ViewState["TrainAimID"].ToString()), "%",true,
				                                                              5);
			}

        	if (coursewareList.Count > 0)
            {
                foreach (RailExam.Model.Courseware  courseware in coursewareList)
                {
                    if (courseware.CoursewareName.Length <= 15)
                    {
                        courseware.CoursewareName = "<a onclick=EditCourseware(" + courseware.CoursewareID + ") href=# title=" + courseware.CoursewareName + " > " + courseware.CoursewareName + " </a>";
                    }
                    else
                    {
                        courseware.CoursewareName = "<a onclick=EditCourseware(" + courseware.CoursewareID + ") href=# title=" + courseware.CoursewareName + " > " + courseware.CoursewareName.Substring(0, 15) + "..." + " </a>";
                    }
                }
                gvCourse.DataSource = coursewareList;
                gvCourse.DataBind();
            }
        }

        //private void BindGrid3()
        //{
        //    TrainTypeExerciseBLL objBll = new TrainTypeExerciseBLL();
        //    IList<TrainTypeExercise> objList = objBll.GetTrainTypeExerciseByTrainTypeID(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));

        //    if(objList.Count > 0)
        //    {
        //        foreach(TrainTypeExercise obj in objList)
        //        {
        //            obj.ObjPaper.PaperName = "<a href=#  onclick=ManagePaper('" + obj.PaperID + "') title=" + obj.ObjPaper.PaperName + ">" + obj.ObjPaper.PaperName;
        //        }
        //    }

        //    gvExercise.DataSource = objList;
        //    gvExercise.DataBind();
        //}

        //private void BindGrid4()
        //{
        //    TrainTypeTaskBLL objBll = new TrainTypeTaskBLL();
        //    IList<TrainTypeTask> objList = objBll.GetTrainTypeTaskByTrainTypeID(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));

        //    if (objList.Count > 0)
        //    {
        //        foreach (TrainTypeTask obj in objList)
        //        {
        //            obj.ObjPaper.PaperName = "<a href=#  onclick=ManagePaper('" + obj.PaperID + "') title=" + obj.ObjPaper.PaperName + ">" + obj.ObjPaper.PaperName;
        //        }
        //    } 
        //    gvTask.DataSource = objList;
        //    gvTask.DataBind();
        //}
    }
}