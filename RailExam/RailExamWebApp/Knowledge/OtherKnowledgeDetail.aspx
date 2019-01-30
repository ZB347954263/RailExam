<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="OtherKnowledgeDetail.aspx.cs" Inherits="RailExamWebApp.Knowledge.OtherKnowledgeDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>����֪ʶ��ϸ��Ϣ</title>
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
             if($F("fvOtherKnowledge_txtOtherKnowledgeNameInsert") && $F("fvOtherKnowledge_txtOtherKnowledgeNameInsert").value.length == 0)
            {
                alert("֪ʶ���Ʋ���Ϊ�գ�");
               return false; 
            }         
            
             if($F("fvOtherKnowledge_txtDescriptionInsert") && $F("fvOtherKnowledge_txtDescriptionInsert").value.length > 2000)
            {
                alert("�������ܳ���2000���֣�");
               return false; 
            }     
            
            if($F("fvOtherKnowledge_txtMemoInsert") && $F("fvOtherKnowledge_txtMemoInsert").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }               
            
            return true;
       }   
       
       //�༭ʱ����֤�û��������� 
       function validateUserInputEdit()
       {
            if($F("fvOtherKnowledge_txtOtherKnowledgeNameEdit") && $F("fvOtherKnowledge_txtOtherKnowledgeNameEdit").value.length == 0)
            {
                alert("֪ʶ���Ʋ���Ϊ�գ�");
               return false; 
            }
                           
            if($F("fvOtherKnowledge_txtDescriptionEdit") && $F("fvOtherKnowledge_txtDescriptionEdit").value.length > 2000)
            {
                alert("�������ܳ���2000���֣�");
               return false; 
            }    
                     
            if($F("fvOtherKnowledge_txtMemoEdit") && $F("fvOtherKnowledge_txtMemoEdit").value.length >50)
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
        <asp:FormView ID="fvOtherKnowledge" runat="server" DataSourceID="odsOtherKnowledgeDetail" DataKeyNames="OtherKnowledgeID"
            OnItemUpdated="fvOtherKnowledge_ItemUpdated" OnItemDeleted="fvOtherKnowledge_ItemDeleted"
            OnItemInserted="fvOtherKnowledge_ItemInserted">
            <EditItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%">֪ʶ����<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtOtherKnowledgeNameEdit" runat="server" Text='<%# Bind("OtherKnowledgeName") %>' MaxLength="20"��></asp:TextBox>
\                        </td>
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
                        <td style="width: 5%">֪ʶ����<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtOtherKnowledgeNameInsert" runat="server" Text='<%# Bind("OtherKnowledgeName") %>'��MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="��֪ʶ���ơ�����Ϊ�գ�"
                                ToolTip="��֪ʶ���ơ�����Ϊ�գ�" EnableClientScript="true" ControlToValidate="txtOtherKnowledgeNameInsert"></asp:RequiredFieldValidator>
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
                        <td style="width: 5%">֪ʶ����</td>
                        <td>
                            <asp:Label ID="lblOtherKnowledgeName" runat="server" Text='<%# Eval("OtherKnowledgeName") %>'></asp:Label>
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
        <asp:ObjectDataSource ID="odsOtherKnowledgeDetail" runat="server" SelectMethod="GetOtherKnowledge"
            TypeName="RailExam.BLL.OtherKnowledgeBLL" DataObjectTypeName="RailExam.Model.OtherKnowledge"
            DeleteMethod="DeleteOtherKnowledge" InsertMethod="AddOtherKnowledge" UpdateMethod="UpdateOtherKnowledge"
            EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter Name="OtherKnowledgeID" QueryStringField="id" Type="Int32"/>
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfInsert" runat="server" /> 
    </form>
</body>
</html>
