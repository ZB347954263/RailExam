<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FocalPointStudy.aspx.cs"
    Inherits="RailExamWebApp.Main.FocalPointStudy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>重点学习</title>
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
            if (node && node.get_depth()==4) 
            {               
                id = node.get_id();

                window.frames["ifrmBook"].location = "FocalPointStudyInfo.aspx?id=" + id;
                node.expand();
            }
        }
        
        function checkedBtnsChangeCallBack_onCallbackComplete(sender,eventArgs) {}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="Div4">
                    <div id="location">
                        <div id="desktop" onclick="window.location='../Main/EmployeeDesktop.aspx'">
                        </div>
                        <div id="parent">
                            我的工作台</div>
                        <div id="separator">
                        </div>
                        <div id="current">
                            培训班学习</div>
                    </div>
                    <div id="welcomeInfo">
                        <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                    </div>
                </div>
            </div>
            <div id="content">
                <div id="left" style="width: 200px">
                    <div id="leftHead">
                        已参加培训
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
                        教材信息</div>
                    <div id="rightContentWithNoHead">
                        <iframe id="ifrmBook" src="FocalPointStudyInfo.aspx" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
