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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PrjPub.StudentID))
            {
                Response.Write("<script>alert('您还没有登录，不能查看在线学习信息！');window.close();</script>");
                return;
            }

            TrainEmployeeBLL objTrainEmployeeBll = new TrainEmployeeBLL();
            ViewState["TrainTypeID"] = objTrainEmployeeBll.GetTrainEmployeeByEmployeeID(PrjPub.CurrentStudent.EmployeeID).TrainTypeID.ToString();

            if (ViewState["TrainTypeID"].ToString() == "0")
            {
				lblType.Text = "请选择培训类别";
            }
            else
            {
                BindGrid1();
                BindGrid2();

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
                    if (book.bookName.Length <= 20)
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                    }
                    else
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 20) + " </a>";
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
                foreach (RailExam.Model.Courseware courseware in coursewareList)
                {
                    if (courseware.CoursewareName.Length <= 20)
                    {
                        courseware.CoursewareName = "<a href=" + courseware.Url + " target='_blank' title=" + courseware.CoursewareName + ">" + courseware.CoursewareName;
                    }
                    else
                    {
                        courseware.CoursewareName = "<a href=" + courseware.Url + " target='_blank' title=" + courseware.CoursewareName + ">" + courseware.CoursewareName.Substring(0, 20) + "...";
                    }
                }
            }

            gvCourse.DataSource = coursewareList;
            gvCourse.DataBind();
        }

    }
}
