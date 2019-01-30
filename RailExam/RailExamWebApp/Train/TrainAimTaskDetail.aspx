<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainAimTaskDetail.aspx.cs" Inherits="RailExamWebApp.Train.TrainAimTaskDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

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
                case '修改':
              
                  var re= window.open('../Paper/PaperManageEdit.aspx?id='+contextDataNode.getMember('PaperId').get_value(),'PaperManageEdit',' Width=800px; Height=600px,status=false,resizable=no',true);		
				    re.focus(); 
                    break;          
             
            
                case '新建':
                
             
                  
                 var re= window.open("../Paper/PaperManageEdit.aspx?categoryId="+contextDataNode.getMember('CategoryId').get_value(),"PaperManageEdit"," Width=800px; Height=600px,status=false,resizable=no",true);		
				   re.focus(); 
                    break;
                
                case '删除': 

                        if(!confirm("您确定要删除“" + contextDataNode.getMember('PaperName').get_text() + "”吗？")){
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
                <ComponentArt:MenuItem Text="修改" ClientSideCommand=" " look-lefticonurl="edit.gif"
                    disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem Text="新建" ClientSideCommand=" " look-lefticonurl="new.gif"
                    disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="删除" ClientSideCommand="" look-lefticonurl="delete.gif"
                    disabledlook-lefticonurl="delete_disabled.gif" />
                <ComponentArt:MenuItem Text="退出" ClientSideCommand="function donthng(){return false;} "
                    look-lefticonurl="cancel.gif" disabledlook-lefticonurl="cancel_disabled.gif" />
            </Items>
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
        </ComponentArt:Menu>
        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <div>
                &nbsp;&nbsp;组卷方式
                <asp:DropDownList runat="server" ID="ddlType">
                    <asp:ListItem Value="0" Text=""></asp:ListItem>
                    <asp:ListItem Value="1" Text="手工出题"></asp:ListItem>
                    <asp:ListItem Value="2" Text="随机出题"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;试卷名称
                <asp:TextBox runat="server" ID="txtName" Width="50px"></asp:TextBox>
                &nbsp;&nbsp;出卷人

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
                            <ComponentArt:GridColumn AllowEditing="True" AllowSorting="False" DataField="Flag" HeadingText="选择" DataType="System.Boolean" ColumnType="CheckBox" />
                            <ComponentArt:GridColumn DataField="PaperId" HeadingText="编号"  Visible="false"/>
                            <ComponentArt:GridColumn DataField="PaperName" HeadingText="作业名称" />
                            <ComponentArt:GridColumn DataField="CategoryId" HeadingText="categoryId" Visible="false" />
                            <ComponentArt:GridColumn DataField="CreateMode" HeadingText="出题方式" Visible="false" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="出题方式" />
                            <ComponentArt:GridColumn DataField="ItemCount" HeadingText="试题总数" />
                            <ComponentArt:GridColumn DataField="TotalScore" HeadingText="总分" />
                            <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="出卷人" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="操作" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="CTedit">
                        <a onclick="ManagePaper(##DataItem.getMember('PaperId').get_value()## )" title="预览作业"
                            href="#"><b>预览练习</b></a>&nbsp;
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="ClientTemplate1">
                        ## DataItem.getMember("CreateMode").get_value() == 1 ? "手工出题":"随机出题" ##
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
                <ClientEvents>
                    <ContextMenu EventHandler="ShowContextMenu1" />
                </ClientEvents>
            </ComponentArt:Grid>
        </div>
        <div style="text-align:center; height:25px">
            <asp:Button ID="btnAdd" runat="server" Text="下移" OnClick="btnAdd_Click"/>  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDel" runat="server" Text="上移" OnClick="btnDel_Click"/> 
        </div>
        <div style="height:260px;">
            <ComponentArt:Grid ID="Grid2" runat="server" PageSize="7"
                Width="600px">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="ObjPaper.PaperId">
                        <Columns>
                            <ComponentArt:GridColumn AllowEditing="True" AllowSorting="False" DataField="Flag" HeadingText="选择" DataType="System.Boolean" ColumnType="CheckBox" />
                            <ComponentArt:GridColumn DataField="ObjPaper.PaperId" HeadingText="编号"  Visible="false"/>
                            <ComponentArt:GridColumn DataField="ObjPaper.PaperName" HeadingText="作业名称" />
                            <ComponentArt:GridColumn DataField="ObjPaper.CategoryId" HeadingText="categoryId" Visible="false" />
                            <ComponentArt:GridColumn DataField="ObjPaper.CreateMode" HeadingText="出题方式" Visible="false" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="出题方式" />
                            <ComponentArt:GridColumn DataField="ObjPaper.ItemCount" HeadingText="试题总数" />
                            <ComponentArt:GridColumn DataField="ObjPaper.TotalScore" HeadingText="总分" />
                            <ComponentArt:GridColumn DataField="ObjPaper.CreatePerson" HeadingText="出卷人" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate2" HeadingText="操作" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="ClientTemplate2">
                        <a onclick="ManagePaper(##DataItem.getMember('ObjPaper.PaperId').get_value()## )" title="预览试卷"
                            href="#"><b>预览作业</b></a>&nbsp;
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="ClientTemplate3">
                        ## DataItem.getMember("ObjPaper.CreateMode").get_value() == 1 ? "手工出题":"随机出题" ##
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