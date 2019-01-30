<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TaskJudge.aspx.cs" Inherits="RailExamWebApp.Task.TaskJudge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>作业信息 - 批阅作业</title>
    <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>
    <script type="text/javascript">    
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
		
		function btnSave_onClick()			
		{
		    $F("hfJudgeData").value = getJudge();
		}
		
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
		
		function btnClose_onClick()
		{
		    self.close();
		}
		
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        作业信息
                    </div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        批阅作业
                    </div>
                </div>
                <div id="welcomeInfo">
                </div>
                <div id="button">
                </div>
            </div>
            <div id="content">
                <div id="contentHead">
                    <asp:Label runat="server" ID="labeltitle"></asp:Label>
                    <asp:Label runat="server" ID="labelTitleRight"></asp:Label>
                </div>
                <div id="mainContent">
                    <table width="100%" class="contentTable">
                        <tr class="tableHeadFont">
                            <td style="text-align: right" width="13%">
                                作业人姓名
                            </td>
                            <td style="text-align: left" width="20%">
                                <asp:Label ID="lblExamineeName" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right" width="13%">
                                答题开始时间
                            </td>
                            <td style="text-align: left" width="20%">
                                <asp:Label ID="lblExamBeginDateTime" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: right" width="13%">
                                答题结束时间
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblExamEndDateTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="tableHeadFont">
                            <td style="text-align: right">
                                批阅人
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
                            <td colspan="6">
                                <%FillPaper(); %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="text-align: center;">
                                <input id='btnSave' name='btnSave' class="button" onclick='btnSave_onClick();' type='submit'
                                    value='保  存' />&nbsp;&nbsp;&nbsp;&nbsp;
                                <input id='btnAuto' name='btnAuto' class="button" onclick='btnAuto_onClick();' type='button'
                                    value='自动评分' />&nbsp;&nbsp;&nbsp;&nbsp;
                                <input id='btnClose' name='btnClose' class="button" onclick='btnClose_onClick();'
                                    type='button' value='关  闭' />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div>
            <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
            <asp:HiddenField ID="hfJudgeData" runat="server" />
        </div>
    </form>
</body>
</html>
