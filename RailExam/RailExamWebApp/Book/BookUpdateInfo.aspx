<%@ Page Language="C#" AutoEventWireup="true" Codebehind="BookUpdateInfo.aspx.cs"
    Inherits="RailExamWebApp.Book.BookUpdateInfo" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教材更新记录</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
          function Grid1_onContextMenu(sender, eventArgs)
        {
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "修改");
          	 var menuItemDelete = ContextMenu.findItemByProperty("Text", "删除");
            var flagUpdate=document.getElementById("HfUpdateRight").value;
                  
	        if(flagUpdate=="True")
            {                     
               menuItemEdit.set_enabled(true); 
            }
            else
            {
               menuItemEdit.set_enabled(false); 
            }
          	
          	 var flagDelete=document.getElementById("HasDeleteRight").value;
                  
	        if(flagDelete=="True")
            {                     
               menuItemDelete.set_enabled(true); 
            }
            else
            {
               menuItemDelete.set_enabled(false); 
            }
         
            switch(eventArgs.get_item().getMember('BookNameBak').get_text())
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
				　ManageChapterUpdate('ReadOnly',contextDataNode.getMember('BookId').get_value(),contextDataNode.getMember('bookUpdateId').get_value(),contextDataNode.getMember('ChapterId').get_value(),contextDataNode.getMember('UpdateObject').get_value());　
				    
				    break;
				
				case '修改':
				　ManageChapterUpdate('Edit',contextDataNode.getMember('BookId').get_value(),contextDataNode.getMember('bookUpdateId').get_value(),contextDataNode.getMember('ChapterId').get_value(),contextDataNode.getMember('UpdateObject').get_value());　
                    break;
			   case '删除':
			   	    if(! confirm("您确定要删除该教材更新记录吗？"))
                    {
                        return false; 
                    }
			   	
			   	    form1.DeleteID.value = contextDataNode.getMember('bookUpdateId').get_value();
                    form1.submit();
                    form1.DeleteID.value = "";
			   	  break;
            }
            
            return true; 
        }
        
        function ManageChapterUpdate(type,bookid,id,chapterid,str)
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-600)*.5;   
            ctop=(screen.availHeight-360)*.5;          
            var ret; 
            if(document.getElementById("hfIsWuhan").value == "True")
            {
                if(str==form1.hfbookinfo.value)
                {
                    ret = window.open("BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=bookinfo&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
                }
                else if(str==form1.hfbookcover.value)
                {
                    ret = window.open("BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=bookcover&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
                }
                else if(str==form1.hfupdatechapterinfo.value)
                {
                     ret = window.open("BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=updatechapterinfo&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
                } 
                else if(str==form1.hfinsertchapterinfo.value)
                {
                     ret = window.open("BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID=0&Object=insertchapterinfo&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
                }     
                else if(str==form1.hfchaptercontent.value)
                {
                     ret = window.open("BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=chaptercontent&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
                }
                else if(str==form1.hfdelbook.value)
                {
                     ret = window.open("BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID=0&Object=delbook&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
                }
                else if(str==form1.hfdelchapter.value)
                {
                     ret = window.open("BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=delchapter&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
                }
                ret.focus(); 
            }
            else
            {
                if(str==form1.hfbookinfo.value)
                {
                    ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=bookinfo&id="+id,'dialogWidth:600px;dialogHeight:410px');
                }
                else if(str==form1.hfbookcover.value)
                {
                    ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=bookcover&id="+id,'dialogWidth:600px;dialogHeight:410px');
                }
                else if(str==form1.hfupdatechapterinfo.value)
                {
                     ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=updatechapterinfo&id="+id,'dialogWidth:600px;dialogHeight:410px');
                } 
                else if(str==form1.hfinsertchapterinfo.value)
                {
                     ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID=0&Object=insertchapterinfo&id="+id,'dialogWidth:600px;dialogHeight:410px');
                }     
                else if(str==form1.hfchaptercontent.value)
                {
                     ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=chaptercontent&id="+id,'dialogWidth:600px;dialogHeight:410px');
                }
                else if(str==form1.hfdelbook.value)
                {
                     ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID=0&Object=delbook&id="+id,'dialogWidth:600px;dialogHeight:410px');
                }
                else if(str==form1.hfdelchapter.value)
                {
                     ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=delchapter&id="+id,'dialogWidth:600px;dialogHeight:410px');
                }
                if(ret == "true")
	            {
		            form1.RefreshGrid.value = ret;
		            form1.submit();
		            form1.RefreshGrid.value = "";
	            } 
	        }
          }
            
          function SelectBook()
          {
                var selectBooked = window.showModalDialog('../Common/SelectBook.aspx', 
                    '', 'help:no; status:no; dialogWidth:320px;dialogHeight:600px;scroll:no;');

                if(! selectBooked)
                {
                    return;
                }
                
                document.getElementById("hfBookID").value = selectBooked.split('|')[0];
                document.getElementById("txtBookName").value = selectBooked.split('|')[1];
          }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="query">
                查询方式
                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    <asp:ListItem Value="0">根据现有教材查询</asp:ListItem>
                    <asp:ListItem Value="1">根据所记录教材名称查询</asp:ListItem>
                </asp:DropDownList>
                &nbsp;教材名称
                <asp:TextBox ID="txtBookName" runat="server" ReadOnly="true"></asp:TextBox>
                <img id="ImgSelectBook" runat="server" style="cursor: hand;" name="ImgSelectBook"
                    onclick="SelectBook();" src="../Common/Image/search.gif" alt="选择现有教材" border="0" />
                更改人
                <asp:TextBox ID="txtPerson" runat="server"></asp:TextBox><br />
                更改时间
                <uc1:Date ID="smalldate" runat="server" />
                ～<uc1:Date ID="bigdate" runat="server" />
                更改对象
                <asp:DropDownList ID="ddlUpdateObject" runat="server" />
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
            </div>
            <div>
                <ComponentArt:Grid ID="dgUpdate" runat="server" Width="780" AllowPaging="true" PageSize="20">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid1_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="bookChapterUpdateId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="bookUpdateId" Visible="false" />
                                <ComponentArt:GridColumn DataField="BookId" Visible="false" />
                                <ComponentArt:GridColumn DataField="ChapterId" Visible="false" />
                                <ComponentArt:GridColumn DataField="UpdateObject" Visible="false" />
                                <ComponentArt:GridColumn DataField="BookNameBak" HeadingText="教材名称" />
                                <ComponentArt:GridColumn DataField="ChapterName" HeadingText="更改对象" />
                                <ComponentArt:GridColumn DataField="updatePerson" HeadingText="更改人" />
                                <ComponentArt:GridColumn DataField="updateDate" FormatString="yyyy-MM-dd" HeadingText="更改日期" />
                                <ComponentArt:GridColumn DataField="updateCause" HeadingText="更改原因" />
                                <ComponentArt:GridColumn DataField="updateContent" HeadingText="更改内容" />
                                <%-- <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
--%>
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <%-- <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="CTEdit">
                                    <a onclick="ManageChapterUpdate('##DataItem.getMember('BookId').get_value()##','##DataItem.getMember('bookUpdateId').get_value()##','##DataItem.getMember('ChapterId').get_value()##','##DataItem.getMember('UpdateObject').get_value()##')"
                                        href="#"><b>修改</b></a>
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>--%>
                </ComponentArt:Grid>
                &nbsp;
            </div>
        </div>
        <ComponentArt:Menu ID="ContextMenu" ContextMenu="Custom" runat="server" EnableViewState="true">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem Text="查看" look-lefticonurl="view.gif" disabledLook-LeftIconUrl="view_disabled.gif" />
                <ComponentArt:MenuItem Text="修改" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                  <ComponentArt:MenuItem Text="删除" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
            </Items>
        </ComponentArt:Menu>
        <asp:HiddenField ID="hfBookID" runat="server" Value="0" />
        <input type="hidden" name="hfbookinfo" value='<%=PrjPub.BOOKUPDATEOBJECT_BOOKINFO%>' />
        <input type="hidden" name="hfbookcover" value='<%=PrjPub.BOOKUPDATEOBJECT_BOOKCOVER %>' />
        <input type="hidden" name="hfinsertchapterinfo" value='<%=PrjPub.BOOKUPDATEOBJECT_INSERTCHAPTERINFO %>' />
        <input type="hidden" name="hfupdatechapterinfo" value='<%=PrjPub.BOOKUPDATEOBJECT_UPDATECHAPTERINFO %>' />
        <input type="hidden" name="hfchaptercontent" value='<%=PrjPub.BOOKUPDATEOBJECT_CHAPTERCONTENT %>' />
        <input type="hidden" name="hfdelbook" value='<%=PrjPub.BOOKUPDATEOBJECT_DELBOOK %>' />
        <input type="hidden" name="hfdelchapter" value='<%=PrjPub.BOOKUPDATEOBJECT_DELCHAPTER %>' />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HasDeleteRight" runat="server" />
        <input type="hidden" name="RefreshGrid" />
        <input type="hidden" name="DeleteID" />
        <asp:HiddenField ID="hfIsWuhan" runat="server" />
    </form>
</body>
</html>
