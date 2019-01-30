<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PaperStrategyItemCategoryInfo.aspx.cs" Inherits="RailExamWebApp.Paper.PaperStrategyItemCategoryInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�����ԣ������⸨������ĸ��Ѷȷ��䣩</title>
    <script type="text/javascript">
        function AddRecord()
		{
			var id = document.getElementById("hfStrategySubjectID").value;
	        var ret = window.open("PaperStrategyItemFourth.aspx?mode=Insert&id="+id ,'PaperStrategyItemFourth','Width=600px; Height=520px,status=false,resizable=no',true);
			if(ret == "true")
			{
			    form1.Refresh.value = ret;
                form1.submit();
                form1.Refresh.value = "";
			}
		}
		
        function Grid_onContextMenu(sender, eventArgs)
        {
            switch(eventArgs.get_item().getMember('SubjectName').get_text())
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
            
            switch(menuItem.get_text())
            {
                case '�鿴':
                    var ret = window.open('PaperStrategyItemFourth.aspx?mode=ReadOnly&id='+contextDataNode.getMember('StrategyItemCategoryId').get_value(),'PaperStrategyItemFourth',' Width=600px; Height=520px,status=false,resizable=no',true);
				    ret.focus();
                    break;
                    
                case '�༭':
				    var ret = window.open('PaperStrategyItemFourth.aspx?mode=Edit&id='+contextDataNode.getMember('StrategyItemCategoryId').get_value(),'PaperStrategyItemFourth','Width=600px; Height=520px,status=false,resizable=no',true);
				                           
                    break;
             
                case '����':
                    AddRecord();
                    break;
                
                case 'ɾ��': 
                    if(! confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('SubjectName').get_text() + "����"))
                    {
                        return false;
                    }
                    form1.DeleteID.value = contextDataNode.getMember('StrategyItemCategoryId').get_value();
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
        <div id="girdPage">
            <div id="gird">
                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" DataSourceID="odsPaperStrategyItemCategory" Width="95%">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="StrategyItemCategoryId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="StrategyItemCategoryId" Visible="false" />
                                <ComponentArt:GridColumn DataField="CategoryName" HeadingText="���⸨������" />
                                <ComponentArt:GridColumn DataField="SubjectName" HeadingText="��������" />
                                <ComponentArt:GridColumn DataField="StrategySubjectId" Visible="false" />
                                <ComponentArt:GridColumn DataField="ItemTypeName" HeadingText="����" />
                                <ComponentArt:GridColumn DataField="ItemDifficultyRandomCount" HeadingText="�������" />                             
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>                   
                </ComponentArt:Grid>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsPaperStrategyItemCategory" runat="server" SelectMethod="GetItemsByPaperSubjectId"
            TypeName="RailExam.BLL.PaperStrategyItemCategoryBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="paperSubjectId" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
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
        <input type="hidden" id="NewButton" onclick="return AddRecord();" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="hfStrategySubjectID" runat="server" />
    </form>
</body>
</html>
