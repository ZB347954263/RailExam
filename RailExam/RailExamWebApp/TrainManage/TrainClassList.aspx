<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainClassList.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.TrainClassList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>培训计划列表</title>
    <link href="../Style/css.css" rel="stylesheet" type="text/css" />

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
		
		function deleteClass() 
		{

			var deleteRight = document.getElementById("HfDeleteRight").value;
			
			if(deleteRight == "False" ) {
				alert("您没有该操作的权限！");
				return false;
			}
			
			return confirm('您确定要删除此培训计划信息吗？');
		}
		
		function addClass() {
			
			var updateRight = document.getElementById("HfUpdateRight").value;
			
			if(updateRight == "False" ) {
				alert("您没有该操作的权限！");
				return;
			}
			
			javascript:location.href = 'TrainClass.aspx';
		}
		
		function updateClass(id) 
		{
			var updateRight = document.getElementById("HfUpdateRight").value;
			
			if(updateRight == "False" ) {
				alert("您没有该操作的权限！");
				return;
			}
			
			javascript:location.href = "TrainClass.aspx?ID=" + id+"&type=update";
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
                        培训管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        培训班列表</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
            </div>
            <div id="content">
                <div style="text-align: left;">
                    <table width="100%">
                        <tr>
                            <td style="height: 24px">
                                培训班编号：</td>
                            <td>
                                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                            </td>
                            <td style="height: 24px">
                                培训班名称：</td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right; height: 24px;" colspan="2">
                                <asp:Button ID="btnQuery" runat="server" Text="查  询" CssClass="button" OnClick="btnQuery_Click" />
                                <asp:Button ID="btnExcel" runat="server" Text="导出Excel" Visible="false" CssClass="button"
                                    PostBackUrl="~/Common/ExportExcel.aspx" OnClientClick="exportExcel();" />
                                <input id="Button2" type="button" value="添  加" class="button" onclick="addClass()" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp; 计划名称：</td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlTrainPlan" runat="server" DataTextField="TRAIN_PLAN_NAME"
                                    DataValueField="TRAIN_PLAN_ID" OnDataBound="ddlTrainPlan_DataBound" >
                                </asp:DropDownList>
                            </td>
                            <td style="display: none;">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;状态：</td>
                            <td style="display: none;">
                                <asp:DropDownList ID="ddlTrainPlanPhase" runat="server" Width="150px">
                                    <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="新增" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="已完成" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: center">
                    <yyc:SmartGridView ID="grdEntity" runat="server" PageSize="15" OnRowCommand="grdEntity_RowCommand"
                        AllowPaging="true" AllowSorting="true" OnRowCreated="grdEntity_RowCreated" AutoGenerateColumns="false"
                        DataKeyNames="TRAIN_CLASS_ID" OnRowDataBoundDataRow="grdEntity_RowDataBoundDataRow"
                        DataSourceID="ObjectDataSource1">
                        <Columns>
                            <asp:TemplateField HeaderText="操作">
                                <headertemplate> 操作</headertemplate>
                                <itemstyle width="80px" horizontalalign="Center" wrap="false" />
                                <headerstyle width="80px" horizontalalign="Center" wrap="false" />
                                <itemtemplate>
                                        <span id="spanView" runat="server">
                       <a href='TrainClass.aspx?ID=<%#Eval("TRAIN_CLASS_ID")%>&type=view' >
                       <img src="../App_Themes/Default/Images/Menu/view.gif" style="border:0" alt="查看" />
                      </a> 
                      </span>         <span id="add" runat="server">        
                                            <a href='#' onclick="updateClass('<%#Eval("TRAIN_CLASS_ID")%>','update')" >
                                                <img src="../Common/Image/edit_col_edit.gif" alt="修改"  style="border:0"/></a></span>
                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="underline" CommandName="del"
                                                OnClientClick="return deleteClass()" CommandArgument='<%#Eval("TRAIN_CLASS_ID")%>'><img src="../Common/Image/edit_col_delete.gif" alt="删除" style="border:0"  /></asp:LinkButton>
                                                        </itemtemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TRAIN_CLASS_ID" HeaderText="TRAIN_CLASS_ID" Visible="false"
                                ReadOnly="True" SortExpression="TRAIN_CLASS_ID" />
                            <asp:BoundField DataField="TRAIN_CLASS_CODE" HeaderText="培训班编号" ReadOnly="True" SortExpression="TRAIN_CLASS_CODE" />
                            <asp:BoundField DataField="TRAIN_CLASS_NAME" HeaderText="培训班名称" ReadOnly="True" SortExpression="TRAIN_CLASS_NAME" />
                            <asp:BoundField DataField="TRAIN_PLAN_NAME" HeaderText="培训计划名称" ReadOnly="True" SortExpression="TRAIN_PLAN_NAME" />
                            <asp:BoundField DataField="BEGIN_DATE_1" HeaderText="开班日期" ReadOnly="True" SortExpression="BEGIN_DATE" />
                            <asp:BoundField DataField="END_DATE_1" HeaderText="结束日期" ReadOnly="True" SortExpression="END_DATE" />
                            <asp:BoundField DataField="MAKEDATE_1" HeaderText="制定日期" ReadOnly="True" SortExpression="MAKEDATE" />
                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="制定人" ReadOnly="True" SortExpression="EMPLOYEE_NAME" />
                            <asp:BoundField DataField="TRAIN_CLASS_STATUS_Name" HeaderText="培训班状态" ReadOnly="True"
                                Visible="false" SortExpression="TRAIN_CLASS_STATUS_Name" />
                            <%--<asp:BoundField DataField="MEMO" HeaderText="备注" ReadOnly="True" SortExpression="MEMO" />--%>
                        </Columns>
                    </yyc:SmartGridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
                        OnSelected="ObjectDataSource1_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="hfSelect" runat="server" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
