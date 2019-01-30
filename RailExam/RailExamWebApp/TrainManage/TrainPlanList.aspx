<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainPlanList.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.TrainPlanList" %>

<%@ Register TagPrefix="yyc" Namespace="YYControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>培训计划列表</title>

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

		function PlanUp(planID) { 
			var updateRight = document.getElementById("HfUpdateRight").value;
			
			if(updateRight == "False" ) {
				alert("您没有该操作的权限！");
				return;
			}
			
			 	var returnvalue = window.showModalDialog('TrainPlanUp.aspx?planID=' + planID + "&num=" + Math.random(),
	   		'', 'help:no; status:no; dialogWidth:850px;dialogHeight:730px');
		}
		
		function deletePlan() 
		{

			var deleteRight = document.getElementById("HfDeleteRight").value;
			
			if(deleteRight == "False" ) {
				alert("您没有该操作的权限！");
				return false;
			}
			
			if(!confirm('您确定要删除此培训计划信息吗？')) {
				return false;
			}

			return true;
		}
		
		function addPlan() {
			
			var updateRight = document.getElementById("HfUpdateRight").value;
			
			if(updateRight == "False" ) {
				alert("您没有该操作的权限！");
				return;
			}
			
			javascript:location.href = 'TrainPlanAdd.aspx';
		}
		
		function updatePlan(id) 
		{
			var updateRight = document.getElementById("HfUpdateRight").value;
			
			if(updateRight == "False" ) {
				alert("您没有该操作的权限！");
				return;
			}
			
			javascript:location.href = 'TrainPlanAdd.aspx?ID=' + id;
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
                        培训计划列表</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
            </div>
            <div id="content">
                <div style="text-align: left;">
                    <table width="100%">
                        <tr>
                            <td>
                                年度：</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                计划类别：</td>
                            <td>
                                <asp:DropDownList ID="ddlTrainPlanType" runat="server" DataTextField="TRAINPLAN_TYPE_NAME"
                                    DataValueField="TRAINPLAN_TYPE_ID" OnDataBound="ddlTrainPlanType_DataBound" >
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right" colspan="2">
                                <asp:Button ID="btnQuery" runat="server" Text="查  询" CssClass="button" OnClick="btnQuery_Click" />
                                <asp:Button ID="btnExcel" runat="server" Text="导出Excel" Visible="false" CssClass="button"
                                    PostBackUrl="~/Common/ExportExcel.aspx" OnClientClick="exportExcel();" />
                                <input id="Button2" type="button" value="新  增" class="button" onclick="addPlan()" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                计划名称：</td>
                            <td>
                                <asp:TextBox ID="txtTrainPlanName" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblOrg" runat="server" Text="主办单位："></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlOrg" runat="server" >
                                </asp:DropDownList>
                            </td>
                            <td style="display: none;">
                                阶段：</td>
                            <td style="display: none;">
                                <asp:DropDownList ID="ddlTrainPlanPhase" runat="server" Width="150px">
                                    <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="新增" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="实施" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="已完成" Value="3"></asp:ListItem>
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
                        DataKeyNames="TRAIN_PLAN_ID" OnRowDataBoundDataRow="grdEntity_RowDataBoundDataRow"
                        DataSourceID="ObjectDataSource1">
                        <Columns>
                            <asp:TemplateField HeaderText="操作">
                                <headertemplate>
                            操作</headertemplate>
                                <itemstyle width="90px" horizontalalign="Left" wrap="false" />
                                <headerstyle width="90px" horizontalalign="Center" wrap="false" />
                                <itemtemplate>
        
                    <asp:HiddenField runat="server" ID="hidOrgID" Value='<%#Eval("orgids")%>'></asp:HiddenField>
                       <asp:HiddenField runat="server" ID="hidUnitID" Value='<%# Eval("undertake_unit_id") %>'></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hidMaiUnitID" Value='<%# Eval("sponsor_unit_id") %>'></asp:HiddenField>
                    
                       <span id="spanView" runat="server">
                       <a href='TrainPlanAdd.aspx?ID=<%#Eval("TRAIN_PLAN_ID")%>&type=view'>
                       <img src="../App_Themes/Default/Images/Menu/view.gif" style="border:0" alt="查看" />
                      </a> 
                      </span>
                        <span id="spanUp" runat="server">
                    <%--  <a href='EmployeeInfo.aspx?ID=<%#Eval("TRAIN_PLAN_ID")%>'>
                       <img src="../Common/Image/edit_col_edit.gif" style="border:0" alt="学员信息" />
                      </a>--%>
                       <a href='#' onclick="PlanUp(<%#Eval("TRAIN_PLAN_ID")%>)">
                       <img src="../App_Themes/Default/Images/Menu/move_up.gif" style="border:0" alt="上报计划" />
                      </a> 
                      </span>
                        <span id="add" runat="server">
                                            <a href='#' onclick="updatePlan('<%#Eval("TRAIN_PLAN_ID")%>')" >
                                                <img src="../Common/Image/edit_col_edit.gif" style="border:0" alt="修改" /></a> </span>
                                              <asp:LinkButton ID="btnDelete" runat="server" CssClass="underline" CommandName="del"
                                                OnClientClick="return deletePlan()" CommandArgument='<%#Eval("TRAIN_PLAN_ID")%>'>
                                                <img src="../Common/Image/edit_col_delete.gif" style="border:0" alt="删除" /></asp:LinkButton>
                      <%-- <asp:LinkButton runat="server" ID="AddStudent" OnClientClick="return addPlan('## DataItem.getMember('TRAIN_PLAN_ID').get_value() ##')"> <img src="../Common/Image/edit_col_edit.gif" style="border:0" alt="学员信息" /></asp:LinkButton>--%>
                        </itemtemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TRAIN_PLAN_ID" HeaderText="TRAIN_PLAN_ID" Visible="False"
                                ReadOnly="True" SortExpression="TRAIN_PLAN_ID" />
                            <asp:BoundField DataField="YEAR" HeaderText="年度" ReadOnly="True" SortExpression="YEAR">
                                <itemstyle wrap="false" />
                                <headerstyle wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TRAIN_PLAN_NAME" HeaderText="计划名称" ReadOnly="True" SortExpression="TRAIN_PLAN_NAME" />
                            <asp:BoundField DataField="LOCATION" HeaderText="培训地点" ReadOnly="True" SortExpression="LOCATION" />
                            <asp:BoundField DataField="MEMO" HeaderText="备注" ReadOnly="True" SortExpression="MEMO"
                                Visible="false" />
                            <asp:BoundField DataField="TRAINPLAN_TYPE_NAME" HeaderText="培训计划类别" ReadOnly="True"
                                SortExpression="TRAINPLAN_TYPE_NAME">
                                <itemstyle wrap="false" />
                                <headerstyle wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SPONSOR_UNIT_NAME" HeaderText="主办单位" ReadOnly="True" SortExpression="SPONSOR_UNIT_NAME">
                                <itemstyle wrap="false" />
                                <headerstyle wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UNDERTAKE_UNIT_NAME" HeaderText="承办单位" ReadOnly="True"
                                SortExpression="UNDERTAKE_UNIT_NAME">
                                <itemstyle wrap="false" />
                                <headerstyle wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BEGINDATE_1" HeaderText="开班日期" ReadOnly="True" SortExpression="BEGINDATE">
                                <itemstyle wrap="false" />
                                <headerstyle wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ENDDATE_1" HeaderText="结束日期" ReadOnly="True" SortExpression="ENDDATE">
                                <itemstyle wrap="false" />
                                <headerstyle wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="制定人" ReadOnly="True" SortExpression="EMPLOYEE_NAME">
                                <itemstyle wrap="false" />
                                <headerstyle wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MAKEDATE_1" HeaderText="制定日期" ReadOnly="True" SortExpression="MAKEDATE">
                                <itemstyle wrap="false" />
                                <headerstyle wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TRAIN_PLAN_PHASE_NAME" HeaderText="计划阶段" ReadOnly="True"
                                Visible="false" SortExpression="TRAIN_PLAN_PHASE_NAME" />
                            <asp:BoundField DataField="ASSIST_UNIT" HeaderText="协办单位" ReadOnly="True" Visible="false"
                                SortExpression="ASSIST_UNIT" />
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
