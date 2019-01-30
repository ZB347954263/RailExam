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

namespace RailExamWebApp.RandomExamOther
{
	public partial class RandomExamBasicInfo : PageBase
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
				ViewState["mode"] = Request.QueryString.Get("mode");
				ViewState["startmode"] = Request.QueryString.Get("startmode");
				hfMode.Value = ViewState["mode"].ToString();

				dateBeginTime.DateValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				dateEndTime.DateValue = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss");

				string ExamCategoryID = Request.QueryString.Get("ExamCategoryIDPath");

				if (ExamCategoryID == "0")
				{
					ExamCategoryID = "";
				}

				if (!string.IsNullOrEmpty(ExamCategoryID))
				{
					string[] str1 = ExamCategoryID.Split(new char[] { '/' });

					int nID = int.Parse(str1[str1.LongLength - 1].ToString());

					hfCategoryId.Value = nID.ToString();
					ExamCategoryBLL pcl = new ExamCategoryBLL();

					RailExam.Model.ExamCategory pc = pcl.GetExamCategory(nID);

					txtCategoryName.Text = pc.CategoryName;
				}


				string strExamID = Request.QueryString.Get("id");
				if (!string.IsNullOrEmpty(strExamID))
				{
					FillPage(int.Parse(strExamID));
				}
				else
				{
					ViewState["OrgID"] = PrjPub.CurrentLoginUser.StationOrgID.ToString();
					SetTrainClassVisible(false);
				}

