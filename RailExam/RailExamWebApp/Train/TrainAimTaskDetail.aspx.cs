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
    public partial class TrainAimTaskDetail : PageBase
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
            PaperBLL paperBLL = new PaperBLL();
            IList<RailExam.Model.Paper> paperList = paperBLL.GetPaperByCategoryID(Convert.ToInt32(ViewState["CategoryID"].ToString()), Convert.ToInt32(ddlType.SelectedValue), txtName.Text, txtPerson.Text);

            ArrayList objList = GetPaperList();

            if (objList.Count > 0)
            {
                foreach (RailExam.Model.Paper paper in paperList)
                {
                    if (objList.IndexOf(paper.PaperId) != -1)
                    {
                        paper.Flag = true;
                    }
                }
            }

            Grid1.DataSource = paperList;
            Grid1.DataBind();
        }

        private void BindGridSelect()
        {
            TrainTypeTaskBLL objBll = new TrainTypeTaskBLL();
            IList<TrainTypeTask> objList = objBll.GetTrainTypeTaskByTrainTypeID(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));

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
            TrainTypeTaskBLL objBll = new TrainTypeTaskBLL();
            IList<TrainTypeTask> objTaskList = objBll.GetTrainTypeTaskByTrainTypeID(Convert.ToInt32(ViewState["TrainTypeID"].ToString()));
            ArrayList objList = new ArrayList();

            if (objTaskList.Count > 0)
            {
                foreach (TrainTypeTask obj in objTaskList)
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
            TrainTypeTaskBLL objBll = new TrainTypeTaskBLL();
            ArrayList objList = GetPaperList();

            GridItemCollection activeItems = Grid1.GetCheckedItems(Grid1.Levels[0].Columns[0]);
            foreach (GridItem activeItem in activeItems)
            {
                if (objList.IndexOf(activeItem[1]) == -1)
                {
                    TrainTypeTask obj = new TrainTypeTask();
                    obj.TrainTypeID = Convert.ToInt32(ViewState["TrainTypeID"].ToString());
                    obj.PaperID = Convert.ToInt32(activeItem[1]);

                    objBll.AddTrainTypeTask(obj);
                }
            }
            BindGrid();
            BindGridSelect();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            TrainTypeTaskBLL objBll = new TrainTypeTaskBLL();

            GridItemCollection activeItems = Grid2.GetCheckedItems(Grid2.Levels[0].Columns[0]);
            foreach (GridItem activeItem in activeItems)
            {
                TrainTypeTask obj = new TrainTypeTask();
                obj.TrainTypeID = Convert.ToInt32(ViewState["TrainTypeID"].ToString());
                obj.PaperID = Convert.ToInt32(activeItem[1]);

                objBll.DelTrainTypeTask(obj.TrainTypeID, obj.PaperID);
            }
            BindGrid();
            BindGridSelect();
        }
    }
}