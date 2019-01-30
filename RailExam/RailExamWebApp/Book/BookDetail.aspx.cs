using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Book
{
    public partial class BookDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}

            	hfIsWuhan.Value = PrjPub.IsWuhan().ToString();

                string strBookID = Request.QueryString.Get("id");
                ViewState["BookID"] = strBookID;

                hfMode.Value = Request.QueryString.Get("mode");

                if (strBookID != null && strBookID != "")
                {
                    if (PrjPub.CurrentLoginUser.RoleID == 1)
                    {
                        ImgSelectEmployee.Visible = true;
                    }
                    else
                    {
                        ImgSelectEmployee.Visible = false;
                    }

                    if (hfMode.Value == "ReadOnly")
                    {
                        SaveButton.Visible = false;
                        CancelButton.Visible = true;
                        SaveNextButton.Visible = false;
                        SaveExitButton.Visible = false;
                        //ClientScript.RegisterStartupScript(GetType(), "jsHide", "<script>document.all.ImgSelectKnowledge.style.display='none';</script>");
                        //Response.Write("<script>document.all.ImgSelectKnowledge.style.display='none';</script>");
                    }
                    else if (hfMode.Value == "Edit")
                    {
                        btnCover.Visible = false;
                        btnChapter.Visible = true;
                        SaveButton.Visible = true;
                        CancelButton.Visible = false;
                        SaveExitButton.Visible = false;
                        SaveNextButton.Visible = false;
                    }

                    FillPage(int.Parse(strBookID));
                }
                else
                {
                    if (PrjPub.CurrentLoginUser.RoleID != 1)
                    {
                        ImgSelectEmployee.Visible = false;
                    }
                    SaveButton.Visible = false;
                    SaveNextButton.Visible = true;
                    SaveExitButton.Visible = true;
                    CancelButton.Visible = false;
                    datePublishDate.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
                    string strKnowledgeID = Request.QueryString.Get("knowledgeId");
                    if(strKnowledgeID != null && strKnowledgeID != string.Empty)
                    {
                        hfKnowledgeID.Value = strKnowledgeID;
                        KnowledgeBLL objBll = new KnowledgeBLL();
                        RailExam.Model.Knowledge obj = objBll.GetKnowledge(Convert.ToInt32(strKnowledgeID));
                        txtKnowledgeName.Text = txtKnowledgeName.Text + GetKnowledgeName("/" + obj.KnowledgeName, obj.ParentId);
                        ImgSelectKnowledge.Visible = false;
                   }

				   ArrayList objOrgList = new ArrayList();
					if(PrjPub.CurrentLoginUser.SuitRange == 0 )
					{
						OrganizationBLL orgBll  = new OrganizationBLL();
						txtPublishOrgName.Text = orgBll.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID).ShortName;
						hfPublishOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();

						IList<Organization> objOrganizationList =
							orgBll.GetOrganizationsByWhereClause("ID_Path || '/' like '/1/" + PrjPub.CurrentLoginUser.StationOrgID + "/%' ");
						foreach (Organization organization in objOrganizationList)
						{
							objOrgList.Add(organization.OrganizationId);
						}
					}

                    txtAuthors.Text = PrjPub.CurrentLoginUser.EmployeeName;
                    hfEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();

                    //txtPublishOrgName.Text = PrjPub.CurrentLoginUser.OrgName;
                    //hfPublishOrgID.Value = PrjPub.CurrentLoginUser.OrgID.ToString();
					BindOrganizationTree(objOrgList);

					ArrayList objList = new ArrayList();
					if (!string.IsNullOrEmpty(Request.QueryString.Get("PostId")))
					{
						objList.Add(Convert.ToInt32(Request.QueryString.Get("PostId")));
					}
                    BindPostTree(objList);

                    ddlTech.SelectedValue = "1";
                }
            }

			if(!string.IsNullOrEmpty(hfTrainTypeID.Value))
			{
				TrainTypeBLL objBll = new TrainTypeBLL();
				string[] str = hfTrainTypeID.Value.Split(',');
				string strTypeName = "";
				for (int i = 0; i < str.Length; i++)
				{
					TrainType obj = objBll.GetTrainTypeInfo(Convert.ToInt32(str[i]));
					if(i == 0)
					{
						strTypeName = GetTrainTypeName("/" + obj.TypeName, obj.ParentID);
					}
					else
					{
						strTypeName = strTypeName + "," + GetTrainTypeName("/" + obj.TypeName, obj.ParentID);
					}
				}

				txtTrainTypeName.Text = strTypeName;
			}

			if(!string.IsNullOrEmpty(hfPublishOrgID.Value))
			{
				OrganizationBLL orgbll  = new OrganizationBLL();
				txtPublishOrgName.Text = orgbll.GetOrganization(Convert.ToInt32(hfPublishOrgID.Value)).ShortName;
			}

            string strRefresh = Request.Form.Get("RefreshCover");
            if (strRefresh != null && strRefresh != "")
            {
                //string strPath = "../Book/" + ViewState["BookID"].ToString() + "/cover.htm";

                //BookBLL objBill = new BookBLL();
                //objBill.UpdateBookUrl(Convert.ToInt32(ViewState["BookID"].ToString()), strPath);

                //string strBookName = objBill.GetBook(Convert.ToInt32(ViewState["BookID"].ToString())).bookName;

                //string srcPath = "../Online/Book/" + ViewState["BookID"].ToString() + "/Cover.htm";
                //string str = File.ReadAllText(Server.MapPath(srcPath), System.Text.Encoding.UTF8);
                //if (str.IndexOf("booktitle") < 0)
                //{
                //    str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                //         + "<div id='booktitle'>" + strBookName + "</div>" + "<br>"
                //         + str;
                //    File.WriteAllText(Server.MapPath(srcPath), str, System.Text.Encoding.UTF8);

                //BookChapterBLL objChapterBll = new BookChapterBLL();
                //objChapterBll.GetIndex(ViewState["BookID"].ToString());

                //SystemLogBLL objLogBll = new SystemLogBLL();
                //objLogBll.WriteLog("编辑教材《"　+　strBookName　+ "》前言");

                //FillPage(Convert.ToInt32(ViewState["BookID"].ToString()));
            }
        }


        private string GetKnowledgeName(string strName, int nID)
        {
            string str = strName;

            if (nID != 0)
            {
               
                KnowledgeBLL objBll = new KnowledgeBLL();
                RailExam.Model.Knowledge obj = objBll.GetKnowledge(nID);

                if (obj.ParentId != 0)
                {
                    str = GetKnowledgeName("/" + obj.KnowledgeName, obj.ParentId) + strName;
                }
                else
                {
                    str = obj.KnowledgeName + strName;
                }
            }

            return str;
        }

		private string GetTrainTypeName(string strName, int nID)
		{
            string str = strName;

			if (nID != 0)
			{
				TrainTypeBLL objBll = new TrainTypeBLL();
				RailExam.Model.TrainType obj = objBll.GetTrainTypeInfo(nID);

				if (obj.ParentID != 0)
				{
					str = GetTrainTypeName("/" + obj.TypeName, obj.ParentID) + strName;
				}
				else
				{
					str = obj.TypeName + strName;
				}
			}

			return str;
		}

        private void FillPage(int nBookID)
        {
            ArrayList orgidAL = new ArrayList();
            ArrayList postidAL = new ArrayList();
            ArrayList trainTypeidAL = new ArrayList();

            BookBLL bookBLL = new BookBLL();

            RailExam.Model.Book book = bookBLL.GetBook(nBookID);

            if (book != null)
            {
                txtBookName.Text = book.bookName;
            	ViewState["BookName"] = book.bookName;
                txtKnowledgeName.Text = book.KnowledgeNames;
                hfKnowledgeID.Value = book.knowledgeId.ToString();
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

                txtBookNo.Text = book.bookNo;
                txtPublishOrgName.Text = book.publishOrgName;
                hfPublishOrgID.Value = book.publishOrg.ToString();

                datePublishDate.DateValue = book.publishDate.ToString("yyyy-MM-dd");
                hfEmployeeID.Value = book.authors;
                if(!string.IsNullOrEmpty(hfEmployeeID.Value))
                {
                    OracleAccess db = new OracleAccess();
					DataTable dt=
                        db.RunSqlDataSet("Select Employee_Name from Employee where Employee_ID=" +
                                         hfEmployeeID.Value).Tables[0];
					if (dt != null && dt.Rows.Count > 0)
						txtAuthors.Text = dt.Rows[0]["Employee_Name"].ToString();


                    if(PrjPub.CurrentLoginUser.EmployeeID.ToString() !=  book.authors && PrjPub.CurrentLoginUser.RoleID !=1)
                    {
                        btnChapter.Visible = false;
                        SaveButton.Visible = false;
                        SaveExitButton.Visible = false;
                        SaveNextButton.Visible = false;
                    }
                }
                txtRevisers.Text = book.revisers;
                txtBookMaker.Text = book.bookmaker;
                txtCoverDesigner.Text = book.coverDesigner;
                txtKeyWords.Text = book.keyWords;
                txtPageCount.Text = book.pageCount.ToString();
                txtWordCount.Text = book.wordCount.ToString();
                txtDescription.Text = book.Description;
                hfOrderIndex.Value = book.OrderIndex.ToString();
                string strUrl = string.Empty;
                if (!string.IsNullOrEmpty(book.url))
                {
                    strUrl = "../Online/Book" + book.url.Substring(7, book.url.Length - 7);
                }

                hlUrl.Text = strUrl;
                hlUrl.NavigateUrl = strUrl;
                txtMemo.Text = book.Memo;
                ddlIsGroup.SelectedValue = book.IsGroupLearder.ToString();
                ddlTech.SelectedValue = book.TechnicianTypeID.ToString();

                orgidAL = book.orgidAL;
                postidAL = book.postidAL;
            }

            BindOrganizationTree(orgidAL);
            BindPostTree(postidAL);
        }

        private void BindOrganizationTree(ArrayList orgidAL)
        {
            OrganizationBLL organizationBLL = new OrganizationBLL();
			IList<Organization> organizationList  = new List<Organization>();
			if (PrjPub.CurrentLoginUser.SuitRange == 1)
			{
			    organizationList = organizationBLL.GetOrganizationsByLevel(2);
			}
			else
			{
                //if (string.IsNullOrEmpty(Request.QueryString.Get("id")))
                //{
                //    organizationList = organizationBLL.GetOrganizations(PrjPub.CurrentLoginUser.StationOrgID);
                //}
                //else
                //{
                //    string strSql = "select a.* from ("
                //                    + "select b.Org_ID,b.Level_Num,b.Order_Index from Book_Range_Org a "
                //                    + "inner join Org b on a.Org_ID=b.Org_ID "
                //                    + " where Book_ID=" + Request.QueryString.Get("id")
                //                    + " and GetStationOrgID(a.org_id) <> " + PrjPub.CurrentLoginUser.StationOrgID
                //                    + " union  "
                //                    + " select Org_ID,Level_Num,Order_Index "
                //                    + " from Org where GetStationOrgID(org_id)=" + PrjPub.CurrentLoginUser.StationOrgID
                //                    + ") a order by Level_Num,Order_Index";
                //    OracleAccess db = new OracleAccess();
                //    DataSet ds = db.RunSqlDataSet(strSql);

                //    foreach (DataRow dr in ds.Tables[0].Rows)
                //    {
                //        Organization organization = organizationBLL.GetOrganization(Convert.ToInt32(dr["Org_ID"]));
                //        organizationList.Add(organization);
                //    }
                //}

                //string strSql = "select  Org_ID,Level_Num,Order_Index from Org where Rail_System_ID=" +
                //                PrjPub.CurrentLoginUser.RailSystemID + " and level_num=2 order by Level_Num,Order_Index";
                string strSql = "select  Org_ID,Level_Num,Order_Index from Org where Org_ID=" +
                                PrjPub.CurrentLoginUser.StationOrgID;
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Organization organization = organizationBLL.GetOrganization(Convert.ToInt32(dr["Org_ID"]));
                    organizationList.Add(organization);
                }
			}

            if (organizationList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (Organization organization in organizationList)
                {
                    if (!organization.IsEffect)
                    {
                        continue;
                    }

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

					if (PrjPub.CurrentLoginUser.SuitRange == 1)
					{
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
					else
					{
						if (organization.ParentId == 1)
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
            }

            tvOrg.DataBind();
            //tvOrg.ExpandAll();
            if(tvOrg.Nodes.Count>0)
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


            BookBLL kBLL = new BookBLL();

			BookBLL objBookBll = new BookBLL();

			if(txtBookName.Text.Trim() != ViewState["BookName"].ToString())
			{
				if (objBookBll.GetBookByName(txtBookName.Text.Trim()).Count > 0)
				{
					SessionSet.PageMessage = "该教材名称已经存在";
					return;
				}
			}

            //修改
            string strId = Request.QueryString.Get("id");

            RailExam.Model.Book book = new RailExam.Model.Book();

            book.bookId = Convert.ToInt32(strId);
            book.Memo = txtMemo.Text;
            book.bookName = txtBookName.Text;
            book.Description = txtDescription.Text;
            book.pageCount = int.Parse(this.txtPageCount.Text);
            book.wordCount = int.Parse(this.txtWordCount.Text);
            book.revisers = txtRevisers.Text;
            book.authors = hfEmployeeID.Value;
            book.bookmaker = txtBookMaker.Text;
            book.bookNo = txtBookNo.Text;
            book.coverDesigner = txtCoverDesigner.Text;
            book.keyWords = txtKeyWords.Text;
            book.knowledgeName = txtKnowledgeName.Text;
            book.knowledgeId = int.Parse(hfKnowledgeID.Value);
            book.publishOrg = int.Parse(hfPublishOrgID.Value);
            book.publishDate = DateTime.Parse(datePublishDate.DateValue.ToString());
            book.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
            book.IsGroupLearder = Convert.ToInt32(ddlIsGroup.SelectedValue);
            book.OrderIndex = Convert.ToInt32(hfOrderIndex.Value);
            if (hlUrl.Text != "")
            {
                book.url = "../Book/" + strId + "/cover.htm";
            }


            ArrayList alTrainTypeID = new ArrayList();

            string strTrainTypeID = hfTrainTypeID.Value;

            string[] str1 = strTrainTypeID.Split(new char[] { ',' });

            for (int i = 0; i < str1.Length; i++)
            {
                if (!string.IsNullOrEmpty(str1[i]))
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

            kBLL.UpdateBook(book);
            kBLL.UpdateBookVersion(Convert.ToInt32(strId));
            SaveBookCover(strId);

			if (PrjPub.IsWuhan())
			{
				Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
			}
			else
			{
				Response.Write("<script>top.returnValue='true';top.close();</script>");
			}
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


            BookBLL kBLL = new BookBLL();

            string strId = Request.QueryString.Get("knowledgeId");

            RailExam.Model.Book book = new RailExam.Model.Book();

            book.Memo = txtMemo.Text;
            if(PrjPub.CurrentLoginUser.SuitRange ==1)
            {
                book.bookName = txtBookName.Text;
            }
            else
            {
                OrganizationBLL orgBll  = new OrganizationBLL();
                string orgName = orgBll.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID).ShortName;
                book.bookName = orgName + txtBookName.Text;
            }
            book.Description = txtDescription.Text;
            book.pageCount = int.Parse(txtPageCount.Text);
            book.wordCount = int.Parse(txtWordCount.Text);
            book.revisers = txtRevisers.Text;
            book.authors = hfEmployeeID.Value;
            book.bookmaker = txtBookMaker.Text;
            book.bookNo = txtBookNo.Text;
            book.coverDesigner = txtCoverDesigner.Text;
            book.keyWords = txtKeyWords.Text;
            book.knowledgeName = txtKnowledgeName.Text;
            book.knowledgeId = int.Parse(hfKnowledgeID.Value);
            book.publishOrg = int.Parse( hfPublishOrgID.Value);
            book.publishDate = DateTime.Parse(datePublishDate.DateValue.ToString());
            book.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
            book.IsGroupLearder = Convert.ToInt32(ddlIsGroup.SelectedValue);

            ArrayList alTrainTypeID = new ArrayList();

            string strTrainTypeID = hfTrainTypeID.Value;

            string[] str1 = strTrainTypeID.Split(new char[] { ',' });

            for (int i = 0; i < str1.Length; i++)
            {
                if(!string.IsNullOrEmpty(str1[i]))
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

            string strNewID = kBLL.AddBook(book).ToString();

            string strPath = Server.MapPath("../Online/Book/" + strNewID);
            Directory.CreateDirectory(strPath);
            Directory.CreateDirectory(strPath + "/Upload");
            CopyTemplate(Server.MapPath("../Online/Book/template/"), Server.MapPath("../Online/Book/" + strNewID + "/"));

            SaveBookCover(strNewID);

            return Convert.ToInt32(strNewID);
        }

        protected void SaveExitButton_Click(object sender, EventArgs e)
        {
			BookBLL objBookBll = new BookBLL();

			if (objBookBll.GetBookByName(txtBookName.Text.Trim()).Count > 0)
			{
				SessionSet.PageMessage = "该教材名称已经存在";
				return;
			}

            SaveNewBook();

			Response.Write("<script>window.opener.frames['ifBookInfo'].form1.Refresh.value='true';window.opener.frames['ifBookInfo'].form1.submit();window.close();</script>");
        }

        protected void SaveNextButton_Click(object sender, EventArgs e)
        {
			BookBLL objBookBll = new BookBLL();

			if(objBookBll.GetBookByName(txtBookName.Text.Trim()).Count > 0)
			{
				SessionSet.PageMessage = "该教材名称已经存在";
				return;
			}

            int newID = SaveNewBook();
            ClientScript.RegisterStartupScript(GetType(), "1", "<script>SaveNext(" + newID + ");</script>", false);
        }

        private void SaveBookCover(string bookid)
        {
                string strBookUrl = "../Book/" + bookid + "/cover.htm";
                BookBLL objBill = new BookBLL();
                objBill.UpdateBookUrl(Convert.ToInt32(bookid), strBookUrl);

                string srcPath = "../Online/Book/" + bookid + "/cover.htm";

                RailExam.Model.Book obj = objBill.GetBook(Convert.ToInt32(bookid));

                if (File.Exists(Server.MapPath(srcPath)))
                {
                    File.Delete(Server.MapPath(srcPath));
                }

            string str = "<link href='book.css' type='text/css' rel='stylesheet' />"
                         +"<body oncontextmenu='return false' ondragstart='return false' onselectstart ='return false' oncopy='document.selection.empty()' onbeforecopy='return false'>"
                         + "<br><br><br><br><br><br><br>"
                         + "<div id='booktitle'>" + obj.bookName + "</div>" + "<br>"
						 + "<br><br><br><br><br><br><br><br><br><br><br>"
						 + "<div id='orgtitle'>" + obj.publishOrgName + "</div>" + "<br>"
						 + "<div id='authortitle'></div>"
                         + "</body>";

                File.AppendAllText(Server.MapPath(srcPath), str, System.Text.Encoding.UTF8);

                BookChapterBLL objChapterBll = new BookChapterBLL();
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
