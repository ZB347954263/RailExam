<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogPage.aspx.cs" Inherits="RailExamWebApp.Common.DialogPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>在线学习考试管理系统</title>
</head>
<body>
		<iframe id="dialogMain" src='<%=_pageUrl%>' frameborder="0"  width="100%" scrolling="no" style="height:100%;" marginwidth="0" marginheight="0"></iframe>
</body>
<script type="text/javascript">
    var search=window.location.search;
    if(search.indexOf("isfull=")>0)
    {
        document.getElementById("dialogMain").style.height=window.screen.availHeight-35; 
    }
    else
    {

           var height = search.substring(search.indexOf("height=")+7,search.indexOf("&pageName"));
           height = height.replace("px","");
           if(height > 35)
           {
            document.getElementById("dialogMain").style.height=height-35;
           }
    }     
</script>
</html>
