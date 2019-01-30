<%@ Page Language="C#" AutoEventWireup="true" Codebehind="OnlineExam.aspx.cs" Inherits="RailExamWebApp.Online.Exam.OnlineExam" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参加考试</title>

    <script type="text/javascript" src="../../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function AttendExam(ExamId,PaperId,type)      
        {
            var employeeID = document.getElementById("hfEmployeeID").value;   
        	if(employeeID == "") {
        		alert('登录超时，请关闭重新登录！');
        		return;
        	}
            if(type == 0)
            {
                var w=window.open("/RailExamBao/Exam/ExamKSTitle.aspx?ExamId="+ExamId+"&PaperId="+PaperId,"ExamKS","fullscreen=yes,toolbar=no,scrollbars=no");	
                //w.focus();	
            } 
            else
            {               
                var ret = window.open("/RailExamBao/RandomExam/CheckAttendExamInfo.aspx?id="+ExamId+"&employeeID="+employeeID,"AttendExamStart","fullscreen=yes,toolbar=no,scrollbars=no,status=no");
                //ret.focus();
                //var ret = showCommonDialogFull("/RailExamBao/RandomExam/AttendExamStart.aspx?id="+ExamId+"&employeeID="+employeeID,'dialogWidth:'+window.screen.width+'px;dialogHeight:'+window.screen.height+'px;');

           }
        }    
        
       function logout()
       {
           top.returnValue = "false";
       } 
       
      function showCheckExam() {
   		 if(!confirm("您确定要开始模拟考试吗？")) {
   		 	return;
   		 }
      	 var employeeID = document.getElementById("hfEmployeeID").value;   
        var ret = window.open("/RailExamBao/RandomExam/CheckAttendExamLeft.aspx?employeeID="+employeeID,"CheckAttendExamLeft","fullscreen=yes,toolbar=no,scrollbars=no,status=no");
        //ret.focus();
   	} 
    </script>

</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        在线考试</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        参加考试</div>
                </div>
                <div id="button">
                </div>
            </div>
            <div style="text-align: center;">
                <font style="font-size: xx-large; color: red; font-weight: bold;vertical-align: middle;">点击<input type="button" id="btnCheck" value="开始模拟考试" onclick="showCheckExam();" class="buttonLong"/>，开始模拟考试</font><br />
                <font style="font-size: xx-large; color: red; font-weight: bold">请点击考试列表中的考试名称，开始正式考试</font>
            </div>
            <div style="text-align: left; width: 100%;">
                <table style="width: 100%; height: 25px; background-color: #EAF4FF; padding-left: 10px;
                    padding-right: 10px; line-height: 25px; color: #2D67CF; font-size: 12px">
                    <tr>
                        <td style="width: 8%;" align="right">
                            姓名：</td>
                        <td style="width: 20%;" align="left">
                            <asp:Label ID="lblName" runat="server"></asp:Label></td>
                        <td style="width: 8%;" align="right">
                            单位：</td>
                        <td style="width: 25%;" align="left">
                            <asp:Label ID="lblOrg" runat="server"></asp:Label></td>
                        <td style="width: 8%;" align="right">
                            职名：</td>
                        <td style="width: 25%;" align="left">
                            <asp:Label ID="lblPost" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <div>
                    <table style="width: 100%; font-size: x-large; color: #2D67CF; border: solid 1px #E0E0E0;
                        border-collapse: collapse;">
                        <tr>
                            <td style="width: 60%; border: solid 1px #E0E0E0; padding: 5px; white-space: nowrap;"
                                align="center">
                                考试名称</td>
                            <td style="width: 40%; border: solid 1px #E0E0E0; padding: 5px; white-space: nowrap;"
                                align="center">
                                有效时间</td>
                        </tr>
                        <%FillPage();%>
                    </table>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
        <input type="hidden" name="ApplyID" />
    </form>
</body>
</html>
