<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerServer.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerServer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
      function FindRecord()
        {
             if(window.frames["ifComputerServerInfo"].document.getElementById("query").style.display == "none")
                    window.frames["ifComputerServerInfo"].document.getElementById("query").style.display = "";
                else
                    window.frames["ifComputerServerInfo"].document.getElementById("query").style.display = "none";
        }
        
        function selectArow(rowIndex) {
            var t = document.getElementById("grdEntity");
            for (var i = 1; i < t.rows.length; i++) {
                if (i - 1 == rowIndex) {
                    t.rows(i).style.backgroundColor = "#FFEEC2";
                }
                else {
                    if ((i - 1) % 2 == 0) {
                        t.rows(i).style.backgroundColor = "#EFF3FB";
                    }
                    else {
                        t.rows(i).style.backgroundColor = "White";
                    }
                }
            }
        }

        function tvView_onNodeSelect(sender, eventArgs) {
            var node = eventArgs.get_node();
            if (node) {
               
                type = "Org";
                id = node.get_id();
                idpath = node.get_value();

                window.frames["ifComputerServerInfo"].location = "ComputerServerInfo.aspx?OrgID=" + id;
                node.expand();
            }
        }
        
        function addComputerServerInfo() 
        {
        	var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
                alert("您没有该操作的权限！");
                return;
             }
        	
            var node = tvView.get_selectedNode();
        	
        	if(!node) {
        		alert("请选择一个服务器！");
        		return;
        	}

        	window.frames["ifComputerServerInfo"].location = 'ComputerServerInfoDetail.aspx?OrgID=' + node.get_id();
        }
        
        function checkedBtnsChangeCallBack_onCallbackComplete(sender,eventArgs) {
        	
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="Div4">
                    <div id="location">
                        <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                        </div>
                        <div id="parent">
                            基本信息</div>
                        <div id="separator">
                        </div>
                        <div id="current">
                            站段服务器</div>
                    </div>
                    <div id="welcomeInfo">
                        <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                    </div>
                    <div id="button">
                        <img id="btnFind" onclick="FindRecord();" src="../Common/Image/find.gif" alt="" style="display:none" />
                        <img id="addComputerServer" onclick="addComputerServerInfo();" src="../Common/Image/add.gif"
                            alt="" />
                    </div>
                </div>
            </div>
            <div id="content">
                <div id="left" style="width: 200px">
                    <div id="leftHead">
                        单位
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
                            <ClientEvents>
                                <CallbackComplete EventHandler="checkedBtnsChangeCallBack_onCallbackComplete" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        服务器信息</div>
                    <div id="rightContentWithNoHead">
                        <iframe id="ifComputerServerInfo" src="ComputerServerInfo.aspx"
                            frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfSelectOrg" />
        <asp:HiddenField runat="server" ID="hfLevelNum" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
