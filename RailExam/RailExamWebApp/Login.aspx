<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Login.aspx.cs" Inherits="RailExamWebApp.Login" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>在线学习考试管理系统</title>
   <META   HTTP-EQUIV="imagetoolbar"   CONTENT="no" />    
    <link href="Online/style/index.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">

    function ShowStudy()
    {
        window.location = "Online/Study/Study.aspx?type=teacher";
    }

   function ShowExam()
    {  
            var url;
            var height;
            var width;
           url = "/RailExamBao/Online/Exam/StudentLogin.aspx";
           width=350;
           height = 250;

             var ret = window.showModalDialog(url+"?Type=middle",'','help:no;status:no;dialogWidth:'+ width +'px;dialogHeight:'+ height +'px;');
             if(ret.indexOf("false")>=0 && ret.split('|')[1]!="")
             {
                Callback1.callback(ret.split('|')[1]);
             }
       } 
   
        function ShowInfo()
       {  
             var ret = showCommonDialog("/RailExamBao/SystemVersionInfo.aspx",'dialogWidth:300px;dialogHeight:230px;');
             if(ret == "true" )
             {
                
             }
       }  
      
       function ShowSelectStudy()
       {
            var ret = window.showModalDialog("/RailExamBao/Online/Exam/StudentLogin.aspx?Type=left&type1=StudySelected",'','help:no;status:no;dialogWidth:350px;dialogHeight:250px;');
       }
       
         function ShowExam1() 
       {
   	        __doPostBack("btnExam", "");
       } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
          <asp:Button runat="server" ID="btnExam" Width="0" OnClick="btnExam_Click"/>
        <table width="100%"; height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td valign="middle">
                    <div id="MainArea">
                        <div id="StudyArea">
                            <span id="IndexStudy"><a href="#" onclick="ShowStudy()">
                                <img src="Online/style/image/IndexStudy.gif" alt="自由或选择性的学习教材、学习课件" /></a> </span>
                         </div>
                         <div id="ExamArea">
                            <span id="IndexExam"><a href="#" onclick="ShowExam1()">
                                <img src="Online/style/image/IndexExam.gif" alt="参加考试" /></a> </span>
                        </div>
                        <div id="EmployeeArea">
                            <span id="Employee"><a href="#" onclick="ShowAdmin('dangan')">
                                <img src="Online/style/image/Employee.gif" alt="电子档案" /></a> </span>
                        </div>
                         <div id="OnlineSelectLogin">
                            <span id="SelectStudy"><a href="#" onclick="ShowSelectStudy()">
                                <img src="Online/style/image/OnlineSelectLogin.gif" alt="个人查询" /></a> </span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <ComponentArt:CallBack ID="Callback1" runat="server" OnCallback="Callback1_Callback">
        </ComponentArt:CallBack>
    </form>
</body>
</html>
