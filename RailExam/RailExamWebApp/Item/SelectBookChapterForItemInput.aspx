<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectBookChapterForItemInput.aspx.cs"
    Inherits="RailExamWebApp.Item.SelectBookChapterForItemInput" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="../Common/JS/JSHelper.js"></script>

    <script type="text/javascript">
           function selectBtnClicked()
        {
                if(! tvView.get_selectedNode())
                {
                    alert("请选择一个章节！");
                    return;
                }
                if(tvView.get_selectedNode().getProperty("isChapter") != "true")
                {
                    alert("请选择一个章节！");
                    return;
                }
                
                // alert(getReturnValue(tvView.get_selectedNode()));
                window.returnValue = getReturnValue(tvView.get_selectedNode());
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
            var chapterId = node.get_id();
            var textPath = node.get_text() + "\\";
            var tempNode = node;
            var idtype=4;
            if(node.getProperty("isBook") == "true")
            {
                idtype=3;
            }
              if(node.getProperty("isChapter") == "true")
            {
                idtype=4;
            }
              if(node.getProperty("isKnowledge") == "true")
            {
                idtype=1;
            }
            
            
            while(tempNode.get_parentNode())
            {
                if(tempNode.getProperty("isBook") == "true")
                {
                    bookId = tempNode.get_id();
                    break;
                }
                tempNode = tempNode.get_parentNode();
                textPath += tempNode.get_text() + "\\";
            }
            textPath = textPath.substring(0, textPath.length-1);
            textPath = textPath.split("\\").reverse().join("\\");             
            
            bookId = bookId.replace("book","");
            chapterId = chapterId.replace("chapter","");
            return (bookId + "|" + chapterId + "|" + textPath+"|"+idtype);
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
        
        function tvView_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {            
                node.expand();  
            }
        }
        
        function treeViewSelect(nodes,id)
        {
            for(var i = 0 ; i < nodes.get_length() ; i++)
            {
                if(nodes.getNode(i).get_id() == id && nodes.getNode(i).getProperty("isChapter") =="true")
               {
                    nodes.getNode(i).select();
                    break;
               } 
               else
               {
                    if(nodes.getNode(i).get_nodes())
                    {
                        treeViewSelect(nodes.getNode(i).get_nodes(),id);
                    }
               }
            } 
        }
        
        function tvView_noLoad(sender, eventArgs)
        {
            var search = window.location.search;
            var chapterId = search.substring(search.indexOf("chapterId=")+10);
            treeViewSelect(tvView.get_nodes(),"chapter"+chapterId);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    教材章节&nbsp;&nbsp;<img onclick="selectBtnClicked();" src="../Common/Image/confirm.gif"
                        alt="" style="cursor: hand;" />
                </div>
                <div>
                </div>
                <div id="mainContent">
                    <ComponentArt:CallBack ID="ddlViewChangeCallBack" runat="server" PostState="true"
                        OnCallback="ddlViewChangeCallBack_Callback">
                        <Content>
                            <ComponentArt:TreeView ID="tvView" runat="server">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvView_onNodeSelect" />
                                    <Load EventHandler="tvView_noLoad" />
                                </ClientEvents>
                            </ComponentArt:TreeView></Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
