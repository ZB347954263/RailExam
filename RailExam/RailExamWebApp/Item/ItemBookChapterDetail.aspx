<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ItemBookChapterDetail.aspx.cs"
    Inherits="RailExamWebApp.Item.ItemBookChapterDetail" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Import Namespace="DSunSoft.Web.Global" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教材章节详细信息</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
       function GetNamePath(node,str)
       {
            if(node.get_parentNode())
            {
                var name = node.get_parentNode().get_text() ;
                if(name.length>15)
                {
                    name = name.substring(0,15) + "...";
                }
                str = name + " >> " + str;
                return GetNamePath(node.get_parentNode(),str);
            }
            return str;
       }
                
        //按钮对象ID回去对象 
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
        
        function editBtnClientClick()
        {            
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "编辑";
            
            return true;
        }
        
        function deleteBtnClientClick()
        {
            var theNode = window.parent.tvBookChapter.get_selectedNode();
            if(theNode.get_nodes().get_length() > 0)
            {
                alert("该章节有下级节点，不能删除！");
               return false; 
            }
            
            if(!confirm("删除此章节将会删除该章节下试题，您确定要删除此章节吗？"))
            {
                return false;
            }
            
            return true;
        }
        
        function newBtnClientClick()
        {            
             var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
             theItem.value = "新增下级";
             return true;
        }
        
        function updateBtnClientClick()
        {
             if(validateUserInputEdit() == false)
            {
                return false;
            }
             var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            theItem.value = "";
            return true;
        }
        
        function insertBtnClientClick()
        {
             if(validateUserInputInsert() == false)
            {
                return false;
            }
           var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            return true;
        }
        
        function cancelBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }
       
              //插入时，验证用户输入数据
       function validateUserInputInsert()
       {
             if($F("fvBookChapter_txtChapterNameInsert") && $F("fvBookChapter_txtChapterNameInsert").value.length == 0)
            {
                alert("章节名称不能为空！");
               return false; 
            }         
            
            
            return true;
       }   
       
       //编辑时，验证用户输入数据 
       function validateUserInputEdit()
       {
            if($F("fvBookChapter_txtChapterNameEdit") && $F("fvBookChapter_txtChapterNameEdit").value.length == 0)
            {
                alert("章节名称不能为空！");
               return false; 
            }
                                    
            
             return true;
       }  
       
      
      function  ImportItem()
      {
         var theNode = window.parent.tvBookChapter.get_selectedNode();
         var bookid= theNode.get_value();
         var chapterid = theNode.get_id();
         if(chapterid==0)
         {
            chapterid=-1;
         }
         var ret = showCommonDialog("/RailExamBao/Item/ItemInput.aspx?bid=" +bookid + "&cid=" + chapterid,'dialogWidth:850px;dialogHeight:650px;');
         form1.RefreshGrid.value = "true";
         form1.submit();
         form1.RefreshGrid.value = "";
      }

       //修改试题 
        function editItem(id,orgid)
        {  
            var editWindow = window.open('ItemDetail.aspx?mode=edit&&id=' + id,
                'ItemDetail',' top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
            editWindow.focus(); 
        }
        
       //删除试题 
        function deleteItem(id,orgid)
        {
            itemsGrid.select(itemsGrid.getItemFromClientId("0 " + id), false);
            if(! confirm("您确定要删除该记录吗？"))
            {
                return;
            }
             form1.Refresh.value = id;
             form1.submit();
             form1.Refresh.value = "";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:FormView ID="fvBookChapter" runat="server" BorderStyle="Solid" GridLines="Both"
                        HorizontalAlign="Left" DataSourceID="dsBookChapter" Width="540px" DataKeyNames="ChapterId,BookId"
                        OnItemDeleted="fvBookChapter_ItemDeleted" OnItemInserted="fvBookChapter_ItemInserted"
                        OnItemUpdated="fvBookChapter_ItemUpdated" OnItemDeleting="fvBookChapter_ItemDeleting">
                        <EditItemTemplate>
                            <div style="display: none;">
                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="更新" OnClientClick="return updateBtnClientClick();">
                                </asp:LinkButton>
                                <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="取消" OnClientClick="return cancelBtnClientClick();">
                                </asp:LinkButton>
                            </div>
                            <table class="contentTable">
                                <tr>
                                    <td style="width: 15%">
                                        章节名称<span class="require">*</span>
                                    </td>
                                    <td colspan="2" style="width: 70%">
                                        <asp:TextBox ID="txtChapterNameEdit" runat="server" MaxLength="60" Text='<%# Bind("ChapterName") %>'>
                                        </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkCannotSeeAnswer" runat="server" Text="所属试题答案不可见" Checked='<%# Bind("IsCannotSeeAnswer") %>'>
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfBookID" runat="server" Value='<%# Bind("BookId") %>' />
                            <asp:HiddenField ID="hfParentID" runat="server" Value='<%# Bind("ParentId") %>' />
                            <asp:HiddenField ID="hfLevelNum" runat="server" Value='<%# Bind("LevelNum") %>' />
                            <asp:HiddenField ID="hfIDPath" runat="server" Value='<%# Bind("IdPath") %>' />
                            <asp:HiddenField ID="hfOrderIndex" runat="server" Value='<%# Bind("OrderIndex") %>' />
                            <asp:HiddenField ID="hfIsEdit" runat="server" Value='<%# Bind("IsEdit") %>' />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <div style="display: none;">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="插入" OnClientClick="return insertBtnClientClick();">
                                </asp:LinkButton>
                                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="取消" OnClientClick="return cancelBtnClientClick();">
                                </asp:LinkButton>
                            </div>
                            <table class="contentTable">
                                <tr>
                                    <td style="width: 15%">
                                        章节名称<span class="require">*</span>
                                    </td>
                                    <td colspan="2" style="width: 70%">
                                        <asp:TextBox ID="txtChapterNameInsert" runat="server" MaxLength="60" Text='<%# Bind("ChapterName") %>'>
                                        </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkCannotSeeAnswer" runat="server" Text="所属试题答案不可见" Checked='<%# Bind("IsCannotSeeAnswer") %>'>
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfBookID" runat="server" Value='<%# Bind("BookId") %>' />
                            <asp:HiddenField ID="hfParentID" runat="server" Value='<%# Bind("ParentId") %>' />
                            <asp:HiddenField ID="hfIsEdit" runat="server" Value='<%# Bind("IsEdit") %>' />
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <div style="display: none;">
                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="编辑" OnClientClick="return editBtnClientClick();">
                                </asp:LinkButton>
                                <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="删除" OnClientClick="return deleteBtnClientClick();">
                                </asp:LinkButton>
                                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                                    Text="新建" OnClientClick="return newBtnClientClick();">
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnAdd" runat="server" Text='&lt;img border=0 src="../Common/Image/ImportItem.gif"  alt="" /&gt;'
                                    OnClientClick="ImportItem()" /><%--Visible='<%# !Eval("ChapterId").ToString().Equals("0") %>' --%>
                            </div>
                            <table class="contentTable">
                                <tr>
                                    <td style="width: 15%;">
                                        章节名称
                                    </td>
                                    <td colspan="2" style="width: 70%;">
                                        <asp:Label ID="TypeNameLabel" runat="server" Text='<%# Bind("ChapterName") %>'></asp:Label></td>
                                    <td>
                                        <asp:CheckBox ID="chk" runat="server" Enabled="false" Text="所属试题答案不可见" Checked='<%# Bind("IsCannotSeeAnswer") %>'>
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="dsBookChapter" runat="server" DeleteMethod="DeleteBookChapter"
                        InsertMethod="AddBookChapter" SelectMethod="GetBookChapterInfo" TypeName="RailExam.BLL.BookChapterBLL"
                        UpdateMethod="UpdateBookChapter" DataObjectTypeName="RailExam.Model.BookChapter"
                        EnableViewState="False">
                        <DeleteParameters>
                            <asp:QueryStringParameter DefaultValue="1" Name="ChapterId" QueryStringField="id"
                                Type="Int32" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="1" Name="ChapterId" QueryStringField="id"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="grid">
                        <ComponentArt:Grid ID="itemsGrid" runat="server" RunningMode="Server" ManualPaging="true"
                            Debug="false" PageSize="18" Width="540">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="ItemId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ItemId" Visible="false" />
                                        <%--<ComponentArt:GridColumn DataField="OrganizationName" HeadingText="组织机构" />--%>
                                        <ComponentArt:GridColumn DataField="TypeName" HeadingText="题型" Width="40" />
                                        <ComponentArt:GridColumn DataField="DifficultyName" HeadingText="难度" Width="40" />
                                        <%--<ComponentArt:GridColumn DataField="Score" HeadingText="分值" />--%>
                                        <ComponentArt:GridColumn DataField="Content" HeadingText="内容" Align="left" Width="250" />
                                        <ComponentArt:GridColumn DataField="Score" HeadingText="分数" Width="40" Visible="false" />
                                        <ComponentArt:GridColumn DataField="StatusName" HeadingText="状态" Width="40" />
                                        <ComponentArt:GridColumn DataField="OrganizationId" HeadingText="组织机构" Visible="false" />
                                        <%--                                        <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作"
                                            Width="60" />
--%>
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="CTEdit">
                                    <a onclick="editItem(##DataItem.getMember('ItemId').get_value()##,##DataItem.getMember('OrganizationId').get_value()##);"
                                        href="#">
                                        <img alt="编辑" border="0" src="../Common/Image/edit_col_edit.gif" /></a> <a onclick="deleteItem(##DataItem.getMember('ItemId').get_value()##,##DataItem.getMember('OrganizationId').get_value()##);"
                                            href="#">
                                            <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a>
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                        </ComponentArt:Grid>
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfChapterID" runat="server" />
        <input type="hidden" id="AddButton" onclick="return AddRecord();" />
        <input type="hidden" name="DeleteBookChapterUpdateID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshGrid" />
        <asp:HiddenField ID="hfInsert" runat="server" />
    </form>
</body>
</html>
