<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateTimeUC.ascx.cs" Inherits="RailExamWebApp.Common.Control.Date.DateTimeUC" %>
<link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
	function selectRecord(pageName, ctrlid)//, AutoPostBack
	{
		var l = event.clientX+event.offsetX+document.body.scrollLeft;
		var t = event.clientY+event.offsetY+document.body.scrollTop+130;

		var left = 'dialogLeft:'+l+'px;';
		var top = 'dialogTop:'+t+'px;';
		
		var date = document.getElementById(ctrlid).value;
		

		if(date == "")   //如果时间为空
		{
		    var date = todayDate();
		    document.getElementById(ctrlid).value = date;
		    pageName = pageName + '?url=' + date;
		    //pageName = pageName + '?url=' + clockon();
		}
		else if(! isDateString(date))   //如果时间输入不正确
		{
		    var date = todayDate();
		    document.getElementById(ctrlid).value = date;
		    pageName = pageName + '?url=' + date;
		}
		else
		{
		    pageName = pageName + '?url=' + document.getElementById(ctrlid).value;
		}

		var selectValue = window.showDialog(pageName,left+top+'dialogWidth:198px;dialogHeight:228px');//"时间格式不正确！"
		if(selectValue != null)
		{
		 
			document.getElementById(ctrlid).value = selectValue+" 09:00:00"; 
		}
	}
	
	function todayDate()
	{
	    var today = new Date();
	    var intMonth = today.getMonth() + 1;
	    var intDate = today.getDate();
	    if(intMonth < 10)
	    {
	        intMonth = "0" + intMonth;
	    }
	    if(intDate < 10)
	    {
	        intDate = "0" + intDate;
	    }
	    
	    return today.getFullYear() + "-" + intMonth + "-" + intDate+" 09:00:00";
	}
	
	function isDateString(sDate)
    {//判断格式不正确 
    
   
     var RegExp = /^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){2}$/;
    
      return (RegExp.test(sDate));
    
    }
	
	function showDialog(pageName, view)
	{
		if(view == null)
		{
			view = 'dialogWidth:800px;dialogHeight:600px';
		}
	    //var idpath = '../../Common/WebPage/DialogPage.aspx?pageName='+pageName;
	    
		return window.showModalDialog(pageName,'','status:no;help:no;'+view);
	}
	
	function MsgBlur(comid)
	{
		document.getElementById(comid).value = '';
	}
</script>
<asp:TextBox ID="DateBox" Width="115px" runat="server" CssClass="Date_Text" BorderStyle="Groove"></asp:TextBox>
<input onclick="selectRecord('../Common/Control/Date/Date.htm','<%=DateBox.ClientID%>')"
    type="button" class="Date_Button" style="<%=(string)ViewState["DateButtonStyle"]%>" />
 
    <asp:RegularExpressionValidator runat="server" id="regularExpressionValidator" ErrorMessage='时间格式不正确'  ValidationExpression="^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){2}$" ControlToValidate="DateBox" Display="Dynamic"></asp:RegularExpressionValidator>
    
<asp:Label ID="labelSetEmpty" runat="server" Visible="False"></asp:Label>
 <%-- ,'<%=ViewState["AutoPostBack"].ToString()%>'   --%>