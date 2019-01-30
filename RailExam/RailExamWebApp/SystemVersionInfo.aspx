<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemVersionInfo.aspx.cs" Inherits="RailExamWebApp.SystemVersionInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <script type="text/javascript">
        function download()
        {
            var ret = window.open('http://10.72.4.196/RailExamBao/Help/太原铁路局职工教育培训考试系统安装程序.rar','','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
            ret.focus(); 
        }
     </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div style="color: #2D67CF; text-align: center; font-size: 14px;">
            <br />
            <br />
            路局系统版本号为：<asp:Label ID="lblVersionCenter" runat="server"></asp:Label><br />
            当前系统版本号为：<asp:Label ID="lblVersion" runat="server"></asp:Label>
            <br />
            <br />
            最新补丁已在路局考试系统中发布<br />
            请下载安装后再使用本系统<br />
            <br />
            <br />
            <input class="button" type="button" value="立即下载" onclick="download()" />&nbsp;&nbsp;
            <input class="button" type="button" value="退   出" onclick="top.returnValue='true';top.close();" />
        </div>
    </form>
</body>
</html>
