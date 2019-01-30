<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Regulation.aspx.cs" Inherits="RailExamWebApp.Regulation.Regulation" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>政策法规</title>
    <script type="text/javascript">
        function tvRegulationCategory_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            
            if(node){
                window.frames["ifRegulationInfo"].location = "RegulationInfo.aspx?id=" + node.get_value();
            }
        }
        	    
        function tvRegulationCategory_onContextMenu(sender, eventArgs)
        {
            var menuItemNew = treeContextMenu.findItemByProperty("Text", "新增");
            var menuItemEdit = treeContextMenu.findItemByProperty("Text", "编辑");
            var menuItemDelete = treeContextMenu.findItemByProperty("Text", "删除");
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
                          
	        if(flagUpdate=="False")
             {
                menuItemNew.set_enabled(false);
                menuItemEdit.set_enabled(false); 
             }               
                      
            var flagDelete=document.getElementById("HfDeleteRight").value;
            if(flagDelete=="False")
            {                       
               menuItemDelete.set_enabled(false); 
            }                           
                      
            switch(eventArgs.get_node().get_text())
            {
                default:
                    treeContextMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node());
                    eventArgs.get_node().select();
                    return false;
                break;
            }
        }
        
        function treeContextMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
             
             var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-500)*.5;   
            ctop=(screen.availHeight-220)*.5;    
          
            switch(menuItem.get_text())
            {
                case '查看':
                    var re = window.open('RegulationCategoryDetail.aspx?mode=ReadOnly&id='+contextDataNode.get_id(),'RegulationCategoryDetail','Width=500px; Height=220px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    re.focus();
                    break;
                    
                case '编辑':
                    var re = window.open('RegulationCategoryDetail.aspx?mode=Edit&id='+contextDataNode.get_id(),'RegulationCategoryDetail','Width=500px; Height=220px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    re.focus();
                    break;
            
                case '新增':
                    var re = window.open("RegulationCategoryDetail.aspx?mode=Insert&id="+contextDataNode.get_id(),"RegulationCategoryDetail",'Width=500px; Height=220px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    re.focus();
                    break;
                
                case '删除':
                    if(tvRegulationCategory.get_selectedNode().get_nodes().get_length() > 0)
                    {
                        alert("节点有子节点，不能删除！");
                        return false;
                    }

                    if(! confirm("您确定要删除“" + contextDataNode.get_text() + "”吗？"))
                    {
                        return false; 
                    }
                    form1.DeleteID.value = contextDataNode.get_id();
                    form1.submit();
                    form1.DeleteID.value = "";
                    break;
            } 
            
            return true;
        }
        
        function imgBtns_onClick(btn)
        {
            var flagupdate=document.getElementById("HfUpdateRight").value;
            if(flagupdate=="False"&&(btn.action == "edit"||btn.action == "new"))
            {
                alert("您没有权限使用该操作！");                       
                return;
            }
            if(btn.action == "new")
                window.frames["ifRegulationInfo"].document.getElementById(btn.id).onclick();
            else
            {
                if(window.frames["ifRegulationInfo"].document.getElementById("query").style.display == "none")
                    window.frames["ifRegulationInfo"].document.getElementById("query").style.display = "";
                else
                    window.frames["ifRegulationInfo"].document.getElementById("query").style.display = "none";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        知识管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        政策法规</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/add.gif"
                        action="new" alt="" />
                    <img id="FindButton" onclick="imgBtns_onClick(this);" src="../Common/Image/find.gif"
                        action="find" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        法规类别</div>
                    <div id="leftContent">
                        <ComponentArt:TreeView ID="tvRegulationCategory" runat="server" EnableViewState="false">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvRegulationCategory_onNodeSelect" />
                                <ContextMenu EventHandler="tvRegulationCategory_onContextMenu" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifRegulationInfo" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <ComponentArt:Menu ID="treeContextMenu" ContextMenu="Custom" runat="server" EnableViewState="true">
            <ClientEvents>
                <ItemSelect EventHandler="treeContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem Text="查看" look-lefticonurl="view.gif" DisabledLook-LeftIconUrl="view_disabled.gif" />
                <ComponentArt:MenuItem Text="编辑" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem Text="新增" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="删除" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
                <ComponentArt:MenuItem Text="退出" look-lefticonurl="cancel.gif" disabledlook-lefticonurl="cancel_disabled.gif" 
                    ClientSideCommand="function donthng(){return false;} " />
            </Items>
        </ComponentArt:Menu>
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
    <script type="text/javascript">
        if (tvRegulationCategory && tvRegulationCategory.get_nodes().get_length() > 0)
        {
            tvRegulationCategory.get_nodes().getNode(0).select();
        }
    </script>
</body>
</html>
