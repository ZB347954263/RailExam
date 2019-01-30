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
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
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

				Grid1.Levels[0].Columns[1].HeadingText = "上岗证号";

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
                SessionSet.PageMessage = "请浏览选择Excel文件！";
                return;
            }
            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/"+strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            string strSql;

            string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
            strSql = "select 顺号,站段名称,车间,班组,姓名,系统,工种,职名,性别,工作证号,是否班组长,技能等级 from [Sheet1$]";
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
                SessionSet.PageMessage = "请核对Excel文件的格式！";
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
                    SessionSet.PageMessage = "Excel中没有任何记录，请核对！";
                    return;
                }

                if (ds.Tables[0].Rows[0]["站段名称"].ToString() != txtOrg.Text.Substring(txtOrg.Text.IndexOf('/') + 1))
                {
                    SessionSet.PageMessage = "站段选择错误，请核对！";
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
                    if (dr["班组"].ToString() != "")
                    {
                        dr["OrgPath"] = dr["站段名称"].ToString().Trim() + "-" + dr["车间"].ToString().Trim() + "-" + dr["班组"].ToString().Trim();
                    }
                    else
                    {
                        dr["OrgPath"] = dr["站段名称"].ToString().Trim() + "-" + dr["车间"].ToString().Trim();
                    }
                    dr["PostPath"] = dr["系统"].ToString().Trim() + "-" + dr["工种"].ToString().Trim() + "-" + dr["职名"].ToString().Trim();

                    //dr["OrgID"] = objOrgBll.GetOrgIDByOrgNamePath(dr["OrgPath"].ToString()).ToString();
                    //dr["PostID"] = objPostBll.GetPostIDByPostNamePath(dr["PostPath"].ToString()).ToString();

                    if(dr["是否班组长"].ToString() == "是")
                    {
                        dr["IsGroup"] = "1";
                    }
                    else
                    {
                        dr["IsGroup"] = "0";
                    }

                    if(dr["技能等级"].ToString() == "高级技师")
                    {
                        dr["TechID"] = "3";
                    }
                    else if (dr["技能等级"].ToString() == "技师")
                    {
                        dr["TechID"] = "2";
                    }
                    else
                    {
                        dr["TechID"] = "1";
                    }

                    if (!htOrg.ContainsKey(dr["OrgPath"].ToString()) || !htPost.ContainsKey(dr["PostPath"].ToString()) || dr["工作证号"].ToString() == "")
                    {
                        DataRow drnew = dt.NewRow();
                        drnew["ExcelNo"] = dr["顺号"].ToString();
                        drnew["WorkNo"] = dr["工作证号"].ToString();
                        drnew["EmployeeName"] = dr["姓名"].ToString();
                        drnew["Sex"] = dr["性别"].ToString();
                        drnew["OrgPath"] = dr["OrgPath"].ToString();
                        drnew["PostPath"] = dr["PostPath"].ToString();
                        drnew["IsGroup"] = dr["是否班组长"].ToString();
                        drnew["Tech"] = dr["技能等级"].ToString();
                        dt.Rows.Add(drnew);
                    }
                    else
                    {
                        dr["OrgID"] = htOrg[dr["OrgPath"].ToString()].ToString();
                        dr["PostID"] = htPost[dr["PostPath"].ToString()].ToString();
                        RailExam.Model.Employee obj = new RailExam.Model.Employee();
                        obj.EmployeeName = dr["姓名"].ToString().Trim();
                        obj.OrgID = Convert.ToInt32(dr["OrgID"].ToString());
                        obj.PostID = Convert.ToInt32(dr["PostID"].ToString());
                        obj.Sex = dr["性别"].ToString().Trim();
                        obj.WorkNo = dr["工作证号"].ToString().Trim();
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
                SessionSet.PageMessage = "导入失败！";
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
                SessionSet.PageMessage = "请浏览选择Excel文件！";
                return;
            }
            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            string strSql;

            string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
            strSql = "select 顺号,站段名称,车间,班组,姓名,系统,工种,职名,性别,工作证号,是否班组长,技能等级 from [Sheet1$]";
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
                SessionSet.PageMessage = "请核对Excel文件的格式！";
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
                    SessionSet.PageMessage = "Excel中没有任何记录，请核对！";
                    return;
                }

                if (ds.Tables[0].Rows[0]["站段名称"].ToString() != txtOrg.Text.Substring(txtOrg.Text.IndexOf('/') + 1))
                {
                    SessionSet.PageMessage = "站段选择错误，请核对！";
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
                    if (dr["班组"].ToString() != "")
                    {
                        dr["OrgPath"] = dr["站段名称"].ToString().Trim() + "-" + dr["车间"].ToString().Trim() + "-" + dr["班组"].ToString().Trim();
                    }
                    else
                    {
                        dr["OrgPath"] = dr["站段名称"].ToString().Trim() + "-" + dr["车间"].ToString().Trim();
                    }
                    dr["PostPath"] = dr["系统"].ToString().Trim() + "-" + dr["工种"].ToString().Trim() + "-" + dr["职名"].ToString().Trim();

                    //dr["OrgID"] = objOrgBll.GetOrgIDByOrgNamePath(dr["OrgPath"].ToString()).ToString();
                    //dr["PostID"] = objPostBll.GetPostIDByPostNamePath(dr["PostPath"].ToString()).ToString();

                    if (dr["是否班组长"].ToString() == "是")
                    {
                        dr["IsGroup"] = "1";
                    }
                    else
                    {
                        dr["IsGroup"] = "0";
                    }

                    if (dr["技能等级"].ToString() == "高级技师")
                    {
                        dr["TechID"] = "3";
                    }
                    else if (dr["技能等级"].ToString() == "技师")
                    {
                        dr["TechID"] = "2";
                    }
                    else
                    {
                        dr["TechID"] = "1";
                    }

                    if (!htOrg.ContainsKey(dr["OrgPath"].ToString()) || !htPost.ContainsKey(dr["PostPath"].ToString()) || dr["工作证号"].ToString() == "")
                    {
                        DataRow drnew = dt.NewRow();
                        drnew["ExcelNo"] = dr["顺号"].ToString();
                        drnew["WorkNo"] = dr["工作证号"].ToString();
                        drnew["EmployeeName"] = dr["姓名"].ToString();
                        drnew["Sex"] = dr["性别"].ToString();
                        drnew["OrgPath"] = dr["OrgPath"].ToString();
                        drnew["PostPath"] = dr["PostPath"].ToString();
                        drnew["IsGroup"] = dr["是否班组长"].ToString();
                        drnew["Tech"] = dr["技能等级"].ToString();
                        dt.Rows.Add(drnew);
                    }
                }

                Grid1.DataSource = dt;
                Grid1.DataBind();

            	lbltitle.Text = "不符合要求的数据！";
                lbltitle.Visible = true;
                Grid1.Visible = true;
                btnExcel.Visible = false;
            }
            catch
            {
                SessionSet.PageMessage = "请检查组织机构是否唯一！";
                return;
            }
        }

        protected void btnExamSelect_Click(object sender, EventArgs e)
        {
            if (File1.FileName == "")
            {
                SessionSet.PageMessage = "请浏览选择Excel文件！";
                return;
            }

            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            string strSql;

            string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
            strSql = "select 顺号,站段名称,车间,班组,姓名,系统,工种,职名,性别,工作证号,是否班组长,技能等级 from [Sheet1$]";
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
                SessionSet.PageMessage = "请核对Excel文件的格式！";
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
                if (htWorkNo.ContainsKey(dr["工作证号"].ToString().Trim()))
                {
                    if (str == "")
                    {
                        str += dr["工作证号"].ToString();
                    }
                    else
                    {
                        str += "," + dr["工作证号"].ToString();
                    }
                }
                else
                {
                    if (dr["工作证号"].ToString() != "")
                    {
                        htWorkNo[dr["工作证号"].ToString()] = dr["工作证号"].ToString();
                    }
                }

                if(dr["工作证号"].ToString().Trim().Length > 13 )
                {
                    if(strLen == "")
                    {
                        strLen += dr["工作证号"].ToString();
                    }
                    else
                    {
                        strLen += "," + dr["工作证号"].ToString();
                    }
                }
            }

            if(strLen != "")
            {
                SessionSet.PageMessage = "有大于13位工作证号：" + strLen;
                return;
            }

            if (str != "")
            {
                SessionSet.PageMessage = "有重复的工作证号：" + str;
            }
            else
            {
                SessionSet.PageMessage = "没有重复的工作证号！";
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
                SessionSet.PageMessage = "请浏览选择Excel文件！";
                return;
            }

            string strFileName = Path.GetFileName(File1.PostedFile.FileName);
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

            if (File.Exists(strPath))
                File.Delete(strPath);

            ((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

            string strSql;

            string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
            strSql = "select 顺号,站段名称,车间,班组,姓名,系统,工种,职名,性别,工作证号,是否班组长,技能等级 from [Sheet1$]";
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
                SessionSet.PageMessage = "请核对Excel文件的格式！";
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
                if (htWorkNo.ContainsKey(dr["工作证号"].ToString().Trim()))
                {
                    if (str == "")
                    {
                        str += dr["工作证号"].ToString();
                    }
                    else
                    {
                        str += "," + dr["工作证号"].ToString();
                    }
                }

                if (dr["工作证号"].ToString().Trim().Length > 13)
                {
                    if (strLen == "")
                    {
                        strLen += dr["工作证号"].ToString();
                    }
                    else
                    {
                        strLen += "," + dr["工作证号"].ToString();
                    }
                }
            }

            if (strLen != "")
            {
                SessionSet.PageMessage = "有大于13位工作证号：" + strLen;
                return;
            }

            if (str != "")
            {
                SessionSet.PageMessage = "有重复的工作证号：" + str;
            }
            else
            {
                SessionSet.PageMessage = "没有重复的工作证号！";
            }
        }

		protected void btnImportModify_Click(object sender, EventArgs e)
		{
			if (File1.FileName == "")
			{
				SessionSet.PageMessage = "请浏览选择Excel文件！";
				return;
			}
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			string strSql;

			string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
			strSql = "select 顺号,站段名称,车间,班组,姓名,系统,工种,职名,性别,工作证号,是否班组长,技能等级,原工作证号 from [Sheet1$]";
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
				SessionSet.PageMessage = "当前导入需要有“原工作证号”字段，请核对Excel文件的格式！";
				return;
			}
			finally
			{
				ODcon.Close();
			}

			if (ds.Tables[0].Rows.Count == 0)
			{
				SessionSet.PageMessage = "Excel中没有任何记录，请核对！";
				return;
			}

			if (ds.Tables[0].Rows[0]["站段名称"].ToString() != txtOrg.Text.Substring(txtOrg.Text.IndexOf('/') + 1))
			{
				SessionSet.PageMessage = "站段选择错误，请核对！";
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
				if (htWorkNo.ContainsKey(dr["原工作证号"].ToString().Trim()))
				{
					htOldWorkNo[i] = dr["原工作证号"].ToString().Trim();
					htNewWorkNo[i] = dr["工作证号"].ToString().Trim();
					i = i + 1;
				}
				else
				{
					if (dr["班组"].ToString() != "")
					{
						dr["OrgPath"] = dr["站段名称"].ToString().Trim() + "-" + dr["车间"].ToString().Trim() + "-" + dr["班组"].ToString().Trim();
					}
					else
					{
						dr["OrgPath"] = dr["站段名称"].ToString().Trim() + "-" + dr["车间"].ToString().Trim();
					}

					dr["PostPath"] = dr["系统"].ToString().Trim() + "-" + dr["工种"].ToString().Trim() + "-" + dr["职名"].ToString().Trim();

					if(dr["原工作证号"].ToString().Trim()!="")
					{
						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["顺号"].ToString();
						drnew["WorkNo"] = dr["工作证号"].ToString();
						drnew["EmployeeName"] = dr["姓名"].ToString();
						drnew["Sex"] = dr["性别"].ToString();
						drnew["OrgPath"] = dr["OrgPath"].ToString();
						drnew["PostPath"] = dr["PostPath"].ToString();
						drnew["IsGroup"] = dr["是否班组长"].ToString();
						drnew["Tech"] = dr["技能等级"].ToString();
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
				SessionSet.PageMessage = "导入成功！";
			}
            catch(Exception ex)
            {
            	throw  ex;
            }
		}

		protected void btnModify_Click(object sender, EventArgs e)
		{
			//不符合数据的数据源
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


			#region 读取Excel文件
			if (File1.FileName == "")
			{
				SessionSet.PageMessage = "请浏览选择Excel文件！";
				return;
			}
			string strFileName = Path.GetFileName(File1.PostedFile.FileName);
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);

			if (File.Exists(strPath))
				File.Delete(strPath);

			((HttpPostedFile)File1.PostedFile).SaveAs(strPath);

			string strSql;

			string strODConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPath + "; Extended Properties=Excel 8.0;";
			strSql = "select 序号,员工编码,工作证号,性别,姓名,单位,工作岗位,员工编码,是否离职 from [Sheet1$]";
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
				SessionSet.PageMessage = "请核对Excel文件的格式！";
				return;
			}
			finally
			{
				ODcon.Close();
			}
			#endregion

			#region 检验数据合理性
			try
			{
				if (ds.Tables[0].Rows.Count == 0)
				{
					SessionSet.PageMessage = "Excel中没有任何记录，请核对！";
					return;
				}

				DataColumn dc1 = ds.Tables[0].Columns.Add("OrgID");
				DataColumn dc2 = ds.Tables[0].Columns.Add("PostID");
				DataColumn dc3 = ds.Tables[0].Columns.Add("Dimission");

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					if (dr["是否离职"].ToString() == "是")
					{
						dr["Dimission"] = "1";
					}
					else
					{
						dr["Dimission"] = "0";
					}

					if (!htOrg.ContainsKey(dr["单位"].ToString()))
					{
						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["序号"].ToString();
						drnew["WorkNo"] = dr["员工编码"].ToString();
						drnew["EmployeeName"] = dr["姓名"].ToString();
						drnew["Sex"] = dr["性别"].ToString();
						drnew["OrgPath"] = dr["单位"].ToString();
						drnew["PostPath"] = dr["工作岗位"].ToString();
						drnew["IsGroup"] = "";
						drnew["Tech"] = "";
						drnew["ErrorReason"] = "组织机构（单位）在系统中不存在";
						dt.Rows.Add(drnew);
					}
					
					if(!htPost.ContainsKey(dr["工作岗位"].ToString()))
					{
						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["序号"].ToString();
						drnew["WorkNo"] = dr["员工编码"].ToString();
						drnew["EmployeeName"] = dr["姓名"].ToString();
						drnew["Sex"] = dr["性别"].ToString();
						drnew["OrgPath"] = dr["单位"].ToString();
						drnew["PostPath"] = dr["工作岗位"].ToString();
						drnew["IsGroup"] = "";
						drnew["Tech"] = "";
						drnew["ErrorReason"] = "工作岗位在系统中不存在";
						dt.Rows.Add(drnew);
					}

					if(dr["员工编码"].ToString() == "")
					{
						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["序号"].ToString();
						drnew["WorkNo"] = dr["员工编码"].ToString();
						drnew["EmployeeName"] = dr["姓名"].ToString();
						drnew["Sex"] = dr["性别"].ToString();
						drnew["OrgPath"] = dr["单位"].ToString();
						drnew["PostPath"] = dr["工作岗位"].ToString();
						drnew["IsGroup"] = "";
						drnew["Tech"] = "";
						drnew["ErrorReason"] = "员工编码不能为空";
						dt.Rows.Add(drnew);
					}
				}
			}
			catch
			{
				SessionSet.PageMessage = "请检查单位和工作岗位是否设置正确！";
				return;
			}
			#endregion

			#region 检验Excel中员工编码
			string str = "";
			string strLen = "";

			Hashtable htWorkNo = new Hashtable();

			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				if (htWorkNo.ContainsKey(dr["员工编码"].ToString().Trim()))
				{
					if (str == "")
					{
						str += dr["员工编码"].ToString();
					}
					else
					{
						str += "," + dr["员工编码"].ToString();
					}

					DataRow drnew = dt.NewRow();
					drnew["ExcelNo"] = dr["序号"].ToString();
					drnew["WorkNo"] = dr["员工编码"].ToString();
					drnew["EmployeeName"] = dr["姓名"].ToString();
					drnew["Sex"] = dr["性别"].ToString();
					drnew["OrgPath"] = dr["单位"].ToString();
					drnew["PostPath"] = dr["工作岗位"].ToString();
					drnew["IsGroup"] = "";
					drnew["Tech"] = "";
					drnew["ErrorReason"] = "员工编码在Excel中重复";
					dt.Rows.Add(drnew);
				}
				else
				{
					if (dr["员工编码"].ToString() != "")
					{
						htWorkNo[dr["员工编码"].ToString()] = dr["员工编码"].ToString();
					}
				}

				if (dr["员工编码"].ToString().Trim().Length > 10)
				{
					if (strLen == "")
					{
						strLen += dr["员工编码"].ToString();
					}
					else
					{
						strLen += "," + dr["员工编码"].ToString();
					}

					DataRow drnew = dt.NewRow();
					drnew["ExcelNo"] = dr["序号"].ToString();
					drnew["WorkNo"] = dr["员工编码"].ToString();
					drnew["EmployeeName"] = dr["姓名"].ToString();
					drnew["Sex"] = dr["性别"].ToString();
					drnew["OrgPath"] = dr["单位"].ToString();
					drnew["PostPath"] = dr["工作岗位"].ToString();
					drnew["IsGroup"] = "";
					drnew["Tech"] = "";
					drnew["ErrorReason"] = "员工编码不能大于10位";
					dt.Rows.Add(drnew);
				}
			}

			#endregion

			#region 检验全局员工编码
			str = "";
			EmployeeBLL objBll = new EmployeeBLL();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				if (dr["员工编码"].ToString().Trim() != null)
				{
					int count = objBll.GetEmployeeByWorkNo(dr["员工编码"].ToString());
					if (count > 0)
					{
						if (str == "")
						{
							str += dr["员工编码"].ToString();
						}
						else
						{
							str += "," + dr["员工编码"].ToString();
						}

						DataRow drnew = dt.NewRow();
						drnew["ExcelNo"] = dr["序号"].ToString();
						drnew["WorkNo"] = dr["员工编码"].ToString();
						drnew["EmployeeName"] = dr["姓名"].ToString();
						drnew["Sex"] = dr["性别"].ToString();
						drnew["OrgPath"] = dr["单位"].ToString();
						drnew["PostPath"] = dr["工作岗位"].ToString();
						drnew["IsGroup"] = "";
						drnew["Tech"] = "";
						drnew["ErrorReason"] = "员工编码在系统中重复";
						dt.Rows.Add(drnew);
					}
				}
			}

			lbltitle.Text = "不符合要求的数据！";
			lbltitle.Visible = true;
			Grid1.Visible = true;

			//if (str != "")
			//{
			//    SessionSet.PageMessage = "有重复的工作证号：" + str;
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

				lbltitle.Text = "不符合要求的数据";
				lbltitle.Visible = true;
				Grid1.Visible = true;
				SessionSet.PageMessage = "请核对Excel数据后，再重新导入！";
				return;
			}
			else
			{
				lbltitle.Text = "";
			}

			#region 导入数据
			//try
			//{
				IList<RailExam.Model.Employee> objEmployeeUpdateList = new List<RailExam.Model.Employee>();
				IList<RailExam.Model.Employee> objEmployeeAddList = new List<RailExam.Model.Employee>();
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					if (dr["员工编码"].ToString() != "")
					{
						dr["OrgID"] = htOrg[dr["单位"].ToString().Trim()].ToString();
						dr["PostID"] = htPost[dr["工作岗位"].ToString().Trim()].ToString();
						RailExam.Model.Employee obj = new RailExam.Model.Employee();
						obj.EmployeeID = Convert.ToInt32(dr["员工编码"].ToString());
						obj.EmployeeName = dr["姓名"].ToString().Trim();
						obj.OrgID = Convert.ToInt32(dr["OrgID"].ToString());
						obj.PostID = Convert.ToInt32(dr["PostID"].ToString());
						obj.Sex = dr["性别"].ToString().Trim();
						obj.WorkNo= dr["员工编码"].ToString().Trim();
						obj. PostNo = dr["工作证号"].ToString();
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
						dr["OrgID"] = htOrg[dr["单位"].ToString().Trim()].ToString();
						dr["PostID"] = htPost[dr["工作岗位"].ToString().Trim()].ToString();
						RailExam.Model.Employee obj = new RailExam.Model.Employee();
						obj.EmployeeName = dr["姓名"].ToString().Trim();
						obj.OrgID = Convert.ToInt32(dr["OrgID"].ToString());
						obj.PostID = Convert.ToInt32(dr["PostID"].ToString());
						obj.Sex = dr["性别"].ToString().Trim();
						obj.WorkNo = dr["员工编码"].ToString().Trim();
						obj.PostNo = dr["工作证号"].ToString();
						obj.PinYinCode = Pub.GetChineseSpell(obj.EmployeeName);
                        obj.IsOnPost = true;
						obj.TechnicianTypeID = 1;
						obj.IsGroupLeader = 0;
						objEmployeeAddList.Add(obj);
					}
				}
				objBll.UpdateEmployee(objEmployeeUpdateList, objEmployeeAddList);

				SessionSet.PageMessage = "导入成功！";
				dt.Clear();
				Grid1.DataSource = dt;
				Grid1.DataBind();
			//}
			//catch(Exception ex)
			//{
			//    SessionSet.PageMessage = "导入失败：" + ex.Message;
			//    return;
			//}
			#endregion
		}

		protected void btnInput_Click(object sender, EventArgs e)
		{
			if (File1.FileName == "")
			{
				SessionSet.PageMessage = "请浏览选择Excel文件！";
				return;
			}

			if(hfOrg.Value == "")
			{
				SessionSet.PageMessage = "请选择单位！";
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
				SessionSet.PageMessage = "请浏览选择Excel文件！";
				return;
			}

			if (hfOrg.Value == "")
			{
				SessionSet.PageMessage = "请选择单位！";
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
