<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamComputerSeatDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamComputerSeatDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调整机位</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td>
                            姓名</td>
                        <td>
                            <asp:Label ID="lblName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            考试名称</td>
                        <td>
                            <asp:Label ID="lblExam" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            现微机教室</td>
                        <td>
                            <asp:Label ID="lblRoom" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            调整后微机教室</td>
                        <td>
                            <asp:DropDownList ID="ddlRoom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            现机位</td>
                        <td>
                            <asp:Label ID="lblOldSeat" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            调整后机位</td>
                        <td>
                            <asp:DropDownList ID="ddlSeat" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnOK" runat="server" CssClass="button" Text="确  定" OnClientClick="return confirm('您确定要调整该考生本场考试的机位吗？');"
                                OnClick="btnOK_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfOldRoomID"/>
        <asp:HiddenField runat="server" ID="hfEmployeeID"/>
        <asp:HiddenField runat="server" ID="hfExamID"/>
    </form>
</body>
</html>
