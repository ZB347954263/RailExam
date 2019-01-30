<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeMatchEdit.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.EmployeeMatchEdit" %>

<%@ Register TagPrefix="uc1" TagName="Date" Src="~/Common/Control/Date/Date.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>新增技能竞赛</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
 
    function  checkInfo() { 
    	var match_date = document.getElementById("match_date_DateBox");
    	var date = document.getElementById("match_date_compareValidator");
    	if (date.style.display != "none") {
    		alert("竞赛时间格式不正确！");
    		match_date.focus();
    		return false;
    	} 
    	if(match_date.value.indexOf("-")<0 && match_date.value.indexOf("/")<0) {
    		alert("竞赛时间不能为空！");
    		match_date.focus();
    		return false;
    	}
    	var dropunit = document.getElementById("dropunit");
    	if (dropunit.value == "0") {
    		alert("请选择举办单位！");
    		dropunit.focus();
    		return false;
    	} 
    	var txtmatch_project = document.getElementById("txtmatch_project");
    	if (txtmatch_project.value == "") {
    		alert("竞赛项目不能为空！");
    		txtmatch_project.focus();
    		return false;
    	}
    	var dropmatch_type = document.getElementById("dropmatch_type");
    	if (dropmatch_type.value == "0") {
    		alert("请选择竞赛类别！");
    		dropmatch_type.focus();
    		return false;
    	}
        var txttotal_score = document.getElementById("txttotal_score");
    	if (txttotal_score.value == "") {
    		alert("总成绩不能为空！");
    		txttotal_score.focus();
    		return false;
    	}
    	  var txtmatch_rank = document.getElementById("txtmatch_rank");
    	if (txtmatch_rank.value == "") {
    		alert("竞赛名次不能为空！");
    		txtmatch_rank.focus();
    		return false;
    	}
    	
    	var memo = document.getElementById("txtmemo");
    	if (memo.value.length>100) {
    		alert("备注不能超过100个字！");
    		txtcontent_brief.focus();
    		return false;
    	}
    }
      function checkNum(obj)
      {
      	var reg = /^\d+(\.\d{1,2})?$/ ;
      	if (obj.value != "")
      	{
      		if (!reg.test(obj.value))
      		{
      			alert("请输入数字或两位有效小数！");
      			obj.value = "";
      			obj.focus();
      			return;
      		}
 
      	}
      }
      function checkInt(obj) {
      	var reg = /^\d+()?$/ ;
      	if (obj.value != "")
      	{
      		if (!reg.test(obj.value))
      		{
      			alert("请输入整数！");
      			obj.value = "";
      			obj.focus();
      			return;
      		}
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
                        新增技能竞赛</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%;">
                            竞赛时间<span class="require">*</span>
                        </td>
                        <td style="width: 39%;">
                            <uc1:Date ID="match_date" runat="server" />
                        </td>
                        <td style="width: 13%;">
                            举办单位<span class="require">*</span>
                        </td>
                        <td style="width: 41%;">
                            <asp:DropDownList ID="dropunit" runat="server" Width="120px">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="全国">全国</asp:ListItem>
                                <asp:ListItem Value="铁道部">铁道部</asp:ListItem>
                                <asp:ListItem Value="铁路局">铁路局</asp:ListItem>
                                <asp:ListItem Value="站段">站段</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            竞赛项目<span class="require">*</span>
                        </td>
                        <td style="width: 39%;">
                            <asp:TextBox ID="txtmatch_project" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td style="width: 13%;">
                            竞赛类别<span class="require">*</span>
                        </td>
                        <td style="width: 41%;">
                            <asp:DropDownList ID="dropmatch_type" runat="server" Width="120px">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="单项">单项</asp:ListItem>
                                <asp:ListItem Value="全能">全能</asp:ListItem>
                                <asp:ListItem Value="团体">团体</asp:ListItem>
                                <asp:ListItem Value="其他">其他</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            总成绩<span class="require">*</span>
                        </td>
                        <td style="width: 39%;">
                            <asp:TextBox ID="txttotal_score" runat="server" MaxLength="8" Width="120px" onblur="checkNum(this)"></asp:TextBox>
                        </td>
                        <td style="width: 13%;">
                            理论成绩
                        </td>
                        <td style="width: 41%;">
                            <asp:TextBox ID="txtlilun_score" runat="server" MaxLength="8" Width="120px" onblur="checkNum(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            实作成绩
                        </td>
                        <td style="width: 39%;">
                            <asp:TextBox ID="txtshizuo_score" runat="server" MaxLength="8" Width="120px" onblur="checkNum(this)"></asp:TextBox>
                        </td>
                        <td style="width: 13%;">
                            竞赛名次<span class="require">*</span>
                        </td>
                        <td style="width: 41%;">
                            <asp:TextBox ID="txtmatch_rank" runat="server" MaxLength="4" onblur="checkInt(this)"
                                Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; height: 56px;">
                            备注
                        </td>
                        <td colspan="3" style="height: 56px">
                            <asp:TextBox ID="txtmemo" runat="server" MaxLength="100" Width="98%" Rows="3" TextMode="MultiLine"></asp:TextBox>
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
