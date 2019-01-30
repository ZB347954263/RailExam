<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectItemCategory.aspx.cs" Inherits="RailExamWebApp.Common.SelectItemCategory" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择辅助分类</title>
    <script type="text/javascript">
        function tvItemCategory_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            if(! tvItemCategory.get_selectedNode())
            {
                alert("请选择一个分类！");
                return;
            }
            //alert(getReturnValue(tvItemCategory.get_selectedNode()));
            window.returnValue = getReturnValue(tvItemCategory.get_selectedNode());
            window.close();
        }
        
        function getReturnValue(node)        
        {
            if(!node)
            {
                alert("无效的节点！");
                return;
            }
            
            var textPath = node.get_text() + "\\";
            var tempNode = node;
            while(tempNode.get_parentNode())
            {
                tempNode = tempNode.get_parentNode();
                textPath += tempNode.get_text() + "\\";
            }
            textPath = textPath.substring(0, textPath.length-1);
            textPath = textPath.split("\\").reverse().join("\\");
            
            return (node.get_id() + "|" + textPath);
        }
    </script>
    <script src="../Common/JS/JSHelper.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    辅助分类</div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvItemCategory" runat="server" EnableViewState="false">
                        <ClientEvents>
                            <NodeMouseDoubleClick EventHandler="tvItemCategory_onNodeMouseDoubleClick" />
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
