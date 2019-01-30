<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectOtherPost.aspx.cs" Inherits="RailExamWebApp.Common.SelectOtherPost" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择工作岗位</title>

    <script type="text/javascript">
       var nodeTexts = new Array();
        var nodeIDs = new Array();
        var nodeText = "";
        var nodeID = "";
        var removeText ="";
        var removeIDs =  "";
        
        
        var search = window.location.search;
        nodeIDs=search.substring(search.indexOf("?id=")+4,search.indexOf("&names=")).split(',');
        nodeTexts=unescape(search.substring(search.indexOf("&names=")+7)).split(',');    
        
        function selectBtnClicked()
        {
            var result = getReturnValue();

        	//alert(result);
        	
            if(result == "" || result=="|")
            {
                return;
            }           
            
            window.returnValue = result;
            
            //alert("返回出去的IDs和Names:"+result);
            
        	window.close();
        }
        
        function getReturnValue()
        {
        	nodeID = "";
            nodeText ="";
        	
		    for(var i = 0; i < nodeTexts.length; i ++)
		    {
		        if((","+nodeText+",").indexOf(","+nodeTexts[i]+",")<0)
		        {
		            if((","+removeText+",").indexOf(","+nodeTexts[i]+",")<0)
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
		         if((","+nodeID+",").indexOf(","+nodeIDs[i]+",")<0)
		        {
		            if((","+removeIDs+",").indexOf(","+nodeIDs[i]+",")<0)
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
        
        function tvPost_checkChange(sender, eventArgs)
        {       
            var node = eventArgs.get_node();
            if(node.get_checked())
            {            
                nodeTexts.push(node.get_text());
                
                nodeIDs.push(node.get_id());
            }
            else
            {
                if(removeText !="")
                {
                    removeText = removeText+ "," + node.get_text();
                }
                else
                {
                    removeText = node.get_text();
                }
                
                if(removeIDs !="")
                {
                    removeIDs = removeIDs+ "," + node.get_id();
                }
                else
                {
                    removeIDs = node.get_id();
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    工作岗位&nbsp;&nbsp;<img id="SaveButton" onclick="selectBtnClicked();" src="../Common/Image/confirm.gif"
                        alt="" style="cursor: hand;" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvPost" runat="server" EnableViewState="false">
                        <ClientEvents>
                             <NodeCheckChange EventHandler="tvPost_checkChange" />
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
                <br />
            </div>
        </div>
        <asp:HiddenField ID="hfSource" runat="server" />
    </form>
</body>
</html>