				if(PrjPub.IsServerCenter)
				{
					hfIsShowTrainClass.Value = ConfigurationManager.AppSettings["IsShowTrainClass"];
				}
				else
				{
					hfIsShowTrainClass.Value = "1";
				}
			}
			//else
			//{
			//    if(!string.IsNullOrEmpty(hfPostID.Value))
			//    {
			//        PostBLL postBLL = new PostBLL();
			//        string[] str = hfPostID.Value.Split(',');
			//        for(int i=0; i< str.Length; i++)
			//        {						
			//            if(i==0)
			//            {
			//                txtPost.Text = postBLL.GetPost(Convert.ToInt32(str[i])).PostName;													
			//            }
			//            else
			//            {
			//                txtPost.Text = txtPost.Text + "," + postBLL.GetPost(Convert.ToInt32(str[i])).PostName;						
			//            }
			//        }
			//    }
			//}

			if (hfPostID.Value != "")
			{
				txtPost.Text = hfPostName.Value;
			}

			chkHasTrainClass.Attributes.Add("onclick", "chkHasTrainClassOnchange();");
		}

		private void SetTrainClassVisible(bool flag)
		{
			btnAddTrainClass.Visible = flag;
			rbnStyle1.Enabled = !flag;

            if(flag)
            {
                rbnStyle1.Checked = false;
                rbnStyle2.Checked = true;
            }
        }


		protected void FillPage(int nExamID)
		{
			RandomExamBLL examBLL = new RandomExamBLL();
			RailExam.Model.RandomExam exam = examBLL.GetExam(nExamID);

			if (exam != null)
			{
				ViewState["OrgID"] = exam.OrgId.ToString();

				if (ViewState["startmode"].ToString() == "Edit")
				{
					if (exam.HasPaper)
					{
						Response.Write("<script>alert('该考试已生成试卷，不能被编辑！');window.close();</script>");
					}
				}
				txtCategoryName.Text = exam.CategoryName;
				hfCategoryId.Value = exam.CategoryId.ToString();
				ddlType.SelectedValue = exam.ExamTypeId.ToString();
				txtExamName.Text = exam.ExamName;
				txtExamTime.Text = exam.ExamTime.ToString();
				ViewState["BeginTime"] = exam.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"); 
				dateBeginTime.DateValue = exam.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
				dateEndTime.DateValue = exam.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
				txtPassScore.Text = exam.PassScore.ToString();

				if (exam.IsComputerExam)
				{
					rbnExamMode1.Checked = true;
				}
				else
				{
					rbnExamMode2.Checked = true;
				}

				if (exam.StartMode == 1)
				{
					rbnStartMode1.Checked = true;
				}
				else
				{
					rbnStartMode2.Checked = true;
				}



				if(exam.MinExamTimes == 1)
				{
					rbnSubject1.Checked = true;
				}
				else if(exam.MinExamTimes == 2)
				{
					rbnSubject2.Checked = true;
				}
				ViewState["Subject"] = exam.MinExamTimes;

				txtMET2.Text = exam.MaxExamTimes.ToString();
				chUD.Checked = exam.IsUnderControl;
				chAutoScore.Checked = exam.IsAutoScore;
				chSeeAnswer.Checked = exam.CanSeeAnswer;
				chSeeScore.Checked = exam.CanSeeScore;
				chPublicScore.Checked = exam.IsPublicScore;
				txtDescription.Text = exam.Description;
				txtMemo.Text = exam.Memo;
				ddlIsGroup.SelectedValue = exam.IsGroupLeader.ToString();
				ddlTech.SelectedValue = exam.TechnicianTypeID.ToString();
				hfPostID.Value = exam.PostID;
				ViewState["PostID"] = hfPostID.Value;

				PostBLL postBLL = new PostBLL();
				string[] strPostID = exam.PostID.Split(',');
				for (int i = 0; i < strPostID.Length; i++ )
				{
					if(i==0)
					{
						txtPost.Text = postBLL.GetPost(Convert.ToInt32(strPostID[i])).PostName;
					}
					else
					{
						txtPost.Text = txtPost.Text + "," + postBLL.GetPost(Convert.ToInt32(strPostID[i])).PostName;
					}
				}
				hfPostName.Value = txtPost.Text;


				lblCreatePerson.Text = exam.CreatePerson;
				lblCreateTime.Text = exam.CreateTime.ToString("yyyy-MM-dd HH:mm");

				if (ViewState["startmode"].ToString() == "Edit")
				{
					RandomExamResultBLL reBll = new RandomExamResultBLL();
					IList<RailExam.Model.RandomExamResult> examResults =
						reBll.GetRandomExamResultByExamID(exam.RandomExamId);

					if (examResults.Count > 0)
					{
						ViewState["mode"] = "ReadOnly";
					}
				}

				chkHasTrainClass.Checked = exam.HasTrainClass;
				hfHasTrainClass.Value = chkHasTrainClass.Checked.ToString();
				ViewState["HasTrainClass"] = chkHasTrainClass.Checked.ToString();
				
                SetTrainClassVisible(chkHasTrainClass.Checked);
                
                if (exam.ExamStyle == 1)
                {
                    rbnStyle1.Checked = true;
                }
                else
                {
                    rbnStyle2.Checked = true;
                }

				if (chkHasTrainClass.Checked)
				{
					OracleAccess db = new OracleAccess();

					RandomExamTrainClassHaBLL objTrainClassBll = new RandomExamTrainClassHaBLL();
					IList<RandomExamTrainClassHa> objTrainClassList =
						objTrainClassBll.GetRandomExamTrainClassByRandomExamID(exam.RandomExamId);
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add(new DataColumn("RandomExamTrainClassID", typeof(int)));
					dataTable.Columns.Add(new DataColumn("RandomExamID", typeof(int)));
					dataTable.Columns.Add(new DataColumn("TrainClassID", typeof(string)));
					dataTable.Columns.Add(new DataColumn("TrainClassName", typeof(string)));

					string strSql = "";
					foreach (RandomExamTrainClassHa trainClass in objTrainClassList)
					{
						DataRow newRow = dataTable.NewRow();

						newRow[0] = trainClass.RandomExamTrainClassID;
						newRow[1] = trainClass.RandomExamID;
						newRow[2] = trainClass.ArchivesID;

						strSql = "select * from Train_Info where Archives_Num='" + trainClass.ArchivesID + "' and Unit_Code='" + exam.OrgId + "'";
						newRow[3] = db.RunSqlDataSet(strSql).Tables[0].Rows[0]["Class_Name"].ToString();

						dataTable.Rows.Add(newRow);

						if (hfTrainClassID.Value == "")
						{
							hfTrainClassID.Value = trainClass.ArchivesID;
						}
						else
						{
							hfTrainClassID.Value = hfTrainClassID.Value + "," + trainClass.ArchivesID;
						}
					}

					ViewState["TrainClass"] = hfTrainClassID.Value;

					Grid1.DataSource = dataTable;
					Grid1.DataBind();

					//当考试为补考的时候，不能修改培训班信息
					if (exam.IsReset)
					{
						chkHasTrainClass.Enabled = false;
						Grid1.Enabled = false;
						btnAddTrainClass.Visible = false;
					}
					hfIsReset.Value = exam.IsReset.ToString();
				}
				else
				{
					ViewState["TrainClass"] = "";
				}
			}

			if (ViewState["mode"].ToString() == "ReadOnly")
			{
				chkHasTrainClass.Enabled = false;
				txtExamName.Enabled = false;
				dateBeginTime.Enabled = false;
				dateEndTime.Enabled = false;
				ddlType.Enabled = false;
				txtExamTime.Enabled = false;
				rbnExamMode1.Enabled = false;
				rbnExamMode2.Enabled = false;
				txtMET2.Enabled = false;
				chUD.Enabled = false;
				chAutoScore.Enabled = false;
				chSeeAnswer.Enabled = false;
				chSeeScore.Enabled = false;
				chPublicScore.Enabled = false;
				txtDescription.Enabled = false;
				txtMemo.Enabled = false;
				rbnStartMode1.Enabled = false;
				rbnStartMode2.Enabled = false;
				rbnStyle1.Enabled = false;
				rbnStyle2.Enabled = false;
				txtPost.Enabled = false;
				ddlIsGroup.Enabled = false;
				ddlTech.Enabled = false;
				txtCategoryName.Enabled = false;
				txtPassScore.Enabled = false;
			}
		}


		private void BindTrainClass(DropDownList ddlTrainClass, string strOrgID)
		{
			OracleAccess db = new OracleAccess();

			string strSql = "select * from Train_Info where Unit_Code='" + ViewState["OrgID"] + "'"
			                + " and (to_date(Class_Date,'yyyy-mm-dd')+Train_Days)>sysdate "
			                + " and sysdate>to_date(Class_Date,'yyyy-mm-dd')";

			DataSet ds = db.RunSqlDataSet(strSql);

			ddlTrainClass.Items.Clear();

			ListItem itemselect = new ListItem();
			itemselect.Value = "0";
			itemselect.Text = "--请选择--";
			ddlTrainClass.Items.Add(itemselect);

			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				ListItem item = new ListItem();
				item.Value = dr["Archives_Num"].ToString();
				item.Text = dr["Class_Name"].ToString();
				ddlTrainClass.Items.Add(item);
			}
		}

		private DataTable BindGrid()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("RandomExamTrainClassID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("RandomExamID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("TrainClassID", typeof(string)));
			dataTable.Columns.Add(new DataColumn("TrainClassName", typeof(string)));

			for (int i = 0; i < Grid1.Rows.Count; i++)
			{
				DataRow newRow = dataTable.NewRow();

				newRow[0] = ((Label)Grid1.Rows[i].FindControl("lblID")).Text;
				newRow[1] = ((Label)Grid1.Rows[i].FindControl("lblRandomExamID")).Text;

				if (Grid1.EditIndex == i)
				{
					newRow[2] = ((HiddenField)Grid1.Rows[i].FindControl("hfTrainClass")).Value;
					newRow[3] = ((DropDownList)Grid1.Rows[i].FindControl("ddlTrainClass")).SelectedItem.Text;
				}
				else
				{
					newRow[2] = ((Label)Grid1.Rows[i].FindControl("lblTrainClassID")).Text;
					newRow[3] = ((Label)Grid1.Rows[i].FindControl("lblTrainClass")).Text;
				}
				dataTable.Rows.Add(newRow);
			}

			return dataTable;
		}

		protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			DataTable dataTable = BindGrid();
			Grid1.EditIndex = -1;
			if ((bool)ViewState["IsNewGoods"])
			{
				dataTable.Rows[e.RowIndex].Delete();
				dataTable.AcceptChanges();
			}
			Grid1.DataSource = dataTable;
			Grid1.DataBind();

			//重置视图
			ViewState["IsNewGoods"] = false;
		}

		protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}

		protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			DataTable dataTable = BindGrid();
			Grid1.EditIndex = -1;
			dataTable.Rows[e.RowIndex].Delete();
			dataTable.AcceptChanges();
			Grid1.DataSource = dataTable;
			Grid1.DataBind();

			//重置视图
			ViewState["IsNewGoods"] = false;

			hfTrainClassID.Value = "";
			foreach (DataRow dr in dataTable.Rows)
			{
				if (hfTrainClassID.Value == "")
				{
					hfTrainClassID.Value = dr["TrainClassID"].ToString();
				}
				else
				{
					hfTrainClassID.Value = hfTrainClassID.Value + "," + dr["TrainClassID"].ToString();
				}
			}

			hfPostID.Value = "";
			txtPost.Text = "";
			hfPostName.Value = "";
		}

		protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			if (Grid1.EditIndex != -1)
			{
				SessionSet.PageMessage = "请先保存正在编辑的项！";
				return;
			}

			DataTable dataTable = BindGrid();
			Grid1.EditIndex = e.NewEditIndex;
			Grid1.DataSource = dataTable;
			Grid1.DataBind();

			DropDownList ddlTrainClass = (DropDownList)Grid1.Rows[Grid1.EditIndex].FindControl("ddlTrainClass");
			BindTrainClass(ddlTrainClass, PrjPub.CurrentLoginUser.StationOrgID.ToString());
			ddlTrainClass.SelectedValue = dataTable.Rows[Grid1.EditIndex]["TrainClassID"].ToString();

			//标记为已添加商品
			ViewState["IsNewGoods"] = false;
		}

		protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			OracleAccess db = new OracleAccess();
			DataTable dataTable = BindGrid();
			string strSql = "";

			DropDownList ddlTrainClass = (DropDownList)Grid1.Rows[e.RowIndex].FindControl("ddlTrainClass");

			if (ddlTrainClass.SelectedValue == "0")
			{
				SessionSet.PageMessage = "请选择培训班！";
				return;
			}

			if (dataTable.Select("TrainClassID='" + ddlTrainClass.SelectedValue + "'").Length > 0)
			{
				SessionSet.PageMessage = "不能重复添加培训班科目！";
				return;
			}

			//if (dataTable.Rows.Count > 1)
			//{
			//    strSql = "select PassResult from TrainClassSubject where TrainClassSubjectID=" + ddlTrainClassSubject.SelectedValue;
			//    if (Convert.ToDecimal(txtPassScore.Text) != Convert.ToDecimal(DBI.ExecuteQuery(strSql).Tables[0].Rows[0][0].ToString()))
			//    {
			//        SessionSet.PageMessage = "该培训班及格分数与已存在培训班及格分数不一样，不能添加在同一个考试中！";
			//        return;
			//    }
			//}

			strSql = "select  Employee_ID  "
							+ "from CheckRoll_Info "
							+ " where Archives_ID='" + ddlTrainClass.SelectedValue+"' and Unit_Code='" + ViewState["OrgID"] + "'";

			DataSet dsEmployee = db.RunSqlDataSet(strSql);
			if (dsEmployee.Tables[0].Rows.Count == 0)
			{
				SessionSet.PageMessage = "当前培训班的考试科目未添加学员，请在职教系统中核实！";
				return;
			}


			if ((bool)ViewState["IsNewGoods"])
			{
				ViewState["IsNewGoods"] = false;
			}

			dataTable.Rows[e.RowIndex]["TrainClassID"] = ddlTrainClass.SelectedValue;
			dataTable.Rows[e.RowIndex]["TrainClassName"] = ddlTrainClass.SelectedItem.Text;

			//if (dataTable.Rows.Count == 1)
			//{
			//    strSql = "select PassResult from TrainClassSubject where TrainClassSubjectID=" + ddlTrainClassSubject.SelectedValue;
			//    txtPassScore.Text = DBI.ExecuteQuery(strSql).Tables[0].Rows[0][0].ToString();
			//}

			hfTrainClassID.Value = "";
			foreach (DataRow dr in dataTable.Rows)
			{
				if (hfTrainClassID.Value == "")
				{
					hfTrainClassID.Value = dr["TrainClassID"].ToString();
				}
				else
				{
					hfTrainClassID.Value = hfTrainClassID.Value + "," + dr["TrainClassID"];
				}
			}

			hfPostID.Value = "";
			txtPost.Text = "";
			hfPostName.Value = "";

			Grid1.EditIndex = -1;
			Grid1.DataSource = dataTable;
			Grid1.DataBind();
		}

		protected void btnAddTrainClass_Click(object sender, EventArgs e)
		{
			if (Grid1.EditIndex != -1 && Grid1.Rows.Count > 0)
			{
				SessionSet.PageMessage = "请先保存您正在编辑的项！";
				return;
			}

			DataTable dataTable = null;
			dataTable = BindGrid();
			DataRow newRow = dataTable.NewRow();

			newRow["RandomExamTrainClassID"] = 0;
			newRow["RandomExamID"] = 0;
			newRow["TrainClassID"] = "";
			newRow["TrainClassName"] = "";

			dataTable.Rows.InsertAt(newRow, 0);

			Grid1.EditIndex = 0;
			Grid1.DataSource = dataTable;
			Grid1.DataBind();

			ViewState["IsNewGoods"] = true;

			DropDownList ddlTrainClass = (DropDownList)Grid1.Rows[0].FindControl("ddlTrainClass");
			BindTrainClass(ddlTrainClass, PrjPub.CurrentLoginUser.StationOrgID.ToString());
		}

		protected void chkHasTrainClass_CheckedChanged(object sender, EventArgs e)
		{
			SetTrainClassVisible(chkHasTrainClass.Checked);
			hfHasTrainClass.Value = chkHasTrainClass.Checked.ToString();
			txtPost.Text = string.Empty;
			hfPostName.Value = string.Empty;
			hfPostID.Value = string.Empty;
		}

		protected void btnSave_Click(object sender, ImageClickEventArgs e)
		{
			DataTable dataTable = BindGrid();

			if (txtMET2.Text == "")
			{
				txtMET2.Text = "1";
			}

			if(!rbnSubject1.Checked && !rbnSubject2.Checked)
			{
				SessionSet.PageMessage = "请选择考试科目类别！";
				return;	
			}

			RandomExamBLL examBLL = new RandomExamBLL();
			RailExam.Model.RandomExam exam = new RailExam.Model.RandomExam();

			string strID = string.Empty;
			string strMode = ViewState["mode"].ToString();
			string strStartMode = ViewState["startmode"].ToString();

			RandomExamTrainClassHaBLL objTrainClassBll = new RandomExamTrainClassHaBLL();
			string[] strPost = hfPostID.Value.Split(',');
			string strErrorMessage = "";

			if (strMode == "Insert")
			{
				if (chkHasTrainClass.Checked)
				{
					foreach (DataRow dr in dataTable.Rows)
					{
						for (int i = 0; i < strPost.Length; i++)
						{
							IList<RandomExamTrainClassHa> objList =
								objTrainClassBll.GetRandomExamTrainClassCount(dr["TrainClassID"].ToString(),
																			  rbnSubject1.Checked?1:2,
																			  Convert.ToInt32(strPost[i]));
							if (objList.Count > 0)
							{
								strErrorMessage = "培训班“" + dr["TrainClassName"] + "”的考试科目“" + (rbnSubject1.Checked ? "安全" : "理论") + "”已新增当前所选职名的试卷！";
							}
						}
					}
				}

				if (strErrorMessage != "")
				{
					SessionSet.PageMessage = strErrorMessage;
					return;
				}

				if (DateTime.Parse(dateBeginTime.DateValue.ToString()) < DateTime.Today)
				{
					SessionSet.PageMessage = "开始时间不能小于当前日期！";
					return;
				}

				exam.CategoryId = int.Parse(hfCategoryId.Value);
				exam.ExamName = txtExamName.Text;
				exam.Memo = txtMemo.Text;
				exam.CreatePerson = PrjPub.CurrentLoginUser.EmployeeName;
				exam.ExamModeId = 1;
				exam.PassScore = Convert.ToDecimal(txtPassScore.Text);

				if (rbnExamMode1.Checked)
				{
					exam.IsComputerExam = true;
				}
				else
				{
					exam.IsComputerExam = false;
				}

				if (rbnStartMode1.Checked)
				{
					exam.StartMode = 1;
				}
				else
				{
					exam.StartMode = 2;
				}

				if (rbnStyle1.Checked)
				{
					exam.ExamStyle = 1;
				}
				else
				{
					exam.ExamStyle = 2;
				}

				exam.IsAutoScore = chAutoScore.Checked;
				exam.CanSeeAnswer = chSeeAnswer.Checked;
				exam.CanSeeScore = chSeeScore.Checked;
				exam.IsPublicScore = chPublicScore.Checked;
				exam.IsUnderControl = chUD.Checked;
				exam.MaxExamTimes = int.Parse(txtMET2.Text);
				if(rbnSubject1.Checked)
				{
					exam.MinExamTimes = 1;
				}
				else if (rbnSubject2.Checked)
				{
					exam.MinExamTimes = 2;
				}

				exam.BeginTime = DateTime.Parse(dateBeginTime.DateValue.ToString());
				exam.EndTime = DateTime.Parse(dateEndTime.DateValue.ToString());
				exam.ExamTypeId = 1;
				exam.CreateTime = DateTime.Now;
				exam.Description = txtDescription.Text;
				exam.ExamTime = int.Parse(txtExamTime.Text);
				exam.StatusId = 1;
				exam.PostID = hfPostID.Value;
				exam.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
				exam.IsGroupLeader = Convert.ToInt32(ddlIsGroup.SelectedValue);

				exam.AutoSaveInterval = 0;
				exam.OrgId = PrjPub.CurrentLoginUser.StationOrgID;
				exam.HasTrainClass = chkHasTrainClass.Checked;

				int id = examBLL.AddExam(exam);
				strID = id.ToString();

				//当考试来源为培训班时，需自动添加考生。
				if (chkHasTrainClass.Checked)
				{
					foreach (DataRow dr in dataTable.Rows)
					{
						RandomExamTrainClassHa obj = new RandomExamTrainClassHa();
						obj.RandomExamID = Convert.ToInt32(strID);
						obj.ArchivesID = dr["TrainClassID"].ToString();
						objTrainClassBll.AddRandomExamTrainClass(obj);
					}
					//ClientScript.RegisterStartupScript(GetType(),
					//    "jsSelectFirstNode",
					//    @"SaveArrange(" + strID + ",'" + strStartMode + "','" + strMode + "');",
					//    true);

					string strClause = strID + "|" + strStartMode + "|" + strMode;
					ClientScript.RegisterStartupScript(GetType(), "import", "inputCallback.callback('" + strClause + "');", true);
				}
				else
				{
					Response.Redirect("RandomExamSubjectInfo.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);
				}
			}
			else if (strMode == "Edit")
			{
				strID = Request.QueryString.Get("id");

				if (chkHasTrainClass.Checked && (ViewState["TrainClass"].ToString() != hfTrainClassID.Value || ViewState["Subject"].ToString() != ( rbnSubject1.Checked ? 1 : 2).ToString()))
				{
					foreach (DataRow dr in dataTable.Rows)
					{
						for (int i = 0; i < strPost.Length; i++)
						{
							IList<RandomExamTrainClassHa> objList =
								objTrainClassBll.GetRandomExamTrainClassCount(dr["TrainClassID"].ToString(),
																			  rbnSubject1.Checked ? 1 : 2,
																			  Convert.ToInt32(strPost[i]));
							if (objList.Count > 0)
							{
								if (objList[0].RandomExamID.ToString() != strID)
								{
									strErrorMessage = "培训班“" + dr["TrainClassName"] + "”的考试科目“" + (rbnSubject1.Checked ? "安全" : "理论") + "”已新增当前所选职名的试卷！";
								}
							}
						}
					}
				}

				if (strErrorMessage != "")
				{
					SessionSet.PageMessage = strErrorMessage;
					return;
				}

				if (DateTime.Parse(dateBeginTime.DateValue.ToString()) < DateTime.Today && ViewState["BeginTime"].ToString() != dateBeginTime.DateValue.ToString())
				{
					SessionSet.PageMessage = "开始时间不能小于当前日期！";
					return;
				}


				exam.ExamName = txtExamName.Text;
				exam.Memo = txtMemo.Text;
				exam.RandomExamId = int.Parse(strID);
				exam.ExamTime = int.Parse(txtExamTime.Text);
				exam.ExamModeId = 1;
				exam.PassScore = Convert.ToDecimal(txtPassScore.Text);

				if (rbnExamMode1.Checked)
				{
					exam.IsComputerExam = true;
				}
				else
				{
					exam.IsComputerExam = false;
				}

				if (rbnStartMode1.Checked)
				{
					exam.StartMode = 1;
				}
				else
				{
					exam.StartMode = 2;
				}

				if (rbnStyle1.Checked)
				{
					exam.ExamStyle = 1;
				}
				else
				{
					exam.ExamStyle = 2;
				}
				exam.BeginTime = DateTime.Parse(dateBeginTime.DateValue.ToString());
				exam.EndTime = DateTime.Parse(dateEndTime.DateValue.ToString());

				exam.IsAutoScore = chAutoScore.Checked;
				exam.CanSeeAnswer = chSeeAnswer.Checked;
				exam.CanSeeScore = chSeeScore.Checked;
				exam.IsPublicScore = chPublicScore.Checked;
				exam.IsUnderControl = chUD.Checked;
				exam.MaxExamTimes = int.Parse(txtMET2.Text);
				if (rbnSubject1.Checked)
				{
					exam.MinExamTimes = 1;
				}
				else if (rbnSubject2.Checked)
				{
					exam.MinExamTimes = 2;
				}
				exam.ExamTypeId = 1;
				exam.Description = txtDescription.Text;
				exam.AutoSaveInterval = 0;
				exam.PostID = hfPostID.Value;
				exam.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
				exam.IsGroupLeader = Convert.ToInt32(ddlIsGroup.SelectedValue);

				RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
				//当是否为培训班改变或所选培训班改变时，需删除考试安排
				if (Convert.ToBoolean(ViewState["HasTrainClass"].ToString()) != chkHasTrainClass.Checked
					|| ViewState["TrainClass"].ToString() != hfTrainClassID.Value || ViewState["PostID"].ToString() != hfPostID.Value)
				{
					objArrangeBll.DeleteRandomExamArrangeByRandomExamID(Convert.ToInt32(strID));
				}

				if ((Convert.ToBoolean(ViewState["HasTrainClass"].ToString()) != chkHasTrainClass.Checked && !chkHasTrainClass.Checked) || ViewState["TrainClass"].ToString() != hfTrainClassID.Value)
				{
					objTrainClassBll.DeleteRandomExamTrainClassByRandomExamID(Convert.ToInt32(strID));
				}

				exam.HasTrainClass = chkHasTrainClass.Checked;
				examBLL.UpdateExam(exam);
				examBLL.UpdateVersion(Convert.ToInt32(strID));

				if (chkHasTrainClass.Checked)
				{
					if (ViewState["TrainClass"].ToString() != hfTrainClassID.Value || ViewState["PostID"].ToString() != hfPostID.Value)
					{
						foreach (DataRow dr in dataTable.Rows)
						{
							RandomExamTrainClassHa obj = new RandomExamTrainClassHa();
							obj.RandomExamID = Convert.ToInt32(strID);
							obj.ArchivesID = dr["TrainClassID"].ToString();
							objTrainClassBll.AddRandomExamTrainClass(obj);
						}

						//ClientScript.RegisterStartupScript(GetType(),
						//"jsSelectFirstNode",
						//@"SaveArrange(" + strID + ",'" + strStartMode + "','" + strMode + "');",
						//true);

						string strClause = strID + "|" + strStartMode + "|" + strMode;
						ClientScript.RegisterStartupScript(GetType(), "import", "inputCallback.callback('" + strClause + "');", true);
					}
					else
					{
						Response.Redirect("RandomExamSubjectInfo.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);
					}
				}
				else
				{
					Response.Redirect("RandomExamSubjectInfo.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);
				}
			}
			else
			{
				strID = Request.QueryString.Get("id");
				Response.Redirect("RandomExamSubjectInfo.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);
			}
		}

		protected void btnCancel_Click(object sender, ImageClickEventArgs e)
		{
			//Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
			Response.Write("<script>top.returnValue='true';top.window.close();</script>");
		}

		protected void inputCallback_Callback(object sender, CallBackEventArgs e)
		{
			hfExam.Value = e.Parameters[0];
			hfExam.RenderControl(e.Output);
		}
	}
}
