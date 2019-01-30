<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InformationChapterDetail.aspx.cs"
    Inherits="RailExamWebApp.AssistBook.InformationChapterDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资料章节详细信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        资料章节详细信息</div>
                </div>
                <div id="button">
                    <asp:Button ID="SaveButton" runat="server" Text="保  存" CssClass="button" OnClick="SaveButton_Click" />&nbsp;&nbsp;
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 15%;">
                            章节名称
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtName" runat="server" MaxLength="60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            描述
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:TextBox ID="DescriptionTextBox" Rows="3" TextMode="MultiLine"
                                Columns="43" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最后更新人</td>
                        <td colspan="3">
                            <asp:Label ID="lblLastPerson" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最后更新时间</td>
                        <td colspan="3">
                            <asp:Label ID="lblLastDate" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemo"  TextMode="MultiLine" Rows="3" Columns="43"
                                runat="server" >
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
