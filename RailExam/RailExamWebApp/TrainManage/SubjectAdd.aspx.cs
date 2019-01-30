using System;
using System.Data;
using System.Web.UI.WebControls;
using RailExamWebApp.Common.Class;
using DSunSoft.Web.UI;
using DSunSoft.Web.Global;
using System.Text;
using System.Collections.Generic;

namespace RailExamWebApp.TrainManage
{
	public partial class SubjectAdd : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}
				 
				if (Request.QueryString["subjectid"] != null)
					LoadInfo();
				LoadTitle();
				
			}
		}

		bool AddSave()
		{
			if (CheckSubjectName(""))
			{
				try
				{
					OracleAccess oracleAccess = new OracleAccess();
				    string sql = string.Format("insert into zj_train_class_subject values({0},{1},'{2}',{3},{4},'{5}',{6})",
				                               "TRAIN_CLASS_SUBJECT_SEQ.Nextval",
				                               Convert.ToInt32(Request.QueryString["classid"]), txtSubjectName.Text,
				                               chkIs.Checked == true ? 1 : 2, Convert.ToDouble(txtPassResult.Text),
				                               txtMemo.Text == "" ? " " : txtMemo.Text,
				                               txtHour.Text.Trim() == "" ? 0 : Convert.ToInt32(txtHour.Text));
					oracleAccess.ExecuteNonQuery(sql);

				    int subjectID = 0;
				    DataTable dt =
				        oracleAccess.RunSqlDataSet("select max(train_class_subject_id) from zj_train_class_subject").Tables[0];
                    if (dt != null)
                    {
                        if (dt.Rows[0][0].ToString().Length > 0)
                            subjectID = Convert.ToInt32(dt.Rows[0][0]);
                    }
                    InserClassBook(subjectID);
				    InsertResult();
					return true;
				}
				catch
				{
					ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);
					return false;
				}
			}
			else
			{
				ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('该培训班科目已经存在,请重新输入！')", true);
				txtSubjectName.Focus();
				return false;
			}
		}

		bool EditSave()
		{
			if (CheckSubjectName(string.Format(" and train_class_subject_id!={0}", Convert.ToInt32(Request.QueryString["subjectid"]))))
			{
				try
				{
					OracleAccess oracleAccess = new OracleAccess();
				    string sql =
				        string.Format(
				            "update zj_train_class_subject set subject_name='{0}', exam_on_computer={1} ,pass_result={2}, memo='{3}',class_hour={4} where train_class_subject_id={5}",
				            txtSubjectName.Text, chkIs.Checked == true ? 1 : 2,
				            Convert.ToDouble(txtPassResult.Text),
				            txtMemo.Text == "" ? " " : txtMemo.Text, txtHour.Text.Trim() == "" ? 0 : Convert.ToInt32(txtHour.Text),
				            Convert.ToInt32(Request.QueryString["subjectid"])
				            );
                    oracleAccess.ExecuteNonQuery(sql);
				    oracleAccess.ExecuteNonQuery("delete from zj_train_class_subject_book where train_class_subject_id=" +
				                                 Convert.ToInt32(Request.QueryString.Get("subjectid")));
				    InserClassBook(Convert.ToInt32(Request.QueryString.Get("subjectid")));
				
					return true;
				}
				catch
				{
					ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);
					return false;
				}
			}
			else
			{
				ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.Page), "", "alert('该培训班科目已经存在,请重新输入！')", true);
				txtSubjectName.Focus();
				return false;
			}
		}

		void LoadInfo()
		{
			DataTable dt = new DataTable();
			OracleAccess oracleAccess = new OracleAccess();
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select ST.*,CS.TRAIN_CLASS_ID,CS.TRAIN_CLASS_NAME from zj_train_class_subject ST ");
			strSql.Append(" left join zj_train_class CS on CS.TRAIN_CLASS_ID=ST.TRAIN_CLASS_ID ");
			strSql.AppendFormat(" where train_class_subject_id={0}", Convert.ToInt32(Request.QueryString["subjectid"]));
			dt = oracleAccess.RunSqlDataSet(strSql.ToString()).Tables[0];
			txtSubjectName.Text = dt.Rows[0]["subject_name"].ToString();
			if (dt.Rows[0]["exam_on_computer"].ToString() == "1")
				chkIs.Checked = true;
			txtPassResult.Text = dt.Rows[0]["pass_result"].ToString();
			txtMemo.Text = dt.Rows[0]["memo"].ToString();
			txtHour.Text = dt.Rows[0]["class_hour"].ToString();
			hfBookIds.Value = SetBookName(Convert.ToInt32(Request.QueryString.Get("subjectid")))[0];
			txtBook.Text = SetBookName(Convert.ToInt32(Request.QueryString.Get("subjectid")))[1];
			current.InnerText = "修改培训科目";
			IsView();

			string sql =
				string.Format("select * from random_exam_train_class where  train_class_id={0} and train_class_subject_id ={1}",
				              Convert.ToInt32(Request.QueryString.Get("classID")),
				              Convert.ToInt32(Request.QueryString.Get("subjectid")));
			DataTable dtHasRes = oracleAccess.RunSqlDataSet(sql).Tables[0];
			if (dtHasRes.Rows.Count > 0)
				txtPassResult.Enabled = false;
		}

		void IsView()
		{
			if (Request.QueryString["mod"] != null && Request.QueryString["mod"].Equals("view"))
			{
				txtSubjectName.ReadOnly = true;
				txtPassResult.ReadOnly = true;
				txtMemo.ReadOnly = true;
				imgSave.Visible = false;
				current.InnerText = "培训科目详细信息";
			}
		}

		void LoadTitle()
		{
			switch (Request.QueryString["mod"])
			{
				case "add":
					this.Title = "新增科目信息";
					break;
				case "edit":
					this.Title = "修改科目信息";
					break;
				default:
					this.Title = "科目详细信息";
					break;
			}

		}

		bool CheckSubjectName(string sqlStr)
		{
			OracleAccess oracleAccess = new OracleAccess();
			string sql =
				string.Format("select * from zj_train_class_subject where subject_name='{0}'  and train_class_id={1} {2}",
				              txtSubjectName.Text.Trim(), Convert.ToInt32(Request.QueryString["classid"]), sqlStr);
			if (oracleAccess.RunSqlDataSet(sql).Tables[0] != null && oracleAccess.RunSqlDataSet(sql).Tables[0].Rows.Count > 0)
				return false;
			else
				return true;
		}

		protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Request.QueryString["mod"] != null && Request.QueryString["mod"].Equals("add"))
			{
				if (AddSave())
					ClientScript.RegisterStartupScript(GetType(), "", "closeAndRef()", true);
			}

			else if (Request.QueryString["mod"] != null && Request.QueryString["mod"].Equals("edit"))
			{

				if (EditSave())
					ClientScript.RegisterStartupScript(GetType(), "", "closeAndRef()", true);
			}
		}

        private void InserClassBook(int subjectID)
        {
            
            OracleAccess access=new OracleAccess();
            if (hfBookIds.Value.Trim().Length > 0)
            {
                string[] strBookID = hfBookIds.Value.Split('|');
                string sql = "begin ";
                foreach (string s in strBookID)
                {
                    sql +=
                        string.Format(
                            "insert into zj_train_class_subject_book(train_class_subject_id,book_id) values ({0},{1});",
                            subjectID, Convert.ToInt32(s));

                }
                sql += "end; ";
                try
                {
                    access.ExecuteNonQuery(sql);
                }
                catch
                {

                }
            }
        }

        private List<string> SetBookName(int subjectID)
       {
         List<string> lst=new List<string>();
         string bookID = "";
         string bookName = "";
           OracleAccess access=new OracleAccess();
           DataTable dt =
               access.RunSqlDataSet(
                   "select sb.*,b.book_name  from zj_train_class_subject_book sb left join  book b on b.book_id=sb.book_id  where sb.train_class_subject_id=" +
                   subjectID)
                   .Tables[0];
           if(dt!=null)
           {
               if(dt.Rows.Count>0)
               {
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {
                       if (i == 0)
                       {
                           bookID = dt.Rows[i]["book_id"].ToString();
                           bookName = dt.Rows[i]["book_name"].ToString();
                       }
                       else
                       {
                           bookID += "|" + dt.Rows[i]["book_id"];
                           bookName += "|" + dt.Rows[i]["book_name"];
                       }
                   }
               }
           }
           lst.Add(bookID);
           lst.Add(bookName);
           return lst;
       }

        /// <summary>
        /// 信息科目结果
        /// </summary>
        private void InsertResult()
        {
            OracleAccess access=new OracleAccess();
            string sqlSel = "select max(train_class_subject_id) from zj_train_class_subject";
            int subjectID = Convert.ToInt32(access.RunSqlDataSet(sqlSel).Tables[0].Rows[0][0]);

            StringBuilder sqlInsert = new StringBuilder();
            DataTable dtEmp = GetEmpByClassID(Convert.ToInt32(Request.QueryString["classid"]));
            if (dtEmp.Rows.Count > 0)
            {
                sqlInsert.Append("begin ");
                foreach (DataRow r in dtEmp.Rows)
                {
                    sqlInsert.Append(" insert into zj_train_class_subject_result(train_class_subject_result_id, ");
                    sqlInsert.Append(" train_class_id,train_class_subject_id,employee_id,exam_date,result,ispass)");
                    sqlInsert.AppendFormat(" values (TRAIN_CLASS_SUBJECT_RESULT_SEQ.nextval,{0},{1},{2},null,null,0);",
                                           Convert.ToInt32(Request.QueryString["classid"]), subjectID,
                                           Convert.ToInt32(r[0]));
                }
                sqlInsert.Append(" end; ");
                access.ExecuteNonQuery(sqlInsert.ToString());
            }
           
        }

	    private DataTable GetEmpByClassID(int classID)
        {
            OracleAccess access=new OracleAccess();
            return access.RunSqlDataSet(" select employee_id from zj_train_plan_employee where train_class_id=" + classID).Tables[0];
        }
	}
}
