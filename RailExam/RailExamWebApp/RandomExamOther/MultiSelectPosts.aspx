<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiSelectPosts.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.MultiSelectPosts" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择培训类别</title>

    <script type="text/javascript">

        var nodeTexts = new Array();
        var nodeIDs = new Array();
        var nodeText = "";
        var nodeID = "";
        
        function init()
        {
            var search = window.location.search;
            var id = search.substring(search.indexOf("?id=")+4,search.indexOf("&name"));
            var name = search.substring(search.indexOf("&name=")+6);
            
            if(id == "")
            {
                return;
            }
            
            var ids=id.split(',');
            for(var i = 0; i<ids.length;i++)
            {                
                nodeIDs.push(ids[i]);
            }
            
            var names = name.split(',');
            for(var j = 0; j<names.length;j++)
            {                
                nodeTexts.push(names[j]);
            }
        }
        
        function tvPost_NodeCheckChange(sender,eventArgs)
        {
            var node = eventArgs.get_node();
            if(node.get_checked())
            {            
                nodeTexts.push(node.get_text());
                
                nodeIDs.push(node.get_id());
                
                return;
            }
            else
            {
                for(var i = 0; i < nodeTexts.length; i ++)
	            {
	                if(nodeTexts[i])
	                {
	                    if(nodeTexts[i] == node.get_text())
	                    {
	                        nodeTexts[i]= "";
	                    }
	                }
	            }
              
	            for(var i = 0; i < nodeIDs.length; i ++)
	            {
	                if(nodeIDs[i])
	                {
	                    if(nodeIDs[i])
	                    {
	                        if(nodeIDs[i] == node.get_id())
	                        {
	                            nodeIDs[i]= "";
	                        }
	                    }
	                }
	            }
                return; 
            }       
          }

        function selectBtnClicked()
        {
            window.returnValue = getReturnValue();
            window.close();
        }
        
        function getReturnValue()
        {
          
		    for(var i = 0; i < nodeTexts.length; i ++)
		    {
		        if(nodeTexts[i])
		        {
		            if(nodeTexts[i] !="")
		            {
		                if(nodeText != "")
		                {
		                    nodeText += ",";
		                }
		                nodeText += nodeTexts[i];
		            }
		        }
		    }
          
		    for(var i = 0; i < nodeIDs.length; i ++)
		    {
		        if(nodeIDs[i])
		        {
		            if(nodeIDs[i] !="")
		            { 
		                if(nodeID != "")
		                {
		                    nodeID += ",";
		                }
		                nodeID += nodeIDs[i];
		            }
		        }
		    }
            
            return nodeID + "|" + nodeText;
        }

    </script>

</head>
<body onload="init();">
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
                        <ClientEvents>
                            <NodeCheckChange EventHandler="tvPost_NodeCheckChange" />
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
