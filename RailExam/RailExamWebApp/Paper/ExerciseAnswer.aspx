<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExerciseAnswer.aspx.cs" Inherits="RailExamWebApp.Paper.ExerciseAnswer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>练习</title>
     <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>

    <script type="text/javascript">        
        //<![CDATA[
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
				window.showModelessDialog("about:blank","","dialogWidth:1px;dialogheight:1px"); 
				return false; 
			} 
		} 	

		function Save()			
		{
		    if(confirm("您确定不提交练习就关闭吗？"))
	        {
               top.window.close();	
	        }
	        else
	        {
	            return;
	        }
		}
		
        var answers = [];
		function SaveRecord()			
		{  
		    answers = [];
		    var ak= getAnswers();
            
		    if(getFinishedItemsCount() != document.getElementById("hfPaperItemsCount").value)
		    {		        
		        if(confirm("您有未完成的试题，仍要提交练习吗？"))
		        {
                    form1.strreturnAnswer.value=  ak;
                    form1.submit();
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
                    form1.strreturnAnswer.value=  ak;
		            form1.submit();
		           
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="95%">
                <tr>
                    <td align="center" style="width: 80%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                    <td align="right">
                        <asp:Label ID="lblTitleRight" runat="server"></asp:Label>
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
