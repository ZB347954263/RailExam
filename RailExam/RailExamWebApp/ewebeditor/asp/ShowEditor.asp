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
            strState = Request("State")
            if strState = "Save"  then
	                str = Request("myField")
	                strBookID = Request("BookID")
	                strType = Request("Type")
	                strPath =  "../../Online/Book/" & strBookID & "/cover.htm"
                	 	 
                    Set fso = CreateObject("Scripting.FileSystemObject")
	                if fso.FileExists(Server.MapPath(strPath)) then
  		                fso.DeleteFile(Server.MapPath(strPath))
	                end if
                	
	                str = Replace(str,"/RailExamBao/Online/Book/" & strBookID & "/", "")
                	
	                Set stm = server.createobject("ADODB.Stream") 
	                stm.Charset = "UTF-8" 
	                stm.Open 
	                stm.WriteText str 
	                stm.SaveToFile Server.MapPath(strPath), 2 
	       end if
    %>
	<%
		strID = Request("BookID")
		strType=Request("Type")
		sSessionValue = "../Online/Book/" &strID & "/Upload/"
		Session("sSession") = sSessionValue
		
		Set fs = CreateObject("Scripting.FileSystemObject")
		
		if fs.FileExists(Server.MapPath("../../Online/Book/" & strID & "/cover.htm")) then
		  strCharactSet   =   "UTF-8"   
		  Set  objStream = CreateObject("adodb.stream")   
		  EditFile = "../../Online/Book/" & strID & "/cover.htm"  
		  objStream.Type=  2  
		  objStream.Mode   =   3   
		  objStream.Charset   =   strCharactSet   
		  objStream.Open   
		  objStream.LoadFromFile   Server.MapPath(EditFile)   
		  strAllContent   =   objStream.ReadText   
		  objStream.Close   
		  Set  objStream   =   Nothing   
		end if
		
		strAllContent = Replace(strAllContent,"Upload","/RailExamBao/Online/Book/" & strID & "/Upload")
		if instr(strAllContent,"booktitle") > 0 then
		        Dim start
		        start = instr(strAllContent,"br") + 6
		       strAllContent = Mid(strAllContent,start,len(strAllContent)) 
		end if
		dim strurl
		strurl = "ShowEditor.asp?State=Save"
	%>
    <form id="form1" runat="server" action=<%=strurl %> method="post">
   <div id="page"> 
    <div id="head">
        <div id="location">
        <div id="parent">
                教材管理</div>
            <div id="separator">
            </div>
            <div id="current">
                编辑教材前言</div>
        </div>
        <div id="button">
         	<input name="btnOK" type="image" src="../../Common/Image/confirm.gif" onClick="submit()"/> 
        </div>
    </div>
    <textarea rows="10" cols="30" name="myField" style="display:none"><%=strAllContent%></textarea>
	<input type="hidden" name="BookID" value=<%=strID%> />
   <input type="hidden" name="Type" value=<%=strType%> /> 
  <input type="hidden" name="State" value=<%=strState%> />  
    <table><tr>
	<td>
    <iframe id="eWebEditor1" src="../ewebeditor.htm?id=myField&style=coolblue" frameborder="0"
         scrolling="no" width="1000" height="630"></iframe>
    </td></tr>
    </table>
  </div> 
    </form>
<script>
  if (form1.State.value == "Save")
  {
      if(form1.Type.value == "Add")
      {   		   
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            var ret = showCommonDialog("/RailExamBao/Book/BookChapter.aspx?id="+form1.BookID.value + "&Type=Add&Mode=Insert",'dialogWidth:800px;dialogHeight:600px;');
            if(ret == "true")
           {
                top.returnValue = "true";
                top.close();
           } 
            //var re= window.open("../../Book/BookChapter.aspx?id="+id + "&Type=Add&Mode=Insert","BookChapter"," Width=800px; Height=600px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);		
           // re.focus();  
           //window.close(); 
      } 
      else
      {
            var ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?BookID="+form1.BookID.value + "&ChapterID=0&Object=bookcover",'dialogWidth:600px;dialogHeight:400px;');
            if(ret == "true")
            {
               if(window.opener)
               {
                   window.opener.form1.Refresh.value='true';
                   window.opener.form1.submit();
               }
               else
               {
                  window.returnValue = 'true';
               }
               window.close();             
            } 
     }
} 

</script>  
</body>
</html>
