<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerRoomUse.aspx.cs" EnableEventValidation="false" 
    Inherits="RailExamWebApp.RandomExamTai.ComputerRoomUse" %>

<%@ Register TagPrefix="uc1" TagName="date" Src="~/Common/Control/Date/Date.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>微机教室使用情况</title>
    <base target="_self" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
		function selectArow(rowIndex)
		{
			var t = document.getElementById("grdEntity");
			for (var i = 1; i < t.rows.length; i++)
			{
				if (i - 1 == rowIndex)
				{
					t.rows(i).style.backgroundColor = "#FFEEC2";
				}
				else
				{
					if ((i - 1) % 2 == 0)
					{
						t.rows(i).style.backgroundColor = "#EFF3FB";
					}
					else
					{
						t.rows(i).style.backgroundColor = "White";
					}
				}
			}
		}
		
		function applyComputer(id,statusId) 
		{
		    if(statusId != "0") 
		    {
		    	alert("该时间段已经被占用，不能预订申请!");
				return;
		    }
			
			if(!confirm('您确定要预订申请机位吗？')) {
				return;
			}

			__doPostBack("btnApply", id);
		}
		
		function showApply(strQuery) 
		{
			//alert(strQuery);
			var ret = window.showModalDialog("ComputerApplyDetail.aspx?strQuery="+ strQuery+"&mode=Insert","ComputerApplyDetail","status:false;dialogWidth:645px;dialogHeight:400px");   

		}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="gridPage">
                <div style="width: 90%;">
                    <table width="100%">
                        <tr>
                            <td style="height: 24px">
                                时间段：</td>
                            <td style="text-align: left;">
                                <uc1:date ID="dateBeginTime" runat="server" />
                                <asp:DropDownList ID="ddlBeginHour" runat="server">
                                </asp:DropDownList>时 至<uc1:date ID="dateEndTime" runat="server" />
                                <asp:DropDownList ID="ddlEndHour" runat="server">
                                </asp:DropDownList>时
                            </td>
                            <td style="text-align: right;">
                                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="查  询" OnClick="btnQuery_Click" />
                                <asp:Button ID="btnApply" runat="server" CssClass="buttonSearch" Text="查  询" Width="0px" OnClick="btnApply_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="content">
                    <yyc:SmartGridView ID="grdEntity" runat="server" DataKeyNames="COMPUTER_ROOM_APPLY_ID"
                        PageSize="15"  OnRowCreated="grdEntity_RowCreated"
                        AllowPaging="True">
                        <Columns>
                            <asp:TemplateField>
                                <headertemplate>
                                操作
                                </headertemplate>
                                <itemtemplate>
                                    <a href='#' onclick='applyComputer(<%#Eval("COMPUTER_ROOM_APPLY_ID")%>,<%#Eval("UseStatusID")%>)' class="underline">预订申请</a>
                                </itemtemplate>
                                <headerstyle width="8%" horizontalalign="Center" wrap="False" />
                                <itemstyle width="8%" horizontalalign="Center" wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="COMPUTER_ROOM_APPLY_ID" Visible="False" />
                            <asp:BoundField DataField="COMPUTER_ROOM_ID" Visible="False" />
                            <asp:BoundField DataField="orgName" HeaderText="单位">
                                <headerstyle width="10%" wrap="False" />
                                <itemstyle wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COMPUTER_ROOM_NAME" HeaderText="微机教室名称">
                                <headerstyle wrap="False" width="10%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Apply_Start_Time" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                                <headerstyle wrap="False" width="10%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Apply_End_Time" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                                <headerstyle wrap="False" width="8%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="UseStatus" HeaderText="状态">
                                <headerstyle wrap="False" width="8%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                           <asp:BoundField DataField="UseStatusID" Visible="False" /> 
                        </Columns>
                    </yyc:SmartGridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
