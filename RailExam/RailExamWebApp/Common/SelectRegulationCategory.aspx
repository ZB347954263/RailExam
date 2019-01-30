<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectRegulationCategory.aspx.cs" Inherits="RailExamWebApp.Common.SelectRegulationCategory" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ѡ�񷨹����</title>
    <script type="text/javascript">
        function tvRegulationCategory_onNodeMouseDoubleClick(sender, eventArgs)
        {
            selectBtnClicked();
        }
        
        function selectBtnClicked()
        {
            if(! tvRegulationCategory.get_selectedNode())
            {
                alert("��ѡ�񷨹���࣡");
                return;
            }
            
            if(tvRegulationCategory.get_selectedNode().get_nodes().get_length() > 0)
            {
                alert("��ѡ��Ҷ�ӽڵ㣡");
                return;
            }
            
            window.returnValue = getReturnValue(tvRegulationCategory.get_selectedNode());
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
                    �������</div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvRegulationCategory" runat="server" EnableViewState="false">
                        <ClientEvents>
                            <NodeMouseDoubleClick EventHandler="tvRegulationCategory_onNodeMouseDoubleClick" />
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
