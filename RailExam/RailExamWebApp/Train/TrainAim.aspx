<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainAim.aspx.cs" Inherits="RailExamWebApp.Train.TrainAim" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��ѵ���</title>
    <script type="text/javascript">
         function $F(objId)
        {
            return document.getElementById(objId);
        }
         
        function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();    
            if(node)
            {       
                var theFrame = window.frames["ifTrainAimDetail"];
                theFrame.location = "TrainAimDetail.aspx?id=" + node.get_id();
                node.expand();
            }
        }   
        function tvTrainPost_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();     
            if(node)
            {       
                var theFrame = window.frames["ifTrainAimDetail"];
                theFrame.location = "TrainAimDetail.aspx?id1=" + node.get_id();
                node.expand();
            }
        } 
        function rbnClicked(btn)
        {
            if(btn.checked)
            {
                if(btn.id == "rbnTrainType")
                {
                    $F("divTrainType").style.display = "";
                    $F("divTrainPost").style.display = "none";
                    
                    if(tvTrainType.get_selectedNode())
                    {
                        temp = tvTrainType.get_selectedNode();
                        window.frames["ifTrainAimDetail"].location = "TrainAimDetail.aspx?id="+ temp.get_id();
                    }
                    else
                    {
                        window.frames["ifTrainAimDetail"].location = "TrainAimDetail.aspx?id=0";
                    }
                }
                else
                {
                    $F("divTrainType").style.display = "none";
                    $F("divTrainPost").style.display = "";

                    if(tvTrainPost.get_selectedNode())
                    {
                        temp = tvTrainPost.get_selectedNode();
                        window.frames["ifTrainAimDetail"].location = "TrainAimDetail.aspx?id1="+ temp.get_id();
                   }
                    else
                    {
                         window.frames["ifTrainAimDetail"].location = "TrainAimDetail.aspx?id1=0";
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        ֪ʶ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ��ѵĿ��</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo %>
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">����:
                        <input id="rbnTrainType" name="KnowledgeOrTrainType" onclick="rbnClicked(this);"type="radio" checked="checked" />��ѵ���
                        <input id="rbnTrainPost" name="KnowledgeOrTrainType" onclick="rbnClicked(this);" type="radio" />������λ</div>
                    <div id="leftContent">
                        <%--<ComponentArt:TreeView ID="tvTrainType1" runat="server" EnableViewState="false">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>--%>
                        <div id="divTrainType">
                            <ComponentArt:TreeView ID="tvTrainType" runat="server"  EnableViewState="false" DefaultTarget="ifTrainAimDetail">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <div id="divTrainPost" style="display: none;">
                            <ComponentArt:TreeView ID="tvTrainPost" runat="server" DefaultTarget="ifTrainAimDetail">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvTrainPost_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        </div>
                    <div id="rightContent">
                        <iframe id="ifTrainAimDetail" frameborder="0" scrolling="auto" class="iframe">
                        </iframe>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var theFrame = window.frames["ifTrainAimDetail"];        
        theFrame.location = "TrainAimDetail.aspx?id=" + tvTrainType.get_nodes().getNode(0).get_id();
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>
</body>
</html>
