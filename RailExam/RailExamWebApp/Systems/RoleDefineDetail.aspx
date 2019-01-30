<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="RoleDefineDetail.aspx.cs" Inherits="RailExamWebApp.Systems.RoleDefineDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>角色权限管理</title>
    <script type="text/javascript">
        function deleteBtnClientClick()
        {
            return confirm("您确定要删除此记录吗？");
        }
    </script>
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
                <asp:FormView ID="FormView1" runat="server" DataSourceID="odsRoleRightDetail" DataKeyNames="RoleID"
                    OnItemUpdated="FormView1_ItemUpdated" OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted"
                    OnDataBound="FormView1_DataBound">
                    <EditItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 3%">角色名称</td>
                                <td style="width: 8%">
                                    <asp:TextBox ID="txtRoleNameEdit" runat="server" Width="60%" Text='<%# Bind("RoleName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="“角色名称”不能为空！"
                                        ToolTip="“角色名称”不能为空！" ControlToValidate="txtRoleNameEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                    </tr>
                           <tr>
                                <td style="width: 5%">是否系统管理员</td>
                                <td style="width: 8%">
                                    <asp:CheckBox ID="chIsAdminEdit" runat="server" Checked='<%# Bind("IsAdmin") %>'></asp:CheckBox>
                                </td>
                            </tr>
                           <tr>
                                <td style="width: 5%">适用系统</td>
                                <td style="width: 8%">
                                    <asp:HiddenField ID="hfRailSystem" runat="server" Value='<%# Bind("RailSystemID") %>' />
                                    <asp:DropDownList ID="ddlRailSystem" runat="server"  >
                                    </asp:DropDownList>                                
                                </td>
                            </tr> 
                            <tr>
                                <td>描述</td>
                                <td  >
                                    <asp:TextBox ID="txtDescriptionEdit" runat="server" Width="460px" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td  >
                                    <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 3%">角色名称</td>
                                <td style="width: 8%">
                                    <asp:TextBox ID="txtRoleNameInsert" runat="server" Width="60%" Text='<%# Bind("RoleName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="“角色名称”不能为空！"
                                        ToolTip="“角色名称”不能为空！" ControlToValidate="txtRoleNameInsert" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                    </tr>
                           <tr>
                                <td style="width: 5%">是否系统管理员</td>
                                <td style="width: 8%">
                                    <asp:CheckBox ID="chIsAdminInsert" runat="server" Checked='<%# Bind("IsAdmin") %>'></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">适用系统</td>
                                <td style="width: 8%">
                                    <asp:HiddenField ID="hfRailSystem" runat="server" Value='<%# Bind("RailSystemID") %>' />
                                    <asp:DropDownList ID="ddlRailSystem" runat="server"  >
                                    </asp:DropDownList>                                
                                </td>
                            </tr>
                           <tr>
                                <td>描述</td>
                                <td  >
                                    <asp:TextBox ID="txtDescriptionInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td  >
                                    <asp:TextBox ID="txtMemoInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 3%">角色名称</td>
                                <td style="width: 8%">
                                    <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                </td>
                                    </tr>
                           <tr>
                                <td style="width: 5%">是否系统管理员</td>
                                <td style="width: 8%">
                                    <asp:Label ID="lblIsAdmin" runat="server" Text='<%# (bool)Eval("IsAdmin") == true ? "是" : "否" %>'></asp:Label>
                                </td>
                            </tr>
                           <tr>
                                <td style="width: 5%">适用系统</td>
                                <td style="width: 8%">
                                    <asp:Label ID="lblRailSystem" runat="server" Text='<%# Eval("RailSystemName") %>'></asp:Label>
                                </td>
                            </tr> 
                            <tr>
                                <td>描述</td>
                                <td   style="white-space: normal;">
                                    <%# Eval("Description") %></td>
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td   style="white-space: normal;">
                                    <%# Eval("Memo") %></td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="btnOk" runat="server" CausesValidation="False" OnClientClick="return window.close();"
                            Text='<img border=0 src="../Common/Image/confirm.gif" />'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="odsRoleRightDetail" runat="server" DataObjectTypeName="RailExam.Model.SystemRole"
                    DeleteMethod="DeleteRole" InsertMethod="AddRole" SelectMethod="GetRole"
                    TypeName="RailExam.BLL.SystemRoleBLL" UpdateMethod="UpdateRole" EnableViewState="false">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="RoleID" QueryStringField="id" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </form>
</body>
</html>