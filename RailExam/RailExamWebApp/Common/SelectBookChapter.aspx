<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectBookChapter.aspx.cs" Inherits="RailExamWebApp.Common.SelectBookChapter" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ѡ��̲��½�</title>
    <script src="../Common/JS/JSHelper.js"></script>
    <script type="text/javascript">
        function tvBookChapter_onNodeSelect(sender, eventArgs)
        {
        }

        function selectBtnClicked()
        {
            if (window.location.search.indexOf("flag") > -1)
            {
                var returnValues = "";
                
                for(var i = 0; i < tvBookChapter.get_nodes().get_length(); i ++)
                {
                    returnValues += getReturnValues(tvBookChapter.get_nodes().getNode(i))
                }
                
                if(returnValues.length == 0)
                {
                    alert("�����ٵ�ѡ��һ���½ڣ�");
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
                if(! tvBookChapter.get_selectedNode())
                {
                    alert("��ѡ��һ���½ڣ�");
                    return;
                }
                if(tvBookChapter.get_selectedNode().getProperty("isChapter") != "true")
                {
                    alert("��ѡ��Ĳ���һ���½ڣ�");
                    return;
                }
                
                // alert(getReturnValue(tvBookChapter.get_selectedNode()));
                window.returnValue = getReturnValue(tvBookChapter.get_selectedNode());
                window.close();
            }
        }
        
        function getReturnValue(node)
        {
            if(! node)
            {
                alert("��Ч�Ľڵ㣡");
                return;
            }
            
            var bookId = "";
            var chapterId = node.get_id();
            var textPath = node.get_text() + "\\";
            var tempNode = node;
            
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
            
            return (bookId + "|" + chapterId + "|" + textPath);
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
        
        function ddlView_onChange()
        {
            if ($F("ddlView").selectedIndex == 0)
            {
                ddlViewChangeCallBack.callback("VIEW_KNOWLEDGE");
            }
            else
            {
                ddlViewChangeCallBack.callback("VIEW_TRAINTYPE");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    �̲��½�&nbsp;&nbsp;<img onclick="selectBtnClicked();" src="../Common/Image/confirm.gif" alt="" />
                </div>
                <div>
                    �鿴��ʽ
                    <select id="ddlView" onchange="ddlView_onChange();">
                        <option>��֪ʶ��ϵ</option>
                        <option>����ѵ���</option>
                    </select>
                </div>
                <div id="mainContent">
                    <ComponentArt:CallBack ID="ddlViewChangeCallBack" runat="server" PostState="true"
                        OnCallback="ddlViewChangeCallBack_Callback">
                        <Content>
                            <ComponentArt:TreeView ID="tvBookChapter" runat="server">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvBookChapter_onNodeSelect" />
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
