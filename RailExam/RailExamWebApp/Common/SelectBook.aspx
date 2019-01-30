<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectBook.aspx.cs" Inherits="RailExamWebApp.Common.SelectBook" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择教材章节</title>
    <script  type="text/javascript" src="../Common/JS/JSHelper.js"></script>
    <script type="text/javascript">
        function tvBookChapter_onNodeSelect(sender, eventArgs)
        {
        }

        function selectBtnClicked()
        {
            if(! tvBookChapter.get_selectedNode())
            {
                alert("请选择一本教材！");
                return;
            }
            if(tvBookChapter.get_selectedNode().getProperty("isBook") != "true")
            {
                alert("您选择的不是一本教材！");
                return;
            }
            
            // alert(getReturnValue(tvBookChapter.get_selectedNode()));
            window.returnValue = getReturnValue(tvBookChapter.get_selectedNode());
            window.close();
        }
        
        function getReturnValue(node)
        {
            if(! node)
            {
                alert("无效的节点！");
                return;
            }
            
            var bookId = "";
            var bookId = node.get_id();
            var bookname = node.get_text();
            
            return (bookId + "|" + bookname);
        }
        
        function getReturnValues(node)
        {
            var allValues = "";
            var tempNode;
            
            for(var i = 0; i < node.get_nodes().get_length(); i ++)
            {
                tempNode = node.get_nodes().getNode(i);
                
                if (tempNode.get_checked())
                {
                    allValues += getReturnValue(tempNode) + "$";
                }
                allValues += getReturnValues(tempNode);
            }
            return allValues;
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    现有教材&nbsp;&nbsp;&nbsp;&nbsp;<img onclick="selectBtnClicked();" src="../Common/Image/confirm.gif" alt="" />
                 </div>
                <div id="mainContent">
                        <ComponentArt:TreeView ID="tvBookChapter" runat="server">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvBookChapter_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                </div>
        </div>
       </div> 
    </form>
</body>
</html>

