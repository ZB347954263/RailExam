<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AttendExamNavigation.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.AttendExamNavigation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
        <link href="../Online/style/ExamPaper.css" type="text/css" rel="stylesheet" />
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
		
        var selColor='#ffcccc';//选中色#EBF9FD
        
        function StartStyle()
        {		
        	if( !window.parent.frames["ifExamDetailInfo"].frames["ifExamInfo"]) {
        		return;
        	}
            var inputs = window.parent.frames["ifExamDetailInfo"].frames["ifExamInfo"].document.getElementsByTagName("input");		    
		    var obj;
		    for(var j=0; j<inputs.length; j++)
		    {  
                 obj = inputs[j];
                 if(obj.checked)
		         { 
                      var startid= obj.id; 
                      var bigobj = window.parent.frames["ifExamDetailInfo"].frames["ifExamInfo"].document.getElementsByName(obj.name);
                      var n = bigobj.length;
                      
                      var str = startid.split('-');
                      document.getElementById("Item"+str[2]+str[3]).style.backgroundColor=selColor;
                   
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
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <% FillPaper();%>
    </form>
</body>
</html>
