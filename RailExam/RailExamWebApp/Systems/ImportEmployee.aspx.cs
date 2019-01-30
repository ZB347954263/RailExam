using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using Microsoft.Office.Interop.Owc11;
using System.Collections.Generic;

namespace RailExamWebApp.Systems
{
    public partial class ImportEmployee : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
                return;
            }
            txtOrg.Text = hfOrgName.Value;

			if (!IsPostBack && !Grid1.IsCallback)
            {
                lbltitle.Visible = false;
                Grid1.Visible = false;
                btnExcel.Visible = false;

                btnModify.Visible = false;
                btnInput.Visible = false;
                btnBind.Visible = true;
                btnExamOther.Visible = false;
                excel.Visible = false;

				Grid1.Levels[0].Columns[1].HeadingText = "�ϸ�֤��";

				if(PrjPub.CurrentLoginUser.SuitRange !=1)
				{
					ImgSelectOrg.Visible = false;
					OrganizationBLL objbll = new OrganizationBLL();
					txtOrg.Text = objbll.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID).ShortName;
					hfOrg.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
				}

				if (Session["EmployeeTable"] != null)
				{
					Session.Remove("EmployeeTable");
				}
            }
			else
			{
				if(!string.IsNullOrEmpty(hfOrg.Value))
				{
					OrganizationBLL objbll = new OrganizationBLL();
					txtOrg.Text = objbll.GetOrganization(Convert.ToInt32(hfOrg.Value)).ShortName;
				}
			}

			string str = Request.Form.Get("Refresh");
			if (str != null && str == "refresh")
			{
                EmployeeErrorBLL objErrorBll = new EmployeeErrorBLL();
                Grid1.DataSource = objErrorBll.GetEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(hfOrg.Value));
                Grid1.DataBind();
                Grid1.Visible = true;
			}
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (File1.FileName == "")
            {
                SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
                return;
            }
            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/"+strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            string strSql;

            string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
            strSql = "select ˳��,վ������,����,����,����,ϵͳ,����,ְ��,�Ա�,����֤��,�Ƿ���鳤,���ܵȼ� from [Sheet1$]";
            OleDbConnection ODcon = new OleDbConnection(strODConn);
            DataSet ds = new DataSet();
            try
            {
                ODcon.Open();
                OleDbCommand OCom = new OleDbCommand(strSql, ODcon);
                OleDbDataAdapter ODA = new OleDbDataAdapter(OCom);
                ODA.Fill(ds);
            }
            catch
            {
                SessionSet.PageMessage = "��˶�Excel�ļ��ĸ�ʽ��";
                return;
            }
            finally
            {
                ODcon.Close();
            }

            IList<RailExam.Model.Employee> objEmployeeList = new List<RailExam.Model.Employee>();

            try
            {
                DataTable dt = new DataTable();
                DataColumn dcnew1 = dt.Columns.Add("ExcelNo");
                DataColumn dcnew2 = dt.Columns.Add("WorkNo");
                DataColumn dcnew3 = dt.Columns.Add("EmployeeName");
                DataColumn dcnew4 = dt.Columns.Add("Sex");
                DataColumn dcnew5 = dt.Columns.Add("OrgPath");
                DataColumn dcnew6 = dt.Columns.Add("PostPath");
                DataColumn dcnew7 = dt.Columns.Add("IsGroup");
                DataColumn dcnew8 = dt.Columns.Add("Tech"); 
                
                OrganizationBLL objOrgBll = new OrganizationBLL();
                PostBLL objPostBll = new PostBLL();
                EmployeeBLL objBll = new EmployeeBLL();
                SystemUserBLL objSystemBll = new SystemUserBLL();
                
                if(ds.Tables[0].Rows.Count == 0)
                {
                    SessionSet.PageMessage = "Excel��û���κμ�¼����˶ԣ�";
                    return;
                }

                if (ds.Tables[0].Rows[0]["վ������"].ToString() != txtOrg.Text.Substring(txtOrg.Text.IndexOf('/') + 1))
                {
                    SessionSet.PageMessage = "վ��ѡ�������˶ԣ�";
                    return;
                }

                //objBll.DeleteEmployeeByOrgID(Convert.ToInt32(hfOrg.Value));

                Hashtable htOrg = GetOrgHash(Convert.ToInt32(hfOrg.Value));
                Hashtable htPost = GetPostHash();

                DataColumn dc1 = ds.Tables[0].Columns.Add("OrgPath");
                DataColumn dc2 = ds.Tables[0].Columns.Add("PostPath");
                DataColumn dc3 = ds.Tables[0].Columns.Add("OrgID");
                DataColumn dc4 = ds.Tables[0].Columns.Add("PostID");
                DataColumn dc5 = ds.Tables[0].Columns.Add("IsGroup");
                DataColumn dc6 = ds.Tables[0].Columns.Add("TechID");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["����"].ToString() != "")
                    {
                        dr["OrgPath"] = dr["վ������"].ToString().Trim() + "-" + dr["����"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
                    }
                    else
                    {
                        dr["OrgPath"] = dr["վ������"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
                    }
                    dr["PostPath"] = dr["ϵͳ"].ToString().Trim() + "-" + dr["����"].ToString().Trim() + "-" + dr["ְ��"].ToString().Trim();

                    //dr["OrgID"] = objOrgBll.GetOrgIDByOrgNamePath(dr["OrgPath"].ToString()).ToString();
                    //dr["PostID"] = objPostBll.GetPostIDByPostNamePath(dr["PostPath"].ToString()).ToString();

                    if(dr["�Ƿ���鳤"].ToString() == "��")
                    {
                        dr["IsGroup"] = "1";
                    }
                    else
                    {
                        dr["IsGroup"] = "0";
                    }

                    if(dr["���ܵȼ�"].ToString() == "�߼���ʦ")
                    {
                        dr["TechID"] = "3";
                    }
                    else if (dr["���ܵȼ�"].ToString() == "��ʦ")
                    {
                        dr["TechID"] = "2";
                    }
                    else
                    {
                        dr["TechID"] = "1";
                    }

                    if (!htOrg.ContainsKey(dr["OrgPath"].ToString()) || !htPost.ContainsKey(dr["PostPath"].ToString()) || dr["����֤��"].ToString() == "")
                    {
                        DataRow drnew = dt.NewRow();
                        drnew["ExcelNo"] = dr["˳��"].ToString();
                        drnew["WorkNo"] = dr["����֤��"].ToString();
                        drnew["EmployeeName"] = dr["����"].ToString();
                        drnew["Sex"] = dr["�Ա�"].ToString();
                        drnew["OrgPath"] = dr["OrgPath"].ToString();
                        drnew["PostPath"] = dr["PostPath"].ToString();
                        drnew["IsGroup"] = dr["�Ƿ���鳤"].ToString();
                        drnew["Tech"] = dr["���ܵȼ�"].ToString();
                        dt.Rows.Add(drnew);
                    }
                    else
                    {
                        dr["OrgID"] = htOrg[dr["OrgPath"].ToString()].ToString();
                        dr["PostID"] = htPost[dr["PostPath"].ToString()].ToString();
                        RailExam.Model.Employee obj = new RailExam.Model.Employee();
                        obj.EmployeeName = dr["����"].ToString().Trim();
                        obj.OrgID = Convert.ToInt32(dr["OrgID"].ToString());
                        obj.PostID = Convert.ToInt32(dr["PostID"].ToString());
                        obj.Sex = dr["�Ա�"].ToString().Trim();
                        obj.WorkNo = dr["����֤��"].ToString().Trim();
                        obj.IsGroupLeader = Convert.ToInt32(dr["IsGroup"].ToString());
                        obj.TechnicianTypeID = Convert.ToInt32(dr["TechID"].ToString());
                        obj.PinYinCode = Pub.GetChineseSpell(obj.EmployeeName);
                        obj.IsOnPost = true;
                        objEmployeeList.Add(obj);
                    }
                 }

                Grid1.DataSource = dt;
                Grid1.DataBind();

                lbltitle.Visible = true;
                Grid1.Visible = true;
                btnExcel.Visible = false;

                objBll.AddEmployeeImport(objEmployeeList);
            }
            catch
            {
                SessionSet.PageMessage = "����ʧ�ܣ�";
                return;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
        }

        protected void btnExam_Click(object sender, EventArgs e)
        {
            if (File1.FileName == "")
            {
                SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
                return;
            }
            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            string strSql;

            string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
            strSql = "select ˳��,վ������,����,����,����,ϵͳ,����,ְ��,�Ա�,����֤��,�Ƿ���鳤,���ܵȼ� from [Sheet1$]";
            OleDbConnection ODcon = new OleDbConnection(strODConn);
            DataSet ds = new DataSet();
            try
            {
                ODcon.Open();
                OleDbCommand OCom = new OleDbCommand(strSql, ODcon);
                OleDbDataAdapter ODA = new OleDbDataAdapter(OCom);
                ODA.Fill(ds);
            }
            catch
            {
                SessionSet.PageMessage = "��˶�Excel�ļ��ĸ�ʽ��";
                return;
            }
            finally
            {
                ODcon.Close();
            }


            try
            {
                DataTable dt = new DataTable();
                DataColumn dcnew1 = dt.Columns.Add("ExcelNo");
                DataColumn dcnew2 = dt.Columns.Add("WorkNo");
                DataColumn dcnew3 = dt.Columns.Add("EmployeeName");
                DataColumn dcnew4 = dt.Columns.Add("Sex");
                DataColumn dcnew5 = dt.Columns.Add("OrgPath");
                DataColumn dcnew6 = dt.Columns.Add("PostPath");
                DataColumn dcnew7 = dt.Columns.Add("IsGroup");
                DataColumn dcnew8 = dt.Columns.Add("Tech");

                OrganizationBLL objOrgBll = new OrganizationBLL();
                PostBLL objPostBll = new PostBLL();
                EmployeeBLL objBll = new EmployeeBLL();
                SystemUserBLL objSystemBll = new SystemUserBLL();

                if (ds.Tables[0].Rows.Count == 0)
                {
                    SessionSet.PageMessage = "Excel��û���κμ�¼����˶ԣ�";
                    return;
                }

                if (ds.Tables[0].Rows[0]["վ������"].ToString() != txtOrg.Text.Substring(txtOrg.Text.IndexOf('/') + 1))
                {
                    SessionSet.PageMessage = "վ��ѡ�������˶ԣ�";
                    return;
                }

                Hashtable htOrg = GetOrgHash(Convert.ToInt32(hfOrg.Value));
                Hashtable htPost = GetPostHash();

                DataColumn dc1 = ds.Tables[0].Columns.Add("OrgPath");
                DataColumn dc2 = ds.Tables[0].Columns.Add("PostPath");
                DataColumn dc3 = ds.Tables[0].Columns.Add("OrgID");
                DataColumn dc4 = ds.Tables[0].Columns.Add("PostID");
                DataColumn dc5 = ds.Tables[0].Columns.Add("IsGroup");
                DataColumn dc6 = ds.Tables[0].Columns.Add("TechID");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["����"].ToString() != "")
                    {
                        dr["OrgPath"] = dr["վ������"].ToString().Trim() + "-" + dr["����"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
                    }
                    else
                    {
                        dr["OrgPath"] = dr["վ������"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
                    }
                    dr["PostPath"] = dr["ϵͳ"].ToString().Trim() + "-" + dr["����"].ToString().Trim() + "-" + dr["ְ��"].ToString().Trim();

                    //dr["OrgID"] = objOrgBll.GetOrgIDByOrgNamePath(dr["OrgPath"].ToString()).ToString();
                    //dr["PostID"] = objPostBll.GetPostIDByPostNamePath(dr["PostPath"].ToString()).ToString();

                    if (dr["�Ƿ���鳤"].ToString() == "��")
                    {
                        dr["IsGroup"] = "1";
                    }
                    else
                    {
                        dr["IsGroup"] = "0";
                    }

                    if (dr["���ܵȼ�"].ToString() == "�߼���ʦ")
                    {
                        dr["TechID"] = "3";
                    }
                    else if (dr["���ܵȼ�"].ToString() == "��ʦ")
                    {
                        dr["TechID"] = "2";
                    }
                    else
                    {
                        dr["TechID"] = "1";
                    }

                    if (!htOrg.ContainsKey(dr["OrgPath"].ToString()) || !htPost.ContainsKey(dr["PostPath"].ToString()) || dr["����֤��"].ToString() == "")
                    {
                        DataRow drnew = dt.NewRow();
                        drnew["ExcelNo"] = dr["˳��"].ToString();
                        drnew["WorkNo"] = dr["����֤��"].ToString();
                        drnew["EmployeeName"] = dr["����"].ToString();
                        drnew["Sex"] = dr["�Ա�"].ToString();
                        drnew["OrgPath"] = dr["OrgPath"].ToString();
                        drnew["PostPath"] = dr["PostPath"].ToString();
                        drnew["IsGroup"] = dr["�Ƿ���鳤"].ToString();
                        drnew["Tech"] = dr["���ܵȼ�"].ToString();
                        dt.Rows.Add(drnew);
                    }
                }

                Grid1.DataSource = dt;
                Grid1.DataBind();

            	lbltitle.Text = "������Ҫ������ݣ�";
                lbltitle.Visible = true;
                Grid1.Visible = true;
                btnExcel.Visible = false;
            }
            catch
            {
                SessionSet.PageMessage = "������֯�����Ƿ�Ψһ��";
                return;
            }
        }

        protected void btnExamSelect_Click(object sender, EventArgs e)
        {
            if (File1.FileName == "")
            {
                SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
                return;
            }

            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            string strSql;

            string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
            strSql = "select ˳��,վ������,����,����,����,ϵͳ,����,ְ��,�Ա�,����֤��,�Ƿ���鳤,���ܵȼ� from [Sheet1$]";
            OleDbConnection ODcon = new OleDbConnection(strODConn);
            DataSet ds = new DataSet();
            try
            {
                ODcon.Open();
                OleDbCommand OCom = new OleDbCommand(strSql, ODcon);
                OleDbDataAdapter ODA = new OleDbDataAdapter(OCom);
                ODA.Fill(ds);
            }
            catch
            {
                SessionSet.PageMessage = "��˶�Excel�ļ��ĸ�ʽ��";
                return;
            }
            finally
            {
                ODcon.Close();
            }


            string str = "";
            string strLen= "";

            Hashtable htWorkNo = new Hashtable();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (htWorkNo.ContainsKey(dr["����֤��"].ToString().Trim()))
                {
                    if (str == "")
                    {
                        str += dr["����֤��"].ToString();
                    }
                    else
                    {
                        str += "," + dr["����֤��"].ToString();
                    }
                }
                else
                {
                    if (dr["����֤��"].ToString() != "")
                    {
                        htWorkNo[dr["����֤��"].ToString()] = dr["����֤��"].ToString();
                    }
                }

                if(dr["����֤��"].ToString().Trim().Length > 13 )
                {
                    if(strLen == "")
                    {
                        strLen += dr["����֤��"].ToString();
                    }
                    else
                    {
                        strLen += "," + dr["����֤��"].ToString();
                    }
                }
            }

            if(strLen != "")
            {
                SessionSet.PageMessage = "�д���13λ����֤�ţ�" + strLen;
                return;
            }

            if (str != "")
            {
                SessionSet.PageMessage = "���ظ��Ĺ���֤�ţ�" + str;
            }
            else
            {
                SessionSet.PageMessage = "û���ظ��Ĺ���֤�ţ�";
            }
        }

        private Hashtable GetOrgHash(int orgID)
        {
            Hashtable htOrg = new Hashtable();

            OrgImportBLL objBll = new OrgImportBLL();
            IList<OrgImport> objList = objBll.GetOrgImport(orgID);

            foreach(OrgImport obj in objList)
            {
                htOrg[obj.OrgNamePath] = obj.OrgID;
            }

            return htOrg;
        }

        private Hashtable GetPostHash()
        {
            Hashtable htPost = new Hashtable();

            PostImportBLL objBll = new PostImportBLL();
            IList<PostImport> objList = objBll.GetPostImport();

            foreach (PostImport obj in objList)
            {
                htPost[obj.PostNamePath] = obj.PostID;
            }

            return htPost;
        }

        protected void btnExamSelectAll_Click(object sender, EventArgs e)
        {
            if (File1.FileName == "")
            {
                SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
                return;
            }

            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            string strSql;

            string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
            strSql = "select ˳��,վ������,����,����,����,ϵͳ,����,ְ��,�Ա�,����֤��,�Ƿ���鳤,���ܵȼ� from [Sheet1$]";
            OleDbConnection ODcon = new OleDbConnection(strODConn);
            DataSet ds = new DataSet();
            try
            {
                ODcon.Open();
                OleDbCommand OCom = new OleDbCommand(strSql, ODcon);
                OleDbDataAdapter ODA = new OleDbDataAdapter(OCom);
                ODA.Fill(ds);
            }
            catch
            {
                SessionSet.PageMessage = "��˶�Excel�ļ��ĸ�ʽ��";
                return;
            }
            finally
            {
                ODcon.Close();
            }


            string str = "";
            string strLen = "";

            Hashtable htWorkNo = new Hashtable();

            EmployeeBLL objBll = new EmployeeBLL();
            IList<RailExam.Model.Employee> objList = objBll.GetAllEmployees();
            foreach (RailExam.Model.Employee employee in objList)
            {
                htWorkNo[employee.WorkNo.ToString()] = employee.WorkNo.ToString();
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (htWorkNo.ContainsKey(dr["����֤��"].ToString().Trim()))
                {
                    if (str == "")
                    {
                        str += dr["����֤��"].ToString();
                    }
                    else
                    {
                        str += "," + dr["����֤��"].ToString();
                    }
                }

                if (dr["����֤��"].ToString().Trim().Length > 13)
                {
                    if (strLen == "")
                    {
                        strLen += dr["����֤��"].ToString();
                    }
                    else
                    {
                        strLen += "," + dr["����֤��"].ToString();
                    }
                }
            }

            if (strLen != "")
            {
                SessionSet.PageMessage = "�д���13λ����֤�ţ�" + strLen;
                return;
            }

            if (str != "")
            {
                SessionSet.PageMessage = "���ظ��Ĺ���֤�ţ�" + str;
            }
            else
            {
                SessionSet.PageMessage = "û���ظ��Ĺ���֤�ţ�";
            }
        }

		protected void btnImportModify_Click(object sender, EventArgs e)
		{
			if (File1.FileName == "")
			{
				SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
				return;
			}
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			string strSql;

			string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
			strSql = "select ˳��,վ������,����,����,����,ϵͳ,����,ְ��,�Ա�,����֤��,�Ƿ���鳤,���ܵȼ�,ԭ����֤�� from [Sheet1$]";
			OleDbConnection ODcon = new OleDbConnection(strODConn);
			DataSet ds = new DataSet();
			try
			{
				ODcon.Open();
				OleDbCommand OCom = new OleDbCommand(strSql, ODcon);
				OleDbDataAdapter ODA = new OleDbDataAdapter(OCom);
				ODA.Fill(ds);
			}
			catch
			{
				SessionSet.PageMessage = "��ǰ������Ҫ�С�ԭ����֤�š��ֶΣ���˶�Excel�ļ��ĸ�ʽ��";
				return;
			}
			finally
			{
				ODcon.Close();
			}

			if (ds.Tables[0].Rows.Count == 0)
			{
				SessionSet.PageMessage = "Excel��û���κμ�¼����˶ԣ�";
				return;
			}

			if (ds.Tables[0].Rows[0]["վ������"].ToString() != txtOrg.Text.Substring(txtOrg.Text.IndexOf('/') + 1))
			{
				SessionSet.PageMessage = "վ��ѡ�������˶ԣ�";
				return;
			}

			DataTable dt = new DataTable();
			DataColumn dcnew1 = dt.Columns.Add("ExcelNo");
			DataColumn dcnew2 = dt.Columns.Add("WorkNo");
			DataColumn dcnew3 = dt.Columns.Add("EmployeeName");
			DataColumn dcnew4 = dt.Columns.Add("Sex");
			DataColumn dcnew5 = dt.Columns.Add("OrgPath");
			DataColumn dcnew6 = dt.Columns.Add("PostPath");
			DataColumn dcnew7 = dt.Columns.Add("IsGroup");
			DataColumn dcnew8 = dt.Columns.Add("Tech"); 

			Hashtable htWorkNo = new Hashtable();

			EmployeeBLL objBll = new EmployeeBLL();
			IList<RailExam.Model.Employee> objList = objBll.GetAllEmployees();
			foreach (RailExam.Model.Employee employee in objList)
			{
				htWorkNo[employee.WorkNo.ToString()] = employee.WorkNo.ToString();
			}

			Hashtable htOldWorkNo = new Hashtable();
			Hashtable htNewWorkNo = new Hashtable();
			int i = 0;
			DataColumn dc1 = ds.Tables[0].Columns.Add("OrgPath");
			DataColumn dc2 = ds.Tables[0].Columns.Add("PostPath");
			DataColumn dc3 = ds.Tables[0].Columns.Add("OrgID");
			DataColumn dc4 = ds.Tables[0].Columns.Add("PostID");
			DataColumn dc5 = ds.Tables[0].Columns.Add("IsGroup");
			DataColumn dc6 = ds.Tables[0].Columns.Add("TechID"); 
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				if (htWorkNo.ContainsKey(dr["ԭ����֤��"].ToString().Trim()))
				{
					htOldWorkNo[i] = dr["ԭ����֤��"].ToString().Trim();
					htNewWorkNo[i] = dr["����֤��"].ToString().Trim();
					i = i + 1;
				}
				else
				{
					if (dr["����"].ToString() != "")
					{
						dr["OrgPath"] = dr["վ������"].ToString().Trim() + "-" + dr["����"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
					}
					else
					{
						dr["OrgPath"] = dr["վ������"].ToString().Trim() + "-" + dr["����"].ToString().Trim();
					}

					dr["PostPath"] = dr["ϵͳ"].ToString().Trim() + "-" + dr["����"].ToString().Trim() + "-" + dr["ְ��"].ToString().Trim();

					if(dr["ԭ����֤��"].ToString().Trim()!="")
					{
						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["˳��"].ToString();
						drnew["WorkNo"] = dr["����֤��"].ToString();
						drnew["EmployeeName"] = dr["����"].ToString();
						drnew["Sex"] = dr["�Ա�"].ToString();
						drnew["OrgPath"] = dr["OrgPath"].ToString();
						drnew["PostPath"] = dr["PostPath"].ToString();
						drnew["IsGroup"] = dr["�Ƿ���鳤"].ToString();
						drnew["Tech"] = dr["���ܵȼ�"].ToString();
						dt.Rows.Add(drnew);		
					}
				}
			}
			Grid1.DataSource = dt;
			Grid1.DataBind();

			lbltitle.Visible = true;
			Grid1.Visible = true;
			btnExcel.Visible = false;

			try
			{
				objBll.UpdateEmployeeImport(htOldWorkNo, htNewWorkNo,Convert.ToInt32(hfOrg.Value));
				SessionSet.PageMessage = "����ɹ���";
			}
            catch(Exception ex)
            {
            	throw  ex;
            }
		}

		protected void btnModify_Click(object sender, EventArgs e)
		{
			//���������ݵ�����Դ
			 DataTable dt = new DataTable();
			DataColumn dcnew1 = dt.Columns.Add("ExcelNo");
			DataColumn dcnew2 = dt.Columns.Add("WorkNo");
			DataColumn dcnew3 = dt.Columns.Add("EmployeeName");
			DataColumn dcnew4 = dt.Columns.Add("Sex");
			DataColumn dcnew5 = dt.Columns.Add("OrgPath");
			DataColumn dcnew6 = dt.Columns.Add("PostPath");
			DataColumn dcnew7 = dt.Columns.Add("IsGroup");
			DataColumn dcnew8 = dt.Columns.Add("Tech");
			DataColumn dcnew9 = dt.Columns.Add("ErrorReason");

			Hashtable htOrg = GetOrgHash(0);
			Hashtable htPost = GetPostHash();


			#region ��ȡExcel�ļ�
			if (File1.FileName == "")
			{
				SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
				return;
			}
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			string strSql;

			string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
			strSql = "select ���,Ա������,����֤��,�Ա�,����,��λ,������λ,Ա������,�Ƿ���ְ from [Sheet1$]";
			OleDbConnection ODcon = new OleDbConnection(strODConn);
			DataSet ds = new DataSet();
			try
			{
				ODcon.Open();
				OleDbCommand OCom = new OleDbCommand(strSql, ODcon);
				OleDbDataAdapter ODA = new OleDbDataAdapter(OCom);
				ODA.Fill(ds);
			}
			catch
			{
				SessionSet.PageMessage = "��˶�Excel�ļ��ĸ�ʽ��";
				return;
			}
			finally
			{
				ODcon.Close();
			}
			#endregion

			#region �������ݺ�����
			try
			{
				if (ds.Tables[0].Rows.Count == 0)
				{
					SessionSet.PageMessage = "Excel��û���κμ�¼����˶ԣ�";
					return;
				}

				DataColumn dc1 = ds.Tables[0].Columns.Add("OrgID");
				DataColumn dc2 = ds.Tables[0].Columns.Add("PostID");
				DataColumn dc3 = ds.Tables[0].Columns.Add("Dimission");

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					if (dr["�Ƿ���ְ"].ToString() == "��")
					{
						dr["Dimission"] = "1";
					}
					else
					{
						dr["Dimission"] = "0";
					}

					if (!htOrg.ContainsKey(dr["��λ"].ToString()))
					{
						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["���"].ToString();
						drnew["WorkNo"] = dr["Ա������"].ToString();
						drnew["EmployeeName"] = dr["����"].ToString();
						drnew["Sex"] = dr["�Ա�"].ToString();
						drnew["OrgPath"] = dr["��λ"].ToString();
						drnew["PostPath"] = dr["������λ"].ToString();
						drnew["IsGroup"] = "";
						drnew["Tech"] = "";
						drnew["ErrorReason"] = "��֯��������λ����ϵͳ�в�����";
						dt.Rows.Add(drnew);
					}
					
					if(!htPost.ContainsKey(dr["������λ"].ToString()))
					{
						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["���"].ToString();
						drnew["WorkNo"] = dr["Ա������"].ToString();
						drnew["EmployeeName"] = dr["����"].ToString();
						drnew["Sex"] = dr["�Ա�"].ToString();
						drnew["OrgPath"] = dr["��λ"].ToString();
						drnew["PostPath"] = dr["������λ"].ToString();
						drnew["IsGroup"] = "";
						drnew["Tech"] = "";
						drnew["ErrorReason"] = "������λ��ϵͳ�в�����";
						dt.Rows.Add(drnew);
					}

					if(dr["Ա������"].ToString() == "")
					{
						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["���"].ToString();
						drnew["WorkNo"] = dr["Ա������"].ToString();
						drnew["EmployeeName"] = dr["����"].ToString();
						drnew["Sex"] = dr["�Ա�"].ToString();
						drnew["OrgPath"] = dr["��λ"].ToString();
						drnew["PostPath"] = dr["������λ"].ToString();
						drnew["IsGroup"] = "";
						drnew["Tech"] = "";
						drnew["ErrorReason"] = "Ա�����벻��Ϊ��";
						dt.Rows.Add(drnew);
					}
				}
			}
			catch
			{
				SessionSet.PageMessage = "���鵥λ�͹�����λ�Ƿ�������ȷ��";
				return;
			}
			#endregion

			#region ����Excel��Ա������
			string str = "";
			string strLen = "";

			Hashtable htWorkNo = new Hashtable();

			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				if (htWorkNo.ContainsKey(dr["Ա������"].ToString().Trim()))
				{
					if (str == "")
					{
						str += dr["Ա������"].ToString();
					}
					else
					{
						str += "," + dr["Ա������"].ToString();
					}

					DataRow drnew = dt.NewRow();
					drnew["ExcelNo"] = dr["���"].ToString();
					drnew["WorkNo"] = dr["Ա������"].ToString();
					drnew["EmployeeName"] = dr["����"].ToString();
					drnew["Sex"] = dr["�Ա�"].ToString();
					drnew["OrgPath"] = dr["��λ"].ToString();
					drnew["PostPath"] = dr["������λ"].ToString();
					drnew["IsGroup"] = "";
					drnew["Tech"] = "";
					drnew["ErrorReason"] = "Ա��������Excel���ظ�";
					dt.Rows.Add(drnew);
				}
				else
				{
					if (dr["Ա������"].ToString() != "")
					{
						htWorkNo[dr["Ա������"].ToString()] = dr["Ա������"].ToString();
					}
				}

				if (dr["Ա������"].ToString().Trim().Length > 10)
				{
					if (strLen == "")
					{
						strLen += dr["Ա������"].ToString();
					}
					else
					{
						strLen += "," + dr["Ա������"].ToString();
					}

					DataRow drnew = dt.NewRow();
					drnew["ExcelNo"] = dr["���"].ToString();
					drnew["WorkNo"] = dr["Ա������"].ToString();
					drnew["EmployeeName"] = dr["����"].ToString();
					drnew["Sex"] = dr["�Ա�"].ToString();
					drnew["OrgPath"] = dr["��λ"].ToString();
					drnew["PostPath"] = dr["������λ"].ToString();
					drnew["IsGroup"] = "";
					drnew["Tech"] = "";
					drnew["ErrorReason"] = "Ա�����벻�ܴ���10λ";
					dt.Rows.Add(drnew);
				}
			}

			#endregion

			#region ����ȫ��Ա������
			str = "";
			EmployeeBLL objBll = new EmployeeBLL();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				if (dr["Ա������"].ToString().Trim() != null)
				{
					int count = objBll.GetEmployeeByWorkNo(dr["Ա������"].ToString());
					if (count > 0)
					{
						if (str == "")
						{
							str += dr["Ա������"].ToString();
						}
						else
						{
							str += "," + dr["Ա������"].ToString();
						}

						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["���"].ToString();
						drnew["WorkNo"] = dr["Ա������"].ToString();
						drnew["EmployeeName"] = dr["����"].ToString();
						drnew["Sex"] = dr["�Ա�"].ToString();
						drnew["OrgPath"] = dr["��λ"].ToString();
						drnew["PostPath"] = dr["������λ"].ToString();
						drnew["IsGroup"] = "";
						drnew["Tech"] = "";
						drnew["ErrorReason"] = "Ա��������ϵͳ���ظ�";
						dt.Rows.Add(drnew);
					}
				}
			}

			lbltitle.Text = "������Ҫ������ݣ�";
			lbltitle.Visible = true;
			Grid1.Visible = true;

			//if (str != "")
			//{
			//    SessionSet.PageMessage = "���ظ��Ĺ���֤�ţ�" + str;
			//    return;
			//}
			#endregion

			int n = 0;
			foreach (DataRow dr in dt.Rows)
			{
				if (dr["ExcelNo"].ToString() != "" || dr["WorkNo"].ToString() != "" || dr["OrgPath"].ToString() != "" || dr["PostPath"].ToString() != "")
				{
					n = n + 1;
				}
			}

			if(n >0)
			{

				Grid1.DataSource = dt;
				Grid1.DataBind();

				lbltitle.Text = "������Ҫ�������";
				lbltitle.Visible = true;
				Grid1.Visible = true;
				SessionSet.PageMessage = "��˶�Excel���ݺ������µ��룡";
				return;
			}
			else
			{
				lbltitle.Text = "";
			}

			#region ��������
			//try
			//{
				IList<RailExam.Model.Employee> objEmployeeUpdateList = new List<RailExam.Model.Employee>();
				IList<RailExam.Model.Employee> objEmployeeAddList = new List<RailExam.Model.Employee>();
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					if (dr["Ա������"].ToString() != "")
					{
						dr["OrgID"] = htOrg[dr["��λ"].ToString().Trim()].ToString();
						dr["PostID"] = htPost[dr["������λ"].ToString().Trim()].ToString();
						RailExam.Model.Employee obj = new RailExam.Model.Employee();
						obj.EmployeeID = Convert.ToInt32(dr["Ա������"].ToString());
						obj.EmployeeName = dr["����"].ToString().Trim();
						obj.OrgID = Convert.ToInt32(dr["OrgID"].ToString());
						obj.PostID = Convert.ToInt32(dr["PostID"].ToString());
						obj.Sex = dr["�Ա�"].ToString().Trim();
						obj.WorkNo= dr["Ա������"].ToString().Trim();
						obj. PostNo = dr["����֤��"].ToString();
						obj.PinYinCode = Pub.GetChineseSpell(obj.EmployeeName);
						if (dr["Dimission"].ToString() == "1")
						{
                            obj.IsOnPost = false;
						}
						else
						{
                            obj.IsOnPost =true;
						}
						objEmployeeUpdateList.Add(obj);
					}
					else
					{
						dr["OrgID"] = htOrg[dr["��λ"].ToString().Trim()].ToString();
						dr["PostID"] = htPost[dr["������λ"].ToString().Trim()].ToString();
						RailExam.Model.Employee obj = new RailExam.Model.Employee();
						obj.EmployeeName = dr["����"].ToString().Trim();
						obj.OrgID = Convert.ToInt32(dr["OrgID"].ToString());
						obj.PostID = Convert.ToInt32(dr["PostID"].ToString());
						obj.Sex = dr["�Ա�"].ToString().Trim();
						obj.WorkNo = dr["Ա������"].ToString().Trim();
						obj.PostNo = dr["����֤��"].ToString();
						obj.PinYinCode = Pub.GetChineseSpell(obj.EmployeeName);
                        obj.IsOnPost = true;
						obj.TechnicianTypeID = 1;
						obj.IsGroupLeader = 0;
						objEmployeeAddList.Add(obj);
					}
				}
				objBll.UpdateEmployee(objEmployeeUpdateList, objEmployeeAddList);

				SessionSet.PageMessage = "����ɹ���";
				dt.Clear();
				Grid1.DataSource = dt;
				Grid1.DataBind();
			//}
			//catch(Exception ex)
			//{
			//    SessionSet.PageMessage = "����ʧ�ܣ�" + ex.Message;
			//    return;
			//}
			#endregion
		}

		protected void btnInput_Click(object sender, EventArgs e)
		{
			if (File1.FileName == "")
			{
				SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
				return;
			}

			if(hfOrg.Value == "")
			{
				SessionSet.PageMessage = "��ѡ��λ��";
				return;
			}
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			ClientScript.RegisterStartupScript(GetType(), "import",
							"showProgressBar('" + strFileName + "','"+ hfOrg.Value +"')", true);
		}

		protected void btnExamOther_Click(object sender, EventArgs e)
		{
			if (File1.FileName == "")
			{
				SessionSet.PageMessage = "�����ѡ��Excel�ļ���";
				return;
			}

			if (hfOrg.Value == "")
			{
				SessionSet.PageMessage = "��ѡ��λ��";
				return;
			}
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			ClientScript.RegisterStartupScript(GetType(), "import",
							"showProgressBarExam('" + strFileName + "','" + hfOrg.Value + "')", true);
		}

		protected void btnBind_Click(object sender, EventArgs e)
		{
			EmployeeErrorBLL objErrorBll = new EmployeeErrorBLL();
			Grid1.DataSource = objErrorBll.GetEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(hfOrg.Value));
			Grid1.DataBind();
			Grid1.Visible = true;
		}
    }
}
