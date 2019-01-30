<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssistBookUpdateInfo.aspx.cs" Inherits="RailExamWebApp.AssistBook.AssistBookUpdateInfo" %>
<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>辅导教材更新记录</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
          function Grid1_onContextMenu(sender, eventArgs)
        {
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "修改");
            var flagUpdate=document.getElementById("HfUpdateRight").value;
                  
	        if(flagUpdate=="True")
            {                     
               menuItemEdit.set_enabled(true); 
            }
            else
            {
               menuItemEdit.set_enabled(false); 
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
				　ManageChapterUpdate('ReadOnly',contextDataNode.getMember('AssistBookId').get_value(),contextDataNode.getMember('AssistBookUpdateId').get_value(),contextDataNode.getMember('ChapterId').get_value(),contextDataNode.getMember('UpdateObject').get_value());　				    
				    break;
				
				case '修改':
				　ManageChapterUpdate('Edit',contextDataNode.getMember('AssistBookId').get_value(),contextDataNode.getMember('AssistBookUpdateId').get_value(),contextDataNode.getMember('ChapterId').get_value(),contextDataNode.getMember('UpdateObject').get_value());　
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
            if(str==form1.hfbookinfo.value)
            {
                ret = window.open("AssistBookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=bookinfo&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
          }
            else if(str==form1.hfbookcover.value)
           {
                ret = window.open("AssistBookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=bookcover&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
           }
           else if(str==form1.hfupdatechapterinfo.value)
           {
                 ret = window.open("AssistBookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=updatechapterinfo&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
           } 
          else if(str==form1.hfinsertchapterinfo.value)
           {
                 ret = window.open("AssistBookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID=0&Object=insertchapterinfo&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
           }     
           else if(str==form1.hfchaptercontent.value)
           {
                 ret = window.open("AssistBookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=chaptercontent&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
           }
           else if(str==form1.hfdelbook.value)
           {
                 ret = window.open("AssistBookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID=0&Object=delbook&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
           }
           else if(str==form1.hfdelchapter.value)
           {
                 ret = window.open("AssistBookChapterUpdate.aspx?Mode=" + type + "&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=delchapter&id="+id,"BookChapterUpdate","Width=600px; Height=360px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
           }  
            if(ret == "true")
	        {
		        form1.Refresh.value = ret;
		        form1.submit();
		        form1.Refresh.value = "";
	        } 
	        ret.focus(); 
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
                    <asp:ListItem Value="0">根据现有辅导教材查询</asp:ListItem>
                    <asp:ListItem Value="1">根据所记录辅导教材名称查询</asp:ListItem>
                </asp:DropDownList>
                &nbsp;辅导教材名称
                <asp:TextBox ID="txtBookName" runat="server" ReadOnly="true"></asp:TextBox>
                <img id="ImgSelectBook" runat="server" style="cursor: hand;" name="ImgSelectBook"
                    onclick="SelectBook();" src="../Common/Image/search.gif" alt="选择现有辅导教材" border="0" />
                更改人
                <asp:TextBox ID="txtPerson" runat="server"></asp:TextBox><br />
                更改时间
                <uc1:Date ID="smalldate" runat="server" />
                ～<uc1:Date ID="bigdate" runat="server" />
                更改对象
                <asp:DropDownList ID="ddlUpdateObject" runat="server" />
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="dgUpdate" runat="server" AllowPaging="true" PageSize="20">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid1_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="bookChapterUpdateId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="AssistBookUpdateId" Visible="false" />
                                <ComponentArt:GridColumn DataField="AssistBookId" Visible="false" />
                                <ComponentArt:GridColumn DataField="ChapterId" Visible="false" />
                                <ComponentArt:GridColumn DataField="UpdateObject" Visible="false" />
                                <ComponentArt:GridColumn DataField="BookNameBak" HeadingText="辅导教材名称" />
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
            </Items>
        </ComponentArt:Menu>
        <asp:HiddenField ID="hfBookID" runat="server" Value="0" />
        <input type="hidden" name="hfbookinfo" value='<%=PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKINFO%>' />
        <input type="hidden" name="hfbookcover" value='<%=PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKCOVER %>' />
        <input type="hidden" name="hfinsertchapterinfo" value='<%=PrjPub.ASSISTBOOKUPDATEOBJECT_INSERTCHAPTERINFO %>' />
        <input type="hidden" name="hfupdatechapterinfo" value='<%=PrjPub.ASSISTBOOKUPDATEOBJECT_UPDATECHAPTERINFO %>' />
        <input type="hidden" name="hfchaptercontent" value='<%=PrjPub.ASSISTBOOKUPDATEOBJECT_CHAPTERCONTENT %>' />
        <input type="hidden" name="hfdelbook" value='<%=PrjPub.ASSISTBOOKUPDATEOBJECT_DELBOOK %>' />
        <input type="hidden" name="hfdelchapter" value='<%=PrjPub.ASSISTBOOKUPDATEOBJECT_DELCHAPTER %>' />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <input type="hidden" name="RefreshGrid" /> 
    </form>
</body>
</html>
