<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default3.aspx.cs" Inherits="RailExamWebApp.Default3" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<head runat="server">
    <title>�人��·������ѧϰ���Թ���ϵͳ</title>
    <LINK href="Common/CSS/Login.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
        function login()
        {
			//window.open('Main/Main.aspx','Main','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no')
        }
    </script>
</head>
<body onload="login()">
    <form id="form1" runat="server">
        <div id="loginOuter">
            <div id="loginMiddle">
                <div id="loginInner">
                    <div id="loginInfo">
                        <span>
                            <asp:Label ID="lblUserName" runat="server" CssClass="label">�û�����</asp:Label>
                            <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ErrorMessage="���û���������Ϊ�գ�"
                                ControlToValidate="txtUserName" Display="None"></asp:RequiredFieldValidator>
                        </span>
                        <span>
                            <asp:Label ID="lblPassword" runat="server" CssClass="label">��&nbsp;&nbsp;�룺</asp:Label>
                            <asp:TextBox ID="txtPassword" TabIndex="2" runat="server" CssClass="textbox"
                                TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="�����롱����Ϊ�գ�"
                                ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
                        </span>
                        <span>
                            <asp:ImageButton ID="ImageButtonLogin" runat="server" ImageUrl="Common/Image/login_btn.gif"
                                OnClick="ImageButtonLogin_Click"></asp:ImageButton>
                        </span> 
                    </div>
                </div>
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowSummary="False"
            ShowMessageBox="True" DisplayMode="List"></asp:ValidationSummary>
        <input type="hidden" name="logout">
    </form>
</body>
</html>
