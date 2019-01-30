<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookInfo.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.BookInfo" %>
<%@ Import namespace="RailExamWebApp.Common.Class"%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教材信息</title>
   <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script> 
    <script type="text/javascript">
        function GetIndex(id)
        {
       		 var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            }   
            form1.Index.value = id;
            form1.submit();
        }
       
       function DelBook(id,orgID)
       {
                var flagDelete=document.getElementById("HfDeleteRight").value;
               
           	     if(flagDelete=="False")
                {                     
                    alert("您没有权限使用该操作！");
                    return;
                }   
               
                var SuitRange= form1.SuitRange.value;
                var noworgid = document.getElementById("HfOrgId").value;
                if(SuitRange == 0 && noworgid !=orgID)
                {
                    alert("您没有权限删除该本教材！");
                    return;
                }  
         
                 if(! confirm("您确定要删除该教材吗？如果删除则该教材的所有试题将被禁用！"))
                 {
                    return false; 
                }
               if(form1.UpdateRecord.value =="1")
               {
                     var ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?BookID="+ id + "&ChapterID=0&Object=delbook",'dialogWidth:600px;dialogHeight:400px;');
                    if(ret == "true")
                    {
                        form1.DeleteID.value = id;
                        form1.submit();
                        form1.DeleteID.value = "";
                    } 
              }
             else
            {
                        form1.DeleteID.value = id;
                        form1.submit();
                        form1.DeleteID.value = "";
            }  
       } 
        
        function AddRecord()
		{
		    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            }   
                      
//          var ret = window.open("/RailExamBao/Book/BookDetail.aspx?Mode=Add",'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
//		    ret.focus();
            var ret = showCommonDialog("/RailExamBao/Book/BookDetail.aspx?Mode=Add",'dialogWidth:800px;dialogHeight:650px');
		    if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
		    }
		}
		
		function EditBook(id,orgid)
		{
       		 var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            }  
            
            var SuitRange= form1.SuitRange.value;
            var noworgid = document.getElementById("HfOrgId").value;
            if(SuitRange == 0 && noworgid != orgid)
            {
                alert("您没有权限修改该本教材！");
                return;
            }
	        
	        var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 

