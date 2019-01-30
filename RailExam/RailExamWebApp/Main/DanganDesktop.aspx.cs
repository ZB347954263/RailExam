using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using RailExam.BLL;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using System.Collections.Generic;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Main
{
    public partial class DanganDesktop : PageBase
    {
        /// <summary>
        /// SON OF A BITCH!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }

                if (PrjPub.HasEditRight("职员管理") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                string selectStation = "select org_id, short_name from org where level_num = 2 order by parent_id,order_index";
                string selectPolitical = "select political_status_id, political_status_name from POLITICAL_STATUS order by order_index";
                string selectBZZ = "select workgroupleader_level_id, level_name from WORKGROUPLEADER_LEVEL t order by order_index";
                string selectTechicianTitleType = "select technician_title_type_id, type_name from TECHNICIAN_TITLE_TYPE t where order_index < 23 order by order_index";
                string selectTechicianType = "select technician_type_id, type_name from TECHNICIAN_TYPE t order by technician_type_id";
                string selectEduBg = "select education_level_id, education_level_name from EDUCATION_LEVEL t order by order_index";
                DropBindSource(this.dropStation, selectStation);
                DropBindSource(this.dropPolitical, selectPolitical);
                ChkListBindSource(this.chkListBZZ, selectBZZ);
                ChkListBindSource(this.chkListJSZC, selectTechicianTitleType);
                ChkListBindSource(this.chkListJNDJ, selectTechicianType);
                ChkListBindSource(this.chkListEduBg, selectEduBg);

                this.dropStation.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                dropStation_SelectedIndexChanged(null, null);

                if(PrjPub.CurrentLoginUser.SuitRange == 0)
                {
                    dropStation.Enabled = false;
                }

                OracleAccess access = new OracleAccess();
                DataSet ds= access.RunSqlDataSet(" select * from zj_certificate order by order_index");
                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = "--请选择--";
                drop_certificate.Items.Add(item);
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    item = new ListItem();
                    item.Value = dr["certificate_id"].ToString();
                    item.Text = dr["certificate_name"].ToString();
                    drop_certificate.Items.Add(item);
                }

                item = new ListItem();
                item.Value = "0";
                item.Text = "--请选择--";
                drop_certificate_Level.Items.Add(item);

                gridBind();
            }

            string strRefresh = Request.Form.Get("Refresh");
            if (!string.IsNullOrEmpty(strRefresh))
            {
                gridBind();
            }

            string strUpdate = Request.Form.Get("UpdatePsw");
            if (!string.IsNullOrEmpty(strUpdate))
            {
                SystemUserBLL objBll = new SystemUserBLL();
                SystemUser obj = objBll.GetUserByEmployeeID(Convert.ToInt32(strUpdate));
                if (obj != null)
                {
                    obj.Password = "111111";
                    if (PrjPub.IsServerCenter)
                    {
                        objBll.UpdateUser(obj);
                    }
                    else
                    {
                        objBll.UpdateUserPsw(obj.UserID, "111111");
                    }
                    SessionSet.PageMessage = "初始化密码成功！";
                }
                else
                {
                    SessionSet.PageMessage = "该员工登录帐户不存在，初始化密码失败！";
                }

                gridBind();
            }
        }

        protected void drop_certificate_SelectedIndexChanged(object sender, EventArgs e)
        {
            drop_certificate_Level.Items.Clear();
            OracleAccess access = new OracleAccess();
            string sql = string.Format(" select * from zj_certificate_level  where certificate_id={0}  order by order_index",
                               drop_certificate.SelectedValue == "" ? 0 : Convert.ToInt32(drop_certificate.SelectedValue));
            DataSet ds = access.RunSqlDataSet(sql);
            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--请选择--";
            drop_certificate_Level.Items.Add(item);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                item = new ListItem();
                item.Value = dr["certificate_level_id"].ToString();
                item.Text = dr["certificate_level_name"].ToString();
                drop_certificate_Level.Items.Add(item);
            }

        }

        private void gridBind()
        {
            hfSelect.Value = GetSql();
            grdEntity.DataBind();
        }

        /// <summary>
        /// 选中行是变色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
                {
                    e.Row.Visible = false;
                }
                else
                {
                    e.Row.Attributes.Add("onclick", "selectArow('" + e.Row.RowIndex + "');");
                }
            }
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
                return;
            }

            gridBind();
        }

        private string GetSql()
        {
            string id = Request.QueryString.Get("ID");

            string employeename = this.txtName.Text.Trim().ToString();
            string sex = this.ddlSex.SelectedValue.ToString();
            string strPath = Request.QueryString.Get("idpath");
            DataSet ds = new DataSet();
            OracleAccess ora = new OracleAccess();
            string strSql = string.Empty;

            if (id == "1" || id == "0")
            {
                strSql = "select EMPLOYEE_ID,EMPLOYEE_NAME,SEX,a.POST_ID,ISONPOST,a.MEMO,c.Post_Name,"
                    + " (case when IsONPost=1 then '是' else '否' end) as IsOnPostName "
                    + " from Employee a "
                    + " inner join Post c on a.Post_ID=c.Post_ID "
                    + " inner join Org b on a.Org_ID=b.Org_ID "
                    + " where 1=2";
            }
            else
            {
                strSql = "select a.*,ISONPOST,c.Post_Name,"
                         + " (case when IsONPost=1 then '是' else '否' end) as IsOnPostName,nvl(e.fingerCount,0) fingerCount,"
                         + " case when f.photo is null then '没有' else '有' end HasPhoto "
                         + " from Employee a "
                         + " inner join Post c on a.Post_ID=c.Post_ID "
                         + " inner join Org b on a.Org_ID=b.Org_ID "
                         + "left join (select employee_id,count(*) fingerCount from employee_fingerPrint group by employee_id) e"
                         + " on a.employee_id=e.employee_id "
                         + "left join employee_photo f on a.employee_id=f.employee_id";

                if(drop_certificate.SelectedValue != "0" || drop_certificate_Level.SelectedValue != "0" || ddlCheck.SelectedValue != "0" || ddlDate.SelectedValue != "0")
                {
                    strSql += " inner join (select distinct a.Employee_ID from ZJ_EMPLOYEE_CERTIFICATE a where 1=1";

                    if(drop_certificate.SelectedValue != "0")
                    {
                        strSql += " and certificate_id=" + drop_certificate.SelectedValue;
                    }

                    if(drop_certificate_Level.SelectedValue != "0")
                    {
                        strSql += " and certificate_level_id=" + drop_certificate_Level.SelectedValue;
                    }

                    //已复审
                    if(ddlCheck.SelectedValue == "1")
                    {
                        strSql += " and (CHECK_DATE is not null or CHECK_UNIT is not null or CHECK_RESULT is not null )";
                    }
                    else if(ddlCheck.SelectedValue == "2")
                    {
                         strSql += " and (CHECK_DATE is  null and CHECK_UNIT is  null and CHECK_RESULT is  null )";
                    }

                    //未超过
                    if(ddlDate.SelectedValue == "1")
                    {
                        strSql += " and END_DATE>=sysdate ";
                    }
                    else if(ddlDate.SelectedValue == "2")
                    {
                        strSql += " and End_Date<sysdate";
                    }

                    strSql += ") d on a.Employee_ID=d.Employee_ID";
                }


                strSql+= " where a.employee_id > 0 and b.id_Path||'/' like '%" + strPath + "/%' ";

                if (!string.IsNullOrEmpty(employeename))
                {
                    strSql += " and a.Employee_Name like '%" + employeename + "%'";
                }

                if (ddlSex.SelectedValue != "")
                {
                    strSql += " and Sex='" + sex + "'";
                }

                strSql += " and IsOnPost=" + ddlStatus.SelectedValue;

                if (this.txtAge1.Text.Trim().Length > 0 && this.txtAge2.Text.Trim().Length > 0)
                {
                    int age1, age2;
                    if (Int32.TryParse(this.txtAge1.Text, out age1) && Int32.TryParse(this.txtAge2.Text, out age2))
                        strSql += " and trunc(months_between(sysdate,BIRTHDAY)/12) >= " + this.txtAge1.Text.Trim()
                            + " and trunc(months_between(sysdate,BIRTHDAY)/12) <= " + this.txtAge2.Text.Trim();
                }
                else if (this.txtAge1.Text.Trim().Length > 0)
                {
                    int age1;
                    if (Int32.TryParse(this.txtAge1.Text, out age1))
                        strSql += " and trunc(months_between(sysdate,BIRTHDAY)/12) >= " + this.txtAge1.Text.Trim();
                }
                else if (this.txtAge2.Text.Trim().Length > 0)
                {
                    int age2;
                    if (Int32.TryParse(this.txtAge2.Text, out age2))
                        strSql += " and trunc(months_between(sysdate,BIRTHDAY)/12) <= " + this.txtAge2.Text.Trim();
                }

                if (this.txtIDCard.Text.Trim().Length > 0)
                {
                    strSql += " and IDENTITY_CARDNO like '" + this.txtIDCard.Text.Trim() + "%'";
                }

                if (this.txtWorkNo.Text.Trim().Length > 0)
                {
                    strSql += " and WORK_NO like '" + this.txtWorkNo.Text.Trim() + "%'";
                }

                if (this.dropStation.SelectedIndex > 0)
                {
                    strSql += " and getstationorgid(a.org_id) = " + this.dropStation.SelectedValue;
                }

                if (this.dropShop.SelectedIndex > 0)
                {
                    strSql += String.Format(@" and b.id_path||'/' like '/1/{0}/{1}/%'", this.dropStation.SelectedValue, this.dropShop.SelectedValue);
                }

                if (this.txtPostName.Text.Length > 0)
                {
                    strSql += " and a.post_id = " + this.hfPostID.Value;
                }

                if (this.txtNowPostName.Text.Length > 0)
                {
                    strSql += " and a.now_post_id = " + this.hfNowPostID.Value;
                }

                if (this.txtPinYin.Text.Trim().Length > 0)
                {
                    strSql += " and a.PINYIN_CODE = '" + this.txtPinYin.Text.Trim().ToUpper() + "'";
                }

                if (this.dropPolitical.SelectedIndex > 0)
                {
                    strSql += " and a.POLITICAL_STATUS_ID = " + this.dropPolitical.SelectedValue;
                }

                if (this.chkListBZZ.SelectedIndex != -1)
                {
                    string bzz = String.Empty;
                    for (int i = 1; i < chkListBZZ.Items.Count; i++)
                    {
                        if (this.chkListBZZ.Items[i].Selected)
                        {
                            bzz += String.IsNullOrEmpty(bzz) ? this.chkListBZZ.Items[i].Value : "," + this.chkListBZZ.Items[i].Value;
                        }
                    }
                    if (this.chkListBZZ.Items[0].Selected && !String.IsNullOrEmpty(bzz))
                    {
                        strSql += " and (a.IS_GROUP_LEADER = 0 or a.WORKGROUPLEADER_TYPE_ID in (" + bzz + "))";
                    }
                    else if (this.chkListBZZ.Items[0].Selected)
                    {
                        strSql += " and a.IS_GROUP_LEADER = 0";
                    }
                    else if (!String.IsNullOrEmpty(bzz))
                    {
                        strSql += " and a.WORKGROUPLEADER_TYPE_ID in (" + bzz + ")";
                    }

                }

                if (this.chkListJSZC.SelectedIndex != -1)
                {
                    string jszc = String.Empty;
                    foreach (ListItem li in this.chkListJSZC.Items)
                    {
                        if (li.Selected)
                            jszc += String.IsNullOrEmpty(jszc) ? li.Value : "," + li.Value;
                    }
                    if (!String.IsNullOrEmpty(jszc))
                    {
                        strSql += " and a.TECHNICAL_TITLE_ID in (" + jszc + ")";
                    }
                }

                if (this.chkListJNDJ.SelectedIndex != -1)
                {
                    string jndj = String.Empty;
                    foreach (ListItem li in this.chkListJNDJ.Items)
                    {
                        if (li.Selected)
                            jndj += String.IsNullOrEmpty(jndj) ? li.Value : "," + li.Value;
                    }
                    if (!String.IsNullOrEmpty(jndj))
                    {
                        strSql += " and a.TECHNICIAN_TYPE_ID in (" + jndj + ")";
                    }
                }

                if (this.chkListEduBg.SelectedIndex != -1)
                {
                    string edubg = String.Empty;
                    foreach (ListItem li in this.chkListEduBg.Items)
                    {
                        if (li.Selected)
                            edubg += String.IsNullOrEmpty(edubg) ? li.Value : "," + li.Value;
                    }
                    if (!String.IsNullOrEmpty(edubg))
                    {
                        strSql += " and a.EDUCATION_LEVEL_ID in (" + edubg + ")";
                    }
                }

                strSql += " order by b.Level_Num,b.Order_Index";
            }

            return strSql;
        }


        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
            if (db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["Employee_ID"] = -1;
                db.Rows.Add(row);
            }
            else
            {
                ViewState["dt"] = db;
            }
        }

        private void DropBindSource(DropDownList drop, string sql)
        {
            OracleAccess ora = new OracleAccess();
            try
            {
                DataSet ds = ora.RunSqlDataSet(sql);
                DataRow newRow = ds.Tables[0].NewRow();
                newRow[0] = -1;
                newRow[1] = "请选择";
                ds.Tables[0].Rows.InsertAt(newRow, 0);
                drop.DataSource = ds.Tables[0];
                drop.DataBind();
            }
            catch { }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (ViewState["dt"] != null && hfRefreshExcel.Value == "")
                Session["EmployeeDangan"] = ViewState["dt"];

            if (hfRefreshExcel.Value == "true")
            {
                hfRefreshExcel.Value = "";
                DownloadExcel("电子档案");
            }
        }


        private void DownloadExcel(string strName)
        {
            string filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

            if (File.Exists(filename))
            {
                FileInfo file = new FileInfo(filename);
                this.Response.Clear();
                this.Response.Buffer = true;
                this.Response.Charset = "utf-7";
                this.Response.ContentEncoding = Encoding.UTF7;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                this.Response.AddHeader("Content-Disposition",
                                        "attachment; filename=" + HttpUtility.UrlEncode(strName) + ".xls");
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                this.Response.AddHeader("Content-Length", file.Length.ToString());

                // 指定返回的是一个不能被客户端读取的流，必须被下载

                this.Response.ContentType = "application/ms-excel";

                // 把文件流发送到客户端

                this.Response.WriteFile(file.FullName);
            }
        }

        protected void dropStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dropStation.SelectedIndex > 0)
            {
                string stationID = this.dropStation.SelectedValue;
                string selectShop = String.Format("select org_id, short_name from org where parent_id = {0}", stationID);
                DropBindSource(this.dropShop, selectShop);
            }
        }

        private void ChkListBindSource(CheckBoxList chkList, string sql)
        {
            OracleAccess ora = new OracleAccess();
            try
            {
                DataSet ds = ora.RunSqlDataSet(sql);
                if (chkList.ID == "chkListBZZ")
                {
                    DataRow newRow = ds.Tables[0].NewRow();
                    newRow[0] = -1;
                    newRow[1] = "无班组长";
                    ds.Tables[0].Rows.InsertAt(newRow, 0);
                }
                chkList.DataSource = ds.Tables[0];
                chkList.DataBind();
            }
            catch { }
        }
    }
}
