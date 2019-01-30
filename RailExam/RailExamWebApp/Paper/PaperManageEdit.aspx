<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperManageEdit.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperManageEdit" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试卷基本信息</title>
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
                        试卷管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        基本信息</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="4">
                            <b>第一步：试卷基本信息</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            试卷类别<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCategoryName" runat="server" Width="90%" ReadOnly="true">
                            </asp:TextBox>
                            <img style="cursor: hand;" onclick="selectPaperCategory();" src="../Common/Image/search.gif"
                                alt="选择试卷类别" border="0" />
                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“试卷类别”不能为空！" ControlToValidate="txtCategoryName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            出题方式</td>
                        <td>
                            <asp:DropDownList ID="ddlMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="手工出题"></asp:ListItem>
                                <asp:ListItem Value="2" Text="随机出题（按照现有组卷策略）"></asp:ListItem>
                                <asp:ListItem Value="3" Text="随机出题（新定义组卷策略）"></asp:ListItem>
                            </asp:DropDownList>&nbsp;
                            <asp:Label ID="LabelMode" runat="server" Text="策略模式" Visible="false"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlStrategyMode" runat="server" Visible="false">
                                <asp:ListItem Text="按教材章节" Value="2"></asp:ListItem>
                                <asp:ListItem Text="按试题辅助分类" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            试卷名称<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtPaperName" runat="server" MaxLength="50" Width="500px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPaperName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“试卷名称”不能为空！" ControlToValidate="txtPaperName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            描述</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="500px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注</td>
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
