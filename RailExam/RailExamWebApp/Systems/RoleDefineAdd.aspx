<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleDefineAdd.aspx.cs" Inherits="RailExamWebApp.Systems.RoleDefineAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>角色权限管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        角色详细信息</div>
                </div>
            </div>
            <div id="content">
                   <table class="contentTable">
                            <tr>
                                <td style="width: 3%">角色名称</td>
                                <td style="width: 8%">
                                    <asp:TextBox ID="txtRoleNameInsert" runat="server" Width="60%" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="“角色名称”不能为空！"
                                        ToolTip="“角色名称”不能为空！" ControlToValidate="txtRoleNameInsert" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                    </tr>
                           <tr>
                                <td style="width: 5%">是否系统管理员</td>
                                <td style="width: 8%">
                                    <asp:CheckBox ID="chIsAdminInsert" runat="server" ></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">适用系统</td>
                                <td style="width: 8%">
                                    <asp:DropDownList ID="ddlRailSystem" runat="server"  >
                                    </asp:DropDownList>                                
                                </td>
                            </tr>
                           <tr>
                                <td>描述</td>
                                <td  >
                                    <asp:TextBox ID="txtDescriptionInsert" runat="server" Width="98%" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td  >
                                    <asp:TextBox ID="txtMemoInsert" runat="server" Width="98%" TextMode="MultiLine" ></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" OnClick="InsertButton_Click"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
            </div>
    </div>
    </form>
</body>
</html>
