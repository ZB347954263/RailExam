<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RefreshSelectType.aspx.cs"
    Inherits="RailExamWebApp.Systems.RefreshSelectType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择下载内容</title>
    <base target="_self"/>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            选择下载内容：<asp:DropDownList runat="server" ID="ddlType">
                <asp:ListItem Value="0" Selected="true">考试试卷</asp:ListItem>
                <asp:ListItem Value="1">教材与试题</asp:ListItem>
               <asp:ListItem Value="2">职员基本信息和档案信息</asp:ListItem> 
                <asp:ListItem Value="3">全部内容</asp:ListItem>
            </asp:DropDownList><br/>
            <asp:Button runat="server" ID="btnOK" OnClick="btnOK_Click" CssClass="button" Text="确   定"/>
        </div>
    </form>
</body>
</html>
