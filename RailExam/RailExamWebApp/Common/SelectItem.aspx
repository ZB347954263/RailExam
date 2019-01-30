<%@ Page Language="C#" AutoEventWireup="True" Codebehind="SelectItem.aspx.cs" Inherits="RailExamWebApp.Common.SelectItem" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择试题</title>

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
        
        function tvView_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            var hfKnowledgeIdPath = $.$F("hfKnowledgeIdPath");
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");
            var hfTrainTypeIdPath = $.$F("hfTrainTypeIdPath");
            var hfCategoryIdPath = $.$F("hfCategoryIdPath");
            
            // Reset hidden fields
            hfKnowledgeIdPath.value = "null";
            hfBookId.value = "-1";
            hfChapterId.value = "-1";
            hfTrainTypeIdPath.value = "null";
            hfCategoryIdPath.value = "null";
            
            // VIEW_KNOWLEDGE
            if(node && node.getProperty("isKnowledge") == "true")
            {
                hfKnowledgeIdPath.value = node.get_value();
                treeNodeSelectedCallBack.callback();
            }
            
            if(node && node.getProperty("isBook") == "true")
            {
                hfBookId.value = node.get_id();
                treeNodeSelectedCallBack.callback();
            }
            if(node && node.getProperty("isChapter") == "true")
            {
                var tempNode = node;
                while(true)
                {
                    if(tempNode.getProperty("isBook") == "true")
                    {
                        break;
                    }
                    else
                    {
                        tempNode = tempNode.get_parentNode();
                    }
                }
                hfBookId.value = tempNode.get_id();
                hfChapterId.value = node.get_id();
                treeNodeSelectedCallBack.callback();
            }
            
            // VIEW_TRAINTYPE
            if (node && node.getProperty("isTrainType") == "true")
            {
                hfTrainTypeIdPath.value = node.get_value();
                treeNodeSelectedCallBack.callback();
            }
            
            // VIEW_CATEGORY
            if (node && node.getProperty("isCategory") == "true")
            {
                hfCategoryIdPath.value = node.get_value();
                treeNodeSelectedCallBack.callback();
            }
        }
        
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
            for(var i = 0; i < itemsGrid.get_table().getRowCount(); i ++)
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
                return;
            }
            if(ids.lastIndexOf(",") == ids.length -1)
            {
                ids = ids.substring(0, ids.length -1);
            }
             
            window.returnValue = ids;
            window.close();
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
                    ids += theItem.GetProperty("Id") + ",";
                }
            }
            
            return ids;
        }
        
        function SaveCheckItems(ckb)
        {  
          var hfSaveItemIDs = $.$F("hfSaveItemIDs"); 
            
           var ids =  hfSaveItemIDs.value;
           if(ids=="")
           {
           ids =  ",";
           }
           var theItem;

            //showProperties(itemsGrid);
                        
            if(ckb.checked)
            {
             ids += ckb.value + ",";
            }
            else
            {
            var id1=","+ckb.value + ",";            
                        
             ids= ids.replace(new RegExp(id1, 'g'), ",");            
            }           
            
//            for(var i = 0; i < itemsGrid.get_table().getRowCount(); i ++)
//            {
//                theItem = itemsGrid.getItemFromClientId(itemsGrid.get_table().getRow(i).get_clientId());
//                if($.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked)
//                {
//                    ids += theItem.GetProperty("Id") + "|";
//                } 
//             }  
              
            hfSaveItemIDs.value= ids;
           // alert(ids);
        }
        
      
        function ddlView_onChange()
        {
            var ddlView = $.$F("ddlView");

            switch(ddlView.selectedIndex)
            {
                case 0:
                {
                    ddlViewChangeCallBack.callback("VIEW_KNOWLEDGE");
                    break;
                }
                case 1:
                {
                    ddlViewChangeCallBack.callback("VIEW_TRAINTYPE");
                    break;
                }
                case 2:
                {
                    ddlViewChangeCallBack.callback("VIEW_CATEGORY");
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        
        var _fHide = true;
        function toggleButton_onClick()
        {
            var h = $("#query").height();
            var res = $("#query").slideToggle("slow");
            if(_fHide)
            {
                $("#grid").height($("#grid").height() - h);
                _fHide = !_fHide;
            }
            else
            {
                $("#grid").height($("#grid").height() + h);
                _fHide = !_fHide;
            }
        }
        
        function searchButton_onClick()
        {
            var hfKnowledgeIdPath = $.$F("hfKnowledgeIdPath");
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");
            var hfTrainTypeIdPath = $.$F("hfTrainTypeIdPath");
            var hfCategoryIdPath = $.$F("hfCategoryIdPath");
            
             var hftxtitemScore = $.$F("hftxtitemScore");    
          
          
            if(hfKnowledgeIdPath.value.length = 0)
            {
                hfKnowledgeIdPath.value = 'null';
            }         
            if(hfBookId.value.length = 0)
            {
                hfBookId.value = '-1';
            }         
            if(hfChapterId.value.length = 0)
            {
                hfChapterId.value = '-1';
            }         
            if(hfTrainTypeIdPath.value.length = 0)
            {
                hfTrainTypeIdPath.value = 'null';
            }         
            if(hfCategoryIdPath.value.length = 0)
            {
                hfCategoryIdPath.value = 'null';
            }         
            
               if($.$F("txtitemScore").value.length > 0)
            {
                hftxtitemScore.value = $.$F("txtitemScore").value;
            }     
            
            $.$F("hfIsSearchCommand").value = "true";
            treeNodeSelectedCallBack.callback();
        }
        
        $(document.body).ready(function(){
            var h = $("#query").height();
            $("#grid").height($("#grid").height() + h);
        });
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
                        选择试题</div>
                </div>
                <div id="button">
                    <img alt="" onclick="toggleButton_onClick();" src="../Common/Image/query.gif" />
                    <img alt="" onclick="btnOkClicked();" src="../Common/Image/confirm.gif" />
                    <img alt="" onclick="return window.close();" src="../Common/Image/cancel.gif" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        查看方式
                        <select id="ddlView" onchange="ddlView_onChange();">
                            <option>按知识体系</option>
                            <option>按培训类别</option>
                            <option>按辅助分类</option>
                        </select>
                    </div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="ddlViewChangeCallBack" runat="server" PostState="true"
                            OnCallback="ddlViewChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvView" runat="server" EnableViewState="true">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvView_onNodeSelect" />
                                        <%--<ContextMenu EventHandler="tvView_onContextMenu" />--%>
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <div id="query" style="display: none; text-align: left;">
                            <asp:DropDownList ID="ddlItemDifficulty" runat="server" DataSourceID="odsItemDifficulty"
                                DataTextField="DifficultyName" DataValueField="ItemDifficultyId">
                            </asp:DropDownList>
                            分数<input id="txtitemScore" type="text" size="5" />
                            <asp:DropDownList ID="ddlUsage" runat="server">
                                <asp:ListItem Text="-试题用途-" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="练习和考试" Value="0"></asp:ListItem>
                                <asp:ListItem Text="仅考试" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            <img id="btnQuery" alt="查  询" onclick="searchButton_onClick();" src="../Common/Image/confirm.gif" />
                        </div>
                        <div id="grid">
                            <ComponentArt:CallBack ID="treeNodeSelectedCallBack" runat="server" PostState="true"
                                OnCallback="treeNodeSelectedCallBack_Callback" Debug="false">
                                <Content>
                                    <ComponentArt:Grid ID="itemsGrid" DataSourceID="odsItems" runat="server" OnDataBinding="itemsGrid_DataBinding"
                                        OnPageIndexChanged="itemsGrid_PageIndexChanged" OnSortCommand="itemsGrid_SortCommand"
                                        PageSize="20" RunningMode="Callback" Debug="false" ManualPaging="true" AllowPaging="true">
                                        <Levels>
                                            <ComponentArt:GridLevel DataKeyField="ItemId">
                                                <Columns>
                                                    <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" Width="40"
                                                        HeadingText="&lt;a href='#' onclick='btnSelectAllClicked()' &gt;全选&lt;/a&gt;&lt;a href='#' onclick='btnUnselectAllClicked()' &gt;反选&lt;/a&gt;" />
                                                    <ComponentArt:GridColumn DataField="ItemId" IsSearchable="true" HeadingText="试题编号"
                                                        Visible="false" />
                                                    <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="组织机构" IsSearchable="true"
                                                        Width="80" />
                                                    <ComponentArt:GridColumn DataField="TypeName" HeadingText="题型" IsSearchable="true"
                                                        Width="40" />
                                                    <ComponentArt:GridColumn DataField="DifficultyName" HeadingText="难度" IsSearchable="true"
                                                        Width="40" />
                                                    <ComponentArt:GridColumn DataField="Score" HeadingText="分值" IsSearchable="true" Width="40" />
                                                    <ComponentArt:GridColumn DataField="Content" HeadingText="内容" IsSearchable="true"
                                                        Width="220" Align="Left" />
                                                    <ComponentArt:GridColumn DataField="StatusName"  HeadingText="状态" IsSearchable="true"
                                                        Width="40" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                        </Levels>
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="CTEdit" >
                                                <input id="cbxSelectItem_##DataItem.getMember('ItemId').get_value()##" name="cbxSelectItem_##DataItem.getMember('ItemId').get_value()##"
                                                    type="checkbox" value="##DataItem.getMember('ItemId').get_value()##"  />
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <%--<ClientEvents>
                                            <PageIndexChange EventHandler="itemsGrid_onPageIndexChange" />
                                        </ClientEvents>--%>
                                    </ComponentArt:Grid>
                                </Content>
                            </ComponentArt:CallBack>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<asp:HiddenField ID="hfItemId" runat="server" Value="-1" />--%>
        <asp:HiddenField ID="hfKnowledgeIdPath" runat="server" />
        <asp:HiddenField ID="hfBookId" runat="server" />
        <asp:HiddenField ID="hfChapterId" runat="server" />
        <asp:HiddenField ID="hfTrainTypeIdPath" runat="server" />
        <asp:HiddenField ID="hfCategoryIdPath" runat="server" />
        <asp:HiddenField ID="hfSource" runat="server" />
        <asp:HiddenField ID="hfVersion" runat="server" />
        <asp:HiddenField ID="hfCreatePerson" runat="server" />
        <asp:HiddenField ID="hfIsSearchCommand" runat="server" />
        <asp:HiddenField ID="hftxtitemScore" runat="server" />
        <asp:HiddenField ID="hfSaveItemIDs" runat="server" />
        <%--<asp:TextBox ID="hfKnowledgeIdPath" runat="server" style="display: none;" />--%>
        <asp:ObjectDataSource ID="odsItems" runat="server" DataObjectTypeName="RailExam.Model.Item"
            DeleteMethod="DeleteItem" InsertMethod="AddItem" SelectMethod="GetItems" TypeName="RailExam.BLL.ItemBLL"
            UpdateMethod="UpdateItem" OnObjectCreated="odsItems_ObjectCreated" SelectCountMethod="GetCount">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfKnowledgeIdPath" Name="knowledgeIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="hfBookId" DefaultValue="-1" Name="bookId" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hfChapterId" DefaultValue="-1" Name="chapterId"
                    PropertyName="Value" Type="Int32" />
                <asp:ControlParameter ControlID="hfTrainTypeIdPath" Name="trainTypeIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="hfCategoryIdPath" Name="categoryIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:QueryStringParameter QueryStringField="ItemType" DefaultValue="1" Name="itemTypeId"
                    Type="Int32" />
                <asp:QueryStringParameter QueryStringField="paperId" DefaultValue="1" Name="paperId"
                    Type="Int32" />
                <asp:ControlParameter ControlID="ddlItemDifficulty" DefaultValue="-1" Name="itemDifficultyId"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="hftxtitemScore" Name="itemScore" PropertyName="Value"
                    Type="Int32" DefaultValue="-1" />
                <asp:Parameter Name="StatusId" Type="Int32" DefaultValue="1" />
                <asp:ControlParameter ControlID="ddlUsage" DefaultValue="-1" Name="usageId" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="itemsGrid" DefaultValue="-1" Name="currentPageIndex"
                    PropertyName="CurrentPageIndex" Type="Int32" />
                <asp:ControlParameter ControlID="itemsGrid" DefaultValue="-1" Name="pageSize" PropertyName="PageSize"
                    Type="Int32" />
                <asp:ControlParameter ControlID="itemsGrid" DefaultValue="null" Name="orderBy" PropertyName="Sort"
                    Type="String" />
                <asp:SessionParameter SessionField="OrganizationID" Name="OrgId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemType" runat="server" SelectMethod="GetItemTypes"
            TypeName="RailExam.BLL.ItemTypeBLL" DataObjectTypeName="RailExam.BLL.ItemType">
            <SelectParameters>
                <asp:Parameter Name="bForSearchUse" Type="boolean" DefaultValue="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemDifficulty" runat="server" SelectMethod="GetItemDifficulties"
            TypeName="RailExam.BLL.ItemDifficultyBLL" DataObjectTypeName="RailExam.BLL.ItemDifficulty">
            <SelectParameters>
                <asp:Parameter Name="bForSearchUse" Type="boolean" DefaultValue="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
