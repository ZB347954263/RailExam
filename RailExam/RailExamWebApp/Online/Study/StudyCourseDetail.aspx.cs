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
    public partial class StudyCourseDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TrainEmployeeBLL objTrainEmployeeBll = new TrainEmployeeBLL();

            string str = Request.QueryString.Get("id");

            if (str != null && str != "")
            {
                ViewState["TrainTypeID"] = str;
            }
            else
            {
                if (PrjPub.StudentID == null)
                {
                    Response.Write("<script>alert('已超时，请注销后重新登录！');window.close();</script>");
                    return;
                }
                ViewState["TrainTypeID"] = objTrainEmployeeBll.GetTrainEmployeeByEmployeeID(PrjPub.CurrentStudent.EmployeeID).TrainTypeID.ToString();
            }

            if (ViewState["TrainTypeID"].ToString() == "0")
            {
				Response.Write("<script>alert('请选择培训类别！');window.close();</script>");
                return;
            }

            TrainTypeBLL objTrainTypeBll = new TrainTypeBLL();
            TrainType objTrainType = objTrainTypeBll.GetTrainTypeInfo(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));

            lblDescription.Text = objTrainType.Description;
            lblMemo.Text = objTrainType.Memo;

            BindGrid1();
            BindGrid2();
        }

        private void BindGrid1()
        {
            bool isGroup = PrjPub.CurrentStudent.IsGroupLearder;
            int tech = PrjPub.CurrentStudent.TechnicianTypeID;

            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> bookList = bookBLL.GetEmployeeStudyBookInfo(Convert.ToInt32(ViewState["TrainTypeID"].ToString()), PrjPub.CurrentStudent.OrgID, PrjPub.CurrentStudent.PostID, isGroup,tech,0);

            if (bookList.Count > 0)
            {
                foreach (RailExam.Model.Book book in bookList)
                {
                    //obj.url = "~/BookHtml/" + obj.bookId + "/index.html";
                    if (book.bookName.Length <= 15)
                    {
                        book.bookName = "<a href=" + book.url + " target='_blank' title=" + book.bookName + ">" + book.bookName;
                    }
                    else
                    {
                        book.bookName = "<a href=" + book.url + " target='_blank' title=" + book.bookName + ">" + book.bookName.Substring(0, 15) + "...";
                    }
                }

                gvBook.DataSource = bookList;
                gvBook.DataBind();
            }
        }

        private void BindGrid2()
        {
            bool isGroup = PrjPub.CurrentStudent.IsGroupLearder;
            int tech = PrjPub.CurrentStudent.TechnicianTypeID;

            CoursewareBLL coursewareBLL = new CoursewareBLL();
            IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetEmployeeStudyCoursewareInfo(Convert.ToInt32(ViewState["TrainTypeID"].ToString()), PrjPub.CurrentStudent.OrgID, PrjPub.CurrentStudent.PostID, isGroup, tech, 0);

            if (coursewareList.Count > 0)
            {
                foreach (RailExam.Model.Courseware courseware in coursewareList)
                {
                    if (courseware.CoursewareName.Length <= 15)
                    {
                        courseware.CoursewareName = "<a href=" + courseware.Url + " target='_blank' title=" + courseware.CoursewareName + ">" + courseware.CoursewareName;
                    }
                    else
                    {
                        courseware.CoursewareName = "<a href=" + courseware.Url + " target='_blank' title=" + courseware.CoursewareName + ">" + courseware.CoursewareName.Substring(0, 15) + "...";
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

        //    gvExercise.DataSource = objList;
        //    gvExercise.DataBind();
        //}

        //private void BindGrid4()
        //{
        //    TrainTypeTaskBLL objBll = new TrainTypeTaskBLL();
        //    IList<TrainTypeTask> objList = objBll.GetTrainTypeTaskByTrainTypeID(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));
        //    gvTask.DataSource = objList;
        //    gvTask.DataBind();
        //}
    }
}
