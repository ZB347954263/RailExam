<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendExamInfo.aspx.cs" Inherits="RailExamWebApp.RandomExam.AttendExamInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统提示</title>
   <script type="text/javascript">
   	var search = window.location.search;
   	var time = parseInt(search.substring(search.indexOf("examtime=") + 9, search.indexOf("&strbutton=")));
   	var strbutton = search.substring(search.indexOf("strbutton=")+ 10);
   	var type = 0;
   	
   	function GetTime()
   	{
   		top.returnValue = time;
   		top.close();
   	}
   	
   	function GetScore() 
   	{
   		type = 1;
   		top.returnValue = time + "|" + "true";
   		top.close();
   	}
   
   	function logOut() 
   	{
   		if(type == 0 ) 
   		{
   			top.returnValue = time;
   			top.close();
   		}
   	}
        
   	function showTime()
   	{
   		if(strbutton !="")
   	    {
		    document.getElementById("btnOK").style.display = "none";
		    document.getElementById("btnCancel").value = "确  定";
	    }

   		//alert(time);
   		time = time - 1;
   		
   		if(time<=0) 
   		{
   			type = 1;
   			top.returnValue = time + "|" + "true";
   			top.close();
   		}
   		setTimeout("showTime()", 1000);
   	}

   </script> 
</head>
<body onload="showTime()" onbeforeunload="logOut()">
    <form id="form1" runat="server">
        <br/>
    <div style="text-align: center">
        <asp:Label runat="server" ID="lblInfo"></asp:Label>
        <br/>
        <br/>
        <br/>
        <input  id="btnOK" name="btnOK" type="button" class="button" value="确  定" onclick="GetScore()"/>&nbsp;
        <input id="btnCancel" name="btnCancel" type="button" class="button" value="取  消" onclick="GetTime()"/>
    </div>
    </form>
</body>
</html>
