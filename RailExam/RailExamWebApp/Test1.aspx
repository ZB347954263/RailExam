<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test1.aspx.cs" Inherits="RailExamWebApp.Test1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<script language="javascript" type="text/javascript">
    var request = false;
    try{
        request = new XMLHttpRequest();
    }catch(trymicrosoft){
        try{
                request = new ActiveXObject("Msxml2.XMLHTTP");
            }catch(othermicrosoft){
                try{
                    request = new ActiveXObject("Microsoft.XMLHTTP");
                }catch(failed){
                request = false;
                }
        }
    }
    if(!request)
        alert("Error initializing XMLHttpRequest!");
    
    
    function getCustomerInfo(){
        var location = window.location.href;
    	var newurl = location.substring(0, location.indexOf("RailExamBao/") + 12);
        request.open("GET", newurl+"Login.aspx", true);
        request.onreadystatechange = updatePage;
        request.send(null);
    }

    function updatePage(){
        if(request.readyState == 4){
            if(request.status == 200){
                var response = request.responseText.split("|");
             document.getElementById("order").value = response[0];
                document.getElementById("address").innerHTML = response[1].replace(/\n/g, "<br/>");
            }else if(request.status == 404){
                alert("Request URL does not exit");
            }else if(request.status == 403){
                alert("Access denied");
            }else{
                alert("Error:status code is" + request.status);
            }
        }
    }


</script>
</head>
<body>
<form id="form1">
    <p>Enter your phone number:
        <input type="text" size="14" name="phone" id="phone" onChange="getCustomerInfo();" />
    </p>
    <p>Your order will be delivered to:</p>
    <div id="address"></div>
    <p>Type your order in here:</p>
    <p><textarea name="order" rows="6" cols="50" id="order"></textarea></p>
    <p><input type="submit" value="Order Pizza" id="submit" /></p>
</form>
</body>
</html>
 
