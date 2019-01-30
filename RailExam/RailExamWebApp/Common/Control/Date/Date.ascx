<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Date.ascx.cs" Inherits="RailExamWebApp.Common.Control.Date.Date" %>
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

		var selectValue = window.showDialog(pageName,left+top+'dialogWidth:200px;dialogHeight:230px');//"时间格式不正确！"
		if(selectValue != null)
		{
			document.getElementById(ctrlid).value = selectValue;
//			if(AutoPostBack == "true")
//			{
//				document.forms[0].submit();
//			}
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
	    
	    return today.getFullYear() + "-" + intMonth + "-" + intDate;
	}
	
	function isDateString(sDate)
    {
        var iaMonthDays = [31,28,31,30,31,30,31,31,30,31,30,31];
        var iaDate = new Array(3);
        var year, month, day;

        if (arguments.length != 1)
            return false;

        iaDate = sDate.toString().split("-");

        if (iaDate.length != 3)
        {
            return false;
        }
        if (iaDate[1].length > 2 || iaDate[2].length > 2)
        {
            return false;
        }

        year = parseFloat(iaDate[0]);
        month = parseFloat(iaDate[1]);
        day = parseFloat(iaDate[2]);

        if (year < 1900 || year > 2100)
            return false;
        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
            iaMonthDays[1] = 29;
        if (month < 1 || month > 12)
            return false;
        if (day < 1 || day > iaMonthDays[month - 1])
            return false;

        return true;
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
<asp:TextBox ID="DateBox" Width="87px" runat="server" CssClass="Date_Text" BorderStyle="Groove"></asp:TextBox>
<input onclick="selectRecord('../Common/Control/Date/Date.htm','<%=DateBox.ClientID%>')"
    type="button" class="Date_Button"  style="<%=(string)ViewState["DateButtonStyle"]%>"/>
<asp:CompareValidator ID="compareValidator" runat="server" ErrorMessage="日期格式不正确！"
    Type="Date" Operator="DataTypeCheck" ControlToValidate="DateBox" Display="Dynamic"></asp:CompareValidator>
<asp:Label ID="labelSetEmpty" runat="server" Visible="False"></asp:Label>
 <%-- ,'<%=ViewState["AutoPostBack"].ToString()%>'   --%>