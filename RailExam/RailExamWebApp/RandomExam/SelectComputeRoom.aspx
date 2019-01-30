<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectComputeRoom.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.SelectComputeRoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择微机教室</title>
    <base target="_self" />

    <script type="text/javascript">
      function init() 
      {
      	var search = window.dialogArguments;
      	document.getElementById("hfUserId").value = search.form1.ChooseID.value;
      }
    </script>

</head>
<body onload="init()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <table>
                    <tr>
                        <td >
                            站段名称
                        </td>
                        <td >
                            <asp:DropDownList ID="ddlOrg" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            微机教室
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlCompute" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnOK" Text="确   定" CssClass="button" runat="server" OnClick="btnOK_Click" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField ID="hfUserId" runat="server" />
        <asp:HiddenField ID="hfStationSuitRage" runat="server" />
    </form>
</body>
</html>
