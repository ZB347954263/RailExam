<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyByKnowledge.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyByKnowledge" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>按教材体系学习</title>
    <script type="text/javascript">
    function $F(objId)
    {
        return document.getElementById(objId);
    }
    
    function tvKnowledge_onNodeSelect(sender, eventArgs)
    {
        var node = eventArgs.get_node();
        if(node)
        {
            window.frames["ifBookInfo"].location = "StudyByKnowledgeDetail.aspx?Book="+ node.get_value();
        }
    }
   
    function tvCourseware_onNodeSelect(sender, eventArgs)
    {
        var node = eventArgs.get_node();
        if(node)
        {
            window.frames["ifBookInfo"].location = "StudyByKnowledgeDetail.aspx?Courseware="+ node.get_value();
        }
    }   
    
        function rbnClicked(btn)
        {
            if(btn.checked)
            {
                if(btn.id == "rbnKnowledge")
                {
                    $F("divKnowledge").style.display = "";
                    $F("divCategory").style.display = "none";
                  
                    var temp = tvKnowledge.get_nodes().getNode(0);
                    if(tvKnowledge.get_selectedNode())
                    {
                        temp = tvKnowledge.get_selectedNode();
                    }
                    temp.select();
                }
                else
                {
                    $F("divKnowledge").style.display = "none";
                    $F("divCategory").style.display = "";
                    
                    var temp = tvCourseware.get_nodes().getNode(0);
                    if(tvCourseware.get_selectedNode())
                    {
                        temp = tvCourseware.get_selectedNode();
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
                    <div id="leftHead" style="width:200px;">分类:
                        <input id="rbnKnowledge" name="KnowledgeOrTrainType" onclick="rbnClicked(this);"type="radio" checked="checked" />教材体系
                        <input id="rbnTrainType" name="KnowledgeOrTrainType" onclick="rbnClicked(this);" type="radio" />课件体系</div>
                    <div id="leftContent">
                        <div id="divKnowledge">
                            <ComponentArt:TreeView Width="200" Height="550" ID="tvKnowledge" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvKnowledge_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <div id="divCategory" style="display: none;">
                            <ComponentArt:TreeView Width="200" Height="550" ID="tvCourseware" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvCourseware_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifBookInfo" src="StudyByKnowledgeDetail.aspx?Book=/1" height="575px" width="550" frameborder="0" scrolling="auto"
                            ></iframe>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
