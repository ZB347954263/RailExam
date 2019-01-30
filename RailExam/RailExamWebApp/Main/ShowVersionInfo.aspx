<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowVersionInfo.aspx.cs" Inherits="RailExamWebApp.Main.ShowVersionInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>更新说明</title>
    <base target="_self"/> 
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-size:10.5pt;">
        <br/>
        <br/>
        <table>
            <tr>
                <td>版本号：<asp:DropDownList runat="server" ID="ddlVersion" AutoPostBack="true" OnSelectedIndexChanged="ddlVersion_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblDate"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left;"><div id="lblVersionInfo" runat="server"></div></td>
            </tr>
            <tr>
                <td><input type="button" onclick="window.close();" class="button" value="确  定"/></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