//		    var ret = window.open('/RailExamBao/Book/BookDetail.aspx?mode=Edit&id='+id,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//		    ret.focus();
            var ret = showCommonDialog('/RailExamBao/Book/BookDetail.aspx?mode=Edit&id='+id,'dialogWidth:800px;dialogHeight:650px');
		    if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
		    } 
	 }
		
		function OpenIndex(id)
		{
		    var re = window.open('../Online/Book/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
		    re.focus();
//            var ret = showCommonDialogFull('../Online/Book/' + id + '/index.html', "dialogWidth:" + window.screen.width + "px;dialogHeight:" + window.screen.height + "px;");
		}
		
		function lookbook(id)
		{
		    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            
//		    var ret = window.open('/RailExamBao/Book/BookDetail.aspx?mode=ReadOnly&id='+id,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//			ret.focus()
            var ret = showCommonDialog('/RailExamBao/Book/BookDetail.aspx?mode=ReadOnly&id='+id,'dialogWidth:800px;dialogHeight:650px');
		}
        
        function ManageChapter(id,orgid)
        {
         
            var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagOrgID=document.getElementById("HfOrgId").value;  
                  
	        if(flagUpdate=="False")
              {                     
                alert("您没有权限使用该操作！");
                return;
              }                          
                      
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;
//            var ret = window.open("/RailExamBao/Book/BookChapter.aspx?id="+id+"&Mode=Edit","BookChapter",'Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//            ret.focus();
            var ret = showCommonDialog("/RailExamBao/Book/BookChapter.aspx?id="+id+"&Mode=Edit",'dialogWidth:800px;dialogHeight:650px');
        }
	
        function Grid1_onContextMenu(sender, eventArgs)
        {
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "修改");
             var menuItemAdd = ContextMenu.findItemByProperty("Text", "新增");
            var menuItemUp = ContextMenu.findItemByProperty("Text", "上移");
            var menuItemDown = ContextMenu.findItemByProperty("Text", "下移");
            var menuItemDelete = ContextMenu.findItemByProperty("Text", "删除");
            var menuItemOrder = ContextMenu.findItemByProperty("Text", "设置序号");
            var menuItemOrg = ContextMenu.findItemByProperty("Text", "设置单位");
            
            var orgid=eventArgs.get_item().getMember('publishOrg').get_text();          
          
            var flagUpdate=document.getElementById("HfUpdateRight").value;
            var flagDelete=document.getElementById("HfDeleteRight").value;   
            var flagOrgID=document.getElementById("HfOrgId").value;  
                  
	        if(flagUpdate=="True")
            {                     
               menuItemEdit.set_enabled(true); 
               menuItemAdd.set_enabled(true); 
               menuItemUp.set_enabled(true);
               menuItemDown.set_enabled(true);
               menuItemOrder.set_enabled(true);
               menuItemOrg.set_enabled(true);
            }
            else
            {
                menuItemEdit.set_enabled(false); 
                menuItemAdd.set_enabled(false); 
                menuItemUp.set_enabled(false);
                menuItemDown.set_enabled(false);
               menuItemOrder.set_enabled(false); 
               menuItemOrg.set_enabled(false);
           }
            
            if(flagDelete=="True")
            {                     
               menuItemDelete.set_enabled(true); 
            }
            else
            {
                
               menuItemDelete.set_enabled(false);
            } 
            
            var search= window.location.search;
            if(form1.SuitRange.value=="0" || search.substring(search.indexOf('id=')+3) =='0' || search.substring(search.indexOf('id1=')+4) =='0' || search.indexOf('id2=') >=0)
            {
                menuItemUp.set_enabled(false);
                menuItemDown.set_enabled(false);
                menuItemOrder.set_enabled(false); 
           }
           
           if(form1.SuitRange.value == "1")
           {
               menuItemOrg.set_enabled(true); 
           }
           
            switch(eventArgs.get_item().getMember('bookName').get_text())
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
            var search= window.location.search;
            
            switch(menuItem.get_text())
            {
                case '查看':
//                  var ret = window.open('/RailExamBao/Book/BookDetail.aspx?mode=ReadOnly&id='+contextDataNode.getMember('bookId').get_value(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//				    ret.focus();
				    var ret = showCommonDialog('/RailExamBao/Book/BookDetail.aspx?mode=ReadOnly&id='+contextDataNode.getMember('bookId').get_value(),'dialogWidth:800px;dialogHeight:650px');

				    break;
				
				case '新增':
                    AddRecord();
                    break;
				    
                case '修改':
                    
                    var SuitRange= form1.SuitRange.value;
                    var noworgid = document.getElementById("HfOrgId").value;
                    if(SuitRange == 0 && noworgid != contextDataNode.getMember('publishOrg').get_value())
                    {
                        alert("您没有权限修改该本教材！");
                        return;
                    } 
//                  var ret = window.open('/RailExamBao/Book/BookDetail.aspx?mode=Edit&id='+contextDataNode.getMember('bookId').get_value(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//				    ret.focus();
				    var ret = showCommonDialog('/RailExamBao/Book/BookDetail.aspx?mode=Edit&id='+contextDataNode.getMember('bookId').get_value(),'dialogWidth:800px;dialogHeight:650px');
				    if(ret == "true")
				    {
					    form1.Refresh.value = ret;
					    form1.submit();
					    form1.Refresh.value = "";
				    }
				    
                    break;
                case '删除':
                    DelBook(contextDataNode.getMember('bookId').get_value(),contextDataNode.getMember('publishOrg').get_value());
                    break;
               case '上移':
                    if(contextDataNode.getMember('OrderIndex').get_value() == 1)
                    {
                        alert('该教材已经排在第一位！');
                       return false; 
                    }
                    form1.UpID.value =contextDataNode.getMember('bookId').get_value();
					form1.submit();
					form1.UpID.value = "";
					break;
               case '下移':
                    if(contextDataNode.getMember('OrderIndex').get_value() == Grid1.get_recordCount())
                    {
                        alert('该教材已经排在最后一位！');
                       return false; 
                    }
                    form1.DownID.value =contextDataNode.getMember('bookId').get_value();
					form1.submit();
				    form1.DownID.value = "";
                    break;
                case '设置序号':
                    var ret ; 
                    if(search.indexOf('id=') >0)
                    {
                          ret = showCommonDialog("/RailExamBao/Book/SetOrderIndex.aspx?BookID="+ contextDataNode.getMember('bookId').get_value() + "&NowOrder="+contextDataNode.getMember('OrderIndex').get_value() +"&MaxOrder="+Grid1.get_recordCount(),'dialogWidth:180px;dialogHeight:100px;');
                    }
                    
                    if(search.indexOf('id1=') >0)
                    {
                          var str = search.substring(search.indexOf('id1=')+4);
                          ret = showCommonDialog("/RailExamBao/Book/SetOrderIndex.aspx?BookID="+ contextDataNode.getMember('bookId').get_value() + "&TrainTypeID="+str + "&NowOrder="+contextDataNode.getMember('OrderIndex').get_value() +"&MaxOrder="+Grid1.get_recordCount(),'dialogWidth:180px;dialogHeight:100px;');
                    }
                    if(ret == "true")
                    {
                        form1.Refresh.value = ret;
                        form1.submit();
                        form1.Refresh.value = "";
                    } 
                    break; 
                case '设置单位':
                    var ret;
                    ret = showCommonDialog("/RailExamBao/RandomExamOther/SelectBookOrg.aspx?BookID="+ contextDataNode.getMember('bookId').get_value(),'dialogWidth:320px;dialogHeight:620px;');
                    //alert(ret); 
                    if(ret == "true")
                    {
                        form1.Refresh.value = ret;
                        form1.submit();
                        form1.Refresh.value = "";
                    } 
            }
            
            return true; 
        }
       
        function eWebEditorPopUp(id)
        {
            var re = window.open("../ewebeditor/asp/ShowEditor.asp?BookID="+id+"&Type=Edit" ,'ShowEditor','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
            re.focus();
	    } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="query" style="display: none;">
                &nbsp;书名
                <asp:TextBox ID="txtBookName" runat="server" Width="10%"></asp:TextBox>
                作者
                <asp:TextBox ID="txtAuthors" runat="server" Width="10%"></asp:TextBox>
                关键字
                <asp:TextBox ID="txtKeyWords" runat="server" Width="10%"></asp:TextBox>
                <%--<asp:ImageButton runat="server" ID="btnQuery" OnClick="btnQuery_Click" ImageUrl="~/Common/Image/confirm.gif" />--%>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click"  />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="98%" OnLoad="Grid1_Load">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid1_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="bookId" >
                            <Columns>
                                <ComponentArt:GridColumn DataField="bookId" Visible="false" />
                                 <ComponentArt:GridColumn DataField="OrderIndex" HeadingText="序号"  />
                                <ComponentArt:GridColumn DataField="bookName" HeadingText="教材名称" Align="Left"   />
                                <ComponentArt:GridColumn DataField="knowledgeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="trainTypeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="bookNo" HeadingText="编号" />
                                <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="编制单位" />
                                <ComponentArt:GridColumn DataField="Authors" HeadingText="编著者" />
                                 <ComponentArt:GridColumn DataField="publishDate" HeadingText="编制时间" FormatString="yyyy-MM-dd" />
                                <ComponentArt:GridColumn DataField="publishOrg" HeadingText="编制单位"  Visible="false"/>                                
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <a  onclick="EditBook(##DataItem.getMember('bookId').get_value()##,##DataItem.getMember('publishOrg').get_value()## )" title="修改教材"  href="#"><b>修改</b></a> &nbsp;
                           <a onclick="DelBook(##DataItem.getMember('bookId').get_value()## ,##DataItem.getMember('publishOrg').get_value()## )" title="删除教材" href="#"><b>删除</b></a>
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
                <ComponentArt:MenuItem Text="修改" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem Text="新增" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="删除" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="上移" look-lefticonurl="move_up.gif" disabledlook-lefticonurl="move_up_disabled.gif" />
                <ComponentArt:MenuItem Text="下移" look-lefticonurl="move_down.gif" disabledlook-lefticonurl="move_down_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                 <ComponentArt:MenuItem Text="设置序号" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                 <ComponentArt:MenuItem Text="设置单位" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
           </Items>
        </ComponentArt:Menu>
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshCover" /> 
         <asp:HiddenField ID="HfUpdateRight" runat="server" />
          <asp:HiddenField ID="HfOrgId" runat="server" />
          <input type="hidden" name="Index" />
          <asp:HiddenField ID="HfDeleteRight" runat="server" />
         <input type="hidden" name="DeleteID" />
         <input type="hidden" name="UpID" />
          <input type="hidden" name="DownID" />
          <input type="hidden" name="UpdateRecord" value='<%=PrjPub.FillUpdateRecord %>' /> 
         <input type="hidden" name="SuitRange" value ='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
    </form>
</body>
</html>
