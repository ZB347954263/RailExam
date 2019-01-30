<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FreeExam.aspx.cs" Inherits="RailExamWebApp.Online.Exam.FreeExam" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自主测试</title>
    <link href="/RailExamBao/Online/style/ExamPaper.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">        
        function document.oncontextmenu()
        {
            event.returnValue = false;
        }
        
        function window.onhelp()
        {
            return false
        }
         
        var answers = [];
        
		function SaveRecord()
		{
		        if(confirm('您确定要提交本次自主测试试卷吗 ? '))
                {
                     document.getElementById("HfExamTime").value = "-1"; 
                     hastime.innerHTML= "考试时间已到，自动提交试卷!";   
                     window.parent.document.getElementById("hastime").innerHTML= "试卷已提交!";   
                     window.parent.document.getElementById("HfExamTime").value = "-2"; 
		             answers = [];
		             var ak = getAnswers(); 
                     form1.strreturnAnswer.value = ak;
		             form1.submit();
		             form1.strreturnAnswer.value = "";                   
                }
                else
		        {
		            return;
		        }            
		}
		
		function getAnswers()
		{
		    var inputs = document.getElementsByTagName("input");
		    var itemIds = [];
		    
		    for(var i = 0; i < inputs.length; i ++)
		    {
		        switch(inputs[i].type)
		        {
		            case "radio":
		            {
		                // itemIds: 1 for PaperItemId, 2 for ItemId, 3 for SubItemId, 4 for SelectAnswerId
                        itemIds = inputs[i].id.split('-');
	                    if(typeof(answers[itemIds[2]]) == "undefined")
	                    {
	                        answers[itemIds[2]] = new Array();
	                    }
		                if(inputs[i].checked)
		                {
		                    answers[itemIds[2]].push(itemIds[4]);
		                }
		                
		                break;
		            }
		            case "checkbox":
		            {
                        itemIds = inputs[i].id.split('-');
	                    if(typeof(answers[itemIds[2]]) == "undefined")
	                    {
	                        answers[itemIds[2]] = new Array();
	                    }
		                if(inputs[i].checked)
		                {
		                    answers[itemIds[2]].push(itemIds[4]);
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
		    for(var i = 0; i < answers.length; i ++)
		    {
		        if(answers[i])
		        {
		            res += i + "|" + answers[i].join("|") + "$";
		        }
		    }
		    
		    if(res.length > 1)
		    {
		        res = res.substring(0, res.length -1);
		    }		    
		   
		    return res;
		}
		
		function getFinishedItemsCount()
		{
		    var count = 0;
		    for(var i = 0; i < answers.length; i ++)
		    {
		        if(answers[i] && answers[i].length > 0)
		        {
		            count ++;
		        }
		    }
		    
		    //alert(count);
		    return count;
		}
		
		var selColor='#EBF9FD';//选中色
		 function CheckStyle(object)
        {		
          var obj = object;   
          var startid= obj.id; 
          var bigobj = document.getElementsByName(obj.name);
          var n = bigobj.length;
          
          var str = startid.split('-');
          document.getElementById("Item"+str[3]).style.backgroundColor=selColor;
          
          for(var i=0; i<n ; i++)
          {
            obj = bigobj[i];
	        while(obj.tagName != "TD")
	        {
		        obj = obj.parentElement;
	        }   
	        
	        if(obj!= null)
	        {
	            if( bigobj[i].id==startid)
	            {
	                obj.style.backgroundColor = selColor;
	            }
	            else
	            {
	                 obj.style.backgroundColor = selColor;
	            }	        
	        }
         }
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
		
			if(difftime ==-1)
			{
		         hastime.innerHTML= "考试时间已到，自动提交试卷!";  
		         answers = [];
		         var ak = getAnswers(); 
                 form1.strreturnAnswer.value = ak;
                 flag = false;       
		         form1.submit();
		         form1.strreturnAnswer.value = "";    
			}
			else  if(difftime >=0)			
			{			
                var diff = "离考试结束还有"       
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
<body oncontextmenu='return false' ondragstart='return false' onselectstart='return false'
    oncopy='document.selection.empty()' onbeforecopy='return false' onload="showTime()">
    <form id="form1" runat="server">
        <div class="time" id="nowtime" style="display: none;">
        </div>
        <div class="time" id="hastime" style="display: none;">
        </div>
        <div id="page">
            <div>
                <% FillPaper(); %>
            </div>
        </div>
        <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
        <asp:HiddenField ID="HfExamTime" runat="server" />
        <input id="strreturnAnswer" type="hidden" name="strreturnAnswer" />
    </form>
</body>
</html>
