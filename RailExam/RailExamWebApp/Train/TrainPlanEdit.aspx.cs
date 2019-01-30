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

namespace RailExamWebApp.Train
{
    public partial class TrainPlanEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strID = Request.QueryString.Get("id");
                string strName = Request.QueryString.Get("name");

                BindStatus(strID, strName);

                if (strID != null && strID != "")
                {//Update
                    GetPlanInfo(strID);
                }
                else
                {//Add
                    txtBegin.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    txtEnd.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
            }
        }

        private void BindStatus(string strID, string strName)
        {
            TrainPlanStatusBLL objTrainPlanStatusBll = new TrainPlanStatusBLL();
            IList<TrainPlanStatus> objList = objTrainPlanStatusBll.GetAllTrainPlanStatusInfo();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--请选择--";
            ddlStatus.Items.Add(item);

            if (objList.Count > 0)
            {
                foreach (TrainPlanStatus obj in objList)
                {
                    ListItem items = new ListItem();
                    items.Value = obj.TrainPlanStatusID.ToString();
                    items.Text = obj.StatusName;
                    ddlStatus.Items.Add(items);
                }
            }
        }

        private void GetPlanInfo(string strID)
        {
            TrainPlanBLL objTrainPlanBll = new TrainPlanBLL();
            RailExam.Model.TrainPlan objTrainPlan = objTrainPlanBll.GetTrainPlanInfo(Convert.ToInt32(strID));

            txtPlanName.Text = objTrainPlan.TrainName;
            txtContent.Text = objTrainPlan.TrainContent;
            txtBegin.Text = objTrainPlan.BeginDate.ToString("yyyy-MM-dd");
            txtEnd.Text = objTrainPlan.EndDate.ToString("yyy-MM-dd");
            chkExam.Checked = objTrainPlan.HasExam;
            txtExam.Text = objTrainPlan.ExamForm;
            ddlStatus.SelectedValue = objTrainPlan.StatusID.ToString();
            txtMemo.Text = objTrainPlan.Memo;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (txtContent.Text.Length > 200)
            {
                Response.Write("<script>alert('内容简介不能超过200个汉字！');</script>");
                return;
            }

            if (txtMemo.Text.Length > 50)
            {
                Response.Write("<script>alert('备注不能超过50个汉字！');</script>");
                return;
            }

            TrainPlanBLL objTrainPlanBll = new TrainPlanBLL();
            RailExam.Model.TrainPlan objTrainPlan = new RailExam.Model.TrainPlan();

            string strID = Request.QueryString.Get("id");
            if (strID != null && strID != "")
            {
                objTrainPlan.TrainPlanID = Convert.ToInt32(strID);
                objTrainPlan.TrainName = txtPlanName.Text;
                objTrainPlan.TrainContent = txtContent.Text;
                objTrainPlan.BeginDate = DateTime.Parse(txtBegin.Text);
                objTrainPlan.EndDate = DateTime.Parse(txtEnd.Text);
                objTrainPlan.HasExam = chkExam.Checked;
                objTrainPlan.ExamForm = txtExam.Text;
                objTrainPlan.StatusID = Convert.ToInt32(ddlStatus.SelectedValue);
                objTrainPlan.Memo = txtMemo.Text;

                objTrainPlanBll.UpdateTrainPlan(objTrainPlan);
            }
            else
            {
                objTrainPlan.TrainName = txtPlanName.Text;
                objTrainPlan.TrainContent = txtContent.Text;
                objTrainPlan.BeginDate = DateTime.Parse(txtBegin.Text);
                objTrainPlan.EndDate = DateTime.Parse(txtEnd.Text);
                objTrainPlan.HasExam = chkExam.Checked;
                objTrainPlan.ExamForm = txtExam.Text;
                objTrainPlan.StatusID = 1;
                objTrainPlan.Memo = txtMemo.Text;

                objTrainPlanBll.AddTrainPlan(objTrainPlan);
            }

            Response.Redirect("TrainPlanCourse.aspx?PlanID=" + objTrainPlan.TrainPlanID + "&PlanName=" + objTrainPlan.TrainName);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true' ;window.opener.form1.submit();window.close();</script>");
        }
    }
}