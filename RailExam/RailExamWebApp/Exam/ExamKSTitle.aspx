<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExamKSTitle.aspx.cs" Inherits="RailExamWebApp.Exam.ExamKSTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../Online/style/ExamPaper.css" type="text/css" rel="stylesheet" />

    <script src="../Common/JS/JSHelper.js" type="text/javascript"></script>

    <script type="text/javascript">
        function document.oncontextmenu()
        {
           //event.returnValue = false;
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
				alert("不可以用特殊功能键！");
				return;	
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
           // var  examtimeDate   =   new   Date(Date.parse(examtime.replace(/-/g,   "/"))); 
           
           document.getElementById("HfExamTime").value=examtime-1;
			var now = new Date();			
			var year = now.getYear();
			var month = now.getMonth() + 1;
			var date = now.getDate();
			var day = now.getDay();
			var hour = now.getHours();
			var minute = now.getMinutes();
			var second = now.getSeconds();	
         
			if(minute < 10)	minute = "0" + minute;
			if(second < 10)	second = "0" + second;
			
			switch(day)
			{
				case 0: day = "日";break;
				case 1: day = "一";break;
				case 2: day = "二";break;
				case 3: day = "三";break;
				case 4: day = "四";break;
				case 5: day = "五";break;
				case 6: day = "六";break;
			}
			
			nowtime.innerHTML =  year + "年" + month + "月" + date + "日 星期" + day + " "+(hour<12?"上午":"下午") + hour + ":" + minute + ":" + second;
						
			var difftime = Math.floor(examtime);
			 
			if(difftime < 0)
			{
//			 hastime.innerHTML= "考试时间已到，请提交试卷!";   
//			 answers = [];
//		     var ak = getAnswers();
//		     form1.strreturnAnswer.value = ak;
//             form1.submit();           
			}
			else			
			{			
               var diff = "离考试结束还有："       
                var hours = Math.floor(difftime%(60*60*24)/3600);
                var minutes = Math.floor((difftime%(60*60*24)-3600*hours)/60);
                var second1 = difftime-3600*hours-60*minutes;
                diff += (hours>10?"":"0") + hours + "小时";
                diff += (minutes>10?"":"0") + minutes + "分";
                diff += (second1>10?"":"0") + second1 + "秒";    
                hastime.innerHTML= diff;     
                }         				
			    setTimeout("showTime()", 1000);				
		}	
    </script>

</head>
<body onload="showTime()">
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class='ExamTitle' colspan="2">
                        武汉铁路局职工培训考试试卷</td>
                </tr>
                <tr>
                    <td id="ExamName">
                        考试名称：
                        <asp:Label runat="server" ID="lblTitle"></asp:Label>
                    </td>
                    <td id="ExamInfo">
                        <asp:Label runat="server" ID="lblTitleRight"></asp:Label>
                    </td>
                </tr>
                <tr align="left">
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="ExamStyle" colspan="6">
                                </td>
                            </tr>
                            <tr class="StudentTable">
                                <td class="StudentTitleInfo">
                                    单&nbsp;&nbsp;&nbsp;&nbsp;位：</td>
                                <td class="StudentInfo" style="width: 30%;">
                                    <asp:Label runat="server" ID="lblOrgName"></asp:Label></td>
                                <td class="StudentTitleInfo">
                                    姓&nbsp;&nbsp;&nbsp;&nbsp;名：</td>
                                <td class="StudentInfo" style="width: 18%;">
                                    <asp:Label runat="server" ID="lblName"></asp:Label>
                                </td>
                                <td class="StudentTitleInfo">
                                    职&nbsp;&nbsp;&nbsp;&nbsp;名：</td>
                                <td class="StudentInfo" style="width: 18%;">
                                    <asp:Label runat="server" ID="lblPost"></asp:Label></td>
                            </tr>
                            <tr class="StudentTable">
                                <td colspan="4" class="NowTimeInfo">
                                    <div class="time" id="nowtime">
                                    </div>
                                </td>
                                <td colspan="2">
                                    <div class="TimeInfo" id="hastime">
                                    </div>
                                </td>
                            </tr>
                        </table>
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
        <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldMaxCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldExamTime" runat="server" />
        <asp:HiddenField ID="HfExamTime" runat="server" />
        <asp:HiddenField ID="hfExamed" runat="server" />
        <input id="strreturnAnswer" type="hidden" name="strreturnAnswer" />
    </form>

    <script type="text/javascript">
            document.getElementById("rightContentWithNoHeight").height = 0.8*screen.availHeight;
            document.getElementById("ifExamInfo").height = 0.8*screen.availHeight; 
            var search=window.location.search;
            window.frames["ifExamInfo"].location = "ExamKS.aspx"+search; 
            
            if(document.getElementById("hfExamed").value == "true")
            {
                document.getElementById("hastime").style.display = "none";
            }
    </script>

</body>
