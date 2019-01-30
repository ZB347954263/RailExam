using System;
using System.Collections.Generic;
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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class TJ_SelectExam : PageBase
    {
        private static DataTable dtAll;

        protected void Page_Load(object sender, EventArgs e)
        {
            SelectedEmpIDs();
            if(!IsPostBack)
            {
                hfSelect.Value = "select a.*,'' as ExamStyleName,'' as StationName from Random_Exam a where 1=2";
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            hfSelect.Value = GetSql();
            OracleAccess db = new OracleAccess();
            dtAll = db.RunSqlDataSet(GetSql()).Tables[0];
            usersDic = InitializeUsersDic(db.RunSqlDataSet(GetSql()).Tables[0]);
            hfRefresh.Value = "false";
        }

        private Dictionary<string, bool> usersDic
        {
            get { return (ViewState["usersdic"] != null) ? (Dictionary<string, bool>)ViewState["usersdic"] : null; }
            set { ViewState["usersdic"] = value; }

        }
        //初始化Dictionary    
        protected Dictionary<string, bool> InitializeUsersDic(DataTable dt)
        {
            Dictionary<string, bool> currentDic = new Dictionary<string, bool>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currentDic.Add(dt.Rows[i]["Random_Exam_ID"].ToString(), false);
            }
            return currentDic;
        }

        private void RememberOldValues(Dictionary<string, bool> dic, GridView gdv)
        {
            for (int i = 0; i < gdv.Rows.Count; i++)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl spanID =
                    (System.Web.UI.HtmlControls.HtmlGenericControl)gdv.Rows[i].Cells[0].FindControl("spanID");

                string currentValue = spanID.InnerText;
                CheckBox currentCbx = gdv.Rows[i].Cells[0].FindControl("item") as CheckBox;
                if (currentCbx.Checked && !dic[currentValue])
                {
                    dic[currentValue] = true;
                }
                else if (!currentCbx.Checked && dic[currentValue])
                {
                    dic[currentValue] = false;
                }

            }
            ViewState["usersdic"] = dic;

        }

        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
                {
                    e.Row.Visible = false;
                }
                else
                {
                    e.Row.Attributes.Add("onclick", "selectArow(this);");
                }
            }
            if (e.Row != null && e.Row.RowType == DataControlRowType.Header)
                ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick", "chkAll(this)");


        }

        protected void grdEntity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CheckBox currentCbx = e.Row.FindControl("item") as CheckBox;
            System.Web.UI.HtmlControls.HtmlGenericControl spanID =
                (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("spanID");


            if (currentCbx != null && usersDic != null)
            {
                if (spanID.InnerText != "" && usersDic.ContainsKey(spanID.InnerText))
                {
                    if (usersDic != null && usersDic[spanID.InnerText] == true)
                        currentCbx.Checked = true;
                }
            }

            if (spanID != null)
            {
                string[] arr = (string[])ViewState["SelectedEmpID"];
                foreach (string s in arr)
                {
                    if (s.Equals(spanID.InnerText))
                    {
                        currentCbx.Checked = true;
                        break;
                    }
                }
            }
        }

        private string GetSql()
        {
            if(PrjPub.CurrentLoginUser.SuitRange == 0)
            {
                hfSql.Value += " and (b.Suit_Range=1 or a.Org_ID=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
            }

            int railSystemId = PrjPub.GetRailSystemId();
            if (railSystemId != 0)
            {
                 hfSql.Value += " and a.Org_ID in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                      " and level_num=2)";
            }

            string sql = "select a.*,case when a.Exam_Style=1 then '不存档考试' else '存档考试' end as ExamStyleName,"
                         + " b.Short_Name as StationName"
                         + " from Random_Exam a "
                         + " inner join Org b on a.Org_ID=b.Org_ID "
                         + " where "+hfSql.Value + " order by Begin_Time desc";

            return sql;
        }

        protected void grdEntity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //记录原来分页面的所有CheckBox的状态
            RememberOldValues(usersDic, grdEntity);
            grdEntity.PageIndex = e.NewPageIndex;
            hfSelect.Value = GetSql();
            grdEntity.DataBind();
        }

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
            if (db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["Random_Exam_ID"] = -1;
                db.Rows.Add(row);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>();
            List<string> lstName = new List<string>();
            foreach (DataRow dr in dtAll.Rows)
            {
                if (usersDic.ContainsKey(dr["Random_Exam_ID"].ToString()))
                {
                    if (usersDic[dr["Random_Exam_ID"].ToString()])
                        lst.Add(dr["Random_Exam_ID"].ToString());
                }

            }

            foreach (GridViewRow r in grdEntity.Rows)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl spanID =
                    (System.Web.UI.HtmlControls.HtmlGenericControl)r.FindControl("spanID");
                CheckBox currentCbx = r.FindControl("item") as CheckBox;
                string currentValue = spanID.InnerText;
                if (currentValue != "" && currentCbx.Checked && !lst.Contains(spanID.InnerText))
                    lst.Add(spanID.InnerText);
            }

            hfid.Value = string.Join("|", lst.ToArray());

            if(string.IsNullOrEmpty(hfid.Value))
            {
                SessionSet.PageMessage = "请至少选择一个考试！";
                return;
            }

            string[] str = hfid.Value.Split('|');
            RandomExamBLL objBll = new RandomExamBLL();

            string strName = string.Empty;
            for (int i = 0; i < str.Length;i++ )
            {
                string examName = objBll.GetExam(Convert.ToInt32(str[i])).ExamName;

                if(strName == string.Empty)
                {
                    strName = examName;
                }
                else
                {
                    strName += "|" + examName;
                }
            }

            hfid.Value = hfid.Value + "$" + strName;

            ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue = '" + hfid.Value + "';window.close();", true);
        }

        private void SelectedEmpIDs()
        {
            string[] arr =  hfExamID.Value.Split('|');
            ViewState["SelectedEmpID"] = arr;
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            btnAll.Enabled = false;
            btnOK.Enabled = false;
            hfSelect.Value = GetSql();
            OracleAccess db = new OracleAccess();
            dtAll = db.RunSqlDataSet(GetSql()).Tables[0];
            string[] strExam = new string[dtAll.Rows.Count];
            for (int i = 0; i < dtAll.Rows.Count; i++)
            {
                strExam[i] = dtAll.Rows[i]["Random_Exam_ID"].ToString();
            }

            RandomExamBLL objBll = new RandomExamBLL();

            string strID = string.Empty;
            string strName = string.Empty;
            for (int i = 0; i < strExam.Length; i++)
            {
                string examName = objBll.GetExam(Convert.ToInt32(strExam[i])).ExamName;

                if (strName == string.Empty)
                {
                    strName = examName;
                }
                else
                {
                    strName += "|" + examName;
                }

                if (strID == string.Empty)
                {
                    strID = strExam[i];
                }
                else
                {
                    strID += "|" + strExam[i];
                }
            }

            hfid.Value = strID + "$" + strName;

            ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue = '" + hfid.Value + "';window.close();", true);
        }
    }
}
