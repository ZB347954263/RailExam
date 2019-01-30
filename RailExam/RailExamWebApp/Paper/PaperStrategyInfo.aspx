<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperStrategyInfo.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperStrategyInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��������Ϣ</title>

    <script type="text/javascript">
        function AddRecord()
		{
		  
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;             
		 
		 var paperCategoryID=document.getElementById("HfPaperCategoryId").value; 
		 
	        var ret = window.open("PaperStrategyEdit.aspx?mode=Insert&&PaperCategoryIDPath="+paperCategoryID,'PaperStrategyEdit','Width=800px, Height=600px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
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
             
                  var item = ContextMenu.get_items();
                
        	        if(flagUpdate=="False" )
                      {  
                         item.getItemByProperty("Text", "����").set_enabled(false);                    
                         item.getItemByProperty("Text", "�༭").set_enabled(false);
                      }  
                      else
                      {  
                       item.getItemByProperty("Text", "����").set_enabled(true);                        
                       item.getItemByProperty("Text", "�༭").set_enabled(true);
                      }              
                                
                       if(flagDelete=="False" )
                      {                     
                     item.getItemByProperty("Text", "ɾ��").set_enabled(false);
                      }  
                      else
                      {
                      item.getItemByProperty("Text", "ɾ��").set_enabled(true);
                      }                        
                      
                      
            switch(eventArgs.get_item().getMember('PaperStrategyName').get_text())
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
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5; 
          
            var menuItem = eventArgs.get_item(); 
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            
            switch(menuItem.get_text())
            {
                case '�鿴':
                    var ret = window.open('PaperStrategyEdit.aspx?mode=ReadOnly&id='+contextDataNode.getMember('PaperStrategyId').get_value(),'PaperStrategyEdit',' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
                    break;
                    
                case '�༭':
                    var ret = window.open('PaperStrategyEdit.aspx?mode=Edit&id='+contextDataNode.getMember('PaperStrategyId').get_value(),'PaperStrategyEdit','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
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
                    if(! confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('PaperStrategyName').get_text() + "����"))
                    {
                        return false;
                    }
                    form1.DeleteID.value = contextDataNode.getMember('PaperStrategyId').get_value();
                    form1.submit();
                    form1.DeleteID.value = "";
                   
                    break;
            } 
            
            return true;
        }
        
        
        function UpdateData(id1)
        {
             var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5; 
          
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
          
            var mode="Edit";
             if(flagUpdate=="False")
             {
             mode="ReadOnly";
             }
             
             
        var ret = window.open('PaperStrategyEdit.aspx?mode='+mode+'&id='+id1,'PaperStrategyEdit','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    if(ret == "true")
				    {
					    form1.Refresh.value = ret;
					    form1.submit();
					    form1.Refresh.value = "";
				    }
        
        }        
        
             function DeleteData(id1,id2)
        {
          var flagDelete=document.getElementById("HfDeleteRight").value; 
         if(flagDelete=="False")
         {
           alert("��û��ɾ������Ϣ��Ȩ�ޣ�");
           return;
         }
         
        if(! confirm("��ȷ��Ҫɾ����" + id1 + "����"))
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
            <div id="gird">
                <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsPaperStrategy" Width="97%">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="PaperStrategyId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="PaperStrategyId" HeadingText="ID" Visible="false" />
                                <ComponentArt:GridColumn DataField="CategoryName" HeadingText="�Ծ����" />
                                <ComponentArt:GridColumn DataField="PaperStrategyName" HeadingText="����������" />
                                <ComponentArt:GridColumn DataField="PaperCategoryId" HeadingText="PaperCategoryId"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="IsRandomOrder" HeadingText="����������ʾ˳��" Visible="false" />
                                <ComponentArt:GridColumn DataField="SingleAsMultiple" HeadingText="�ѵ�ѡ��ʾ�ɶ�ѡ" Visible="false" />
                                <ComponentArt:GridColumn DataField="StrategyMode" HeadingText="����ģʽ" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="����ģʽ" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="����������ʾ˳��"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate2" HeadingText="�ѵ�ѡ��ʾ�ɶ�ѡ"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="����" Width="60" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            ## DataItem.getMember("IsRandomOrder").get_value() == 1 ? "��":"��" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate2">
                            ## DataItem.getMember("SingleAsMultiple").get_value() == 1 ? "��":"��" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate3">
                            ## DataItem.getMember("StrategyMode").get_value() == 2 ? "���̲��½�":"�����Ը�������" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <a onclick="UpdateData(##DataItem.getMember('PaperStrategyId').get_value()## )" href="#">
                                <b>�༭</b></a> <a onclick="DeleteData('## DataItem.getMember('PaperStrategyName').get_value() ##','## DataItem.getMember('PaperStrategyId').get_value() ##')"
                                    href="#"><b>ɾ��</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <ComponentArt:Menu ID="ContextMenu" ContextMenu="Custom" runat="server" EnableViewState="true">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem Text="�鿴" look-lefticonurl="view.gif" disabledLook-LeftIconUrl="view_disabled.gif" />
                <ComponentArt:MenuItem Text="�༭" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem Text="����" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="ɾ��" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
            </Items>
        </ComponentArt:Menu>
        <asp:ObjectDataSource ID="odsPaperStrategy" runat="server" SelectMethod="GetPaperStrategyByPaperCategoryIDPath"
            TypeName="RailExam.BLL.PaperStrategyBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="PaperCategoryIDPath" QueryStringField="id" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <input type="hidden" id="NewButton" onclick="return AddRecord();" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfPaperCategoryId" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
