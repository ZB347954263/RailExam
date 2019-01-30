<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectBookRange.aspx.cs" Inherits="RailExamWebApp.Online.Exam.SelectBookRange" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择教材章节</title>
    <script src="/RailExamBao/Common/JS/JSHelper.js"></script>
    <script type="text/javascript">
        function tvBookChapter_onNodeCheckChange(sender,eventArgs)
       {
               if (window.location.search.indexOf("flag=2") > -1)
               {
                    var itemType = document.getElementById("hfItemType").value;
                    var node = eventArgs.get_node();
                    if(node.getProperty("isBook") == "true")
                     {
                        node.set_contentCallbackUrl( "/RailExamBao/Common/GetBookChapter.aspx?itemTypeID=1&flag=2&state=" + node.get_checked() + "&id=" + node.get_id());
                        node.expand();
                      }
                     if(node.get_nodes().get_length() > 0)
                    {
                        //check(node,node.get_checked());
                        if(node.get_checked())
                        {
                            node.checkAll();
                        }
                        else
                        {
                            node.unCheckAll();
                        }
                         node.set_checked(node.get_checked());
                    }
               } 
        }

        function check(node, state)
        {
            var childNodes = node.get_nodes();
            var count = childNodes.get_length();
        
            node.set_checked(state);
            for(var i = 0; i < count; i ++)
            {
                //alert(childNodes.getNode(i));
                check(childNodes.getNode(i),state);
            }
        }
        
        function tvBookChapter_onNodeSelect(sender, eventArgs)
        {
        }

        function selectBtnClicked()
        {
            if (window.location.search.indexOf("flag") > -1)
            {
                var returnValues = "";
               
                if (window.location.search.indexOf("flag=3") > -1)
               {
                         for(var i = 0; i < tvBookChapter.get_nodes().get_length(); i ++)
                        {
                            returnValues += getReturnValues(tvBookChapter.get_nodes().getNode(i))
                        }
                       
                         if(returnValues.length == 0)
                         {
                            alert('请至少选择一本教材！');
                            return; 
                         }  
                        if (returnValues.lastIndexOf('$') == returnValues.length - 1)
                        {
                            returnValues = returnValues.substring(0, returnValues.length - 1)
                        }
                        
                        //alert(returnValues);
                        window.returnValue = returnValues;
                        window.close();        
             }
               else
               { 
                         for(var i = 0; i < tvBookChapter.get_nodes().get_length(); i ++)
                        {
                            returnValues += getReturnValues(tvBookChapter.get_nodes().getNode(i))
                        }
                        
                          if(returnValues.length == 0)
                          {
                                if(tvBookChapter.get_selectedNode())
                                {
                                       if(tvBookChapter.get_selectedNode().getProperty("isKnowledge") == "true" || tvBookChapter.get_selectedNode().getProperty("isTrainType") == "true")
                                       {
                                            window.returnValue = getReturnValue(tvBookChapter.get_selectedNode());
                                            window.close();
                                       }
                                      else
                                     {
                                            window.returnValue = "";
                                            window.close();
                                     }    
                                } 
                         }
                         else
                         {
                                if (returnValues.lastIndexOf('$') == returnValues.length - 1)
                                {
                                    returnValues = returnValues.substring(0, returnValues.length - 1)
                                }
                                
                                //alert(returnValues);
                                window.returnValue = returnValues;
                                window.close();  
                       } 
               }            
            }
            else
            {
                if(! tvBookChapter.get_selectedNode())
                {
                    alert("请选择一个章节！");
                    return;
                }
//                if(tvBookChapter.get_selectedNode().getProperty("isChapter") != "true")
//                {
//                    alert("您选择的不是一个章节！");
//                    return;
//                }
                
                // alert(getReturnValue(tvBookChapter.get_selectedNode()));
                window.returnValue = getReturnValue(tvBookChapter.get_selectedNode());
                window.close();
            }
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
              if(node.getProperty("isTrainType") == "true")
            {
                idtype=2;
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
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    教材章节&nbsp;&nbsp;<img onclick="selectBtnClicked();" src="/RailExamBao/Common/Image/confirm.gif" alt="" style="cursor:hand;" />
                </div>

                <div id="mainContent">
                    <ComponentArt:CallBack ID="ddlViewChangeCallBack" runat="server" PostState="true">
                        <Content>
                            <ComponentArt:TreeView ID="tvBookChapter" runat="server">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvBookChapter_onNodeSelect" />
                                   <NodeCheckChange EventHandler="tvBookChapter_onNodeCheckChange" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfItemType" runat="server" />
    </form>
</body>
</html>
