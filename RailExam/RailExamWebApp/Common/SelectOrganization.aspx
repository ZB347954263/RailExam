<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectOrganization.aspx.cs" Inherits="RailExamWebApp.Common.SelectOrganization" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择组织机构</title>
    <script type="text/javascript">
        function tvOrganization_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            var node = tvOrganization.get_selectedNode();
            if(! node)
            {
                alert("请选择一个组织机构！");
                return;
            }

            window.returnValue = getReturnValue(node);
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
            return (node.get_id() + "|" + strNodeText);
        }
       
        function GetNodeText(strNodeText, node)
        {
            if(node.get_parentNode())
            {
                strNodeText = GetNodeText(node.get_parentNode().get_text() + "/" + strNodeText, node.get_parentNode());
            }
            
            return strNodeText;
        } 
        
        function tvOrganization_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {            
                node.expand();  
            }
        }   
    </script>
    <script type="text/javascript" src="../Common/JS/JSHelper.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    组织机构&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img style="cursor:hand;" onclick="selectBtnClicked();" src="../Common/Image/confirm.gif" alt="" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvOrganization" runat="server" EnableViewState="false">
                        <ClientEvents>
                            <NodeMouseDoubleClick EventHandler="tvOrganization_onNodeMouseDoubleClick" />
                           <NodeSelect EventHandler="tvOrganization_onNodeSelect" /> 
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
