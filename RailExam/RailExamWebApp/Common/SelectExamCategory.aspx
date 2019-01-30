<%@ Page Language="C#" AutoEventWireup="True" Codebehind="SelectExamCategory.aspx.cs"
    Inherits="RailExamWebApp.Common.SelectExamCategory" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择考试分类</title>

    <script type="text/javascript">
        function tvExamCategory_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            if(! tvExamCategory.get_selectedNode())
            {
                alert("请选择考试分类！");
                return;
            }
           
            
            window.returnValue = getReturnValue(tvExamCategory.get_selectedNode());
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
        
        function tvExamCategory_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {            
                node.expand();  
            }
        }  
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    考试分类&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<img onclick="selectBtnClicked();"
                        src="../Common/Image/confirm.gif" alt="" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvExamCategory" runat="server" EnableViewState="false">
                        <ClientEvents>
                            <NodeMouseDoubleClick EventHandler="tvExamCategory_onNodeMouseDoubleClick" />
                           <NodeSelect EventHandler="tvExamCategory_onNodeSelect" />  
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
