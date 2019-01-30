<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectCoursewareType.aspx.cs" Inherits="RailExamWebApp.Common.SelectCoursewareType" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择知识体系</title>
    <script type="text/javascript">
        function tvCoursewareType_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            if(! tvCoursewareType.get_selectedNode())
            {
                alert("请选择知识体系！");
                return;
            }
            
            if(tvCoursewareType.get_selectedNode().get_nodes().get_length() > 0)
            {
                alert("请选择叶子节点！");
                return;
            }
            
            window.returnValue = getReturnValue(tvCoursewareType.get_selectedNode());
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
                    知识体系</div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvCoursewareType" runat="server" EnableViewState="false">
                        <ClientEvents>
                            <NodeMouseDoubleClick EventHandler="tvCoursewareType_onNodeMouseDoubleClick" />
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
                <div>
                    <br />
                    <img onclick="selectBtnClicked();" src="../Common/Image/confirm.gif" alt="" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
