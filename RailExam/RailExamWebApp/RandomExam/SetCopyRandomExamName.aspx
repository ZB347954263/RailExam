<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SetCopyRandomExamName.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.SetCopyRandomExamName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>复制考试</title>
    <base target="_self"/>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table>
                <tr>
                    <td>
                        <font style="color: #2D67CF;">考试名称：</font><asp:TextBox ID="txtName" runat="server"  Columns="30"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="考试名称不能为空！" ControlToValidate="txtName" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnOK" Text="确   定" CssClass="button" runat="server" OnClick="btnOK_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
