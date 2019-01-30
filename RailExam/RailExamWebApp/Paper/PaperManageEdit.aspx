<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperManageEdit.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperManageEdit" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�Ծ������Ϣ</title>
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
                
                document.getElementById("hfCategoryId").value = selectedPaperCategory.split('|')[0];
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
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        �Ծ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ������Ϣ</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="4">
                            <b>��һ�����Ծ������Ϣ</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            �Ծ����<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCategoryName" runat="server" Width="90%" ReadOnly="true">
                            </asp:TextBox>
                            <img style="cursor: hand;" onclick="selectPaperCategory();" src="../Common/Image/search.gif"
                                alt="ѡ���Ծ����" border="0" />
                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="���Ծ���𡱲���Ϊ�գ�" ControlToValidate="txtCategoryName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            ���ⷽʽ</td>
                        <td>
                            <asp:DropDownList ID="ddlMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="�ֹ�����"></asp:ListItem>
                                <asp:ListItem Value="2" Text="������⣨�������������ԣ�"></asp:ListItem>
                                <asp:ListItem Value="3" Text="������⣨�¶��������ԣ�"></asp:ListItem>
                            </asp:DropDownList>&nbsp;
                            <asp:Label ID="LabelMode" runat="server" Text="����ģʽ" Visible="false"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlStrategyMode" runat="server" Visible="false">
                                <asp:ListItem Text="���̲��½�" Value="2"></asp:ListItem>
                                <asp:ListItem Text="�����⸨������" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �Ծ�����<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtPaperName" runat="server" MaxLength="50" Width="500px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPaperName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="���Ծ����ơ�����Ϊ�գ�" ControlToValidate="txtPaperName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ����</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="500px">
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
                </table>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnSaveAndNext" runat="server" OnClick="btnSaveAndNext_Click"
                        ImageUrl="../Common/Image/next.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/cancel.gif" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfCategoryId" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
