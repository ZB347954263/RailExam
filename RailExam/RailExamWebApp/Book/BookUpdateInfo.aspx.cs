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

namespace RailExamWebApp.Book
{
    public partial class BookUpdateInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
            	hfIsWuhan.Value = PrjPub.IsWuhan().ToString();
				if (PrjPub.HasEditRight("教材管理") && PrjPub.IsServerCenter)//&& PrjPub.CurrentLoginUser.SuitRange == 1
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.HasDeleteRight("教材管理") && PrjPub.IsServerCenter)
                {
                    HasDeleteRight.Value = "True";
                }
                else
                {
                    HasDeleteRight.Value = "False";
                }

                BindDdl();

                DateTime start = new DateTime(2000, 1, 1);
                DateTime end = new DateTime(2100, 12, 31);
                BookUpdateBLL  objBll = new BookUpdateBLL();
                IList<BookUpdate> objList = objBll.GetBookUpdateBySelect(0, "", "", start, end, "");
                dgUpdate.DataSource = objList;
                dgUpdate.DataBind();
            }
            string strRefreshGrid = Request.Form.Get("RefreshGrid");
            if (strRefreshGrid != null & strRefreshGrid != "")
            {
                BindGrid();
            }

            string strID = Request.Form.Get("DeleteID");
            if (strID != null & strID != "")
            {
                BookUpdateBLL objBll = new BookUpdateBLL();
                objBll.DeleteBookUpdate(Convert.ToInt32(strID));
                BindGrid();
            }
        }

        private void BindDdl()
        {
            ddlUpdateObject.Items.Clear();
            ddlUpdateObject.Items.Add(new ListItem("--请选择--","0"));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.BOOKUPDATEOBJECT_BOOKINFO,PrjPub.BOOKUPDATEOBJECT_BOOKINFO));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.BOOKUPDATEOBJECT_BOOKCOVER,PrjPub.BOOKUPDATEOBJECT_BOOKCOVER));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.BOOKUPDATEOBJECT_DELBOOK,PrjPub.BOOKUPDATEOBJECT_DELBOOK));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.BOOKUPDATEOBJECT_INSERTCHAPTERINFO,PrjPub.BOOKUPDATEOBJECT_INSERTCHAPTERINFO));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.BOOKUPDATEOBJECT_UPDATECHAPTERINFO,PrjPub.BOOKUPDATEOBJECT_UPDATECHAPTERINFO));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.BOOKUPDATEOBJECT_CHAPTERCONTENT,PrjPub.BOOKUPDATEOBJECT_CHAPTERCONTENT));
            ddlUpdateObject.Items.Add(new ListItem(PrjPub.BOOKUPDATEOBJECT_DELCHAPTER,PrjPub.BOOKUPDATEOBJECT_DELCHAPTER));
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlType.SelectedValue == "0")
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

            BookUpdateBLL objBll = new BookUpdateBLL();
            IList<BookUpdate> objList = null;
            if (ddlType.SelectedValue == "0")
            {
                objList =
                    objBll.GetBookUpdateBySelect(Convert.ToInt32(hfBookID.Value), "", txtPerson.Text, start, end,
                                                 updateObject);
            }
            else
            {
                objList = objBll.GetBookUpdateBySelect(0, txtBookName.Text, txtPerson.Text, start, end, updateObject);
            }

            dgUpdate.DataSource = objList;
            dgUpdate.DataBind();

            hfBookID.Value = "0";
        }
    }
}
