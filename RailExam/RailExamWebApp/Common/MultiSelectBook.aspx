<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MultiSelectBook.aspx.cs"
    Inherits="RailExamWebApp.Common.MultiSelectBook" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
        function tvBook_onNodeCheckChange(sender,eventArgs)
       {
                var node = eventArgs.get_node();
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
    
        function tvBook_onNodeSelect(sender, eventArgs)
        {
        }
        
        var answers = [];
        var answers1 = [];
        
        function selectBtnClicked()
        {
            window.returnValue = getReturnValue();
            window.close();
        }
    
        function getReturnValue()
        {
            for(var i=0; i<tvBook.get_nodes().get_length();i++)
            {
                var node= tvBook.get_nodes().getNode(i);
                check(node);                                     
            }               
                             
            var res = "";
		    for(var i=0; i<answers.length; i++)
		    {  
		        if(answers[i])
		        {		        
		        if(res!="")
		        {
		        res += ",";
		        }
		            res +=  answers[i];
		        }		        
		    }
          
            var res1 = "";
		    for(var i=0; i<answers1.length; i++)
		    {  
		        if(answers1[i])
		        {		        
		        if(res1!="")
		        {
		        res1 += ",";
		        }
		            res1 +=  answers1[i];
		        }		        
		    }

            return res1+"|"+ res;
        }
        
        function check(node)
        {
            if(node.get_checked() == true)
            {
                var m=node.get_id();
                answers[m] = new Array();               
                answers[m].push(node.get_text());
            
                answers1[m] = new Array();               
                answers1[m].push(m);
            }
                  
            for(var i=0; i<node.get_nodes().get_length();i++)
            {
                check(node.get_nodes().getNode(i));
            }
        }
    </script>

    <script type="text/javascript" src="../Common/JS/JSHelper.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    教材章节&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img onclick="selectBtnClicked();" src="../Common/Image/confirm.gif" alt="" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvBook" runat="server" EnableViewState="true" KeyboardEnabled="true">
                        <ClientEvents>
                            <NodeSelect EventHandler="tvBook_onNodeSelect" />
                            <NodeCheckChange EventHandler="tvBook_onNodeCheckChange" />
                        </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
