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
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamManageFirst : PageBase
    {
		private bool _isWuhan = true;
    	private bool _isWuhanOnly = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                ViewState["mode"] = Request.QueryString.Get("mode");
                ViewState["startmode"] = Request.QueryString.Get("startmode");
                hfMode.Value = ViewState["mode"].ToString();

            	_isWuhan = PrjPub.IsWuhan();
            	_isWuhanOnly = PrjPub.IsWuhanOnly();

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

                OracleAccess db = new OracleAccess();
                string strSql = "select * from Random_Exam_Modular_Type order by Level_Num";
                DataSet ds = db.RunSqlDataSet(strSql);
                
                ListItem item = new ListItem();
                item.Text = "--请选择--";
                item.Value = "0";
                ddlModularType.Items.Add(item);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Random_Exam_Modular_Type_Name"].ToString();
                    item.Value = dr["Random_Exam_Modular_Type_ID"].ToString();
                    ddlModularType.Items.Add(item);
                }


                string strExamID = Request.QueryString.Get("id");
                if (!string.IsNullOrEmpty(strExamID))
                {
                    FillPage(int.Parse(strExamID));
                }
                else
                {
                    rbnStyle2.Checked = true;
                    saveTd.Visible = true;
                    ddlDate.SelectedValue = "1";
                    dateSaveDate.Visible = false;

                	SetTrainClassVisible(false);
                }
            }

			if(hfPostID.Value != "" )
			{
				txtPost.Text = hfPostName.Value;
			}

            if (hfCategoryId.Value != "")
            {
                ExamCategoryBLL pcl = new ExamCategoryBLL();
                RailExam.Model.ExamCategory pc = pcl.GetExamCategory(Convert.ToInt32(hfCategoryId.Value));
                txtCategoryName.Text = pc.CategoryName;
            }

			chkHasTrainClass.Attributes.Add("onclick","chkHasTrainClassOnchange();");
        }

        protected void FillPage(int nExamID)
        {
            RandomExamBLL examBLL = new RandomExamBLL();
            RailExam.Model.RandomExam exam = examBLL.GetExam(nExamID);

            if (exam != null)
            {
                if (ViewState["startmode"].ToString() == "Edit")
                {
                    if (Pub.HasPaper(exam.RandomExamId))
                    {
                        Response.Write("<script>alert('该考试已生成试卷，不能被编辑！');window.close();</script>");
                        return;
                    }
                }
            	hfHasTrainClass.Value = exam.HasTrainClass.ToString();
                txtCategoryName.Text = exam.CategoryName;
                hfCategoryId.Value = exam.CategoryId.ToString();
                ddlType.SelectedValue = exam.ExamTypeId.ToString();
                txtExamName.Text = exam.ExamName;
                txtExamTime.Text = exam.ExamTime.ToString();
                dateBeginTime.DateValue = exam.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
                dateEndTime.DateValue = exam.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
            	txtPassScore.Text = exam.PassScore.ToString();
                if (exam.IsComputerExam)
                {
                    rbnExamMode1.Checked = true;
                    rbnExamMode2.Checked = false;
                }
                else
                {
                    rbnExamMode1.Checked = false;
					rbnExamMode2.Checked = true;
                    rbnExamMode1.Enabled = false;
                    rbnExamMode2.Enabled = false;
                }

                if(exam.StartMode == 1)
                {
                    rbnStartMode1.Checked = true;
                }
                else
                {
                    rbnStartMode2.Checked = true;
                }

				if(exam.ExamStyle == 1)
				{
					rbnStyle1.Checked = true;
				}
				else
				{
					rbnStyle2.Checked = true;
				}

                if(exam.AutoSaveInterval == 1)
                {
                    chkAllItem.Checked = true;
                }
                else
                {
                    chkAllItem.Checked = false;
                }
             
                txtMET2.Text = exam.MaxExamTimes.ToString();
                chUD.Checked = exam.IsUnderControl;
                chAutoScore.Checked = exam.IsAutoScore;
                chkCanSeeAnswer.Checked = exam.CanSeeAnswer;
                chSeeScore.Checked = exam.CanSeeScore;
                chPublicScore.Checked = exam.IsPublicScore;
                txtDescription.Text = exam.Description;
                txtMemo.Text = exam.Memo;
                ddlModularType.SelectedValue = exam.RandomExamModularTypeID.ToString();
                chkIsReduceScore.Checked = exam.IsReduceError;

                //存档考试
                if(exam.ExamStyle == 2)
                {
                    rbnStyle2.Checked = true;
                    saveTd.Visible = true;
                    ddlDate.SelectedValue = exam.SaveStatus.ToString();

                    if(exam.SaveStatus == 2)
                    {
                        dateSaveDate.Visible = true;
                        dateSaveDate.DateValue = Convert.ToDateTime(exam.SaveDate).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        dateSaveDate.Visible = false;
                    }
                }
                else
                {
                    rbnStyle1.Checked = true;
                    saveTd.Visible = false;
                }

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
            	ViewState["HasTrainClass"] = chkHasTrainClass.Checked.ToString();
				SetTrainClassVisible(chkHasTrainClass.Checked);
				if(chkHasTrainClass.Checked)
				{
					RandomExamTrainClassBLL objTrainClassBll = new RandomExamTrainClassBLL();
					IList<RandomExamTrainClass> objTrainClassList =
						objTrainClassBll.GetRandomExamTrainClassByRandomExamID(exam.RandomExamId);
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add(new DataColumn("RandomExamTrainClassID", typeof(int)));
					dataTable.Columns.Add(new DataColumn("RandomExamID", typeof(int)));
					dataTable.Columns.Add(new DataColumn("TrainClassID", typeof(int)));
					dataTable.Columns.Add(new DataColumn("TrainClassName", typeof(string)));
					dataTable.Columns.Add(new DataColumn("TrainClassSubjectID", typeof(int)));
					dataTable.Columns.Add(new DataColumn("TrainClassSubjectName", typeof(string)));

					string strSql = "";
					foreach (RandomExamTrainClass trainClass in objTrainClassList)
					{
						DataRow newRow = dataTable.NewRow();

						newRow[0] = trainClass.RandomExamTrainClassID;
						newRow[1] = trainClass.RandomExamID;
						newRow[2] = trainClass.TrainClassID;

                        OracleAccess db = new OracleAccess();
						strSql = "select * from ZJ_Train_Class where Train_Class_ID=" + trainClass.TrainClassID;
                        newRow[3] = db.RunSqlDataSet(strSql).Tables[0].Rows[0]["Train_Class_Name"].ToString();

						newRow[4] = trainClass.TrainClassSubjectID;

                        strSql = "select * from ZJ_Train_Class_Subject where Train_Class_Subject_ID=" + trainClass.TrainClassSubjectID;
                        newRow[5] = db.RunSqlDataSet(strSql).Tables[0].Rows[0]["Subject_Name"].ToString();
						 
						dataTable.Rows.Add(newRow);

						if(hfTrainClassID.Value == "")
						{
							hfTrainClassID.Value = trainClass.TrainClassID.ToString();
						}
						else
						{
							hfTrainClassID.Value = hfTrainClassID.Value + "," + trainClass.TrainClassID.ToString();
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
					ViewState["PostID"] = "";
				}

                hfPostID.Value = exam.PostID;
                ViewState["PostID"] = hfPostID.Value;

                if (!string.IsNullOrEmpty(exam.PostID))
                {
                    PostBLL postBLL = new PostBLL();
                    string[] strPostID = exam.PostID.Split(',');
                    for (int i = 0; i < strPostID.Length; i++)
                    {
                        if (i == 0)
                        {
                            txtPost.Text = postBLL.GetPost(Convert.ToInt32(strPostID[i])).PostName;
                        }
                        else
                        {
                            txtPost.Text = txtPost.Text + "," + postBLL.GetPost(Convert.ToInt32(strPostID[i])).PostName;
                        }
                    }
                    hfPostName.Value = txtPost.Text;
                }
            }

            if (ViewState["mode"].ToString() == "ReadOnly")
            {
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
                chkCanSeeAnswer.Enabled = false;
                chSeeScore.Enabled = false;
                chPublicScore.Enabled = false;
                txtDescription.Enabled = false;
                txtMemo.Enabled = false;
                rbnStartMode1.Enabled = false;
                rbnStartMode2.Enabled = false;
				txtPassScore.Enabled = false;
            	chkHasTrainClass.Enabled = false;
            	Grid1.Enabled = false;
            }
        }

		private void BindTrainClass(DropDownList ddlTrainClass,string strOrgID)
		{
            string strSql = string.Empty;

            strSql = "select * from ZJ_Train_Class a  "
                 + " inner join ZJ_Train_Plan b on a.Train_Plan_ID=b.Train_Plan_ID "
                 + " where (b.SPONSOR_UNIT_ID=" + PrjPub.CurrentLoginUser.StationOrgID + " or undertake_unit_id=" + PrjPub.CurrentLoginUser.StationOrgID + " ) and  a.Train_Class_Status_ID<3 "
                 + " and a.Train_Class_ID in (select distinct Train_Class_ID from ZJ_Train_Class_Subject where Exam_On_Computer = 1) "
                 + "order by BeginDate desc";

            OracleAccess db = new OracleAccess();
			DataSet ds = db.RunSqlDataSet(strSql);

			ddlTrainClass.Items.Clear();

			ListItem itemselect = new ListItem();
			itemselect.Value = "0";
			itemselect.Text = "--请选择--";
			ddlTrainClass.Items.Add(itemselect);

			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				ListItem item = new ListItem();
				item.Value = dr["Train_Class_ID"].ToString();
                item.Text = dr["Train_Class_Name"].ToString();
				ddlTrainClass.Items.Add(item);
			}
		}

		private void BindSubject(DropDownList ddlSubject, string strTrainClassID)
		{
			string strSql = "select * from ZJ_Train_Class_Subject   "
					 + " where Exam_On_Computer=1 and Train_Class_ID=" + strTrainClassID;

            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);

			ddlSubject.Items.Clear();

			ListItem itemselect = new ListItem();
			itemselect.Value = "0";
			itemselect.Text = "--请选择--";
			ddlSubject.Items.Add(itemselect);

			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				ListItem item = new ListItem();
				item.Value = dr["Train_Class_Subject_ID"].ToString();
				item.Text = dr["Subject_Name"].ToString();
				ddlSubject.Items.Add(item);
			}
		}

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
        	DataTable dataTable = BindGrid();
            if (txtMET2.Text == "")
            {
                txtMET2.Text = "1";
            }

			if (chkHasTrainClass.Checked)
			{
				if (hfPostID.Value == "")
				{
					SessionSet.PageMessage = "带有培训班的考试，必须选择职名！";
					return;
				}
			}

        	RandomExamBLL examBLL = new RandomExamBLL();
            RailExam.Model.RandomExam exam = new RailExam.Model.RandomExam();

            string strID = string.Empty;
            string strMode = ViewState["mode"].ToString();
            string strStartMode = ViewState["startmode"].ToString();

			RandomExamTrainClassBLL objTrainClassBll = new RandomExamTrainClassBLL();
        	string[] strPost = hfPostID.Value.Split(',');
        	string strErrorMessage = "";

            OracleAccess db = new OracleAccess();

            if (strMode == "Insert")
            {
                string strExam = "select * from Random_Exam where Exam_Name='" + txtExamName.Text + "'";
                if(db.RunSqlDataSet(strExam).Tables[0].Rows.Count>0)
                {
                    SessionSet.PageMessage = "该考试名称在系统中已经存在，请重新输入！";
                    txtExamName.Focus();
                    return;
                }

				if (chkHasTrainClass.Checked)
				{
					foreach (DataRow dr in dataTable.Rows)
					{
						for(int i = 0 ; i< strPost.Length; i++)
						{
							IList<RandomExamTrainClass> objList =
								objTrainClassBll.GetRandomExamTrainClassCount(Convert.ToInt32(dr["TrainClassID"].ToString()),
								                                              Convert.ToInt32(dr["TrainClassSubjectID"].ToString()),
								                                              Convert.ToInt32(strPost[i]));
							if(objList.Count > 0)
							{
								strErrorMessage = "培训班“" + dr["TrainClassName"] + "”的考试科目“" + dr["TrainClassSubjectName"] + "”已新增当前所选职名的试卷！";
							}
						}
					}
				}

				if(strErrorMessage != "")
				{
					SessionSet.PageMessage = strErrorMessage;
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

                if(rbnStartMode1.Checked)
                {
                    exam.StartMode = 1;
                }
                else
                {
                    exam.StartMode = 2;
                }

				if(rbnStyle1.Checked)
				{
					exam.ExamStyle = 1;
				}
				else
				{
					exam.ExamStyle = 2;
				}

                exam.IsAutoScore = chAutoScore.Checked;
                exam.CanSeeAnswer = chkCanSeeAnswer.Checked;
                exam.CanSeeScore = chSeeScore.Checked;
                exam.IsPublicScore = chPublicScore.Checked;
                exam.IsUnderControl = chUD.Checked;                
                exam.MaxExamTimes = int.Parse(txtMET2.Text);
                exam.MinExamTimes = 1;
                exam.BeginTime = DateTime.Parse(dateBeginTime.DateValue.ToString());
                exam.EndTime = DateTime.Parse(dateEndTime.DateValue.ToString());
                exam.ExamTypeId = 1;
                exam.CreateTime = DateTime.Now;
                exam.Description = txtDescription.Text;
                exam.ExamTime = int.Parse(txtExamTime.Text);
                exam.StatusId = 1;
            	exam.PostID = hfPostID.Value;

                exam.RandomExamModularTypeID = Convert.ToInt32(ddlModularType.SelectedValue);
                exam.IsReduceError = chkIsReduceScore.Checked;

                exam.AutoSaveInterval = chkAllItem.Checked ? 1 : 0;

                if(saveTd.Visible)
                {
                    exam.SaveStatus = Convert.ToInt32(ddlDate.SelectedValue);

                    if(dateSaveDate.Visible)
                    {
                        if(dateSaveDate.DateValue == null)
                        {
                            SessionSet.PageMessage = "请选择一个存档时间！";
                            return;
                        }

                        exam.SaveDate = Convert.ToDateTime(dateSaveDate.DateValue);
                    }
                }
                else
                {
                    exam.SaveStatus = 0;
                    exam.SaveDate = null;
                }

                //exam.AutoSaveInterval = 0;
                exam.OrgId = PrjPub.CurrentLoginUser.StationOrgID;

            	exam.HasTrainClass = chkHasTrainClass.Checked;

                int id = examBLL.AddExam(exam);
                strID = id.ToString();

				if (_isWuhanOnly)
				{
					//当考试来源为培训班时，需自动添加考生。
					if (chkHasTrainClass.Checked)
					{
						foreach (DataRow dr in dataTable.Rows)
						{
							RandomExamTrainClass obj = new RandomExamTrainClass();
							obj.RandomExamID = Convert.ToInt32(strID);
							obj.TrainClassID = Convert.ToInt32(dr["TrainClassID"].ToString());
							obj.TrainClassSubjectID = Convert.ToInt32(dr["TrainClassSubjectID"].ToString());
							objTrainClassBll.AddRandomExamTrainClass(obj);
						}
						//ClientScript.RegisterStartupScript(GetType(),
						//    "jsSelectFirstNode",
						//    @"SaveArrange(" + strID + ",'" + strStartMode + "','" + strMode + "');",
						//    true);

						//string strClause = strID + "|" + strStartMode + "|" + strMode;
						//ClientScript.RegisterStartupScript(GetType(), "import", "inputCallback.callback('" + strClause + "');", true);

						try
						{
							SaveRandomExamArrange(strID, hfTrainClassID.Value, hfPostID.Value);
						}
						catch
						{
							SessionSet.PageMessage = "添加考生失败";
							return;
						}

						Response.Redirect("RandomExamManageSecond.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);
					}
					else
					{
						Response.Redirect("RandomExamManageSecond.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);
					}
				}
				else
				{
					Response.Redirect("RandomExamManageSecond.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);
				}
            }
            else if (strMode == "Edit")
            {
                strID = Request.QueryString.Get("id");

                if (Pub.HasPaper(Convert.ToInt32(strID)))
                {
                    Response.Write("<script>alert('该考试已生成试卷，不能被编辑！');window.close();</script>");
                    return;
                }

                string strExam = "select * from Random_Exam where Random_Exam_ID!="+ strID +" and Exam_Name='" + txtExamName.Text + "'";
                if (db.RunSqlDataSet(strExam).Tables[0].Rows.Count > 0)
                {
                    SessionSet.PageMessage = "该考试名称在系统中已经存在，请重新输入！";
                    txtExamName.Focus();
                    return;
                }

                string strSql = "select * from Random_Exam_Computer_Server where Has_Paper=1 and Random_Exam_ID=" + strID;
                DataTable dt = db.RunSqlDataSet(strSql).Tables[0];
                if(dt.Rows.Count > 0)
                {
                    SessionSet.PageMessage = "当前考试已经生成试卷，不能继续编辑！";
                    return;
                }


				if (chkHasTrainClass.Checked && ViewState["TrainClass"].ToString() != hfTrainClassID.Value)
				{
					foreach (DataRow dr in dataTable.Rows)
					{
						for (int i = 0; i < strPost.Length; i++)
						{
							IList<RandomExamTrainClass> objList =
								objTrainClassBll.GetRandomExamTrainClassCount(Convert.ToInt32(dr["TrainClassID"].ToString()),
																			  Convert.ToInt32(dr["TrainClassSubjectID"].ToString()),
																			  Convert.ToInt32(strPost[i]));
							if (objList.Count > 0)
							{
								if(objList[0].RandomExamID.ToString() != strID)
								{
									strErrorMessage = "培训班“" + dr["TrainClassName"] + "”的考试科目“" + dr["TrainClassSubjectName"] + "”已新增当前所选职名的试卷！";
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

                exam.CategoryId = Convert.ToInt32(hfCategoryId.Value);
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
                exam.CanSeeAnswer = chkCanSeeAnswer.Checked;
                exam.CanSeeScore = chSeeScore.Checked;
                exam.IsPublicScore = chPublicScore.Checked;
                exam.IsUnderControl = chUD.Checked;
                exam.MaxExamTimes = int.Parse(txtMET2.Text);
                exam.MinExamTimes = 1;
                exam.ExamTypeId = 1;
                exam.Description = txtDescription.Text;
                //exam.AutoSaveInterval = 0;

                exam.RandomExamModularTypeID = Convert.ToInt32(ddlModularType.SelectedValue);
                exam.IsReduceError = chkIsReduceScore.Checked;
                exam.AutoSaveInterval = chkAllItem.Checked ? 1 : 0;

                if (saveTd.Visible)
                {
                    exam.SaveStatus = Convert.ToInt32(ddlDate.SelectedValue);

                    if (dateSaveDate.Visible)
                    {
                        if (dateSaveDate.DateValue == null)
                        {
                            SessionSet.PageMessage = "请选择一个存档时间！";
                            return;
                        }

                        exam.SaveDate = Convert.ToDateTime(dateSaveDate.DateValue);
                    }
                }
                else
                {
                    exam.SaveStatus = 0;
                    exam.SaveDate = null;
                }

				RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
				//当是否为培训班改变或则培训班改变时，需删除考试安排
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
				exam.PostID = hfPostID.Value;
            	exam.Version = exam.Version + 1;
				examBLL.UpdateExam(exam);

				if (_isWuhanOnly)
				{
					if (chkHasTrainClass.Checked)
					{
						//当培训班或职名发生变化时，更改考生名单
						if (ViewState["TrainClass"].ToString() != hfTrainClassID.Value || ViewState["PostID"].ToString() != hfPostID.Value)
						{
							objTrainClassBll.DeleteRandomExamTrainClassByRandomExamID(Convert.ToInt32(strID));
							foreach (DataRow dr in dataTable.Rows)
							{
								RandomExamTrainClass obj = new RandomExamTrainClass();
								obj.RandomExamID = Convert.ToInt32(strID);
								obj.TrainClassID = Convert.ToInt32(dr["TrainClassID"].ToString());
								obj.TrainClassSubjectID = Convert.ToInt32(dr["TrainClassSubjectID"].ToString());
								objTrainClassBll.AddRandomExamTrainClass(obj);
							}

							//ClientScript.RegisterStartupScript(GetType(),
							//"jsSelectFirstNode",
							//@"SaveArrange(" + strID + ",'" + strStartMode + "','" + strMode + "');",
							//true);

							//string strClause = strID + "|" + strStartMode + "|" + strMode;
							//ClientScript.RegisterStartupScript(GetType(), "import", "inputCallback.callback('" + strClause + "');", true);

							try
							{
								SaveRandomExamArrange(strID, hfTrainClassID.Value, hfPostID.Value);
							}
							catch
							{
								SessionSet.PageMessage = "添加考生失败";
								return;
							}

							Response.Redirect("RandomExamManageSecond.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);                
						}
						else
						{
							Response.Redirect("RandomExamManageSecond.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);                
						}
					}
					else
					{
						Response.Redirect("RandomExamManageSecond.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);                
					}
				}
				else
				{
					Response.Redirect("RandomExamManageSecond.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID);                
				}
            }
            else
            {
                strID = Request.QueryString.Get("id");
                Response.Redirect("RandomExamManageSecond.aspx?startmode="+ strStartMode +"&mode=" + strMode + "&id=" + strID);
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {            
            Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
        }

		private void SetTrainClassVisible(bool flag)
		{
			txtPassScore.Enabled = !flag;
			btnAddTrainClass.Visible = flag;

            if(flag)
            {
                rbnStyle1.Enabled = false;
                rbnStyle2.Checked = true;
                rbnStyle1.Checked = false;
                saveTd.Visible = true;
                dateSaveDate.Visible = false;
            }
            else
            {
                rbnStyle1.Enabled = true;
            }
		}


		private DataTable BindGrid()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("RandomExamTrainClassID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("RandomExamID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("TrainClassID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("TrainClassName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("TrainClassSubjectID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("TrainClassSubjectName", typeof(string)));

			for (int i = 0; i < Grid1.Rows.Count; i++)
			{
				DataRow newRow = dataTable.NewRow();

				newRow[0] = ((Label)Grid1.Rows[i].FindControl("lblID")).Text;
				newRow[1] = ((Label)Grid1.Rows[i].FindControl("lblRandomExamID")).Text;

				if (Grid1.EditIndex == i)
				{
					newRow[2] = ((HiddenField)Grid1.Rows[i].FindControl("hfTrainClass")).Value;
					newRow[3] = ((DropDownList)Grid1.Rows[i].FindControl("ddlTrainClass")).SelectedItem.Text;
					newRow[4] = ((HiddenField)Grid1.Rows[i].FindControl("hfTrainclassSubjectID")).Value;
					newRow[5] = ((DropDownList)Grid1.Rows[i].FindControl("ddlTrainClassSubject")).SelectedItem.Text;
				}
				else
				{
					newRow[2] = ((Label)Grid1.Rows[i].FindControl("lblTrainClassID")).Text;
					newRow[3] = ((Label)Grid1.Rows[i].FindControl("lblTrainClass")).Text;
					newRow[4] = ((Label)Grid1.Rows[i].FindControl("lblSubjectID")).Text;
					newRow[5] = ((Label)Grid1.Rows[i].FindControl("lblSubjectName")).Text;
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

			DropDownList ddlTrainClassSubject = (DropDownList)Grid1.Rows[Grid1.EditIndex].FindControl("ddlTrainClassSubject");
			BindSubject(ddlTrainClassSubject, ddlTrainClass.SelectedValue);
			ddlTrainClassSubject.SelectedValue = dataTable.Rows[Grid1.EditIndex]["TrainClassSubjectID"].ToString();

			//标记为已添加商品
			ViewState["IsNewGoods"] = false;
		}

		protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			DataTable dataTable = BindGrid();
			string strSql = "";

			DropDownList ddlTrainClass = (DropDownList)Grid1.Rows[e.RowIndex].FindControl("ddlTrainClass");
			DropDownList ddlTrainClassSubject = (DropDownList)Grid1.Rows[Grid1.EditIndex].FindControl("ddlTrainClassSubject");

			if(ddlTrainClass.SelectedValue == "0")
			{
				SessionSet.PageMessage = "请选择培训班！";
				return;
			}

			if (ddlTrainClassSubject.SelectedValue == "0")
			{
				SessionSet.PageMessage = "请选择培训班科目！";
				return;
			}

			if (dataTable.Select("TrainClassID=" + ddlTrainClass.SelectedValue+ " and TrainClassSubjectID="+ddlTrainClassSubject.SelectedValue).Length > 0)
			{
				SessionSet.PageMessage = "不能重复添加培训班科目！";
				return;
			}

            OracleAccess db = new OracleAccess();

			if (dataTable.Rows.Count > 1)
			{
                strSql = "select Pass_Result from ZJ_Train_Class_Subject where Train_Class_Subject_ID=" + ddlTrainClassSubject.SelectedValue;
				if(Convert.ToDecimal(txtPassScore.Text) != Convert.ToDecimal(db.RunSqlDataSet(strSql).Tables[0].Rows[0][0].ToString()))
				{
					SessionSet.PageMessage = "该培训班及格分数与已存在培训班及格分数不一样，不能添加在同一个考试中！";
					return;
				}
			}

			strSql = "select  Employee_ID  "
							+ "from ZJ_Train_Plan_Employee a "
							+ " where a.Train_Class_ID=" + ddlTrainClass.SelectedValue;

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

			dataTable.Rows[e.RowIndex]["TrainClassID"] = Convert.ToInt32(ddlTrainClass.SelectedValue);
			dataTable.Rows[e.RowIndex]["TrainClassName"] = ddlTrainClass.SelectedItem.Text;
			dataTable.Rows[e.RowIndex]["TrainClassSubjectID"] = Convert.ToInt32(ddlTrainClassSubject.SelectedValue);
			dataTable.Rows[e.RowIndex]["TrainClassSubjectName"] = ddlTrainClassSubject.SelectedItem.Text;

			if(dataTable.Rows.Count == 1)
			{
                strSql = "select Pass_Result from ZJ_Train_Class_Subject where Train_Class_Subject_ID=" + ddlTrainClassSubject.SelectedValue;
				txtPassScore.Text = db.RunSqlDataSet(strSql).Tables[0].Rows[0][0].ToString();
			}

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

			Grid1.EditIndex = -1;
			Grid1.DataSource = dataTable;
			Grid1.DataBind();
		}


		public void ddlTrainClassChange(object sender, System.EventArgs e)
		{
			DropDownList ddlTrainClass = (DropDownList)Grid1.Rows[Grid1.EditIndex].FindControl("ddlTrainClass");
			DropDownList ddlTrainClassSubject = (DropDownList)Grid1.Rows[Grid1.EditIndex].FindControl("ddlTrainClassSubject");
			BindSubject(ddlTrainClassSubject,ddlTrainClass.SelectedValue);
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
			newRow["TrainClassID"] = 0;
			newRow["TrainClassName"] = "";
			newRow["TrainClassSubjectID"] = 0;
			newRow["TrainClassSubjectName"] = "";

			dataTable.Rows.InsertAt(newRow, 0);

			Grid1.EditIndex = 0;
			Grid1.DataSource = dataTable;
			Grid1.DataBind();

			ViewState["IsNewGoods"] = true;

			DropDownList ddlTrainClass = (DropDownList)Grid1.Rows[0].FindControl("ddlTrainClass");
			BindTrainClass(ddlTrainClass, PrjPub.CurrentLoginUser.StationOrgID.ToString());

			DropDownList ddlTrainClassSubject = (DropDownList)Grid1.Rows[0].FindControl("ddlTrainClassSubject");
			BindSubject(ddlTrainClassSubject, "0");
		}

		protected void chkHasTrainClass_CheckedChanged(object sender, EventArgs e)
		{
			SetTrainClassVisible(chkHasTrainClass.Checked);
			hfHasTrainClass.Value = chkHasTrainClass.Checked.ToString();
		}

		protected void inputCallback_Callback(object sender, CallBackEventArgs e)
		{
			hfExam.Value = e.Parameters[0];
			hfExam.RenderControl(e.Output);
		}


		private void SaveRandomExamArrange(string strExamID, string strTrainClassID,string strPostID)
		{
			string strSql = "";
			Hashtable htUserID = new Hashtable();
			string strUserIDs = string.Empty;

            OracleAccess db = new OracleAccess();

			string[] str = strTrainClassID.Split(',');
			for (int j = 0; j < str.Length; j++)
			{
				strSql = "select  a.Employee_ID  "
								+ "from ZJ_Train_Plan_Employee a "
								+ " inner join Employee  b on a.Employee_ID=b.Employee_ID "
                                + " where a.Train_Class_ID=" + str[j] + " and b.Post_ID in (" + strPostID + ")";
                DataSet ds = db.RunSqlDataSet(strSql);

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
                    if (!htUserID.ContainsKey(dr["Employee_ID"].ToString()))
					{
						if (strUserIDs == string.Empty)
						{
                            strUserIDs = dr["Employee_ID"].ToString();
						}
						else
						{
                            strUserIDs = strUserIDs + "," + dr["Employee_ID"];
						}

                        htUserID.Add(dr["Employee_ID"].ToString(), dr["Employee_ID"].ToString());
					}
				}
			}

			RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
			RandomExamArrange objArrange = new RandomExamArrange();
			objArrange.RandomExamId = Convert.ToInt32(strExamID);
			objArrange.Memo = "";
			objArrange.UserIds = strUserIDs;
			objArrangeBll.AddRandomExamArrange(objArrange);
		}

        protected  void chkCanSeeAnswer_CheckedChanged(object sender,EventArgs e)
        {
            if(chkCanSeeAnswer.Checked)
            {
                chSeeScore.Checked = true;
            }
        }

        protected  void rbnStyle2_CheckedChanged(object sender,EventArgs e)
        {
            if(rbnStyle2.Checked)
            {
                saveTd.Visible = true;
                ddlDate.SelectedValue = "1";
                dateSaveDate.Visible = false;
            }
            else
            {
                saveTd.Visible = false;
            }
        }

        protected void rbnStyle1_CheckedChanged(object sender, EventArgs e)
        {
            //存档考试
            if (!rbnStyle1.Checked)
            {
                saveTd.Visible = true;
                ddlDate.SelectedValue = "1";
                dateSaveDate.Visible = false;
            }
            else
            {
                saveTd.Visible = false;
            }
        }

        protected  void ddlDate_SelectedIndexChanged(object sender,EventArgs e)
        {
            if(ddlDate.SelectedValue == "1")
            {
                dateSaveDate.Visible = false;
            }
            else
            {
                dateSaveDate.Visible = true;
            }
        }
    }
}
