<%@LANGUAGE="VBSCRIPT" CODEPAGE="936"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>编辑教材章节内容</title>
<script src="/RailExamBao/Common/JS/Common.js"></script>
<link href="/RailExamBao/App_Themes/Default/PageStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <%
        Dim strState,str
         strState = Request("State")
        if strState = "Save" then
                 str = ""
       	         For i = 1 To Request("myField").Count
                            str = str & Request("myField")(i)
                 Next 
	            strChapterID = Request("ChapterID")
	            strBookID = Request("BookID")

	            strPath =  "../../Online/Book/" & strBookID & "/" & strChapterID & ".htm"
            	 	 
                Set fso = CreateObject("Scripting.FileSystemObject")
	            if fso.FileExists(Server.MapPath(strPath)) then
  		            fso.DeleteFile(Server.MapPath(strPath))
	            end if
            	
               'Response.Write(str) 
	           str = Replace(str,"/RailExamBao/Online/Book/" & strBookID & "/", "")

	            Set stm = server.createobject("ADODB.Stream") 
	            stm.Charset = "UTF-8" 
	            stm.Open 
	            stm.WriteText str 
	            stm.SaveToFile Server.MapPath(strPath), 2 
       end if   
     %>
	<%
		strID = Request("ChapterID")
	    strBookID = Request("BookID")
	   strType=Request("Type") 
		
		'sqlstr = "select * from Book_Chapter where Chapter_ID=" & strID
		'set rs = server.CreateObject("adodb.recordset")
		'rs.Open sqlstr,conn,1,1
		'strBookID = rs("Book_ID")
		
		sSessionValue = "../Online/Book/" &strBookID & "/Upload/"
		
		Session("sSession") = sSessionValue
		
		Set fs = CreateObject("Scripting.FileSystemObject")
		
		strPath = "../../Online/Book/" & strBookID & "/" & strID & ".htm"
		
		if fs.FileExists(Server.MapPath(strPath)) then
		  strCharactSet   =   "UTF-8"   
		  Set  objStream = CreateObject("adodb.stream")   
		  EditFile = strPath  
		  objStream.Type=  2  
		  objStream.Mode   =   3   
		  objStream.Charset   =   strCharactSet   
		  objStream.Open   
		  objStream.LoadFromFile   Server.MapPath(EditFile)   
		  strAllContent   =   Server.HTMLEncode(objStream.ReadText)   
		  objStream.Close   
		  Set  objStream   =   Nothing   
		end if		
    
		strAllContent = Replace(strAllContent,"Upload","/RailExamBao/Online/Book/" & strBookID & "/Upload")
		
		if instr(strAllContent,"chaptertitle") > 0 then
		        Dim start
		        start = instr(strAllContent,"br") + 6
		       strAllContent = Mid(strAllContent,start,len(strAllContent)) 
		end if
		dim strurl
		strurl = "ShowChapterEditor.asp?State=Save"
	%>
    <form id="form1" runat="server" action=<%=strurl %>  method="post">
   <div id="page"> 
    <div id="head">
        <div id="location">
        <div id="parent">
                教材管理</div>
            <div id="separator">
            </div>
            <div id="current">
                编辑章节内容</div>
        </div>
        <div id="chapterNamePath">
              <script>
                var search = window.location.search;
                var str= search.substring(search.indexOf("NamePath=")+9);
                document.write(unescape(str));
             </script> 
        </div> 
        <div id="button">
         	<input name="btnOK" type="image" src="../../Common/Image/confirm.gif" onClick="submit()"> 
        </div>
    </div>
    <textarea rows="10" cols="30" name="myField" style="display:none"><%=strAllContent%></textarea>
	<input type="hidden" name="ChapterID" value=<%=strID%> />
	<input type="hidden" name="Type" value=<%=strType%> />
	<input type="hidden" name="BookID" value=<%=strBookID%> />
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
      else
      {
            var ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?BookID="+form1.BookID.value + "&ChapterID=" + form1.ChapterID.value + "&Object=chaptercontent",'dialogWidth:600px;dialogHeight:400px;');
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