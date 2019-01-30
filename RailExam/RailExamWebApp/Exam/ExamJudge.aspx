<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExamJudge.aspx.cs" Inherits="RailExamWebApp.Exam.ExamJudge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>手工评卷</title>
    <link href="../Online/style/ExamPaperResult.css" type="text/css" rel="stylesheet" />
    <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>
    <script type="text/javascript">    
        //屏蔽按钮 
		function document_onkeydown() 
		{
			if(event.shiftKey || event.altKey || event.ctrlKey)
			{
				event.keyCode = 0;
				event.returnValue = false;
				//alert("不可以用特殊功能键！");
				try
				{
					//top.fraRight.ShowMessageBox("不可以用特殊功能键！");
				}
				catch(e)
				{
				}
				return;
			}
		}
		
		//保存按钮点击事件处理函数
		function btnSave_onClick()			
		{  
		     var ExamineeName=document.getElementById("HiddenFieldEmployeeName").value;
		     var OldScore=document.getElementById("HiddenFieldScore").value;
		   
		     var ret = window.showModalDialog('ExamResultUpdateDetail.aspx?ExamineeName='+escape(ExamineeName)+'&&OldScore='+escape(OldScore), 
                    '', 'help:no; status:no; dialogWidth:800px;dialogHeight:600px');              
                
		    if(ret!=null)
		    { 
		     $F("HiddenFieldUpdateCause").value=ret;
		     $F("hfJudgeData").value = getJudge();
		    }
		    else
		    {
		      return;
		    }   
		}
		
		//自动评分按钮点击事件处理函数
		function btnAuto_onClick()
		{
		    var spans = document.getElementsByTagName("span");
		    var spaninfo;
		    var items = [];
		    
		    for(var i=0; i<spans.length; i++)
		    {
		        spaninfo = spans[i].id.split('-');
		        if(spaninfo.length == 3)
		        {
		            items[spaninfo[1]] = new Array(2);
		        }
		    }		    
		    
		    for(var i=0; i<spans.length; i++)
		    {   
		        spaninfo = spans[i].id.split('-');
	            if(spaninfo.length == 3 && spaninfo[2] == 0)
	            {
	                items[spaninfo[1]][0] = spans[i].innerText;
	            }
	            else if(spaninfo.length == 3 && spaninfo[2] == 1)
	            {
	                items[spaninfo[1]][1] = spans[i].innerText;
	            }
		    }	
		    
		    var btns;
		    var theCorrectBtn;
		    var theIncorrectBtn;
		    for(var i=0; i<items.length; i++)
		    {
		        if(items[i] && items[i].length == 2)
		        {
		            btns = document.getElementsByName("rbnJudge-" + i);
		            for(var j=0; j<btns.length; j++)
		            {
		                if(btns[j].rate == 100)
		                {
		                    theCorrectBtn = btns[j];
		                }
		                if(btns[j].rate == 0)
		                {
		                    theIncorrectBtn = btns[j];
		                }
		            }
		            		            
		            if(theCorrectBtn && items[i][0] == items[i][1])
		            {
    		            theCorrectBtn.checked = true;
    		            theCorrectBtn.onclick();
		            }
		            else if(theIncorrectBtn && items[i][0] != items[i][1])
		            {
    		            theIncorrectBtn.checked = true;
    		            theIncorrectBtn.onclick();
		            }
		        }
		    }
		}
		
		//关闭按钮点击事件处理函数
		function btnClose_onClick()
		{
		    self.close();
		}
		
		//评卷状态复选框点击事件初恋函数
		function rbnJudgeStatus_onClick(btn)
		{
		    var theRate = btn.rate;
		    var theScore = btn.parentNode.parentNode.score;
		    var theScoreInputBox = $F("txtScore-" + btn.id.split('-')[1]);
		    var scoreString = ((parseFloat(theRate) * parseFloat(theScore) / 100) + 0.001).toString()
		    
		    if(theScoreInputBox)
		    {
		        theScoreInputBox.value = scoreString.substring(0, scoreString.length-1);
		    }
		}
		
		//获取评卷结果
		function getJudge()
		{
		    var inputs = document.getElementsByTagName("input");
	        var judges = [];
		    var itemIds = [];
		    
		    for(var i=0; i<inputs.length; i++)
		    {
                // itemIds: 1 for PaperItemId, 2 for ItemId, 3 for SubItemId, 4 for SelectAnswerId
                itemIds = inputs[i].id.split('-');
                if(typeof(judges[itemIds[1]]) == "undefined")
                {
                    judges[itemIds[1]] = new Array();
                }
                if(inputs[i].disabled)
                {
                    continue;
                }
		        switch(inputs[i].type)
		        {
		            case "radio":
		            {
		                if(inputs[i].checked)
		                {
		                    judges[itemIds[1]].push(itemIds[2]);
		                }
		                
		                break;
		            }
		            case "text":
		            {
		                if(inputs[i])
		                {
		                    judges[itemIds[1]].push(inputs[i].value);
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
		    for(var i=0; i<judges.length; i++)
		    {
		        if(judges[i] && judges[i].length > 0)
		        {
		            res += i + "|" + judges[i].join("|") + "$"
		        }		        
		    }
		    
		    if(res.length > 1)
		    {
		        res = res.substring(0, res.length -1);
		    }
		    
		    //alert(res);
		    return res;
		}
		
		function logout()
		{
		    window.opener.form1.submit();
		    window.close();
		}
    </script>
</head>
<body onunload="logout()">
    <form id="form1" runat="server">
        <div>
            <table width="95%">
                <tr>
                    <td id="ExamName">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                    <td id="ExamInfo">
                        <asp:Label ID="lblTitleRight" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="ExamResultInfo">
                            <tr>
                                <td style="text-align: right">
                                    考生姓名
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblExamineeName" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    答题开始时间
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblExamBeginDateTime" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    答题结束时间
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblExamEndDateTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    评卷人
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblJudgerName" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    评卷开始时间
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblJudgeBeginDateTime" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    评卷结束时间
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblJudgeEndDateTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" colspan="5">
                                     成绩
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblScore" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <%FillPaper(); %>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input id='btnSave' name='btnSave' onclick='btnSave_onClick();' type='submit' value='保存' />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id='btnAuto' name='btnAuto' onclick='btnAuto_onClick();' type='button' value='自动评分' />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id='btnClose' name='btnClose' onclick='btnClose_onClick();' type='button' value='关闭' />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
        <asp:HiddenField ID="hfJudgeData" runat="server" />
         <asp:HiddenField ID="HiddenFieldUpdateCause" runat="server" />
          <asp:HiddenField ID="HiddenFieldEmployeeName" runat="server" />
           <asp:HiddenField ID="HiddenFieldScore" runat="server" />
           
    </form>
</body>
</html>
