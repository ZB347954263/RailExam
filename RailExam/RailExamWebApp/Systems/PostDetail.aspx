<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PostDetail.aspx.cs" Inherits="RailExamWebApp.Systems.PostDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>������λ��ϸ��Ϣ</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
       
        // ��֤�û�����
        function validateUserInputEdit()
        {
            if($F("fvPost_txtPostNameEdit") && $F("fvPost_txtPostNameEdit").value.trim().length == 0)
            {
                alert("��λ���Ʋ���Ϊ�գ�");
                
                return false;
            }
            if($F("fvPost_txtDescriptionEdit") && $F("fvPost_txtDescriptionEdit").value.length > 500)
            {
                alert("�������ܳ���500���֣�");
               return false; 
            }    
                     
            if($F("fvPost_txtMemoEdit") && $F("fvPost_txtMemoEdit").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }                  
            
             return true;
        }
        
       //����ʱ����֤�û��������� 
        function validateUserInputInsert()
        {
            if($F("fvPost_txtPostNameInsert") && $F("fvPost_txtPostNameInsert").value.trim().length == 0)
            {
                alert("��λ���Ʋ���Ϊ�գ�");
                
                return false;
            }
            
             if($F("fvPost_txtDescriptionInsert") && $F("fvPost_txtDescriptionInsert").value.length > 500)
            {
                alert("�������ܳ���500���֣�");
               return false; 
            }     
            
            if($F("fvPost_txtMemoInsert") && $F("fvPost_txtMemoInsert").value.length >50)
            {
                alert("��ע���ܳ���50���֣�");
               return false; 
            }     
                      
            return true;
        }
        
       //�ͻ��˱༭��ť����¼�������
        function editBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "�༭";
            
            return true;
        }
        
       //�ͻ���ɾ����ť����¼�������
        function deleteBtnClientClick()
        {
            if(confirm("��ȷ��Ҫɾ���˼�¼��"))
            {
                    var  node =window.parent.tvPost.get_selectedNode();                  
		            form1.DeleteID.value = node.get_id();
		            form1.submit();
		            form1.DeleteID.value = "";            
		     }
        }
        
       //�ͻ���������ť����¼�������
        function newBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            var theNode = window.parent.tvPost.get_selectedNode();
            
            theItem.value = "�����¼�";
            
            return true;
        }
        
       //�ͻ����޸İ�ť����¼�������
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
        
       //�ͻ�����İ�ť����¼�������
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
        
       //�ͻ�ȡ����ť����¼�������
        function cancelBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }   
        
        //����ѡ������λ����
        function selectPost()
        {
            var ids=document.getElementById('fvPost_hfPromotionPostID').value;
            var names=document.getElementById("fvPost_txtPromotionPostID").value;
            var postId = document.getElementById("fvPost_hfPostId").value;

            var url1="../Common/MultiSelectPost.aspx?ids="+ids+"&names="+escape(names)+"&postId="+postId;

            var selectedPost = window.showModalDialog(url1, 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');
            
            if(! selectedPost)
            {
                return;
            }
        	
            var ids=selectedPost.split('|')[0];        
            document.getElementById('fvPost_hfPromotionPostID').value = ids;
            document.getElementById('fvPost_txtPromotionPostID').value = selectedPost.split('|')[1];
        }     
        
        function init() {

        	var search = window.location.search;

        	var depth = search.substring(search.indexOf("&depth=") + 7);
        	
        	if(depth<2) {
        		document.getElementById("promotion").style.display = "none";
        	}
        }
        
    </script>

    <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>

</head>
<body onload="init();">
    <form id="form1" runat="server">
        <asp:FormView ID="fvPost" runat="server" DataSourceID="odsPostDetail" DataKeyNames="PostId,ParentId"
            EnableViewState="false" OnItemInserted="fvPost_ItemInserted" OnItemUpdated="fvPost_ItemUpdated">
            <EditItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%">
                            ��λ����<span class="require">*</span></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPostNameEdit" runat="server" Text='<%# Bind("PostName") %>' MaxLength="20"
                                Width="60%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��λ���</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" Text='<%# Bind("Description") %>'
                                TextMode="MultiLine" Width="98%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr id="promotion">
                        <td>
                            һר����</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPromotionPostID" runat="server"
                                Width="300px" ReadOnly="true" Text='<%# ShowPostName(Eval("PromotionPostID")==null?String.Empty:Eval("PromotionPostID").ToString()) %>'>
                            </asp:TextBox>
                            <asp:HiddenField ID="hfPromotionPostID" runat="server" Value='<%# Bind("PromotionPostID") %>'/>
                            <input type="button" id="btnPromotionPostIDEdit" runat="server" value="�༭" class="button" OnClick="selectPost()" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemoEdit" runat="server" Text='<%# Bind("Memo") %>' TextMode="MultiLine"
                                Width="98%">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfPostId" runat="server" Value='<%# Bind("PostId") %>' />
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
                <asp:HiddenField ID="hfPostLevel" runat="server" Value='<%# Bind("PostLevel") %>' />
                <asp:HiddenField ID="hfOrderIndex" runat="server" Value='<%# Bind("OrderIndex") %>' />
                <asp:HiddenField ID="hfIdPath" runat="server" Value='<%# Bind("IdPath") %>' />
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
                        <td style="width: 10%">
                            ��λ����<span class="require">*</span></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPostNameInsert" runat="server" MaxLength="20" Text='<%# Bind("PostName") %>'
                                Width="60%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��λ���</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" Text='<%# Bind("Description") %>'
                                TextMode="MultiLine" Width="98%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr id="promotion">
                        <td>
                            һר����</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPromotionPostID" runat="server" 
                                Width="300px" ReadOnly="true" Text='<%# ShowPostName(Eval("PromotionPostID")==null?String.Empty:Eval("PromotionPostID").ToString()) %>'>
                            </asp:TextBox>
                            <asp:HiddenField ID="hfPromotionPostID" runat="server" Value='<%# Bind("PromotionPostID") %>'/>
                            <asp:Button ID="btnPromotionPostIDInsert" runat="server" Text="�༭" OnClientClick="selectPost()" OnClick="btnPromotionPostIDEdit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemoInsert" runat="server" Text='<%# Bind("Memo") %>' TextMode="MultiLine"
                                Width="98%">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
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
                        <td style="width: 10%;">
                            ��λ����</td>
                        <td colspan="3">
                            <asp:Label ID="lblPostName" runat="server" Text='<%# Eval("PostName") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��λ���</td>
                        <td colspan="3" style="white-space: normal;">
                            <%# Eval("Description") %>
                        </td>
                    </tr>
                    <tr id="promotion">
                        <td>
                            һר����</td>
                        <td colspan="3">
                            <asp:Label ID="txtPromotionPostID" runat="server" Text='<%# ShowPostName(Eval("PromotionPostID")==null?String.Empty:Eval("PromotionPostID").ToString()) %>'
                                Width="300px" ReadOnly="true">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td colspan="3" style="white-space: normal;">
                            <%# Eval("Memo") %>
                        </td>
                    </tr>
                </table>
                <div style="display: none;">
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
        <div>
            <asp:ObjectDataSource ID="odsPostDetail" runat="server" InsertMethod="AddPost" SelectMethod="GetPost"
                TypeName="RailExam.BLL.PostBLL" UpdateMethod="UpdatePost" DataObjectTypeName="RailExam.Model.Post"
                OnInserting="odsPostDetail_Inserting" OnUpdating="odsPostDetail_Updating">
                <SelectParameters>
                    <asp:QueryStringParameter ConvertEmptyStringToNull="False" DefaultValue="1" Name="PostId"
                        QueryStringField="id" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <div id="divPromotionPost" runat="server">
        </div>
        <asp:HiddenField ID="hfInsert" runat="server" />
        <input type="hidden" name="DeleteID" />
    </form>
</body>
</html>
