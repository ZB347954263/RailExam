<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReadBook.aspx.cs" Inherits="RailExamWebApp.Online.Study.ReadBook" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学习教材</title>

    <script type="text/javascript">     
         function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function TreeView1_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {            
                window.frames["bookFrame"].location = "ReadBookDetail.aspx?id="+ node.get_id();
                node.expand();  
            }
        }       
       
        function TreeView2_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {            
                window.frames["bookFrame"].location = "ReadBookDetail.aspx?id1="+ node.get_id();
                node.expand(); 
            }
        }   
        
        
        function rbnClicked(btn)
        {
            if(btn.checked)
            {
                if(btn.id == "rbnKnowledge")
                {
                    $F("divKnowledge").style.display = ""; 
                    $F("divCategory").style.display = "none"; 
                   
                    if(TreeView1.get_selectedNode())
                    {
                        temp = TreeView1.get_selectedNode();
                        window.frames["bookFrame"].location = "ReadBookDetail.aspx?id="+ temp.get_id();
                    }
                    else
                    {
                        window.frames["bookFrame"].location = "ReadBookDetail.aspx?id=0";
                    }                
               }
                else
                {
                    $F("divKnowledge").style.display = "none"; 
                    $F("divCategory").style.display = ""; 
                  
                    if(TreeView2.get_selectedNode())
                    {
                        temp = TreeView2.get_selectedNode();
                        window.frames["bookFrame"].location = "ReadBookDetail.aspx?id1="+ temp.get_id();
                   }
                    else
                    {
                         window.frames["bookFrame"].location = "ReadBookDetail.aspx?id1=0";
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
                <div id="left">
                    <div id="leftHead">
                        <div id="divSwitchBtns" style="text-align: center; white-space: nowrap;">分类:
                            <input id="rbnKnowledge" name="knowledgeOrCategory" onclick="rbnClicked(this);" type="radio"
                                checked="checked" />按知识体系
                            <input id="rbnCategory" name="knowledgeOrCategory" onclick="rbnClicked(this);" type="radio" />按培训类别
                        </div>    
                    </div>
                    <div id="leftContentWithNoHeight">
                        <div id="divKnowledge">
                            <ComponentArt:TreeView ID="TreeView1" runat="server"  EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="TreeView1_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <div id="divCategory" style="display: none;">
                            <ComponentArt:TreeView ID="TreeView2" runat="server"   EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="TreeView2_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                    </div>
                </div>
                <div id="right">
                   <div id="rightHead">
                      教材和课件信息</div> 
                    <div id="rightContentWithNoHeight">
                        <iframe id="bookFrame" src="ReadBookDetail.aspx?id=0" frameborder="0" scrolling="auto" width="100%"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input id="Test1" type="hidden" name="Test1" />
       <script type="text/javascript" language="javascript">
      //document.getElementById("leftContentWithNoHeight").height = 0.9*screen.availHeight;
      document.getElementById("rightContentWithNoHeight").height = 0.9*screen.availHeight;
      document.getElementById("bookFrame").height = 0.9*screen.availHeight;
       </script>
    </form>
</body>
</html>