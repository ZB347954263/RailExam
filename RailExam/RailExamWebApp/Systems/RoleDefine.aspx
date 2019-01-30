<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="RoleDefine.aspx.cs" Inherits="RailExamWebApp.Systems.RoleDefine" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色管理</title>
    <script type="text/javascript">
        function FunctionDefine(id)
        {
		    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-400)*.5;   
            ctop=(screen.availHeight-600)*.5;              
            var flagUpdate=document.getElementById("HfUpdateRight").value;           
	        if(flagUpdate=="False")
             {
                alert("您没有权限使用该操作！");                       
                return;
             }   
            
            var re = window.open("FunctionDefine.aspx?id="+id,'FunctionDefine','Width=400px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	        re.focus();
        }
        function FunctionClass(id)
        {
		    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-600)*.5;   
            ctop=(screen.availHeight-600)*.5;              
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False") 
             {
                alert("您没有权限使用该操作！");                       
                return;
             }   
            
            var re = window.open("FunctionClass.aspx?id="+id,'FunctionDefine','Width=600px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	        re.focus();
        }
        function AddRecord()
        {
		    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-580)*.5;   
            ctop=(screen.availHeight-250)*.5;        
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
                          
	        if(flagUpdate=="False")
             {
                alert("您没有权限使用该操作！");                       
                return;
             }            
            
            var ret = window.open('RoleDefineAdd.aspx?mode=Insert','RoleDefineDetail',' Width=580px; Height=350px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		    if(ret == "true")
		    {
			    Form1.Refresh.value = ret;
			    Form1.submit();
			    Form1.Refresh.value = "";
		    }
        }
        
        function Grid1_onContextMenu(sender, eventArgs)
        {
            var menuItemNew = ContextMenu.findItemByProperty("Text", "新增");
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "编辑");
            var menuItemDelete = ContextMenu.findItemByProperty("Text", "删除");
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
            switch(eventArgs.get_item().getMember('RoleID').get_text())
            {
                default:
                    ContextMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_item());
                    return false;
                break;
            }
        }
        
        function ContextMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-580)*.5;   
            ctop=(screen.availHeight-250)*.5;        

            switch(menuItem.get_text())
            {
                case '查看':
                    var re = window.open('RoleDefineDetail.aspx?mode=ReadOnly&&id='+contextDataNode.getMember('RoleID').get_value(),'RoleDefineDetail',' Width=580px; Height=350px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
				    re.focus();
                    break;
                    
                case '编辑':
                    var ret = window.open('RoleDefineAdd.aspx?mode=Edit&&id='+contextDataNode.getMember('RoleID').get_value(),'RoleDefineDetail',' Width=580px; Height=350px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
				    if(ret == "true")
				    {
					    Form1.Refresh.value = ret;
					    Form1.submit();
					    Form1.Refresh.value = "";
				    }
                    break;
            
                case '新增':
                    AddRecord();
                    break;
                
                case '删除':
                    if(!confirm("您确定要删除“" + contextDataNode.getMember('RoleName').get_value() + "”吗？"))
                    {
                        return false;
                    }
                    
                    form1.DeleteID.value = contextDataNode.getMember('RoleID').get_value();
					form1.submit();
					form1.DeleteID.value = "";

                    break;
            }
            
            return true;
        }       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        角色权限</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="AddButton" alt="" onclick="AddRecord();" src="../Common/Image/add.gif" />
                </div>
            </div>
            <div id="content">
                <div id="mainContent">
                    <ComponentArt:Grid ID="Grid1" runat="server" AutoGenerateColumns="False" DataSourceID="odsRole">
                        <ClientEvents>
                            <ContextMenu EventHandler="Grid1_onContextMenu" />
                        </ClientEvents>
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="RoleID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="RoleID" Visible="False" />
                                    <ComponentArt:GridColumn DataField="RoleName" HeadingText="角色名称" />
                                    <ComponentArt:GridColumn DataField="Description" HeadingText="角色描述" />
                                    <ComponentArt:GridColumn DataField="IsAdmin" DataCellClientTemplateId="AdminIconTemplate" HeadingText="管理标志" />
                                    <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate Id="AdminIconTemplate">
                            ##DataItem.GetMember("IsAdmin").get_value() == "1" ? "<img alt='#' src='../Common/Image/charge.gif'/>" : ""##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="FunctionDefine(##DataItem.getMember('RoleID').get_value()##)" href="#">
                                    <b>分配角色权限</b></a>
                                <a onclick="FunctionClass(##DataItem.getMember('RoleID').get_value()##)" href="#" style="display: none;">
                                    <b>分配部门权限</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsRole" runat="server" SelectMethod="GetRolesAll" TypeName="RailExam.BLL.SystemRoleBLL">
        </asp:ObjectDataSource>
        <ComponentArt:Menu ID="ContextMenu" runat="server" EnableViewState="true" ContextMenu="Custom">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="查看">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="新增">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="编辑">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem LookId="BreakItem">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                    Text="删除">
                </ComponentArt:MenuItem>
            </Items>
        </ComponentArt:Menu>
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
