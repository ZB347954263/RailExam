<%@ Page Language="C#" AutoEventWireup="True" Codebehind="BookChapterDetail.aspx.cs"
    Inherits="RailExamWebApp.Book.BookChapterDetail" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Import Namespace="DSunSoft.Web.Global" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教材章节详细信息</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function Grid1_onContextMenu(sender, eventArgs)
        {
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "修改");
         
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
            
            var isWuhan = document.getElementById("hfIsWuhan").value;
            if(isWuhan == "True")
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
                if(ret == "true")
                {
                    form1.RefreshGrid.value = ret;
                    form1.submit();
                    form1.RefreshGrid.value = "";
                }
            }
 
        }
       
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
        
        function eWebEditorPopUp()
        { 
            var theNode = window.parent.tvBookChapter.get_selectedNode();
            var name = theNode.get_text();
            if(name.length>15)
            {
                name = name.substring(0,15) + "...";
            }
            var namepath = GetNamePath(theNode,name);
            var bookid= theNode.get_value();
            var chapterid = theNode.get_id();
             var search = window.location.search;
             var str= search.substring(search.indexOf("Mode=")+5);
            
             var isWuhan = document.getElementById("hfIsWuhan").value;
            if(isWuhan == "True")
            {
                if(str=='Edit'&&form1.UpdateRecord.value =="1")
                {            
                     var re = window.open("../ewebeditor/asp/ShowChapterEditor.asp?BookID="+bookid +"&ChapterID="+chapterid+"&NamePath="+escape(namepath),'ShowChapterEditor','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
                       re.focus();            
                }
                else
                {
                     var re = window.open("../ewebeditor/asp/ShowChapterEditor.asp?BookID="+bookid +"&ChapterID="+chapterid+"&Type=Add&NamePath="+escape(namepath),'ShowChapterEditor','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
                      re.focus();  
                }
            }
            else
            {
                if(str=='Edit'&&form1.UpdateRecord.value =="1")
                {            
                     ret = showCommonDialogFull("../ewebeditor/asp/ShowChapterEditor.asp?BookID="+bookid +"&ChapterID="+chapterid+"&NamePath="+escape(namepath),"dialogWidth:" + window.screen.width + "px;dialogHeight:" + window.screen.height + "px;");
                }
                else
                {
                     ret = showCommonDialogFull("../ewebeditor/asp/ShowChapterEditor.asp?BookID="+bookid +"&ChapterID="+chapterid+"&Type=Add&NamePath="+escape(namepath),"dialogWidth:" + window.screen.width + "px;dialogHeight:" + window.screen.height + "px;");
                }
                if(ret == "true")
                {
                    form1.Refresh.value = ret;
                    form1.submit();
                    form1.Refresh.value = "";
                }
            }
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
            if(!confirm("您确定要删除此章节吗？"))
            {
                return false;
            }
            
            var theNode = window.parent.tvBookChapter.get_selectedNode();
            var bookid= theNode.get_value();
            var chapterid = theNode.get_id();
             var search = window.location.search;
             var str= search.substring(search.indexOf("Mode=")+5);
            if(str=='Edit'&&form1.UpdateRecord.value =="1")
            {            
                var ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=Insert&BookID="+ bookid + "&ChapterID="+chapterid+"&Object=delchapter",'dialogWidth:600px;dialogHeight:400px;');
                if(ret == "true")
                {
                    return true;
                } 
               else
               {
                    return false;
               } 
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
            
            var theNode = window.parent.tvBookChapter.get_selectedNode();
            var bookid= theNode.get_value();
            var chapterid = theNode.get_id();
 
             var search = window.location.search;
             var str= search.substring(search.indexOf("Mode=")+5);
            if(str=='Edit'&&form1.UpdateRecord.value =="1")
            {
            	var ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=Insert&BookID=" + bookid + "&ChapterID=" + chapterid + "&Object=updatechapterinfo", 'dialogWidth:600px;dialogHeight:400px;');
                if(ret != "true")
                {
                     return false;
                }
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
        	var theNode;
            if(window.parent.tvBookChapter.get_selectedNode())
            {
                 theNode= window.parent.tvBookChapter.get_selectedNode();
            }
            else
            {
                theNode = window.parent.tvBookChapter.get_nodes().getNode(0);
            }
           var bookid= theNode.get_value();
           var chapterid = theNode.get_id();
 
            var search = window.location.search;
            var str= search.substring(search.indexOf("Mode=")+5);
            //alert($F("fvBookChapter_txtChapterNameInsert").value);
           // alert(escape($F("fvBookChapter_txtChapterNameInsert").value));
            if(str=='Edit'&&form1.UpdateRecord.value =="1")
            {
            	var ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?Mode=Insert&BookID=" + bookid + "&ChapterID=0&Object=insertchapterinfo&newchaptername=" + escape($F("fvBookChapter_txtChapterNameInsert").value), 'dialogWidth:600px;dialogHeight:410px;');
                if(ret != "true")
                {
                     return false;
                }
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
            
             if($F("fvBookChapter_txtDescriptionInsert") && $F("fvBookChapter_txtDescriptionInsert").value.length > 200)
            {
                alert("描述不能超过200个字！");
               return false; 
            }     
            
            if($F("fvBookChapter_txtMemoInsert") && $F("fvBookChapter_txtMemoInsert").value.length >50)
            {
                alert("备注不能超过50个字！");
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
                           
            if($F("fvBookChapter_txtDescriptionEdit") && $F("fvBookChapter_txtDescriptionEdit").value.length > 200)
            {
                alert("描述不能超过200个字！");
               return false; 
            }    
                     
            if($F("fvBookChapter_txtMemoEdit") && $F("fvBookChapter_txtMemoEdit").value.length >50)
            {
                alert("备注不能超过50个字！");
               return false; 
            }               
            
             return true;
       }  
       
       function ItemEnabled(id)
       {
        	var ret = showCommonDialog("/RailExamBao/Book/ItemEnabled.aspx?ChapterID=" + id, 'dialogWidth:600px;dialogHeight:400px;');
            if(ret != "true")
            {
                 return false;
            }
            return true; 
       }
       
       var btnIds = new Array("fvBookChapter_NewButton", "fvBookChapter_EditButton", "fvBookChapter_DeleteButton",
    "fvBookChapter_UpdateButton", "fvBookChapter_UpdateCancelButton",
    "fvBookChapter_InsertButton", "fvBookChapter_InsertCancelButton","fvBookChapter_NewButtonBrother","fvBookChapter_btnAdd");

       
       function setImageBtnVisiblityUpdate()
       {
           window.parent.document.getElementById(btnIds[0]).style.display = "none";
           window.parent.document.getElementById(btnIds[1]).style.display = "none";
           window.parent.document.getElementById(btnIds[2]).style.display = "none";
           window.parent.document.getElementById(btnIds[3]).style.display = "";
           window.parent.document.getElementById(btnIds[4]).style.display = "";
           window.parent.document.getElementById(btnIds[5]).style.display = "none";
           window.parent.document.getElementById(btnIds[6]).style.display = "none";
           window.parent.document.getElementById(btnIds[7]).style.display = "none";  
           window.parent.document.getElementById(btnIds[8]).style.display = "none"; 
       }
       
       function setImageBtnVisiblityInsert()
       {
           window.parent.document.getElementById(btnIds[0]).style.display = "none";
            window.parent.document.getElementById(btnIds[1]).style.display = "none";
            window.parent.document.getElementById(btnIds[2]).style.display = "none";
           window.parent.document.getElementById(btnIds[3]).style.display = "none";
           window.parent.document.getElementById(btnIds[4]).style.display = "none";
           window.parent.document.getElementById(btnIds[5]).style.display = "";
            window.parent.document.getElementById(btnIds[6]).style.display = "";
            window.parent.document.getElementById(btnIds[7]).style.display = "none"; 
           window.parent.document.getElementById(btnIds[8]).style.display = "none";  
       }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:FormView ID="fvBookChapter" runat="server" BorderStyle="Solid" GridLines="Both"
                        HorizontalAlign="Left" DataSourceID="dsBookChapter" Width="550px" DataKeyNames="ChapterId,BookId"
                        OnItemDeleted="fvBookChapter_ItemDeleted" OnItemInserted="fvBookChapter_ItemInserted"
                        OnItemUpdated="fvBookChapter_ItemUpdated" OnItemInserting="fvBookChapter_ItemInserting" OnItemUpdating="fvBookChapter_ItemUpdating">
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
                                        名称<span class="require">*</span>
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
                                <tr style="display: none;">
                                    <td style="width: 15%">
                                        属性<span class="require">*</span>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkIsMotherItem" runat="server" Text="复习要点" Checked='<%# Bind("IsMotherItem") %>'>
                                        </asp:CheckBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        参考规章
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtRegularEdit" runat="server" MaxLength="50" Text='<%# Bind("ReferenceRegulation") %>'>
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        描述
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="tbxDescriptionEdit" TextMode="MultiLine" Rows="5" Columns="43" runat="server"
                                            Text='<%# Bind("Description") %>'>
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最后更新人</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtLastPerson" runat="server" Text='<%# Bind("LastPerson") %>'>
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最后更新时间</td>
                                    <td colspan="3">
                                        <asp:Label ID="txtLastDate" runat="server" Text='<%# Bind("LastDate") %>'>
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        备注
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtMemoEdit" MaxLength="50" runat="server" Text='<%# Bind("Memo") %>'>
                                        </asp:TextBox>
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
                                <tr style="display: none;">
                                    <td style="width: 15%">
                                        属性<span class="require">*</span>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkIsMotherItem" runat="server" Text="复习要点" Checked='<%# Bind("IsMotherItem") %>'>
                                        </asp:CheckBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        参考规章
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtRegularInsert" runat="server" MaxLength="50" Text='<%# Bind("ReferenceRegulation") %>'>
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        描述
                                    </td>
                                    <td colspan="3" style="width: 35%">
                                        <asp:TextBox ID="txtDescriptionInsert" TextMode="MultiLine" Rows="3" Columns="43"
                                            runat="server" Text='<%# Bind("Description") %>'>
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最后更新人</td>
                                    <td colspan="3">
                                        <asp:Label ID="lblLastPerson" runat="server" Text='<%# SessionSet.EmployeeName %>'>
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最后更新时间</td>
                                    <td colspan="3">
                                        <asp:Label ID="txtLastDate1" runat="server" Text='<%# DateTime.Today.ToString("yyyy-MM-dd") %>'>
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        备注
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtMemoInsert" MaxLength="50" runat="server" Text='<%# Bind("Memo") %>'>
                                        </asp:TextBox>
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
                                <asp:LinkButton ID="btnAdd" runat="server" Text='&lt;img border=0 src="../Common/Image/chat.gif"  alt="" /&gt;'
                                    OnClientClick="eWebEditorPopUp()" Visible='<%# !Eval("ChapterId").ToString().Equals("0") %>' />
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
                                <tr style="display: none;">
                                    <td style="width: 15%">
                                        属性<span class="require">*</span>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkIsMotherItem" Enabled="false" runat="server" Text="复习要点" Checked='<%# Bind("IsMotherItem") %>'>
                                        </asp:CheckBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        参考规章
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lbl" runat="server" Text='<%# Bind("ReferenceRegulation") %>'>
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 30;">
                                    <td>
                                        网址
                                        <td align="left" colspan="3">
                                            <asp:HyperLink ID="hUrl" runat="server" Target="_blank" Text='<%# Bind("Url") %>'
                                                NavigateUrl='<%# Bind("Url") %>'></asp:HyperLink>
                                        </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        描述
                                    </td>
                                    <td colspan="3" style="width: 35%">
                                        <asp:TextBox ID="DescriptionTextBox11" Rows="3" TextMode="MultiLine" Enabled="false"
                                            Columns="43" runat="server" Text='<%# Bind("Description") %>'>
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最后更新人</td>
                                    <td colspan="3">
                                        <asp:Label ID="lblLastPerson1" runat="server" Text='<%# Bind("LastPerson") %>'>
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最后更新时间</td>
                                    <td colspan="3">
                                        <asp:Label ID="lblLastDate1" runat="server" Text='<%# ((DateTime)Eval("LastDate")).ToString("yyyy-MM-dd") %>'>
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        备注
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="MemoTextBox11" Enabled="false" TextMode="MultiLine" Rows="3" Columns="43"
                                            runat="server" Text='<%# Bind("Memo") %>'>
                                        </asp:TextBox>
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
                        <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsBookChapterUpdate"
                            AllowPaging="true" PageSize="9">
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
                                        <ComponentArt:GridColumn DataField="BookNameBak" HeadingText="教材名称" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ChapterName" HeadingText="更改对象" Width="220" />
                                        <ComponentArt:GridColumn DataField="updatePerson" HeadingText="更改人" Width="100" />
                                        <ComponentArt:GridColumn DataField="updateDate" FormatString="yyyy-MM-dd" HeadingText="更改日期"
                                            Width="100" />
                                        <%--                                        <ComponentArt:GridColumn DataField="updateCause" HeadingText="更改原因" />
                                        <ComponentArt:GridColumn DataField="updateContent" HeadingText="更改内容" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
--%>
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <%--                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="CTEdit">
                                    <a onclick="ManageChapterUpdate('##DataItem.getMember('BookId').get_value()##','##DataItem.getMember('bookUpdateId').get_value()##','##DataItem.getMember('ChapterId').get_value()##','##DataItem.getMember('UpdateObject').get_value()##')"
                                        href="#"><b>修改</b></a>
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>--%>
                        </ComponentArt:Grid>
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
                    <asp:ObjectDataSource ID="odsBookChapterUpdate" runat="server" SelectMethod="GetBookUpdateByChapterID"
                        TypeName="RailExam.BLL.BookUpdateBLL">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="chapterID" QueryStringField="id" Type="Int32" />
                            <asp:QueryStringParameter Name="bookID" QueryStringField="BookID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <%--<asp:HiddenField ID="hfParentId" runat="server" />
        <asp:HiddenField ID="hfLevelNum" runat="server" />
        <asp:HiddenField ID="hfIdPath" runat="server" />
        <asp:HiddenField ID="hfOrderIndex" runat="server" />
        <asp:HiddenField ID="hfBookID" runat="server" />--%>
        <asp:HiddenField ID="hfChapterID" runat="server" />
        <input type="hidden" id="AddButton" onclick="return AddRecord();" />
        <input type="hidden" name="DeleteBookChapterUpdateID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshGrid" />
        <asp:HiddenField ID="hfInsert" runat="server" />
        <input type="hidden" name="hfbookinfo" value='<%=PrjPub.BOOKUPDATEOBJECT_BOOKINFO%>' />
        <input type="hidden" name="hfbookcover" value='<%=PrjPub.BOOKUPDATEOBJECT_BOOKCOVER %>' />
        <input type="hidden" name="hfinsertchapterinfo" value='<%=PrjPub.BOOKUPDATEOBJECT_INSERTCHAPTERINFO %>' />
        <input type="hidden" name="hfupdatechapterinfo" value='<%=PrjPub.BOOKUPDATEOBJECT_UPDATECHAPTERINFO %>' />
        <input type="hidden" name="hfchaptercontent" value='<%=PrjPub.BOOKUPDATEOBJECT_CHAPTERCONTENT %>' />
        <input type="hidden" name="UpdateRecord" value='<%=PrjPub.FillUpdateRecord %>' />
        <asp:HiddenField ID="hfIsWuhan" runat="server" />
    </form>
</body>
</html>
