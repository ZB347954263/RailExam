<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainCourseBook.aspx.cs" Inherits="RailExamWebApp.Train.TrainCourseBook" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑课程相关教材信息</title>
    <script type="text/javascript">
    
    function tvChooseBook_onNodeCheckChange(sender, eventArgs)
    {   
        var node = eventArgs.get_node();
        check(node,node.get_checked());
        if(node.get_checked() == false)
        {
            UnChecked(node);
        }
    }
    function tvBook_onNodeCheckChange(sender, eventArgs)
    {
         var node = eventArgs.get_node();
         var state = node.get_checked();
        
        if(state == true)
        { 
            check(node,node.get_checked());
            checkUp(node,node.get_checked());
        }
        else
        {
            check(node,node.get_checked());
            node.set_checked(false);
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
        
    
    function check(node,state)
    {
        node.set_checked(state);
        for(var i=0; i<node.get_nodes().get_length();i++)
        {
            check(node.get_nodes().getNode(i),state);
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
    
    function tvChooseBook_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemEdit = tvChooseBookMenu.findItemByProperty("Text", "编辑");            
            var menuItemSave = tvChooseBookMenu.findItemByProperty("Text", "保存");
            var menuItemCancel = tvChooseBookMenu.findItemByProperty("Text", "取消");
            
            if(theItem == "新增类别" || theItem == "编辑")
            {
                menuItemEdit.set_enabled(false);
                menuItemSave.set_enabled(true);
                menuItemCancel.set_enabled(true);
            }
            else
            {
                menuItemEdit.set_enabled(true);            
                menuItemSave.set_enabled(false);
                menuItemCancel.set_enabled(false);            
            }
            
            if(!tvChooseBook.get_selectedNode() || 
                tvChooseBook.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }
        
            switch(eventArgs.get_node().get_text())
            {                
                default:
                if(tvChooseBook.get_selectedNode().get_id() == eventArgs.get_node().get_id()){
                    tvChooseBookMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node()); 
                }
                break; 
            }      
            return true;
        }
        
        function tvChooseBookMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();     
            var contextDataNode = menuItem.get_parentMenu().get_contextData(); 
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifTrainCourseBookDetail"].document.forms[0];

            switch(menuItem.get_text())
            {
                case '展开':
                    contextDataNode.expand();
                    break;
                    
                case '折叠':
                    contextDataNode.collapse();
                    break;
                    
                case '展开全部':
                    tvChooseBook.expandAll();
                    break;
                    
                case '折叠全部':
                    tvChooseBook.collapseAll();
                    break;
                    
                case '查看':
                    if(tvChooseBook.get_selectedNode() == contextDataNode)
                    {
                        return false;
                    }
                    contextDataNode.select();
                    break;
            
                case '编辑':
                    contextDataNode.select();
                    var theBtn = window.frames["ifTrainCourseBookDetail"].document.getElementById("fvTrainCourseBook_EditButton");
                    
                    if(theForm && theBtn && theItem)
                    {
                        theItem.value = "编辑";
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                                        
                        theForm.submit();
                    }
                    break;
                    
                case '保存':
                    var theBtn;
                    if(theItem && theItem.value == "编辑")
                    {
                        theBtn = window.frames["ifTrainCourseBookDetail"].document.getElementById("fvTrainCourseBook_UpdateButton");
                    }
                    if(theForm && theBtn && theItem)
                    {
                        theItem.value = "";
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                    }
                    tvChooseBookChangeCallBack.callback(contextDataNode.get_value(), "Rebuild");
                    //window.location.reload();                
                    break;     
                    
                case '取消':
                    var theBtn = window.frames["ifTrainCourseBookDetail"].document.getElementById("fvTrainCourseBook_InsertCancelButton");                    
                    if(theForm && theBtn)
                    {
                        theItem.value = "";
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                    }  
                    break;
                
                case '删除':
                    var theBtn = window.frames["ifTrainCourseBookDetail"].document.getElementById("fvTrainCourseBook_DeleteButton");
                    if(theForm&&theBtn)
                    {
                        if(!confirm("您确定要删除“" + contextDataNode.get_text() + "”吗？"))
                        {
                            return false; 
                        }
                        if(contextDataNode.get_nodes().get_length() > 0)
                        {
                            alert("节点有子节点，不能删除！");
                            return false;
                        }
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                        
                        // 删除该节点并选择第一个节点


                        tvChooseBook.get_selectedNode().remove();
                        if(tvChooseBook.get_nodes().get_length() > 0)
                        {
                            tvChooseBook.get_nodes().getNode(0).select();
                        }
                    }                
                    break;     
            } 
            
            return true; 
        }
        function tvChooseBook_onLoad(sender, eventArgs)
        {
            tvChooseBook.expandAll();
        }
        function tvChooseBook_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifTrainCourseBookDetail"];
            var theItem = document.getElementById("hfSelectedMenuItem");
            if(theItem && (theItem.value == "新增类别" || theItem.value == "编辑"))
            {
                if(confirm("要保存数据吗？"))
                {
                    tvChooseBookMenu_onItemSelect(sender, tvChooseBookMenu.findItemByProperty("Text", "保存"));
                    return;                    
                }
                theItem.value = "";               
            }
             //alert(node.get_id());
             theFrame.location = "TrainCourseBookDetail.aspx?id=" + node.get_id();

        }
        
        function tvChooseBookChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            tvChooseBook.expandAll();
            tvChooseBook.selectNodeById(sender.get_parameter());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
    <table style="width:90%; text-align: center;">
            <tr>
                <td style="width:25%; text-align: center;">
                    <asp:Label ID="lblTitle1" runat="server" Text=""></asp:Label></td>
                <td style="width:5%"></td>
                <td style="text-align: center; width:25%">
                    <asp:Label ID="lblTitle2" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: center; vertical-align: top;">
                     <div style="text-align: center; width: 100%;">
                        <ComponentArt:TreeView ID="tvBook" Width="300" Height="530" EnableViewState="true" DragAndDropEnabled="false"
                            NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                            KeyboardCutCopyPasteEnabled="false" runat="server"
                            AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                            <ClientEvents>
                                <NodeCheckChange EventHandler="tvBook_onNodeCheckChange" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </td>
                <td align="center" valign="top" > 
                    <table style="height: 388px">
                        <tr>
                            <td valign="middle">
                                <asp:Button ID="btnChoose" runat="server" Text=">>" OnClick="btnChoose_Click" /><br /><br /><br /><br />
                                <asp:Button ID="btnDel" runat="server" Text="<<" OnClick="btnDel_Click" /></td>
                        </tr>
                    </table>
                 </td>
                <td style="text-align: center; vertical-align: top;">
                    <table style="width:100%">
                        <tr>
                            <td style="height:250">
                             <div style="text-align: left; width: 100%; vertical-align:top;">
                                <ComponentArt:CallBack ID="tvChooseBookChangeCallBack" runat="server" OnCallback="tvChooseBookChangeCallBack_Callback">
                                     <Content>
                                        <ComponentArt:TreeView ID="tvChooseBook" Width="300" Height="320" EnableViewState="true" DragAndDropEnabled="false"
                                                NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                                KeyboardCutCopyPasteEnabled="false" runat="server" DefaultTarget="ifTrainCourseBookChapter"
                                                AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                                <ClientEvents>
                                                        <ContextMenu EventHandler="tvChooseBook_onContextMenu" />
                                                        <NodeSelect EventHandler="tvChooseBook_onNodeSelect" />
                                                        <NodeCheckChange EventHandler="tvChooseBook_onNodeCheckChange" />
                                                 </ClientEvents>                               
                                        </ComponentArt:TreeView>
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="tvChooseBookChangeCallBack_onCallbackComplete" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                                <ComponentArt:Menu ID="tvChooseBookMenu" runat="server" SiteMapXmlFile="TrainCourseBookTree.xml">
                                    <ClientEvents>
                                        <ItemSelect EventHandler="tvChooseBookMenu_onItemSelect" />
                                    </ClientEvents>
                                </ComponentArt:Menu>
                            </div>
                            </td>
                        </tr>
                        <tr>
                             <td style="text-align: left; vertical-align: top;">
                                <iframe id="ifTrainCourseBookDetail" frameborder="0" width="300" height="200" scrolling="no">
                                </iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnUp" runat="server" Text="上一步" OnClick="btnUp_Click" />
                     &nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="下一步" />&nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取&nbsp; &nbsp; 消" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
    </div>
    </form>
    <script type="text/javascript">
        var theFrame = window.frames["ifTrainCourseBookDetail"];        
        theFrame.location = "TrainCourseBookDetail.aspx?id=" + tvChooseBook.get_nodes().getNode(0).get_id();
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
   </script>
</body>
</html>
