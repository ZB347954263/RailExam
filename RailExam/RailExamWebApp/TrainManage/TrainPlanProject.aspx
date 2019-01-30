<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainPlanProject.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.TrainPlanProject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训项目</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
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
               
                id = node.get_id();

                window.frames["ifrmPro"].location = "TrainPlanProjectInfo.aspx?id=" + id;
                node.expand();
            }
        }
        
        function checkedBtnsChangeCallBack_onCallbackComplete(sender,eventArgs) {
        	
        }
        
        function addProject()
        {
        	if(document.getElementById("hfUpdate").value == "False") {
        		 alert("您没有权限使用该操作！");                       
                        return;
        	}

            var node = tvView.get_selectedNode();
        	
        	if(!node) {
        		alert("请选择一个培训类别");
        		return;
        	}

            var scrH=screen.height;
            var scrW=screen.width;
            var top=scrH/2-20;
            var left=scrW/2-150;
            var features="width=300px,height=1px,top="+top+",left="+left+",menubar=no,toolbar=no,location=no,scrollbar=no,resizable=no,status=no";

            open("TrainPlanProjectDetail.aspx?typeID="+node.get_id(),"TrainPlanProjectDetail",features);
        }
        
        function refreshProjectInfo()
        {
            var search=window.location.search;
            if(search.length>0)
            {
                var id=search.substring(12);
                if(id.length>0)
                {
                    window.frames["ifrmPro"].location = "TrainPlanProjectInfo.aspx?id=" + id;
                }
            }
            
        }
    </script>

</head>
<body onload="refreshProjectInfo()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="Div4">
                    <div id="location">
                        <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                        </div>
                        <div id="parent">
                            培训管理</div>
                        <div id="separator">
                        </div>
                        <div id="current">
                            培训项目</div>
                    </div>
                    <div id="welcomeInfo">
                        <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                    </div>
                    <div id="button">
                        <img id="imgAdd" onclick="addProject()" src="../Common/Image/add.gif" alt="新增培训项目" />
                    </div>
                </div>
            </div>
            <div id="content">
                <div id="left" style="width: 200px">
                    <div id="leftHead">
                        培训类别
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
                        培训项目信息</div>
                    <div id="rightContentWithNoHead">
                        <iframe id="ifrmPro" src="TrainPlanProjectInfo.aspx" frameborder="0" scrolling="auto"
                            class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfUpdate" />
        <asp:HiddenField ID="hfDelete" runat="server" />
    </form>
</body>
</html>
