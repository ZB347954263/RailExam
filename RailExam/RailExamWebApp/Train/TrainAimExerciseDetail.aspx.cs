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

namespace RailExamWebApp.Train
{
    public partial class TrainAimExerciseDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CategoryID"] = Request.QueryString.Get("id");
                ViewState["TrainTypeID"] = Request.QueryString.Get("TypeID");
                BindGrid();
                BindGridSelect();
            }
            string strFlag = Request.QueryString.Get("Flag");

            if (strFlag != null & strFlag != "")
            {
                if (Panel1.Visible == false)
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Panel1.Visible = false;
                }
            }

            string str1 = Request.Form.Get("Test1");
            if (str1 != null & str1 != "")
            {
                BindGrid();
                BindGridSelect();
            }

            string strflag = Request.Form.Get("DeleteId");
            if (strflag != null & strflag != "")
            {
                DeleteData(strflag);
                BindGrid();
                BindGridSelect();
            }
        }

        private void BindGrid()
        {
            PaperBLL objPaperBll = new PaperBLL();
            IList<RailExam.Model.Paper> objPaperList = objPaperBll.GetPaperByCategoryID(Convert.ToInt32(ViewState["CategoryID"].ToString()), Convert.ToInt32(ddlType.SelectedValue), txtName.Text, txtPerson.Text);

            ArrayList objList = GetPaperList();

            if (objList.Count > 0)
            {
                foreach (RailExam.Model.Paper paper in objPaperList)
                {
                    if (objList.IndexOf(paper.PaperId) != -1)
                    {
                        paper.Flag = true;
                    }
                }
            }

            Grid1.DataSource = objPaperList;
            Grid1.DataBind();
        }

        private void BindGridSelect()
        {
            TrainTypeExerciseBLL objBll = new TrainTypeExerciseBLL();
            IList<TrainTypeExercise> objList = objBll.GetTrainTypeExerciseByTrainTypeID(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));

            Grid2.DataSource = objList;
            Grid2.DataBind();
        }

        private void DeleteData(string strPaperID)
        {
            PaperBLL PaperBLL = new PaperBLL();
            PaperBLL.DeletePaper(int.Parse(strPaperID));
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
            Panel1.Visible = false;
        }

        private ArrayList GetPaperList()
        {
            TrainTypeExerciseBLL objBll = new TrainTypeExerciseBLL();
            IList<TrainTypeExercise> objExerciseList = objBll.GetTrainTypeExerciseByTrainTypeID(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));
            ArrayList objList = new ArrayList();

            if (objExerciseList.Count > 0)
            {
                foreach (TrainTypeExercise obj in objExerciseList)
                {
                    objList.Add(obj.PaperID);
                }
            }

            return objList;
        }

        protected void btnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write("<script>window.parent.opener.form1.Refresh.value='true' ;window.parent.opener.form1.submit();window.parent.close();</script>");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            TrainTypeExerciseBLL objBll = new TrainTypeExerciseBLL();
            ArrayList objList = GetPaperList();

            GridItemCollection activeItems = Grid1.GetCheckedItems(Grid1.Levels[0].Columns[0]);
            foreach (GridItem activeItem in activeItems)
            {
                if (objList.IndexOf(activeItem[1]) == -1)
                {
                    TrainTypeExercise obj = new TrainTypeExercise();
                    obj.TrainTypeID = Convert.ToInt32(ViewState["TrainTypeID"].ToString());
                    obj.PaperID = Convert.ToInt32(activeItem[1]);

                    objBll.AddTrainTypeExercise(obj);
                }
            }
            BindGrid();
            BindGridSelect();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            TrainTypeExerciseBLL objBll = new TrainTypeExerciseBLL();

            GridItemCollection activeItems = Grid2.GetCheckedItems(Grid2.Levels[0].Columns[0]);
            foreach (GridItem activeItem in activeItems)
            {
                TrainTypeExercise obj = new TrainTypeExercise();
                obj.TrainTypeID = Convert.ToInt32(ViewState["TrainTypeID"].ToString());
                obj.PaperID = Convert.ToInt32(activeItem[1]);

                objBll.DelTrainTypeExercise(obj.TrainTypeID, obj.PaperID);
            }
            BindGrid();
            BindGridSelect();
        }
    }
}
