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
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Main
{
    public partial class Main : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !CallBack.IsCallback)
            {
                NavBarItem parentItem = null;

				if (PrjPub.CurrentLoginUser == null)
				{
					CallBack.RefreshInterval = 0;
					return;
				}

            	hfEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();

                foreach (FunctionRight functionRight in PrjPub.CurrentLoginUser.FunctionRights)
                {
					if(PrjPub.IsWuhan())
					{
						if(functionRight.Function.FunctionID ==  "0406")
						{
							continue;
						}
					}
                    if (functionRight.Right == 0)
                    {
                        continue;
                    }

                    NavBarItem item = new NavBarItem();

                    item.Text = functionRight.Function.MenuName;
                    item.Expanded = functionRight.Function.IsDefault;

                    if (functionRight.Function.FunctionID.Length == 2)
                    {
                        item.SelectedLookId = "TopItemLook";
                        item.SubGroupCssClass = "Level2Group";
                        item.DefaultSubItemLookId = "Level2ItemLook";

                        SystemMenu.Items.Add(item);
                        parentItem = item;
                    }
                    else
                    {
                        if (parentItem != null)
                        {
                        	item.ClientSideCommand = "goToUrl('" + functionRight.Function.PageUrl + "');";
                            //item.NavigateUrl = functionRight.Function.PageUrl;
                            //item.Target = "ContentFrame";

                            parentItem.Items.Add(item);
                        }
                    }
                }
            }
        }

		protected  void CallBack_OnCallback(object sender,  CallBackEventArgs e)
		{
			if (PrjPub.CurrentLoginUser != null)
			{
				LoginUser loginUser = PrjPub.CurrentLoginUser;
			}
		}
    }
}