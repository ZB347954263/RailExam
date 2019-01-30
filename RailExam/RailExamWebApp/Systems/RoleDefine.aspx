<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="RoleDefine.aspx.cs" Inherits="RailExamWebApp.Systems.RoleDefine" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��ɫ����</title>
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
                alert("��û��Ȩ��ʹ�øò�����");                       
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
                alert("��û��Ȩ��ʹ�øò�����");                       
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
                alert("��û��Ȩ��ʹ�øò�����");                       
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
            var menuItemNew = ContextMenu.findItemByProperty("Text", "����");
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "�༭");
            var menuItemDelete = ContextMenu.findItemByProperty("Text", "ɾ��");
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
                case '�鿴':
                    var re = window.open('RoleDefineDetail.aspx?mode=ReadOnly&&id='+contextDataNode.getMember('RoleID').get_value(),'RoleDefineDetail',' Width=580px; Height=350px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
				    re.focus();
                    break;
                    
                case '�༭':
                    var ret = window.open('RoleDefineAdd.aspx?mode=Edit&&id='+contextDataNode.getMember('RoleID').get_value(),'RoleDefineDetail',' Width=580px; Height=350px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
				    if(ret == "true")
				    {
					    Form1.Refresh.value = ret;
					    Form1.submit();
					    Form1.Refresh.value = "";
				    }
                    break;
            
                case '����':
                    AddRecord();
                    break;
                
                case 'ɾ��':
                    if(!confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('RoleName').get_value() + "����"))
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
                        ϵͳ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ��ɫȨ��</div>
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
                                    <ComponentArt:GridColumn DataField="RoleName" HeadingText="��ɫ����" />
                                    <ComponentArt:GridColumn DataField="Description" HeadingText="��ɫ����" />
                                    <ComponentArt:GridColumn DataField="IsAdmin" DataCellClientTemplateId="AdminIconTemplate" HeadingText="�����־" />
                                    <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="����" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate Id="AdminIconTemplate">
                            ##DataItem.GetMember("IsAdmin").get_value() == "1" ? "<img alt='#' src='../Common/Image/charge.gif'/>" : ""##
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="FunctionDefine(##DataItem.getMember('RoleID').get_value()##)" href="#">
                                    <b>�����ɫȨ��</b></a>
                                <a onclick="FunctionClass(##DataItem.getMember('RoleID').get_value()##)" href="#" style="display: none;">
                                    <b>���䲿��Ȩ��</b></a>
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
                    Text="�鿴">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="����">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="�༭">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem LookId="BreakItem">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                    Text="ɾ��">
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
