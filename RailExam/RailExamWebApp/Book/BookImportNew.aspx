<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BookImportNew.aspx.cs" Inherits="RailExamWebApp.Book.BookImportNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>待新增教材</title>
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
                        <asp:Button ID="lblAdd" runat="server" Text="待新增教材" CssClass="button" Enabled="false" />
                        <asp:Button ID="lblUpdate" runat="server" Text="待更新教材" OnClick="lblUpdate_Click" CssClass="button" />
                        <asp:Button ID="btnDel" runat="server" Text="待删除教材" OnClick="btnDel_Click" CssClass="button" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnInput" runat="server" Text="导  入" OnClick="btnInput_Click" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 832px; height: 27px; font-size: 12px">
            &nbsp;&nbsp;
            <asp:CheckBox ID="checkAll" runat="server" Text="全选" />
            &nbsp;&nbsp;教材名称：
            <asp:TextBox ID="txtBookName" Columns="8" runat="server"></asp:TextBox>
            教材编号：<asp:TextBox ID="txtBookNo" Columns="6" runat="server"></asp:TextBox>
            <asp:DropDownList ID="ddlGroup" runat="server">
            </asp:DropDownList>
            编著者：<asp:TextBox ID="txtAuthors" Columns="6" runat="server"></asp:TextBox>
            关键字：<asp:TextBox ID="txtKeyWord" Columns="8" runat="server"></asp:TextBox>
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
                            <asp:HiddenField ID="hfBookId" runat="server" />
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
                            <asp:TextBox ID="txtKownledgeName" runat="server" Text='<%# Bind("knowledgeName") %>'
                                ReadOnly="true"></asp:TextBox>
                            <asp:HiddenField ID="hfKownledgeName" runat="server" />
                            <asp:HiddenField ID="hfKownledgeId" runat="server" />
                            <a onclick='selectKnowledge("<%# Eval("bookId") %>", "<%#DataBinder.Eval(gvBook,"Rows.Count")%>")'
                                href="#">
                                <img alt="选择教材体系" src="../Common/Image/search.gif" border="0">
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训类别">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtTrainTypeName" runat="server" Text=''
                                ReadOnly="true"></asp:TextBox>
                            <asp:HiddenField ID="hfTrainTypeName" runat="server" />    
                            <asp:HiddenField ID="hfTrainTypeId" runat="server" />
                            <a onclick='selectTrainType("<%# Eval("bookId") %>", "<%#DataBinder.Eval(gvBook,"Rows.Count")%>")'
                                href="#">
                                <img alt="选择培训类别" src="../Common/Image/search.gif" border="0">
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编制单位">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrgName" runat="server" Text='<%# Bind("publishOrgName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编著者">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="lblAuthors" runat="server" Text='<%# Bind("authors") %>'></asp:Label>
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
    var kownledgeTxts = new Array();
    var kownledgeNames = new Array();
    var kownledgeIds = new Array();
    var bookIds = new Array();
    var trainTypeTxts = new Array();
    var trainTypeNames = new Array();
    var trainTypeIds = new Array();
    var chkIds = new Array();

    <%foreach (GridViewRow r in gvBook.Rows)
    {%>
         chkIds.push('<%= r.FindControl("chSelect").ClientID %>');
    <%}%>

    <%foreach (GridViewRow r in gvBook.Rows)
    {%>
         kownledgeTxts.push('<%= r.FindControl("txtKownledgeName").ClientID %>');
    <%}%>
    
    <%foreach (GridViewRow r in gvBook.Rows)
    {%>
         kownledgeNames.push('<%= r.FindControl("hfKownledgeName").ClientID %>');
    <%}%>
    
    <%foreach (GridViewRow r in gvBook.Rows)
    {%>
         kownledgeIds.push('<%= r.FindControl("hfKownledgeId").ClientID %>');
    <%}%>
    
    <%foreach (GridViewRow r in gvBook.Rows)
    {%>
         bookIds.push('<%= r.FindControl("hfBookId").ClientID %>');
    <%}%>
    
    <%foreach (GridViewRow r in gvBook.Rows)
    {%>
         trainTypeTxts.push('<%= r.FindControl("txtTrainTypeName").ClientID %>');
    <%}%>
    
    <%foreach (GridViewRow r in gvBook.Rows)
    {%>
         trainTypeNames.push('<%= r.FindControl("hfTrainTypeName").ClientID %>');
    <%}%>
    
    <%foreach (GridViewRow r in gvBook.Rows)
    {%>
         trainTypeIds.push('<%= r.FindControl("hfTrainTypeId").ClientID %>');
    <%}%>
    
    function selectKnowledge(str, n)   
    {
        var selectedKnowledge = window.showModalDialog('../Common/SelectKnowledge.aspx', 
            '', 'help:no; status:no; dialogWidth:300px;dialogHeight:600px');
        
        if(! selectedKnowledge)
        {
            return;
        }
        
        document.getElementById(kownledgeIds[n]).value = selectedKnowledge.split('|')[0];
        document.getElementById(kownledgeTxts[n]).value = selectedKnowledge.split('|')[1];
        document.getElementById(kownledgeNames[n]).value = selectedKnowledge.split('|')[1];
        document.getElementById(bookIds[n]).value = str;
    }
    
    function selectTrainType(str, n)   
    { 
        var str = document.getElementById(trainTypeIds[n]).value;
        var selectedTrainType = window.showModalDialog('../Common/MultiSelectTrainType.aspx?id='+str , 
            '', 'help:no; status:no; dialogWidth:300px;dialogHeight:600px');
        
        if(! selectedTrainType)
        {
            return;
        }

        document.getElementById(trainTypeIds[n]).value = selectedTrainType.split('|')[0];
        document.getElementById(trainTypeTxts[n]).value = selectedTrainType.split('|')[1];
        document.getElementById(trainTypeNames[n]).value = selectedTrainType.split('|')[1];
        document.getElementById(bookIds[n]).value = str;
    }
    
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

