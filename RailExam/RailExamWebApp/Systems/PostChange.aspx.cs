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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class PostChange : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            OracleAccess db = new OracleAccess();

            string[] str = hfOldPostID.Value.Split(',');

            string sql = "select count(*) from Post where Parent_ID=" + hfNewPostID.Value;
            int orderIndex = Convert.ToInt32(db.RunSqlDataSet(sql).Tables[0].Rows[0][0]) + 1;

            for (int i = 0; i < str.Length; i++ )
            {
                orderIndex = orderIndex + i;

                sql = "select id_path from Post where Parent_ID=" + hfNewPostID.Value;
                string idPath = db.RunSqlDataSet(sql).Tables[0].Rows[0][0] + "/" + str[i];

                sql = "update Post set Parent_ID=" + hfNewPostID.Value + ", id_path='" + idPath + "',order_index='" + orderIndex + "' where post_id=" + str[i];
                db.ExecuteNonQuery(sql);
            }


            txtNewPost.Text = "";
            txtOldPost.Text = "";
            hfNewPostID.Value = "";
            hfOldPostID.Value = "";

            SessionSet.PageMessage = "ÐÞ¸Ä³É¹¦£¡";
        }
    }
}
