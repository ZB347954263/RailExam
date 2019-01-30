<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExerciseManage.aspx.cs" Inherits="RailExamWebApp.Exercise.ExerciseManage" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Á·Ï°</title>
    <script type="text/javascript">
        function tvBookChapter_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            var bookid = document.getElementById("hfBookID").value;
            var chapterID = document.getElementById("hfChapterID").value;

            if(node.get_id() != chapterID && node.get_id()!=0)
            {
		            if(node.get_id() !=0)
		            {
                           window.frames["ifExerciseManageInfo"].location = "ExerciseManageInfo.aspx?bookid="+bookid+"&ChapterId=" + node.get_id();
		            } 
                    document.getElementById("hfChapterID").value = node.get_id();
            }
             else
            {
                    window.frames["ifExerciseManageInfo"].location = "ExerciseManageInfo.aspx?bookid="+node.get_value()+"&ChapterId=0";
                    document.getElementById("hfChapterID").value = node.get_id();
            }

        }
    </script>
</head>
<body oncontextmenu='return false' ondragstart='return false' onselectstart='return false'
    oncopy='document.selection.empty()' onbeforecopy='return false'>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        ½Ì²ÄÕÂ½Ú</div>
                    <div id="leftContentWithNoHeight">
                        <ComponentArt:TreeView ID="tvBookChapter" runat="server" EnableViewState="true">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvBookChapter_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        Á·Ï°</div>
                    <div id="rightContentWithNoHeight">
                        <iframe id="ifExerciseManageInfo"
                            frameborder="0" scrolling="yes"  width="100%"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfBookID" runat="server" />
        <asp:HiddenField ID="hfChapterID" runat="server" />
    </form>
     <script type="text/javascript" language="javascript">
      function GetSelect(nodes,id)
      {
             for(var i=0;i<nodes.get_length();i++)
            {
                if(nodes.getNode(i).get_value() == id)
               {
                    nodes.getNode(i).select();
                    break;
               } 
               else
               {
                    if(nodes.getNode(i).get_nodes())
                    {
                        GetSelect(nodes.getNode(i).get_nodes(),id);
                    }
               }
            }
      } 
      //document.getElementById("leftContentWithNoHeight").height = 0.9*screen.availHeight;
      document.getElementById("rightContentWithNoHeight").height = 0.9*screen.availHeight;
      document.getElementById("ifExerciseManageInfo").height = 0.9*screen.availHeight;
        if(tvBookChapter && tvBookChapter.get_nodes().get_length() > 0)
        {
            var search = window.location.search;
            if(search.indexOf("ChapterID") >=0)
            {
                var chapterid= search.substring(search.indexOf("ChapterID=")+10);
                var bookid = search.substring(search.indexOf("id=")+3,search.indexOf("&")); 
                //alert(chapterid); 
                GetSelect(tvBookChapter.get_nodes(),chapterid); 
            }
            else
            {
                tvBookChapter.get_nodes().getNode(0).select();
            }
        } 
     </script> 
</body>
</html>
