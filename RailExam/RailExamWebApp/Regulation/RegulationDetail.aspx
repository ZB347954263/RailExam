<%@ Page Language="C#" AutoEventWireup="True" Codebehind="RegulationDetail.aspx.cs"
    Inherits="RailExamWebApp.Regulation.RegulationDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>政策法规详细信息</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function selectRegulationCategory()
        {
            var selectedRegulationCategory = window.showModalDialog('../Common/SelectRegulationCategory.aspx', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedRegulationCategory)
            {
                return;
            }
            
            document.getElementById("fvRegulation_hfCategoryID").value = selectedRegulationCategory.split('|')[0];

            if(document.getElementById("fvRegulation_txtCategoryNameEdit"))
                document.getElementById("fvRegulation_txtCategoryNameEdit").value = selectedRegulationCategory.split('|')[1];
            else
                document.getElementById("fvRegulation_txtCategoryNameInsert").value = selectedRegulationCategory.split('|')[1];
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
                        政策法规</div>
                </div>
            </div>
            <div id="content">
                <asp:FormView ID="fvRegulation" runat="server" DataSourceID="odsRegulationDetail"
                    DataKeyNames="RegulationID" OnItemUpdated="fvRegulation_ItemUpdated" OnItemDeleted="fvRegulation_ItemDeleted"
                    OnItemInserted="fvRegulation_ItemInserted">
                    <EditItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 2%;">
                                    法规分类名称</td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtCategoryNameEdit" runat="server" Width="92%" Text='<%# Bind("CategoryName") %>'></asp:TextBox>
                                    <img style="cursor: hand;" onclick="selectRegulationCategory();" src="../Common/Image/search.gif"
                                        alt="选择法规分类" border="0" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="“法规分类名称”不能为空！"
                                        ToolTip="“法规分类名称”不能为空！" ControlToValidate="txtCategoryNameEdit" Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    政策法规名称</td>
                                <td>
                                    <asp:TextBox ID="txtRegulationNameEdit" runat="server"  MaxLength="50" Text='<%# Bind("RegulationName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="“政策法规名称”不能为空！"
                                        ToolTip="“政策法规名称”不能为空！" ControlToValidate="txtRegulationNameEdit" Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    法规编号</td>
                                <td>
                                    <asp:TextBox ID="txtRegulationNoEdit" runat="server" Text='<%# Bind("RegulationNo") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="“法规编号”不能为空！"
                                        ToolTip="“法规编号”不能为空！" ControlToValidate="txtRegulationNoEdit" Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    版本</td>
                                <td>
                                    <asp:TextBox ID="txtVersionEdit" runat="server" Text='<%# Bind("Version") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    文号</td>
                                <td>
                                    <asp:TextBox ID="txtFileNoEdit" runat="server" Text='<%# Bind("FileNo") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    题注</td>
                                <td>
                                    <asp:TextBox ID="txtTitleRemarkEdit" runat="server" Text='<%# Bind("TitleRemark") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    颁布时间</td>
                                <td>
                                    <uc1:Date ID="dateIssueDateEdit" runat="server" DateValue='<%# Bind("IssueDate","{0:yyyy-MM-dd}") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    实施时间</td>
                                <td>
                                    <uc1:Date ID="dateExecuteDateEdit" runat="server" DateValue='<%# Bind("ExecuteDate","{0:yyyy-MM-dd}") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    状态</td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                        <asp:ListItem Value="1" Text="有效"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="失效"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    网址</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtUrlEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Url") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    备注</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfCategoryID" runat="server" Value='<%# Bind("CategoryID") %>' />
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="false" CommandName="Update"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 2%;">
                                    法规分类名称</td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtCategoryNameInsert" runat="server" Width="92%" Text='<%# Bind("CategoryName") %>'></asp:TextBox>
                                    <img style="cursor: hand;" onclick="selectRegulationCategory();" src="../Common/Image/search.gif"
                                        alt="选择法规分类" border="0" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="“法规分类名称”不能为空！"
                                        ToolTip="“法规分类名称”不能为空！" ControlToValidate="txtCategoryNameInsert" Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    政策法规名称</td>
                                <td>
                                    <asp:TextBox ID="txtRegulationNameInsert" runat="server"  MaxLength="50" Text='<%# Bind("RegulationName") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="“政策法规名称”不能为空！"
                                        ToolTip="“政策法规名称”不能为空！" ControlToValidate="txtRegulationNameInsert" Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    法规编号</td>
                                <td>
                                    <asp:TextBox ID="txtRegulationNoInsert" runat="server" Text='<%# Bind("RegulationNo") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="“法规编号”不能为空！"
                                        ToolTip="“法规编号”不能为空！" ControlToValidate="txtRegulationNoInsert" Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    版本</td>
                                <td>
                                    <asp:TextBox ID="txtVersionInsert" runat="server" Text='<%# Bind("Version") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    文号</td>
                                <td>
                                    <asp:TextBox ID="txtFileNoInsert" runat="server" Text='<%# Bind("FileNo") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    题注</td>
                                <td>
                                    <asp:TextBox ID="txtTitleRemarkInsert" runat="server" Text='<%# Bind("TitleRemark") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    颁布时间</td>
                                <td>
                                    <uc1:Date ID="dateIssueDateInsert" runat="server" DateValue='<%# Bind("IssueDate","{0:yyyy-MM-dd}") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    实施时间</td>
                                <td>
                                    <uc1:Date ID="dateExecuteDateInsert" runat="server" DateValue='<%# Bind("ExecuteDate","{0:yyyy-MM-dd}") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    状态</td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                        <asp:ListItem Value="1" Text="有效"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="失效"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    网址</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtUrlInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Url") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    备注</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfCategoryID" runat="server" Value='<%# Bind("CategoryID") %>' />
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="false" CommandName="Insert"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 2%;">
                                    法规分类名称</td>
                                <td style="width: 10%;">
                                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    政策法规名称</td>
                                <td>
                                    <asp:Label ID="lblRegulationName" runat="server"   Text='<%# Eval("RegulationName") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    法规编号</td>
                                <td>
                                    <asp:Label ID="lblRegulationNo" runat="server" Text='<%# Eval("RegulationNo") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    版本</td>
                                <td>
                                    <asp:Label ID="lblVersion" runat="server" Text='<%# Eval("Version") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    文号</td>
                                <td>
                                    <asp:Label ID="lblFileNo" runat="server" Text='<%# Eval("FileNo") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    题注</td>
                                <td>
                                    <asp:Label ID="lblTitleRemark" runat="server" Text='<%# Eval("TitleRemark") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    颁布时间</td>
                                <td>
                                    <asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("IssueDate") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    实施时间</td>
                                <td>
                                    <asp:Label ID="lblExecuteDate" runat="server" Text='<%# Eval("ExecuteDate") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    状态</td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# (int)Eval("Status") == 1 ? "有效" : "失效"%>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    网址</td>
                                <td colspan="3" style="white-space: normal;">
                                    <%# Eval("Url") %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    备注</td>
                                <td colspan="3">
                                    <asp:Label ID="lblMemo" runat="server" Width="98%" Text='<%# Eval("Memo") %>'></asp:Label></td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            Text='&lt;img border=0 src="../Common/Image/edit.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                            Text='&lt;img border=0 src="../Common/Image/delete.gif" alt="" /&gt;' OnClientClick="return deleteBtnClientClick();">
                        </asp:LinkButton>
                        <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                            Text='&lt;img border=0 src="../Common/Image/add.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:FormView>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsRegulationDetail" runat="server" DataObjectTypeName="RailExam.Model.Regulation"
            InsertMethod="AddRegulation" SelectMethod="GetRegulationByRegulationID" TypeName="RailExam.BLL.RegulationBLL"
            UpdateMethod="UpdateRegulation" EnableViewState="False">
            <SelectParameters>
                <asp:QueryStringParameter Name="regulationID" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
