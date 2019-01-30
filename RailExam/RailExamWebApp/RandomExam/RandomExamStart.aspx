<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamStart.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamStart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>开始考试</title>
    <base target="_self"/>
   <script type="text/javascript">
   	function confirmRandom() {
   		if(document.getElementById("txtCode").value == "") {
   			alert("验证码不能为空！");
   			return false;
   		}

   		return true;
   	}
   </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table>
                <tr>
                    <td>
                        <font style="color: #2D67CF;">考试验证码：</font><asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtCode"
                            Display="None" ErrorMessage="验证码必须为1000～9999之间的任意整数！" MaximumValue="9999" MinimumValue="1000"
                            Type="Integer"></asp:RangeValidator>
                        <asp:Button ID="btnRandom" CausesValidation="false" Text="随机生成" CssClass="button"  runat="server" OnClick="btnRandom_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnOK" Text="确   定" CssClass="button" runat="server" OnClientClick="return confirmRandom();"  OnClick="btnOK_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
