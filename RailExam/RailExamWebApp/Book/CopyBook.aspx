<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CopyBook.aspx.cs" Inherits="RailExamWebApp.Book.CopyBook" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>复制模版</title>

    <script type="text/javascript">
  function ConfirmDel()
  {
     return confirm("确认要整理教材吗？");
  }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <table style="height: 98%;">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnCopy" runat="server" CssClass="buttonEnableLong" Text="复制普通教材模版"
                            OnClick="btnCopy_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCopyHtm" runat="server" CssClass="buttonLong" Text="复制网页代码" OnClick="btnCopyHtm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDel" runat="server" CssClass="buttonLong" OnClientClick="return ConfirmDel();"
                            Text="整理普通教材" ToolTip="清理无效的电子教材" OnClick="btnDel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSend" runat="server" CssClass="buttonLong" Text="发布普通教材" OnClick="btnSend_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCopyAssist" runat="server" CssClass="buttonEnableLong" Text="复制辅导教材模版"
                            OnClick="btnCopyAssist_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelAssist" runat="server" OnClientClick="return ConfirmDel();"
                            CssClass="buttonLong" Text="整理辅导教材" ToolTip="清理无效的电子辅导教材" OnClick="btnDelAssist_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnItem" runat="server" CssClass="button" Text="整理试题" OnClick="btnItem_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUpper" runat="server" CssClass="buttonEnableLong" Text="生成姓名拼音码"
                            OnClick="btnUpper_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSelectName" runat="server" CssClass="buttonEnableLong" Text="查询错误姓名"
                            OnClick="btnSelectName_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUpdateName" runat="server" CssClass="buttonEnableLong" Text="更新错误姓名"
                            OnClick="btnUpdateName_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
