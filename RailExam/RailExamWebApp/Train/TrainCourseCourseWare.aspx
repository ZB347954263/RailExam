<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainCourseCourseWare.aspx.cs" Inherits="RailExamWebApp.Train.TrainCourseCourseWare" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�༭�γ���ؿμ���Ϣ</title>
    <script type="text/javascript">
        function tvChooseWare_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemEdit = tvChooseWareMenu.findItemByProperty("Text", "�༭");            
            var menuItemSave = tvChooseWareMenu.findItemByProperty("Text", "����");
            var menuItemCancel = tvChooseWareMenu.findItemByProperty("Text", "ȡ��");
            
            if(theItem == "�༭")
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
            
            if(!tvChooseWare.get_selectedNode() || 
                tvChooseWare.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }
        
            switch(eventArgs.get_node().get_text())
            {                
                default:
                if(tvChooseWare.get_selectedNode().get_id() == eventArgs.get_node().get_id()){
                    tvChooseWareMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node()); 
                }
                break; 
            }      
            return true;
        }
        
        function tvChooseWareMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();     
            var contextDataNode = menuItem.get_parentMenu().get_contextData(); 
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifTrainCourseCourseWareDetail"].document.forms[0];

            switch(menuItem.get_text())
            {
                case 'չ��':
                    contextDataNode.expand();
                    break;
                    
                case '�۵�':
                    contextDataNode.collapse();
                    break;
                    
                case 'չ��ȫ��':
                    tvChooseWare.expandAll();
                    break;
                    
                case '�۵�ȫ��':
                    tvChooseWare.collapseAll();
                    break;
                    
                case '�鿴':
                    if(tvChooseWare.get_selectedNode() == contextDataNode)
                    {
                        return false;
                    }
                    contextDataNode.select();
                    break;
            
                case '�༭':
                    contextDataNode.select();
                    var theBtn = window.frames["ifTrainCourseCourseWareDetail"].document.getElementById("fvTrainCourseCourseWare_EditButton");
                    
                    if(theForm && theBtn && theItem)
                    {
                        theItem.value = "�༭";
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                                        
                        theForm.submit();
                    }
                    break;
                    
                case '����':
                    var theBtn;
                    if(theItem && theItem.value == "�༭")
                    {
                        theBtn = window.frames["ifTrainCourseCourseWareDetail"].document.getElementById("fvTrainCourseCourseWare_UpdateButton");
                    }
                    if(theForm && theBtn && theItem)
                    {
                        theItem.value = "";
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                    }
                    tvChooseWareChangeCallBack.callback(contextDataNode.get_value(), "Rebuild");
                    //window.location.reload();                
                    break;     
                    
                case 'ȡ��':
                    var theBtn = window.frames["ifTrainCourseCourseWareDetail"].document.getElementById("fvTrainCourseCourseWare_InsertCancelButton");                    
                    if(theForm && theBtn)
                    {
                        theItem.value = "";
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                    }  
                    break;
                
                case 'ɾ��':
                    var theBtn = window.frames["ifTrainCourseCourseWareDetail"].document.getElementById("fvTrainCourseCourseWare_DeleteButton");
                    if(theForm&&theBtn)
                    {
                        if(!confirm("��ȷ��Ҫɾ����" + contextDataNode.get_text() + "����"))
                        {
                            return false; 
                        }
                        if(contextDataNode.get_nodes().get_length() > 0)
                        {
                            alert("�ڵ����ӽڵ㣬����ɾ����");
                            return false;
                        }
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                        
                        // ɾ���ýڵ㲢ѡ���һ���ڵ�


                        tvChooseWare.get_selectedNode().remove();
                        if(tvChooseWare.get_nodes().get_length() > 0)
                        {
                            tvChooseWare.get_nodes().getNode(0).select();
                        }
                    }                
                    break;     
            } 
            
            return true; 
        }
        function tvChooseWare_onLoad(sender, eventArgs)
        {
            tvChooseWare.expandAll();
        }
        function tvChooseWare_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifTrainCourseCourseWareDetail"];
            var theItem = document.getElementById("hfSelectedMenuItem");
            if(theItem && (theItem.value == "�������" || theItem.value == "�༭"))
            {
                if(confirm("Ҫ����������"))
                {
                    tvChooseWareMenu_onItemSelect(sender, tvChooseWareMenu.findItemByProperty("Text", "����"));
                    return;                    
                }
                theItem.value = "";               
            }
            //alert(node.get_id());
            theFrame.location = "TrainCourseCoursewareDetail.aspx?id=" + node.get_value();
        }
        
        function tvChooseWareChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            tvChooseWare.expandAll();
            tvChooseWare.selectNodeById(sender.get_parameter());
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
                        <ComponentArt:TreeView ID="tvWare" Width="300" Height="530" EnableViewState="true" DragAndDropEnabled="false"
                            NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                            KeyboardCutCopyPasteEnabled="false" runat="server"
                            AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
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
                                <ComponentArt:CallBack ID="tvChooseWareChangeCallBack" runat="server" OnCallback="tvChooseWareChangeCallBack_Callback">
                                     <Content>
                                        <ComponentArt:TreeView ID="tvChooseWare" Width="300" Height="320" EnableViewState="true" DragAndDropEnabled="false"
                                                NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                                KeyboardCutCopyPasteEnabled="false" runat="server" DefaultTarget="ifTrainCourseCourseWare"
                                                AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                                <ClientEvents>
                                                        <ContextMenu EventHandler="tvChooseWare_onContextMenu" />
                                                        <NodeSelect EventHandler="tvChooseWare_onNodeSelect" />
                                                 </ClientEvents>                               
                                        </ComponentArt:TreeView>
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="tvChooseWareChangeCallBack_onCallbackComplete" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                                <ComponentArt:Menu ID="tvChooseWareMenu" runat="server" SiteMapXmlFile="TrainCourseBookTree.xml">
                                    <ClientEvents>
                                        <ItemSelect EventHandler="tvChooseWareMenu_onItemSelect" />
                                    </ClientEvents>
                                </ComponentArt:Menu>
                            </div>
                            </td>
                        </tr>
                        <tr>
                             <td style="text-align: left; vertical-align: top;">
                                <iframe id="ifTrainCourseCourseWareDetail" frameborder="0" width="300" height="200" scrolling="no">
                                </iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnUp" runat="server" Text="��һ��" OnClick="btnUp_Click" />
                     &nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Button ID="btnCancel" runat="server" Text="��&nbsp; &nbsp; ��" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
    </div>
    </form>
    <script type="text/javascript">
        var theFrame = window.frames["ifTrainCourseCourseWareDetail"];        
        theFrame.location = "TrainCourseCoursewareDetail.aspx?id=" + tvChooseWare.get_nodes().getNode(0).get_value();
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
   </script>
</body>
</html>

