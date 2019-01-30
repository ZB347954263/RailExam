<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerManageInfo.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerManageInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>微机教室信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

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
		function computerMAC(comID,orgID) {
			var stationOrgID = document.getElementById("hfOrgID").value;
			if(orgID!=stationOrgID && document.getElementById("hfRoleID").value!="1" && document.getElementById("hfRoleID").value!="363")
			{
				alert("您没有该操作的权限！");
				return;
			}
			 var returnvalue = window.showModalDialog('ComputerMACInfo.aspx?comid=' + comID + "&num=" + Math.random(),
					'', 'help:no; status:no; dialogWidth:550px;dialogHeight:500px');
				
		}
		
		  function AddComputerRoom() {
//            var selectOrgId = document.getElementById("hfSelectOrg").value;
//		  	var stationOrgID = document.getElementById("hfOrgID").value;
//            if(selectOrgId==null||selectOrgId.length==0)
//            {
//                alert("请选择一个机构");
//                return;
//            }
//		  	var selectOrgId = "";
//		  	var orgIDs =  window.showModalDialog('../Common/SelectOrganization.aspx?Type=Station', 
//                    '', 'help:no; status:no; dialogWidth:310px;dialogHeight:600px;scroll:no;');
//     	    if(orgIDs!=undefined)
//     	    {
//     		    selectOrgId = orgIDs.split("|")[0];
//     	    }
//		  	if(selectOrgId!="")
//		  	{
//		  		var stationOrgId = document.getElementById("hfOrgID").value;
//		  		if (selectOrgId != stationOrgId && document.getElementById("hfRoleID").value!="1") {
//		  			alert("只能新增该单位的微机教室！");
//		  			return;
//		  		}
//		  		
		  	 var updateRight = document.getElementById("HfUpdateRight").value;
			
			if(updateRight == "False" ) {
				alert("您没有该操作的权限！");
				return;
			}
		  	 
		  	 var selectOrgId = document.getElementById("hfSelectOrg").value;
		  	 var room = document.getElementById("txtCOMPUTER_ROOM").value;
		  	 var address = document.getElementById("txtAddress").value;
		  	var ddl = document.getElementById("ddl").value;

		  	 var strQuery = selectOrgId + "|" + room + "|" + address+"|"+ddl;

		  		window.location = "ComputerDetail.aspx?Type=2&strQuery="+strQuery;
//		  	}
		  }
		  function editComputerRoom(roomID,orgID) {
		  	var updateRight = document.getElementById("HfUpdateRight").value;
			
			if(updateRight == "False" ) {
				alert("您没有该操作的权限！");
				return;
			}
		  	
		  	var stationOrgID = document.getElementById("hfOrgID").value;
			if(orgID!=stationOrgID && document.getElementById("hfRoleID").value!="1" && document.getElementById("hfRoleID").value!="363")
			{
				alert("您没有该操作的权限！");
				return;
			}

		  	var selectOrgId = document.getElementById("hfSelectOrg").value;
		  	var room = document.getElementById("txtCOMPUTER_ROOM").value;
		  	var address = document.getElementById("txtAddress").value;

		  	var ddl = document.getElementById("ddl").value;

		  	 var strQuery = selectOrgId + "|" + room + "|" + address+"|"+ddl;
		  	
		  	window.location = "ComputerDetail.aspx?ID=" + roomID + "&Type=1&OrgID=" + orgID+"&strQuery="+strQuery;
		  	 
		  }

		  function deleteRoom(roomID,orgID) {
		  	var deleteRight = document.getElementById("HfDeleteRight").value;
			
			if(deleteRight == "False" ) {
				alert("您没有该操作的权限！");
				return;
			}
		  	
		  	var stationOrgID = document.getElementById("hfOrgID").value;
		  	if (orgID != stationOrgID && document.getElementById("hfRoleID").value!="1" && document.getElementById("hfRoleID").value!="363")
		  	{
		  		alert("您没有该操作的权限！");
		  		return;
		  	}
		  	
		  	if(confirm('您确定要删除此记录吗？')) {
		  		document.getElementById("hfComID").value = roomID;
		  		document.getElementById("btnDel").click();
		  	}
		  	
		  }
		  function SelectOrg() {
		  	var orgIDs =  window.showModalDialog('../Common/SelectOrganization.aspx?Type=Station', 
                    '', 'help:no; status:no; dialogWidth:310px;dialogHeight:600px;scroll:no;');
     	    if(orgIDs!=undefined)
     	    {
     		    document.getElementById("hfSelectOrg").value = orgIDs.split("|")[0]; 
     	    	 document.getElementById("txtOrg").value = orgIDs.split("|")[1];
     	    }
		  }
		  
		  function showUse(roomid) {
		  	
		  	    var updateRight = document.getElementById("HfUpdateRight").value;
    			
			    if(updateRight == "False" ) {
				    alert("您没有该操作的权限！");
				    return;
			    }
		  		var orgIDs =  window.showModalDialog('ComputerRoomUse.aspx?roomId='+roomid, 
                    '', 'help:no; status:no; dialogWidth:800px;dialogHeight:600px;scroll:no;');
		  }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        微机教室管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        微机教室信息</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
            </div>
           
            <div id="gridPage">
                <div style="  width: 93%;">
                    <table width="100%">
                        <tr>
                            <td style="height: 24px; width: 41px;">
                                单位：</td>
                            <td style="text-align: left; width: 222px;">
                                <asp:TextBox ID="txtOrg" runat="server" Width="180px"></asp:TextBox>
                                 <a href="#" title="选择单位" onclick="SelectOrg()">
                                <asp:Image runat="server" ID="Image1" ImageUrl="../Common/Image/search.gif" AlternateText="选择单位">
                                </asp:Image>
                            </a>
                            </td>
                            <td style="height: 24px; width: 121px; text-align: right ">
                                微机教室名称：</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCOMPUTER_ROOM" runat="server" Width="180px"></asp:TextBox>
                            </td>
                             <td style="text-align: right;">
                               <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="查  询" OnClick="btnQuery_Click" />
                                <input type="button" id="add" class="button" value="新  增" onclick=" AddComputerRoom()"/>
                                  <asp:Button runat="server" ID="btnDel" style="display: none;" OnClick="btnDel_Click"></asp:Button>
                             </td>
                        </tr>
                        <tr>
                            <td style="height: 24px; width: 41px;">
                                地址：</td>
                            <td style="text-align: left; width: 222px;">
                                <asp:TextBox ID="txtAddress" runat="server" Width="180px"></asp:TextBox>
                            </td>
                           <td style="height: 24px; width: 121px;text-align: right">
                                是否有效：</td>
                            <td style="text-align: left; ">
                                <asp:DropDownList runat="server" ID="ddl">
                                    <asp:ListItem  Value="" Text="全部"></asp:ListItem>
                                     <asp:ListItem  Value="1" Text="有效" Selected="True"></asp:ListItem>
                                    <asp:ListItem  Value="0" Text="无效"></asp:ListItem> 
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="content">
                    <yyc:SmartGridView ID="grdEntity" runat="server" DataKeyNames="COMPUTER_ROOM_ID"
                        PageSize="15" OnRowCommand="grdEntity_RowCommand" AllowSorting="True" OnRowCreated="grdEntity_RowCreated"
                        AllowPaging="True" DataSourceID="ObjectDataSource1">
                        <Columns>
                            <asp:TemplateField>
                                <headertemplate>
                                操作
                                </headertemplate>
                                <itemtemplate>
							<a href='#' onclick='editComputerRoom(<%#Eval("COMPUTER_ROOM_ID")%>,<%#Eval("ORG_ID") %>)' class="underline">编辑</a>
							<a href='#' onclick='computerMAC(<%#Eval("COMPUTER_ROOM_ID")%>,<%# Eval("ORG_ID") %>)' class="underline">设置机位</a>
							<a href="#" onclick="deleteRoom(<%#Eval("COMPUTER_ROOM_ID")%>,<%# Eval("ORG_ID") %>)" class="underline">删除</a>
							<a href="#" onclick="showUse(<%#Eval("COMPUTER_ROOM_ID") %>)" class="underline">使用情况</a>
                                </itemtemplate>
                                <headerstyle width="8%" horizontalalign="Center" wrap="False" />
                                <itemstyle width="8%" horizontalalign="Center" wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="COMPUTER_ROOM_ID" Visible="False" />
                            <asp:BoundField DataField="orgName" HeaderText="单位" SortExpression="orgName">
                                <headerstyle width="10%" wrap="False" />
                                <itemstyle wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COMPUTER_ROOM_NAME" HeaderText="微机教室名称" SortExpression="COMPUTER_ROOM_NAME">
                                <headerstyle wrap="False" width="10%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ADDRESS" HeaderText="地址" SortExpression="ADDRESS">
                                <headerstyle wrap="False" width="30%"></headerstyle>
                                <itemstyle wrap="True"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="COMPUTER_NUMBER" HeaderText="机位数" SortExpression="COMPUTER_NUMBER">
                                <headerstyle wrap="False" width="10%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CONTRACT_PERSON" HeaderText="联系人" SortExpression="CONTRACT_PERSON">
                                <headerstyle wrap="False" width="8%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CONTRACT_PHONE" HeaderText="联系电话" SortExpression="CONTRACT_PHONE">
                                <headerstyle wrap="False" width="15%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="COMPUTER_SERVER_NO" HeaderText="服务器编号" SortExpression="COMPUTER_SERVER_NO">
                                <headerstyle wrap="False" width="8%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                           <asp:BoundField DataField="IsEffect" HeaderText="是否有效" SortExpression="IsEffect" >
                                <headerstyle wrap="False" width="8%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField> 
                            <asp:BoundField DataField="is_used" HeaderText="是否被占用" SortExpression="is_used" Visible="false">
                                <headerstyle wrap="False" width="8%"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                        </Columns>
                    </yyc:SmartGridView>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="Get" TypeName="OjbData" OnSelected="ObjectDataSource1_Selected">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfSql" Name="sql" PropertyName="Value" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfSql" runat="server" />
        <asp:HiddenField ID="hfSelectOrg" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfRoleID" runat="server" />
         <asp:HiddenField ID="hfComID" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" /> 
    </form>
</body>
</html>
