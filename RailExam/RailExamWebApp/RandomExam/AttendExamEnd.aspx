<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AttendExamEnd.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.AttendExamEnd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="text-align: center">
                <br />
                <br />
                <span style="font-family: 宋体; color: Red; font-size: 20pt">正在保存答卷并计算成绩<br />
                    请耐心等待！</span>
                <br />
                <asp:Image ID="img" runat="server" ImageUrl="../Common/Image/Progress.gif" />
               <br />
               <asp:Button ID="btnUpload" runat="server" Text="重新提交"  CssClass="button" OnClick="btnUpload_Click"  Visible="false"/> 
               &nbsp;<asp:Button ID="btnClose" runat="server" Text="关闭考试页面"  CssClass="buttonLong"  OnClientClick="return window.close();"  Visible="false"/> 
            </div>
        </div>
        <asp:HiddenField ID="hfAnswer" runat="server" />
    </form>
</body>
</html>

<script type="text/javascript">
        if(document.getElementById('hfAnswer').value == "")
        {
             document.getElementById('hfAnswer').value = window.dialogArguments.document.getElementById('hfReturnAnswer').value;
             form1.submit();
        }
</script>

