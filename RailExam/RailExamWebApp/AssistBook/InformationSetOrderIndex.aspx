<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationSetOrderIndex.aspx.cs" Inherits="RailExamWebApp.AssistBook.InformationSetOrderIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>设置序号</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table>
                <tr>
                    <td>
                        设置序号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderIndex" runat="server" Columns="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOrderIndex"
                            Display="None" ErrorMessage="序号不能为空！"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtOrderIndex"
                            Display="None" ErrorMessage="序号必须在1～9999之间！" Type="Integer" MaximumValue="9999" MinimumValue="1"></asp:RangeValidator></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnOK" runat="server" CssClass="button" Text="确定" OnClick="btnOK_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>