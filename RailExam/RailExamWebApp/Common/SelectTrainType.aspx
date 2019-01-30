<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectTrainType.aspx.cs"
    Inherits="RailExamWebApp.Common.SelectTrainType" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择培训类别</title>

    <script type="text/javascript">
        var nodeTexts = new Array();
        var nodeIDs = new Array();
        var nodeText = "";
        var nodeID = "";
        
        function tvTrainType_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            if(! tvTrainType.get_selectedNode())
            {
                alert("请选择知识体系！");
                return;
            }
            
            if(tvTrainType.get_selectedNode().get_nodes().get_length() > 0)
            {
                alert("请选择叶子节点！");
                return;
            }
            
            window.returnValue = getReturnValue(tvTrainType.get_selectedNode());
            window.close();
        }
        
        function getReturnValue(node)
        {
            if(! node)
            {
                alert("无效的节点！");
                return;
            }
            
            var strNodeText = GetNodeText(node.get_text(), node);
           
            return node.get_id() + "|" + strNodeText;
        }
        
        function GetNodeText(strNodeText, node)
        {
            if(node.get_parentNode())
            {
                strNodeText = GetNodeText(node.get_parentNode().get_text() + "/" + strNodeText, node.get_parentNode());
            }
            
            return strNodeText;
        }
    </script>

    <script src="../Common/JS/JSHelper.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    培训类别&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img onclick="selectBtnClicked();" style="cursor: hand;" src="../Common/Image/confirm.gif"
                        alt="" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvTrainType" runat="server" EnableViewState="true" KeyboardEnabled="true">
                        <ClientEvents>
                            <NodeMouseDoubleClick EventHandler="tvTrainType_onNodeMouseDoubleClick" />
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
