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
using ComponentArt.Web.UI;

namespace RailExamWebApp.Main
{
    public partial class FocalPointStudy : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TreeViewDataBind_TrainPlanType();
            }
        }

        //根节点绑定培训类别
        private void TreeViewDataBind_TrainPlanType()
        {
            OracleAccess oracle = new OracleAccess();
            string sql = "select * from ZJ_TRAINPLAN_TYPE t";
            DataSet ds = oracle.RunSqlDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TreeViewNode tvNode = new TreeViewNode();
                    tvNode.ID = dr["trainplan_type_id"].ToString();
                    tvNode.Text = dr["trainplan_type_name"].ToString();
                    TreeViewDataBind_TrainPlanProject(tvNode);  //绑定该培训类别下的培训项目
                    this.tvView.Nodes.Add(tvNode);
                }
            }
        }

        //第二级节点绑定培训项目
        private void TreeViewDataBind_TrainPlanProject(TreeViewNode parentNode)
        {
            OracleAccess oracle = new OracleAccess();
            string sql = String.Format("select * from ZJ_TRAINPLAN_PROJECT t where trainplan_type_id = {0}", parentNode.ID);
            DataSet ds = oracle.RunSqlDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TreeViewNode newNode = new TreeViewNode();
                    newNode.ID = dr["trainplan_project_id"].ToString();
                    newNode.Text = dr["trainplan_project_name"].ToString();
                    TreeViewDataBind_TrainPlan(newNode);    //绑定该培训项目下的当前登录者所属的培训计划
                    parentNode.Nodes.Add(newNode);
                }
            }
        }

        //第三级节点绑定培训计划
        private void TreeViewDataBind_TrainPlan(TreeViewNode parentNode)
        {
            string sql1 = String.Format("select train_plan_id from ZJ_TRAIN_PLAN_EMPLOYEE t where employee_id = {0}", PrjPub.CurrentStudent.EmployeeID);
            string trainPlanIDs = GetIDsBySQL(sql1);
            if (!String.IsNullOrEmpty(trainPlanIDs))
            {
                OracleAccess oracle = new OracleAccess();
                string sql2 = String.Format("select * from ZJ_TRAIN_PLAN t where train_plan_id in ({0}) and train_plan_project_id = {1}", trainPlanIDs, parentNode.ID);
                DataSet ds = oracle.RunSqlDataSet(sql2);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TreeViewNode newNode = new TreeViewNode();
                        newNode.ID = dr["train_plan_id"].ToString();
                        newNode.Text = dr["train_plan_name"].ToString() + "(培训计划)";
                        TreeViewDataBind_TrainClass(newNode);   //绑定该培训计划下的培训班组
                        parentNode.Nodes.Add(newNode);
                    }
                }
            }
        }

        //第四级节点绑定培训班组
        private void TreeViewDataBind_TrainClass(TreeViewNode parentNode)
        {
            OracleAccess oracle = new OracleAccess();
            string sql = String.Format("select * from ZJ_TRAIN_CLASS t where train_plan_id = {0} and train_class_id in (select distinct train_class_id from zj_train_plan_employee where employee_id = {1})", parentNode.ID, PrjPub.CurrentStudent.EmployeeID);
            DataSet ds = oracle.RunSqlDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TreeViewNode newNode = new TreeViewNode();
                    newNode.ID = dr["train_class_id"].ToString();
                    newNode.Text = dr["train_class_name"].ToString() + "(培训班组)";
                    TreeViewDataBind_TrainClassSubject(newNode);    //绑定该培训班组下的培训科目
                    parentNode.Nodes.Add(newNode);
                }
            }
        }

        //第无级节点邦定培训科目
        private void TreeViewDataBind_TrainClassSubject(TreeViewNode parentNode)
        {
            OracleAccess oracle = new OracleAccess();
            string sql = String.Format("select * from ZJ_TRAIN_CLASS_SUBJECT t where train_class_id = {0}", parentNode.ID);
            DataSet ds = oracle.RunSqlDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TreeViewNode newNode = new TreeViewNode();
                    newNode.ID = dr["train_class_subject_id"].ToString();
                    newNode.Text = dr["subject_name"].ToString() + "(培训科目)";
                    parentNode.Nodes.Add(newNode);
                }
            }
        }

        private string GetIDsBySQL(string sql)
        {
            OracleAccess oracle = new OracleAccess();
            string ids = String.Empty;
            DataSet ds = oracle.RunSqlDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (String.IsNullOrEmpty(ids))
                    {
                        ids = dr[0].ToString();
                    }
                    else
                    {
                        ids += "," + dr[0].ToString();
                    }
                }
            }
            return ids;
        }
    }
}
