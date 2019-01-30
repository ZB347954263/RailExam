<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamResult.aspx.cs" Inherits="RailExamWebApp.Online.Exam.ExamResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�鿴����</title>
    <link href="../style/ExamPaperResult.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="Head">
                    <table width="95%">
                        <tr>
                            <td id='ExamTitle' colspan="6">
                                <%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>�����Ծ�</td>
                        </tr>
                        <tr>
                            <td id='ExamName' colspan="3">
                                �������ƣ�
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
                                ��λ��</td>
                            <td style="width: 23%;" align="left">
                                <asp:Label runat="server" ID="lblOrg"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                ���䣺</td>
                            <td style="width: 24%;" align="left">
                                <asp:Label runat="server" ID="lblWorkShop"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                ְ����</td>
                            <td style="width: 23%;" align="left">
                                <asp:Label runat="server" ID="lblPost"></asp:Label>
                            </td>
                        </tr>
                        <tr id="ExamStudentInfo2">
                            <td style="width: 10%;">
                                ������</td>
                            <td style="width: 23%;" align="left">
                                <asp:Label runat="server" ID="lblName"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                ʱ�䣺</td>
                            <td style="width: 24%;" align="left">
                                <asp:Label runat="server" ID="lblTime"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                �ɼ���</td>
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
                    <input id="btnCloseBottom" type="button" value="��  ��" class="button" onclick="self.close();" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
