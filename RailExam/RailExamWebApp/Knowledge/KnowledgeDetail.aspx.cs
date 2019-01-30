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
using DSunSoft.Web.Global;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Knowledge
{
    public partial class KnowledgeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvKnowledge.CurrentMode == FormViewMode.Insert)
            {
                if(hfInsert.Value == "-1")
                {
                    ((HiddenField)fvKnowledge.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvKnowledge.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != "" && strDeleteID != null)
            {
                KnowledgeBLL objBll = new KnowledgeBLL();
            	string strParentID = objBll.GetKnowledge(Convert.ToInt32(strDeleteID)).ParentId.ToString();
                int code = 0;
                objBll.DeleteKnowledge(Convert.ToInt32(strDeleteID), ref code);

                if (code != 0)//code=2292
                {
					SessionSet.PageMessage = "该知识体系已被引用，不能删除！";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(),
                    "jsSelectFirstNode",
					@"window.parent.tvKnowledgeChangeCallBack.callback('Delete','"+ strParentID + @"', 'Rebuild');
                            if(window.parent.tvKnowledge.get_nodes().get_length() > 0)
                            {
                                window.parent.tvKnowledge.get_nodes().getNode(0).select();
                            }",
                    true);
                }
            }
        }

        protected void fvKnowledge_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            string strId = e.Keys["KnowledgeId"].ToString();

            bool isTemplate = Convert.ToBoolean(e.NewValues["IsTemplate"]);

            KnowledgeBLL knowledgeBll = new KnowledgeBLL();

            if(isTemplate)
            {
                IList<RailExam.Model.Knowledge> objChildList = knowledgeBll.GetKnowledgesByParentID(Convert.ToInt32(strId));
                if (objChildList.Count>0)
                {
                    SessionSet.PageMessage = "一专多能类别只能为知识体系叶子节点！";
                    e.Cancel = true;
                    ClientScript.RegisterStartupScript(GetType(),"jsSelectFirstNode",@"setImageBtnVisiblityUpdate()",true);
                    return;
                }

                IList<RailExam.Model.Knowledge> objTemplate = knowledgeBll.GetKnowledgesByWhereClause("Is_Template=1 and Knowledge_ID!=" + strId, "Knowledge_ID");
                if (objTemplate.Count > 0)
                {
                    SessionSet.PageMessage = "知识体系中已经存在名为【" + objTemplate[0].KnowledgeName + "】的一专多能类别！";
                    e.Cancel = true;
                    ClientScript.RegisterStartupScript(GetType(),"jsSelectFirstNode", @"setImageBtnVisiblityUpdate()",true);
                    return;
                }
            }


        }

        protected void fvKnowledge_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            bool isTemplate = Convert.ToBoolean(e.Values["IsTemplate"]);

            KnowledgeBLL knowledgeBll = new KnowledgeBLL();

            if (isTemplate)
            {
                IList<RailExam.Model.Knowledge> objTemplate = knowledgeBll.GetKnowledgesByWhereClause("Is_Template=1", "Knowledge_ID");
                if (objTemplate.Count > 0)
                {
                    SessionSet.PageMessage = "知识体系中已经存在名为【" + objTemplate[0].KnowledgeName + "】的一专多能类别！";
                    e.Cancel = true;
                    ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode",@"setImageBtnVisiblityInsert()",true);
                    return;
                }
            }
        }

        protected void fvKnowledge_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvKnowledgeChangeCallBack.callback('Insert', 'Rebuild');
            if(window.parent.tvKnowledge.get_nodes().get_length() > 0)
            {
                window.parent.tvKnowledge.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvKnowledge_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvKnowledgeChangeCallBack.callback('Update'," + e.Keys["KnowledgeId"] + @", 'Rebuild');
            if(window.parent.tvKnowledge.get_nodes().get_length() > 0)
            {
                window.parent.tvKnowledge.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}