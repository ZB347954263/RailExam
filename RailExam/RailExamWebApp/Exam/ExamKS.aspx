<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamKS.aspx.cs" Inherits="RailExamWebApp.Exam.ExamKS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ޱ���ҳ</title>
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
				return false;
			}
		}

		function Save()
		{
		    if(confirm("��ȷ�����ύ�Ծ�͹ر���"))
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
		
        var answers = [];
        
		function SaveRecord()
		{
		    answers = [];
		    var ak = getAnswers();

		    if(getFinishedItemsCount() != document.getElementById("hfPaperItemsCount").value)
		    {
		        if(!confirm("����δ��ɵ����⣬��Ҫ�ύ�Ծ���"))
		        {  		       
		          return;
		        }  	        
		        if(!confirm("һ���ύ�Ծ����Ͳ������޸��Ծ��ˣ�"))
		        {  		          
		            return;
		        }		        
		         if(!confirm("�ٴ�ȷ�����Ƿ��ύ�Ծ�"))	       
		        {   		        
		            return;
		        }
		        
		        form1.strreturnAnswer.value = ak;
                form1.submit(); 		               
		    }
		    else
		    {
                if(!confirm('��ȷ��Ҫ�ύ����� ? '))
                {
                     return;
		        }
                
               if(!confirm("һ���ύ�Ծ����Ͳ������޸��Ծ��ˣ�"))
		        {  	     return;
		        }	          
		         if(!confirm("�ٴ�ȷ�����Ƿ��ύ�Ծ�"))	       
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
				case 0: day = "��";break;
				case 1: day = "һ";break;
				case 2: day = "��";break;
				case 3: day = "��";break;
				case 4: day = "��";break;
				case 5: day = "��";break;
				case 6: day = "��";break;
			}
			
			nowtime.innerHTML =  year + "��" + month + "��" + date + "�� ����" + day + " "+(hour<12?"����":"����") + hour + ":" + minute + ":" + second;
						
			var difftime = Math.floor(examtime);
			 
			if(difftime < 0)
			{
			   if(document.getElementById("hfExamed").value != "true")
			   {
			     hastime.innerHTML= "����ʱ���ѵ������ύ�Ծ�!";   
			     answers = [];
		         var ak = getAnswers();
		         form1.strreturnAnswer.value = ak;
                 form1.submit();           
			   }
			}
			else			
			{			
            var diff = "���Ե���ʱ��"       
            var hours = Math.floor(difftime%(60*60*24)/3600);
            var minutes = Math.floor((difftime%(60*60*24)-3600*hours)/60);
            var second1 = difftime-3600*hours-60*minutes;
            diff += (hours>10?"":"0") + hours + "Сʱ";
            diff += (minutes>10?"":"0") + minutes + "��";
            diff += (second1>10?"":"0") + second1 + "��";    
            hastime.innerHTML= diff;     
            }         				
			setTimeout("showTime()", 1000);				
		}	
		
		var selColor='#EBF9FD';//ѡ��ɫ
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
    </script>

</head>
<body onload="showTime()">
    <form id="form1" runat="server">
        <div> 
            <div class="time" id="nowtime" style="display:none;">
            </div>
            <div class="time" id="hastime" style="display:none;">
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
        <input id="strreturnAnswer" type="hidden" name="strreturnAnswer" />
       <asp:HiddenField ID="HiddenFieldExamTime" runat="server" />
        <asp:HiddenField ID="HfExamTime" runat="server" /> 
         <asp:HiddenField  ID="hfExamed" runat="server" />  
    </form>
       <script type="text/javascript">
        var tdArray = document.getElementsByTagName("td");
       for( var i =0; i<tdArray.length; i++ )
       {
            tdArray[i].width = screen.availWidth;
       }
       </script>  
</body>
</html>
