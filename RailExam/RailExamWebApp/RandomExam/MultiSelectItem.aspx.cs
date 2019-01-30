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
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class MultiSelectItem : PageBase
    {
        int RecordCount, CurrentPage, PageCount, JumpPage; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewState["ID"] = Request.QueryString.Get("id");
                ViewState["TypeID"] = Request.QueryString.Get("itemTypeID");
                ViewState["BookChapterID"] = Request.QueryString.Get("BookChapterID");
                ViewState["RangeType"] = Request.QueryString.Get("RangeType");
                ViewState["ExamID"] = Request.QueryString.Get("examId");
                ViewState["exculdeIDs"] = Request.QueryString.Get("exculdeIDs");
                ViewState["subjectId"] = Request.QueryString.Get("subjectId");

                HasExamId();
                ViewState["ChooseId"] = ViewState["HasExamId"].ToString();


                if (ViewState["RangeType"].ToString() == "4")
                {
                    BookChapterBLL bookChapterBll = new BookChapterBLL();
                    BookChapter bookChapter = bookChapterBll.GetBookChapter(Convert.ToInt32(ViewState["BookChapterID"]));
                    lblChapterName.Text = bookChapter.NamePath;
                }
                else
                {
                    BookBLL bookBll = new BookBLL();
                    RailExam.Model.Book book = bookBll.GetBook(Convert.ToInt32(ViewState["BookChapterID"]));
                    lblChapterName.Text = book.bookName;
                }

                ViewState["StartRow"] = 0;
                ViewState["EndRow"] = Grid1.PageSize;

                ViewState["EmploySortField"] = "nlssort(a.Item_ID,'NLS_SORT=SCHINESE_PINYIN_M')";

                RecordCount = GetCount();
                //������ҳ��������OverPage()������ֹ�����������ʾ���ݲ�������   
                PageCount = RecordCount / Grid1.PageSize + OverPage();
                ViewState["RecordCount"] = RecordCount;
                //������ҳ������ViewState����ȥModPage()������ֹSQL���ִ��ʱ�����ѯ��Χ�������ô洢���̷�ҳ�㷨�������䣩   
                ViewState["PageCounts"] = RecordCount / Grid1.PageSize - ModPage();
                //����һ��Ϊ0��ҳ������ֵ��ViewState   
                ViewState["PageIndex"] = 0;
                //����PageCount��ViewState����ҳʱ�ж��û��������Ƿ񳬳�ҳ�뷶Χ   
                ViewState["JumpPages"] = PageCount;
                //��ʾLPageCount��LRecordCount��״̬   
                this.lbPageCount.Text = PageCount.ToString();
                this.lbRecordCount.Text = RecordCount.ToString();
                //�ж���ҳ�ı���ʧЧ   
                if (RecordCount <= Grid1.PageSize)
                {
                    btnJumpPage.Enabled = false;
                }
                else
                {
                    btnJumpPage.Enabled = true;
                }

                BindGrid(ViewState["EmploySortField"].ToString());
            }
        }

        private void HasExamId()
        {
            string strSql = "select a.* from Random_Exam_Item_Select a "
                            + "inner join Item b on a.Item_ID=b.Item_ID "
                            + " where Random_Exam_Strategy_ID=" + ViewState["ID"];

            OracleAccess db = new OracleAccess();
            string strIDs = string.Empty;

            DataSet ds = db.RunSqlDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strIDs += strIDs == string.Empty ? dr["Item_ID"].ToString() : "," + dr["Item_ID"];
            }

            ViewState["HasExamId"] = strIDs;
        }

        private string  GetWhereClause()
        {
            string strSql = "";
            if(txtContent.Text.Trim() != "")
            {
                strSql += " and Content like '%" + txtContent.Text + "%'";
            }

            if(ddlDiff.SelectedValue == "0")
            {
                strSql += " and DIFFICULTY_ID=" + ddlDiff.SelectedValue;
            }

            //�½�
            if(ViewState["RangeType"].ToString() == "4")
            {
                OracleAccess db = new OracleAccess();
                string str = "select * from Book_Chapter where Chapter_ID=" + ViewState["BookChapterID"];
                string idPath = db.RunSqlDataSet(str).Tables[0].Rows[0]["Id_Path"].ToString();

                strSql = " and   c.id_path || '/' like '" + idPath + "/%'";
            }
            else
            {
                strSql += "  and a.Book_ID=" + ViewState["BookChapterID"];
            }

            strSql += " and a.Type_ID=" + ViewState["TypeID"] +" and Status_ID=1";

            if (ViewState["exculdeIDs"].ToString() != "")
            {
                strSql += " and  instr(','||" + ViewState["exculdeIDs"] + "||',',','||CHAPTER_ID||',' )=0 ";
            }

            //��������ID��Ϊ��ʱ����Ҫ�ų���ǰ������ID��ѡ������
            strSql += " and Item_ID not in (select Item_ID from Random_Exam_Item_Select where Random_Exam_ID=" +
                      ViewState["ExamID"] + " and RANDOM_EXAM_STRATEGY_ID!=" + ViewState["ID"] + ")";

            return strSql;
        }

        private int GetCount()
        {
            string strWhere=GetWhereClause();

            OracleAccess db = new OracleAccess();

            string strSql = @"select a.*,b.Type_Name as TypeName
                     from Item a
                     inner join  Item_Type b on a.Type_Id=b.item_type_id
                     inner join Book_Chapter c on a.Chapter_ID=c.Chapter_ID 
                     where 1=1 " + strWhere;
            DataSet ds = db.RunSqlDataSet(strSql);
            return ds.Tables[0].Rows.Count;
        }

        private void BindGrid(string strOrderBy)
        {
            //HasExamId();

            int nItemcount = 0;
            int startRow = int.Parse(ViewState["StartRow"].ToString());
            int endRow = int.Parse(ViewState["EndRow"].ToString());

             string strWhere=GetWhereClause();

            //��ViewState�ж�ȡҳ��ֵ���浽CurrentPage�����н��а�ťʧЧ����   
            CurrentPage = (int)ViewState["PageIndex"];
            //��ViewState�ж�ȡ��ҳ�������а�ťʧЧ����   
            PageCount = (int)ViewState["PageCounts"];
            //�ж��ĸ���ť����ҳ����ҳ����ҳ��βҳ��״̬   
            if (CurrentPage + 1 > 1)
            {
                Fistpage.Enabled = true;
                Prevpage.Enabled = true;
            }
            else
            {
                Fistpage.Enabled = false;
                Prevpage.Enabled = false;
            }
            if (CurrentPage == PageCount)
            {
                Nextpage.Enabled = false;
                Lastpage.Enabled = false;
            }
            else
            {
                Nextpage.Enabled = true;
                Lastpage.Enabled = true;
            }

            string strSql =
                @" select  * from 
                    (select rownum rn ,t.* from 
                    (select a.*,b.Type_Name as TypeName
                     from Item a
                     inner join  Item_Type b on a.Type_Id=b.item_type_id
                     inner join Book_Chapter c on a.Chapter_ID=c.Chapter_ID 
                     where 1=1 " + strWhere+@"
                     order by "+strOrderBy  +@" ) t
                     where rownum<(" + endRow + @"+1)) where rn>(" + startRow + ")";

            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid1.VirtualItemCount = nItemcount;
                Grid1.DataSource = ds.Tables[0];
                Grid1.DataBind();
                ViewState["EmptyFlag"] = 0;

                RecordCount = ds.Tables[0].Rows.Count;

                //��ʾ�ı���ؼ�txtJumpPage״̬   
                txtJumpPage.Text = (CurrentPage + 1).ToString();
            }
            else
            {
                BindEmptyGrid1();
                txtJumpPage.Text = (CurrentPage + 1).ToString();
                RecordCount = 0;
            }
        }

        private void BindEmptyGrid1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Item_ID", typeof(int)));
            dt.Columns.Add(new DataColumn("content", typeof(string)));
            dt.Columns.Add(new DataColumn("TypeName", typeof(string)));
            dt.Columns.Add(new DataColumn("DIFFICULTY_ID", typeof(int)));

            DataRow dr = dt.NewRow();

            dr["Item_ID"] = 0;
            dr["content"] = "";
            dr["TypeName"] = "";
            dr["DIFFICULTY_ID"] = 0;
            dt.Rows.Add(dr);

            Grid1.VirtualItemCount = 1;
            Grid1.DataSource = dt;
            Grid1.DataBind();

            CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[0].FindControl("chkSelect");
            CheckBox1.Visible = false;
            ViewState["EmptyFlag"] = 1;
        }

        protected void Grid1_PageIndexChanging(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();

            for (int i = 0; i < this.Grid1.Items.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");

                string strEmId = ((Label)this.Grid1.Items[i].FindControl("LabelItemID")).Text;
                if (CheckBox1.Checked)
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
                    {
                        if (strAllId.Length == 0)
                        {
                            strAllId += strEmId;
                        }
                        else
                        {
                            strAllId += "," + strEmId;
                        }
                    }
                }
                else
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") >= 0)
                    {
                        strOldAllId = strOldAllId.Replace("," + strEmId + ",", ",");
                    }

                    strAllId = strOldAllId.TrimStart(',').TrimEnd(',');
                }
            }

            ViewState["ChooseId"] = strAllId;

            this.Grid1.CurrentPageIndex = e.NewPageIndex;
            ViewState["StartRow"] = Grid1.PageSize * e.NewPageIndex;
            ViewState["EndRow"] = Grid1.PageSize * (e.NewPageIndex + 1);

            RecordCount = (int)ViewState["RecordCount"];
            PageCount = RecordCount / Grid1.PageSize + OverPage();
            ViewState["PageCounts"] = RecordCount / Grid1.PageSize - ModPage();
            ViewState["JumpPages"] = PageCount;
            this.lbPageCount.Text = PageCount.ToString();
            if (RecordCount <= Grid1.PageSize)
            {
                btnJumpPage.Enabled = false;
            }
            else
            {
                btnJumpPage.Enabled = true;
            }

            BindGrid(ViewState["EmploySortField"].ToString());
        }

        //������ҳ   
        public int OverPage()
        {
            int pages = 0;
            if (RecordCount % Grid1.PageSize != 0)
                pages = 1;
            else
                pages = 0;
            return pages;
        }
        //������ҳ����ֹSQL���ִ��ʱ�����ѯ��Χ   
        public int ModPage()
        {
            int pages = 0;
            if (RecordCount % Grid1.PageSize == 0 && RecordCount != 0)
                pages = 1;
            else
                pages = 0;
            return pages;
        }


        protected void Grid1_Sorting(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            if (ViewState["EmploySortField"] != null && ViewState["EmploySortField"].ToString() == e.SortExpression)
            {
                ViewState["EmploySortField"] = "nlssort(" + e.SortExpression + ",'NLS_SORT=SCHINESE_PINYIN_M')  desc";
            }
            else
            {
                ViewState["EmploySortField"] = "nlssort(" + e.SortExpression + ",'NLS_SORT=SCHINESE_PINYIN_M')";
            }

            BindGrid(ViewState["EmploySortField"].ToString());
        }

        protected void Page_OnClick(object sender, EventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();

            for (int i = 0; i < this.Grid1.Items.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");

                string strEmId = ((Label)this.Grid1.Items[i].FindControl("LabelItemID")).Text;
                if (CheckBox1.Checked)
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
                    {
                        if (strAllId.Length == 0)
                        {
                            strAllId += strEmId;
                        }
                        else
                        {
                            strAllId += "," + strEmId;
                        }
                    }
                }
                else
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") >= 0)
                    {
                        strOldAllId = strOldAllId.Replace("," + strEmId + ",", ",");
                    }

                    strAllId = strOldAllId.TrimStart(',').TrimEnd(',');
                }
            }

            ViewState["ChooseId"] = strAllId;

            //��ViewState�ж�ȡҳ��ֵ���浽CurrentPage�����н��в�������   
            CurrentPage = (int)ViewState["PageIndex"];
            //��ViewState�ж�ȡ��ҳ��������   
            PageCount = (int)ViewState["PageCounts"];
            string cmd = ((LinkButton)sender).CommandArgument;
            switch (cmd)
            {
                case "Next":
                    CurrentPage++;
                    break;
                case "Prev":
                    CurrentPage--;
                    break;
                case "Last":
                    CurrentPage = PageCount;
                    break;
                default:
                    CurrentPage = 0;
                    break;
            }
            //��������CurrentPage�����ٴα�����ViewState   
            ViewState["PageIndex"] = CurrentPage;

            ViewState["StartRow"] = Grid1.PageSize * CurrentPage;
            ViewState["EndRow"] = Grid1.PageSize * (CurrentPage + 1);


            BindGrid(ViewState["EmploySortField"].ToString());
        }

        protected void btnJumpPage_Click(object sender, EventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();

            for (int i = 0; i < this.Grid1.Items.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");

                string strEmId = ((Label)this.Grid1.Items[i].FindControl("LabelItemID")).Text;
                if (CheckBox1.Checked)
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
                    {
                        if (strAllId.Length == 0)
                        {
                            strAllId += strEmId;
                        }
                        else
                        {
                            strAllId += "," + strEmId;
                        }
                    }
                }
                else
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") >= 0)
                    {
                        strOldAllId = strOldAllId.Replace("," + strEmId + ",", ",");
                    }

                    strAllId = strOldAllId.TrimStart(',').TrimEnd(',');
                }
            }

            ViewState["ChooseId"] = strAllId;

            JumpPage = (int)ViewState["JumpPages"];
            //�ж��û�����ֵ�Ƿ񳬹�����ҳ����Χֵ   
            if (Int32.Parse(this.txtJumpPage.Text.Trim()) > JumpPage || Int32.Parse(this.txtJumpPage.Text.Trim()) <= 0)
            {
                CurrentPage = (int)ViewState["PageIndex"];
                this.txtJumpPage.Text = (CurrentPage + 1).ToString();
                SessionSet.PageMessage = "ҳ�뷶ΧԽ��";
                return;
            }
            else
            {
                int InputPage = Int32.Parse(this.txtJumpPage.Text.Trim()) - 1;
                ViewState["PageIndex"] = InputPage;

                ViewState["StartRow"] = Grid1.PageSize * InputPage;
                ViewState["EndRow"] = Grid1.PageSize * (InputPage + 1);

                BindGrid(ViewState["EmploySortField"].ToString());
            }
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            ViewState["EmploySortField"] = "nlssort(a.Item_ID,'NLS_SORT=SCHINESE_PINYIN_M')";
            ViewState["StartRow"] = 0;
            ViewState["EndRow"] = Grid1.PageSize;
            this.Grid1.CurrentPageIndex = 0;

            RecordCount = GetCount();
            //������ҳ��������OverPage()������ֹ�����������ʾ���ݲ�������   
            PageCount = RecordCount / Grid1.PageSize + OverPage();
            ViewState["RecordCount"] = RecordCount;
            //������ҳ������ViewState����ȥModPage()������ֹSQL���ִ��ʱ�����ѯ��Χ�������ô洢���̷�ҳ�㷨�������䣩   
            ViewState["PageCounts"] = RecordCount / Grid1.PageSize - ModPage();
            //����һ��Ϊ0��ҳ������ֵ��ViewState   
            ViewState["PageIndex"] = 0;
            //����PageCount��ViewState����ҳʱ�ж��û��������Ƿ񳬳�ҳ�뷶Χ   
            ViewState["JumpPages"] = PageCount;
            //��ʾLPageCount��LRecordCount��״̬   
            this.lbPageCount.Text = PageCount.ToString();
            this.lbRecordCount.Text = RecordCount.ToString();
            //�ж���ҳ�ı���ʧЧ   
            if (RecordCount <= Grid1.PageSize)
            {
                btnJumpPage.Enabled = false;
            }
            else
            {
                btnJumpPage.Enabled = true;
            }

            BindGrid(ViewState["EmploySortField"].ToString());
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();

            for (int i = 0; i < this.Grid1.Items.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");

                string strEmId = ((Label)this.Grid1.Items[i].FindControl("LabelItemID")).Text;
                if (CheckBox1.Checked)
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
                    {
                        if (strAllId.Length == 0)
                        {
                            strAllId += strEmId;
                        }
                        else
                        {
                            strAllId += "," + strEmId;
                        }
                    }
                }
                else
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") >= 0)
                    {
                        strOldAllId = strOldAllId.Replace("," + strEmId + ",", ",");
                    }

                    strAllId = strOldAllId.TrimStart(',').TrimEnd(',');
                }
            }

            string strSql = "select * from Random_Exam_Item_Select ";

            if(ViewState["ID"].ToString() == string.Empty)
            {
                strSql += " where RANDOM_EXAM_STRATEGY_ID  is null  and Random_Exam_Subject_ID=" + ViewState["subjectId"];
            }
            else
            {
                strSql += " where RANDOM_EXAM_STRATEGY_ID=" + ViewState["ID"];
            }

            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);

            //������ѡ���⣬������ѡ����ID���ڵ�ǰ��ѡ�У�ɾ����
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if((","+strAllId+",").IndexOf(","+dr["Item_ID"]+",")<0)
                {
                    strSql = "delete from Random_Exam_Item_Select where Random_Exam_Item_Select_ID=" + dr["Random_Exam_Item_Select_ID"];
                    db.ExecuteNonQuery(strSql);
                }
            }

            string[] str = strAllId.Split(',');
            for(int i=0; i <str.Length; i++)
            {
                DataRow[] drs = ds.Tables[0].Select("Item_ID=" + str[i]);
                if(drs.Length == 0)
                {
                    strSql = @"insert into Random_Exam_Item_Select " +
                             " values(Random_Exam_Item_Select_Seq.nextval," + str[i] + "," + ViewState["ExamID"] + @"," + ViewState["ID"] + @"," + ViewState["subjectId"] + ")";
                    db.ExecuteNonQuery(strSql);
                }
            }
            Response.Write("<script>window.returnValue='true';window.close();</script>");
        }

    }
}
