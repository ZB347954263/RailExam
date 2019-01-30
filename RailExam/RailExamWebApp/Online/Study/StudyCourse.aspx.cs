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

namespace RailExamWebApp.Online.Study
{
    public partial class StudyCourse : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PrjPub.CurrentStudent != null)
            {
                TrainEmployeeBLL objTrainEmployeeBll = new TrainEmployeeBLL();
                ViewState["TrainTypeID"] = objTrainEmployeeBll.GetTrainEmployeeByEmployeeID(PrjPub.CurrentStudent.EmployeeID).TrainTypeID.ToString();

                if(ViewState["TrainTypeID"].ToString() == "0")
                {
					lblType.Text = "请选择培训类别";
                }
                else
                {
                    BindGrid1();
                    BindGrid2();
                    //BindGrid3();
                    //BindGrid4();

                    lblType.Text = "";

                    TrainTypeBLL objTrainTypeBll = new TrainTypeBLL();
                    TrainType objTrainType = objTrainTypeBll.GetTrainTypeInfo(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));

                    if (objTrainType == null)
                    {
                        lblType.Text = "";
                    }
                    else
                    {
                        lblType.Text = lblType.Text + GetType(" - " + objTrainType.TypeName, objTrainType.ParentID);
                    }
                }
            }
            else
            {
                Response.Redirect("NewStudyInfo.aspx");
            }
        }

        private string GetType(string strName, int nID)
        {
            string str = "";
            if (nID != 0)
            {
                TrainTypeBLL objTrainTypeBll = new TrainTypeBLL();
                TrainType objTrainType = objTrainTypeBll.GetTrainTypeInfo(nID);

                if (objTrainType.ParentID != 0)
                {
                    str = GetType(" - " + objTrainType.TypeName, objTrainType.ParentID) + strName;
                }
                else
                {
                    str = objTrainType.TypeName + strName;
                }
            }

            return str;
        }

        private void BindGrid1()
        {
            bool isGroup = PrjPub.CurrentStudent.IsGroupLearder;
            int tech = PrjPub.CurrentStudent.TechnicianTypeID;

            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> bookList = bookBLL.GetEmployeeStudyBookInfo(Convert.ToInt32(ViewState["TrainTypeID"].ToString()), PrjPub.CurrentStudent.OrgID, PrjPub.CurrentStudent.PostID, isGroup,tech,5);

            if (bookList.Count > 0)
            {
                foreach (RailExam.Model.Book book in bookList)
                {
                    if (book.bookName.Length <= 13)
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                    }
                    else
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 13) + " </a>";
                    }
                }
            }

            gvBook.DataSource = bookList;
            gvBook.DataBind();
        }

        private void BindGrid2()
        {
            bool isGroup = PrjPub.CurrentStudent.IsGroupLearder;
            int tech = PrjPub.CurrentStudent.TechnicianTypeID;

            CoursewareBLL coursewareBLL = new CoursewareBLL();
            IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetEmployeeStudyCoursewareInfo(Convert.ToInt32(ViewState["TrainTypeID"].ToString()), PrjPub.CurrentStudent.OrgID, PrjPub.CurrentStudent.PostID, isGroup,tech,5);

            if (coursewareList.Count > 0)
            {
                foreach (RailExam.Model.Courseware  courseware in coursewareList)
                {
                    courseware.ToolTip = courseware.CoursewareName;
                    if (courseware.CoursewareName.Length <= 13)
                    {
                        courseware.CoursewareName = courseware.CoursewareName;
                    }
                    else
                    {
                        courseware.CoursewareName = courseware.CoursewareName.Substring(0, 13) + "...";
                    }
                }
            }

            gvCourse.DataSource = coursewareList;
            gvCourse.DataBind();
        }

        //private void BindGrid3()
        //{
        //    TrainTypeExerciseBLL objBll = new TrainTypeExerciseBLL();
        //    IList<TrainTypeExercise> objList = objBll.GetTrainTypeExerciseByTrainTypeID(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));

        //    if (objList.Count > 0)
        //    {
        //        foreach (TrainTypeExercise obj in objList)
        //        {
        //            obj.Memo = "<a href=../../Paper/ExerciseAnswer.aspx?id=" + obj.PaperID + "  target='_blank' title=" + obj.ObjPaper.PaperName + ">" + obj.ObjPaper.PaperName;
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
        //            obj.Memo = "<a href=../../Paper/TaskAnswer.aspx?id=" + obj.PaperID + "&TrainTypeID=" + ViewState["TrainTypeID"].ToString() + " target='_blank' title=" + obj.ObjPaper.PaperName + ">" + obj.ObjPaper.PaperName;
        //        }
        //    }
        //    gvTask.DataSource = objList;
        //    gvTask.DataBind();
        //}
    }
}
