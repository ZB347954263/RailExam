<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExamCategoryDetail.aspx.cs" Inherits="RailExamWebApp.Exam.ExamCategoryDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试分类详细信息</title>
   <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />  
    <script type="text/javascript">  
         //按钮对象ID回去对象 
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
       
              //编辑按钮点击事件处理函数 
        function editBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "编辑";
            
            return true;
        }
        
        //删除按钮点击事件处理函数 
        function deleteBtnClientClick()
        {            
            return confirm("您确定要删除此记录吗？");
        }
        
        //新增按钮点击事件处理函数 
        function newBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "新增下级";
            
            return true;
        }
        
        //修改按钮点击事件处理函数 
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
        
        //插入按钮点击事件处理函数 
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
        
        //取消按钮点击事件处理函数 
        function cancelBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }        
               //插入时，验证用户输入数据
       function validateUserInputInsert()
       {
             if($F("fvExamCategory_txtExamCategoryNameInsert") && $F("fvExamCategory_txtExamCategoryNameInsert").value.length == 0)
            {
                alert("考试分类名称不能为空！");
               return false; 
            }         
            
             if($F("fvExamCategory_txtDescriptionInsert") && $F("fvExamCategory_txtDescriptionInsert").value.length > 200)
            {
                alert("描述不能超过200个字！");
               return false; 
            }     
            
            if($F("fvExamCategory_txtMemoInsert") && $F("fvExamCategory_txtMemoInsert").value.length >50)
            {
                alert("备注不能超过50个字！");
               return false; 
            }               
            
            return true;
       }   
       
       //编辑时，验证用户输入数据 
       function validateUserInputEdit()
       {
            if($F("fvExamCategory_txtExamCategoryNameEdit") && $F("fvExamCategory_txtExamCategoryNameEdit").value.length == 0)
            {
                alert("考试分类名称不能为空！");
               return false; 
            }
                           
            if($F("fvExamCategory_txtDescriptionEdit") && $F("fvExamCategory_txtDescriptionEdit").value.length > 200)
            {
                alert("描述不能超过200个字！");
               return false; 
            }    
                     
            if($F("fvExamCategory_txtMemoEdit") && $F("fvExamCategory_txtMemoEdit").value.length >50)
            {
                alert("备注不能超过50个字！");
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
                        <td style="width: 5%">考试分类名称<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtExamCategoryNameEdit" runat="server" Text='<%# Bind("CategoryName") %>' Width="35%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>描述</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionEdit" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>备注</td>
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
                        <td style="width: 5%">考试分类名称<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtExamCategoryNameInsert" runat="server" Text='<%# Bind("CategoryName") %>' Width="35%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>描述</td>
                        <td>
                            <asp:TextBox ID="txtDescriptionInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Description") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>备注</td>
                        <td>
                            <asp:TextBox ID="txtMemoInsert" runat="server" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>'/>
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
                        <td style="width: 5%">考试分类名称</td>
                        <td>
                            <asp:Label ID="lblExamCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>描述</td>
                        <td style="white-space: normal;">
                            <%# Eval("Description") %></td>
                    </tr>
                    <tr>
                        <td>备注</td>
                        <td style="white-space: normal;">
                            <%# Eval("Memo") %></td>
                    </tr>
                </table>
                <div style="display:none;">
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="编辑" OnClientClick="return editBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="删除" OnClientClick="return deleteBtnClientClick();">
                    </asp:LinkButton>
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="新建" OnClientClick="return newBtnClientClick();">
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
