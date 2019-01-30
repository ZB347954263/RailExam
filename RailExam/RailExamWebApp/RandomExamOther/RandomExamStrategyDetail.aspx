<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamStrategyDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExamOther.RandomExamStrategyDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>取题范围（按教材章节的各难度分配）</title>

    <script type="text/javascript">
        function showItem()
        {
            var search = window.location.search;
            var id = search.substring(search.indexOf("id=")+3,search.indexOf("&itemType"));
            
            parent.subjectCallback.callback(id);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="contentTableCenter">
            <tr>
                <td align="center">
                    <div style="overflow: auto; height: 410px; vertical-align: top;">
                        <asp:Button runat="server" ID="btnAdd" Text="新  增" CssClass="button" OnClick="btnAdd_Click" /><br /><br />
                        <asp:GridView ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" AutoGenerateColumns="False"
                            Width="97%" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="Grid1_RowCancelingEdit"
                            DataKeyNames="RandomExamStrategyId" OnRowDataBound="Grid1_RowDataBound" OnRowEditing="Grid1_RowEditing"
                            OnRowUpdating="Grid1_RowUpdating" OnRowDeleting="Grid1_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderText="题型">
                                    <ItemStyle HorizontalAlign="Center" Width="8%"  Wrap="false"/>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("RandomExamStrategyId") %>' />
                                        <asp:Label ID="lblTypeName" runat="server" Text='<%# Bind("ItemTypeName") %>'></asp:Label>
                                        <asp:HiddenField ID="hfItemTypeId" runat="server" Value='<%# Bind("ItemTypeId") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hfEditID" runat="server" Value='<%# Bind("RandomExamStrategyId") %>' />
                                        <asp:Label ID="lblEditTypeName" runat="server" Text='<%# Bind("ItemTypeName") %>'></asp:Label>
                                        <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemTypeChange">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfEditItemTypeId" runat="server" Value='<%# Bind("ItemTypeId") %>' />
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="大题名称">
                                    <ItemStyle HorizontalAlign="Center" Width="25%"  Wrap="false"/>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubjectName" runat="server" Text='<%# Bind("SubjectName") %>'></asp:Label>
                                        <asp:HiddenField ID="hfSubjectId" runat="server" Value='<%# Bind("SubjectId") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="最小难度">
                                    <HeaderStyle HorizontalAlign="Center" Width="15%"  Wrap="false"/>
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDiff" runat="server" Text='<%# Bind("ItemDifficultyId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hfItemDiff" runat="server" Value='<%# Bind("ItemDifficultyId") %>' />
                                        <asp:DropDownList ID="ddlItemDiff" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemDiffChange">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="最大难度">
                                    <HeaderStyle HorizontalAlign="Center" Width="15%"  Wrap="false"/>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaxItemDiff" runat="server" Text='<%# Bind("MaxItemDifficultyId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hfMaxItemDiff" runat="server" Value='<%# Bind("MaxItemDifficultyId") %>' />
                                        <asp:DropDownList ID="ddlMaxItemDiff" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMaxItemDiffChange">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="最大题数">
                                    <ItemStyle HorizontalAlign="Center" Width="20%"  Wrap="false"/>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaxCount" runat="server" Text='<%# Bind("MaxCount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="设置题数">
                                    <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCount" runat="server" Text='<%# Bind("ItemCount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtItemCount" Width="90px" runat="server" Text='<%# Bind("ItemCount") %>'></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" Display="None" ErrorMessage="题数必须为整数！"
                                            MaximumValue="99999999" MinimumValue="0" Type="Integer" ControlToValidate="txtItemCount"></asp:RangeValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnModify" runat="server" ImageUrl="../Common/Image/edit_col_edit.gif"
                                            AlternateText="修改" CommandName="Edit"></asp:ImageButton>&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnDel" runat="server" ImageUrl="../Common/Image/edit_col_Delete.gif"
                                            AlternateText="清空组卷策略" CommandName="Delete" OnClientClick="return confirm('您确定要清空该组卷策略吗？');">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="../Common/Image/edit_col_save.gif"
                                            AlternateText="保存" CommandName="Update"></asp:ImageButton>&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Common/Image/edit_col_cancel.gif"
                                            AlternateText="取消" CommandName="Cancel" CausesValidation="False"></asp:ImageButton>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
