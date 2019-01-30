<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectMoreOrgGroupBySystem.aspx.cs" Inherits="RailExamWebApp.Common.SelectMoreOrgGroupBySystem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base target="_self" />
    <title>上报单位</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
 <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <style type="text/css">
</style>

    <script type="text/javascript">
     function tv1_onNodeCheckChange(sender, eventArgs)
        {
            var node = eventArgs.get_node();

            if(node.get_nodes().get_length() > 0)
            {
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
     function col (obj) {
     	window.returnValue = obj;
     	window.close();
     }
     
  function checkAll() 
  {
    	    for(var i = 0; i < tvOrg.get_nodes().get_length(); i ++)
            {  
                    if(form1.chkOrg.checked)
                    {
                        tvOrg.get_nodes().getNode(i).checkAll();
                    }
                    else
                    {
                        tvOrg.get_nodes().getNode(i).unCheckAll();
                    }
                     tvOrg.get_nodes().getNode(i).set_checked(form1.chkOrg.checked);   
            }
   }
        
         function tvOrg_onNodeCheckChange(sender, eventArgs)
        { 
                var node = eventArgs.get_node();
                 var state = node.get_checked();
                
                if(state)
                { 
                    node.checkAll();
                    node.set_checked(state); 
                    checkUp(node,node.get_checked());
                }
                else
                {
                    node.unCheckAll();
                    node.set_checked(state); 
                    var n=0;
                    if(node.get_parentNode())
                    {
                        for(var i=0;i<node.get_parentNode().get_nodes().get_length();i++)
                        {
                            if(node.get_parentNode().get_nodes().getNode(i).get_checked())
                            {
                                n = n + 1;
                            }
                        }
                        if(n == 0)
                        {
                            UnChecked(node);
                            IsTop(node);
                        }
                    }
                } 
                
        }
  
      
    function checkUp(node,state)
    {
        node.set_checked(state);
        if(node.get_parentNode())
        {
            checkUp(node.get_parentNode(),state);
        }
    }    
    
     function UnChecked(node)
    {
        if(node.get_parentNode())
        {
           node.get_parentNode().set_checked(false);
           UnChecked(node.get_parentNode());
        }
    }
    
    function IsTop(node)
    {
        if(node.get_parentNode())
        {
            IsTop(node.get_parentNode());
        }
        else
        {
             IsCheck(node);
        }
    }
    
    function CheckParent(node)
    {
        if(node.get_parentNode())
        {
           node.get_parentNode().set_checked(true);
           CheckParent(node.get_parentNode());
        }
    }
    
    function IsCheck(node)
    {
        if(node.get_nodes().get_length() > 0)
        {
            for(var i=0;i<node.get_nodes().get_length();i++)
            { 
                if(node.get_nodes().getNode(i).get_checked())
                {
                     CheckParent(node.get_nodes().getNode(i));                        
                }
                else
                {
                    IsCheck(node.get_nodes().getNode(i));
                }
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
                    组织机构&nbsp;&nbsp;&nbsp; <input type="checkbox" onclick="checkAll()" name="chkOrg" />全选&nbsp;&nbsp;
                    <asp:Button ID="btnOK" runat="server" Text="确  定" CssClass="button" OnClick="btnOK_Click" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvOrg" runat="server" Height="530" Width="300" EnableViewState="true"
                        KeyboardEnabled="true">
                         <ClientEvents>
                           <NodeCheckChange EventHandler="tvOrg_onNodeCheckChange" />
                         </ClientEvents>
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
