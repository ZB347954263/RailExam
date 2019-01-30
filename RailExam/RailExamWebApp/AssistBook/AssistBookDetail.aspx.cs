using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public partial class AssistBookDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strBookID = Request.QueryString.Get("id");
                ViewState["BookID"] = strBookID;

                hfMode.Value = Request.QueryString.Get("mode");

                if (strBookID != null && strBookID != "")
                {
                    FillPage(int.Parse(strBookID));

                    if (hfMode.Value == "ReadOnly")
                    {
                        SaveButton.Visible = false;
                        CancelButton.Visible = true;
                        SaveNextButton.Visible = false;
                        SaveExitButton.Visible = false;
                    }
                    else if (hfMode.Value == "Edit")
                    {
                        btnChapter.Visible = true;
                        SaveButton.Visible = true;
                        CancelButton.Visible = false;
                        SaveExitButton.Visible = false;
                        SaveNextButton.Visible = false;
                    }
                }
                else
                {
                    SaveButton.Visible = false;
                    SaveNextButton.Visible = true;
                    SaveExitButton.Visible = true;
                    CancelButton.Visible = false;
                    datePublishDate.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
                    string strKnowledgeID = Request.QueryString.Get("knowledgeId");
                    if (strKnowledgeID != null && strKnowledgeID != string.Empty)
                    {
                        hfKnowledgeID.Value = strKnowledgeID;
                        AssistBookCategoryBLL objBll = new AssistBookCategoryBLL();
                        RailExam.Model.AssistBookCategory obj = objBll.GetAssistBookCategory(Convert.ToInt32(strKnowledgeID));
                        txtKnowledgeName.Text = txtKnowledgeName.Text + GetCategoryName("/" + obj.AssistBookCategoryName, obj.ParentId);
                        ImgSelectKnowledge.Visible = false;
                    }
                    ArrayList objList = new ArrayList();
                    BindOrganizationTree(objList);
                    BindPostTree(objList);
                }
            }
        }

        private string GetCategoryName(string strName, int nID)
        {
            string str = "";

            if (nID != 0)
            {
                AssistBookCategoryBLL objBll = new AssistBookCategoryBLL();
                RailExam.Model.AssistBookCategory obj = objBll.GetAssistBookCategory(nID);

                if (obj.ParentId != 0)
                {
                    str = GetCategoryName("/" + obj.AssistBookCategoryName, obj.ParentId) + strName;
                }
                else
                {
                    str = obj.AssistBookCategoryName+ strName;
                }
            }
            return str;
        }

        private void FillPage(int nBookID)
        {
            ArrayList orgidAL = new ArrayList();
            ArrayList postidAL = new ArrayList();
            ArrayList trainTypeidAL = new ArrayList();

            AssistBookBLL bookBLL = new AssistBookBLL();

            RailExam.Model.AssistBook book = bookBLL.GetAssistBook(nBookID);

            if (book != null)
            {
                txtBookName.Text = book.BookName;
                txtKnowledgeName.Text = book.AssistBookCategoryName;
                hfKnowledgeID.Value = book.AssistBookCategoryId.ToString();
                txtTrainTypeName.Text = book.trainTypeNames;

                trainTypeidAL = book.trainTypeidAL;
                string strTrainTypeID = "";
                for (int i = 0; i < trainTypeidAL.Count; i++)
                {
                    strTrainTypeID += trainTypeidAL[i].ToString() + ",";
                }

                if (strTrainTypeID.Length > 0)
                {
                    strTrainTypeID = strTrainTypeID.Substring(0, strTrainTypeID.Length - 1);
                }

                hfTrainTypeID.Value = strTrainTypeID;

                txtBookNo.Text = book.BookNo;
                txtPublishOrgName.Text = book.PublishOrgName;
                hfPublishOrgID.Value = book.PublishOrg.ToString();

                datePublishDate.DateValue = book.PublishDate.ToString("yyyy-MM-dd");
                txtAuthors.Text = book.Authors;
                txtRevisers.Text = book.Revisers;
                txtBookMaker.Text = book.Bookmaker;
                txtCoverDesigner.Text = book.CoverDesigner;
                txtKeyWords.Text = book.KeyWords;
                txtPageCount.Text = book.PageCount.ToString();
                txtWordCount.Text = book.WordCount.ToString();
                txtDescription.Text = book.Description;
                string strUrl = string.Empty;
                if (!string.IsNullOrEmpty(book.url))
                {
                    strUrl = "../Online/AssistBook" + book.url.Substring(13, book.url.Length - 13);
                }

                hlUrl.Text = strUrl;
                hlUrl.NavigateUrl = strUrl;
                txtMemo.Text = book.Memo;
                ddlIsGroup.SelectedValue = book.IsGroupLearder.ToString();
                ddlTech.SelectedValue = book.TechnicianTypeID.ToString();
                hfOrderIndex.Value = book.OrderIndex.ToString();

                orgidAL = book.orgidAL;
                postidAL = book.postidAL;
            }

            BindOrganizationTree(orgidAL);
            BindPostTree(postidAL);
        }

        private void BindOrganizationTree(ArrayList orgidAL)
        {
            OrganizationBLL organizationBLL = new OrganizationBLL();
            IList<Organization> organizationList = organizationBLL.GetOrganizations();

            if (organizationList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (Organization organization in organizationList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = organization.OrganizationId.ToString();
                    tvn.Value = organization.OrganizationId.ToString();
                    tvn.Text = organization.ShortName;
                    tvn.ToolTip = organization.FullName;
                    tvn.ShowCheckBox = true;

                    if (orgidAL.Count > 0)
                    {
                        if (orgidAL.IndexOf(organization.OrganizationId) != -1)
                        {
                            tvn.Checked = true;
                        }
                    }

                    if (organization.ParentId == 0)
                    {
                        tvOrg.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvOrg.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvOrg.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            tvOrg.DataBind();
            //tvOrg.ExpandAll();
            if (tvOrg.Nodes.Count > 0)
            {
                tvOrg.Nodes[0].Expanded = true;
            }
        }

        private void BindPostTree(ArrayList postidAL)
        {
            PostBLL PostBLL = new PostBLL();
            IList<Post> postsList = PostBLL.GetPosts();

            if (postsList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (Post post in postsList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = post.PostId.ToString();
                    tvn.Value = post.PostId.ToString();
                    tvn.Text = post.PostName;
                    tvn.ToolTip = post.PostName;
                    tvn.ShowCheckBox = true;

                    if (postidAL.Count > 0)
                    {
                        if (postidAL.IndexOf(post.PostId) != -1)
                        {
                            tvn.Checked = true;
                        }
                    }

                    if (post.ParentId == 0)
                    {
                        tvPost.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvPost.FindNodeById(post.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvPost.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            tvPost.DataBind();
            //tvPost.ExpandAll();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.txtPageCount.Text == "")
            {
                this.txtPageCount.Text = "0";
            }

            if (this.txtWordCount.Text == "")
            {
                this.txtWordCount.Text = "0";
            }


            AssistBookBLL kBLL = new AssistBookBLL();
            //修改
            string strId = Request.QueryString.Get("id");

            RailExam.Model.AssistBook book = new RailExam.Model.AssistBook();

            book.AssistBookId = Convert.ToInt32(strId);
            book.Memo = txtMemo.Text;
            book.BookName = txtBookName.Text;
            book.Description = txtDescription.Text;
            book.PageCount = int.Parse(this.txtPageCount.Text);
            book.WordCount = int.Parse(this.txtWordCount.Text);
            book.Revisers = txtRevisers.Text;
            book.Authors = txtAuthors.Text;
            book.Bookmaker = txtBookMaker.Text;
            book.BookNo = txtBookNo.Text;
            book.CoverDesigner = txtCoverDesigner.Text;
            book.KeyWords = txtKeyWords.Text;
            book.AssistBookCategoryName = txtKnowledgeName.Text;
            book.AssistBookCategoryId = int.Parse(hfKnowledgeID.Value);
            book.PublishOrg = int.Parse(hfPublishOrgID.Value);
            book.PublishDate = DateTime.Parse(datePublishDate.DateValue.ToString());
            book.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
            book.IsGroupLearder = Convert.ToInt32(ddlIsGroup.SelectedValue);
            book.OrderIndex = Convert.ToInt32(hfOrderIndex.Value);
            if (hlUrl.Text != "")
            {
                book.url = "../AssistBook/" + strId + "/cover.htm";
            }


            ArrayList alTrainTypeID = new ArrayList();

            string strTrainTypeID = hfTrainTypeID.Value;

            if(strTrainTypeID != "")
            {
                string[] str1 = strTrainTypeID.Split(new char[] { ',' });

                for (int i = 0; i < str1.Length; i++)
                {
                    alTrainTypeID.Add(str1[i]);
                }
            }

            book.trainTypeidAL = alTrainTypeID;

            ArrayList al = new ArrayList();
            book.orgidAL = GetOrg(tvOrg.Nodes, al);

            ArrayList al1 = new ArrayList();

            foreach (TreeViewNode tn in tvPost.Nodes)
            {
                if (tn.Checked)
                {
                    al1.Add(tn.ID);
                }

                if (tn.Nodes.Count > 0)
                {
                    foreach (TreeViewNode tns in tn.Nodes)
                    {
                        if (tns.Checked)
                        {
                            al1.Add(tns.ID);
                        }

                        if (tns.Nodes.Count > 0)
                        {
                            foreach (TreeViewNode tnss in tns.Nodes)
                            {
                                if (tnss.Checked)
                                {
                                    al1.Add(tnss.ID);
                                }
                            }
                        }
                    }
                }
            }
            book.postidAL = al1;

            kBLL.UpdateAssistBook(book);

            SaveBookCover(strId);

            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
        }

        private void CopyTemplate(string srcPath, string aimPath)
        {
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }

            string[] fileList = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    CopyTemplate(file, aimPath + Path.GetFileName(file) + "\\");
                }
                else
                {
                    File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
        }

        private int SaveNewBook()
        {
            if (this.txtPageCount.Text == "")
            {
                this.txtPageCount.Text = "0";
            }

            if (this.txtWordCount.Text == "")
            {
                this.txtWordCount.Text = "0";
            }


            AssistBookBLL kBLL = new AssistBookBLL();

            string strId = Request.QueryString.Get("knowledgeId");

            RailExam.Model.AssistBook book = new RailExam.Model.AssistBook();

            book.Memo = txtMemo.Text;
            book.BookName = txtBookName.Text;
            book.Description = txtDescription.Text;
            book.PageCount = int.Parse(txtPageCount.Text);
            book.WordCount = int.Parse(txtWordCount.Text);
            book.Revisers = txtRevisers.Text;
            book.Authors = txtAuthors.Text;
            book.Bookmaker = txtBookMaker.Text;
            book.BookNo = txtBookNo.Text;
            book.CoverDesigner = txtCoverDesigner.Text;
            book.KeyWords = txtKeyWords.Text;
            book.AssistBookCategoryName = txtKnowledgeName.Text;
            book.AssistBookCategoryId = int.Parse(hfKnowledgeID.Value);
            book.PublishOrg = int.Parse(hfPublishOrgID.Value);
            book.PublishDate = DateTime.Parse(datePublishDate.DateValue.ToString());
            book.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
            book.IsGroupLearder = Convert.ToInt32(ddlIsGroup.SelectedValue);

            ArrayList alTrainTypeID = new ArrayList();

            string strTrainTypeID = hfTrainTypeID.Value;

            if (strTrainTypeID != "")
            {
                string[] str1 = strTrainTypeID.Split(new char[] { ',' });

                for (int i = 0; i < str1.Length; i++)
                {
                    alTrainTypeID.Add(str1[i]);
                }
            }

            book.trainTypeidAL = alTrainTypeID;

            ArrayList al = new ArrayList();
            book.orgidAL = GetOrg(tvOrg.Nodes, al);

            ArrayList al1 = new ArrayList();

            foreach (TreeViewNode tn in tvPost.Nodes)
            {
                if (tn.Checked)
                {
                    al1.Add(tn.ID);
                }

                if (tn.Nodes.Count > 0)
                {
                    foreach (TreeViewNode tns in tn.Nodes)
                    {
                        if (tns.Checked)
                        {
                            al1.Add(tns.ID);
                        }

                        if (tns.Nodes.Count > 0)
                        {
                            foreach (TreeViewNode tnss in tns.Nodes)
                            {
                                if (tnss.Checked)
                                {
                                    al1.Add(tnss.ID);
                                }
                            }

                        }

                    }
                }
            }


            book.postidAL = al1;

            string strNewID = kBLL.AddAssistBook(book).ToString();

            string strPath = Server.MapPath("../Online/AssistBook/" + strNewID);
            Directory.CreateDirectory(strPath);
            Directory.CreateDirectory(strPath + "/Upload");
            CopyTemplate(Server.MapPath("../Online/AssistBook/template/"), Server.MapPath("../Online/AssistBook/" + strNewID + "/"));

            SaveBookCover(strNewID);

            return Convert.ToInt32(strNewID);
        }

        protected void SaveExitButton_Click(object sender, EventArgs e)
        {
            SaveNewBook();
            Response.Write("<script>window.opener.frames['ifBookInfo'].form1.Refresh.value='true';window.opener.frames['ifBookInfo'].form1.submit();window.close();</script>");
        }

        protected void SaveNextButton_Click(object sender, EventArgs e)
        {
            int newID = SaveNewBook();
            ClientScript.RegisterStartupScript(GetType(), "1", "<script>SaveNext(" + newID + ");</script>", false);
        }

        private void SaveBookCover(string bookid)
        {
            string strBookUrl = "../AssistBook/" + bookid + "/cover.htm";
            AssistBookBLL objBill = new AssistBookBLL();
            objBill.UpdateAssistBookUrl(Convert.ToInt32(bookid), strBookUrl);

            string srcPath = "../Online/AssistBook/" + bookid + "/cover.htm";

            RailExam.Model.AssistBook obj = objBill.GetAssistBook(Convert.ToInt32(bookid));

            if (File.Exists(Server.MapPath(srcPath)))
            {
                File.Delete(Server.MapPath(srcPath));
            }

            string str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                         + "<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                         + "<br><br><br><br><br><br><br>"
                         + "<div id='booktitle'>" + obj.BookName + "</div>" + "<br>"
                         + "<br><br><br><br><br><br><br><br><br><br><br><br>"
                         + "<div id='orgtitle'>" + obj.PublishOrgName + "</div>"
                         + "</body>";

            File.AppendAllText(Server.MapPath(srcPath), str, System.Text.Encoding.UTF8);

            AssistBookChapterBLL objChapterBll = new AssistBookChapterBLL();
            objChapterBll.GetIndex(bookid);
        }

        private ArrayList GetOrg(TreeViewNodeCollection nodes, ArrayList al)
        {
            foreach (TreeViewNode tn in nodes)
            {
                if (tn.Checked)
                {
                    al.Add(tn.ID);
                }

                if (tn.Nodes.Count > 0)
                {
                    GetOrg(tn.Nodes, al);
                }
            }
            return al;
        }
    }
}
