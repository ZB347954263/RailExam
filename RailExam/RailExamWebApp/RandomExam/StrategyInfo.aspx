<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StrategyInfo.aspx.cs" Inherits="RailExamWebApp.RandomExam.StrategyInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>取题范围（按教材章节的各难度分配）</title>
    <script type="text/javascript" src="../Common/JS/Common.js"></script>
    <script type="text/javascript">
  	    function AddRecord()
		{  
		 var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-600)*.5;   
          ctop=(screen.availHeight-520)*.5;             
		 
		    var itemTypeID = document.getElementById("hfItemType").value;
		    var id = document.getElementById("hfStrategySubjectID").value;
	       
  	    	//var ret = window.open("StrategyEdit.aspx?itemTypeID="+itemTypeID+"&mode=Insert&id="+id ,'StrategyEdit','Width=600px; Height=520px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			
  	    	var ret = showCommonDialog("/RailExamBao/RandomExam/StrategyEdit.aspx?itemTypeID="+ itemTypeID+"&mode=Insert&subjectid="+id,'dialogWidth:600px;dialogHeight:520px;');
		    if(ret.indexOf("False")>=0) 
		  	 {
		  	 	form1.Refresh.value = ret.split('|')[1];
		  	 	form1.submit();
		  	 	form1.Refresh.value = "";
		  	 } 
		    else 
		    {
		    	 form1.Refresh.value = "true";
                form1.submit();
                form1.Refresh.value = "";
		    }
		}
		
        function Grid_onContextMenu(sender, eventArgs)
        {
        	 var item = ContextMenu1.get_items();
        	 if(window.parent.document.getElementById("hfMode").value=="readonly") {
        	 	item.getItemByProperty("Text", "新增").set_enabled(false);                        
                item.getItemByProperty("Text", "编辑").set_enabled(false);
        	 	item.getItemByProperty("Text", "删除").set_enabled(false);        
        	 }
        	
            switch(eventArgs.get_item().getMember('RangeName').get_text())
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
          cleft=(screen.availWidth-600)*.5;   
          ctop=(screen.availHeight-520)*.5;    
          
          var itemTypeID = document.getElementById("hfItemType").value; 
            switch(menuItem.get_text())
            {
                case '查看':
//                    var ret = window.open('StrategyEdit.aspx?itemTypeID='+itemTypeID+'&mode=ReadOnly&id='+contextDataNode.getMember('RandomExamStrategyId').get_value(),'StrategyEdit',' Width=600px; Height=520px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//				      ret.focus();
                	
                	var ret = showCommonDialog('/RailExamBao/RandomExam/StrategyEdit.aspx?itemTypeID='+itemTypeID+'&mode=ReadOnly&id='+contextDataNode.getMember('RandomExamStrategyId').get_value(),'dialogWidth:600px;dialogHeight:520px;');
  	    	        if(ret == "true")
			        {
			        }
                	
                    break;

                case '编辑':
                    
                	//var ret = window.open('StrategyEdit.aspx?itemTypeID='+itemTypeID+'&mode=Edit&id='+contextDataNode.getMember('RandomExamStrategyId').get_value(),'StrategyEdit','Width=600px; Height=520px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				   
                	var ret = showCommonDialog('/RailExamBao/RandomExam/StrategyEdit.aspx?itemTypeID='+itemTypeID+'&mode=Edit&id='+contextDataNode.getMember('RandomExamStrategyId').get_value(),'dialogWidth:600px;dialogHeight:520px;');
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
                    if(! confirm("您确定要删除该信息吗？"))
                    {
                        return false;
                    }
                    form1.DeleteID.value = contextDataNode.getMember('RandomExamStrategyId').get_value();
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
                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" DataSourceID="odsPaperStrategyBookChapter" Width="95%">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="RandomExamStrategyId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="RandomExamStrategyId" Visible="false" />
                                <ComponentArt:GridColumn DataField="RangeName" HeadingText="教材章节范围" />
                               <ComponentArt:GridColumn DataField="ItemTypeName" HeadingText="题型" />    
                               <ComponentArt:GridColumn DataField="SelectCount" HeadingText="选择题数" />    
                                  <ComponentArt:GridColumn DataField="TotalItemCount" HeadingText="随机题数" />                           
                                <ComponentArt:GridColumn DataField="StrategySubjectId" Visible="false" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>                   
                </ComponentArt:Grid>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsPaperStrategyBookChapter" runat="server" SelectMethod="GetTotalRandomExamStrategys"
            TypeName="RailExam.BLL.RandomExamStrategyBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="SubjectID" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <ComponentArt:Menu ID="ContextMenu1" ContextMenu="Custom" runat="server" EnableViewState="true">
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
        <asp:HiddenField ID="hfStrategySubjectID" runat="server" />
        <asp:HiddenField ID="hfItemType" runat="server" />
    </form>
</body>
</html>

