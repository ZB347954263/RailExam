<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FreeExamTitle.aspx.cs"
    Inherits="RailExamWebApp.Online.Exam.FreeExamTitle" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试页面</title>
    <link href="../style/ExamPaper.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="../../Common/JS/Common.js"></script>

    <script src="../Common/JS/JSHelper.js" type="text/javascript"></script>

    <script type="text/javascript">
        function document.oncontextmenu()
        {
           event.returnValue = false;
        }
        
        function window.onhelp()
        {
            return false
        }
         
        function document.onkeydown() 
		{
			if(window.event.shiftKey || window.event.altKey || window.event.ctrlKey|| window.event.tabKey)
			{
				event.keyCode = 0;
				event.returnValue = false;
				//alert("不可以用特殊功能键！");
				//return;	
			}

			if ((window.event.altKey) &&  
				((window.event.keyCode == 37) ||    //屏蔽 Alt+ 方向键 ← 
				 (window.event.keyCode == 39)))   //屏蔽 Alt+ 方向键 → 
			{ 
				event.returnValue = false; 
			}
				/* 注：这还不是真正地屏蔽 Alt+ 方向键， 
				因为 Alt+ 方向键弹出警告框时，按住 Alt 键不放， 
				用鼠标点掉警告框，这种屏蔽方法就失效了。以后若 
				有哪位高手有真正屏蔽 Alt 键的方法，请告知。*/ 
			if ( (event.keyCode == 116) ||                  //屏蔽 F5 刷新键 
				 (event.ctrlKey && event.keyCode == 82))  //Ctrl + R 
			{ 
				 event.keyCode = 0; 
				 event.returnValue = false; 
			} 
			if (event.keyCode == 122)  //屏蔽F11 
			{
				event.keyCode = 0;
				event.returnValue = false;
			}  
			if (event.ctrlKey && event.keyCode == 78)  //屏蔽 Ctrl+n 
			{
				event.returnValue = false;
			}
			if (event.shiftKey && event.keyCode == 121)  //屏蔽shift+F10 
			{
				event.returnValue = false;
			}
			 
			if (window.event.srcElement.tagName == "A" && window.event.shiftKey)    //屏蔽 shift 加鼠标左键新开一网页 
			{	
				window.event.returnValue = false;
			}
			if ((window.event.altKey) && (window.event.keyCode == 115))             //屏蔽Alt+F4 
			{  
				return false;
			}
		}
		
		function Close()
		{
            top.window.close();
		}
		       
		function getFinishedItemsCount()
		{
		    var count = 0;
		    for(var i=0; i<answers.length; i++)
		    {
		        if(answers[i] && answers[i].length > 0)
		        {
		            count++;
		        }
		    }
		    
		    //alert(count);
		    return count;
		}
		
		function showTime()
		{
		    var  examtime=document.getElementById("HfExamTime").value;//Exam Time 	    
           
           document.getElementById("HfExamTime").value=examtime-1;
			var difftime = Math.floor(examtime);
			
			//alert(difftime);
			if(difftime == -1)
			{
		         hastime.innerHTML= "考试时间已到，自动提交试卷!";  
			}
			else if(difftime >=0)			
			{			
                var diff = "倒计时："       
                var hours = Math.floor(difftime%(60*60*24)/3600);
                var minutes = Math.floor((difftime%(60*60*24)-3600*hours)/60);
                var second1 = difftime-3600*hours-60*minutes;
                diff += (hours>=10?"":"0") + hours + "小时";
                diff += (minutes>=10?"":"0") + minutes + "分";
                diff += (second1>=10?"":"0") + second1 + "秒";    
                hastime.innerHTML= diff;     
             } 
                     				
			 setTimeout("showTime()", 1000);				
		}
	
    </script>

</head>
<body onload="showTime()">
    <form id="form1" runat="server">
        <div>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class='ExamTitle' colspan="2">
                        <%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>考试试卷</td>
                </tr>
                <tr style="height:25px;background-color: #F3F7CA;">
                    <td style="text-align: left;font-size:13pt;">
                        <ComponentArt:CallBack OnCallback="CallBack1_Callback" ID="CallBack1" RefreshInterval="1000"
                            runat="server">
                            <Content>
                                <asp:Label ID="lblServerDateTime" runat="server" />
                            </Content>
                        </ComponentArt:CallBack>
                    </td>
                    <td style="text-align: right;font-size:13pt;">
                        <div style="font-weight:bold;color:Red;" id="hastime">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="rightContentWithNoHeight">
                            <iframe id="ifExamInfo" frameborder="0" scrolling="yes" width="100%"></iframe>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="HfExamTime" runat="server" />
    </form>

    <script type="text/javascript">
    document.getElementById("rightContentWithNoHeight").height = 0.9*screen.availHeight-50;
    document.getElementById("ifExamInfo").height = 0.9*screen.availHeight-50; 
     var search=window.location.search;
     window.frames["ifExamInfo"].location = "FreeExam.aspx"+search; 
    </script>

</body>
