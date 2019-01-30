<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ChangePassword.aspx.cs" Inherits="RailExamWebApp.Online.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�޸�����</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        �޸�����
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
				        <td align="left" width="4%" style="font-size: 12px">�û���</td>
                        <td align="left" width="8%">
                            <asp:Label ID="lblUserID" runat="server" Width="60%" CssClass="label"></asp:Label>
                        </td>
                    </tr>
			        <tr>
				        <td align="left" width="4%" style="font-size: 12px">�����룺</td>
				        <td align="left" width="8%">
                            <asp:TextBox ID="txtOldPassword" runat="server" Width="60%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="�������롱����Ϊ�գ�"
                                ToolTip="�������롱����Ϊ�գ�" ControlToValidate="txtOldPassword"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" width="4%" style="font-size: 12px">�����룺</td>
                        <td align="left" width="8%">
                            <asp:TextBox ID="txtNewPassword" runat="server" Width="60%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="�������롱����Ϊ�գ�"
                                ToolTip="�������롱����Ϊ�գ�" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" width="4%" style="font-size: 12px">ȷ�������룺</td>
                        <td align="left" width="8%">
                            <asp:TextBox ID="txtPassword" runat="server" Width="60%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="��ȷ�������롱����Ϊ�գ�"
                                ToolTip="��ȷ�������롱����Ϊ�գ�" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator" runat="server" ErrorMessage="����������벻ƥ�䣡"
                                ToolTip="����������벻ƥ�䣡" ControlToValidate="txtPassword" ControlToCompare="txtNewPassword"></asp:CompareValidator></td>
                    </tr>
		        </table>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </div>
    </form>
</body>
</html>
