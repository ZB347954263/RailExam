<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperManageInfo.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperManageInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�Ծ���Ϣ</title>

    <script type="text/javascript">
        function ManagePaper(id)
        {                   
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;              
          
           var re = window.open("PaperPreview.aspx?id="+id,"PaperPreview","Width=800px; Height=600px,status=false,resizable=yes,left="+cleft+",top="+ctop+",scrollbars=yes",true);
           re.focus();
        }
        
        function OutPutPaper(id)
        {
            form1.OutPut.value = id;
	        form1.submit();
	        form1.OutPut.value = "";        
        }

        function AddRecord()
		{
		   var   cleft;   
           var   ctop;   
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5; 
		    var paperCategoryID=document.getElementById("HfPaperCategoryId").value; 
			var ret = window.open("PaperManageEdit.aspx?mode=Insert&&PaperCategoryIDPath="+paperCategoryID,'PaperManageEdit','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
		    }
		}
		 
        function Grid_onContextMenu(sender, eventArgs)
        {
        
          var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagDelete=document.getElementById("HfDeleteRight").value;   
              var NowOrgID = document.getElementById("hfOrgID").value; 
                  var item = ContextMenu1.get_items();
             var orgid = eventArgs.get_item().getMember('OrgId').get_value(); 
        	        if(flagUpdate=="True" && orgid==NowOrgID )
                      {  
                       item.getItemByProperty("Text", "����").set_enabled(true);                        
                       item.getItemByProperty("Text", "�༭").set_enabled(true);
                      }  
                      else
                      {  
                         item.getItemByProperty("Text", "����").set_enabled(false);                    
                         item.getItemByProperty("Text", "�༭").set_enabled(false);
                      }              
                                
                       if(flagDelete=="True" && orgid ==NowOrgID)
                      {                     
                          item.getItemByProperty("Text", "ɾ��").set_enabled(true);
                      }  
                      else
                      {
                            item.getItemByProperty("Text", "ɾ��").set_enabled(false);
                    }                       
                      
                      
            switch(eventArgs.get_item().getMember('PaperName').get_text())
            {
                default:
                    ContextMenu1.showContextMenu(eventArgs.get_event(), eventArgs.get_item()); 
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
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5; 
            
            switch(menuItem.get_text())
            {
                case '�鿴':
                    var ret = window.open('PaperManageEdit.aspx?mode=ReadOnly&id='+contextDataNode.getMember('PaperId').get_value(),'PaperManageEdit','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
                    break;
                    
                case '�༭':
                    if(contextDataNode.getMember('UsedCount').get_value() > 0)
                    {
                        alert("���Ծ��Ѿ����ڿ��ԣ������޸ģ�");
                        return;
                    }

                    var ret = window.open('PaperManageEdit.aspx?mode=Edit&id='+contextDataNode.getMember('PaperId').get_value(),'PaperManageEdit',' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
                    if(ret == "true")
				    {
					    form1.Refresh.value = ret;
					    form1.submit();
					    form1.Refresh.value = "";
				    }
                    break;
            
                case '����':
                    AddRecord();
                    break;
                
                case 'ɾ��':
                    if(contextDataNode.getMember('UsedCount').get_value() > 0)
                    {
                        alert("���Ծ��Ѿ����ڿ��ԣ�����ɾ����");
                        return;
                    }

                    if(!confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('PaperName').get_text() + "����"))
                    {
                        return false; 
                    }
                    form1.DeleteID.value = contextDataNode.getMember('PaperId').get_value();
                    form1.submit();
                    form1.DeleteID.value = "";

                    break;
            } 
            
            return true; 
        }
        
         function UpdateData(id1,id2,orgid)
        {
             var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5; 
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
            var NowOrgID = document.getElementById("hfOrgID").value; 
            var mode="Edit";
             if(flagUpdate=="False"  || NowOrgID !=orgid)
             {
                   alert("��û��ɾ���õ�Ȩ�ޣ�");
                   return;
             }
             
                   if(id1 > 0)
                    {
                        alert("���Ծ��Ѿ����ڿ��ԣ������޸ģ�");
                        return;
                    }

                    var ret = window.open('PaperManageEdit.aspx?mode=Edit&id='+id2,'PaperManageEdit',' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
                    if(ret == "true")
				    {
					    form1.Refresh.value = ret;
					    form1.submit();
					    form1.Refresh.value = "";
				    }
        
        }        
        
        function DeleteData(id1,id2,id3,orgid)
        {
                var NowOrgID = document.getElementById("hfOrgID").value; 
                 var flagDelete=document.getElementById("HfDeleteRight").value; 
                 if(flagDelete=="False" || NowOrgID !=orgid)
                 {
                   alert("��û��ɾ���õ�Ȩ�ޣ�");
                   return;
                 }
         
                   if(id1 > 0)
                    {
                        alert("���Ծ��Ѿ����ڿ��ԣ�����ɾ����");
                        return;
                    }

                    if(!confirm("��ȷ��Ҫɾ����" + id3 + "����"))
                    {
                        return false; 
                    }
                    form1.DeleteID.value = id2;
                    form1.submit();
                    form1.DeleteID.value = "";

        
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="query" style="display: none;">
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlType" runat="server" Width="15%">
                    <asp:ListItem Value="0" Text="-���ⷽʽ-"></asp:ListItem>
                    <asp:ListItem Value="1" Text="�ֹ�����"></asp:ListItem>
                    <asp:ListItem Value="2" Text="�������"></asp:ListItem>
                </asp:DropDownList>
                �Ծ�����
                <asp:TextBox runat="server" ID="txtPaperName" Width="10%"></asp:TextBox>
                ������
                <asp:TextBox runat="server" ID="txtCreatePerson" Width="10%"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="ȷ  ��" OnClick="btnQuery_Click" />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsPaper" PageSize="20"
                    Width="96%">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="PaperId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="PaperId" Visible="false" />
                                <ComponentArt:GridColumn DataField="PaperName" Align="Left" HeadingText="�Ծ�����" />
                                <ComponentArt:GridColumn DataField="CategoryName" HeadingText="�Ծ����" />
                                <ComponentArt:GridColumn DataField="CategoryId" Visible="false" />
                                <ComponentArt:GridColumn DataField="CreateMode" HeadingText="���ⷽʽ" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="���ⷽʽ"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="ItemCount" HeadingText="�趨����" />
                                <ComponentArt:GridColumn DataField="UsedCount" HeadingText="ʹ�ô���" Visible="false" />
                                <ComponentArt:GridColumn DataField="TotalScore" HeadingText="�趨�ܷ�" />
                                <ComponentArt:GridColumn DataField="CurrentItemCount" HeadingText="ʵ������" />
                                <ComponentArt:GridColumn DataField="CurrentTotalScore" HeadingText="ʵ���ܷ�" />
                                <ComponentArt:GridColumn DataField="StationName" HeadingText="��λ" />
                                <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="������" />
                                <ComponentArt:GridColumn DataField="OrgId" Visible ="false"/>
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="����" Width="130" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <a onclick="UpdateData('##DataItem.getMember('UsedCount').get_value()##',##DataItem.getMember('PaperId').get_value()##,##DataItem.getMember('OrgId').get_value()##)"
                                href="#"><b>�༭</b></a> <a onclick="DeleteData('##DataItem.getMember('UsedCount').get_value()##',## DataItem.getMember('PaperId').get_value() ##,'## DataItem.getMember('PaperName').get_value() ##',##DataItem.getMember('OrgId').get_value()##)"
                                    href="#"><b>ɾ��</b></a> <a onclick="ManagePaper(##DataItem.getMember('PaperId').get_value()## )"
                                        title="Ԥ���Ծ�" href="#"><b>Ԥ��</b></a> <a onclick="OutPutPaper(## DataItem.getMember('PaperId').get_value() ##)"
                                            href="#" title="�����Ծ�"><b>����</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            ## DataItem.getMember("CreateMode").get_value() == 1 ? "�ֹ�����" : "�������" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsPaper" runat="server" SelectMethod="GetPaperByCategoryIDPath"
            TypeName="RailExam.BLL.PaperBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="idPath" QueryStringField="id" Type="String" />
                <asp:ControlParameter ControlID="ddlType" Type="Int32" PropertyName="SelectedValue"
                    Name="CreateMode" />
                <asp:ControlParameter ControlID="txtPaperName" Type="String" PropertyName="Text"
                    Name="PaperName" />
                <asp:ControlParameter ControlID="txtCreatePerson" Type="String" PropertyName="Text"
                    Name="CreatePerson" />
                <asp:SessionParameter SessionField="StationOrgID" Name="OrgId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <input type="hidden" id="NewButton" onclick="return AddRecord();" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="HfPaperCategoryId" runat="server" />
       <asp:HiddenField ID="hfOrgID" runat="server" /> 
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="strategyID" />
        <input type="hidden" name="OutPut" />
        <ComponentArt:Menu ID="ContextMenu1" ContextMenu="Custom" runat="server" EnableViewState="true">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="�鿴" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="����" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="�༭" />
                <ComponentArt:MenuItem LookId="BreakItem" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                    Text="ɾ��" />
            </Items>
        </ComponentArt:Menu>
    </form>
</body>
</html>
