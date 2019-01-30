<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KnowledgeDetail.aspx.cs" Inherits="RailExamWebApp.Knowledge.KnowledgeDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>知识体系详细信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />  
    <script type="text/javascript">  
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
              if (confirm("您确定要删除此记录吗？"))
             {
                    var  node =window.parent.tvKnowledge.get_selectedNode();                  
		            form1.DeleteID.value = node.get_id();
		            form1.submit();
		            form1.DeleteID.value = ""; 
             }  
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
             if($F("fvKnowledge_txtKnowledgeNameInsert") && $F("fvKnowledge_txtKnowledgeNameInsert").value.length == 0)
            {
                alert("知识体系名称不能为空！");
               return false; 
            }         
            
             if($F("fvKnowledge_txtDescriptionInsert") && $F("fvKnowledge_txtDescriptionInsert").value.length > 200)
            {
                alert("描述不能超过200个字！");
               return false; 
            }     
            
            if($F("fvKnowledge_txtMemoInsert") && $F("fvKnowledge_txtMemoInsert").value.length >50)
            {
                alert("备注不能超过50个字！");
               return false; 
            }               
            
            return true;
       }   
       
       //编辑时，验证用户输入数据 
       function validateUserInputEdit()
       {
            if($F("fvKnowledge_txtKnowledgeNameEdit") && $F("fvKnowledge_txtKnowledgeNameEdit").value.length == 0)
            {
                alert("知识体系名称不能为空！");
               return false; 
            }
                           
            if($F("fvKnowledge_txtDescriptionEdit") && $F("fvKnowledge_txtDescriptionEdit").value.length > 200)
            {
                alert("描述不能超过200个字！");
               return false; 
            }    
                     
            if($F("fvKnowledge_txtMemoEdit") && $F("fvKnowledge_txtMemoEdit").value.length >50)
            {
                alert("备注不能超过50个字！");
               return false; 
            }               
            
             return true;
       }
       
       var btnIds = new Array("fvKnowledge_NewButton", "fvKnowledge_EditButton", "fvKnowledge_DeleteButton",
            "fvKnowledge_UpdateButton", "fvKnowledge_UpdateCancelButton",
            "fvKnowledge_InsertButton", "fvKnowledge_InsertCancelButton","fvKnowledge_NewButtonBrother");
       
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
       }
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:FormView ID="fvKnowledge" runat="server" DataSourceID="odsKnowledgeDetail" DataKeyNames="KnowledgeId"
            OnItemUpdated="fvKnowledge_ItemUpdated"  OnItemUpdating = "fvKnowledge_ItemUpdating" OnItemInserting = "fvKnowledge_ItemInserting"
            OnItemInserted="fvKnowledge_ItemInserted">
            <EditItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">知识体系名称<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtKnowledgeNameEdit" runat="server" Text='<%# Bind("KnowledgeName") %>'  MaxLength="20"  Width="50%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>描述</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>属性</td>
                        <td>
                            <asp:CheckBox ID="chkIsTemplateEdit" runat="server" Checked='<%# Bind("IsTemplate") %>' Text="一专多能知识体系"></asp:CheckBox>&nbsp;&nbsp;
                             <asp:CheckBox ID="chkIsPromotionEdit" runat="server" Checked='<%# Bind("IsPromotion") %>' Text="一专多能级联类别"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>备注</td>
                        <td>
                            <asp:TextBox ID="txtMemoEdit" runat="server" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>'/>
                <asp:HiddenField ID="hfLevelNum" runat="server" Value='<%# Bind("LevelNum") %>'/>
                <asp:HiddenField ID="hfIdPath" runat="server" Value='<%# Bind("IdPath") %>'/>
                <asp:HiddenField ID="hfOrderIndex" runat="server" Value='<%# Bind("OrderIndex") %>'/>
                <div style="display: none;">
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                        Text="更新" OnClientClick="return updateBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="取消" OnClientClick="return cancelBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">知识体系名称<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtKnowledgeNameInsert" runat="server" Text='<%# Bind("KnowledgeName") %>' MaxLength="20"  Width="50%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="“知识体系名称”不能为空！"
                                ToolTip="“知识体系名称”不能为空！" EnableClientScript="true" ControlToValidate="txtKnowledgeNameInsert"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>描述</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>属性</td>
                        <td>
                            <asp:CheckBox ID="chkIsTemplateInsert" runat="server" Checked='<%# Bind("IsTemplate") %>' Text="一专多能知识体系"></asp:CheckBox>&nbsp;&nbsp;
                             <asp:CheckBox ID="chkIsPromotionInsert" runat="server" Checked='<%# Bind("IsPromotion") %>' Text="一专多能级联类别"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>备注</td>
                        <td>
                            <asp:TextBox ID="txtMemoInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>'/>
                <div style="display: none;">
                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                        Text="插入" OnClientClick="return insertBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="取消" OnClientClick="return cancelBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </InsertItemTemplate>
            <ItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">知识体系名称</td>
                        <td>
                            <asp:Label ID="lblKnowledgeName" runat="server" Text='<%# Eval("KnowledgeName") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>描述</td>
                        <td style="white-space: normal;">
                            <%# Eval("Description") %></td>
                    </tr>
                    <tr>
                        <td>属性</td>
                        <td>
                            <asp:CheckBox ID="chkIsTemplate" Enabled="false" runat="server" Checked='<%# Bind("IsTemplate") %>' Text="一专多能知识体系"></asp:CheckBox>&nbsp;&nbsp;
                             <asp:CheckBox ID="chkIsPromotion" Enabled="false" runat="server" Checked='<%# Bind("IsPromotion") %>' Text="一专多能级联类别"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>备注</td>
                        <td style="white-space: normal;">
                            <%# Eval("Memo") %></td>
                    </tr>
                </table>
                <div style="display:none;">
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="编辑" OnClientClick="return editBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="删除" OnClientClick="deleteBtnClientClick()">
                    </asp:LinkButton>
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="新建" OnClientClick="return newBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="odsKnowledgeDetail" runat="server" SelectMethod="GetKnowledge"
            TypeName="RailExam.BLL.KnowledgeBLL" DataObjectTypeName="RailExam.Model.Knowledge"
            InsertMethod="AddKnowledge" UpdateMethod="UpdateKnowledge"
            EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="knowledgeId" QueryStringField="id" Type="Int32"/>
            </SelectParameters>
        </asp:ObjectDataSource>
       <asp:HiddenField ID="hfInsert" runat="server" /> 
       <input type="hidden" name="DeleteID" /> 
    </form>
</body>
</html>
