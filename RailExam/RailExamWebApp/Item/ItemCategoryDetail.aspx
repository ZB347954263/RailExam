<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ItemCategoryDetail.aspx.cs"
    Inherits="RailExamWebApp.Item.ItemCategoryDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />  
<head runat="server">
    <title>����������ϸ��Ϣ</title>

    <script type="text/javascript">
         //��ť����ID��ȥ���� 
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
       
              //�༭��ť����¼������� 
        function editBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "�༭";
            
            return true;
        }
        
        //ɾ����ť����¼������� 
        function deleteBtnClientClick()
        {            
              if (confirm("��ȷ��Ҫɾ���˼�¼��"))
             {
                    var  node =window.parent.tvItemCategory.get_selectedNode();                  
		            form1.DeleteID.value = node.get_id();
		            form1.submit();
		            form1.DeleteID.value = ""; 
             }          
       }
        
        //������ť����¼������� 
        function newBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "�����¼�";
            
            return true;
        }
        
        //�޸İ�ť����¼������� 
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
        
        //���밴ť����¼������� 
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
        
        //ȡ����ť����¼������� 
        function cancelBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }        
               //����ʱ����֤�û���������
       function validateUserInputInsert()
       {
             if($F("fvItemCategoryDetail_txtCategoryNameInsert") && $F("fvItemCategoryDetail_txtCategoryNameInsert").value.length == 0)
            {
                alert("�����������Ʋ���Ϊ�գ�");
               return false; 
            }         
            
             if($F("fvItemCategoryDetail_txtDescriptionInsert") && $F("fvItemCategoryDetail_txtDescriptionInsert").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }     
            
            if($F("fvItemCategoryDetail_txtMemoInsert") && $F("fvItemCategoryDetail_txtMemoInsert").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }               
            
            return true;
       }   
       
       //�༭ʱ����֤�û��������� 
       function validateUserInputEdit()
       {
            if($F("fvItemCategoryDetail_txtCategoryNameEdit") && $F("fvItemCategoryDetail_txtCategoryNameEdit").value.length == 0)
            {
                alert("�����������Ʋ���Ϊ�գ�");
               return false; 
            }
                           
             if($F("fvItemCategoryDetail_txtDescriptionEdit") && $F("fvItemCategoryDetail_txtDescriptionEdit").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }     
                     
            if($F("fvItemCategoryDetail_txtMemoEdit") && $F("fvItemCategoryDetail_txtMemoEdit").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }               
            
             return true;
       }  
       
         </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:FormView ID="fvItemCategoryDetail" runat="server" EnableViewState="false" DataKeyNames="ItemCategoryId"
            DataSourceID="odsItemCategoryDetail"
            OnItemInserted="fvItemCategoryDetail_ItemInserted" OnItemUpdated="fvItemCategoryDetail_ItemUpdated">
            <EditItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">
                            ��������<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCategoryNameEdit" runat="server" Width="35%" Text='<%# Bind("CategoryName") %>' MaxLength="20">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ������</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" Width="98%" Text='<%# Bind("Description") %>'
                                TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" Text='<%# Bind("Memo") %>' TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfItemCategoryId" runat="server" Value='<%# Bind("ItemCategoryId") %>' />
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
                <asp:HiddenField ID="hfLevelNum" runat="server" Value='<%# Bind("LevelNum") %>' />
                <asp:HiddenField ID="hfOrderIndex" runat="server" Value='<%# Bind("OrderIndex") %>' />
                <asp:HiddenField ID="hfIdPath" runat="server" Value='<%# Bind("IdPath") %>' />
                <div style="display: none;">
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="False" CommandName="Update"
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
                        <td style="width: 5%">
                            ��������<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCategoryNameInsert" runat="server" Width="35%" Text='<%# Bind("CategoryName") %>' MaxLength="20">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ������</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" Width="98%" Text='<%# Bind("Description") %>'
                                TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemoInsert" runat="server" Width="98%" Text='<%# Bind("Memo") %>'
                                TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
                <div style="display: none;">
                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="False" CommandName="Insert"
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
                        <td style="width: 5%">
                            ��������</td>
                        <td>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ������</td>
                        <td style="white-space: normal;">
                            <%# Eval("Description") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td style="white-space: normal;">
                            <%# Eval("Memo") %>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfItemCategoryId" runat="server" Value='<%# Bind("ItemCategoryId") %>' />
                <div style="display: none;">
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="�༭" OnClientClick="return editBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="ɾ��" OnClientClick="deleteBtnClientClick()">
                    </asp:LinkButton>
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="����" OnClientClick="return newBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="odsItemCategoryDetail" runat="server"
            InsertMethod="AddItemCategory" SelectMethod="GetItemCategory" TypeName="RailExam.BLL.ItemCategoryBLL"
            UpdateMethod="UpdateItemCategory" DataObjectTypeName="RailExam.Model.ItemCategory">
            <SelectParameters>
                <asp:QueryStringParameter Name="ItemCategoryId" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
       <asp:HiddenField ID="hfInsert" runat="server" /> 
       <input type="hidden" name="DeleteID" />
    </form>
</body>
</html>
