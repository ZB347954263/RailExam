<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemExam.aspx.cs" Inherits="RailExamWebApp.Systems.SystemExam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        考场规则</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%; vertical-align: middle;">考场规则</td>
                        <td  style="width: 95%; ">
                            <asp:TextBox runat="server" ID="txtMesage" Width="98%" TextMode="MultiLine" Height="400px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%;">模拟考试时间</td>
                        <td  style="width: 95%; ">
                            <asp:TextBox runat="server" ID="txtTime" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5%;">模拟考试题数</td>
                        <td  style="width: 95%; ">
                            <asp:TextBox runat="server" ID="txtNumber" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;"><asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="button" Text="保  存"/></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
