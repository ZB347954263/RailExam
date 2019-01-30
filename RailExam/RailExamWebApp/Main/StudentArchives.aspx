<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StudentArchives.aspx.cs"
    Inherits="RailExamWebApp.Main.StudentArchives" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学生档案</title>
    <script type="text/javascript">
    function gotoStudentArchives()
    {
        var id = document.getElementById("hfEmployeeID").value;
        window.frames["ifrmArchives"].location = "../RandomExamTai/EmployeeArchives.aspx?id=" + id + "&Type=0";
    }
    </script>
</head>
<body onload="gotoStudentArchives()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="Div4">
                    <div id="location">
                        <div id="desktop" onclick="window.location='../Main/EmployeeDesktop.aspx'">
                        </div>
                        <div id="parent">
                            我的工作台</div>
                        <div id="separator">
                        </div>
                        <div id="current">
                            学生档案</div>
                    </div>
                    <div id="welcomeInfo">
                        <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                    </div>
                </div>
            </div>
            <div id="content">
                <iframe id="ifrmArchives" src="#" frameborder="0"
                    scrolling="no" class="iframe"></iframe>
            </div>
        </div>
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
    </form>
</body>
<script type="text/javascript">
    document.getElementById("ifrmArchives").style.height =(document.documentElement.clientHeight)+'px';
</script>
</html>
