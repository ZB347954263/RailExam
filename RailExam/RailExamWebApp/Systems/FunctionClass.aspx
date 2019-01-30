<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunctionClass.aspx.cs" Inherits="RailExamWebApp.Systems.FunctionClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>角色权限定义</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        配置部分权限</div>
                </div>
            </div>
            <div id="content">
                <div style="overflow: auto; height: 495px; width: 600px;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsRoleRight"
                        DataKeyNames="RoleID" Width="584px" GridLines="None" CellPadding="4" ForeColor="#333333">
                        <Columns>
                            <asp:BoundField DataField="FunctionID" Visible="False" HeaderText="功能ID"/>
                            <asp:TemplateField HeaderText="功能名称">
                                <ItemTemplate>
                                    <asp:Label ID="lblFunctionName" runat="server" Text='<%# Eval("FunctionName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FunctionRight" Visible="false" HeaderText="角色权限"/>
                            <asp:TemplateField HeaderText="范围权限">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="rblRangeRight" runat="server" RepeatDirection="Horizontal"
                                        SelectedIndex='<%# Eval("RangeRight") %>'>
                                        <asp:ListItem Value="0" Selected="True">无</asp:ListItem>
			                            <asp:ListItem Value="1">本部门</asp:ListItem>
			                            <asp:ListItem Value="2">本站段</asp:ListItem>
			                            <asp:ListItem Value="3">所有站段</asp:ListItem>
		                            </asp:RadioButtonList>
		                            <asp:Label ID="lblFunctionRight" Visible="false" runat="server" Text='<%# Bind("FunctionRight") %>'></asp:Label>
		                            <asp:Label ID="lblFunctionID" Visible="false" runat="server" Text='<%# Bind("FunctionID") %>'></asp:Label>
	                            </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsRoleRight" runat="server" SelectMethod="GetRoleRightsClass"
                        TypeName="RailExam.BLL.SystemRoleRightBLL">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="roleID" QueryStringField="id" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div>
                    <br />
                    <asp:ImageButton ID="btnSave" runat="server" CausesValidation="False" OnClick="btnSave_Click"
                        ImageUrl="../Common/Image/save.gif" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
