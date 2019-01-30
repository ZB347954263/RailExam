<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PaperStrategyEdit.aspx.cs" Inherits="RailExamWebApp.Paper.PaperStrategyEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组卷策略基本信息</title>
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
                        组卷策略</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        基本信息</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="3">
                            <b>第一步：组卷策略基本信息</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8%">试卷类别<span class="require">*</span></td>
                        <td style="width: 30%">
                            <asp:TextBox ID="txtCategoryName" runat="server" Width="90%" ReadOnly="true">
                            </asp:TextBox>
                            <img style="cursor:hand;" onclick="selectPaperCategory();" src="../Common/Image/search.gif"
                                alt="选择试卷类别" border="0" />
                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“试卷类别”不能为空！" ControlToValidate="txtCategoryName">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>出题方式：随机出题
                        </td>
                    </tr>
                    <tr>
                        <td>组卷策略名称<span class="require">*</span></td>
                        <td colspan="2">
                            <asp:TextBox ID="txtPaperStrategyName" runat="server"  MaxLength="50" Width="30%">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPaperName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“组卷策略名称”不能为空！" ControlToValidate="txtPaperStrategyName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>策略模式</td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlStrategyMode" runat="server">
                                <asp:ListItem Text="按教材章节" Value="2"></asp:ListItem>
                                <asp:ListItem Text="按试题辅助分类" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style=" display:none">
                        <td>设定</td>
                        <td>
                            <asp:CheckBox ID="chDisplay" runat="server" />是否把单选显示成多选
                        </td>
                        <td>
                            <asp:CheckBox ID="chChoose" runat="server" />是否打乱试题显示顺序
                        </td>
                    </tr>
                    <tr>
                        <td>描述</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescription" runat="server" Width="90%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>备注</td>
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
