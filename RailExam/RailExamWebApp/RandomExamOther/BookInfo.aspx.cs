using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;

namespace RailExamWebApp.RandomExamOther
{
	public partial class BookInfo : PageBase
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

				//ViewState["NowID"] = "false";
				if (PrjPub.HasEditRight("教材管理") && PrjPub.IsServerCenter)//&& PrjPub.CurrentLoginUser.SuitRange == 1
				{
					HfUpdateRight.Value = "True";
				}
				else
				{
					HfUpdateRight.Value = "False";
				}
				if (PrjPub.HasDeleteRight("教材管理") && PrjPub.IsServerCenter)//&& PrjPub.CurrentLoginUser.SuitRange == 1
				{
					HfDeleteRight.Value = "True";
				}
				else
				{
					HfDeleteRight.Value = "False";
				}

				OrganizationBLL orgBll = new OrganizationBLL();
				int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);
				HfOrgId.Value = orgID.ToString();

				BindGrid();

			}
			else
			{
				if (Request.Form.Get("Refresh") == "true")
				{
					//ViewState["NowID"] = "false";
					BindGrid();
				}
			}

			string strDeleteID = Request.Form.Get("DeleteID");
			if (strDeleteID != null && strDeleteID != "")
			{
				DelBook(strDeleteID);
				BindGrid();
			}

			string strUpID = Request.Form.Get("UpID");
			if (strUpID != null && strUpID != "")
			{
				if (Request.QueryString.Get("id") != null)
				{
					BookBLL objBll = new BookBLL();
					RailExam.Model.Book obj = objBll.GetBook(Convert.ToInt32(strUpID));
					obj.OrderIndex = obj.OrderIndex - 1;
					objBll.UpdateBook(obj);
				}

				if (Request.QueryString.Get("id1") != null)
				{
					int trainTypeID = Convert.ToInt32(Request.QueryString.Get("id1"));
					BookTrainTypeBLL objTrainTypeBll = new BookTrainTypeBLL();
					BookTrainType objTrainType =
						objTrainTypeBll.GetBookTrainType(Convert.ToInt32(strUpID), trainTypeID);
					objTrainType.OrderIndex = objTrainType.OrderIndex - 1;
					objTrainTypeBll.UpdateBookTrainType(objTrainType);
				}
				BindGrid();
			}

			string strDownID = Request.Form.Get("DownID");
			if (strDownID != null && strDownID != "")
			{
				if (Request.QueryString.Get("id") != null)
				{
					BookBLL objBll = new BookBLL();
					RailExam.Model.Book obj = objBll.GetBook(Convert.ToInt32(strDownID));
					obj.OrderIndex = obj.OrderIndex + 1;
					objBll.UpdateBook(obj);
				}

				if (Request.QueryString.Get("id1") != null)
				{
					int trainTypeID = Convert.ToInt32(Request.QueryString.Get("id1"));
					BookTrainTypeBLL objTrainTypeBll = new BookTrainTypeBLL();
					BookTrainType objTrainType =
						objTrainTypeBll.GetBookTrainType(Convert.ToInt32(strDownID), trainTypeID);
					objTrainType.OrderIndex = objTrainType.OrderIndex + 1;
					objTrainTypeBll.UpdateBookTrainType(objTrainType);
				}
				BindGrid();
			}
		}

		private void DelBook(string strID)
		{
			ItemBLL objItemBll = new ItemBLL();
			objItemBll.UpdateItemEnabled(Convert.ToInt32(strID), 0, 2);

			BookBLL objBll = new BookBLL();
			objBll.DeleteBook(Convert.ToInt32(strID));
		}

		private void BindGrid()
		{
			BookBLL bookBLL = new BookBLL();
			IList<RailExam.Model.Book> books = new List<RailExam.Model.Book>();

			OrganizationBLL orgBll = new OrganizationBLL();
			int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);

			string strKnowledgeIDPath = Request.QueryString.Get("id");
			if (!string.IsNullOrEmpty(strKnowledgeIDPath))
			{
				if (strKnowledgeIDPath != "0")
				{
					books = bookBLL.GetBookByKnowledgeID(Convert.ToInt32(strKnowledgeIDPath), orgID);
				}
				else
				{
					if (PrjPub.CurrentLoginUser.SuitRange == 1)
					{
						books = bookBLL.GetAllBookInfo(0);
					}
					else
					{
						books = bookBLL.GetAllBookInfo(orgID);
					}
				}
			}

			string strTrainTypeIDPath = Request.QueryString.Get("id1");
			if (!string.IsNullOrEmpty(strTrainTypeIDPath))
			{
				if (strTrainTypeIDPath != "0")
				{
					books = bookBLL.GetBookByTrainTypeID(Convert.ToInt32(strTrainTypeIDPath), orgID);
				}
				else
				{
					if (PrjPub.CurrentLoginUser.SuitRange == 1)
					{
						books = bookBLL.GetAllBookInfo(0);
					}
					else
					{
						books = bookBLL.GetAllBookInfo(orgID);
					}
				}
			}

			string strPostID = Request.QueryString.Get("id2");
			if (!string.IsNullOrEmpty(strPostID))
			{
				if (strPostID != "0")
				{
					books = bookBLL.GetBookByPostID(Convert.ToInt32(strPostID), orgID);
				}
				else
				{
					if (PrjPub.CurrentLoginUser.SuitRange == 1)
					{
						books = bookBLL.GetAllBookInfo(0);
					}
					else
					{
						books = bookBLL.GetAllBookInfo(orgID);
					}
				}
			}

			if (books.Count > 0)
			{
				foreach (RailExam.Model.Book book in books)
				{
					if (book.bookName.Length <= 15)
					{
						book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
					}
					else
					{
						book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 15) + "...</a>";
					}
				}
				Grid1.DataSource = books;
				Grid1.DataBind();
			}
		}

		protected void btnQuery_Click(object sender, EventArgs e)
		{
			BookBLL bookBLL = new BookBLL();
			IList<RailExam.Model.Book> books = new List<RailExam.Model.Book>();

			OrganizationBLL orgBll = new OrganizationBLL();
			int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);

			string strKnowledgeID = Request.QueryString.Get("id");

			if (!string.IsNullOrEmpty(strKnowledgeID))
			{
				if (strKnowledgeID != "0")
				{
					string[] str1 = strKnowledgeID.Split(new char[] { '/' });
					int nKnowledgeId = int.Parse(str1[str1.LongLength - 1].ToString());
					books = bookBLL.GetBookByKnowledgeID(nKnowledgeId, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
				}
				else
				{
					books = bookBLL.GetBookByKnowledgeID(0, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
				}
			}

			string strTrainTypeID = Request.QueryString.Get("id1");
			if (!string.IsNullOrEmpty(strTrainTypeID))
			{
				if (strTrainTypeID != "0")
				{
					string[] str2 = strTrainTypeID.Split(new char[] { '/' });
					int nTrainTypeID = int.Parse(str2[str2.LongLength - 1].ToString());
					books = bookBLL.GetBookByTrainTypeID(nTrainTypeID, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
				}
				else
				{
					books = bookBLL.GetBookByTrainTypeID(0, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
				}
			}

			string strPostID = Request.QueryString.Get("id2");
			if (!string.IsNullOrEmpty(strPostID))
			{
				if (strPostID != "0")
				{
					int nPostID = int.Parse(strPostID);
					books = bookBLL.GetBookByPostID(nPostID, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
				}
				else
				{
					books = bookBLL.GetBookByPostID(0, txtBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
				}
			}

			if (books != null)
			{
				foreach (RailExam.Model.Book book in books)
				{
					if (book.bookName.Length <= 15)
					{
						book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
					}
					else
					{
						book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 15) + "...</a>";
					}
				}

				Grid1.DataSource = books;
				Grid1.DataBind();
			}
		}

		protected void Grid1_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Get("id") == "0" || Request.QueryString.Get("id1") == "0" || !string.IsNullOrEmpty(Request.QueryString.Get("id2")) || PrjPub.CurrentLoginUser.SuitRange == 0)
			{
				Grid1.Levels[0].Columns[1].Visible = false;
			}
		}
	}
}
