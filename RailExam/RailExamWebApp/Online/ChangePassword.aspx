<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ChangePassword.aspx.cs" Inherits="RailExamWebApp.Online.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改密码</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        修改密码
                    </div>
                </div>
                <div id="button">
                    <%--<img id="btnSave" onclick="btnSave_Click" src="../Common/Image/confirm.gif"
                        alt="" />--%>
                    <asp:ImageButton ID="btnSave" runat="server" CausesValidation="true" OnClick="btnSave_Click"
                        ImageUrl="../Common/Image/confirm.gif" />
                </div>
            </div>
            <div id="content" style="width: 80%;">
		        <table class="contentTable">
			        <tr>
				        <td align="left" width="4%" style="font-size: 12px">用户：</td>
                        <td align="left" width="8%">
                            <asp:Label ID="lblUserID" runat="server" Width="60%" CssClass="label"></asp:Label>
                        </td>
                    </tr>
			        <tr>
				        <td align="left" width="4%" style="font-size: 12px">旧密码：</td>
				        <td align="left" width="8%">
                            <asp:TextBox ID="txtOldPassword" runat="server" Width="60%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="“旧密码”不能为空！"
                                ToolTip="“旧密码”不能为空！" ControlToValidate="txtOldPassword"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" width="4%" style="font-size: 12px">新密码：</td>
                        <td align="left" width="8%">
                            <asp:TextBox ID="txtNewPassword" runat="server" Width="60%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="“新密码”不能为空！"
                                ToolTip="“新密码”不能为空！" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" width="4%" style="font-size: 12px">确认新密码：</td>
                        <td align="left" width="8%">
                            <asp:TextBox ID="txtPassword" runat="server" Width="60%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="“确认新密码”不能为空！"
                                ToolTip="“确认新密码”不能为空！" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator" runat="server" ErrorMessage="您输入的密码不匹配！"
                                ToolTip="您输入的密码不匹配！" ControlToValidate="txtPassword" ControlToCompare="txtNewPassword"></asp:CompareValidator></td>
                    </tr>
		        </table>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </div>
    </form>
</body>
</html>
