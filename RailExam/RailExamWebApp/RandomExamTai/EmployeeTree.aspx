<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeTree.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function FindRecord()
        {
             if(window.frames["ifEmployeeInfo"].document.getElementById("highquery").style.display == "none")
                    window.frames["ifEmployeeInfo"].document.getElementById("highquery").style.display = "";
                else
                    window.frames["ifEmployeeInfo"].document.getElementById("highquery").style.display = "none";
        }
        
        function tvView_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
                var rbn = $F("rbnOrg");
                
                var type = "";
                var id = "";
                var idpath = ""; 

                type = "Org";
                id = node.get_id();
                idpath = node.get_value();

                window.frames["ifEmployeeInfo"].location = "EmployeeInfo.aspx?id=" + id + "&idpath="+ idpath +"&type=" + type;
               node.expand(); 
            }
        }
        
        function addEmployeeInfo() {
        	
        var flagUpdate=document.getElementById("HfUpdateRight").value; 
           if(flagUpdate=="False")
            {
               alert("您没有权限使用该操作！");
               return;
           } 
            var node = tvView.get_selectedNode();
        	
        	if(!node) {
        		alert("请选择一个车间或班组！");
        		return;
        	}

        	var idPath = node.get_value();
        	var str = idPath.split('/');
        	if( str.length<=3) {
        		alert("请选择一个车间或班组！");
        		return;
        	}
        	
        	var name = window.frames["ifEmployeeInfo"].document.getElementById("txtName").value;
		  	var sex = window.frames["ifEmployeeInfo"].document.getElementById("ddlSex").value;
		  	var status = window.frames["ifEmployeeInfo"].document.getElementById("ddlStatus").value;
            var pinyin = window.frames["ifEmployeeInfo"].document.getElementById("txtPinYin").value;
        	var code = window.frames["ifEmployeeInfo"].document.getElementById("txtTechnicalCode").value;
        	var postId = window.frames["ifEmployeeInfo"].document.getElementById("hfPostID").value;

		  	var strQuery = name + "|" + sex + "|" + status+ "|" + pinyin+ "|" + code+ "|" + postId;
        	//alert(strQuery);
        	
         	 window.frames["ifEmployeeInfo"].location = 'EmployeeInfoDetail.aspx?Type=2&idpath='+idPath+'&strQuery='+strQuery+'&OrgID=' + node.get_id();
        }
        
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        
        function deleteOrg()
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-400)*.5;   
            ctop=(screen.availHeight-200)*.5;   
            if(form1.IsWuhan.value == "False")
            {
                var ret = window.open('/RailExamBao/RandomExamOther/DeleteEmployeeByOrg.aspx','DeleteEmployeeByOrg','Width=400px; Height=200px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	            ret.focus();
            }
        }
        
        function exportTemplate()
        {
            var ret =  window.showModalDialog("/RailExamBao/RandomExamTai/ExportTemplate.aspx?Type=template",'','help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
            if(ret != "")
            {
             document.getElementById("hfRefresh").value = ret;
             form1.submit();
           }
        }
        
        function exportEmployee()
        {
        	var role = document.getElementById("hfRole").value; 
        	if(role !=1) {
        		alert("您没有权限使用该操作！");
               return;
        	}
        	
        	var flagUpdate=document.getElementById("HfUpdateRight").value; 
           if(flagUpdate=="False")
            {
               alert("您没有权限使用该操作！");
               return;
           } 
        	
            var node = tvView.get_selectedNode();
            if(node.get_id() == 1)
            {
                alert("请选择站段或车间或班组导出员工信息！");
            	return;
            }
            var ret = window.showModalDialog("/RailExamBao/RandomExamTai/ExportTemplate.aspx?OrgID="+node.get_id()+"&Type=employee",'','help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
            if(ret != "")
            {
             document.getElementById("hfRefresh").value = ret;
             form1.submit();
           }
        }
        
        function init() {
        	var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
	        	document.getElementById("ExportEmployee").style.display = "none";
	        	document.getElementById("NewButton").style.display = "none";
             } 
        }
    </script>

</head>
<body onload="init()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        职员管理</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="NewButton" onclick="addEmployeeInfo();" src="../Common/Image/add.gif"
                        action="new" alt=""  />
                    <img id="FindButton" onclick="FindRecord();" src="../Common/Image/find.gif"
                        action="find" alt="" />
                    <img id="ExportTemplate" onclick="exportTemplate();" src="../Common/Image/ExportTemplate.jpg"
                        action="ExportTemplate" alt="" />
                    <img id="ExportEmployee" onclick="exportEmployee();" src="../Common/Image/ExportEmployee.jpg" 
                        action="ExportEmployee" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        组织机构
                    </div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="checkedBtnsChangeCallBack" runat="server" PostState="true">
                            <Content>
                                <ComponentArt:TreeView ID="tvView" runat="server" EnableViewState="false">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvView_onNodeSelect" />
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifEmployeeInfo" src="EmployeeInfo.aspx?id=1&idpath=/1&type=Org" frameborder="0"
                            scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="hfRefresh" runat="server" />
        <asp:HiddenField ID="hfRole" runat="server" />
    </form>

    <script type="text/javascript">
        if(tvView && tvView.get_nodes().get_length() > 0)
        {
            tvView.get_nodes().getNode(0).select();
        }
    </script>

</body>
</html>
