<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectType.aspx.cs" Inherits="RailExamWebApp.Train.SelectType" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择培训类别</title>
    <script type="text/javascript">
        function tvType_onLoad(sender, eventArgs)
        {
            tvType.expandAll();
        }
        function tvType_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();            
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:90%; text-align: center;">
            <tr>
                <td style="text-align: center; vertical-align: top;">
                    <div style="text-align: center; height:530px; width: 100%;">
                       <table>
                       <tr><td align="center">
                           <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>相关培训类别</td></tr>
                       <tr><td>                       
                        <ComponentArt:TreeView ID="tvType" runat="server" Height="480" Width="200" EnableViewState="true" DragAndDropEnabled="true"
                                    NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                    KeyboardCutCopyPasteEnabled="false" 
                                    AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvType_onNodeSelect" />
                                        <Load EventHandler="tvType_onLoad" />
                                    </ClientEvents>
                        </ComponentArt:TreeView>
                        </td></tr>  
                        </table>
                    </div>
                </td>
                
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

