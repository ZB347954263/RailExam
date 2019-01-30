<%@ Page Language="C#" AutoEventWireup="True" Codebehind="MultiSelectBookChapter.aspx.cs"
    Inherits="RailExamWebApp.Common.MultiSelectBookChapter" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
        function tvKnowledge_onNodeCheckChange(sender,eventArgs)
       {
                var node = eventArgs.get_node();
                if(node.getProperty("isBook") == "true")
                 {
                    node.set_contentCallbackUrl( "../Common/GetBookChapter.aspx?flag=1&state=" + node.get_checked() + "&id=" + node.get_id());
                    node.expand();
                  }
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
    
        function tvKnowledge_onNodeSelect(sender, eventArgs)
        {
        }
        
        var answers = [];
        var answers1 = [];
        
        function selectBtnClicked()
        {
            // window.returnValue = getReturnValue(tvKnowledge.get_selectedNode());
            window.returnValue = getReturnValue();
            window.close();
        }
    
        function getReturnValue()
        {
            for(var i=0; i<tvKnowledge.get_nodes().get_length();i++)
            {
                var node= tvKnowledge.get_nodes().getNode(i);
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

    <script src="../Common/JS/JSHelper.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    教材章节&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img onclick="selectBtnClicked();" src="../Common/Image/confirm.gif" alt="" />
                </div>
                <div>
                    查看方式
                    <select id="ddlView" onchange="ddlView_onChange();">
                        <option>按知识体系</option>
                        <option>按培训类别</option>
                    </select>
                </div>
                <div id="mainContent">
                    <ComponentArt:CallBack ID="ddlViewChangeCallBack" runat="server" PostState="true"
                        OnCallback="ddlViewChangeCallBack_Callback">
                        <Content>
                            <ComponentArt:TreeView ID="tvKnowledge" runat="server" EnableViewState="true" KeyboardEnabled="true">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvKnowledge_onNodeSelect" />
                                    <NodeCheckChange EventHandler="tvKnowledge_onNodeCheckChange" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
