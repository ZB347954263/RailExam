<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="MultiSelectItemCategory.aspx.cs" Inherits="RailExamWebApp.Common.MultiSelectItemCategory" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择辅助分类</title>
    <script type="text/javascript">
        function tvItemCategory_onNodeSelect(sender, eventArgs)
        {
        }
       
        var answers = [];
        var answers1 = [];
         
        function selectBtnClicked()
        {
            if(!tvItemCategory.get_selectedNode())
            {
                alert("请选择一个分类！");
                return;
            }

            // alert(getReturnValue());
            window.returnValue = getReturnValue();
            window.close();
        }
        
        function getReturnValue()        
        {          
            for(var i=0; i<tvItemCategory.get_nodes().get_length();i++)
            {
                var node= tvItemCategory.get_nodes().getNode(i);
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
                  
            for(var i = 0; i < node.get_nodes().get_length(); i ++)
            {
                check(node.get_nodes().getNode(i));
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
                    辅助分类</div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvItemCategory" runat="server" EnableViewState="true" KeyboardEnabled="true">
                        <ClientEvents>
                            <NodeSelect EventHandler="tvItemCategory_onNodeSelect" />
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
