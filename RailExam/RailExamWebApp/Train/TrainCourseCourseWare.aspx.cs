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
using DSunSoft.Web.UI;

namespace RailExamWebApp.Train
{
    public partial class TrainCourseCourseWare : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["StandardID"] = Request.QueryString.Get("StandardID");
                ViewState["CourseID"] = Request.QueryString.Get("CourseID");
                ViewState["CourseName"] = Request.QueryString.Get("CourseName");
                //ViewState["CourseID"] = "1";
                BindWareTree();
                BindChooseWareTree();
                lblTitle2.Text = Request.QueryString.Get("CourseName") + "课程课件";
                lblTitle1.Text = "课件信息";
            }
        }

        private void BindWareTree()
        {
            tvWare.Nodes.Clear();
            CoursewareTypeBLL coursewareTypeBll = new CoursewareTypeBLL();
            IList<CoursewareType> coursewareTypeList = coursewareTypeBll.GetCoursewareTypes(0, 0, "", 0, 0,
                                                                                    "", "", "", 0, 40,
                                                                                    "LevelNum,OrderIndex Asc");
            ArrayList objList = GetCourseWareList();

            if (coursewareTypeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (CoursewareType coursewareType in coursewareTypeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = coursewareType.CoursewareTypeId.ToString();
                    tvn.Value = coursewareType.CoursewareTypeId.ToString();
                    tvn.Text = coursewareType.CoursewareTypeName;
                    tvn.ToolTip = coursewareType.CoursewareTypeName;

                    if (coursewareType.ParentId == 0)
                    {
                        tvWare.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvWare.FindNodeById(coursewareType.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvWare.Nodes.Clear();
                            Response.Write("数据错误！");
                            return;
                        }
                    }
                }
            }
            tvWare.DataBind();

            foreach (TreeViewNode node1 in tvWare.Nodes)
            {
                if (node1.Nodes.Count > 0)
                {
                    foreach (TreeViewNode node in node1.Nodes)
                    {
                        CoursewareBLL coursewareBll = new CoursewareBLL();
                        IList<RailExam.Model.Courseware> coursewareList = null;
                        //coursewareBll.GetCoursewares(Convert.ToString(node.Value), -1);

                        if (coursewareList.Count > 0)
                        {
                            foreach (RailExam.Model.Courseware  courseware in coursewareList)
                            {
                                TreeViewNode tvn = new TreeViewNode();
                                tvn.ID = courseware.CoursewareTypeID.ToString();
                                tvn.Value = courseware.CoursewareID.ToString();
                                tvn.Text = courseware.CoursewareName;
                                tvn.ToolTip = courseware.CoursewareName;
                                tvn.ShowCheckBox = true;

                                if (objList.Count > 0)
                                {
                                    if (objList.IndexOf(courseware.CoursewareID.ToString()) != -1)
                                    {
                                        tvn.Checked = true;
                                    }
                                }

                                tvWare.FindNodeById(node.ID.ToString()).Nodes.Add(tvn);
                            }
                        }
                    }
                }
            }
            tvWare.DataBind();

            tvWare.ExpandAll();
        }

        protected void btnChoose_Click(object sender, EventArgs e)
        {
            AddTrainCourseCourseware(tvWare.Nodes);
            BindChooseWareTree();
        }

        private void AddTrainCourseCourseware(TreeViewNodeCollection tvNodes)
        {
            ArrayList objList = GetCourseWareList();
            foreach (TreeViewNode node in tvNodes)
            {
                if (node.ShowCheckBox == true)
                {
                    if (node.Checked == true)
                    {
                        if (objList.IndexOf(node.Value) == -1)
                        {
                            TrainCourseCoursewareBLL obj = new TrainCourseCoursewareBLL();
                            TrainCourseCourseware objTrainCourseCourseware = new TrainCourseCourseware();

                            objTrainCourseCourseware.CourseID = Convert.ToInt32(ViewState["CourseID"].ToString());
                            objTrainCourseCourseware.CoursewareID = Convert.ToInt32(node.Value);

                            obj.AddTrainCourseCourseware(objTrainCourseCourseware);
                        }
                    }
                }

                if (node.Nodes.Count > 0)
                {
                    AddTrainCourseCourseware(node.Nodes);
                }
            }
        }

        /// <summary>
        /// 获取当前课程中教材章节ID的列表

        /// </summary>
        /// <returns></returns>
        private ArrayList GetCourseWareList()
        {
            ArrayList objList = new ArrayList();

            TrainCourseCoursewareBLL objTrainCourseCoursewareBll = new TrainCourseCoursewareBLL();
            IList<TrainCourseCourseware> objTrainCourseCoursewareList =
                objTrainCourseCoursewareBll.GetTrainCourseCoursewareByCourseID(Convert.ToInt32(ViewState["CourseID"].ToString()));

            if (objTrainCourseCoursewareList.Count > 0)
            {
                foreach (TrainCourseCourseware objTrainCourseCourseware in objTrainCourseCoursewareList)
                {
                    objList.Add(objTrainCourseCourseware.CoursewareID.ToString());
                }
            }
            return objList;
        }

        private void BindChooseWareTree()
        {
            tvChooseWare.Nodes.Clear();
            CoursewareTypeBLL coursewareTypeBll = new CoursewareTypeBLL();
            IList<CoursewareType> coursewareTypeList =
                coursewareTypeBll.GetCoursewareTypesByCourseID(Convert.ToInt32(ViewState["CourseID"].ToString()));

            if (coursewareTypeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (CoursewareType coursewareType in coursewareTypeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = "t" + coursewareType.CoursewareTypeId.ToString();
                    tvn.Value = "0";
                    tvn.Text = coursewareType.CoursewareTypeName;
                    tvn.ToolTip = coursewareType.CoursewareTypeName;

                    if (coursewareType.ParentId == 0)
                    {
                        tvChooseWare.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvChooseWare.FindNodeById("t" + coursewareType.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvChooseWare.Nodes.Clear();
                            Response.Write("数据错误！");
                            return;
                        }
                    }
                }
            }

            foreach (TreeViewNode node1 in tvChooseWare.Nodes)
            {
                if (node1.Nodes.Count > 0)
                {
                    foreach (TreeViewNode node in node1.Nodes)
                    {
                        string strID = node.ID.Replace("t", "").Trim();
                        TrainCourseCoursewareBLL objTrainCourseWareBll = new TrainCourseCoursewareBLL();
                        IList<TrainCourseCourseware> objTrainCourseWareList =
                            objTrainCourseWareBll.GetTrainCourseCoursewareByTypeID(Convert.ToInt32(ViewState["CourseID"].ToString()), Convert.ToInt32(strID));

                        if (objTrainCourseWareList.Count > 0)
                        {
                            foreach (TrainCourseCourseware courseware in objTrainCourseWareList)
                            {
                                TreeViewNode tvn = new TreeViewNode();
                                tvn.ID = courseware.CoursewareTypeID.ToString();
                                tvn.Value = courseware.TrainCourseCoursewareID.ToString();
                                tvn.Text = courseware.CoursewareName;
                                tvn.ToolTip = courseware.CoursewareName;
                                tvn.ShowCheckBox = true;

                                tvChooseWare.FindNodeById("t" + courseware.CoursewareTypeID.ToString()).Nodes.Add(tvn);
                            }
                        }
                    }
                }
            }
            tvChooseWare.DataBind();

            tvChooseWare.ExpandAll();
        }

        protected void tvChooseWareChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            tvChooseWare.Nodes.Clear();
            BindChooseWareTree();
            tvChooseWare.RenderControl(e.Output);
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("StandardID") != null && Request.QueryString.Get("StandardID") != "")
            {
                Response.Redirect("TrainCourseBook.aspx?CourseID=" + ViewState["CourseID"].ToString() + "&CourseName=" +
                                  ViewState["CourseName"].ToString() + "&StandardID=" + ViewState["StandardID"].ToString());
            }
            else
            {
                Response.Redirect("TrainCourseBook.aspx?CourseID=" + ViewState["CourseID"].ToString() + "&CourseName=" +
                                  ViewState["CourseName"].ToString());
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true' ;window.opener.form1.submit();window.close();</script>");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            DelTrainCourseCourseware(tvChooseWare.Nodes);
            BindWareTree();
            BindChooseWareTree();
        }

        private void DelTrainCourseCourseware(TreeViewNodeCollection tvNodes)
        {
            foreach (TreeViewNode node in tvNodes)
            {
                if (node.ShowCheckBox == true)
                {
                    if (node.Checked == true)
                    {
                        TrainCourseCoursewareBLL obj = new TrainCourseCoursewareBLL();
                        obj.DeleteTrainCourseCourseware(Convert.ToInt32(node.Value));
                    }
                }
                if (node.Nodes.Count > 0)
                {
                    DelTrainCourseCourseware(node.Nodes);
                }
            }
        }
    }
}