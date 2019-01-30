<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StudyAtLiberty.aspx.cs"
    Inherits="RailExamWebApp.Online.Study.StudyAtLiberty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">     
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function tvKnowledge_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
                window.frames["ifBookInfo"].location = "StudyAtLibertyDetail.aspx?KnowledgeID="+ node.get_id()+"&PostID="+node.get_value();
                node.expand(); 
            }
        }   
        
        //弹出选择工作岗位窗体
        function selectPost()
        {
            var url1="../../Common/SelectPost.aspx";

            var selectedPost = window.showModalDialog(url1, 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');
            
            if(! selectedPost)
            {
                return;
            }
        	
            var id=selectedPost.split('|')[0];        
            document.getElementById('hfPostID').value = id;
            document.getElementById("hfPostName").value=selectedPost.split('|')[1];
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
                    <asp:Button ID="btnSelectPost" runat="server" Text="选择职名" OnClientClick="selectPost()"
                        OnClick="btnSelectPost_Click" />
                    <span id="spanPostName" runat="server"></span>
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
