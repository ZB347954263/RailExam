<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BulletinDetail.aspx.cs" Inherits="RailExamWebApp.Notice.BulletinDetail" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>信息公告详细信息</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function deleteBtnClientClick()
        {
            return confirm("您确定要删除此记录吗？");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        信息公告详细信息
                    </div>
                </div>
            </div>
            <div id="content">
                <asp:FormView ID="FormView1" runat="server" DataSourceID="odsBulletinDetail" DataKeyNames="BulletinID"
                    OnItemUpdated="FormView1_ItemUpdated" OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted"
                    OnDataBound="FormView1_DataBound">
                    <EditItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 5%">发布机构</td>
                                <td style="width: 15%"><asp:Label ID="lblOrgNameEdit" runat="server" Text='<%# Eval("OrgName") %>'></asp:Label></td>
                                <td style="width: 5%">发布人</td>
                                <td style="width: 15%"><asp:Label ID="lblEmployeeNameEdit" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>标题</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtTitleEdit" runat="server" Width="70%" Text='<%# Bind("Title") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="“标题”不能为空！"
                                        ToolTip="“标题”不能为空！" ControlToValidate="txtTitleEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>紧急程度</td>
                                <td>
                                    <asp:DropDownList ID="ddlImportanceNameEdit" runat="server" DataSourceID="odsDdlImportanceName" 
                                        DataTextField="ImportanceName" DataValueField="ImportanceID" SelectedValue='<%# Bind("ImportanceID") %>'>
                                    </asp:DropDownList>
                                </td>
                                    <td>发布时间</td>
                                <td>
                                    <uc1:Date ID="dateCreateTimeEdit" runat="server" DateValue='<%# Bind("CreateTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                
                               
                            </tr>
                            <tr>
                             <td>有效天数</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDayCountEdit" runat="server" Width="50%" Text='<%# Bind("DayCount") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="“有效天数”不能为空！"
                                        ToolTip="“有效天数”不能为空！" ControlToValidate="txtDayCountEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                   <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="“有效天数”应为1～9999的整数！"
                                        Display="Dynamic" MaximumValue="9999" MinimumValue="1" Type="Integer" ControlToValidate="txtDayCountEdit">
                                        </asp:RangeValidator>
                                </td>
                              
                              
                            </tr>
                            <tr>
                                <td>内容</td>
                                <td colspan="3"><asp:TextBox ID="txtContentEdit" runat="server" Height="160px" Width="98%" TextMode="MultiLine" Text='<%# Bind("Content") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 5%">发布机构</td>
                                <td style="width: 15%"><asp:Label ID="lblOrgNameInsert" runat="server"></asp:Label></td>
                                <td style="width: 5%">发布人</td>
                                <td style="width: 15%"><asp:Label ID="lblEmployeeNameInsert" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>标题</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtTitleInsert" runat="server" Width="70%" Text='<%# Bind("Title") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="“标题”不能为空！"
                                        ToolTip="“标题”不能为空！" ControlToValidate="txtTitleInsert" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>紧急程度</td>
                                <td>
                                    <asp:DropDownList ID="ddlImportanceNameInsert" runat="server" DataSourceID="odsDdlImportanceName" 
                                        DataTextField="ImportanceName" DataValueField="ImportanceID">
                                    </asp:DropDownList>
                                </td>
                                   <td>发布时间</td>
                                <td>
                                    <uc1:Date ID="dateCreateTimeInsert" runat="server" DateValue='<%# Bind("CreateTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                
                               
                                
                                
                            </tr>
                            <tr>
                              <td>有效天数</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDayCountInsert" runat="server" Width="50%" Text='<%# Bind("DayCount") %>'></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="“有效天数”不能为空！"
                                        ToolTip="“有效天数”不能为空！" ControlToValidate="txtDayCountInsert" Display="Dynamic"></asp:RequiredFieldValidator>
                                        
                                        
                                      
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="“有效天数”应为1～9999的整数！"
                                        MaximumValue="9999" MinimumValue="1" Type="Integer" ControlToValidate="txtDayCountInsert" Display="Dynamic"></asp:RangeValidator>
                                </td>
                                
                                
                                 
                            </tr>
                            <tr>
                                <td>内容</td>
                                <td colspan="3"><asp:TextBox ID="txtContentInsert" runat="server" Height="160px" Width="98%" TextMode="MultiLine" Text='<%# Bind("Content") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 5%">发布机构</td>
                                <td style="width: 15%">
                                    <asp:Label ID="lblOrgName" runat="server" Text='<%# Eval("OrgName") %>'></asp:Label></td>
                                <td style="width: 5%">发布人</td>
                                <td style="width: 15%">
                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>标题</td>
                                <td colspan="3"><asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>紧急程度</td>
                                <td><asp:Label ID="lblImportanceName" runat="server" Text='<%# Eval("ImportanceName") %>'></asp:Label></td>
                                
                                <td>发布时间</td>
                                <td><asp:Label ID="lblCreateTime" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label></td>
                               
                            </tr>
                            <tr>
                                 <td>有效天数</td>
                                <td colspan="3"><asp:Label ID="lblDayCount" runat="server" Text='<%# Eval("DayCount") %>'></asp:Label></td>
                                
                                
                              
                            </tr>
                            <tr>
                                <td>内容</td>
                                <td colspan="3" style="white-space: normal;">
                                    <%# Eval("Content") %></td>
                            </tr>
                            <tr>
                                <td>备注</td>
                                <td colspan="3" style="white-space: normal;">
                                    <%# Eval("Memo") %></td>
                            </tr>
                        </table>
                        <br />
                        <div>
                            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                Text='&lt;img border=0 src="../Common/Image/edit.gif" alt="" /&gt;'>
                            </asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text='&lt;img border=0 src="../Common/Image/delete.gif" alt="" /&gt;' OnClientClick="return deleteBtnClientClick();">
                            </asp:LinkButton>
                            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                                Text='&lt;img border=0 src="../Common/Image/add.gif" alt="" /&gt;'>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="odsBulletinDetail" runat="server" DataObjectTypeName="RailExam.Model.Bulletin"
                    DeleteMethod="DeleteBulletin" InsertMethod="AddBulletin" SelectMethod="GetBulletin"
                    TypeName="RailExam.BLL.BulletinBLL" UpdateMethod="UpdateBulletin" EnableViewState="false">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="BulletinID" QueryStringField="id" Type="Int32"/>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsDdlImportanceName" runat="server" SelectMethod="GetImportances" TypeName="RailExam.BLL.ImportanceBLL">
                </asp:ObjectDataSource>
            </div>
        </div>
    </form>
</body>
</html>
