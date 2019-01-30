<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectKnowledge.aspx.cs" Inherits="RailExamWebApp.Common.SelectKnowledge" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ѡ��֪ʶ��ϵ</title>
    <script type="text/javascript">
        function tvKnowledge_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            if(! tvKnowledge.get_selectedNode())
            {
                alert("��ѡ��֪ʶ��ϵ��");
                return;
            }
            
            if(tvKnowledge.get_selectedNode().get_nodes().get_length() > 0)
            {
                alert("��ѡ��Ҷ�ӽڵ㣡");
                return;
            }
            
            window.returnValue = getReturnValue(tvKnowledge.get_selectedNode());
            window.close();
        }
        
        function getReturnValue(node)
        {
            if(! node)
            {
                alert("��Ч�Ľڵ㣡");
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
                    ֪ʶ��ϵ&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  
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
