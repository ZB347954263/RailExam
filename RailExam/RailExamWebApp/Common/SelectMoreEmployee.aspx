<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectMoreEmployee.aspx.cs"
    Inherits="RailExamWebApp.Common.SelectMoreEmployee" %>

<%@ Register TagPrefix="yyc" Namespace="YYControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择员工</title>
    <base target="_self" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        
         function selectArow(obj)
         {
         	var TBL = document.getElementById("grdEntity");
         	for (var i = 1; i < TBL.rows.length; i++)
         	{
         		TBL.rows[i].style.backgroundColor = "White";
         		if (i % 2 != 0)
         			TBL.rows[i].style.backgroundColor = "#EFF3FB";
         	}
         	obj.style.backgroundColor = "#FFEEC2";
         }

//         function check(checkbox, employeeid)
//		{
//		    //删除
//		    if (document.getElementById('hfid').value.indexOf(employeeid) > -1)
//		    {
//		        removeID(employeeid);
//		    }
//		    else
//		        document.getElementById('hfid').value += employeeid + ",";
//		}

//		function removeID(empID)
//		{
//		    var ids = document.getElementById('hfid').value.substring(0, document.getElementById('hfid').value.length - 1);
//		    document.getElementById('hfid').value = "";
//		    var arrID = ids.split(',');
//		    for (var i = 0; i < arrID.length; i++)
//		    {
//		        //找不到时相等的就添加
//		        if (arrID[i] != empID)
//		        {
//		            document.getElementById('hfid').value += arrID[i] + ",";
//		        }
//		    }
//		    
//		}
//		 function setIDToHid()
//		 {
//		 	var arr = [];
//		 	var hidID = document.getElementById("hfid");
//		 	var TBL = document.getElementById("grdEntity");

//		 	for (var i = 1; i < TBL.rows.length; i++)
//		 	{

