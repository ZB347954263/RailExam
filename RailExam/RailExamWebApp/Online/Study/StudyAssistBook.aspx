<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StudyAssistBook.aspx.cs"
    Inherits="RailExamWebApp.Online.Study.StudyAssistBook" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学习辅导教材</title>

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
                window.frames["iframe1"].location = "StudyAssistBookDetail.aspx?KnowledgeID="+ node.get_value();
            }
        }       
       
//        function tvTrainType_onNodeSelect(sender, eventArgs)
//        {     
//              var node = eventArgs.get_node();
//            if(node)
//            {            
//                window.frames["iframe1"].location = "StudyAssistBookDetail.aspx?TrainTypeID="+ node.get_value();
//            }
//        }   
        
        
//        function rbnClicked(btn)
//        {
//            if(btn.checked)
//            {
//                if(btn.id == "rbnAssist")
//                {
//                    $F("divKnowledge").style.display = ""; 
//                    $F("divCategory").style.display = "none"; 
//                  
//                    
//                    var temp = tvAssist.get_nodes().getNode(0);
//                    if(tvAssist.get_selectedNode())
//                    {
//                        temp = tvAssist.get_selectedNode();
//                    }
//                    temp.select();
//                    
//                }
//                else
//                {
//                    $F("divKnowledge").style.display = "none"; 
//                    $F("divCategory").style.display = ""; 
//                  
//                    
//                    var temp = tvTrainType.get_nodes().getNode(0);
//                    if(tvTrainType.get_selectedNode())
//                    {
//                        temp = tvTrainType.get_selectedNode();
//                    }
//                    temp.select();
//                     
//                }
//            }
//        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        <div id="divSwitchBtns" style="text-align: center; white-space: nowrap;">
                            辅导体系
                            <%--                            <input id="rbnAssist" name="knowledgeOrCategory" onclick="rbnClicked(this);" type="radio"
                                checked="checked" />按辅导体系
                            <input id="rbnTrainType" name="knowledgeOrCategory" onclick="rbnClicked(this);" type="radio" />按培训类别--%>
                        </div>
                    </div>
                    <div id="leftContent">
                        <div id="divKnowledge">
                            <ComponentArt:TreeView ID="tvAssist" runat="server" EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvAssist_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <%--                        <div id="divCategory" style="display: none;">
                            <ComponentArt:TreeView ID="tvTrainType" runat="server"   EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>--%>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        辅导教材信息</div>
                    <div id="rightContent">
                        <iframe id="iframe1" src="StudyAssistBookDetail.aspx?id=0" frameborder="0" scrolling="auto"
                            class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input id="Test1" type="hidden" name="Test1" />
    </form>
</body>
</html>
