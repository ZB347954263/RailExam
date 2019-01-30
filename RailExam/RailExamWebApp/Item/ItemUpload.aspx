<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ItemUpload.aspx.cs" Inherits="RailExamWebApp.Item.ItemUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传文件</title>
    <base target="_self" />

    <script type="text/javascript">
        function inputConfirm()
        {
            var filePath = escape(document.getElementById("File1").value);
            if( filePath== "")
            {
                alert("请浏览选择Excel文件！");
               return false; 
            }
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; color: #2D67CF;">
            <table>
                <tr>
                    <td align="right">
                        Excel文件
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="File1" runat="server" Width="230px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnInput" runat="server" CssClass="button" Text="上传导入" OnClientClick="return inputConfirm();"
                            OnClick="btnInput_Click" />&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
