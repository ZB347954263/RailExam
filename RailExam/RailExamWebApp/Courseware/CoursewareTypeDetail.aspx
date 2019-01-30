<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="CoursewareTypeDetail.aspx.cs" Inherits="RailExamWebApp.Courseware.CoursewareTypeDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�μ������ϸ��Ϣ</title>
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
                     var  node =window.parent.tvCoursewareType.get_selectedNode();                  
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
             if($F("fvCoursewareType_txtCoursewareTypeNameInsert") && $F("fvCoursewareType_txtCoursewareTypeNameInsert").value.length == 0)
            {
                alert("�μ�������Ʋ���Ϊ�գ�");
               return false; 
            }         
            if($F("fvCoursewareType_txtDescriptionInsert") && $F("fvCoursewareType_txtDescriptionInsert").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }     
            
            if($F("fvCoursewareType_txtMemoInsert") && $F("fvCoursewareType_txtMemoInsert").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }               
            
            return true;
       }   
       
       //�༭ʱ����֤�û��������� 
       function validateUserInputEdit()
       {
            if($F("fvCoursewareType_txtCoursewareTypeNameEdit") && $F("fvCoursewareType_txtCoursewareTypeNameEdit").value.length == 0)
            {
                alert("�μ�������Ʋ���Ϊ�գ�");
               return false; 
            }
                           
            if($F("fvCoursewareType_txtDescriptionEdit") && $F("fvCoursewareType_txtDescriptionEdit").value.length > 200)
            {
                alert("�������ܳ���200���֣�");
               return false; 
            }    
                     
            if($F("fvCoursewareType_txtMemoEdit") && $F("fvCoursewareType_txtMemoEdit").value.length >50)
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
            <asp:FormView ID="fvCoursewareType" runat="server" DataSourceID="odsCoursewareTypeDetail"
                DataKeyNames="CoursewareTypeId" OnItemUpdated="fvCoursewareType_ItemUpdated"
                OnItemInserted="fvCoursewareType_ItemInserted">
                <EditItemTemplate>
                    <table class="contentTable">
                        <tr>
                            <td style="width: 3%;">�μ��������<span class="require">*</span></td>
                            <td>
                                <asp:TextBox ID="txtCoursewareTypeNameEdit" runat="server" Width="60%" Text='<%# Bind("CoursewareTypeName") %>' MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>����</td>
                            <td><asp:TextBox ID="txtDescriptionEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>��ע</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                        </tr>
                    </table>
                    <br />
                    <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
                    <asp:HiddenField ID="hfIdPath" runat="server" Value='<%# Bind("IdPath") %>' />
                    <asp:HiddenField ID="hfLevelNum" runat="server" Value='<%# Bind("LevelNum") %>' />
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
                            <td style="width: 3%;">�μ��������<span class="require">*</span></td>
                            <td>
                                <asp:TextBox ID="txtCoursewareTypeNameInsert" runat="server" Width="60%" Text='<%# Bind("CoursewareTypeName") %>' MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>����</td>
                            <td><asp:TextBox ID="txtDescriptionInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>��ע</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtMemoInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                        </tr>
                    </table>
                    <br />
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
                            <td style="width: 3%;">�μ��������</td>
                            <td>
                                <asp:Label ID="lblCoursewareTypeName" runat="server" Text='<%# Eval("CoursewareTypeName") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>����</td>
                            <td colspan="3" style="white-space: normal;">
                                <%# Eval("Description") %></td>
                        </tr>
                        <tr>
                            <td>��ע</td>
                            <td colspan="3" style="white-space: normal;">
                                <%# Eval("Memo") %></td>
                        </tr>
                    </table>
                    <br />
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
        <asp:ObjectDataSource ID="odsCoursewareTypeDetail" runat="server" DataObjectTypeName="RailExam.Model.CoursewareType"
             InsertMethod="AddCoursewareType" SelectMethod="GetCoursewareType"
            TypeName="RailExam.BLL.CoursewareTypeBLL" UpdateMethod="UpdateCoursewareType" EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter Name="coursewareTypeId" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
         <asp:HiddenField ID="hfInsert" runat="server" />  
       <input type="hidden" name="DeleteID" /> 
    </form>
</body>
</html>
