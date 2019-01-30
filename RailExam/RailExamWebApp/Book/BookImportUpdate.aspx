<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BookImportUpdate.aspx.cs" Inherits="RailExamWebApp.Book.BookImportUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>待更新教材</title>
    <script type="text/javascript">
        function load()
        {
            document.getElementById("checkAll").onclick = checkAll;
        }
    </script>
</head>
<body onload="load()">
    <form id="form1" runat="server">
        <div>
            <table style="width: 98%">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnNew" runat="server" Text="待新增教材" CssClass="button"  OnClick="btnNew_Click"/>
                        <asp:Button ID="lblUpdate" runat="server" Text="待更新教材" Enabled="false" CssClass="button" />
                        <asp:Button ID="btnDel" runat="server" Text="待删除教材" OnClick="btnDel_Click" CssClass="button" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnInput" runat="server" Text="导  入" OnClick="btnInput_Click" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 832px; height: 27px; font-size: 12px">
            &nbsp;&nbsp; <asp:CheckBox ID="checkAll" runat="server" Text="全选" />
            &nbsp;&nbsp;教材名称：
            <asp:TextBox ID="txtName" Columns="8" runat="server"></asp:TextBox>
            教材编号：<asp:TextBox ID="txtNo" Columns="6" runat="server"></asp:TextBox>
            <asp:DropDownList ID="ddlGroup" runat="server">
            </asp:DropDownList>
            编著者：<asp:TextBox ID="txtAuthor" Columns="6" runat="server"></asp:TextBox>
            关键字：<asp:TextBox ID="txtKey" Columns="8" runat="server"></asp:TextBox>
            <asp:ImageButton ID="btnQuery" ImageUrl="../Common/Image/find.gif" runat="server"
                OnClick="btnQuery_Click" />
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
                            <asp:Label ID="lblKownledgeName" runat="server" Text='<%# Bind("knowledgeName") %>'></asp:Label>
                            <asp:HiddenField ID="hfKownledgeId" runat="server"  Value='<%# Eval("knowledgeId") %>' />
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

<script>
    function checkAll()
    {
        var chkId1s = new Array();
       
        chkId1s = document.getElementsByTagName("input")
       
        if (document.getElementById("checkAll").checked)
        {
           for(var i = 0; i < chkId1s.length; i ++)
           {
                if(chkId1s[i].type == "checkbox")
                {
                    chkId1s[i].checked = true;
                }
           }
        }
        else
        {
           for(var i = 0; i < chkId1s.length; i ++)
           {
                if(chkId1s[i].type == "checkbox")
                {
                    chkId1s[i].checked = false;
                }
           }
        }
    }
</script>