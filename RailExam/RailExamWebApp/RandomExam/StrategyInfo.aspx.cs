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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class StrategyInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfStrategySubjectID.Value = Request.QueryString.Get("id");
				//获取大题的取题类型
				hfItemType.Value = Request.QueryString.Get("itemTypeID");

                OracleAccess db = new OracleAccess();
                string strSql = @"delete from Random_Exam_Item_Select where RANDOM_EXAM_STRATEGY_ID not in (select RANDOM_EXAM_STRATEGY_ID
                            from RANDOM_EXAM_STRATEGY where Subject_ID=" + hfStrategySubjectID.Value + ") and RANDOM_EXAM_SUBJECT_ID=" + hfStrategySubjectID.Value;
                db.ExecuteNonQuery(strSql);
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    DeleteData(int.Parse(strDeleteID));
                    Grid1.DataBind();

                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"window.parent.subjectCallback.callback();",
                        true);
                }

                string strRefresh = Request.Form.Get("Refresh");
                if (!string.IsNullOrEmpty(strRefresh))
                {
                    if(strRefresh != "true")
                    {
                        OracleAccess db = new OracleAccess();
                        string strSql = "delete from Random_Exam_Item_Select where RANDOM_EXAM_STRATEGY_ID=" + strRefresh;
                        db.ExecuteNonQuery(strSql);
                    }

                    Grid1.DataBind();
                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"window.parent.subjectCallback.callback();",
                        true);
                }
            }
        }

        private void DeleteData(int nBookID)
        {
            RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();
            RandomExamSubject paperStrategySubject = paperStrategySubjectBLL.GetRandomExamSubject(int.Parse(Request.QueryString.Get("id")));

            if (paperStrategySubject != null)
            {
                RandomExamResultBLL reBll = new RandomExamResultBLL();
                IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(paperStrategySubject.RandomExamId);

                if (Pub.HasPaper(Convert.ToInt32(paperStrategySubject.RandomExamId)))
                {
                    SessionSet.PageMessage = "该考试已生成试卷，取题范围不能被删除！";
                    return;
                }

                if (examResults.Count > 0)
                {
                    SessionSet.PageMessage = "已有考生参加考试，取题范围不能被删除！";
                    return;
                }
            }

            RandomExamStrategyBLL paperStrategyBookChapterBLL = new RandomExamStrategyBLL();
            paperStrategyBookChapterBLL.DeleteRandomExamStrategy(nBookID);

            OracleAccess db = new OracleAccess();
            string strSql = "delete from Random_Exam_Strategy where Is_Mother_Item=1 and Mother_ID=" + nBookID;
            db.ExecuteNonQuery(strSql);

            strSql = "delete from Random_Exam_Item_Select where  RANDOM_EXAM_STRATEGY_ID=" + nBookID;
            db.ExecuteNonQuery(strSql);
        }
    }
}
