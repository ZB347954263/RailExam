<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RandomExamPreview.aspx.cs" Inherits="RailExamWebApp.RandomExam.RandomExamPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../Online/style/ExamPaper.css" type="text/css" rel="stylesheet" />

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

		function Save()
		{
		    
               top.window.close();
	        
		}
		
		function Close()
		{
            top.window.close();
		}
		
        var answers = [];
        
		function SaveRecord()
		{
		    answers = [];
		    var ak = getAnswers();

		    if(getFinishedItemsCount() != document.getElementById("hfPaperItemsCount").value)
		    {
		        if(!confirm("您有未完成的试题，仍要提交试卷吗？"))
		        {  		       
		          return;
		        }  	        
		        if(!confirm("一旦提交试卷，您就不能再修改试卷了？"))
		        {  		          
		            return;
		        }		        
		         if(!confirm("再次确认您是否提交试卷？"))	       
		        {   		        
		            return;
		        }
		        
		        form1.strreturnAnswer.value = ak;
                form1.submit(); 		               
		    }
		    else
		    {
                if(!confirm('您确定要提交答卷吗 ? '))
                {
                     return;
		        }
                
               if(!confirm("一旦提交试卷，您就不能再修改试卷了？"))
		        {  	     return;
		        }	          
		         if(!confirm("再次确认您是否提交试卷？"))	       
		        {        return;
		        }
		        
		         form1.strreturnAnswer.value = ak;
                 form1.submit();
		  	}
	    }
		function getAnswers()
		{
		    var inputs = document.getElementsByTagName("input");
		    var itemIds = [];
		    
		    for(var i=0; i<inputs.length; i++)
		    {
		        switch(inputs[i].type)
		        {
		            case "radio":
		            {
		                // itemIds: 1 for PaperItemId, 2 for ItemId, 3 for SubItemId, 4 for SelectAnswerId
                        itemIds = inputs[i].id.split('-');
	                    if(typeof(answers[itemIds[1]]) == "undefined")
	                    {
	                        answers[itemIds[1]] = new Array();
	                    }
		                if(inputs[i].checked)
		                {
		                    answers[itemIds[1]].push(itemIds[4]);
		                }
		                
		                break;
		            }
		            case "checkbox":
		            {
                        itemIds = inputs[i].id.split('-');
	                    if(typeof(answers[itemIds[1]]) == "undefined")
	                    {
	                        answers[itemIds[1]] = new Array();
	                    }
		                if(inputs[i].checked)
		                {
		                    answers[itemIds[1]].push(itemIds[4]);
		                }
		                
		                break;
		            }
		            default:
		            {
		                break;
		            }
		        }
		    }

		    var res = "";
		    for(var i=0; i<answers.length; i++)
		    {
		        if(answers[i])
		        {
		            res += i + "|" + answers[i].join("|") + "$"
		        }
		    }
		    
		    if(res.length > 1)
		    {
		        res = res.substring(0, res.length -1);
		    }
		    
		    //alert(res);
		    return res;
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
			
			setTimeout("showTime()", 960);
		}
    </script>

</head>
<body onload="showTime()">
    <form id="form1" runat="server">
        <div>
            <table width="95%">
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class='ExamTitle' colspan="2">
                      <%=RailExamWebApp.Common.Class.PrjPub.GetRailNameBao()%>职工培训考试试卷</td>
                </tr>
                <tr>
                    <td id="ExamName" colspan="2">考试名称：
                        <asp:Label runat="server" ID="lblTitle"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="ExamInfo" colspan="2">
                        <asp:Label runat="server" ID="lblTitleRight"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <div class="time" id="nowtime">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%FillPaper(); %>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
        <input id="strreturnAnswer" type="hidden" name="strreturnAnswer" />
    </form>
</body>
</html>

