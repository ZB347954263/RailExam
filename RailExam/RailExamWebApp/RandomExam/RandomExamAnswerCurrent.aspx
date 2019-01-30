<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RandomExamAnswerCurrent.aspx.cs" Inherits="RailExamWebApp.RandomExam.RandomExamAnswerCurrent" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试成绩</title>
    <link href="../Online/style/ExamPaperResult.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        随机考试
                    </div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        考试成绩
                    </div>
                </div>
            </div>
            <div id="content">
                <div id="Div1">
                    <table width="95%">
                        <tr>
                            <td id='ExamTitle' colspan="6">
                                <%=RailExamWebApp.Common.Class.PrjPub.GetRailNameBao()%>职工培训考试试卷</td>
                        </tr>
                        <tr>
                            <td id='ExamName' colspan="3">
                                考试名称：
                                <asp:Label runat="server" ID="lblTitle"></asp:Label></td>
                            <td id="ExamInfo" colspan="3">
                                <asp:Label runat="server" ID="lblTitleRight"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="ExamStyle">
                            </td>
                        </tr>
                        <tr align="center" id="ExamStudentInfo1">
                            <td style="width: 10%;">
                                单位：</td>
                            <td style="width: 23%;" align="left">
                                <asp:Label runat="server" ID="lblOrg"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                车间：</td>
                            <td style="width: 24%;" align="left">
                                <asp:Label runat="server" ID="lblWorkShop"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                职名：</td>
                            <td style="width: 23%;" align="left">
                                <asp:Label runat="server" ID="lblPost"></asp:Label>
                            </td>
                        </tr>
                        <tr id="ExamStudentInfo2">
                            <td style="width: 10%;">
                                姓名：</td>
                            <td style="width: 23%;" align="left">
                                <asp:Label runat="server" ID="lblName"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                时间：</td>
                            <td style="width: 24%;" align="left">
                                <asp:Label runat="server" ID="lblTime"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                成绩：</td>
                            <td style="width: 23%;" align="left">
                                <asp:Label runat="server" ID="lblScore"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="ExamStyle">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="mainContent">
                    <% FillPaper(); %>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
