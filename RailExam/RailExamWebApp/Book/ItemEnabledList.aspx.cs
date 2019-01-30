using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.Book
{
    public partial class ItemEnabledList : PageBase
    {
        private ItemBLL _itemBLL = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFalse_Click(object sender, EventArgs e)
        {
            string[] itemvalue = hfItemID.Value.Split('|');

            ItemBLL objItemBll = new ItemBLL(); 

            for(int i=0;i<itemvalue.Length;i++)
            {
                objItemBll.UpdateItemEnabled(Convert.ToInt32(itemvalue[i]), 2);
            }

            itemsGrid.DataBind();
        }

        protected void btnTrue_Click(object sender, EventArgs e)
        {
            string[] itemvalue = hfItemID.Value.Split('|');

            ItemBLL objItemBll = new ItemBLL();

            for (int i = 0; i < itemvalue.Length; i++)
            {
                objItemBll.UpdateItemEnabled(Convert.ToInt32(itemvalue[i]), 1);
            }

            itemsGrid.DataBind();
        }

        #region // DataBind Control EventHandlers

        protected void odsItems_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
        {
            _itemBLL = (ItemBLL)e.ObjectInstance;
        }

        protected void itemsGrid_DataBinding(object sender, EventArgs e)
        {
            if (_itemBLL != null)
            {
                itemsGrid.RecordCount = _itemBLL.GetCount();
            }
        }

        protected void itemsGrid_PageIndexChanged(object sender, GridPageIndexChangedEventArgs e)
        {
            itemsGrid.CurrentPageIndex = e.NewIndex;
            itemsGrid.DataBind();
        }

        #endregion

    }
}
