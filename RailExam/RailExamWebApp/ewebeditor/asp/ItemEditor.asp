<%@LANGUAGE="VBSCRIPT" CODEPAGE="936"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>编辑教材前言</title>
<script src="/RailExamBao/Common/JS/Common.js"></script>
<link href="/RailExamBao/App_Themes/Default/PageStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
	<%
		sSessionValue = "../Online/Item/" 
		Session("sSession") = sSessionValue
	%>
    <form id="form1" runat="server" method="post">
    <textarea rows="10" cols="30" name="myField" style="display:none"></textarea>
    <table><tr>
	<td>
    <iframe id="eWebEditor1" src="../ewebeditor.htm?id=myField&style=adv_imagetext" frameborder="0"
         scrolling="no" width="840" height="118"></iframe>
    </td></tr>
    </table>
    </form>
    <script >
   var search = window.location.search;
   var mode = search.substring(search.indexOf('Mode=')+5,search.indexOf('&'));
   var type = search.substring(search.indexOf('Type=')+5);
  if(mode == 'Edit')
    {
        if(type == "Content")
        {
            form1.myField.value=window.parent.document.getElementById("fvItem_txtContentEdit").value;
        }
        else
        {
           form1.myField.value=window.parent.document.getElementById("fvItem_txtContentAnswer").value;
        }
    } 
   else
  {
        if(type == "Content")
        {
            form1.myField.value=window.parent.document.getElementById("fvItem_txtContentInsert").value;
        }
        else
        {
           form1.myField.value=window.parent.document.getElementById("fvItem_txtContentAnswer").value;
        }
  }   
</script>  
</body>
</html>
