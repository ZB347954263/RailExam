<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AttendExamNew.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.AttendExamNew" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../Online/style/ExamPaper.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../Common/JS/jquery1.3.2.js"></script>
    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script src="../Common/JS/JSHelper.js" type="text/javascript"></script>

    <script type="text/javascript">
    	var s;
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

    	var photoCount = 0;
        
    	function SaveRecord()
    	{
    		var needtime = document.getElementById("hfNeed").value;
    		if(needtime!= "") 
    		{
    			if(photoCount <needtime)
    			{
//    				alert("考试前10分钟不能提交答卷！");
//    				return;
    				
//    				if(!showInfo("考试前10分钟不能提交答卷！","nocancel")) 
//    				{
//    					return;
//    				}
    			}
    		}

    			
    		document.getElementById("btnClose").disabled = true;
    		window.parent.parent.document.getElementById("btnClose").disabled = true;
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
    		var  maxCount=document.getElementById("HiddenFieldMaxCount").value;
    		if(getFinishedItemsCount() < document.getElementById("hfPaperItemsCount").value)
    		{		      
    			if(!confirm("您有未完成的试题，仍要提交试卷吗？"))//未完成的试题已标记为粉红色，
    			{  		     
    				document.getElementById("btnClose").disabled = false;
    				window.parent.parent.document.getElementById("btnClose").disabled = false;
    				return;
    			}  	
    			
//    			if(!showInfo("您有未完成的试题，仍要提交试卷吗？","")) 
//    			{
//    				return;
//    			}
		          
//		         if(!confirm("您还可以参加本次考试的次数为"+maxCount+",再次确认您是否提交试卷？"))	       
//		        {   		        
//		            return;
//		        }
		    	
    			form1.btnSave.style.display="none";  
    			document.getElementById("hfReturnAnswer").value = ak; 
    			form1.strreturnAnswer.value = ak;
    			form1.submit(); 	
    			form1.strreturnAnswer.value = ""; 	               
    		}
    		else
    		{
    			if(!confirm('您确定要提交答卷吗 ? '))
                {
                	 document.getElementById("btnClose").disabled = false;
                	 window.parent.parent.document.getElementById("btnClose").disabled = false;
                     return;
		        }

//    			if(!showInfo("您确定要提交答卷吗 ?","")) 
//    			{
//    				return;
//    			}

//             if(!confirm("您还可以参加本次考试的次数为"+maxCount+",再次确认您是否提交试卷？"))	       
//		        {   		        
//		            return;
//		        }
		        
    			form1.btnSave.style.display="none";  
    			form1.strreturnAnswer.value = ak;
    			document.getElementById("hfReturnAnswer").value = ak;
    			form1.submit();
    			form1.strreturnAnswer.value = "";
    		}
    	} 
    	
    	function showInfo(str,strbutton) 
    	{
    		var examtime = window.parent.document.getElementById("HfExamTime").value;
    		var ret = window.showModalDialog("/RailExamBao/RandomExam/AttendExamInfo.aspx?Info="+str+"&examtime="+examtime+"&strbutton="+strbutton, 
    			'', 'help:no; status:no; dialogWidth:280px;dialogHeight:100px;scroll:no;');
    		var sec;
    		if(String(ret).indexOf("true") < 0  ) 
    		{
    			sec = ret;
    		} 
    		else 
    		{
    			sec = parseInt(String(ret).split("|")[0]);
    		}
    		//alert(sec);
    		window.parent.document.getElementById("HfExamTime").value = sec;
    		document.getElementById("HfExamTime").value = sec;
    		var difftime = Math.floor(sec);
    		var diff = "倒计时：";      
    		var hours = Math.floor(difftime%(60*60*24)/3600);
    		var minutes = Math.floor((difftime%(60*60*24)-3600*hours)/60);
    		var second1 = difftime-3600*hours-60*minutes;
    		diff += (hours>=10?"":"0") + hours + "小时";
    		diff += (minutes>=10?"":"0") + minutes + "分";
    		diff += (second1>=10?"":"0") + second1 + "秒";    
    		window.parent.hastime.innerHTML= diff;    
    		hastime.innerHTML= diff;

    		if(String(ret).indexOf("true") < 0  ) 
    		{
    			return false;
    		}

    		return true;
    	}
	   
	    
    	function showProgressbar(examID,randomExamResultID,studentID,orgID,endTime,examTime)
    	{
//	        alert("/RailExamBao/RandomExam/AttendExamEnd.aspx?ExamID=" +
//							    examID+ "&RandomExamResultID=" + randomExamResultID +
//							   "&StudentID=" + studentID + "&OrgID=" + orgID+ "&EndTime=" +
//							  endTime+ "&ExamTime=" +examTime);
    		var ret=showCommonDialog("/RailExamBao/RandomExam/AttendExamEnd.aspx?ExamID=" +
    			examID+ "&RandomExamResultID=" + randomExamResultID +
    			"&StudentID=" + studentID + "&OrgID=" + orgID+ "&EndTime=" +
    			endTime+ "&ExamTime=" +examTime,'dialogWidth:500px;dialogHeight:200px;');
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
    			
    			//alert("a");
    			hastime.innerHTML= "考试时间已到，自动提交试卷!";  
    			answers = [];
    			var ak = getAnswers();
    			document.getElementById("hfReturnAnswer").value = ak; 
    			form1.strreturnAnswer.value = ak;
    			flag = false;
    			form1.submit();           
    		}
    		else			
    		{
    			var diff = "倒计时：";     
    			var hours = Math.floor(difftime%(60*60*24)/3600);
    			var minutes = Math.floor((difftime%(60*60*24)-3600*hours)/60);
    			var second1 = difftime-3600*hours-60*minutes;
    			diff += (hours>10?"":"0") + hours + "小时";
    			diff += (minutes>10?"":"0") + minutes + "分";
    			diff += (second1>10?"":"0") + second1 + "秒";    
    			hastime.innerHTML= diff;   
    		}
            
    		if(document.getElementById("hfStart").value == 2)
    		{
    			window.parent.parent.location = '/RailExamBao/Online/Exam/ExamSuccess.aspx?IsResult=1&ExamType=1&ExamResultID='+document.getElementById("hfResultID").value;
    		}
    		else if(document.getElementById("hfStart").value == "-1")
    		{
    			window.parent.parent.location = '/RailExamBao/Common/Error.aspx?error=考试出现异常，请重启微机重新登录考试！';  		
    		}
    		
    		s= setTimeout("showTime()", 1000);

    		photoCount = photoCount + 1;
			
    		if(photoCount == 60 || photoCount == 580) 
    		{
    			if(window.parent.parent.document.getElementById('cap1')) 
    			{
    				if(window.parent.parent.document.getElementById('cap1').camCount >0)
    				{
    					window.parent.parent.paizhao();
    				}
    			}
    		}
    		
    		var bTest=NetPing();
            if(!bTest)
            {
            	window.parent.parent.close();
            }
    	}	
    	
    	 function CheckStatus(url)
        {
    	 	var XMLHTTP = new ActiveXObject("Microsoft.XMLHTTP");
    	 	XMLHTTP.open("HEAD", url, false);
    	 	XMLHTTP.send();
    	 	return XMLHTTP.status == 200;
        }
        
        function NetPing()
        {
              return CheckStatus("/RailExamBao/Test.aspx");
        }	
		
    	var selColor='#ffcccc';//选中色#EBF9FD

    	function CheckStyle(object)
    	{
    		var obj = object;   
    		var startid= obj.id; 
    		var bigobj = document.getElementsByName(obj.name);
    		var n = bigobj.length;

    		var needEmpty = 0; 
    		if(obj.type == "checkbox" && !obj.checked) 
    		{
    			var m = 0;
    			for(var i=0; i<n ; i++)
    			{
    				obj = bigobj[i];
    				if(!obj.checked) {
    					m = m + 1;
    				}
    			}
    			if(m==n) 
    			{
    				needEmpty = 1;
    			}
    		}
          
           
    		var str = startid.split('-');
        	
    		if(needEmpty ==0) 
    		{
    			document.getElementById("Item"+str[2]+str[3]).style.backgroundColor=selColor;
    			window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item"+str[2]+str[3]).style.backgroundColor=selColor;
    		} 
    		else
    		{
    			document.getElementById("Item"+str[2]+str[3]).style.backgroundColor='#ffffff';
    			window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item"+str[2]+str[3]).style.backgroundColor='#ffffff';
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
    					if(needEmpty ==0) 
    					{
    						obj.style.backgroundColor = selColor;
    					} 
    					else 
    					{
    						obj.style.backgroundColor = '#ffffff';;
    					}
    				}
    				else
    				{
    					if(needEmpty ==0)
    					{
    						obj.style.backgroundColor = selColor;
    					} 
    					else 
    					{
    						obj.style.backgroundColor = '#ffffff';;
    					}
    				}	 
    			}
    		}

            var location = window.location.href;
    		var newurl = location.substring(0, location.indexOf("RailExamBao/") + 12);

