<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PostDetail.aspx.cs" Inherits="RailExamWebApp.Systems.PostDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工作岗位详细信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
       
        // 验证用户输入
        function validateUserInputEdit()
        {
            if($F("fvPost_txtPostNameEdit") && $F("fvPost_txtPostNameEdit").value.trim().length == 0)
            {
                alert("岗位名称不能为空！");
                
                return false;
            }
            if($F("fvPost_txtDescriptionEdit") && $F("fvPost_txtDescriptionEdit").value.length > 500)
            {
                alert("描述不能超过500个字！");
               return false; 
            }    
                     
            if($F("fvPost_txtMemoEdit") && $F("fvPost_txtMemoEdit").value.length >50)
            {
                alert("备注不能超过50个字！");
               return false; 
            }                  
            
             return true;
        }
        
       //新增时，验证用户输入数据 
        function validateUserInputInsert()
        {
            if($F("fvPost_txtPostNameInsert") && $F("fvPost_txtPostNameInsert").value.trim().length == 0)
            {
                alert("岗位名称不能为空！");
                
                return false;
            }
            
             if($F("fvPost_txtDescriptionInsert") && $F("fvPost_txtDescriptionInsert").value.length > 500)
            {
                alert("描述不能超过500个字！");
               return false; 
            }     
            
            if($F("fvPost_txtMemoInsert") && $F("fvPost_txtMemoInsert").value.length >50)
            {
                alert("备注不能超过50个字！");
               return false; 
            }     
                      
            return true;
        }
        
       //客户端编辑按钮点击事件处理函数
        function editBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "编辑";
            
            return true;
        }
        
       //客户端删除按钮点击事件处理函数
        function deleteBtnClientClick()
        {
            if(confirm("您确定要删除此记录吗？"))
            {
                    var  node =window.parent.tvPost.get_selectedNode();                  
		            form1.DeleteID.value = node.get_id();
		            form1.submit();
		            form1.DeleteID.value = "";            
		     }
        }
        
       //客户端新增按钮点击事件处理函数
        function newBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            var theNode = window.parent.tvPost.get_selectedNode();
            
            theItem.value = "新增下级";
            
            return true;
        }
        
       //客户端修改按钮点击事件处理函数
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
        
       //客户插入改按钮点击事件处理函数
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
        
       //客户取消按钮点击事件处理函数
        function cancelBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }   
        
        //弹出选择工作岗位窗体
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
                            岗位名称<span class="require">*</span></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPostNameEdit" runat="server" Text='<%# Bind("PostName") %>' MaxLength="20"
                                Width="60%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            岗位简介</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" Text='<%# Bind("Description") %>'
                                TextMode="MultiLine" Width="98%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr id="promotion">
                        <td>
                            一专多能</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPromotionPostID" runat="server"
                                Width="300px" ReadOnly="true" Text='<%# ShowPostName(Eval("PromotionPostID")==null?String.Empty:Eval("PromotionPostID").ToString()) %>'>
                            </asp:TextBox>
                            <asp:HiddenField ID="hfPromotionPostID" runat="server" Value='<%# Bind("PromotionPostID") %>'/>
                            <input type="button" id="btnPromotionPostIDEdit" runat="server" value="编辑" class="button" OnClick="selectPost()" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注</td>
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
                        Text="保存" OnClientClick="return updateBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="取消" OnClientClick="return cancelBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%">
                            岗位名称<span class="require">*</span></td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPostNameInsert" runat="server" MaxLength="20" Text='<%# Bind("PostName") %>'
                                Width="60%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            岗位简介</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" Text='<%# Bind("Description") %>'
                                TextMode="MultiLine" Width="98%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr id="promotion">
                        <td>
                            一专多能</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPromotionPostID" runat="server" 
                                Width="300px" ReadOnly="true" Text='<%# ShowPostName(Eval("PromotionPostID")==null?String.Empty:Eval("PromotionPostID").ToString()) %>'>
                            </asp:TextBox>
                            <asp:HiddenField ID="hfPromotionPostID" runat="server" Value='<%# Bind("PromotionPostID") %>'/>
                            <asp:Button ID="btnPromotionPostIDInsert" runat="server" Text="编辑" OnClientClick="selectPost()" OnClick="btnPromotionPostIDEdit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注</td>
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
                        Text="保存" OnClientClick="return insertBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="取消" OnClientClick="return cancelBtnClientClick();">
                    </asp:LinkButton>
                </div>
            </InsertItemTemplate>
            <ItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%;">
                            岗位名称</td>
                        <td colspan="3">
                            <asp:Label ID="lblPostName" runat="server" Text='<%# Eval("PostName") %>'>
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            岗位简介</td>
                        <td colspan="3" style="white-space: normal;">
                            <%# Eval("Description") %>
                        </td>
                    </tr>
                    <tr id="promotion">
                        <td>
                            一专多能</td>
                        <td colspan="3">
                            <asp:Label ID="txtPromotionPostID" runat="server" Text='<%# ShowPostName(Eval("PromotionPostID")==null?String.Empty:Eval("PromotionPostID").ToString()) %>'
                                Width="300px" ReadOnly="true">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注</td>
                        <td colspan="3" style="white-space: normal;">
                            <%# Eval("Memo") %>
                        </td>
                    </tr>
                </table>
                <div style="display: none;">
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
