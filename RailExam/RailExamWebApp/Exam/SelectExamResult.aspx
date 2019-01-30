<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectExamResult.aspx.cs"
    Inherits="RailExamWebApp.Exam.SelectExamResult" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>员工</title>

    <script type="text/javascript">
    	function tvView_onNodeSelect(sender, eventArgs)
    	{
    		var node = eventArgs.get_node();
    		if(node)
    		{
    			var begin = window.frames["ifExamResultInfo"].document.getElementById("dateStartDateTime_DateBox").value;
    			var end = window.frames["ifExamResultInfo"].document.getElementById("dateEndDateTime_DateBox").value;
    			var type = "";
    			var id = "";
    			id = node.get_id();
    			window.frames["ifExamResultInfo"].location = "SelectExamResultDetail.aspx?Orgid=" + id+"&begin="+begin+"&end="+end ;
    		}
    	}
       
    	function  QueryRecord()
    	{
    		if(window.frames["ifExamResultInfo"].document.getElementById("query").style.display == "none")
    			window.frames["ifExamResultInfo"].document.getElementById("query").style.display = "";
    		else
    			window.frames["ifExamResultInfo"].document.getElementById("query").style.display = "none";
    	} 
        
    	function $F(objId)
    	{
    		return document.getElementById(objId);
    	}
        
    	function showSaveExam() {
    		if(document.getElementById("hfIsServer").value == "False") {
    			alert("请连接路局服务器使用该功能！");
    			return;
    		}
    		var node = tvView.get_selectedNode();
    		if(!node) {
    			alert("请选择一个站段单位！");
    			return;
    		}
    		var id = node.get_id();
    		var ret = window.showModalDialog("ShowSaveExam.aspx?orgID="+id,'','help:no;status:no;dialogWidth:800px;dialogHeight:600px;');
    		window.frames["ifExamResultInfo"].location = "SelectExamResultDetail.aspx?Orgid=" + id ;
    	}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        成绩查询</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <input type="button" id="btnQuery" onclick="QueryRecord();" value="查  询" style="display:none" class="button" />
                    <input type="button" id="btnExam" onclick="showSaveExam();" value="存档考试管理" class="buttonLong" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        <div id="divSwitchBtns">
                            组织机构
                        </div>
                    </div>
                    <div id="leftContent">
                        <ComponentArt:TreeView ID="tvView" runat="server" EnableViewState="false">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvView_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifExamResultInfo" src="SelectExamResultDetail.aspx" frameborder="0"
                            scrolling="no" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="tvType" runat="server" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshAdd" />
        <asp:HiddenField ID="hfIsServer" runat="server" />
    </form>

</body>
</html>
