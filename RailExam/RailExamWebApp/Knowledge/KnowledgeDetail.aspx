<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="KnowledgeDetail.aspx.cs" Inherits="RailExamWebApp.Knowledge.KnowledgeDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>֪ʶ��ϵ��ϸ��Ϣ</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />  
    <script type="text/javascript">  
        //��ť����ID��ȥ���� 
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
        
        function editBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "�༭";
            
            return true;
        }
        
        function deleteBtnClientClick()
        {
              if (confirm("��ȷ��Ҫɾ���˼�¼��"))
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
            theItem.value = "�����¼�";
            
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
       
              //����ʱ����֤�û���������
       function validateUserInputInsert()
       {
             if($F("fvKnowledge_txtKnowledgeNameInsert") && $F("fvKnowledge_txtKnowledgeNameInsert").value.length == 0)
            {
                alert("֪ʶ��ϵ���Ʋ���Ϊ�գ�");
               return false; 
            }         
            
             if($F("fvKnowledge_txtDescriptionInsert") && $F("fvKnowledge_txtDescriptionInsert").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }     
            
            if($F("fvKnowledge_txtMemoInsert") && $F("fvKnowledge_txtMemoInsert").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }               
            
            return true;
       }   
       
       //�༭ʱ����֤�û��������� 
       function validateUserInputEdit()
       {
            if($F("fvKnowledge_txtKnowledgeNameEdit") && $F("fvKnowledge_txtKnowledgeNameEdit").value.length == 0)
            {
                alert("֪ʶ��ϵ���Ʋ���Ϊ�գ�");
               return false; 
            }
                           
            if($F("fvKnowledge_txtDescriptionEdit") && $F("fvKnowledge_txtDescriptionEdit").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }    
                     
            if($F("fvKnowledge_txtMemoEdit") && $F("fvKnowledge_txtMemoEdit").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
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
                        <td style="width: 5%">֪ʶ��ϵ����<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtKnowledgeNameEdit" runat="server" Text='<%# Bind("KnowledgeName") %>'  MaxLength="20"  Width="50%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>����</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>����</td>
                        <td>
                            <asp:CheckBox ID="chkIsTemplateEdit" runat="server" Checked='<%# Bind("IsTemplate") %>' Text="һר����֪ʶ��ϵ"></asp:CheckBox>&nbsp;&nbsp;
                             <asp:CheckBox ID="chkIsPromotionEdit" runat="server" Checked='<%# Bind("IsPromotion") %>' Text="һר���ܼ������"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
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
                        Text="����" OnClientClick="return updateBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="ȡ��" OnClientClick="return cancelBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">֪ʶ��ϵ����<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtKnowledgeNameInsert" runat="server" Text='<%# Bind("KnowledgeName") %>' MaxLength="20"  Width="50%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="��֪ʶ��ϵ���ơ�����Ϊ�գ�"
                                ToolTip="��֪ʶ��ϵ���ơ�����Ϊ�գ�" EnableClientScript="true" ControlToValidate="txtKnowledgeNameInsert"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>����</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>����</td>
                        <td>
                            <asp:CheckBox ID="chkIsTemplateInsert" runat="server" Checked='<%# Bind("IsTemplate") %>' Text="һר����֪ʶ��ϵ"></asp:CheckBox>&nbsp;&nbsp;
                             <asp:CheckBox ID="chkIsPromotionInsert" runat="server" Checked='<%# Bind("IsPromotion") %>' Text="һר���ܼ������"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemoInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>'/>
                <div style="display: none;">
                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                        Text="����" OnClientClick="return insertBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="ȡ��" OnClientClick="return cancelBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </InsertItemTemplate>
            <ItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">֪ʶ��ϵ����</td>
                        <td>
                            <asp:Label ID="lblKnowledgeName" runat="server" Text='<%# Eval("KnowledgeName") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>����</td>
                        <td style="white-space: normal;">
                            <%# Eval("Description") %></td>
                    </tr>
                    <tr>
                        <td>����</td>
                        <td>
                            <asp:CheckBox ID="chkIsTemplate" Enabled="false" runat="server" Checked='<%# Bind("IsTemplate") %>' Text="һר����֪ʶ��ϵ"></asp:CheckBox>&nbsp;&nbsp;
                             <asp:CheckBox ID="chkIsPromotion" Enabled="false" runat="server" Checked='<%# Bind("IsPromotion") %>' Text="һר���ܼ������"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
                        <td style="white-space: normal;">
                            <%# Eval("Memo") %></td>
                    </tr>
                </table>
                <div style="display:none;">
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="�༭" OnClientClick="return editBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="ɾ��" OnClientClick="deleteBtnClientClick()">
                    </asp:LinkButton>
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="�½�" OnClientClick="return newBtnClientClick();">
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
