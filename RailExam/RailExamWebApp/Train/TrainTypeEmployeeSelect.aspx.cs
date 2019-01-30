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
using ComponentArt.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Train
{
    public partial class TrainTypeEmployeeSelect : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PrjPub.StudentID == null)
            {
                Response.Write("<script>alert('已超时，请注销后重新登录！');window.close();</script>");
                return;
            }
            if (!IsPostBack)
            {
                BindTrainTypeTree();
            }
        }

        private void BindTrainTypeTree()
        {
            TrainTypeBLL objTrainType = new TrainTypeBLL();

            IList<TrainType> train = objTrainType.GetTrainTypes();

            Pub.BuildComponentArtTreeView(tvType, (IList)train, "TrainTypeID", "ParentID", "TypeName", "TypeName", "TrainTypeID", null, null, null);
            //tvType.ExpandAll();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (tvType.SelectedNode == null)
            {
                SessionSet.PageMessage = "请选择培训类别！";
                return;
            }

            if (tvType.SelectedNode.Nodes.Count > 0)
            {
                SessionSet.PageMessage = "请选择叶子节点！";
                return;
            }

            int nEmpID;

            nEmpID = PrjPub.CurrentStudent.EmployeeID;

            TrainEmployeeBLL objBll = new TrainEmployeeBLL();

            TrainEmployee objNow = objBll.GetTrainEmployeeByEmployeeID(nEmpID);

            TrainEmployee obj = new TrainEmployee();

            obj.EmployeeID = nEmpID;
            obj.TrainTypeID = Convert.ToInt32(tvType.SelectedNode.ID);

            if (objNow.EmployeeID == 0)
            {
                objBll.AddTrainEmployee(obj);
            }
            else
            {
                obj.TrainEmployeeID = objNow.TrainEmployeeID;
                objBll.UpdateTrainEmployee(obj);
            }

            Response.Write("<script>window.opener.form1.submit();window.close();</script>");
        }
    }
}