//		 		var obj = TBL.rows[i].cells[0].childNodes[0];
//		 		if (obj.type == "checkbox")
//		 		{
//		 			if (obj.checked == true)
//		 				arr.push(TBL.rows[i].cells[0].innerText);
//		 		}
//		 	}
//		 	document.getElementById("hfid").value = arr;
//		 	 
//		 }
 
		 function chkAll(obj)
		 {
		 	var TBL = document.getElementById("grdEntity");
		 	for (var i = 1; i < TBL.rows.length; i++)
		 	{
		 		var ob = TBL.rows[i].cells[0].childNodes[0];
		 		if (ob.type == "checkbox")
		 			ob.checked = obj.checked;
		 	}
		 }

 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="contentTable">
            <tr>
                <td style="width: 10%" align="right">
                    站段
                </td>
                <td style="width: 23%" align="left">
                    <asp:DropDownList ID="ddlStation" Width="200px" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlStation_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%" align="right">
                    &nbsp;&nbsp;车间
                </td>
                <td style="width: 22%" align="left">
                    <asp:DropDownList ID="ddlWorkShop" Width="200px" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlWorkShop_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td style="width: 10%" align="right">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;班组
                </td>
                <td style="width: 25%" align="left" colspan="2">
                    <asp:DropDownList ID="ddlOrg" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%" align="right">
                    系统
                </td>
                <td style="width: 23%" align="left">
                    <asp:DropDownList ID="ddlSystem" Width="200px" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlSystem_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%" align="right">
                    &nbsp;&nbsp;工种
                </td>
                <td style="width: 22%" align="left">
                    <asp:DropDownList ID="ddlType" Width="200px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td style="width: 10%" align="right">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;职名
                </td>
                <td style="width: 25%" align="left" colspan="2">
                    <asp:DropDownList ID="ddlPost" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%" align="right">
                    姓名</td>
                <td style="width: 23%" align="left">
                    <asp:TextBox runat="server" ID="txtEmployeeName" Width="200px"></asp:TextBox>
                </td>
                <td style="width: 10%" align="right">
                    拼音码</td>
                <td style="width: 22%" align="left">
                    <asp:TextBox runat="server" ID="txtPinyinCode" Width="200px"></asp:TextBox></td>
                <td style="width: 10%" align="right">
                    是否班组长</td>
                <td style="width: 22%" align="left">
                    <asp:DropDownList ID="ddlGroupLeader" runat="server" Width="200px">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:DropDownList></td>
                <td align="center">
                    <asp:ImageButton runat="server" ID="btnQuery" ImageUrl="~/Common/Image/find.gif"
                        OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <div style="overflow-x: auto">
            <table border="0" style="width: 99%;">
                <tr>
                    <td style="width: 100%;  " valign="top" align="center">
                        <table style="width: 100%;  ">
                            <tr>
                                <td style="vertical-align: top;">
                                    <yyc:SmartGridView ID="grdEntity" runat="server" PageSize="15" OnRowCommand="grdEntity_RowCommand"
                                        OnRowCreated="grdEntity_RowCreated" AutoGenerateColumns="False" AllowSorting="True"
                                        AllowPaging="True" DataKeyNames="EMPLOYEE_ID" OnRowDataBoundDataRow="grdEntity_RowDataBoundDataRow"
                                        DataSourceID="ObjectDataSource1" OnPageIndexChanging="grdEntity_PageIndexChanging"
                                        OnRowDataBound="grdEntity_RowDataBound" OnPageIndexChanged="grdEntity_PageIndexChanged">
                                        <Columns>
                                            <asp:TemplateField>
                                                <headertemplate>
                                          <asp:CheckBox ID="chkAll" runat="server"/>
                                        
                                                </headertemplate>
                                                <itemtemplate>
                         
                                                <asp:CheckBox ID="item" runat="server"/>
                                                 <span style="display:none" runat="server" id="spanID"><%# Eval("EMPLOYEE_ID")%></span>
                                               <img src="../Common/Image/charge.gif" runat="server" id="imgchecked" style=" display: none"/>
                                                </itemtemplate>
                                                <headerstyle width="50px" wrap="false" />
                                                <itemstyle width="50px" wrap="false" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" Visible="False"
                                                ReadOnly="True" SortExpression="EMPLOYEE_ID" />
                                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="姓名" ReadOnly="True" SortExpression="EMPLOYEE_NAME">
                                                <headerstyle width="80px" wrap="false" />
                                                <itemstyle width="80px" wrap="false" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="员工编码" SortExpression="WORK_NO">
                                                <itemtemplate>
                                            <asp:Label runat="server" Text='<%# Bind("WORK_NO") %>' id="lblWork_NO"></asp:Label>
                                            <asp:Label runat="server" Text='<%# Bind("identity_cardno") %>' id="lblidentity_cardno" Visible="false"></asp:Label>
                                            </itemtemplate>
                                                <headerstyle width="200px" wrap="false" />
                                                <itemstyle width="200px" wrap="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="身份证号" SortExpression="WORK_NO">
                                                <itemtemplate>
                                            <asp:Label runat="server" Text='<%# Bind("identity_cardno") %>' id="lblidentity_cardno1"></asp:Label>
                                            </itemtemplate>
                                                <headerstyle width="200px" wrap="false" />
                                                <itemstyle width="200px" wrap="false" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SEX" HeaderText="性别" ReadOnly="True" SortExpression="SEX">
                                                <headerstyle width="50px" wrap="false" />
                                                <itemstyle width="50px" wrap="false" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="short_name" HeaderText="组织机构" ReadOnly="True" SortExpression="short_name">
                                                <headerstyle width="150px" wrap="false" />
                                                <itemstyle width="150px" wrap="false" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="post_name" HeaderText="职名" ReadOnly="True" SortExpression="post_name">
                                                <headerstyle width="150px" wrap="false" />
                                                <itemstyle width="150px" wrap="false" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IS_GROUP_LEADER" HeaderText="是否班组长" ReadOnly="True" SortExpression="IS_GROUP_LEADER">
                                                <headerstyle width="50px" wrap="false" />
                                                <itemstyle width="50px" wrap="false" />
                                            </asp:BoundField>
                                        </Columns>
                                    </yyc:SmartGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90%;" align="center">
                       <asp:Button ID="btnAllOK" runat="server" Text="添加所有" CssClass="button" OnClick="btnAllOK_Click" />
                        <asp:Button ID="btnOK" runat="server" Text="确  定" class="button" OnClick="btnOK_Click" />
                        <input type="button" id="btnClose" value="关  闭" class="button" onclick="window.close()" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
            OnSelected="ObjectDataSource1_Selected">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfid" runat="server" />
        <asp:HiddenField ID="hfSelect" runat="server" />
    </form>
</body>
</html>
