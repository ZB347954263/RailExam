<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainStandardType.aspx.cs" Inherits="RailExamWebApp.Train.TrainStandardType" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
    
        function AddRecord(id)
		{			
	        var re= window.open("TrainStandardTypeSelect.aspx?id="+id,'TrainStandardTypeSelect',' Width=300px; Height=600px,status=false,resizable=yes',true);		
			re.focus();  				    
		}
		
       function tvTrainType_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemEdit = tvTrainTypeMenu.findItemByProperty("Text", "编辑");            
            var menuItemSave = tvTrainTypeMenu.findItemByProperty("Text", "保存");
            var menuItemCancel = tvTrainTypeMenu.findItemByProperty("Text", "取消");
            
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
            
            if(!tvTrainType.get_selectedNode() || 
                tvTrainType.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }
        
            switch(eventArgs.get_node().get_text())
            {                
                default:
                if(tvTrainType.get_selectedNode().get_id() == eventArgs.get_node().get_id()){
                    tvTrainTypeMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node()); 
                }
                break; 
            }      
            return true;
        }
        
        function tvTrainTypeMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();     
            var contextDataNode = menuItem.get_parentMenu().get_contextData(); 
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifTrainStandardDetail"].document.forms[0];

            switch(menuItem.get_text())
            {
                case '展开':
                    contextDataNode.expand();
                    break;
                    
                case '折叠':
                    contextDataNode.collapse();
                    break;
                    
                case '展开全部':
                    tvTrainType.expandAll();
                    break;
                    
                case '折叠全部':
                    tvTrainType.collapseAll();
                    break;
                    
                case '查看':
                    if(tvTrainType.get_selectedNode() == contextDataNode)
                    {
                        return false;
                    }
                    contextDataNode.select();
                    break;
            
                case '编辑':
                    contextDataNode.select();
                    var theBtn = window.frames["ifTrainStandardDetail"].document.getElementById("fvTrainStandard_EditButton");
                    
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
                        theBtn = window.frames["ifTrainStandardDetail"].document.getElementById("fvTrainStandard_UpdateButton");
                    }
                    if(theForm && theBtn && theItem)
                    {
                        theItem.value = "";
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                    }
                    tvTrainTypeChangeCallBack.callback(contextDataNode.get_value(), "Rebuild");
                    //window.location.reload();                
                    break;     
                    
                case '取消':
                    var theBtn = window.frames["ifTrainStandardDetail"].document.getElementById("fvTrainStandard_InsertCancelButton");                    
                    if(theForm && theBtn)
                    {
                        theItem.value = "";
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                    }  
                    break;
                
                case '删除':
                    var theBtn = window.frames["ifTrainStandardDetail"].document.getElementById("fvTrainStandard_DeleteButton");
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


                        tvTrainType.get_selectedNode().remove();
                        if(tvTrainType.get_nodes().get_length() > 0)
                        {
                            tvTrainType.get_nodes().getNode(0).select();
                        }
                    }                
                    break;     
            } 
            
            return true; 
        }
        function tvTrainType_onLoad(sender, eventArgs)
        {
            tvTrainType.expandAll();
        }
        function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifTrainStandardDetail"];
            var theFrame1 = window.frames["ifTrainCourse"];
            var theItem = document.getElementById("hfSelectedMenuItem");
            if(theItem && (theItem.value == "新增类别" || theItem.value == "编辑"))
            {
                if(confirm("要保存数据吗？"))
                {
                    tvTrainTypeMenu_onItemSelect(sender, tvTrainTypeMenu.findItemByProperty("Text", "保存"));
                    return;                    
                }
                theItem.value = "";               
            }
             theFrame.location = "TrainStandardDetail.aspx?id=" + node.get_id();
             theFrame1.location = "TrainCourse.aspx?id=" + node.get_id();

        }
        
        function tvTrainTypeChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            tvTrainType.expandAll();
            tvTrainType.selectNodeById(sender.get_parameter());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:90%; text-align: left;">
            <tr>
                <td style="width:40%; text-align: center;">培训类别</td>
                <td style="text-align: center;">培训规范</td>
                <td style="text-align: center; white-space: nowrap;"></td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top;">
                    <div style="text-align: left; width: 100%;">
                       <table>
                            <tr>
                                <td valign="top">
                                     <asp:Button ID="btnAddType" text="新增类别"  runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Button ID="btnInsertType" text="导入模版"  runat="server" OnClick="btnInsertType_Click" />
                                </td>
                            </tr>
                           <tr>
                              <td>
                                   <ComponentArt:CallBack ID="tvTrainTypeChangeCallBack" runat="server" OnCallback="tvTrainTypeChangeCallBack_Callback">
                                         <Content>
                                            <ComponentArt:TreeView ID="tvTrainType" runat="server" Height="440" Width="170" EnableViewState="true" DragAndDropEnabled="true"
                                                NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                                KeyboardCutCopyPasteEnabled="false" DefaultTarget="ifTrainStandardDetail"
                                                AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                                 <ClientEvents>
                                                        <ContextMenu EventHandler="tvTrainType_onContextMenu" />
                                                        <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                                                 </ClientEvents>
                                            </ComponentArt:TreeView>
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="tvTrainTypeChangeCallBack_onCallbackComplete" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                    <ComponentArt:Menu ID="tvTrainTypeMenu" runat="server" SiteMapXmlFile="TrainStandardTypeXml.xml">
                                        <ClientEvents>
                                            <ItemSelect EventHandler="tvTrainTypeMenu_onItemSelect" />
                                        </ClientEvents>
                                    </ComponentArt:Menu>
                                </td>
                            </tr>
                        </table>
                     </div>
                </td>
                <td style="text-align: left; vertical-align: top;" colspan="2" >
                    <table>
                        <tr>
                            <td>
                                <iframe id="ifTrainStandardDetail" frameborder="0" width="470" height="270" scrolling="no">
                                </iframe>
                            </td>
                        </tr>
                        <tr>
                            <td align = "center">
                                培训课程
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <iframe id="ifTrainCourse" frameborder="0" width="470" height="200" scrolling="auto">
                                </iframe>
                           </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
    </div>
    <input id="Refresh" type="hidden" name="Refresh" />
    </form>
    <script type="text/javascript">
        var theFrame = window.frames["ifTrainStandardDetail"];   
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
        
        var theFrame = window.frames["ifTrainCourse"];
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>
</body>
</html>
