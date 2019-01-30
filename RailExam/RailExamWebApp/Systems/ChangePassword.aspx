<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ChangePassword.aspx.cs" Inherits="RailExamWebApp.Systems.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�޸�����</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        ϵͳ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �޸�����</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:ImageButton ID="btnSave" runat="server" CausesValidation="true" OnClick="btnSave_Click" ImageUrl="../Common/Image/confirm.gif" />
                </div>
            </div>
            <div id="content" style="width: 90%">
                <table class="contentTable">
	                <tr>
		                <td align="left" width="4%" style="font-size: 12px">�û�����</td>
                        <td align="left" width="8%" style="font-size: 12px">
                            <asp:Label ID="lblUserID" runat="server" Width="50%" CssClass="label"></asp:Label>
                        </td>
                    </tr>
	                <tr>
		                <td align="left" width="4%" style="font-size: 12px">�����룺</td>
		                <td align="left" width="8%">
                            <asp:TextBox ID="txtOldPassword" runat="server" Width="70%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="�������롱����Ϊ�գ�"
                                ToolTip="�������롱����Ϊ�գ�" ControlToValidate="txtOldPassword" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" width="4%" style="font-size: 12px">�����룺</td>
                        <td align="left" width="8%">
                            <asp:TextBox ID="txtNewPassword" runat="server" Width="70%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="�������롱����Ϊ�գ�"
                                ToolTip="�������롱����Ϊ�գ�" ControlToValidate="txtNewPassword" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" width="4%" style="font-size: 12px">ȷ�������룺</td>
                        <td align="left" width="8%">
                            <asp:TextBox ID="txtPassword" runat="server" Width="70%" TextMode="Password" CssClass="textbox"
                                MaxLength="18"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="��ȷ�������롱����Ϊ�գ�"
                                ToolTip="��ȷ�������롱����Ϊ�գ�" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator" runat="server" ErrorMessage="����������벻ƥ�䣡"
                                ToolTip="����������벻ƥ�䣡" ControlToValidate="txtPassword" ControlToCompare="txtNewPassword" Display="Dynamic"></asp:CompareValidator></td>
                    </tr>
                </table>
	        </div>
        </div>
    </form>
</body>
</html>
