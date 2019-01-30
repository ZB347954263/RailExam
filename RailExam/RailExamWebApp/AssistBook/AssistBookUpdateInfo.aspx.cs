using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public partial class AssistBookUpdateInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.HasEditRight("¸¨µ¼½Ì²Ä") && PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange == 1)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                BindDdl();

                DateTime start = new DateTime(2000, 1, 1);
                DateTime end = new DateTime(2100, 12, 31);
                AssistBookUpdateBLL objBll = new AssistBookUpdateBLL();
                IList<AssistBookUpdate> objList = objBll.GetAssistBookUpdateBySelect(0, "", "", start, end, "");
                dgUpdate.DataSource = objList;
                dgUpdate.DataBind();
            }

            string strRefreshGrid = Request.Form.Get("RefreshGrid");
            if (strRefreshGrid != null & strRefreshGrid != "")
            {
                BindGrid();
            }
        }

        private void BindDdl()
        {
            ddlUpdateObject.Items.Clear();
            ddlUpdateObject.Items.Add(new ListItem("--ÇëÑ¡Ôñ--", "0"));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKINFO, PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKINFO));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKCOVER, PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKCOVER));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.ASSISTBOOKUPDATEOBJECT_DELBOOK, PrjPub.ASSISTBOOKUPDATEOBJECT_DELBOOK));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.ASSISTBOOKUPDATEOBJECT_INSERTCHAPTERINFO, PrjPub.ASSISTBOOKUPDATEOBJECT_INSERTCHAPTERINFO));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.ASSISTBOOKUPDATEOBJECT_UPDATECHAPTERINFO, PrjPub.ASSISTBOOKUPDATEOBJECT_UPDATECHAPTERINFO));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.ASSISTBOOKUPDATEOBJECT_CHAPTERCONTENT, PrjPub.ASSISTBOOKUPDATEOBJECT_CHAPTERCONTENT));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.ASSISTBOOKUPDATEOBJECT_DELCHAPTER, PrjPub.ASSISTBOOKUPDATEOBJECT_DELCHAPTER));
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "0")
            {
                ImgSelectBook.Visible = true;
                txtBookName.ReadOnly = true;
            }
            else
            {
                ImgSelectBook.Visible = false;
                txtBookName.ReadOnly = false;
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            DateTime start = new DateTime(2000, 1, 1);
            DateTime end = new DateTime(2100, 12, 31);
            if (smalldate.DateValue.ToString() != string.Empty)
            {
                start = DateTime.Parse(smalldate.DateValue.ToString());
            }
            if (bigdate.DateValue.ToString() != string.Empty)
            {
                end = DateTime.Parse(bigdate.DateValue.ToString());
            }

            string updateObject = "";
            if (ddlUpdateObject.SelectedValue == "0")
            {
                updateObject = "";
            }
            else
            {
                updateObject = ddlUpdateObject.SelectedValue;
            }

            AssistBookUpdateBLL objBll = new AssistBookUpdateBLL();
            IList<AssistBookUpdate> objList = null;
            if (ddlType.SelectedValue == "0")
            {
                objList =
                    objBll.GetAssistBookUpdateBySelect(Convert.ToInt32(hfBookID.Value), "", txtPerson.Text, start, end,
                                                 updateObject);
            }
            else
            {
                objList = objBll.GetAssistBookUpdateBySelect(0, txtBookName.Text, txtPerson.Text, start, end, updateObject);
            }

            dgUpdate.DataSource = objList;
            dgUpdate.DataBind();

            hfBookID.Value = "0";
        }
    }
}
