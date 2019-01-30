<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Default.aspx.cs" Inherits="RailExamWebApp.Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>
        <%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>在线学习考试管理系统</title>
    <link href="Common/CSS/Login.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <object classid="clsid:D39A5EBE-F907-4EC2-BCDF-A72F58DA01F4" id="eWebEditor" name="eWebEditor"
            style="left: 0px; top: 0px" width="0" height="0" viewastext codebase="eWebEditor.CAB#version=1,1,0,1"
            type="application/x-oleobject">
        </object>
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td>
                    <table id="loginArea" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="right">
                                <div id="loginInfo">
                                    <table cellspacing="20" cellpadding="1" align="center" border="0">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblUserName" runat="server" CssClass="label">用户名：</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" CssClass="textbox" Width="163px"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ErrorMessage="“用户名”不能为空！"
                                                    ControlToValidate="txtUserName" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblPassword" runat="server" CssClass="label">密&nbsp;&nbsp;码：</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtPassword" TabIndex="2" runat="server" CssClass="textbox" TextMode="Password"
                                                    Width="163px"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="“密码”不能为空！"
                                                    ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:ImageButton ID="ImageButtonLogin" runat="server" ImageUrl="Common/Image/login_btn.gif"
                                                    OnClick="ImageButtonLogin_Click"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowSummary="False"
            ShowMessageBox="True" DisplayMode="List"></asp:ValidationSummary>
        <input type="hidden" name="logout">
    </form>
</body>
</html>
