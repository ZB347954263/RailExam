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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class PostDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvPost.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvPost.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvPost.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }


            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != "" && strDeleteID != null)
            {
                PostBLL objBll = new PostBLL();
                string strParentID = objBll.GetPost(Convert.ToInt32(strDeleteID)).ParentId.ToString();
                int code = 0;
                objBll.DeletePost(Convert.ToInt32(strDeleteID), ref code);

                if (code != 0)//code=2292
                {
                    SessionSet.PageMessage = "该工作岗位已被引用，不能删除！";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"window.parent.tvPostChangeCallBack.callback('Delete','" + strParentID + @"', 'Rebuild');                        
                            if(window.parent.tvPost.get_nodes().get_length() > 0)
                            {
                                window.parent.tvPost.get_nodes().getNode(0).select();
                            }",
                        true);
                }
            }
        }

        protected void fvPost_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if (e.AffectedRows == 0)
            {
                return;
            }

            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvPostChangeCallBack.callback('Insert', 'Rebuild');                        
            if(window.parent.tvPost.get_nodes().get_length() > 0)
            {
                window.parent.tvPost.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvPost_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            if (e.AffectedRows == 0)
            {
                return;
            }

            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvPostChangeCallBack.callback(" + e.Keys["PostId"] + @", 'Rebuild');                        
            if(window.parent.tvPost.get_nodes().get_length() > 0)
            {
                window.parent.tvPost.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void odsPostDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            RailExam.Model.Post objPost = (RailExam.Model.Post)e.InputParameters[0];
            PostBLL objPostBll = new PostBLL();
            IList<RailExam.Model.Post> posts = objPostBll.GetPostsByWhereClause("Post_ID=" + objPost.ParentId);
            if (posts[0].PostLevel + 1 == 3)
            {
                IList<RailExam.Model.Post> objPostList =
                        objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + objPost.PostName + "'");
                if (objPostList.Count > 0)
                {
                    SessionSet.PageMessage = "该职名已经在系统中存在！";
                    e.Cancel = true;
                    return;
                }
            }
        }

        protected void odsPostDetail_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            RailExam.Model.Post objPost = (RailExam.Model.Post)e.InputParameters[0];
            if (objPost.PostLevel == 3)
            {
                PostBLL objPostBll = new PostBLL();
                IList<RailExam.Model.Post> objPostList =
                        objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + objPost.PostName + "' and Post_ID!=" + objPost.PostId);
                if (objPostList.Count > 0)
                {
                    SessionSet.PageMessage = "该职名已经在系统中存在！";
                    e.Cancel = true;
                    return;
                }
            }
        }

        //一专多能编辑
        protected void btnPromotionPostIDEdit_Click(object sender, EventArgs e)
        {
            //Button btn=sender as Button;
            //TextBox txt = btn.Parent.FindControl("txtPromotionPostID") as TextBox;
            //string ids=this.hfPromotionPostID.Value;
            //string sql = String.Format("select POST_NAME from POST where POST_ID in ({0})",ids);
            //DataSet ds;
            //OracleAccess ora = new OracleAccess();
            //ds = ora.RunSqlDataSet(sql);
            //if (ds.Tables.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        txt.Text += ds.Tables[0].Rows[i][0].ToString()+", ";
            //    }
            //    txt.Text = txt.Text.Substring(0, txt.Text.Length - 2);
            //}
        }

        protected string ShowPostName(string ids)
        {
            string postName = String.Empty;
            if (!String.IsNullOrEmpty(ids))
            {
                OracleAccess oa = new OracleAccess();
                string sql = String.Format("select POST_NAME from POST where POST_ID in ({0})", ids);
                DataSet ds = oa.RunSqlDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        postName += String.IsNullOrEmpty(postName) ?
                            ds.Tables[0].Rows[i][0].ToString() : ", " + ds.Tables[0].Rows[i][0].ToString();
                    }
                }
            }
            
            return postName;
        }
    }
}
