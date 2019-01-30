<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationStudy.aspx.cs" Inherits="RailExamWebApp.AssistBook.InformationStudy" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>资料查阅</title>
    <script type="text/javascript">     
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function tvAssist_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
                window.frames["ifBookInfo"].location = "InformationStudyInfo.aspx?id="+ node.get_id();
               node.expand(); 
            }
        }
       
        function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
                window.frames["ifBookInfo"].location = "InformationStudyInfo.aspx?id1="+ node.get_value();
            }
        }   
        
        function rbnClicked(btn)
        {
            if(btn.checked)
            {
                if(btn.id == "rbnAssist")
                {
                    $F("divAssist").style.display = "";
                    $F("divCategory").style.display = "none";
                  
                    var temp = tvAssist.get_nodes().getNode(0);
                    if(tvAssist.get_selectedNode())
                    {
                        temp = tvAssist.get_selectedNode();
                    }
                    temp.select();
                }
                else
                {
                    $F("divAssist").style.display = "none";
                    $F("divCategory").style.display = "";
                    
                    var temp = tvTrainType.get_nodes().getNode(0);
                    if(tvTrainType.get_selectedNode())
                    {
                        temp = tvTrainType.get_selectedNode();
                    }
                    temp.select();
                }
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="left">
                    <div id="leftHead">资料体系
                     <input id="rbnAssist" name="KnowledgeOrTrainType" onclick="rbnClicked(this);"type="radio" checked="checked" />资料领域 
                        <input id="rbnTrainType" name="KnowledgeOrTrainType" onclick="rbnClicked(this);" type="radio" />资料级别
                        </div>
                    <div id="leftContent">
                        <div id="divAssist">
                            <ComponentArt:TreeView ID="tvAssist" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvAssist_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <div id="divCategory" style="display: none;">
                            <ComponentArt:TreeView ID="tvTrainType" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifBookInfo" src="InformationStudyInfo.aspx?id=0" frameborder="0" scrolling="auto"
                            class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
