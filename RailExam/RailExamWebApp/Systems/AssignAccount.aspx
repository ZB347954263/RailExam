<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="AssignAccount.aspx.cs" Inherits="RailExamWebApp.Systems.AssignAccount" %>
<%@ Import namespace="RailExamWebApp.Common.Class"%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>分配帐户</title>
    <script type="text/javascript">
        function deleteBtnClientClick()
        {
            if(document.getElementById('FormView1_hfUserID').value == form1.NowUserID.value)
            {
                      alert('登录用户不能删除本人信息！');
                      return false; 
            }   
            
            if(document.getElementById('FormView1_hfEmployeeID').value == "1" || document.getElementById('FormView1_hfEmployeeID').value == "2")
			{
				alert("该登录用户为最高权限用户，不能被删除！");
				return false;
			}
            return confirm("您确定要删除此记录吗？");
        }
    </script>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        分配帐户
                    </div>
                </div>
            </div>
            <div id="content">
                <asp:FormView ID="FormView1" runat="server" DataSourceID="odsAssignAccount" DataKeyNames="EmployeeID,UserID"
                    OnItemUpdated="FormView1_ItemUpdated" OnItemDeleted="FormView1_ItemDeleted" OnDataBound="FormView1_DataBound"
                    OnItemInserted="FormView1_ItemInserted" >
                    <EditItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 3%;">登录帐户</td>
                                <td  >
                                    <asp:TextBox ID="txtUserIDEdit" runat="server" Width="80%" Text='<%# Bind("UserID") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="“登录帐户”不能为空！"
                                        ToolTip="“登录帐户”不能为空！" ControlToValidate="txtUserIDEdit" Display="None"></asp:RequiredFieldValidator>
                                </td>
                                
                                  </tr>
                            <tr>
                                <td style="width: 3%;">密码</td>
                                <td  >
                                    <asp:TextBox ID="txtPasswordEdit" runat="server" Width="50%" TextMode="Password" Text='<%# Bind("Password") %>'></asp:TextBox>
                                    <input type="hidden" name="passwordChanged" />
									<script type="text/javascript">
										var pwdBox = document.getElementById('<%= ((TextBox)FormView1.FindControl("txtPasswordEdit")).ClientID %>');
										pwdBox.onchange = function pwdChanged(){document.getElementById("passwordChanged").value = "true";};
									</script>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="“密码”不能为空！"
                                        ToolTip="“密码”不能为空！" ControlToValidate="txtPasswordEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>角色</td>
                                <td>
                                    <asp:DropDownList ID="ddlRoleNameEdit" runat="server" DataSourceID="odsDdlRoleName"
                                        DataTextField="RoleName" DataValueField="RoleID" SelectedValue='<%# Bind("RoleID") %>'>
                                    </asp:DropDownList>
                                </td>
                               
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td  >
                                    <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:HiddenField ID="hfEmployeeID" runat="server" Value='<%# Bind("EmployeeID") %>'/>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 3%;">登录帐户</td>
                                <td  >
                                    <asp:TextBox ID="txtUserIDInsert" runat="server" Width="80%" Text='<%# Bind("UserID") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="“登录帐户”不能为空！"
                                        ToolTip="“登录帐户”不能为空！" ControlToValidate="txtUserIDInsert" Display="None"></asp:RequiredFieldValidator>
                                </td>
                                   </tr>
                            <tr>
                                <td style="width: 3%;">密码</td>
                                <td  >
                                    <asp:TextBox ID="txtPasswordInsert" runat="server" Width="50%" TextMode="Password" Text='<%# Bind("Password") %>'></asp:TextBox>
                                    <input type="hidden" name="passwordChanged" />
									<script type="text/javascript">
										var pwdBox = document.getElementById('<%= ((TextBox)FormView1.FindControl("txtPasswordInsert")).ClientID %>');
										pwdBox.onchange = function pwdChanged(){document.getElementById("passwordChanged").value = "true";};
									</script>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="“密码”不能为空！"
                                        ToolTip="“密码”不能为空！" ControlToValidate="txtPasswordInsert" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>角色</td>
                                <td>
                                    <asp:DropDownList ID="ddlRoleNameInsert" runat="server" DataSourceID="odsDdlRoleName" 
                                        DataTextField="RoleName" DataValueField="RoleID" SelectedValue='<%# Bind("RoleID") %>'>
                                    </asp:DropDownList>
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
                        <asp:HiddenField ID="hfEmployeeID" runat="server" Value='<%# Bind("EmployeeID") %>'/>
                    </InsertItemTemplate>
                    <EmptyDataTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 3%;">登录帐户</td>
                                <td  >
                                    <asp:Label ID="lblUserIDEmpty" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                                </td>
                                   </tr>
                            <tr>
                                <td style="width: 3%;">密码</td>
                                <td  >
                                    <asp:Label ID="lblPasswordEmpty" runat="server" Text='<%# Eval("Password") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>角色</td>
                                <td>
                                    <asp:Label ID="lblRoleNameEmpty" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td  >
                                    <asp:Label ID="lblMemoEmpty" runat="server" Text='<%# Eval("Memo") %>'></asp:Label></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 3%;">登录帐户</td>
                                <td  >
                                    <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                                   <asp:HiddenField ID="hfUserID" runat="server"  Value='<%# Eval("UserID") %>'/> 
                                   <asp:HiddenField ID="hfEmployeeID" runat="server" Value='<%# Bind("EmployeeID") %>'/>
                                </td>
                                  </tr>
                            <tr>
                                <td style="width: 3%;">密码</td>
                                <td  >
                                    <asp:Label ID="lblPassword" runat="server" Text='******'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>角色</td>
                                <td>
                                    <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td  >
                                    <asp:Label ID="lblMemo" runat="server" Text='<%# Eval("Memo") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="True" CommandName="Edit"
                            Text='&lt;img border=0 src="../Common/Image/edit.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                            Text='&lt;img border=0 src="../Common/Image/delete.gif" alt="" /&gt;' OnClientClick="return deleteBtnClientClick();">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="odsAssignAccount" runat="server" DataObjectTypeName="RailExam.Model.SystemUser"
                    DeleteMethod="DeleteUser" InsertMethod="AddUser" SelectMethod="GetUserByEmployeeID"
                    TypeName="RailExam.BLL.SystemUserBLL" UpdateMethod="UpdateUser"  OnInserting="odsAssignAccount_Inserting" OnUpdating="odsAssignAccount_Updating">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="employeeID" QueryStringField="id" Type="Int32"/>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsDdlRoleName" runat="server" SelectMethod="GetRoles" TypeName="RailExam.BLL.SystemRoleBLL">
                <SelectParameters>
                <asp:ControlParameter ControlID="hfSuitRange" DefaultValue="0" PropertyName="Value" Type="int32" Size="4" Name="suitRange" />
                </SelectParameters> 
                </asp:ObjectDataSource>
            </div>
        </div>
              <input type="hidden" name="NowUserID" value="<%=PrjPub.CurrentLoginUser.UserID%>"/> 
             <asp:HiddenField  ID="hfSuitRange" runat="server"/> 
    </form>
</body>
</html>
