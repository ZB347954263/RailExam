<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TaskResult.aspx.cs" Inherits="RailExamWebApp.Task.TaskResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>作业信息 - 作业成绩</title>
    <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        作业信息
                    </div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        作业成绩
                    </div>
                </div>
                <div id="button">
                    <input id="btnCloseTop" class="button" onclick="self.close();" type="button" value="关  闭" />
                </div>
            </div>
            <div id="content">
                <div id="contentHead">
                    <asp:Label runat="server" ID="lblTitle"></asp:Label>
                    <asp:Label runat="server" ID="lblTitleRight"></asp:Label>
                </div>
                <div id="mainContent">
                    <table width="100%" class="contentTable">
                        <tr>
                            <td>
                                <% FillPaper(); %>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <input id="btnCloseBottom" type="button" value="关  闭" class="button" onclick="self.close();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
