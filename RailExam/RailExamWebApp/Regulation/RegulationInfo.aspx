<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="RegulationInfo.aspx.cs" Inherits="RailExamWebApp.Regulation.RegulationInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>政策法规信息</title>
    <script type="text/javascript">
        function AddRecord()
        {
              var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-520)*.5;   
            ctop=(screen.availHeight-480)*.5;    
             var ret = window.open('RegulationDetail.aspx?mode=Insert','RegulationDetail','Width=520px; Height=480px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		    if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
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
            
            switch(eventArgs.get_item().getMember('RegulationID').get_text())
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
            cleft=(screen.availWidth-520)*.5;   
            ctop=(screen.availHeight-480)*.5;    
            
            switch(menuItem.get_text())
            {
                case '查看':
                    var re = window.open('RegulationDetail.aspx?mode=ReadOnly&id='+contextDataNode.getMember('RegulationID').get_value(),'RegulationDetail',' Width=520px; Height=480px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    re.focus();
                    break;
                    
                case '编辑':
                    var ret = window.open('RegulationDetail.aspx?mode=Edit&id='+contextDataNode.getMember('RegulationID').get_value(),'RegulationDetail',' Width=520px; Height=480px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    if(ret == "true")
				    {
					    form1.Refresh.value = ret;
					    form1.submit();
					    form1.Refresh.value = "";
				    }
                    break;
            
                case '新增':
                    AddRecord();
                    break;
                
                case '删除':
                    if(! confirm("您确定要删除“" + contextDataNode.getMember('RegulationName').get_value() + "”吗？"))
                    {
                        return false;
                    }
                    
                    form1.DeleteID.value = contextDataNode.getMember('RegulationID').get_value();
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
        <div id="gridPage">
            <div id="query" style="display: none;">
                &nbsp;&nbsp;法规名称
                <asp:TextBox ID="txtRegulationName" runat="server" Width="10%"></asp:TextBox>
                法规编号
                <asp:TextBox ID="txtRegulationNo" runat="server" Width="10%"></asp:TextBox>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="-1" Text="-状态-"></asp:ListItem>
                    <asp:ListItem Value="1" Text="有效"></asp:ListItem>
                    <asp:ListItem Value="0" Text="失效"></asp:ListItem>
                </asp:DropDownList>
                <asp:ImageButton ID="btnQuery" runat="server" CausesValidation="False" OnClick="btnQuery_Click"
                    ImageUrl="../Common/Image/confirm.gif" />
            </div>
            <div id="gird">
                <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsRegulation" PageSize="20" Width="96%">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid1_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="RegulationID">
                            <Columns> 
                                <ComponentArt:GridColumn DataField="RegulationID" Visible="false" /> 
                                <ComponentArt:GridColumn DataField="RegulationName" HeadingText="法规名称" />
                                <ComponentArt:GridColumn DataField="CategoryName" HeadingText="法规分类" />                                
                                <ComponentArt:GridColumn DataField="RegulationNo" HeadingText="法规编号" />
                                <ComponentArt:GridColumn DataField="CategoryID" HeadingText="法规分类ID" Visible="false" />
                                <ComponentArt:GridColumn DataField="Version" HeadingText="版本" Visible="false" />
                                <ComponentArt:GridColumn DataField="FileNo" HeadingText="文号" Visible="false" />
                                <ComponentArt:GridColumn DataField="TitleRemark" HeadingText="题注" Visible="false" />
                                <ComponentArt:GridColumn DataField="IssueDate" FormatString="yyyy-MM-dd" HeadingText="颁布日期" />
                                <ComponentArt:GridColumn DataField="ExecuteDate" FormatString="yyyy-MM-dd" HeadingText="实施日期" />
                                <ComponentArt:GridColumn DataField="Status" HeadingText="状态" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTStatus" HeadingText="状态" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTStatus">
                            ## DataItem.getMember("Status").get_value() == 1 ? "有效" : "失效" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsRegulation" runat="server" SelectMethod="GetRegulationsByCategoryID"
            TypeName="RailExam.BLL.RegulationBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="categoryID" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <ComponentArt:Menu ID="ContextMenu" ContextMenu="Custom" runat="server" EnableViewState="true">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="查看" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="新增" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="编辑" />
                <ComponentArt:MenuItem LookId="BreakItem" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                    Text="删除" />
            </Items>
        </ComponentArt:Menu>
        <input type="hidden" id="NewButton" onclick="return AddRecord();" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>