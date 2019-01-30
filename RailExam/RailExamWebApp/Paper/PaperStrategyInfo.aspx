<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperStrategyInfo.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperStrategyInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组卷策略信息</title>

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
                         item.getItemByProperty("Text", "新增").set_enabled(false);                    
                         item.getItemByProperty("Text", "编辑").set_enabled(false);
                      }  
                      else
                      {  
                       item.getItemByProperty("Text", "新增").set_enabled(true);                        
                       item.getItemByProperty("Text", "编辑").set_enabled(true);
                      }              
                                
                       if(flagDelete=="False" )
                      {                     
                     item.getItemByProperty("Text", "删除").set_enabled(false);
                      }  
                      else
                      {
                      item.getItemByProperty("Text", "删除").set_enabled(true);
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
                case '查看':
                    var ret = window.open('PaperStrategyEdit.aspx?mode=ReadOnly&id='+contextDataNode.getMember('PaperStrategyId').get_value(),'PaperStrategyEdit',' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
                    break;
                    
                case '编辑':
                    var ret = window.open('PaperStrategyEdit.aspx?mode=Edit&id='+contextDataNode.getMember('PaperStrategyId').get_value(),'PaperStrategyEdit','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
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
                    if(! confirm("您确定要删除“" + contextDataNode.getMember('PaperStrategyName').get_text() + "”吗？"))
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
           alert("您没有删除该信息的权限！");
           return;
         }
         
        if(! confirm("您确定要删除“" + id1 + "”吗？"))
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
                                <ComponentArt:GridColumn DataField="CategoryName" HeadingText="试卷分类" />
                                <ComponentArt:GridColumn DataField="PaperStrategyName" HeadingText="组卷策略名称" />
                                <ComponentArt:GridColumn DataField="PaperCategoryId" HeadingText="PaperCategoryId"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="IsRandomOrder" HeadingText="打乱试题显示顺序" Visible="false" />
                                <ComponentArt:GridColumn DataField="SingleAsMultiple" HeadingText="把单选显示成多选" Visible="false" />
                                <ComponentArt:GridColumn DataField="StrategyMode" HeadingText="策略模式" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="策略模式" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="打乱试题显示顺序"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate2" HeadingText="把单选显示成多选"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" Width="60" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            ## DataItem.getMember("IsRandomOrder").get_value() == 1 ? "是":"否" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate2">
                            ## DataItem.getMember("SingleAsMultiple").get_value() == 1 ? "是":"否" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate3">
                            ## DataItem.getMember("StrategyMode").get_value() == 2 ? "按教材章节":"按考试辅助分类" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <a onclick="UpdateData(##DataItem.getMember('PaperStrategyId').get_value()## )" href="#">
                                <b>编辑</b></a> <a onclick="DeleteData('## DataItem.getMember('PaperStrategyName').get_value() ##','## DataItem.getMember('PaperStrategyId').get_value() ##')"
                                    href="#"><b>删除</b></a>
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
                <ComponentArt:MenuItem Text="查看" look-lefticonurl="view.gif" disabledLook-LeftIconUrl="view_disabled.gif" />
                <ComponentArt:MenuItem Text="编辑" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem Text="新增" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="删除" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
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
