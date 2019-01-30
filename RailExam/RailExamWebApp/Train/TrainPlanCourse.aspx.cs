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
    public partial class TrainPlanCourse : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PlanID"] = Request.QueryString.Get("PlanID");
                ViewState["PlanName"] = Request.QueryString.Get("PlanName");
                ViewState["QuerySql"] = "";
                BindGridQuery();
                btnSelectType.Visible = false;
                //TrainTypeBLL objTrainTypeBll = new TrainTypeBLL();
                //IList<TrainType> objList = objTrainTypeBll.GetTrainStandardTypeInfo(Convert.ToInt32(ddlPost.SelectedValue), 1);
            }

            string strPostID = Request.Form.Get("txtPostID");
            if (strPostID != null && strPostID != "")
            {
                BindGridQuery();
                Grid2.DataBind();
                txtPost.Text = Request.Form.Get("txtPostName");
                ViewState["QueryPostID"] = strPostID;
                btnSelectType.Visible = true;
                btnSelectType.Attributes.Add("onclick", "SelectType('" + strPostID + "','" + txtPost.Text + "')");
            }

            string strTypeID = Request.Form.Get("txtTypeID");
            if (strTypeID != null && strTypeID != "")
            {
                BindGridQuery();
                Grid2.DataBind();
                txtType.Text = Request.Form.Get("txtTypeName");
                ViewState["QueryTypeID"] = strTypeID;
            }
        }

        private void BindGrid1()
        {
            TrainPlanCourseBLL objBll = new TrainPlanCourseBLL();
            IList<RailExam.Model.TrainCourse> objTrainCourse = objBll.GetTrainCommandCourseInfoByPlanID(Convert.ToInt32(ViewState["PlanID"].ToString()));

            Grid1.DataSource = objTrainCourse;
            Grid1.DataBind();
        }

        private void BindGridQuery()
        {
            TrainPlanCourseBLL objBll = new TrainPlanCourseBLL();
            IList<RailExam.Model.TrainCourse> objTrainCourse = objBll.GetTrainCourseQueryInfo(ViewState["QuerySql"].ToString(), Convert.ToInt32(ViewState["PlanID"].ToString()));

            Grid1.DataSource = objTrainCourse;
            Grid1.DataBind();
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainPlanEdit.aspx?id=" + ViewState["PlanID"].ToString() + "&name=" + ViewState["PlanName"].ToString());
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainPlanEmployee.aspx?PlanID=" + ViewState["PlanID"].ToString() + "&PlanName=" + ViewState["PlanName"].ToString());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true' ;window.opener.form1.submit();window.close();</script>");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            TrainPlanCourseBLL objBll = new TrainPlanCourseBLL();
            ArrayList objList = objBll.GetCourseList(Convert.ToInt32(ViewState["PlanID"].ToString()));

            GridItemCollection activeItems = Grid1.GetCheckedItems(Grid1.Levels[0].Columns[0]);
            foreach (GridItem activeItem in activeItems)
            {
                if (objList.IndexOf(activeItem[1]) == -1)
                {
                    RailExam.Model.TrainPlanCourse obj = new RailExam.Model.TrainPlanCourse();
                    obj.TrainPlanID = Convert.ToInt32(ViewState["PlanID"].ToString());
                    obj.TrainCourseID = Convert.ToInt32(activeItem[1]);

                    objBll.AddTrainPlanCourse(obj);
                }
            }
            Grid2.DataBind();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            TrainPlanCourseBLL objBll = new TrainPlanCourseBLL();
            GridItemCollection activeItems = Grid2.GetCheckedItems(Grid2.Levels[0].Columns[0]);
            foreach (GridItem activeItem in activeItems)
            {
                RailExam.Model.TrainPlanCourse obj = new RailExam.Model.TrainPlanCourse();
                obj.TrainPlanID = Convert.ToInt32(ViewState["PlanID"].ToString());
                obj.TrainCourseID = Convert.ToInt32(activeItem[1]);

                objBll.DeleteTrainPlanCourse(obj.TrainPlanID, obj.TrainCourseID);
            }
            BindGridQuery();
            Grid2.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string strSql = "where 1=1 ";

            if (txtPost.Text != string.Empty)
            {
                strSql += " and f.Post_ID='" + ViewState["QueryPostID"].ToString() + "'";
            }

            if (txtType.Text != string.Empty)
            {
                strSql += " and g.train_type_id='" + ViewState["QueryTypeID"].ToString() + "'";
            }

            if (txtName.Text != string.Empty)
            {
                strSql += " and a.Course_Name like '%" + txtName.Text + "%'";
            }

            if (chk.Checked)
            {
                strSql += " and e.train_standard_id is null ";
            }

            ViewState["QuerySql"] = strSql;
            BindGridQuery();
        }
    }
}