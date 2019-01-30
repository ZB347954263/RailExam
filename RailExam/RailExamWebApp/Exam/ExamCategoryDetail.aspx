<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExamCategoryDetail.aspx.cs" Inherits="RailExamWebApp.Exam.ExamCategoryDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���Է�����ϸ��Ϣ</title>
   <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />  
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
            return confirm("��ȷ��Ҫɾ���˼�¼��");
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
             if($F("fvExamCategory_txtExamCategoryNameInsert") && $F("fvExamCategory_txtExamCategoryNameInsert").value.length == 0)
            {
                alert("���Է������Ʋ���Ϊ�գ�");
               return false; 
            }         
            
             if($F("fvExamCategory_txtDescriptionInsert") && $F("fvExamCategory_txtDescriptionInsert").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }     
            
            if($F("fvExamCategory_txtMemoInsert") && $F("fvExamCategory_txtMemoInsert").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }               
            
            return true;
       }   
       
       //�༭ʱ����֤�û��������� 
       function validateUserInputEdit()
       {
            if($F("fvExamCategory_txtExamCategoryNameEdit") && $F("fvExamCategory_txtExamCategoryNameEdit").value.length == 0)
            {
                alert("���Է������Ʋ���Ϊ�գ�");
               return false; 
            }
                           
            if($F("fvExamCategory_txtDescriptionEdit") && $F("fvExamCategory_txtDescriptionEdit").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }    
                     
            if($F("fvExamCategory_txtMemoEdit") && $F("fvExamCategory_txtMemoEdit").value.length >50)
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
        <asp:FormView ID="fvExamCategory" runat="server" DataSourceID="odsExamCategoryDetail" DataKeyNames="ExamCategoryId"
            OnItemUpdated="fvExamCategory_ItemUpdated" OnItemDeleted="fvExamCategory_ItemDeleted"
            OnItemInserted="fvExamCategory_ItemInserted">
            <EditItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">���Է�������<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtExamCategoryNameEdit" runat="server" Text='<%# Bind("CategoryName") %>' Width="35%"></asp:TextBox>
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
                        <td style="width: 5%">���Է�������<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtExamCategoryNameInsert" runat="server" Text='<%# Bind("CategoryName") %>' Width="35%"></asp:TextBox>
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
                        <td style="width: 5%">���Է�������</td>
                        <td>
                            <asp:Label ID="lblExamCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
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
        <asp:ObjectDataSource ID="odsExamCategoryDetail" runat="server" SelectMethod="GetExamCategory"
            TypeName="RailExam.BLL.ExamCategoryBLL" DataObjectTypeName="RailExam.Model.ExamCategory"
            DeleteMethod="DeleteExamCategory" InsertMethod="AddExamCategory" UpdateMethod="UpdateExamCategory"
            EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="ExamCategoryId" QueryStringField="id" Type="Int32"/>
            </SelectParameters>
        </asp:ObjectDataSource>
         <asp:HiddenField ID="hfInsert" runat="server" />   
    </form>
</body>
</html>
