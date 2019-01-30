<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Study.aspx.cs" Inherits="RailExamWebApp.Online.Study.Study" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="imagetoolbar" content="no">
    <title>在线学习</title>
    <link href="../style/Study.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="../../Common/JS/Common.js"></script>

    <script type="text/javascript">
     function GoBack()
     {
        var search = window.location.search;
        var str = search.substring(search.indexOf('=')+1);
               
        if(str == "student")
        {
           window.location = "/RailExamBao/Login.aspx";
        }  
        else
        {
          window.location = "/RailExamBao/LoginTeacher.aspx";
        }
     }
          
            
        function ShowIndex()
        {
            window.location = "/RailExamBao/Login.aspx";
        }

        function ShowAdmin() 
        {
            var ret = window.open('/RailExamBao/Default.aspx','','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
            ret.focus(); 
        }  
        
        function ShowFreedomStudy()
        {
           var   cleft;   
           var   ctop;   
           cleft=(screen.availWidth-900)*.5;   
           ctop=(screen.availHeight-600)*.5;              
           var re= window.open("StudyAtLiberty.aspx","StudyAtLiberty",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');		
           re.focus();   	
        }
      
       
       function ShowAssistStudy(id)
       {  
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-600)*.5;
            var re= window.open("/RailExamBao/Online/Study/StudyAssistBook.aspx",'StudentLogin'," Width=900; Height=600,status=false,left="+cleft+",top="+ctop+",resizable=yes",true);
            re.focus();
       } 
       
       function ShowOnlineBook() {
            
//       	    alert("系统正在维护中...... 请先结清本系统尾款后，方可查看资料查阅！");
//       	    return;
       	
       	    var   cleft;   
           var   ctop;   
           cleft=(screen.availWidth-900)*.5;   
           ctop=(screen.availHeight-600)*.5;              
           //var re= window.open("/RailExam/Online/Study/ReadBook.aspx?type=1","ReadBook",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');		
           var re= window.open("/RailExamBao/AssistBook/InformationStudy.aspx","InformationStudy",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');		
           re.focus(); 
       }
       
       function ShowOnlineSelect() {
       	    var search = window.location.search;
            var str = search.substring(search.indexOf('=')+1);
           
            if(str == "student")
           {
                window.location ="/RailExamBao/Online/Study/SelectStudy.aspx?type=student";
           }  
           else
           {
                window.location = "/RailExamBao/Online/Study/SelectStudy.aspx?type=teacher";
           }
       }
       
       function ShowOnlineExam() {
   	        var search = window.location.search;
            var str = search.substring(search.indexOf('=')+1);
           
            if(str == "student")
           {
                window.location ="/RailExamBao/Online/Exam/OnlineExamMyself.aspx?type=student";
           }  
           else
           {
                window.location = "/RailExamBao/Online/Exam/OnlineExamMyself.aspx?type=teacher";
           } 
       }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td valign="middle">
                    <div id="MainArea">
                        <div class="OnlineStudyArea">
                            <a href="#" onclick="ShowFreedomStudy()" title="学习全局所有培训资料"></a>
                        </div>
                        <div class="OnlineSelect">
                           <a href="#" onclick="ShowOnlineSelect()" title="根据单位、职名选择学习"></a> 
                        </div>
                        <div class="OnlineFreeExam">
                            <a href="#" onclick="ShowOnlineExam()" title="学员自主考试"></a> 
                        </div>
                        <div id="FunctionArea">
                            <span class="Function"><span class="FunctionBarLeft"></span><span onclick="GoBack()"
                                class="FunctionName">后退</span><span class="FunctionBarRight"></span> </span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <ComponentArt:CallBack ID="Callback1" runat="server" OnCallback="Callback1_Callback">
        </ComponentArt:CallBack>
        <asp:HiddenField ID="hfIswuhan" Value="" runat="server" />
    </form>
</body>
</html>
