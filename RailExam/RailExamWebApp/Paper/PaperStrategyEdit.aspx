<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PaperStrategyEdit.aspx.cs" Inherits="RailExamWebApp.Paper.PaperStrategyEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�����Ի�����Ϣ</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript">
        function selectPaperCategory()
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
        	    var selectedPaperCategory = window.showModalDialog('../Common/SelectPaperCategory.aspx', 
                    '', 'help:no; status:no; dialogWidth:320px;dialogHeight:600px');

                if(! selectedPaperCategory)
                {
                    return;
                }
                
                document.getElementById("hfPaperCategoryID").value = selectedPaperCategory.split('|')[0];
                document.getElementById("txtCategoryName").value = selectedPaperCategory.split('|')[1];
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        ������</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ������Ϣ</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="3">
                            <b>��һ���������Ի�����Ϣ</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8%">�Ծ����<span class="require">*</span></td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCategoryName" runat="server" Width="90%" ReadOnly="true">
                            </asp:TextBox>
                            <img style="cursor:hand;" onclick="selectPaperCategory();" src="../Common/Image/search.gif"
                                alt="ѡ���Ծ����" border="0" />
                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="���Ծ���𡱲���Ϊ�գ�" ControlToValidate="txtCategoryName">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>���ⷽʽ���������
                        </td>
                    </tr>
                    <tr>
                        <td>����������<span class="require">*</span></td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPaperStrategyName" runat="server"  MaxLength="50" Width="30%">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPaperName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="�����������ơ�����Ϊ�գ�" ControlToValidate="txtPaperStrategyName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>����ģʽ</td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlStrategyMode" runat="server">
                                <asp:ListItem Text="���̲��½�" Value="2"></asp:ListItem>
                                <asp:ListItem Text="�����⸨������" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style=" display:none">
                        <td>�趨</td>
                        <td>
                            <asp:CheckBox ID="chDisplay" runat="server" />�Ƿ�ѵ�ѡ��ʾ�ɶ�ѡ
                        </td>
                        <td>
                            <asp:CheckBox ID="chChoose" runat="server" />�Ƿ����������ʾ˳��
                        </td>
                    </tr>
                    <tr>
                        <td>����</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescription" runat="server" Width="90%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="90%">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align:center;">
                    <br />
                    <asp:ImageButton ID="btnSave" runat="server" OnClick="btnSave_Click" ImageUrl="../Common/Image/next.gif" />
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/cancel.gif" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfPaperCategoryID" runat="server" />
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
