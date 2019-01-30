using System;
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyBookDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TrainEmployeeBLL objTrainEmployeeBll = new TrainEmployeeBLL();

                if (PrjPub.StudentID == null)
                {
                    Response.Write("<script>alert('已超时，请注销后重新登录！');window.close();</script>");
                    return;
                }
                ViewState["TrainTypeID"] = objTrainEmployeeBll.GetTrainEmployeeByEmployeeID(PrjPub.CurrentStudent.EmployeeID).TrainTypeID.ToString();

 
                if (ViewState["TrainTypeID"].ToString() == "0")
                {
                    Response.Write("<script>alert('请选择培训类别！');window.close();</script>");
                    return;
                }

                BindGrid();
            }
        }

        private void BindGrid()
        {
            bool isGroup = PrjPub.CurrentStudent.IsGroupLearder;
            int tech = PrjPub.CurrentStudent.TechnicianTypeID;

            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> bookList = bookBLL.GetEmployeeStudyBookInfo(Convert.ToInt32(ViewState["TrainTypeID"].ToString()), PrjPub.CurrentStudent.OrgID, PrjPub.CurrentStudent.PostID,  isGroup,tech,0);

            if (bookList.Count > 0)
            {
                foreach (RailExam.Model.Book book in bookList)
                {
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
    }
}
