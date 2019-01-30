<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MultiSelectPost.aspx.cs"
    Inherits="RailExamWebApp.Common.MultiSelectPost" %>

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
        var removeText ="";
        var removeIDs =  "";
        
        
        var search = window.location.search;
        var indexAnd=location.search.indexOf("&");
        
        //alert("接收到的PostIDs:"+window.location.search.substring(5,indexAnd));
        //alert("接受到的PostNames:"+unescape(search.substring(search.indexOf("&names=")+7,search.indexOf("&postId"))));
        
        nodeIDs=window.location.search.substring(5,indexAnd).split(',');
        nodeTexts=unescape(search.substring(search.indexOf("&names=")+7,search.indexOf("&postId"))).split(',');
        
        var postId = window.location.search.substring(search.indexOf("&postId=")+8);
        
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
                       
            if(postId != "")
            {
                if((","+nodeID+",").indexOf(","+postId+",")>=0)
                {
                    alert("一专多能职名不能包含本职名!");
                    return "";
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
                        <ClientEvents>
                            <NodeCheckChange EventHandler="tvPost_checkChange" />
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
