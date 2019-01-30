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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Item
{
	public partial class ItemCount : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				hfSelect.Value = GetSql();
				grdBook.DataBind();
			}
		}

		private string GetSql()
		{
			if (hfPostID.Value == "")
			{
                hfPostID.Value = "-1";
            }

		    string str = string.Empty;
		    int railSystemId = PrjPub.GetRailSystemId();
            if(railSystemId != 0)
            {
                str =
                    " and m.book_id in (select distinct book_id from Book_Range_Org where Org_ID in (select Org_ID from Org where Rail_System_ID=" +
                    railSystemId + " and level_Num=2))";
            }
            else
            {
                if(PrjPub.CurrentLoginUser.SuitRange == 0)
                {
                    str = " and m.book_id in (select distinct book_id from Book_Range_Org where Org_ID="+ PrjPub.CurrentLoginUser.StationOrgID +")";
                }
            }

			string sql =
				string.Format(
					@"
                    
                    select 
					   m.book_id,to_char(k.book_name) book_name,to_char(g.short_name) short_name,
                       sum(case when m.type_id>0 and m.type_id<5  then 1 else 0 end) as xiaoji,
					   sum(case m.type_id when 1 then 1 else 0 end)  as dan,
					   sum(case m.type_id when 2 then 1 else 0 end) as duo,
					   sum(case m.type_id when 3 then 1 else 0 end) as pan,
					   sum(case m.type_id when 4 then 1 else 0 end) as wan,
					   sum(case m.type_id when 5 then 1 else 0 end) as wanz
					from item m
					left join book k on m.book_id=k.book_id
					left join org g on g.org_id=k.Publish_Org 
					where m.book_id in (select distinct book_id from book_range_post where 
                    post_id in (select post_id from post where parent_id  in ({0}) or post_id in ({0}))) {1}
					group by m.book_id,k.book_name,g.short_name
                    union
					select 0,'ºÏ¼Æ','',
					  sum(case when m.type_id>0 and m.type_id<5  then 1 else 0 end) as xiaoji,
								   sum(case m.type_id when 1 then 1 else 0 end)  as dan,
								   sum(case m.type_id when 2 then 1 else 0 end) as duo,
								   sum(case m.type_id when 3 then 1 else 0 end) as pan,
								   sum(case m.type_id when 4 then 1 else 0 end) as wan,
								   sum(case m.type_id when 5 then 1 else 0 end) as wanz
       				from item m
								left join book k on m.book_id=k.book_id
								left join org g on g.org_id=k.Publish_Org 
								where m.book_id in (select distinct book_id from book_range_post where 
					post_id in (select post_id from post where parent_id  in ({0}) or post_id in ({0}))) {1}
					group by  g.parent_id 
        
                  ",
					hfPostID.Value,str);
				return sql;
			 
		}

		protected void btnQuery_Click(object sender, EventArgs e)
		{
			 
			hfSelect.Value = GetSql();
			grdBook.DataBind();
			txtPost.Text = hfPost.Value;
		}

		protected void grdBook_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if(e.Row.RowType==DataControlRowType.DataRow)
			{
				if (grdBook.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
					e.Row.Visible = false;
				else
				    e.Row.Attributes.Add("onclick", "selectArow(this);");

			}
		}

		protected void ObjectDataSourceBook_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			DataTable dt = e.ReturnValue as DataTable;
			if(dt.Rows.Count==0)
			{
				DataRow r = dt.NewRow();
				r["book_id"] = "-1";
				dt.Rows.Add(r);
			}
		}
	 
		 
	}
}
