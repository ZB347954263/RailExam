<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PaperCategoryDetail.aspx.cs" Inherits="RailExamWebApp.Paper.PaperCategoryDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�Ծ������ϸ��Ϣ</title>
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
            return confirm("��ȷ��Ҫɾ���˼�¼��");
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
             if($F("fvPaperCategory_txtCategoryNameInsert") && $F("fvPaperCategory_txtCategoryNameInsert").value.length == 0)
            {
                alert("�Ծ�������Ʋ���Ϊ�գ�");
               return false; 
            }         
            
             if($F("fvPaperCategory_txtDescriptionInsert") && $F("fvPaperCategory_txtDescriptionInsert").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }     
            
            if($F("fvPaperCategory_txtMemoInsert") && $F("fvPaperCategory_txtMemoInsert").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }               
            
            return true;
       }   
       
       //�༭ʱ����֤�û��������� 
       function validateUserInputEdit()
       {
            if($F("fvPaperCategory_txtCategoryNameEdit") && $F("fvPaperCategory_txtCategoryNameEdit").value.length == 0)
            {
                alert("�Ծ�������Ʋ���Ϊ�գ�");
               return false; 
            }
                           
            if($F("fvPaperCategory_txtDescriptionEdit") && $F("fvPaperCategory_txtDescriptionEdit").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }    
                     
            if($F("fvPaperCategory_txtMemoEdit") && $F("fvPaperCategory_txtMemoEdit").value.length >50)
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
        <asp:FormView ID="fvPaperCategory" runat="server" DataSourceID="odsPaperCategoryDetail" DataKeyNames="PaperCategoryId"
            OnItemUpdated="fvPaperCategory_ItemUpdated" OnItemDeleted="fvPaperCategory_ItemDeleted"
            OnItemInserted="fvPaperCategory_ItemInserted">
            <EditItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">�Ծ����<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCategoryNameEdit" runat="server"   Text='<%# Bind("CategoryName") %>' Width="35%" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>                  
                    <tr>
                        <td>����</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemoEdit" runat="server" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentID" runat="server" Value='<%# Bind("ParentID") %>'/>
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
                        <td style="width: 5%">�Ծ����<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCategoryNameInsert" runat="server" Text='<%# Bind("CategoryName") %>' Width="35%" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>                   
                    <tr>
                        <td>����</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemoInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentID" runat="server" Value='<%# Bind("ParentID") %>'/>
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
                        <td style="width: 5%">�Ծ����</td>
                        <td>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>' ></asp:Label>
                        </td>
                    </tr>                  
                    <tr>
                        <td>����</td>
                        <td style="white-space: normal;">
                            <%# Eval("Description") %></td>
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
                        Text="ɾ��" OnClientClick="return deleteBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="�½�" OnClientClick="return newBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="odsPaperCategoryDetail" runat="server" SelectMethod="GetPaperCategory"
            TypeName="RailExam.BLL.PaperCategoryBLL" DataObjectTypeName="RailExam.Model.PaperCategory"
            DeleteMethod="DeletePaperCategory" InsertMethod="AddPaperCategory" UpdateMethod="UpdatePaperCategory"
            EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter Name="PaperCategoryId" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
         <asp:HiddenField ID="hfInsert" runat="server" />  
    </form>
</body>
</html>
