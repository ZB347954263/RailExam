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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerMACInfo : PageBase
    {
        private OracleAccess access;
        private static Hashtable hsInfo;
        public static int RowIndex = 0;
        private static DataTable dtAllMAC;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TempInfo();
                GetAllMAC();
				SetBtn();
            }
        }
        private DataTable GetInfo()
        {
           access=new OracleAccess();
            return
                access.RunSqlDataSet("select * from computer_room_detail where computer_room_id=" +
                                     Convert.ToInt32(Request.QueryString.Get("comid"))).Tables[0];
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void TempInfo()
        {
            DataTable dtInfo = GetInfo();
            hsInfo=new Hashtable();
            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                foreach (DataRow r in dtInfo.Rows)
                {
                    hsInfo.Add(Convert.ToInt32(r["computer_room_seat"]), r["mac_address"].ToString());
                }
            }
            grdEntity.DataSource = hsInfo;
            grdEntity.DataBind();
            
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void RefInfo()
        {
           
            btnSave.Enabled = true;
            access=new OracleAccess();
            string sql = "select computer_number from computer_room where computer_room_id="+Convert.ToInt32(Request.QueryString.Get("comid"));
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];
            int num = 0;
            if(dt!=null && dt.Rows.Count>0)
                 num = Convert.ToInt32(dt.Rows[0]["computer_number"]);
            DataTable dtInfo = GetInfo();
          
          
            hsInfo=new Hashtable();
            foreach (GridViewRow row in grdEntity.Rows)
            {
                Label lblNum = (Label)row.FindControl("lblNum");
                HtmlInputText txtMAC = (HtmlInputText)row.FindControl("txtMAC");
                hsInfo.Add(Convert.ToInt32(lblNum.Text), txtMAC.Value);
            }
           
            if (dtInfo.Rows.Count == 0 && hsInfo.Count==0)
            {
                for (int i = 1; i < num+1; i++)
                {
                    hsInfo.Add(i,"");
                }
            }
            if (hsInfo.Count > 0)
            {
                if (dtInfo.Rows.Count > num)
                {
                    for (int i = num + 1; i < dtInfo.Rows.Count + 1; i++)
                    {
                        hsInfo.Remove(i);
                    }

                }
                if (dtInfo.Rows.Count < num && hsInfo.Count < num)
                {
                    for (int i = dtInfo.Rows.Count + 1; i < num + 1; i++)
                    {
                        hsInfo.Add(i, "");
                    }
                }
            }
            grdEntity.DataSource = hsInfo;
            grdEntity.DataBind();
            
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveInfo()
        {
            bool bl=true;
            foreach (GridViewRow row in grdEntity.Rows)
            {
                HtmlInputText txtMAC = (HtmlInputText)row.FindControl("txtMAC");
                if(txtMAC.Value.Trim()!="")
                {
                   DataRow[] arr= dtAllMAC.Select("mac_address='" + txtMAC.Value + "'");
                   if (arr.Length > 0)
                   {
                       bl = false;
                       ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                              "alert('MAC地址：" + txtMAC.Value +
                                                              "已存在，请重新输入！');", true);
                       txtMAC.Focus();
                       break;
                   }
                }
            }
            
            if(bl)
            {
                try
                {
                    access = new OracleAccess();
                    access.ExecuteNonQuery("delete from computer_room_detail where computer_room_id=" +
                                           Convert.ToInt32(Request.QueryString.Get("comid")));
                }
                catch
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！')", true);
                }
                foreach (GridViewRow row in grdEntity.Rows)
                {
                    Label lblNum = (Label) row.FindControl("lblNum");
                    HtmlInputText txtMAC = (HtmlInputText) row.FindControl("txtMAC");

                    AddFun(Convert.ToInt32(lblNum.Text), txtMAC.Value);
                }
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存成功！');window.close();", true);
            }
        }

        private void AddFun(int num,string mac)
        {
            access=new OracleAccess();
			mac = mac.ToUpper();
            string inSql =
                string.Format(
                    "insert into  computer_room_detail(computer_room_detail_id,computer_room_id,computer_room_seat,mac_address) values(computer_room_detail_seq.nextval,{0},{1},'{2}')",
                   Convert.ToInt32(Request.QueryString.Get("comid")), num, mac);
            try
            {
                access.ExecuteNonQuery(inSql.ToString());
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(),"","alert('数据保存失败！')",true);
            }           
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {
            RefInfo();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        protected void grdEntity_RowDataBoundDataRow(object sender, GridViewRowEventArgs e)
        {
            Label lblNum = (Label)e.Row.FindControl("lblNum");
          

            HtmlInputText txtMAC = (HtmlInputText)e.Row.FindControl("txtMAC");
            if (hsInfo != null)
            {
                txtMAC.Value = hsInfo[Convert.ToInt32(lblNum.Text)].ToString();
            }
           
        }

        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
           RowIndex= e.Row.RowIndex;
        }

        private void GetAllMAC()
        {
            access=new OracleAccess();
            DataTable dt =
                access.RunSqlDataSet(
                    "select mac_address from computer_room_detail where mac_address is not null and computer_room_id!=" +
                    Convert.ToInt32(Request.QueryString.Get("comid"))).Tables[0];
            dtAllMAC = dt;
        }

		/// <summary>
		/// 如果机位数改变，需先更新再保存
		/// </summary>
		private void SetBtn()
		{
			access = new OracleAccess();
			string sql = "select computer_number from computer_room where computer_room_id=" + Convert.ToInt32(Request.QueryString.Get("comid"));
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			int num = 0;
			int oldnum = 0;
			if (dt != null && dt.Rows.Count > 0)
				num = Convert.ToInt32(dt.Rows[0]["computer_number"]);
			 sql = "select count(*) from computer_room_detail where computer_room_id=" + Convert.ToInt32(Request.QueryString.Get("comid"));
			 dt = access.RunSqlDataSet(sql).Tables[0];
			if (dt != null && dt.Rows.Count > 0)
				 oldnum = Convert.ToInt32(dt.Rows[0][0]);
			 if (num == oldnum)
				btnSave.Enabled = true;
		}
    }

   
}
