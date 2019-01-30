<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainStandard.aspx.cs" Inherits="RailExamWebApp.Train.TrainStandard" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>培训规范</title>
    <script type="text/javascript">
        function tvPost_onLoad(sender, eventArgs)
        {
            tvPost.expandAll();
        }
        function tvPost_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();            
            var theFrame = window.frames["ifTrainStandardType"];
            theFrame.location = "TrainStandardType.aspx?id=" + node.get_id();
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:90%; text-align: center;">
            <tr>
                <td style="width:25%; text-align: center; vertical-align: top;">
                    <div style="text-align: left; height:530px; width: 100%;">
                       <table>
                       <tr><td align="center">工作岗位</td></tr>
                       <tr><td>                       
                        <ComponentArt:TreeView ID="tvPost" runat="server" Height="470" Width="180" EnableViewState="true" DragAndDropEnabled="true"
                                    NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                    KeyboardCutCopyPasteEnabled="false"  DefaultTarget="ifTrainStandardType"
                                    AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvPost_onNodeSelect" />
                                        <Load EventHandler="tvPost_onLoad" />
                                    </ClientEvents>
                        </ComponentArt:TreeView>
                        </td></tr>  
                        </table>
                    </div>
                </td>
                <td style="text-align: left; vertical-align: top;">
                     <iframe id="ifTrainStandardType" frameborder="0" width="620" height="500" scrolling="no">
                    </iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        var theFrame = window.frames["ifTrainStandardType"];        
        theFrame.location = "TrainStandardType.aspx?id=" + tvPost.get_nodes().getNode(0).get_id();
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>
</body>
</html>
