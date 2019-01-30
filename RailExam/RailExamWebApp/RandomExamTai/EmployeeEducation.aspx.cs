using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExamWebApp.Common.Class;
using DSunSoft.Web.UI;
using System.Text;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeEducation : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfID.Value = Request.QueryString.Get("ID");
                BindGrid();

                string type = Request.QueryString.Get("Type");
                if (type == "0" || !PrjPub.IsServerCenter)
                {
                    int columnsCount = this.grdEntity.Levels[0].Columns.Count;
                    this.grdEntity.Levels[0].Columns[columnsCount - 1].Visible = false;
                }

                hfIsServerCenter.Value = PrjPub.IsServerCenter.ToString();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string mode = Request.Form["__EVENTARGUMENT"];
            if (mode != "ref")
                DeleteInfo();
            BindGrid();
			ClientScript.RegisterClientScriptBlock(GetType(), "", "window.parent.frames[0].location=window.parent.frames[0].location", true);
        }
        private void BindGrid()
        {
            grdEntity.DataSource = GetInfo();
             grdEntity.DataBind();
        }
        private DataTable GetInfo()
        {
           Hashtable ht=new Hashtable();
             ht.Add(0,"");
             ht.Add(1, "ȫ����");
             ht.Add(2, "�������");
             ht.Add(3, "��ѧ����");
             ht.Add(4, "��У����");
             ht.Add(5, "����ѧϰ");
             ht.Add(6, "���ѧϰ");
             ht.Add(7, "ְУѧϰ");
             ht.Add(8, "ҵУѧϰ");
             ht.Add(9, "ҹУѧϰ");
             ht.Add(10, "���˽���");

            DataTable dt = new DataTable();
            OracleAccess access = new OracleAccess();
            StringBuilder sql=new StringBuilder();
            sql.Append("select e.*,ol.education_level_name oname,wl.education_level_name nname from zj_employee_education e left join education_level ol ");
            sql.Append(" on e.old_education_id=ol.education_level_id left join education_level wl ");
			sql.Append(" on e.now_education_id=wl.education_level_id ");
            sql.AppendFormat(" where e.employee_id={0} order by employee_education_id desc",Convert.ToInt32(Request.QueryString.Get("ID")));
            dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
            dt.Columns.Add("graduate_date1", typeof(string));
            dt.Columns.Add("create_date1", typeof(string));
             dt.Columns.Add("school_type1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    r["graduate_date1"] = Convert.ToDateTime(r["graduate_date"].ToString()).ToString("yyyy-MM-dd");
                    r["create_date1"] = Convert.ToDateTime(r["create_date"].ToString()).ToString("yyyy-MM-dd");
                    r["school_type1"] = ht[Convert.ToInt32(r["school_type"])].ToString();
                }
            }
            return dt;
        }
        private void DeleteInfo()
        {
            try
            {
                string id = Request.Form["__EVENTARGUMENT"];
                string sql = string.Format("delete from zj_employee_education where employee_education_id={0}", Convert.ToInt32(id));
                OracleAccess access = new OracleAccess();
            	UpdateEmployeeInfo(Request.QueryString.Get("ID"));   //����������Ϣ
				access.ExecuteNonQuery(sql);                         //���»�����Ϣ
				ClientScript.RegisterClientScriptBlock(GetType(), "", "window.parent.frames[0].location=window.parent.frames[0].location;", true);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('����ɾ���ɹ���')", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('����ɾ��ʧ�ܣ�')", true);
            }


        }

		/// <summary>
		/// ����������Ϣ
		/// </summary>
		/// <param name="empID"></param>
		private void UpdateEmployeeInfo(string empID)
		{
			OracleAccess access = new OracleAccess();
			try
			{
				string eduID = Request.Form["__EVENTARGUMENT"];    //Ҫɾ����ѧϰ��̬ID
				string maxEduID = "";
				string sql = "select max(EMPLOYEE_EDUCATION_ID) from zj_employee_education  where employee_id=" + empID;
				DataTable dtMax = access.RunSqlDataSet(sql).Tables[0];
				if (dtMax.Rows.Count > 0)
				{
					maxEduID = dtMax.Rows[0][0].ToString();    //���µ�ѧϰ��̬ID
				}
				if (eduID.Equals(maxEduID))
				{
					//ɾ���������µĶ�̬
					sql = "select * from   zj_employee_education where EMPLOYEE_EDUCATION_ID=" + maxEduID;
					DataTable dtNewInfo = access.RunSqlDataSet(sql).Tables[0];
					if (dtNewInfo.Rows.Count > 0)
					{
						//��ԭ֮ǰ�Ķ�̬
						string oldEduID = dtNewInfo.Rows[0]["old_education_id"].ToString();

						sql = "update employee set EDUCATION_LEVEL_ID=" + oldEduID
                            + " where employee_id=" +
						      empID;
						access.ExecuteNonQuery(sql);
					}
				}


                sql = "select max(EMPLOYEE_EDUCATION_ID) from zj_employee_education  where EMPLOYEE_EDUCATION_ID<>" + eduID + " and  employee_id=" + empID;
                DataTable dtLastMax = access.RunSqlDataSet(sql).Tables[0];
                string lastmaxEduID = "";
                if (dtLastMax.Rows.Count > 0)
                {
                    lastmaxEduID = dtLastMax.Rows[0][0].ToString();    //���µ�ѧϰ��̬ID
                }

                if(lastmaxEduID != "")
                {
                    sql = "select * from   zj_employee_education where EMPLOYEE_EDUCATION_ID=" + lastmaxEduID;
                    DataTable dtLastNew = access.RunSqlDataSet(sql).Tables[0];
                    if (dtLastNew.Rows.Count > 0)
                    {
                        sql = "update employee set "
                            + " Study_Major='" + dtLastNew.Rows[0]["School_Subject"] + "',"
                            + " Graduate_University='" + dtLastNew.Rows[0]["Graducate_School"] + "',"
                            + " Graduate_Date=to_date('" + dtLastNew.Rows[0]["Graduate_Date"] + "','yyyy-mm-dd hh24:mi:ss') "
                            + " where employee_id=" +
                              empID;
                        access.ExecuteNonQuery(sql);
                    }
                    else
                    {
                        sql = "update employee set "
                           + " Study_Major=null,"
                           + " Graduate_University=null,"
                           + " Graduate_Date=null "
                           + " where employee_id=" +
                             empID;
                        access.ExecuteNonQuery(sql);
                    }
                }
                else
                {
                    sql = "update employee set "
                           + " Study_Major=null,"
                           + " Graduate_University=null,"
                           + " Graduate_Date=null "
                           + " where employee_id=" +
                             empID;
                    access.ExecuteNonQuery(sql);
                }
			}
			catch
			{
			}
		}
    }
}
