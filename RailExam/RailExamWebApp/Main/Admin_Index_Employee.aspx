<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Index_Employee.aspx.cs" Inherits="RailExamWebApp.Main.Admin_Index_Employee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>在线学习考试管理系统</title>

    <script type="text/javascript">
   function logout()
   {
       top.returnValue = "false";
   }  
    </script>
</head>
<frameset rows="131,*,11" frameborder="no" border="0" framespacing="0">
	<frame src="Admin_Top.aspx?type=employee" name="topFrame" scrolling="No"
		noresize="noresize" id="topFrame" />
	<frame src="Middel.html?type=employee" name="mainFrame" id="mainFrame" />
	<frame src="Down.html" name="bottomFrame" scrolling="No"
		noresize="noresize" id="bottomFrame" />
</frameset>
<noframes>
    <body onbeforeunload="logout()" >
        <form id="form1" runat="server">
        </form>
    </body>
</noframes>
</html>