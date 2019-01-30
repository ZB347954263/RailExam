<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckAttendExamInfo.aspx.cs" Inherits="RailExamWebApp.RandomExam.CheckAttendExamInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
   <script type="text/javascript">
   	var search = window.location.search;
   	function showExam() {
   		 if(!confirm("您确定开始正式考试吗？")) {
   		 	return;
   		 }
   		document.getElementById("btnStart").disabled =true;
   		window.location = "AttendExamStart.aspx" + search;
   	}
   	     
   	function showCheckExam() {
   		 if(!confirm("您确定要开始模拟考试吗？")) {
   		 	return;
   		 }
   	     window.location = "CheckAttendExamLeft.aspx" + search;	
   	}
   </script> 
</head>
<body>
    <form id="form1" runat="server">
  <div style="text-align: center">
            <br />
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <font style="color: #2D67CF; font-size: 30pt;font-weight:bold;">
                            考场规则</font>
                        <br />
                    </td>
                </tr>
               <tr>
                   <td style="text-align: left;color: #2D67CF;font-size: 23pt;">
                       <div id="examInfo" runat="server">
                        </div>
                   </td>
               </tr> 
            </table>
          <div style="display: none">
            <object id="UserControl1" classid="CLSID:F09D6002-3AD0-42E6-8E3D-D02149CD8F07" codebase="../Hook2012.CAB#version=1,0,0,0">
            </object>
          </div>
            <input type="button" id="btnStart" value="开始正式考试" onclick="showExam();" class="buttonLong"/>
           <!-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" id="btnCheck" value="开始模拟考试" onclick="showCheckExam();" class="buttonLong"/>-->
        </div>
    </form>
</body>
</html>

