using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public partial class InformationDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {

                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                string strInformationId = Request.QueryString.Get("id");
                ViewState["InformationID"] = strInformationId;

                hfMode.Value = Request.QueryString.Get("mode");

                if (!string.IsNullOrEmpty(strInformationId))
                {
                    if (hfMode.Value == "ReadOnly")
                    {
                        SaveButton.Visible = false;
                        CancelButton.Visible = true;
                        SaveNextButton.Visible = false;
                        SaveExitButton.Visible = false;
                    }
                    else if (hfMode.Value == "Edit")
                    {
                        btnChapter.Visible = true;
                        SaveButton.Visible = true;
                        CancelButton.Visible = false;
                        SaveExitButton.Visible = false;
                        SaveNextButton.Visible = false;
                    }

                    FillPage(int.Parse(strInformationId));
                }
                else
                {
                    SaveButton.Visible = false;
                    SaveNextButton.Visible = true;
                    SaveExitButton.Visible = true;
                    CancelButton.Visible = false;
                    datePublishDate.DateValue = DateTime.Today.ToString("yyyy-MM-dd");

                    string strKnowledgeID = Request.QueryString.Get("knowledgeId");
                    if (!string.IsNullOrEmpty(strKnowledgeID))
                    {
                        hfKnowledgeID.Value = strKnowledgeID;
                        ImgSelectKnowledge.Visible =  false;
                    }

                    string strTrainTypeID = Request.QueryString.Get("TrainTypeId");
                    if (!string.IsNullOrEmpty(strTrainTypeID))
                    {
                        hfTrainTypeID.Value = strTrainTypeID;
                        ImgSelectTrainType.Visible = false;
                    }

                    txtCreatePerson.Text = PrjPub.CurrentLoginUser.EmployeeName;
                    hfEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();
                    OrganizationBLL org = new OrganizationBLL();
                    txtOrg.Text = org.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID).ShortName;
                }
            }

            OracleAccess oa = new OracleAccess();

            if (!string.IsNullOrEmpty(hfKnowledgeID.Value))
            {
                string strSql = "select * from Information_System where Information_System_ID=" + hfKnowledgeID.Value;
                DataSet ds = oa.RunSqlDataSet(strSql);

                if(ds.Tables[0].Rows.Count > 0)
                {
                    txtKnowledgeName.Text = ds.Tables[0].Rows[0]["Information_System_Name"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(hfTrainTypeID.Value))
            {
                string strSql = "select * from Information_Level where Information_Level_ID=" + hfTrainTypeID.Value;
                DataSet ds = oa.RunSqlDataSet(strSql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTrainTypeName.Text = ds.Tables[0].Rows[0]["Information_Level_Name"].ToString();
                }
            }
        }

        private void FillPage(int informationId)
        {
            OracleAccess oa = new OracleAccess();

            string strSql = "select a.*,b.Employee_Name as CreatePersonName from Information a "
                +" inner join Employee b on a.Create_Person=b.Employee_ID"
                +" where Information_ID=" + informationId;

            DataSet ds = oa.RunSqlDataSet(strSql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                txtBookName.Text = dr["Information_Name"].ToString();
                ViewState["BookName"] = dr["Information_Name"].ToString();
                hfKnowledgeID.Value = dr["Information_System_ID"].ToString();
                hfTrainTypeID.Value = dr["Information_Level_ID"].ToString();
                txtBookNo.Text = dr["Information_No"].ToString();
                txtPublishOrgName.Text = dr["Publish_Org"].ToString();

                DateTime publishdate;
                DateTime.TryParse(dr["Publish_Date"].ToString(), out publishdate);
                datePublishDate.DateValue =publishdate.ToString("yyyy-MM-dd");

                hfEmployeeID.Value = dr["Create_Person"].ToString();
                txtCreatePerson.Text = dr["CreatePersonName"].ToString();
                txtKeyWords.Text = dr["keyWords"] == DBNull.Value ? string.Empty : dr["keyWords"].ToString();
                txtPageCount.Text = dr["Page_Count"] == DBNull.Value ? "" : dr["Page_Count"].ToString();
                txtWordCount.Text = dr["Word_Count"] == DBNull.Value ? "" : dr["Word_Count"].ToString();
                txtDescription.Text = dr["Description"] == DBNull.Value ? string.Empty : dr["Description"].ToString();
                txtAuthors.Text = dr["Authors"] == DBNull.Value ? string.Empty : dr["Authors"].ToString();
                txtLastPerson.Text = dr["LAST_UPDATE_PERSON"] == DBNull.Value ? string.Empty : dr["LAST_UPDATE_PERSON"].ToString();

                DateTime lastDate;
                DateTime.TryParse(dr["LAST_UPDATE_DATE"].ToString(), out lastDate);
                txtLastDate.Text = lastDate.ToString("yyyy-MM-dd");

                string strUrl = dr["Url"] == DBNull.Value ? string.Empty : dr["Url"].ToString();
                if(strUrl != string.Empty)
                {
                    strUrl = "../Online/AssistBook" + strUrl.Substring(7, strUrl.Length - 7);

                    hlUrl.Text = strUrl;
                    hlUrl.NavigateUrl = strUrl;
                }
                txtMemo.Text = dr["Memo"] == DBNull.Value ? string.Empty : dr["Memo"].ToString();
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            OracleAccess oa = new OracleAccess();
            string strSql;
            
            string strId = Request.QueryString.Get("id");
            if (txtBookName.Text.Trim() != ViewState["BookName"].ToString())
            {
                strSql = "select * from Information where Information_Name='" + txtBookName.Text.Trim() + "'";
                if (oa.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                {
                    SessionSet.PageMessage = "该资料名称已经存在";
                    return;
                }
            }

            strSql = @" update Information set 
                     Information_Name='" + txtBookName.Text.Trim() + @"',
                     Information_System_ID="+hfKnowledgeID.Value+@",
                     Information_No='"+txtBookNo.Text.Trim()+@"',
                     Publish_Org='" + txtPublishOrgName.Text.Trim() + @"',
                     Publish_Date="+(datePublishDate.DateValue==null?"null":"to_date('"+ datePublishDate.DateValue +"','yyyy-MM-dd')")+@",
                     Authors='" +txtAuthors.Text.Trim()+ @"',
                     KeyWords='" + txtKeyWords.Text.Trim() + @"',
                     Page_Count="+(txtPageCount.Text.Trim() == string.Empty?"null":txtPageCount.Text.Trim())+@",
                     Word_Count=" + (txtWordCount.Text.Trim() == string.Empty ? "null" : txtWordCount.Text.Trim()) + @",
                     Description='" + txtDescription.Text.Trim() + @"',
                     Last_Update_Person='" + PrjPub.CurrentLoginUser.EmployeeName + @"',
                     Last_Update_Date=sysdate,Memo='"+ txtMemo.Text.Trim() +@"',
                     Version=Version+1,Information_Level_ID="+hfTrainTypeID.Value+@" 
                     where Information_ID="+strId;

            oa.RunSqlDataSet(strSql);

             SaveBookCover(strId);

			 Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");

        }


        protected void SaveExitButton_Click(object sender, EventArgs e)
        {
            OracleAccess oa = new OracleAccess();
            string strSql= "select * from Information where Information_Name='" + txtBookName.Text.Trim() + "'";
            if (oa.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
            {
                SessionSet.PageMessage = "该资料名称已经存在";
                return;
            }

            SaveNewBook();

            Response.Write("<script>window.opener.frames['ifBookInfo'].form1.Refresh.value='true';window.opener.frames['ifBookInfo'].form1.submit();window.close();</script>");

        }

        protected void SaveNextButton_Click(object sender, EventArgs e)
        {
            OracleAccess oa = new OracleAccess();
            string strSql = "select * from Information where Information_Name='" + txtBookName.Text.Trim() + "'";
            if (oa.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
            {
                SessionSet.PageMessage = "该资料名称已经存在";
                return;
            }

            int newID = SaveNewBook();
            ClientScript.RegisterStartupScript(GetType(), "1", "<script>SaveNext(" + newID + ");</script>", false);
        }

        public  int SaveNewBook()
        {
            OracleAccess oa = new OracleAccess();

            string strSql = "select Information_Seq.nextval from dual";
            DataSet dsKey = oa.RunSqlDataSet(strSql);

            string strNewID = dsKey.Tables[0].Rows[0][0].ToString();

            int levelIndex, systemIndex;

            strSql = "select * from Information where Information_System_ID=" + hfKnowledgeID.Value+ " order by SYSTEM_ORDER_INDEX desc";
            DataSet dsSystem = oa.RunSqlDataSet(strSql);
            if(dsSystem.Tables[0].Rows.Count > 0)
            {
                systemIndex = Convert.ToInt32(dsSystem.Tables[0].Rows[0]["SYSTEM_ORDER_INDEX"]) + 1;
            }
            else
            {
                systemIndex = 1;
            }

            strSql = "select * from Information where Information_Level_ID=" + hfTrainTypeID.Value + " order by LEVEL_ORDER_INDEX desc";
            DataSet dsLevel = oa.RunSqlDataSet(strSql);
            if (dsLevel.Tables[0].Rows.Count > 0)
            {
                levelIndex = Convert.ToInt32(dsLevel.Tables[0].Rows[0]["LEVEL_ORDER_INDEX"]) + 1;
            }
            else
            {
                levelIndex = 1;
            }

            strSql = @"insert into Information(Information_ID,Information_Name,Information_System_ID,Information_No,
                Publish_Org,Publish_Date,Authors,KeyWords,Page_Count,Word_Count,Description,Last_Update_Person,
                Last_Update_Date,Memo,Version,Information_Level_ID,Create_Person,Create_Org_ID,
                SYSTEM_ORDER_INDEX,LEVEL_ORDER_INDEX) values( 
                " + strNewID + ",'" + txtBookName.Text.Trim() + @"'," + hfKnowledgeID.Value + @",'" + txtBookNo.Text.Trim() + @"',
                '" + txtPublishOrgName.Text.Trim() + @"',
                " + (datePublishDate.DateValue == null ? "null" : "to_date('" + datePublishDate.DateValue + "','yyyy-MM-dd')") + @",
                '" + txtAuthors.Text.Trim() + @"','" + txtKeyWords.Text.Trim() + @"',
                " + (txtPageCount.Text.Trim() == string.Empty ? "null" : txtPageCount.Text.Trim()) + @",
                " + (txtWordCount.Text.Trim() == string.Empty ? "null" : txtWordCount.Text.Trim()) + @",
                '" + txtDescription.Text.Trim() + @"','" + PrjPub.CurrentLoginUser.EmployeeName + @"',
                sysdate,'" + txtMemo.Text.Trim() + @"',1," + hfTrainTypeID.Value + @" ,
                " + PrjPub.CurrentLoginUser.EmployeeID + @","+ PrjPub.CurrentLoginUser.StationOrgID +@",
                "+systemIndex+@","+ levelIndex+@")";

            oa.RunSqlDataSet(strSql);

            string strPath = Server.MapPath("../Online/AssistBook/" + strNewID);
            Directory.CreateDirectory(strPath);
            Directory.CreateDirectory(strPath + "/Upload");
            CopyTemplate(Server.MapPath("../Online/AssistBook/template/"), Server.MapPath("../Online/AssistBook/" + strNewID + "/"));

            SaveBookCover(strNewID);
            return Convert.ToInt32(strNewID);
        }

        private void CopyTemplate(string srcPath, string aimPath)
        {
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }

            string[] fileList = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    CopyTemplate(file, aimPath + Path.GetFileName(file) + "\\");
                }
                else
                {
                    File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
        }

        private void SaveBookCover(string bookid)
        {
            string strBookUrl = "../AssistBook/" + bookid + "/cover.htm";
            BookBLL objBill = new BookBLL();
            objBill.UpdateBookUrl(Convert.ToInt32(bookid), strBookUrl);

            string srcPath = "../Online/AssistBook/" + bookid + "/cover.htm";

            OracleAccess db = new OracleAccess();
            string strSql = "select * from Information where Information_ID=" + bookid;

            DataSet ds = db.RunSqlDataSet(strSql);

            if (File.Exists(Server.MapPath(srcPath)))
            {
                File.Delete(Server.MapPath(srcPath));
            }

            if(ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                string str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                     + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                     + "<br><br><br><br><br><br><br>"
                     + "<div id='booktitle'>" + dr["Information_Name"] + "</div>" + "<br>"
                     + "<br><br><br><br><br><br><br><br><br><br><br>"
                     + "<div id='orgtitle'>" + dr["Publish_Org"] + "</div>" + "<br>"
                     + "<div id='authortitle'></div>"
                     + "</body>";

                File.AppendAllText(Server.MapPath(srcPath), str, System.Text.Encoding.UTF8);

                InformationShow show = new InformationShow();
                show.GetIndex(bookid);
            }
        }
    }
}