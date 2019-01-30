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
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!(PrjPub.HasEditRight("微机教室信息") && PrjPub.IsServerCenter))
                {
                    btnSave.Enabled = false;
                }
          
                ViewState["ComputerDetailType"] = Request.QueryString.Get("Type"); //0--预览,1--编辑，2--新增
                ViewState["ComputerDetailID"] = Request.QueryString.Get("ID");

                //if (!string.IsNullOrEmpty(Request.QueryString.Get("OrgID")))
                //{
                //    ddlSelectOrg.SelectedValue = Request.QueryString.Get("OrgID");
                //}

                OrganizationBLL orgBll = new OrganizationBLL();
                IList<Organization> orgList = orgBll.GetOrganizationsByLevel(2);
                foreach (Organization organization in orgList)
                {
                    if (organization.LevelNum == 2)
                    {
                        ListItem item = new ListItem();
                        item.Text = organization.ShortName;
                        item.Value = organization.OrganizationId.ToString();
                        ddlSelectOrg.Items.Add(item);
                    }
                }
                ddlSelectOrg.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                

                int typeBool = 0;
                if (!string.IsNullOrEmpty(ViewState["ComputerDetailType"].ToString()))
                {
                    typeBool = int.Parse(ViewState["ComputerDetailType"].ToString());
                    setReadOnlyControl(typeBool == 0 ? true : false);
                }

                //新增
                if (typeBool == 2)
                {
                    string orgID = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                    dropServerBind(orgID);
                    DataSet orgDs;
                    OracleAccess ora = new OracleAccess();
                    orgDs = ora.RunSqlDataSet("select SHORT_NAME from org where LEVEL_NUM =2 and org_ID = " + orgID);
                    if (orgDs.Tables.Count > 0)
                    {
                        if (orgDs.Tables[0].Rows.Count > 0)
                        {
                            txtORG.Text = getEntityString(orgDs.Tables[0].Rows[0][0]);
                            hfOrgID.Value = orgID;
                            hfOrgName.Value = orgDs.Tables[0].Rows[0]["Short_Name"].ToString();
                        }
                    }

                    ddlIS_EFFECT.SelectedValue = "1";

                }

                string strSql = "";
                if (ViewState["ComputerDetailID"] != null)
                {
                    if (!string.IsNullOrEmpty(ViewState["ComputerDetailID"].ToString()))
                    {
                        DataSet orgDs;
                        OracleAccess ora = new OracleAccess();
                        strSql = "select b.SHORT_NAME,a.ORG_ID, a.COMPUTER_ROOM_NAME,a.ADDRESS,a.COMPUTER_NUMBER,"
                                 + "a.BAD_SEAT,a.IS_EFFECT,a.COMPUTER_SERVER_ID,a.CONTRACT_PERSON,a.CONTRACT_PHONE "
                                 + "  from Computer_room  a "
                                 + " inner join org b on a.org_ID = b.org_id "
                                 + " where computer_room_ID =" + ViewState["ComputerDetailID"];

                        orgDs = ora.RunSqlDataSet(strSql);
                        if (orgDs.Tables.Count > 0)
                        {
                            loadData(orgDs.Tables[0]);
                        }
                    }
                }                
            }
            else
            {
                txtORG.Text = hfOrgName.Value;
            }
            //string strRefresh = Request.Form.Get("Refresh");
            if (ViewState["StrBad_SEAT"] != null)
            {
                txtBAD_SEAT.Text = ViewState["StrBad_SEAT"].ToString();
            }
            txtBAD_SEAT.ReadOnly = true;
        }

        protected void ddlSelectOrg_SelectedIndexChanged(object sender,EventArgs e)
        {
            dropServerBind(ddlSelectOrg.SelectedValue);
        }

        private void setReadOnlyControl(bool isReadOnly)
        {
            txtCOMPUTER_ROOM_NAME.ReadOnly = isReadOnly;
            txtADDRESS.ReadOnly = isReadOnly;
            txtCOMPUTER_NUMBER.ReadOnly = isReadOnly;
            txtBAD_SEAT.ReadOnly = isReadOnly;
            ddlIS_EFFECT.Enabled = !isReadOnly;
            btnSave.Enabled = !isReadOnly;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="employee"></param>
        private void loadData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }

            txtORG.Text = getEntityString(dt.Rows[0]["SHORT_NAME"]);
            hfOrgName.Value = txtORG.Text;
            txtCOMPUTER_ROOM_NAME.Text = getEntityString(dt.Rows[0]["COMPUTER_ROOM_NAME"]);
            txtADDRESS.Text = getEntityString(dt.Rows[0]["ADDRESS"]);
            txtCOMPUTER_NUMBER.Text = getEntityString(dt.Rows[0]["COMPUTER_NUMBER"]);
            txtBAD_SEAT.Text = getEntityString(dt.Rows[0]["BAD_SEAT"]);
			hfBadSeatIDs.Value = getEntityString(dt.Rows[0]["BAD_SEAT"]);
            ddlIS_EFFECT.SelectedValue = getEntityString(dt.Rows[0]["IS_EFFECT"]);
            hfOrgID.Value = getEntityString(dt.Rows[0]["ORG_ID"]);         
            txtContractPerson.Text = getEntityString(dt.Rows[0]["CONTRACT_PERSON"]);
            txtContractPhone.Text = getEntityString(dt.Rows[0]["CONTRACT_PHONE"]);

            OracleAccess db =new OracleAccess();
            string strSql = "select * from Computer_Server where Computer_Server_ID=" +
                            getEntityString(dt.Rows[0]["COMPUTER_SERVER_ID"]);
            DataSet ds = db.RunSqlDataSet(strSql);

            ddlSelectOrg.SelectedValue = ds.Tables[0].Rows[0]["Org_ID"].ToString();
            dropServerBind(ddlSelectOrg.SelectedValue);
            dropServer.SelectedValue = getEntityString(dt.Rows[0]["COMPUTER_SERVER_ID"]);
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

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (isHaveNull())
            {
                return;
            }
            if (saveData())
            {
                string strQuery = Request.QueryString.Get("strQuery");
                this.ClientScript.RegisterStartupScript(GetType(), "OK", "this.location='ComputerManageInfo.aspx?strQuery="+ strQuery+"&id=" + int.Parse(hfOrgID.Value) + "'", true);
            }
        }

        /// <summary>
        /// 判断是否有空
        /// </summary>
        /// <returns></returns>
        private bool isHaveNull()
        {
            if (string.IsNullOrEmpty(hfOrgID.Value))
            {
                OxMessageBox.alert(this, "请选择组织机构！");
                return true;
            }
            if (string.IsNullOrEmpty(txtCOMPUTER_ROOM_NAME.Text))
            {
                OxMessageBox.alert(this, "请填写微机教室名！");
                return true;
            }
            if (string.IsNullOrEmpty(txtCOMPUTER_NUMBER.Text))
            {
                OxMessageBox.alert(this, "请填写机位数！");
                return true;
            }

            if(string.IsNullOrEmpty(dropServer.SelectedValue))
            {
                OxMessageBox.alert(this, "请选择微机教室服务器！");
                return true;  
            }
            int num = 0;
            if (txtCOMPUTER_NUMBER.Text.Length > 3 || !int.TryParse(txtCOMPUTER_NUMBER.Text, out num))
            {
                OxMessageBox.alert(this, "请填写正确的机位数！");
                return true;
            }
            if (ddlIS_EFFECT.SelectedValue == "2")
            {
                OxMessageBox.alert(this, "请选择是否有效！");
                return true;
            }
            if (!string.IsNullOrEmpty(this.txtCOMPUTER_NUMBER.Text.Trim().ToString()) && !string.IsNullOrEmpty(this.txtBAD_SEAT.Text.Trim().ToString()))
            {
                int allcount = int.Parse(this.txtCOMPUTER_NUMBER.Text.Trim().ToString());
                int badCount = this.txtBAD_SEAT.Text.Trim().ToString().Split(',').Length;
                if (badCount > allcount)
                {
                    OxMessageBox.alert(this, "损坏机位数不能大于总机位数，请重新填写！");
                    return true;
                }
            }
            return false;
        }

        //保存数据
        private bool saveData()
        {

            int type = 2;
            if (!string.IsNullOrEmpty(ViewState["ComputerDetailType"].ToString()))
            {
                type = int.Parse(ViewState["ComputerDetailType"].ToString());
            }

            string sqlStr = string.Empty;

            if (type == 1)//修改
            {
                sqlStr = "update COMPUTER_ROOM set "
                    + " ORG_ID =" + hfOrgID.Value + " ,"
                    + "COMPUTER_ROOM_NAME='" + txtCOMPUTER_ROOM_NAME.Text + "',"
                    + "ADDRESS='" + txtADDRESS.Text + "',"
                    + "COMPUTER_NUMBER=" + txtCOMPUTER_NUMBER.Text + ","
					+ "BAD_SEAT='" + hfBadSeatIDs.Value + "',"
                    + "IS_EFFECT= " + ddlIS_EFFECT.SelectedValue + ","
                    + "Contract_Person='" + txtContractPerson.Text.Trim() + "',"
                    + "Contract_Phone='" + txtContractPhone.Text.Trim() + "',"
                    + "COMPUTER_SERVER_ID = " + (dropServer.SelectedValue == "" ? "null" : dropServer.SelectedValue) 
                    + " where COMPUTER_ROOM_ID=" + ViewState["ComputerDetailID"];
            }
            else if (type == 2) //新增
            {
                sqlStr = "insert into Computer_room values (COMPUTER_ROOM_SEQ.nextval," 
                    + hfOrgID.Value + ", '" + txtCOMPUTER_ROOM_NAME.Text + "','" + txtADDRESS.Text + "',"
					+ txtCOMPUTER_NUMBER.Text + ",'" + hfBadSeatIDs.Value + "'," 
                    + ddlIS_EFFECT.SelectedValue + "," + (dropServer.SelectedValue==""?"null":dropServer.SelectedValue) 
                    + ",'"+txtContractPerson.Text.Trim()+"','"+txtContractPhone.Text.Trim()+"',0)";

            }

            OracleAccess ora = new OracleAccess();

            try
            {
                ora.ExecuteNonQuery(sqlStr);

                if(ddlIS_EFFECT.SelectedValue == "0" && type == 1)
                {
                    sqlStr = "delete from COMPUTER_ROOM_DETAIL where COMPUTER_ROOM_ID=" + ViewState["ComputerDetailID"];
                    ora.ExecuteNonQuery(sqlStr);
                }
            }
            catch
            {
                OxMessageBox.alert(this, "编辑失败！");
                return false;
            }

            ViewState["ComputerDetailType"] = 1; //设置为编辑 
            return true;
        }

        private void dropServerBind(string orgID)
        {
            dropServer.Items.Clear();
            ListItem item = new ListItem();
            item.Value = "";
            item.Text = "--请选择--";
            dropServer.Items.Add(item);
            OracleAccess oa = new OracleAccess();
            string sql = String.Format("select * from COMPUTER_SERVER where ORG_ID = {0}", orgID);
            DataSet dsServer = oa.RunSqlDataSet(sql);
            if (dsServer != null && dsServer.Tables.Count > 0)
            {
                foreach (DataRow row in dsServer.Tables[0].Rows)
                {
                    ListItem li = new ListItem(row["COMPUTER_SERVER_NAME"].ToString(), row["COMPUTER_SERVER_ID"].ToString());
                    this.dropServer.Items.Add(li);
                }
            }
        }
    }
}
