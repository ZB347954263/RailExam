<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainAimTaskDetail.aspx.cs" Inherits="RailExamWebApp.Train.TrainAimTaskDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ޱ���ҳ</title>

    <script type="text/javascript">              
      
       function ManagePaper(id)
       {
           var re= window.open("../Paper/PaperPreview.aspx?id="+id,"PaperPreview"," Width=800px; Height=600px,status=false,resizable=yes,scrollbars=yes",true);		
	       re.focus(); 
       }
       
       function ShowContextMenu1(sender, eventArgs)
       {
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
            
            switch(menuItem.get_text()){
                case '�޸�':
              
                  var re= window.open('../Paper/PaperManageEdit.aspx?id='+contextDataNode.getMember('PaperId').get_value(),'PaperManageEdit',' Width=800px; Height=600px,status=false,resizable=no',true);		
				    re.focus(); 
                    break;          
             
            
                case '�½�':
                
             
                  
                 var re= window.open("../Paper/PaperManageEdit.aspx?categoryId="+contextDataNode.getMember('CategoryId').get_value(),"PaperManageEdit"," Width=800px; Height=600px,status=false,resizable=no",true);		
				   re.focus(); 
                    break;
                
                case 'ɾ��': 

                        if(!confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('PaperName').get_text() + "����")){
                            return false; 
                        }                                             
                      form1.DeleteID.value=contextDataNode.getMember('PaperId').get_value();                      
                      form1.submit();
                   
                    break;     
            } 
            return true; 
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ComponentArt:Menu ID="ContextMenu1" ContextMenu="Custom" runat="server" Orientation="Vertical">
            <Items>
                <ComponentArt:MenuItem Text="�޸�" ClientSideCommand=" " look-lefticonurl="edit.gif"
                    disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem Text="�½�" ClientSideCommand=" " look-lefticonurl="new.gif"
                    disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="ɾ��" ClientSideCommand="" look-lefticonurl="delete.gif"
                    disabledlook-lefticonurl="delete_disabled.gif" />
                <ComponentArt:MenuItem Text="�˳�" ClientSideCommand="function donthng(){return false;} "
                    look-lefticonurl="cancel.gif" disabledlook-lefticonurl="cancel_disabled.gif" />
            </Items>
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
        </ComponentArt:Menu>
        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <div>
                &nbsp;&nbsp;���ʽ
                <asp:DropDownList runat="server" ID="ddlType">
                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                    <asp:ListItem Value="1" Text="�ֹ�����"></asp:ListItem>
                    <asp:ListItem Value="2" Text="�������"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;�Ծ�����
                <asp:TextBox runat="server" ID="txtName" Width="50px"></asp:TextBox>
                &nbsp;&nbsp;������

                <asp:TextBox runat="server" ID="txtPerson" Width="50px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:ImageButton runat="server" ID="btnQuery" OnClick="btnQuery_Click" ImageUrl="~/Common/Image/confirm.gif" />
            </div>
        </asp:Panel>
        <div style="height:260px;">
            <ComponentArt:Grid ID="Grid1" runat="server" PageSize="7"
                Width="600px" >
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="PaperId">
                        <Columns>
                            <ComponentArt:GridColumn AllowEditing="True" AllowSorting="False" DataField="Flag" HeadingText="ѡ��" DataType="System.Boolean" ColumnType="CheckBox" />
                            <ComponentArt:GridColumn DataField="PaperId" HeadingText="���"  Visible="false"/>
                            <ComponentArt:GridColumn DataField="PaperName" HeadingText="��ҵ����" />
                            <ComponentArt:GridColumn DataField="CategoryId" HeadingText="categoryId" Visible="false" />
                            <ComponentArt:GridColumn DataField="CreateMode" HeadingText="���ⷽʽ" Visible="false" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="���ⷽʽ" />
                            <ComponentArt:GridColumn DataField="ItemCount" HeadingText="��������" />
                            <ComponentArt:GridColumn DataField="TotalScore" HeadingText="�ܷ�" />
                            <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="������" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="����" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="CTedit">
                        <a onclick="ManagePaper(##DataItem.getMember('PaperId').get_value()## )" title="Ԥ����ҵ"
                            href="#"><b>Ԥ����ϰ</b></a>&nbsp;
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="ClientTemplate1">
                        ## DataItem.getMember("CreateMode").get_value() == 1 ? "�ֹ�����":"�������" ##
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
                <ClientEvents>
                    <ContextMenu EventHandler="ShowContextMenu1" />
                </ClientEvents>
            </ComponentArt:Grid>
        </div>
        <div style="text-align:center; height:25px">
            <asp:Button ID="btnAdd" runat="server" Text="����" OnClick="btnAdd_Click"/>  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDel" runat="server" Text="����" OnClick="btnDel_Click"/> 
        </div>
        <div style="height:260px;">
            <ComponentArt:Grid ID="Grid2" runat="server" PageSize="7"
                Width="600px">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="ObjPaper.PaperId">
                        <Columns>
                            <ComponentArt:GridColumn AllowEditing="True" AllowSorting="False" DataField="Flag" HeadingText="ѡ��" DataType="System.Boolean" ColumnType="CheckBox" />
                            <ComponentArt:GridColumn DataField="ObjPaper.PaperId" HeadingText="���"  Visible="false"/>
                            <ComponentArt:GridColumn DataField="ObjPaper.PaperName" HeadingText="��ҵ����" />
                            <ComponentArt:GridColumn DataField="ObjPaper.CategoryId" HeadingText="categoryId" Visible="false" />
                            <ComponentArt:GridColumn DataField="ObjPaper.CreateMode" HeadingText="���ⷽʽ" Visible="false" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="���ⷽʽ" />
                            <ComponentArt:GridColumn DataField="ObjPaper.ItemCount" HeadingText="��������" />
                            <ComponentArt:GridColumn DataField="ObjPaper.TotalScore" HeadingText="�ܷ�" />
                            <ComponentArt:GridColumn DataField="ObjPaper.CreatePerson" HeadingText="������" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate2" HeadingText="����" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="ClientTemplate2">
                        <a onclick="ManagePaper(##DataItem.getMember('ObjPaper.PaperId').get_value()## )" title="Ԥ���Ծ�"
                            href="#"><b>Ԥ����ҵ</b></a>&nbsp;
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="ClientTemplate3">
                        ## DataItem.getMember("ObjPaper.CreateMode").get_value() == 1 ? "�ֹ�����":"�������" ##
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
            </ComponentArt:Grid>
        </div>
        <div style="text-align:center;height:25px">
            <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="~/Common/Image/confirm.gif" OnClick="btnConfirm_Click" />
        </div>
        <input id="DeleteID" type="hidden" name="DeleteID" />
        <input id="Test1" type="hidden" name="Test1" />
        <asp:HiddenField ID="hfId" runat="server" />
    </form>
</body>
</html>