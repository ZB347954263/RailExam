<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudySelectTree.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudySelectTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <script type="text/javascript">     
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function tvKnowledge_onNodeSelect(sender, eventArgs)
        {
        	var search = window.location.search;
             var str= search.substring(search.indexOf("?")+1);           
            var node = eventArgs.get_node();
            if(node)
            {
                window.frames["ifBookInfo"].location = "StudySelectBook.aspx?KnowledgeID="+ node.get_id()+"&"+str;
                node.expand(); 
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
                        知识体系</div>
                    <div id="leftContent">
                        <div id="divKnowledge">
                            <ComponentArt:TreeView ID="tvKnowledge" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvKnowledge_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifBookInfo" src="#" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfPostName" runat="server" />
    </form>
</body>
</html>
