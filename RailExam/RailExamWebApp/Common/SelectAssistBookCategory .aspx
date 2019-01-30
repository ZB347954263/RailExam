<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectAssistBookCategory .aspx.cs" Inherits="RailExamWebApp.Common.SelectAssistBookCategory" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>选择辅导教材体系</title>
    <script type="text/javascript">
        function tvKnowledge_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            if(! tvKnowledge.get_selectedNode())
            {
                alert("请选择辅导教材体系！");
                return;
            }
            
            if(tvKnowledge.get_selectedNode().get_nodes().get_length() > 0)
            {
                alert("请选择叶子节点！");
                return;
            }
            
            window.returnValue = getReturnValue(tvKnowledge.get_selectedNode());
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
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    辅导教材体系&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  
                    <img id="SaveButton" style="cursor:hand;" onclick="selectBtnClicked();" src="../Common/Image/confirm.gif" alt="" />
            </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvKnowledge" Width="300" runat="server" EnableViewState="false">
                        <ClientEvents>
                            <NodeMouseDoubleClick EventHandler="tvKnowledge_onNodeMouseDoubleClick" />
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
