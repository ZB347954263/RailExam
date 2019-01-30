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
using ComponentArt.Web.UI;
using System.Collections.Generic;
using System.Text;

namespace RailExamWebApp.Train
{
	public partial class TrainBadSeat : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Request.QueryString["badCount"] != null)
				{
					int badCount = int.Parse(Request.QueryString["badCount"]);
					BindTypeTree(badCount);
				}
			}
			
		}
		private void BindTypeTree(int count)
		{
			TreeViewNode SeatNode = null;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					SeatNode = new TreeViewNode();
					SeatNode.Checked = false;
					SeatNode.ID = (i+1).ToString();
					SeatNode.Value = (i+1).ToString();
					SeatNode.Text = (i+1) + "号机位";
					SeatNode.ToolTip = (i+1) + "号机位";
					SeatNode.ShowCheckBox = true;
					this.SelectSeatTree.Nodes.Add(SeatNode);
				}
			}
			//this.SelectSeatTree.DataBind();
			if (Request.QueryString["mode"] != null)
			{
				if (Request.QueryString["mode"] == "Edit")
				{
					if (Request.QueryString["txtDadSeat"] != string.Empty)
					{
						string[] strDadSeat = Request.QueryString["txtDadSeat"].ToString().Split(',');
						for (int j = 0; j < this.SelectSeatTree.Nodes.Count;j++)
						{
							for (int i = 0; i < strDadSeat.Length; i++)
							{
								string a = int.Parse(SelectSeatTree.Nodes[j].Value).ToString();
								string b = strDadSeat[i].ToString();
								if (int.Parse(SelectSeatTree.Nodes[j].Value).ToString() == strDadSeat[i].ToString())
								{
									this.SelectSeatTree.Nodes[j].Checked= true;
									break;
								}
							}
						}
					}
				}
				else if (Request.QueryString["Edit"] == "Insert")
				{

				}
				//this.SelectSeatTree.DataBind();
			}
		}


		protected void btnConfirm_Click(object sender, ImageClickEventArgs e)
		{
			StringBuilder StrBAD_SEAT = new StringBuilder();
			foreach (TreeViewNode TVN in SelectSeatTree.CheckedNodes)
			{
				if (TVN.Checked)
				{
					StrBAD_SEAT.Append(int.Parse(TVN.ID).ToString() + ",");
				}
			}
			string strReturn=string.Empty;
			if (StrBAD_SEAT.ToString().IndexOf(',') > 0)
			{
				strReturn = StrBAD_SEAT.ToString().Substring(0, StrBAD_SEAT.Length - 1);
			}
			else
			{
				strReturn = StrBAD_SEAT.ToString();
			}
			ClientScript.RegisterStartupScript(GetType(), "","window.returnValue='" + strReturn + "';window.close();",  true);
		}
	}
}
