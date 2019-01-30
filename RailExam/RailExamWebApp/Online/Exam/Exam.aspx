<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Exam.aspx.cs" Inherits="RailExamWebApp.Online.Exam.Exam" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="imagetoolbar" content="no">
    <title>在线考试</title>
    <link href="../style/Exam.css" type="text/css" rel="stylesheet" />

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
        
        function ShowExamMyself()
       {
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
       
       function ShowExam(id)
       {  
            var isWuhan = document.getElementById("hfIsWuhan").value;
            var url;
            var height;
            var width;
            if(isWuhan=="True")
            {
               url = "/RailExamBao/Online/Exam/StudentLogin.aspx";
               width=350;
               height = 250;
            }
            else
            {
                url = "/RailExamBao/RandomExamOther/StudentLogin.aspx";
                height = 200;
                width = 300;
            }
            
            if(id==1)
            {
                 var ret = window.showModalDialog(url+"?Type=middle",'','help:no;status:no;dialogWidth:'+ width +'px;dialogHeight:'+ height +'px;');
                 if(ret.indexOf("false")>=0 )
                 {
                    Callback1.callback(ret.split('|')[1]);
                 }
            }
            else
            {
                if(document.getElementById("hfIsShowResult").value != "1")
                 {
                    alert("您没有查看试卷的权限！");
                    return;
                 }
                var ret =  window.showModalDialog(url+"?Type=right",'','help:no;status:no;dialogWidth:'+ width +'px;dialogHeight:'+ height +'px;');
                 if(ret.indexOf("false")>=0 )
                 {
                    Callback1.callback(ret.split('|')[1]);
                 }
            }
       } 
       
        function ChangePassword()
        {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-350)*.5;   
          ctop=(screen.availHeight-200)*.5;    
          var ret = window.open('../ChangePassword.aspx','ChangePassword','Width=350px; Height=200px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
          ret.focus();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td valign="middle">
                    <div id="MainArea">
                        <%--<div id="OnlineExamLeft">
                            <span id="ExamMyself"><a href="#" onclick="ShowExamMyself()">
                                <img src="../image/OnlineExamLeft.gif" alt="" /></a> </span>
                        </div>--%>
                        <div id="OnlineExamMiddle">
                            <span id="Exam"><a href="#" onclick="ShowExam(1)">
                                <img src="../style/image/OnlineExamMiddle.gif" alt="" /></a> </span>
                        </div>
                        <div id="OnlineExamRight">
                            <span id="ExamResult"><a href="#" onclick="ShowExam(2)">
                                <img src="../style/image/OnlineExamRight.gif" alt="" /></a> </span>
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
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowSummary="False"
            ShowMessageBox="True" DisplayMode="List"></asp:ValidationSummary>
        <asp:HiddenField runat="server" ID="hf" />
        <asp:HiddenField ID="hfIsShowResult" runat="server" />
        <asp:HiddenField ID="hfIsWuhan" runat="server" />
    </form>
</body>
</html>
