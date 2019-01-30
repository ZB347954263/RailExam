<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckAttendExamNew.aspx.cs" Inherits="RailExamWebApp.RandomExam.CheckAttendExamNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link href="../Online/style/ExamPaper.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script src="../Common/JS/JSHelper.js" type="text/javascript"></script>

    <script type="text/javascript">
        function document.oncontextmenu()
        {
           event.returnValue = false;
        }
        
        function window.onhelp()
        {
        	return false;
        }
         
		function Save()
		{
		    if(confirm("您确定不提交试卷就关闭吗？"))
	        {
               top.window.close();
	        }
	        else
	        {
	            return;
	        }
		}
		
		function Close()
		{
            top.window.close();
		}
		
        var answers = new Array();
       
        var LastAnswers  = new Array();
        
		function SaveRecord()
		{
			//document.getElementById("btnClose").disabled = true;
			//window.parent.parent.document.getElementById("btnClose").disabled = true;
		
			try 
			{
			    if(window.parent.parent.document.getElementById('cap1'))
			    {
				    if (window.parent.parent.document.getElementById('cap1').camCount > 0)
				    {
					   window.parent.parent.paizhao();
				    }
			    }
			} 
			catch(e) 
			{

			} 
			
		    answers = [];
		    var ak = getAnswers();
            if(!confirm('您确定要提交模拟考试吗 ? '))
            {
            	 //document.getElementById("btnClose").disabled = false;
            	 //window.parent.parent.document.getElementById("btnClose").disabled = false;
                 return;
	        }
	        
	        form1.btnSave.style.display="none";  
	        form1.strreturnAnswer.value = ak;
	        document.getElementById("hfReturnAnswer").value = ak;

	    	window.parent.parent.close() ;
	    	//window.parent.parent.location = "CheckClose.aspx";
	    } 
	  
	   
	   function ItemAnswer(id,answer)
	   {
	        this.id=id;
	        this.answer = answer;
	   }
	    
		function getAnswers()
		{
		    var inputs = document.getElementsByTagName("input");
		    var itemIds = [];
		    
		    var n = 0;
		    var m = 0;
		    for(var i=0; i<inputs.length; i++)
		    {
		        switch(inputs[i].type)
		        {
		            case "radio":
		            {
		                // itemIds: 1 for PaperItemId, 2 for ItemId, 3 for SubItemId, 4 for SelectAnswerId
                        itemIds = inputs[i].id.split('-');
	                   	                    
	                    if(i>1)
		               {
	                        if(inputs[i].name != inputs[i-1].name)
	                        {
	                            n=n+1; 
                                if(inputs[i].checked)
                                {  
                                    m=1;
                                }
                               else
                               {
                                   m=0;
                               } 	                        
                            }
	                        else
	                        {
	                           //判断同一题下的单选是否有被选中的 
	                           if(inputs[i].checked)
	                           {
	                               m=m+1; 
	                           }
	                            
	                           if((m==0 && inputs[i].name != inputs[i+1].name)|| (m==0 && !inputs[i+1]))
	                           {
                                      var obj=inputs[i]; 
                                      var startid= obj.id; 
                                      var bigobj = document.getElementsByName(obj.name);
                                                                            
                                      var str = startid.split('-');
//                                      document.getElementById("Item"+str[2]+str[3]).style.backgroundColor='#ffcccc';
                                      
                                      for(var j=0; j<bigobj.length ; j++)
                                      {
                                        obj = bigobj[j];
                                        while(obj.tagName != "TD")
                                        {
	                                        obj = obj.parentElement;
                                        }   
                            	        
//                                        if(obj!= null)
//                                        {
//                                            if( bigobj[j].id==startid)
//                                            {
//                                                obj.style.backgroundColor = '#ffcccc';
//                                            }
//                                            else
//                                            {
//                                                 obj.style.backgroundColor ='#ffcccc';
//                                            }	        
//                                        }
                                     }                 
                                 }    
	                        }     
		                }   
	                    if(typeof(answers[n]) == "undefined")
	                    {
	                        answers[n] = new ItemAnswer(itemIds[1],"");
	                    }  
		                if(inputs[i].checked)
		                {
		                    answers[n].id=itemIds[1];
		                    answers[n].answer=itemIds[4]; 
		                }
		                
		                break;
		            }
		            case "checkbox":
		            {
                        itemIds = inputs[i].id.split('-');
                        if(i>1)
		               {
	                        if(inputs[i].name != inputs[i-1].name)
	                        {
	                            n=n+1; 
	                        }  
		                }  
		            	
	                    /*if(typeof(answers[itemIds[1]]) == "undefined")
	                    { 
	                        answers[itemIds[1]] = new Array();
	                    }
		                if(inputs[i].checked)
		                {
		                    answers[itemIds[1]].push(itemIds[4]);
		                }*/
		                
		                if(typeof(answers[n]) == "undefined")
	                    {
	                        answers[n] = new ItemAnswer(itemIds[1],"");
	                    }  
		                if(inputs[i].checked)
		                {
		                    answers[n].id=itemIds[1];
		                    if( answers[n].answer== "")
		                   {
		                       answers[n].answer=  itemIds[4]; 
		                   } 
		                   else
		                   {
		                       answers[n].answer =  answers[n].answer +"|"+ itemIds[4];
		                   }
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
		            res += answers[i].id+"|"+answers[i].answer+"$"; 
		       } 
		    }
		    
		    //alert(res);

		    if(res.length > 1)
		    {
		        res = res.substring(0, res.length -1);
		    }
		    
		    return res;
		}
		
		function getFinishedItemsCount()
		{
		    var count = 0;
		    for(var i=0; i<answers.length; i++)
		    {
		        if(answers[i])
		       {
		            if(answers[i].answer !="")
		            {
		                count++;
		            }		       
		        } 
		    }
		    
		    //alert(count);
		    return count;
		}
		
		var flag = true;
		var photoCount = 0;
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
			 
			if(difftime < 0 && flag)
			{
			    //alert("a");
		         hastime.innerHTML= "考试时间已到，自动提交试卷!";  
		         answers = [];
	             var ak = getAnswers();
		         document.getElementById("hfReturnAnswer").value = ak; 
	             form1.strreturnAnswer.value = ak;
	             flag = false;
				 
				window.parent.parent.close();  
		    	 //window.parent.parent.location = "CheckClose.aspx";
			}
			else			
			{			
                var diff = "倒计时："       
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
		
		
		var selColor='#ffcccc';//选中色#EBF9FD

        function CheckStyle(object)
        {
          var obj = object;   
          var startid= obj.id; 
          var bigobj = document.getElementsByName(obj.name);
          var n = bigobj.length;
          
          var str = startid.split('-');
          if(str.length==6)
          {          
                document.getElementById("Item"+str[2]+str[3]+str[5]).style.backgroundColor=selColor;
                window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item"+str[2]+str[3]+str[5]).style.backgroundColor=selColor;
          } 
          else 
          {
                document.getElementById("Item"+str[2]+str[3]).style.backgroundColor=selColor;
                window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item"+str[2]+str[3]).style.backgroundColor=selColor;
          }
          
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
	                 obj.style.backgroundColor =selColor;
	            }	        
	        }
         }
       }
       
       function CheckEmpty()
       {
 		    var inputs = document.getElementsByTagName("input");
           var m=0;
		    for(var i=0; i<inputs.length; i++)
		    {
	                // itemIds: 1 for PaperItemId, 2 for ItemId, 3 for SubItemId, 4 for SelectAnswerId
                    itemIds = inputs[i].id.split('-');
                   	                    
                    if(i>1)
	               {
                        if(inputs[i].name != inputs[i-1].name)
                        {
                            if(inputs[i].checked)
                            {  
                                m=1;
                            }
                           else
                           {
                               m=0;
                           } 
                        }
                        else
                        {
                           //判断同一题下的单选是否有被选中的 
                           if(inputs[i].checked || inputs[i-1].checked)
                           {
                               m=m+1; 
                           }
                            
                           if((m==0 && inputs[i].name != inputs[i+1].name)|| (m==0 && !inputs[i+1]))
                           {
                                  var obj=inputs[i]; 
                                  var startid= obj.id; 
                                  var bigobj = document.getElementsByName(obj.name);
                                                                        
                                  var str = startid.split('-');
                           	      if(str.length==6) {
                           	      	document.getElementById("Item" + str[2] + str[3]+str[5]).style.backgroundColor = '#ffcccc';
                           	      } else {
                           	      	document.getElementById("Item" + str[2] + str[3]).style.backgroundColor = '#ffcccc';
                           	      }

                           	    for(var j=0; j<bigobj.length ; j++)
                                  {
                                    obj = bigobj[j];
                                    while(obj.tagName != "TD")
                                    {
                                        obj = obj.parentElement;
                                    }   
                        	        
                                    if(obj!= null)
                                    {
                                        if( bigobj[j].id==startid)
                                        {
                                            obj.style.backgroundColor = '#ffcccc';
                                        }
                                        else
                                        {
                                             obj.style.backgroundColor ='#ffcccc';
                                        }	        
                                    }
                                 }                 
                             }    
                        }     
	                } 
	         } 
       }
       
       function StartStyle()
        {		
            var inputs = document.getElementsByTagName("input");		    
		    var obj;
		    for(var j=0; j<inputs.length; j++)
		    {  
                 obj = inputs[j];
                 if(obj.checked)
		         { 
                      var startid= obj.id; 
                      var bigobj = document.getElementsByName(obj.name);
                      var n = bigobj.length;
                      
                      var str = startid.split('-');
                 	    if(str.length==6) {
                 	    	document.getElementById("Item" + str[2] + str[3]+str[5]).style.backgroundColor = selColor;
                 	    	if (window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item" + str[2] + str[3]+str[5])) {
                 	    		window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item" + str[2] + str[3]+str[5]).style.backgroundColor = selColor;
                 	    	}
                 	    } else {
                 	    	document.getElementById("Item" + str[2] + str[3]).style.backgroundColor = selColor;
                 	    	if (window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item" + str[2] + str[3])) {
                 	    		window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item" + str[2] + str[3]).style.backgroundColor = selColor;
                 	    	}
                 	    }

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
	                             obj.style.backgroundColor =selColor;
	                        }	        
	                    }
                     }
                } 
            }
           }
       
       function clickEmpty(object) 
      {
       	 var id = object.id;
       	 var num = id.substring(id.indexOf('Empty') + 5);
       	 var name = 'Answer' + num;
       	 var bigobj = document.getElementsByName(name);
         var n = bigobj.length;
       	
        if(n == 0) 
        {
        	name = 'RAnswer' + id.substring(id.indexOf('Empty') + 5);
       	   bigobj = document.getElementsByName(name);
           n = bigobj.length;
        }
       	 
       	 document.getElementById("Item"+num).style.backgroundColor='#ffffff';
       	 window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item"+num).style.backgroundColor='#ffffff';

       	var itemID="";
       	
       	 for(var i=0; i<n ; i++)
       	 {
       	 	obj = bigobj[i];
       	 	obj.checked = false;
       	 	itemID = obj.id;
       	 	
       	 	while(obj.tagName != "TD")
            {
                obj = obj.parentElement;
            }   
	        
            if(obj!= null)
            {
                obj.style.backgroundColor = '#ffffff';
            }
       	 }
       }
    </script>

</head>
<body onload="showTime()">
    <form id="form1" runat="server">
        <div>
            <div class="time" id="nowtime" style="display: none;">
            </div>
            <div class="time" id="hastime" style="display: none;">
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <%FillPaper(); %>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldMaxCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldExamTime" runat="server" />
        <asp:HiddenField ID="HfExamTime" runat="server" />
        <input id="strreturnAnswer" type="hidden" name="strreturnAnswer" />
        <asp:HiddenField ID="hfReturnAnswer" runat="server" />
    </form>
</body>
</html>
