<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ItemEnabledList.aspx.cs"
    Inherits="RailExamWebApp.Book.ItemEnabledList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部分禁用试题</title>

    <script src="../Common/JS/jquery.js" type="text/javascript">
    </script>

    <!-- DO NOT DELETE THIS TAG -->

    <script type="text/javascript">
    </script>

    <script type="text/javascript">
          $.extend({
            $F : function(objId){
                return document.getElementById(objId);                
            },            
            getProperties : function(obj){	
                var ps;
                if(typeof(obj) != "object" || obj == "undefined")
                {
                    ps = "[" + obj + "]<BR/>";
                    
                    return ps;
                }
                
                try
                {
                    $.each(obj, function(n, v){
                        if(typeof(p) != "object")
                        {					
	                        ps += "[" + n + "=" + v + "]<BR/>";
                        }
                        else
                        {
	                        ps += "[" + n + "=" + $.getProperties(p) + "]<BR/>";
                        }
                    });
                }
                catch(e)
                {
                    ps = "Not DOM Objects！" + $.getProperties(e);
                }
                
                return ps;			
            },
            showProperties : function (obj){
	            var win = window.open();
	            
	            win.title = "Properties of " + obj;
	            win.document.write($.getProperties(obj));
	            win.document.close();
            }
        });
         
        function btnSelectAllClicked()
        {
            if(itemsGrid.get_table().getRowCount() == 0)
            {
                alert("无试题可选！");
                return;
            }
            
            var theItem;
            for(var i = 0; i < itemsGrid.get_table().getRowCount(); i ++)
            {
                theItem = itemsGrid.getItemFromClientId(itemsGrid.get_table().getRow(i).get_clientId());
                $.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked = true;
            }
        }
        
        function btnUnselectAllClicked()
        {
            if(getSelectedItems() == "")
            {
                alert("无试题可反选！");
                return;
            }
          
            var theItem;
            for(var i = 0; i <itemsGrid.get_table().getRowCount(); i ++)
            {
                theItem = itemsGrid.getItemFromClientId(itemsGrid.get_table().getRow(i).get_clientId());
                $.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked = false;
            }
        }
       
        function btnOkClicked()
        {
            var ids = getSelectedItems();
            if(!ids)
            {
                alert("您至少得选择一道试题！");
                return false;
            }
            if(ids.lastIndexOf("|") == ids.length -1)
            {
                ids = ids.substring(0, ids.length -1);
            } 
            document.getElementById("hfItemID").value = ids;
            return true;  
        }
        
        function getSelectedItems()
        {
            var ids = "";
            var theItem;

            //showProperties(itemsGrid);
            for(var i = 0; i < itemsGrid.get_table().getRowCount(); i ++)
            {
                theItem = itemsGrid.getItemFromClientId(itemsGrid.get_table().getRow(i).get_clientId());
                if($.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked)
                {
                    ids += theItem.GetProperty("Id") + "|";
                }
            }
            
            return ids;
        } 
       
       function btnAllClicked()
       {
          if(form1.btnAll.checked)
          {
               btnSelectAllClicked();
          }
          else
          {
              btnUnselectAllClicked();
          }
       } 
       
              //显示Grid上下文菜单 
        function itemsGrid_onContextMenu(sender, eventArgs)
        {
            var e = (eventArgs.get_event() == null) ? window.event : eventArgs.get_event();
            var contextMenuX = e.clientX ? e.clientX : e.x;
            var contextMenuY = e.clientY ? e.clientY : e.y;

            // Single select
            if(itemsGrid.getSelectedItems().length == 0)
            {
                itemsGrid.select(eventArgs.get_item()); 
            }
            else
            {
                var items = itemsMenu.get_items();
                
                if(itemsGrid.getSelectedItems().length < 2)
                {
                    items.getItemByProperty("Text", "查看").set_enabled(true);
                    itemsGrid.select(eventArgs.get_item());
                }
                else
                {
                    items.getItemByProperty("Text", "查看").set_enabled(false);
                }
            }
           var item = itemsMenu.get_items();                      
           
            itemsMenu.showContextMenu(contextMenuX, contextMenuY);
            itemsMenu.set_contextData(eventArgs.get_item());
            //showProperties(eventArgs.get_event());
        }
        
       //菜单处理函数 
        function itemsMenu_onItemSelect(sender, eventArgs)
        {
            var menuItemSelectecd = eventArgs.get_item();
            var gridItemSelected = itemsMenu.get_contextData();
             var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-570)*.5;    
          
            
            //showProperties(itemsMenu.get_contextData());
            switch(menuItemSelectecd.get_text())
            {
                case "查看":
                {
                
                    var viewWindow = window.open('../Item/ItemDetail.aspx?mode=readonly&id=' + gridItemSelected.GetProperty("Id"),
                        'ItemDetail',' Width=800px; Height=570px,status=false,left='+cleft+',top='+ctop+',resizable=no,scrollbars=yes',true);
                    
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        部分禁用试题</div>
                </div>
                <div id="button">
                    <asp:Button runat="server" ID="btnFalse" OnClientClick="return btnOkClicked();" CssClass="button"
                        Text="禁  用" OnClick="btnFalse_Click" />&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnTrue" OnClientClick="return btnOkClicked();" CssClass="button"
                        Text="可  用" OnClick="btnTrue_Click" />
                </div>
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="itemsGrid" runat="server" DataSourceID="odsItems" OnDataBinding="itemsGrid_DataBinding"
                    OnPageIndexChanged="itemsGrid_PageIndexChanged" RunningMode="Server" ManualPaging="true"
                    Debug="false" PageSize="20">
                    <ClientEvents>
                        <ContextMenu EventHandler="itemsGrid_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ItemId">
                            <Columns>
                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" Width="40"
                                    HeadingText="&lt;input  type='checkbox' onclick='btnAllClicked()' name='btnAll'  /&gt;" />
                                <ComponentArt:GridColumn DataField="ItemId" IsSearchable="true" HeadingText="试题编号"
                                    Visible="false" />
                                <ComponentArt:GridColumn AllowSorting="false" DataField="OrganizationName" HeadingText="组织机构"
                                    IsSearchable="true" Width="80" />
                                <ComponentArt:GridColumn AllowSorting="false" DataField="TypeName" HeadingText="题型"
                                    IsSearchable="true" Width="40" />
                                <ComponentArt:GridColumn AllowSorting="false" DataField="DifficultyName" HeadingText="难度"
                                    IsSearchable="true" Width="40" />
                                <ComponentArt:GridColumn AllowSorting="false" DataField="Score" HeadingText="分值"
                                    IsSearchable="true" Width="40" />
                                <ComponentArt:GridColumn AllowSorting="false" DataField="Content" HeadingText="内容"
                                    IsSearchable="true" Width="220" />
                                <ComponentArt:GridColumn AllowSorting="false" DataField="StatusName" HeadingText="状态"
                                    IsSearchable="true" Width="40" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <input id="cbxSelectItem_##DataItem.getMember('ItemId').get_value()##" name="cbxSelectItem_##DataItem.getMember('ItemId').get_value()##"
                                type="checkbox" value="##DataItem.getMember('ItemId').get_value()##" />
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <%--<ClientEvents>
                                        <PageIndexChange EventHandler="itemsGrid_onPageIndexChange" />
                                    </ClientEvents>--%>
                </ComponentArt:Grid>
                <asp:ObjectDataSource ID="odsItems" runat="server" DataObjectTypeName="RailExam.Model.Item"
                    SelectMethod="GetItemsByBookChapterId" TypeName="RailExam.BLL.ItemBLL" OnObjectCreated="odsItems_ObjectCreated"
                    SelectCountMethod="GetCount">
                    <SelectParameters>
                        <asp:QueryStringParameter QueryStringField="BookID" Name="bookId" Type="int32" Size="4"
                            DefaultValue="561" />
                        <asp:QueryStringParameter QueryStringField="ChapterID" Name="chapterId" Type="int32"
                            Size="4" DefaultValue="426" />
                        <asp:ControlParameter ControlID="itemsGrid" DefaultValue="-1" Name="currentPageIndex"
                            PropertyName="CurrentPageIndex" Type="Int32" />
                        <asp:ControlParameter ControlID="itemsGrid" DefaultValue="-1" Name="pageSize" PropertyName="PageSize"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <ComponentArt:Menu ID="itemsMenu" runat="server" EnableViewState="false">
            <ClientEvents>
                <ItemSelect EventHandler="itemsMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="查看">
                </ComponentArt:MenuItem>
            </Items>
        </ComponentArt:Menu>
        <asp:HiddenField ID="hfItemID" runat="server" />
    </form>
</body>
</html>