//    		  $.ajax({
//              url: 
//              	   newurl+"Login.aspx",
//              type: 'GET',
//              complete: function(response) {
//              	try 
//              	{
//                   if(response.status != 200) 
//                    {
//                        alert("无法连接服务器，请检查网络并重启客户端微机。网络正常后，重新登录进行考试！");
//                    }
//              	} 
//              	catch(e) 
//              	{
//                     alert("无法连接服务器，请检查网络并重启客户端微机。网络正常后，重新登录进行考试！");
//              	} 
//              }
//            });
    		
    		if(!getURL(newurl+"Login.aspx")) 
    		{
    			alert("无法连接服务器，请检查网络并重启客户端微机。网络正常后，重新登录进行考试！");
    			return;
    		}

    		ItemAnswerChangeCallBack.callback(str[1],str[4],object.checked,object.type); 
    	}

        function getURL(url)
        {
            var xmlhttp = new ActiveXObject( "Microsoft.XMLHTTP");
            xmlhttp.open("GET", url, false);
            xmlhttp.send();
            if(xmlhttp.readyState==4) {
                if(xmlhttp.Status != 200) 
                {
                	return false;
                }
            	return true;
            }
            return true;
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
    						document.getElementById("Item"+str[2]+str[3]).style.backgroundColor='#ffcccc';
                                  
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
    				document.getElementById("Item"+str[2]+str[3]).style.backgroundColor=selColor;
                 	  
    				if(window.parent.parent.frames["ifExamNavigation"].document.getElementById("Item"+str[2]+str[3])) {
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
       	 
    		if(itemID != "") 
    		{
    			var str = itemID.split('-');
    			//alert(itemID);
    			ItemAnswerChangeCallBack.callback(str[1],"",true,"radio"); 
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
        <ComponentArt:CallBack ID="Callback1" runat="server" PostState="true" OnCallback="Callback1_Callback"
            RefreshInterval="10000">
            <Content>
                <asp:HiddenField ID="hfStart" runat="server" />
                <asp:HiddenField ID="hfResultID" runat="server" />
            </Content>
        </ComponentArt:CallBack>
        <ComponentArt:CallBack ID="ItemAnswerChangeCallBack" runat="server" PostState="true"
            OnCallback="ItemAnswerChangeCallBack_Callback">
        </ComponentArt:CallBack>
        <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldMaxCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldExamTime" runat="server" />
        <asp:HiddenField ID="HfExamTime" runat="server" />
        <input id="strreturnAnswer" type="hidden" name="strreturnAnswer" />
        <asp:HiddenField ID="hfReturnAnswer" runat="server" />
        <asp:HiddenField runat="server" ID="hfNeed"/>
    </form>
</body>
</html>
