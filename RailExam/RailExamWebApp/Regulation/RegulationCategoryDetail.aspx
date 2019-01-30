<%@ Page Language="C#" AutoEventWireup="True" Codebehind="RegulationCategoryDetail.aspx.cs"
    Inherits="RailExamWebApp.Regulation.RegulationCategoryDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>法规类别</title>

    <script type="text/javascript">
           //按钮对象ID回去对象 
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
        
        function deleteBtnClientClick()
        {
            return confirm("您确定要删除此记录吗？");
        }
        
        function updateBtnClientClick()
        {
             if(validateUserInputEdit() == false)
            {
                return false;
            }
            
            return true;
        }
        
        function insertBtnClientClick()
        {
             if(validateUserInputInsert() == false)
            {
               return false; 
            }
            
            return true;
        }
       
              //插入时，验证用户输入数据
       function validateUserInputInsert()
       {
             if($F("fvRegulationCategory_txtCategoryNameInsert") && $F("fvRegulationCategory_txtCategoryNameInsert").value.length == 0)
            {
                alert("法规类别名称不能为空！");
               return false; 
            }         
                         if($F("fvRegulationCategory_txtDescriptionInsert") && $F("fvRegulationCategory_txtDescriptionInsert").value.length > 200)
            {
                alert("描述不能超过200个字！");
               return false; 
            }     
            
            if($F("fvRegulationCategory_txtMemoInsert") && $F("fvRegulationCategory_txtMemoInsert").value.length >50)
            {
                alert("备注不能超过50个字！");
               return false; 
            }               
            
            return true;
       }   
       
       //编辑时，验证用户输入数据 
       function validateUserInputEdit()
       {
            if($F("fvRegulationCategory_txtCategoryNameEdit") && $F("fvRegulationCategory_txtCategoryNameEdit").value.length == 0)
            {
                alert("法规类别名称不能为空！");
               return false; 
            }
                           
            if($F("fvRegulationCategory_txtDescriptionEdit") && $F("fvRegulationCategory_txtDescriptionEdit").value.length > 200)
            {
                alert("描述不能超过200个字！");
               return false; 
            }    
                     
            if($F("fvRegulationCategory_txtMemoEdit") && $F("fvRegulationCategory_txtMemoEdit").value.length >50)
            {
                alert("备注不能超过50个字！");
               return false; 
            }               
            
             return true;
       }   </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        法规类别详细信息
                    </div>
                </div>
            </div>
            <div id="content">
                <asp:FormView ID="fvRegulationCategory" runat="server" DataKeyNames="RegulationCategoryID"
                    DataSourceID="odsRegulationCategoryDetail" OnItemDeleted="fvRegulationCategory_ItemDeleted"
                    OnItemInserted="fvRegulationCategory_ItemInserted" OnItemUpdated="fvRegulationCategory_ItemUpdated">
                    <EditItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 2%;">
                                    法规分类名称
                                </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtCategoryNameEdit" runat="server" Width="98%" Text='<%# Bind("CategoryName") %>' MaxLength="20">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    描述
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDescriptionEdit" runat="server" Width="98%" TextMode="MultiLine"
                                        Text='<%# Bind("Description") %>'>
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    备注
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'>
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
                        <asp:HiddenField ID="hfIdPath" runat="server" Value='<%# Bind("IdPath") %>' />
                        <asp:HiddenField ID="hfLevelNum" runat="server" Value='<%# Bind("LevelNum") %>' />
                        <asp:HiddenField ID="hfOrderIndex" runat="server" Value='<%# Bind("OrderIndex") %>' />
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="false" CommandName="Update"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;' OnClientClick="return updateBtnClientClick();">
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 2%;">
                                    法规分类名称
                                </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtCategoryNameInsert" runat="server" Width="98%" Text='<%# Bind("CategoryName") %>' MaxLength="20"></asp:TextBox>
                               </td>
                            </tr>
                            <tr>
                                <td>
                                    描述
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDescriptionInsert" runat="server" Width="98%" TextMode="MultiLine"
                                        Text='<%# Bind("Description") %>'>
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    备注
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'>
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Bind("ParentId") %>' />
                        <asp:HiddenField ID="hfRegulationCategoryId" runat="server" Value='<%# Bind("RegulationCategoryId") %>' />
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="false" CommandName="Insert"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;' OnClientClick="return insertBtnClientClick();">
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 2%;">
                                    法规分类名称
                                </td>
                                <td style="width: 10%;">
                                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    描述
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
                        <br />
                        <asp:LinkButton ID="btnOk" runat="server" CausesValidation="False" OnClientClick="return window.close();"
                            Text='<img border=0 src="../Common/Image/confirm.gif" />'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:FormView>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsRegulationCategoryDetail" runat="server" DataObjectTypeName="RailExam.Model.RegulationCategory"
            DeleteMethod="DeleteRegulationCategory" InsertMethod="AddRegulationCategory"
            SelectMethod="GetRegulationCategory" TypeName="RailExam.BLL.RegulationCategoryBLL"
            UpdateMethod="UpdateRegulationCategory" EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter Name="regulationCategoryID" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
       <asp:HiddenField  ID="IsCanSave" runat="server" /> 
    </form>
</body>
</html>
