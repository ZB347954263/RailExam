using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing.Imaging;
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
using System.Xml;
using System.Drawing;
using System.IO;
using Oracle.DataAccess.Client;
using System.Data.OracleClient;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeInfoDetail : PageBase
    {
        private bool isReadOnly = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //给FileUpLoad控件添加即时显示图片的事件
            this.fileUpload1.Attributes.Add("onchange", "showSelectedImage()");

            if (!IsPostBack)
            {
                initXiaLa();
                string type = Request.QueryString["Type"];
                string employeeid = Request.QueryString["ID"];
                if (!string.IsNullOrEmpty(type))
                {
                    int typeBool = int.Parse(type);
                    isReadOnly = typeBool == 0 ? true : false;
                    if (isReadOnly)
                    {
                        setReadOnlyControl();
                    }
                }
                if (!string.IsNullOrEmpty(employeeid))
                {
                    OracleAccess ora = new OracleAccess();
                    DataSet ds = ora.RunSqlDataSet(" select * from employee where EMPLOYEE_ID = " + employeeid);
                    loadData(ds);
                }
                else
                {
                    txtPinYin.Enabled = false;
                    imgORG.Visible = true;
                    ddlSafe.Enabled = true;
                    ddlTECHNICIAN_TYPE_ID.Enabled = true;
                    datePost.Enabled = true;
                    dateTechnicalDate.Enabled = true;
                    Image2.Visible = true;
                    DDLeducation_level_id.Enabled = true;

                    string orgID = Request.QueryString.Get("OrgID");

                    OrganizationBLL orgBll = new OrganizationBLL();
                    Organization org = orgBll.GetOrganization(Convert.ToInt32(orgID));
                    txtORG.Text = org.ShortName;
                    hfOrgID.Value = org.OrganizationId.ToString();

                    hfStationOrgID.Value = orgBll.GetStationOrgID(org.OrganizationId).ToString();
                    ddlISREGISTERED.SelectedValue = "1";
                    ddlIsGroup.SelectedValue = "0";
                    cbISONPOST.Checked = true;
                }
            }

            if (!string.IsNullOrEmpty(hfOrgID.Value))
            {
                DataSet ds = new DataSet();
                OracleAccess ora = new OracleAccess();
                ds = ora.RunSqlDataSet("select a.*,GetOrgName(org_id) orgPath from ORG a where ORG_ID=" + Convert.ToInt32(hfOrgID.Value));
                //OrganizationBLL orgBll = new OrganizationBLL();
                //Organization org = orgBll.GetOrganization(Convert.ToInt32(hfOrgID.Value));
                if (ds.Tables.Count > 0)
                {
                    txtORG.Text = ds.Tables[0].Rows[0]["orgPath"].ToString();
                    //txtORG.Text = org.ShortName;
                }
            }

            if (!string.IsNullOrEmpty(hfPostID.Value))
            {
                PostBLL postBll = new PostBLL();
                Post post = postBll.GetPost(Convert.ToInt32(hfPostID.Value));
                txtPOST.Text = post.PostName;
            }

            if (!string.IsNullOrEmpty(hfNowPostID.Value))
            {
                PostBLL postBll = new PostBLL();
                Post post = postBll.GetPost(Convert.ToInt32(hfNowPostID.Value));
                txtNowPost.Text = post.PostName;
            }
        }

        private void initXiaLa()
        {
            //班组长
            OracleAccess ora = new OracleAccess();
            DataSet ds = ora.RunSqlDataSet(" select WORKGROUPLEADER_LEVEL_ID,LEVEL_NAME from WORKGROUPLEADER_LEVEL order by ORDER_INDEX ");
            ddlWORKGROUPLEADER_TYPE_ID.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                ddlWORKGROUPLEADER_TYPE_ID.Items.Add(new ListItem("--请选择--", "-1"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlWORKGROUPLEADER_TYPE_ID.Items.Add(new ListItem(dr["LEVEL_NAME"].ToString(), dr["WORKGROUPLEADER_LEVEL_ID"].ToString()));
                }
            }
            //政治面貌
            ora = new OracleAccess();
            ds = ora.RunSqlDataSet(" select POLITICAL_STATUS_ID,POLITICAL_STATUS_NAME from POLITICAL_STATUS order by ORDER_INDEX ");
            ddlPOLITICAL_STATUS.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlPOLITICAL_STATUS.Items.Add(new ListItem(dr["POLITICAL_STATUS_NAME"].ToString(), dr["POLITICAL_STATUS_ID"].ToString()));
                }
            }
            //职教人员类型  
            ora = new OracleAccess();
            ds = ora.RunSqlDataSet(" select EDUCATION_EMPLOYEE_TYPE_ID,EDUCATION_EMPLOYEE_TYPE_NAME from ZJ_EDUCATION_EMPLOYEE_TYPE ");
            ddlEDUCATION_EMPLOYEE_TYPE_ID.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                ddlEDUCATION_EMPLOYEE_TYPE_ID.Items.Add(new ListItem("--请选择--", "-1"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlEDUCATION_EMPLOYEE_TYPE_ID.Items.Add(new ListItem(dr["EDUCATION_EMPLOYEE_TYPE_NAME"].ToString(), dr["EDUCATION_EMPLOYEE_TYPE_ID"].ToString()));
                }
            }
            //职教委员会职务  
            ora = new OracleAccess();
            ds = ora.RunSqlDataSet(" select COMMITTEE_HEAD_SHIP_ID,COMMITTEE_HEAD_SHIP_NAME from ZJ_COMMITTEE_HEAD_SHIP ");
            ddlCOMMITTEE_HEAD_SHIP_ID.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                ddlCOMMITTEE_HEAD_SHIP_ID.Items.Add(new ListItem("--请选择--", "-1"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlCOMMITTEE_HEAD_SHIP_ID.Items.Add(new ListItem(dr["COMMITTEE_HEAD_SHIP_NAME"].ToString(), dr["COMMITTEE_HEAD_SHIP_ID"].ToString()));
                }
            }
            //技术职称 
            ora = new OracleAccess();
            ds = ora.RunSqlDataSet(" select TECHNICIAN_TITLE_TYPE_ID,TYPE_NAME from TECHNICIAN_TITLE_TYPE order by ORDER_INDEX ");
            ddlTECHNICAL_TITLE_ID.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                ddlTECHNICAL_TITLE_ID.Items.Add(new ListItem("--请选择--", "-1"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlTECHNICAL_TITLE_ID.Items.Add(new ListItem(dr["TYPE_NAME"].ToString(), dr["TECHNICIAN_TITLE_TYPE_ID"].ToString()));
                }
            }
            //技能等级
            ora = new OracleAccess();
            ds = ora.RunSqlDataSet(" select TECHNICIAN_TYPE_ID,TYPE_NAME from TECHNICIAN_TYPE order by TECHNICIAN_TYPE_ID ");
            ddlTECHNICIAN_TYPE_ID.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                ddlTECHNICIAN_TYPE_ID.Items.Add(new ListItem("--请选择--", "-1"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlTECHNICIAN_TYPE_ID.Items.Add(new ListItem(dr["TYPE_NAME"].ToString(), dr["TECHNICIAN_TYPE_ID"].ToString()));
                }
            }
            //学历
            ora = new OracleAccess();
            ds = ora.RunSqlDataSet("select EDUCATION_LEVEL_ID,EDUCATION_LEVEL_NAME from EDUCATION_LEVEL");
            this.DDLeducation_level_id.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                this.DDLeducation_level_id.Items.Add(new ListItem("--请选择--", "-1"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.DDLeducation_level_id.Items.Add(new ListItem(dr["EDUCATION_LEVEL_NAME"].ToString(), dr["EDUCATION_LEVEL_ID"].ToString()));
                }
            }


            ds = ora.RunSqlDataSet("select Safe_Level_ID,Safe_Level_Name from ZJ_Safe_Level order by order_Index");
            this.ddlSafe.Items.Clear();
            if (ds.Tables.Count > 0)
            {
                this.ddlSafe.Items.Add(new ListItem("--请选择--", ""));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.ddlSafe.Items.Add(new ListItem(dr["Safe_Level_Name"].ToString(), dr["Safe_Level_ID"].ToString()));
                }
            }
        }

        private void setReadOnlyControl()
        {
            txtEMPLOYEE_NAME.ReadOnly = isReadOnly;
            txtPOST_NO.ReadOnly = isReadOnly;
            txtNATIVE_PLACE.ReadOnly = isReadOnly;
            txtFOLK.ReadOnly = isReadOnly;
            txtIDENTITY_CARDNO.ReadOnly = isReadOnly;
            txtWORK_PHONE.ReadOnly = isReadOnly;
            txtHOME_PHONE.ReadOnly = isReadOnly;
            txtMOBILE_PHONE.ReadOnly = isReadOnly;
            txtADDRESS.ReadOnly = isReadOnly;
            txtPOST_CODE.ReadOnly = isReadOnly;
            txtMEMO.ReadOnly = isReadOnly;

            dateAWARD_DATE.Enabled = !isReadOnly;//颁发岗位培训合格证日期
            dateJOIN_RAIL_DATE.Enabled = !isReadOnly;//入路日期
            dateBEGIN_DATE.Enabled = !isReadOnly;//入职日期
            rblWEDDING.Enabled = !isReadOnly;//婚否
            cbISONPOST.Enabled = !isReadOnly; //已离职
            ddlSex.Enabled = !isReadOnly;//性别
            ddlPOLITICAL_STATUS.Enabled = !isReadOnly;//政治面貌
            ddlWORKGROUPLEADER_TYPE_ID.Enabled = !isReadOnly;//班组长类型
            ddlEDUCATION_EMPLOYEE_TYPE_ID.Enabled = !isReadOnly;//职教人员类型
            ddlCOMMITTEE_HEAD_SHIP_ID.Enabled = !isReadOnly;//职教委员会职务
            ddlTECHNICAL_TITLE_ID.Enabled = !isReadOnly;//技术职称
            ddlTECHNICIAN_TYPE_ID.Enabled = !isReadOnly;//技能等级
            ddlISREGISTERED.Enabled = !isReadOnly;//在册
            ddlEMPLOYEE_TRANSPORT_TYPE_ID.Enabled = !isReadOnly;//运输业的职工类型
            ddlEMPLOYEE_TYPE_ID.Enabled = !isReadOnly;//职工类型
            ddlIsGroup.Enabled = !isReadOnly;
            btnSave1.Visible = false;
        }

        private void saveNewClear()
        {


        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="employee"></param>
        private void loadData(DataSet ds)
        {
            OracleAccess ora = new OracleAccess();

            hfOrgID.Value = getEntityString(ds.Tables[0].Rows[0]["ORG_ID"]);
            if (!string.IsNullOrEmpty(getEntityString(ds.Tables[0].Rows[0]["ORG_ID"])))
            {
                DataSet dsn = ora.RunSqlDataSet(" select GetOrgName(org_ID) from ORG where ORG_ID =" + ds.Tables[0].Rows[0]["ORG_ID"]);
                if (dsn.Tables.Count > 0)
                {
                    if (dsn.Tables[0].Rows.Count > 0)
                    {
                        txtORG.Text = dsn.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            txtEMPLOYEE_NAME.Text = getEntityString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
            txtPOST_NO.Text = getEntityString(ds.Tables[0].Rows[0]["POST_NO"]);
            hfPostID.Value = getEntityString(ds.Tables[0].Rows[0]["POST_ID"]);
            if (!string.IsNullOrEmpty(getEntityString(ds.Tables[0].Rows[0]["POST_ID"])))
            {
                ora = new OracleAccess();
                DataSet dsn = ora.RunSqlDataSet("select POST_Name from post where post_ID=" + ds.Tables[0].Rows[0]["POST_ID"]);
                if (dsn.Tables.Count > 0)
                {
                    if (dsn.Tables[0].Rows.Count > 0)
                    {
                        txtPOST.Text = dsn.Tables[0].Rows[0][0].ToString();
                    }
                }
            }

            if (ds.Tables[0].Rows[0]["Now_Post_ID"] !=DBNull.Value)
            {
                hfNowPostID.Value = ds.Tables[0].Rows[0]["Now_Post_ID"].ToString();
                ora = new OracleAccess();
                DataSet dsn = ora.RunSqlDataSet("select POST_Name from post where post_ID=" + ds.Tables[0].Rows[0]["Now_Post_ID"]);
                if (dsn.Tables.Count > 0)
                {
                    if (dsn.Tables[0].Rows.Count > 0)
                    {
                        txtNowPost.Text = dsn.Tables[0].Rows[0][0].ToString();
                    }
                }
            }

            ddlSex.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["SEX"]);
            //this.dateBIRTHDAY.DateValue = DateTime.Parse(getEntityString(ds.Tables[0].Rows[0]["BIRTHDAY"]));
            txtNATIVE_PLACE.Text = getEntityString(ds.Tables[0].Rows[0]["NATIVE_PLACE"]);
            txtFOLK.Text = getEntityString(ds.Tables[0].Rows[0]["FOLK"]);
            this.rblWEDDING.SelectedValue = ds.Tables[0].Rows[0]["WEDDING"] == null ? "0" : ds.Tables[0].Rows[0]["WEDDING"].ToString();
            //this.dateBEGIN_DATE.DateValue = DateTime.Parse(getEntityString(ds.Tables[0].Rows[0]["BEGIN_DATE"]));
            cbISONPOST.Checked = getEntityString(ds.Tables[0].Rows[0]["ISONPOST"]) == "0" ? false : true;
            txtMEMO.Text = getEntityString(ds.Tables[0].Rows[0]["MEMO"]);
            this.ddlIsGroup.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["IS_GROUP_LEADER"]);
            this.ddlTECHNICIAN_TYPE_ID.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["TECHNICIAN_TYPE_ID"]);
            this.ddlTECHNICAL_TITLE_ID.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["TECHNICAL_TITLE_ID"]);
            this.txtWORK_NO.Text = getEntityString(ds.Tables[0].Rows[0]["WORK_NO"]);
            txtIDENTITY_CARDNO.Text = getEntityString(ds.Tables[0].Rows[0]["IDENTITY_CARDNO"]);
            this.ddlPOLITICAL_STATUS.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["POLITICAL_STATUS_ID"]);
            //this.dateJOIN_RAIL_DATE.DateValue = DateTime.Parse(getEntityString(ds.Tables[0].Rows[0]["JOIN_RAIL_DATE"]));
            //学历
            this.DDLeducation_level_id.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["EDUCATION_LEVEL_ID"]);
            this.ddlEMPLOYEE_TYPE_ID.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["EMPLOYEE_TYPE_ID"]);
            this.ddlWORKGROUPLEADER_TYPE_ID.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["WORKGROUPLEADER_TYPE_ID"]);
            //this.dateWORKGROUPLEADER_ORDER_DATE.DateValue = DateTime.Parse(getEntityString(ds.Tables[0].Rows[0]["WORKGROUPLEADER_ORDER_DATE"]));
            this.ddlEDUCATION_EMPLOYEE_TYPE_ID.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["EDUCATION_EMPLOYEE_TYPE_ID"]);
            this.ddlCOMMITTEE_HEAD_SHIP_ID.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["COMMITTEE_HEAD_SHIP_ID"]);
            this.ddlISREGISTERED.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["ISREGISTERED"]);
            this.ddlEMPLOYEE_TRANSPORT_TYPE_ID.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["EMPLOYEE_TRANSPORT_TYPE_ID"]);
            //this.dateAWARD_DATE.DateValue = DateTime.Parse(getEntityString(ds.Tables[0].Rows[0]["AWARD_DATE"]));
            this.hfCOULD_POST_ID.Value = getEntityString(ds.Tables[0].Rows[0]["COULD_POST_ID"]);
            //DateTime datetime1, datetime2, datetime3, datetime4;
            //if (DateTime.TryParse(getEntityString(ds.Tables[0].Rows[0]["GRADUATE_DATE"]), out datetime1))
            //    this.dateGraduate.DateValue = datetime1;
            //if (DateTime.TryParse(getEntityString(ds.Tables[0].Rows[0]["POST_DATE"]), out datetime2))
            //    this.datePost.DateValue = datetime2;
            //if (DateTime.TryParse(getEntityString(ds.Tables[0].Rows[0]["TECHNICAL_DATE"]), out datetime3))
            //    this.dateTechnicalDate.DateValue = datetime3;
            //if (DateTime.TryParse(getEntityString(ds.Tables[0].Rows[0]["TECHNICAL_TITLE_DATE"]), out datetime4))
            //    this.dateTechnicalTitle.DateValue = datetime4;

            txtPinYin.Text = getEntityString(ds.Tables[0].Rows[0]["PinYin_Code"]);

            if (ds.Tables[0].Rows[0]["WORKGROUPLEADER_ORDER_DATE"] != DBNull.Value)
            {
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["WORKGROUPLEADER_ORDER_DATE"]).ToString("yyyy-MM-dd") != "0001-01-01")
                {
                    dateWORKGROUPLEADER_ORDER_DATE.DateValue =
                        Convert.ToDateTime(ds.Tables[0].Rows[0]["WORKGROUPLEADER_ORDER_DATE"]).ToString("yyyy-MM-dd");
                }
            }
            
            if(ds.Tables[0].Rows[0]["Graduate_Date"] != DBNull.Value)
            {
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["Graduate_Date"]).ToString("yyyy-MM-dd") != "0001-01-01")
                {
                    dateGraduate.DateValue =
                        Convert.ToDateTime(ds.Tables[0].Rows[0]["Graduate_Date"]).ToString("yyyy-MM-dd");
                }
            }
            if (ds.Tables[0].Rows[0]["POST_DATE"] != DBNull.Value)
            {
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["POST_DATE"]).ToString("yyyy-MM-dd") != "0001-01-01")
                {
                    datePost.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["POST_DATE"]).ToString("yyyy-MM-dd");
                }
            }
            if (ds.Tables[0].Rows[0]["TECHNICAL_DATE"] != DBNull.Value)
            {
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["TECHNICAL_DATE"]).ToString("yyyy-MM-dd") != "0001-01-01")
                {
                    dateTechnicalDate.DateValue =
                        Convert.ToDateTime(ds.Tables[0].Rows[0]["TECHNICAL_DATE"]).ToString("yyyy-MM-dd");
                }
            }
            if (ds.Tables[0].Rows[0]["TECHNICAL_TITLE_DATE"] != DBNull.Value)
            {
                if (Convert.ToDateTime(ds.Tables[0].Rows[0]["TECHNICAL_TITLE_DATE"]).ToString("yyyy-MM-dd") != "0001-01-01")
                {
                    dateTechnicalTitle.DateValue =
                        Convert.ToDateTime(ds.Tables[0].Rows[0]["TECHNICAL_TITLE_DATE"]).ToString("yyyy-MM-dd");
                }
            }

            if (ds.Tables[0].Rows[0]["Safe_Level_ID"] != DBNull.Value)
            {
                ddlSafe.SelectedValue = ds.Tables[0].Rows[0]["Safe_Level_ID"].ToString();
            }
            else
            {
                ddlSafe.SelectedValue = "";
            }

            dateBIRTHDAY.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["Birthday"]).ToString("yyyy-MM-dd");
            dateBEGIN_DATE.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["Begin_Date"]).ToString("yyyy-MM-dd");
            dateJOIN_RAIL_DATE.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["Join_Rail_Date"]).ToString("yyyy-MM-dd");
            if (ds.Tables[0].Rows[0]["Award_Date"] != DBNull.Value)
            {
                if( Convert.ToDateTime(ds.Tables[0].Rows[0]["Award_Date"]).ToString("yyyy-MM-dd")!= "0001-01-01")
                {
                    dateAWARD_DATE.DateValue = Convert.ToDateTime(ds.Tables[0].Rows[0]["Award_Date"]).ToString("yyyy-MM-dd");
                }
            }

            this.txtFinishSchool.Text = getEntityString(ds.Tables[0].Rows[0]["GRADUATE_UNIVERSITY"]);
            this.txtMajor.Text = getEntityString(ds.Tables[0].Rows[0]["STUDY_MAJOR"]);
            this.dropSchoolCategory.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["UNIVERSITY_TYPE"]);

            this.txtTechnicalCode.Text = getEntityString(ds.Tables[0].Rows[0]["TECHNICAL_CODE"]);

            txtWORK_PHONE.Text = getEntityString(ds.Tables[0].Rows[0]["WORK_PHONE"]);
            txtHOME_PHONE.Text = getEntityString(ds.Tables[0].Rows[0]["HOME_PHONE"]);
            txtMOBILE_PHONE.Text = getEntityString(ds.Tables[0].Rows[0]["MOBILE_PHONE"]);
            txtADDRESS.Text = getEntityString(ds.Tables[0].Rows[0]["ADDRESS"]);
            txtPOST_CODE.Text = getEntityString(ds.Tables[0].Rows[0]["POST_CODE"]);

            //ddlIsGroup.SelectedValue = getEntityString(ds.Tables[0].Rows[0]["IS_GROUP_LEADER"]);
            initJianZhiCMB(ds, getEntityString(ds.Tables[0].Rows[0]["COULD_POST_ID"]));

            this.myImagePhoto.ImageUrl = "ShowImage.aspx?EmployeeID=" + ds.Tables[0].Rows[0]["Employee_ID"].ToString();
        }

        private void initJianZhiCMB(DataSet ds, string could_Post_ID_str)
        {
            string[] postIDs = could_Post_ID_str.Split(',');
            string postNames = string.Empty;
            string domStr = "var jzHTMLText=\"兼职1 : <select id='cmbJianZhi1' onchange='changeJianZhi1();'" + (isReadOnly ? " disabled='disabled' " : "") + "><option value='-1' >--请选择--</option>";
            Hashtable hs = new Hashtable();
            for (int i = 0; i < postIDs.Length; i++)
            {
                if (string.IsNullOrEmpty(postIDs[i]))
                {
                    continue;
                }
                RailExam.BLL.PostBLL PostBLL = new RailExam.BLL.PostBLL();
                int postID = 0;
                int.TryParse(postIDs[i], out postID);

                RailExam.Model.Post post = PostBLL.GetPost(postID);
                if (post == null)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(postNames))
                {
                    postNames += post.PostName;
                }
                else
                {
                    postNames += ',' + post.PostName;
                }
                domStr += "<option value='" + postIDs[i] + "' ";
                if (postIDs[i] == getEntityString(ds.Tables[0].Rows[0]["SECOND_POST_ID"]))
                {
                    domStr += " selected='selected'";
                }
                if (isReadOnly)
                {
                    domStr += " disabled='disabled' ";
                }

                domStr += ">" + post.PostName + "</option>";

                hs.Add(postIDs[i], post.PostName);
            }
            domStr += "</select>&nbsp;&nbsp;&nbsp;&nbsp;兼职2 : <select id='cmbJianZhi2' onchange='changeJianZhi2();'" + (isReadOnly ? " disabled='disabled' " : "") + "><option value='-1' >--请选择--</option>";
            for (int i = 0; i < postIDs.Length; i++)
            {
                if (string.IsNullOrEmpty(postIDs[i]))
                {
                    continue;
                }

                domStr += "<option value='" + postIDs[i] + "' ";
                if (postIDs[i] == getEntityString(ds.Tables[0].Rows[0]["THIRD_POST_ID"]))
                {
                    domStr += " selected='selected'";
                }
                domStr += ">" + hs[postIDs[i]] + "</option>";
            }
            domStr += "\";document.getElementById('divJianZhi1').innerHTML=jzHTMLText;";
            this.ClientScript.RegisterStartupScript(this.GetType(), " ", "<script language='javascript'>" + domStr + "</script>");
            txtCOULD_POST_Name.Text = postNames;
        }

        private string getEntityString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (isHaveNull())
            {
                return;
            }
            saveData();
        }

        /// <summary>
        /// 判断是否有空
        /// </summary>
        /// <returns></returns>
        private bool isHaveNull()
        {
         
            string id = Request.QueryString.Get("ID");
            if (string.IsNullOrEmpty(this.hfOrgID.Value))
            {
                //OxMessageBox.alert(this, "请选择组织机构！");
                this.ClientScript.RegisterStartupScript(this.GetType(), " ", "<script language='javascript'>alert('" + "请选择组织机构！" + "');</script>");
                return true;
            }
            if (string.IsNullOrEmpty(this.hfPostID.Value))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), " ", "<script language='javascript'>alert('" + "请选择工作岗位！" + "');</script>");
                return true;
            }
            if (string.IsNullOrEmpty(this.txtEMPLOYEE_NAME.Text.Trim().ToString()))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), " ", "<script language='javascript'>alert('" + "请填写员工姓名！" + "');</script>");
                return true;
            }

            if(string.IsNullOrEmpty(txtIDENTITY_CARDNO.Text.Trim()) && string.IsNullOrEmpty(txtWORK_NO.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(GetType(), "OK", "alert('身份证号码和岗位培训合格证书编号不能同时为空！');", true);
                return true;
            }

            if (ddlEMPLOYEE_TYPE_ID.SelectedValue == "-1")
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), " ", "<script language='javascript'>alert('" + "请选择职工类型！" + "');</script>");
                return true;
            }

            return false;
        }

        //保存数据
        private void saveData()
        {
            string type = Request.QueryString["Type"];//1为修改 2为新增
            string id = Request.QueryString["ID"];

            OracleAccess db = new OracleAccess();
            string strSql = "";
            if (!string.IsNullOrEmpty(txtIDENTITY_CARDNO.Text.Trim()))
            {
                if (type == "1")
                {
                    strSql = "select a.*,GetOrgName(GetStationOrgID(a.Org_ID)) OrgName　from Employee a where Identity_CardNo='" + txtIDENTITY_CARDNO.Text.Trim() +
                             "' and Employee_ID<>" + id;
                }
                else
                {
                    strSql = "select a.*,GetOrgName(GetStationOrgID(a.Org_ID)) OrgName　from Employee a where Identity_CardNo='" + txtIDENTITY_CARDNO.Text.Trim() + "'";
                }

                DataSet ds = db.RunSqlDataSet(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strName = ds.Tables[0].Rows[0]["Employee_Name"].ToString();
                    string orgName = ds.Tables[0].Rows[0]["OrgName"].ToString();
                    SessionSet.PageMessage = "该身份证号码在系统中已存在，与【" + orgName + "】的【" + strName + "】身份证号相同！";
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtWORK_NO.Text.Trim()))
            {
                if (type == "1")
                {
                    strSql = "select a.*,GetOrgName(GetStationOrgID(a.Org_ID)) OrgName　from Employee a where Work_No='" + txtWORK_NO.Text.Trim() +
                             "' and Employee_ID<>" + id;
                }
                else
                {
                    strSql = "select a.*,GetOrgName(GetStationOrgID(a.Org_ID)) OrgName　from Employee a where Work_No='" + txtWORK_NO.Text.Trim() + "'";
                }

                DataSet ds = db.RunSqlDataSet(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strName = ds.Tables[0].Rows[0]["Employee_Name"].ToString();
                    string orgName = ds.Tables[0].Rows[0]["OrgName"].ToString();
                    SessionSet.PageMessage = "该岗位培训合格证书编号在系统中已存在，与【" + orgName + "】的【" + strName + "】岗位培训合格证书编号相同！";
                    //ClientScript.RegisterStartupScript(GetType(), "OK", "alert('该岗位培训合格证书编号在系统中已存在，\r\n与【" + orgName + "】的【" + strName + "】岗位培训合格证书编号相同！');", true);
                    return;
                }
            }

            string sqlStr = string.Empty;
            DateTime isnulldate = new DateTime(0001, 01, 01);
            DateTime birthday,
                begin_date,
                join_rail_date,
                workgroupleader_order_date,
                award_date;
            DateTime postDate,  //任现职名时间
                     technicalTitleDate,    //技术职称聘任时间
                     technicalDate, //技能等级取得时间
                     graduateDate;  //毕业时间
            string graduateUniversity,  //毕业院校
                   studyMajor,  //所学专业
                   universityType;  //学校类别
            if (!DateTime.TryParse(this.dateBIRTHDAY.DateValue.ToString(), out birthday))
            {
                birthday = isnulldate;
            }
            if (!DateTime.TryParse(this.dateBEGIN_DATE.DateValue.ToString(), out begin_date))
            {
                begin_date = isnulldate;
            }
            if (!DateTime.TryParse(this.dateJOIN_RAIL_DATE.DateValue.ToString(), out join_rail_date))
            {
                join_rail_date = isnulldate;
            }

            if (ddlWORKGROUPLEADER_TYPE_ID.SelectedValue != "-1")
            {
                if (!DateTime.TryParse(this.dateWORKGROUPLEADER_ORDER_DATE.DateValue.ToString(), out workgroupleader_order_date))
                {
                    workgroupleader_order_date = isnulldate;
                }
            }
            else
            {
                workgroupleader_order_date = isnulldate;
            }

            if (!DateTime.TryParse(this.dateAWARD_DATE.DateValue.ToString(), out award_date))
            {
                award_date = isnulldate;
            }
            if (!DateTime.TryParse(this.datePost.DateValue.ToString(), out postDate))
            {
                postDate = isnulldate;
            }
            if (!DateTime.TryParse(this.dateTechnicalDate.DateValue.ToString(), out technicalDate))
            {
                technicalDate = isnulldate;
            }
            if (!DateTime.TryParse(this.dateTechnicalTitle.DateValue.ToString(), out technicalTitleDate))
            {
                technicalTitleDate = isnulldate;
            }
            if (!DateTime.TryParse(this.dateGraduate.DateValue.ToString(), out graduateDate))
            {
                graduateDate = isnulldate;
            }
            if (this.txtMEMO.Text.ToString().Length > 50)
            {
                ClientScript.RegisterStartupScript(GetType(), "OK", "alert('备注不能超过50个字符！');", true);
                return;
            }

            OracleAccess ora = new OracleAccess();

            int ISONPOST = this.cbISONPOST.Checked == true ? 1 : 0;
            if (!string.IsNullOrEmpty(type))
            {
                int typeBool = int.Parse(type);
                if (typeBool == 1)
                {
                    sqlStr = "update employee set org_id= '" + Convert.ToInt32(hfOrgID.Value) + "',";
                    sqlStr += "post_no= '" + this.txtPOST_NO.Text.Trim() + "',";
                    sqlStr += "employee_name= '" + this.txtEMPLOYEE_NAME.Text.Trim() + "',";
                    sqlStr += "pinyin_code= '" + this.txtPinYin.Text.Trim() + "',";
                    sqlStr += "post_id= '" + Convert.ToInt32(this.hfPostID.Value) + "',";
                    sqlStr += "now_post_id= " + (hfNowPostID.Value == "" ? "null" : "'" + hfNowPostID.Value + "'") + ",";
                    sqlStr += "sex= '" + this.ddlSex.SelectedValue + "',";
                    sqlStr += "birthday=to_date('" + birthday + "','yyyy-mm-dd hh24:mi:ss'),";
                    sqlStr += "native_place= '" + this.txtNATIVE_PLACE.Text.Trim() + "',";
                    sqlStr += "folk= '" + this.txtFOLK.Text.Trim() + "',";
                    sqlStr += "wedding= '" + Convert.ToInt32(this.rblWEDDING.SelectedValue) + "',";
                    sqlStr += "begin_date= to_date('" + begin_date + "','yyyy-mm-dd hh24:mi:ss'),";
                    sqlStr += "IsOnPost= '" + ISONPOST + "',";
                    sqlStr += "memo= '" + this.txtMEMO.Text.Trim() + "',";
                    sqlStr += "is_group_leader= '" + Convert.ToInt32(this.ddlIsGroup.SelectedValue) + "',";
                    sqlStr += "technician_type_id = '" + int.Parse(this.ddlTECHNICIAN_TYPE_ID.SelectedValue) + "',";
                    sqlStr += "technical_title_id='" + int.Parse(this.ddlTECHNICAL_TITLE_ID.SelectedValue) + "',";
                    sqlStr += "work_no='" + this.txtWORK_NO.Text.Trim() + "',";
                    sqlStr += "identity_cardno='" + this.txtIDENTITY_CARDNO.Text.Trim() + "',";
                    sqlStr += "political_status_id='" + int.Parse(this.ddlPOLITICAL_STATUS.SelectedValue) + "',";
                    sqlStr += "join_rail_date=to_date('" + join_rail_date + "','yyyy-mm-dd hh24:mi:ss'),";
                    sqlStr += "education_level_id='" + int.Parse(this.DDLeducation_level_id.SelectedValue) + "',";
                    sqlStr += "employee_type_id='" + int.Parse(this.ddlEMPLOYEE_TYPE_ID.SelectedValue) + "',";
                    sqlStr += "second_post_id=" + (this.hfSECOND_POST_ID.Value == "" ? "NULL" : hfSECOND_POST_ID.Value) +
                              ",";
                    sqlStr += "third_post_id=" + (this.hfTHIRD_POST_ID.Value == "" ? "NULL" : hfTHIRD_POST_ID.Value) +
                              ",";

                    sqlStr += "workgroupleader_type_id='" + int.Parse(this.ddlWORKGROUPLEADER_TYPE_ID.SelectedValue) +
                              "'," +
                              "workgroupleader_order_date=to_date('" + workgroupleader_order_date +
                              "','yyyy-mm-dd hh24:mi:ss')," +
                              "education_employee_type_id='" +
                              int.Parse(this.ddlEDUCATION_EMPLOYEE_TYPE_ID.SelectedValue) + "'," +
                              "committee_head_ship_id='" + int.Parse(this.ddlCOMMITTEE_HEAD_SHIP_ID.SelectedValue) +
                              "'," +
                              "isregistered='" + int.Parse(this.ddlISREGISTERED.SelectedValue) + "'," +
                              "employee_transport_type_id='" +
                              int.Parse(this.ddlEMPLOYEE_TRANSPORT_TYPE_ID.SelectedValue) + "'," +
                              "award_date=to_date('" + award_date + "','yyyy-mm-dd hh24:mi:ss')," +
                              "could_post_id='" + this.hfCOULD_POST_ID.Value.Trim().ToString() + "'," +
                              "TECHNICAL_DATE = to_date('" + technicalDate + "','yyyy-mm-dd hh24:mi:ss')," +
                              "TECHNICAL_TITLE_DATE = to_date('" + technicalTitleDate + "','yyyy-mm-dd hh24:mi:ss')," +
                              "POST_DATE = to_date('" + postDate + "','yyyy-mm-dd hh24:mi:ss')," +
                              "GRADUATE_DATE = to_date('" + graduateDate + "','yyyy-mm-dd hh24:mi:ss')," +
                              "GRADUATE_UNIVERSITY = '" + this.txtFinishSchool.Text + "'," +
                              "STUDY_MAJOR = '" + this.txtMajor.Text + "'," +
                              "UNIVERSITY_TYPE = '" + this.dropSchoolCategory.SelectedValue + "'," +
                              "TECHNICAL_CODE = '" + this.txtTechnicalCode.Text + "'," +
                              "Safe_Level_ID=" + (ddlSafe.SelectedValue == "" ? "NULL" : ddlSafe.SelectedValue) +
                              " where employee_id = " + id;
                }
                else if (typeBool == 2)
                {
                    sqlStr = "select EMPLOYEE_SEQ.NEXTVAL from dual";
                    id = ora.RunSqlDataSet(sqlStr).Tables[0].Rows[0][0].ToString();

                    sqlStr = "insert into employee (employee_id," +
                             "org_id," +
                             "post_no," +
                             "employee_name," +
                             "pinyin_code,"+                             
                             "post_id," +
                             "sex," +
                             "birthday," +
                             "native_place," +
                             "folk," +
                             "wedding," +
                             "begin_date," +
                             "IsOnPost," +
                             "memo," +
                             "is_group_leader," +
                             "technician_type_id," +
                             "technical_title_id," +
                             "work_no," +
                             //"photo," +
                             "identity_cardno," +
                             "political_status_id," +
                             "join_rail_date," +
                             "education_level_id," +
                             "employee_type_id," +
                             "second_post_id," +
                             "third_post_id," +
                             "workgroupleader_type_id," +
                             "workgroupleader_order_date," +
                             "education_employee_type_id," +
                             "committee_head_ship_id," +
                             "isregistered," +
                             "employee_transport_type_id," +
                             "award_date," +
                             "could_post_id," +
                             "TECHNICAL_DATE," +
                             "TECHNICAL_TITLE_DATE," +
                             "POST_DATE," +
                             "GRADUATE_UNIVERSITY," +
                             "GRADUATE_DATE," +
                             "STUDY_MAJOR," +
                             "UNIVERSITY_TYPE," +
                             "TECHNICAL_CODE,Safe_Level_ID,Now_Post_ID)" +
                             " values(" + id + "," +
                             Convert.ToInt32(this.hfOrgID.Value) + ",'" +
                             this.txtPOST_NO.Text.Trim().ToString() + "','" +
                             this.txtEMPLOYEE_NAME.Text.Trim().ToString() + "','" +
                             Pub.GetChineseSpell(this.txtEMPLOYEE_NAME.Text.Trim().ToString()) + "'," +
                             Convert.ToInt32(this.hfPostID.Value) + ",'" +
                             this.ddlSex.SelectedValue + "'," +
                             "to_date('" + birthday + "','yyyy-mm-dd hh24:mi:ss'),'" +
                             this.txtNATIVE_PLACE.Text.Trim().ToString() + "','" +
                             this.txtFOLK.Text.Trim().ToString() + "'," +
                             Convert.ToInt32(this.rblWEDDING.SelectedValue) + "," +
                             "to_date('" + begin_date + "','yyyy-mm-dd hh24:mi:ss')," +
                             ISONPOST + ",'" +
                             this.txtMEMO.Text.Trim().ToString() + "'," +
                             Convert.ToInt32(this.ddlIsGroup.SelectedValue) + "," +
                             int.Parse(this.ddlTECHNICIAN_TYPE_ID.SelectedValue) + "," +
                             int.Parse(this.ddlTECHNICAL_TITLE_ID.SelectedValue) + ",'" +
                             this.txtWORK_NO.Text.Trim().ToString() + "','" +
                             this.txtIDENTITY_CARDNO.Text.Trim().ToString() + "'," +
                             int.Parse(this.ddlPOLITICAL_STATUS.SelectedValue) + "," +
                             "to_date('" + join_rail_date + "','yyyy-mm-dd hh24:mi:ss')," +
                             int.Parse(this.DDLeducation_level_id.SelectedValue) + "," +
                             int.Parse(this.ddlEMPLOYEE_TYPE_ID.SelectedValue) + "," +
                             (this.hfSECOND_POST_ID.Value == "" ? "NULL" : hfSECOND_POST_ID.Value) + "," +
                             (this.hfTHIRD_POST_ID.Value == "" ? "NULL" : hfSECOND_POST_ID.Value) + "," +
                             int.Parse(this.ddlWORKGROUPLEADER_TYPE_ID.SelectedValue) + "," +
                             "to_date('" + workgroupleader_order_date + "','yyyy-mm-dd hh24:mi:ss')," +
                             int.Parse(this.ddlEDUCATION_EMPLOYEE_TYPE_ID.SelectedValue) + "," +
                             int.Parse(this.ddlCOMMITTEE_HEAD_SHIP_ID.SelectedValue) + "," +
                             int.Parse(this.ddlISREGISTERED.SelectedValue) + "," +
                             int.Parse(this.ddlEMPLOYEE_TRANSPORT_TYPE_ID.SelectedValue) + "," +
                             "to_date('" + award_date + "','yyyy-mm-dd hh24:mi:ss'),'" +
                             this.hfCOULD_POST_ID.Value.Trim().ToString() + "'," +
                             "to_date('" + technicalDate + "','yyyy-mm-dd hh24:mi:ss')," +
                             "to_date('" + technicalTitleDate + "','yyyy-mm-dd hh24:mi:ss')," +
                             "to_date('" + postDate + "','yyyy-mm-dd hh24:mi:ss')," +
                             "'" + this.txtFinishSchool.Text + "'," +
                             "to_date('" + graduateDate + "','yyyy-mm-dd hh24:mi:ss')," +
                             "'" + this.txtMajor.Text + "'," +
                             "'" + this.dropSchoolCategory.SelectedValue + "'," +
                             "'" + this.txtTechnicalCode.Text + "'," +
                             (ddlSafe.SelectedValue == "" ? "NULL" : ddlSafe.SelectedValue) +","+
                              (hfNowPostID.Value == "" ? "null" : "'" + hfNowPostID.Value + "'")+")";
                }

                try
                {
                    ora.ExecuteNonQuery(sqlStr);

                    if (typeBool == 2)
                    {
                        sqlStr = "insert into Employee_Photo values(" + id + ",null)";
                        ora.ExecuteNonQuery(sqlStr);
                    }

                    SystemUserBLL objSystemBll = new SystemUserBLL();
                    if (type == "1")
                    {
                        RailExam.Model.SystemUser objSystem = objSystemBll.GetUserByEmployeeID(Convert.ToInt32(id));
                        if (objSystem != null)
                        {
                            objSystem.UserID = txtWORK_NO.Text.Trim() == string.Empty
                                                   ? txtIDENTITY_CARDNO.Text.Trim()
                                                   : txtWORK_NO.Text.Trim();
                            objSystemBll.UpdateUser(objSystem);
                        }
                        else
                        {
                            objSystem = new SystemUser();
                            objSystem.EmployeeID = Convert.ToInt32(id);
                            objSystem.Memo = "";
                            objSystem.Password = "111111";
                            objSystem.RoleID = 0;
                            objSystem.UserID = txtWORK_NO.Text.Trim() == string.Empty
                                                   ? txtIDENTITY_CARDNO.Text.Trim()
                                                   : txtWORK_NO.Text.Trim();
                            objSystemBll.AddUser(objSystem);
                        }
                    }
                    else
                    {
                        RailExam.Model.SystemUser objSystem = new SystemUser();
                        objSystem.EmployeeID = Convert.ToInt32(id);
                        objSystem.Memo = "";
                        objSystem.Password = "111111";
                        objSystem.RoleID = 0;
                        objSystem.UserID = txtWORK_NO.Text.Trim() == string.Empty
                                               ? txtIDENTITY_CARDNO.Text.Trim()
                                               : txtWORK_NO.Text.Trim();
                        objSystemBll.AddUser(objSystem);
                    }

                    if (!string.IsNullOrEmpty(fileUpload1.PostedFile.FileName))
                    {
                        string str = Server.MapPath("/RailExamBao/Excel/image");
                        if (!Directory.Exists(str))
                        {
                            Directory.CreateDirectory(str);
                        }

                        string strFileName = Path.GetFileName(fileUpload1.PostedFile.FileName);
                        string strPath = Server.MapPath("/RailExamBao/Excel/image/" + strFileName);

                        if (File.Exists(strPath))
                            File.Delete(strPath);

                        FileInfo fi = new FileInfo(fileUpload1.PostedFile.FileName);
                        string extensions = ".jpg,.jpeg,.gif";
                        if (!extensions.Contains(fi.Extension.ToLower()))
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "OK",
                                                                    "alert('只能上传GIF, JPEG(JPG)格式的图片！');", true);
                            return;
                        }

                        ((HttpPostedFile) fileUpload1.PostedFile).SaveAs(strPath);

                        this.myImagePhoto.ImageUrl = strPath;
                        AddImage(Int32.Parse(id), strPath);
                    }
					SystemLogBLL objLogBll = new SystemLogBLL();
					objLogBll.WriteLog("保存职工:"+txtEMPLOYEE_NAME.Text+"的档案信息！");
                }
                catch (Exception ex)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('" + ex.Message + "');", true);
                    return;
                }
                string OrgID = this.hfOrgID.Value;
                OrganizationBLL objBll = new OrganizationBLL();
                string idpath = Request.QueryString.Get("idpath");

                if (string.IsNullOrEmpty(Request.QueryString.Get("style")))
                {
                    string strQuery = Request.QueryString.Get("strQuery");
                    this.ClientScript.RegisterStartupScript(this.GetType(), "OK",
                                                            "this.location.href='EmployeeInfo.aspx?ID=" +
                                                            OrgID + "&idpath=" + idpath + "&type=Org&strQuery="+strQuery+"';", true);
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "top.returnValue='true';top.close();",
                                                            true);
                }
            }
        }

        protected void btnSaveAdd_Click(object sender, EventArgs e)
        {
            if (isHaveNull())
            {
                return;
            }
            saveData();
            saveNewClear();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            string OrgID = this.hfOrgID.Value;
            OrganizationBLL objBll = new OrganizationBLL();
            string idpath = objBll.GetOrganization(Convert.ToInt32(OrgID)).IdPath;

            if (string.IsNullOrEmpty(Request.QueryString.Get("style")))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "this.location.href='EmployeeInfo.aspx?ID=" + OrgID + "&idpath=" + idpath + "&type=Org';", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "top.returnValue='true';top.close();", true);
            }
        }

        private void AddImage(int employeeID, string fileName)
        {
            string OrgID = this.hfOrgID.Value;

            //FileStream fs = new FileStream(fileName, FileMode.Open);
            //int len = int.Parse(fs.Length.ToString());
            //byte[] byteImage = new Byte[len];
            //fs.Read(byteImage, 0, len);
            //fs.Close();

            System.Drawing.Image image = System.Drawing.Image.FromFile(fileName);
            System.Drawing.Image thumbnail = image.GetThumbnailImage(120, 150, null, IntPtr.Zero);
            MemoryStream ms = new MemoryStream();
            thumbnail.Save(ms,ImageFormat.Jpeg );
            byte[] byteImage = ms.ToArray();

            //添加
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string value = node.Value;

            if (value == "Oracle")
            {
                System.Data.OracleClient.OracleParameter para1 = new System.Data.OracleClient.OracleParameter("p_photo", OracleType.Blob);
                System.Data.OracleClient.OracleParameter para2 = new System.Data.OracleClient.OracleParameter("p_id", OracleType.Number);
                para1.Value = byteImage;
                para2.Value = employeeID;

                IDataParameter[] paras = new IDataParameter[] { para1, para2 };

                Pub.RunAddProcedureBlob(false, "USP_EMPLOYEE_IMAGE", paras, byteImage);
            }
        }

        protected  void ddlISREGISTERED_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(ddlISREGISTERED.SelectedValue == "0")
            //{
            //    cbISONPOST.Checked = false;
            //    cbISONPOST.Enabled = false;
            //}
            //else
            //{
            //    cbISONPOST.Enabled = true;
            //}
        }
    }
}