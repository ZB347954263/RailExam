<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Bulletin.aspx.cs" Inherits="RailExamWebApp.Notice.Bulletin" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��Ϣ����</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function AddRecord()    
        {        
        
         var flagUpdate=document.getElementById("HfUpdateRight").value;  
          if(flagUpdate=="False")
             {  
             alert("��û��Ȩ��ʹ�øò���!");
             return;
                         
              }  
              
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-580)*.5;   
          ctop=(screen.availHeight-420)*.5;    
                
            var ret = window.open('BulletinDetail.aspx?mode=Insert','BulletinDetail','Width=580px; Height=420px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		    if(ret == "true")
		    {
			    Form1.Refresh.value = ret;
			    Form1.submit();
			    Form1.Refresh.value = "";
		    }
        }
        
        function QueryRecord()
        {
            if(document.getElementById("query").style.display == "none")
            {
                document.getElementById("query").style.display = "";
            }
            else
            {
                document.getElementById("query").style.display = "none";
            }
        }
        
        function Grid1_onContextMenu(sender, eventArgs)
        {
           var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagDelete=document.getElementById("HfDeleteRight").value;  
             
              var item = ContextMenu.get_items();
                
        	        if(flagUpdate=="False")
                      {  
                         item.getItemByProperty("Text", "����").set_enabled(false);                    
                         item.getItemByProperty("Text", "�༭").set_enabled(false);
                      }  
                      else
                      {                
                        
                       item.getItemByProperty("Text", "����").set_enabled(true);
                        
                       item.getItemByProperty("Text", "�༭").set_enabled(true);
                      }         
                      
                                     
                       if(flagDelete=="False")
                      {                     
                     item.getItemByProperty("Text", "ɾ��").set_enabled(false);
                      }  
                      else
                      {
                      item.getItemByProperty("Text", "ɾ��").set_enabled(true);
                      }                        
                      
                      
            switch(eventArgs.get_item().getMember('BulletinID').get_text())
            {
                default:
                    ContextMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_item());
                    return false;
                break;
            }
        }
        
        function ContextMenu_onItemSelect(sender, eventArgs)
        {
         var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-580)*.5;   
          ctop=(screen.availHeight-420)*.5;                  
                 
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            
            switch(menuItem.get_text())
            {
                case '�鿴':
                    var re = window.open('BulletinDetail.aspx?mode=ReadOnly&id='+contextDataNode.getMember('BulletinID').get_value(),'BulletinDetail',' Width=580px; Height=420px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
				    re.focus();
                    break;
                    
                case '�༭':
                    var ret = window.open('BulletinDetail.aspx?mode=Edit&id='+contextDataNode.getMember('BulletinID').get_value(),'BulletinDetail',' Width=580px; Height=420px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
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
                    if(!confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('Title').get_value() + "����"))
                    {
                        return false;
                    }
                    
                    form1.DeleteID.value = contextDataNode.getMember('BulletinID').get_value();
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
                        ��Ϣ����</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="btnAdd" onclick="AddRecord();" src="../Common/Image/add.gif" alt="" />
                    <img id="btnFind" onclick="QueryRecord();" src="../Common/Image/find.gif" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="query" style="display: none;">
                    &nbsp;&nbsp;����<asp:TextBox ID="txtTitle" runat="server" Width="10%"></asp:TextBox>
                    �����̶�
                    <asp:DropDownList ID="ddlImportanceName" runat="server" Width="8%">
                    </asp:DropDownList>
                    ��������<asp:TextBox ID="txtOrgName" runat="server" Width="8%"></asp:TextBox>
                    ������<asp:TextBox ID="txtEmployeeName" runat="server" Width="8%"></asp:TextBox>
                    ����ʱ�� ��<uc1:Date ID="dateBeginTime" runat="server" />
                    ��<uc1:Date ID="dateEndTime" runat="server" />
                    <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="ȷ  ��" OnClick="btnQuery_Click" />
                </div>
                <div id="mainContent">
                    <ComponentArt:Grid ID="Grid1" runat="server" PageSize="19">
                        <ClientEvents>
                            <ContextMenu EventHandler="Grid1_onContextMenu" />
                        </ClientEvents>
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="BulletinID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="BulletinID" Visible="False" Align="Center" />
                                    <ComponentArt:GridColumn DataField="Title" HeadingText="����" Align="Center" />
                                    <ComponentArt:GridColumn DataField="ImportanceName" HeadingText="�����̶�" Align="Center" />
                                    <ComponentArt:GridColumn DataField="DayCount" HeadingText="��Ч����" Align="Center" />
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="������" Align="Center" />
                                    <ComponentArt:GridColumn DataField="OrgName" HeadingText="��������" Align="Center" />
                                    <ComponentArt:GridColumn DataField="CreateTime" FormatString="yyyy-MM-dd" HeadingText="����ʱ��"
                                        Align="Center" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsBulletin" runat="server" SelectMethod="GetBulletins"
            TypeName="RailExam.BLL.BulletinBLL">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="nNum" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <ComponentArt:Menu ID="ContextMenu" ContextMenu="Custom" runat="server" EnableViewState="false">
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
