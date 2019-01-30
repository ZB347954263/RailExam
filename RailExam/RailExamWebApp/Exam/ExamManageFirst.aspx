<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamManageFirst.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamManageFirst" %>

<%@ Register Src="../Common/Control/Date/DateTimeUC.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���Ի�����Ϣ</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function selectExamCategory()
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
        	    var selectedExamCategory = window.showModalDialog('../Common/SelectExamCategory.aspx', 
                    '', 'help:no; status:no; dialogWidth:320px;dialogHeight:600px');

                if(! selectedExamCategory)
                {
                    return;
                }
                
                document.getElementById("hfCategoryId").value = selectedExamCategory.split('|')[0];
                document.getElementById("txtCategoryName").value = selectedExamCategory.split('|')[1];
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        �������</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ������Ϣ</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="2">
                            <b>��һ�������Ի�����Ϣ</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            ���Է���<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCategoryName" runat="server" Width="30%" ReadOnly="true">
                            </asp:TextBox>
                            <img style="cursor: hand;"   onclick="selectExamCategory();" src="../Common/Image/search.gif"
                                alt="ѡ���Է���" border="0" />
                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="�����Է��ࡱ����Ϊ�գ�" ControlToValidate="txtCategoryName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��������<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtExamName" runat="server" MaxLength="50" Width="500px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvExamName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="�������Ծ����ƣ�" ControlToValidate="txtExamName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��Чʱ��<span class="require">*</span></td>
                        <td>
                            <uc1:Date ID="dateBeginTime" runat="server" />
                            ��<uc1:Date ID="dateEndTime" runat="server" />
                            <asp:DropDownList ID="ddlType" runat="server" DataSourceID="odsExamType" DataTextField="TypeName"
                                DataValueField="ExamTypeId" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ����ʱ��<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtExamTime" runat="server" Width="60px"></asp:TextBox>(����)
                            <asp:RequiredFieldValidator ID="rfvExamTime" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="������ʱ�䡱����Ϊ�գ�" ControlToValidate="txtExamTime">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ����ģʽ</td>
                        <td>
                            <asp:RadioButton ID="rbExamMode1" runat="server" Text="�������" Checked="true" GroupName="rdaGN" />
                            <asp:RadioButton ID="rbExamMode2" runat="server" Text="������" GroupName="rdaGN" Visible="false" />
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="Panel1" Visible="false">
                        <%--<tr>
                        <td>��������</td>
                        <td>
                            �ܷ�����100<asp:TextBox ID="txtTotalScore" runat="Server" Width="60px" Text="100"></asp:TextBox>
                            �֣� ���������60<asp:TextBox ID="txtPassScore" runat="Server" Width="60px" Text="60"></asp:TextBox>��</td>
                    </tr>--%>
                        <tr>
                            <td rowspan="5">
                                ѡ��</td>
                            <%--<asp:CheckBox ID="chIssave" runat="server" />�Զ�������--%>
                            <td>
                                <asp:CheckBox ID="chUD" runat="server" />�������Ա�ֶ����ƿ���</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chAutoScore" runat="server" />�Ƿ��Զ�����</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chSeeScore" runat="server" />�������鿴�ɼ�</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chSeeAnswer" runat="server" />�������鿴���ʹ�</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chPublicScore" runat="server" />���Գɼ��������û�����</td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>
                            ����</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="500px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="500px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ������</td>
                        <td>
                            <asp:Label ID="lblCreatePerson" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ����ʱ��</td>
                        <td>
                            <asp:Label ID="lblCreateTime" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnSave" runat="server" OnClick="btnSave_Click" ImageUrl="../Common/Image/next.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/cancel.gif" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfCategoryId" runat="server" />
        <asp:ObjectDataSource ID="odsExamType" runat="server" SelectMethod="GetExamTypes"
            TypeName="RailExam.BLL.ExamTypeBLL"></asp:ObjectDataSource>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
