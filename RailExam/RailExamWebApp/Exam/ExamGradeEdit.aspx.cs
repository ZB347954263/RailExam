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
using RailExam.Model;
using RailExam.BLL;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using System.Reflection;

namespace RailExamWebApp.Exam
{
    public partial class ExamGradeEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = GetDataSet();
                if (ds != null && ds.Tables.Count == 2)
                {
                    gradesGrid.DataSource = ds;
                    gradesGrid.DataBind();
                }
                else
                {
                    SessionSet.PageMessage = "数据错误！";

                    return;
                }
                string strId = Request.QueryString.Get("eid");
                RailExam.Model.Exam exam = new RailExam.Model.Exam();
                ExamBLL eBll = new ExamBLL();
                exam = eBll.GetExam(int.Parse(strId));
                TextBoxExamCategory.Text = exam.CategoryName;
                TextBoxExamName.Text = exam.ExamName;
                TextBoxExamTime.Text = exam.BeginTime.ToString() + "/" + exam.EndTime.ToString();
            }
        }

        protected DataSet GetDataSet()
        {
            string qsTypeId = Request.QueryString.Get("type");
            string qsExamId = Request.QueryString.Get("eid");
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(qsTypeId) && !string.IsNullOrEmpty(qsExamId))
            {
                int nOrganizationId = string.IsNullOrEmpty(hfOrganizationId.Value) ? PrjPub.DEFAULT_INT_IN_DB : int.Parse(hfOrganizationId.Value);
                string strExamineeName = string.IsNullOrEmpty(txtExamineeName.Text) ? string.Empty : txtExamineeName.Text;
                decimal dScoreLower = string.IsNullOrEmpty(txtScoreLower.Text) ? 0 : decimal.Parse(txtScoreLower.Text);
                decimal dScoreUpper = string.IsNullOrEmpty(txtScoreUpper.Text) ? 500 : decimal.Parse(txtScoreUpper.Text);
                int nExamStatusId = string.IsNullOrEmpty(ddlStatusId.SelectedValue) ? PrjPub.DEFAULT_INT_IN_DB : int.Parse(ddlStatusId.SelectedValue);
                string strOrganizationName = string.IsNullOrEmpty(txtOrganizationName.Text) ? string.Empty : txtOrganizationName.Text;

                ExamResultBLL bllExamResult = new ExamResultBLL();
                IList<RailExam.Model.ExamResult> examResults = bllExamResult.GetExamResults(
                    int.Parse(qsExamId), strOrganizationName, strExamineeName,string.Empty, dScoreLower,
                    dScoreUpper, nExamStatusId);

                ExamResultStatusBLL bllExamResultStatus = new ExamResultStatusBLL();
                IList<RailExam.Model.ExamResultStatus> examResultStatuses = bllExamResultStatus.GetExamResultStatuses();

                DataTable dtExamResults = ConvertToDataTable((IList)examResults);


                DataTable dtExamResultStatuses = ConvertToDataTable((IList)examResultStatuses);

                if (dtExamResults != null)
                {
                    ds.Tables.Add(dtExamResults);
                }
                else
                {
                    ds.Tables.Add(ConvertToDataTable(typeof(RailExam.Model.ExamResult)));
                }
                ds.Tables.Add(dtExamResultStatuses);
                ds.Relations.Add(ds.Tables["ExamResultStatus"].Columns["ExamResultStatusId"],
                    ds.Tables["ExamResult"].Columns["StatusId"]);
            }

            return ds;
        }

        protected void gradesGrid_UpdateCommand(Object s, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            RailExam.Model.ExamResult examResult = new RailExam.Model.ExamResult();

            examResult.ExamResultId = (int)e.Item["ExamResultId"];
            examResult.BeginDateTime = (DateTime)e.Item["BeginDateTime"];
            examResult.EndDateTime = (DateTime)e.Item["EndDateTime"];
            examResult.Score = (decimal)e.Item["Score"];
            examResult.StatusId = int.Parse((string)e.Item["StatusName"]);

            ExamResultBLL bllExamResult = new ExamResultBLL();
            bllExamResult.UpdateExamResult(examResult);

            gradesGrid.DataSource = GetDataSet();
            gradesGrid.DataBind();
        }

        protected void gradesGrid_DeleteCommand(Object s, ComponentArt.Web.UI.GridItemEventArgs e)
        {
            int nExamResultId = (int)e.Item["ExamResultId"];
            ExamResultBLL bllExamResult = new ExamResultBLL();
            bllExamResult.DeleteExamResult(nExamResultId);

            gradesGrid.DataSource = GetDataSet();
            gradesGrid.DataBind();
        }

        protected void searchExamCallBack_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            DataSet ds = GetDataSet();
            if (ds != null && ds.Tables.Count == 2)
            {
                gradesGrid.DataSource = ds;
                gradesGrid.DataBind();
            }
            gradesGrid.RenderControl(e.Output);
        }

        protected void btnsClickCallBack_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            switch (e.Parameters[0])
            {
                case "delete":
                    {
                        string[] strExamResultIds = e.Parameters[1].Split('|');
                        int[] nExamResultIds = new int[strExamResultIds.Length];

                        int index = 0;
                        foreach (string s in strExamResultIds)
                        {
                            nExamResultIds[index] = int.Parse(s);
                            index++;
                        }

                        ExamResultBLL bllExamResult = new ExamResultBLL();
                        bllExamResult.DeleteExamResults(nExamResultIds);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            DataSet ds = GetDataSet();
            if (ds != null && ds.Tables.Count == 2)
            {
                gradesGrid.DataSource = ds;
                gradesGrid.DataBind();
            }
            gradesGrid.RenderControl(e.Output);
        }

        protected static DataTable ConvertToDataTable(IList list)
        {
            DataTable dt = null;

            if (list.Count > 0)
            {
                dt = new DataTable(list[0].GetType().Name);
                Type type = list[0].GetType();

                foreach (PropertyInfo pi in type.GetProperties())
                {
                    dt.Columns.Add(pi.Name, pi.PropertyType);
                }

                DataRow dr = null;
                foreach (Object o in list)
                {
                    dr = dt.NewRow();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc] = type.GetProperty(dc.ColumnName).GetValue(o, null);
                    }
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }

            return dt;
        }

        protected static DataTable ConvertToDataTable(Type type)
        {
            DataTable dt = new DataTable(type.Name);

            foreach (PropertyInfo pi in type.GetProperties())
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            dt.AcceptChanges();

            return dt;
        }
    }
}
