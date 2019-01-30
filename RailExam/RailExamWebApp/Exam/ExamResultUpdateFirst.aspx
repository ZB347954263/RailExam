<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExamResultUpdateFirst.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamResultUpdateFirst" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成绩修改记录</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        手工评卷</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        成绩修改记录</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="text-align: center; width: 15%">
                            修改人
                        </td>
                        <td style="text-align: left; width: 35%">
                            <asp:Label ID="LabelWorkMan" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: center; width: 15%">
                            修改时间
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="LabelTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            考试名称
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="LabelExamName" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            考生姓名
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblExamineeName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            考生原始分数
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="LabelOldScore" runat="server"></asp:Label>
                        </td>
                        <td style="text-align: center">
                            修改改后分数
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="LabelNewScore" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            修改原因
                        </td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox runat="server" ID="TextBoxCause" TextMode="MultiLine" Height="60px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“修改原因”不能为空！" ControlToValidate="TextBoxCause">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            备注
                        </td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox runat="server" ID="TextBoxRemark" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <br /> <br />
                <asp:ImageButton runat="server" ID="btnsave" ImageUrl="~/Common/Image/save.gif" OnClick="btnsave_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                    ImageUrl="../Common/Image/close.gif" />
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
