<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainTypeDetail.aspx.cs" Inherits="RailExamWebApp.Train.TrainTypeDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��ѵ�����ϸ��Ϣ</title>
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
            if(confirm("��ȷ��Ҫɾ���˼�¼��"))
            {
                var node = window.parent.tvTrainType.get_selectedNode();
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
             if($F("fvTrainType_txtTypeNameInsert") && $F("fvTrainType_txtTypeNameInsert").value.length == 0)
            {
                alert("��ѵ������Ʋ���Ϊ�գ�");
               return false; 
            }         
            
             if($F("fvTrainType_txtDescriptionInsert") && $F("fvTrainType_txtDescriptionInsert").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }     
            
            if($F("fvTrainType_txtMemoInsert") && $F("fvTrainType_txtMemoInsert").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }               
            
            return true;
       }   
       
       //�༭ʱ����֤�û��������� 
       function validateUserInputEdit()
       {
            if($F("fvTrainType_txtTypeNameEdit") && $F("fvTrainType_txtTypeNameEdit").value.length == 0)
            {
                alert("��ѵ������Ʋ���Ϊ�գ�");
               return false; 
            }
                           
            if($F("fvTrainType_txtDescriptionEdit") && $F("fvTrainType_txtDescriptionEdit").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }    
                     
            if($F("fvTrainType_txtMemoEdit") && $F("fvTrainType_txtMemoEdit").value.length >50)
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
        <asp:FormView ID="fvTrainType" runat="server" DataSourceID="dsTrainType" DataKeyNames="TrainTypeID"
            OnItemInserted="fvTrainType_ItemInserted"
            OnItemUpdated="fvTrainType_ItemUpdated">
            <EditItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 5%;">��ѵ���<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtTypeNameEdit" runat="server" Width="35%" Text='<%# Bind("TypeName") %>' MaxLength="20">
                            </asp:TextBox>
                        </td>
                     </tr>
                     <tr>
                        <td>����</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemoEdit" runat="server" TextMode="MultiLine" Text='<%# Bind("Memo") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentID" runat="server" Value='<%# Bind("ParentID") %>' />
                <asp:HiddenField ID="hfLevelNum" runat="server" Value='<%# Bind("LevelNum") %>' />
                <asp:HiddenField ID="hfIDPath" runat="server" Value='<%# Bind("IDPath") %>' />
                <asp:HiddenField ID="hfOrderIndex" runat="server" Value='<%# Bind("OrderIndex") %>' />
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
                        <td style="width: 5%">��ѵ���<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtTypeNameInsert" runat="server" Width="35%" Text='<%# Bind("TypeName") %>' MaxLength="20">
                            </asp:TextBox>
                        </td>
                     </tr>
                     <tr>
                        <td>����</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemoInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Memo") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentID" runat="server" Value='<%# Bind("ParentID") %>' />
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
                        <td style="width: 5%;">��ѵ���</td>
                        <td>
                            <asp:Label ID="TypeNameLabel" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label></td>
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
                        Text="ɾ��" OnClientClick="deleteBtnClientClick()">
                    </asp:LinkButton>
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="�½�" OnClientClick="return newBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="dsTrainType" runat="server"
            InsertMethod="AddTrainType" SelectMethod="GetTrainTypeInfo" TypeName="RailExam.BLL.TrainTypeBLL"
            UpdateMethod="UpdateTrainType" DataObjectTypeName="RailExam.Model.TrainType"
            EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="TrainTypeID" QueryStringField="id"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfInsert" runat="server" /> 
       <input type="hidden" name="DeleteID"/> 
   </form>
</body>
</html>
