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

namespace RailExamWebApp.AssistBook
{
    public partial class InformationChapterDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(!string.IsNullOrEmpty(Request.QueryString.Get("id")))
                {
                    OracleAccess db = new OracleAccess();
                    string strSql = "select * from Information_Chapter where Chapter_ID=" + Request.QueryString.Get("id");

                    DataSet ds = db.RunSqlDataSet(strSql);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        txtName.Text = dr["Chapter_Name"].ToString();
                        DescriptionTextBox.Text = dr["Description"] == DBNull.Value ? "" : dr["Description"].ToString();
                        txtMemo.Text = dr["Memo"] == DBNull.Value ? "" : dr["Memo"].ToString();
                        lblLastPerson.Text = dr["LAST_UPDATE_PERSON"] == DBNull.Value
                                                 ? ""
                                                 : dr["LAST_UPDATE_PERSON"].ToString();
                        lblLastDate.Text = dr["LAST_UPDATE_DATE"] == DBNull.Value
                                               ? ""
                                               : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]).ToString("yyyy-MM-dd");
                    } 
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if(DescriptionTextBox.Text.Length>200)
            {
                SessionSet.PageMessage = "描述不能超过200个字符！";
                return;
            }

            if(txtMemo.Text.Length>50)
            {
                SessionSet.PageMessage = "描述不能超过50个字符！";
                return;
            }

            OracleAccess db = new OracleAccess();
            string strInformationId = Request.QueryString.Get("BookID");

            string strSql;
            if (!string.IsNullOrEmpty(Request.QueryString.Get("id")))
            {
                strSql = @"update Information_Chapter set 
                        Chapter_Name='" + txtName.Text.Trim() + @"',
                        Description='" + DescriptionTextBox.Text + @"',
                        Memo ='" + txtMemo.Text + @"',
                        LAST_UPDATE_PERSON = '" + PrjPub.CurrentLoginUser.EmployeeName + @"',
                        LAST_UPDATE_DATE=sysdate where Chapter_ID=" + Request.QueryString.Get("id");

                db.ExecuteNonQuery(strSql);

                InformationShow show = new InformationShow();
                show.GetIndex(strInformationId);

                Response.Write("<script>window.opener.tvBookChapterChangeCallBack.callback(" + Request.QueryString.Get("id") + ", 'edit');window.close();</script>");

            }
            else
            {
                string strparent = Request.QueryString.Get("ParentID");

                int levelNum=0, orderindex;
                string idpath=string.Empty;

                strSql = "select Information_Chapter_Seq.nextval from dual";
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                string strKey = dr[0].ToString();
                
                if(strparent == "0")
                {
                    idpath = "/" + strKey;
                    levelNum = 1;

                    strSql = "select * from Information_Chapter where Parent_ID=0 and Information_ID=" 
                        + strInformationId+" order by Order_Index desc";
                    DataSet dsParent = db.RunSqlDataSet(strSql);

                    if(dsParent.Tables[0].Rows.Count > 0)
                    {
                        orderindex = Convert.ToInt32(dsParent.Tables[0].Rows[0]["Order_Index"]) + 1;
                    }
                    else
                    {
                        orderindex = 1;
                    }
                }
                else
                {
                    strSql = "select * from Information_Chapter where Chapter_ID=" + strparent;
                    DataSet dsParent = db.RunSqlDataSet(strSql);
                    if (dsParent.Tables[0].Rows.Count > 0)
                    {
                        levelNum = Convert.ToInt32(dsParent.Tables[0].Rows[0]["Level_Num"]) + 1;

                        idpath = dsParent.Tables[0].Rows[0]["ID_Path"] + "/" + strKey;
                    }

                    strSql = "select * from Information_Chapter where Parent_ID=" + strparent +
                             " order by order_Index desc";
                    dsParent = db.RunSqlDataSet(strSql);

                    if (dsParent.Tables[0].Rows.Count > 0)
                    {
                        orderindex = Convert.ToInt32(dsParent.Tables[0].Rows[0]["Order_Index"]) + 1;
                    }
                    else
                    {
                        orderindex = 1;
                    }
                }

                strSql = @"insert into Information_Chapter(Information_ID,Chapter_ID,Parent_ID,ID_Path,
                        Level_NUM,Order_Index,Chapter_Name,Description,Memo, LAST_UPDATE_PERSON,LAST_UPDATE_DATE) 
                        values("+strInformationId+@","+strKey+@","+strparent+@",'"+idpath+@"',
                        "+levelNum+@","+orderindex+@",
                        '" + txtName.Text.Trim() + @"',
                        '" + DescriptionTextBox.Text + @"',
                        '" + txtMemo.Text + @"',
                        '" + PrjPub.CurrentLoginUser.EmployeeName + @"',
                        sysdate)";
                
                db.ExecuteNonQuery(strSql);

                InformationShow show = new InformationShow();
                show.GetIndex(strInformationId);

                Response.Write("<script>window.opener.tvBookChapterChangeCallBack.callback(" + strparent + ", 'Insert');window.close();</script>");
            }

        }
    }
}
