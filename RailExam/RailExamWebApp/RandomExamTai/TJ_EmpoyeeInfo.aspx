<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TJ_EmpoyeeInfo.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.TJ_EmpoyeeInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
     
    <script type="text/javascript">
    var oldHref = "各单位工人总数统计";
    
    var ti = null;
     function employeeList_onChange() 
     {
     	var id =document.getElementById("employeeList").value;
     	if(id==0) 
     	{
     	    window.frames["iframeFirst"].location = "TJ_EmployeeWorkerByOrg.aspx?orgid="+document.getElementById("ddlOrg").value;
     	    clearInterval(ti);
     		oldHref = "各单位工人总数统计";
     		ti= setInterval("isLoad()",500);
     	}
     	else if(id==1) 
     	{
     	    window.frames["iframeFirst"].location = "TJ_EmployeeWorkerPostByOrg.aspx?orgid="+document.getElementById("ddlOrg").value;
     		clearInterval(ti);
     		oldHref = "各单位工种人数统计";
     		ti= setInterval("isLoad()",500);
     	}
     	else if(id==2) 
     	{
            window.frames["iframeFirst"].location = "TJ_EmployeeWorkerGroupHeaderByOrg.aspx?orgid="+document.getElementById("ddlOrg").value;
     	    clearInterval(ti);
     		oldHref = "各单位班组长人数统计";
     		ti= setInterval("isLoad()",500);
     	}
     	else if(id==3) 
     	{
     		clearInterval(ti);
            window.frames["iframeFirst"].location = "TJ_EmployeeWorkerStructureByOrg.aspx?orgid="+document.getElementById("ddlOrg").value;
     	 	oldHref = "各单位工人结构统计";
     		ti= setInterval("isLoad()",500);
     	}
     	else if(id==4) 
     	{
     		clearInterval(ti);
            window.frames["iframeFirst"].location = "TJ_EmployeeWorkerStructureByPost.aspx?orgid="+document.getElementById("ddlOrg").value;
     	 	oldHref = "各工种工人结构统计";
     		ti= setInterval("isLoad()",500);
     	}
     	else if(id==5) 
     	{
     		clearInterval(ti);
            window.frames["iframeFirst"].location = "TJ_EmployeeWorkerByEducation.aspx?orgid="+document.getElementById("ddlOrg").value;
     		oldHref = "各单位职教工作人员统计";
     		ti= setInterval("isLoad()",500);
     	}
     	else if(id==6) 
     	{
     		clearInterval(ti);
            window.frames["iframeFirst"].location = "TJ_EmployeePrize.aspx?orgid="+document.getElementById("ddlOrg").value;
     		oldHref = "各单位技能竞赛总体情况统计";
     	}
     	else if(id==7) 
     	{
     		clearInterval(ti);
            window.frames["iframeFirst"].location = "TJ_EmployeeOther.aspx?orgid="+document.getElementById("ddlOrg").value;
     		oldHref = "各单位其他持证情况统计";
     	}
     } 
     function isLoad() {
     	if(oldHref!=window.frames["iframeFirst"].document.title) {
     		document.getElementById("isLoad").style.display = "block";
     		 document.getElementById("rightContent").style.display = "none";
     	}
     	else {
     		document.getElementById("isLoad").style.display = "none";
     		document.getElementById("rightContent").style.display = "block";
     		clearInterval(ti);
     	}

     }
     function clearTime() {
     	clearInterval(ti);
     }
    </script>
</head>
<body  onunload="clearTime()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        基本信息</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        职员信息分析统计</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        统计类别</div>
                    <div id="leftContent">
                        <asp:ListBox ID="employeeList" runat="server" Width="100%" Height="100%" AutoPostBack="false">
                            <asp:ListItem Value="0">各单位工人总数统计</asp:ListItem>
                            <asp:ListItem Value="1">各单位工种人数统计</asp:ListItem>
                            <asp:ListItem Value="2">各单位班组长人数统计</asp:ListItem>
                            <asp:ListItem Value="3">各单位工人结构统计</asp:ListItem>
                            <asp:ListItem Value="4">各工种工人结构统计</asp:ListItem>
                            <asp:ListItem Value="5">各单位职教工作人员统计</asp:ListItem>
                            <asp:ListItem Value="6">各单位技能竞赛总体情况统计</asp:ListItem>
                            <asp:ListItem Value="7">各单位其他持证情况统计</asp:ListItem>
                        </asp:ListBox>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        统计结果
                    </div>
                     选择单位：<asp:DropDownList runat="server" ID="ddlOrg"></asp:DropDownList>
                    <div id="isLoad" style="display: none; color: red"><br/>数据加载中，请稍等...</div>
                    <div id="rightContent">
                        <iframe id="iframeFirst" style="width:100%;" height="100%" frameborder="0" class="iframe" src=""></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>

    <script type="text/javascript">
        var search = window.location.search;
        window.frames["iframeFirst"].location = "TJ_EmployeeWorkerByOrg.aspx?orgid="+document.getElementById("ddlOrg").value;
        document.getElementById("employeeList").value = 0;
       //document.getElementById("iframeFirst").style.height =(document.documentElement.clientHeight)+'px';
    </script>
