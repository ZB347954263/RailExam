<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InformationChapterInfo.aspx.cs"
    Inherits="RailExamWebApp.AssistBook.InformationChapterInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="contentTable">
                <tr>
                    <td style="width: 15%;">
                        章节名称
                    </td>
                    <td colspan="3">
                        <asp:Label ID="TypeNameLabel" runat="server"></asp:Label></td>
                </tr>
                <tr style="height: 30;">
                    <td>
                        网址</td>
                    <td align="left" colspan="3">
                        <asp:HyperLink ID="hUrl" runat="server" Target="_blank"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%">
                        描述
                    </td>
                    <td colspan="3" style="width: 35%">
                        <asp:TextBox ID="DescriptionTextBox" Rows="3" TextMode="MultiLine" Enabled="false"
                            Columns="43" runat="server" >
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        最后更新人</td>
                    <td colspan="3">
                        <asp:Label ID="lblLastPerson" runat="server" >
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        最后更新时间</td>
                    <td colspan="3">
                        <asp:Label ID="lblLastDate" runat="server" >
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        备注
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="MemoTextBox11" Enabled="false" TextMode="MultiLine" Rows="3" Columns="43"
                            runat="server" >
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
