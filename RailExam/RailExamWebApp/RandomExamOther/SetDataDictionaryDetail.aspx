<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetDataDictionaryDetail.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.SetDataDictionaryDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>数据字典详细</title>
    <script type="text/javascript">
        function deleteBtnClientClick()
        {
            return confirm("您确定要删除此记录吗？");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                       数据字典详细信息</div>
                </div>
            </div>
            <div id="content">
                 <table class="contentTable">
                            <tr>
                                <td style="width: 3%">数据项</td>
                                <td style="width: 8%">
                                    <asp:TextBox ID="txtItemName" runat="server" Width="60%" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="“数据项”不能为空！"
                                        ToolTip="“数据项”不能为空！" ControlToValidate="txtItemName" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                    </tr>
                            <tr id="trType" runat="server">
                                <td>数据项类别</td>
                                <td  >
                                    <asp:DropDownList ID="ddlType" runat="server">
                                        <asp:ListItem Value="1" Text="初级"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="中级"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="高级"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="其它"></asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr id="trMemo" runat="server">
                                <td>备注</td>
                                <td  >
                                    <asp:TextBox ID="txtMemo" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="InsertButton" runat="server" 
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;' OnClick="InsertButton_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
