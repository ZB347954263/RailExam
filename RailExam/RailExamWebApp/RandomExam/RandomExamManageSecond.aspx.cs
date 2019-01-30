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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamManageSecond : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["mode"] = Request.QueryString.Get("mode");
                ViewState["startmode"] = Request.QueryString.Get("startmode");

                hfMode.Value = ViewState["mode"].ToString();

                if (ViewState["mode"].ToString() == "ReadOnly")
                {
                    btnInput.Enabled = false;
                }

                string strId = Request.QueryString.Get("id");
                if (!string.IsNullOrEmpty(strId))
                {
                    HfRandomExamid.Value = strId;
                    RandomExamBLL randomExamBLL = new RandomExamBLL();
                    RailExam.Model.RandomExam RandomExam = randomExamBLL.GetExam(int.Parse(strId));
                    if (RandomExam != null)
                    {
                        txtPaperName.Text = RandomExam.ExamName;
                    }


                    ItemTypeBLL objTypeBll = new ItemTypeBLL();
                    IList<ItemType> objTypeList = objTypeBll.GetItemTypes();
                    foreach (ItemType objType in objTypeList)
                    {
                        if (RandomExam.IsComputerExam)
                        {
                            if (objType.ItemTypeId > PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {
                                continue;
                            }
                        }

                        ListItem item = new ListItem();
                        item.Text = objType.TypeName;
                        item.Value = objType.ItemTypeId.ToString();
                        lbType.Items.Add(item);
                    }

                    Grid1.DataBind();
                }
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    int nID = Int32.Parse(strDeleteID);
                    RandomExamSubjectBLL RandomExamSubjectBLL = new RandomExamSubjectBLL();
                    RandomExamSubjectBLL.DeleteRandomExamSubject(nID);
                    Grid1.DataBind();
                }
            }
        }

        protected void btnLast_Click(object sender, ImageClickEventArgs e)
        {
            string strId = Request.QueryString.Get("id");
            string strStartMode = ViewState["startmode"].ToString();
            string strFlag = "";

            if (ViewState["mode"].ToString() == "Insert")
            {
                strFlag = "Edit";
            }
            else
            {
                strFlag = ViewState["mode"].ToString();
            }

            Response.Redirect("RandomExamManageFirst.aspx?startmode="+ strStartMode +"&mode=" + strFlag + "&id=" + strId);

        }

        protected void btnSaveAndNext_Click(object sender, ImageClickEventArgs e)
        {
            string strId = Request.QueryString.Get("id");
            string strMode = ViewState["mode"].ToString();
            string strStartMode = ViewState["startmode"].ToString();

            if (strMode != "ReadOnly")
            {
                if (Pub.HasPaper(Convert.ToInt32(strId)))
                {
                    Response.Write("<script>alert('该考试已生成试卷，不能被编辑！');window.close();</script>");
                    return;
                }

                if (Grid1.Rows.Count == 0)
                {
                    SessionSet.PageMessage = "请选择大题！";
                    return;
                }

                decimal  totalScore = 0;

                IList<RandomExamSubject> paperStrategySubjects = new List<RandomExamSubject>();

                int zeroCount = 0;
                int zeroItemCount = 0;
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string strPaperStrategySubjectId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperStrategySubjectId")).Value;
                    string strItemTypeId = ((HiddenField)Grid1.Rows[i].FindControl("hfItemTypeId")).Value;
                    string strSubjectName = ((TextBox)Grid1.Rows[i].FindControl("txtSubjectName")).Text;
                    string strUnitScore = ((TextBox)Grid1.Rows[i].FindControl("txtUnitScore")).Text;
                    string strItemCount = ((TextBox)Grid1.Rows[i].FindControl("txtItemCount")).Text;

                    if (strUnitScore == "")
                    {
                        strUnitScore = "0";
                    }

                    if (strItemCount == "")
                    {
                        strItemCount = "0";
                    }


                    totalScore += Convert.ToDecimal(strUnitScore);

                    RandomExamSubject paperStrategySubject = new RandomExamSubject();

                    paperStrategySubject.RandomExamSubjectId = int.Parse(strPaperStrategySubjectId);
                    paperStrategySubject.RandomExamId = int.Parse(strId);
                    paperStrategySubject.ItemCount = int.Parse(strItemCount);
                    paperStrategySubject.ItemTypeId = int.Parse(strItemTypeId);
                    paperStrategySubject.OrderIndex = 0;
                    paperStrategySubject.Remark = "";
                    paperStrategySubject.SubjectName = strSubjectName;
                    paperStrategySubject.UnitScore = Convert.ToDecimal(strUnitScore);
                    paperStrategySubject.TotalScore = Convert.ToDecimal(strUnitScore);
                    paperStrategySubject.Memo = "";

                    if(strUnitScore == "0")
                    {
                        zeroCount++;
                    }

                    if (strItemCount == "0")
                    {
                        zeroItemCount++;
                    }

                    paperStrategySubjects.Add(paperStrategySubject);
                }

                if(zeroCount > 0)
                {
                    SessionSet.PageMessage = "不能设置每题为0分的大题，请输入每题分数！";
                    return;
                }

                if (zeroItemCount > 0)
                {
                    SessionSet.PageMessage = "不能设置题目数为0的大题，请输入大题题数！";
                    return;
                }

                RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();
                paperStrategySubjectBLL.UpdateRandomExamSubject(paperStrategySubjects);
            }

            Response.Redirect("RandomExamManageThird.aspx?startmode="+strStartMode+"&mode=" + strMode + "&id=" + strId);

        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");       
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            if (lbType.SelectedIndex < 0)
            {
                SessionSet.PageMessage = "请先选题型！";
                return;
            }

            string strId = Request.QueryString.Get("id");
            RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();

            decimal  totalScore = 0;
            IList<RandomExamSubject> paperStrategySubjects = new List<RandomExamSubject>();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string strPaperStrategySubjectId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperStrategySubjectId")).Value;
                string strItemTypeId = ((HiddenField)Grid1.Rows[i].FindControl("hfItemTypeId")).Value;
                string strSubjectName = ((TextBox)Grid1.Rows[i].FindControl("txtSubjectName")).Text;
                string strUnitScore = ((TextBox)Grid1.Rows[i].FindControl("txtUnitScore")).Text;
                string strItemCount = ((TextBox)Grid1.Rows[i].FindControl("txtItemCount")).Text;

                if (strUnitScore == "")
                {
                    strUnitScore = "0";
                }

                if (strItemCount == "")
                {
                    strItemCount = "0";
                }

                totalScore += Convert.ToDecimal(strUnitScore);

                RandomExamSubject paperStrategySubject = new RandomExamSubject();
                paperStrategySubject.RandomExamSubjectId = int.Parse(strPaperStrategySubjectId);
                paperStrategySubject.RandomExamId = int.Parse(strId);
                paperStrategySubject.ItemCount = int.Parse(strItemCount);
                paperStrategySubject.ItemTypeId = int.Parse(strItemTypeId);
                paperStrategySubject.OrderIndex = 0;
                paperStrategySubject.Remark = "";
                paperStrategySubject.SubjectName = strSubjectName;
                paperStrategySubject.UnitScore = Convert.ToDecimal(strUnitScore);
                paperStrategySubject.TotalScore = Convert.ToDecimal(strUnitScore);
                paperStrategySubject.Memo = "";

                paperStrategySubjects.Add(paperStrategySubject);
            }


            if (paperStrategySubjects.Count>0)
            {
                 paperStrategySubjectBLL.UpdateRandomExamSubject(paperStrategySubjects);
            }         

           
            RandomExamSubject RandomStrategySubject = new RandomExamSubject();

            RandomStrategySubject.RandomExamId = int.Parse(strId);
            RandomStrategySubject.RandomExamSubjectId = Grid1.Rows.Count + 1;
            RandomStrategySubject.ItemTypeId = int.Parse(lbType.SelectedValue);
            RandomStrategySubject.TypeName = lbType.SelectedItem.Text;
            RandomStrategySubject.SubjectName = lbType.SelectedItem.Text;
            RandomStrategySubject.UnitScore = 0;
            RandomStrategySubject.TotalScore = 0;
            RandomStrategySubject.Memo = "";
            RandomStrategySubject.ItemCount = 10;
            RandomStrategySubject.OrderIndex = 0;
            RandomStrategySubject.Remark = "";

            paperStrategySubjectBLL.AddRandomExamSubject(RandomStrategySubject);

            Grid1.DataBind();
        }

        protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["mode"].ToString() == "ReadOnly")
                {
                    ((TextBox)e.Row.FindControl("txtSubjectName")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtUnitScore")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtItemCount")).Enabled = false;
                }

                ((Label)e.Row.FindControl("lblID")).Text = (e.Row.RowIndex + 1).ToString();
            }
        }

    }
}
