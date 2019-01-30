<%@ Page Language="C#" AutoEventWireup="true" Codebehind="LoginTeacher.aspx.cs" Inherits="RailExamWebApp.LoginTeacher" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>在线学习考试管理系统</title>
    <meta http-equiv="imagetoolbar" content="no" />
    <link href="Online/style/index.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Common/JS/Common.js"></script>

    <script type="text/javascript">
    function ShowAdmin(type) 
    {
    	if(type != '') {
    		type = "?type=" + type;
    	}
        var ret = window.open('Default.aspx'+type,'','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
        ret.focus();    
    }

    function ShowStudy()
    {
        window.location = "Online/Study/Study.aspx?type=teacher";
    }
   
   function ShowExam1() 
   {
   	__doPostBack("btnExam", "");
   }

   function ShowExam(isfinger)
    {  
            var url;
            var height;
            var width;
           url = "/RailExamBao/Online/Exam/StudentLogin.aspx";
           width=350;
           height = 250;

             var ret = window.showModalDialog(url+"?Type=middle&IsFinger="+isfinger,'','help:no;status:no;dialogWidth:'+ width +'px;dialogHeight:'+ height +'px;');
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
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" ID="btnExam" Width="0" OnClick="btnExam_Click" />
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td valign="middle">
                    <div id="MainArea">
                        <div class="StudyArea">
                            <a href="#" onclick="ShowStudy()" title="自由或选择性的学习教材、学习课件"></a>
                        </div>
                        <div class="ExamArea">
                            <a href="#" onclick="ShowExam1()" title="在线考试"></a>
                        </div>
                        <div class="OnlineSelectLogin">
                            <a href="#" onclick="ShowSelectStudy()" title="个人查询"></a>
                        </div>
                        <div id="FunctionArea">
                            <span class="Function"><span class="FunctionBarLeft"></span><span onclick="ShowAdmin('')"
                                class="FunctionName">后台管理</span><span class="FunctionBarRight"></span> </span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <ComponentArt:CallBack ID="Callback1" runat="server" OnCallback="Callback1_Callback">
        </ComponentArt:CallBack>
        <input type="hidden" name="IsWuhan" value='<%=PrjPub.IsWuhan()%>' />
    </form>
</body>
</html>
