<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RefreshDataDefault.aspx.cs"
    Inherits="RailExamWebApp.RefreshDataDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title><%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>在线学习考试管理系统</title>
    <link href="Common/CSS/Login.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td style="height: 566px">
                    <table id="loginArea" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="right" >
                                <div id="loginInfo">
                                    <table cellspacing="20" cellpadding="1" align="center" border="0">
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblUserName" runat="server" CssClass="label">&nbsp;&nbsp;&nbsp;&nbsp;请选择当前单位：</asp:Label>
                                                <asp:DropDownList ID="ddlOrg" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:ImageButton ID="ImageButtonLogin" runat="server" ImageUrl="Common/Image/DefultOK.gif"
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
