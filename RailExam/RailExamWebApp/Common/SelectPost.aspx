<%@ Page Language="C#" AutoEventWireup="True" Codebehind="SelectPost.aspx.cs" Inherits="RailExamWebApp.Common.SelectPost" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择工作岗位</title>

    <script type="text/javascript">
        function tvPost_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            if(! tvPost.get_selectedNode())
            {
                alert("请选择工作岗位！");
                return;
            }
            
            var src=document.getElementById("hfSource").value;
            if(src=="book" && tvPost.get_selectedNode().get_depth()==0)
            {
                alert("不能选择根节点");
                return;
            }
            if(src.length==0 && tvPost.get_selectedNode().get_nodes().get_length() > 0)
            {
                alert("请选择叶子节点！");
                return;
            }
            
            //alert(getReturnValue(tvPost.get_selectedNode()));
            window.returnValue = getReturnValue(tvPost.get_selectedNode());
            window.close();
        }
        
        function getReturnValue(node)
        {
            if(! node)
            {
                alert("无效的节点！");
                return;
            }
            //var strNodeText = GetNodeText(node.get_text(), node);
            return (node.get_id() + "|" + node.get_text());
        }
       
        function GetNodeText(strNodeText, node)
        {
            if(node.get_parentNode())
            {
                strNodeText = GetNodeText(node.get_parentNode().get_text() + "/" + strNodeText, node.get_parentNode());
            }
            
            return strNodeText;
        }  
        
        function tvPost_onNodeSelect(sender, eventArgs)
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
                    工作岗位&nbsp;&nbsp;<img id="SaveButton" onclick="selectBtnClicked();" src="../Common/Image/confirm.gif"
                        alt="" style="cursor: hand;" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvPost" runat="server" EnableViewState="false">
                        <ClientEvents>
                            <NodeMouseDoubleClick EventHandler="tvPost_onNodeMouseDoubleClick" />
                            <NodeSelect EventHandler="tvPost_onNodeSelect" />
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
                <br />
            </div>
        </div>
        <asp:HiddenField ID="hfSource" runat="server" />
    </form>
</body>
</html>
