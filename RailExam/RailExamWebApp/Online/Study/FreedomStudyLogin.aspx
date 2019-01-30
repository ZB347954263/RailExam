<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FreedomStudyLogin.aspx.cs"
    Inherits="RailExamWebApp.Online.Study.FreedomStudyLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考生登录</title>

    <script type="text/javascript" src="../../Common/JS/Common.js"></script>

    <script type="text/javascript">
       function ShowStudy()
      {
        var search = window.location.search;
        var str1 = search.substring(search.indexOf('&')+1);
        var str =  str1.substring(search.indexOf('=')+1);
        if(str == "student")
        {
            var ret = showCommonDialog("../Online/Study/StudyAtLiberty.aspx?type=student",'dialogWidth:'+window.screen.width+'px;dialogHeight:'+window.screen.height+'px;');
            if(ret == "true" )
            {
                
            }
        }  
        else
        {
            var ret = showCommonDialog("../Online/Study/StudyAtLiberty.aspx?type=teacher",'dialogWidth:'+window.screen.width+'px;dialogHeight:'+window.screen.height+'px;');
            if(ret == "true" )
            {

            }
         }
      }
    </script>

</head>
<body >
    <form id="form1" runat="server">
        <br />
        <div style="text-align: center; color: #2D67CF;">
            <table cellspacing="10" cellpadding="1" border="0">
                <tr>
                    <td nowrap align="right" valign="middle" style="width: 30%">
                        用户名：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblEmployeeName" runat="server" Visible="False" CssClass="LoginLabel"></asp:Label>
                        <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" Columns="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td nowrap align="right">
                        <asp:Label id="lblPassword" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPassword" TabIndex="3" runat="server" TextMode="Password" Columns="21"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="“密码”不能为空！"
                            ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnStudy" runat="server" OnClientClick="ShowStudy()" Text="进入学习"
                            CssClass="button" Visible="false" />
                        <asp:Button ID="ImageButtonLogin" runat="server" CssClass="button" Text="登   录" OnClick="ImageButtonLogin_Click">
                        </asp:Button>
                        <asp:Button ID="btnExit" runat="server" CssClass="button" Text="退    出" Visible="false"
                            OnClick="btnExit_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowSummary="False"
            ShowMessageBox="True" DisplayMode="List"></asp:ValidationSummary>
    </form>
</body>
</html>
