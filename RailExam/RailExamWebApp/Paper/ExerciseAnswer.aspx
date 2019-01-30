<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExerciseAnswer.aspx.cs" Inherits="RailExamWebApp.Paper.ExerciseAnswer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��ϰ</title>
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
				alert("�����������⹦�ܼ���");				
				return;	
			}

			if ((window.event.altKey) &&  
				((window.event.keyCode == 37) ||    //���� Alt+ ����� �� 
				 (window.event.keyCode == 39)))   //���� Alt+ ����� �� 
			{ 
				event.returnValue = false; 
			}
				/* ע���⻹�������������� Alt+ ������� 
				��Ϊ Alt+ ��������������ʱ����ס Alt �����ţ� 
				�������������������η�����ʧЧ�ˡ��Ժ��� 
				����λ�������������� Alt ���ķ��������֪��*/ 
			if ( (event.keyCode == 116) ||                  //���� F5 ˢ�¼� 
				 (event.ctrlKey && event.keyCode == 82))  //Ctrl + R 
			{ 
				 event.keyCode = 0; 
				 event.returnValue = false; 
			} 
			if (event.keyCode == 122)  //����F11 
			{
				event.keyCode = 0;
				event.returnValue = false;
			}  
			if (event.ctrlKey && event.keyCode == 78)  //���� Ctrl+n 
			{
				event.returnValue = false;   
			}
			if (event.shiftKey && event.keyCode == 121)  //����shift+F10 
			{
				event.returnValue = false;  
			}
			 
			if (window.event.srcElement.tagName == "A" && window.event.shiftKey)    //���� shift ���������¿�һ��ҳ 
			{	
				window.event.returnValue = false; 
			}          
			if ((window.event.altKey) && (window.event.keyCode == 115))             //����Alt+F4 
			{  
				window.showModelessDialog("about:blank","","dialogWidth:1px;dialogheight:1px"); 
				return false; 
			} 
		} 	

		function Save()			
		{
		    if(confirm("��ȷ�����ύ��ϰ�͹ر���"))
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
		        if(confirm("����δ��ɵ����⣬��Ҫ�ύ��ϰ��"))
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
                if(confirm('��ȷ��Ҫ�ύ��ϰ�� ? '))
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
