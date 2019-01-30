<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectPostByTrainClass.aspx.cs"
    Inherits="RailExamWebApp.RandomExamOther.SelectPostByTrainClass" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择职名</title>

    <script type="text/javascript">
        var nodeTexts = new Array();
        var nodeIDs = new Array();
        var nodeText = "";
        var nodeID = "";

        function selectBtnClicked()
        {
            window.returnValue = getReturnValue();
            window.close();
        }
        
        function getReturnValue()
        {
            for(var i = 0; i < tvPost.get_nodes().get_length(); i ++)
            {
                var node = tvPost.get_nodes().getNode(i);
                check(node);
            }
             
		    for(var i = 0; i < nodeTexts.length; i ++)
		    {
		        if(nodeTexts[i])
		        {
		            if(nodeText != "")
		            {
		                nodeText += ",";
		            }
		            nodeText += nodeTexts[i];
		        }
		    }
          
		    for(var i = 0; i < nodeIDs.length; i ++)
		    {
		        if(nodeIDs[i])
		        {
		            if(nodeID != "")
		            {
		                nodeID += ",";
		            }
		            nodeID += nodeIDs[i];
		        }
		    }
            
            return nodeID + "|" + nodeText;
        }
        
        function check(node)
        {
            if(node.get_checked())
            {
                var strNodeText = GetNodeText(node.get_text(), node);
            
                nodeTexts.push(strNodeText);
                
                nodeIDs.push(node.get_id());
                
                return;
            }
            
            for(var i = 0; i < node.get_nodes().get_length(); i ++)
            {
                check(node.get_nodes().getNode(i));
            }
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

    <script src="../Common/JS/JSHelper.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    职名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img onclick="selectBtnClicked();" style="cursor: hand;" src="../Common/Image/confirm.gif"
                        alt="" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvPost" runat="server" EnableViewState="true" KeyboardEnabled="true">
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
