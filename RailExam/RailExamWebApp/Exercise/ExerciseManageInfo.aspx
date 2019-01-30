<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExerciseManageInfo.aspx.cs" Inherits="RailExamWebApp.Exercise.ExerciseManageInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>练习</title>
    <link href="/RailExamBao/Online/style/ExamPaper.css" type="text/css" rel="stylesheet" /> 
    <script src="../Common/JS/JSHelper.js" type="text/javascript"></script>
    <script type="text/javascript">        
        /*function document.oncontextmenu()
        {
            event.returnValue = false;
        }
        
        function window.onhelp()
        {
            return false
        }
         
        function document.onkeydown()
		{
			if(window.event.shiftKey || window.event.altKey || window.event.ctrlKey || window.event.tabKey)
			{
				event.keyCode = 0;
				event.returnValue = false;
				alert("不可以用特殊功能键！");
				return;	
			}

			if ((window.event.altKey) &&  
				((window.event.keyCode == 37) ||    //屏蔽 Alt+ 方向键 ← 
				 (window.event.keyCode == 39)))     //屏蔽 Alt+ 方向键 → 
			{ 
				event.returnValue = false; 
			}
			
			if ( (event.keyCode == 116) ||                  //屏蔽 F5 刷新键 
				 (event.ctrlKey && event.keyCode == 82))    //Ctrl + R 
			{ 
				event.keyCode = 0; 
				event.returnValue = false; 
			} 
			if (event.keyCode == 122)  //屏蔽F11 
			{
				event.keyCode = 0;
				event.returnValue = false;
			}
			if (event.ctrlKey && event.keyCode == 78)    //屏蔽 Ctrl+n 
			{
				event.returnValue = false;
			}
			if (event.shiftKey && event.keyCode == 121)  //屏蔽 Shift+F10 
			{
				event.returnValue = false;
			}
			 
			if (window.event.srcElement.tagName == "A" && window.event.shiftKey)    //屏蔽 Shift 加鼠标左键新开一网页 
			{
				window.event.returnValue = false;
			}
			if ((window.event.altKey) && (window.event.keyCode == 115))             //屏蔽Alt+F4 
			{
				window.showModelessDialog("about:blank","","dialogWidth:1px;dialogheight:1px"); 
				return false;
			}
		}
        */
        var answers = [];
        
		function SaveRecord()
		{
		    answers = [];
		    var ak = getAnswers();
            
		    if(getFinishedItemsCount() != document.getElementById("hfPaperItemsCount").value)
		    {
		        if(confirm("您有未完成的题目，你确定要提交练习吗？"))
		        {
                    form1.strreturnAnswer.value = ak;
                    form1.submit();
                    form1.strreturnAnswer.value = "";
		            //top.window.close();
		        }
		        else
		        {
		            return;
		        }
		    }
		    else
		    {
                if(confirm('您确定要提交练习吗 ? '))
                {
                    form1.strreturnAnswer.value = ak;
		            form1.submit();
		            form1.strreturnAnswer.value = "";
                    //top.window.close();
                }
                else
		        {
		            return;
		        }
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
		    
		    //alert(res);
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
		var selColor='#ffcccc';//选中色#EBF9FD
        function CheckStyle(object)
        {		
          var obj = object;   
          var startid= obj.id; 
          var bigobj = document.getElementsByName(obj.name);
          var n = bigobj.length;
          
          var str = startid.split('-');
          document.getElementById("Item-"+str[1]+"-"+str[3]).style.backgroundColor=selColor;
          
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
    </script>
</head>
<body oncontextmenu='return false' ondragstart='return false' onselectstart='return false'
    oncopy='document.selection.empty()' onbeforecopy='return false'>
    <form id="form1" runat="server">
        <div style=" width:100%">
            <% FillPaper(); %>
        </div>
        <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
        <input id="strreturnAnswer" type="hidden" name="strreturnAnswer" />
        <asp:HiddenField  ID="hfCannotSeeAnswer" runat="server"/>
    </form>
</body>
</html>
