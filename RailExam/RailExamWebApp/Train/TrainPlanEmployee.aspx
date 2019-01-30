<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainPlanEmployee.aspx.cs" Inherits="RailExamWebApp.Train.TrainPlanEmployee" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择参加培训的员工</title>
    <script type="text/javascript">
        function tvOrg_onLoad(sender, eventArgs)
        {
            tvOrg.expandAll();
        }
        function tvOrg_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var search = window.location.search;
            var str = search.substring(search.indexOf("?")+1);
            var node = eventArgs.get_node();            
            var theFrame = window.frames["ifTrainEmployeeSelect"];
            //alert("TrainPlanEmployeeSelect.aspx?id=" + node.get_id()+"&"+str);
            theFrame.location = "TrainPlanEmployeeSelect.aspx?id=" + node.get_id()+"&"+str;
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
                       <tr><td align="center">组织机构</td></tr>
                       <tr><td>                       
                        <ComponentArt:TreeView ID="tvOrg" runat="server" Height="520" Width="200" EnableViewState="true" DragAndDropEnabled="true"
                                    NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                    KeyboardCutCopyPasteEnabled="false"  DefaultTarget="ifTrainEmployeeSelect"
                                    AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvOrg_onNodeSelect" />
                                        <Load EventHandler="tvOrg_onLoad" />
                                    </ClientEvents>
                        </ComponentArt:TreeView>
                        </td></tr>  
                        </table>
                    </div>
                </td>
                <td style="text-align: left; vertical-align: top;">
                     <iframe id="ifTrainEmployeeSelect" frameborder="0" width="500" height="500" scrolling="no">
                    </iframe>
                </td>
            </tr>
            <tr align="center">
                <td colspan="3">
                <asp:Button ID="btnUp" runat="server" Text="上一步" OnClick="btnUp_Click"  /> &nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Button ID="btnCancel" runat="server" Text="完&nbsp; &nbsp; 成" OnClick="btnCancel_Click" /></td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        var search = window.location.search;
        var str = search.substring(search.indexOf("?")+1);
        var theFrame = window.frames["ifTrainEmployeeSelect"];        
        theFrame.location = "TrainPlanEmployeeSelect.aspx?id=" + tvOrg.get_nodes().getNode(0).get_id()+"&"+str;
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>
</body>
</html>
