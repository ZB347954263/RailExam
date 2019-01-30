<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BookImportDel.aspx.cs" Inherits="RailExamWebApp.Book.BookImportDel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>待删除教材</title>
</head>
<body >
    <form id="form1" runat="server">
        <div>
            <table style="width: 98%">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnNew" runat="server" Text="待新增教材" CssClass="button"  OnClick="btnNew_Click"/>
                        <asp:Button ID="lblUpdate" runat="server" Text="待更新教材" OnClick="lblUpdate_Click" CssClass="button" />
                        <asp:Button ID="btnDel" runat="server" Text="待删除教材" Enabled="false" CssClass="button" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnInput" runat="server" Text="删  除" OnClick="btnInput_Click" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 98%;">
            <asp:GridView ID="gvBook" runat="server" AutoGenerateColumns="False" DataKeyNames="bookId"
                HeaderStyle-BackColor="ActiveBorder" CellPadding="4" ForeColor="#333333" GridLines="None">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="教材名称">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="True" Width="30%" />
                        <ItemTemplate>
                            <asp:Label ID="lblBookName" runat="server" Text='<%# Bind("bookName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="教材编号">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lblBookNo" runat="server" Text='<%# Bind("bookNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="教材体系">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="20%" />
                        <ItemTemplate>
                            <asp:Label ID="txtKownName" runat="server" Text='<%# Bind("knowledgeName") %>'></asp:Label>
                            <asp:Label ID="lblKownID" runat="server" Visible="false" Text='<%# Bind("knowledgeId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训类别">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="true" Width="20%" />
                        <ItemTemplate>
                            <asp:Label ID="lblTrainType" runat="server" Text='<%# Bind("trainTypeNames") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编制单位">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrg" runat="server" Text='<%# Bind("publishOrgName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编著者">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lblAuthor" runat="server" Text='<%# Bind("authors") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>

