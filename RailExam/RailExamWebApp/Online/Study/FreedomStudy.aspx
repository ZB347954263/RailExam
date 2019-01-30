<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FreedomStudy.aspx.cs" Inherits="RailExamWebApp.Online.Study.FreedomStudy" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自由学习</title>
    <link href="../style/FreedomStudy.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
            function GoBack()
          {
                window.location = "/RailExamBao/Online/Study/Study.aspx";
          }   
         function ShowIndex()
        {
            window.location = "/RailExamBao/Login.aspx";
        }
        
        function ShowAccount() 
        {
            var ret = window.open('/RailExamBao/Default.aspx','','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
            ret.focus();
        }
        
        function ShowAdmin() 
        {
            var ret = window.open('/RailExamBao/Default.aspx','','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
            ret.focus(); 
        }  
        
          function ShowStudyBookAll()
          {
                  var   cleft;   
                var   ctop;   
                cleft=(screen.availWidth-900)*.5;   
                ctop=(screen.availHeight-600)*.5;              
                var re= window.open("ReadBook.aspx","ReadBook",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');		
                re.focus();  
          }
          
          function ShowStudyCoursewareAll()
          {
                   var   cleft;   
                var   ctop;   
                cleft=(screen.availWidth-900)*.5;   
                ctop=(screen.availHeight-600)*.5;             
                var re= window.open("ReadCourse.aspx","ReadCourse",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');		
                re.focus();  
          }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td valign="middle">
                    <div id="MainArea">
                        <div id="FreeStudyArea">
                            <span id="FreeStudyBook"><a href="#" onclick="ShowStudyBookAll()">
                                <img src="/RailExamBao/Online/image/FreeStudyBook.gif" alt="" /></a></span> <span id="FreeStudyCourseware">
                                    <a href="#" onclick="ShowStudyCoursewareAll()">
                                        <img src="/RailExamBao/Online/image/FreeStudyCourseware.gif" alt="" /></a></span>
                        </div>
                        <div id="FunctionArea">
                            <span class="Function"><span class="FunctionBarLeft"></span><span onclick="GoBack()"
                                class="FunctionName">后退</span><span class="FunctionBarRight"></span> </span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
