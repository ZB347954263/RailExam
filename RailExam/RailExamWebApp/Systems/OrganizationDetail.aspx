<%@ Page Language="C#" AutoEventWireup="True" Codebehind="OrganizationDetail.aspx.cs"
    Inherits="RailExamWebApp.Systems.OrganizationDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织机构详细信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //按钮对象ID回去对象 
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
       
        //编辑时，验证用户输入数据 
       function validateUserInputEdit()
       {
            if($F("fvOrganization_txtShortNameEdit") && $F("fvOrganization_txtShortNameEdit").value.length == 0)
            {
                alert("机构简称不能为空！");
               
               return false; 
            }               
            else if($F("fvOrganization_txtFullNameEdit") && $F("fvOrganization_txtFullNameEdit").value.length == 0)
            {
                alert("机构全称不能为空！");
               
               return false; 
            }               
            
            return true;
       }  
       
       //插入时，验证用户输入数据
       function validateUserInputInsert()
       {
            if($F("fvOrganization_txtShortNameInsert") && $F("fvOrganization_txtShortNameInsert").value.length == 0)
            {
                alert("机构简称不能为空！");
               
               return false; 
            }               
            else if($F("fvOrganization_txtFullNameInsert") && $F("fvOrganization_txtFullNameInsert").value.length == 0)
            {
                alert("机构全称不能为空！");
               
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
                    var  node =window.parent.tvOrganization.get_selectedNode();                  
		            form1.DeleteID.value = node.get_id();
		            form1.submit();
		            form1.DeleteID.value = ""; 
            }
        }
        
        //客户端新增按钮点击事件处理函数 
        function newBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "新增下级";
            
            return true;
        }
        
        //客户端更新按钮点击事件处理函数 
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
        
        //客户端插入按钮点击事件处理函数 
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
        
        //客户端取消按钮点击事件处理函数 
        function cancelBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:FormView ID="fvOrganization" runat="server" DataSourceID="odsOrganizationDetail"
            DataKeyNames="OrganizationId" OnItemInserted="fvOrganization_ItemInserted" OnItemUpdated="fvOrganization_ItemUpdated"
            OnItemInserting="fvOrganization_ItemInserting" OnItemUpdating ="fvOrganization_ItemUpdating" 
            OnDataBound="fvOrganization_DataBound">
            <EditItemTemplate>
                <table class="contentTable">
                    <tr>
                        <td width="10%">
                            简称<span class="require">*</span>
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtShortNameEdit" MaxLength="16" runat="server" Text='<%# Bind("ShortName") %>'>
                            </asp:TextBox>
                        </td>
                        <td width="10%">
                            全称<span class="require">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFullNameEdit" runat="server" MaxLength="20" Text='<%# Bind("FullName") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            地址
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressEdit" runat="server" MaxLength="30" Text='<%# Bind("Address") %>'>
                            </asp:TextBox>
                        </td>
                        <td>
                            邮政编码
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostCodeEdit" runat="server" MaxLength="6" Text='<%# Bind("PostCode") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            联系人
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactPersonEdit" runat="server" MaxLength="20" Text='<%# Bind("ContactPerson") %>'>
                            </asp:TextBox>
                        </td>
                        <td>
                            电话
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneEdit" runat="server" MaxLength="30" Text='<%# Bind("Phone") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            网址
                        </td>
                        <td>
                            <asp:TextBox ID="txtWebSiteEdit" runat="server" MaxLength="100" Text='<%# Bind("WebSite") %>'>
                            </asp:TextBox>
                        </td>
                        <td>
                            电子邮件
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailEdit" runat="server" MaxLength="50" Text='<%# Bind("Email") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            所属系统
                        </td>
                        <td>
                            <asp:HiddenField ID="hfRailSystem" runat="server" Value='<%# Bind("RailSystemID") %>' />
                            <asp:DropDownList ID="ddlRailSystem" runat="server"  >
                            </asp:DropDownList>
                        </td>
                        <td>
                            有效性
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkIsEffect" Checked='<%# Bind("IsEffect") %>' Text="有效"/>
                        </td>
                 </tr>       
                 <tr>
                        <td>
                            简介
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" MaxLength="500" Text='<%# Bind("Description") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemoEdit" runat="server" MaxLength="50" Text='<%# Bind("Memo") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
                <asp:HiddenField ID="hfIdPath" runat="server" Value='<%# Bind("IdPath") %>' />
                <asp:HiddenField ID="hfLevelNum" runat="server" Value='<%# Bind("LevelNum") %>' />
                <asp:HiddenField ID="hfOrderIndex" runat="server" Value='<%# Bind("OrderIndex") %>' />
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
                        <td width="10%">
                            简称<span class="require">*</span>
                        </td>
                        <td width="40%">
                            <asp:TextBox ID="txtShortNameInsert" runat="server" MaxLength="16" Text='<%# Bind("ShortName") %>'>
                            </asp:TextBox>
                        </td>
                        <td width="10%">
                            全称<span class="require">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFullNameInsert" runat="server" MaxLength="20" Text='<%# Bind("FullName") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            地址
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddressInsert" runat="server" MaxLength="30" Text='<%# Bind("Address") %>'>
                            </asp:TextBox></td>
                        <td>
                            邮政编码
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostCodeInsert" runat="server" MaxLength="6" Text='<%# Bind("PostCode") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            联系人
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactPersonInsert" runat="server" MaxLength="20" Text='<%# Bind("ContactPerson") %>'>
                            </asp:TextBox>
                        </td>
                        <td>
                            电话
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneInsert" runat="server" MaxLength="30" Text='<%# Bind("Phone") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            网址
                        </td>
                        <td>
                            <asp:TextBox ID="txtWebSiteInsert" runat="server" MaxLength="100" Text='<%# Bind("WebSite") %>'>
                            </asp:TextBox>
                        </td>
                        <td>
                            电子邮件
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailInsert" runat="server" MaxLength="50" Text='<%# Bind("Email") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            所属系统
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRailSystem" runat="server">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hfRailSystem" runat="server" Value='<%# Bind("RailSystemID") %>' />
                        </td>
                        <td>
                            有效性
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkIsEffect" Checked='<%# Bind("IsEffect") %>' Text="有效"/>
                        </td>
                 </tr>       
                 <tr>
                        <td>
                            简介
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" MaxLength="500" Text='<%# Bind("Description") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemoInsert" runat="server" MaxLength="50" Text='<%# Bind("Memo") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
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
                        <td width="10%">
                            简称
                        </td>
                        <td width="40%">
                            <asp:Label ID="lblShortName" runat="server" Text='<%# Eval("ShortName") %>'></asp:Label></td>
                        <td width="10%">
                            全称
                        </td>
                        <td>
                            <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            地址
                        </td>
                        <td>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label></td>
                        <td>
                            邮政编码
                        </td>
                        <td>
                            <asp:Label ID="lblPostCode" runat="server" Text='<%# Eval("PostCode") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            联系人
                        </td>
                        <td>
                            <asp:Label ID="lblContactPerson" runat="server" Text='<%# Eval("ContactPerson") %>'></asp:Label></td>
                        <td>
                            电话
                        </td>
                        <td>
                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            网址
                        </td>
                        <td>
                            <asp:Label ID="lblWebSite" runat="server" Text='<%# Eval("WebSite") %>'></asp:Label></td>
                        <td>
                            电子邮件
                        </td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            所属系统
                        </td>
                        <td>
                            <asp:Label ID="lblRailSystem" runat="server" Text='<%# Eval("RailSystemName") %>'></asp:Label>
                        </td>
                        <td>
                            有效性
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkIsEffect" Checked='<%# Eval("IsEffect") %>' Text="有效" Enabled="False"/>
                        </td>
                 </tr>       
                 <tr>
                        <td>
                            简介
                        </td>
                        <td colspan="3" style="white-space: normal;">
                            <%# Eval("Description") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注
                        </td>
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
        <asp:ObjectDataSource ID="odsOrganizationDetail" runat="server" InsertMethod="AddOrganization"
            SelectMethod="GetOrganization" TypeName="RailExam.BLL.OrganizationBLL" UpdateMethod="UpdateOrganization"
            DataObjectTypeName="RailExam.Model.Organization" EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="OrganizationId" QueryStringField="id"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfInsert" runat="server" />
        <input type="hidden" name="DeleteID" />
    </form>
</body>
</html>
