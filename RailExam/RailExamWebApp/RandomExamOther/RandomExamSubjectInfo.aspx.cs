using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;


namespace RailExamWebApp.RandomExamOther
{
	public partial class RandomExamSubjectInfo : PageBase
	{
		private Hashtable _htBook = new Hashtable();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
				ViewState["mode"] = Request.QueryString.Get("mode");
				ViewState["startmode"] = Request.QueryString.Get("startmode");

				hfMode.Value = ViewState["mode"].ToString();

				if (ViewState["mode"].ToString() == "ReadOnly")
				{
					btnInput.Enabled = false;
				}

				string strId = Request.QueryString.Get("id");
				if (!string.IsNullOrEmpty(strId))
				{
					HfRandomExamid.Value = strId;
					RandomExamBLL randomExamBLL = new RandomExamBLL();
					RailExam.Model.RandomExam RandomExam = randomExamBLL.GetExam(int.Parse(strId));
					if (RandomExam != null)
					{
						txtPaperName.Text = RandomExam.ExamName;
					}

					ItemTypeBLL objTypeBll = new ItemTypeBLL();
					IList<ItemType> objTypeList = objTypeBll.GetItemTypes();
					foreach (ItemType objType in objTypeList)
					{
						if (RandomExam.IsComputerExam)
						{
							if (objType.ItemTypeId > PrjPub.ITEMTYPE_JUDGE)
							{
								continue;
							}
						}

						ListItem item = new ListItem();
						item.Text = objType.TypeName;
						item.Value = objType.ItemTypeId.ToString();
						lbType.Items.Add(item);
					}

					BindGrid();
				}
			}
			else
			{
				string strDeleteID = Request.Form.Get("DeleteID");
				if (!string.IsNullOrEmpty(strDeleteID))
				{
					int nID = Int32.Parse(strDeleteID);
					RandomExamSubjectBLL RandomExamSubjectBLL = new RandomExamSubjectBLL();
					RandomExamSubjectBLL.DeleteRandomExamSubject(nID);
					BindGrid();
				}
			}
		}

		private void GetHashTable()
		{
			string strId = Request.QueryString.Get("id");
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(strId));


			BookBLL bookBLL = new BookBLL();
			IList<RailExam.Model.Book> bookList = null;

			string strPostID = objRandomExam.PostID;
			string[] str = strPostID.Split(',');
			int orgID = PrjPub.CurrentLoginUser.OrgID;
			int leader = objRandomExam.IsGroupLeader;
			int techID = objRandomExam.TechnicianTypeID;

			PostBLL objPostBll = new PostBLL();
			Hashtable htBook = new Hashtable();
			for (int i = 0; i < str.Length; i++)
			{
				int postID = Convert.ToInt32(str[i]);
				IList<Post> objPostList = objPostBll.GetPostsByParentID(postID);
				if (objPostList.Count > 0)
				{
					continue;
				}

				bookList = bookBLL.GetEmployeeStudyBookInfoByKnowledgeID(-1, orgID, postID, leader, techID, 0);

				if (bookList.Count > 0)
				{
					foreach (RailExam.Model.Book book in bookList)
					{
						if (!htBook.ContainsKey(book.bookId))
						{
							htBook.Add(book.bookId, book.bookName);
						}
					}
				}
			}
			_htBook = htBook;
		}

		private int GetMaxNumByItemType(int itemTypeID)
		{
			int num = 0;
			ItemBLL objItemBll = new ItemBLL();
			foreach (DictionaryEntry entry in _htBook)
			{
				num = num + objItemBll.GetItemsByBookID(Convert.ToInt32(entry.Key), itemTypeID);
			}

			return num;
		}

		private void BindGrid()
		{
			RandomExamSubjectBLL objBll = new RandomExamSubjectBLL();
			IList<RandomExamSubject> objList =
				objBll.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(Request.QueryString.Get("id")));
            //GetHashTable();
            //foreach (RandomExamSubject subject in objList)
            //{
            //    subject.MaxItemCount = GetMaxNumByItemType(subject.ItemTypeId);
            //}

			Grid1.DataSource = objList;
			Grid1.DataBind();
		}

		protected void btnLast_Click(object sender, ImageClickEventArgs e)
		{
			string strId = Request.QueryString.Get("id");
			string strStartMode = ViewState["startmode"].ToString();
			string strFlag = "";

			if (ViewState["mode"].ToString() == "Insert")
			{
				strFlag = "Edit";
			}
			else
			{
				strFlag = ViewState["mode"].ToString();
			}

			Response.Redirect("RandomExamBasicInfo.aspx?startmode=" + strStartMode + "&mode=" + strFlag + "&id=" + strId);

		}

		protected void btnSaveAndNext_Click(object sender, ImageClickEventArgs e)
		{
			string strId = Request.QueryString.Get("id");
			string strMode = ViewState["mode"].ToString();
			string strStartMode = ViewState["startmode"].ToString();

			string strItemType = "";
			if (strMode != "ReadOnly")
			{
				if (Grid1.Rows.Count == 0)
				{
					SessionSet.PageMessage = "请选择大题！";
					return;
				}

				decimal totalScore = 0;
				IList<RandomExamSubject> paperStrategySubjects = new List<RandomExamSubject>();
				for (int i = 0; i < Grid1.Rows.Count; i++)
				{
					string strPaperStrategySubjectId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperStrategySubjectId")).Value;
					string strItemTypeId = ((HiddenField)Grid1.Rows[i].FindControl("hfItemTypeId")).Value;
					string strSubjectName = ((TextBox)Grid1.Rows[i].FindControl("txtSubjectName")).Text;
					string strUnitScore = ((TextBox)Grid1.Rows[i].FindControl("txtUnitScore")).Text;
					string strItemCount = ((TextBox)Grid1.Rows[i].FindControl("txtItemCount")).Text;

					if (strUnitScore == "")
					{
						strUnitScore = "0";
					}

					if (strItemCount == "")
					{
						strItemCount = "0";
					}


					totalScore += Convert.ToDecimal(strUnitScore);

					RandomExamSubject paperStrategySubject = new RandomExamSubject();

					paperStrategySubject.RandomExamSubjectId = int.Parse(strPaperStrategySubjectId);
					paperStrategySubject.RandomExamId = int.Parse(strId);
					paperStrategySubject.ItemCount = int.Parse(strItemCount);
					paperStrategySubject.ItemTypeId = int.Parse(strItemTypeId);
					paperStrategySubject.OrderIndex = 0;
					paperStrategySubject.Remark = "";
					paperStrategySubject.SubjectName = strSubjectName;
					paperStrategySubject.UnitScore = Convert.ToDecimal(strUnitScore);
					paperStrategySubject.TotalScore = Convert.ToDecimal(strUnitScore);
					paperStrategySubject.Memo = "";

					if(i == 0)
					{
						strItemType = strItemTypeId;
					}
					else
					{
						strItemType = strItemType + "|" + strItemTypeId;
					}

					paperStrategySubjects.Add(paperStrategySubject);
				}

				RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();
				paperStrategySubjectBLL.UpdateRandomExamSubject(paperStrategySubjects);
			}

			Response.Redirect("RandomExamStrategyInfo.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&itemType="+ strItemType+"&id=" + strId);
		}

		protected void btnCancel_Click(object sender, ImageClickEventArgs e)
		{
			//Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
			Response.Write("<script>top.returnValue='true';top.window.close();</script>");
		}

		protected void btnInput_Click(object sender, EventArgs e)
		{
			if (lbType.SelectedIndex < 0)
			{
				SessionSet.PageMessage = "请先选题型！";
				return;
			}
			int count = 0;

			string strId = Request.QueryString.Get("id");
			RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();

			decimal totalScore = 0;
			IList<RandomExamSubject> paperStrategySubjects = new List<RandomExamSubject>();
			for (int i = 0; i < Grid1.Rows.Count; i++)
			{
				string strPaperStrategySubjectId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperStrategySubjectId")).Value;
				string strItemTypeId = ((HiddenField)Grid1.Rows[i].FindControl("hfItemTypeId")).Value;
				string strSubjectName = ((TextBox)Grid1.Rows[i].FindControl("txtSubjectName")).Text;
				string strUnitScore = ((TextBox)Grid1.Rows[i].FindControl("txtUnitScore")).Text;
				string strItemCount = ((TextBox)Grid1.Rows[i].FindControl("txtItemCount")).Text;

				if (strItemTypeId == lbType.SelectedValue)
				{
					count = 1;
					break;
				}

				if (strUnitScore == "")
				{
					strUnitScore = "0";
				}

				if (strItemCount == "")
				{
					strItemCount = "0";
				}


				totalScore += Convert.ToDecimal(strUnitScore);

				RandomExamSubject paperStrategySubject = new RandomExamSubject();
				paperStrategySubject.RandomExamSubjectId = int.Parse(strPaperStrategySubjectId);
				paperStrategySubject.RandomExamId = int.Parse(strId);
				paperStrategySubject.ItemCount = int.Parse(strItemCount);
				paperStrategySubject.ItemTypeId = int.Parse(strItemTypeId);
				paperStrategySubject.OrderIndex = 0;
				paperStrategySubject.Remark = "";
				paperStrategySubject.SubjectName = strSubjectName;
				paperStrategySubject.UnitScore = Convert.ToDecimal(strUnitScore);
				paperStrategySubject.TotalScore = Convert.ToDecimal(strUnitScore);
				paperStrategySubject.Memo = "";
				paperStrategySubjects.Add(paperStrategySubject);
			}

			if(count == 1)
			{
				SessionSet.PageMessage = "您已经添加该题型！";
				return;
			}


			if (paperStrategySubjects.Count > 0)
			{
				paperStrategySubjectBLL.UpdateRandomExamSubject(paperStrategySubjects);
			}


			RandomExamSubject RandomStrategySubject = new RandomExamSubject();

			RandomStrategySubject.RandomExamId = int.Parse(strId);
			RandomStrategySubject.RandomExamSubjectId = Grid1.Rows.Count + 1;
			RandomStrategySubject.ItemTypeId = int.Parse(lbType.SelectedValue);
			RandomStrategySubject.TypeName = lbType.SelectedItem.Text;
			RandomStrategySubject.SubjectName = lbType.SelectedItem.Text;
			RandomStrategySubject.UnitScore = 0;
			RandomStrategySubject.TotalScore = 0;
			RandomStrategySubject.Memo = "";
			RandomStrategySubject.ItemCount = 10;
			RandomStrategySubject.OrderIndex = 0;
			RandomStrategySubject.Remark = "";

			paperStrategySubjectBLL.AddRandomExamSubject(RandomStrategySubject);

			BindGrid();
		}

		protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (ViewState["mode"].ToString() == "ReadOnly")
				{
					((TextBox)e.Row.FindControl("txtSubjectName")).Enabled = false;
					((TextBox)e.Row.FindControl("txtUnitScore")).Enabled = false;
					((TextBox)e.Row.FindControl("txtItemCount")).Enabled = false;
				}

				((Label)e.Row.FindControl("lblID")).Text = (e.Row.RowIndex + 1).ToString();
			}
		}
	}
}
