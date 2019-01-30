using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerServerInfoDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                string orgID = Request.QueryString.Get("OrgID");
                string csID = Request.QueryString.Get("CSID");
                if (!String.IsNullOrEmpty(orgID))
                {
                    this.hfOrgID.Value = orgID;
                    if (!String.IsNullOrEmpty(csID))
                    {
                        this.hfCSID.Value = csID;
                        InfoDetailShow(csID);
                    }
                    else
                    {
                        OracleAccess db = new OracleAccess();
                        string sql = "select * from Computer_Server where Org_ID="+orgID + " order by Computer_Server_No desc";
                        DataTable dt = db.RunSqlDataSet(sql).Tables[0];

                        if(dt.Rows.Count== 0)
                        {
                            txtNo.Text = orgID + "01";
                        }
                        else
                        {
                            int maxNo = Convert.ToInt32(dt.Rows[0]["Computer_Server_No"].ToString());

                            txtNo.Text = (maxNo + 1).ToString();
                        }
                    }
                }
            }
        }

        //确认
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            InfoDetailSave();
        }

        //显示已有的信息
        private void InfoDetailShow(string id)
        {
            OracleAccess oa = new OracleAccess();
            string sql = String.Format("select * from COMPUTER_SERVER where COMPUTER_SERVER_ID = {0}", id);
            DataSet dsInfoDetail = oa.RunSqlDataSet(sql);
            if (dsInfoDetail != null && dsInfoDetail.Tables.Count > 0)
            {
                this.txtNo.Text = dsInfoDetail.Tables[0].Rows[0]["COMPUTER_SERVER_NO"].ToString();
                this.txtName.Text = dsInfoDetail.Tables[0].Rows[0]["COMPUTER_SERVER_NAME"].ToString();
                this.txtAddress.Text = dsInfoDetail.Tables[0].Rows[0]["ADDRESS"].ToString();
                this.txtIP.Text = dsInfoDetail.Tables[0].Rows[0]["IPADDRESS"].ToString();
                this.txtMemo.Text = dsInfoDetail.Tables[0].Rows[0]["MEMO"].ToString();
            }
        }

        //保存数据
        private void InfoDetailSave()
        {
            OracleAccess oa = new OracleAccess();
            string sql;
            string csID = this.hfCSID.Value;
            string orgID = this.hfOrgID.Value;
            //新增信息
            if (String.IsNullOrEmpty(csID))
            {
                sql = String.Format(
                    @"insert into COMPUTER_SERVER values({0},'{1}','{2}','{3}','{4}',{5},'{6}')",
                    "COMPUTER_SERVER_SEQ.NEXTVAL",
                    this.txtNo.Text.Trim(),
                    this.txtName.Text.Trim(),
                    this.txtAddress.Text.Trim(),
                    this.txtMemo.Text,
                    this.hfOrgID.Value,
                    this.txtIP.Text
                   );
            }
            //修改信息
            else
            {
                sql = String.Format(
                @"update COMPUTER_SERVER 
                  set 
                    COMPUTER_SERVER_NO = '{0}',
                    COMPUTER_SERVER_NAME = '{1}',
                    ADDRESS = '{2}',
                    IPADDRESS = '{3}',
                    MEMO = '{4}'
                  where
                    COMPUTER_SERVER_ID = {5}",
                 this.txtNo.Text.Trim(),
                 this.txtName.Text.Trim(),
                 this.txtAddress.Text.Trim(),
                 this.txtIP.Text,
                 this.txtMemo.Text,
                 this.hfCSID.Value
                );
            }
            try
            {
                oa.ExecuteNonQuery(sql);
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('保存成功'); location.href='ComputerServerInfo.aspx?OrgID=" + orgID + "';", true);
            }
            catch
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('保存失败');", true);
            }
        }
    }
}
