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

namespace RailExamWebApp.AssistBook
{
    public partial class InformationChapterInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strid = Request.QueryString.Get("id");
                string informationid = Request.QueryString.Get("BookID");

                OracleAccess db = new OracleAccess();

                if(strid=="0")
                {
                    string strSql = "select * from Information where Information_ID=" + informationid;

                    DataSet ds = db.RunSqlDataSet(strSql);

                    if(ds.Tables[0].Rows.Count>0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        TypeNameLabel.Text = dr["Information_Name"].ToString();
                        lblLastPerson.Text = dr["LAST_UPDATE_PERSON"] == DBNull.Value
                                                 ? ""
                                                 : dr["LAST_UPDATE_PERSON"].ToString();
                        lblLastDate.Text = dr["LAST_UPDATE_DATE"] == DBNull.Value
                                               ? ""
                                               : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]).ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    string strSql = "select * from Information_Chapter where Chapter_ID=" + strid;

                    DataSet ds = db.RunSqlDataSet(strSql);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        TypeNameLabel.Text = dr["Chapter_Name"].ToString();
                        hUrl.Text = dr["Url"] == DBNull.Value ? "" : dr["Url"].ToString();
                        hUrl.NavigateUrl = dr["Url"] == DBNull.Value ? "" : dr["Url"].ToString();
                        DescriptionTextBox.Text = dr["Description"] == DBNull.Value ? "" : dr["Description"].ToString();
                        MemoTextBox11.Text = dr["Memo"] == DBNull.Value ? "" : dr["Memo"].ToString();
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
    }
}
