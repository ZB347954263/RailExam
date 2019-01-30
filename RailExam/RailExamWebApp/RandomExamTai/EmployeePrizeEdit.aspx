<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePrizeEdit.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeePrizeEdit" %>
<%@ Register TagPrefix="uc1" TagName="Date" Src="~/Common/Control/Date/Date.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
         <base target="_self" />
    <title>新增奖惩情况</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
 
    function  checkInfo() {  
    	var prize_date = document.getElementById("prize_date_DateBox");
    	var date = document.getElementById("prize_date_compareValidator");
    	if (date.style.display != "none") {
    		alert("奖惩日期格式不正确！");
    		prize_date.focus();
    		return false;
    	}
    	if(prize_date.value.indexOf("-")<0 && prize_date.value.indexOf("/")<0) {
    		alert("奖惩日期不能为空！");
    		prize_date.focus();
    		return false;
    	}
    	var dropprize_type = document.getElementById("dropprize_type");
    	if (dropprize_type.value == "0") {
    		alert("请选择奖惩类别！");
    		dropprize_type.focus();
    		return false;
    	}
    	var txtcontent_brief = document.getElementById("txtcontent_brief");
    	if (txtcontent_brief.value == "") {
    		alert("事迹概况不能为空！");
    		txtcontent_brief.focus();
    		return false;
    	}

    	alert(txtcontent_brief.value.length);
    	if(txtcontent_brief.value.length >500) {
    		alert("事迹概况不能超过500个字！");
    		txtcontent_brief.focus();
    		return false;
    	}
    	
    	var txtprize_result = document.getElementById("txtprize_result");
    	if (txtprize_result.value.length>200) {
    		alert("奖惩结果不能超过200个字！");
    		txtcontent_brief.focus();
    		return false;
    	}
    	
    	var memo = document.getElementById("txtmemo");
    	if (memo.value.length>100) {
    		alert("备注不能超过100个字！");
    		txtcontent_brief.focus();
    		return false;
    	}
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 610px">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current" runat="server">
                        新增奖惩情况</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%;">
                            奖惩日期<span class="require">*</span>
                            
                        </td>
                        <td style="width: 39%;">
                            <uc1:Date ID="prize_date" runat="server" />
                        </td>
                        <td style="width: 13%;">
                            奖惩类别<span class="require">*</span>
                        </td>
                        <td style="width: 41%;">
                               <asp:DropDownList ID="dropprize_type" runat="server" Width="120px">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                               <asp:ListItem Value="1">奖励</asp:ListItem>
                               <asp:ListItem Value="2">惩罚</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                         奖惩文号
                        </td>
                        <td style="width: 39%;">
                            <asp:TextBox ID="txtprize_no" runat="server" Width="98%" MaxLength="20"></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                          <td style="width: 10%;">
                            事迹概况 <span class="require">*</span>
                        </td>
                        <td style="width: 90%;" colspan="3">
                            <asp:TextBox ID="txtcontent_brief" runat="server" Rows="3" Width="100%" MaxLength="200" TextMode="MultiLine"></asp:TextBox><br/>
                             （500个字以内）
                        </td>
                    </tr>
                     <tr>
                          <td style="width: 10%;">
                            奖惩结果 
                        </td>
                        <td style="width: 90%;" colspan="3">
                            <asp:TextBox ID="txtprize_result" runat="server" Rows="3" Width="100%" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                            <br/>（200个字以内） 
                        </td>
                    </tr>
                      <tr>
                          <td style="width: 10%;">
                            备注 
                        </td>
                        <td style="width: 90%;" colspan="3">
                            <asp:TextBox ID="txtmemo" runat="server" Width="100%" Rows="3" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                             <br/>（100个字以内）  
                        </td>
                    </tr>
                   
                </table>
                <div align="center" style="margin-top: 20px">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保  存" OnClientClick="return checkInfo()"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="button" Text="关  闭" OnClientClick="javascript:window.close()" />
                </div>
            </div>
        </div>
      
    </form>
</body>
</html>
