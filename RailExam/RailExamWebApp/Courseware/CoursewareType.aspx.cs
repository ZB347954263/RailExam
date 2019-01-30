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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Courseware
{
    public partial class CoursewareType : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindTree();
                if (PrjPub.HasEditRight("课件体系") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("课件体系") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
            }
        }

        private void BindTree()
        {
            CoursewareTypeBLL coursewareTypeBLL = new CoursewareTypeBLL();

            IList<RailExam.Model.CoursewareType> coursewareTypeList = coursewareTypeBLL.GetCoursewareTypes(0, 0, "", 0, 0, "", "", "", 0, 4000, "LevelNum, OrderIndex ASC");

            Pub.BuildComponentArtTreeView(tvCoursewareType, (IList)coursewareTypeList,
                "CoursewareTypeId", "ParentId", "CoursewareTypeName", "CoursewareTypeName", "CoursewareTypeId", null, null, null);
        }


        protected void tvCoursewareTypeMoveCallBack_Callback(object sender, CallBackEventArgs e)
        {
            ComponentArt.Web.UI.TreeViewNode node = tvCoursewareType.FindNodeById(e.Parameters[0]);

            if (node != null && e.Parameters[1] == "CanMoveUp")
            {
                if (node.PreviousSibling != null)
                {
                    hfCanMove.Value = "true";
                    hfCanMove.RenderControl(e.Output);
                }
                else
                {
                    hfCanMove.Value = string.Empty;
                    hfCanMove.RenderControl(e.Output);
                }
            }
            else if (node != null && e.Parameters[1] == "CanMoveDown")
            {
                if (node.NextSibling != null)
                {
                    hfCanMove.Value = "true";
                    hfCanMove.RenderControl(e.Output);
                }
                else
                {
                    hfCanMove.Value = string.Empty;
                    hfCanMove.RenderControl(e.Output);
                }
            }
        }

        protected void tvCoursewareTypeChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            CoursewareTypeBLL objCoursewareType = new CoursewareTypeBLL();
            RailExam.Model.CoursewareType obj  = objCoursewareType.GetCoursewareType(int.Parse(e.Parameters[0]));
            int cout  = 0;
            if(obj != null)
            {
                cout = obj.ParentId == 0 ? tvCoursewareType.Nodes.Count : tvCoursewareType.FindNodeById(obj.ParentId.ToString()).Nodes.Count;
            }

            if (e.Parameters[1] == "MoveUp")
            {
                if (obj.OrderIndex <= cout && obj.OrderIndex >= 2)
                {
                    obj.OrderIndex--;
                    objCoursewareType.UpdateCoursewareType(obj);

                    obj = objCoursewareType.GetCoursewareType(int.Parse(tvCoursewareType.FindNodeById(e.Parameters[0]).PreviousSibling.ID));
                    obj.OrderIndex++;
                    objCoursewareType.UpdateCoursewareType(obj);
                }
            }
            if (e.Parameters[1] == "MoveDown")
            {
                if (obj.OrderIndex <= cout - 1 && obj.OrderIndex >= 1)
                {
                    obj.OrderIndex++;
                    objCoursewareType.UpdateCoursewareType(obj);

                    obj = objCoursewareType.GetCoursewareType(int.Parse(tvCoursewareType.FindNodeById(e.Parameters[0]).NextSibling.ID));
                    obj.OrderIndex--;
                    objCoursewareType.UpdateCoursewareType(obj);
                }
            }

            tvCoursewareType.Nodes.Clear();
            BindTree();

            tvCoursewareType.RenderControl(e.Output);
        }
    }
}
