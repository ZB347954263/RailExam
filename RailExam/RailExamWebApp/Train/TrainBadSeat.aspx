<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainBadSeat.aspx.cs" Inherits="RailExamWebApp.Train.TrainBadSeat"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <title>无标题页</title>
    <script type="text/javascript" language="javascript">
        function STR_onLoad(sender, eventArgs)
        {
            SelectSeatTree.expandAll();
        }
        function STR_onNodeSelect(sender, eventArgs)
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
                        <tr align="center">
                            <td>
                            <asp:ImageButton ID="btnConfirm" ImageUrl="~/Common/Image/confirm.gif" runat="server" OnClick="btnConfirm_Click"/>
                                <%--<asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click"></asp:Button>--%>
                            </td>
                        </tr>
                       <%--<tr><td align="center">
                           <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>损坏机位</td></tr>--%>
                       <tr><td>              
                        <ComponentArt:TreeView ID="SelectSeatTree" runat="server" Height="480" Width="200" EnableViewState="true" DragAndDropEnabled="true"
                                    NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                    KeyboardCutCopyPasteEnabled="false" 
                                    AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="STR_onNodeSelect" />
                                        <Load EventHandler="STR_onLoad" />
                                    </ClientEvents>
                        </ComponentArt:TreeView>
                        </td></tr>  
                        </table>
                    </div>
                </td>
                
            </tr>
           
        </table>
    </div>
    </form>
</body>
</html>
