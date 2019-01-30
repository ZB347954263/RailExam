<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainPlanPostClassEdit.aspx.cs" Inherits="RailExamWebApp.TrainManage.TrainPlanClassPostEdit" %>
<%@ Register TagPrefix="uc1" TagName="Date" Src="~/Common/Control/Date/Date.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />

    <title>新增计划培训班</title>
      <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
      <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
      
      <script type="text/javascript">
      function check() {
   
      	var spanPost = document.getElementById("spanPost");
      	if( spanPost!=null)
      		{
      	if(spanPost.className=="require") {
      		var dropPost = document.getElementById("dropPlanPost");
      		if (dropPost.value == "0") {
      			alert("请选择职名！");
      			return false;
      		}
      	}
      	}
      	var txtPostClassName=document.getElementById("txtPostClassName");
      	if(txtPostClassName.value.replace(" ","")=="") {
      		alert("请输入培训班名称！");
      		txtPostClassName.focus();
      		return false;
      	}
      	if(txtPostClassName.value.length>100) {
      		alert("输入长度超过100个字符!");
      		txtPostClassName.focus();
      		return false;
      	}
      	var beginDate = document.getElementById("beginDate_DateBox");
    	var date = document.getElementById("beginDate_compareValidator");
    	if(date.style.display!="none") {
    		alert("开始时间格式不正确！");
    		beginDate.focus();
    		return false;
    	}
    	if(beginDate.value.indexOf("-")<0 && beginDate.value.indexOf("/")<0) {
    		alert("开始时间不能为空！");
    		beginDate.focus();
    		return false;
    	}
      	
      	var endDate = document.getElementById("endDate_DateBox");
        var date1 = document.getElementById("endDate_compareValidator");
    	if(date1.style.display!="none") {
    		alert("结束时间格式不正确！");
    		endDate.focus();
    		return false;
    	}
    	if(endDate.value.indexOf("-")<0 && endDate.value.indexOf("/")<0) {
    		alert("结束时间不能为空！");
    		endDate.focus();
    		return false;
    	}
      if(beginDate.value>endDate.value) {
      	alert("开始日期不能大于结束日期！");
      	return false;
      }
      }
      </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 530px">
                <div id="location">
                    <div id="current" runat="server">
                        新增计划培训班</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable" width="400px">
                    <tr>
                        <td style="width: 15%">
                            计划职名<span id="spanPost" class="" runat="server">*</span></td>
                        <td>
                            <asp:DropDownList ID="dropPlanPost" runat="server" Width="170px">
                            </asp:DropDownList></td>
                        <td style="width: 15%">
                            培训班名称<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtPostClassName" runat="server"   width="170px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 15%">
                            开始时间<span class="require">*</span></td>
                        <td>
                              <uc1:Date ID="beginDate" runat="server" />
                          </td>
                        <td style="width: 15%">
                            结束时间<span class="require">*</span></td>
                        <td>
                              <uc1:Date ID="endDate" runat="server" />
                        </td>
                    </tr>
                </table>
                <div align="center" style="margin-top: 20px">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保  存" onClientClick="return check()" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="button" Text="关  闭" OnClientClick="javascript:window.close()" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
